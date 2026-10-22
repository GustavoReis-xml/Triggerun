const test=require('node:test'),assert=require('node:assert/strict'),fs=require('fs');
const {assets,resolveAsset}=require('../src/lamsterAssets');
test('Lamster icon requests resolve with Unity casing/version/query and stay isolated',()=>{
 for(const [url,file] of Object.entries(assets)){
  assert.equal(resolveAsset('/1.3.5/StreamingAssets/'+url.toUpperCase()+'?v=135'),file);
  const bytes=fs.readFileSync('public/lamster/'+file);assert.equal(bytes.subarray(0,8).toString('hex'),'89504e470d0a1a0a');
  assert.ok(fs.readFileSync('files_dir.xml','utf8').toLowerCase().includes('path="'+url+'"'));
 }
 for(const url of ['/1/StreamingAssets/Icon/headsmall/male_1.txt','/1/StreamingAssets/../../secret.txt','/other/icon/headsmall/lamster_1.txt'])assert.equal(resolveAsset(url),null);
});
test('selection and HUD portraits preserve native dimensions',()=>{
 for(const [file,w,h] of [['selection.png',90,90],['portrait.png',200,308],['head.png',109,158],['kill-head.png',44,44]]){const b=fs.readFileSync('public/lamster/'+file);assert.equal(b.readUInt32BE(16),w);assert.equal(b.readUInt32BE(20),h);}
});
