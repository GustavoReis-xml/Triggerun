// Explicit local resources: Unity decodes PNG bytes from its .txt asset URLs.
const assets=Object.freeze({
  "icon/dtsitemico/lamster_1.txt": "portrait.png",
  "icon/herounlock/skin/lamster_1.txt": "selection.png",
  "icon/selectheroui/headbig/lamster_1.txt": "portrait.png",
  "icon/headsmall/lamster_1.txt": "head.png",
  "icon/headjiaodou/lamster_1.txt": "kill-head.png",
  "icon/dtsitemico/lamstertop.txt": "portrait.png",
  "icon/herounlock/emotion/lamster_1.txt": "selection.png",
  "icon/herounlock/weapon/lamster_primary.txt": "primary.png",
  "icon/weaponicon/lamster_primary.txt": "primary.png",
  "icon/skillicon/lamster_primary.txt": "primary.png",
  "icon/skillicon/lamster_rally.txt": "rally.png",
  "icon/skillicon/lamster_passive.txt": "rally.png"
});
function resolveAsset(urlPath){
 const marker='/streamingassets/';
 const value=String(urlPath).split('?')[0].toLowerCase();
 const index=value.indexOf(marker);
 return index<0?null:(assets[value.slice(index+marker.length)]||null);
}
module.exports={assets,resolveAsset};
