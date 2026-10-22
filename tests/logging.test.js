const {test}=require('node:test');
const assert=require('node:assert/strict');
const fs=require('node:fs');
const os=require('node:os');
const path=require('node:path');
const {spawnSync}=require('node:child_process');
const {createRotatingLog}=require('../src/rotatingLog');
function temporary(t){const dir=fs.mkdtempSync(path.join(os.tmpdir(),'tr-logs-'));t.after(()=>fs.rmSync(dir,{recursive:true,force:true}));return dir;}
test('rotation bounds file count and preserves complete ordered records across reopening',t=>{
 const dir=temporary(t),file=path.join(dir,'events.log');
 let write=createRotatingLog(file,{maxBytes:128,backups:2});
 for(let i=0;i<20;i++){if(i===10)write=createRotatingLog(file,{maxBytes:128,backups:2});assert.equal(write(JSON.stringify({i,text:'á'.repeat(10)})+'\n'),true);}
 assert.equal(fs.readdirSync(dir).length,3);
 const rows=[file+'.2',file+'.1',file].flatMap(p=>{assert.ok(fs.statSync(p).size<=128);return fs.readFileSync(p,'utf8').trim().split('\n').map(JSON.parse);});
 assert.equal(rows.at(-1).i,19);for(let i=1;i<rows.length;i++)assert.equal(rows[i].i,rows[i-1].i+1);
});
test('telemetry survives two server processes with different run IDs and bounded large records',t=>{
 const dir=temporary(t);const logger=path.resolve(__dirname,'../src/logger.js');
 for(let i=0;i<2;i++){
  const child=spawnSync(process.execPath,['-e',`const l=require(${JSON.stringify(logger)});l.logTelemetry('TEST',{player:'king',message:'x'.repeat(100000)});`],{env:{...process.env,TR_LOG_DIR:dir},encoding:'utf8'});
  assert.equal(child.status,0,child.stderr);
 }
 const lines=fs.readFileSync(path.join(dir,'ai_telemetry.log'),'utf8').trim().split('\n'),rows=lines.map(JSON.parse);
 assert.equal(rows.filter(r=>r.type==='SERVER_START').length,2);
 assert.equal(new Set(rows.map(r=>r.runId)).size,2);
 assert.equal(rows.filter(r=>r.truncated).length,2);
 assert.ok(lines.every(l=>Buffer.byteLength(l)<65536));
});
test('filesystem failure does not throw into gameplay or recursively log',t=>{
 const dir=temporary(t);let errors=0;const write=createRotatingLog(dir,{onError:()=>{errors++;throw Error('reporter');}});
 assert.equal(write('first\n'),false);assert.equal(write('second\n'),false);assert.equal(errors,1);
});
test('trace command filters a player through rotation, skipping legacy and broken lines',t=>{
 const dir=temporary(t);fs.writeFileSync(path.join(dir,'ai_telemetry.log.1'),'old header\n'+JSON.stringify({player:'king',room:7,seq:1})+'\n');
 fs.writeFileSync(path.join(dir,'ai_telemetry.log'),[JSON.stringify({player:'other',room:7,seq:2}),JSON.stringify({player:'king',room:7,seq:3}),'broken'].join('\n'));
 const run=spawnSync(process.execPath,[path.resolve(__dirname,'../tools/trace-player.cjs'),'--dir',dir,'--player','king'],{encoding:'utf8'});
 assert.equal(run.status,0,run.stderr);assert.deepEqual(run.stdout.trim().split('\n').map(JSON.parse).map(r=>r.seq),[1,3]);
});
