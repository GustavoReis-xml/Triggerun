const fs=require('node:fs');
const path=require('node:path');
const {PacketBuilder}=require('./utils');
const scope=require('./sceneScope');
function readRows(file) {
    const xml=fs.readFileSync(file,'utf8');
    return [...xml.matchAll(/<i\s+([^>]+)>/g)].map(m=>Object.fromEntries([...m[1].matchAll(/(\w+)\s*=\s*"([^"]*)"/g)].map(a=>[a[1],a[2].replace(/&amp;/g,'&').replace(/&quot;/g,'"')])));
}
function loadCatalog(dir) {
    const heroes=readRows(path.join(dir,'heroinfo.xml'));
    const byHero=new Map(heroes.map(h=>[Number(h.id),h]));
    const list=heroes.map(h=>({key:`hero:${h.id}`,kind:'hero',id:Number(h.id),name:h.name,model:h.tps_model,category:'Heróis e modelos',source:'heroinfo.xml',heroId:Number(h.id)}));
    for(const n of readRows(path.join(dir,'Npc.xml')))list.push({key:`npc:${n.Index}`,kind:'npc',id:Number(n.Index),name:n.Name,model:n.Resouce_Name,category:'NPCs e objetos de habilidades',source:'Npc.xml',hp:Number(n.HP),scale:Number(n.model_size)||100,type:Number(n.Type),weaponId:Number(n.Weapon_Index)||0,shield:Number(n.Shield)||0});
    for(const r of readRows(path.join(dir,'robot.xml'))) {
        const hero=byHero.get(Number(r.Hero_Id));
        list.push({key:`robot:${r.Index}`,kind:'robot',id:Number(r.Index),name:hero?.name||`Bot ${r.Hero_Id}`,model:hero?.tps_model||'',heroId:Number(r.Hero_Id),ai:r.robot_ai,description:r.des,category:Number(r.monster)>1?'Chefes':Number(r.monster)?'Monstros do Aventura':'Bots de heróis',source:'robot.xml',skin:Number(r.SkinIndex)||1});
    }
    return [...new Map(list.map(e=>[e.key,e])).values()].sort((a,b)=>a.kind.localeCompare(b.kind)||a.id-b.id);
}
function createSpawner({state,db,game,catalog}) {
    const entries=new Map(catalog.map(e=>[e.key,e]));let nextId=900000;
    return function spawn(input) {
        const key=input.entityKey || `npc:${input.npcIndex}`;
        const entry=entries.get(key);if(!entry)throw Error('Entidade não encontrada no catálogo.');
        const player=state.activeSockets.find(s=>String(s.playerId)===String(input.playerId)&&scope.canReceive(s,s));
        if(!player)throw Error('Escolha um jogador que já terminou de carregar a partida.');
        const count=Math.max(1,Math.min(20,Math.trunc(Number(input.count)||1)));
        const scale=Math.max(10,Math.min(500,Math.trunc(Number(input.scale)||100)));
        const native=input.behavior==='native';
        if(native && entry.kind!=='robot')throw Error('IA nativa está disponível nas definições de bots.');
        const ids=[];
        for(let i=0;i<count;i++) {
            const pos={x:(player.lastX??0)+(i%5)*180,y:player.lastY??0,z:(player.lastZ??0)+600+Math.floor(i/5)*180};
            if(entry.kind!=='npc') {
                const bot=native ? game.spawnAdminRobot(player,entry.id,pos,entry) : game.spawnPracticeHeroBot(player,{heroId:entry.heroId,createIndex:entry.kind==='robot'?entry.id:undefined,mode:'static',...pos});
                if(!bot)throw Error('Falha ao criar modelo de personagem.');
                bot.isAdminSpawn=true;bot.adminEntityKey=key;
                // Native RobotAI has no NSS contract; model scaling stays on the existing admin API.
                ids.push(bot.playerIndex);
            } else {
                while(state.spawnedNpcs[nextId]||state.activeBots[nextId]||state.activeSockets.some(s=>Number(s.playerId)===nextId))nextId++;
                const id=nextId++;
                const npc=scope.tagEntity({id,index:entry.id,hp:entry.hp,maxHp:entry.hp,shield:entry.shield,maxShield:entry.shield,
                    ...pos,force:2,masterId:Number(player.playerId),weaponIndex:entry.weaponId,isAdminSpawn:true,adminEntityKey:key},player);
                state.spawnedNpcs[id]=npc;
                const size=Math.round(entry.scale*scale/100);
                const packet=PacketBuilder.create(db.getEntityClassName(entry.id),id,'Initialize',[[1,id,entry.id,2,[pos.x,pos.y,pos.z],[0,0,0],[size,size,size],[0,0,0],Number(player.playerId),entry.hp,entry.shield,1,0,''],[[],[]],0]);
                state.activeSockets.forEach(p=>{if(scope.canReceive(npc,p))p.write(packet);});ids.push(id);
            }
        }
        return {ok:true,ids,message:`${ids.length} × ${entry.name} (${entry.key}) criado(s) na sala do jogador.${entry.kind!=='npc'&&scale!==100?' Modelos de personagem usam tamanho original.':''}`};
    };
}
module.exports={loadCatalog,createSpawner};
