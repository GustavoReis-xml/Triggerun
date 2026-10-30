// Lamster's own IDs: 1201..1203 already belong to David. Wire messages below
// match CPlayerSkill/CPlayerDazhao, not the opcode examples in the design guide.
const scope = require('./sceneScope');
const combat = require('./combatRules');
const { encodeInt: enc } = require('./utils');

const SKILLS = Object.freeze({ containment: 22001, sprint: 22002, ultimate: 22003, pulse: 22004 });
const BUFFS = Object.freeze({ slow: 22011, sprint: 22012, guard: 22013, wave: 22014, heal: 22015, inspiration: 22016 });
const KIT = Object.freeze({
    containment: { id: SKILLS.containment, cd: 12000, duration: 2500, radius: 600 },
    sprint: { id: SKILLS.sprint, cd: 8000, duration: 3000 },
    ultimate: { id: SKILLS.ultimate, cd: 0, duration: 6000, radius: 1000 },
    pulse: { id: SKILLS.pulse, cd: 6000, duration: 0 }
});
const SKILL_IDS = new Set(Object.values(SKILLS));
const BUFF_IDS = new Set(Object.values(BUFFS));

function ready(s) {
    return Number(s?.currentHeroId) === 2000 && scope.canReceive(s, s) && !s.isDead &&
        !s.isRagdoll && !s.isStunned && !s.isFrozen && !s.isSilenced &&
        !s.activeBuffs?.[1015] && s.hp > 0 && [s.lastX, s.lastY, s.lastZ].every(Number.isFinite);
}
function reduceDamage(target, amount, now = Date.now()) {
    let reduction = 0;
    for (const [id, value] of [[BUFFS.sprint, .30], [BUFFS.guard, .40]]) {
        const buff = target?.activeBuffs?.[id];
        if (buff && Number.isFinite(buff.lamsterEndsAt) && now < buff.lamsterEndsAt &&
            buff.lamsterEpoch === (target.sceneEpoch || 0)) reduction = Math.max(reduction, value);
    }
    return amount > 0 ? Math.max(1, Math.round(amount * (1 - reduction))) : amount;
}

