const { test } = require('node:test');
const assert = require('node:assert/strict');
const fs = require('node:fs');
const path = require('node:path');
const vm = require('node:vm');
const scope = require('../src/sceneScope');
const utils = require('../src/utils');

function harness(fastPackets = false) {
    const state = { activeSockets: [], activeBots: {}, spawnedNpcs: {}, ipNames: {}, ipIndexes: {},
        playerSessions: {}, playerInventory: {}, playerEquipment: {},
        getLastMapId: () => 5204, setLastMapId() {}, setLastLobbySocket() {} };
    const timers = [];
    const logs = [];
    const globals = { userIndexes: {}, globalPlayerCounter: 0 };
    let accept;
    const db = { MAP_CONFIG: { 5204: { name: 'jiaodouchang_02_3', type: 7 }, 5010: { name: 'dz_jjc', type: 7 }, 5301: { name: 'wudingzhengduo_baked_3_xiugai', type: 11 } },
        getHeroMaxHp: () => 2000, generateFullHeroData: () => 'Ft,(1,0,2)',
        generateHeroSkillsData: () => '()', generateHeroDazhaoData: () => '()', weaponsMap: {},
        mapBases: {}, heroDazhaoMap: {}, buffsMap: {}, heroSkillsMap: {}, getEntityClassName: () => 'Npc' };
    const noop = () => {};
    const shared = { state, db, globals, telemetry: [], rewards: [] };
    function load(file, extra = '') {
        const module = { exports: {} };
        const sandbox = {
            module, exports: module.exports, global: globals, Buffer, Date, Math, Set, Map,
            console: { log: (...args) => logs.push(args.join(' ')), error: (...args) => logs.push(args.join(' ')), warn: noop },
            setTimeout: (fn, ms) => { const t = { fn, ms }; timers.push(t); return t; },
            clearTimeout: t => { if (t) t.cancelled = true; },
            setInterval: (fn, ms) => ({ fn, ms }), clearInterval: noop,
            require(name) {
                if (name === './state') return state;
                if (name === './database') return db;
                if (name === './utils') return { ...utils, logPacket: noop,
                    ...(fastPackets ? { sleep: async () => {}, sendPacket: async (s, data) => s.write(data) } : {}) };
                if (name === './sceneScope') return scope;
                if (name === './combatRules') return require('../src/combatRules');
                if (name === './pveRobots.json') return require('../src/pveRobots.json');
                if (name === './ffaMaps.json') return require('../src/ffaMaps.json');
                if (name === './sdgProtocol') return require('../src/sdgProtocol');
                if (name === './matchLifecycle') return shared.lifecycle;
                if (name === './clientStream') return require('../src/clientStream');
                if (name === './lamster') return require('../src/lamster');
                if (name === './dbPersistence') return { loadPlayer: () => ({ level: 1, equipment: {} }), savePlayer: noop, addMatchRewards: (name, data) => { shared.rewards.push({name, ...data}); return null; } };
                if (name === './gameLogic') return shared.game;
                if (name === './logger') return { runId: 'test', logTelemetry: (type,data)=>shared.telemetry.push({type,...data}) };
                if (name === './packetSniffer') return { inspect: noop };
                if (name === './config') return { SERVER_IP: '127.0.0.1', tcpPort: 0 };
                if (name === '../bots') return {};
                if (name === 'net') return { createServer: fn => { accept = fn; return { listen: noop }; } };
                return require(name);
            }
        };
        vm.runInNewContext(fs.readFileSync(path.join(__dirname, '..', file), 'utf8') + extra, sandbox, { filename: file });
        return module.exports;
    }
    shared.lifecycle = load('src/matchLifecycle.js');
    shared.game = load('src/gameLogic.js');
    const server = load('src/socketServer.js', '\nmodule.exports = { getSceneSocketsForRoom, cleanPlayerMatchState, updateSessionFromSocket, getFullMatchUserList, roomContainers };');
    function socket(name, room = 1000, id = 1) {
        const events = {};
        const s = { playerName: name, playerNameEnc: utils.encodeHex(name), playerIndex: id, playerId: String(id),
            currentRoomUID: room, targetMapId: 5204, finalMapId: 5204, force: 1,
            socketType: 'SCENE', inMatch: true, isCombatReady: true, currentHeroId: 1015,
            hp: 123, maxHp: 2000, lastX: 0, lastY: 400, lastZ: 900, writes: [],
            remoteAddress: '127.0.0.1', destroyed: false,
            write(data) { this.writes.push(data); return true; },
            on(event, fn) { events[event] = fn; }, setNoDelay() {}, setKeepAlive() {},
            destroy() { this.destroyed = true; if (events.close) events.close(); }, events };
        return s;
    }
    function connect(s) { accept(s); s.writes.length = 0; return s; }
    function emit(s, text) {
        let data = Buffer.from(text + '\r\n');
        if (s.isEncrypted) data = Buffer.from(data.map(b => (((b ^ 227) + 37) & 255) ^ 173));
        s.events.data(data);
    }
    function room(id, players = []) {
        const r = { roomUID: id, state: 4, mapId: 5204, finalMapId: 5204, roomName: 'test',
            creatorName: 'host', password: '', mapType: 7, maxPlayers: 10, createTime: '2026/09/11 00:00:00',
            players: Array(10).fill(null), tdmMatch: shared.game.createTdmMatchInstance(id, 5204) };
        players.forEach((s, i) => { r.players[i] = { socket: s, name: s.playerName }; r.tdmMatch.players.push(s); });
        server.roomContainers.set(id, r); return r;
    }
    return { ...shared, ...server, socket, connect, emit, room, timers, logs };
}

function lamsterFight() {
    const h = harness();
    const a = h.connect(h.socket('medic', 1000, 10));
    h.emit(a, 'PlayerUser');
    Object.assign(a, { socketType: 'SCENE', currentHeroId: 2000, isCombatReady: true,
        playerId: '10', playerIndex: 10, inMatch: true, hp: 700, maxHp: 1400 });
    const b = h.socket('ally', 1000, 11); b.hp = 500; b.maxHp = 1000;
    const enemy = h.socket('enemy', 1000, 12); enemy.force = 2; enemy.hp = 700;
    const foreign = h.socket('foreign', 1001, 13); foreign.hp = 400;
    if (!h.state.activeSockets.includes(a)) h.state.activeSockets.push(a);
    h.state.activeSockets.push(b, enemy, foreign);
    h.db.weaponsMap[2000] = { cartridgePublic: 0, 1: { damage: 120 }, 2: { damage: 250 }, 4: { damage: 120 } };
    function pulse(shootname, targets = [b], weapon = 2000) {
        // CBulletMe.sendDamageToServer -> HeroPlayer.DamageMsgToServer -> TosFun.
        const hits = targets.map(t=>[10, Number(t.playerId), 0, 100, [0,0,0], 0, [0,0,0], 0, 'body', [0,400,900]]);
        const payload = [hits, shootname, 0, 1, 0, 0, 0, 1, 10, 1, 0, 1, 0, 6000, weapon];
        h.emit(a, utils.PacketBuilder.rpc(10, 'SDG', [[payload]]).trim());
    }
    return { h, a, b, enemy, foreign, pulse };
}

test('Lamster native SDG heals allies, damages enemies and isolates other rooms', () => {
    const { h,a,b,enemy,foreign,pulse } = lamsterFight();
    pulse(1, [b, enemy, foreign]);
    assert.equal(b.hp, 680, h.logs.join('\n'));
    assert.equal(a.hp, 745);
    assert.equal(enemy.hp, 580);
    assert.equal(foreign.hp, 400);
    assert.equal(a.matchHeal, 225);
    assert.ok(b.writes.some(p=>p.startsWith('eB:SetHp,')));
    assert.equal(foreign.writes.length, 0);
    pulse(1, [b]);
    assert.equal(b.hp, 680, 'duplicate hit must not repeat the pulse');
});

test('Lamster secondary burst, full health and melee remain distinct on the real handler', () => {
    const { a,b,enemy,pulse } = lamsterFight();
    pulse(2);
    assert.equal(b.hp, 860);
    assert.equal(a.hp, 745);
    pulse(4, [enemy]);
    assert.equal(enemy.hp, 580);
    assert.equal(a.hp, 745, 'melee must not heal Lamster');
    pulse(4, [b]);
    assert.equal(b.hp, 860, 'melee must not hurt allies');
    b.hp = b.maxHp;
    pulse(1);
    assert.equal(a.hp, 745, 'full-health allies cannot fuel self healing');
});

test('Lamster stops healing when loading, dead, out of range or after returning to lobby', () => {
    for (const change of [
        ({a})=>{a.isCombatReady=false;},
        ({a})=>{a.isDead=true;},
        ({b})=>{b.lastX=2000;},
        ({b})=>{b.isDead=true;b.hp=0;},
        ({b})=>{b.isCombatReady=false;},
        ({a})=>{a.inMatch=false;},
        ({a})=>{a.sceneRetired=true;}
    ]) {
        const f = lamsterFight(); change(f); const hp = f.b.hp;
        f.pulse(1); assert.equal(f.b.hp, hp); assert.equal(f.a.hp,700);
    }
});

