// Regenerate the server's native PvE model/path lookup from the shipped client XML.
const fs = require('node:fs');
const path = require('node:path');
const root = path.join(__dirname, '..');
function rows(file) {
    return [...fs.readFileSync(path.join(root,'public/baxi',file),'utf8').matchAll(/<i\s+([^>]+)\/?\s*>/g)]
        .map(m=>Object.fromEntries([...m[1].matchAll(/([\w]+)\s*=\s*"([^"]*)"/g)].map(a=>[a[1],a[2]])));
}
const ai=Object.fromEntries(rows('robot_ai.xml').map(r=>[r.Index,r]));
const data={};
for (const r of rows('robot.xml')) {
    if (!Number(r.monster)) continue;
    if (!ai[r.robot_ai]) throw Error(`Missing native robot AI ${r.robot_ai}`);
    const point=ai[r.robot_ai].Move_Paths.split(';')[0].split(',').slice(1,4).map(Number);
    data[r.Index]={hero:Number(r.Hero_Id),ai:Number(r.robot_ai),
        pos:point.length===3 && point.every(Number.isFinite) ? point : null,boss:Number(r.monster)>1};
}
fs.writeFileSync(path.join(root,'src/pveRobots.json'),JSON.stringify(data,null,2)+'\n');
console.log(`Generated ${Object.keys(data).length} native PvE robot definitions`);
