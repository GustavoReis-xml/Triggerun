// A missing room is a private practice session, never a wildcard for all rooms.
function sameScene(a, b) {
    if (!a || !b) return false;
    const ar = a.currentRoomUID;
    const br = b.currentRoomUID;
    if (ar != null || br != null) {
        if (ar == null || br == null || String(ar) !== String(br)) return false;
    } else if (a !== b && (!(a.sceneOwnerName || a.playerName) ||
        (a.sceneOwnerName || a.playerName) !== (b.sceneOwnerName || b.playerName))) {
        return false;
    }
    return a.targetMapId == null || b.targetMapId == null || Number(a.targetMapId) === Number(b.targetMapId);
}

function tagEntity(entity, owner) {
    entity.currentRoomUID = owner.currentRoomUID ?? null;
    entity.targetMapId = owner.targetMapId;
    entity.sceneOwnerName = owner.sceneOwnerName || owner.playerName;
    return entity;
}

function canReceive(source, receiver) {
    return source && !source.sceneRetired && source.inMatch !== false && receiver && receiver.inMatch !== false && receiver.socketType === 'SCENE' && !receiver.destroyed &&
        !receiver.sceneRetired && receiver.isCombatReady === true && sameScene(source, receiver);
}

function clearCombatTimers(socket) {
    if (!socket) return;
    socket.lamsterKit = null;
    socket.lastUltTimeUpdate = Date.now();
    for (const key of ['reloadTimer', 'invisibilityTimer', 'ultimateTimer', 'respawnTimeout']) {
        if (socket[key]) clearTimeout(socket[key]);
        socket[key] = null;
    }
    socket.isReloading = false;
    socket.isRagdoll = false;
}

function invalidate(socket) {
    socket.lamsterKit = null;
    socket.lastUltTimeUpdate = Date.now();
    socket.sceneEpoch = (socket.sceneEpoch || 0) + 1;
    socket.isCombatReady = false;
    socket.sceneLoadPending = false;
    socket.tdmInitialized = false;
    for (const key of ['sceneSpawnTimeout', 'sceneLoadTimeout', 'sceneDoorTimeout', 'heroLockTimeout', 'respawnTimeout', 'reloadTimer', 'invisibilityTimer', 'ultimateTimer']) {
        if (socket[key]) clearTimeout(socket[key]);
        socket[key] = null;
    }
    socket.isReloading = false;
    socket.isRagdoll = false;
    if (socket.activeBuffs) {
        for (const buffId in socket.activeBuffs) {
            const b = socket.activeBuffs[buffId];
            if (b) {
                if (b.timeout) clearTimeout(b.timeout);
                else clearTimeout(b);
                if (b.intervalId) clearInterval(b.intervalId);
            }
        }
        socket.activeBuffs = null;
    }
    socket.activeUltimateBuffs = null;
    if (socket.activeTargetBuffs) {
        for (const key in socket.activeTargetBuffs) {
            const b = socket.activeTargetBuffs[key];
            if (b) {
                if (b.timeout) clearTimeout(b.timeout);
                else clearTimeout(b);
                if (b.intervalId) clearInterval(b.intervalId);
            }
        }
        socket.activeTargetBuffs = null;
    }
}

function capture(socket) {
    const epoch = socket.sceneEpoch || 0;
    const room = socket.currentRoomUID;
    const map = socket.targetMapId;
    const sameRoom = () => (socket.currentRoomUID == null && room == null) ||
        (socket.currentRoomUID != null && room != null && String(socket.currentRoomUID) === String(room));
    const sameMap = () => (socket.targetMapId == null && map == null) ||
        (socket.targetMapId != null && map != null && Number(socket.targetMapId) === Number(map));
    return () => !socket.destroyed && !socket.sceneRetired &&
        (socket.sceneEpoch || 0) === epoch && sameRoom() && sameMap();
}

module.exports = { sameScene, canReceive, invalidate, capture, tagEntity, clearCombatTimers };