test('room ids are strict; same-map rooms and unrelated practice sessions never mix', () => {
    const h = harness(); const a = h.socket('a'); const b = h.socket('b', 1001);
    assert.equal(scope.sameScene(a, b), false);
    assert.equal(scope.sameScene(a, { ...a, currentRoomUID: '1000' }), true);
    assert.equal(scope.sameScene(a, { ...b, currentRoomUID: null }), false);
    assert.equal(scope.sameScene({ ...a, currentRoomUID: null }, { ...b, currentRoomUID: null }), false);
    h.state.activeSockets.push(a, b);
    assert.equal(h.getSceneSocketsForRoom(9999).length, 0);
    assert.equal(h.getSceneSocketsForRoom(null).length, 0);
});

test('damage reaches only ready members of its room, including delayed-loading peers', () => {
    const h = harness(); const a = h.socket('a'); const b = h.socket('b', 1000, 2);
    const foreign = h.socket('foreign', 1001, 3); const loading = h.socket('loading', 1000, 4);
    loading.isCombatReady = false; h.state.activeSockets.push(a, b, foreign, loading);
    h.game.sendEntityDamageUpdate(1, a, 20, 2);
    assert.equal(a.writes.length, 1); assert.equal(b.writes.length, 1);
    assert.equal(foreign.writes.length, 0); assert.equal(loading.writes.length, 0);
});

test('spawn uses base64 NetID and live HP/zero coordinates; never destroys the receiver', () => {
    const h = harness(); const a = h.socket('a', 1000, 10); const b = h.socket('b', 1000, 11);
    const own = h.game.getPlayerSpawnPacket(a, a);
    assert.match(own, /^eA:SendMsgToClient/);
    const remote = h.game.getPlayerSpawnPacket(a, b);
    assert.match(remote, /^dA\ncGamePlayer:A,/);
    assert.ok(remote.includes(`(1,${utils.encodeInt(123)},${utils.encodeInt(2000)},`));
    assert.ok(remote.includes(`(${utils.encodeInt(5204)},0,${utils.encodeInt(900)},${utils.encodeInt(400)},`));
    assert.equal(h.game.getPlayerSpawnPacket(a, h.socket('c', 1001)), '');
});

test('leaving retires the old scene and cannot overwrite the lobby session afterwards', () => {
    const h = harness(); const a = h.socket('a'); const lobby = { ...a, socketType: 'LOBBY' };
    h.state.activeSockets.push(a, lobby); h.state.playerSessions.a = { currentRoomUID: 1000, inMatch: true };
    h.room(1000, [a]); const valid = scope.capture(a);
    h.cleanPlayerMatchState(lobby, 'a');
    assert.equal(a.sceneRetired, true); assert.equal(a.isCombatReady, false); assert.equal(valid(), false);
    a.currentRoomUID = 999; a.hasConfirmedHero = true;
    h.updateSessionFromSocket(a);
    assert.equal(h.state.playerSessions.a.currentRoomUID, null);
    assert.equal(h.state.playerSessions.a.inMatch, false);
});

test('stale or missing room never falls back to another active TDM match', () => {
    const h = harness(); const a = h.socket('a'); const r = h.room(1000, [a]);
    h.globals.activeTdmMatch = r.tdmMatch;
    assert.equal(h.game.getTdmMatch(h.socket('b', 9999), false), null);
    assert.equal(h.game.getTdmMatch(h.socket('solo', null), false), null);
    assert.equal(h.game.getTdmMatch(a, false), r.tdmMatch);
});

test('disconnect uses original room before clearing membership; other room stays untouched', () => {
    const h = harness(); const a = h.connect(h.socket('a')); const b = h.socket('b', 1000, 2);
    const c = h.socket('c', 1001, 3); h.state.activeSockets.push(b, c);
    const ra = h.room(1000, [a, b]); const rb = h.room(1001, [c]);
    h.globals.activeTdmMatch = rb.tdmMatch;
    // The closure learns its socket type through the normal handshake classification.
    a.events.data(Buffer.from("NGOL'SceneGroup','PlayerUser','1','1',61\n"));
    a.isCombatReady = true; a.currentRoomUID = 1000; a.targetMapId = 5204;
    b.writes.length = 0; c.writes.length = 0;
    a.destroy();
    assert.ok(b.writes.some(p => p.startsWith('d1\n')));
    assert.equal(c.writes.length, 0);
    assert.equal(rb.tdmMatch.players.length, 1);
    assert.equal(ra.tdmMatch.players.includes(a), false);
});

test('packet scheduling is cancelled after leaving the match', async () => {
    const h = harness(); const a = h.socket('a');
    const pending = utils.sendPacket(a, 'e1:SendMsgToClient,(1,())\n', 10);
    scope.invalidate(a); a.sceneRetired = true;
    await pending; assert.equal(a.writes.length, 0);
});

test('TCP reconstruction handles fragmented UTF-8, joined commands and NGOL encryption switch', () => {
    const stream = require('../src/clientStream'); const socket = {}; const messages = [];
    const encrypted = Buffer.from(Buffer.from('ação\r\nsecond\r\n').map(b => (((b ^ 227) + 37) & 255) ^ 173));
    const wire = Buffer.concat([Buffer.from('NGOL\r\n'), encrypted]);
    for (const byte of wire) stream.consume(socket, Buffer.from([byte]), msg => {
        messages.push(msg); if (msg === 'NGOL') socket.isEncrypted = true;
    });
    assert.deepEqual(messages, ['NGOL', 'ação', 'second']);
    const coalesced = []; const peer = {};
    stream.consume(peer, wire, msg => { coalesced.push(msg); if (msg === 'NGOL') peer.isEncrypted = true; });
    assert.deepEqual(coalesced, messages);
});

test('late entry initializes once, skips loading/foreign players and accepts real combat map 5010', () => {
    const h = harness(); const a = h.connect(h.socket('a', 1000, 10));
    const lobby = h.socket('a', 1000, 10); lobby.socketType = 'LOBBY'; lobby.hasConfirmedHero = true;
    lobby.targetMapId = 5010; lobby.finalMapId = 5010;
    h.state.activeSockets.push(lobby); h.state.playerSessions.a = {};
    const r = h.room(1000, [lobby]); r.mapId = r.finalMapId = 5010;
    h.emit(a, "NGOL'SceneGroup','PlayerUser','10','1',61");
    assert.equal(a.waitingForHeroLock, false);
    assert.ok(a.writes.some(p => p === 'mHeroPlayer:A\n'));
    a.isCombatReady = false;
    const ready = h.socket('ready', 1000, 11); ready.targetMapId = 5010;
    const loading = h.socket('loading', 1000, 12); loading.targetMapId = 5010; loading.isCombatReady = false;
    const foreign = h.socket('foreign', 1001, 13); foreign.targetMapId = 5010;
    h.state.activeSockets.push(ready, loading, foreign);
    h.emit(a, 'SLoadMapOKMsg'); h.emit(a, 'SLoadMapOKMsg');
    const spawnTimers = h.timers.filter(t => t.ms === 1500);
    assert.equal(spawnTimers.length, 1);
    spawnTimers[0].fn();
    assert.equal(a.isCombatReady, true);
    assert.equal(a.tdmInitialized, true);
    assert.ok(a.writes.some(p => p.includes('cGamePlayer:B,')));
    assert.equal(a.writes.some(p => /cGamePlayer:[CD],/.test(p)), false);
    assert.ok(ready.writes.some(p => p.includes('cGamePlayer:A,')));
    assert.equal(loading.writes.length, 0); assert.equal(foreign.writes.length, 0);
});

test('return during scene loading cancels spawn and ignores subsequent stale combat input', () => {
    const h = harness(); const a = h.connect(h.socket('a'));
    const lobby = h.socket('a'); lobby.socketType = 'LOBBY'; lobby.hasConfirmedHero = true;
    h.state.activeSockets.push(lobby); h.state.playerSessions.a = {}; h.room(1000, [lobby]);
    h.emit(a, "NGOL'SceneGroup','PlayerUser','1','1',61"); a.isCombatReady = false;
    h.emit(a, 'SLoadMapOKMsg');
    const timer = h.timers.find(t => t.ms === 1500); assert.ok(timer);
    h.emit(a, `e1:SendMsgToServer,(${utils.encodeInt(20001)},())`);
    const count = a.writes.length;
    timer.fn();
    h.emit(a, 'e1:SP,((1,2,3),0,1)');
    assert.equal(a.writes.length, count);
    assert.equal(a.sceneRetired, true);
    assert.equal(lobby.inMatch, false);
    assert.equal(h.state.playerSessions.a.currentRoomUID, null);
});

