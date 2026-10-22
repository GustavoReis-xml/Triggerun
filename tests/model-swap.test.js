const test=require('node:test'),assert=require('node:assert/strict');
const {createModelSwapper}=require('../src/modelSwap');
const {encodeInt}=require('../src/utils');
function fixture(){
 const p={playerId:'10',playerIndex:10,currentHeroId:1015,hp:300,socketType:'SCENE',isCombatReady:true,inMatch:true,currentRoomUID:7,targetMapId:5105,writes:[],write(x){this.writes.push(x);}};
 const peer={...p,playerId:'11',writes:[]},other={...p,playerId:'12',currentRoomUID:8,writes:[]},loading={...peer,playerId:'13',isCombatReady:false,writes:[]};
 const swap=createModelSwapper({activeSockets:[p,peer,other,loading]},[{key:'robot:1109',kind:'robot',id:1109},{key:'hero:1015',kind:'hero',id:1015}]);
 return {p,peer,other,loading,swap};
}
test('monster swap sends only NVM to ready scene peers, preserving gameplay state',()=>{
 const {p,peer,other,loading,swap}=fixture();swap({playerId:10,entityKey:'robot:1109'});
 assert.equal(p.visualHeroId,-1109);assert.equal(p.currentHeroId,1015);assert.equal(p.hp,300);
 assert.deepEqual(p.writes,[`e${encodeInt(10)}:NVM,((${encodeInt(-1109)}))\n`]);
 assert.deepEqual(peer.writes,p.writes);assert.equal(other.writes.length,0);assert.equal(loading.writes.length,0);
});
test('reset sends zero and legacy hero models still work',()=>{
 const {p,swap}=fixture();swap({playerId:10,entityKey:'robot:1109'});swap({playerId:10,visualHeroId:0});
 assert.equal(p.visualHeroId,null);assert.match(p.writes[1],/NVM,\(\(0\)\)/);
 swap({playerId:10,visualHeroId:1015});assert.equal(p.visualHeroId,1015);
});
test('invalid models, dead and loading players rejected without mutation',()=>{
 const {p,swap}=fixture();for(const data of [{entityKey:'npc:1109'},{visualHeroId:-1109},{visualHeroId:'abc'},{visualHeroId:9999}])assert.throws(()=>swap({playerId:10,...data}));
 p.isDead=true;assert.throws(()=>swap({playerId:10,entityKey:'robot:1109'}));
 p.isDead=false;p.isCombatReady=false;assert.throws(()=>swap({playerId:10,visualHeroId:0}));assert.equal(p.writes.length,0);
});
