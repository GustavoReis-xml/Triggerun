const test=require('node:test'),assert=require('node:assert/strict'),path=require('node:path');
const {loadCatalog,createSpawner}=require('../src/entityCatalog');
const {parseArrayList}=require('../src/utils');
const catalog=loadCatalog(path.join(__dirname,'../public/baxi'));
test('catalog covers all shipped NPC, robot and hero definitions without ID collisions',()=>{
    assert.equal(catalog.length,803);assert.equal(new Set(catalog.map(e=>e.key)).size,803);
    assert.equal(catalog.find(e=>e.key==='npc:1101').model,'hl_npc_hong');
    assert.equal(catalog.find(e=>e.key==='robot:1101').heroId,2001);
    assert.equal(catalog.find(e=>e.key==='hero:2001').model,'monsterbq_tps');
    assert.ok(catalog.some(e=>e.key==='npc:1001'));
    for(const e of catalog.filter(e=>e.kind==='robot'))assert.ok(catalog.some(h=>h.kind==='hero'&&h.id===e.heroId),e.key);
});
function fixture() {
    const player={playerId:'1',socketType:'SCENE',isCombatReady:true,inMatch:true,currentRoomUID:7,targetMapId:5105,lastX:0,lastY:0,lastZ:0,writes:[],write(p){this.writes.push(p);}};
    const foreign={...player,playerId:'2',currentRoomUID:8,writes:[]};
    const state={activeSockets:[player,foreign],activeBots:{},spawnedNpcs:{}};
    const calls=[];
    const game={spawnPracticeHeroBot(p,o){calls.push(o);return {playerIndex:50};},spawnAdminRobot(p,id,pos,e){calls.push({id,pos,e});return {playerIndex:60};}};
    const spawn=createSpawner({state,db:{getEntityClassName:()=> 'Npc_Entity'},game,catalog});
    return {spawn,state,player,foreign,calls};
}
test('NPC spawn preserves zero coordinates, native scale, owner and scene isolation',()=>{
    const {spawn,state,player,foreign}=fixture();
    const result=spawn({playerId:1,entityKey:'npc:1101',count:2,scale:200});
    assert.equal(result.ids.length,2);assert.equal(foreign.writes.length,0);
    assert.equal(player.writes.length,2);const npc=state.spawnedNpcs[result.ids[0]];
    assert.equal(npc.x,0);assert.equal(npc.y,0);assert.equal(npc.z,600);assert.equal(npc.currentRoomUID,7);
    const args=parseArrayList(player.writes[0].split(',').slice(1).join(','));
    assert.equal(args[1][6][0],catalog.find(e=>e.key==='npc:1101').scale*2);
    assert.equal(args[1][8],1);
});
test('robot and hero IDs dispatch to different model constructors; native choice is explicit',()=>{
    const {spawn,calls}=fixture();
    spawn({playerId:1,entityKey:'robot:1101'});assert.equal(calls[0].heroId,2001);assert.equal(calls[0].createIndex,1101);
    spawn({playerId:1,entityKey:'hero:2001'});assert.equal(calls[1].heroId,2001);assert.equal(calls[1].createIndex,undefined);
    spawn({playerId:1,entityKey:'robot:1101',behavior:'native'});assert.equal(calls[2].id,1101);assert.equal(calls[2].e.heroId,2001);
});
test('unknown definitions and loading players cannot silently spawn the wrong entity',()=>{
    const {spawn,player}=fixture();assert.throws(()=>spawn({playerId:1,entityKey:'npc:999999'}));
    assert.throws(()=>spawn({playerId:1,entityKey:'hero:2001',behavior:'native'}));
    player.isCombatReady=false;assert.throws(()=>spawn({playerId:1,entityKey:'npc:1101'}));
});