for (const quick of [false, true]) test(`${quick ? 'quick backfill' : 'room-list JIP'} sends current roster and final map; leaving cancels redirect`, async () => {
    const h = harness(true);
    const host = h.socket('host', 1000, 11); host.hasConfirmedHero = true;
    const r = h.room(1000, [host]); r.mapId = 5010; r.finalMapId = 5204; r.isMatchmaking = quick;
    r.tdmMatch.state = 'fight';
    const other = h.socket('foreign', 1001, 12); h.room(1001, [other]);
    h.state.activeSockets.push(host, other);
    const s = h.connect(h.socket('joiner', null, 10));
    Object.assign(s, { playerName: 'joiner', playerNameEnc: utils.encodeHex('joiner'), playerIndex: 10,
        playerId: '10', socketType: 'LOBBY', inMatch: false, currentRoomUID: null });
    h.state.playerSessions.joiner = {};
    h.globals.userIndexes.joiner = 10;
    h.emit(s, 'HallUser');
    const request = quick
        ? `e1:SendMsgToHallServer,(${utils.encodeInt(80001)},(${utils.encodeInt(80002)},1,'${s.playerNameEnc}',7,0,0,0))`
        : `e1:SendMsgToHallServer,(${utils.encodeInt(20013)},(${utils.encodeInt(3303)},('',${utils.encodeInt(1000)})))`;
    h.emit(s, request);
    for (let i = 0; i < 6; i++) await Promise.resolve();
    assert.equal(s.currentRoomUID, 1000);
    const matchPacket = s.writes.find(p => p.includes(`(${utils.encodeInt(90001)},`));
    assert.ok(matchPacket);
    assert.ok(matchPacket.includes(`,${utils.encodeInt(5204)},7,'jiaodouchang_02_3'`));
    assert.ok(matchPacket.includes(host.playerNameEnc));
    assert.equal(matchPacket.includes(other.playerNameEnc), false);
    assert.equal(utils.parseArrayList(s.userData)[9], 0);
    assert.equal(s.waitingForHeroLock, true);
    h.cleanPlayerMatchState(s, 'joiner');
    const count = s.writes.length;
    for (const timer of h.timers.filter(t => t.ms === 800)) timer.fn();
    assert.equal(s.writes.length, count);
});

test('practice NPCs have unique ids and their updates stay with their owner', () => {
    const h = harness(); const a = h.socket('a', null); const b = h.socket('b', null, 2);
    h.state.activeSockets.push(a, b);
    h.game.spawnPracticeNpcs(a, true); h.game.spawnPracticeNpcs(b, true);
    assert.equal(new Set([...a.practiceNpcIds, ...b.practiceNpcIds]).size, 6);
    a.writes.length = 0; b.writes.length = 0;
    const id = a.practiceNpcIds[0]; const npc = h.state.spawnedNpcs[id];
    h.game.sendEntityHpUpdate(a, id, npc, false);
    assert.equal(a.writes.length, 1); assert.equal(b.writes.length, 0);
});

for (const quick of [false, true]) for (const finish of ['lock', 'client-timeout', 'server-timeout']) {
    test(`${quick ? 'backfill' : 'room JIP'}: selection preview and ${finish} load before spawning`, async () => {
        const h = harness(true);
        const host = h.socket('host', 1000, 11);
        host.hasConfirmedHero = true;
        h.state.activeSockets.push(host);
        const room = h.room(1000, [host]); room.isMatchmaking = quick;
        const match = room.tdmMatch; match.state = 'fight'; match.timeRemaining = 321;
        const lobby = h.connect(h.socket('joiner', null, 10));
        Object.assign(lobby, { playerName: 'joiner', playerNameEnc: utils.encodeHex('joiner'),
            playerIndex: 10, playerId: '10', socketType: 'LOBBY', inMatch: false, currentRoomUID: null });
        h.state.playerSessions.joiner = {}; h.globals.userIndexes.joiner = 10;
        h.emit(lobby, 'HallUser');
        h.emit(lobby, quick
            ? `e1:SendMsgToHallServer,(${utils.encodeInt(80001)},(${utils.encodeInt(80002)},1,'${lobby.playerNameEnc}',7,0,0,0))`
            : `e1:SendMsgToHallServer,(${utils.encodeInt(20013)},(${utils.encodeInt(3303)},('',${utils.encodeInt(1000)})))`);
        for (let i = 0; i < 6; i++) await Promise.resolve();
        assert.equal(lobby.hasConfirmedHero, false);
        const entrant = h.socket('joiner', 1000, 10);
        entrant.isCombatReady = false; // Fresh TCP connection, before spawn.
        h.connect(entrant);
        h.emit(entrant, `NGOL'SceneGroup','PlayerUser','10','1',${lobby.playerNameEnc}`);
        assert.equal(entrant.waitingForHeroLock, true);
        assert.equal(entrant.isCombatReady, false);
        const send = (sub, ...args) => h.emit(entrant,
            `eA:SendMsgToServer,(${utils.encodeInt(20014)},(${[sub,...args].map(utils.encodeInt).join(',')}))`);
        send(90002, 2000, 0);
        const preview = entrant.writes.find(p => p.includes(`(${utils.encodeInt(90002)},`));
        assert.ok(preview, h.logs.join('\n'));
        assert.equal(entrant.currentHeroId, 2000);
        assert.equal(host.writes.some(p=>p.includes('cGamePlayer:A,')), false);
        // No fabricated map-ready message: the server must actually request loading first.
        if (finish === 'lock') send(90002, 2000, 1);
        else if (finish === 'client-timeout') send(90004, 0);
        else {
            const fallback = h.timers.find(t=>t.ms===60000 && !t.cancelled);
            assert.ok(fallback); fallback.fn();
        }
        assert.equal(entrant.waitingForHeroLock, false, h.logs.join('\n'));
        assert.equal(entrant.hasConfirmedHero, true);
        assert.ok(entrant.writes.some(p=>p.includes(`(${utils.encodeInt(90004)})`)));
        const loading = h.timers.filter(t=>t.ms===1000 && !t.cancelled);
        assert.equal(loading.length, 1);
        send(90002, 1015, 1); send(90004, 0);
        assert.equal(entrant.currentHeroId, 2000, 'duplicate lock cannot change a loading hero');
        assert.equal(host.writes.some(p=>p.includes('cGamePlayer:A,')), false);
        loading[0].fn();
        assert.ok(entrant.writes.some(p=>p.startsWith('e:CLoadScene,')));
        h.emit(entrant, 'SLoadMapOKMsg');
        const spawn = h.timers.find(t=>t.ms===1500 && !t.cancelled);
        assert.ok(spawn); spawn.fn();
        assert.equal(entrant.isCombatReady, true);
        assert.ok(host.writes.some(p=>p.includes('cGamePlayer:A,')));
        assert.equal(room.tdmMatch, match);
        assert.equal(match.timeRemaining, 321);
        assert.equal(host.writes.some(p=>/CLoadScene|mHeroPlayer/.test(p)), false);
    });
}

for (const cancel of ['leave', 'ended']) test(`late selection fallback ignores ${cancel}`, () => {
    const h = harness();
    const lobby = h.socket('joiner', 1000, 10);
    Object.assign(lobby, { socketType: 'LOBBY', waitingForHeroLock: true, hasConfirmedHero: false });
    h.state.activeSockets.push(lobby); h.state.playerSessions.joiner = {};
    const room = h.room(1000, [lobby]);
    const entrant = h.socket('joiner', 1000, 10); entrant.isCombatReady = false;
    h.connect(entrant);
    h.emit(entrant, `NGOL'SceneGroup','PlayerUser','10','1',${lobby.playerNameEnc}`);
    const fallback = h.timers.find(t=>t.ms===60000); assert.ok(fallback);
    if (cancel === 'leave') h.cleanPlayerMatchState(lobby, 'joiner');
    else room.state = 5;
    const count = entrant.writes.length;
    fallback.fn();
    assert.equal(entrant.writes.length, count);
    assert.equal(h.timers.some(t=>t.ms===1000), false);
});

for (const first of ['hall', 'scene', 'hall-completed']) test(`return to lobby: ${first} first removes entity once and preserves peers`, () => {
    const h = harness(true);
    const lobby = h.connect(h.socket('leaver', 1000, 10));
    Object.assign(lobby, {playerName:'leaver', playerNameEnc:utils.encodeHex('leaver'), playerIndex:10, socketType:'LOBBY'});
    h.emit(lobby, 'HallUser');
    h.state.playerSessions.leaver = {};
    const scene = h.connect(h.socket('leaver',1000,10));
    h.emit(scene, `NGOL'SceneGroup','PlayerUser','10','1',${utils.encodeHex('leaver')}`);
    scene.isCombatReady = true;
    const peer = h.socket('peer',1000,11), foreign = h.socket('foreign',1001,12);
    h.state.activeSockets.push(peer,foreign);
    const room = h.room(1000,[lobby,peer]); room.tdmMatch.players=[scene,peer];
    const hall = cmd=>h.emit(lobby,`e1:SendMsgToHallServer,(${utils.encodeInt(cmd)},(1,0))`);
    const leave = ()=>h.emit(scene,`eA:SendMsgToServer,(${utils.encodeInt(20001)},(0,0))`);
    if(first==='hall') { hall(20002); leave(); }
    else if(first==='scene') { leave(); hall(20002); }
    else hall(1310);
    scene.destroy(); hall(1310);
    assert.equal(peer.writes.filter(p=>p==='dA\n').length,1);
    assert.equal(foreign.writes.length,0);
    assert.equal(scene.writes.filter(p=>p.includes(`(${utils.encodeInt(20001)},(0,'',0))`)).length,first==='hall-completed'?0:1);
    assert.equal(room.players.some(p=>p && p.name==='leaver'),false);
    assert.equal(room.tdmMatch.players.includes(scene),false);
    assert.equal(peer.inMatch,true);
});

