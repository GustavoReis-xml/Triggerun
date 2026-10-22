const logger = require('./logger');
let connectionSequence = 0;
function attach(socket) {
    if (!socket.connectionId) socket.connectionId = `${logger.runId || 'test'}:${++connectionSequence}`;
    return socket.connectionId;
}
function event(socket, type, details = {}) {
    attach(socket);
    logger.logTelemetry(type, { ...details, connectionId: socket.connectionId,
        player: socket.playerName || null, playerId: socket.playerIndex ?? null,
        channel: socket.socketType || null, room: socket.currentRoomUID ?? null,
        map: socket.targetMapId ?? null, epoch: socket.sceneEpoch || 0,
        phase: socket.matchPhase || 'connected' });
}
// Own selection/loading flags; room membership and protocol keep their owners.
function transition(socket, phase, reason) {
    const previous = socket.matchPhase || 'connected';
    if (!['connected', 'lobby', 'queued', 'selecting', 'loading', 'playing', 'leaving', 'ended', 'closed'].includes(phase)) throw new Error(`Unknown match phase ${phase}`);
    if ((socket.sceneRetired || socket.destroyed || previous === 'closed' || (previous === 'ended' && phase === 'playing')) && ['selecting', 'loading', 'playing'].includes(phase)) {
        event(socket, 'MATCH_TRANSITION_REJECTED', { previous, next: phase, reason });
        return false;
    }
    socket.matchPhase = phase;
    if (phase === 'selecting') {
        socket.waitingForHeroLock = true; socket.hasConfirmedHero = false; socket.isCombatReady = false;
    } else if (phase === 'loading') {
        socket.waitingForHeroLock = false; socket.isCombatReady = false;
    } else if (phase === 'playing') {
        socket.waitingForHeroLock = false; socket.sceneLoadPending = false; socket.isCombatReady = true;
    } else if (['leaving', 'ended', 'closed', 'lobby', 'queued'].includes(phase)) socket.isCombatReady = false;
    if (previous !== phase) event(socket, 'MATCH_TRANSITION', { previous, next: phase, reason });
    return true;
}
module.exports = { attach, event, transition };
