// Read-only diagnostic: real server handlers in the existing test VM, no ports,
// database writes, application logger or Oracle connection. Not a Unity emulator.
const fs = require('node:fs');
const path = require('node:path');
const vm = require('node:vm');
const { createRequire } = require('node:module');
const root = path.resolve(__dirname, '..');
const testPath = path.join(root, 'tests/match-lifecycle.test.js');
const source = fs.readFileSync(testPath, 'utf8');
const boundary = source.indexOf('\nfunction lamsterFight()');
if (boundary < 0) throw Error('Test harness boundary changed; review diagnostic adapter.');
const oldExports = 'getFullMatchUserList, roomContainers };';
let harnessSource = source.slice(0, boundary);
if (!harnessSource.includes(oldExports)) throw Error('Server harness exports changed.');
harnessSource = harnessSource.replace(oldExports,
    'getFullMatchUserList, roomContainers, processMatchmakingQueue, broadcastMatchUserList, buildRoomUserList };');
const box = { require: createRequire(testPath), __dirname: path.dirname(testPath), Buffer, module: { exports: {} } };
vm.runInNewContext(harnessSource + '\nmodule.exports = harness;', box, { filename: testPath });
const harness = box.module.exports;
const u = require('../src/utils');
const results = [];
function record(id, invariant, passes, observed) { results.push({ id, invariant, passes: !!passes, observed }); }
function lobby(h, name, id) {
    const s = h.connect(h.socket(name, null, id));
    Object.assign(s, { playerName: name, playerNameEnc: u.encodeHex(name), playerIndex: id,
        playerId: String(id), socketType: 'LOBBY', inMatch: false, currentRoomUID: null });
    h.state.playerSessions[name] = {};
    h.globals.userIndexes[name] = id;
    h.emit(s, 'HallUser');
    return s;
}
function search(h, s, type = 1, mode = 7, map = 0) {
    h.emit(s, `e1:SendMsgToHallServer,(${u.encodeInt(80001)},(${u.encodeInt(80002)},${u.encodeInt(type)},'${s.playerNameEnc}',${u.encodeInt(mode)},0,${u.encodeInt(map)},0))`);
}
function join(h, s, room = 1000) {
    h.emit(s, `e1:SendMsgToHallServer,(${u.encodeInt(20013)},(${u.encodeInt(3303)},('',${u.encodeInt(room)})))`);
}
function loading(h) {
    const s = h.connect(h.socket('joiner', 1000, 10));
    const l = h.socket('joiner', 1000, 10);
    Object.assign(l, { socketType: 'LOBBY', hasConfirmedHero: true });
    h.state.activeSockets.push(l); h.state.playerSessions.joiner = {};
    const r = h.room(1000, [l]);
    h.emit(s, `NGOL'SceneGroup','PlayerUser','10','1',${u.encodeHex('joiner')}`);
    s.isCombatReady = false;
    return { s, l, r };
}
async function main() {
    {
        const h = harness(true); const s = lobby(h, 'joiner', 10);
        search(h, s); // A search is pending before a room becomes available.
        const r = h.room(1000, [h.socket('host')]); join(h, s);
        h.processMatchmakingQueue();
        record('queued-then-jip', 'Room entry must remove an earlier pending search',
            s.currentRoomUID === 1000,
            { requestedRoom: 1000, roomAfterQueueRuns: s.currentRoomUID,
                oldRoomStillContainsPlayer: r.players.some(p => p && p.socket === s) });
    }
    {
        const h = harness(true); const s = lobby(h, 'joiner', 10);
        search(h, s); h.processMatchmakingQueue();
        for (let i = 0; i < 10; i++) await Promise.resolve();
        h.cleanPlayerMatchState(s, 'joiner'); s.writes.length = 0;
        for (const t of h.timers.filter(t => t.ms === 1500)) t.fn();
        record('queue-redirect-after-leave', 'Pending queue redirect must stop after returning to lobby',
            !s.writes.some(p => p.includes(`(${u.encodeInt(1304)},`)),
            { redirectsAfterLeave: s.writes.filter(p => p.includes(`(${u.encodeInt(1304)},`)).length });
    }
    {
        const h = harness(true); const host = h.socket('host');
        const r = h.room(1000, [host]); r.isMatchmaking = true;
        const s = lobby(h, 'joiner', 10); search(h, s); search(h, s);
        const slots = r.players.filter(p => p && p.name === 'joiner').length;
        record('duplicate-backfill', 'One room slot per player after repeated search', slots === 1, { slots });
    }
    {
        const h = harness(true); h.db.MAP_CONFIG[5105] = { name: 'shuijingkuangdong', type: 9 };
        const r = h.room(1000, [h.socket('pve-host')]);
        Object.assign(r, { isMatchmaking: true, mapId: 5105, finalMapId: 5105, mapType: 9, tdmMatch: null });
        const s = lobby(h, 'pvp-player', 10); search(h, s);
        record('cross-mode-backfill', 'PVP search must not enter a PVE room', s.currentRoomUID !== 1000,
            { requestedMode: 7, assignedMap: s.targetMapId, assignedRoom: s.currentRoomUID });
    }
    {
        const h = harness(true); h.db.MAP_CONFIG[5105] = { name: 'shuijingkuangdong', type: 9 };
        const a = lobby(h, 'pvp', 10), b = lobby(h, 'pve', 11);
        search(h, a); search(h, b, 3, 9, 5105); h.processMatchmakingQueue();
        record('mixed-queue', 'PVP and PVE requests must not share a match', a.currentRoomUID !== b.currentRoomUID,
            { pvpRoom: a.currentRoomUID, pveRoom: b.currentRoomUID, pveAssignedAsPve: b.isPveMatch });
        const r = h.roomContainers.get(a.currentRoomUID);
        const mismatch = r.players.map((p, slot) => p && ({ slot, force: p.force, slotForce: slot < 5 ? 1 : 2 }))
            .filter(p => p && p.force !== p.slotForce);
        record('team-slot-mismatch', 'Slot team and actual force must agree', mismatch.length === 0, mismatch);
    }
    {
        const h = harness(true); const { s, r } = loading(h);
        h.emit(s, 'SLoadMapOKMsg'); const timer = h.timers.find(t => t.ms === 1500);
        const endedMatch = r.tdmMatch;
        h.game.endTdmMatch(endedMatch); s.writes.length = 0; timer.fn();
        record('ended-during-load', 'No player spawn into a match that ended while loading',
            !s.isCombatReady && !s.writes.some(p => p.includes(`(${u.encodeInt(10001)},`)),
            { roomState: r.state, matchState: r.tdmMatch?.state, isCombatReady: s.isCombatReady,
                recreatedController: !!r.tdmMatch && r.tdmMatch !== endedMatch });
    }
    {
        const h = harness(true); const { s } = loading(h); s.isCombatReady = true;
        const peer = h.socket('peer', 1000, 11); h.state.activeSockets.push(peer);
        h.emit(s, `eA:SendMsgToServer,(${u.encodeInt(20001)},())`);
        s.destroy();
        record('leave-ghost', 'Explicit leave must destroy the entity in remaining clients',
            peer.writes.some(p => p.startsWith('dA\n')), { peerPackets: peer.writes.length, sceneRetired: s.sceneRetired });
    }
    {
        const h = harness(true); const r = h.room(1000, [h.socket('host')]);
        const s = lobby(h, 'joiner', 10); join(h, s);
        h.emit(s, `e1:SendMsgToHallServer,(${u.encodeInt(1310)},(0,()))`);
        record('hall-return-1310', 'Client return-to-Hall notification must reconcile match membership',
            !s.inMatch && s.currentRoomUID == null,
            { inMatch: s.inMatch, room: s.currentRoomUID, slotRetained: r.players.some(p => p && p.socket === s) });
    }
    {
        const h = harness(true); const a = h.socket('dead', 1000, 10), b = h.socket('new', 1000, 11);
        a.hp = 0; a.isDead = true;
        const packet = h.game.getPlayerSpawnPacket(a, b).split('\n').find(p => p.startsWith('cGamePlayer'));
        const args = u.parseArrayList(packet.slice(packet.indexOf(',') + 1));
        const payload = args[1];
        record('dead-snapshot', 'Snapshot life state must agree with HP and isDead', payload[1][0] === 0,
            { hp: payload[0][1][1], clientLifeState: payload[1][0], serverIsDead: a.isDead });
    }
    {
        const h = harness(true); const a = h.socket('a'); h.state.activeSockets.push(a);
        const r = h.room(1000, [a]); r.tdmMatch.state = 'fight'; h.game.startTdmMatch(a);
        const retry = h.timers.find(t => t.ms === 500); h.cleanPlayerMatchState(a, 'a'); a.writes.length = 0;
        retry.fn(); record('late-tdm-retry', 'TDM retry must stop after leaving', a.writes.length === 0,
            { packetsAfterRetirement: a.writes.length });
    }
    {
        const h = harness(true); const { s } = loading(h);
        h.emit(s, `eA:SendMsgToServer,(${u.encodeInt(9002)},(7,'diagnostic loadmapok:True'))`);
        record('diagnostic-as-ready', 'Diagnostic text must not count as a map-ready command',
            !s.sceneLoadPending, { sceneLoadPending: s.sceneLoadPending, spawnTimers: h.timers.filter(t => t.ms === 1500).length });
    }
    {
        const h = harness(true); const { s } = loading(h);
        const peer = h.socket('peer', 1000, 11); h.state.activeSockets.push(peer);
        const r = h.roomContainers.get(1000), match = r.tdmMatch;
        match.state = 'fight'; match.blueScore = 12; match.fightSeconds = 321;
        h.emit(s, 'SLoadMapOKMsg'); h.timers.find(t => t.ms === 1500).fn();
        const roster = peer.writes.findIndex(p => p.includes(`(${u.encodeInt(90006)},`));
        const spawn = peer.writes.findIndex(p => p.includes('cGamePlayer:A,'));
        record('incumbent-roster', 'Existing client gets refreshed roster before new remote player',
            roster >= 0 && roster < spawn, { rosterPacketIndex: roster, remoteSpawnIndex: spawn });
        const resets = peer.writes.filter(p => /mHeroPlayer|CLoadScene/.test(p) ||
            [90001, 1304, 110301].some(cmd => p.includes(`(${u.encodeInt(cmd)},`)));
        record('normal-tdm-preserves-match', 'Ordinary TDM entry preserves incumbent match and sends no restart',
            r.tdmMatch === match && match.blueScore === 12 && match.fightSeconds === 321 && resets.length === 0,
            { sameMatch: r.tdmMatch === match, blueScore: match.blueScore, fightSeconds: match.fightSeconds,
                incumbentRestartPackets: resets.length });
    }
    {
        const h = harness(true); const { s } = loading(h);
        const peer = h.socket('peer', 1000, 11); h.state.activeSockets.push(peer);
        h.emit(s, `eA:SendMsgToServer,(${u.encodeInt(20014)},(${u.encodeInt(90002)},${u.encodeInt(1017)},1))`);
        const remoteSpawns = peer.writes.filter(p => p.includes('cGamePlayer:A,')).length;
        record('hero-lock-before-load', 'Initial hero choice must wait for map ready before publishing remote spawn',
            remoteSpawns === 0,
            { isCombatReady: s.isCombatReady, remoteSpawnsBeforeMapReady: remoteSpawns,
                enterGamePacketsBeforeMapReady: s.writes.filter(p => p.includes(`(${u.encodeInt(90008)},`)).length });
    }
    {
        const h = harness(true); h.db.MAP_CONFIG[5105] = { name: 'shuijingkuangdong', type: 9 };
        const a = h.socket('host', 1000, 10); a.targetMapId = 5105;
        h.game.startPveMatch(a, 5105); const m = h.globals.activePveMatch;
        m.state = 'fight'; m.round = 2; m.crystal.hp = 1234; a.writes.length = 0;
        const b = h.socket('late', 1000, 11); b.targetMapId = 5105; h.game.startPveMatch(b, 5105);
        record('pve-jip-reset', 'Late PVE entry preserves crystal HP and does not restart incumbent HUD',
            m.crystal.hp === 1234 && !a.writes.some(p => p.includes(`(${u.encodeInt(19001)},`)),
            { crystalHp: m.crystal.hp, gameStartPacketsForHost: a.writes.filter(p => p.includes(`(${u.encodeInt(19001)},`)).length });
    }
    {
        const h = harness(true); h.db.MAP_CONFIG[5007] = { name: 'control', type: 3 };
        const a = h.socket('a', 1000, 10), b = h.socket('b', 1001, 11);
        a.targetMapId = b.targetMapId = 5007; h.game.startControlMatch(a); h.game.startControlMatch(b);
        record('control-room-isolation', 'Different Control rooms must have separate match controllers',
            !h.globals.activeControlMatch.players.includes(a) || !h.globals.activeControlMatch.players.includes(b),
            { controllerRooms: h.globals.activeControlMatch.players.map(p => p.currentRoomUID) });
    }
    // Drain only mocked async sends; no real timers or sockets are started.
    for (let i = 0; i < 12; i++) await Promise.resolve();
    console.log(JSON.stringify({ capturedAt: new Date().toISOString(), scope: 'local mocked connections; real JS handlers; no Unity execution',
        checks: results.length, violatedInvariants: results.filter(r => !r.passes).length, results }, null, 2));
    if (process.argv.includes('--check') && results.some(r => !r.passes)) process.exitCode = 1;
}
main().catch(error => { console.error(error); process.exitCode = 2; });