for (const quick of [true,false]) test(`balanced active admission via ${quick?'matchmaking':'room list'}`, async()=>{
    const h=harness(true), host=h.socket('host',1000,11);
    h.state.activeSockets.push(host);
    const room=h.room(1000,[host]);room.isMatchmaking=quick;room.tdmMatch.state='fight';
    const s=h.connect(h.socket('newcomer',null,10));
    Object.assign(s,{playerName:'newcomer',playerNameEnc:utils.encodeHex('newcomer'),playerIndex:10,socketType:'LOBBY',inMatch:false,currentRoomUID:null});
    h.globals.userIndexes.newcomer=10;h.state.playerSessions.newcomer={};h.emit(s,'HallUser');
    const request=quick?`e1:SendMsgToHallServer,(${utils.encodeInt(80001)},(${utils.encodeInt(80002)},1,'${s.playerNameEnc}',7))`:`e1:SendMsgToHallServer,(${utils.encodeInt(20013)},(${utils.encodeInt(3303)},('',${utils.encodeInt(1000)})))`;
    h.emit(s,request);
    for(let i=0;i<6;i++)await Promise.resolve();
    assert.equal(s.force,2);
    assert.equal(room.players[5].name,'newcomer');
    h.emit(s,request);
    assert.equal(room.players.filter(p=>p && p.name==='newcomer').length,1);
});

test('initial matchmaking alternates teams in the corresponding room slots',()=>{
    const h=harness(true);
    for(const [name,id] of [['alpha',10],['bravo',11]]) {
        const s=h.connect(h.socket(name,null,id));
        Object.assign(s,{playerName:name,playerNameEnc:utils.encodeHex(name),playerIndex:id,socketType:'LOBBY',inMatch:false,currentRoomUID:null});
        h.globals.userIndexes[name]=id;h.state.playerSessions[name]={};h.emit(s,'HallUser');
        h.emit(s,`e1:SendMsgToHallServer,(${utils.encodeInt(80001)},(${utils.encodeInt(80002)},1,'${s.playerNameEnc}',7))`);
    }
    const timer=h.timers.find(t=>t.ms===1500 && !t.cancelled);assert.ok(timer);timer.fn();
    const room=[...h.roomContainers.values()][0];
    assert.equal(room.players[0].force,1);assert.equal(room.players[5].force,2);
    assert.equal(room.players[1],null);
});

test('lifecycle records selection, loading, playing and departure with stable connection identity',()=>{
 const h=harness();const s=h.socket('king',1000,10);s.isCombatReady=false;
 h.lifecycle.transition(s,'selecting','admission');h.lifecycle.transition(s,'loading','hero-confirmed');
 h.lifecycle.transition(s,'playing','spawn');h.lifecycle.transition(s,'leaving','exit');
 s.sceneRetired=true;h.lifecycle.transition(s,'closed','retired');
 assert.equal(h.lifecycle.transition(s,'playing','late-spawn'),false);
 const events=h.telemetry.filter(e=>e.type==='MATCH_TRANSITION');
 assert.deepEqual(events.map(e=>e.next),['selecting','loading','playing','leaving','closed']);
 assert.equal(new Set(events.map(e=>e.connectionId)).size,1);
 assert.ok(events.every(e=>e.player==='king' && e.room===1000 && e.playerId===10));
 assert.equal(s.isCombatReady,false);
});

test('ending TDM invalidates pending spawn and prevents resurrection of its controller',()=>{
 const h=harness();const lobby=h.socket('late',1000,10);lobby.socketType='LOBBY';lobby.hasConfirmedHero=true;
 h.state.activeSockets.push(lobby);h.state.playerSessions.late={};const room=h.room(1000,[lobby]);
 const s=h.socket('late',1000,10);s.isCombatReady=false;h.connect(s);
 h.emit(s,`NGOL'SceneGroup','PlayerUser','10','1',${lobby.playerNameEnc}`);
 h.emit(s,'SLoadMapOKMsg');const timer=h.timers.find(t=>t.ms===1500);assert.ok(timer);
 h.game.endTdmMatch(room.tdmMatch);const count=s.writes.length;timer.fn();
 assert.equal(s.writes.length,count);assert.equal(s.isCombatReady,false);assert.equal(s.matchPhase,'ended');
});

test('client diagnostic text does not authorize combat spawn',()=>{
 const h=harness();const lobby=h.socket('late',1000,10);lobby.socketType='LOBBY';lobby.hasConfirmedHero=true;
 h.state.activeSockets.push(lobby);h.state.playerSessions.late={};h.room(1000,[lobby]);
 const s=h.socket('late',1000,10);s.isCombatReady=false;h.connect(s);
 h.emit(s,`NGOL'SceneGroup','PlayerUser','10','1',${lobby.playerNameEnc}`);
 h.emit(s,`eA:SendMsgToServer,(${utils.encodeInt(9002)},(7,'loadmapok:True'))`);
 assert.equal(h.timers.some(t=>t.ms===1500),false);
 h.emit(s,'eA:SLoadMapOKMsg,()');assert.equal(h.timers.some(t=>t.ms===1500),true);
});

test('initial matchmaking redirect is cancelled when leaving during admission',async()=>{
 const h=harness(true),s=h.connect(h.socket('queued',null,10));
 Object.assign(s,{playerName:'queued',playerNameEnc:utils.encodeHex('queued'),playerIndex:10,socketType:'LOBBY',inMatch:false,currentRoomUID:null});
 h.state.playerSessions.queued={};h.globals.userIndexes.queued=10;h.emit(s,'HallUser');
 h.emit(s,`e1:SendMsgToHallServer,(${utils.encodeInt(80001)},(${utils.encodeInt(80002)},1,'${s.playerNameEnc}',7))`);
 const queue=h.timers.find(t=>t.ms===10000);assert.ok(queue);queue.fn();
 for(let i=0;i<8;i++)await Promise.resolve();
 h.cleanPlayerMatchState(s,'queued');const count=s.writes.length;
 for(const timer of h.timers.filter(t=>t.ms===1500))timer.fn();
 assert.equal(s.writes.length,count);
});

test('an old queue entry cannot readmit a player who already joined an active match',()=>{
 const h=harness(true),s=h.connect(h.socket('queued',null,10));
 Object.assign(s,{playerName:'queued',playerNameEnc:utils.encodeHex('queued'),playerIndex:10,socketType:'LOBBY',inMatch:false,currentRoomUID:null});
 h.state.playerSessions.queued={};h.globals.userIndexes.queued=10;h.emit(s,'HallUser');
 const request=`e1:SendMsgToHallServer,(${utils.encodeInt(80001)},(${utils.encodeInt(80002)},1,'${s.playerNameEnc}',7))`;
 h.emit(s,request);const queue=h.timers.find(t=>t.ms===10000);assert.ok(queue);
 const host=h.socket('host',1000,11);h.state.activeSockets.push(host);const room=h.room(1000,[host]);room.isMatchmaking=true;room.tdmMatch.state='fight';
 h.emit(s,request);assert.equal(s.currentRoomUID,1000);queue.fn();
 assert.equal(s.currentRoomUID,1000);assert.equal(h.roomContainers.size,1);
});

for (const secondary of [false,true]) for (const head of [false,true]) test(`native SDG uses correct ${secondary?'secondary':'primary'} weapon and ${head?'head':'body'} damage`,()=>{
 const {h,a,enemy}=lamsterFight();a.currentHeroId=1015;
 h.db.weaponsMap[1015]={cartridgePublic:0,1:{damage:40,damageHead:0},2:{damage:90,damageHead:1}};
 const hit=[10,12,head?1:0,100,[0,0,0],90,[0,0,0],0,head?'Bip001 Head':'Bip001 Spine',[0,400,900]];
 const payload=[[hit],secondary?2:1,0,1,0,0,head?1:0,1,10,1,0,1,0,0,1015];
 h.emit(a,utils.PacketBuilder.rpc(10,'SDG',[[payload]]).trim());
 assert.equal(enemy.hp,700-(secondary?(head?180:90):40),h.logs.slice(-8).join('\n'));
});

test('SDG decoder never treats metadata or vectors as targets',()=>{
 const {decodeDamageEnvelope}=require('../src/sdgProtocol');
 const empty=[[],2,0,1,0,0,0,0,2,-1,-2,0,0,0,1017];
 assert.equal(decodeDamageEnvelope([[empty]]).hits.length,0);
 assert.equal(decodeDamageEnvelope([1,2,3]),null);
 const fake=[[[1,2,3]],1,0,1,0,0,0,0,1,1,0,1,0,0,1015];
 assert.equal(decodeDamageEnvelope(fake).hits.length,0);
});

for (const secondary of [false,true]) for (const head of [false,true]) test(`Celina native SDG applies XML damage for ${secondary?'secondary':'primary'} / ${head?'head':'body'}`,()=>{
 const {h,a,enemy}=lamsterFight();a.currentHeroId=1017;enemy.hp=2000;
 const xml=fs.readFileSync(path.join(__dirname,'../public/baxi/weapon.xml'),'utf8');
 const block=xml.match(/<i id="1017"[\s\S]*?<\/i>/)[0];const weapons={cartridgePublic:0};
 for(const m of block.matchAll(/<weapon shootname="(\d+)"[\s\S]*?<launcher\s([^>]+)>/g)){
  weapons[m[1]]={damage:Number(m[2].match(/damageparamenemy="(\d+)"/)[1]),damageHead:Number(m[2].match(/damagehead="(\d+)"/)[1])};
 }
 h.db.weaponsMap[1017]=weapons;
 const hit=[10,12,head?1:0,100,[0,0,0],90,[0,0,0],0,head?'Bip001 Head':'Bip001 Spine',[0,400,900]];
 const payload=[[hit],secondary?2:1,0,1,0,0,head?1:0,1,10,1,0,1,0,0,1017];
 h.emit(a,utils.PacketBuilder.rpc(10,'SDG',[[payload]]).trim());
 assert.equal(enemy.hp,2000-(secondary?300:(head?750:375)));
});

