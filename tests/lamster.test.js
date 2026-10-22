const { test } = require('node:test');
const assert = require('node:assert/strict');
const fs = require('node:fs');
const path = require('node:path');
const lamster = require('../src/lamster');

function player(extra = {}) {
    return { currentHeroId: 2000, hp: 700, maxHp: 1400, force: 1,
        currentRoomUID: 1000, targetMapId: 5204, socketType: 'SCENE',
        isCombatReady: true, inMatch: true, lastX: 0, lastY: 0, lastZ: 0, ...extra };
}

test('support pulse heals multiple allies with independent cooldowns and actual self healing', () => {
    const a = player(); const b = player({ hp: 1390 }); const c = player({ hp: 500 });
    assert.deepEqual(lamster.healAlly(a,b,1,1000), { healed: 10, selfHealed: 2 });
    assert.deepEqual(lamster.healAlly(a,c,1,1000), { healed: 180, selfHealed: 45 });
    assert.equal(lamster.healAlly(a,c,1,1100), null);
    assert.deepEqual(lamster.healAlly(a,c,2,1200), { healed: 360, selfHealed: 45 });
    assert.equal(lamster.healAlly(a,c,2,5800), null);
    assert.equal(lamster.healAlly(a,c,2,7200).healed, 360);
    assert.equal(a.hp, 837);
});

test('support rejects self, enemy, dead, non-ready, missing-position and other-room targets', () => {
    const a = player();
    assert.equal(lamster.healAlly(a,a,1), null);
    for(const extra of [{force:2}, {isDead:true}, {hp:0}, {sceneRetired:true},
        {currentRoomUID:1001}, {targetMapId:5010}, {isCombatReady:false},
        {lastX:801}, {lastX:undefined}, {inMatch:false}, {destroyed:true}]) {
        const b = player(extra); const hp = b.hp;
        assert.equal(lamster.healAlly(a,b,1), null);
        assert.equal(b.hp, hp);
    }
    assert.equal(a.hp, 700);
});

test('Lamster supports living allied bots and never heals from an obsolete weapon', () => {
    const a = player(); const b = player({socketType:undefined, isCombatReady:undefined});
    assert.equal(lamster.healAlly(a,b,1).healed, 180);
    assert.equal(lamster.isLamsterWeapon(a,2000,3),false);
    assert.equal(lamster.isLamsterWeapon(a,1019,1),false);
    assert.equal(lamster.isLamsterWeapon(player({currentHeroId:1019}),2000,1),false);
    assert.equal(lamster.healAlly(a,b,4),null);
});

test('support cooldown belongs to the current scene and cannot leak across scene replacement', () => {
    const a = player(); const b = player();
    assert.ok(lamster.healAlly(a,b,1,1000));
    assert.equal(lamster.healAlly(a,b,1,1100),null);
    a.sceneEpoch = 1;
    assert.ok(lamster.healAlly(a,b,1,1100));
});

function block(dir, file) {
    return fs.readFileSync(path.join(__dirname,'..',dir,file),'utf8')
        .match(/<i\s+(?:id|heroid)="2000"[\s\S]*?(?:\/>|<\/i>)/)[0];
}

