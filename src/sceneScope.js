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

function invalidate(socket) {
    socket.sceneEpoch = (socket.sceneEpoch || 0) + 1;
    socket.isCombatReady = false;
    socket.sceneLoadPending = false;
    socket.tdmInitialized = false;
    for (const key of ['sceneSpawnTimeout', 'sceneLoadTimeout', 'sceneDoorTimeout', 'heroLockTimeout', 'respawnTimeout']) {
        if (socket[key]) clearTimeout(socket[key]);
        socket[key] = null;
    }
}

function capture(socket) {
    const epoch = socket.sceneEpoch || 0;
    const room = socket.currentRoomUID;
    const map = socket.targetMapId;
    return () => !socket.destroyed && !socket.sceneRetired &&
        (socket.sceneEpoch || 0) === epoch && socket.currentRoomUID === room && socket.targetMapId === map;
}

module.exports = { sameScene, canReceive, invalidate, capture, tagEntity };