function individualFight() {
    const f = lamsterFight();
    const { h, a, b, enemy } = f;
    for (const p of [a,b,enemy]) Object.assign(p, { targetMapId: 5301, finalMapId: 5301, force: 1 });
    const room = h.room(1000, [a,b,enemy]);
    Object.assign(room, { mapId: 5301, finalMapId: 5301, mapType: 11, tdmMatch: null });
    for (const p of [a,b,enemy]) h.game.startFreeForAll(p);
    return { ...f, room, match: room.ffaMatch };
}

test('individual same-slot players take native SDG damage and cannot heal each other', () => {
    const {a,b,enemy,pulse} = individualFight();
    pulse(1,[b,enemy]);
    assert.equal(b.hp,380); assert.equal(enemy.hp,580); assert.equal(a.hp,700);
    pulse(4,[b,enemy]);
    assert.equal(b.hp,260); assert.equal(enemy.hp,460);
});

test('individual HUD uses native 19701, shared clock and populated spawn statistics', () => {
    const {h,a,b,match} = individualFight();
    assert.equal(match.duration,600); assert.equal(match.targetScore,20);
    a.matchKills=7; a.matchDeaths=2;
    const packet=h.game.getPlayerSpawnPacket(a,a).split('\n')[0];
    const data=utils.parseArrayList(packet.split(':SendMsgToClient,')[1]);
    assert.deepEqual(data[1][1][7],[7,2,10]);
    const start=match.startedAt;
    const before=a.writes.length;
    h.game.startFreeForAll(b);
    assert.equal(match.startedAt,start); assert.equal(a.matchKills,7); assert.equal(a.writes.length,before);
    const init=b.writes.find(p=>p.includes(`(${utils.encodeInt(19700)},`));
    assert.deepEqual(utils.parseArrayList(init.split(':SendMsgToClient,')[1]),[19700,[19701,[600,20]]]);
    assert.ok(b.writes.some(p=>p.includes(match.startDate)));
});

test('individual kills are idempotent, suicides do not award kills and remote rooms are excluded', () => {
    const {h,a,b,foreign,match} = individualFight();
    h.game.handlePlayerDeath(b,a); h.game.handlePlayerDeath(b,a);
    assert.equal(a.matchKills,1); assert.equal(b.matchDeaths,1);
    h.game.handlePlayerDeath(a,a);
    assert.equal(a.matchKills,1); assert.equal(a.matchDeaths,1);
    assert.equal(foreign.writes.length,0); assert.equal(match.state,'fight');
});

test('individual environment kill credits a recent opponent in the same slot', () => {
    const {h,a,b} = individualFight();
    b.lastAttackerSocket=a; b.lastAttackerTime=Date.now();
    h.game.handlePlayerDeath(b);
    assert.equal(a.matchKills,1); assert.equal(b.matchDeaths,1);
});

test('individual respawn request respects death timer and cannot resurrect after leaving', () => {
    const {h,a,b} = individualFight();
    h.game.handlePlayerDeath(a,b);
    const request=utils.PacketBuilder.rpc(10,'SendMsgToServer',[19702,[10]]).trim();
    h.emit(a,request); assert.equal(a.isDead,true);
    a.ffaRespawnAt=Date.now()-1;
    h.emit(a,request); assert.equal(a.isDead,false,h.logs.slice(-8).join('\n')); assert.equal(a.hp,a.maxHp);
    const positions=require('../src/ffaMaps.json')[5301].spawns;
    assert.ok(positions.some(p=>p.sx===a.lastX && p.sy===a.lastY && p.sz===a.lastZ));
    h.game.handlePlayerDeath(a,b); const old=h.timers.at(-1);
    h.cleanPlayerMatchState(a,a.playerName); const before=a.writes.length;
    old.fn(); assert.equal(a.writes.length,before);
});

test('20th individual kill ends only its room, invalidates pending entry and awards only the winner', () => {
    const {h,a,b,enemy,match,room,foreign} = individualFight();
    const pending=h.socket('pending',1000,15); pending.targetMapId=5010; pending.finalMapId=5301;
    pending.isCombatReady=false; pending.waitingForHeroLock=true; h.state.activeSockets.push(pending);
    const pendingCallback=scope.capture(pending);
    a.matchKills=19;
    h.game.handlePlayerDeath(b,a);
    assert.equal(match.state,'ended'); assert.equal(room.state,5); assert.equal(match.winner,a);
    assert.equal(pendingCallback(),false); assert.equal(pending.waitingForHeroLock,false);
    assert.equal(a.isCombatReady,false); assert.equal(enemy.inMatch,false); assert.equal(foreign.inMatch,true);
    assert.ok(a.writes.some(p=>p.includes(`(${utils.encodeInt(19703)},(1,'${utils.encodeHex(a.playerName)}')`)));
    h.game.tickFreeForAll(match);
    assert.equal(h.timers.filter(t=>t.ms===3000).length,1);
    h.timers.find(t=>t.ms===3000).fn();
    assert.equal(h.rewards.filter(r=>r.isWin).length,1); assert.equal(h.rewards.find(r=>r.isWin).name,a.playerName);
});

test('individual timeout ranks kills then deaths; old results cannot reach a new scene', () => {
    const {h,a,b,enemy,match} = individualFight();
    a.matchKills=3; a.matchDeaths=2; b.matchKills=3; b.matchDeaths=1;
    match.startedAt=Date.now()-601000; h.game.tickFreeForAll(match);
    assert.equal(match.winner,b);
    const before=enemy.writes.length;
    scope.invalidate(enemy); enemy.currentRoomUID=2000; enemy.targetMapId=5204;
    h.timers.find(t=>t.ms===3000).fn(); h.timers.find(t=>t.ms===35000).fn();
    assert.equal(enemy.writes.length,before); assert.equal(h.rewards.some(r=>r.name===enemy.playerName),false);
});

test('individual empty controller stops, and native map spawns preserve x/z/height ordering', () => {
    const {h,a,b,enemy,match} = individualFight();
    for(const p of [a,b,enemy]) p.destroyed=true;
    h.game.tickFreeForAll(match); assert.equal(match.state,'ended'); assert.equal(match.timer,null);
    const maps=require('../src/ffaMaps.json');
    const xml=fs.readFileSync(path.join(__dirname,'../public/baxi/fbmodel.xml'),'utf8');
    for(let i=1;i<=7;i++) {
        const row=xml.match(new RegExp('<i id="300'+i+'"[^>]*>'))[0];
        const positions=row.match(/revivepoint1="([^"]+)"/)[1].split('|').map(p=>p.split(',').map(Number));
        assert.deepEqual(maps[5300+i].spawns.map(p=>[p.sx,p.sz,p.sy,p.yaw]),positions);
        assert.equal(maps[5300+i].duration,600);
    }
});

test('individual summons belong to their owner; team mode relationships remain unchanged', () => {
    const combat=require('../src/combatRules');
    const a={playerId:10,force:1,targetMapId:5301}, b={playerId:11,force:1,targetMapId:5301};
    const turret={id:2000,masterId:10,force:1,targetMapId:5301};
    assert.equal(combat.sameSide(a,b),false); assert.equal(combat.sameSide(a,turret),true);
    assert.equal(combat.sameSide(b,turret),false);
    a.targetMapId=b.targetMapId=5204;
    assert.equal(combat.sameSide(a,b),true); b.force=2; assert.equal(combat.sameSide(a,b),false);
});

test('individual late entry uses real scene-ready handler without restarting existing players', () => {
    const {h,a,match}=individualFight(); a.matchKills=5;
    const lobby=h.socket('late',1000,16); lobby.socketType='LOBBY'; lobby.hasConfirmedHero=true;
    lobby.targetMapId=lobby.finalMapId=5301; h.state.activeSockets.push(lobby); h.state.playerSessions.late={};
    const late=h.socket('late',1000,16); late.isCombatReady=false; h.connect(late);
    h.emit(late,`NGOL'SceneGroup','PlayerUser','16','1',${lobby.playerNameEnc}`);
    h.emit(late,'SLoadMapOKMsg'); const timer=h.timers.find(t=>t.ms===1500); assert.ok(timer);
    const start=match.startedAt; timer.fn();
    assert.equal(late.isCombatReady,true,h.logs.slice(-12).join('\n'));
    assert.equal(h.game.getFreeForAllMatch(late),match); assert.equal(match.startedAt,start); assert.equal(a.matchKills,5);
    assert.ok(late.writes.some(p=>p.includes(`(${utils.encodeInt(19700)},`)));
    assert.equal(late.sceneDoorTimeout,undefined);
    assert.equal(h.game.getTdmMatch(late,false),null);
});

