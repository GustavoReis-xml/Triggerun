const sceneScope = require('./sceneScope');
const combat = require('./combatRules');

const HERO_ID = 2000;
const MAX_HP = 1400;
const SUPPORT = Object.freeze({
    1: Object.freeze({ heal: 180, cooldown: 1000, range: 220 }),
    2: Object.freeze({ heal: 360, cooldown: 6000, range: 800 })
});
const casts = new WeakMap();
const strikes = new WeakMap();

function isLamsterWeapon(attacker, weaponId, shootname) {
    return Number(attacker && attacker.currentHeroId) === HERO_ID &&
        Number(weaponId) === HERO_ID && ['1', '2', '4'].includes(String(shootname));
}

// Native circle projectiles report each affected ally. Rate-limit each target
// independently so one pulse can heal several allies without duplicate hits.
function healAlly(source, target, shootname, now = Date.now()) {
    const skill = SUPPORT[shootname];
    if (!skill || Number(source && source.currentHeroId) !== HERO_ID || !target || (source === target && Number(shootname)!==2) ||
        !sceneScope.canReceive(source, source) || source.isDead || source.hp <= 0 ||
        !sceneScope.sameScene(source, target) || target.destroyed || target.sceneRetired ||
        target.inMatch === false || target.isDead || target.hp <= 0 ||
        (target.socketType === 'SCENE' && !target.isCombatReady) ||
        !Number.isFinite(Number(source.force)) || !Number.isFinite(Number(target.force)) ||
        !combat.sameSide(source, target)) return null;
    const coords = [source.lastX, source.lastY, source.lastZ, target.lastX, target.lastY, target.lastZ];
    if (!coords.every(Number.isFinite) || Math.hypot(source.lastX - target.lastX,
        source.lastY - target.lastY, source.lastZ - target.lastZ) > skill.range + 100) return null;
    if (![source.hp, source.maxHp, target.hp, target.maxHp].every(Number.isFinite)) return null;

    let scene = casts.get(source);
    if (!scene || scene.epoch !== source.sceneEpoch || scene.room !== source.currentRoomUID || scene.map !== source.targetMapId) {
        scene = { epoch: source.sceneEpoch, room: source.currentRoomUID, map: source.targetMapId, targets: new WeakMap() };
        casts.set(source, scene);
    }
    const timers = scene.targets.get(target) || {};
    if (now < (timers[shootname] || 0)) return null;
    timers[shootname] = now + skill.cooldown - 100; // Allow ordinary packet timing jitter.
    scene.targets.set(target, timers);
    const healed = source===target ? 0 : Math.max(0, Math.min(skill.heal, target.maxHp - target.hp));
    const selfHealed = Math.max(0, Math.min(45, Math.floor(healed * 0.25), source.maxHp - source.hp));
    target.hp += healed;
    source.hp += selfHealed;
    return { healed, selfHealed };
}

function canStrikeEnemy(source, target, shootname, now = Date.now()) {
    const key=String(shootname), cooldown=key==='1'?1000:key==='2'?6000:key==='4'?800:0;
    if (!cooldown || Number(source && source.currentHeroId)!==HERO_ID || !target || source===target ||
        !sceneScope.canReceive(source, source) || source.isDead || source.hp<=0 || target.isDead || target.hp<=0 ||
        !sceneScope.sameScene(source,target) || combat.sameSide(source,target) || target.destroyed || target.sceneRetired) return false;
    // Practice NPCs store spawn coordinates as x/y/z; moving actors use lastX/Y/Z.
    const coords=[source.lastX,source.lastY,source.lastZ,target.lastX ?? target.x,target.lastY ?? target.y,target.lastZ ?? target.z];
    if (!coords.every(Number.isFinite) || Math.hypot(coords[0]-coords[3],coords[1]-coords[4],coords[2]-coords[5])>(key==='2'?900:320)) return false;
    let scene=strikes.get(source);
    if (!scene || scene.epoch!==source.sceneEpoch || scene.room!==source.currentRoomUID || scene.map!==source.targetMapId) {
        scene={epoch:source.sceneEpoch,room:source.currentRoomUID,map:source.targetMapId,targets:new WeakMap()};strikes.set(source,scene);
    }
    const timers=scene.targets.get(target)||{};
    if(now<(timers[key]||0))return false;
    timers[key]=now+cooldown-100;scene.targets.set(target,timers);return true;
}

module.exports = { HERO_ID, MAX_HP, SUPPORT, isLamsterWeapon, healAlly, canStrikeEnemy };