test('Lamster XMLs use native model and controller, distinct heals, no phantom ultimate and fresh cache keys', () => {
    const root = path.join(__dirname,'..');
    const manifest = fs.readFileSync(path.join(root,'files_dir.xml'),'utf8');
    const readBlock = (dir,file) => {
        const xml=fs.readFileSync(path.join(root,dir,file),'utf8');
        return file==='heroinfo.xml' ? xml.match(/<i id="2000"[^>]*\/>/)[0] : xml.match(/<i\s+(?:id|heroid)="2000"[\s\S]*?<\/i>/)[0];
    };
    for(const file of ['heroinfo.xml','weapon.xml','heroskill.xml','heroskillinfo.xml','dazhao.xml']) {
        assert.equal(readBlock('public/baxi',file),readBlock('zh',file));
        for(const dir of ['baxi','zh']) assert.ok(manifest.includes(`path="${dir}/${file}" crc="${file==='dazhao.xml'?'26091201':file==='buff.xml'?'26100704':'26100705'}"`));
    }
    const hero=readBlock('public/baxi','heroinfo.xml');
    assert.match(hero,/heroclass="LuoMo"/); assert.match(hero,/career="4"/);
    assert.match(hero,/fps_model="male_fps"/);
    assert.doesNotMatch(readBlock('public/baxi','dazhao.xml'),/<skill /);
    const weapons=readBlock('public/baxi','weapon.xml');
    const entries=[...weapons.matchAll(/<weapon\s[\s\S]*?<\/weapon>/g)].map(m=>m[0]);
    assert.equal(entries.length,3);
    for(const [i,id] of [1,2,4].entries()) {
        assert.match(entries[i],new RegExp(`shootname="${id}"`));
        assert.match(entries[i],/muzzlename="m1"/); assert.match(entries[i],/bullet_type="2"/);
        assert.match(entries[i],/keygroup="0"/);
        const heal=lamster.SUPPORT[id];
        if(heal) {
            assert.match(entries[i],new RegExp(`damageparamfriend="${heal.heal}"`));
            assert.match(entries[i],new RegExp(`damageparamenemy="${id===1?120:250}"`));
            assert.match(entries[i],new RegExp(`bulletboomrange="${heal.range*10}"`));
            const rate=Number(entries[i].match(/launchershootspeed="(\d+)"/)[1]);
            assert.equal(60000/rate,heal.cooldown);
        } else assert.match(entries[i],/damageparamenemy="120"/);
    }
});

test('Lamster selection has native skin, weapon and neutral emote in both languages',()=>{
 for(const dir of ['public/baxi','zh']){
  const unlock=fs.readFileSync(path.join(__dirname,'..',dir,'herounlock.xml'),'utf8').match(/<i heroid="2000"[\s\S]*?<\/i>/)[0];
  assert.match(unlock,/default="1"/);assert.match(unlock,/isshow="1"/);
  assert.match(unlock,/<skin id="1"[^>]*skinname="male_tps"[^>]*controller="male_tps_animator"[^>]*skinname1="male_fps"[^>]*controller1="male_fps_animator"/);
  assert.match(unlock,/<weapon id="1"[^>]*default="1"/);assert.match(unlock,/<emotion id="1"[^>]*animparm="0"/);
  const xml=fs.readFileSync(path.join(__dirname,'..',dir,'weapon.xml'),'utf8').match(/<i id="2000"[\s\S]*?<\/i>/)[0];
  const weapons=[...xml.matchAll(/<weapon\s[\s\S]*?<\/weapon>/g)].map(m=>m[0]);
  assert.match(weapons[0],/layertype="0"/);assert.match(weapons[0],/damagelayertype="0"/);
  assert.match(weapons[1],/layertype="0"/);assert.match(weapons[1],/damagelayertype="0"/);
 }
});

test('Lamster short-range healing and offensive cooldown reset across scenes',()=>{
 const a=player(),enemy=player({force:2});
 assert.equal(lamster.healAlly(a,player({lastX:321}),1,1000),null);
 assert.equal(lamster.canStrikeEnemy(a,enemy,1,1000),true);
 assert.equal(lamster.canStrikeEnemy(a,enemy,1,1100),false);
 assert.equal(lamster.canStrikeEnemy(a,enemy,2,1100),true);
 assert.equal(lamster.canStrikeEnemy(a,player(),1,1100),false);
 a.sceneEpoch=3;assert.equal(lamster.canStrikeEnemy(a,enemy,1,1100),true);
});

test('Lamster attacks practice NPC positions without accepting missing coordinates',()=>{
 const source=player();
 const target={id:100000,index:1122,hp:1000,maxHp:1000,force:2,currentRoomUID:1000,targetMapId:5204,x:700,y:0,z:0};
 assert.equal(lamster.canStrikeEnemy(source,target,2,10000),true);
 assert.equal(lamster.canStrikeEnemy(source,target,2,10001),false);
 assert.equal(lamster.canStrikeEnemy(source,{...target,x:950},2,10000),false);
 assert.equal(lamster.canStrikeEnemy(source,{...target,x:undefined},2,10000),false);
 assert.equal(lamster.canStrikeEnemy(source,{...target,lastX:950,lastY:0,lastZ:0},2,10000),false,'new movement position overrides spawn');
});