test('individual controllers are isolated and spawns avoid a living opponent', () => {
    const {h,a,match}=individualFight();
    const other=h.socket('other',2000,30);other.targetMapId=other.finalMapId=5301;h.state.activeSockets.push(other);
    const room=h.room(2000,[other]);Object.assign(room,{mapId:5301,finalMapId:5301,mapType:11,tdmMatch:null});
    h.game.startFreeForAll(other);const otherMatch=room.ffaMatch;
    const first=require('../src/ffaMaps.json')[5301].spawns[0];a.lastX=first.sx;a.lastY=first.sy;a.lastZ=first.sz;
    const join=h.socket('join',1000,19);join.targetMapId=5301;
    const spawn=h.game.getPlayerSpawnCoordinates(join);
    assert.notEqual(spawn.sx,first.sx);
    match.startedAt=Date.now()-601000;h.game.tickFreeForAll(match);
    assert.equal(otherMatch.state,'fight');assert.equal(other.isCombatReady,true);
});

function queuePlayer(h,name,id,type) {
    const s=h.connect(h.socket(name,null,id));
    Object.assign(s,{playerName:name,playerNameEnc:utils.encodeHex(name),playerIndex:id,socketType:'LOBBY',inMatch:false,currentRoomUID:null});
    h.state.playerSessions[name]={};h.globals.userIndexes[name]=id;h.emit(s,'HallUser');
    h.emit(s,`e1:SendMsgToHallServer,(${utils.encodeInt(80001)},(${utils.encodeInt(80002)},1,'${s.playerNameEnc}',${utils.encodeInt(type)}))`);
    return s;
}

test('individual matchmaking selects individual maps and does not backfill a TDM room', async () => {
    const h=harness(true);for(let i=5301;i<=5307;i++)h.db.MAP_CONFIG[i]={name:'solo',type:11};
    const host=h.socket('host',1000,11);h.state.activeSockets.push(host);const team=h.room(1000,[host]);team.isMatchmaking=true;
    const solo=queuePlayer(h,'solo',20,11);assert.equal(solo.inMatch,false);
    h.timers.find(t=>t.ms===10000).fn();for(let i=0;i<8;i++)await Promise.resolve();
    assert.ok(solo.finalMapId>=5301 && solo.finalMapId<=5307);assert.notEqual(solo.currentRoomUID,1000);
});

test('mixed team and individual queue entries are admitted to different rooms', async () => {
    const h=harness(true);for(let i=5301;i<=5307;i++)h.db.MAP_CONFIG[i]={name:'solo',type:11};
    const team=queuePlayer(h,'team',20,7),solo=queuePlayer(h,'solo',21,11);
    h.timers.filter(t=>t.ms===10000).at(-1).fn();for(let i=0;i<8;i++)await Promise.resolve();
    h.timers.find(t=>t.ms===0).fn();for(let i=0;i<8;i++)await Promise.resolve();
    assert.notEqual(team.currentRoomUID,solo.currentRoomUID);
    assert.ok(solo.finalMapId>=5301 && solo.finalMapId<=5307);assert.ok(team.finalMapId<5301);
});

test('individual skill deaths pre-marked by collider handlers still count exactly once', () => {
    const {h,a,b}=individualFight();b.isDead=true;b.hp=0;
    h.game.handleEntityDeath(b,b.playerId,a,a.playerId,1033);
    h.game.handleEntityDeath(b,b.playerId,a,a.playerId,1033);
    assert.equal(b.matchDeaths,1);assert.equal(a.matchKills,1);assert.ok(b.respawnTimeout);
});

test('individual spawn raw teams and roster are viewer-relative even when all room slots share a team', () => {
    const {h,a,b,enemy}=individualFight();enemy.force=2;
    for(const viewer of [a,b,enemy]) {
        for(const player of [a,b,enemy]) {
            const packet=h.game.getPlayerSpawnPacket(player,viewer);
            let data;
            if(player===viewer) data=utils.parseArrayList(packet.split('\n')[0].split(':SendMsgToClient,')[1])[1];
            else data=utils.parseArrayList(packet.split('("PlayerSelect",')[1].trim().slice(0,-1));
            assert.equal(data[0][0][2],player===viewer?1:2);
        }
        const roster=utils.parseArrayList(`(${h.getFullMatchUserList(viewer)})`);
        for(let i=0;i<roster.length;i+=2) assert.equal(roster[i+1][3],roster[i]===viewer.playerNameEnc?1:2);
    }
});

test('individual menu is enabled, cache key changes, and every advertised solo map has server data', () => {
    const menu=fs.readFileSync(path.join(__dirname,'../public/baxi/MatchModel.xml'),'utf8');
    assert.match(menu,/<i id="2"[^>]+fbtype="11"[^>]+isopen="1"/);
    const manifest=fs.readFileSync(path.join(__dirname,'../files_dir.xml'),'utf8');
    assert.match(manifest,/<f path="baxi\/MatchModel.xml" crc="100004"/);
    const fbset=fs.readFileSync(path.join(__dirname,'../public/baxi/fbset.xml'),'utf8');
    const database=fs.readFileSync(path.join(__dirname,'../src/database.js'),'utf8');
    const combat=require('../src/combatRules');const maps=require('../src/ffaMaps.json');
    for(const row of fbset.match(/<i [^>]+fbtype="11"[^>]+>/g)) {
        const id=Number(row.match(/id="(\d+)"/)[1]);
        assert.equal(combat.isFreeForAll({targetMapId:id}),true);
        assert.ok(maps[id].spawns.length>1);
        assert.match(database,new RegExp(id+': \\{ name: \'[^\']+\', type: 11 \\}'));
    }
});

test('initial individual selection updates client map type before loading assets and spawning', () => {
    const h=harness();const s=h.socket('first',1000,10);h.state.activeSockets.push(s);
    const room=h.room(1000,[s]);Object.assign(room,{mapId:5301,finalMapId:5301,mapType:11,tdmMatch:null});
    Object.assign(s,{targetMapId:5010,finalMapId:5301,waitingForHeroLock:true,isCombatReady:false});
    h.game.switchPlayerHero(s,1015);
    const switchPacket=s.writes.find(p=>p.includes(`(${utils.encodeInt(90009)},`));
    assert.ok(switchPacket,'90004/CLoadScene alone leave MatchManager on staging type 7');
    const decoded=utils.parseArrayList(switchPacket.split(':SendMsgToClient,')[1]);
    assert.deepEqual(decoded,[20014,[90009,[5301,h.db.MAP_CONFIG[5301].name]]]);
    assert.equal(s.writes.some(p=>p.includes('CLoadScene')),false);
    h.timers.find(t=>t.ms===1000).fn();
    assert.ok(s.writes.at(-1).includes('CLoadScene'));
    assert.equal(s.targetMapId,5301);
});

function pveFight(map=5105,roomId=1000) {
    const h=harness();
    h.db.MAP_CONFIG[map]={name:map===5115?'hasd_qingshen':'shuijingkuangdong',type:map===5115?12:9};
    const a=h.connect(h.socket('PveHost',roomId,10));
    h.emit(a,'PlayerUser,0');
    Object.assign(a,{socketType:'SCENE',targetMapId:map,finalMapId:map,currentRoomUID:roomId,isCombatReady:true,inMatch:true,playerId:'10',playerIndex:10});
    const match=h.game.startPveMatch(a,map);
    match.timer.fn();
    return {h,a,match};
}

test('Adventure spawns native monster heroes at XML path origins, never training NPCs',()=>{
    const {h,a,match}=pveFight();
    const defs=require('../src/pveRobots.json');
    assert.equal(match.monsterNum,8);
    for(const bot of Object.values(match.monsters)) {
        assert.equal(bot.currentHeroId,2001);
        assert.deepEqual([bot.lastX,bot.lastY,bot.lastZ],defs[bot.createIndex].pos);
        assert.equal(h.state.activeBots[bot.playerIndex],bot);
        assert.equal(h.state.spawnedNpcs[bot.playerIndex],undefined);
        const spawn=a.writes.find(p=>p.includes(`cRobotAI:${utils.encodeInt(bot.playerIndex)},`));
        assert.ok(spawn?.endsWith(`,1,${utils.encodeInt(bot.createIndex)})\n`));
    }
    assert.ok(match.crystal);
    assert.equal(match.state,'fight');
});

test('Adventure authority waits for loaded model and belongs to only one same-scene client',()=>{
    const {h,a,match}=pveFight();
    const b=h.socket('Peer',1000,11); b.targetMapId=5105; h.state.activeSockets.push(b); h.game.startPveMatch(b,5105);
    const foreign=h.socket('Foreign',1001,12); foreign.targetMapId=5105;
    const bot=Object.values(match.monsters)[0];
    h.game.tickPveMatch(match);
    assert.ok(a.writes.some(p=>p===utils.PacketBuilder.rpc(bot.playerIndex,'CheckPlayer',[])));
    assert.equal(h.game.selectPveBotAuthority(foreign,bot.playerIndex),false);
    h.emit(a,utils.PacketBuilder.rpc(10,'SendMsgToServer',[4010002,[bot.playerIndex,50]]).trim());
    assert.equal(bot.authority,a,h.logs.join('\n'));
    h.game.selectPveBotAuthority(b,bot.playerIndex);
    assert.equal(bot.authority,a);
    a.writes.length=0; b.writes.length=0;
    h.game.tickPveMatch(match);
    assert.ok(!a.writes.some(p=>p.includes(`e${utils.encodeInt(bot.playerIndex)}:CheckPlayer`)));
    h.game.leavePveMatch(a);
    assert.equal(bot.authority,b);
    assert.ok(b.writes.some(p=>p.includes(utils.encodeInt(4010001))));
});

