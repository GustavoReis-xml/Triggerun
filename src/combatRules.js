const maps = require('./ffaMaps.json');
// Keep wire teams unchanged: GamePlayer.setHeroForce already handles type 11.
// Server combat relationships must also treat every other owner as an enemy.
function isFreeForAll(entity) {
    const map = Number(entity && entity.targetMapId);
    return Object.hasOwn(maps, map);
}

function owner(entity) {
    return entity && (entity.masterId || entity.playerId || entity.playerIndex || entity.id);
}

function sameSide(a, b, teamResult = Number(a && a.force) === Number(b && b.force)) {
    if (!isFreeForAll(a) && !isFreeForAll(b)) return teamResult;
    if (!a || !b) return false;
    if (a === b) return true;
    const id = owner(a);
    return id != null && Number(id) > 0 && String(id) === String(owner(b));
}

// Raw server-team fields also drive names, sounds and outlines in the client.
function visibleForce(entity, viewer) {
    if (!isFreeForAll(entity)) return entity.force || 1;
    return sameSide(entity, viewer) ? 1 : 2;
}

module.exports = { isFreeForAll, sameSide, visibleForce, mapIds: Object.keys(maps).map(Number) };
