const { test } = require('node:test');
const assert = require('node:assert/strict');
const fs = require('node:fs');
const path = require('node:path');
const { createKit, SKILLS, BUFFS, reduceDamage } = require('../src/lamsterHero');
const scope = require('../src/sceneScope');
const utils = require('../src/utils');

function player(id, extra={}) {
    return { playerId:String(id), playerIndex:id, currentHeroId:2000, socketType:'SCENE',
        currentRoomUID:77, targetMapId:5204, sceneEpoch:2, isCombatReady:true, inMatch:true,
        force:1, hp:800, maxHp:1400, lastX:0,lastY:0,lastZ:0,writes:[],
        write(s){this.writes.push(s);}, ...extra };
}
function harness() {
    let clock=10000;
    const a=player(10),ally=player(11,{currentHeroId:1017,lastX:500}),enemy=player(12,{force:2,lastX:590});
    const foreign=player(13,{currentRoomUID:78});
    const state={activeSockets:[a,ally,enemy,foreign],activeBots:{},spawnedNpcs:{}};
    const db={heroDazhaoMap:{2000:[{maxEnergy:10000}]},buffsMap:{
        22011:{type:8,value:-35},22012:{type:8,value:40},22013:{type:0,value:0},
        1014:{type:8,value:-40},1013:{type:11,value:0}
    }};
    const applied=[],timers=[];
    const game={applyBuffToEntity(s,id,buff,duration){
        const t=state.activeSockets.find(p=>Number(p.playerId)===Number(id));
        if(!t)return;
        applied.push({source:s.playerId,target:t.playerId,buff,duration});
        t.activeBuffs??={};t.activeBuffs[buff]={};
    },cleanupBuff(t,id,buff){delete t.activeBuffs[buff];}};
    const kit=createKit({state,game,db,now:()=>clock,later:(fn,ms)=>{const t={fn,ms};timers.push(t);return t;},cancel:t=>t.cancelled=true});
    return {a,ally,enemy,foreign,state,db,applied,timers,kit,advance:ms=>clock+=ms};
}