test('Adventure SSP, aim and animation preserve native arrays, XYZ and base62 encoding',()=>{
    const {h,a,match}=pveFight();
    const b=h.socket('Peer',1000,11); b.targetMapId=5105; h.state.activeSockets.push(b); h.game.startPveMatch(b,5105);
    const bot=Object.values(match.monsters)[0];
    h.game.selectPveBotAuthority(a,bot.playerIndex); b.writes.length=0;
    const pos=[8522,8243,14257];
    h.emit(a,utils.PacketBuilder.rpc(10,'SSP',[[pos,0,bot.playerIndex]]).trim());
    assert.deepEqual([bot.lastX,bot.lastY,bot.lastZ],pos,h.logs.join('\n'));
    assert.ok(b.writes.includes(utils.PacketBuilder.rpc(bot.playerIndex,'SCP',[[pos,0]])));
    h.emit(a,utils.PacketBuilder.rpc(bot.playerIndex,'RST',[[pos,0,bot.playerIndex]]).trim());
    assert.ok(b.writes.includes(utils.PacketBuilder.rpc(bot.playerIndex,'RCT',[[pos,0]])));
    h.emit(a,utils.PacketBuilder.rpc(10,'SA_Robot',[[['Run',0,1],1,bot.playerIndex]]).trim());
    assert.ok(b.writes.includes(utils.PacketBuilder.rpc(bot.playerIndex,'CA',[[['Run',0,1],1]])));
    h.game.relayPveBot(b,utils.PacketBuilder.rpc(11,'SSP',[[[1,2,3],0,bot.playerIndex]]).trim());
    assert.deepEqual([bot.lastX,bot.lastY,bot.lastZ],pos,'non-owner must not move bot');
});

test('Adventure late join preserves fight, wave timer and damaged crystal; rooms remain isolated',()=>{
    const {h,a,match}=pveFight();
    const timer=match.timer, crystal=match.crystal;
    crystal.hp=4321; a.writes.length=0;
    const b=h.socket('Peer',1000,11); b.targetMapId=5105; h.state.activeSockets.push(b);
    h.game.startPveMatch(b,5105);
    assert.equal(match.timer,timer); assert.equal(match.crystal,crystal); assert.equal(crystal.hp,4321);
    assert.equal(match.state,'fight');
    assert.ok(!a.writes.some(p=>p.includes(utils.encodeInt(19001))));
    const other=h.socket('OtherRoom',1001,12); other.targetMapId=5105; h.state.activeSockets.push(other);
    const second=h.game.startPveMatch(other,5105); second.timer.fn();
    assert.notEqual(second.crystal.id,crystal.id);
    assert.equal(Object.keys(h.state.activeBots).length,16);
    h.game.endPveMatch(match);
    assert.equal(Object.keys(h.state.activeBots).length,8);
    assert.equal(second.state,'fight');
});

test('Adventure bot and human exchange native SDG damage; kills advance only their own wave',()=>{
    const {h,a,match}=pveFight();
    const bot=Object.values(match.monsters)[0];
    h.game.selectPveBotAuthority(a,bot.playerIndex);
    h.db.weaponsMap[bot.currentHeroId]={cartridgePublic:30,1:{damage:75}};
    h.db.weaponsMap[a.currentHeroId]={cartridgePublic:30,1:{damage:75}};
    const hit=(att,target,weapon)=> {
        const hits=[[Number(att.playerId),Number(target.playerId),0,100,[0,0,0],0,[0,0,0],0,'body',[0,400,900]]];
        const payload=[hits,1,0,1,0,0,0,1,Number(att.playerId),1,0,1,0,6000,weapon];
        h.emit(a,utils.PacketBuilder.rpc(10,'SDG',[[payload]]).trim());
    };
    const hp=a.hp;
    hit(bot,a,bot.currentHeroId);
    assert.equal(a.hp,hp-75,h.logs.join('\n'));
    const bhp=bot.hp; hit(a,bot,a.currentHeroId); assert.equal(bot.hp,bhp-75,h.logs.join('\n'));
    for(const mon of Object.values(match.monsters))h.game.handlePlayerDeath(mon,a);
    assert.equal(match.monsterNum,0); assert.equal(match.state,'round_cleared');
    match.timer.fn();
    assert.equal(match.round,2); assert.equal(match.monsterNum,12);
    assert.ok(Object.values(match.monsters).some(b=>b.currentHeroId===2003));
});

test('Dark Land has its own heroes and patrol positions, no mine crystal',()=>{
    const {match}=pveFight(5115);
    assert.equal(match.crystal,undefined);
    assert.equal(match.pveType,12);
    assert.ok(Object.values(match.monsters).every(b=>b.currentHeroId>=3001 && b.lastY<5000));
});

test('Adventure map types agree with client XML and every wave references native data',()=>{
    const source=fs.readFileSync(path.join(__dirname,'../src/database.js'),'utf8');
    for(const [id,type] of [[5105,9],[5115,12]]) {
        assert.match(source,new RegExp(`${id}: \\{ name: '[^']+', type: ${type} \\}`));
        for(const lang of ['public/baxi','zh']) {
            const xml=fs.readFileSync(path.join(__dirname,'..',lang,'fbset.xml'),'utf8');
            const row=xml.match(new RegExp(`<i\\s+[^>]*id="${id}"[^>]*>`))[0];
            assert.ok(row.includes(`fbtype="${type}"`));
        }
    }
    const defs=require('../src/pveRobots.json');
    for(let lv=0;lv<12;lv++) {
        const mine=[1100,1200,1300,1400,1500,1600,1700,1800,1900,2000,4000,4100][lv];
        for(const offset of [1,2,3,4,5,6,7,8,9,10,11,12,13,14,17])assert.ok(defs[mine+offset],`${mine+offset}`);
        for(const offset of [4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,21])assert.ok(defs[2100+lv*100+offset]);
    }
});

test('Adventure initial selection switches mode before assets, and matchmaking keeps humans allied',async()=>{
    const h=harness(true);h.db.MAP_CONFIG[5105]={name:'shuijingkuangdong',type:9};
    const a=queuePlayer(h,'adventureA',20,9),b=queuePlayer(h,'adventureB',21,9);
    h.timers.filter(t=>t.ms===8000).at(-1).fn();for(let i=0;i<8;i++)await Promise.resolve();
    assert.equal(a.finalMapId,5105);assert.equal(b.finalMapId,5105);
    assert.equal(a.force,1);assert.equal(b.force,1);assert.equal(a.currentRoomUID,b.currentRoomUID);
    assert.equal(a.waitingForHeroLock,true);
    const s=h.socket('private',2000,25);h.state.activeSockets.push(s);
    Object.assign(s,{targetMapId:5010,finalMapId:5105,waitingForHeroLock:true,isCombatReady:false});
    const r=h.room(2000,[s]);Object.assign(r,{mapId:5105,finalMapId:5105,mapType:9});
    h.game.switchPlayerHero(s,1015);
    assert.ok(s.writes.some(p=>p.includes(`(${utils.encodeInt(90009)},(${utils.encodeInt(5105)},`)));
});

test('Adventure victory, crystal loss and leaving cancel waves and clean only that match',()=>{
    const {h,a,match}=pveFight();
    for(let wave=1;wave<=3;wave++) {
        for(const bot of Object.values(match.monsters)) h.game.handlePlayerDeath(bot,a);
        if(wave<3)match.timer.fn();
    }
    assert.equal(match.state,'victory');
    assert.ok(a.writes.includes(utils.PacketBuilder.rpc(10,'SendMsgToClient',[19000,[19002,[0,1]]])));
    match.timer.fn();assert.equal(Object.keys(h.state.activeBots).length,0);
    assert.equal(Object.keys(h.state.spawnedNpcs).length,0);
    const other=h.game.startPveMatch(a,5105),timer=other.timer;
    h.game.handleNpcHurt(a,other.crystal.id,10000,0);
    assert.equal(other.state,'defeat');assert.equal(timer.cancelled,true);
    timer.fn();assert.equal(other.monsterNum,0,'cancelled ready callback cannot restart defeat');
    h.game.leavePveMatch(a);assert.equal(h.game.getPveMatch(a),null);
});

test('Adventure native bullet visual preserves bot actor and refuses a second controller',()=>{
    const {h,a,match}=pveFight();const bot=Object.values(match.monsters)[0];
    const b=h.socket('Peer',1000,11);b.targetMapId=5105;h.state.activeSockets.push(b);h.game.startPveMatch(b,5105);
    const shot=utils.PacketBuilder.rpc(bot.playerIndex,'SB',[[1,0,'0',1,0,[1,2,3],[4,5,6],[],1,30,1]]).trim();
    b.writes.length=0;h.emit(a,shot);assert.equal(b.writes.length,0);
    h.game.selectPveBotAuthority(a,bot.playerIndex);b.writes.length=0;
    h.emit(a,shot);assert.ok(b.writes.some(p=>p.startsWith(`e${utils.encodeInt(bot.playerIndex)}:CB,`)),h.logs.join('\n'));
});