function createKit({ state, game, db, now = Date.now, later = setTimeout, cancel = clearTimeout }) {
    function record(s) {
        let r = s.lamsterKit;
        if (!r || r.epoch !== (s.sceneEpoch || 0) || r.room !== s.currentRoomUID || r.map !== s.targetMapId) {
            r = s.lamsterKit = { epoch: s.sceneEpoch || 0, room: s.currentRoomUID, map: s.targetMapId, cooldowns: {} };
        }
        return r;
    }
    function own(s, message) { if (!s.destroyed) s.write(`e${enc(Number(s.playerId || s.playerIndex))}:${message}\n`); }
    function broadcast(s, message) {
        for (const peer of state.activeSockets) if (scope.canReceive(s, peer))
            peer.write(`e${enc(Number(s.playerId || s.playerIndex))}:${message}\n`);
    }
    function skillData(s, id) {
        const cfg = Object.values(KIT).find(k => k.id === id);
        if (!cfg) return;
        const cd = s.nocd ? 0 : cfg.cd;
        const remaining = Math.max(0, (record(s).cooldowns[id] || 0) - now());
        own(s, `SkillDataUpdate,(${enc(31102)},((${enc(id)},0,${enc(remaining ? 0 : 1)},0,1,${enc(remaining)},0,0,0,0,${enc(cd)})))`);
    }
    function spend(s, id) {
        const r = record(s), cfg = Object.values(KIT).find(k => k.id === id);
        if (!cfg || now() < (r.cooldowns[id] || 0)) { skillData(s, id); return false; }
        r.cooldowns[id] = now() + (s.nocd ? 0 : cfg.cd);
        skillData(s, id);
        return true;
    }
    function skillDo(s, id) {
        broadcast(s, `SkillDo,((${enc(id)},(${[s.lastX,s.lastY,s.lastZ].map(v => enc(Math.round(v))).join(',')})),())`);
    }
    function apply(s, target, id, duration) {
        const targetId = target.playerId || target.playerIndex || target.id;
        if (!targetId) return;
        game.applyBuffToEntity(s, targetId, id, duration);
        const b = target.activeBuffs?.[id];
        if (b) { b.lamsterEndsAt = now() + duration; b.lamsterEpoch = target.sceneEpoch || 0; }
    }
    function targets(s, radius, allies) {
        // Active bots include mobile adventure monsters. Pickups, turrets and
        // other static map objects must not receive hero movement/guard buffs.
        const all = new Set([...state.activeSockets.filter(t => t?.socketType === 'SCENE'), ...Object.values(state.activeBots || {})]);
        return [...all].filter(t => t && !t.destroyed && !t.sceneRetired && !t.isDead && t.hp > 0 &&
            t.inMatch !== false && scope.sameScene(s,t) &&
            (t.socketType !== 'SCENE' || t.isCombatReady) && combat.sameSide(s,t) === allies &&
            [t.lastX ?? t.x,t.lastY ?? t.y,t.lastZ ?? t.z].every(Number.isFinite) &&
            Math.hypot(s.lastX-(t.lastX ?? t.x),s.lastY-(t.lastY ?? t.y),s.lastZ-(t.lastZ ?? t.z)) <= radius);
    }
    function cast(s, id) {
        if (!ready(s) || !SKILL_IDS.has(id)) return false;
        if (id === SKILLS.ultimate) return false; // Only 31208 can consume the Q meter.
        if (!spend(s,id)) return false;
        if (id === SKILLS.pulse) return true; // Existing weapon SB/SDG owns the pulse, no duplicate attack.
        skillDo(s,id);
        if (id === SKILLS.containment) {
            apply(s,s,BUFFS.wave,600);
            for (const t of targets(s,KIT.containment.radius,false)) apply(s,t,BUFFS.slow,KIT.containment.duration);
        } else if (id === SKILLS.sprint) apply(s,s,BUFFS.sprint,KIT.sprint.duration);
        return true;
    }
    function pulseFired(s) {
        if (!ready(s)) return;
        const r = record(s);
        // A native WeaponSec skill request usually precedes SB. Both share one
        // displayed cooldown, while damage/healing retain their per-target guards.
        if (now() >= (r.cooldowns[SKILLS.pulse] || 0)) r.cooldowns[SKILLS.pulse] = now() + (s.nocd ? 0 : KIT.pulse.cd);
        if (now() >= (r.pulseVisualAt || 0)) {
            r.pulseVisualAt = now() + 900;
            apply(s,s,BUFFS.inspiration,600);
        }
        skillData(s,SKILLS.pulse);
    }
    function energy(s, value) {
        own(s, `SendMsgToAllClient,(${enc(50001)},(${enc(50004)},(0,${enc(value)},0)))`);
    }
    function endUltimate(s) {
        const r = s.lamsterKit;
        if (!r?.ultimateActive) return;
        r.ultimateActive = false;
        if (s.ultimateTimer) cancel(s.ultimateTimer);
        s.ultimateTimer = null;
        if (scope.canReceive(s,s)) broadcast(s,`SendMsgToAllClient,(${enc(50001)},(${enc(50005)},(0,'',0)))`);
    }
    function ultimate(s) {
        if (!ready(s)) return false;
        const r = record(s);
        const max = db.heroDazhaoMap[2000]?.[0]?.maxEnergy || 10000;
        if (r.ultimateActive || s.ultimateTimer || (s.ultEnergy || 0) < max) {
            energy(s,s.ultEnergy || 0); return false;
        }
        s.ultEnergy = 0; r.ultimateActive = true; r.ultimateUntil = now() + KIT.ultimate.duration; energy(s,0);
        broadcast(s,`SendMsgToAllClient,(${enc(50001)},(${enc(50005)},(1,'',${enc(KIT.ultimate.duration)})))`);
        // SkillBaseMe completes instantly and leaves weapons usable. Calling
        // SuperSkillUpdate as well would cause an extra 31103 and a second cast.
        skillDo(s,SKILLS.ultimate);
        for (const t of targets(s,KIT.ultimate.radius,true)) {
            // Remove movement slows; don't break freezes/stuns through a cast.
            for (const id of Object.keys(t.activeBuffs || {})) {
                const cfg = db.buffsMap[id];
                if (cfg?.type === 8 && cfg.value < 0) game.cleanupBuff(t, t.playerId || t.playerIndex || t.id, Number(id), true);
            }
            apply(s,t,BUFFS.guard,KIT.ultimate.duration);
            apply(s,t,BUFFS.sprint,KIT.sprint.duration);
        }
        const current = scope.capture(s);
        s.ultimateTimer = later(() => { if (current() && s.lamsterKit === r) endUltimate(s); },KIT.ultimate.duration);
        return true;
    }
    return { cast, ultimate, endUltimate, pulseFired, skillData, targets };
}

module.exports = { SKILLS, BUFFS, KIT, SKILL_IDS, BUFF_IDS, ready, reduceDamage, createKit };