test('containment slows only living enemies in 6m, with no HP change or foreign-room packets',()=>{
    const h=harness();const {a,ally,enemy,foreign,kit}=h;
    const distant=player(14,{force:2,lastX:601}),dead=player(15,{force:2,hp:0,isDead:true});
    h.state.activeSockets.push(distant,dead);
    const lobby=player(16,{socketType:'LOBBY',force:2});h.state.activeSockets.push(lobby);
    assert.ok(kit.cast(a,SKILLS.containment));
    assert.ok(enemy.activeBuffs[BUFFS.slow]);assert.ok(a.activeBuffs[BUFFS.wave]);
    assert.equal(ally.activeBuffs,undefined);assert.equal(distant.activeBuffs,undefined);assert.equal(dead.activeBuffs,undefined);
    assert.equal(lobby.activeBuffs,undefined,'lobby connection cannot be selected as a combat actor');
    for(const t of [a,ally,enemy,foreign])assert.equal(t.hp,800);
    assert.equal(foreign.writes.length,0);
    assert.equal(kit.cast(a,SKILLS.containment),false);
    h.advance(11999);assert.equal(kit.cast(a,SKILLS.containment),false);
    h.advance(1);assert.equal(kit.cast(a,SKILLS.containment),true);
});
test('authoritative cooldown display ignores early CDEND and does not share E/Shift/RMB cooldowns',()=>{
    const {a,kit,advance}=harness();
    kit.cast(a,SKILLS.containment);assert.ok(kit.cast(a,SKILLS.sprint));assert.ok(kit.cast(a,SKILLS.pulse));
    kit.skillData(a,SKILLS.containment);
    const raw=a.writes.at(-1).match(/SkillDataUpdate,\((.*)\)/)[1];
    const data=utils.parseArrayList(raw)[1][0];
    assert.equal(data[0],SKILLS.containment);assert.equal(data[2],0);assert.equal(data[5],12000);assert.equal(data[10],12000);
    advance(8000);assert.ok(kit.cast(a,SKILLS.sprint));assert.equal(kit.cast(a,SKILLS.containment),false);
});
test('ultimate needs full energy, affects nearby allies and cleanses slows without another heal',()=>{
    const {a,ally,enemy,foreign,kit,timers}=harness();
    assert.equal(kit.ultimate(a),false);a.ultEnergy=9999;assert.equal(kit.ultimate(a),false);
    a.ultEnergy=10000;ally.activeBuffs={1014:{},1013:{}};
    assert.ok(kit.ultimate(a));assert.equal(a.ultEnergy,0);assert.equal(kit.ultimate(a),false);
    for(const t of [a,ally]){
        assert.ok(t.activeBuffs[BUFFS.guard]);assert.ok(t.activeBuffs[BUFFS.sprint]);assert.equal(t.hp,800);
    }
    assert.equal(ally.activeBuffs[1014],undefined);assert.ok(ally.activeBuffs[1013],'does not remove unrelated CC');
    assert.equal(enemy.activeBuffs,undefined);assert.equal(foreign.activeBuffs,undefined);assert.equal(foreign.writes.length,0);
    assert.ok(a.writes.some(w=>w.includes(':SkillDo,')));assert.ok(!a.writes.some(w=>w.includes(':SuperSkillUpdate,')),'no second cast');
    assert.equal(timers[0].ms,6000);timers[0].fn();assert.equal(a.ultimateTimer,null);
});
test('FFA opponents with the same wire force never receive ally protection',()=>{
    const {a,ally,enemy,state,kit}=harness();
    for(const t of state.activeSockets)t.targetMapId=5301;
    a.ultEnergy=10000;kit.ultimate(a);
    assert.ok(a.activeBuffs[BUFFS.guard]);assert.equal(ally.activeBuffs,undefined);
    kit.cast(a,SKILLS.containment);assert.ok(ally.activeBuffs[BUFFS.slow]);assert.ok(enemy.activeBuffs[BUFFS.slow]);
});
test('defensive bonuses use the strongest effect and expire at the server deadline',()=>{
    const {a,kit,advance}=harness();kit.cast(a,SKILLS.sprint);
    assert.equal(reduceDamage(a,100,10000),70);
    a.ultEnergy=10000;kit.ultimate(a);assert.equal(reduceDamage(a,100,10000),60);
    advance(3001);assert.equal(reduceDamage(a,100,13001),60);
    advance(3000);assert.equal(reduceDamage(a,100,16001),100);
    a.sceneEpoch++;assert.equal(reduceDamage(a,100,10000),100);
});
test('death, loading, stale scene and foreign hero cannot cast or consume the ultimate',()=>{
    for(const state of [{isDead:true},{hp:0},{isCombatReady:false},{sceneRetired:true},{destroyed:true},
        {inMatch:false},{isStunned:true},{isFrozen:true},{isRagdoll:true},{currentHeroId:1037},{lastX:undefined}]){
        const {a,kit}=harness();Object.assign(a,state,{ultEnergy:10000});
        assert.equal(kit.cast(a,SKILLS.sprint),false);assert.equal(kit.ultimate(a),false);assert.equal(a.ultEnergy,10000);
    }
});
test('old ultimate timers cannot mutate a replaced scene or a later cast',()=>{
    const {a,kit,timers}=harness();a.ultEnergy=10000;kit.ultimate(a);const old=timers[0];
    scope.invalidate(a);a.isCombatReady=true;a.inMatch=true;a.ultEnergy=10000;kit.ultimate(a);
    const next=a.lamsterKit;old.fn();assert.equal(a.lamsterKit,next);assert.ok(next.ultimateActive);
    assert.equal(kit.cast(a,1201),false,'David skill is not part of this kit');
    assert.equal(kit.cast(a,SKILLS.ultimate),false,'Q cannot be requested through E/Shift protocol');
});
test('new XMLs preserve every other hero/skill/buff and use native supported classes',()=>{
    const root=path.join(__dirname,'..');
    const read=(file)=>fs.readFileSync(path.join(root,file),'utf8');
    const baseline=require('./fixtures/lamster007-existing-xml.json');
    for(const folder of ['public/baxi','zh'])for(const name of ['heroinfo','playerskill','buff','dazhao','heroskill','heroskillinfo','weapon']){
        const file=`${folder}/${name}.xml`,after=read(file);
        // Remove only the added Lamster records, compare all pre-existing records verbatim.
        const normalize=text=>text.replace(/<i\s+(?:id|heroid)="(?:2000|2200[1-4]|2201[1-6])"[^>]*(?:\/>|>[\s\S]*?<\/i>)/g,'').replace(/\s/g,'');
        assert.equal(require('node:crypto').createHash('sha256').update(normalize(after)).digest('hex'),baseline[file],file);
    }
    const ps=read('public/baxi/playerskill.xml');
    for(const id of Object.values(SKILLS))assert.match(ps,new RegExp(`<i id="${id}"[^>]*skillclass="0"`));
    assert.match(ps,/<i id="22003"[^>]*dokey="0"[^>]*icon="0"/,'Q uses central ring only');
    const buff=read('public/baxi/buff.xml');
    assert.match(buff,/<i id="22015"[^>]*type="0"[^>]*value="0"/,'healing feedback cannot add an extra 500 HP');
});