test('Adventure controller transfers away from a dead player without consuming other bot protocols',()=>{
    const {h,a,match}=pveFight();const bot=Object.values(match.monsters)[0];
    const b=h.socket('Peer',1000,11);b.targetMapId=5105;h.state.activeSockets.push(b);h.game.startPveMatch(b,5105);
    h.game.selectPveBotAuthority(a,bot.playerIndex);h.game.selectPveBotAuthority(b,bot.playerIndex);
    a.isDead=true;h.game.tickPveMatch(match);
    assert.equal(bot.authority,null);
    assert.ok(a.writes.includes(utils.PacketBuilder.rpc(10,'SendMsgToClient',[4010004,[bot.playerIndex]])));
    assert.equal(h.game.selectPveBotAuthority(a,bot.playerIndex),false);
    h.game.selectPveBotAuthority(b,bot.playerIndex);assert.equal(bot.authority,b);
    h.state.activeBots[333]={isBot:true};
    assert.equal(h.game.relayPveBot(b,utils.PacketBuilder.rpc(11,'SSP',[[[1,2,3],0,333]]).trim()),false);
});

for(const mode of [9,12]) for(const finish of ['confirm','timeout']) test(`Adventure ${mode} queue selects before loading, then reaches combat via ${finish}`,async()=>{
    const h=harness(true), map=mode===9?5105:5115;
    h.db.MAP_CONFIG[map]={name:mode===9?'shuijingkuangdong':'hasd_qingshen',type:mode};
    const lobby=queuePlayer(h,'adventure',10,mode);
    h.timers.filter(t=>t.ms===8000).at(-1).fn();for(let i=0;i<8;i++)await Promise.resolve();
    assert.equal(lobby.targetMapId,5010);assert.equal(lobby.finalMapId,map);
    assert.equal(lobby.waitingForHeroLock,true);
    assert.ok(!lobby.writes.some(p=>p.includes(`(${utils.encodeInt(90004)})`)),'queue must not force loading over selection');
    const s=h.connect(h.socket('adventure',lobby.currentRoomUID,10));s.isCombatReady=false;
    h.emit(s,`NGOL'SceneGroup','PlayerUser','10','1',${lobby.playerNameEnc}`);
    assert.equal(s.waitingForHeroLock,true,h.logs.join('\n'));
    h.emit(s,'SLoadMapOKMsg');
    assert.equal(h.game.getPveMatch(s),null,'staging must not start waves');
    assert.ok(!s.writes.some(p=>p.includes('CLoadScene')));
    h.emit(s,utils.PacketBuilder.rpc(10,'SendMsgToServer',[20014,[90002,1025,0]]).trim());
    assert.equal(s.waitingForHeroLock,true);
    assert.ok(!s.writes.some(p=>p.includes('CLoadScene')));
    if(finish==='confirm')h.emit(s,utils.PacketBuilder.rpc(10,'SendMsgToServer',[20014,[90002,1025,1]]).trim());
    else h.emit(s,utils.PacketBuilder.rpc(10,'SendMsgToServer',[20014,[90004]]).trim());
    assert.equal(s.currentHeroId,1025);assert.equal(s.waitingForHeroLock,false);
    assert.ok(s.writes.some(p=>p.includes(`(${utils.encodeInt(90009)},(${utils.encodeInt(map)},`)));
    const loads=h.timers.filter(t=>t.ms===1000&&!t.cancelled);assert.equal(loads.length,1);
    loads[0].fn();assert.ok(s.writes.some(p=>p.includes(`e:CLoadScene,((0,'${h.db.MAP_CONFIG[map].name}'`)));
    assert.equal(h.game.getPveMatch(s),null,'loading destination must not start waves');
    h.emit(s,'SLoadMapOKMsg');s.sceneSpawnTimeout.fn();
    assert.equal(s.isCombatReady,true);assert.equal(h.game.getPveMatch(s).pveType,mode);
});

test('admin native bots use their own controller without changing Adventure waves',()=>{
    const {h,a,match}=pveFight();
    const bot=h.game.spawnAdminRobot(a,1109,{x:0,y:0,z:600},{heroId:2003,name:'Fuzileiro'});
    assert.equal(bot.createIndex,1109);assert.equal(bot.currentHeroId,2003);
    assert.equal(h.game.getPveMatch(a),match);assert.equal(match.monsterNum,8);
    assert.equal(bot.pveMatch.isAdmin,true);
    h.game.tickPveMatch(bot.pveMatch);h.game.selectPveBotAuthority(a,bot.playerIndex);
    assert.equal(bot.authority,a);
    h.game.handlePlayerDeath(bot,a);assert.equal(match.monsterNum,8);assert.equal(bot.pveMatch.state,'fight');
});

test('damage and confirmed death preserve weapon and launcher for native voices',()=>{
 const h=harness();const a=h.socket('attacker',77,10),v=h.socket('victim',77,11),foreign=h.socket('foreign',78,12);
 v.force=2;h.state.activeSockets.push(a,v,foreign);
 h.game.sendEntityDamageUpdate(11,v,123,10,1,1024,null,0,1,4);
 const damage=utils.parseArrayList(v.writes.find(x=>x.includes(':HaveDamage,')).split(':HaveDamage,')[1]);
 assert.equal(damage[0][8],1024);assert.equal(damage[0][13],4);assert.equal(damage[0][14],1);assert.equal(damage[0][5],1);
 assert.equal(v.lastAttackerLauncher,1);
 h.game.handlePlayerDeath(v,a);
 const death=utils.parseArrayList(v.writes.find(x=>x.includes(':OnDeath,')).split(':OnDeath,')[1]);
 assert.equal(death[0],10);assert.equal(death[4],4);assert.equal(death[5],1);
 assert.equal(foreign.writes.filter(x=>x.includes('HaveDamage')||x.includes('OnDeath')).length,0);
});
test('damage without explicit launcher keeps primary zero for compatibility',()=>{
 const h=harness(),a=h.socket('a',7,10),v=h.socket('v',7,11);v.force=2;h.state.activeSockets.push(a,v);
 h.game.sendEntityDamageUpdate(11,v,10,10,0,1015);
 const damage=utils.parseArrayList(v.writes[0].split(':HaveDamage,')[1]);assert.equal(damage[0][14],0);
});

test('respawn preserves loaded actors and orders recovery before native respawn voice',()=>{
 const h=harness();const a=h.socket('reviving',77,10),peer=h.socket('peer',77,11),foreign=h.socket('foreign',78,12);
 a.isDead=true;a.hp=0;h.state.activeSockets.push(a,peer,foreign);
 h.game.respawnPlayer(a);
 assert.equal(a.isDead,false);assert.equal(a.hp,a.maxHp);
 for(const s of [a,peer]) {
  const revive=s.writes.findIndex(p=>p.includes(':OnRevive,'));
  const hp=s.writes.findIndex(p=>p.includes(':SetHp,'));
  assert.ok(hp>=0 && revive>hp,'HP recovery must precede respawn voice');
  assert.equal(s.writes.filter(p=>p.includes(':OnRevive,')).length,1);
  assert.ok(!s.writes.some(p=>p.includes('PlayerSelect')||p.includes(`(${utils.encodeInt(10001)},`)||p.startsWith('d')),'no actor recreation');
 }
 assert.ok(a.writes.some(p=>p.includes(':OnFillBullet,')),'native ammo refill retained');
 assert.equal(foreign.writes.length,0);
});



test('Lamster secondary damages enemies; primary duplicates and distant punches are rejected',()=>{
 const {a,b,enemy,pulse}=lamsterFight();
 pulse(2,[b,enemy]);assert.equal(b.hp,860);assert.equal(enemy.hp,450);
 pulse(1,[enemy]);assert.equal(enemy.hp,330);
 pulse(1,[enemy]);assert.equal(enemy.hp,330);
 enemy.lastX=1500;pulse(4,[enemy]);assert.equal(enemy.hp,330);
});


test('Lamster secondary buffs self on native SB and allies on SDG; damage bonus is real',()=>{
 const {h,a,b,enemy,pulse}=lamsterFight();
 h.db.buffsMap[1007]={type:6,time:5000,value:25,overlaytype:1};
 const shot=utils.PacketBuilder.rpc(10,'SB',[[2,0,'m1',1,2,[0,400,900],[0,400,900],[],1,0,1]]).trim();
 h.emit(a,shot);assert.ok(a.activeBuffs?.[1007],h.logs.join('\n'));
 pulse(2,[b,enemy]);assert.ok(b.activeBuffs?.[1007]);assert.equal(enemy.activeBuffs?.[1007],undefined);assert.equal(enemy.hp,450);
 pulse(2,[b,enemy]);assert.equal(enemy.hp,450);assert.equal(b.activeBuffs[1007].stacks,1);
 pulse(1,[enemy]);assert.equal(enemy.hp,300,'inspiration boosts primary120 to150');
});

test('inspiration1007 boosts an allied hero attack by25 percent and expires',()=>{
 const {h,a,enemy}=lamsterFight();a.currentHeroId=1015;
 h.db.buffsMap[1007]={type:6,time:5000,value:25,overlaytype:1};
 h.db.weaponsMap[1015]={cartridgePublic:30,1:{damage:40}};
 h.game.applyBuffToEntity(a,10,1007);
 const hit=[10,12,0,100,[0,0,0],0,[0,0,0],0,'body',[0,400,900]];
 const payload=[[hit],1,0,1,0,0,0,1,10,1,0,1,0,0,1015];
 const packet=utils.PacketBuilder.rpc(10,'SDG',[[payload]]).trim();
 h.emit(a,packet);assert.equal(enemy.hp,650);
 a.activeBuffs[1007].timeout.fn();assert.equal(a.activeBuffs[1007],undefined);
 h.emit(a,packet);assert.equal(enemy.hp,610);
});
