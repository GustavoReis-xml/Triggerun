
// ══════════════════════════════════════════════════════════════
// ⚡ GERENCIADOR DINÂMICO DE ENERGIA DE ULTIMATE (DAZHAO)
// ══════════════════════════════════════════════════════════════
function addPlayerUltEnergy(socket, amount) {
    if (!Number.isFinite(amount)) return;
    if (!socket || socket.destroyed || socket.socketType !== "SCENE" || socket.isDead || !socket.isCombatReady || socket.sceneRetired || socket.inMatch === false) return;
    if (socket.ultimateTimer) return; // Não ganha energia enquanto a Ultimate estiver ativa
    const currentHero = socket.currentHeroId || 1015;
    if (Number(currentHero) === 2000 && Date.now() < (socket.lamsterKit?.ultimateUntil || 0)) return;
    const dzList = db.heroDazhaoMap[currentHero] || [];
    const maxEnergy = (dzList.length > 0 && dzList[0].maxEnergy) ? dzList[0].maxEnergy : 10000;
    
    if (socket.ultEnergy === undefined) {
        socket.ultEnergy = 0;
    }
    
    const oldEnergy = socket.ultEnergy;
    socket.ultEnergy = Math.min(maxEnergy, Math.max(0, oldEnergy + Math.round(amount)));
    
    if (socket.ultEnergy !== oldEnergy) {
        const p50001 = utils.encodeInt(50001);
        const p50004 = utils.encodeInt(50004);
        const zero = utils.encodeInt(0);
        const energyEnc = utils.encodeInt(socket.ultEnergy);
        const chargePacket = `e${utils.encodeInt(Number(socket.playerId))}:SendMsgToAllClient,(${p50001},(${p50004},(${zero},${energyEnc},${zero})))\n`;
        socket.write(chargePacket);
    }
}

// Módulo de Servidor TCP (Lobby, Login, Scene e IA da Angel) do Trigger Run
const net = require('net');
const config = require('./config');
const { SERVER_IP } = config;
const PLAYER_NAME = "Admin";
const PLAYER_NAME_ENC = "41646d696e";
const state = require('./state');
const db = require('./database');
const lamster = require('./lamster');
const lamsterHero = require('./lamsterHero');
const utils = require('./utils');
const gameLogic = require('./gameLogic');
const dbPersistence = require('./dbPersistence');
const packetSniffer = require('./packetSniffer');
const sceneScope = require('./sceneScope');
const combat = require('./combatRules');
const heroCatalog = require('./heroCatalog');
const lifecycle = require('./matchLifecycle');
const clientStream = require('./clientStream');
const { decodeDamageEnvelope } = require('./sdgProtocol');
const socialServer = require('./socialServer');
const lamsterKit = lamsterHero.createKit({ state, game: gameLogic, db, later: setTimeout, cancel: clearTimeout });

// Desestruturação das referências globais mutáveis do estado
const { activeSockets, spawnedNpcs, ipNames, ipIndexes, playerSessions, playerInventory, playerEquipment } = state;

function isEntityInsideEvaCage(entity, entityForce) {
    if (!entity) return false;
    const ex = entity.lastX !== undefined ? entity.lastX : (entity.x !== undefined ? entity.x : 0);
    const ey = entity.lastY !== undefined ? entity.lastY : (entity.y !== undefined ? entity.y : 0);
    const ez = entity.lastZ !== undefined ? entity.lastZ : (entity.z !== undefined ? entity.z : 0);
    
    // Se a entidade não tiver posição válida registrada no mapa, não está na jaula
    if (ex === 0 && ey === 0 && ez === 0) return false;
    
    const cageRadiusSq = 400 * 400; // Raio real da jaula: 4 metros (400 unidades)
    for (const id in spawnedNpcs) {
        const npc = spawnedNpcs[id];
        if (!sceneScope.sameScene(entity, npc)) continue;
        if (npc && (npc.index === 1015 || String(npc.index) === "1015") && !npc.isDead) {
            // Apenas inimigos da criadora da jaula sofrem a penalidade (forças opostas)
            if (entityForce !== undefined && npc.force !== undefined && combat.sameSide(entity, npc, Number(entityForce) === Number(npc.force))) {
                continue;
            }
            const cx = npc.x !== undefined ? npc.x : 0;
            const cy = npc.y !== undefined ? npc.y : 0;
            const cz = npc.z !== undefined ? npc.z : 0;
            
            const distSq = (ex - cx) ** 2 + (ey - cy) ** 2 + (ez - cz) ** 2;
            if (distSq <= cageRadiusSq) {
                return true;
            }
        }
    }
    return false;
}

function getBaseTeleportCoordinates(mapId, force) {
    const mid = String(mapId || 5202);
    const f = String(force !== undefined ? force : 1);
    
    // Tabela calibrada de pontos de Spawn do Portal da Base para cada Mapa e Equipe (1 = Azul, 2 = Vermelha)
    const baseTeleports = {
        "5204": { "1": { sx: 9600, sy: 3800, sz: 5292 }, "2": { sx: 2000, sy: 3800, sz: 5300 } }, // KingHall / Sala de Treino
        "5059": { "1": { sx: 9600, sy: 3800, sz: 5292 }, "2": { sx: 2000, sy: 3800, sz: 5300 } },
        "5103": { "1": { sx: 9600, sy: 3800, sz: 5292 }, "2": { sx: 2000, sy: 3800, sz: 5300 } },
        "5202": { "1": { sx: 9600, sy: 3800, sz: 5292 }, "2": { sx: 2000, sy: 3800, sz: 5300 } },
        "5205": { "1": { sx: 9600, sy: 3800, sz: 5292 }, "2": { sx: 2000, sy: 3800, sz: 5300 } },
        "5010": { "1": { sx: 12147, sy: 2610, sz: 19500 }, "2": { sx: 12184, sy: 2610, sz: 27200 } }, // Depósito
        "5058": { "1": { sx: 12147, sy: 2610, sz: 19500 }, "2": { sx: 12184, sy: 2610, sz: 27200 } },
        "5104": { "1": { sx: 12147, sy: 2610, sz: 19500 }, "2": { sx: 12184, sy: 2610, sz: 27200 } },
        "5068": { "1": { sx: 7200, sy: 970, sz: 23550 }, "2": { sx: 22300, sy: 970, sz: 23572 } },   // Arakawa River
        "5102": { "1": { sx: 7200, sy: 970, sz: 23550 }, "2": { sx: 22300, sy: 970, sz: 23572 } },
        "5208": { "1": { sx: 16300, sy: 5220, sz: 9600 }, "2": { sx: 3700, sy: 5220, sz: 9600 } },   // Northern Forest
        "5210": { "1": { sx: 5400, sy: 5100, sz: 8500 }, "2": { sx: 4600, sy: 5100, sz: 1500 } },     // Ancestors Altar
        "5212": { "1": { sx: 5400, sy: 5000, sz: 8800 }, "2": { sx: 4600, sy: 5000, sz: 1200 } },     // Deserted Villages
        "5049": { "1": { sx: 25200, sy: 15300, sz: 17800 }, "2": { sx: 15300, sy: 15300, sz: 24200 } },
        "5260": { "1": { sx: 25200, sy: 15300, sz: 17800 }, "2": { sx: 15300, sy: 15300, sz: 24200 } },
        "5050": { "1": { sx: 57500, sy: 18034, sz: 45374 }, "2": { sx: 42800, sy: 18043, sz: 45056 } },
        "5055": { "1": { sx: 115200, sy: 79010, sz: 107410 }, "2": { sx: 115400, sy: 79010, sz: 122500 } },
        "5056": { "1": { sx: 11000, sy: 2014, sz: 12000 }, "2": { sx: 19000, sy: 2014, sz: 12000 } }
    };
    if (baseTeleports[mid] && baseTeleports[mid][f]) {
        return baseTeleports[mid][f];
    }
    const spawnCoords = gameLogic.getSpawnCoordinates(mapId, force);
    return { sx: spawnCoords.sx, sy: spawnCoords.sy, sz: spawnCoords.sz };
}

const {
    MAP_CONFIG,
    GAME_DB,
    getEntityClassName,
    npcData,
    heroDazhaoMap,
    playerSkillsMap,
    buffsMap,
    weaponsMap,
    groupSkillsOverrides,
    getHeroMaxHp,
    generateFullHeroData,
    isFaceUnlocked,
    isPaintUnlocked
} = db;

const getHeroIdsList = () => {
    const fromDb = Object.keys(db.heroUnlockPrices).map(Number);
    const combined = Array.from(new Set([...fromDb, ...heroCatalog.HERO_IDS_LIST])).filter(id => id > 0);
    return combined.sort((a, b) => a - b);
};

// Retorna dados de customização (face, paint, skin, weapon, emote, pose) do jogador
function getPlayerCustomizationData(playerName, heroId) {
    if (!playerName) {
        return { faceId: 2009, paintId: 1019, skinId: 1, weaponId: 1, emoteId: 1, poseId: 0 };
    }
    const profile = dbPersistence.loadPlayer(playerName);
    const faceId = profile ? (profile.face || 2009) : 2009;
    const paintId = profile ? (profile.paint || 1019) : 1019;
    const playerEq = state.playerEquipment[playerName] || (profile && profile.equipment) || {};
    const eq = playerEq[heroId] || { skin: 1, weapon: 1, emote: 1, pose: 0 };
    
    return {
        faceId,
        paintId,
        skinId: eq.skin || 1,
        weaponId: eq.weapon || 1,
        emoteId: eq.emote || 1,
        poseId: eq.pose || 0
    };
}

function getFullMatchUserList(source) {
    const rawMatchPlayers = activeSockets.filter(s => (s.socketType === "SCENE" || s.socketType === "LOBBY") && s.inMatch && !s.destroyed && sceneScope.sameScene(source, s));
    // Agrupa por playerName único para evitar que o mesmo jogador apareça duplicado (ou em ambos os times)
    const uniquePlayersMap = new Map();
    rawMatchPlayers.forEach(p => {
        const name = p.playerName || (p.remoteAddress && ipNames[p.remoteAddress]) || `Jogador_${p.playerIndex || 1}`;
        if (!uniquePlayersMap.has(name)) {
            uniquePlayersMap.set(name, p);
        } else {
            const existing = uniquePlayersMap.get(name);
            // Prioriza o socket SCENE se estiver em cena de jogo, senão mantém o mais atual
            if (p.socketType === "SCENE" && existing.socketType !== "SCENE") {
                uniquePlayersMap.set(name, p);
            }
        }
    });
    const matchPlayers = Array.from(uniquePlayersMap.values());
    let elements = [];
    const one = utils.encodeInt(1);
    const zero = utils.encodeInt(0);
    matchPlayers.forEach(p => {
        const pHeroId = p.currentHeroId || 1015;
        const pHeroEnc = utils.encodeInt(pHeroId);
        const pCust = getPlayerCustomizationData(p.playerName, pHeroId);
        const pProf = dbPersistence.loadPlayer(p.playerName);
        const pLvl = pProf && pProf.level ? pProf.level : (p.level || 1);
        const isCapEnc = utils.encodeInt(p.playerIndex === 1 ? 1 : 0);
        const pHeroInfo = `(${pHeroEnc},${utils.encodeInt(pCust.skinId || 1)},${utils.encodeInt(pCust.emoteId || 1)},${one},${utils.encodeInt(pCust.weaponId || 1)})`;
        const pPrerog = `(${isCapEnc},${isCapEnc},${isCapEnc},${isCapEnc},'${p.playerNameEnc}',${zero},'',${zero})`;
        const isChs = p.hasConfirmedHero ? one : zero;
        const pUserData = `('${p.playerNameEnc}',${utils.encodeInt(pLvl)},${one},${utils.encodeInt(combat.visibleForce(p, source))},${one},${one},${pHeroEnc},${one},${one},${isChs},${utils.encodeInt(pCust.faceId || 1)},'Local',${zero},${pHeroInfo},${utils.encodeInt(p.playerIndex || 1)},${zero},${zero},${isCapEnc},${pPrerog},())`;
        elements.push(`'${p.playerNameEnc}',${pUserData}`);
    });
    return elements.join(',');
}

function broadcastMatchUserList(source) {
    const userList = getFullMatchUserList(source);
    if (!userList) return;
    const p20014 = utils.encodeInt(20014);
    const s90006 = utils.encodeInt(90006);
    activeSockets.forEach(s => {
        if (s.socketType === "SCENE" && !s.destroyed && !s.sceneRetired && sceneScope.sameScene(source, s)) {
            s.write(`e${utils.encodeInt(s.playerIndex || 1)}:SendMsgToClient,(${p20014},(${s90006},(${combat.isFreeForAll(s) ? getFullMatchUserList(s) : userList})))\n`);
            s.write(`e${utils.encodeInt(s.playerIndex || 1)}:OnFightStat,(1,0)\n`);
        }
    });
}


// OnShowLoad reads the roster to decide which hero model to load.
function confirmInitialHero(socket, heroId) {
    if (!socket.waitingForHeroLock || socket.destroyed || socket.sceneRetired || !socket.inMatch) return;
    const room = roomContainers.get(socket.currentRoomUID);
    if (socket.currentRoomUID != null && (!room || room.state !== 4)) return;
    socket.currentHeroId = heroId;
    socket.hasConfirmedHero = true;
    const userList = getFullMatchUserList(socket);
    socket.write(`e${utils.encodeInt(socket.playerIndex || 1)}:SendMsgToClient,(${utils.encodeInt(20014)},(${utils.encodeInt(90006)},(${userList})))\n`);
    gameLogic.switchPlayerHero(socket, heroId);
    broadcastMatchUserList(socket);
}

// Funções de sincronização de sessão de jogadores
function updateSessionFromSocket(socket) {
    if (!socket || !socket.playerName) return;
    if (socket.socketType !== "SCENE") return; // Apenas atualiza a sessão de gameplay a partir do socket de cena
    if (socket.sceneRetired) return; // Uma conexão antiga não pode ressuscitar a sessão do lobby.
    const session = playerSessions[socket.playerName];
    if (session) {
        if (socket.playerIndex !== undefined) session.playerIndex = socket.playerIndex;
        if (socket.playerId !== undefined) session.playerId = socket.playerId;
        if (socket.force !== undefined) session.force = socket.force;
        if (socket.targetMapId !== undefined) session.targetMapId = socket.targetMapId;
        if (socket.targetMapName !== undefined) session.targetMapName = socket.targetMapName;
        if (socket.currentHeroId !== undefined) session.currentHeroId = socket.currentHeroId;
        if (socket.currentSkinId !== undefined) session.currentSkinId = socket.currentSkinId;
        if (socket.currentWeaponId !== undefined) session.currentWeaponId = socket.currentWeaponId;
        if (socket.finalMapId !== undefined) session.finalMapId = socket.finalMapId;
        if (socket.waitingForHeroLock !== undefined) session.waitingForHeroLock = socket.waitingForHeroLock;
        if (socket.hasConfirmedHero !== undefined) session.hasConfirmedHero = socket.hasConfirmedHero;
        if (socket.userData !== undefined) session.userData = socket.userData;
        if (socket.currentRoomUID !== undefined) session.currentRoomUID = socket.currentRoomUID;
    }
    if (socket.lobbySocket) {
        if (socket.currentHeroId !== undefined) socket.lobbySocket.currentHeroId = socket.currentHeroId;
        if (socket.targetMapId !== undefined) socket.lobbySocket.targetMapId = socket.targetMapId;
        if (socket.targetMapName !== undefined) socket.lobbySocket.targetMapName = socket.targetMapName;
        if (socket.finalMapId !== undefined) socket.lobbySocket.finalMapId = socket.finalMapId;
        if (socket.waitingForHeroLock !== undefined) socket.lobbySocket.waitingForHeroLock = socket.waitingForHeroLock;
        if (socket.hasConfirmedHero !== undefined) socket.lobbySocket.hasConfirmedHero = socket.hasConfirmedHero;
        if (socket.currentRoomUID !== undefined) socket.lobbySocket.currentRoomUID = socket.currentRoomUID;
    }
}

function syncSocketFromSession(socket) {
    if (!socket || !socket.playerName) return;
    const session = playerSessions[socket.playerName];
    if (session) {
        socket.playerIndex = session.playerIndex || (global.userIndexes && global.userIndexes[socket.playerName]) || (Number(session.playerId) || 1);
        socket.playerId = session.playerId || String(socket.playerIndex);
        socket.force = session.force;
        socket.targetMapId = session.targetMapId;
        socket.targetMapName = session.targetMapName;
        socket.currentHeroId = session.currentHeroId;
        socket.currentSkinId = session.currentSkinId;
        socket.currentWeaponId = session.currentWeaponId;
        socket.finalMapId = session.finalMapId;
        socket.waitingForHeroLock = session.waitingForHeroLock || false;
        socket.hasConfirmedHero = session.hasConfirmedHero || false;
        socket.userData = session.userData;
        if (session.currentRoomUID !== undefined) socket.currentRoomUID = session.currentRoomUID;
    }
}

function findBalancedSlot(room) {
    const counts = [0, 0];
    room.players.forEach((p, i) => { if (p) counts[(p.force || (i < 5 ? 1 : 2)) - 1]++; });
    const first = counts[0] <= counts[1] ? 0 : 5;
    for (const start of [first, first === 0 ? 5 : 0]) {
        for (let i = start; i < start + 5; i++) if (!room.players[i]) return i;
    }
    return -1;
}

function cleanPlayerMatchState(socket, playerName, returnToHall = false) {
    const pName = playerName || (socket && socket.playerName);
    // Detach before clearing room identifiers. Retire every old scene connection so
    // late packets cannot copy combat state back into the persistent lobby session.
    const related = activeSockets.filter(s => s === socket || (pName && s.playerName === pName));
    matchmakingQueue = matchmakingQueue.filter(entry => !related.includes(entry.socket));
    for (const s of related) {
        if (s.socketType === 'SCENE') {
            if (!s.sceneRetired) {
                const id = utils.encodeInt(s.playerIndex || Number(s.playerId) || 1);
                if (s.inMatch !== false) {
                    activeSockets.forEach(peer => {
                        if (peer !== s && sceneScope.canReceive(s, peer) && String(peer.playerIndex) !== String(s.playerIndex)) {
                            peer.write(`d${id}\n`);
                        }
                    });
                }
                // Hall or end-of-match UI can request departure even after match state set inMatch = false.
                if (returnToHall && !s.destroyed) {
                    s.write(`e${id}:SendMsgToClient,(${utils.encodeInt(20001)},(0,'',0))\n`);
                }
            }
            gameLogic.leavePveMatch(s);
            const match = gameLogic.getTdmMatch(s, false);
            if (match) match.players = match.players.filter(p => p !== s);
            s.sceneRetired = true;
        }
        lifecycle.transition(s, 'leaving', 'return-or-disconnect');
        sceneScope.invalidate(s);
        s.inMatch = false;
        s.currentRoomUID = null;
        s.finalMapId = null;
        s.waitingForHeroLock = false;
        s.hasConfirmedHero = false;
        lifecycle.transition(s, s.socketType === 'SCENE' ? 'closed' : 'lobby', 'match-state-cleared');
    }
    
    // Limpa socket fornecido
    if (socket) {
        socket.inMatch = false;
        socket.force = null;
        socket.waitingForHeroLock = false;
        socket.hasConfirmedHero = false;
        socket.finalMapId = null;
        socket.targetMapId = 5202;
        socket.targetMapName = 'bachang_baxi';
        socket.matchKills = 0;
        socket.matchDeaths = 0;
        socket.matchAssists = 0;
        socket.matchDamage = 0;
        socket.matchHeal = 0;
        if (socket.heroLockTimeout) {
            clearTimeout(socket.heroLockTimeout);
            socket.heroLockTimeout = null;
        }
        if (socket.lobbySocket) {
            socket.lobbySocket.inMatch = false;
            socket.lobbySocket.force = null;
            socket.lobbySocket.waitingForHeroLock = false;
            socket.lobbySocket.hasConfirmedHero = false;
            socket.lobbySocket.finalMapId = null;
            socket.lobbySocket.currentRoomUID = null;
        }
        socket.currentRoomUID = null;
    }

    // Limpa sessão persistente
    if (pName && playerSessions[pName]) {
        playerSessions[pName].inMatch = false;
        playerSessions[pName].force = null;
        playerSessions[pName].finalMapId = null;
        playerSessions[pName].targetMapId = 5202;
        playerSessions[pName].targetMapName = 'bachang_baxi';
        playerSessions[pName].waitingForHeroLock = false;
        playerSessions[pName].hasConfirmedHero = false;
        playerSessions[pName].currentRoomUID = null;
        playerSessions[pName].playerIndex = null;
    }

    // Limpa lobby socket se encontrado em activeSockets
    if (pName) {
        const lobbyMatch = activeSockets.find(s => s.socketType === "LOBBY" && s.playerName === pName && !s.destroyed);
        if (lobbyMatch && lobbyMatch !== socket) {
            lobbyMatch.inMatch = false;
            lobbyMatch.force = null;
            lobbyMatch.waitingForHeroLock = false;
            lobbyMatch.hasConfirmedHero = false;
            lobbyMatch.finalMapId = null;
            lobbyMatch.currentRoomUID = null;
        }
    }
}

// ---------------------------------------------------------------------------
// 🏠 SISTEMA DE CONTAINERS DE SALAS AUTÔNOMOS
// ---------------------------------------------------------------------------
const roomContainers = new Map();
global.roomContainers = roomContainers;
let nextRoomUID = 1000;
let currentRoom = null; // Mantido como fallback dinâmico

function getRoom(roomUID) {
    if (!roomUID) return null;
    return roomContainers.get(Number(roomUID)) || null;
}

function getSocketRoom(socket) {
    if (socket.currentRoomUID && roomContainers.has(Number(socket.currentRoomUID))) {
        return roomContainers.get(Number(socket.currentRoomUID));
    }
    for (const room of roomContainers.values()) {
        for (let i = 0; i < 10; i++) {
            const p = room.players[i];
            if (p && (p.socket === socket || (socket.playerName && p.name === socket.playerName))) {
                socket.currentRoomUID = room.roomUID;
                return room;
            }
        }
    }
    return null;
}

function getSceneSocketsForRoom(roomUID) {
    if (roomUID == null) return [];
    return activeSockets.filter(s => s.socketType === "SCENE" && !s.destroyed && !s.sceneRetired &&
        s.isCombatReady && s.currentRoomUID != null && String(s.currentRoomUID) === String(roomUID));
}

function broadcastToSceneRoom(roomUID, packet, excludeSocket = null) {
    const targets = getSceneSocketsForRoom(roomUID);
    for (const s of targets) {
        if (s !== excludeSocket) {
            s.write(packet);
        }
    }
}

function getRoomActivePlayersCount(room) {
    if (!room || !room.players) return 0;
    let count = 0;
    for (let i = 0; i < 10; i++) {
        if (room.players[i]) count++;
    }
    return count;
}

function buildRoomData(room) {
    if (!room) return '((),())';
    const one = utils.encodeInt(1);
    const zero = utils.encodeInt(0);
    
    // 1. Build roomInfo array
    const roomInfo = `(${utils.encodeInt(room.mapId)},'${utils.encodeHex(room.creatorName)}','${utils.encodeHex(room.roomName)}','${utils.encodeHex(room.password)}',${utils.encodeInt(room.mapId)},${utils.encodeInt(room.mapType)},${zero},${utils.encodeInt(room.maxPlayers)},${zero},${zero},${zero},${utils.encodeInt(getRoomActivePlayersCount(room))},${zero},${utils.encodeInt(room.state)},${zero},${utils.encodeInt(7777)},'${room.createTime}',${utils.encodeInt(room.roomUID)},${zero})`;
    
    // 2. Build userList array (10 slots = 20 elements)
    const userListElements = [];
    for (let i = 0; i < 10; i++) {
        const p = room.players[i];
        if (p) {
            const force = i < 5 ? 1 : 2;
            const isReady = p.isReady ? 1 : 0;
            const isCaptain = p.isCaptain ? 1 : 0;
            
            // Build player's heroInfo
            const heroId = p.socket.currentHeroId || 1015;
            const heroIdEnc = utils.encodeInt(heroId);
            const pProfile = dbPersistence.loadPlayer(p.socket.playerName || p.name);
            const pLevel = pProfile && pProfile.level ? pProfile.level : (p.socket.level || 1);
            const pLevelEnc = utils.encodeInt(pLevel);
            const cust = getPlayerCustomizationData(p.socket.playerName || p.name, heroId);
            const userDataHeroInfo = `(${heroIdEnc},${utils.encodeInt(cust.skinId)},${utils.encodeInt(cust.emoteId)},${one},${utils.encodeInt(cust.weaponId)})`;
            
            const isCapOpt = utils.encodeInt(isCaptain);
            const prerogativeLobby = `(${isCapOpt},${isCapOpt},${isCapOpt},${isCapOpt},'${p.socket.playerNameEnc || utils.encodeHex(p.name)}',${zero},'',${zero})`;
            const uData = `('${p.socket.playerNameEnc || utils.encodeHex(p.name)}',${pLevelEnc},${one},${utils.encodeInt(force)},${utils.encodeInt(isReady)},${one},${heroIdEnc},${one},${one},${one},${utils.encodeInt(cust.faceId)},'Local',${zero},${userDataHeroInfo},${one},${zero},${zero},${utils.encodeInt(isCaptain)},${prerogativeLobby},())`;
            
            userListElements.push(`'${p.socket.playerNameEnc || utils.encodeHex(p.name)}',${uData}`);
        } else {
            userListElements.push(`'',()`);
        }
    }
    const userList = `(${userListElements.join(',')})`;
    
    return `(${roomInfo},${userList})`;
}

function buildRoomUserList(room) {
    if (!room) return '()';
    const one = utils.encodeInt(1);
    const zero = utils.encodeInt(0);
    const userListElements = [];
    for (let i = 0; i < 10; i++) {
        const p = room.players[i];
        if (p) {
            const force = i < 5 ? 1 : 2;
            const isReady = p.isReady ? 1 : 0;
            const isCaptain = p.isCaptain ? 1 : 0;
            
            // Build player's heroInfo
            const heroId = p.socket.currentHeroId || 1015;
            const heroIdEnc = utils.encodeInt(heroId);
            const pProfile = dbPersistence.loadPlayer(p.socket.playerName || p.name);
            const pLevel = pProfile && pProfile.level ? pProfile.level : (p.socket.level || 1);
            const pLevelEnc = utils.encodeInt(pLevel);
            const cust = getPlayerCustomizationData(p.socket.playerName || p.name, heroId);
            const userDataHeroInfo = `(${heroIdEnc},${utils.encodeInt(cust.skinId)},${utils.encodeInt(cust.emoteId)},${one},${utils.encodeInt(cust.weaponId)})`;
            
            const isCapOpt = utils.encodeInt(isCaptain);
            const prerogativeLobby = `(${isCapOpt},${isCapOpt},${isCapOpt},${isCapOpt},'${p.socket.playerNameEnc || utils.encodeHex(p.name)}',${zero},'',${zero})`;
            const uData = `('${p.socket.playerNameEnc || utils.encodeHex(p.name)}',${pLevelEnc},${one},${utils.encodeInt(force)},${utils.encodeInt(isReady)},${one},${heroIdEnc},${one},${one},${one},${utils.encodeInt(cust.faceId)},'Local',${zero},${userDataHeroInfo},${one},${zero},${zero},${utils.encodeInt(isCaptain)},${prerogativeLobby},())`;
            
            userListElements.push(`'${p.socket.playerNameEnc || utils.encodeHex(p.name)}',${uData}`);
        } else {
            userListElements.push(`'',()`);
        }
    }
    return `(${userListElements.join(',')})`;
}

function broadcastToRoom(room, packet) {
    if (!room || !room.players) return;
    for (let i = 0; i < 10; i++) {
        const p = room.players[i];
        if (p && p.socket && !p.socket.destroyed) {
            p.socket.write(packet);
        }
    }
}

function removePlayerFromRoom(room, socket) {
    if (!room || !room.players) return;
    let pSlot = -1;
    for (let i = 0; i < 10; i++) {
        const p = room.players[i];
        if (p && (p.socket === socket || (socket.playerName && p.name === socket.playerName))) {
            pSlot = i;
            break;
        }
    }
    if (pSlot === -1) return;

    const leavingPlayer = room.players[pSlot];
    room.players[pSlot] = null;
    socket.currentRoomUID = null;

    const remainingCount = getRoomActivePlayersCount(room);
    console.log(`[🏠 CONTAINER] Jogador ${leavingPlayer.name} saiu do slot ${pSlot} da Sala ${room.roomUID}. Restantes: ${remainingCount}`);

    if (remainingCount === 0) {
        // Se a sala estiver em combate (state === 4) ou com partida TDM ativa, concede tolerância de 45 segundos para reconexão
        if (room.state === 4 || (room.tdmMatch && room.tdmMatch.state !== 'ended')) {
            console.log(`[⏳ CONTAINER] Sala ${room.roomUID} ficou sem jogadores ativos, mas a partida continua ativa (state: ${room.state}). Aguardando reconexão (45s)...`);
            if (!room.autoDestroyTimer) {
                room.autoDestroyTimer = setTimeout(() => {
                    if (getRoomActivePlayersCount(room) === 0) {
                        roomContainers.delete(room.roomUID);
                        if (room.tdmMatch) {
                            gameLogic.endTdmMatch(room.tdmMatch);
                        }
                        if (currentRoom && currentRoom.roomUID === room.roomUID) {
                            currentRoom = null;
                        }
                        console.log(`[🗑️ CONTAINER] Container da Sala ${room.roomUID} (${room.roomName}) auto-destruído após timeout de reconexão.`);
                    }
                }, 45000);
            }
        } else {
            // 🗑️ AUTO-DESTRUIÇÃO: Quando não tem nenhum player no container e não está em jogo, ele para de existir!
            roomContainers.delete(room.roomUID);
            if (currentRoom && currentRoom.roomUID === room.roomUID) {
                currentRoom = null;
            }
            console.log(`[🗑️ CONTAINER] Container da Sala ${room.roomUID} (${room.roomName}) foi DESTRUÍDO por estar vazio!`);
        }
    } else {
        // 👑 INDEPENDÊNCIA DE HOST: Se quem saiu era capitão, promove automaticamente o próximo jogador
        if (leavingPlayer.isCaptain) {
            const nextPlayer = room.players.find(p => p !== null);
            if (nextPlayer) {
                nextPlayer.isCaptain = 1;
                nextPlayer.isReady = 1;
                room.creatorName = nextPlayer.name;
                console.log(`[👑 CONTAINER] Novo capitão da Sala ${room.roomUID}: ${nextPlayer.name}!`);
            }
        }
        broadcastToRoom(room, `e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3318)},(1,${buildRoomUserList(room)})))\n`);
    }
}

// ---------------------------------------------------------------------------
// 🎯 COORDENADOR DE FILA DE MATCHMAKING UNIFICADO
// ---------------------------------------------------------------------------
let matchmakingQueue = [];
let matchmakingTimer = null;

function matchmakingKey(entry) {
    return entry.isPve ? `pve:${entry.pveType}:${entry.pveLv}:${entry.pveId}` : (entry.nType === 11 ? 'individual' : `mode:${entry.nType || 7}`);
}

const MODE_MAP_POOLS = {
    7: [5204, 5010, 5024, 5210, 5212],
    3: [5030, 5074, 5068, 5050, 5055, 5208],
    2: [5063, 5022],
    5: [5049, 5260],
    4: [5056, 5058, 5059]
};

function processMatchmakingQueue() {
    if (matchmakingTimer) {
        clearTimeout(matchmakingTimer);
        matchmakingTimer = null;
    }
    
    // Filtra sockets válidos, ativos e não destruídos
    matchmakingQueue = matchmakingQueue.filter(entry => entry.socket && !entry.socket.destroyed && !entry.socket.inMatch);
    if (matchmakingQueue.length === 0) return;
    
    const key = matchmakingKey(matchmakingQueue[0]);
    const queuedEntries = matchmakingQueue.filter(e => matchmakingKey(e) === key).slice(0, key.startsWith('pve:') ? 4 : 10);
    matchmakingQueue = matchmakingQueue.filter(e => !queuedEntries.includes(e));
    if (matchmakingQueue.length) matchmakingTimer = setTimeout(processMatchmakingQueue, 0);
    
    console.log(`[🎯 MATCHMAKING COORD] Agrupando ${queuedEntries.length} jogador(es) no mesmo container de partida!`);
    
    const firstEntry = queuedEntries[0];
    const isPve = firstEntry.isPve;
    const pveType = firstEntry.pveType;
    const pveLv = firstEntry.pveLv;
    const pveId = firstEntry.pveId;
    
    const PVP_MAP_POOL = firstEntry.nType === 11 ? combat.mapIds : (MODE_MAP_POOLS[firstEntry.nType] || MODE_MAP_POOLS[7]);
    const sharedMapId = isPve ? pveId : PVP_MAP_POOL[Math.floor(Math.random() * PVP_MAP_POOL.length)];
    const mapConf = MAP_CONFIG[sharedMapId] || (isPve ? { name: (pveType === 12 ? 'hasd_qingshen' : 'shuijingkuangdong'), type: pveType } : { name: 'jiaodouchang_02_3', type: firstEntry.nType || 7 });
    
    state.setLastMapId(sharedMapId);
    lastMapName = mapConf.name;
    lastFinalMapId = sharedMapId;
    lastFinalMapName = mapConf.name;
    
    // All queued modes use the existing hero-selection scene before combat.
    const stagingMapId = 5010;
    const stagingMapConf = MAP_CONFIG[stagingMapId] || mapConf;
    
    // 🏠 Cria um Container dedicado para a partida de Matchmaking
    nextRoomUID++;
    const matchRoomUID = nextRoomUID;
    const matchRoom = {
        roomUID: matchRoomUID,
        isMatchmaking: true,
        roomName: `Match_${matchRoomUID}`,
        password: '',
        mapId: stagingMapId,
        finalMapId: sharedMapId,
        mapType: stagingMapConf.type,
        maxPlayers: queuedEntries.length,
        creatorName: firstEntry.socket.playerName || 'Matchmaker',
        state: 4, // Em combate
        createTime: utils.Fun.GetServerDate(),
        players: new Array(10).fill(null)
    };
    
    const one = utils.encodeInt(1);
    const zero = utils.encodeInt(0);
    const startTime = utils.Fun.GetServerDate();
    const p80001 = utils.encodeInt(80001);
    const s80011 = utils.encodeInt(80011);
    const s90001 = utils.encodeInt(90001);
    const s90002 = utils.encodeInt(90002);
    const s90004 = utils.encodeInt(90004);
    const p1304 = utils.encodeInt(1304);
    
    // Configura cada jogador na fila: alterna equipes (1=Azul, 2=Vermelho)
    socialServer.recordRecentPlayers(queuedEntries.map(e => e.socket && e.socket.playerName).filter(Boolean));
    queuedEntries.forEach((entry, idx) => {
        const s = entry.socket;
        const assignedForce = isPve ? 1 : (idx % 2 === 0 ? 1 : 2);
        const playerIndex = (global.userIndexes && s.playerName && global.userIndexes[s.playerName]) ? global.userIndexes[s.playerName] : (idx + 1);
        
        s.playerIndex = playerIndex;
        s.playerId = String(playerIndex);
        s.force = assignedForce;
        s.currentRoomUID = matchRoomUID;
        s.inMatch = true;
        s.targetMapId = stagingMapId;
        s.finalMapId = sharedMapId;
        s.targetMapName = stagingMapConf.name;
        s.isPveMatch = isPve;
        s.pveFbType = pveType;
        s.pveFbLv = pveLv;
        s.pveFbId = pveId;
        s.waitingForHeroLock = true;
        s.hasConfirmedHero = false;
        lifecycle.transition(s, 'selecting', 'queue-admission');
        
        matchRoom.players[isPve ? idx : Math.floor(idx / 2) + (assignedForce === 2 ? 5 : 0)] = {
            socket: s,
            name: s.playerName,
            force: assignedForce,
            isReady: 1,
            isCaptain: idx === 0 ? 1 : 0
        };
        
        if (s.playerName && playerSessions[s.playerName]) {
            playerSessions[s.playerName].currentRoomUID = matchRoomUID;
            playerSessions[s.playerName].finalMapId = sharedMapId;
            playerSessions[s.playerName].targetMapId = stagingMapId;
            playerSessions[s.playerName].targetMapName = stagingMapConf.name;
            playerSessions[s.playerName].isPveMatch = isPve;
            playerSessions[s.playerName].pveFbType = pveType;
            playerSessions[s.playerName].pveFbLv = pveLv;
            playerSessions[s.playerName].pveFbId = pveId;
            playerSessions[s.playerName].force = assignedForce;
        }
        
        const myHeroId = s.currentHeroId || 1015;
        const myHeroIdEnc = utils.encodeInt(myHeroId);
        const cust = getPlayerCustomizationData(s.playerName, myHeroId);
        const profileMe = dbPersistence.loadPlayer(s.playerName);
        const myLevel = profileMe && profileMe.level ? profileMe.level : (s.level || 1);
        const myUserDataHeroInfo = `(${myHeroIdEnc},${utils.encodeInt(cust.skinId)},${utils.encodeInt(cust.emoteId)},${one},${utils.encodeInt(cust.weaponId)})`;
        const isCapEnc = utils.encodeInt(idx === 0 ? 1 : 0);
        const prerogativeLobbyMe = `(${isCapEnc},${isCapEnc},${isCapEnc},${isCapEnc},'${s.playerNameEnc}',${zero},'',${zero})`;
        const netIdEnc = utils.encodeInt(s.playerIndex || 1);
        s.userData = `('${s.playerNameEnc}',${utils.encodeInt(myLevel)},${one},${utils.encodeInt(assignedForce)},${one},${one},${myHeroIdEnc},${one},${one},${zero},${utils.encodeInt(cust.faceId)},'Local',${zero},${myUserDataHeroInfo},${netIdEnc},${zero},${zero},${isCapEnc},${prerogativeLobbyMe},())`;
    });
    
    roomContainers.set(matchRoomUID, matchRoom);
    
    // Constrói a lista com TODOS os jogadores da fila juntos
    let userListElements = [];
    queuedEntries.forEach(entry => {
        userListElements.push(`'${entry.socket.playerNameEnc}',${entry.socket.userData}`);
    });
    global.currentMatchBots = [];
    const userList = userListElements.join(',');
    
    const isChangeHeroEnc = utils.encodeInt(1);
    const matchMsg = `(${s90001},(${userList}),${utils.encodeInt(stagingMapId)},${utils.encodeInt(stagingMapConf.type)},'${stagingMapConf.name}',${utils.encodeInt(60)},${isChangeHeroEnc},'${startTime}',${zero},${zero})`;
    
    // 1. Avisa primeiro que a partida foi encontrada (80011 com flag=1)
    queuedEntries.forEach(entry => {
        const s = entry.socket;
        s.write(`e1:SendMsgToHallClient,(${p80001},(${s80011},(1,${utils.encodeInt(queuedEntries.length)},${utils.encodeInt(queuedEntries.length)},())))\n`);
    });
    
    // 2. Fecha a busca e transiciona para a seleção de heróis
    queuedEntries.forEach(entry => {
        const s = entry.socket;
        const admissionCurrent = sceneScope.capture(s);
        const activeAdmission = () => admissionCurrent() && s.inMatch && matchRoom.state === 4;
        (async () => {
            await utils.sleep(300);
            if (!activeAdmission()) return;
            s.write(`e1:SendMsgToHallClient,(${p80001},(${utils.encodeInt(80007)},()))\n`);
            if (!activeAdmission()) return;
            await utils.sendPacket(s, `e1:SendMsgToHallClient,(${p80001},${matchMsg})\n`, 400);
            if (!activeAdmission()) return;
            await utils.sendPacket(s, `e1:SendMsgToHallClient,(${p80001},(${s90002},'${s.playerNameEnc}',${s.userData}))\n`, 400);

            setTimeout(() => {
                if (activeAdmission()) {
                    s.write(`e1:SendMsgToHallClient,(${p1304},(${utils.encodeInt(s.playerIndex || 1)},'${s.playerNameEnc}','${SERVER_IP}',${utils.encodeInt(7777)},'SceneGroup',${one}))\n`);
                }
            }, 1500);
        })().catch(err => console.error('[🎯 ERRO no envio sincronizado do Quick Match]', err));
    });
}

const tcpServer = net.createServer((socket) => {
    lifecycle.attach(socket);
    lifecycle.event(socket, 'CONNECTION_OPEN');
    socket.setNoDelay(true); // Desativa algoritmo de Nagle para latência mínima e zero jitter
    socket.setKeepAlive(true, 15000); // Mantém conexões TCP ativas contra timeouts de NAT/firewall da nuvem
    activeSockets.push(socket);
    let socketType = "DESCONHECIDO";
    socket.isEncrypted = false;

    const ip = socket.remoteAddress;
    if (!ipNames[ip]) {
        const uniqueIpsCount = Object.keys(ipNames).length;
        ipNames[ip] = `Jogador_${uniqueIpsCount + 1}`;
        ipIndexes[ip] = uniqueIpsCount + 1;
        console.log(`[🌐 IP REGISTRY] IP ${ip} registrado como Jogador_${uniqueIpsCount + 1} (Index: ${uniqueIpsCount + 1})`);
    }
    socket.playerName = ipNames[ip];
    socket.playerNameEnc = utils.encodeHex(socket.playerName);
    socket.playerIndex = ipIndexes[ip];
    socket.playerId = String(socket.playerIndex);

    // INTERCEPTOR CRÍTICO DE PREFIXO MULTIPLAYER:
    // Garante que mensagens direcionadas à UI local do jogador (SendMsgToHallClient ou SendMsgToClient)
    // usem o playerIndex dinâmico do socket apenas no servidor de cena (socketType === "SCENE").
    // No Lobby (LOBBY), a UI do jogador sempre espera o prefixo "e1:" (pois mHallUser:1 é fixo).
    const originalWrite = socket.write;
    socket.write = function (data, encoding, callback) {
        if (socket.socketType === "SCENE" && !socket.bypassInterceptor) {
            if (typeof data === 'string') {
                if (data.includes('SendMsgToHallClient') || data.includes('SendMsgToClient')) {
                    const idxPrefix = `e${utils.encodeInt(socket.playerIndex || 1)}:`;
                    data = data.replace(/^e[0-9A-Z_`a-z]+:(?=SendMsgTo(?:Hall)?Client,)/gm, idxPrefix);
                }
            } else if (Buffer.isBuffer(data)) {
                let str = data.toString('utf8');
                if (str.includes('SendMsgToHallClient') || str.includes('SendMsgToClient')) {
                    const idxPrefix = `e${utils.encodeInt(socket.playerIndex || 1)}:`;
                    str = str.replace(/^e[0-9A-Z_`a-z]+:(?=SendMsgTo(?:Hall)?Client,)/gm, idxPrefix);
                    data = Buffer.from(str, 'utf8');
                }
            }
        }
        
        if (global.broadcastPacket) {
            try {
                let text = "";
                if (Buffer.isBuffer(data)) {
                    text = data.toString('utf8');
                } else if (typeof data === 'string') {
                    text = data;
                }
                global.broadcastPacket({
                    direction: 'out',
                    socketType: socket.socketType || 'DESCONHECIDO',
                    playerId: socket.playerId || socket.playerIndex || '?',
                    playerName: socket.playerName || '?',
                    raw: text
                });
            } catch (e) {}
        }

        return originalWrite.call(socket, data, encoding, callback);
    };

    console.log(`\n========================================`);
    console.log(`[🟢 NOVA CONEXÃO ABERTA] IP: ${socket.remoteAddress} (Nome: ${socket.playerName}, Index: ${socket.playerIndex})`);
    console.log(`========================================`);

    socket.write("B1A_6CDA_GEMINI");

    socket.on('data', data => clientStream.consume(socket, data, handleMessage));
    function handleMessage(textoRecebido) {
        if (global.broadcastPacket) {
            try {
                global.broadcastPacket({
                    direction: 'in',
                    socketType: socket.socketType || 'DESCONHECIDO',
                    playerId: socket.playerId || socket.playerIndex || '?',
                    playerName: socket.playerName || '?',
                    raw: textoRecebido
                });
            } catch (e) {}
        }

        // Interceptar logs do cliente C# globally
        let remainingLines = [];
        const receivedLines = textoRecebido.split('\n');
        for (let line of receivedLines) {
            const trimmed = line.trim();
            if (trimmed.startsWith('LOG:')) {
                const clientName = socket.playerName || `Jogador_${socket.playerIndex || '?'}`;
                console.log(`\x1b[36m[📱 CLIENT LOG] (${clientName} / ${socket.remoteAddress}): ${trimmed.substring(4)}\x1b[0m`);
            } else {
                remainingLines.push(line);
            }
        }

        if (remainingLines.length === 0 || remainingLines.every(l => !l.trim())) {
            return;
        }
        textoRecebido = remainingLines.join('\n');

        if (textoRecebido.includes("LoginUser")) socketType = "LOGIN";
        else if (textoRecebido.includes("HallUser")) socketType = "LOBBY";
        else if (textoRecebido.includes("PlayerUser")) socketType = "SCENE";
        socket.socketType = socketType;
        if (socketType === 'SCENE' && socket.sceneRetired) return;

        if (socketType === "LOBBY" && socket.playerIndex === undefined) {
            socket.playerIndex = ipIndexes[ip] || 1;
            socket.playerId = String(socket.playerIndex);
            socket.currentHeroId = 1015;
            socket.targetMapId = 5202;
            socket.targetMapName = 'bachang_baxi';
        }

        if (socketType === "SCENE" && (socket.force === undefined || textoRecebido.includes("NGOL"))) {
            const ip = socket.remoteAddress;
            let lobbyMatch = null;
            
            let extractedName = gameLogic.getPlayerNameFromNgol(textoRecebido);
            if (extractedName) {
                socket.playerName = extractedName;
                socket.playerNameEnc = utils.encodeHex(extractedName);
                
                // Procurar Lobby socket correspondente pelo playerName para evitar colisões de IP local
                lobbyMatch = activeSockets.find(s => s.socketType === "LOBBY" && s.playerName === extractedName && !s.destroyed);
                
                // Garantir que a sessão persistente exista no playerSessions
                if (!playerSessions[extractedName]) {
                    if (!global.userIndexes) global.userIndexes = {};
                    if (!global.userIndexes[extractedName]) {
                        global.globalPlayerCounter = (global.globalPlayerCounter || 0) + 1;
                        global.userIndexes[extractedName] = global.globalPlayerCounter;
                    }
                    playerSessions[extractedName] = {
                        playerIndex: lobbyMatch ? (lobbyMatch.playerIndex || global.userIndexes[extractedName]) : global.userIndexes[extractedName],
                        playerId: lobbyMatch ? (lobbyMatch.playerId || String(global.userIndexes[extractedName])) : String(global.userIndexes[extractedName]),
                        playerName: extractedName,
                        force: lobbyMatch ? (lobbyMatch.force || 1) : 1,
                        targetMapId: lobbyMatch ? (lobbyMatch.targetMapId || 5202) : 5202,
                        targetMapName: lobbyMatch ? (lobbyMatch.targetMapName || 'bachang_baxi') : 'bachang_baxi',
                        currentHeroId: lobbyMatch ? (lobbyMatch.currentHeroId || 1015) : 1015,
                        currentSkinId: lobbyMatch ? (lobbyMatch.currentSkinId || 1) : 1,
                        currentWeaponId: lobbyMatch ? (lobbyMatch.currentWeaponId || 1) : 1,
                        waitingForHeroLock: lobbyMatch ? lobbyMatch.waitingForHeroLock : false,
                        hasConfirmedHero: lobbyMatch ? lobbyMatch.hasConfirmedHero : false,
                        finalMapId: lobbyMatch ? lobbyMatch.finalMapId : null,
                        currentRoomUID: lobbyMatch ? lobbyMatch.currentRoomUID : (socket.currentRoomUID || null)
                    };
                }
                
                if (lobbyMatch) {
                    if (lobbyMatch.playerIndex) playerSessions[extractedName].playerIndex = lobbyMatch.playerIndex;
                    if (lobbyMatch.playerId) playerSessions[extractedName].playerId = lobbyMatch.playerId;
                    playerSessions[extractedName].finalMapId = lobbyMatch.finalMapId;
                    playerSessions[extractedName].targetMapId = lobbyMatch.targetMapId;
                    playerSessions[extractedName].targetMapName = lobbyMatch.targetMapName;
                    playerSessions[extractedName].force = lobbyMatch.force !== undefined ? lobbyMatch.force : 1;
                    playerSessions[extractedName].currentHeroId = lobbyMatch.currentHeroId || 1015;
                    if (lobbyMatch.currentSkinId !== undefined) playerSessions[extractedName].currentSkinId = lobbyMatch.currentSkinId;
                    if (lobbyMatch.currentWeaponId !== undefined) playerSessions[extractedName].currentWeaponId = lobbyMatch.currentWeaponId;
                    playerSessions[extractedName].waitingForHeroLock = lobbyMatch.waitingForHeroLock || false;
                    playerSessions[extractedName].hasConfirmedHero = lobbyMatch.hasConfirmedHero || false;
                    if (lobbyMatch.currentRoomUID !== undefined) {
                        playerSessions[extractedName].currentRoomUID = lobbyMatch.currentRoomUID;
                        socket.currentRoomUID = lobbyMatch.currentRoomUID;
                    }
                }
                
                syncSocketFromSession(socket);
                socket.inMatch = true;
                socket.sceneRetired = false;
                for (const old of activeSockets) {
                    if (old !== socket && old.socketType === 'SCENE' && old.playerName === extractedName && !old.sceneRetired) {
                        old.sceneRetired = true;
                        lifecycle.transition(old, 'closed', 'scene-replaced');
                        sceneScope.invalidate(old);
                        const oldMatch = gameLogic.getTdmMatch(old, false);
                        if (oldMatch) oldMatch.players = oldMatch.players.filter(p => p !== old);
                        old.destroy();
                    }
                }
                
                if (lobbyMatch) {
                    socket.lobbySocket = lobbyMatch;
                    state.setLastLobbySocket(lobbyMatch);
                }
                
                console.log(`[🌐 SCENE] Sessão persistente recuperada com sucesso para: ${extractedName} (ID: ${socket.playerId}, Index: ${socket.playerIndex})`);
            }
            
            if (!socket.playerIndex) {
                if (socket.playerName) {
                    lobbyMatch = activeSockets.find(s => s.socketType === "LOBBY" && s.playerName === socket.playerName && !s.destroyed);
                }
                if (!lobbyMatch) {
                    lobbyMatch = activeSockets.find(s => s.socketType === "LOBBY" && s.remoteAddress === socket.remoteAddress && !s.destroyed);
                }
                if (lobbyMatch) {
                    socket.playerId = lobbyMatch.playerId || String(lobbyMatch.playerIndex || 1);
                    socket.playerIndex = lobbyMatch.playerIndex || 1;
                    socket.force = lobbyMatch.force !== undefined ? lobbyMatch.force : 1;
                    socket.playerName = lobbyMatch.playerName;
                    socket.playerNameEnc = lobbyMatch.playerNameEnc;
                    socket.currentHeroId = lobbyMatch.currentHeroId || 1015;
                    socket.targetMapId = lobbyMatch.targetMapId || 5202;
                    socket.targetMapName = lobbyMatch.targetMapName || 'bachang_baxi';
                    socket.finalMapId = lobbyMatch.finalMapId;
                    socket.lobbySocket = lobbyMatch;
                    state.setLastLobbySocket(lobbyMatch);
                } else {
                    socket.playerId = String(ipIndexes[ip] || 1);
                    socket.playerIndex = ipIndexes[ip] || 1;
                    socket.force = 1;
                    socket.targetMapId = 5202;
                    socket.targetMapName = 'bachang_baxi';
                    console.log(`[🌐 SCENE] IP ${ip} não encontrou sessão de LOBBY ativa. Usando dados padrão.`);
                }
            }
            if (socket.finalMapId) {
                lastFinalMapId = socket.finalMapId;
            }
        }

        utils.logPacket(socketType, 'IN', textoRecebido);
        packetSniffer.inspect('RECV', socketType, textoRecebido, socket);

        // --- CENA 1: TENTATIVA DE LOGIN ---
        if (socketType === "LOGIN" && textoRecebido.includes("NGOL")) {
            console.log(`[🔐 LOGIN] Autenticando...`);
            socket.isEncrypted = true;
            
            const ip = socket.remoteAddress;
            const parts = textoRecebido.split(',');
            let username = `Jogador_${(global.totalLogins || 0) + 1}`;
            let password = '';
            if (parts.length >= 4) {
                username = parts[2].replace(/['"\r\n]/g, "").trim();
                password = parts[3].replace(/['"\r\n]/g, "").trim();
            }
            
            const authResult = dbPersistence.authenticate(username, password);
            if (!authResult.ok) {
                console.log(`[🔐 LOGIN] Falha na autenticação para ${username}: ${authResult.error}`);
                socket.write(`cLoginUser:1119,(1000,'${authResult.error}')\n`);
                return;
            }
            
            const resolvedName = authResult.player.username;
            global.totalLogins = (global.totalLogins || 0) + 1;
            if (!global.userIndexes) global.userIndexes = {};
            if (!global.userIndexes[resolvedName]) {
                global.userIndexes[resolvedName] = global.totalLogins;
            }
            ipNames[ip] = resolvedName; // Mantém compatibilidade
            ipIndexes[ip] = global.userIndexes[resolvedName]; // Mantém compatibilidade
            console.log(`[🔐 LOGIN] IP: ${ip} autenticado/registrado como: ${resolvedName} (Index: ${global.userIndexes[resolvedName]})`);
            
            const portEncoded = utils.encodeInt(7777);
            socket.write(`cLoginUser:1,('OnLoginOK',1,1,'${resolvedName}',('${SERVER_IP}',${portEncoded},'HallGroup'))\n`);
        }

        // --- CENA 2: ENTRANDO NO SALÃO (LOBBY) ---
        else if (socketType === "LOBBY" && textoRecebido.includes("NGOL")) {
            console.log(`[🏰 LOBBY] Carregando a tela principal...`);
            const extractedName = gameLogic.getPlayerNameFromNgol(textoRecebido);
            const ip = socket.remoteAddress;
            if (extractedName) {
                socket.playerName = extractedName;
                socket.playerNameEnc = utils.encodeHex(extractedName);
                
                // Procura se já existe sessão ativa
                const activeLobby = activeSockets.find(s => s.socketType === "LOBBY" && s.playerName === extractedName && s !== socket && !s.destroyed);
                if (activeLobby) {
                    socket.playerIndex = activeLobby.playerIndex;
                    socket.playerId = activeLobby.playerId;
                } else {
                    if (!playerSessions[extractedName]) {
                        if (!global.userIndexes) global.userIndexes = {};
                        if (!global.userIndexes[extractedName]) {
                            global.globalPlayerCounter = (global.globalPlayerCounter || 0) + 1;
                            global.userIndexes[extractedName] = global.globalPlayerCounter;
                        }
                        playerSessions[extractedName] = {
                            playerIndex: global.userIndexes[extractedName],
                            playerId: String(global.userIndexes[extractedName]),
                            playerName: extractedName,
                            force: 1,
                            targetMapId: 5202,
                            targetMapName: 'bachang_baxi',
                            currentHeroId: 1015,
                            currentSkinId: 1,
                            currentWeaponId: 1,
                            finalMapId: null
                        };
                    }
                    socket.playerIndex = playerSessions[extractedName].playerIndex;
                    socket.playerId = playerSessions[extractedName].playerId;
                }
                console.log(`[🏰 LOBBY] Identificado com sucesso: ${socket.playerName} (ID: ${socket.playerId})`);
                lifecycle.transition(socket, 'lobby', 'hall-connected');
            }
            socket.isEncrypted = true;
            socket.write(`mHallUser:1\n`);

            // Fila de pacotes a enviar, cada um com seu delay
            const sendQueue = async () => {
                console.log(`[📤 LOBBY] Injetando dados do jogador...`);

                // Carrega dados do banco de dados do jogador
                const profile = dbPersistence.loadPlayer(socket.playerName) || dbPersistence.authenticate(socket.playerName, "123").player;
                
                // Inicializa no estado em memória
                state.playerInventory[socket.playerName] = {
                    gold: profile.gold !== undefined ? profile.gold : 999999,
                    diamonds: profile.diamonds !== undefined ? profile.diamonds : 999999,
                    items: profile.items || {}
                };
                if (!state.playerEquipment[socket.playerName]) {
                    state.playerEquipment[socket.playerName] = profile.equipment || {};
                }

                // ══════════════════════════════════════
                // PACOTE 1: Dados do Jogador (1301)
                // ══════════════════════════════════════
                const faceId = profile.face || 2009;
                const faceIdEnc = utils.encodeInt(faceId);
                const levelEnc = utils.encodeInt(profile.level || 1);
                const expEnc = utils.encodeInt(profile.exp || 0);
                const regDate = profile.createdAt ? profile.createdAt.replace('T', ' ').substring(0, 19).replace(/-/g, '/') : '2024/01/01 12:00:00';
                const userCid = profile.cid || ((global.userIndexes && socket.playerName && global.userIndexes[socket.playerName]) ? global.userIndexes[socket.playerName] : (socket.playerIndex || 1));
                socket.cid = userCid;
                const arr0 = `(${utils.encodeInt(userCid)},'${socket.playerNameEnc}',1,${faceIdEnc},1,1,${levelEnc},${expEnc},1,0,0,0,0,0,'${regDate}',0,0,0,0,0,0,0,0,-1,(),0,0,0,0,0,0)`;
                // Saldo real vindo do banco do jogador:
                const inv = state.playerInventory[socket.playerName];
                const goldEnc = utils.encodeInt(inv.gold);
                const diaEnc = utils.encodeInt(inv.diamonds);
                const arr1 = `(${goldEnc},${goldEnc},${diaEnc},0,${utils.encodeInt(10)},0,${goldEnc},${goldEnc},${goldEnc},0,${goldEnc},0,0,0)`;
                const arr6 = `((),(),())`;
                const arr7 = `(0,1,0,(0))`;
                const nowStr = utils.Fun.GetServerDate();
                const starsList = `(${utils.encodeInt(3)},${utils.encodeInt(3)},${utils.encodeInt(3)},${utils.encodeInt(3)},${utils.encodeInt(3)},${utils.encodeInt(3)},${utils.encodeInt(3)},${utils.encodeInt(3)},${utils.encodeInt(3)},${utils.encodeInt(3)},${utils.encodeInt(3)},${utils.encodeInt(3)})`;
                const sweepList = `(0,0,0,0,0,0,0,0,0,0,0,0)`;
                const pveData9 = `(${utils.encodeInt(12)},${starsList},0,0,0,0,${utils.encodeInt(9999)},0,'${nowStr}',0,0,'${nowStr}',0,${sweepList},0,0)`;
                const pveData12 = `(${utils.encodeInt(12)},${starsList},0,0,0,0,${utils.encodeInt(9999)},0,'${nowStr}',0,0,'${nowStr}',0,${sweepList},0,0)`;
                // ══════════════════════════════════════
                // FORTALECIMENTO / RATING PVE (QIANGHUA)
                // ══════════════════════════════════════
                const qhJie = profile.qhJie !== undefined ? profile.qhJie : 50; // Grau 50 (Máximo)
                const qhLv = profile.qhLv !== undefined ? profile.qhLv : 10;   // Nível 10 (Máximo)
                const jieEnc = utils.encodeInt(qhJie);
                const qhLvEnc = utils.encodeInt(qhLv);

                // Monta os 7 atributos para cada uma das 3 classes (1: Ataque, 2: Defesa, 3: Suporte)
                const qhAttrs = `(${utils.encodeInt(1)},(${jieEnc},${qhLvEnc}),${utils.encodeInt(2)},(${jieEnc},${qhLvEnc}),${utils.encodeInt(3)},(${jieEnc},${qhLvEnc}),${utils.encodeInt(4)},(${jieEnc},${qhLvEnc}),${utils.encodeInt(5)},(${jieEnc},${qhLvEnc}),${utils.encodeInt(6)},(${jieEnc},${qhLvEnc}),${utils.encodeInt(7)},(${jieEnc},${qhLvEnc}))`;
                const strengthenData = `((),${qhAttrs},${qhAttrs},${qhAttrs})`;

                const arr8 = `((),'',0,${utils.encodeInt(100)},${pveData9},${strengthenData},${pveData12},())`;
                const arr9 = `(0,0,0,0,0,0,0,0,0,0)`;
                const arr13 = `('','','','','','','','','','','','','','','')`;
                
                // ══════════════════════════════════════
                // GENES / RUNAS (GENEDATA)
                // ══════════════════════════════════════
                const geneStep = profile.geneStep !== undefined ? profile.geneStep : (profile.runes ? profile.runes.step : 10);
                const geneStar = profile.geneStar !== undefined ? profile.geneStar : (profile.runes ? profile.runes.star : 10);
                const stepEnc = utils.encodeInt(geneStep);
                const starEnc = utils.encodeInt(geneStar);
                const arr15 = `(0,0,0,0,0,0,0,0,0,0,0,0,0,0,(${stepEnc},${starEnc}))`;
                const arr16 = `()`;
                const szMsg = `(${arr0},${arr1},(),(),(),(),${arr6},${arr7},${arr8},${arr9},(),(),(),${arr13},(),${arr15},${arr16},(),(),())`;
                await utils.sendPacket(socket, `e1:SendMsgToHallClient,(KL,${szMsg})\n`, 1000);

                const porta1306 = utils.encodeInt(1306);

                // ══════════════════════════════════════
                // SISTEMAS ADICIONAIS: Avatares, Sprays e Weapon Parts
                // ══════════════════════════════════════
                // 1. Envia lista de Avatares (11001)
                const faceList = db.allFaceIds.map(fId => {
                    const lockStateVal = isFaceUnlocked(fId, profile) ? 2 : 0;
                    return `${utils.encodeInt(fId)},(${utils.encodeInt(lockStateVal)},0,0,0)`;
                }).join(',');
                await utils.sendPacket(socket, `e1:SendMsgToHallClient,(${porta1306},(${utils.encodeInt(11001)},(${faceList})))\n`, 200);

                // 2. Envia lista de Sprays (11008)
                const paintList = db.allPaintIds.map(pId => {
                    const lockStateVal = isPaintUnlocked(pId, profile) ? 2 : 0;
                    return `${utils.encodeInt(pId)},(${utils.encodeInt(pId)},${utils.encodeInt(lockStateVal)},0,0,'2099/01/01 12:00:00',0)`;
                }).join(',');
                const equippedPaintId = utils.encodeInt(profile.paint || 1019);
                await utils.sendPacket(socket, `e1:SendMsgToHallClient,(${porta1306},(${utils.encodeInt(11008)},((${paintList}),${equippedPaintId})))\n`, 200);

                // 3. Envia lista de Weapon Parts (12020)
                const partsList = [];
                for (let p = 1; p <= 8; p++) {
                    const lvl = profile.parts && profile.parts[p] ? profile.parts[p].lvl : 1;
                    const exp = profile.parts && profile.parts[p] ? profile.parts[p].exp : 0;
                    partsList.push(`${p},(${utils.encodeInt(lvl)},${utils.encodeInt(exp)})`);
                }
                await utils.sendPacket(socket, `e1:SendMsgToHallClient,(${porta1306},(${utils.encodeInt(12020)},(${partsList.join(',')})))\n`, 200);

                // Sincronização de tempo inicial
                const serverTime = utils.Fun.GetServerDate();
                const zero = utils.encodeInt(0);
                await utils.sendPacket(socket, `e1:SendMsgToHallClient,(${utils.encodeInt(20058)},('${serverTime}',${zero}))\n`, 200);

                // ══════════════════════════════════════
                // PACOTE 2: Configurações Salvas do Usuário (3010001)
                // ══════════════════════════════════════
                const settings = profile.settings || ["", "", "", "", ""];
                const porta3010001 = utils.encodeInt(3010001);
                await utils.sendPacket(socket, `e1:SendMsgToHallClient,(${porta3010001},('${settings[0]}','${settings[1]}','${settings[2]}','${settings[3]}','${settings[4]}'))\n`, 200);

                // ══════════════════════════════════════
                // PACOTE 3: Lista de Heróis (1306 → 12002) - DYNAMIC UNLOCK ALL
                // ══════════════════════════════════════
                const respostaHerois = utils.encodeInt(12002);
                const heroIds = getHeroIdsList();

                const heroDataArray = heroIds.map(id => {
                    if (!state.playerEquipment[socket.playerName]) {
                        state.playerEquipment[socket.playerName] = {};
                    }
                    if (!state.playerEquipment[socket.playerName][id]) {
                        state.playerEquipment[socket.playerName][id] = (profile.equipment && profile.equipment[id]) || { skin: 1, weapon: 1, emote: 1, pose: 0 };
                    }
                    const eq = state.playerEquipment[socket.playerName][id];
                    return db.generateFullHeroData(id, eq.skin, eq.weapon, eq.emote, eq.pose, profile);
                }).join(',');

                // Envia a lista colossal para o Unity
                socket.write(`e1:SendMsgToHallClient,(${porta1306},(${respostaHerois},(${heroDataArray})))\n`);

                // PACOTE 4: FCM Anti-Vício (1307 → 13001)
                // ══════════════════════════════════════
                const porta1307 = utils.encodeInt(1307);
                const fcmSubCmd = utils.encodeInt(13001);
                await utils.sendPacket(socket, `e1:SendMsgToHallClient,(${porta1307},(${fcmSubCmd},(0,0,0)))\n`, 200);

                const porta1313 = utils.encodeInt(1313);
                const tutorialDone = utils.encodeInt(1);
                await utils.sendPacket(socket, `e1:SendMsgToHallClient,(${porta1313},(${tutorialDone}))\n`, 200);

                // ==========================================
                // PACOTE 5: Atividades (16100 → 16101)
                // ==========================================
                const p16100 = utils.encodeInt(16100);
                // Usa data segura que o C# consegue fazer DateTime.Parse na cultura pt-BR
                const dateVal = `'${utils.Fun.GetServerDate()}'`;
                const dateEnd = `'2099/01/01 12:00:00'`;
                const buildActivityData = (actId, actType, xmlId) => {
                    const fields = [
                        utils.encodeInt(actId),
                        utils.encodeInt(0),
                        utils.encodeInt(0),
                        utils.encodeInt(0),
                        utils.encodeInt(0),
                        utils.encodeInt(actType),
                        '()',
                        '()',
                        utils.encodeInt(0),
                        dateVal,
                        dateEnd,
                        utils.encodeInt(0),
                        utils.encodeInt(0),
                        `'LimitAct'`,
                        utils.encodeInt(xmlId),
                        utils.encodeInt(1)
                    ];
                    return `(${fields.join(',')})`;
                };
                const act1 = buildActivityData(1, 41, 101);
                const act2 = buildActivityData(2, 42, 102);
                const arrayList = `(${act1},${act2})`;
                await utils.sendPacket(socket, `e1:SendMsgToHallClient,(${p16100},(${p16100},(${arrayList},${dateVal},${dateEnd})))\n`, 200);

                // PACOTE 6: Sincronização inicial de carreira (100001)
                const careerPacket = dbPersistence.buildCareerDataPacket(socket.playerName || "Gustavo");
                await utils.sendPacket(socket, careerPacket, 200);

                // PACOTE 7: Inicialização do Modo Aventura / PvE (19500)
                const p19500 = utils.encodeInt(19500);
                // 19515: Nível de desbloqueio da Aventura (Nível 1)
                await utils.sendPacket(socket, `e1:SendMsgToHallClient,(${p19500},(${utils.encodeInt(19515)},(${utils.encodeInt(1)})))\n`, 100);
                // 19511: Vigor / Stamina inicial (100)
                await utils.sendPacket(socket, `e1:SendMsgToHallClient,(${p19500},(${utils.encodeInt(19511)},(${utils.encodeInt(100)})))\n`, 100);

                // PACOTE 8: Inicialização de Amigos (60118) e Clãs/Guildas (91020)
                const friendsPacket = socialServer.buildFriendsInitPacket(socket.playerName || "Gustavo", activeSockets);
                await utils.sendPacket(socket, friendsPacket, 100);
                const guildPacket = socialServer.buildGuildLoginPacket(socket.playerName || "Gustavo");
                await utils.sendPacket(socket, guildPacket, 100);
                socialServer.notifyFriendsState(socket.playerName, activeSockets);

                console.log(`[✅ LOBBY] Todos os pacotes de inicialização enviados!`);
            };
            sendQueue().catch(err => console.error('[❌ ERRO no envio de pacotes]', err));
        }

        // --- CENA 3: JOGADOR CLICANDO EM BOTÕES ---
        else if (socketType === "LOBBY" && textoRecebido.includes("SendMsgToHallServer")) {
            const match = textoRecebido.match(/SendMsgToHallServer,\(([^,]+),(.+)\)\s*$/);
            if (match) {
                const cmdId = utils.decodeInt(match[1]); // Traduz a letra para número
                const args = match[2]; // Dados brutos do comando

                // ==========================================
                // 🚪 RETORNO AO LOBBY / SAÍDA (20002 / 20001 / 1310)
                // ==========================================
                if (cmdId === 20002 || cmdId === 20001 || cmdId === 1310) {
                    console.log(`[🚪 LOBBY] Jogador ${socket.playerName || 'Jogador'} retornou ao Lobby/Hall (Cmd: ${cmdId}). Limpando estado de partida...`);

                    // Remove o jogador do container de sala em que estava
                    const targetRoom = getSocketRoom(socket);
                    cleanPlayerMatchState(socket, socket.playerName, cmdId !== 1310);
                    if (targetRoom) removePlayerFromRoom(targetRoom, socket);

                    // Sincroniza dados frescos de saldo e nível no retorno ao Lobby (10025: SetMoneyData e 21002: EXP)
                    if (socket.playerName) {
                        const profile = dbPersistence.loadPlayer(socket.playerName);
                        if (profile) {
                            const gold = profile.gold !== undefined ? profile.gold : 999999;
                            const diamonds = profile.diamonds !== undefined ? profile.diamonds : 999999;
                            const goldEnc = utils.encodeInt(gold);
                            const diaEnc = utils.encodeInt(diamonds);
                            const szMoney = `(${goldEnc},${goldEnc},${diaEnc},0,${utils.encodeInt(10)},0,${goldEnc},${goldEnc},${goldEnc},0,${goldEnc},0,0,0)`;
                            socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(10025)},(0,${szMoney}))\n`);

                            const curExp = utils.encodeInt(profile.exp || 0);
                            const curLvl = utils.encodeInt(profile.level || 1);
                            socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(21002)},(0,0,${curExp},${curLvl},0))\n`);
                            console.log(`[💰 SYNC LOBBY RETURN] Saldo sincronizado para ${socket.playerName}: ${gold} Ouro, Nível ${profile.level || 1}.`);
                        }
                    }
                    return;
                }

                // ==========================================
                // 🎮 MATCHMAKING TDM MULTIPLAYER (60100)
                // ==========================================
                
                // ==========================================
                // 🕹️ QUICK MATCH (80001) E MATCHMAKING UI
                // ==========================================
                if (cmdId === 80001) {
                    const subCmd = utils.parseArrayList(args);
                    const subId = subCmd[0];
                    
                    if (subId === 80012) {
                        // 📊 O cliente quer saber quantos players estao nas filas!
                        // Resposta: (80012, (fbType, count, fbType, count...))
                        const fakeCounts = "(1,2543,2,1432,4,890)"; // 1=TDM, 2=Ranked, 4=DTS
                        socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(80001)},(${utils.encodeInt(80012)},${fakeCounts}))\n`);
                        console.log(`[📊 MATCHMAKING] Atualizando numeros de jogadores nas filas para o cliente!`);
                    }
                    else if (subId === 80002) {
                        // subCmd: [80002, matchType, playerName, nType, nFbLv, nFbId, nCheck]
                        const matchType = subCmd[1]; // 1=PVP, 2=Ranked, 3=PVE, 4=DTS
                        const playerName = subCmd[2];
                        const nType = subCmd[3];
                        const nFbLv = subCmd[4] !== undefined ? subCmd[4] : 0;
                        const nFbId = subCmd[5];

                        const isPve = (matchType === 3 || nType === 9 || nType === 12);
                        const pveType = isPve ? (nType || 9) : null;
                        const pveLv = isPve ? nFbLv : 0;
                        const pveId = isPve ? (nFbId || (pveType === 12 ? 5115 : 5105)) : 5204;

                        // 🎯 BACKFILL INTELIGENTE: Se houver uma partida já rolando com vagas livres, entra direto nela!
                        if (!isPve) {
                            let activeMatchRoom = Array.from(roomContainers.values()).find(r => 
                                r.isMatchmaking && 
                                r.state === 4 &&
                                Number(MAP_CONFIG[r.finalMapId || r.mapId]?.type) === nType &&
                                (!r.ffaMatch || (r.ffaMatch.state === 'fight' && Date.now() - r.ffaMatch.startedAt < (r.ffaMatch.duration - 20) * 1000)) && 
                                (!r.tdmMatch || (r.tdmMatch.state !== 'ended' && (r.tdmMatch.fightSeconds === undefined || r.tdmMatch.fightSeconds > 20))) &&
                                getRoomActivePlayersCount(r) < 10
                            );

                            if (!activeMatchRoom) {
                                activeMatchRoom = Array.from(roomContainers.values()).find(r => 
                                    !r.isMatchmaking && 
                                    r.state === 4 &&
                                Number(MAP_CONFIG[r.finalMapId || r.mapId]?.type) === nType &&
                                (!r.ffaMatch || (r.ffaMatch.state === 'fight' && Date.now() - r.ffaMatch.startedAt < (r.ffaMatch.duration - 20) * 1000)) && 
                                    (!r.password || r.password === '') &&
                                    (!r.tdmMatch || (r.tdmMatch.state !== 'ended' && (r.tdmMatch.fightSeconds === undefined || r.tdmMatch.fightSeconds > 20))) &&
                                    getRoomActivePlayersCount(r) < (r.maxPlayers || 10)
                                );
                            }

                            if (activeMatchRoom) {
                                const existingSlot = activeMatchRoom.players.findIndex(p => p && p.name === socket.playerName);
                                const joinSlot = existingSlot !== -1 ? existingSlot : findBalancedSlot(activeMatchRoom);

                                if (joinSlot !== -1) {
                                    const assignedForce = joinSlot < 5 ? 1 : 2;
                                    const playerIndex = (global.userIndexes && socket.playerName && global.userIndexes[socket.playerName]) ? global.userIndexes[socket.playerName] : (joinSlot + 1);

                                    socket.playerIndex = playerIndex;
                                    socket.playerId = String(playerIndex);
                                    socket.force = assignedForce;
                                    socket.currentRoomUID = activeMatchRoom.roomUID;
                                    socket.inMatch = true;
                                    socket.targetMapId = activeMatchRoom.finalMapId || activeMatchRoom.mapId;
                                    socket.finalMapId = socket.targetMapId;
                                    const targetMapConf = MAP_CONFIG[socket.targetMapId] || { name: 'jiaodouchang_02_3', type: 7 };
                                    socket.targetMapName = targetMapConf.name;
                                    socket.waitingForHeroLock = true;
                                    socket.hasConfirmedHero = false;
                                    lifecycle.transition(socket, 'selecting', 'quick-backfill');

                                    activeMatchRoom.players[joinSlot] = {
                                        socket: socket,
                                        name: socket.playerName,
                                        force: assignedForce,
                                        isReady: 1,
                                        isCaptain: 0
                                    };

                                    if (socket.playerName && playerSessions[socket.playerName]) {
                                        playerSessions[socket.playerName].currentRoomUID = activeMatchRoom.roomUID;
                                        playerSessions[socket.playerName].finalMapId = socket.targetMapId;
                                        playerSessions[socket.playerName].targetMapId = socket.targetMapId;
                                        playerSessions[socket.playerName].targetMapName = targetMapConf.name;
                                        playerSessions[socket.playerName].force = assignedForce;
                                        playerSessions[socket.playerName].inMatch = true;
                                    }

                                    console.log(`[🎯 MATCHMAKING BACKFILL] Jogador ${socket.playerName} pareado imediatamente na partida ativa Sala ${activeMatchRoom.roomUID} (Time ${assignedForce}, Slot ${joinSlot})!`);

                                    const one = utils.encodeInt(1);
                                    const zero = utils.encodeInt(0);
                                    const dateStr = utils.Fun.GetServerDate();
                                    const p80001 = utils.encodeInt(80001);
                                    const s80011 = utils.encodeInt(80011);
                                    const s90001 = utils.encodeInt(90001);
                                    const s90002 = utils.encodeInt(90002);
                                    const p1304 = utils.encodeInt(1304);

                                    const myHeroId = socket.currentHeroId || 1015;
                                    const myHeroIdEnc = utils.encodeInt(myHeroId);
                                    const cust = getPlayerCustomizationData(socket.playerName, myHeroId);
                                    const profileMe = dbPersistence.loadPlayer(socket.playerName);
                                    const myLevel = profileMe && profileMe.level ? profileMe.level : (socket.level || 1);
                                    const myUserDataHeroInfo = `(${myHeroIdEnc},${utils.encodeInt(cust.skinId)},${utils.encodeInt(cust.emoteId)},${one},${utils.encodeInt(cust.weaponId)})`;
                                    const isCapEnc = zero;
                                    const prerogativeLobbyMe = `(${isCapEnc},${isCapEnc},${isCapEnc},${isCapEnc},'${socket.playerNameEnc}',${zero},'',${zero})`;
                                    const netIdEnc = utils.encodeInt(socket.playerIndex || 1);
                                    socket.userData = `('${socket.playerNameEnc}',${utils.encodeInt(myLevel)},${one},${utils.encodeInt(assignedForce)},${one},${one},${myHeroIdEnc},${one},${one},${zero},${utils.encodeInt(cust.faceId)},'Local',${zero},${myUserDataHeroInfo},${netIdEnc},${zero},${zero},${isCapEnc},${prerogativeLobbyMe},())`;

                                    const userList = getFullMatchUserList(socket);

                                    const isChangeHeroEnc = utils.encodeInt(1);
                                    const matchMsg = `(${s90001},(${userList}),${utils.encodeInt(socket.targetMapId)},${utils.encodeInt(targetMapConf.type)},'${targetMapConf.name}',${utils.encodeInt(60)},${isChangeHeroEnc},'${dateStr}',${one},${zero})`;

                                    socket.write(`e1:SendMsgToHallClient,(${p80001},(${s80011},(1,${one},${one},())))\n`);

                                    const backfillIsCurrent = sceneScope.capture(socket);
                                    (async () => {
                                        await utils.sleep(300);
                                        if (!backfillIsCurrent() || !socket.inMatch || activeMatchRoom.state !== 4) return;
                                        socket.write(`e1:SendMsgToHallClient,(${p80001},(${utils.encodeInt(80007)},()))\n`);
                                        await utils.sendPacket(socket, `e1:SendMsgToHallClient,(${p80001},${matchMsg})\n`, 300);
                                        if (!backfillIsCurrent() || !socket.inMatch || activeMatchRoom.state !== 4) return;
                                        await utils.sendPacket(socket, `e1:SendMsgToHallClient,(${p80001},(${s90002},'${socket.playerNameEnc}',${socket.userData}))\n`, 300);
                                        setTimeout(() => {
                                            if (backfillIsCurrent() && socket.inMatch && activeMatchRoom.state === 4) {
                                                socket.write(`e1:SendMsgToHallClient,(${p1304},(${utils.encodeInt(socket.playerIndex || 1)},'${socket.playerNameEnc}','${SERVER_IP}',${utils.encodeInt(7777)},'SceneGroup',${one}))\n`);
                                            }
                                        }, 800);
                                    })().catch(err => console.error('[❌ ERRO no Backfill do Matchmaking]', err));

                                    return;
                                }
                            }
                        }

                        // 1. Envia pacote 80006: "Procurando Partida..."
                        const targetMax = isPve ? 4 : 2;
                        const requestKey = matchmakingKey({isPve, pveType, pveLv, pveId, nType});
                        const currentCount = matchmakingQueue.filter(e => matchmakingKey(e) === requestKey && e.socket !== socket).length + 1;
                        const searchArr = `(0,60,1,0,${currentCount},${targetMax},(),0,${pveType || matchType || 1})`;
                        socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(80001)},(${utils.encodeInt(80006)},(1,${searchArr})))\n`);

                        // 2. Insere na fila de matchmaking coordenada (evita duplicatas do mesmo socket)
                        matchmakingQueue = matchmakingQueue.filter(entry => entry.socket !== socket);
                        lifecycle.transition(socket, 'queued', 'matchmaking-request');
                        matchmakingQueue.push({
                            socket,
                            playerName,
                            matchType,
                            nType,
                            isPve,
                            pveType,
                            pveLv,
                            pveId
                        });

                        console.log(`[🎯 FILA] Jogador ${playerName || socket.playerName} entrou na fila de matchmaking. Total na fila: ${matchmakingQueue.length}/${targetMax}`);

                        // 3. Atualiza a contagem no MatchUI (80011) de TODOS os jogadores na fila
                        const p80001 = utils.encodeInt(80001);
                        const s80011 = utils.encodeInt(80011);
                        matchmakingQueue.forEach(entry => {
                            if (matchmakingKey(entry) === requestKey && entry.socket && !entry.socket.destroyed) {
                                entry.socket.write(`e1:SendMsgToHallClient,(${p80001},(${s80011},(0,${utils.encodeInt(currentCount)},${utils.encodeInt(targetMax)},())))\n`);
                            }
                        });

                        // 4. Temporizador de Pareamento:
                        // Se atingiu o alvo (ex: 2 players em PVP ou 4 em PVE), inicia em 1.5s
                        // Se for solo, dá 10s de janela para outro jogador entrar antes de iniciar com bots
                        if (matchmakingTimer) clearTimeout(matchmakingTimer);

                        if (currentCount >= targetMax) {
                            matchmakingTimer = setTimeout(processMatchmakingQueue, 1500);
                        } else {
                            matchmakingTimer = setTimeout(processMatchmakingQueue, isPve ? 8000 : 10000);
                        }
                    }
                    else if (subId === 80003) {
                        // 🛑 Cancelar Matchmaking
                        matchmakingQueue = matchmakingQueue.filter(entry => entry.socket !== socket);
                        if (socket.matchPhase === 'queued') lifecycle.transition(socket, 'lobby', 'queue-cancelled');
                        socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(80001)},(${utils.encodeInt(80007)},()))\n`);
                        console.log(`[🎯 FILA] Jogador ${socket.playerName || 'Jogador'} cancelou a busca de partida. Restantes na fila: ${matchmakingQueue.length}`);
                        
                        // Atualiza a contagem (80011) para quem ainda está na fila
                        const p80001 = utils.encodeInt(80001);
                        const s80011 = utils.encodeInt(80011);
                        matchmakingQueue.forEach(entry => {
                            if (entry.socket && !entry.socket.destroyed) {
                                entry.socket.write(`e1:SendMsgToHallClient,(${p80001},(${s80011},(0,${utils.encodeInt(matchmakingQueue.filter(e => matchmakingKey(e) === matchmakingKey(entry)).length)},${utils.encodeInt(entry.isPve ? 4 : 2)},())))\n`);
                            }
                        });
                    }
                    return;
                }

                // ==========================================
                // 🏆 RANKING GLOBAL (17000)
                // 👥 AMIGOS (60100)
                // 🛡️ ESQUADRÃO / PARTY (60000)
                // 🏰 CLÃS / GUILDAS (91000)
                // ==========================================
                if (cmdId === 17000) {
                    socialServer.handleRankCommand(socket, args);
                    return;
                }
                if (cmdId === 60100) {
                    socialServer.handleFriendCommand(socket, args, activeSockets);
                    return;
                }
                if (cmdId === 60000) {
                    socialServer.handleTeamCommand(socket, args, activeSockets);
                    return;
                }
                if (cmdId === 91000) {
                    socialServer.handleGuildCommand(socket, args, activeSockets);
                    return;
                }

                // ==========================================
                // 🏠 SALA PERSONALIZADA (20013)
                // ==========================================
                if (cmdId === 20013) {
                    const roomData = utils.parseArrayList(args);
                    const subCmd = roomData[0];
                    const subArgs = roomData[1] || [];
                    console.log(`[🏠 SALA PERSONALIZADA] SubCmd: ${subCmd} | Dados: ${JSON.stringify(subArgs)}`);

                    // 3301: Criar Sala
                    if (subCmd === 3301) {
                        nextRoomUID++;
                        const roomUID = nextRoomUID;
                        const roomName = subArgs[0] || `Sala ${roomUID}`;
                        const password = subArgs[1] || "";
                        const mapId = subArgs[2] || 5202; // Default to bachang_baxi
                        const mapType = subArgs[3] || 6;
                        const maxPlayers = subArgs[5] || 10;
                        const creatorName = socket.playerName || `Jogador_${socket.playerIndex || 1}`;
                        
                        // Se o socket já estava em alguma sala, sai dela primeiro
                        const oldRoom = getSocketRoom(socket);
                        if (oldRoom) {
                            removePlayerFromRoom(oldRoom, socket);
                        }
                        
                        const newRoom = {
                            roomUID: roomUID,
                            roomName: roomName,
                            password: password,
                            mapId: mapId,
                            mapType: mapType,
                            maxPlayers: maxPlayers,
                            creatorName: creatorName,
                            state: 0, // 0 = Esperando
                            createTime: utils.Fun.GetServerDate(),
                            players: new Array(10).fill(null)
                        };
                        
                        // Adiciona criador na vaga 0
                        newRoom.players[0] = {
                            socket: socket,
                            name: creatorName,
                            force: 1,
                            isReady: 1,
                            isCaptain: 1
                        };
                        
                        socket.currentRoomUID = roomUID;
                        roomContainers.set(roomUID, newRoom);
                        currentRoom = newRoom; // Fallback para compatibilidade
                        
                        socket.targetMapId = mapId;
                        const mapConf = MAP_CONFIG[mapId] || { name: 'bachang_baxi', type: 6 };
                        socket.targetMapName = mapConf.name;
                        
                        // 1. Envia 3310 com o ID da sala para atualizar o m_nRoomUID no cliente
                        socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(3310)},(${utils.encodeInt(roomUID)}))\n`);
                        
                        // 2. Envia 3301 com (1) indicando sucesso
                        socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3301)},(${utils.encodeInt(1)})))\n`);
                        console.log(`[🏠 CONTAINER] Nova Sala ${roomUID} criada por ${creatorName} (${roomName} - Mapa ${mapId})`);
                        return;
                    }

                    // 3307: Obter informações da sala
                    if (subCmd === 3307) {
                        const targetUID = (subArgs && subArgs[0] !== undefined) ? Number(subArgs[0]) : null;
                        const targetRoom = targetUID ? getRoom(targetUID) : getSocketRoom(socket);
                        if (!targetRoom) {
                            socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3307)},(-1,())))\n`);
                            return;
                        }
                        const rData = buildRoomData(targetRoom);
                        socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3307)},(1,${rData})))\n`);
                        return;
                    }

                    // 3306: Lista de salas
                    if (subCmd === 3306) {
                        // Limpa salas vazias que NÃO estejam em tolerância de reconexão
                        for (const [uid, r] of roomContainers.entries()) {
                            if (r.isMatchmaking) continue; // Matchmaking não entra na lista de salas do saguão
                            if (getRoomActivePlayersCount(r) === 0 && !r.autoDestroyTimer && (!r.tdmMatch || r.tdmMatch.state === 'ended')) {
                                roomContainers.delete(uid);
                            }
                        }
                        // Salas criadas no saguão: aguardando (state === 0) OU em andamento (state === 4)
                        const activeRooms = Array.from(roomContainers.values()).filter(r => 
                            !r.isMatchmaking && 
                            (r.state === 0 || r.state === 4) && 
                            (getRoomActivePlayersCount(r) > 0 || r.autoDestroyTimer || (r.tdmMatch && r.tdmMatch.state !== 'ended'))
                        );
                        if (activeRooms.length === 0) {
                            socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3306)},(0,1,())))\n`);
                            return;
                        }
                        const roomInfoList = activeRooms.map(r => {
                            return `(${utils.encodeInt(r.mapId)},'${utils.encodeHex(r.creatorName)}','${utils.encodeHex(r.roomName)}','${utils.encodeHex(r.password)}',${utils.encodeInt(r.mapId)},${utils.encodeInt(r.mapType)},0,${utils.encodeInt(r.maxPlayers)},0,0,0,${utils.encodeInt(getRoomActivePlayersCount(r))},0,${utils.encodeInt(r.state)},0,${utils.encodeInt(7777)},'${r.createTime}',${utils.encodeInt(r.roomUID)},0)`;
                        }).join(',');
                        socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3306)},(1,${utils.encodeInt(activeRooms.length)},(${roomInfoList}))))\n`);
                        return;
                    }

                    // 3303: Entrar na sala
                    if (subCmd === 3303) {
                        const targetUID = (subArgs[1] !== undefined && subArgs[1] !== null && Number(subArgs[1]) > 0) ? Number(subArgs[1]) : null;
                        const targetRoom = targetUID ? getRoom(targetUID) : (roomContainers.values().next().value || null);
                        if (!targetRoom) {
                            socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3303)},(-1,1)))\n`);
                            return;
                        }

                        // Se a sala tem timer de auto-destruição pendente (tolerância de reconexão), cancela pois alguém entrou!
                        if (targetRoom.autoDestroyTimer) {
                            clearTimeout(targetRoom.autoDestroyTimer);
                            targetRoom.autoDestroyTimer = null;
                            console.log(`[⏳ CONTAINER] Timer de auto-destruição da Sala ${targetRoom.roomUID} cancelado por reconexão.`);
                        }

                        // Verificação de senha
                        if (targetRoom.password && targetRoom.password !== '') {
                            const providedPass = subArgs[0] ? utils.decodeHex(subArgs[0]) : '';
                            if (providedPass !== targetRoom.password) {
                                socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3303)},(-2,1)))\n`);
                                return;
                            }
                        }

                        let joinSlot = -1;
                        // 1. Verifica se o jogador já possui um slot na sala (Reconexão!)
                        for (let i = 0; i < 10; i++) {
                            if (targetRoom.players[i] && (targetRoom.players[i].socket === socket || targetRoom.players[i].name === socket.playerName)) {
                                joinSlot = i;
                                break;
                            }
                        }
                        // Preserve explicit pre-match slots; balance late arrivals.
                        if (joinSlot === -1) {
                            joinSlot = targetRoom.state === 4 ? findBalancedSlot(targetRoom) : targetRoom.players.findIndex(p => !p);
                        }
                        if (joinSlot === -1) {
                            socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3303)},(-2,1)))\n`);
                            return;
                        }
                        
                        // Se já estava em outra sala, remove da antiga primeiro
                        const prevRoom = getSocketRoom(socket);
                        if (prevRoom && prevRoom.roomUID !== targetRoom.roomUID) {
                            removePlayerFromRoom(prevRoom, socket);
                        }
                        
                        const assignedForce = [9,12].includes(MAP_CONFIG[targetRoom.finalMapId || targetRoom.mapId]?.type) ? 1 : (joinSlot < 5 ? 1 : 2);
                        const playerIndex = (global.userIndexes && socket.playerName && global.userIndexes[socket.playerName]) ? global.userIndexes[socket.playerName] : (joinSlot + 1);
                        socket.playerIndex = playerIndex;
                        socket.playerId = String(playerIndex);
                        socket.force = assignedForce;
                        socket.inMatch = true;
                        socket.currentRoomUID = targetRoom.roomUID;
                        const effectiveMapId = targetRoom.finalMapId || targetRoom.mapId;
                        socket.targetMapId = effectiveMapId;
                        socket.finalMapId = effectiveMapId;
                        const mapConfJoin = MAP_CONFIG[effectiveMapId] || { name: 'bachang_baxi', type: 7 };
                        socket.targetMapName = mapConfJoin.name;

                        targetRoom.players[joinSlot] = {
                            socket: socket,
                            name: socket.playerName || `Jogador_${playerIndex}`,
                            force: assignedForce,
                            isReady: 1,
                            isCaptain: (joinSlot === 0 && !targetRoom.players.some((p, idx) => idx !== 0 && p && p.isCaptain)) ? 1 : 0
                        };
                        
                        if (socket.playerName && playerSessions[socket.playerName]) {
                            playerSessions[socket.playerName].playerIndex = playerIndex;
                            playerSessions[socket.playerName].playerId = String(playerIndex);
                            playerSessions[socket.playerName].currentRoomUID = targetRoom.roomUID;
                            playerSessions[socket.playerName].targetMapId = effectiveMapId;
                            playerSessions[socket.playerName].finalMapId = effectiveMapId;
                            playerSessions[socket.playerName].targetMapName = mapConfJoin.name;
                            playerSessions[socket.playerName].force = assignedForce;
                            playerSessions[socket.playerName].inMatch = true;
                        }

                        socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(3310)},(${utils.encodeInt(targetRoom.roomUID)}))\n`);
                        
                        const rDataJoin = buildRoomData(targetRoom);
                        const joiningNameEnc = socket.playerNameEnc || utils.encodeHex(socket.playerName || `Jogador_${socket.playerIndex || 1}`);
                        const joinSuccessMsg = `(1,(1,${rDataJoin},'${joiningNameEnc}'))`;

                        // 🚀 SE A PARTIDA JÁ ESTIVER EM COMBATE (state === 4), TRANSIÇÃO DIRETA PARA A CENA (JIP / RECONNECT)!
                        if (targetRoom.state === 4) {
                            console.log(`[🚀 SALA JIP] ${socket.playerName} entrando diretamente na partida em combate da Sala ${targetRoom.roomUID} (${mapConfJoin.name})!`);
                            
                            socket.waitingForHeroLock = true;
                            socket.hasConfirmedHero = false;
                            lifecycle.transition(socket, 'selecting', 'room-admission');

                            // Confirma entrada da sala no Hall para sincronizar dados
                            socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3303)},${joinSuccessMsg}))\n`);

                            const one = utils.encodeInt(1);
                            const zero = utils.encodeInt(0);
                            const dateStr = utils.Fun.GetServerDate();
                            const p80001 = utils.encodeInt(80001);
                            const s90001 = utils.encodeInt(90001);
                            const s90002 = utils.encodeInt(90002);
                            const p1304 = utils.encodeInt(1304);

                            const myHeroId = socket.currentHeroId || 1015;
                            const myHeroIdEnc = utils.encodeInt(myHeroId);
                            const cust = getPlayerCustomizationData(socket.playerName, myHeroId);
                            const profileMe = dbPersistence.loadPlayer(socket.playerName);
                            const myLevel = profileMe && profileMe.level ? profileMe.level : (socket.level || 1);
                            const myUserDataHeroInfo = `(${myHeroIdEnc},${utils.encodeInt(cust.skinId)},${utils.encodeInt(cust.emoteId)},${one},${utils.encodeInt(cust.weaponId)})`;
                            const isCapEnc = zero;
                            const prerogativeLobbyMe = `(${isCapEnc},${isCapEnc},${isCapEnc},${isCapEnc},'${socket.playerNameEnc}',${zero},'',${zero})`;
                            const netIdEnc = utils.encodeInt(socket.playerIndex || 1);
                            socket.userData = `('${socket.playerNameEnc}',${utils.encodeInt(myLevel)},${one},${utils.encodeInt(assignedForce)},${one},${one},${myHeroIdEnc},${one},${one},${zero},${utils.encodeInt(cust.faceId)},'Local',${zero},${myUserDataHeroInfo},${netIdEnc},${zero},${zero},${isCapEnc},${prerogativeLobbyMe},())`;

                            const userList = getFullMatchUserList(socket);

                            const isChangeHeroEnc = utils.encodeInt(1);
                            const matchMsg = `(${s90001},(${userList}),${utils.encodeInt(effectiveMapId)},${utils.encodeInt(mapConfJoin.type)},'${mapConfJoin.name}',${utils.encodeInt(60)},${isChangeHeroEnc},'${dateStr}',${one},${zero})`;

                            const joinIsCurrent = sceneScope.capture(socket);
                            (async () => {
                                if (!joinIsCurrent() || !socket.inMatch) return;
                                await utils.sendPacket(socket, `e1:SendMsgToHallClient,(${p80001},${matchMsg})\n`, 300);
                                if (!joinIsCurrent() || !socket.inMatch || targetRoom.state !== 4) return;
                                await utils.sendPacket(socket, `e1:SendMsgToHallClient,(${p80001},(${s90002},'${socket.playerNameEnc}',${socket.userData}))\n`, 300);
                                setTimeout(() => {
                                    if (joinIsCurrent() && socket.inMatch && targetRoom.state === 4) {
                                        socket.write(`e1:SendMsgToHallClient,(${p1304},(${utils.encodeInt(socket.playerIndex || 1)},'${socket.playerNameEnc}','${SERVER_IP}',${utils.encodeInt(7777)},'SceneGroup',${one}))\n`);
                                    }
                                }, 800);
                            })().catch(err => console.error('[❌ ERRO no JIP da Sala]', err));

                            return;
                        }

                        // Envia 3303 para toda a sala: para quem entrou abre a sala (a == me), para os outros atualiza (a != me)
                        broadcastToRoom(targetRoom, `e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3303)},${joinSuccessMsg}))\n`);
                        broadcastToRoom(targetRoom, `e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3318)},(1,${buildRoomUserList(targetRoom)})))\n`);
                        console.log(`[🏠 CONTAINER] ${socket.playerName} entrou no slot ${joinSlot} da Sala ${targetRoom.roomUID}`);
                        return;
                    }

                    // 3304: Sair da sala
                    if (subCmd === 3304) {
                        const targetRoom = getSocketRoom(socket);
                        if (!targetRoom) return;
                        
                        let pSlot = -1;
                        for (let i = 0; i < 10; i++) {
                            if (targetRoom.players[i] && (targetRoom.players[i].socket === socket || (socket.playerName && targetRoom.players[i].name === socket.playerName))) {
                                pSlot = i;
                                break;
                            }
                        }
                        if (pSlot === -1) return;
                        
                        const p = targetRoom.players[pSlot];
                        const rDataLeave = buildRoomData(targetRoom);
                        
                        socket.targetMapId = 5202;
                        socket.targetMapName = 'bachang_baxi';
                        socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3304)},(1,${utils.encodeInt(targetRoom.roomUID)},${rDataLeave},'${socket.playerNameEnc || utils.encodeHex(p.name)}',0,1)))\n`);
                        
                        removePlayerFromRoom(targetRoom, socket);
                        return;
                    }

                    // 3318: Mudar de Time (Swap Faction)
                    if (subCmd === 3318) {
                        const targetRoom = getSocketRoom(socket);
                        if (!targetRoom) return;
                        let pSlot = -1;
                        for (let i = 0; i < 10; i++) {
                            if (targetRoom.players[i] && (targetRoom.players[i].socket === socket || (socket.playerName && targetRoom.players[i].name === socket.playerName))) {
                                pSlot = i;
                                break;
                            }
                        }
                        if (pSlot === -1) return;
                        
                        const p = targetRoom.players[pSlot];
                        const toTeam = (subArgs[1] !== undefined && subArgs[1] !== null) ? Number(subArgs[1]) : 0;
                        let toIdx = (subArgs[2] !== undefined && subArgs[2] !== null) ? Number(subArgs[2]) : -1;
                        
                        console.log(`[🏠 SALA] Mudar de time solicitado por ${p.name}. De slot ${pSlot} (Time ${p.force}) para time ${toTeam}, slot ${toIdx}`);

                        // Determine target team: Client sends 0=Blue, 1=Red
                        // Server uses: 1 = Blue (slots 0-4), 2 = Red (slots 5-9)
                        let targetTeam;
                        if (toTeam === 0 || toTeam === '0') {
                            targetTeam = 1; // Blue
                        } else if (toTeam === 1 || toTeam === '1') {
                            targetTeam = 2; // Red
                        } else {
                            targetTeam = pSlot < 5 ? 2 : 1;
                        }

                        let absoluteIdx = -1;
                        if (toIdx !== -1) {
                            absoluteIdx = Math.floor(toIdx / 2);
                            if (absoluteIdx < 0 || absoluteIdx >= 10) absoluteIdx = -1;
                        } else {
                            const startSlot = targetTeam === 1 ? 0 : 5;
                            const endSlot = targetTeam === 1 ? 5 : 10;
                            for (let i = startSlot; i < endSlot; i++) {
                                if (!targetRoom.players[i]) {
                                    absoluteIdx = i;
                                    break;
                                }
                            }
                        }
                        
                        if (absoluteIdx !== -1 && absoluteIdx !== pSlot && absoluteIdx < 10 && !targetRoom.players[absoluteIdx]) {
                            targetRoom.players[absoluteIdx] = p;
                            targetRoom.players[pSlot] = null;
                            p.force = absoluteIdx < 5 ? 1 : 2;
                            
                            console.log(`[🏠 SALA] Mudar de time bem-sucedida! ${p.name} agora está no slot ${absoluteIdx} (Time ${p.force})`);
                            broadcastToRoom(targetRoom, `e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3318)},(1,${buildRoomUserList(targetRoom)})))\n`);
                        } else {
                            console.log(`[⚠️ SALA] Mudança de time rejeitada. Slot ${absoluteIdx} ocupado ou inválido.`);
                        }
                        return;
                    }

                    // 3324: Alterar mapa / Configurações da sala
                    if (subCmd === 3324) {
                        const targetRoom = getSocketRoom(socket);
                        if (!targetRoom) return;
                        const changes = subArgs[2] || [];
                        for (let i = 0; i < changes.length; i += 2) {
                            const idx = changes[i];
                            const val = changes[i + 1];
                            if (idx === 4) {
                                targetRoom.mapId = val;
                                const mapConfMod = MAP_CONFIG[val] || { name: 'bachang_baxi', type: 6 };
                                for (let j = 0; j < 10; j++) {
                                    const pl = targetRoom.players[j];
                                    if (pl && pl.socket) {
                                        pl.socket.targetMapId = val;
                                        pl.socket.targetMapName = mapConfMod.name;
                                    }
                                }
                            } else if (idx === 5) {
                                targetRoom.mapType = val;
                            }
                        }
                        const changesStr = changes.map(v => typeof v === 'number' ? utils.encodeInt(v) : v).join(',');
                        broadcastToRoom(targetRoom, `e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3324)},(${changesStr})))\n`);
                        return;
                    }

                    // 3308: Alternar Preparado (Ready) / Confirmar herói
                    if (subCmd === 3308) {
                        const targetRoom = getSocketRoom(socket);
                        if (!targetRoom) return;
                        let pSlot = -1;
                        for (let i = 0; i < 10; i++) {
                            if (targetRoom.players[i] && (targetRoom.players[i].socket === socket || (socket.playerName && targetRoom.players[i].name === socket.playerName))) {
                                pSlot = i;
                                break;
                            }
                        }
                        if (pSlot === -1) return;
                        
                        const p = targetRoom.players[pSlot];
                        p.isReady = p.isReady ? 0 : 1;
                        
                        const rDataReady = buildRoomData(targetRoom);
                        broadcastToRoom(targetRoom, `e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3308)},(1,${rDataReady},'${socket.playerNameEnc || utils.encodeHex(p.name)}')))\n`);
                        return;
                    }

                    // 3313: Expulsar jogador
                    if (subCmd === 3313) {
                        const targetRoom = getSocketRoom(socket);
                        if (!targetRoom) return;
                        let senderSlot = -1;
                        for (let i = 0; i < 10; i++) {
                            if (targetRoom.players[i] && (targetRoom.players[i].socket === socket || (socket.playerName && targetRoom.players[i].name === socket.playerName))) {
                                senderSlot = i;
                                break;
                            }
                        }
                        if (senderSlot === -1 || !targetRoom.players[senderSlot].isCaptain) return;
                        
                        const targetNameDec = utils.decodeHex(subArgs[1]);
                        let targetSlot = -1;
                        for (let i = 0; i < 10; i++) {
                            if (targetRoom.players[i] && targetRoom.players[i].name === targetNameDec) {
                                targetSlot = i;
                                break;
                            }
                        }
                        
                        if (targetSlot !== -1) {
                            const targetEntry = targetRoom.players[targetSlot];
                            const targetSocket = targetEntry.socket;
                            targetRoom.players[targetSlot] = null;
                            
                            if (targetSocket) {
                                targetSocket.currentRoomUID = null;
                                targetSocket.targetMapId = 5202;
                                targetSocket.targetMapName = 'bachang_baxi';
                                const rDataKick = buildRoomData(targetRoom);
                                targetSocket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3304)},(1,${utils.encodeInt(targetRoom.roomUID)},${rDataKick},'${targetSocket.playerNameEnc || utils.encodeHex(targetEntry.name)}',0,1)))\n`);
                            }
                            
                            broadcastToRoom(targetRoom, `e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3318)},(1,${buildRoomUserList(targetRoom)})))\n`);
                        }
                        return;
                    }

                    // 3320: Iniciar Partida da Sala
                    if (subCmd === 3320) {
                        const targetRoom = getSocketRoom(socket);
                        if (!targetRoom) return;
                        if (targetRoom.state === 4) {
                            console.log(`[⚠️ SALA] Partida da Sala ${targetRoom.roomUID} já está em andamento! Ignorando 3320 duplicado.`);
                            return;
                        }

                        const mapId = targetRoom.mapId;
                        const mapConf = MAP_CONFIG[mapId] || { name: 'bachang_baxi', type: 6 };
                        targetRoom.ffaMatch = null; // A new round owns a new individual controller.
                        targetRoom.state = 4; // Jogando
                        targetRoom.finalMapId = mapId;
                        
                        state.setLastMapId(mapId);
                        lastMapName = mapConf.name;
                        
                        // 1. Notifica a sala que o jogo iniciou (3320) e atualiza o estado para 4 (3324)
                        broadcastToRoom(targetRoom, `e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3320)},()))\n`);
                        broadcastToRoom(targetRoom, `e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3324)},(${utils.encodeInt(13)},${utils.encodeInt(4)})))\n`);
                        
                        const roomPlayers = targetRoom.players.filter(p => p !== null && p.socket && !p.socket.destroyed);
                        if (roomPlayers.length === 0) return;
                        
                        const stagingMapId = 5010;
                        const stagingConf = MAP_CONFIG[stagingMapId];
                        
                        roomPlayers.forEach(p => {
                            p.socket.finalMapId = mapId; // MAPA DE COMBATE REAL
                            p.socket.targetMapId = stagingMapId; // STAGING MAP FOR SELECT HERO
                            p.socket.targetMapName = stagingConf.name;
                            p.socket.inMatch = true;
                            p.force = [9,12].includes(MAP_CONFIG[mapId]?.type) ? 1 : p.force;
                            p.socket.force = p.force; // PvE is cooperative
                            lifecycle.transition(p.socket, 'selecting', 'room-start');
                            p.socket.waitingForHeroLock = true; // ESPERA O LOCK DO HERÓI
                            p.socket.hasConfirmedHero = false;
                            
                            // Sincroniza a sessão persistente com os novos mapas de partida
                            if (p.socket.playerName && playerSessions[p.socket.playerName]) {
                                playerSessions[p.socket.playerName].finalMapId = mapId;
                                playerSessions[p.socket.playerName].targetMapId = stagingMapId;
                                playerSessions[p.socket.playerName].targetMapName = stagingConf.name;
                                playerSessions[p.socket.playerName].force = p.force;
                            }
                        });
                        
                        const one = utils.encodeInt(1);
                        const zero = utils.encodeInt(0);
                        const dateStr = utils.Fun.GetServerDate();
                        const ninetyNine = utils.encodeInt(99);
                        
                        roomPlayers.forEach((p, idx) => {
                            const playerIndex = (global.userIndexes && p.socket.playerName && global.userIndexes[p.socket.playerName]) ? global.userIndexes[p.socket.playerName] : (idx + 1);
                            p.socket.playerIndex = playerIndex;
                            p.socket.playerId = String(playerIndex);
                            
                            const heroId = p.socket.currentHeroId || 1015;
                            const heroIdEnc = utils.encodeInt(heroId);
                            const profile = dbPersistence.loadPlayer(p.socket.playerName);
                            const playerEq = state.playerEquipment[p.socket.playerName] || (profile && profile.equipment) || {};
                            const eq = playerEq[heroId] || { skin: 1, weapon: 1, emote: 1, pose: 0 };
                            const heroInfo = generateFullHeroData(heroId, eq.skin, eq.weapon, eq.emote, eq.pose, profile).split(',').slice(1).join(',');
                            
                            const cust = getPlayerCustomizationData(p.socket.playerName, heroId);
                            const userDataHeroInfo = `(${heroIdEnc},${utils.encodeInt(cust.skinId)},${utils.encodeInt(cust.emoteId)},${one},${utils.encodeInt(cust.weaponId)})`;
                            const isCaptain = p.isCaptain ? 1 : 0;
                            const isCaptainEnc = utils.encodeInt(isCaptain);
                            const prerogativeLobby = `(${isCaptainEnc},${isCaptainEnc},${isCaptainEnc},${isCaptainEnc},'${p.socket.playerNameEnc || utils.encodeHex(p.name)}',${zero},'',${zero})`;
                            p.socket.hasConfirmedHero = false;
                            const isChsOkEnc = zero;
                            const netIdEnc = utils.encodeInt(p.socket.playerIndex || 1);
                            p.socket.userData = `('${p.socket.playerNameEnc || utils.encodeHex(p.name)}',${ninetyNine},${one},${utils.encodeInt(p.force)},${one},${one},${heroIdEnc},${one},${one},${isChsOkEnc},${utils.encodeInt(cust.faceId)},'Local',${zero},${userDataHeroInfo},${netIdEnc},${zero},${zero},${isCaptainEnc},${prerogativeLobby},())`;
                        });
                        
                        let userListElements = [];
                        roomPlayers.forEach(p => {
                            userListElements.push(`'${p.socket.playerNameEnc || utils.encodeHex(p.name)}',${p.socket.userData}`);
                        });
                        global.currentMatchBots = [];
                        const userList = userListElements.join(',');
                        
                        const p80001 = utils.encodeInt(80001);
                        const s90001 = utils.encodeInt(90001);
                        const s90002 = utils.encodeInt(90002);
                        const p1304 = utils.encodeInt(1304);
                        
                        const isChangeHeroEnc = utils.encodeInt(1);
                        const matchMsg = `(${s90001},(${userList}),${utils.encodeInt(stagingMapId)},${utils.encodeInt(stagingConf.type)},'${stagingConf.name}',${utils.encodeInt(60)},${isChangeHeroEnc},'${dateStr}',${utils.encodeInt(1)},${zero})`;
                        
                        roomPlayers.forEach(p => {
                            (async () => {
                                await utils.sendPacket(p.socket, `e1:SendMsgToHallClient,(${p80001},${matchMsg})\n`, 400);
                                await utils.sendPacket(p.socket, `e1:SendMsgToHallClient,(${p80001},(${s90002},'${p.socket.playerNameEnc || utils.encodeHex(p.name)}',${p.socket.userData}))\n`, 400);
                                
                                setTimeout(() => {
                                    if (!p.socket.destroyed) {
                                        p.socket.write(`e1:SendMsgToHallClient,(${p1304},(${utils.encodeInt(p.socket.playerIndex || 1)},'${p.socket.playerNameEnc || utils.encodeHex(p.name)}','${SERVER_IP}',${utils.encodeInt(7777)},'SceneGroup',${one}))\n`);
                                    }
                                }, 1500);
                            })().catch(err => console.error('[❌ ERRO no envio do Room Match]', err));
                        });
                        
                        return;
                    }
                }

                // Ignora logs de erro e Heartbeat (Ping) para não sujar o terminal
                if (cmdId !== 2110 && cmdId !== 100001) {
                    let logName = "AÇÃO";
                    if (cmdId === 1307) logName = "ANTI-VÍCIO (FCM)";
                    if (cmdId === 5030) logName = "INICIAR PARTIDA";
                    if (cmdId === 5032) logName = "CONFIRMAÇÃO DE MAPA";
                    if (cmdId === 70000) logName = "LOJA (SHOP)";

                    console.log(`[🕹️ JOGO PEDIU ${logName}] ID: ${cmdId} | DADOS: ${args}`);
                }

                // ==========================================
                // 💬 CHAT E COMANDOS DE ADMIN (ID 1006)
                // ==========================================
                if (cmdId === 1006) {
                    const chatData = utils.parseArrayList(args);
                    const channel = chatData[0];
                    const msgHex = chatData[1]; // A mensagem vem em Hex
                    let msg = utils.decodeHex(msgHex); // Traduz para texto normal

                    console.log(`[💬 CHAT] Canal: ${channel} | Mensagem: ${msg}`);

                    // 1. Processar Comandos de Admin
                    if (msg.startsWith('/')) {
                        const parts = msg.split(' ');
                        const command = parts[0].toLowerCase();

                        if (command === '/start') {
                            socket.targetMapId = 5202;
                            socket.targetMapName = 'bachang_baxi';
                            gameLogic.startTdmMatch(socket);
                            return;
                        }

                        if (command === '/start') {
                            gameLogic.startTdmMatch(socket);
                            return;
                        }

                        const val = parts[1] ? parseInt(parts[1]) : 999999;
                        const valEnc = utils.encodeInt(val);
                        const moneyList = `(${valEnc},${valEnc},${valEnc},0,${utils.encodeInt(10)},0,${valEnc},${valEnc},${valEnc},0,${valEnc},0,0,0)`;

                        if (command === '/gold' || command === '/gem' || command === '/money') {
                            socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(10025)},(0,${moneyList}))\n`);
                            console.log(`[💰 MOEDA] Riqueza injetada: ${val}`);

                            const sysMsg = utils.encodingUTF8(`Comando aplicado! Riqueza de ${val} injetada.`);
                            socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(1006)},(8,'system','${sysMsg}','','',${utils.encodeInt(0)},${utils.encodeInt(0)},${utils.encodeInt(0)},()))\n`);
                        }

                        if (command === '/unlockall' || command === '/cheat') {
                            const porta1306 = utils.encodeInt(1306);
                            const respostaHerois = utils.encodeInt(12002);
                            const heroIds = getHeroIdsList();
                            const profile = dbPersistence.loadPlayer(socket.playerName);

                            const heroDataArray = heroIds.map(id => {

                                const playerEq = state.playerEquipment[socket.playerName] || (profile && profile.equipment) || {};

                                const eq = playerEq[id] || { skin: 1, weapon: 1, emote: 1, pose: 0 };

                                return generateFullHeroData(id, eq.skin, eq.weapon, eq.emote, eq.pose, profile);

                            }).join(',');
                            socket.write(`e1:SendMsgToHallClient,(${porta1306},(${respostaHerois},(${heroDataArray})))\n`);

                            const sysMsg = utils.encodingUTF8(`TUDO LIBERADO! Reinicie a tela de heróis.`);
                            socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(1006)},(8,'system','${sysMsg}','','',${utils.encodeInt(0)},${utils.encodeInt(0)},${utils.encodeInt(0)},()))\n`);
                        }

                        if (command === '/map') {
                            const mapId = parts[1] ? parseInt(parts[1]) : 5205;
                            let mapName = 'learnfb';
                            if (mapId === 1) mapName = 'fb001';

                            state.setLastMapId(mapId);
                            lastMapName = mapName;

                            console.log(`[🚀 MAPA] Forçando carregamento: ${mapName} (${mapId})`);

                            const p80001 = utils.encodeInt(80001);
                            const s90001 = utils.encodeInt(90001);
                            const userData = `('Admin',${utils.encodeInt(1)},1,1,1,1,${utils.encodeInt(1003)},1,1,1,${utils.encodeInt(2009)},'Local',0,())`;
                            const userList = `('Admin',${userData})`;

                            const matchMsg = `(${s90001},${userList},${mapId},1,'${mapName}',30,1,'2024-01-01 12:00:00',0,0)`;
                            socket.write(`e1:SendMsgToHallClient,(${p80001},${matchMsg})\n`);

                            const p1304 = utils.encodeInt(1304);
                            const one = utils.encodeInt(1);
                            socket.write(`e1:SendMsgToHallClient,(${p1304},(${one},'${PLAYER_NAME_ENC}','${SERVER_IP}',${utils.encodeInt(7777)},'SceneGroup',${one}))\n`);

                            const sysMsg = utils.encodingUTF8(`Teleportando para ${mapName}...`);
                            socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(1006)},(8,'system','${sysMsg}','','',0,0,0,()))\n`);
                        }

                        if (command === '/item') {
                            const itemId = parts[1] ? parseInt(parts[1]) : 1001;
                            const count = parts[2] ? parseInt(parts[2]) : 1;
                            const p70000 = utils.encodeInt(70000);
                            socket.write(`e1:SendMsgToHallClient,(${p70000},(${utils.encodeInt(70018)},(${itemId},${count},0)))\n`);
                            socket.write(`e1:SendMsgToHallClient,(${p70000},(${utils.encodeInt(70012)},(1,${itemId},${count},'Comando Admin',(),'Admin')))\n`);
                            const sysMsg = utils.encodingUTF8('O Item apareceu na sua mochila!');
                            socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(1006)},(8,'system','${sysMsg}','','',0,0,0,()))\n`);
                        }

                        if (command === '/team' || command === '/time' || command === '/lado') {
                            if (!currentRoom) {
                                const sysMsg = utils.encodingUTF8('Você precisa estar em uma sala para usar este comando!');
                                socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(1006)},(8,'system','${sysMsg}','','',0,0,0,()))\n`);
                            } else {
                                let pSlot = -1;
                                for (let i = 0; i < 10; i++) {
                                    if (currentRoom.players[i] && currentRoom.players[i].socket === socket) {
                                        pSlot = i;
                                        break;
                                    }
                                }
                                if (pSlot === -1) {
                                    const sysMsg = utils.encodingUTF8('Erro: Você não foi encontrado na sala!');
                                    socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(1006)},(8,'system','${sysMsg}','','',0,0,0,()))\n`);
                                } else {
                                    const p = currentRoom.players[pSlot];
                                    const targetIsRed = pSlot < 5;
                                    const startSlot = targetIsRed ? 5 : 0;
                                    const endSlot = targetIsRed ? 10 : 5;
                                    let absoluteIdx = -1;
                                    for (let i = startSlot; i < endSlot; i++) {
                                        if (!currentRoom.players[i]) {
                                            absoluteIdx = i;
                                            break;
                                        }
                                    }
                                    
                                    if (absoluteIdx !== -1) {
                                        currentRoom.players[absoluteIdx] = p;
                                        currentRoom.players[pSlot] = null;
                                        p.force = absoluteIdx < 5 ? 1 : 2;
                                        
                                        console.log(`[💬 COMANDO] Troca de time via chat bem-sucedida para ${p.name}! Novo slot: ${absoluteIdx}`);
                                        
                                        const rDataChange = buildRoomData(currentRoom);
                                        broadcastToRoom(currentRoom, `e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3318)},(1,${rDataChange})))\n`);
                                        
                                        const teamName = targetIsRed ? 'Vermelho' : 'Azul';
                                        const sysMsg = utils.encodingUTF8(`Você foi movido para o Time ${teamName} (Vaga ${absoluteIdx})!`);
                                        socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(1006)},(8,'system','${sysMsg}','','',0,0,0,()))\n`);
                                    } else {
                                        const teamName = targetIsRed ? 'Vermelho' : 'Azul';
                                        const sysMsg = utils.encodingUTF8(`O Time ${teamName} está cheio!`);
                                        socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(1006)},(8,'system','${sysMsg}','','',0,0,0,()))\n`);
                                    }
                                }
                            }
                        }
                    } else {
                        // Eco de Chat Normal (Faz a mensagem aparecer pra você)
                        const p1006 = utils.encodeInt(1006);
                        socket.write(`e1:SendMsgToHallClient,(${p1006},(${channel},'[mi]${PLAYER_NAME_ENC}[/mi]','[mi]${msgHex}[/mi]','','',0,0,0,()))\n`);
                    }
                }

                // ==========================================
                // 🛒 LOJA (ID 70000)
                // ==========================================
                if (cmdId === 70000) {
                    const shopData = utils.parseArrayList(args);
                    const subId = shopData[0];
                    const subArgs = shopData[1];
                    console.log(`[🛒 LOJA] SubCmd: ${subId} | Dados brutos: ${JSON.stringify(subArgs)}`);

                    // 70001: Comprar Item Normal | 70013: Comprar Presente (H5l)
                    if (subId === 70001 || subId === 70013) {
                        const shopId = subArgs[0];
                        const count = subArgs[1];
                        const payType = subArgs[2] || 0; // 0 = Diamante, 1 = Ouro (varia de acordo com a loja, verifique o padrão do seu cliente)

                        console.log(`[🛒 LOJA] Tentativa de compra: ShopID ${shopId} x${count}`);

                        // 1. Buscar informações do produto no DB Falso
                        const shopEntry = db.GAME_DB.shop[shopId];

                        // Fallback: se o item não estiver no nosso Mock DB, inventamos um para o jogo não quebrar
                        const itemId = shopEntry ? shopEntry.itemId : (1000 + shopId);
                        const itemEntry = db.GAME_DB.items[itemId] || { name: `Item Desconhecido ${itemId}`, goldPrice: 100, diamondPrice: 10, type: 2 };

                        // 2. Deduzir o valor da conta (Supondo que payType 0 seja diamante e 1 seja ouro)
                        const inv = state.playerInventory[socket.playerName];
                        const totalCost = (payType === 1 ? itemEntry.goldPrice : itemEntry.diamondPrice) * count;
                        if (payType === 1) {
                            inv.gold -= totalCost;
                        } else {
                            inv.diamonds -= totalCost;
                        }

                        // Atualizar inventário na memória do servidor
                        if (!inv.items[itemId]) inv.items[itemId] = 0;
                        inv.items[itemId] += count;

                        // Persistir no banco de dados do jogador
                        const profile = dbPersistence.loadPlayer(socket.playerName);
                        if (profile) {
                            profile.gold = inv.gold;
                            profile.diamonds = inv.diamonds;
                            profile.items = inv.items;
                            
                            // Se o item for um desbloqueador de herói ou cosmético, desbloqueá-lo no perfil do jogador
                            if (itemEntry.type === 3 && itemEntry.heroid) {
                                if (!profile.unlockedHeroes) profile.unlockedHeroes = [1015];
                                if (!profile.unlockedHeroes.includes(itemEntry.heroid)) {
                                    profile.unlockedHeroes.push(itemEntry.heroid);
                                }
                            } else if (itemEntry.type === 4 && itemEntry.heroid && itemEntry.typeid) {
                                if (!profile.unlockedCosmetics) profile.unlockedCosmetics = {};
                                if (!profile.unlockedCosmetics[itemEntry.heroid]) {
                                    profile.unlockedCosmetics[itemEntry.heroid] = { skins: [1], weapons: [1], emotes: [1], poses: [0] };
                                }
                                if (!profile.unlockedCosmetics[itemEntry.heroid].skins.includes(itemEntry.typeid)) {
                                    profile.unlockedCosmetics[itemEntry.heroid].skins.push(itemEntry.typeid);
                                }
                            } else if (itemEntry.type === 5 && itemEntry.heroid && itemEntry.typeid) {
                                if (!profile.unlockedCosmetics) profile.unlockedCosmetics = {};
                                if (!profile.unlockedCosmetics[itemEntry.heroid]) {
                                    profile.unlockedCosmetics[itemEntry.heroid] = { skins: [1], weapons: [1], emotes: [1], poses: [0] };
                                }
                                if (!profile.unlockedCosmetics[itemEntry.heroid].emotes) {
                                    profile.unlockedCosmetics[itemEntry.heroid].emotes = [1];
                                }
                                if (!profile.unlockedCosmetics[itemEntry.heroid].emotes.includes(itemEntry.typeid)) {
                                    profile.unlockedCosmetics[itemEntry.heroid].emotes.push(itemEntry.typeid);
                                }
                            } else if (itemEntry.type === 6 && itemEntry.heroid && itemEntry.typeid) {
                                if (!profile.unlockedCosmetics) profile.unlockedCosmetics = {};
                                if (!profile.unlockedCosmetics[itemEntry.heroid]) {
                                    profile.unlockedCosmetics[itemEntry.heroid] = { skins: [1], weapons: [1], emotes: [1], poses: [0] };
                                }
                                if (!profile.unlockedCosmetics[itemEntry.heroid].weapons.includes(itemEntry.typeid)) {
                                    profile.unlockedCosmetics[itemEntry.heroid].weapons.push(itemEntry.typeid);
                                }
                            } else if (itemEntry.type === 7 && itemEntry.heroid && itemEntry.typeid) {
                                if (!profile.unlockedCosmetics) profile.unlockedCosmetics = {};
                                if (!profile.unlockedCosmetics[itemEntry.heroid]) {
                                    profile.unlockedCosmetics[itemEntry.heroid] = { skins: [1], weapons: [1], emotes: [1], poses: [0] };
                                }
                                if (!profile.unlockedCosmetics[itemEntry.heroid].poses) {
                                    profile.unlockedCosmetics[itemEntry.heroid].poses = [0];
                                }
                                if (!profile.unlockedCosmetics[itemEntry.heroid].poses.includes(itemEntry.typeid)) {
                                    profile.unlockedCosmetics[itemEntry.heroid].poses.push(itemEntry.typeid);
                                }
                            } else if (itemEntry.type === 8 && itemEntry.typeid) {
                                if (!profile.unlockedSprays) profile.unlockedSprays = [1019];
                                if (!profile.unlockedSprays.includes(itemEntry.typeid)) {
                                    profile.unlockedSprays.push(itemEntry.typeid);
                                }
                            } else if (itemEntry.type === 15 && itemEntry.typeid) {
                                if (!profile.unlockedFaces) profile.unlockedFaces = [2009];
                                if (!profile.unlockedFaces.includes(itemEntry.typeid)) {
                                    profile.unlockedFaces.push(itemEntry.typeid);
                                }
                            }

                            dbPersistence.savePlayer(socket.playerName, profile);

                            // Sincronizar atualizações de liberação se for herói, cosmético, spray ou avatar
                            const porta1306 = utils.encodeInt(1306);
                            if (itemEntry.heroid) {
                                const heroId = itemEntry.heroid;
                                const eq = (state.playerEquipment[socket.playerName] && state.playerEquipment[socket.playerName][heroId]) || { skin: 1, weapon: 1, emote: 1, pose: 0 };
                                const fullHeroData = generateFullHeroData(heroId, eq.skin, eq.weapon, eq.emote, eq.pose, profile);
                                
                                // Envia 12006 para o herói afetado (ou comprado)
                                socket.write(`e1:SendMsgToHallClient,(${porta1306},(${utils.encodeInt(12006)},(${fullHeroData})))\n`);
                                
                                if (itemEntry.type === 3) {
                                    // Se for o herói em si, envia também a lista completa 12002
                                    const heroIds = getHeroIdsList();
                                    const heroDataArray = heroIds.map(id => {
                                        if (!state.playerEquipment[socket.playerName]) {
                                            state.playerEquipment[socket.playerName] = {};
                                        }
                                        if (!state.playerEquipment[socket.playerName][id]) {
                                            state.playerEquipment[socket.playerName][id] = (profile && profile.equipment && profile.equipment[id]) || { skin: 1, weapon: 1, emote: 1, pose: 0 };
                                        }
                                        const eqVal = state.playerEquipment[socket.playerName][id];
                                        return db.generateFullHeroData(id, eqVal.skin, eqVal.weapon, eqVal.emote, eqVal.pose, profile);
                                    }).join(',');
                                    socket.write(`e1:SendMsgToHallClient,(${porta1306},(${utils.encodeInt(12002)},(${heroDataArray})))\n`);
                                }
                            }
                            if (itemEntry.type === 8 && itemEntry.typeid) {
                                // Spray
                                const paintId = itemEntry.typeid;
                                const paintList = db.allPaintIds.map(pId => {
                                    const lockStateVal = isPaintUnlocked(pId, profile) ? 2 : 0;
                                    return `${utils.encodeInt(pId)},(${utils.encodeInt(pId)},${utils.encodeInt(lockStateVal)},0,0,'2099/01/01 12:00:00',0)`;
                                }).join(',');
                                // Envia confirmação de compra 11009 no formato correto esperado pelo C# (OpenPlayerPaint)
                                const sprayData = `(${utils.encodeInt(paintId)},${utils.encodeInt(2)},0,0,'2099/01/01 12:00:00',0)`;
                                socket.write(`e1:SendMsgToHallClient,(${porta1306},(${utils.encodeInt(11009)},(${sprayData},${utils.encodeInt(paintId)})))\n`);
                                socket.write(`e1:SendMsgToHallClient,(${porta1306},(${utils.encodeInt(11008)},((${paintList}),${utils.encodeInt(paintId)})))\n`);
                            }
                            if (itemEntry.type === 15 && itemEntry.typeid) {
                                // Avatar
                                const faceId = itemEntry.typeid;
                                const faceList = db.allFaceIds.map(fId => {
                                    const lockStateVal = isFaceUnlocked(fId, profile) ? 2 : 0;
                                    return `${utils.encodeInt(fId)},(${utils.encodeInt(lockStateVal)},0,0,0)`;
                                }).join(',');
                                // Envia confirmação de compra 11002 no formato correto esperado pelo C# (InitPlayerFace da subLista)
                                socket.write(`e1:SendMsgToHallClient,(${porta1306},(${utils.encodeInt(11002)},((${faceList}))))\n`);
                                socket.write(`e1:SendMsgToHallClient,(${porta1306},(${utils.encodeInt(11001)},(${faceList})))\n`);
                            }
                        }

                        console.log(`[🛒 LOJA] Vendido! Item: ${itemEntry.name} (ID: ${itemId}) por ${totalCost}. Saldo: G:${inv.gold} D:${inv.diamonds}`);

                        // 3. Enviar pacote OnBuyItemOk (70009) para fechar a telinha de "Comprando..."
                        // A estrutura esperada no C# é: (shopId, count, desconto/bônus, erro)
                        const p70000 = utils.encodeInt(70000);
                        const p70009 = utils.encodeInt(70009);
                        socket.write(`e1:SendMsgToHallClient,(${p70000},(${p70009},(${shopId},${count},0,0)))\n`);

                        // 4. Atualizar o dinheiro na HUD do jogador (10025)
                        const goldEnc = utils.encodeInt(inv.gold);
                        const diaEnc = utils.encodeInt(inv.diamonds);
                        // A lista gigantesca de moedas do jogo: Gold, GoldBind, Gems, GemsBind...
                        const moneyList = `(${goldEnc},${goldEnc},${diaEnc},0,${utils.encodeInt(10)},0,${goldEnc},${goldEnc},${goldEnc},0,${goldEnc},0,0,0)`;
                        socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(10025)},(0,${moneyList}))\n`);

                        // 5. ENTREGAR O ITEM (Verificando se é Buff ou Item Físico)
                        if (itemEntry.bufftype > 0) {
                            // É um BUFF (XP/Moeda). O cliente espera o pacote 70007.
                            // Estrutura: (70007, ((TipoDoBuff, (Multiplicador, 'DataFinal', ItemId))), FlagAtualizarUI)
                            const p70007 = utils.encodeInt(70007);
                            const buffData = `(${itemEntry.bufftype},(${itemEntry.buffbeishu},'2026/12/31 12:00:00',${itemId}))`;
                            socket.write(`e1:SendMsgToHallClient,(${p70000},(${p70007},((${buffData}),1)))\n`);
                            console.log(`[✨ BUFF] Ativado: ${itemEntry.name}`);
                        } else {
                            // Item normal (Peça de arma, Material). Usa o pacote 70018.
                            const p70018 = utils.encodeInt(70018);
                            const newTotal = inv.items[itemId];
                            socket.write(`e1:SendMsgToHallClient,(${p70000},(${p70018},(${itemId},${newTotal},1)))\n`);
                            console.log(`[🎒 ITEM] Entregue: ${itemEntry.name}`);
                        }

                        // Mensagem opcional no chat
                        const sysMsg = utils.encodingUTF8(`Você obteve ${count}x ${itemEntry.name}!`);
                        socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(1006)},(8,'system','${sysMsg}','','',${utils.encodeInt(0)},${utils.encodeInt(0)},${utils.encodeInt(0)},()))\n`);
                    }
                }
                // ==========================================
                // 🎒 SISTEMA DE INVENTÁRIO E HERÓIS (ID 1306)
                // ==========================================
                if (cmdId === 1306) {
                    const heroData = utils.parseArrayList(args);
                    const subCmdId = heroData[0];
                    const subArgs = heroData[1] || [];
                    const porta1306 = utils.encodeInt(1306);

                    if (subCmdId === 11002) {
                        const faceId = subArgs[0];
                        const payType = subArgs[1] || 5; // 5 = gold, 2 = diamond
                        console.log(`[🎭 AVATAR COMPRA] Desbloqueando avatar/face: ${faceId} via payType: ${payType}`);
                        const profile = dbPersistence.loadPlayer(socket.playerName);
                        if (profile) {
                            if (!profile.unlockedFaces) profile.unlockedFaces = [2009];
                            if (!profile.unlockedFaces.includes(faceId)) {
                                const price = db.faceUnlockPrices[faceId] || { gold: 30, diamond: 15 };
                                let paid = false;
                                if (payType === 5) {
                                    if (profile.gold >= (price.gold || 30)) {
                                        profile.gold -= (price.gold || 30);
                                        paid = true;
                                    }
                                } else {
                                    if (profile.diamonds >= (price.diamond || 15)) {
                                        profile.diamonds -= (price.diamond || 15);
                                        paid = true;
                                    }
                                }
                                // Fallback em caso de falta de moedas:
                                if (!paid) {
                                    if (profile.gold >= (price.gold || 30) && (price.gold || 30) > 0) {
                                        profile.gold -= (price.gold || 30);
                                        paid = true;
                                    } else if (profile.diamonds >= (price.diamond || 15) && (price.diamond || 15) > 0) {
                                        profile.diamonds -= (price.diamond || 15);
                                        paid = true;
                                    } else if (price.gold === 0 && price.diamond === 0) {
                                        paid = true;
                                    }
                                }
                                
                                if (paid) {
                                    profile.unlockedFaces.push(faceId);
                                    
                                    const inv = state.playerInventory[socket.playerName];
                                    if (inv) {
                                        inv.gold = profile.gold;
                                        inv.diamonds = profile.diamonds;
                                    }
                                    
                                    const goldEnc = utils.encodeInt(profile.gold);
                                    const diaEnc = utils.encodeInt(profile.diamonds);
                                    const moneyList = `(${goldEnc},${goldEnc},${diaEnc},0,${utils.encodeInt(10)},0,${goldEnc},${goldEnc},${goldEnc},0,${goldEnc},0,0,0)`;
                                    socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(10025)},(0,${moneyList}))\n`);
                                    console.log(`[🎭 AVATAR COMPRA] Avatar ${faceId} comprado com sucesso.`);
                                }
                            }
                            dbPersistence.savePlayer(socket.playerName, profile);
                        }
                        
                        const faceList = db.allFaceIds.map(fId => {
                            const lockStateVal = isFaceUnlocked(fId, profile) ? 2 : 0;
                            return `${utils.encodeInt(fId)},(${utils.encodeInt(lockStateVal)},0,0,0)`;
                        }).join(',');
                        
                        // Envia confirmação de compra 11002 no formato correto esperado pelo C# (InitPlayerFace da subLista)
                        socket.write(`e1:SendMsgToHallClient,(${porta1306},(${utils.encodeInt(11002)},((${faceList}))))\n`);
                        socket.write(`e1:SendMsgToHallClient,(${porta1306},(${utils.encodeInt(11001)},(${faceList})))\n`);
                    }
                    else if (subCmdId === 11003) {
                        const faceId = subArgs[0];
                        console.log(`[🎭 AVATAR EQUIPE] Definindo avatar/face: ${faceId}`);
                        const profile = dbPersistence.loadPlayer(socket.playerName);
                        if (profile) {
                            if (!profile.unlockedFaces) profile.unlockedFaces = [2009];
                            
                            // Se não estiver liberado, desbloquear (fallback)
                            if (!profile.unlockedFaces.includes(faceId)) {
                                profile.unlockedFaces.push(faceId);
                            }
                            
                            profile.face = faceId;
                            dbPersistence.savePlayer(socket.playerName, profile);
                        }
                        
                        const faceList = db.allFaceIds.map(fId => {
                            const lockStateVal = isFaceUnlocked(fId, profile) ? 2 : 0;
                            return `${utils.encodeInt(fId)},(${utils.encodeInt(lockStateVal)},0,0,0)`;
                        }).join(',');
                        
                        // Envia confirmação de equipar 11003 no formato correto esperado pelo C# (SetPlayerFace)
                        socket.write(`e1:SendMsgToHallClient,(${porta1306},(${utils.encodeInt(11003)},(${utils.encodeInt(faceId)},(${faceList}))))\n`);
                        socket.write(`e1:SendMsgToHallClient,(${porta1306},(${utils.encodeInt(11001)},(${faceList})))\n`);
                    }
                    else if (subCmdId === 11009) {
                        const paintId = subArgs[0];
                        const payType = subArgs[1] || 5; // 5 = gold, 2 = diamond
                        console.log(`[🎨 SPRAY COMPRA] Definindo/Comprando spray/paint: ${paintId} via payType: ${payType}`);
                        const profile = dbPersistence.loadPlayer(socket.playerName);
                        if (profile) {
                            if (!profile.unlockedSprays) profile.unlockedSprays = [1019];
                            
                            // Se não estiver liberado, comprar/desbloquear
                            if (!profile.unlockedSprays.includes(paintId)) {
                                const price = db.paintUnlockPrices[paintId] || { gold: 100, diamond: 50 };
                                let paid = false;
                                if (payType === 5) {
                                    if (profile.gold >= (price.gold || 100)) {
                                        profile.gold -= (price.gold || 100);
                                        paid = true;
                                    }
                                } else {
                                    if (profile.diamonds >= (price.diamond || 50)) {
                                        profile.diamonds -= (price.diamond || 50);
                                        paid = true;
                                    }
                                }
                                // Fallback em caso de falta de moedas:
                                if (!paid) {
                                    if (profile.gold >= (price.gold || 100) && (price.gold || 100) > 0) {
                                        profile.gold -= (price.gold || 100);
                                        paid = true;
                                    } else if (profile.diamonds >= (price.diamond || 50) && (price.diamond || 50) > 0) {
                                        profile.diamonds -= (price.diamond || 50);
                                        paid = true;
                                    } else if (price.gold === 0 && price.diamond === 0) {
                                        paid = true;
                                    }
                                }
                                
                                if (paid) {
                                    profile.unlockedSprays.push(paintId);
                                    
                                    const inv = state.playerInventory[socket.playerName];
                                    if (inv) {
                                        inv.gold = profile.gold;
                                        inv.diamonds = profile.diamonds;
                                    }
                                    
                                    const goldEnc = utils.encodeInt(profile.gold);
                                    const diaEnc = utils.encodeInt(profile.diamonds);
                                    const moneyList = `(${goldEnc},${goldEnc},${diaEnc},0,${utils.encodeInt(10)},0,${goldEnc},${goldEnc},${goldEnc},0,${goldEnc},0,0,0)`;
                                    socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(10025)},(0,${moneyList}))\n`);
                                    console.log(`[🎨 SPRAY COMPRA] Spray ${paintId} comprado com sucesso.`);
                                }
                            }
                            
                            profile.paint = paintId;
                            dbPersistence.savePlayer(socket.playerName, profile);
                        }
                        
                        const paintList = db.allPaintIds.map(pId => {
                            const lockStateVal = isPaintUnlocked(pId, profile) ? 2 : 0;
                            return `${utils.encodeInt(pId)},(${utils.encodeInt(pId)},${utils.encodeInt(lockStateVal)},0,0,'2099/01/01 12:00:00',0)`;
                        }).join(',');
                        
                        // Envia confirmação de compra 11009 no formato correto esperado pelo C# (OpenPlayerPaint)
                        const sprayData = `(${utils.encodeInt(paintId)},${utils.encodeInt(2)},0,0,'2099/01/01 12:00:00',0)`;
                        socket.write(`e1:SendMsgToHallClient,(${porta1306},(${utils.encodeInt(11009)},(${sprayData},${utils.encodeInt(paintId)})))\n`);
                        socket.write(`e1:SendMsgToHallClient,(${porta1306},(${utils.encodeInt(11008)},((${paintList}),${utils.encodeInt(paintId)})))\n`);
                    }
                    else if (subCmdId === 11010) {
                        const paintId = subArgs[0];
                        console.log(`[🎨 SPRAY EQUIPE] Definindo/Equipando spray: ${paintId}`);
                        const profile = dbPersistence.loadPlayer(socket.playerName);
                        if (profile) {
                            if (!profile.unlockedSprays) profile.unlockedSprays = [1019];
                            
                            // Se não estiver liberado, desbloquear (fallback)
                            if (!profile.unlockedSprays.includes(paintId)) {
                                profile.unlockedSprays.push(paintId);
                            }
                            
                            profile.paint = paintId;
                            dbPersistence.savePlayer(socket.playerName, profile);
                        }
                        
                        const paintList = db.allPaintIds.map(pId => {
                            const lockStateVal = isPaintUnlocked(pId, profile) ? 2 : 0;
                            return `${utils.encodeInt(pId)},(${utils.encodeInt(pId)},${utils.encodeInt(lockStateVal)},0,0,'2099/01/01 12:00:00',0)`;
                        }).join(',');
                        
                        socket.write(`e1:SendMsgToHallClient,(${porta1306},(${utils.encodeInt(11010)},(${utils.encodeInt(paintId)},(${paintList}))))\n`);
                        socket.write(`e1:SendMsgToHallClient,(${porta1306},(${utils.encodeInt(11008)},((${paintList}),${utils.encodeInt(paintId)})))\n`);
                    }
                    else if (subCmdId === 12022) {
                        const heroId = subArgs[0];
                        const weaponId = subArgs[1];
                        const slotId = subArgs[2];
                        const partId = subArgs[3];
                        console.log(`[⚙️ EQUIP PART] Hero: ${heroId} | Weapon: ${weaponId} | Slot: ${slotId} | Part: ${partId}`);
                        const profile = dbPersistence.loadPlayer(socket.playerName);
                        if (profile) {
                            if (!profile.weaponParts) profile.weaponParts = {};
                            if (!profile.weaponParts[heroId]) profile.weaponParts[heroId] = {};
                            if (!profile.weaponParts[heroId][weaponId]) profile.weaponParts[heroId][weaponId] = {};
                            profile.weaponParts[heroId][weaponId][slotId] = partId;
                            dbPersistence.savePlayer(socket.playerName, profile);
                        }
                        const eq = (state.playerEquipment[socket.playerName] && state.playerEquipment[socket.playerName][heroId]) || { skin: 1, weapon: 1, emote: 1, pose: 0 };
                        const fullHeroData = generateFullHeroData(heroId, eq.skin, eq.weapon, eq.emote, eq.pose, profile);
                        socket.write(`e1:SendMsgToHallClient,(${porta1306},(${utils.encodeInt(12006)},(${fullHeroData})))\n`);
                    }
                    else if (subCmdId === 12023) {
                        const heroId = subArgs[0];
                        const weaponId = subArgs[1];
                        const slotId = subArgs[2];
                        const partId = subArgs[3];
                        console.log(`[⚙️ REMOVE PART] Hero: ${heroId} | Weapon: ${weaponId} | Slot: ${slotId}`);
                        const profile = dbPersistence.loadPlayer(socket.playerName);
                        if (profile) {
                            if (profile.weaponParts && profile.weaponParts[heroId] && profile.weaponParts[heroId][weaponId]) {
                                delete profile.weaponParts[heroId][weaponId][slotId];
                                dbPersistence.savePlayer(socket.playerName, profile);
                            }
                        }
                        const eq = (state.playerEquipment[socket.playerName] && state.playerEquipment[socket.playerName][heroId]) || { skin: 1, weapon: 1, emote: 1, pose: 0 };
                        const fullHeroData = generateFullHeroData(heroId, eq.skin, eq.weapon, eq.emote, eq.pose, profile);
                        socket.write(`e1:SendMsgToHallClient,(${porta1306},(${utils.encodeInt(12006)},(${fullHeroData})))\n`);
                        socket.write(`e1:SendMsgToHallClient,(${porta1306},(12023,(0)))\n`);
                    }
                    else if (subCmdId === 12021) {
                        const partId = subArgs[0];
                        const count = subArgs[1] || 1;
                        console.log(`[⚙️ UPGRADE PART] Part: ${partId} | Count: ${count}`);
                        const profile = dbPersistence.loadPlayer(socket.playerName);
                        let lvl = 1;
                        let exp = 0;
                        let leveledUp = 0;
                        let addedExp = count * 10;
                        if (profile) {
                            if (!profile.parts) profile.parts = {};
                            if (!profile.parts[partId]) profile.parts[partId] = { lvl: 1, exp: 0 };
                            let part = profile.parts[partId];
                            part.exp += addedExp;
                            
                            const maxLvl = 30;
                            while (part.lvl < maxLvl) {
                                const nextLvlExp = part.lvl * 100;
                                if (part.exp >= nextLvlExp) {
                                    part.exp -= nextLvlExp;
                                    part.lvl++;
                                    leveledUp = 1;
                                } else {
                                    break;
                                }
                            }
                            lvl = part.lvl;
                            exp = part.exp;
                            dbPersistence.savePlayer(socket.playerName, profile);
                        }
                        socket.write(`e1:SendMsgToHallClient,(${porta1306},(12021,(${partId},(${utils.encodeInt(lvl)},${utils.encodeInt(exp)}),${utils.encodeInt(addedExp)},${utils.encodeInt(leveledUp)})))\n`);
                    }
                    else if (subCmdId === 12007) {
                        const heroId = subArgs[0];
                        const type = subArgs[1]; // 1 = skin, 2 = weapon, 3 = emote, 4 = pose
                        const itemId = subArgs[2]; // cosmetic index
                        console.log(`[👕 COMPRA/EQUIPE] Herói: ${heroId} | Tipo: ${type} | ItemIndex: ${itemId}`);
                        
                        const profile = dbPersistence.loadPlayer(socket.playerName);
                        if (profile) {
                            if (!profile.unlockedCosmetics) profile.unlockedCosmetics = {};
                            if (!profile.unlockedCosmetics[heroId]) {
                                profile.unlockedCosmetics[heroId] = { skins: [1], weapons: [1], emotes: [1], poses: [0] };
                            }
                            const uc = profile.unlockedCosmetics[heroId];
                            
                            // Verificar se o item já está desbloqueado
                            let isAlreadyUnlocked = false;
                            if (type === 1) isAlreadyUnlocked = (itemId === 1 || uc.skins.includes(itemId));
                            else if (type === 2) isAlreadyUnlocked = (itemId === 1 || uc.weapons.includes(itemId));
                            else if (type === 3) isAlreadyUnlocked = (itemId === 1 || (uc.emotes && uc.emotes.includes(itemId)));
                            else if (type === 4) isAlreadyUnlocked = (itemId === 0 || (uc.poses && uc.poses.includes(itemId)));
                            
                            if (!isAlreadyUnlocked) {
                                // Buscar preços
                                let goldPrice = 0, diamondPrice = 0;
                                const xmlPriceKey = `${heroId}_${type}_${itemId}`;
                                const xmlPrice = db.cosmeticUnlockPrices[xmlPriceKey];
                                if (xmlPrice) {
                                    goldPrice = xmlPrice.gold || 0;
                                    diamondPrice = xmlPrice.diamond || 0;
                                    if (xmlPrice.arenaPoints > 0 && goldPrice === 0 && diamondPrice === 0) {
                                        // Golden weapons buyable with arena points -> use gold fallback
                                        goldPrice = xmlPrice.arenaPoints * 0.5;
                                    }
                                } else {
                                    // Try iteminfo.xml matching
                                    let itemType = 0;
                                    if (type === 1) itemType = 4;
                                    else if (type === 2) itemType = 6;
                                    else if (type === 3) itemType = 5;
                                    else if (type === 4) itemType = 7;
                                    
                                    let matchedItem = null;
                                    for (const itId in db.GAME_DB.items) {
                                        const it = db.GAME_DB.items[itId];
                                        if (it.heroid === heroId && it.typeid === itemId && it.type === itemType) {
                                            matchedItem = it;
                                            break;
                                        }
                                    }
                                    if (matchedItem) {
                                        goldPrice = matchedItem.goldPrice || 0;
                                        diamondPrice = matchedItem.diamondPrice || 0;
                                    } else {
                                        // Fallbacks
                                        if (type === 1) { goldPrice = 998; diamondPrice = 498; }
                                        else if (type === 2) { goldPrice = 1000; diamondPrice = 100; }
                                        else { goldPrice = 178; diamondPrice = 88; }
                                    }
                                }
                                
                                // Deduzir dinheiro
                                let paid = false;
                                if (profile.gold >= goldPrice && goldPrice > 0) {
                                    profile.gold -= goldPrice;
                                    paid = true;
                                    console.log(`[👕 COMPRA] Debitado ${goldPrice} Gold.`);
                                } else if (profile.diamonds >= diamondPrice && diamondPrice > 0) {
                                    profile.diamonds -= diamondPrice;
                                    paid = true;
                                    console.log(`[👕 COMPRA] Debitado ${diamondPrice} Diamonds.`);
                                } else if (goldPrice === 0 && diamondPrice === 0) {
                                    paid = true;
                                }
                                
                                if (paid) {
                                    // Adicionar aos desbloqueados
                                    if (type === 1) uc.skins.push(itemId);
                                    else if (type === 2) uc.weapons.push(itemId);
                                    else if (type === 3) {
                                        if (!uc.emotes) uc.emotes = [1];
                                        uc.emotes.push(itemId);
                                    } else if (type === 4) {
                                        if (!uc.poses) uc.poses = [0];
                                        uc.poses.push(itemId);
                                    }
                                    
                                    const inv = state.playerInventory[socket.playerName];
                                    if (inv) {
                                        inv.gold = profile.gold;
                                        inv.diamonds = profile.diamonds;
                                    }
                                    
                                    const goldEnc = utils.encodeInt(profile.gold);
                                    const diaEnc = utils.encodeInt(profile.diamonds);
                                    const moneyList = `(${goldEnc},${goldEnc},${diaEnc},0,${utils.encodeInt(10)},0,${goldEnc},${goldEnc},${goldEnc},0,${goldEnc},0,0,0)`;
                                    socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(10025)},(0,${moneyList}))\n`);
                                }
                            }
                            
                            // Equipar
                            if (!profile.equipment) profile.equipment = {};
                            if (!profile.equipment[heroId]) profile.equipment[heroId] = { skin: 1, weapon: 1, emote: 1, pose: 0 };
                            
                            if (type === 1) profile.equipment[heroId].skin = itemId;
                            else if (type === 2) profile.equipment[heroId].weapon = itemId;
                            else if (type === 3) profile.equipment[heroId].emote = itemId;
                            else if (type === 4) profile.equipment[heroId].pose = itemId;
                            
                            dbPersistence.savePlayer(socket.playerName, profile);
                            state.playerEquipment[socket.playerName] = profile.equipment;
                        }
                        
                        const eq = (state.playerEquipment[socket.playerName] && state.playerEquipment[socket.playerName][heroId]) || { skin: 1, weapon: 1, emote: 1, pose: 0 };
                        const fullHeroData = generateFullHeroData(heroId, eq.skin, eq.weapon, eq.emote, eq.pose, profile);
                        socket.write(`e1:SendMsgToHallClient,(${porta1306},(${utils.encodeInt(12006)},(${fullHeroData})))\n`);
                        
                        // Envia confirmações de sucesso para o subCmd 12007 (compra de cosmético) em múltiplos formatos
                        socket.write(`e1:SendMsgToHallClient,(${porta1306},(12007,(${utils.encodeInt(heroId)},${utils.encodeInt(type)},${utils.encodeInt(itemId)},1)))\n`);
                        socket.write(`e1:SendMsgToHallClient,(${porta1306},(12007,(${utils.encodeInt(heroId)},1)))\n`);
                        socket.write(`e1:SendMsgToHallClient,(${porta1306},(12007,(${utils.encodeInt(itemId)},1)))\n`);
                        
                        // Envia OnChangeUseOk (12009) para forçar o cliente a equipar a skin/arma imediatamente na UI!
                        const encHeroId = utils.encodeInt(heroId);
                        const p12009 = utils.encodeInt(12009);
                        socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(1306)},(${p12009},(1,${encHeroId},${utils.encodeInt(type)},${utils.encodeInt(itemId)})))\n`);
                    }
                    else if (subCmdId === 12001) {
                        const heroId = subArgs[0];
                        const payTypeNum = subArgs[1] || 5; // 5 = gold, 2 = diamond
                        console.log(`[👤 UNLOCK HERO] Hero: ${heroId} via payTypeNum: ${payTypeNum}`);
                        
                        const price = db.heroUnlockPrices[heroId] || { gold: 1298, diamond: 688 };
                        let payType = 'gold';
                        let profile = dbPersistence.loadPlayer(socket.playerName);
                        
                        if (profile) {
                            let paid = false;
                            if (payTypeNum === 5) {
                                if (profile.gold >= (price.gold || 1298)) {
                                    profile.gold -= (price.gold || 1298);
                                    payType = 'gold';
                                    paid = true;
                                }
                            } else {
                                if (profile.diamonds >= (price.diamond || 688)) {
                                    profile.diamonds -= (price.diamond || 688);
                                    payType = 'diamond';
                                    paid = true;
                                }
                            }
                            
                            // Fallback em caso de falha de moedas:
                            if (!paid) {
                                if (profile.gold >= (price.gold || 1298)) {
                                    profile.gold -= (price.gold || 1298);
                                    payType = 'gold';
                                    paid = true;
                                } else if (profile.diamonds >= (price.diamond || 688)) {
                                    profile.diamonds -= (price.diamond || 688);
                                    payType = 'diamond';
                                    paid = true;
                                }
                            }
                            
                            if (paid) {
                                if (!profile.unlockedHeroes) profile.unlockedHeroes = [1015];
                                if (!profile.unlockedHeroes.includes(heroId)) {
                                    profile.unlockedHeroes.push(heroId);
                                }
                                
                                if (!profile.unlockedCosmetics) profile.unlockedCosmetics = {};
                                if (!profile.unlockedCosmetics[heroId]) {
                                    profile.unlockedCosmetics[heroId] = { skins: [1], weapons: [1], emotes: [1], poses: [0] };
                                }
                                
                                dbPersistence.savePlayer(socket.playerName, profile);
                                
                                const inv = state.playerInventory[socket.playerName];
                                if (inv) {
                                    inv.gold = profile.gold;
                                    inv.diamonds = profile.diamonds;
                                }
                                
                                const goldEnc = utils.encodeInt(profile.gold);
                                const diaEnc = utils.encodeInt(profile.diamonds);
                                const moneyList = `(${goldEnc},${goldEnc},${diaEnc},0,${utils.encodeInt(10)},0,${goldEnc},${goldEnc},${goldEnc},0,${goldEnc},0,0,0)`;
                                socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(10025)},(0,${moneyList}))\n`);
                            }
                        }
                        
                        const eq = (state.playerEquipment[socket.playerName] && state.playerEquipment[socket.playerName][heroId]) || { skin: 1, weapon: 1, emote: 1, pose: 0 };
                        const fullHeroData = generateFullHeroData(heroId, eq.skin, eq.weapon, eq.emote, eq.pose, profile);
                        
                        // 1. Envia a resposta de sucesso 12001
                        socket.write(`e1:SendMsgToHallClient,(${porta1306},(12001,(${fullHeroData},'${payType}')))\n`);
                        
                        // 2. Envia a atualização do herói 12006
                        socket.write(`e1:SendMsgToHallClient,(${porta1306},(${utils.encodeInt(12006)},(${fullHeroData})))\n`);
                        
                        // 3. Envia a lista colossal atualizada de heróis 12002
                        const heroIds = getHeroIdsList();
                        const heroDataArray = heroIds.map(id => {
                            if (!state.playerEquipment[socket.playerName]) {
                                state.playerEquipment[socket.playerName] = {};
                            }
                            if (!state.playerEquipment[socket.playerName][id]) {
                                state.playerEquipment[socket.playerName][id] = (profile && profile.equipment && profile.equipment[id]) || { skin: 1, weapon: 1, emote: 1, pose: 0 };
                            }
                            const eqVal = state.playerEquipment[socket.playerName][id];
                            return db.generateFullHeroData(id, eqVal.skin, eqVal.weapon, eqVal.emote, eqVal.pose, profile);
                        }).join(',');
                        socket.write(`e1:SendMsgToHallClient,(${porta1306},(${utils.encodeInt(12002)},(${heroDataArray})))\n`);
                    }
                    else if (subCmdId === 12013 || subCmdId === 12014 || subCmdId === 12015) {
                        socket.write(`e1:SendMsgToHallClient,(${porta1306},(${utils.encodeInt(subCmdId)},(${subArgs[0]},${utils.encodeInt(1)})))\n`);
                    }
                    // 12009: Mudar Equipamento (Skin, Arma, Emote)
                    else if (subCmdId === 12009) {
                        const params = heroData[1];
                        const heroId = params[0]; // Isso é 1021 (Decodificado)
                        const type = params[1];
                        const itemId = params[2];
                        console.log(`[👕 EQUIPE] Herói ${heroId} | Tipo ${type} | ID ${itemId}`);

                        // 1. Atualiza a memória de equipamentos por jogador
                        if (!state.playerEquipment[socket.playerName]) {
                            state.playerEquipment[socket.playerName] = {};
                        }
                        if (!state.playerEquipment[socket.playerName][heroId]) {
                            state.playerEquipment[socket.playerName][heroId] = { skin: 1, weapon: 1, emote: 1, pose: 0 };
                        }

                        if (type === 1) state.playerEquipment[socket.playerName][heroId].skin = itemId;
                        else if (type === 2) state.playerEquipment[socket.playerName][heroId].weapon = itemId;
                        else if (type === 3) state.playerEquipment[socket.playerName][heroId].emote = itemId;
                        else if (type === 4) state.playerEquipment[socket.playerName][heroId].pose = itemId;

                        // Persiste as alterações no banco de dados do jogador
                        const profile = dbPersistence.loadPlayer(socket.playerName);
                        if (profile) {
                            profile.equipment = state.playerEquipment[socket.playerName];
                            dbPersistence.savePlayer(socket.playerName, profile);
                        }

                        // 3. Monta a nova ficha do herói com as roupas novas usando generateFullHeroData
                        const eq = state.playerEquipment[socket.playerName][heroId];
                        const fullHeroData = db.generateFullHeroData(heroId, eq.skin, eq.weapon, eq.emote, eq.pose, profile);

                        // 4. O SEGREDO: Re-codificar o HeroID (1021 -> Fx) antes de devolver!
                        const encHeroId = utils.encodeInt(heroId);

                        // 5. Envia 12006 (UpdateOneHero) -> Atualiza a memória 3D do Unity
                        const p12006 = utils.encodeInt(12006);
                        socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(1306)},(${p12006},(${fullHeroData})))\n`);

                        // 6. Envia 12009 (OnChangeUseOk) -> Toca o som de sucesso na tela
                        const p12009 = utils.encodeInt(12009);
                        socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(1306)},(${p12009},(1,${encHeroId},${utils.encodeInt(type)},${utils.encodeInt(itemId)})))\n`);
                    }
                }

                // ==========================================
                // ==========================================
                // 🏰 MODO AVENTURA / PVE / DUNGEONS (ID 19500)
                // ==========================================
                if (cmdId === 19500) {
                    const parsedArgs = utils.parseArrayList(textoRecebido.substring(textoRecebido.indexOf('(')));
                    const subCmdId = parsedArgs[0];
                    console.log(`[🏰 PVE TELEMETRIA] Jogador: ${socket.playerName} | SubCmd: ${subCmdId} | Raw: ${textoRecebido.trim().slice(0, 150)}`);

                    const profile = dbPersistence.loadPlayer(socket.playerName);
                    const p19500 = utils.encodeInt(19500);

                    // 19517: Upgrade de Runa / Gene
                    if (subCmdId === 19517) {
                        console.log(`[🧬 RUNE UPGRADE] Jogador: ${socket.playerName} solicitou upgrade de runa`);
                        if (profile) {
                            if (!profile.runes) profile.runes = { step: 1, star: 1 };
                            let runes = profile.runes;
                            
                            let nowStep = runes.step || 1;
                            let nowStar = runes.star || 1;
                            
                            let nextStep = nowStep;
                            let nextStar = nowStar + 1;
                            
                            if (nextStar > 10) {
                                nextStep = nowStep + 1;
                                nextStar = 1;
                            }
                            
                            if (nextStep > 49) {
                                nextStep = 49;
                                nextStar = 10;
                            }
                            
                            runes.step = nextStep;
                            runes.star = nextStar;
                            
                            dbPersistence.savePlayer(socket.playerName, profile);
                            console.log(`[🧬 RUNE UPGRADE] Runa atualizada para: Step ${runes.step} | Star ${runes.star}`);
                            
                            const p19517 = utils.encodeInt(19517);
                            const retOk = utils.encodeInt(1);
                            const stepEnc = utils.encodeInt(runes.step);
                            const starEnc = utils.encodeInt(runes.star);
                            const costEnc = utils.encodeInt(0);
                            
                            socket.write(`e1:SendMsgToHallClient,(${p19500},(${p19517},(${retOk},${stepEnc},${starEnc},${costEnc})))\n`);
                        }
                        return;
                    }

                    // 19503: Iniciar Partida / Fase PVE (King Trial / Dark Land / Adventure)
                    if (subCmdId === 19503) {
                        console.log(`[🏰 PVE INICIAR FASE] Jogador: ${socket.playerName} iniciando partida PVE`);
                        const p19503 = utils.encodeInt(19503);
                        const retOk = utils.encodeInt(1); // 1 = Sucesso
                        socket.write(`e1:SendMsgToHallClient,(${p19500},(${p19503},(${retOk})))\n`);
                        return;
                    }

                    // 19514: Seleção / Varredura de Estágio PVE
                    if (subCmdId === 19514) {
                        console.log(`[🏰 PVE STAGE SELECT] Jogador: ${socket.playerName} selecionou estágio PVE`);
                        const p19514 = utils.encodeInt(19514);
                        const retOk = utils.encodeInt(1);
                        socket.write(`e1:SendMsgToHallClient,(${p19500},(${p19514},(${retOk},())))\n`);
                        return;
                    }

                    // 19516: Obter Dados de Masmorra Específica (Prova do Rei / Terra Sombria)
                    if (subCmdId === 19516) {
                        const subArgs = parsedArgs[1] || [];
                        const reqFbType = subArgs[0] !== undefined ? subArgs[0] : 9;
                        console.log(`[🏰 PVE MAP DATA] Jogador: ${socket.playerName} solicitou dados da masmorra tipo: ${reqFbType}`);
                        const p19516 = utils.encodeInt(19516);
                        const pveData = gameLogic.buildPveData(reqFbType, profile);
                        const fbTypeEnc = utils.encodeInt(reqFbType);
                        socket.write(`e1:SendMsgToHallClient,(${p19500},(${p19516},(${pveData},${fbTypeEnc})))\n`);
                        return;
                    }

                    // 19518: Dados Iniciais de Genes e PVE Global (Tipo 9)
                    if (subCmdId === 19518) {
                        console.log(`[🏰 PVE GENE INIT] Jogador: ${socket.playerName} carregando dados de PVE e Genes`);
                        const p19518 = utils.encodeInt(19518);
                        const geneData = gameLogic.buildGeneData(profile);
                        const pveData9 = gameLogic.buildPveData(9, profile);
                        const pveData12 = gameLogic.buildPveData(12, profile);
                        
                        socket.write(`e1:SendMsgToHallClient,(${p19500},(${p19518},(${geneData},${pveData9})))\n`);
                        
                        const p19516 = utils.encodeInt(19516);
                        socket.write(`e1:SendMsgToHallClient,(${p19500},(${p19516},(${pveData9},${utils.encodeInt(9)})))\n`);
                        socket.write(`e1:SendMsgToHallClient,(${p19500},(${p19516},(${pveData12},${utils.encodeInt(12)})))\n`);
                        return;
                    }

                    // 19511: Sincronização de Vigor / Energia
                    if (subCmdId === 19511) {
                        console.log(`[⚡ PVE STAMINA] Jogador: ${socket.playerName} solicitou sincronização de vigor`);
                        const p19511 = utils.encodeInt(19511);
                        const energyEnc = utils.encodeInt(100);
                        socket.write(`e1:SendMsgToHallClient,(${p19500},(${p19511},(${energyEnc})))\n`);
                        return;
                    }

                    // 19520: Recompensas da Primeira Conclusão PVE
                    if (subCmdId === 19520) {
                        console.log(`[🏰 PVE REWARDS] Jogador: ${socket.playerName} solicitou recompensas PVE`);
                        const p19520 = utils.encodeInt(19520);
                        socket.write(`e1:SendMsgToHallClient,(${p19500},(${p19520},()))\n`);
                        return;
                    }
                }

                // ==========================================
                // 🏰 PVE MATCH LOAD / MATCHMAKING (ID 19000)
                // ==========================================
                if (cmdId === 19000) {
                    const parsedArgs = utils.parseArrayList(textoRecebido.substring(textoRecebido.indexOf('(')));
                    const subCmdId = parsedArgs[0];
                    console.log(`[🏰 PVE SCENE LOAD] Jogador: ${socket.playerName} | SubCmd: ${subCmdId} | Raw: ${textoRecebido.trim().slice(0, 150)}`);

                    if (subCmdId === 19010) {
                        const pveMapId = socket.targetMapId || 5105;
                        const mapConf = MAP_CONFIG[pveMapId] || { name: 'xinshouyindao_3', type: 8 };
                        console.log(`[🚀 PVE SCENE] Carregando cena PVE: ${mapConf.name} (Mapa ID: ${pveMapId})`);
                        socket.write(`e:CLoadScene,((0,'${mapConf.name}',0,${utils.encodeInt(gameLogic.getSafeMapId(pveMapId))}))\n`);
                        return;
                    }
                }

                // ==========================================
                // 📜 CARREIRA E ESTATÍSTICAS DO JOGADOR (ID 100001 - CAREER_GETDATA)
                // ==========================================
                if (cmdId === 100001) {
                    const playerName = socket.playerName || (global.lastLobbySocket && global.lastLobbySocket.playerName) || "Gustavo";
                    const careerPacket = dbPersistence.buildCareerDataPacket(playerName);
                    socket.write(careerPacket);
                    console.log(`[📜 CAREER DATA SYNC] Enviados dados de carreira e estatísticas de heróis para o jogador ${playerName}`);
                    return;
                }

                // ==========================================
                // ⚙️ SALVAR CONFIGURAÇÕES (ID 3010000)
                // ==========================================
                if (cmdId === 3010000) {
                    try {
                        const parsedArgs = utils.parseArrayList(textoRecebido.substring(textoRecebido.indexOf('(')));
                        // Estrutura: [ 3010013, [arg0, arg1, arg2, arg3, arg4] ]
                        if (parsedArgs.length >= 2 && parsedArgs[0] === 3010013) {
                            const settingsArray = parsedArgs[1];
                            if (Array.isArray(settingsArray) && settingsArray.length >= 5) {
                                // Salva no banco de dados do jogador
                                const profile = dbPersistence.loadPlayer(socket.playerName);
                                if (profile) {
                                    profile.settings = settingsArray.map(x => String(x));
                                    dbPersistence.savePlayer(socket.playerName, profile);
                                    console.log(`[⚙️ SETTINGS] Configurações salvas para o jogador: ${socket.playerName}`);
                                    
                                    // Retorna o pacote de confirmação 3010014
                                    const porta3010014 = utils.encodeInt(3010014);
                                    socket.write(`e1:SendMsgToHallClient,(${porta3010014},())\n`);
                                }
                            }
                        }
                    } catch (e) {
                        console.error("[⚠️ ERRO ao processar 3010000 Settings Save]:", e);
                    }
                    return;
                }

                // ==========================================
                // 🎭 SELEÇÃO DE HERÓI (ID 20014 -> 90002)
                // ==========================================
                if (textoRecebido.includes("LyL") || textoRecebido.includes("90002")) {
                    console.log("[🎭] Jogador selecionou um herói! Detectando ID...");
                    
                    // Tenta extrair o ID do herói do pacote. 
                    // Formato típico: (20014, (90002, heroIdEnc, isOk))
                    let selectedHeroId = 1015; // Fallback
                    try {
                        const match = textoRecebido.match(/LyL,\(?([^,\)]+)/);
                        if (match && match[1]) {
                            selectedHeroId = utils.decodeInt(match[1]);
                            socket.currentHeroId = selectedHeroId; // Salva o herói escolhido na sessão
                            console.log(`[🎭] Herói escolhido: ${selectedHeroId}`);
                        }
                    } catch(e) {
                        console.log("[⚠️] Erro ao decodificar HeroID, usando fallback 1015.");
                    }

                    // 1. Atualiza dados de HP/Escudo e coordenadas para o spawn na base
                    const maxHp = getHeroMaxHp(selectedHeroId);
                    socket.maxHp = maxHp;
                    socket.hp = maxHp;
                    socket.shield = 0;

                    const defaultCoords = gameLogic.getSpawnCoordinates(socket.targetMapId || state.getLastMapId() || 5202, socket.force);
                    socket.lastX = defaultCoords.sx;
                    socket.lastY = defaultCoords.sy;
                    socket.lastZ = defaultCoords.sz;

                    // 2. Envia pacote 12002 para liberar herói/cosméticos na memória do cliente
                    const p1306 = utils.encodeInt(1306);
                    const p12002 = utils.encodeInt(12002);
                    const heroIdEnc = utils.encodeInt(selectedHeroId);
                    const pName = socket.playerName || (socket.remoteAddress && ipNames[socket.remoteAddress]);
                    const loadedProfile = dbPersistence.loadPlayer(pName);
                    const eqSkin = (state.playerEquipment[pName] && state.playerEquipment[pName][selectedHeroId]) ? state.playerEquipment[pName][selectedHeroId].skin : 1;
                    const eqWeapon = (state.playerEquipment[pName] && state.playerEquipment[pName][selectedHeroId]) ? state.playerEquipment[pName][selectedHeroId].weapon : 1;
                    socket.write(`e1:SendMsgToClient,(${p1306},(${p12002},(${heroIdEnc},${generateFullHeroData(selectedHeroId, eqSkin, eqWeapon, 1, 0, loadedProfile).split(',').slice(1).join(',')})))\n`);

                    // 3. Envia o spawn (10001) para si mesmo
                    socket.bypassInterceptor = true;
                    socket.write(gameLogic.getPlayerSpawnPacket(socket, socket));
                    socket.bypassInterceptor = false;

                    // 4. Broadcast do novo spawn para todos os outros jogadores da cena
                    activeSockets.forEach(s => {
                        if (sceneScope.canReceive(socket, s) && s !== socket && !s.destroyed) {
                            s.bypassInterceptor = true;
                            s.write(gameLogic.getPlayerSpawnPacket(socket, s));
                            s.bypassInterceptor = false;
                        }
                    });

                    // 5. Fecha a tela de seleção de heróis no cliente (90008)
                    const p20014 = utils.encodeInt(20014);
                    const s90008 = utils.encodeInt(90008);
                    const one = utils.encodeInt(1);
                    socket.write(`e${utils.encodeInt(socket.playerIndex || 1)}:SendMsgToClient,(${p20014},(${s90008},(),${one}))\n`);
                    return;
                }

                // ==========================================
                // 🏃 MOVIMENTO (ID 20001 - SP/SN)
                // ==========================================
                if (textoRecebido.includes("SP") || textoRecebido.includes("SN")) {
                    socket.write(`e${utils.encodeInt(socket.playerIndex || 1)}:SendMsgToClient,(${utils.encodeInt(20058)},('${utils.Fun.GetServerDate()}',${utils.encodeInt(0)}))\n`);
                    return;
                }

                // ==========================================
                // 🎓 ENTRAR NO TREINAMENTO (ID 5030) ou TUTORIAL (ID 5032)
                // ==========================================
                if (cmdId === 5030 || cmdId === 5032) {
                    if (socket.inMatch) return;
                    socket.inMatch = true;
                    
                    const mapId = cmdId === 5032 ? 5205 : 5202;
                    const mapIdEnc = utils.encodeInt(mapId);
                    const mapConf = MAP_CONFIG[mapId] || { name: 'xinshouyindao_3', type: 8 };
                    state.setLastMapId(mapId);
                    lastMapName = mapConf.name;
                    socket.targetMapId = mapId;
                    socket.targetMapName = mapConf.name;
                    
                    const heroId = 1015;
                    const heroIdEnc = utils.encodeInt(heroId);
                    const one = utils.encodeInt(1);
                    const zero = utils.encodeInt(0);
                    const dateStr = utils.Fun.GetServerDate();
                    const ninetyNine = utils.encodeInt(99);

                    // Unlocked items inner list structure (SkinUnlock.SetData):
                    // 0:?, 1:nLockState, 2:nPieceNum, 3:?, 4:overTime, 5:nNewState
                    const unlockedSkin = `(${one},(${one},${one},${zero},${zero},'${dateStr}',${zero}))`;
                    // WeaponUnlock.SetData: index 6 is szPartsGrid (ArrayList)
                    const unlockedWeapon = `(${one},(${one},${one},${zero},${zero},'${dateStr}',${zero},()))`;

                    // OpenHeroData structure:
                    // 0:Lvl, 1:Exp, 2:OpenState, 3:overTime, 4:ChipNum, 5:szAward, 6:szSkin, 7:szWeapon, 8:szEmotion, 9:szPose, 10:Zero, 11:UseSkin, 12:UseWeapon, 13:UseEmotion, 14:UsePose
                    const heroInfo = `(${one},${zero},${utils.encodeInt(2)},'${dateStr}',${zero},(),${unlockedSkin},${unlockedWeapon},(),(),${zero},${one},${one},${zero},${zero},${zero})`;

                    const cust = getPlayerCustomizationData(socket.playerName, heroId);
                    const userDataHeroInfo = `(${heroIdEnc},${utils.encodeInt(cust.skinId)},${utils.encodeInt(cust.emoteId)},${one},${utils.encodeInt(cust.weaponId)})`;
                    const isCapEnc = utils.encodeInt(1); // O próprio jogador de lobby é o capitão local da sua sessão até entrar em salas
                    const prerogativeLobby = `(${isCapEnc},${isCapEnc},${isCapEnc},${isCapEnc},'${socket.playerNameEnc}',${zero},'',${zero})`;

                    const userData = `('${socket.playerNameEnc}',${ninetyNine},${one},${one},${one},${one},${heroIdEnc},${one},${one},${one},${utils.encodeInt(cust.faceId)},'Local',${zero},${userDataHeroInfo},${one},${zero},${zero},${isCapEnc},${prerogativeLobby},())`;
                    const userList = `('${socket.playerNameEnc}',${userData})`;

                    const mapTypeEnc = utils.encodeInt(mapConf.type);
                    const startTime = utils.Fun.GetServerDate();

                    const isChangeHeroEnc = mapConf.type === 7 ? utils.encodeInt(1) : zero;
                    const matchMsg = `(${s90001},${userList},${mapIdEnc},${mapTypeEnc},'${mapConf.name}',${utils.encodeInt(60)},${isChangeHeroEnc},'${startTime}',${utils.encodeInt(1)},${zero})`;

                    state.setLastLobbySocket(socket);
                    (async () => {
                        await utils.sendPacket(socket, `e1:SendMsgToHallClient,(${p80001},${matchMsg})\n`, 400);
                        await utils.sendPacket(socket, `e1:SendMsgToHallClient,(${p80001},(${s90002},'${socket.playerNameEnc}',${userData}))\n`, 400);
                        await utils.sendPacket(socket, `e1:SendMsgToHallClient,(${p80001},(${s90004}))\n`, 400);

                        setTimeout(() => {
                            socket.write(`e1:SendMsgToHallClient,(${p1304},(${one},'${socket.playerNameEnc}','${SERVER_IP}',${utils.encodeInt(7777)},'SceneGroup',${one}))\n`);
                        }, 1500);
                    })();
                }
            }
        }

        // =====================================================
        // 🚀 PONTE TRANSPARENTE DE CENA: C# (.NET 8.0) ENGINE
        // =====================================================
        if (textoRecebido.includes("NGOL")) {
            socket.isEncrypted = true;
        }

        const USE_CSHARP_FOR_MATCHES = false;
        if (USE_CSHARP_FOR_MATCHES && socketType === "SCENE") {
            if (!socket.csharpBridge) {
                console.log(`[🚀 C# BRIDGE] Redirecionando conexão de cena para o Servidor C# (.NET 8) na porta 8001...`);
                try {
                    const bridge = net.connect(8001, '127.0.0.1', () => {
                        console.log(`[✅ C# BRIDGE CONECTADO] Jogador ${socket.playerName} conectado ao servidor C#!`);
                        const pSession = (socket.playerName && playerSessions[socket.playerName]) ? playerSessions[socket.playerName] : {};
                        const targetMap = socket.finalMapId || pSession.finalMapId || socket.targetMapId || state.getLastMapId() || 5202;
                        const mapConf = MAP_CONFIG[targetMap] || { name: 'bachang_baxi' };
                        const targetName = mapConf.name;
                        const force = socket.force || pSession.force || 1;
                        const heroId = socket.currentHeroId || pSession.currentHeroId || 1015;
                        let botHeroId = 1016;
                        if (state.activeBots) {
                            const bKeys = Object.keys(state.activeBots);
                            if (bKeys.length > 0 && state.activeBots[bKeys[0]].currentHeroId) {
                                botHeroId = state.activeBots[bKeys[0]].currentHeroId;
                            }
                        }
                        bridge.write(`INIT_SESSION:${socket.playerIndex || 1}:${targetMap}:${targetName}:${force}:${socket.playerName || 'Player'}:${heroId}:${botHeroId}\n`);
                        bridge.write(textoRecebido + '\n');
                    });

                    bridge.on('data', (d) => {
                        socket.write(d);
                    });

                    bridge.on('error', (err) => {
                        console.log(`[⚠️ C# BRIDGE ERRO] Falha ao conectar no servidor C# (${err.message}). Usando fallback Node.js.`);
                        socket.csharpBridge = null;
                    });

                    bridge.on('close', () => {
                        socket.csharpBridge = null;
                    });

                    socket.csharpBridge = bridge;
                    return;
                } catch (e) {
                    console.log(`[⚠️ C# BRIDGE EXCEPTION]`, e);
                }
            } else {
                socket.csharpBridge.write(textoRecebido + '\n');
                return;
            }
        }

        // --- CENA 4: SERVIDOR DE CENA (PlayerUser) ---
        else if (socketType === "SCENE" && textoRecebido.includes("NGOL")) {
            console.log(`[🌐 SCENE] Jogador conectando ao socket de cena...`);
            socket.isEncrypted = true;
            const stagingCheckId = socket.targetMapId || state.getLastMapId() || 5202;
            socket.waitingForHeroLock = socket.waitingForHeroLock === true && !socket.hasConfirmedHero;
            lifecycle.transition(socket, socket.waitingForHeroLock ? 'selecting' : 'loading', 'scene-connected');

            // Late entry selection must not wait for a map that only loads after confirmation.
            if (socket.waitingForHeroLock && Number(stagingCheckId) === Number(socket.finalMapId)) {
                const selectionIsCurrent = sceneScope.capture(socket);
                socket.heroLockTimeout = setTimeout(() => {
                    if (selectionIsCurrent()) confirmInitialHero(socket, socket.currentHeroId || 1015);
                }, 60000);
            }

            // 1. Força o Unity a entrar no modo de Jogo (HeroPlayer)
            socket.write(`mHeroPlayer:${utils.encodeInt(socket.playerIndex || 1)}\n`);

            // 2. NÃO ENVIAR 80005 AQUI! O 80005 DEVE SER ENVIADO QUANDO O MAPA STAGING (5010) CARREGAR!
            // Para não corromper o fundo da tela com bachang_baxi, esperamos o SLoadMapOKMsg do 5010.

            // 3. Envia o pacote 90005 (Início da Partida) via 20014
            const scnId = gameLogic.getSafeMapId(socket.targetMapId || state.getLastMapId() || 5202);
            const mapConf = MAP_CONFIG[scnId];
            const isChangeHero = (mapConf && [2, 3, 5, 6, 7, 11].includes(mapConf.type)) ? 1 : 0;
            const isChangeHeroEnc = utils.encodeInt(isChangeHero);
            const startTime = utils.Fun.GetServerDate();
            const p20014 = utils.encodeInt(20014);
            const s90005 = utils.encodeInt(90005);
            socket.write(`e${utils.encodeInt(socket.playerIndex || 1)}:SendMsgToClient,(${p20014},(${s90005},(${isChangeHeroEnc},'${startTime}')))\n`);
        }
        else if (socketType === "SCENE" && (/(?:^|:)SLoadMapOKMsg(?:,|$)/.test(textoRecebido))) {
            if (socket.sceneLoadPending || socket.isCombatReady || socket.sceneRetired) return;
            lifecycle.event(socket, 'MAP_READY');
            const readyRoom = socket.currentRoomUID == null ? null : roomContainers.get(Number(socket.currentRoomUID));
            if (socket.currentRoomUID != null && (!readyRoom || readyRoom.state !== 4)) return;
            console.log(`[🚀 SCENE] Mapa pronto! Spawnando herói padrão e abrindo seleção...`);

            const p1306 = utils.encodeInt(1306);
            const p12002 = utils.encodeInt(12002);
            const p20014 = utils.encodeInt(20014);
            const s90005 = utils.encodeInt(90005);
            const s90008 = utils.encodeInt(90008);
            const p5000 = utils.encodeInt(5000);
            const s80005 = utils.encodeInt(80005);
            
            // Herói padrão inicial
            const heroId = socket.currentHeroId || 1015;
            socket.currentHeroId = heroId;
            socket.maxHp = getHeroMaxHp(heroId);
            socket.hp = socket.maxHp;
            socket.shield = 0;
            socket.isInvincible = false;
            socket.godMode = false;
            
            const heroIdEnc = utils.encodeInt(heroId);
            const one = utils.encodeInt(1);
            const zero = utils.encodeInt(0);
            const dateStr = utils.Fun.GetServerDate();

            // Determina as coordenadas do spawn baseadas no mapa e equipe (Usando o mapa original!)
            const originalMapId = socket.targetMapId || state.getLastMapId() || 5202;
            const currentMapId = gameLogic.getSafeMapId(originalMapId);
            if ([9,12].includes(MAP_CONFIG[originalMapId]?.type)) socket.force = 1;
            const mapConf = MAP_CONFIG[currentMapId];
            const isChangeHero = (mapConf && [2, 3, 5, 6, 7, 11].includes(mapConf.type)) ? 1 : 0;
            const isChangeHeroEnc = utils.encodeInt(isChangeHero);

            // SE AINDA ESTIVERMOS AGUARDANDO O LOCK DO HERÓI (Fase de Staging/Seleção 5010), APENAS ABRA A UI (80005) E SUSPENDA O RESTO!
            const isStagingMap = (Number(originalMapId) === 5010 || Number(socket.targetMapId) === 5010);
            if (socket.waitingForHeroLock && isStagingMap) {
                socket.sceneLoadPending = true;
                socket.write(`e${utils.encodeInt(socket.playerIndex || 1)}:SendMsgToClient,(${p20014},(${s90005},(${isChangeHeroEnc},'${dateStr}')))\n`);
                socket.write(`e${utils.encodeInt(socket.playerIndex || 1)}:SendMsgToClient,(${p5000},(${s80005},()))\n`);
                console.log(`[🚀 ARENA UI] Mapa de Staging (5010) carregado. UI 80005 enviada com sucesso! Aguardando jogador dar Lock...`);

                // Auto-lock de segurança: Se o jogador não travar o herói em 25 segundos, o servidor trava automaticamente
                if (socket.heroLockTimeout) clearTimeout(socket.heroLockTimeout);
                socket.heroLockTimeout = setTimeout(() => {
                    if (socket.waitingForHeroLock && !socket.destroyed) {
                        console.log(`[⏰ AUTO-LOCK TIMEOUT] Jogador ${socket.playerName || socket.playerId} não travou herói a tempo. Travando automaticamente com herói atual...`);
                        gameLogic.switchPlayerHero(socket, socket.currentHeroId || 1015);
                        broadcastMatchUserList(socket);
                    }
                }, 25000);

                return; // NÃO SPAWNA NINGUÉM AQUI!
            }

            if (socket.waitingForHeroLock) return;

            // Garante que o flag de espera esteja false ao carregar qualquer mapa de combate real
            socket.waitingForHeroLock = false;
            if (socket.heroLockTimeout) {
                clearTimeout(socket.heroLockTimeout);
                socket.heroLockTimeout = null;
            }

            // SE NÃO FOR 5010 (Ex: É 5204), INICIA A PARTIDA NORMALMENTE!
            const coords = gameLogic.getPlayerSpawnCoordinates(socket);
            let sx = coords.sx, sy = coords.sy, sz = coords.sz;
            socket.lastX = sx;
            socket.lastY = sy;
            socket.lastZ = sz;

            // 1. Inicia a partida e libera o herói na memória (com delay seguro de 1500ms para inicialização estável dos componentes da Unity no cliente local)
            socket.sceneLoadPending = true;
            const spawnIsCurrent = sceneScope.capture(socket);
            socket.sceneSpawnTimeout = setTimeout(() => {
                if (!spawnIsCurrent()) return;
                socket.sceneLoadPending = false;
                socket.sceneSpawnTimeout = null;
                if (combat.isFreeForAll(socket)) {
                    if (!gameLogic.prepareFreeForAll(socket)) return;
                    const spawn = gameLogic.getPlayerSpawnCoordinates(socket);
                    socket.lastX = spawn.sx; socket.lastY = spawn.sy; socket.lastZ = spawn.sz;
                    socket.spawnYaw = spawn.yaw;
                }
                
                const pName = socket.playerName || (socket.remoteAddress && ipNames[socket.remoteAddress]);
                const profile = dbPersistence.loadPlayer(pName);
                if (!profile) return;

                const playerEq = state.playerEquipment[pName] || (profile && profile.equipment) || {};
                const eq = playerEq[heroId] || { skin: 1, weapon: 1, emote: 1, pose: 0 };
                const heroInfo = generateFullHeroData(heroId, eq.skin, eq.weapon, eq.emote, eq.pose, profile);

                socket.write(`e${utils.encodeInt(socket.playerIndex || 1)}:SendMsgToClient,(${p20014},(${s90005},(${isChangeHeroEnc},'${dateStr}')))\n`);
                socket.write(`e${utils.encodeInt(socket.playerIndex || 1)}:SendMsgToClient,(${p1306},(${p12002},(${heroIdEnc},${heroInfo.split(',').slice(1).join(',')})))\n`);

                // 2. Envia MatchUserInfo (10094) para destruir a Lobby UI e fechar popups, e depois o Spawn (10001)
                socket.bypassInterceptor = true;
                const spawnPacketMe = gameLogic.getPlayerSpawnPacket(socket, socket);
                const cmd10001 = utils.encodeInt(10001);
                const cmd10094 = utils.encodeInt(10094);
                const matchUserInfoPacket = spawnPacketMe.replace(
                    `SendMsgToClient,(${cmd10001},`,
                    `SendMsgToClient,(${cmd10094},`
                );
                socket.write(matchUserInfoPacket);
                socket.write(spawnPacketMe);
                socket.bypassInterceptor = false;
                lifecycle.transition(socket, 'playing', 'spawn-complete'); // Ready before peer handshake

                // Broadcast do spawn para todos os outros jogadores na cena (verão como oponente index 2)
                activeSockets.forEach(s => {
                    if (sceneScope.canReceive(socket, s) && s !== socket && !s.destroyed && !s.waitingForHeroLock) {
                        s.bypassInterceptor = true; // Protege o pacote
                        s.write(gameLogic.getPlayerSpawnPacket(socket, s));
                        s.bypassInterceptor = false; // Liga novamente
                    }
                });

                // Proativamente envia os spawns de todos os outros jogadores conectados para o novo jogador
                activeSockets.forEach(s => {
                    if (sceneScope.canReceive(socket, s) && s !== socket && !s.destroyed && !s.waitingForHeroLock) {
                        socket.bypassInterceptor = true; // Protege o pacote
                        socket.write(gameLogic.getPlayerSpawnPacket(s, socket));
                        socket.bypassInterceptor = false; // Liga novamente
                    }
                });

                // Proativamente envia os spawns de todos os bots ativos na partida para o novo jogador
                if (state.activeBots) {
                    for (const botId in state.activeBots) {
                        const bot = state.activeBots[botId];
                        if (!sceneScope.sameScene(socket, bot)) continue;
                        socket.bypassInterceptor = true;
                        socket.write(gameLogic.getPlayerSpawnPacket(bot, null));
                        socket.bypassInterceptor = false;
                    }
                }

                // 4. Destrava tudo (90008) - Fecha telas de seleção e loading no cliente
                socket.write(`e${utils.encodeInt(socket.playerIndex || 1)}:SendMsgToClient,(${p20014},(${s90008},(),${one}))\n`);
                console.log(`[🚀 SCENE] Spawn concluído com delay de 1500ms para o jogador ${socket.playerName} (NetID: ${socket.playerIndex})`);
                if (originalMapId === 5202 || currentMapId === 5202) {
                    gameLogic.spawnPracticeNpcs(socket, true);
                }
                
                // Inicializa as barreiras e bases no mapa de forma genérica
                if (!combat.isFreeForAll(socket) && ![9,12].includes(MAP_CONFIG[originalMapId]?.type)) gameLogic.spawnTdmBases(socket, originalMapId);

                // Inicializa a partida TDM, Control, Escort, Resource Capture, Arena ou PVE garantindo que o jogador já recebeu seu spawn e time (m_nShili)
                const destType = MAP_CONFIG[originalMapId] ? MAP_CONFIG[originalMapId].type : 0;
                if (!socket.waitingForHeroLock && destType === 7) {
                    gameLogic.startTdmMatch(socket);
                } else if (combat.isFreeForAll(socket)) {
                    gameLogic.startFreeForAll(socket);
                } else if (!socket.waitingForHeroLock && (destType === 3 || destType === 5)) {
                    gameLogic.startControlMatch(socket);
                } else if (!socket.waitingForHeroLock && destType === 2) {
                    gameLogic.startEscortMatch(socket);
                } else if (!socket.waitingForHeroLock && destType === 4) {
                    gameLogic.startArenaMatch(socket);
                } else if (!socket.waitingForHeroLock && (destType === 9 || destType === 12)) {
                    gameLogic.startPveMatch(socket, originalMapId);
                } else {
                    // Para todos os outros modos/mapas, abre a porta da base após um delay seguro de 5 segundos
                    socket.sceneDoorTimeout = setTimeout(() => {
                        if (!spawnIsCurrent()) return;
                        const p1030000 = utils.encodeInt(1030000);
                        socket.write(`e${utils.encodeInt(socket.playerIndex || 1)}:SendMsgToClient,(${p1030000},(${utils.encodeInt(1)},'close_open'))\n`);
                        console.log(`[🚀 SCENE] Modo genérico: Portas da base abertas automaticamente (Mapa ID: ${originalMapId}, Tipo: ${MAP_CONFIG[originalMapId] ? MAP_CONFIG[originalMapId].type : 'unknown'})`);
                    }, 5000);
                }
            }, 1500);

        }
        else if (socketType === "SCENE") {
            const lines = textoRecebido.split('\n');
            for (let line of lines) {
                line = line.trim();
                if (!line) continue;

                // Extrai o playerId (como e1, e2, etc.) de forma global no escopo da linha
                const idMatch = line.match(/^e([0-9A-Z_`a-z]+):/);
                const packetPlayerId = idMatch ? idMatch[1] : "1";
                // CORREÇÃO CRÍTICA MULTIPLAYER: Não sobrescrever socket.playerId se já foi associado a partir do Lobby ou ipIndexes
                if (!socket.playerId) {
                    socket.playerId = String(socket.playerIndex || ipIndexes[socket.remoteAddress] || 1);
                }
                const playerId = socket.playerId;
                if (gameLogic.relayPveBot(socket, line)) continue;

                // 1. Tratamento do Ping/Sync (SN->CN, SP->CP, ST->CT, SR->CR, SA->CA)
                // Essencial para o anti-desync do cliente não ativar a flag 'isSleep' que trava a movimentação
                const syncMatch = line.match(/:(S[NPTRVABW]),\((.+)\)/);
                if (syncMatch) {
                    const syncType = syncMatch[1]; // SN, SP, ST, SR, SV, SA
                    if (syncType !== 'SN' && (!socket.isCombatReady || socket.inMatch === false)) continue;
                    const syncArgs = syncMatch[2]; // ex: (Fkp,1c2,BNd),0,1c2 ou 0,0
                    
                    const clientType = "C" + syncType.charAt(1);

                    // Mantém as coordenadas do jogador atualizadas em tempo real para spawnar os bots à sua frente!
                    if (syncType === 'SP' || syncType === 'NSP') {
                        let dropStaleSp = false;
                        try {
                            const coordMatch = syncArgs.match(/\(([^)]+)\)(?:,([^,]+))?/);
                            if (coordMatch) {
                                const spDeadFlag = coordMatch[2] !== undefined ? utils.decodeInt(coordMatch[2]) : 0;
                                const parts = coordMatch[1].split(',');
                                if (parts.length >= 3) {
                                    const rawX = utils.decodeInt(parts[0]);
                                    const rawY = utils.decodeInt(parts[1]);
                                    const rawZ = utils.decodeInt(parts[2]);
                                    const inRespawnGrace = socket.lastRespawnTime && (Date.now() - socket.lastRespawnTime < 1000);
                                    if (!socket.isDead && !socket.isRagdoll && spDeadFlag === 1) {
                                        // Pacote SP em trânsito enviado pelo cliente enquanto estava morto antes de processar OnRevive
                                        dropStaleSp = true;
                                    } else if (inRespawnGrace && rawY !== undefined && rawY < -3500) {
                                        // Ignora coordenadas de abismo em trânsito logo após o respawn
                                        dropStaleSp = true;
                                    } else if (rawX !== undefined && rawY !== undefined && rawZ !== undefined && !socket.isDead && !socket.isRagdoll && spDeadFlag !== 1) {
                                        socket.lastX = rawX;
                                        socket.lastY = rawY;
                                        socket.lastZ = rawZ;

                                        // Detecção de queda no abismo / fora do mapa
                                        if (rawY < -3500) {
                                            console.log(`[☠️ QUEDA NO ABISMO] Jogador ${socket.playerName || socket.playerId} caiu para fora do mapa (Y=${rawY})!`);
                                            gameLogic.handlePlayerDeath(socket, null);
                                        }
                                    }
                                }
                            }
                        } catch (e) {
                            // Silently ignore
                        }
                        if (dropStaleSp) continue;
                    }

                    // FAIL-SAFE: Se o jogador estiver em partida ativa mas não foi inicializado (devido a loading lento)
                    if (global.activeControlMatch && !socket.controlInitialized && !socket.waitingForHeroLock && MAP_CONFIG[socket.targetMapId]?.type === 3) {
                        console.log(`[🏆 CONTROL FAIL-SAFE] Jogador ${socket.playerName} ativo em cena. Inicializando fluxo do HUD...`);
                        gameLogic.startControlMatch(socket);
                    } else if (!socket.tdmInitialized && !socket.waitingForHeroLock && Number(socket.targetMapId) !== 5010) {
                        const socketTdm = gameLogic.getTdmMatch ? gameLogic.getTdmMatch(socket, false) : global.activeTdmMatch;
                        if (socketTdm && socketTdm.state === 'fight') {
                            console.log(`[🏆 TDM FAIL-SAFE Sala ${socketTdm.roomUID}] Jogador ${socket.playerName} ativo em cena. Inicializando fluxo do TDM...`);
                            gameLogic.startTdmMatch(socket);
                        }
                    }

                    // A função C# public void CN(int no) recebe UM ÚNICO INT PURO (Sem parênteses)!
                    if (syncType === 'SN') {
                        const pingVal = syncArgs.split(',')[1] || "0";
                        socket.write(`e${utils.encodeInt(Number(playerId))}:CN,${pingVal}\n`);

                        // Carregamento passivo por tempo da Ultimate conforme dazhao.xml (energyaddtime por segundo real)
                        try {
                            const now = Date.now();
                            if (!socket.lastUltTimeUpdate) {
                                socket.lastUltTimeUpdate = now;
                            }
                            const elapsedMs = now - socket.lastUltTimeUpdate;
                            if (elapsedMs >= 1000) {
                                socket.lastUltTimeUpdate = now;
                                const currentHero = socket.currentHeroId || 1015;
                                const dzList = db.heroDazhaoMap[currentHero] || [];
                                const energyPerSec = Number(currentHero) === 2000 ? (dzList[0]?.energyAddTime ?? 30) : 50;
                                // A delayed heartbeat must not instantly award all time
                                // spent disconnected, loading or paused in the background.
                                const chargeMs = Number(currentHero) === 2000 ? Math.min(elapsedMs, 2000) : elapsedMs;
                                const addAmount = Math.round((chargeMs / 1000) * energyPerSec);
                                addPlayerUltEnergy(socket, addAmount);
                            }
                        } catch (e) {
                            console.error("[⚠️ ERRO ao atualizar energia passiva de Ultimate]:", e);
                        }
                    } 
                    // As funções C# public void CP(ArrayList arr), CT, CR, CV, CA recebem UMA ARRAYLIST!
                    // CB (Shoot Bullet / Projéteis / Ult) recebe parâmetros soltos: public void CB(launcherName, launcherNo, ...)
                    // CW (Shoot Weapon) recebe ArrayList: public void CW(ArrayList data)
                    else {
                        let packet;
                        if (syncType === 'SB') {
                            const shotBot = state.activeBots[utils.decodeInt(packetPlayerId)];
                            if (shotBot?.isPveMonster && !gameLogic.pveOwnsBot(socket, shotBot)) continue;
                            if (!shotBot && Number(socket.currentHeroId)===2000) {
                                let shot=utils.parseArrayList(syncArgs);
                                while(Array.isArray(shot) && shot.length===1 && Array.isArray(shot[0]))shot=shot[0];
                                if(Number(shot[0])===2) {
                                    lamsterKit.pulseFired(socket);
                                    if(lamster.healAlly(socket,socket,2)) gameLogic.applyBuffToEntity(socket,socket.playerId,1007);
                                }
                            }
                            const fixedArgs = utils.fixSbToCbArgs(syncArgs, shotBot?.loadId || socket.loadId || socket.playerId || 1);
                            packet = `e${packetPlayerId}:CB,(${fixedArgs})\n`;
                        } else if (syncType === 'SW') {
                            packet = `e${packetPlayerId}:CW,((${syncArgs}))\n`;
                        } else {
                            packet = `e${utils.encodeInt(Number(playerId))}:${clientType},((${syncArgs}))\n`;
                        }
                        activeSockets.forEach(s => {
                            if (sceneScope.canReceive(socket, s) && s !== socket && !s.destroyed && !s.waitingForHeroLock && s.isCombatReady) {
                                s.write(packet);
                            }
                        });
                        // CRITICAL: Echo CP back to the sender so playerCommon.pos stays updated.
                        if (syncType === 'SP') {
                            socket.write(packet);
                        }
                    }
                    continue;
                }

                // 2. Log genérico e Interceptação de Ações (Ataque, skills, hits, reload)
                if (line.includes("SendMsgToServer,")) {
                    const actionMatch = line.match(/SendMsgToServer,\(([^,]+),(.+)\)/);
                    if (actionMatch) {
                        const cmdId = utils.decodeInt(actionMatch[1]);
                        const argsStr = actionMatch[2];
                        if ((!socket.isCombatReady || socket.inMatch === false) && ![20001, 20002, 20014, 2110].includes(cmdId)) continue;
                        console.log(`[⚔️ AÇÃO DE BATALHA] ID: ${cmdId} (Base62: ${actionMatch[1]}) | DADOS: ${argsStr}`);

                        if (cmdId === 19702 && combat.isFreeForAll(socket)) {
                            gameLogic.respawnPlayer(socket);
                            continue;
                        }

                        // Se for 1000000 (SUICIDE / Slay Trigger - Espinhos, abismo ou queda fora do mapa)
                        if (cmdId === 1000000) {
                            console.log(`[☠️ GATILHO MORTAL / SLAY] Jogador ${socket.playerName || socket.playerId} caiu no abismo ou atingiu espinhos mortais!`);
                            if (!socket.isDead) {
                                gameLogic.handlePlayerDeath(socket, null);
                            }
                        }

                        // Se for 1000001 (HURT / Area_Hurt / HeiAn_Hurt - Dano de área do mapa, espinhos contínuos ou zonas sombrias)
                        if (cmdId === 1000001) {
                            try {
                                const parsed = utils.parseArrayList(argsStr);
                                const hurtType = parsed[2] || "Area_Hurt";
                                console.log(`[⚠️ DANO DE MAPA] Jogador ${socket.playerName || socket.playerId} pisou em zona de perigo (${hurtType})`);
                                if (!socket.isDead) {
                                    const hazardDamage = 150;
                                    socket.hp = Math.max(0, (socket.hp || 2000) - hazardDamage);
                                    gameLogic.sendEntityHpUpdate(socket, socket.playerId || "1", socket, true);
                                    if (socket.hp <= 0) {
                                        gameLogic.handlePlayerDeath(socket, null);
                                    }
                                }
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 1000001 map hazard]:", e);
                            }
                        }

                        // Se for 5007 (Detecção de jogadores na zona pelo MapEffectBox)
                        if (cmdId === 5007) {
                            try {
                                const parsed = utils.parseArrayList(argsStr);
                                const list = parsed[1] || [];
                                if (list.length > 0) {
                                    const mapEffectId = list[list.length - 1]; // O último elemento é o id do MapEffect
                                    const playerNetIds = list.slice(0, -1); // Os anteriores são os NetIDs dos jogadores na zona
                                    if (global.activeControlMatch) {
                                        gameLogic.handleControlZonePlayers5007(socket, mapEffectId, playerNetIds);
                                    }
                                }
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 5007 packet]:", e);
                            }
                        }

                        // Se for 5016 (SVR_CARINFO / UpDateCarDis, UpDateCarNum, UpDateCaPoint, UpDateCarState)
                        if (cmdId === 5016) {
                            try {
                                const parsed = utils.parseArrayList(argsStr);
                                const subCmd = parsed[0];
                                console.log(`[🚩 CONTROL/ESCORT 5016] SubCmd: ${subCmd} | argsStr: ${argsStr}`);
                                
                                // Processa atualizações de Control Point / Escolta se houver partida ativa
                                if (global.activeControlMatch) {
                                    if (subCmd === 5017) {
                                        gameLogic.handleControlZonePlayersUpdate(socket, parsed);
                                    } else if (subCmd === 5015) {
                                        gameLogic.handleControlZoneCaptureUpdate(socket, parsed);
                                    }
                                }
                                if (global.activeEscortMatch) {
                                    gameLogic.handleEscortCarPacket(socket, subCmd, parsed);
                                }

                                // Sincroniza/retransmite para todos os SCENE sockets na sala (incluindo o próprio cliente, pois ZhanDianCon.UpdateCarDis espera o subCmd como arr[0])
                                const subCmdEnc = utils.encodeInt(subCmd);
                                const fullListEnc = parsed.map(val => {
                                    if (typeof val === 'number') return utils.encodeInt(val);
                                    if (Array.isArray(val)) return `(${val.map(v => typeof v === 'number' ? utils.encodeInt(v) : v).join(',')})`;
                                    return val;
                                });
                                activeSockets.forEach(s => {
                                    if ((s === socket || sceneScope.canReceive(socket, s)) && !s.destroyed) {
                                        s.write(`e${utils.encodeInt(s.playerIndex || 1)}:SendMsgToClient,(${utils.encodeInt(5000)},(${subCmdEnc},(${fullListEnc.join(',')})))\n`);
                                    }
                                });
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 5016 packet]:", e);
                            }
                        }

                        // Opcode 4010002 (ROBOTAI_TOG_SELECT_SEARCH): Cliente local solicita autoridade de IA/NavMesh para o bot
                        if (cmdId === 4010002) {
                            try {
                                const parsed = utils.parseArrayList(argsStr);
                                const botId = parsed[0];
                                if (state.activeBots[botId]?.isPveMonster) {
                                    gameLogic.selectPveBotAuthority(socket, botId);
                                    continue;
                                }
                                const pIdx = socket.playerIndex || 1;
                                console.log(`[🤖 ROBOT AI AUTORIDADE] Atribuindo autoridade do Bot NetID ${botId} ao jogador ${socket.playerName || 'Host'} (Slot ${pIdx})`);
                                const resp = `e${utils.encodeInt(pIdx)}:SendMsgToClient,(${utils.encodeInt(4010001)},(${utils.encodeInt(botId)},${utils.encodeInt(0)},${utils.encodeInt(pIdx)}))\n`;
                                socket.write(resp);
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 4010002]:", e);
                            }
                        }

                        // Opcode 4010003 (ROBOTAI_TOG_SELECT_SUCCESS): Confirmação de que o cliente ativou a IA local do bot
                        if (cmdId === 4010003) {
                            console.log(`[🤖 ROBOT AI ATIVADO] Cliente confirmou ativação da IA NavMesh local do bot!`);
                        }

                        // Opcode 5025 (SVR_TABGETDATA): Cliente pressionou a tecla TAB para abrir o painel de estatísticas da partida
                        if (cmdId === 5025) {
                            gameLogic.sendTabScoreboard(socket);
                        }

                        // Opcode 20001 / 20002 (SVR_REV_GOTOHALL / SVR_RETURNHALLLOG): Jogador clica em "Voltar para o Hall" no menu ou encerra a partida
                        if (cmdId === 20001 || cmdId === 20002) {
                            console.log(`[🚪 RETORNO AO LOBBY] Jogador ${socket.playerName || 'Jogador'} (NetID ${socket.playerIndex || 1}) solicitou retorno ao Lobby.`);
                            const tdm = gameLogic.getTdmMatch(socket, false);
                            const leavingRoom = getSocketRoom(socket);
                            cleanPlayerMatchState(socket, socket.playerName, true);
                            if (leavingRoom) removePlayerFromRoom(leavingRoom, socket);
                            if (tdm && tdm.players) {
                                tdm.players = tdm.players.filter(p => p !== socket);
                                if (tdm.players.length === 0) {
                                    if (tdm.timer) clearInterval(tdm.timer);
                                    tdm.timer = null;
                                    if (tdm.endTimeout) clearTimeout(tdm.endTimeout);
                                    tdm.state = 'ended';
                                    if (tdm.roomUID && roomContainers.has(Number(tdm.roomUID))) {
                                        const r = roomContainers.get(Number(tdm.roomUID));
                                        if (r) r.tdmMatch = null;
                                    }
                                    if (global.activeTdmMatch === tdm) global.activeTdmMatch = null;
                                    console.log(`[🏆 TDM Sala ${tdm.roomUID}] Todos os jogadores retornaram ao saguão. Partida limpa antecipadamente.`);
                                }
                            }
                            if (global.activeControlMatch && global.activeControlMatch.players) {
                                global.activeControlMatch.players = global.activeControlMatch.players.filter(p => p !== socket);
                                if (global.activeControlMatch.players.length === 0) {
                                    if (global.activeControlMatch.timer) clearInterval(global.activeControlMatch.timer);
                                    if (global.activeControlMatch.interval) clearInterval(global.activeControlMatch.interval);
                                    global.activeControlMatch = null;
                                }
                            }

                            return;
                        }

                        // Se for 31203 (Reload / Fill Cartridge)
                        if (cmdId === 31203) {
                            const argParts = argsStr.replace(/\(/g, "").replace(/\)/g, "").split(",");
                            const shootName = argParts[0] || 1;
                            const currentHeroId = socket.currentHeroId || 1015;
                            const maxAmmo = weaponsMap[currentHeroId] && weaponsMap[currentHeroId].cartridgePublic !== undefined ? weaponsMap[currentHeroId].cartridgePublic : 30;
                            
                            // Cancela recarga anterior caso esteja em andamento
                            if (socket.reloadTimer) {
                                clearTimeout(socket.reloadTimer);
                                socket.reloadTimer = null;
                            }

                            if (socket.isDead || socket.isRagdoll) {
                                socket.isReloading = false;
                                const unblockPacket = `e${utils.encodeInt(Number(playerId))}:OnFillBullet,((${shootName},0,${utils.encodeInt(maxAmmo)},${utils.encodeInt(maxAmmo)}))\n`;
                                socket.write(unblockPacket);
                                console.log(`[🚫 RELOAD UNBLOCKED] Jogador ${playerId} está morto/ragdoll; enviando OnFillBullet imediato para destravar _fillCartridge.`);
                                return;
                            }
                            
                            const reloadDuration = heroCatalog.getReloadDuration(currentHeroId);
                            console.log(`[🔄 RELOAD STARTED] Jogador ${playerId} (Herói ${currentHeroId}) iniciou recarga (${reloadDuration}ms)...`);
                            socket.isReloading = true;
                            socket.lastReloadShootName = shootName;
                            const reloadIsCurrent = sceneScope.capture(socket);
                            socket.reloadTimer = setTimeout(() => {
                                socket.isReloading = false;
                                socket.reloadTimer = null;
                                if (reloadIsCurrent()) {
                                    const fillBulletPacket = `e${utils.encodeInt(Number(playerId))}:OnFillBullet,((${shootName},0,${utils.encodeInt(maxAmmo)},${utils.encodeInt(maxAmmo)}))\n`;
                                    socket.write(fillBulletPacket);
                                    console.log(`[✅ RELOAD FINISHED]: ${fillBulletPacket.trim()}`);
                                }
                            }, reloadDuration);
                        }

                        // Se for 10095 (CPaintManager - Sincronização de sprays/paints)
                        if (cmdId === 10095) {
                            try {
                                const parsed = utils.parseArrayList(argsStr);
                                const subType = parsed[0];
                                console.log(`[🎨 SPRAY/PAINT SYNC] SubType: ${subType}`);

                                if (!global.activePaints) {
                                    global.activePaints = [];
                                }

                                if (subType === 2) {
                                    // Solicitação de sincronização de sprays existentes
                                    const cid = socket.playerIndex || 1;
                                    const cidEnc = utils.encodeInt(cid);
                                    const subtypeEnc = utils.encodeInt(2);
                                    
                                    // Formata a lista de sprays ativos
                                    const activePaintsList = global.activePaints.map(p => {
                                        const pIdEnc = utils.encodeInt(p.paintId);
                                        const pxEnc = utils.encodeInt(p.x);
                                        const pyEnc = utils.encodeInt(p.y);
                                        const pzEnc = utils.encodeInt(p.z);
                                        const rxEnc = utils.encodeInt(p.rx);
                                        const ryEnc = utils.encodeInt(p.ry);
                                        const rzEnc = utils.encodeInt(p.rz);
                                        const parentIdEnc = utils.encodeInt(p.parentId);
                                        
                                        const sprayDataTuple = `(${pIdEnc},${pxEnc},${pyEnc},${pzEnc},${rxEnc},${ryEnc},${rzEnc},${parentIdEnc})`;
                                        return `${utils.encodeInt(p.ownerCid)},${sprayDataTuple}`;
                                    }).join(',');
                                    
                                    const response = `e${utils.encodeInt(socket.playerIndex || 1)}:SendMsgToClient,(${utils.encodeInt(10095)},(${cidEnc},(${activePaintsList}),${subtypeEnc}))\n`;
                                    socket.write(response);
                                    console.log(`[🎨 SPRAY/PAINT SYNC] Enviado ${global.activePaints.length} sprays ativos para o jogador.`);
                                }
                                else if (subType === 1) {
                                    // Adicionando um spray
                                    const sprayArgs = parsed[1] || [];
                                    if (sprayArgs.length >= 8) {
                                        const paintId = sprayArgs[0];
                                        const x = sprayArgs[1];
                                        const y = sprayArgs[2];
                                        const z = sprayArgs[3];
                                        const rx = sprayArgs[4];
                                        const ry = sprayArgs[5];
                                        const rz = sprayArgs[6];
                                        const parentId = sprayArgs[7];
                                        const ownerCid = socket.playerIndex || 1;
                                        
                                        // Remove spray anterior deste mesmo jogador se existir
                                        global.activePaints = global.activePaints.filter(p => p.ownerCid !== ownerCid);
                                        
                                        // Armazena o novo spray
                                        global.activePaints.push({
                                            ownerCid,
                                            paintId,
                                            x, y, z,
                                            rx, ry, rz,
                                            parentId
                                        });
                                        
                                        // Envia o spray para todos os jogadores na cena
                                        const cidEnc = utils.encodeInt(ownerCid);
                                        const subtypeEnc = utils.encodeInt(1);
                                        const netIdEnc = utils.encodeInt(socket.playerIndex || 1);
                                        
                                        const pIdEnc = utils.encodeInt(paintId);
                                        const pxEnc = utils.encodeInt(x);
                                        const pyEnc = utils.encodeInt(y);
                                        const pzEnc = utils.encodeInt(z);
                                        const rxEnc = utils.encodeInt(rx);
                                        const ryEnc = utils.encodeInt(ry);
                                        const rzEnc = utils.encodeInt(rz);
                                        const parentIdEnc = utils.encodeInt(parentId);
                                        
                                        const sprayDataTuple = `(${pIdEnc},${pxEnc},${pyEnc},${pzEnc},${rxEnc},${ryEnc},${rzEnc},${parentIdEnc})`;
                                        
                                        activeSockets.forEach(s => {
                                            if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                                const broadcastPacket = `e${utils.encodeInt(s.playerIndex || 1)}:SendMsgToClient,(${utils.encodeInt(10095)},(${cidEnc},${sprayDataTuple},${subtypeEnc},${netIdEnc}))\n`;
                                                s.write(broadcastPacket);
                                            }
                                        });
                                        console.log(`[🎨 SPRAY/PAINT SYNC] Spray de Jogador ${ownerCid} (PaintID: ${paintId}) retransmitido para todos.`);
                                    }
                                }
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 10095 packet]:", e);
                            }
                        }

                        // Se for 8475 (MSG_MATCH_CLIENTREADY / Entrada de Jogo Confirmada)
                        if (cmdId === 8475) {
                            console.log(`[🤖 CLIENT READY] O jogador carregou o mapa de treino! Spawnando NPCs...`);
                            gameLogic.spawnPracticeNpcs(socket);
                        }

                        // Se for 31207 (ChangeWeaponGroup)
                        if (cmdId === 31207) {
                            if (socket.isDead || socket.isRagdoll) {
                                console.log(`[🚫 WEAPON SWITCH BLOCKED] Jogador ${playerId} está morto e não pode trocar de arma!`);
                                return;
                            }
                            try {
                                const parsed = utils.parseArrayList(argsStr);
                                const xingtai = parsed[0] || 0;
                                const changeGroupPacket = `e${utils.encodeInt(Number(playerId))}:ChangeWeaponGroupUpdate,((${utils.encodeInt(xingtai)}))\n`;
                                activeSockets.forEach(s => {
                                    if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                        s.write(changeGroupPacket);
                                    }
                                });
                                socket.currentWeaponGroup = xingtai;
                                console.log(`[🎯 WEAPON GROUP SWITCH] ChangeWeaponGroup to ${xingtai} (Player: ${playerId}). Broadcasted to all scene players.`);
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 31207 ChangeWeaponGroup]:", e);
                            }
                        }

                        // Se for 31401 (MSG_SKILLS_PARM / Parâmetros customizados de habilidades como escudos/invocações)
                        if (cmdId === 31401) {
                            try {
                                let skillParmsRaw = utils.parseArrayList(argsStr);
                                if (skillParmsRaw.length === 1 && Array.isArray(skillParmsRaw[0])) {
                                    skillParmsRaw = skillParmsRaw[0];
                                }
                                console.log(`[⚔️ SKILLS PARM RAW RECEIVED]:`, skillParmsRaw);
                                
                                // NÃO decodificamos mais aqui, pois utils.parseArrayList já realiza a decodificação de Base62 para inteiros automaticamente!
                                const netId = skillParmsRaw[0] || playerId;
                                const skId = skillParmsRaw[1];
                                const veCreartPos = skillParmsRaw[2] || [0, 0, 0];
                                const quapos = skillParmsRaw[3] || [0, 0, 0];
                                
                                // Guarda os parâmetros salvos decodificados no socket
                                socket.pendingSkillParms = { netId, skId, veCreartPos, quapos };
                                console.log(`[⚔️ SKILLS PARM DECODED]:`, socket.pendingSkillParms);

                                // For Dragos Bat Skill (1100) cooldown start on finish (when state is 1)
                                if ((skId === 1100 || String(skId) === "1100") && (skillParmsRaw[2] === 1 || String(skillParmsRaw[2]) === "1")) {
                                    let cdVal = 10000;
                                    if (playerSkillsMap[1100] !== undefined) {
                                        cdVal = playerSkillsMap[1100].cd;
                                    }
                                    if (socket.nocd) {
                                        cdVal = 0;
                                    }
                                    const cdValEnc = utils.encodeInt(cdVal);
                                    const skIdEnc = utils.encodeInt(1100);
                                    const skillUpdatePacket = `e${utils.encodeInt(Number(playerId))}:SkillDataUpdate,(${utils.encodeInt(31102)},((${skIdEnc},0,0,0,${utils.encodeInt(1)},${cdValEnc},0,0,0,0,${cdValEnc})))\n`;
                                    socket.write(skillUpdatePacket);
                                    console.log(`[🔋 POST-TRANSFORMATION CD STARTED] Started CD of ${cdVal}ms for skill 1100 (Player: ${playerId})`);
                                }

                                // Broadcast SkillParmsUpdate para sincronizar estado de bat de Dragos ou outras habilidades customizadas nos outros clientes
                                const paramsEnc = skillParmsRaw.map(val => {
                                    if (typeof val === 'number') return utils.encodeInt(val);
                                    if (Array.isArray(val)) return `(${val.map(v => typeof v === 'number' ? utils.encodeInt(v) : v).join(',')})`;
                                    return val;
                                });
                                const updatePacket = `e${netId}:SkillParmsUpdate,((${paramsEnc.join(',')}))\n`;
                                activeSockets.forEach(s => {
                                    if (sceneScope.canReceive(socket, s) && !s.destroyed && s !== socket) {
                                        s.write(updatePacket);
                                    }
                                });
                                console.log(`[⚔️ SKILLS PARM BROADCASTED]:`, updatePacket.trim());
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 31401 skill parms]:", e);
                            }
                        }

                        // Se for 31103 (Skill Cast / MSG_PLAYERSKILL_DO)
                        if (cmdId === 31103) {
                            if (socket.isDead || socket.isRagdoll) {
                                console.log(`[🚫 CAST BLOCKED] Jogador ${playerId} está morto e não pode usar habilidades!`);
                                gameLogic.sendSystemChat(socket, "⚠️ Você está morto!");
                                return;
                            }
                            if (socket.isStunned || socket.isFrozen) {
                                console.log(`[🚫 CAST BLOCKED] Jogador ${playerId} está atordoado/congelado e não pode usar habilidades!`);
                                gameLogic.sendSystemChat(socket, "⚠️ Você está atordoado/congelado e não pode usar habilidades!");
                                return;
                            }
                            if (socket.activeBuffs && socket.activeBuffs[1015]) {
                                console.log(`[🚫 CAST BLOCKED] Jogador ${playerId} está no casulo e não pode usar habilidades!`);
                                gameLogic.sendSystemChat(socket, "⚠️ Você está no casulo e não pode usar habilidades!");
                                return;
                            }
                            try {
                                let skillArgs = utils.parseArrayList(argsStr);
                                if (skillArgs.length === 1 && Array.isArray(skillArgs[0])) {
                                    skillArgs = skillArgs[0];
                                }
                                
                                // NÃO decodificamos mais aqui, pois utils.parseArrayList já realiza a decodificação de Base62 para inteiros automaticamente!
                                const skillId = skillArgs[0];
                                // Own IDs and cooldown authority are exclusive to Lamster.
                                // Never pass a foreign skill through the generic legacy path.
                                if (Number(socket.currentHeroId) === 2000 || lamsterHero.SKILL_IDS.has(Number(skillId))) {
                                    lamsterKit.cast(socket, Number(skillId));
                                    continue;
                                }
                                const vec = skillArgs[1] || [0, 0, 0];
                                let targets = skillArgs[2] || [];
                                 
                                 // Correção de alvos para habilidades benéficas de grupo (Venus/Daji Ult 1153 e Angel Ult 1122)
                                 if (skillId === 1153 || skillId === 1122) {
                                     const casterForce = socket.force !== undefined ? socket.force : 1;
                                     targets = activeSockets
                                         .filter(s => sceneScope.canReceive(socket, s) && !s.destroyed && combat.sameSide(socket, s, Number(s.force) === Number(casterForce)) && String(s.playerId) !== String(playerId))
                                         .map(s => Number(s.playerId));
                                     console.log(`[🦊/🍃 GROUP SKILL FIX] Forçando alvos aliados (exceto caster) para a habilidade ${skillId}:`, targets);
                                 }

                                 // 🔮 CARMEN / TALIA (1021) — CUT-HEAL (Skill 1062) & CUT-DEATH / BEACON (Skill 1064)
                                 // O cliente C# envia OverlapSphere de 40m adicionando todos os aliados/inimigos próximos em arrayList2.
                                 // Para respeitar a regra oficial de maxbuffnumber=1, limitamos targets estritamente ao 1º alvo (o alvo mirado).
                                 if (skillId === 1062 || skillId === 1064) {
                                     if (Array.isArray(targets) && targets.length > 1) {
                                         console.log(`[🔮 CARMEN SINGLE-TARGET] Habilidade ${skillId} limitando alvos de ${targets.length} para 1 único alvo (${targets[0]})`);
                                         targets = targets.slice(0, 1);
                                     }
                                 }
                                
                                const skillIdEnc = utils.encodeInt(skillId);
                                const xEnc = utils.encodeInt(vec[0] || 0);
                                const yEnc = utils.encodeInt(vec[1] || 0);
                                const zEnc = utils.encodeInt(vec[2] || 0);
                                
                                let targetsStr = "";
                                if (socket.pendingSkillParms) {
                                    console.log(`[⚔️ SKILL DO] Reconstruindo parâmetros da habilidade usando os dados salvos de 31401!`);
                                    const netId = socket.pendingSkillParms.netId || playerId;
                                    const skId = socket.pendingSkillParms.skId || skillId;
                                    const veCreartPos = socket.pendingSkillParms.veCreartPos || [0, 0, 0];
                                    const quapos = socket.pendingSkillParms.quapos || [0, 0, 0];
                                    
                                     if (skId === 1163 || skillId === 1163) {
                                         const lilyBarrier = heroCatalog.getHeroBarrierSpec(1033);
                                         const shieldId = 20000 + Number(playerId);
                                         const hp = lilyBarrier.maxHp;
                                         const maxHp = lilyBarrier.maxHp;
                                         const dateStr = "'2026-05-18 12:00:00'"; // C# expects a valid DateTime format
                                         const creatorId = Number(playerId);

                                         const shieldIdEnc = utils.encodeInt(shieldId);
                                         const hpEnc = utils.encodeInt(hp);
                                         const maxHpEnc = utils.encodeInt(maxHp);
                                         const creatorIdEnc = utils.encodeInt(creatorId);
                                         const veCreartPosEnc = `(${utils.encodeInt(veCreartPos[0])},${utils.encodeInt(veCreartPos[1])},${utils.encodeInt(veCreartPos[2])})`;
                                         const quaposEnc = `(${utils.encodeInt(quapos[0])},${utils.encodeInt(quapos[1])},${utils.encodeInt(quapos[2])})`;
                                         
                                         targetsStr = `${shieldIdEnc},${hpEnc},${maxHpEnc},${dateStr},${creatorIdEnc},${veCreartPosEnc},${quaposEnc}`;
                                         
                                         // Inicializa o shield de Lily no socket
                                         socket.lilyBarrierHp = hp;
                                         socket.lilyBarrierMaxHp = maxHp;
                                         socket.lilyBarrierActive = true;
                                         socket.lilyBarrierPos = veCreartPos;
                                         socket.lilyBarrierRot = quapos;
                                         console.log(`[🍃 LILY BARREIRA ATIVADA] Player ${playerId} - HP da barreira: ${maxHp} | Pos: ${veCreartPos} | Rot: ${quapos}`);
                                    } else {
                                        const netIdEnc = utils.encodeInt(netId);
                                        const skIdEnc = utils.encodeInt(skId);
                                        const veCreartPosEnc = `(${utils.encodeInt(veCreartPos[0])},${utils.encodeInt(veCreartPos[1])},${utils.encodeInt(veCreartPos[2])})`;
                                        const quaposEnc = `(${utils.encodeInt(quapos[0])},${utils.encodeInt(quapos[1])},${utils.encodeInt(quapos[2])})`;
                                        targetsStr = `${netIdEnc},${skIdEnc},0,0,0,${veCreartPosEnc},${quaposEnc}`;
                                    }
                                    socket.pendingSkillParms = null; // limpa o cache
                                } else {
                                    if (Array.isArray(targets)) {
                                        targetsStr = targets.map(t => utils.encodeInt(t)).join(",");
                                    }
                                }
                                
                                // 1. Envia o SkillDo para o jogador e todos os clientes na cena
                                const skillDoPacket = `e${utils.encodeInt(Number(playerId))}:SkillDo,((${skillIdEnc},(${xEnc},${yEnc},${zEnc})),(${targetsStr}))\n`;
                                activeSockets.forEach(s => {
                                    if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                        s.write(skillDoPacket);
                                    }
                                });
                                console.log(`[⚔️ SKILL DO]: ${skillDoPacket.trim()}`);

                                 // Glider Ultimate Lock-on Start
                                 if (skillId === 1018) {
                                     socket.ultimateStart = Date.now();
                                     console.log(`[🚀 GLIDER ULTIMATE START] Registrado início da ult para o jogador ${playerId}`);
                                 }

                                  // 🛡️ LOKI (1028) — ESCUDO DE RETALIAÇÃO / REVERSÃO DE DANO (Skill 1112)
                                  if (skillId === 1112) {
                                      const lokiSpec = heroCatalog.getHeroSpec(1028);
                                      socket.lokiShieldActive = true;
                                      socket.lokiAbsorbedDamage = 0;
                                      socket.lokiShieldEnd = Date.now() + lokiSpec.retaliationDurationMs;
                                      console.log(`[🛡️ LOKI SHIELD ACTIVATED] Jogador ${playerId} ativou Escudo de Retaliação (1112) por ${lokiSpec.retaliationDurationMs}ms!`);
                                  }

                                  // 👼 ANGEL (1029) — SYLPH'S WIND (Skill 1122: Buff 1035 Speed Boost estritamente para si e aliados dentro da área de cura ~12m)
                                  if (skillId === 1122) {
                                      const angelSpec = heroCatalog.getHeroSpec(1029);
                                      const angelForce = Number(socket.force !== undefined ? socket.force : (playerSessions[socket.playerName]?.force || 1));
                                      const aX = socket.lastX || 0;
                                      const aY = socket.lastY || 0;
                                      const aZ = socket.lastZ || 0;
                                      
                                      // 1. Aplica em si mesma (6s de speed boost)
                                      gameLogic.applyBuffToEntity(socket, playerId, angelSpec.sylphWindBuffId, angelSpec.sylphWindDurationMs);
                                      console.log(`[👼 ANGEL SPEED BOOST] Angel ${playerId} (Força ${angelForce}) ativou Sylph's Wind (1122) em si mesma!`);

                                      // 2. Aplica estritamente nos aliados humanos presentes dentro do raio de 12 metros (1200 unidades)
                                      activeSockets.forEach(s => {
                                          if (sceneScope.canReceive(socket, s) && !s.destroyed && s !== socket && !s.isDead && (s.hp === undefined || s.hp > 0)) {
                                              const sForce = Number(s.force !== undefined ? s.force : (playerSessions[s.playerName]?.force || 1));
                                              // Trava de segurança absoluta: apenas membros da mesma equipe (aliados)
                                              if (combat.sameSide(socket, s, sForce === angelForce)) {
                                                  const sX = s.lastX || 0;
                                                  const sY = s.lastY || 0;
                                                  const sZ = s.lastZ || 0;
                                                  const dist = Math.hypot(sX - aX, sY - aY, sZ - aZ);
                                                  // Estritamente dentro de 12 metros (1200 unidades) e posições válidas
                                                  if (dist <= angelSpec.sylphWindRadius && (aX !== 0 || aZ !== 0) && (sX !== 0 || sZ !== 0)) {
                                                      gameLogic.applyBuffToEntity(socket, s.playerId, angelSpec.sylphWindBuffId, angelSpec.sylphWindDurationMs);
                                                      console.log(`[👼 ANGEL SPEED BOOST] Aliado ${s.playerId} (Força ${sForce}) recebeu Buff ${angelSpec.sylphWindBuffId} (Sylph's Wind)! Dist: ${(dist/100).toFixed(1)}m <= 12.0m`);
                                                  } else {
                                                      console.log(`[👼 ANGEL SPEED BOOST IGNORED] Aliado ${s.playerId} FORA da área de cura (${(dist/100).toFixed(1)}m > 12.0m). Não recebeu buff.`);
                                                  }
                                              }
                                          }
                                      });

                                      // 3. Aplica estritamente nos bots aliados dentro do raio de 12 metros (1200 unidades)
                                      if (state.activeBots) {
                                          Object.keys(state.activeBots).forEach(botId => {
                                              const bot = state.activeBots[botId];
                                              const botForce = Number(bot && bot.force !== undefined ? bot.force : (bot && bot.team !== undefined ? bot.team : 2));
                                              if (bot && !bot.isDead && combat.sameSide(socket, bot, botForce === angelForce)) {
                                                  const bX = (bot.lastX !== undefined) ? bot.lastX : 0;
                                                  const bY = (bot.lastY !== undefined) ? bot.lastY : 0;
                                                  const bZ = (bot.lastZ !== undefined) ? bot.lastZ : 0;
                                                  const dist = Math.hypot(bX - aX, bY - aY, bZ - aZ);
                                                  if (dist <= angelSpec.sylphWindRadius && (aX !== 0 || aZ !== 0) && (bX !== 0 || bZ !== 0)) {
                                                      gameLogic.applyBuffToEntity(socket, botId, angelSpec.sylphWindBuffId, angelSpec.sylphWindDurationMs);
                                                      console.log(`[👼 ANGEL SPEED BOOST] Bot aliado ${botId} (Força ${botForce}) recebeu Buff ${angelSpec.sylphWindBuffId} (Sylph's Wind)! Dist: ${(dist/100).toFixed(1)}m <= 12.0m`);
                                                  }
                                              }
                                          });
                                      }
                                  }


                                 // ==========================================
                                 // 🛡️ GERENCIADOR DINÂMICO DE BUFFS DE SKILL (Self & Target Buffs)
                                 // ==========================================
                                 let skillConfig = playerSkillsMap[skillId];
                                 if (groupSkillsOverrides[skillId]) {
                                     skillConfig = Object.assign({}, skillConfig, groupSkillsOverrides[skillId]);
                                 }

                                 if (skillConfig) {
                                     const buffDateStr = utils.Fun.GetServerDate();

                                     // --- 1. APLICAÇÃO DE AUTO-BUFFS (buff_idx_me) ---
                                     if (Array.isArray(skillConfig.buff_idx_me) && skillConfig.buff_idx_me.length > 0) {
                                         socket.activeBuffs = socket.activeBuffs || {};
                                         socket.activeUltimateBuffs = socket.activeUltimateBuffs || [];

                                         // Determina se esta habilidade é uma Ultimate para o herói atual
                                         const currentHero = socket.currentHeroId || 1015;
                                         const dazhaoList = heroDazhaoMap[currentHero] || [];
                                         const isUltimateSkill = dazhaoList.some(dz => dz.doskill === skillId);

                                         skillConfig.buff_idx_me.forEach(buffId => {
                                             // Determina a duração: prioritiza tempo do buff.xml, senão longtime do playerskill.xml, senão dazhao continuestime se for ult
                                             let duration = 0;
                                             if (buffsMap[buffId] && buffsMap[buffId].time > 0) {
                                                 duration = buffsMap[buffId].time;
                                             }
                                             if (duration <= 0 && skillConfig.longtime > 0) {
                                                 duration = skillConfig.longtime;
                                             }
                                             if (duration <= 0 && isUltimateSkill && dazhaoList.length > 0) {
                                                 duration = dazhaoList[0].continueTime || 3000;
                                             }
                                             if (duration <= 0) {
                                                 duration = 5000; // default fallback
                                             }

                                             // Se for ultimate, registra o Buff ID para remoção instantânea em caso de terminação precoce
                                             if (isUltimateSkill && !socket.activeUltimateBuffs.includes(buffId)) {
                                                 socket.activeUltimateBuffs.push(buffId);
                                             }

                                             // Aplica o buff dinamicamente através do Motor de Buffs do Servidor
                                             gameLogic.applyBuffToEntity(socket, playerId, buffId, duration);
                                         });
                                     }

                                     // --- 2. APLICAÇÃO DE BUFFS EM ALVOS (buff_idx) ---
                                     if (Array.isArray(skillConfig.buff_idx) && skillConfig.buff_idx.length > 0 && Array.isArray(targets) && targets.length > 0) {
                                          skillConfig.buff_idx.forEach(buffId => {
                                              // 🔮 CARMEN (1021) — CUT-HEAL (Skill 1062 / Buff 1021: Marca em APENAS 1 aliado)
                                              if (skillId === 1062 || buffId === 1021) {
                                                  const tId = targets[0];
                                                  if (tId !== undefined && tId !== null && Number(tId) !== Number(playerId)) {
                                                      if (socket.carmenHealTargetId && String(socket.carmenHealTargetId) !== String(tId)) {
                                                          console.log(`[🔮 CARMEN CUT-HEAL] Transferindo marca: removendo de ${socket.carmenHealTargetId} para ${tId}`);
                                                          gameLogic.cleanupBuff(null, socket.carmenHealTargetId, 1021, true);
                                                      }
                                                      socket.carmenHealTargetId = String(tId);
                                                      const dur = (buffsMap[buffId] && buffsMap[buffId].time > 0) ? buffsMap[buffId].time : (skillConfig.longtime || 15000);
                                                      gameLogic.applyBuffToEntity(socket, tId, buffId, dur);
                                                      console.log(`[🔮 CARMEN CUT-HEAL] Marca de cura aplicada no aliado ${tId} por ${dur}ms`);
                                                  }
                                                  return;
                                              }

                                              // 🔮 CARMEN (1021) — CUT-DEATH / BEACON (Skill 1064 / Buff 1005|1010: Marca em APENAS 1 inimigo)
                                              if (skillId === 1064 || buffId === 1005 || buffId === 1010) {
                                                  const tId = targets[0];
                                                  if (tId !== undefined && tId !== null && Number(tId) !== Number(playerId)) {
                                                      if (socket.carmenBeaconTargetId && String(socket.carmenBeaconTargetId) !== String(tId)) {
                                                          console.log(`[🔮 CARMEN BEACON] Transferindo beacon: removendo de ${socket.carmenBeaconTargetId} para ${tId}`);
                                                          gameLogic.cleanupBuff(null, socket.carmenBeaconTargetId, 1005, true);
                                                          gameLogic.cleanupBuff(null, socket.carmenBeaconTargetId, 1010, true);
                                                      }
                                                      socket.carmenBeaconTargetId = String(tId);
                                                      const dur = (buffsMap[buffId] && buffsMap[buffId].time > 0) ? buffsMap[buffId].time : (skillConfig.longtime || 10000);
                                                      gameLogic.applyBuffToEntity(socket, tId, buffId, dur);
                                                  }
                                                  return;
                                              }

                                              targets.forEach(targetId => {
                                                if (Number(targetId) === Number(playerId)) return; // Trava: buff_idx nunca afeta o próprio conjurador
                                                // Determina a duração: prioritiza tempo do buff.xml, senão longtime do playerskill.xml
                                                 let duration = 0;
                                                 if (buffsMap[buffId] && buffsMap[buffId].time > 0) {
                                                     duration = buffsMap[buffId].time;
                                                 }
                                                 if (duration <= 0 && skillConfig.longtime > 0) {
                                                     duration = skillConfig.longtime;
                                                 }
                                                 if (duration <= 0) {
                                                     duration = 5000; // default fallback
                                                 }

                                                 // Aplica o buff/debuff dinamicamente no alvo através do Motor do Servidor
                                                 gameLogic.applyBuffToEntity(socket, targetId, buffId, duration);
                                             });
                                         });
                                     }
                                 }



                                  // 2. Envia o SkillDataUpdate para iniciar o cooldown e travar spam
                                  // 2. Envia o SkillDataUpdate para atualizar cargas e cooldown independente
                                  const skillConfigForCd = playerSkillsMap[skillId];
                                  const maxChargesForSkill = (skillConfigForCd && skillConfigForCd.maxSkillNumber > 1) ? skillConfigForCd.maxSkillNumber : 1;
                                  socket.skillCharges = socket.skillCharges || {};
                                  socket.skillChargeTimers = socket.skillChargeTimers || {};
                                  if (socket.skillCharges[skillId] === undefined) {
                                      socket.skillCharges[skillId] = maxChargesForSkill;
                                  }
                                  if (maxChargesForSkill > 1) {
                                      socket.skillCharges[skillId] = Math.max(0, socket.skillCharges[skillId] - 1);
                                  } else {
                                      socket.skillCharges[skillId] = 0;
                                  }
                                  const remainingCharges = socket.skillCharges[skillId];
                                  const remainingChargesEnc = utils.encodeInt(remainingCharges);

                                  let actualCd = 10000; // Fallback default de 10s
                                  if (playerSkillsMap[skillId] !== undefined) {
                                      actualCd = playerSkillsMap[skillId].cd;
                                  }

                                  if (socket.nocd) {
                                      actualCd = 0;
                                  }

                                  // Cooldown contínuo e independente por carga:
                                  // Se já existir um temporizador de recarga em andamento, mantemos o tempo restante em vez de resetar do zero!
                                  let cdToSend = actualCd;
                                  const now = Date.now();
                                  if (maxChargesForSkill > 1 && remainingCharges < maxChargesForSkill) {
                                      if (!socket.skillChargeTimers[skillId] || socket.skillChargeTimers[skillId] <= now) {
                                          socket.skillChargeTimers[skillId] = now + actualCd;
                                          cdToSend = actualCd;
                                      } else {
                                          cdToSend = Math.max(100, socket.skillChargeTimers[skillId] - now);
                                      }
                                  }

                                  const actualCdEnc = utils.encodeInt(actualCd);
                                  const cdToSendEnc = utils.encodeInt(cdToSend);
                                  const skillLongTime = (skillConfig && skillConfig.longtime) ? skillConfig.longtime : 0;
                                   const longtimeEnc = utils.encodeInt(skillLongTime);
                                   const skillUpdatePacket = `e${utils.encodeInt(Number(playerId))}:SkillDataUpdate,(${utils.encodeInt(31102)},((${skillIdEnc},0,${remainingChargesEnc},0,${utils.encodeInt(1)},${cdToSendEnc},0,0,0,${longtimeEnc},${actualCdEnc})))\n`;
                                 if (actualCd > 0) {
                                     // For Dragos Bat Skill (1100), we delay cooldown start until OneSkillFinish (handled in 31401)
                                     if (skillId === 1100 || String(skillId) === "1100") {
                                         console.log(`[🔋 DEFERRED COOLDOWN] Skill 1100 cast by ${playerId}, deferring cooldown start...`);
                                     } else {
                                         socket.write(skillUpdatePacket);
                                         console.log(`[🔋 STARTING COOLDOWN]: ${skillUpdatePacket.trim()}`);
                                     }
                                 } else if (socket.nocd) {
                                     // Se noCD estiver ativo, enviamos o pacote com CD 0 e pronto para reuso imediato
                                     const zeroEnc = utils.encodeInt(0);
                                     const nocdUpdatePacket = `e${utils.encodeInt(Number(playerId))}:SkillDataUpdate,(${utils.encodeInt(31102)},((${skillIdEnc},0,${utils.encodeInt(1)},0,${utils.encodeInt(1)},${zeroEnc},0,0,0,${longtimeEnc},${zeroEnc})))\n`;
                                     socket.write(nocdUpdatePacket);
                                     console.log(`[🔋 NOCD UPDATE SENT]: ${nocdUpdatePacket.trim()}`);
                                 }
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 31103 skill cast]:", e);
                            }
                        }

                        // Se for 31104 (Skill CD End / MSG_PLAYERSKILL_CDEND)
                        if (cmdId === 31104) {
                            try {
                                const cdArgs = utils.parseArrayList(argsStr);
                                const skillId = cdArgs[0];
                                if (Number(socket.currentHeroId) === 2000 || lamsterHero.SKILL_IDS.has(Number(skillId))) {
                                    if (Number(socket.currentHeroId) === 2000) lamsterKit.skillData(socket, Number(skillId));
                                    continue;
                                }
                                const skillConfig = playerSkillsMap[skillId];
                                const maxCharges = (skillConfig && skillConfig.maxSkillNumber > 1) ? skillConfig.maxSkillNumber : 1;
                                socket.skillCharges = socket.skillCharges || {};
                                socket.skillChargeTimers = socket.skillChargeTimers || {};
                                const curCharges = socket.skillCharges[skillId] !== undefined ? socket.skillCharges[skillId] : 0;
                                const newCharges = Math.min(maxCharges, curCharges + 1);
                                socket.skillCharges[skillId] = newCharges;
                                
                                const chargesEnc = utils.encodeInt(newCharges);
                                const skillIdEnc = utils.encodeInt(skillId);
                                const one = utils.encodeInt(1);
                                
                                let nextCd = 0;
                                let actualTotalCd = skillConfig ? skillConfig.cd : 0;
                                if (newCharges < maxCharges) {
                                    nextCd = actualTotalCd;
                                    socket.skillChargeTimers[skillId] = Date.now() + actualTotalCd;
                                } else {
                                    socket.skillChargeTimers[skillId] = 0;
                                }
                                const nextCdEnc = utils.encodeInt(nextCd);
                                const actualTotalCdEnc = utils.encodeInt(actualTotalCd);

                                const skillLongTime = (skillConfig && skillConfig.longtime) ? skillConfig.longtime : 0;
                                const longtimeEnc = utils.encodeInt(skillLongTime);
                                const skillUpdatePacket = `e${utils.encodeInt(Number(playerId))}:SkillDataUpdate,(${utils.encodeInt(31102)},((${skillIdEnc},0,${chargesEnc},0,${one},${nextCdEnc},0,0,0,${longtimeEnc},${actualTotalCdEnc})))\n`;
                                socket.write(skillUpdatePacket);
                                console.log(`[🔋 SKILL CD END REFILLED]: Skill ${skillId} refilled charge ${newCharges}/${maxCharges} (Next CD: ${nextCd}ms): ${skillUpdatePacket.trim()}`);
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 31104 skill CD end]:", e);
                            }
                        }

                        // Se for 31105 (Skill Use Time Reset / MSG_PLAYERSKILL_USETIME_RESET)
                        if (cmdId === 31105) {
                            try {
                                const resetArgs = utils.parseArrayList(argsStr);
                                const skillId = resetArgs[0];
                                console.log(`[🔋 SKILL USETIME RESET]: Jogador ${playerId} finalizou duração da skill ${skillId}`);
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 31105 skill usetime reset]:", e);
                            }
                        }

                        // Se for 31208 (MSG_PLAYERSKILL_SUPPERSKILL - Ativação da Ultimate Q)
                        if (cmdId === 31208) {
                            if (Number(socket.currentHeroId) === 2000) {
                                lamsterKit.ultimate(socket);
                                continue;
                            }
                            if (socket.isDead || socket.isRagdoll) {
                                console.log(`[🚫 ULT CAST BLOCKED] Jogador ${playerId} está morto e não pode usar a Ultimate!`);
                                gameLogic.sendSystemChat(socket, "⚠️ Você está morto!");
                                return;
                            }
                            if (socket.isStunned || socket.isFrozen) {
                                console.log(`[🚫 ULT CAST BLOCKED] Jogador ${playerId} está atordoado/congelado e não pode usar a Ultimate!`);
                                gameLogic.sendSystemChat(socket, "⚠️ Você está atordoado/congelado e não pode usar a Ultimate!");
                                return;
                            }
                            if (socket.activeBuffs && socket.activeBuffs[1015]) {
                                console.log(`[🚫 ULT CAST BLOCKED] Jogador ${playerId} está no casulo e não pode usar a Ultimate!`);
                                gameLogic.sendSystemChat(socket, "⚠️ Você está no casulo e não pode usar a Ultimate!");
                                return;
                            }
                            try {
                                console.log(`[🚀 ULTIMATE START] Jogador ativou a Ultimate! Agradecendo e ativando CDazhao...`);

                                // ==========================================
                                // 💖 ANGEL / LOLITA / GUANYIN RESURRECTION ULTIMATE
                                // ==========================================
                                const currentHeroForRevive = socket.currentHeroId || 1015;
                                if (currentHeroForRevive === 1029 || currentHeroForRevive === 1019) {
                                    const reviveArgs = utils.parseArrayList(argsStr);
                                    console.log(`[💖 ANGEL REVIVE] Parsed szReliveId:`, JSON.stringify(reviveArgs));
                                    const casterForce = socket.force !== undefined ? socket.force : 1;
                                    const angelX = socket.lastX || 0;
                                    const angelY = socket.lastY || 0;
                                    const angelZ = socket.lastZ || 0;
                                    let revivedCount = 0;

                                    // 1. Ressuscitação via lista explícita szReliveId (se enviada pelo cliente)
                                    if (reviveArgs.length >= 2) {
                                        for (let ri = 0; ri < reviveArgs.length; ri += 2) {
                                            const reviveTargetId = reviveArgs[ri];
                                            const reviveCoords = reviveArgs[ri + 1]; // [x*100, y*100, z*100]
                                            
                                            if (!reviveTargetId) continue;
                                            
                                            // Busca em jogadores
                                            const targetSocket = activeSockets.find(s => 
                                                sceneScope.canReceive(socket, s) && !s.destroyed && 
                                                (s.playerId === reviveTargetId || String(s.playerId) === String(reviveTargetId) ||
                                                 s.playerIndex === reviveTargetId || String(s.playerIndex) === String(reviveTargetId))
                                            );
                                            
                                            if (targetSocket && targetSocket.isDead) {
                                                const maxHp = targetSocket.maxHp || 2000;
                                                targetSocket.isDead = false;
                                                if (targetSocket.respawnTimeout) {
                                                    clearTimeout(targetSocket.respawnTimeout);
                                                    targetSocket.respawnTimeout = null;
                                                }
                                                targetSocket.hp = maxHp;
                                                targetSocket.shield = 0;
                                                if (Array.isArray(reviveCoords) && reviveCoords.length >= 3) {
                                                    targetSocket.lastX = Math.round(reviveCoords[0] / 100);
                                                    targetSocket.lastY = Math.round(reviveCoords[1] / 100);
                                                    targetSocket.lastZ = Math.round(reviveCoords[2] / 100);
                                                }
                                                const targetPlayerIdEnc = utils.encodeInt(Number(targetSocket.playerIndex || targetSocket.playerId || 1));
                                                const rx = utils.encodeInt(Math.round(targetSocket.lastX * 100));
                                                const ry = utils.encodeInt(Math.round(targetSocket.lastZ * 100));
                                                const rz = utils.encodeInt(Math.round(targetSocket.lastY * 100));
                                                const revivePacket = `e${targetPlayerIdEnc}:OnRevive,((${rx},${ry},${rz},${utils.encodeInt(180)},0,0,''))\n`;
                                                activeSockets.forEach(s => {
                                                    if (sceneScope.canReceive(socket, s) && !s.destroyed) s.write(revivePacket);
                                                });
                                                gameLogic.sendEntityHpUpdate(targetSocket, targetSocket.playerId || "1", targetSocket, true);
                                                revivedCount++;
                                                console.log(`[✨ ANGEL REVIVED PLAYER] ${targetSocket.playerName || reviveTargetId} em (${targetSocket.lastX}, ${targetSocket.lastY}, ${targetSocket.lastZ})`);
                                            }

                                            // Busca em bots
                                            if (state.activeBots && state.activeBots[reviveTargetId]) {
                                                const bot = state.activeBots[reviveTargetId];
                                                if (bot && bot.isDead) {
                                                    bot.isDead = false;
                                                    bot.hp = bot.maxHp || 2000;
                                                    const botRevivePacket = `e${utils.encodeInt(Number(bot.playerIndex || reviveTargetId))}:OnRevive,((${utils.encodeInt(bot.lastX * 100 || 0)},${utils.encodeInt(bot.lastZ * 100 || 0)},${utils.encodeInt(bot.lastY * 100 || 0)},${utils.encodeInt(180)},0,0,''))\n`;
                                                    activeSockets.forEach(s => {
                                                        if (sceneScope.canReceive(socket, s) && !s.destroyed) s.write(botRevivePacket);
                                                    });
                                                    gameLogic.sendEntityHpUpdate(bot, bot.playerIndex || reviveTargetId, bot, true);
                                                    revivedCount++;
                                                    console.log(`[✨ ANGEL REVIVED BOT] Bot #${reviveTargetId}`);
                                                }
                                            }
                                        }
                                    }

                                    // 2. Ressuscitação em área de 20 metros (fallback / garantia de proximidade)
                                    activeSockets.forEach(ally => {
                                        if (ally.socketType === "SCENE" && !ally.destroyed && ally.playerId !== playerId && ally.isDead) {
                                            const allyForce = ally.force !== undefined ? ally.force : 1;
                                            if (combat.sameSide(socket, ally, Number(casterForce) === Number(allyForce))) {
                                                const dist = Math.hypot((ally.lastX || 0) - angelX, (ally.lastZ || 0) - angelZ);
                                                if (dist <= 2000) { // 20 metros
                                                    ally.isDead = false;
                                                    if (ally.respawnTimeout) {
                                                        clearTimeout(ally.respawnTimeout);
                                                        ally.respawnTimeout = null;
                                                    }
                                                    ally.hp = ally.maxHp || 2000;
                                                    ally.shield = 0;
                                                    const allyNetIdEnc = utils.encodeInt(Number(ally.playerIndex || ally.playerId || 1));
                                                    const rx = utils.encodeInt(Math.round((ally.lastX || 0) * 100));
                                                    const ry = utils.encodeInt(Math.round((ally.lastZ || 0) * 100));
                                                    const rz = utils.encodeInt(Math.round((ally.lastY || 0) * 100));
                                                    const revivePacket = `e${allyNetIdEnc}:OnRevive,((${rx},${ry},${rz},${utils.encodeInt(180)},0,0,''))\n`;
                                                    activeSockets.forEach(s => {
                                                        if (sceneScope.canReceive(socket, s) && !s.destroyed) s.write(revivePacket);
                                                    });
                                                    gameLogic.sendEntityHpUpdate(ally, ally.playerId || "1", ally, true);
                                                    revivedCount++;
                                                    console.log(`[✨ ANGEL PROXIMITY REVIVED ALLY] ${ally.playerName} (#${ally.playerId})`);
                                                }
                                            }
                                        }
                                    });

                                    if (state.activeBots) {
                                        for (const botId in state.activeBots) {
                                            const bot = state.activeBots[botId];
                                            if (bot && bot.isDead && combat.sameSide(socket, bot, Number(bot.force) === Number(casterForce))) {
                                                const dist = Math.hypot((bot.lastX || 0) - angelX, (bot.lastZ || 0) - angelZ);
                                                if (dist <= 2000) {
                                                    bot.isDead = false;
                                                    bot.hp = bot.maxHp || 2000;
                                                    const botRevivePacket = `e${utils.encodeInt(Number(bot.playerIndex || botId))}:OnRevive,((${utils.encodeInt(bot.lastX * 100 || 0)},${utils.encodeInt(bot.lastZ * 100 || 0)},${utils.encodeInt(bot.lastY * 100 || 0)},${utils.encodeInt(180)},0,0,''))\n`;
                                                    activeSockets.forEach(s => {
                                                        if (sceneScope.canReceive(socket, s) && !s.destroyed) s.write(botRevivePacket);
                                                    });
                                                    gameLogic.sendEntityHpUpdate(bot, bot.playerIndex || botId, bot, true);
                                                    revivedCount++;
                                                    console.log(`[✨ ANGEL PROXIMITY REVIVED BOT] Bot #${botId}`);
                                                }
                                            }
                                        }
                                    }

                                    if (revivedCount > 0) {
                                        gameLogic.sendGlobalSystemChat(`💖 [Angel] ${socket.playerName || 'Angel'} usou Luz Vital e ressuscitou ${revivedCount} aliado(s)!`);
                                    }
                                    
                                    // Executa animação da ultimate no caster (SuperSkillUpdate)
                                    const zeroR = utils.encodeInt(0);
                                    const ultExecuteRevive = `e${utils.encodeInt(Number(playerId))}:SuperSkillUpdate,(${zeroR})\n`;
                                    activeSockets.forEach(s => {
                                        if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                            s.write(ultExecuteRevive);
                                        }
                                    });
                                }
                                
                                const d50001 = utils.encodeInt(50001);
                                const d50005 = utils.encodeInt(50005);
                                const one = utils.encodeInt(1);
                                
                                // Determina dinamicamente a duração da Ultimate baseada no HeroID do socket e tabelas XML
                                const currentHero = socket.currentHeroId || 1015;
                                const dazhaoList = heroDazhaoMap[currentHero] || [];
                                let duration = 0;
                                if (dazhaoList.length > 0) {
                                    if (dazhaoList[0].continueTime > 0) {
                                        duration = dazhaoList[0].continueTime;
                                    }
                                    const doskill = dazhaoList[0].doskill;
                                    let skillConfig = playerSkillsMap[doskill];
                                    if (groupSkillsOverrides[doskill]) {
                                        skillConfig = Object.assign({}, skillConfig, groupSkillsOverrides[doskill]);
                                    }
                                    if (!duration && skillConfig && skillConfig.longtime > 0) {
                                        duration = skillConfig.longtime;
                                    }
                                    if (!duration && skillConfig && Array.isArray(skillConfig.buff_idx_me) && skillConfig.buff_idx_me.length > 0) {
                                        const firstBuff = buffsMap[skillConfig.buff_idx_me[0]];
                                        if (firstBuff && firstBuff.time > 0) {
                                            duration = firstBuff.time;
                                        }
                                    }
                                }
                                if (!duration || duration <= 0) {
                                    duration = 8000; // Fallback oficial
                                }
                                console.log(`[🚀 ULTIMATE CAST] Hero: ${currentHero} | Duração oficial calculada: ${duration}ms`);
                                 // Reseta energia da Ultimate para 0 ao conjurar
                                 socket.ultEnergy = 0;
                                 const p50001_cast = utils.encodeInt(50001);
                                 const p50004_cast = utils.encodeInt(50004);
                                 const zero_cast = utils.encodeInt(0);
                                 socket.write(`e${utils.encodeInt(Number(socket.playerId))}:SendMsgToAllClient,(${p50001_cast},(${p50004_cast},(${zero_cast},${zero_cast},${zero_cast})))\n`);

                                    // ==========================================
                                 // 🚀 GERENCIADOR DINÂMICO DE BUFFS DE ULTIMATE (dazhao)
                                 // ==========================================
                                 if (dazhaoList.length > 0) {
                                     const doskill = dazhaoList[0].doskill;
                                     let skillConfig = playerSkillsMap[doskill];
                                     if (groupSkillsOverrides[doskill]) {
                                         skillConfig = Object.assign({}, skillConfig, groupSkillsOverrides[doskill]);
                                     }
                                     if (skillConfig && Array.isArray(skillConfig.buff_idx_me) && skillConfig.buff_idx_me.length > 0) {
                                         socket.activeBuffs = socket.activeBuffs || {};
                                         socket.activeUltimateBuffs = socket.activeUltimateBuffs || [];

                                         skillConfig.buff_idx_me.forEach(buffId => {
                                             if (!socket.activeUltimateBuffs.includes(buffId)) {
                                                 socket.activeUltimateBuffs.push(buffId);
                                             }
                                             gameLogic.applyBuffToEntity(socket, playerId, buffId, duration);
                                         });
                                     }
                                     

                                      // 🎯 ALICE (1013) — WALLHACK GLOBAL
                                      if (doskill === 1013) {
                                          console.log(`[🎯 ALICE ULTIMATE ACTIVE] Applying Wallhack (Buff 1005) to all enemies for ${duration}ms!`);
                                          const casterForce = socket.force !== undefined ? socket.force : 1;
                                          activeSockets.forEach(s => {
                                              if (sceneScope.canReceive(socket, s) && !s.destroyed && s.playerId !== playerId) {
                                                  const enemyForce = s.force !== undefined ? s.force : 1;
                                                  if (!combat.sameSide(socket, s, Number(casterForce) === Number(enemyForce))) {
                                                      gameLogic.applyBuffToEntity(socket, s.playerId, 1005, duration);
                                                  }
                                              }
                                          });
                                          for (const npcId in spawnedNpcs) {
                                              const npc = spawnedNpcs[npcId];
                                              if (npc && !npc.destroyed) {
                                                  const enemyForce = npc.force !== undefined ? npc.force : 2;
                                                  if (!combat.sameSide(socket, npc, Number(casterForce) === Number(enemyForce))) {
                                                      gameLogic.applyBuffToEntity(socket, npcId, 1005, duration);
                                                  }
                                              }
                                          }
                                      }

                                       // 🔮 CARMEN / TALIA (1021) — FATE (AURA DE CURA EM ÁREA DE 9 METROS - 140 HP/0.2s = 700 HP/s)
                                       if (currentHero === 1021 || doskill === 1063) {
                                           const tickInterval = 200; // 0.2 segundos
                                           const healAmount = 140; // 140 HP por tick
                                           const carmenForce = socket.force !== undefined ? socket.force : 1;
                                           let ticks = 0;
                                           const maxTicks = Math.floor(duration / tickInterval) || 30;
                                           console.log(`[🔮 CARMEN ULTIMATE - FATE] Iniciando aura de cura de ${healAmount} HP a cada ${tickInterval}ms (700 HP/s) em raio de 9m por ${duration}ms (${maxTicks} ticks)`);
                                           const healInterval = setInterval(() => {
                                               if (socket.destroyed || socket.isDead || !socket.ultimateTimer || ticks >= maxTicks) {
                                                   clearInterval(healInterval);
                                                   return;
                                               }
                                               ticks++;
                                               const carmenX = socket.lastX || 0;
                                               const carmenY = socket.lastY || 0;
                                               const carmenZ = socket.lastZ || 0;

                                               // Curar a própria Carmen
                                               if (socket.hp < (socket.maxHp || 2000)) {
                                                   socket.hp = Math.min(socket.maxHp || 2000, (socket.hp || 0) + healAmount);
                                                   gameLogic.sendEntityHpUpdate(socket, socket.playerId || playerId, socket, true);
                                                   if (ticks % 5 === 0) {
                                                       console.log(`[🔮 CARMEN ULTIMATE HEAL] Curou a si mesma (+${healAmount * 5} HP/s). HP atual: ${socket.hp}`);
                                                   }
                                               }

                                               // Curar aliados próximos no raio de 9 metros (900 unidades)
                                               activeSockets.forEach(ally => {
                                                   if (ally.socketType === "SCENE" && !ally.destroyed && !ally.isDead && combat.sameSide(socket, ally, Number(ally.force) === Number(carmenForce)) && ally !== socket && ally.playerId !== playerId) {
                                                       const dist = Math.hypot((ally.lastX || 0) - carmenX, (ally.lastZ || 0) - carmenZ);
                                                       if (dist <= 900) {
                                                           ally.hp = Math.min(ally.maxHp || 2000, (ally.hp || 0) + healAmount);
                                                           gameLogic.sendEntityHpUpdate(ally, ally.playerId, ally, true);
                                                           if (ticks % 5 === 0) {
                                                               console.log(`[🔮 CARMEN ULTIMATE HEAL] Curou aliado ${ally.playerId} (+${healAmount * 5} HP/s). HP atual: ${ally.hp}`);
                                                           }
                                                       }
                                                   }
                                               });

                                               if (state.activeBots) {
                                                   for (const bId in state.activeBots) {
                                                       const b = state.activeBots[bId];
                                                       if (b && !b.isDead && combat.sameSide(socket, b, Number(b.force) === Number(carmenForce))) {
                                                           const dist = Math.hypot((b.lastX || 0) - carmenX, (b.lastZ || 0) - carmenZ);
                                                           if (dist <= 900) {
                                                               if (b.hp < (b.maxHp || 2000)) {
                                                                   b.hp = Math.min(b.maxHp || 2000, (b.hp || 0) + healAmount);
                                                                   gameLogic.sendEntityHpUpdate(b, b.playerIndex || bId, b, true);
                                                               }
                                                           }
                                                       }
                                                   }
                                               }
                                           }, tickInterval);
                                       }

                                       // ⛓️ LILY / LILITH (1033) — SOUL CHAINS (SILÊNCIO 1041 & DANO CONTÍNUO 1042 - 11 TICKS DE 40 DANO)
                                       if (currentHero === 1033 || doskill === 1164) {
                                           console.log(`[⛓️ LILY ULTIMATE - SOUL CHAINS] Disparou correntes da alma (Buff 1041 Silêncio + Buff 1042 DoT)!`);
                                           const lilyForceNum = (socket.force !== undefined && socket.force !== null) ? Number(socket.force) : 1;
                                           const lilyX = socket.lastX || 0;
                                           const lilyZ = socket.lastZ || 0;

                                           // Aplica APENAS em inimigos (nunca na própria Lily nem em aliados)
                                           activeSockets.forEach(enemy => {
                                               if (enemy.socketType === "SCENE" && !enemy.destroyed && !enemy.isDead && enemy !== socket && enemy.playerId !== playerId) {
                                                   const enemyForceNum = (enemy.force !== undefined && enemy.force !== null) ? Number(enemy.force) : 2;
                                                   if (!combat.sameSide(socket, enemy, enemyForceNum === lilyForceNum)) {
                                                       const dist = Math.hypot((enemy.lastX || 0) - lilyX, (enemy.lastZ || 0) - lilyZ);
                                                       if (dist <= 1500) {
                                                           // Aplica Silêncio (1041) por 5000ms com visual de correntes da alma
                                                           gameLogic.applyBuffToEntity(socket, enemy.playerId, 1041, 5000);
                                                           // Aplica DoT Veneno Tóxico (1042 - 40 dano a cada 500ms por 5000ms = 11 ticks)
                                                           gameLogic.applyBuffToEntity(socket, enemy.playerId, 1042, 5000);
                                                           console.log(`[⛓️ LILY ULTIMATE HIT] Correntes da alma e DoT aplicados ao jogador inimigo ${enemy.playerId}!`);
                                                       }
                                                   }
                                               }
                                           });

                                           if (state.activeBots) {
                                               for (const bId in state.activeBots) {
                                                   const bot = state.activeBots[bId];
                                                   if (bot && !bot.isDead) {
                                                       const botForceNum = (bot.force !== undefined && bot.force !== null) ? Number(bot.force) : 2;
                                                       if (!combat.sameSide(socket, bot, botForceNum === lilyForceNum)) {
                                                           const dist = Math.hypot((bot.lastX || 0) - lilyX, (bot.lastZ || 0) - lilyZ);
                                                           if (dist <= 1500) {
                                                               gameLogic.applyBuffToEntity(socket, bot.playerIndex || bId, 1041, 5000);
                                                               gameLogic.applyBuffToEntity(socket, bot.playerIndex || bId, 1042, 5000);
                                                               console.log(`[⛓️ LILY ULTIMATE HIT] Correntes da alma e DoT aplicados ao bot inimigo ${bot.playerIndex || bId}!`);
                                                           }
                                                       }
                                                   }
                                               }
                                           }
                                       }

                                     // 🛡️ KHAN (1034) — IRON (IMORTALIDADE)
                                     if (currentHero === 1034 || doskill === 1173) {
                                         socket.isKhanUltActive = true;
                                         console.log(`[🛡️ KHAN ULTIMATE - IRON] Ativou Imortalidade por ${duration}ms! HP não cairá abaixo de 1.`);
                                     }

                                      // ⚡ NEZHA (1035) — GREAT SPIRIT / SUPERMAN POWERS
                                      if (currentHero === 1035 || doskill === 1183) {
                                          socket.isNezhaUltActive = true;
                                          console.log(`[⚡ NEZHA ULTIMATE - GREAT SPIRIT] Ativou modo Superman por ${duration}ms (+50% Dano, -40% Dano Recebido)!`);
                                      }

                                      // 🦊 VENUS / DAJI (1032) — MASS SHIELD / FOX PHANTOM (Skill 1153)
                                      if (currentHero === 1032 || doskill === 1153) {
                                          socket.isVenusUltActive = true;
                                          const shieldDur = 5000;
                                          gameLogic.applyBuffToEntity(socket, playerId, 1047, shieldDur);
                                          const venusForce = Number(socket.force !== undefined ? socket.force : 1);
                                          activeSockets.forEach(ally => {
                                              if (ally.socketType === "SCENE" && !ally.destroyed && !ally.isDead && ally.playerId !== playerId) {
                                                  if (combat.sameSide(socket, ally, Number(ally.force) === venusForce)) {
                                                      gameLogic.applyBuffToEntity(socket, ally.playerId, 1047, shieldDur);
                                                  }
                                              }
                                          });
                                          console.log(`[🦊 VENUS ULTIMATE - FOX PHANTOM] Aplicou 4000 de Escudo e Imunidade a CC (Buff 1047) em toda a equipe!`);
                                      }

                                       // 🧛 DRAGOS (1026) — VAMPIRE BLOOD RAGE (LIFESTEAL NA ULTIMATE)
                                       if (currentHero === 1026 || doskill === 1101) {
                                           socket.isDragosUltActive = true;
                                           console.log(`[🧛 DRAGOS ULTIMATE - BLOOD RAGE] Ativou modo Vampiro por ${duration}ms (+50% Velocidade e Lifesteal / Roubo de Vida)!`);
                                       }

                                      // 🤺 VINCENT (1030) — DUEL (RESET DE COOLDOWNS E +50% DANO GERAL)
                                      if (currentHero === 1030 || doskill === 1133) {
                                          socket.isVincentUltActive = true;
                                          socket.skillCharges = socket.skillCharges || {};
                                          socket.skillChargeTimers = socket.skillChargeTimers || {};
                                          socket.skillCharges[1131] = 3; // Refill 3 cargas de Shift
                                          socket.skillCharges[1132] = 1;
                                          socket.skillChargeTimers[1131] = 0;
                                          socket.skillChargeTimers[1132] = 0;

                                          // Envia MSG_PLAYERSKILL_INIT (31101) para forçar o cliente Unity a limpar todos os CDs e reconstruir a UI instantaneamente com 3 cargas
                                          const s31101 = utils.encodeInt(31101);
                                          const fullSkillsData = db.generateHeroSkillsData(1030);
                                          const resetSkillsPacket = `e${utils.encodeInt(Number(playerId))}:SkillDataUpdate,(${s31101},${fullSkillsData})\n`;
                                          socket.write(resetSkillsPacket);
                                          console.log(`[🤺 VINCENT ULTIMATE - DUEL] Ativou Duel por ${duration}ms! Resetou cooldowns e restaurou 3 cargas de Shift via 31101: ${resetSkillsPacket.trim()}`);
                                      }
                                 
                                       // 🌀 DAVID (1037) — TELEPORT EM GRUPO / TELEPORT PANEL (Skill 1203 -> 2080001 DAWEI_CAR_BLACK)
                                        if (currentHero === 1037 || doskill === 1203) {
                                            try {
                                                const parsedArgs = utils.parseArrayList(argsStr);
                                                if (Array.isArray(parsedArgs) && parsedArgs.length >= 3) {
                                                    const destX = Number(parsedArgs[0]);
                                                    const destY = Number(parsedArgs[1]);
                                                    const destZ = Number(parsedArgs[2]);
                                                    const davidForce = Number(socket.force !== undefined ? socket.force : 1);
                                                    
                                                    console.log(`[🌀 DAVID TELEPORT PORTAL OPENED] David ${playerId} abriu portal em (${destX}, ${destY}, ${destZ}) por ${duration}ms para time ${davidForce}!`);
                                                    
                                                    // Constrói lista de NetIDs dos aliados da equipe
                                                    const allyIds = [];
                                                    activeSockets.forEach(ally => {
                                                        if (ally.socketType === "SCENE" && !ally.destroyed && !ally.isDead && ally.playerId !== playerId) {
                                                            const allyForce = Number(ally.force !== undefined ? ally.force : 1);
                                                            if (combat.sameSide(socket, ally, allyForce === davidForce)) {
                                                                allyIds.push(Number(ally.playerIndex || ally.playerId));
                                                            }
                                                        }
                                                    });
                                                    
                                                    if (allyIds.length > 0) {
                                                        const allyListEnc = allyIds.map(id => utils.encodeInt(id)).join(',');
                                                        const promptPacket = `e${utils.encodeInt(Number(playerId))}:SendMsgToAllClient,(${utils.encodeInt(2080001)},((${allyListEnc}),(${utils.encodeInt(destX)},${utils.encodeInt(destY)},${utils.encodeInt(destZ)}),${utils.encodeInt(playerId)},${utils.encodeInt(5)}))\n`;
                                                        
                                                        activeSockets.forEach(s => {
                                                            if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                                                s.write(promptPacket);
                                                            }
                                                        });
                                                        console.log(`[🌀 DAVID TELEPORT PROMPT BROADCAST] Pacote 2080001 enviado via SendMsgToAllClient para aliados: ${allyIds.join(', ')}`);
                                                    }
                                                }
                                            } catch (e) {
                                                console.error("[⚠️ ERRO ao processar David Teleport]:", e);
                                            }
                                        }
}
                                
                                const durationEnc = utils.encodeInt(duration); 
                                
                                // 1. Mostra a barra de duração do Dazhao no cliente de todos
                                const ultStartPacket = `e${utils.encodeInt(Number(playerId))}:SendMsgToAllClient,(${d50001},(${d50005},(${one},'',${durationEnc})))\n`;
                                activeSockets.forEach(s => {
                                    if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                        s.write(ultStartPacket);
                                    }
                                });
                                
                                // 2. Aciona a execução da Ultimate (SuperSkillUpdate) no cliente para animar e castar a habilidade!
                                const zero = utils.encodeInt(0);
                                const ultExecutePacket = `e${utils.encodeInt(Number(playerId))}:SuperSkillUpdate,(${zero})\n`;
                                activeSockets.forEach(s => {
                                    if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                        s.write(ultExecutePacket);
                                    }
                                });

                                 
                                if (socket.ultimateTimer) {
                                    clearTimeout(socket.ultimateTimer);
                                }
                                socket.ultimateTimer = setTimeout(() => {
                                    socket.isKhanUltActive = false;
                                    socket.isNezhaUltActive = false;
                                     socket.isDragosUltActive = false;
                                socket.isVincentUltActive = false;
                                    socket.isVincentUltActive = false;
                                    if (!socket.destroyed) {
                                        const ultEndPacket = `e${utils.encodeInt(Number(playerId))}:SendMsgToAllClient,(${d50001},(${d50005},(${zero},'',${zero})))
`;
                                        activeSockets.forEach(s => {
                                            if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                                s.write(ultEndPacket);
                                            }
                                        });
                                        console.log(`[💀 ULTIMATE TIMER EXPIRED] Sending: ${ultEndPacket.trim()}`);

                                        // Se for Jack, libera todos os inimigos puxados
                                        if (socket.jackPulledEnemies && socket.jackPulledEnemies.size > 0) {
                                            const p2030002 = utils.encodeInt(2030002);
                                            const jackIdEnc = utils.encodeInt(Number(socket.playerIndex || playerId || 1));
                                            const allPulled = Array.from(socket.jackPulledEnemies);
                                            const encReleaseIds = allPulled.map(id => utils.encodeInt(id)).join(",");
                                            const endPullPacket = `e${utils.encodeInt(Number(playerId))}:SendMsgToAllClient,(${p2030002},('end',${jackIdEnc},(${encReleaseIds})))
`;
                                            activeSockets.forEach(s => {
                                                if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                                    s.write(endPullPacket);
                                                }
                                            });
                                            console.log(`[🌪️ JACK ULTIMATE PULL EXPIRED RELEASE]: ${endPullPacket.trim()}`);
                                            socket.jackPulledEnemies.clear();
                                        }
                                        if (socket.jackPullReleaseTimer) {
                                            clearTimeout(socket.jackPullReleaseTimer);
                                            socket.jackPullReleaseTimer = null;
                                        }
                                    }
                                    socket.ultimateTimer = null;
                                }, duration);
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 31208 Ultimate Start]:", e);
                            }
                        }

                        // Se for 31211 (MSG_PLAYERSKILL_SUPPERSKILLEND - Fim da Ultimate)
                        if (cmdId === 31211) {
                            if (Number(socket.currentHeroId) === 2000) {
                                // Cosmetic interruptions may end the Q ring early, but
                                // they cannot reset its charge or grant another Q.
                                lamsterKit.endUltimate(socket);
                                continue;
                            }
                            try {
                                console.log(`[💀 ULTIMATE END] Ultimate terminou! Desativando CDazhao flag...`);
                                socket.isKhanUltActive = false;
                                socket.isNezhaUltActive = false;
                                if (socket.ultimateTimer) {
                                    clearTimeout(socket.ultimateTimer);
                                    socket.ultimateTimer = null;
                                }
                                
                                // Cancela dinamicamente todos os buffs associados à Ultimate ativa
                                if (socket.activeUltimateBuffs && socket.activeUltimateBuffs.length > 0) {
                                    socket.activeUltimateBuffs.forEach(buffId => {
                                        gameLogic.cleanupBuff(socket, playerId, buffId, true);
                                    });
                                    socket.activeUltimateBuffs = [];
                                }
                                const d50001 = utils.encodeInt(50001);
                                const d50005 = utils.encodeInt(50005);
                                const zero = utils.encodeInt(0);
                                
                                // CORREÇÃO DEFINITIVA: Usamos SendMsgToAllClient no lugar de SendMsgToClient, e tornamos o e1 dinâmico para e${playerId}
                                const ultEndPacket = `e${utils.encodeInt(Number(playerId))}:SendMsgToAllClient,(${d50001},(${d50005},(${zero},'',${zero})))
`;
                                activeSockets.forEach(s => {
                                    if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                        s.write(ultEndPacket);
                                    }
                                });
                                console.log(`[✅ SERVER BROADCASTED ULTIMATE END]: ${ultEndPacket.trim()}`);

                                // Se for Jack, garante a liberação de todos os inimigos puxados
                                if (socket.jackPulledEnemies && socket.jackPulledEnemies.size > 0) {
                                    const p2030002 = utils.encodeInt(2030002);
                                    const jackIdEnc = utils.encodeInt(Number(socket.playerIndex || playerId || 1));
                                    const allPulled = Array.from(socket.jackPulledEnemies);
                                    const encReleaseIds = allPulled.map(id => utils.encodeInt(id)).join(",");
                                    const endPullPacket = `e${utils.encodeInt(Number(playerId))}:SendMsgToAllClient,(${p2030002},('end',${jackIdEnc},(${encReleaseIds})))
`;
                                    activeSockets.forEach(s => {
                                        if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                            s.write(endPullPacket);
                                        }
                                    });
                                    console.log(`[🌪️ JACK ULTIMATE PULL END RELEASE 31211]: ${endPullPacket.trim()}`);
                                    socket.jackPulledEnemies.clear();
                                }
                                if (socket.jackPullReleaseTimer) {
                                    clearTimeout(socket.jackPullReleaseTimer);
                                    socket.jackPullReleaseTimer = null;
                                }
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 31211 Ultimate End]:", e);
                            }
                        }

                        // Se for 2010001 (XingTian Shield State / MSG_SHIELD_STATE)
                        if (cmdId === 2010001) {
                            try {
                                const parsed = utils.parseArrayList(argsStr);
                                const state = Number(parsed[0]);
                                socket.tagaloaShieldActive = (state === 1);
                                if (state === 1) {
                                    if (socket.tagaloaShieldHp === undefined) {
                                        socket.tagaloaShieldHp = 8000;
                                    }
                                    console.log(`[🛡️ TAGALOA SHIELD ACTIVATE] Player ${playerId} activated physical shield with HP ${socket.tagaloaShieldHp}/8000`);
                                } else {
                                    console.log(`[🛡️ TAGALOA SHIELD DEACTIVATE] Player ${playerId} deactivated physical shield.`);
                                }
                                
                                // Tagaloa possui barreira frontal física, NÃO escudo de vida pessoal!
                                socket.shield = 0;
                                socket.maxShield = 0;
                                gameLogic.sendEntityHpUpdate(socket, playerId, socket, true);
                                
                                const p2010000 = utils.encodeInt(2010000);
                                const p2010002 = utils.encodeInt(2010002);
                                const stateEnc = utils.encodeInt(state);
                                const currentShieldHp = Math.round(socket.tagaloaShieldHp !== undefined ? socket.tagaloaShieldHp : 8000);
                                const energyEnc = utils.encodeInt(currentShieldHp);
                                const maxEnergyEnc = utils.encodeInt(8000);
                                const brPlayerIdEnc = utils.encodeInt(Number(socket.playerIndex || playerId || 1));
                                
                                // 1. Envia para o próprio jogador via SendMsgToClient (necessário para atualizar o Animator SheildState e exibir a barreira!)
                                const respSelf = `e${utils.encodeInt(Number(playerId))}:SendMsgToClient,(${p2010000},(${p2010002},(${stateEnc},${energyEnc},${maxEnergyEnc})))\n`;
                                socket.write(respSelf);
                                console.log(`[🛡️ TAGALOA SHIELD SYNC SELF]: ${respSelf.trim()}`);
                                
                                // 2. Broadcast de animação para os outros jogadores (JvDun + SheildState)
                                const isShieldUp = (state === 1) ? 1 : 0;
                                const animStateVal = (state === 1) ? 1 : 0;
                                const animJvDun = `e${utils.encodeInt(Number(playerId))}:CA,((('JvDun',0,${isShieldUp}),1,${brPlayerIdEnc}))\n`;
                                const animSheildState = `e${utils.encodeInt(Number(playerId))}:CA,((('SheildState',2,${animStateVal}),1,${brPlayerIdEnc}))\n`;
                                
                                activeSockets.forEach(s => {
                                    if (sceneScope.canReceive(socket, s) && !s.destroyed && s !== socket) {
                                        s.write(animJvDun);
                                        s.write(animSheildState);
                                    }
                                });
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 2010001 XingTian Shield]:", e);
                            }
                        }



                        // Se for 2030001 (SHAWANG_SANDSTORM_DASH - Jack Dash Stun & Impact Damage)
                        if (cmdId === 2030001) {
                            try {
                                const parsed = utils.parseArrayList(argsStr);
                                const targetId = Number(parsed[0]);
                                console.log(`[🌪️ JACK DASH HIT] Jack ${playerId} colidiu o Dash no alvo ${targetId}! Aplicando Stun de 1.5s e dano.`);
                                if (targetId) {
                                    // 1. Aplica Buff 1024 (Stun / Atordoamento por 1500ms)
                                    gameLogic.applyBuffToEntity(socket, targetId, 1024, 1500);
                                    
                                    // 2. Causa dano de impacto (80 de dano)
                                    let targetEntity = activeSockets.find(s => Number(s.playerId) === Number(targetId) || Number(s.playerIndex) === Number(targetId));
                                    if (!targetEntity && state.activeBots && state.activeBots[targetId]) {
                                        targetEntity = state.activeBots[targetId];
                                    }
                                    if (targetEntity && !targetEntity.isDead && !targetEntity.godMode && !targetEntity.isInvincible) {
                                        const dashDmg = 80;
                                        targetEntity.hp = Math.max(0, (targetEntity.hp !== undefined ? targetEntity.hp : (targetEntity.maxHp || 2000)) - dashDmg);
                                        gameLogic.sendEntityDamageUpdate(targetId, targetEntity, dashDmg, playerId, 1, 1027);
                                        if (targetEntity.hp <= 0) {
                                            targetEntity.isDead = true;
                                            gameLogic.handleEntityDeath(targetEntity, targetId, socket, playerId, 1027);
                                        }
                                    }
                                }
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 2030001 Jack Dash]:", e);
                            }
                        }

                        // Se for 2030003 (SHAWANG_SUPERPULL - Jack Ultimate Vortex Gravity Pull)
                        if (cmdId === 2030003) {
                            try {
                                const parsed = utils.parseArrayList(argsStr);
                                const flattenArray = (arr) => {
                                    if (!Array.isArray(arr)) return [arr];
                                    return arr.reduce((acc, val) => acc.concat(Array.isArray(val) ? flattenArray(val) : val), []);
                                };
                                const hitIds = flattenArray(parsed).map(id => Number(id)).filter(id => id > 0);
                                if (hitIds.length > 0) {
                                    socket.jackPulledEnemies = socket.jackPulledEnemies || new Set();
                                    hitIds.forEach(id => socket.jackPulledEnemies.add(id));

                                    console.log(`[🌪️ JACK ULTIMATE PULL] Jack ${playerId} puxando inimigos para o vórtice:`, hitIds);
                                    const p2030002 = utils.encodeInt(2030002);
                                    const jackIdEnc = utils.encodeInt(Number(socket.playerIndex || playerId || 1));
                                    const encodedHitIds = hitIds.map(id => utils.encodeInt(id)).join(",");
                                    
                                    // Broadcast para todos os clientes executarem HitByShawangSuper("start", jackId, hitIds)
                                    const pullPacket = `e${utils.encodeInt(Number(playerId))}:SendMsgToAllClient,(${p2030002},('start',${jackIdEnc},(${encodedHitIds})))\n`;
                                    activeSockets.forEach(s => {
                                        if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                            s.write(pullPacket);
                                        }
                                    });
                                    console.log(`[🌪️ JACK ULTIMATE PULL START BROADCAST]: ${pullPacket.trim()}`);

                                    // Timeout de segurança para soltar os inimigos puxados após 4.5s
                                    if (socket.jackPullReleaseTimer) clearTimeout(socket.jackPullReleaseTimer);
                                    socket.jackPullReleaseTimer = setTimeout(() => {
                                        if (socket.jackPulledEnemies && socket.jackPulledEnemies.size > 0) {
                                            const allPulled = Array.from(socket.jackPulledEnemies);
                                            const encReleaseIds = allPulled.map(id => utils.encodeInt(id)).join(",");
                                            const endPullPacket = `e${utils.encodeInt(Number(playerId))}:SendMsgToAllClient,(${p2030002},('end',${jackIdEnc},(${encReleaseIds})))\n`;
                                            activeSockets.forEach(s => {
                                                if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                                    s.write(endPullPacket);
                                                }
                                            });
                                            console.log(`[🌪️ JACK ULTIMATE PULL AUTO-RELEASE]: ${endPullPacket.trim()}`);
                                            socket.jackPulledEnemies.clear();
                                        }
                                        socket.jackPullReleaseTimer = null;
                                    }, 4500);
                                }
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 2030003 Jack Super Pull]:", e);
                            }
                        }

                        // Se for 2070001 (LUOLI_ADDBLOOD_HUMAN - Angel Circle Aura targets update)
                         if (cmdId === 2070001) {
                             try {
                                 const parsed = utils.parseArrayList(argsStr);
                                 const flattenArray = (arr) => {
                                     if (!Array.isArray(arr)) return [arr];
                                     return arr.reduce((acc, val) => acc.concat(Array.isArray(val) ? flattenArray(val) : val), []);
                                 };
                                 socket.angelAuraTargets = flattenArray(parsed).map(id => Number(id) || String(id));
                                 console.log(`[💖 ANGEL AURA UPDATE] Player ${playerId} set aura targets:`, socket.angelAuraTargets);
                             } catch (e) {
                                 console.error("[⚠️ ERRO ao processar 2070001 Angel Aura]:", e);
                             }
                         }

                        // Se for 2100001 (DaJi Shield / MSG_DAJI_SHIELD)
                        // A barreira da Venus é uma ENTIDADE SEPARADA - NÃO afeta o HP do jogador
                        if (cmdId === 2100001) {
                            try {
                                const parsed = utils.parseArrayList(argsStr);
                                const type = Number(parsed[0]);
                                const pos = parsed[1] || [0, 0, 0];
                                const relesPos = parsed[2] || [0, 0, 0];

                                if (type === 1) {
                                    socket.venusBarrierHp = 4000;
                                    socket.venusBarrierMaxHp = 4000;
                                    socket.venusBarrierActive = true;
                                    socket.venusBarrierPos = pos;
                                    socket.venusBarrierRelesPos = relesPos;
                                    console.log(`[🦊 VENUS BARREIRA ATIVADA] Player ${playerId} - HP da barreira: 4000 | Pos: ${pos} | RelesPos: ${relesPos}`);
                                } else {
                                    socket.venusBarrierHp = 0;
                                    socket.venusBarrierMaxHp = 0;
                                    socket.venusBarrierActive = false;
                                    console.log(`[🦊 VENUS BARREIRA DESATIVADA] Player ${playerId}`);
                                }
                                
                                // Broadcast visual do escudo para todos (2010002) - com pos e relesPos para que apareça para os inimigos!
                                const p2010002 = utils.encodeInt(2010002);
                                const playerIdEnc = utils.encodeInt(playerId);
                                const typeEnc = utils.encodeInt(type);
                                const energyEnc = utils.encodeInt(socket.venusBarrierHp || 0);
                                const maxEnergyEnc = utils.encodeInt(4000);
                                
                                const curPos = socket.venusBarrierPos || pos || [0, 0, 0];
                                const curReles = socket.venusBarrierRelesPos || relesPos || [0, 0, 0];
                                const posEnc = `(${utils.encodeInt(curPos[0])},${utils.encodeInt(curPos[1])},${utils.encodeInt(curPos[2])})`;
                                const relesPosEnc = `(${utils.encodeInt(curReles[0])},${utils.encodeInt(curReles[1])},${utils.encodeInt(curReles[2])})`;
                                
                                const resp = `e${utils.encodeInt(Number(playerId))}:SendMsgToAllClient,(${p2010002},(${playerIdEnc},(${typeEnc},${energyEnc},${maxEnergyEnc}),${posEnc},${relesPosEnc}))\n`;
                                activeSockets.forEach(s => {
                                    if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                        s.write(resp);
                                    }
                                });
                                console.log(`[🦊 VENUS BARREIRA SYNC]: ${resp.trim()}`);
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 2100001 DaJi Shield]:", e);
                            }
                        }

                        // ==========================================
                        // 🛡️ SHIELD / EFFECT OBJECT PHYSICAL COLLIDER HIT (41001 - SVR_EFFOBJ_ADD)
                        // ==========================================
                        if (cmdId === 41001) {
                            try {
                                const parsed = utils.parseArrayList(argsStr);
                                const casterNetId = Number(parsed[0]);
                                const targetNetId = Number(parsed[1]);
                                const buffId = Number(parsed[2]);
                                
                                console.log(`[🛡️ PHYSICAL COLLIDER 41001] Caster ${casterNetId} atingiu Alvo ${targetNetId} (Buff: ${buffId})`);
                                
                                // Buff 1048 (Colisor Físico do Escudo da Lily - Unity OnTriggerEnter)
                                if (buffId === 1048) {
                                    const casterForce = socket.force !== undefined ? socket.force : 1;
                                    
                                    // 1. Verifica se o alvo é um Jogador
                                    let targetSocket = null;
                                    activeSockets.forEach(s => {
                                        if (sceneScope.canReceive(socket, s) && !s.destroyed && (Number(s.playerId) === targetNetId || s.playerIndex === targetNetId)) {
                                            targetSocket = s;
                                        }
                                    });
                                    
                                    if (targetSocket && !targetSocket.isDead) {
                                        const isEnemy = !combat.sameSide(socket, targetSocket, Number(targetSocket.force) === Number(casterForce));
                                        if (isEnemy) {
                                            const dmg = 5; // Dano de colisão do escudo = 5 HP
                                            targetSocket.hp = Math.max(0, (targetSocket.hp !== undefined ? targetSocket.hp : (targetSocket.maxHp || 2000)) - dmg);
                                            gameLogic.sendEntityDamageUpdate(targetSocket.playerId, targetSocket, dmg, playerId, 0, 1033);
                                            gameLogic.sendEntityHpUpdate(targetSocket, targetSocket.playerId, targetSocket, true);
                                            console.log(`[🛡️⚡ LILY SHIELD COLLIDER DAMAGE] Escudo da Lily atingiu Inimigo ${targetSocket.playerId} causando ${dmg} de dano! HP: ${targetSocket.hp}`);
                                            if (targetSocket.hp <= 0) {
                                                targetSocket.isDead = true;
                                                gameLogic.handleEntityDeath(targetSocket, targetSocket.playerId, socket, playerId, 1033);
                                            }
                                        } else {
                                            const heal = 300;
                                            const maxH = targetSocket.maxHp || 2000;
                                            targetSocket.hp = Math.min(maxH, (targetSocket.hp !== undefined ? targetSocket.hp : maxH) + heal);
                                            gameLogic.sendEntityHpUpdate(targetSocket, targetSocket.playerId, targetSocket, true);
                                            console.log(`[🛡️💚 LILY SHIELD COLLIDER HEAL] Escudo da Lily atingiu Aliado ${targetSocket.playerId} curando +${heal} HP! HP: ${targetSocket.hp}/${maxH}`);
                                        }
                                    }
                                    
                                    // 2. Verifica se o alvo é um Bot ativo
                                    if (state.activeBots) {
                                        for (const bId in state.activeBots) {
                                            const bot = state.activeBots[bId];
                                            if (bot && !bot.isDead && (Number(bot.playerIndex) === targetNetId || Number(bId) === targetNetId)) {
                                                const isEnemy = !combat.sameSide(socket, bot, Number(bot.force) === Number(casterForce));
                                                if (isEnemy) {
                                                    const dmg = 5; // Dano de colisão do escudo = 5 HP
                                                    bot.hp = Math.max(0, (bot.hp !== undefined ? bot.hp : (bot.maxHp || 2000)) - dmg);
                                                    gameLogic.sendEntityDamageUpdate(bot.playerIndex || bId, bot, dmg, playerId, 0, 1033);
                                                    gameLogic.sendEntityHpUpdate(bot, bot.playerIndex || bId, bot, true);
                                                    console.log(`[🛡️⚡ LILY SHIELD COLLIDER DAMAGE] Escudo da Lily atingiu Bot Inimigo ${bot.playerIndex || bId} causando ${dmg} de dano! HP: ${bot.hp}`);
                                                    if (bot.hp <= 0) {
                                                        bot.isDead = true;
                                                        gameLogic.handleEntityDeath(bot, bot.playerIndex || bId, socket, playerId, 1033);
                                                    }
                                                } else {
                                                    const heal = 300;
                                                    const maxH = bot.maxHp || 2000;
                                                    bot.hp = Math.min(maxH, (bot.hp !== undefined ? bot.hp : maxH) + heal);
                                                    gameLogic.sendEntityHpUpdate(bot, bot.playerIndex || bId, bot, true);
                                                    console.log(`[🛡️💚 LILY SHIELD COLLIDER HEAL] Escudo da Lily atingiu Bot Aliado ${bot.playerIndex || bId} curando +${heal} HP!`);
                                                }
                                            }
                                        }
                                    }
                                }
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 41001 Effect Object Add]:", e);
                            }
                        }

                        // Se for 2210002 (Lily Shield Position Sync from client)
                        if (cmdId === 2210002) {
                            try {
                                const parsed = utils.parseArrayList(argsStr);
                                const action = Number(parsed[0]);
                                const shieldId = Number(parsed[1]);
                                const pos = parsed[2] || [0, 0, 0];
                                
                                if (action === -1) {
                                    // Escudo quebrou / colidiu com obstáculo
                                    socket.lilyBarrierActive = false;
                                    socket.lilyShieldHitEnemies = null;
                                    socket.lilyShieldHealedAllies = null;
                                    if (socket.lilyShieldTimer) {
                                        clearTimeout(socket.lilyShieldTimer);
                                        socket.lilyShieldTimer = null;
                                    }
                                } else {
                                    socket.lilyBarrierActive = true;
                                    socket.lilyBarrierPos = pos;
                                    socket.lilyShieldHitEnemies = socket.lilyShieldHitEnemies || new Set();
                                    socket.lilyShieldHealedAllies = socket.lilyShieldHealedAllies || new Set();
                                    
                                    // Timer de duração máxima de 5.0 segundos para o escudo
                                    if (!socket.lilyShieldTimer) {
                                        socket.lilyShieldTimer = setTimeout(() => {
                                            if (socket.lilyBarrierActive && !socket.destroyed) {
                                                const p2210001 = utils.encodeInt(2210001);
                                                const targetIdEnc = utils.encodeInt(playerId);
                                                const shieldIdEnc = utils.encodeInt(shieldId);
                                                const zero = utils.encodeInt(0);
                                                const maxHpEnc = utils.encodeInt(1000);
                                                const dateStr = "'2026-05-18 12:00:00'";
                                                const creatorIdEnc = utils.encodeInt(playerId);
                                                const xEnc = utils.encodeInt(socket.lilyBarrierPos ? socket.lilyBarrierPos[0] : 0);
                                                const yEnc = utils.encodeInt(socket.lilyBarrierPos ? socket.lilyBarrierPos[1] : 0);
                                                const zEnc = utils.encodeInt(socket.lilyBarrierPos ? socket.lilyBarrierPos[2] : 0);
                                                const rxEnc = utils.encodeInt(0);
                                                const ryEnc = utils.encodeInt(0);
                                                const rzEnc = utils.encodeInt(0);

                                                const expirePacket = `e${utils.encodeInt(Number(playerId))}:SendMsgToAllClient,(${p2210001},(${targetIdEnc},(${shieldIdEnc},${zero},${maxHpEnc},${dateStr},${creatorIdEnc},(${xEnc},${yEnc},${zEnc}),(${rxEnc},${ryEnc},${rzEnc})))))\n`;
                                                activeSockets.forEach(s => {
                                                    if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                                        s.write(expirePacket);
                                                    }
                                                });
                                                console.log(`[🛡️💥 LILY SHIELD TIMEOUT EXPIRED] Escudo da Lily atingiu tempo máximo de 5.0s e foi destruído!`);
                                                socket.lilyBarrierActive = false;
                                                socket.lilyShieldHitEnemies = null;
                                                socket.lilyShieldHealedAllies = null;
                                            }
                                            socket.lilyShieldTimer = null;
                                        }, 5000);
                                    }

                                    const shieldX = pos[0];
                                    const shieldZ = pos[2];
                                    const lilyForce = socket.force !== undefined ? socket.force : 1;
                                    
                                    // 1. Dano em Inimigos que passam pelo escudo (320 de dano) e Cura em Aliados (+300 HP)
                                    // Raio aumentado para 650 (6.5m) para cobrir a largura total do escudo e movimentação rápida
                                    activeSockets.forEach(other => {
                                        if (sceneScope.canReceive(socket, other) && !other.isDead && other !== socket) {
                                            const isEnemy = (!combat.sameSide(socket, other, Number(other.force) === Number(lilyForce)));
                                            const isAlly = !isEnemy;
                                            const dist = Math.hypot((other.lastX || 0) - shieldX, (other.lastZ || 0) - shieldZ);
                                            
                                            if (dist <= 650) {
                                                if (isEnemy && !socket.lilyShieldHitEnemies.has(other.playerId)) {
                                                    socket.lilyShieldHitEnemies.add(other.playerId);
                                                    const dmg = 5; // Dano de colisão = 5 HP
                                                    other.hp = Math.max(0, (other.hp !== undefined ? other.hp : (other.maxHp || 2000)) - dmg);
                                                    gameLogic.sendEntityDamageUpdate(other.playerId, other, dmg, playerId, 0, 1033);
                                                    gameLogic.sendEntityHpUpdate(other, other.playerId, other, true);
                                                    console.log(`[🛡️⚡ LILY SHIELD DAMAGE] Escudo da Lily atingiu inimigo ${other.playerId} causando ${dmg} de dano! HP: ${other.hp}`);
                                                    if (other.hp <= 0) {
                                                        other.isDead = true;
                                                        gameLogic.handleEntityDeath(other, other.playerId, socket, playerId, 1033);
                                                    }
                                                } else if (isAlly && !socket.lilyShieldHealedAllies.has(other.playerId)) {
                                                    socket.lilyShieldHealedAllies.add(other.playerId);
                                                    const heal = 300;
                                                    const maxH = other.maxHp || 2000;
                                                    other.hp = Math.min(maxH, (other.hp !== undefined ? other.hp : maxH) + heal);
                                                    gameLogic.sendEntityHpUpdate(other, other.playerId, other, true);
                                                    console.log(`[🛡️💚 LILY SHIELD HEAL] Escudo da Lily curou aliado ${other.playerId} em +${heal} HP! HP: ${other.hp}/${maxH}`);
                                                }
                                            }
                                        }
                                    });
                                    
                                    // Cura a própria Lily ao passar pelo escudo (+300 HP)
                                    const distSelf = Math.hypot((socket.lastX || 0) - shieldX, (socket.lastZ || 0) - shieldZ);
                                    if (distSelf <= 650 && !socket.lilyShieldHealedAllies.has(playerId)) {
                                        socket.lilyShieldHealedAllies.add(playerId);
                                        const heal = 300;
                                        const maxH = socket.maxHp || 1500;
                                        socket.hp = Math.min(maxH, (socket.hp !== undefined ? socket.hp : maxH) + heal);
                                        gameLogic.sendEntityHpUpdate(socket, playerId, socket, true);
                                        console.log(`[🛡️💚 LILY SHIELD HEAL SELF] Escudo da Lily curou a própria Lily em +${heal} HP! HP: ${socket.hp}/${maxH}`);
                                    }

                                    // Interações com Bots ativos
                                    if (state.activeBots) {
                                        for (const bId in state.activeBots) {
                                            const bot = state.activeBots[bId];
                                            if (bot && !bot.isDead) {
                                                const dist = Math.hypot((bot.lastX || 0) - shieldX, (bot.lastZ || 0) - shieldZ);
                                                if (dist <= 650) {
                                                    const isEnemy = (!combat.sameSide(socket, bot, Number(bot.force) === Number(lilyForce)));
                                                    const isAlly = !isEnemy;
                                                    const botKey = "bot_" + (bot.playerIndex || bId);
                                                    if (isEnemy && !socket.lilyShieldHitEnemies.has(botKey)) {
                                                        socket.lilyShieldHitEnemies.add(botKey);
                                                        const dmg = 5; // Dano de colisão do escudo = 5 HP
                                                    bot.hp = Math.max(0, (bot.hp !== undefined ? bot.hp : (bot.maxHp || 2000)) - dmg);
                                                        gameLogic.sendEntityDamageUpdate(bot.playerIndex || bId, bot, dmg, playerId, 0, 1033);
                                                        gameLogic.sendEntityHpUpdate(bot, bot.playerIndex || bId, bot, true);
                                                        console.log(`[🛡️⚡ LILY SHIELD DAMAGE] Escudo da Lily atingiu bot inimigo ${bot.playerIndex || bId} causando ${dmg} de dano!`);
                                                        if (bot.hp <= 0) {
                                                            bot.isDead = true;
                                                            gameLogic.handleEntityDeath(bot, bot.playerIndex || bId, socket, playerId, 1033);
                                                        }
                                                    } else if (isAlly && !socket.lilyShieldHealedAllies.has(botKey)) {
                                                        socket.lilyShieldHealedAllies.add(botKey);
                                                        const heal = 300;
                                                        const maxH = bot.maxHp || 2000;
                                                        bot.hp = Math.min(maxH, (bot.hp !== undefined ? bot.hp : maxH) + heal);
                                                        gameLogic.sendEntityHpUpdate(bot, bot.playerIndex || bId, bot, true);
                                                        console.log(`[🛡️💚 LILY SHIELD HEAL] Escudo da Lily curou bot aliado ${bot.playerIndex || bId} em +${heal} HP!`);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                
                                // Broadcast para os outros jogadores da cena atualizando a posição
                                const p2210001 = utils.encodeInt(2210001);
                                const targetIdEnc = utils.encodeInt(playerId);
                                const shieldIdEnc = utils.encodeInt(shieldId);
                                const hpEnc = utils.encodeInt(socket.lilyBarrierHp !== undefined ? socket.lilyBarrierHp : 1000);
                                const maxHpEnc = utils.encodeInt(socket.lilyBarrierMaxHp || 1000);
                                const dateStr = "'2026-05-18 12:00:00'";
                                const creatorIdEnc = utils.encodeInt(playerId);
                                
                                const xEnc = utils.encodeInt(pos[0]);
                                const yEnc = utils.encodeInt(pos[1]);
                                const zEnc = utils.encodeInt(pos[2]);
                                const rxEnc = utils.encodeInt(socket.lilyBarrierRot ? socket.lilyBarrierRot[0] : 0);
                                const ryEnc = utils.encodeInt(socket.lilyBarrierRot ? socket.lilyBarrierRot[1] : 0);
                                const rzEnc = utils.encodeInt(socket.lilyBarrierRot ? socket.lilyBarrierRot[2] : 0);
                                
                                const resp = `e${utils.encodeInt(Number(playerId))}:SendMsgToAllClient,(${p2210001},(${targetIdEnc},(${shieldIdEnc},${hpEnc},${maxHpEnc},${dateStr},${creatorIdEnc},(${xEnc},${yEnc},${zEnc}),(${rxEnc},${ryEnc},${rzEnc})))))\n`;
                                activeSockets.forEach(s => {
                                    if (sceneScope.canReceive(socket, s) && s !== socket && !s.destroyed) {
                                        s.write(resp);
                                    }
                                });
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 2210002 Lily Shield Sync]:", e);
                            }
                        }

                        // Se for 1010001 (MSG_CREATENPC - Criação de Entidades/NPCs via Habilidade)
                        if (cmdId === 1010001) {
                            try {
                                const parsed = utils.parseArrayList(argsStr);
                                const momId = parsed[0];
                                const npcIndex = parsed[1];
                                const force = parsed[2];
                                const x = parsed[3];
                                const y = parsed[4];
                                const z = parsed[5];
                                const rx = parsed[6] || 0;
                                const ry = parsed[7] || 0;
                                const rz = parsed[8] || 0;
                                const sx = parsed[9] || 100;
                                const sy = parsed[10] || 100;
                                const sz = parsed[11] || 100;
                                const fx = parsed[12] || 0;
                                const fy = parsed[13] || 0;
                                const fz = parsed[14] || 0;
                                const skillId = parsed[15] || 0;
                                const targetId = parsed[16] || -1;
                                const momType = parsed[17] || 0;
                                const colliderName = parsed[18] || "";

                                // Aloca um ID único de NPC para a entidade
                                global.nextNpcId = global.nextNpcId || 3000;
                                const npcId = global.nextNpcId++;

                                // Resolvendo a força real baseada no time do jogador criador
                                const masterSocket = activeSockets.find(s => sceneScope.canReceive(socket, s) && !s.destroyed && (s.playerId === momId || String(s.playerId) === String(momId) || s.playerIndex === momId || String(s.playerIndex) === String(momId)));
                                const masterNetId = masterSocket ? (masterSocket.playerIndex !== undefined ? masterSocket.playerIndex : momId) : momId;
                                const npcForce = masterSocket ? (masterSocket.force !== undefined ? masterSocket.force : force) : force;

                                // Busca dados do NPC no banco de dados xml
                                const npcConf = db.npcData[npcIndex] || { hp: 100, shield: 0, name: "NPC", time: 0, weaponIndex: 0 };
                                const maxHp = npcConf.hp;
                                const npcTime = npcConf.time || 0;

                                console.log(`[🤖 ENTITY SPAWN] Spawning Entity: ${npcConf.name} (Index: ${npcIndex}, ID: ${npcId}) with Lifetime: ${npcTime}ms for Master NetID: ${masterNetId} (Force: ${npcForce}) at (${x}, ${y}, ${z})`);

                                // Limite de 3 Sentries / Torretas ativas simultâneas por jogador para a Brigit (NPC Index 1001)
                                if (npcIndex === 1001 || String(npcIndex) === "1001") {
                                    const existingMasterSentries = Object.values(spawnedNpcs).filter(n => 
                                        n && (n.index === 1001 || String(n.index) === "1001") &&
                                        Number(n.masterId) === Number(masterNetId)
                                    );
                                    if (existingMasterSentries.length >= 3) {
                                        existingMasterSentries.sort((a, b) => a.id - b.id);
                                        const oldestSentry = existingMasterSentries[0];
                                        delete spawnedNpcs[oldestSentry.id];
                                        const destPacket = `d${utils.encodeInt(oldestSentry.id)}\n`;
                                        activeSockets.forEach(s => {
                                            if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                                s.write(destPacket);
                                            }
                                        });
                                        console.log(`[🤖 BRIGIT SENTRIES LIMIT] Master ${masterNetId} atingiu limite de 3 torretas. Destruindo a mais antiga (NPC ID: ${oldestSentry.id})`);
                                    }
                                }

                                // Limite de 1 Auto Cannon / Torreta ativa simultânea por jogador para a Eva (NPC Index 1014)
                                if (npcIndex === 1014 || String(npcIndex) === "1014") {
                                    const existingMasterTurrets = Object.values(spawnedNpcs).filter(n => 
                                        n && (n.index === 1014 || String(n.index) === "1014") &&
                                        Number(n.masterId) === Number(masterNetId)
                                    );
                                    if (existingMasterTurrets.length >= 1) {
                                        existingMasterTurrets.sort((a, b) => a.id - b.id);
                                        const oldestTurret = existingMasterTurrets[0];
                                        delete spawnedNpcs[oldestTurret.id];
                                        const destPacket = `d${utils.encodeInt(oldestTurret.id)}\n`;
                                        activeSockets.forEach(s => {
                                            if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                                s.write(destPacket);
                                            }
                                        });
                                        console.log(`[🤖 EVA TURRET LIMIT] Master ${masterNetId} atingiu limite de 1 torreta. Destruindo a anterior (NPC ID: ${oldestTurret.id})`);
                                    }
                                }

                                // Limite de 1 Par de Portais da Eva ativos por vez (NPC Index 1016 / 1017)
                                if (npcIndex === 1016 || String(npcIndex) === "1016") {
                                    const existingTps = Object.values(spawnedNpcs).filter(n => 
                                        n && (n.index === 1016 || String(n.index) === "1016" || n.index === 1017 || String(n.index) === "1017") &&
                                        Number(n.masterId) === Number(masterNetId)
                                    );
                                    existingTps.forEach(oldTp => {
                                        delete spawnedNpcs[oldTp.id];
                                        const destPacket = `d${utils.encodeInt(oldTp.id)}\n`;
                                        activeSockets.forEach(s => {
                                            if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                                s.write(destPacket);
                                            }
                                        });
                                    });
                                    console.log(`[🌀 EVA TELEPORT LIMIT] Destruindo portais anteriores do jogador ${masterNetId}`);
                                }

                                // Limite de 1 Claymore Mine ativa simultânea por jogador para o Johnny (NPC Index 1021)
                                 if (npcIndex === 1021 || String(npcIndex) === "1021") {
                                     const existingMasterClaymores = Object.values(spawnedNpcs).filter(n => 
                                         n && (n.index === 1021 || String(n.index) === "1021") &&
                                         Number(n.masterId) === Number(masterNetId)
                                     );
                                     if (existingMasterClaymores.length >= 1) {
                                         existingMasterClaymores.sort((a, b) => a.id - b.id);
                                         const oldestClaymore = existingMasterClaymores[0];
                                         delete spawnedNpcs[oldestClaymore.id];
                                         const destPacket = `d${utils.encodeInt(oldestClaymore.id)}\n`;
                                         activeSockets.forEach(s => {
                                             if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                                 s.write(destPacket);
                                             }
                                         });
                                         console.log(`[🪤 JOHNNY TRAP LIMIT] Master ${masterNetId} atingiu limite de 1 mina. Destruindo a anterior (NPC ID: ${oldestClaymore.id})`);
                                     }
                                 }

                                 // Limite de 1 Frozen Trap ativa simultânea por jogador para a Scarlett (NPC Index 1010)
                                if (npcIndex === 1010 || String(npcIndex) === "1010") {
                                    const existingMasterTraps = Object.values(spawnedNpcs).filter(n => 
                                        n && (n.index === 1010 || String(n.index) === "1010") &&
                                        Number(n.masterId) === Number(masterNetId)
                                    );
                                    if (existingMasterTraps.length >= 1) {
                                        existingMasterTraps.sort((a, b) => a.id - b.id);
                                        const oldestTrap = existingMasterTraps[0];
                                        delete spawnedNpcs[oldestTrap.id];
                                        const destPacket = `d${utils.encodeInt(oldestTrap.id)}\n`;
                                        activeSockets.forEach(s => {
                                            if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                                s.write(destPacket);
                                            }
                                        });
                                        console.log(`[❄️ SCARLETT TRAP LIMIT] Master ${masterNetId} atingiu limite de 1 trap. Destruindo a mais antiga (NPC ID: ${oldestTrap.id})`);
                                    }
                                }

                        // Track the spawned NPC on the server
                        spawnedNpcs[npcId] = {
                            id: npcId,
                            index: npcIndex,
                            hp: maxHp,
                            maxHp: maxHp,
                            x: x,
                            y: y,
                            z: z,
                            masterId: masterNetId,
                            force: npcForce,
                            weaponIndex: npcConf.weaponIndex || 0
                        };
                        sceneScope.tagEntity(spawnedNpcs[npcId], socket);

                        global.npcHistory = global.npcHistory || {};
                        global.npcHistory[npcId] = spawnedNpcs[npcId];

                        // Envia a inicialização da entidade diretamente para todos os clientes ativos
                        const className = getEntityClassName(npcIndex);
                        const spawnPacket = `c${className}:${utils.encodeInt(npcId)},("Initialize",(${utils.encodeInt(masterNetId)},${utils.encodeInt(npcId)},${utils.encodeInt(npcIndex)},${utils.encodeInt(npcForce)},(${utils.encodeInt(x)},${utils.encodeInt(y)},${utils.encodeInt(z)}),(${utils.encodeInt(rx)},${utils.encodeInt(ry)},${utils.encodeInt(rz)}),(${utils.encodeInt(sx)},${utils.encodeInt(sy)},${utils.encodeInt(sz)}),(${utils.encodeInt(fx)},${utils.encodeInt(fy)},${utils.encodeInt(fz)}),${utils.encodeInt(targetId)},${utils.encodeInt(maxHp)},${utils.encodeInt(npcTime)},${utils.encodeInt(1)},${utils.encodeInt(momType)},'${colliderName}'),((),()),0)\n`;
                        spawnedNpcs[npcId].spawnPacket = spawnPacket;
                        activeSockets.forEach(s => {
                            if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                s.write(spawnPacket);
                            }
                        });
                                console.log(`[🤖 ENTITY SPAWNED GLOBAL] Class: ${className}, Index: ${npcIndex}, ID: ${npcId}, Master: ${masterNetId}`);

                                // 🌀 EVA ULTIMATE: Quando o portal de saída 1016 é colocado, cria automaticamente o portal de entrada 1017 na base aliada!
                                if (npcIndex === 1016 || String(npcIndex) === "1016") {
                                    const currentMapId = socket.targetMapId || state.getLastMapId() || 5202;
                                    const baseCoords = getBaseTeleportCoordinates(currentMapId, npcForce);
                                    const baseNpcId = global.nextNpcId++;
                                    const baseNpcTime = npcTime || 120000;

                                    const baseNpc = {
                                        id: baseNpcId,
                                        index: 1017,
                                        hp: maxHp,
                                        maxHp: maxHp,
                                        x: baseCoords.sx,
                                        y: baseCoords.sy,
                                        z: baseCoords.sz,
                                        masterId: masterNetId,
                                        force: npcForce,
                                        weaponIndex: 0,
                                        pairedNpcId: npcId
                                    };
                                    spawnedNpcs[baseNpcId] = baseNpc;
                                    sceneScope.tagEntity(baseNpc, socket);
                                    spawnedNpcs[npcId].pairedNpcId = baseNpcId;

                                    const baseClass = getEntityClassName(1017);
                                    activeSockets.forEach(s => {
                                        if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                            const baseSpawnPacket = `c${baseClass}:${utils.encodeInt(baseNpcId)},("Initialize",(${utils.encodeInt(masterNetId)},${utils.encodeInt(baseNpcId)},${utils.encodeInt(1017)},${utils.encodeInt(npcForce)},(${utils.encodeInt(baseCoords.sx)},${utils.encodeInt(baseCoords.sy)},${utils.encodeInt(baseCoords.sz)}),(${utils.encodeInt(0)},${utils.encodeInt(0)},${utils.encodeInt(0)}),(${utils.encodeInt(100)},${utils.encodeInt(100)},${utils.encodeInt(100)}),(${utils.encodeInt(0)},${utils.encodeInt(0)},${utils.encodeInt(0)}),${utils.encodeInt(-1)},${utils.encodeInt(maxHp)},${utils.encodeInt(baseNpcTime)},${utils.encodeInt(1)},${utils.encodeInt(momType)},''),((),()),0)\n`;
                                            s.write(baseSpawnPacket);
                                        }
                                    });
                                    console.log(`[🌀 EVA TELEPORTER MATRIX] Portais conectados! Saída #${npcId} em (${x}, ${y}, ${z}) <-> Base #${baseNpcId} em (${baseCoords.sx}, ${baseCoords.sy}, ${baseCoords.sz})`);

                                    // AURA DE CURA E REGENERAÇÃO NOS PORTAIS (Cura +100 HP/s para aliados dentro de 8m dos portais)
                                    const healInterval = setInterval(() => {
                                        if (!spawnedNpcs[npcId] && !spawnedNpcs[baseNpcId]) {
                                            clearInterval(healInterval);
                                            return;
                                        }
                                        const portals = [spawnedNpcs[npcId], spawnedNpcs[baseNpcId]].filter(Boolean);
                                        activeSockets.forEach(s => {
                                            if (sceneScope.canReceive(socket, s) && !s.destroyed && !s.isDead && combat.sameSide(masterSocket || socket, s, Number(s.force) === Number(npcForce))) {
                                                const px = s.lastX || 0;
                                                const py = s.lastY || 0;
                                                const pz = s.lastZ || 0;
                                                for (const p of portals) {
                                                    const distSq = (px - p.x) ** 2 + (py - p.y) ** 2 + (pz - p.z) ** 2;
                                                    if (distSq <= 800 ** 2) { // 8 metros
                                                        if (s.hp < (s.maxHp || 2000)) {
                                                            s.hp = Math.min(s.maxHp || 2000, (s.hp || 2000) + 100);
                                                            gameLogic.sendEntityHpUpdate(s, s.playerId || s.playerIndex || 1, s, true);
                                                        }
                                                        break;
                                                    }
                                                }
                                            }
                                        });
                                    }, 1000);

                                    setTimeout(() => clearInterval(healInterval), baseNpcTime);
                                }

                                // ⚡ JAULA DA EVA (NPC Index 1015): Aplica Buff 1019 (Eva - Attack Reduction -25% dano) nos inimigos APENAS enquanto estiverem DENTRO da jaula (raio 4 metros)
                                if (npcIndex === 1015 || String(npcIndex) === "1015") {
                                    const cageX = x;
                                    const cageY = y;
                                    const cageZ = z;
                                    const cageRadius = 400; // 4 metros (tamanho real da jaula)
                                    const affectedEntityIds = new Set();
                                    
                                    const cageInterval = setInterval(() => {
                                        if (!spawnedNpcs[npcId] || spawnedNpcs[npcId].isDead) {
                                            clearInterval(cageInterval);
                                            // Limpa o debuff de todos quando a jaula expirar/ser destruída
                                            affectedEntityIds.forEach(entId => {
                                                const s = activeSockets.find(p => p.socketType === "SCENE" && !p.destroyed && (p.playerId === entId || p.playerIndex === entId));
                                                if (s && s.activeBuffs && s.activeBuffs[1019]) {
                                                    gameLogic.cleanupBuff(s, s.playerId || s.playerIndex || 1, 1019, true);
                                                } else if (state.activeBots && state.activeBots[entId]) {
                                                    const b = state.activeBots[entId];
                                                    if (b && b.activeBuffs && b.activeBuffs[1019]) {
                                                        gameLogic.cleanupBuff(b, b.playerIndex, 1019, false);
                                                    }
                                                }
                                            });
                                            affectedEntityIds.clear();
                                            return;
                                        }
                                        
                                        activeSockets.forEach(s => {
                                            if (sceneScope.canReceive(socket, s) && !s.destroyed && !s.isDead && !combat.sameSide(masterSocket || socket, s, Number(s.force) === Number(npcForce))) {
                                                const px = s.lastX || 0;
                                                const py = s.lastY || 0;
                                                const pz = s.lastZ || 0;
                                                const hasPos = (px !== 0 || py !== 0 || pz !== 0);
                                                const distSq = (px - cageX) ** 2 + (py - cageY) ** 2 + (pz - cageZ) ** 2;
                                                const isInside = hasPos && (distSq <= cageRadius ** 2);
                                                const entId = s.playerId || s.playerIndex || 1;
                                                
                                                if (isInside) {
                                                    affectedEntityIds.add(entId);
                                                    if (!s.activeBuffs || !s.activeBuffs[1019]) {
                                                        gameLogic.applyBuffToEntity(masterSocket || s, entId, 1019, 5000);
                                                    }
                                                } else if (affectedEntityIds.has(entId)) {
                                                    // Saiu da jaula -> remove o debuff imediatamente!
                                                    affectedEntityIds.delete(entId);
                                                    if (s.activeBuffs && s.activeBuffs[1019]) {
                                                        gameLogic.cleanupBuff(s, entId, 1019, true);
                                                    }
                                                }
                                            }
                                        });
                                        
                                        if (state.activeBots) {
                                            Object.values(state.activeBots).forEach(b => {
                                                if (b && !b.isDead && !combat.sameSide(masterSocket || socket, b, Number(b.force) === Number(npcForce))) {
                                                    const bx = b.lastX || 0;
                                                    const by = b.lastY || 0;
                                                    const bz = b.lastZ || 0;
                                                    const hasPos = (bx !== 0 || by !== 0 || bz !== 0);
                                                    const distSq = (bx - cageX) ** 2 + (by - cageY) ** 2 + (bz - cageZ) ** 2;
                                                    const isInside = hasPos && (distSq <= cageRadius ** 2);
                                                    const entId = b.playerIndex;
                                                    
                                                    if (isInside) {
                                                        affectedEntityIds.add(entId);
                                                        if (!b.activeBuffs || !b.activeBuffs[1019]) {
                                                            gameLogic.applyBuffToEntity(masterSocket || socket, entId, 1019, 5000);
                                                        }
                                                    } else if (affectedEntityIds.has(entId)) {
                                                        affectedEntityIds.delete(entId);
                                                        if (b.activeBuffs && b.activeBuffs[1019]) {
                                                            gameLogic.cleanupBuff(b, entId, 1019, false);
                                                        }
                                                    }
                                                }
                                            });
                                        }
                                    }, 250);

                                    setTimeout(() => {
                                        clearInterval(cageInterval);
                                        affectedEntityIds.forEach(entId => {
                                            const s = activeSockets.find(p => p.socketType === "SCENE" && !p.destroyed && (p.playerId === entId || p.playerIndex === entId));
                                            if (s && s.activeBuffs && s.activeBuffs[1019]) {
                                                gameLogic.cleanupBuff(s, s.playerId || s.playerIndex || 1, 1019, true);
                                            } else if (state.activeBots && state.activeBots[entId]) {
                                                const b = state.activeBots[entId];
                                                if (b && b.activeBuffs && b.activeBuffs[1019]) {
                                                    gameLogic.cleanupBuff(b, b.playerIndex, 1019, false);
                                                }
                                            }
                                        });
                                        affectedEntityIds.clear();
                                    }, npcTime || 5000);
                                }
                                 
// ⏳ LIFETIME TIMER PARA ENTIDADES COM TEMPO DEFINIDO (Time > 0)
                                if (npcTime > 0) {
                                    const timeoutMs = (npcIndex === 1003 || String(npcIndex) === "1003") ? npcTime + 1500 : npcTime;
                                    setTimeout(() => {
                                        if (spawnedNpcs[npcId] !== undefined) {
                                            const targetNpc = spawnedNpcs[npcId];
                                            delete spawnedNpcs[npcId];
                                            const destPacket = `d${utils.encodeInt(npcId)}\n`;
                                            activeSockets.forEach(s => {
                                                if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                                    s.write(destPacket);
                                                }
                                            });
                                            console.log(`[⏳ ENTITY LIFETIME EXPIRED] NPC ID: ${npcId} (Index: ${npcIndex}, Name: ${npcConf.name}) destroyed after ${npcTime}ms.`);
                                            
                                            // Scarlett (1022) Ultimate snowball (1013) camera tracking reset upon lifetime expiration
                                            if (targetNpc.index === 1013) {
                                                const creatorSocket = activeSockets.find(s => sceneScope.canReceive(socket, s) && !s.destroyed && (Number(s.playerId) === Number(targetNpc.masterId) || Number(s.playerIndex) === Number(targetNpc.masterId)));
                                                if (creatorSocket) {
                                                    creatorSocket.write(`e${utils.encodeInt(Number(creatorSocket.playerId))}:SuperSkillFinish\n`);
                                                    creatorSocket.write(`e${utils.encodeInt(Number(creatorSocket.playerIndex || 1))}:SendMsgToClient,(${utils.encodeInt(20014)},(${utils.encodeInt(90008)},(),${utils.encodeInt(1)}))\n`);
                                                    console.log(`[❄️ SNOWBALL RESET] Scarlett Camera reset: e${utils.encodeInt(Number(creatorSocket.playerId))}:SuperSkillFinish`);
                                                }
                                            }
                                        }
                                    }, timeoutMs);
                                }
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 1010001 Create Npc]:", e);
                            }
                        }

                        
                        // Se for 1010100 (NPC_TRANSMISSION - Jogador pisou no portal de teletransporte da Eva)
                        if (cmdId === 1010100) {
                            try {
                                const parsed = utils.parseArrayList(argsStr);
                                const portalNetId = parsed[0];
                                const portal = spawnedNpcs[portalNetId] || (global.npcHistory && global.npcHistory[portalNetId]);
                                console.log(`[🌀 TELEPORT TRIGGER] Jogador ${socket.playerName || socket.playerId} pisou no portal #${portalNetId}`);
                                
                                if (portal && portal.pairedNpcId && spawnedNpcs[portal.pairedNpcId]) {
                                    const destPortal = spawnedNpcs[portal.pairedNpcId];
                                    const destX = destPortal.x;
                                    const destY = destPortal.y + 50; // Leve elevação de 50cm para não colidir com o chão
                                    const destZ = destPortal.z;

                                    // Atualiza coordenadas no servidor
                                    socket.lastX = destX;
                                    socket.lastY = destY;
                                    socket.lastZ = destZ;

                                    // Envia comando de teletransporte para o cliente C# (Hero_Message_Handle.cs:33)
                                    const telePacket = `e${utils.encodeInt(socket.playerIndex || 1)}:SendMsgToClient,(${utils.encodeInt(1010000)},(${utils.encodeInt(1010100)},(${utils.encodeInt(destX)},${utils.encodeInt(destY)},${utils.encodeInt(destZ)})))\n`;
                                    socket.write(telePacket);
                                    console.log(`[🌀 TELEPORT EXECUTED] Jogador ${socket.playerName || socket.playerId} teleportado para (${destX}, ${destY}, ${destZ})`);

                                    // Buff/Cura ao usar o teletransporte: cura instantânea de +300 HP e regeneração
                                    socket.hp = Math.min(socket.maxHp || 2000, (socket.hp || 2000) + 300);
                                    gameLogic.sendEntityHpUpdate(socket, socket.playerId || "1", socket, true);
                                    gameLogic.applyBuffToEntity(socket, socket.playerId || "1", 1002, 3000); // Regeneração/Escudo da base
                                } else if (portal) {
                                    // Se o portal destino não estiver instanciado, teleporta para a base da equipe como fallback
                                    const currentMapId = socket.targetMapId || state.getLastMapId() || 5202;
                                    const baseCoords = gameLogic.getSpawnCoordinates(currentMapId, socket.force || 1);
                                    const destX = baseCoords.sx;
                                    const destY = baseCoords.sy + 50;
                                    const destZ = baseCoords.sz;

                                    socket.lastX = destX;
                                    socket.lastY = destY;
                                    socket.lastZ = destZ;

                                    const telePacket = `e${utils.encodeInt(socket.playerIndex || 1)}:SendMsgToClient,(${utils.encodeInt(1010000)},(${utils.encodeInt(1010100)},(${utils.encodeInt(destX)},${utils.encodeInt(destY)},${utils.encodeInt(destZ)})))\n`;
                                    socket.write(telePacket);
                                    console.log(`[🌀 TELEPORT BASE FALLBACK] Jogador ${socket.playerName || socket.playerId} teleportado para a base (${destX}, ${destY}, ${destZ})`);
                                }
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 1010100 Teleport]:", e);
                            }
                        }

                        // Se for 1010004 (MSG_NPC_SUICIDE - NPC suicida/detonação)
                        if (cmdId === 1010004) {
                            try {
                                const parsed = utils.parseArrayList(argsStr);
                                const npcId = parsed[0];
                                console.log(`[🤖 ENTITY SUICIDE] Npc ID: ${npcId} suicided/detonated!`);
                                
                                // Remove from tracking if present
                                if (spawnedNpcs[npcId] !== undefined) {
                                    const targetNpc = spawnedNpcs[npcId];
                                    if (targetNpc.index === 1013) {
                                        const creatorSocket = activeSockets.find(s => sceneScope.canReceive(socket, s) && !s.destroyed && (Number(s.playerId) === Number(targetNpc.masterId) || Number(s.playerIndex) === Number(targetNpc.masterId)));
                                        if (creatorSocket) {
                                            creatorSocket.write(`e${utils.encodeInt(Number(creatorSocket.playerId))}:SuperSkillFinish\n`);
                                            creatorSocket.write(`e1:SendMsgToClient,(${utils.encodeInt(20014)},(${utils.encodeInt(90008)},(),${utils.encodeInt(1)}))\n`);
                                            console.log(`[❄️ SNOWBALL RESET] Scarlett Camera reset on suicide: e${utils.encodeInt(Number(creatorSocket.playerId))}:SuperSkillFinish`);
                                        }
                                    }
                                    delete spawnedNpcs[npcId];
                                }

                                // Envia a destruição da entidade diretamente para todos os clientes ativos
                                const destPacket = `d${utils.encodeInt(npcId)}\n`;
                                activeSockets.forEach(s => {
                                    if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                        s.write(destPacket);
                                    }
                                });
                                console.log(`[🤖 ENTITY DESTROYED]: ${destPacket.trim()}`);
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 1010004 Suicide Npc]:", e);
                            }
                        }

                        // Se for 1020001 (MSG_NPC_SYNCHRONIZATION_VALIDATE - Validação de Sincronização do NPC)
                        if (cmdId === 1020001) {
                            try {
                                const parsed = utils.parseArrayList(argsStr);
                                const npcId = parsed[0];
                                console.log(`[🤖 ENTITY SYNC] Validando sincronização do NPC ID: ${npcId}`);

                                // Envia a confirmação de validação de sucesso encapsulada no NetMsg 1020000 para todos os clientes
                                const p1020000 = utils.encodeInt(1020000);
                                const p1020002 = utils.encodeInt(1020002);
                                const npcIdEnc = utils.encodeInt(npcId);
                                const resp = `e${utils.encodeInt(Number(socket.playerIndex || playerId))}:SendMsgToClient,(${p1020000},(${p1020002},(${npcIdEnc})))\n`;
                                activeSockets.forEach(s => {
                                    if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                        s.write(resp);
                                    }
                                });
                                console.log(`[🤖 ENTITY SYNCED GLOBAL]: ${resp.trim()}`);
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 1020001 NPC Sync Validate]:", e);
                            }
                        }

                        // Se for 2060001 (JINGKE_ADDBUFFE_SELF - Vincent pegou a orbe de cura da alma)
                        if (cmdId === 2060001) {
                            try {
                                const parsed = utils.parseArrayList(argsStr);
                                const targetNetId = parsed[0] || playerId;
                                console.log(`[🩸 VINCENT SOUL ORB PICKUP] Jogador ${targetNetId} pegou a orbe de cura do Vincent!`);
                                
                                const healAmount = 250;
                                socket.hp = Math.min(socket.maxHp || 2000, (socket.hp !== undefined ? socket.hp : (socket.maxHp || 2000)) + healAmount);
                                gameLogic.sendEntityHpUpdate(socket, targetNetId, socket, true);
                                
                                // Aplica Buff 1038 / 1036 de cura e efeito visual da orbe
                                socket.matchHeal = (socket.matchHeal || 0) + 250;
                                gameLogic.sendTabScoreboard(socket);
                                gameLogic.applyBuffToEntity(socket, targetNetId, 1038, 100);
                                gameLogic.applyBuffToEntity(socket, targetNetId, 1036, 2000);
                                
                                console.log(`[🩸 VINCENT HEAL ORB EXECUTED] Vincent curado em +${healAmount} HP. Novo HP: ${socket.hp}`);
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 2060001 JingKe AddBuff Self]:", e);
                            }
                        }

                        // Se for 20014 (🎭 SELEÇÃO/TROCA DE HERÓI EM SCENE SOCKET - MUDANÇA DE PERSONAGEM EM JOGO)
                        if (cmdId === 20014) {
                            try {
                                const parsed = utils.parseArrayList(argsStr);
                                const subCmd = parsed[0];
                                // The selection countdown calls OnEnterGame, which sends 90004.
                                if (subCmd === 90004 && socket.waitingForHeroLock) {
                                    confirmInitialHero(socket, socket.currentHeroId || 1015);
                                }
                                if (subCmd === 90002) {
                                    const selectedHeroId = parsed[1];
                                    const isLocked = parsed[2] || 0; // 0 = preview, 1 = locked
                                    lifecycle.event(socket, 'HERO_SELECTION', { hero: selectedHeroId, locked: isLocked });
                                    
                                    console.log(`[🎭 SELEÇÃO DE HERÓI] Jogador ${socket.playerNameEnc} selecionou HeroID ${selectedHeroId} (Locked: ${isLocked})`);
                                    
                                    if (isLocked === 1) {
                                        if (socket.waitingForHeroLock) {
                                            confirmInitialHero(socket, selectedHeroId);
                                        } else if (socket.isCombatReady) {
                                            socket.hasConfirmedHero = true;
                                            gameLogic.switchPlayerHero(socket, selectedHeroId);
                                            broadcastMatchUserList(socket);
                                        }
                                    } else {
                                        if (!socket.waitingForHeroLock && !socket.isCombatReady) return;
                                        if (socket.waitingForHeroLock) {
                                            socket.hasConfirmedHero = false;
                                            socket.currentHeroId = selectedHeroId;
                                        }
                                        
                                        const p20014 = utils.encodeInt(20014);
                                        const s90002 = utils.encodeInt(90002);
                                        const zero = utils.encodeInt(0);
                                        const one = utils.encodeInt(1);
                                        const forceEnc = utils.encodeInt(socket.force || 1);
                                        const isReadyEnc = utils.encodeInt(0); // 0 porque é apenas preview
                                        const heroIdEnc = utils.encodeInt(selectedHeroId);
                                        const isCapEnc = utils.encodeInt(socket.playerIndex === 1 ? 1 : 0);
                                        
                                        const prerogativeLobby = `(${isCapEnc},${isCapEnc},${isCapEnc},${isCapEnc},'${socket.playerNameEnc}',${zero},'',${zero})`;
                                        const cust = getPlayerCustomizationData(socket.playerName, selectedHeroId);
                                        const userDataHeroInfo = `(${heroIdEnc},${utils.encodeInt(cust.skinId)},${utils.encodeInt(cust.emoteId)},${one},${utils.encodeInt(cust.weaponId)})`;
                                        const myUserData = `('${socket.playerNameEnc}',${utils.encodeInt(99)},${one},${forceEnc},${isReadyEnc},${one},${heroIdEnc},${one},${one},${zero},${utils.encodeInt(cust.faceId)},'Local',${zero},${userDataHeroInfo},${utils.encodeInt(socket.playerIndex || 1)},${zero},${zero},${isCapEnc},${prerogativeLobby},())`;
                                        
                                        const resp = `e${utils.encodeInt(Number(playerId))}:SendMsgToClient,(${p20014},(${s90002},'${socket.playerNameEnc}',${myUserData}))\n`;
                                        
                                        // Broadcast para todos os jogadores na cena verem o preview do herói
                                        activeSockets.forEach(s => {
                                            if ((s === socket || sceneScope.canReceive(socket, s)) && !s.destroyed && !s.sceneRetired) {
                                                s.write(resp);
                                            }
                                        });
                                    }
                                }
                            } catch (e) {
                                console.error("[⚠️ ERRO ao trocar de herói em SCENE]:", e);
                            }
                        }

                        // Se for 51006 (🏰 TRIGGER DE BASE SPAWN - ENTRAR/SAIR DA BASE EM TDM)
                        if (cmdId === 51006) {
                            try {
                                const parsed = utils.parseArrayList(argsStr);
                                const playerNetId = parsed[0]; // player NetID (int)
                                const state = parsed[1];      // 1 = entrou, 0 = saiu (int)
                                const baseNetId = parsed[2] || 0;
                                
                                console.log(`[🏰 BASE SPAWN TDM] Jogador NetID ${playerNetId} ${state === 1 ? 'ENTROU na' : 'SAIU da'} base spawn (BaseNetID: ${baseNetId})`);
                                
                                // Atualiza o estado InJidi do socket associado na cena
                                const targetSocket = activeSockets.find(s => sceneScope.canReceive(socket, s) && s.playerIndex === playerNetId && !s.destroyed);
                                if (targetSocket) {
                                    targetSocket.inJidi = state;
                                }
                                
                                // Broadcast de 52002 usando NetID absoluto para cada cliente da cena
                                const p52002 = utils.encodeInt(52002);
                                const stateEnc = utils.encodeInt(state);
                                
                                activeSockets.forEach(s => {
                                    if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                        const playerNetIdEnc = utils.encodeInt(playerNetId);
                                        const resp = `e${utils.encodeInt(s.playerIndex || 1)}:SendMsgToClient,(${p52002},(${playerNetIdEnc},${stateEnc}))\n`;
                                        s.write(resp);
                                    }
                                });
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 51006 InJidi Trigger]:", e);
                            }
                        }

                        // Se for 1011 ou 1012 (Fo/Fp - Emote / ZhaoHu / Chat em Partida)
                        if (cmdId === 1011 || cmdId === 1012) {
                            try {
                                const parsed = utils.parseArrayList(argsStr);
                                const nPinDao = parsed[0];
                                const msgArray = parsed[4] || [];
                                const valueArray = parsed[5] || [1, 1, 1, 0];

                                const sound = msgArray[1] || "";
                                const anim = msgArray[2] || "";
                                const type = msgArray[3] || 0;
                                const ico = msgArray[4] || "";

                                const cdReady = valueArray[0] || 0;
                                const onGround = valueArray[1] || 0;
                                const bePlayActive = valueArray[2] || 0;
                                const nHuDongState = valueArray[3] || 0;

                                const p2000 = utils.encodeInt(2000);
                                const p2001 = utils.encodeInt(2001);
                                const netId = utils.encodeInt(socket.playerIndex || 1); // NetID absoluto dinâmico
                                const forceEnc = utils.encodeInt(socket.force || 1);

                                // msg: (text_hex, sound, anim, type, ico)
                                const msgList = `('${msgArray[0] || ""}','${sound}','${anim}',${utils.encodeInt(type)},'${ico}')`;

                                // value: (cdReady, onGround, bePlayActive, nHuDongState)
                                const valList = `(${utils.encodeInt(cdReady)},${utils.encodeInt(onGround)},${utils.encodeInt(bePlayActive)},${utils.encodeInt(nHuDongState)})`;

                                // userChatParam
                                const ucp = parsed[6] || [];
                                const ucpList = `('${ucp[0] || ""}','${ucp[1] || ""}',${utils.encodeInt(ucp[2] || 0)})`;

                                // szNetMsg = (nPinDao, msg_0, msg_1, (), msg, value, userChatParam)
                                const szNetMsg = `(${utils.encodeInt(nPinDao)},'${parsed[1] || ""}','${parsed[2] || ""}',(),${msgList},${valList},${ucpList})`;

                                activeSockets.forEach(s => {
                                    if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                        const szData = `(${szNetMsg},'${sound}','${anim}',${utils.encodeInt(type)},${utils.encodeInt(type)},${forceEnc},${netId},'${ico}',${utils.encodeInt(cdReady)},${utils.encodeInt(onGround)},${utils.encodeInt(bePlayActive)},${utils.encodeInt(nHuDongState)})`;
                                        const broadcastPacket = `e${utils.encodeInt(s.playerIndex || 1)}:SendMsgToClient,(${p2000},(${p2001},${szData}))\n`;
                                        s.write(broadcastPacket);
                                    }
                                });
                                console.log(`[🎭 EMOTE/CHAT BROADCAST]: Sender PlayerIndex ${socket.playerIndex || 1}`);
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar emote/chat]:", e);
                            }
                        }

                        // Se for 1006 (Chat de partida/canal in-game)
                        if (cmdId === 1006) {
                            try {
                                const parsed = utils.parseArrayList(argsStr);
                                const nPinDao = parsed[0];
                                const hexMsg = parsed[1] || "";
                                const targetUser = parsed[2] || "";
                                const ucp = parsed[3] || [];

                                const p1006 = utils.encodeInt(1006);
                                const pPinDao = utils.encodeInt(nPinDao);
                                const pCid = utils.encodeInt(socket.playerIndex || 1); // NetID absoluto dinâmico do remetente

                                const senderNameHex = socket.playerNameEnc || PLAYER_NAME_ENC;
                                const msgHex = hexMsg;

                                const ucpList = `('${ucp[0] || ""}','${ucp[1] || ""}',${utils.encodeInt(ucp[2] || 0)})`;
                                const szNetMsg = `(${pPinDao},'[mi]${senderNameHex}[/mi]','[mi]${msgHex}[/mi]','${targetUser}',(),(),${pCid},${ucpList},())`;

                                activeSockets.forEach(s => {
                                    if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                        const broadcastPacket = `e${utils.encodeInt(s.playerIndex || 1)}:SendMsgToClient,(${p1006},${szNetMsg})\n`;
                                        s.write(broadcastPacket);
                                    }
                                });
                                console.log(`[💬 CHAT BROADCAST]: ${senderNameHex} (NetID: ${socket.playerIndex || 1}) -> ${msgHex}`);

                                // ==========================================
                                // ⚔️ DETECTOR E INTERCEPTADOR DE COMANDOS DE CHAT
                                // ==========================================
                                if (msgHex) {
                                    const chatText = Buffer.from(msgHex, 'hex').toString('utf8').trim();
                                    console.log(`[💬 CHAT INTERCEPTADO]: "${chatText}"`);

                                    if (chatText.startsWith('/')) {
                                        const commandParts = chatText.slice(1).split(' ');
                                        const cmdName = commandParts[0].toLowerCase();
                                        const cmdArg = commandParts[1] || "";

                                        console.log(`[🛠️ COMANDO DETECTADO] Nome: "${cmdName}" | Arg: "${cmdArg}"`);

                                        if (cmdName === 'bot' || cmdName === 'spawn' || cmdName === 'dummy' || cmdName === 'alvo' || cmdName === 'boneco') {
                                            let heroId = 1015;
                                            let mode = 'static';
                                            let hpOption = 'normal';

                                            for (let i = 1; i < commandParts.length; i++) {
                                                const arg = commandParts[i].toLowerCase();
                                                const resolvedHeroId = heroCatalog.resolveHeroAlias(arg);
                                                if (resolvedHeroId) {
                                                    heroId = resolvedHeroId;
                                                } else if (arg === 'strafe' || arg === 'mover' || arg === 'move') {
                                                    mode = 'strafe';
                                                } else if (arg === 'follow' || arg === 'seguir' || arg === 'segue') {
                                                    mode = 'follow';
                                                } else if (arg === 'combat' || arg === 'luta' || arg === 'lutar') {
                                                    mode = 'combat';
                                                } else if (arg === 'tank' || arg === 'tanque' || arg === '10k') {
                                                    hpOption = 'tank';
                                                } else if (arg === 'immortal' || arg === 'imortal') {
                                                    hpOption = 'immortal';
                                                }
                                            }

                                            const dummy = gameLogic.spawnPracticeHeroBot(socket, {
                                                heroId,
                                                mode,
                                                hpOption
                                            });

                                            if (dummy) {
                                                const hName = db.getHeroName(heroId) || `Herói ${heroId}`;
                                                gameLogic.sendSystemChat(socket, `🎯 [Treino] Boneco "${hName}" (Modo: ${mode}, HP: ${dummy.maxHp}/${dummy.shield || 0}) spawnado na sua frente!`);
                                            } else {
                                                gameLogic.sendSystemChat(socket, `⚠️ [Treino] Não foi possível spawnar o boneco de treino.`);
                                            }
                                        } else if (cmdName === 'hero' || cmdName === 'heroi' || cmdName === 'char') {
                                             const selectedHeroId = heroCatalog.resolveHeroAlias(cmdArg) || parseInt(cmdArg);
                                             if (selectedHeroId) {
                                                 gameLogic.switchPlayerHero(socket, selectedHeroId);
                                                 
                                                 const sysHex = Buffer.from(`[Sistema] Mudando de personagem para o ID ${selectedHeroId}!\n`).toString('hex');
                                                 const sysNetMsg = `(${utils.encodeInt(8)},'','[mi]${sysHex}[/mi]','',(),(),${pCid},('','',0),())`;
                                                 const sysBroadcast = `e1:SendMsgToClient,(${p1006},${sysNetMsg})\n`;
                                                 socket.write(sysBroadcast);
                                             }
                                         } else if (cmdName === 'sync' || cmdName === 'vis') {
                                             let playersSynced = 0;
                                             let botsSynced = 0;
                                             let npcsSynced = 0;

                                             activeSockets.forEach(s => {
                                                 if (s !== socket && !s.destroyed && !s.waitingForHeroLock && sceneScope.canReceive(s, socket)) {
                                                     const spawnPkt = gameLogic.getPlayerSpawnPacket(s, socket);
                                                     if (spawnPkt) {
                                                         socket.bypassInterceptor = true;
                                                         socket.write(spawnPkt);
                                                         socket.bypassInterceptor = false;
                                                         gameLogic.sendEntityHpUpdate(socket, s.playerId || s.playerIndex || 1, s, true);
                                                         playersSynced++;
                                                     }
                                                 }
                                             });

                                             if (state.activeBots) {
                                                 for (const botId in state.activeBots) {
                                                     const bot = state.activeBots[botId];
                                                     if (!bot || bot.isDead || !sceneScope.sameScene(socket, bot)) continue;
                                                     const botPkt = gameLogic.getPlayerSpawnPacket(bot, null);
                                                     if (botPkt) {
                                                         socket.bypassInterceptor = true;
                                                         socket.write(botPkt);
                                                         socket.bypassInterceptor = false;
                                                         botsSynced++;
                                                     }
                                                 }
                                             }

                                             for (const npcId in spawnedNpcs) {
                                                 const npc = spawnedNpcs[npcId];
                                                 if (!npc || npc.isDead || !sceneScope.sameScene(socket, npc)) continue;
                                                 if (npc.spawnPacket) {
                                                     socket.write(`d${utils.encodeInt(npc.id)}\n`);
                                                     socket.write(npc.spawnPacket);
                                                     npcsSynced++;
                                                 }
                                             }

                                             gameLogic.sendSystemChat(socket, `🔄 [Sync] Visibilidade ressincronizada (${playersSynced} jogadores, ${botsSynced} bots, ${npcsSynced} entidades)!`);
                                         } else if (cmdName === 'fix') {
                                             sceneScope.clearCombatTimers(socket);
                                             socket.isStunned = false;
                                             socket.isFrozen = false;
                                             const fixHeroId = socket.currentHeroId || 1015;
                                             const fixWeapon = weaponsMap[fixHeroId];
                                             const fixMaxAmmo = fixWeapon && fixWeapon.cartridgePublic !== undefined ? fixWeapon.cartridgePublic : 30;
                                             const targetPlayerIdEnc = utils.encodeInt(Number(socket.playerId || socket.playerIndex || 1));
                                             const shootName = socket.lastReloadShootName ?? 0;

                                             socket.write(`e${targetPlayerIdEnc}:OnFillBullet,((${shootName},0,${utils.encodeInt(fixMaxAmmo)},${utils.encodeInt(fixMaxAmmo)}))\n`);
                                             socket.write(`e${targetPlayerIdEnc}:SuperSkillFinish\n`);
                                             socket.write(`e${utils.encodeInt(socket.playerIndex || 1)}:SendMsgToClient,(${utils.encodeInt(20014)},(${utils.encodeInt(90008)},(),${utils.encodeInt(1)}))\n`);

                                             if (!socket.isDead) {
                                                 gameLogic.sendEntityHpUpdate(socket, playerId, socket, true);
                                             }
                                             gameLogic.sendSystemChat(socket, "🔧 [Fix] Arma, recarga, animações, timers e câmera destravados!");
                                         } else if (cmdName === 'god') {
                                             socket.godMode = !socket.godMode;
                                             if (socket.godMode) {
                                                 socket.isDead = false;
                                                 if (socket.respawnTimeout) {
                                                     clearTimeout(socket.respawnTimeout);
                                                     socket.respawnTimeout = null;
                                                 }
                                                 
                                                 // Envia OnRevive para limpar o estado de morte no cliente
                                                 const targetPlayerIdEnc = utils.encodeInt(Number(socket.playerId || 1));
                                                 const rx = utils.encodeInt(Math.round(socket.lastX || 0));
                                                 const ry = utils.encodeInt(Math.round(socket.lastZ || 0));
                                                 const rz = utils.encodeInt(Math.round(socket.lastY || 0));
                                                 const revivePacket = `e${targetPlayerIdEnc}:OnRevive,((${rx},${ry},${rz},${utils.encodeInt(180)},0,0,''))\n`;
                                                 activeSockets.forEach(s => {
                                                     if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                                         s.write(revivePacket);
                                                     }
                                                 });

                                                 socket.hp = socket.maxHp || 2000;
                                                 socket.shield = 50000;
                                                 socket.isInvincible = true;
                                                 gameLogic.sendEntityHpUpdate(socket, playerId, socket, true);
                                                 gameLogic.applyBuffToEntity(socket, playerId, 1008, 3600000);
                                                 gameLogic.sendSystemChat(socket, "🟢 [Mod Menu] MODO DEUS ATIVADO! Você está invencível e com escudo infinito.");
                                             } else {
                                                 socket.shield = 0;
                                                 socket.isInvincible = false;
                                                 gameLogic.sendEntityHpUpdate(socket, playerId, socket, true);
                                                 gameLogic.cleanupBuff(socket, playerId, 1008, true);
                                                 gameLogic.sendSystemChat(socket, "🔴 [Mod Menu] MODO DEUS DESATIVADO! Vulnerabilidade restaurada.");
                                             }
                                          } else if (cmdName === 'invisible' || cmdName === 'inv') {
                                              socket.isInvis = !socket.isInvis;
                                              if (socket.isInvis) {
                                                  gameLogic.applyBuffToEntity(socket, playerId, 1012, 3600000);
                                                  gameLogic.sendSystemChat(socket, "🟢 [Mod Menu] INVISIBILIDADE ATIVADA! Os inimigos não podem te ver.");
                                              } else {
                                                  gameLogic.cleanupBuff(socket, playerId, 1012, true);
                                                  gameLogic.sendSystemChat(socket, "🔴 [Mod Menu] INVISIBILIDADE DESATIVADA!");
                                              }
                                          } else if (cmdName === 'cd' || cmdName === 'nocd') {
                                              socket.nocd = !socket.nocd;
                                              if (socket.nocd) {
                                                  gameLogic.sendSystemChat(socket, "🟢 [Mod Menu] ZERO COOLDOWN ATIVADO! Use suas habilidades sem limites.");
                                              } else {
                                                  gameLogic.sendSystemChat(socket, "🔴 [Mod Menu] ZERO COOLDOWN DESATIVADO!");
                                              }
                                          } else if (cmdName === 'heal') {
                                              socket.isDead = false;
                                              if (socket.respawnTimeout) {
                                                  clearTimeout(socket.respawnTimeout);
                                                  socket.respawnTimeout = null;
                                              }
                                              // Envia OnRevive para limpar o estado de morte no cliente
                                              const targetPlayerIdEnc = utils.encodeInt(Number(socket.playerId || 1));
                                              const rx = utils.encodeInt(Math.round(socket.lastX || 0));
                                              const ry = utils.encodeInt(Math.round(socket.lastZ || 0));
                                              const rz = utils.encodeInt(Math.round(socket.lastY || 0));
                                              const revivePacket = `e${targetPlayerIdEnc}:OnRevive,((${rx},${ry},${rz},${utils.encodeInt(180)},0,0,''))\n`;
                                              activeSockets.forEach(s => {
                                                  if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                                      s.write(revivePacket);
                                                  }
                                              });
                                              socket.hp = socket.maxHp || 2000;
                                              socket.shield = 1000; socket.maxShield = 1000;
                                              gameLogic.sendEntityHpUpdate(socket, playerId, socket, true);
                                              gameLogic.sendSystemChat(socket, "💖 [Mod Menu] VIDA E ESCUDO RESTAURADOS COMPLETAMENTE!");
                                          } else if (cmdName === 'ragdoll' || cmdName === 'boneco') {
                                              socket.isRagdoll = !socket.isRagdoll;
                                              const targetPlayerIdEnc = utils.encodeInt(Number(socket.playerId || 1));
                                              if (socket.isRagdoll) {
                                                  const zero = utils.encodeInt(0);
                                                  const pDuration = utils.encodeInt(9999);
                                                  const deadPacket = `e${targetPlayerIdEnc}:OnDeath,(0,${pDuration},(${zero},${zero},${zero}),${zero},${zero},${zero},${zero},(),${zero},())\n`;
                                                  activeSockets.forEach(s => {
                                                      if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                                          s.write(deadPacket);
                                                      }
                                                  });
                                                  gameLogic.sendSystemChat(socket, "🤪 [Mod Menu] MODO BONECO ATIVADO! Digite /ragdoll novamente para se levantar.");
                                              } else {
                                                  const rx = utils.encodeInt(Math.round(socket.lastX || 0));
                                                  const ry = utils.encodeInt(Math.round(socket.lastZ || 0));
                                                  const rz = utils.encodeInt(Math.round(socket.lastY || 0));
                                                  const revivePacket = `e${targetPlayerIdEnc}:OnRevive,((${rx},${ry},${rz},${utils.encodeInt(180)},0,0,''))\n`;
                                                  activeSockets.forEach(s => {
                                                      if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                                          s.write(revivePacket);
                                                      }
                                                  });
                                                  gameLogic.sendSystemChat(socket, "🙂 [Mod Menu] MODO BONECO DESATIVADO! Você se levantou.");
                                              }
                                          } else if (cmdName === 'speed') {
                                              socket.superSpeed = !socket.superSpeed;
                                              if (socket.superSpeed) {
                                                  gameLogic.applyBuffToEntity(socket, playerId, 1023, 3600000);
                                                  gameLogic.sendSystemChat(socket, "⚡ [Mod Menu] SUPER VELOCIDADE ATIVADA! (Buff 1023 aplicado)");
                                              } else {
                                                  gameLogic.cleanupBuff(socket, playerId, 1023, true);
                                                  gameLogic.sendSystemChat(socket, "⚡ [Mod Menu] VELOCIDADE RESTAURADA AO NORMAL.");
                                              }
                                          } else if (cmdName === 'kill' || cmdName === 'clear') {
                                              let count = 0;
                                              for (const npcId in spawnedNpcs) {
                                                  if (spawnedNpcs[npcId]) {
                                                      socket.write(`d${utils.encodeInt(npcId)}\n`);
                                                      delete spawnedNpcs[npcId];
                                                      count++;
                                                  }
                                              }
                                              gameLogic.sendSystemChat(socket, `💀 [Mod Menu] Todos os ${count} bot(s) de treino foram eliminados/limpos!\n`);
                                          } else if (cmdName === 'team' || cmdName === 'time' || cmdName === 'lado') {
                                             if (!currentRoom) {
                                                 gameLogic.sendSystemChat(socket, "⚠️ Você precisa estar em uma sala para usar este comando!");
                                             } else {
                                                 let pSlot = -1;
                                                 for (let i = 0; i < 10; i++) {
                                                     if (currentRoom.players[i] && currentRoom.players[i].name === socket.playerName) {
                                                         pSlot = i;
                                                         break;
                                                     }
                                                 }
                                                 if (pSlot === -1) {
                                                     gameLogic.sendSystemChat(socket, "⚠️ Erro: Você não foi encontrado na sala!");
                                                 } else {
                                                     const p = currentRoom.players[pSlot];
                                                     const targetIsRed = pSlot < 5;
                                                     const startSlot = targetIsRed ? 5 : 0;
                                                     const endSlot = targetIsRed ? 10 : 5;
                                                     let absoluteIdx = -1;
                                                     for (let i = startSlot; i < endSlot; i++) {
                                                         if (!currentRoom.players[i]) {
                                                             absoluteIdx = i;
                                                             break;
                                                         }
                                                     }
                                                     
                                                     if (absoluteIdx !== -1) {
                                                         currentRoom.players[absoluteIdx] = p;
                                                         currentRoom.players[pSlot] = null;
                                                         p.force = absoluteIdx < 5 ? 1 : 2;
                                                         
                                                         console.log(`[💬 COMANDO CENA] Troca de time via chat de cena bem-sucedida para ${p.name}! Novo slot: ${absoluteIdx}`);
                                                         
                                                         const rDataChange = buildRoomData(currentRoom);
                                                         broadcastToRoom(currentRoom, `e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3318)},(1,${buildRoomUserList(currentRoom)})))\n`);
                                                         
                                                         const teamName = targetIsRed ? 'Vermelho' : 'Azul';
                                                         gameLogic.sendSystemChat(socket, `🟢 Você foi movido para o Time ${teamName} (Vaga ${absoluteIdx})!`);
                                                     } else {
                                                         const teamName = targetIsRed ? 'Vermelho' : 'Azul';
                                                         gameLogic.sendSystemChat(socket, `🔴 O Time ${teamName} está cheio!`);
                                                     }
                                                 }
                                             }
                                          } else if (cmdName === 'buff') {
                                             const buffId = parseInt(cmdArg);
                                             const customDur = parseInt(commandParts[2]) || 10000;
                                             if (!isNaN(buffId)) {
                                                 if (buffsMap[buffId]) {
                                                     gameLogic.applyBuffToEntity(socket, playerId, buffId, customDur);
                                                     gameLogic.sendSystemChat(socket, `💪 [Mod Menu] Aplicou Buff "${buffsMap[buffId].name}" (${buffId}) por ${customDur}ms.`);
                                                 } else {
                                                     gameLogic.sendSystemChat(socket, `⚠️ [Mod Menu] Buff ID ${buffId} não existe no buff.xml!`);
                                                 }
                                             } else {
                                                 gameLogic.sendSystemChat(socket, `⚠️ [Mod Menu] Uso correto: /buff [ID] [Duração em MS]`);
                                             }
                                         } else if (cmdName === 'debuff') {
                                             const debuffId = parseInt(cmdArg);
                                             const customDur = parseInt(commandParts[2]) || 10000;
                                             if (!isNaN(debuffId)) {
                                                 if (buffsMap[debuffId]) {
                                                     gameLogic.applyBuffToEntity(socket, playerId, debuffId, customDur);
                                                     gameLogic.sendSystemChat(socket, `🔥 [Mod Menu] Aplicou Debuff "${buffsMap[debuffId].name}" (${debuffId}) por ${customDur}ms.`);
                                                 } else {
                                                     gameLogic.sendSystemChat(socket, `⚠️ [Mod Menu] Debuff ID ${debuffId} não existe no buff.xml!`);
                                                 }
                                             } else {
                                                 gameLogic.sendSystemChat(socket, `⚠️ [Mod Menu] Uso correto: /debuff [ID] [Duração em MS]`);
                                             }
                                         } else if (cmdName === 'help' || cmdName === 'ajuda' || cmdName === '?') {
                                             const godState = socket.godMode ? "🟢 ATIVO" : "🔴 INATIVO";
                                             const cdState = socket.nocd ? "🟢 ATIVO" : "🔴 INATIVO";
                                             const speedState = socket.superSpeed ? "🟢 ATIVO" : "🔴 INATIVO";
                                             const invisState = socket.isInvis ? "🟢 ATIVO" : "🔴 INATIVO";

                                             gameLogic.sendSystemChat(socket, "========================================");
                                             gameLogic.sendSystemChat(socket, "🛠️ MOD MENU GM - COMANDOS DISPONÍVEIS:");
                                             gameLogic.sendSystemChat(socket, `• /god        - Modo Deus (Invencível + Escudo) [Status: ${godState}]`);
                                             gameLogic.sendSystemChat(socket, `• /nocd       - Sem Cooldown de Habilidades  [Status: ${cdState}]`);
                                             gameLogic.sendSystemChat(socket, `• /speed      - Super Velocidade (Buff 1023) [Status: ${speedState}]`);
                                             gameLogic.sendSystemChat(socket, `• /invisible  - Invisibilidade (Buff 1012)   [Status: ${invisState}]`);
                                             gameLogic.sendSystemChat(socket, "• /heal       - Recupera Vida e Escudo ao Máximo");
                                             gameLogic.sendSystemChat(socket, "• /sync (/vis)- Ressincroniza todos os jogadores/bots da sala");
                                             gameLogic.sendSystemChat(socket, "• /fix        - Destrava arma, recarga, animações e câmera");
                                             gameLogic.sendSystemChat(socket, "• /bot [ID]   - Spawna Bot de Treino na sua frente");
                                             gameLogic.sendSystemChat(socket, "• /hero [ID]  - Troca de Herói instantaneamente");
                                             gameLogic.sendSystemChat(socket, "• /kill /clear - Remove/Elimina todos os Bots spawnados");
                                             gameLogic.sendSystemChat(socket, "• /buff [ID] [MS] - Aplica qualquer Buff do buff.xml");
                                             gameLogic.sendSystemChat(socket, "• /debuff [ID] [MS] - Aplica qualquer Debuff do buff.xml");
                                             gameLogic.sendSystemChat(socket, "• /help       - Mostra esta lista de ajuda");
                                             gameLogic.sendSystemChat(socket, "========================================");
                                         }
                                    }
                                }
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 1006 chat]:", e);
                            }
                        }
                    }
                } else if (line.includes(":SDG,")) {
                    const sdgMatch = line.match(/^e([0-9A-Z_`a-z]+):(?:.*?:)?SDG,\((.+)\)/);
                    if (sdgMatch) {
                        try {
                            const playerId = String(utils.decodeInt(sdgMatch[1]));
                            const argsStr = sdgMatch[2];
                            const decoded = decodeDamageEnvelope(utils.parseArrayList(argsStr));
                            if (!decoded) {
                                lifecycle.event(socket, 'DAMAGE_REJECTED', { reason: 'invalid-envelope' });
                                continue;
                            }
                            const parsed = decoded.payload;
                            const hits = decoded.hits;
                            if (Number(socket.currentHeroId) === 2000 &&
                                (!sceneScope.canReceive(socket, socket) || socket.isDead || socket.hp <= 0)) continue;
                            console.log(`[🔬 SDG] parsed.slice(0,15)=${JSON.stringify(parsed.slice(0,15))} hits=${hits.length}`);

                            // Decodifica a ID da arma/herói e o shootname correspondente usando a base64 real
                            const rawWeaponId = parsed[14];
                            const rawShootname = parsed[1];
                            
                            let resolvedWeaponId = rawWeaponId ? utils.decodeInt(rawWeaponId) : null;
                            let resolvedShootname = rawShootname ? utils.decodeInt(rawShootname).toString() : null;

                            // Fallback robusto se resolvedWeaponId não for encontrado na base de dados
                            if (!resolvedWeaponId || !weaponsMap[resolvedWeaponId]) {
                                const attackerSocket = activeSockets.find(s => sceneScope.canReceive(socket, s) && !s.destroyed && (s.playerId === playerId || String(s.playerId) === String(playerId)));
                                if (attackerSocket && attackerSocket.currentHeroId) {
                                    resolvedWeaponId = attackerSocket.currentHeroId;
                                }
                            }

                            if (!resolvedShootname) {
                                resolvedShootname = "1";
                            }

                            const weaponId = resolvedWeaponId;
                            const shootname = resolvedShootname;
                            const weaponData = weaponsMap[weaponId] ? weaponsMap[weaponId][shootname] : null;
                            let baseDamage = 100;
                            let canHeadshot = false;

                            if (weaponData) {
                                baseDamage = weaponData.damage;
                                canHeadshot = weaponData.damageHead === 1;
                            } else if (weaponsMap[weaponId]) {
                                // Fallback para o primeiro shootname disponível se o específico não constar na XML
                                const availableShootnames = Object.keys(weaponsMap[weaponId]).filter(k => k !== 'cartridgePublic');
                                if (availableShootnames.length > 0) {
                                    const fallbackData = weaponsMap[weaponId][availableShootnames[0]];
                                    baseDamage = fallbackData.damage;
                                    canHeadshot = fallbackData.damageHead === 1;
                                 }
                             }

                             const launcherNo = parsed[4] !== undefined ? utils.decodeInt(parsed[4]) : 0;

                             

                             // Shogun (1024) Arrow scaling
                             if (weaponId === 1024 && shootname === "1") {
                                 if (launcherNo === 1) {
                                     // Flecha de Repulsão / Salto (Skill E): Dano fixo de 125, dano em área com repulsão, sem headshot
                                     baseDamage = 125;
                                     canHeadshot = false;
                                     console.log(`[🏹 SHOGUN REPULSION ARROW] launcherNo: 1 | baseDamage fixo: 125 (sem escalonamento de carga)`);
                                 } else if (launcherNo === 0) {
                                     // Flecha Normal: Escala com o tempo de puxada do arco (150 a 900 de dano)
                                     const chargeMs = (typeof parsed[5] === 'number') ? parsed[5] : (parsed[5] ? Number(parsed[5]) : 0);
                                     baseDamage = 150 + Math.round((900 - 150) * Math.min(1.0, Math.max(0.0, chargeMs / 1000.0)));
                                     canHeadshot = true;
                                     console.log(`[🏹 SHOGUN CHARGE] chargeMs: ${chargeMs} | baseDamage escalado: ${baseDamage}`);
                                 }
                             }

                                                          // Deduplica os acertos por alvo para evitar dano excessivo causado por múltiplos colidores no mesmo projétil/explosão.
                             // Prioriza acertos na cabeça (boomHead === 1).
                             // Deduplica os acertos por alvo para evitar dano excessivo causado por múltiplos colidores no mesmo projétil/explosão.
                             // Prioriza acertos na cabeça (boomHead === 1).
                             const uniqueHits = [];
                             const seenTargets = new Set();
                             for (const dmg of hits) {
                                 const rawTargetId = dmg[1];
                                 const rawBoomHead = dmg[2];
                                 const targetId = rawTargetId !== undefined ? utils.decodeInt(rawTargetId) : null;
                                 const boomHead = rawBoomHead !== undefined ? utils.decodeInt(rawBoomHead) : 0;
                                 if (targetId && boomHead === 1) {
                                     if (!seenTargets.has(targetId)) {
                                         uniqueHits.push(dmg);
                                         seenTargets.add(targetId);
                                     }
                                 }
                             }
                             for (const dmg of hits) {
                                 const rawTargetId = dmg[1];
                                 const targetId = rawTargetId !== undefined ? utils.decodeInt(rawTargetId) : null;
                                 if (targetId && !seenTargets.has(targetId)) {
                                     uniqueHits.push(dmg);
                                     seenTargets.add(targetId);
                                 }
                             }
                             
                             console.log(`[🔬 SDG DIAG] playerId=${playerId} | weaponId=${weaponId} | sn=${shootname} | baseDamage=${baseDamage} | hits=${hits.length} | uniqueHits=${uniqueHits.length} | raw14=${parsed[14]} | raw1=${parsed[1]}`);
                             for (const dmg of uniqueHits) {
                                 const rawAttackerId = dmg[0];
                                 const rawTargetId = dmg[1];
                                 const rawBoomHead = dmg[2];
                                 const rawDistance = dmg[3];
                                 const collName = dmg[8];
                                 const rawForceV3 = dmg[6];
                                 const rawForceMax = dmg[7];
                                 let clientForceDir = null;
                                 if (Array.isArray(rawForceV3) && rawForceV3.length >= 3) {
                                     const fx = utils.decodeInt(rawForceV3[0]);
                                     const fy = utils.decodeInt(rawForceV3[1]);
                                     const fz = utils.decodeInt(rawForceV3[2]);
                                     if (fx !== 0 || fy !== 0 || fz !== 0) {
                                         clientForceDir = [fx * 0.01, fy * 0.01, fz * 0.01];
                                     }
                                 }
                                 const clientForceVal = rawForceMax !== undefined ? utils.decodeInt(rawForceMax) : 0;

                                 const attackerId = rawAttackerId !== undefined ? utils.decodeInt(rawAttackerId) : null;
                                 const targetId = rawTargetId !== undefined ? utils.decodeInt(rawTargetId) : null;
                                 const boomHead = rawBoomHead !== undefined ? utils.decodeInt(rawBoomHead) : 0;
                                 const distance = rawDistance !== undefined ? utils.decodeInt(rawDistance) : 0;
                                 
                                 // Diagnóstico detalhado de cada hit
                                 const _dbgPlayerTarget = activeSockets.find(s => sceneScope.canReceive(socket, s) && !s.destroyed && (s.playerId === targetId || String(s.playerId) === String(targetId)));
                                 const _dbgNpcTarget = spawnedNpcs[targetId] || spawnedNpcs[String(targetId)];
                                 const _dbgAttackerNpc = spawnedNpcs[attackerId] || spawnedNpcs[String(attackerId)];
                                 console.log(`[🔍 HIT DETAIL] raw=[${rawAttackerId},${rawTargetId}] decoded=attackerId:${attackerId} targetId:${targetId} boomHead:${boomHead} dist:${distance} | playerTarget:${!!_dbgPlayerTarget} npcTarget:${!!_dbgNpcTarget} attackerNpc:${!!_dbgAttackerNpc} | spawnedNpcKeys=[${Object.keys(spawnedNpcs).slice(0,10).join(',')}]`);
                                 
                                 let loopWeaponId = weaponId;
                                 let loopShootname = shootname;
                                 let loopWeaponData = weaponData;
                                 let loopBaseDamage = baseDamage;
                                 let loopCanHeadshot = canHeadshot;

                                 const attackerNpc = spawnedNpcs[attackerId] || spawnedNpcs[String(attackerId)] || (global.npcHistory ? global.npcHistory[attackerId] || global.npcHistory[String(attackerId)] : null);
                                 if (attackerNpc) {
                                     let npcWeaponOverride = attackerNpc.weaponIndex || null;
                                     if (!npcWeaponOverride) {
                                         if (attackerNpc.index === 1003 || String(attackerNpc.index) === "1003") {
                                             npcWeaponOverride = 9995;
                                         } else if (attackerNpc.index === 1005 || String(attackerNpc.index) === "1005") {
                                             npcWeaponOverride = 9996;
                                         } else if (attackerNpc.index === 1011 || String(attackerNpc.index) === "1011") {
                                             npcWeaponOverride = 9992;
                                         } else if (attackerNpc.index === 1012 || String(attackerNpc.index) === "1012") {
                                             npcWeaponOverride = 9980;
                                         } else if (attackerNpc.index === 1025 || String(attackerNpc.index) === "1025") {
                                             npcWeaponOverride = 9984;
                                         } else if (attackerNpc.index === 1001 || String(attackerNpc.index) === "1001") {
                                             npcWeaponOverride = 9979;
                                         } else if (attackerNpc.index === 1010 || String(attackerNpc.index) === "1010") {
                                             npcWeaponOverride = 9991;
                                         } else if (attackerNpc.index === 1013 || String(attackerNpc.index) === "1013") {
                                             npcWeaponOverride = 9994;
                                         } else if (attackerNpc.index === 1014 || String(attackerNpc.index) === "1014") {
                                             npcWeaponOverride = 9993;
                                         }
                                     }

                                     if (npcWeaponOverride) {
                                         loopWeaponId = npcWeaponOverride;
                                         loopShootname = "1";
                                         loopWeaponData = weaponsMap[loopWeaponId] ? weaponsMap[loopWeaponId][loopShootname] : null;
                                         if (loopWeaponData) {
                                             loopBaseDamage = loopWeaponData.damage;
                                             loopCanHeadshot = loopWeaponData.damageHead === 1;
                                         }
                                     }
                                 }

                                 // 🪤 JOHNNY TRAP (NPC 1021 / Arma 9988) - Timer de 6s e Rate Limit de 660ms
                                  if (loopWeaponId === 9988 || (attackerNpc && (attackerNpc.index === 1021 || String(attackerNpc.index) === "1021"))) {
                                      if (attackerNpc) {
                                           if (!attackerNpc.isActivated) {
                                               attackerNpc.isActivated = true;
                                               console.log(`[🪤 JOHNNY TRAP ACTIVATED] Trap ${attackerId} iniciou contagem de 6000ms para autodestruição!`);

                                               // 🌀 Sincroniza animação de giro para TODOS os clientes da sala (especialmente inimigos)
                                               const ncasPacket = `e${utils.encodeInt(attackerId)}:NCAS,()\n`;
                                               const caPacket = `e${utils.encodeInt(attackerId)}:CA,(('Attack',0,1),1)\n`;
                                               activeSockets.forEach(s => {
                                                   if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                                       s.write(ncasPacket);
                                                       s.write(caPacket);
                                                   }
                                               });

                                               setTimeout(() => {
                                                  if (spawnedNpcs[attackerId]) {
                                                      delete spawnedNpcs[attackerId];
                                                      const destPacket = `d${utils.encodeInt(attackerId)}\n`;
                                                      activeSockets.forEach(s => {
                                                          if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                                              s.write(destPacket);
                                                          }
                                                      });
                                                      console.log(`[🪤 JOHNNY TRAP EXPIRED] Trap ${attackerId} completou 6000ms e se autodestruiu.`);
                                                  }
                                              }, 6000);
                                          }

                                          const now = Date.now();
                                          if (!attackerNpc.lastHitTimes) attackerNpc.lastHitTimes = {};
                                          const lastHit = attackerNpc.lastHitTimes[targetId] || 0;
                                          if (now - lastHit < 660) {
                                              continue; // Ignora pacotes excedentes da taxa de quadros
                                          }
                                          attackerNpc.lastHitTimes[targetId] = now;
                                          loopBaseDamage = 150;
                                          console.log(`[🪤 JOHNNY TRAP TICK] Trap ${attackerId} aplicou 150 de dano ao alvo ${targetId} (intervalo: ${now - lastHit}ms)`);
                                      }
                                  }

                                  // 🏹 Trava de detonação única para Flecha Explosiva do Shogun (9995 / NPC 1003)
                                 if (loopWeaponId === 9995 || (attackerNpc && (attackerNpc.index === 1003 || String(attackerNpc.index) === "1003"))) {
                                     if (attackerNpc && attackerNpc.hasExploded) {
                                         console.log(`[🚫 SHOGUN ARROW] NPC ${attackerId} já explodiu. Ignorando dano repetido.`);
                                         continue;
                                     }
                                     if (attackerNpc) {
                                         attackerNpc.hasExploded = true;
                                         delete spawnedNpcs[attackerId];
                                         const destPacket = `d${utils.encodeInt(attackerId)}\n`;
                                         activeSockets.forEach(s => {
                                             if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                                 s.write(destPacket);
                                             }
                                         });
                                         console.log(`[💥 SHOGUN ARROW DETONATION] Flecha explosiva ${attackerId} explodiu com sucesso (600 de dano)!`);
                                     }
                                 }

                                 console.log(`[💥 HIT] Atacante: ${attackerId} (raw: ${rawAttackerId}) | Alvo: ${targetId} (raw: ${rawTargetId}) | WeaponId: ${loopWeaponId} | Shootname: ${loopShootname} | Parte: ${collName} | Headshot: ${boomHead === 1}`);
                                let target = null;
                                let isPlayerTarget = false;
                                
                                // 1. Procurar em activeSockets para PvP e suporte a hits em outros jogadores
                                let foundPlayer = activeSockets.find(s => sceneScope.canReceive(socket, s) && !s.destroyed && (s.playerId === targetId || String(s.playerId) === String(targetId)));
                                if (foundPlayer) {
                                    target = foundPlayer;
                                    isPlayerTarget = true;
                                } else if (state.activeBots && state.activeBots[targetId]) {
                                    target = state.activeBots[targetId];
                                    isPlayerTarget = true; // Trata o bot como jogador para fins de HP e eliminação
                                } else if (spawnedNpcs[targetId]) {
                                    target = spawnedNpcs[targetId];
                                }

                                    if (target) {
                                        if (!sceneScope.sameScene(socket, target)) continue;

                                        if (target.isDead) {
                                        console.log(`[🛡️ HIT BLOCKED] Alvo ${targetId} está morto e não pode ser afetado.`);
                                        continue;
                                    }
                                    // Resolvendo o Hero ID do atacante (suportando jogadores ativos, NPCs e bots)
                                    let attackerSocket = activeSockets.find(s => sceneScope.canReceive(socket, s) && !s.destroyed && (s.playerId === attackerId || String(s.playerId) === String(attackerId)));
                                    if (!attackerSocket && state.activeBots && state.activeBots[attackerId]) {
                                        attackerSocket = state.activeBots[attackerId];
                                        if (attackerSocket.isPveMonster && !gameLogic.pveOwnsBot(socket, attackerSocket)) continue;
                                    }
                                    let attackerHeroId = attackerSocket ? attackerSocket.currentHeroId : null;
                                    if (!attackerHeroId && spawnedNpcs[attackerId]) {
                                        const masterId = spawnedNpcs[attackerId].masterId;
                                        const masterSocket = activeSockets.find(s => sceneScope.canReceive(socket, s) && !s.destroyed && (s.playerId === masterId || String(s.playerId) === String(masterId)));
                                        if (masterSocket) {
                                            attackerHeroId = masterSocket.currentHeroId;
                                        }
                                    }

                                    // Calcula as facções/times
                                    const attackerForce = attackerSocket ? (attackerSocket.force !== undefined ? attackerSocket.force : 1) : (attackerNpc ? (attackerNpc.force !== undefined ? attackerNpc.force : 1) : 1);
                                    const targetForce = isPlayerTarget ? (target.force !== undefined ? target.force : 1) : (target ? (target.force !== undefined ? target.force : 2) : 2);

                                    // Verifica se é Cura da Venus (Zona de cura)
                                    const isVenusHeal = (
                                        (attackerNpc && (attackerNpc.index === 1007 || String(attackerNpc.index) === "1007")) ||
                                        loopWeaponId === 1007 || String(loopWeaponId) === "1007" ||
                                        resolvedWeaponId === 1007 || String(resolvedWeaponId) === "1007" ||
                                        ((loopWeaponId === 1032 || String(loopWeaponId) === "1032") && String(loopShootname) === "3")
                                    );
                                    // Verifica se é Cura da Lily (Bola de cura)
                                    const isLilyHeal = (
                                        (attackerNpc && (attackerNpc.index === 1023 || String(attackerNpc.index) === "1023")) ||
                                        ((loopWeaponId === 1023 || String(loopWeaponId) === "1023") && (attackerHeroId === 1033 || String(attackerHeroId) === "1033")) ||
                                        ((loopWeaponId === 1033 || String(loopWeaponId) === "1033") && String(loopShootname) === "3") ||
                                        resolvedWeaponId === 1023 || String(resolvedWeaponId) === "1023"
                                    );

                                    if (isVenusHeal) {
                                         const healAmount = 60;
                                         target.hp = Math.min(target.maxHp || 2000, (target.hp !== undefined ? target.hp : (target.maxHp || 2000)) + healAmount);
                                         gameLogic.sendEntityHpUpdate(target, targetId, target, isPlayerTarget);
                                         if (attackerSocket && healAmount > 0) {
                                             attackerSocket.matchHeal = (attackerSocket.matchHeal || 0) + healAmount;
                                             gameLogic.sendTabScoreboard(attackerSocket);
                                         }
                                         console.log(`[💖 VENUS HEAL] Curou o Alvo: ${targetId} em +${healAmount} HP. Novo HP: ${target.hp}`);
                                         continue;
                                     }

                                     if (isLilyHeal) {
                                          if (combat.sameSide(attackerSocket || attackerNpc, target, Number(attackerForce) === Number(targetForce))) {
                                              const isShield = ((loopWeaponId === 1033 || String(loopWeaponId) === "1033") && String(loopShootname) === "3");
                                              const healAmount = isShield ? 300 : 250;
                                              target.hp = Math.min(target.maxHp || 2000, (target.hp !== undefined ? target.hp : (target.maxHp || 2000)) + healAmount);
                                              gameLogic.sendEntityHpUpdate(target, targetId, target, isPlayerTarget);
                                              if (attackerSocket && healAmount > 0) {
                                                  attackerSocket.matchHeal = (attackerSocket.matchHeal || 0) + healAmount;
                                                  gameLogic.sendTabScoreboard(attackerSocket);
                                              }
                                              console.log(`[💖 LILY HEAL] Curou o Aliado: ${targetId} em +${healAmount} HP (${isShield ? 'Escudo Mágico' : 'Poção'}). Novo HP: ${target.hp}`);
                                              continue;
                                          } else {
                                              // Se for o escudo da Lily atravessando um inimigo, causa 5 de dano
                                              if ((loopWeaponId === 1033 || String(loopWeaponId) === "1033") && String(loopShootname) === "3") {
                                                  loopBaseDamage = 5;
                                                  finalDamage = 5;
                                                  console.log(`[🛡️ LILY SHIELD ENEMY HIT] Escudo da Lily atingiu o inimigo ${targetId}! Causando 5 de dano.`);
                                              }
                                          }
                                      }

                                    // Verifica se é Cura da Guanyin (Healing Beam - shootname 1)
                                    const isLamsterWeapon = Number(loopWeaponId) === 2000 || Number(resolvedWeaponId) === 2000;
                                    if (isLamsterWeapon) {
                                        if (!lamster.isLamsterWeapon(attackerSocket, loopWeaponId, loopShootname)) continue;
                                        if (['1','2'].includes(String(loopShootname)) && combat.sameSide(attackerSocket, target)) {
                                            const result = isPlayerTarget ? lamster.healAlly(attackerSocket, target, loopShootname) : null;
                                            if (result && String(loopShootname)==='2') gameLogic.applyBuffToEntity(attackerSocket, targetId, 1007);
                                            if (result && result.healed > 0) {
                                                gameLogic.sendEntityHpUpdate(target, targetId, target, true);
                                                gameLogic.applyBuffToEntity(attackerSocket, targetId, lamsterHero.BUFFS.heal);
                                                if (result.selfHealed > 0) {
                                                    gameLogic.sendEntityHpUpdate(attackerSocket, attackerId, attackerSocket, true);
                                                    gameLogic.applyBuffToEntity(attackerSocket, attackerId, lamsterHero.BUFFS.heal);
                                                }
                                                attackerSocket.matchHeal = (attackerSocket.matchHeal || 0) + result.healed + result.selfHealed;
                                                // Charge only actual allied HP restored; missing HP
                                                // and shared self-healing cannot farm the Q meter.
                                                const cureRate = db.heroDazhaoMap[2000]?.[0]?.energyAddCure || 120;
                                                addPlayerUltEnergy(attackerSocket, result.healed * cureRate / 200);
                                                gameLogic.sendTabScoreboard(attackerSocket);
                                            }
                                            continue; // Aliados recebem cura; secundário também aplica inspiração.
                                        }
                                    }
                                    if (isLamsterWeapon && !lamster.canStrikeEnemy(attackerSocket, target, loopShootname)) {
                                        if (String(loopShootname)==='2') console.log('[LAMSTER SECONDARY REJECT] '+JSON.stringify({attackerId,targetId,force:attackerSocket?.force,targetForce:target.force,ready:attackerSocket?.isCombatReady,hp:attackerSocket?.hp,targetHp:target.hp,source:[attackerSocket?.lastX,attackerSocket?.lastY,attackerSocket?.lastZ],target:[target.lastX ?? target.x,target.lastY ?? target.y,target.lastZ ?? target.z]}));
                                        continue;
                                    }
                                    const isGuanyinHeal = (
                                        (Number(loopWeaponId) === 1019 || Number(resolvedWeaponId) === 1019) && String(loopShootname) === "1"
                                    );

                                    if (isGuanyinHeal) {
                                        const healAmount = 80;
                                        target.hp = Math.min(target.maxHp || 2000, (target.hp !== undefined ? target.hp : (target.maxHp || 2000)) + healAmount);
                                        gameLogic.sendEntityHpUpdate(target, targetId, target, isPlayerTarget);
                                        if (attackerSocket && healAmount > 0) {
                                            attackerSocket.matchHeal = (attackerSocket.matchHeal || 0) + healAmount;
                                            gameLogic.sendTabScoreboard(attackerSocket);
                                        }
                                        console.log(`[💖 GUANYIN HEAL] Curou o Alvo: ${targetId} em +${healAmount} HP. Novo HP: ${target.hp}`);
                                        continue;
                                    }



                                    // 0. Detecção de Armas/Ataques Melee (Nenhum falloff em área ou distância)
                                    const isMeleeAttack = (
                                        loopWeaponId === 1006 || loopWeaponId === 1035 || loopWeaponId === 1028 ||
                                        Number(attackerHeroId) === 1006 || Number(attackerHeroId) === 1035 || Number(attackerHeroId) === 1028 ||
                                        (loopWeaponData && (loopWeaponData.damageType === 1 || String(loopWeaponData.damageType) === "1" || loopWeaponData.shootType === 1 || String(loopWeaponData.shootType) === "1"))
                                    );

                                    let finalDamage = loopBaseDamage;

                                    // 💥 LOKI (1028) — ARMA 2 (RETALIAÇÃO / DAMAGE REBOUND)
                                    if ((loopWeaponId === 1028 || Number(attackerHeroId) === 1028) && String(loopShootname) === "2") {
                                        const absorbed = socket.lokiAbsorbedDamage || 0;
                                        finalDamage = loopBaseDamage + absorbed;
                                        console.log(`[💥 LOKI DAMAGE REBOUND] Disparou retaliação! Base: ${loopBaseDamage} + Absorvido: ${absorbed} = ${finalDamage}`);
                                        socket.lokiAbsorbedDamage = 0;
                                        socket.lokiShieldActive = false;
                                    }

                                    // 1. Aplica Falloff Oficial de Distância da Tabela XML (dampBegin / dampEnd / dampParm)
                                    if (!isMeleeAttack && loopWeaponData && loopWeaponData.dampBegin > 0 && loopWeaponData.dampEnd > 0) {
                                        const distVal = Number(distance || 0);
                                        const begin = loopWeaponData.dampBegin;
                                        const end = loopWeaponData.dampEnd;
                                        const parm = loopWeaponData.dampParm / 100.0;
                                        
                                        let distFalloff = 1.0;
                                        if (distVal <= begin) {
                                            distFalloff = 1.0;
                                        } else if (distVal >= end) {
                                            distFalloff = parm;
                                        } else {
                                             const ratio = (distVal - begin) / (end - begin);
                                             distFalloff = 1.0 - ratio * (1.0 - parm);
                                        }
                                        const prevDmg = finalDamage;
                                        finalDamage = finalDamage * distFalloff;
                                        if (distFalloff < 0.999) {
                                            console.log(`[🎯 DISTANCE FALLOFF OFICIAL] Weapon: ${loopWeaponId} (sn:${loopShootname}) | Dist: ${(distVal/100).toFixed(1)}m (raw:${distVal}) | Base: ${prevDmg} -> Falloff: ${Math.round(finalDamage)} (${Math.round(distFalloff*100)}%)`);
                                        }
                                    }

                                    // 2. Aplica Falloff de Splash / AoE APENAS para armas com boomdamagetype > 0 no XML (Medusa, David, Glider, LeiZhenZi, etc.)
                                    // Armas com boomdamagetype = 0 (Dragos, Loki, Shogun) NÃO sofrem falloff de splash e causam 100% de dano!
                                    if (!isMeleeAttack && loopWeaponData && loopWeaponData.boomType > 0 && loopWeaponData.boomRange > 0) {
                                        const distVal = Number(distance || 0);
                                        const radius = loopWeaponData.boomRange / 10.0; // Raio em cm (ex: 5000 / 10 = 500cm para Medusa)
                                        
                                        let aoeFalloff = 1.0;
                                        if (distVal <= 30) {
                                            // Impacto direto / centro da explosão
                                            aoeFalloff = 1.0;
                                        } else if (distVal >= radius) {
                                            // Borda máxima da explosão
                                            aoeFalloff = 0.50;
                                        } else {
                                            // Decaimento linear do centro (100%) até a borda (50%)
                                            aoeFalloff = 1.0 - (distVal / radius) * 0.50;
                                        }
                                        const prevDmg = finalDamage;
                                        finalDamage = finalDamage * aoeFalloff;
                                        if (aoeFalloff < 0.999) {
                                            console.log(`[💥 AOE SPLASH FALLOFF (boomType:${loopWeaponData.boomType})] Weapon: ${loopWeaponId} (sn:${loopShootname}) | Dist do Epicentro: ${(distVal/100).toFixed(2)}m / ${(radius/100).toFixed(2)}m | Base: ${prevDmg} -> Splash: ${Math.round(finalDamage)} (${Math.round(aoeFalloff*100)}%)`);
                                        }
                                    }

                                    let damageAmount = finalDamage;
                                    if (boomHead === 1 && loopCanHeadshot) {
                                        damageAmount = Math.round(finalDamage * 2.0); // Multiplicador oficial de 2.0x para headshots
                                    } else {
                                        damageAmount = Math.round(finalDamage);
                                    }

                                    // Inspiração1007: bônus ofensivo nativo; pulso simbólico mantém250.
                                    damageAmount = lamsterHero.reduceDamage(target, damageAmount);
                                    if(attackerSocket?.activeBuffs?.[1007] && !(Number(loopWeaponId)===2000 && String(loopShootname)==='2')) damageAmount=Math.round(damageAmount*1.25);

                                    // 🔮 Multiplicador de Debuff do Beacon da Carmen (Buff 1010 ou 1005: +20% dano no alvo marcado)
                                    if (target && target.activeBuffs && (target.activeBuffs[1010] || target.activeBuffs[1005])) {
                                        const prevDmg = damageAmount;
                                        damageAmount = Math.round(damageAmount * 1.20);
                                        console.log(`[🔮 CARMEN BEACON DEBUFF] Alvo ${targetId} com Beacon (1010/1005)! Dano amplificado: ${prevDmg} -> ${damageAmount} (+20%)`);
                                    }



                                    
                                    // 🩸 KHAN (1034) — PASSIVA BLOOD ANGER (+1% de dano por ponto de BA, ou ~0.45 por ponto)
                                    const isKhanAttacker = Boolean(
                                        (attackerSocket && (attackerSocket.currentHeroId === 1034 || Number(attackerHeroId) === 1034)) ||
                                        loopWeaponId === 1034 || Number(resolvedWeaponId) === 1034
                                    );
                                    if (isKhanAttacker && attackerSocket) {
                                        const ba = attackerSocket.bloodAnger || 0;
                                        if (ba > 0) {
                                            const prevDmg = damageAmount;
                                            const baMultiplier = 1 + (ba * 0.01);
                                            damageAmount = Math.round(damageAmount * baMultiplier);
                                            console.log(`[🩸 KHAN BLOOD ANGER BONUS] Khan ${attackerId} com ${ba} BA! Dano amplificado: ${prevDmg} -> ${damageAmount} (+${ba}%)`);
                                        }
                                    }
// ⚡ Multiplicador e Redução da Ultimate do Nezha (1035: Great Spirit / Superman Powers)
                                    const isNezhaAttackerUlt = Boolean(
                                        attackerSocket &&
                                        (attackerSocket.currentHeroId === 1035 || Number(attackerHeroId) === 1035) &&
                                        (attackerSocket.isNezhaUltActive || (attackerSocket.activeBuffs && (attackerSocket.activeBuffs[1049] || attackerSocket.activeBuffs[1050] || attackerSocket.activeBuffs[1051])))
                                    );
                                    if (isNezhaAttackerUlt) {
                                        const prevDmg = damageAmount;
                                        damageAmount = Math.round(damageAmount * 1.50); // +50% dano APENAS durante a Ult
                                        console.log(`[⚡ NEZHA ULTIMATE ATTACK BUFF] Nezha ${attackerId} causou dano aumentado: ${prevDmg} -> ${damageAmount} (+50%)`);
                                    }

                                    const isNezhaTargetUlt = Boolean(
                                        target &&
                                        (target.currentHeroId === 1035 || target.heroId === 1035) &&
                                        (target.isNezhaUltActive || (target.activeBuffs && (target.activeBuffs[1049] || target.activeBuffs[1050] || target.activeBuffs[1051])))
                                    );
                                    if (isNezhaTargetUlt) {
                                        const prevDmg = damageAmount;
                                        damageAmount = Math.round(damageAmount * 0.60); // Redução de dano de 40% APENAS durante a Ult
                                        console.log(`[⚡ NEZHA ULTIMATE DAMAGE REDUCTION] Nezha ${targetId} reduziu dano recebido: ${prevDmg} -> ${damageAmount} (-40%)`);
                                    }

                                    // 🤺 Multiplicador e Redução da Ultimate do Vincent (1030: Duel / Buff 1033 / 1039 / 1040)
                                    const isVincentAttackerUlt = Boolean(
                                        attackerSocket &&
                                        (attackerSocket.currentHeroId === 1030 || Number(attackerHeroId) === 1030) &&
                                        (attackerSocket.isVincentUltActive || (attackerSocket.activeBuffs && (attackerSocket.activeBuffs[1033] || attackerSocket.activeBuffs[1039] || attackerSocket.activeBuffs[1040])))
                                    );
                                    if (isVincentAttackerUlt) {
                                        const prevDmg = damageAmount;
                                        damageAmount = Math.round(damageAmount * 1.50); // +50% dano geral (armas e dashs) durante a Ult Duel
                                        console.log(`[🤺 VINCENT ULTIMATE DUEL BUFF] Vincent ${attackerId} causou dano aumentado (+50%): ${prevDmg} -> ${damageAmount}`);
                                    }

                                    const isVincentTargetUlt = Boolean(
                                        target &&
                                        (target.currentHeroId === 1030 || target.heroId === 1030) &&
                                        (target.isVincentUltActive || (target.activeBuffs && (target.activeBuffs[1033] || target.activeBuffs[1039] || target.activeBuffs[1040])))
                                    );
                                    if (isVincentTargetUlt) {
                                        const prevDmg = damageAmount;
                                        damageAmount = Math.round(damageAmount * 0.60); // Redução de dano de 40% durante a Ult
                                        console.log(`[🤺 VINCENT ULTIMATE MITIGATION] Vincent ${targetId} reduziu dano recebido (-40%): ${prevDmg} -> ${damageAmount}`);
                                    }

                                    
                                    // 🐍 Redução de Dano da Ultimate da Medusa / Gaya (1036: Gorgon / Buff 1055: -80% de dano recebido)
                                    const isMedusaTargetUlt = Boolean(
                                        target &&
                                        (target.currentHeroId === 1036 || target.heroId === 1036) &&
                                        (target.isMedusaUltActive || (target.activeBuffs && target.activeBuffs[1055]))
                                    );
                                    if (isMedusaTargetUlt) {
                                        const prevDmg = damageAmount;
                                        damageAmount = Math.max(1, Math.round(damageAmount * 0.20)); // Redução de 80% de dano durante a Ult Gorgon
                                        console.log(`[🐍 MEDUSA GORGON MITIGATION] Medusa ${targetId} reduziu dano recebido (-80%): ${prevDmg} -> ${damageAmount}`);
                                    }

                                    // 🌪️ Jack (1027) Dash Stun (Skill 1102 / Sandstorm Dash)
                                    if ((loopWeaponId === 1027 || Number(attackerHeroId) === 1027) && (String(loopShootname) === "3" || loopWeaponId === 1102)) {
                                        gameLogic.applyBuffToEntity(socket, targetId, 1024, 1000); // 1s Stun
                                        console.log(`[🌪️ JACK SANDSTORM DASH STUN] Inimigo ${targetId} atordoado por 1s!`);
                                    }

                                    // Glider (1005) Ultimate dynamic lock-on damage scaling
                                    if (loopWeaponId === 1005 && loopShootname === "6") {
                                        let elapsedSec = 0;
                                        if (attackerSocket && attackerSocket.ultimateStart) {
                                            elapsedSec = (Date.now() - attackerSocket.ultimateStart) / 1000;
                                        }
                                        const chargeDamage = Math.min(4200, Math.round(elapsedSec * 700));
                                        const targetHp = target.hp !== undefined ? target.hp : (target.maxHp || 2000);
                                        if (chargeDamage >= targetHp) {
                                            damageAmount = 4200;
                                        } else {
                                            damageAmount = chargeDamage;
                                        }
                                        console.log(`[🚀 GLIDER ULTIMATE HIT] targetId: ${targetId} | targetHp: ${targetHp} | chargeDamage: ${chargeDamage} | final damageAmount: ${damageAmount}`);
                                    }

                                    const isAngelActiveHeal = (
                                        (loopWeaponId === 1029 || String(loopWeaponId) === "1029") &&
                                        (String(loopShootname) === "3" || String(loopShootname) === "4")
                                    );

                                    if (isAngelActiveHeal) {
                                        if (combat.sameSide(attackerSocket || attackerNpc, target, Number(attackerForce) === Number(targetForce))) {
                                            const healAmount = String(loopShootname) === "3" ? 400 : 75;
                                            target.hp = Math.min(target.maxHp || 2000, (target.hp !== undefined ? target.hp : (target.maxHp || 2000)) + healAmount);
                                            gameLogic.sendEntityHpUpdate(target, targetId, target, isPlayerTarget);
                                            if (attackerSocket && healAmount > 0) {
                                                attackerSocket.matchHeal = (attackerSocket.matchHeal || 0) + healAmount;
                                                gameLogic.sendTabScoreboard(attackerSocket);
                                                const attHero = attackerSocket.currentHeroId || 1029;
                                                const dzList = db.heroDazhaoMap[attHero] || [];
                                                const cureMult = (dzList.length > 0 && dzList[0].energyAddCure) ? dzList[0].energyAddCure : 120;
                                                const cureEnergy = Math.round((healAmount * cureMult) / 200);
                                                addPlayerUltEnergy(attackerSocket, cureEnergy);
                                            }
                                            console.log(`[💖 ANGEL ACTIVE HEAL] Curou o Aliado: ${targetId} em +${healAmount} HP (Shootname: ${loopShootname}). Novo HP: ${target.hp}`);
                                            continue;
                                        } else {
                                            console.log(`[💥 ANGEL ACTIVE DAMAGE] Bola de cura atingiu Inimigo: ${targetId} | WeaponId: ${loopWeaponId} | Shootname: ${loopShootname}. Causando dano.`);
                                        }
                                    }

                                      // 1. Prevenção de Fogo Amigo (Teammate Hit Prevention) em Partidas de Equipe
                                      console.log(`[🔎 PRE-FF] attackerId=${attackerId} attackerSocket=${!!attackerSocket} attackerNpc=${attackerNpc?attackerNpc.index:'null'} attackerForce=${attackerForce} | targetId=${targetId} targetForce=${targetForce} | damageAmount=${damageAmount} loopBaseDamage=${loopBaseDamage} loopWeaponId=${loopWeaponId} loopShootname=${loopShootname} loopWeaponData=${JSON.stringify(loopWeaponData)}`);
                                      
                                      // Exceção: Shogun Flecha de Repulsão disparada nos próprios pés para Auto-Propulsão (Rocket Jump / Movimentação)
                                      const isShogunSelfJump = (
                                          Number(attackerId) === Number(targetId) &&
                                          (Number(loopWeaponId) === 1024 || Number(resolvedWeaponId) === 1024) &&
                                          Number(launcherNo) === 1
                                      );

                                      if (combat.sameSide(attackerSocket || attackerNpc, target, Number(attackerForce) === Number(targetForce)) && !isShogunSelfJump) {
                                          console.log(`[🛡️ FOGO AMIGO NEGADO] Alvo ${targetId} é aliado do Atacante ${attackerId} (Time ${attackerForce})! Dano bloqueado.`);
                                          continue;
                                      }
                                      if (isShogunSelfJump) {
                                          damageAmount = 0; // Não causa dano a si mesmo, apenas o impulso de salto
                                      }

                                     // 🛡️ LOKI (1028) — ESCUDO DE ABSORÇÃO & RETALIAÇÃO (Skill 1112)
                                      if (target && target.lokiShieldActive && Date.now() < (target.lokiShieldEnd || 0)) {
                                          target.lokiAbsorbedDamage = (target.lokiAbsorbedDamage || 0) + damageAmount;
                                          console.log(`[🛡️ LOKI SHIELD ABSORB] Loki ${targetId} absorveu ${damageAmount} de dano! Total acumulado: ${target.lokiAbsorbedDamage}`);
                                          
                                          // Atualiza a animação/medidor da arma do Loki no cliente via opcode 2050001 (LuoJiHurtLeiJi)
                                          try {
                                              const p2050001 = utils.encodeInt(2050001);
                                              const tIdEnc = utils.encodeInt(targetId);
                                              const sk1112Enc = utils.encodeInt(1112);
                                              const dmgAccEnc = utils.encodeInt(target.lokiAbsorbedDamage);
                                              target.write(`e${targetId}:SendMsgToClient,(${p2050001},(${tIdEnc},${sk1112Enc},${dmgAccEnc}))\n`);
                                          } catch (e) {
                                              // Silently ignore
                                          }
                                          continue; // Loki fica completamente invulnerável (dano = 0)!
                                      }

                                      // 2. Invencibilidade / God Mode
                                      const isTargetInvincible = target.godMode || target.isInvincible || (target.activeBuffs && (target.activeBuffs[1015] || target.activeBuffs[1045]));
                                       if (isTargetInvincible) {
                                          console.log(`[🛡️ HIT NEGADO] Alvo ${targetId} está invencível / no Casulo de Fogo (Buff 1015)!`);
                                          continue;
                                      }

                                      // 🪤 Invulnerabilidade da Claymore Mine do Johnny durante a rotação ativada (6s)
                                      if (target && (target.index === 1021 || String(target.index) === "1021") && target.isActivated) {
                                          console.log(`[🛡️ JOHNNY TRAP INVULNERABLE] Trap ${targetId} está girando ativada (6s) e é imune a danos! Dano bloqueado.`);
                                          continue;
                                      }

                                       // 🛡️ KHAN ASYLUM (Buff 1043) — Redirecionamento de 50% do dano para o Khan protetor
                                      if (target.activeBuffs && target.activeBuffs[1043] && target.khanProtectorSocket && !target.khanProtectorSocket.destroyed && !target.khanProtectorSocket.isDead) {
                                          const khanSocket = target.khanProtectorSocket;
                                          const khanId = target.khanProtectorId || khanSocket.playerId;
                                          const redirectedDmg = Math.round(damageAmount * 0.50);
                                          damageAmount = Math.max(1, damageAmount - redirectedDmg);
                                          
                                          console.log(`[🛡️ KHAN ASYLUM REDIRECTION] Alvo ${targetId} protegido! Dano dividido: ${damageAmount} no alvo, ${redirectedDmg} no Khan ${khanId}`);
                                          
                                          // Khan recebe os 50% de dano redirecionado
                                          if (!khanSocket.godMode && !khanSocket.isInvincible && !(khanSocket.activeBuffs && (khanSocket.activeBuffs[1015] || khanSocket.activeBuffs[1045]))) {
                                              khanSocket.hp = Math.max(0, (khanSocket.hp !== undefined ? khanSocket.hp : (khanSocket.maxHp || 2000)) - redirectedDmg);
                                              gameLogic.sendEntityDamageUpdate(khanId, khanSocket, redirectedDmg, attackerId, 0, loopWeaponId);
                                              if (khanSocket.hp <= 0) {
                                                  gameLogic.handlePlayerDeath(khanSocket, attackerSocket);
                                              }
                                          }
                                      }
                                     if (isPlayerTarget) {
                                          let place = 0;
                                           // Redução de Dano da Jaula da Eva (-25% de dano causado APENAS se o atacante estiver DENTRO da jaula da Eva)
                                           const attackerInsideEvaCage = isEntityInsideEvaCage(attackerSocket || attackerNpc, attackerSocket ? attackerSocket.force : (attackerNpc ? attackerNpc.force : 0));
                                           if (attackerInsideEvaCage) {
                                               const prevDmg = damageAmount;
                                               damageAmount = Math.max(1, Math.round(damageAmount * 0.75));
                                               console.log(`[⚡ EVA CAGE ATTACK REDUCTION] Atacante ${attackerId} DENTRO da Jaula da Eva! Dano reduzido: ${prevDmg} -> ${damageAmount} (-25%)`);
                                           }
                                          let originalDamage = damageAmount;
                                          const lamsterDamageCapacity = Math.max(0, target.hp || 0) + Math.max(0, target.shield || 0);
                                          let shieldAbsorbed = 0;
                                          // Registrar último momento em que sofreu danos para regeneração do Tagaloa
                                          target.lastDamageTime = Date.now();

                                          // ===== VERIFICAÇÃO DE HIT EM BARREIRA (entidade separada) =====
                                          // Barreiras são entidades independentes do HP do jogador.
                                          // Se o hit atingiu a barreira, o dano vai para o HP DA BARREIRA e NÃO afeta o jogador.
                                          const collNameStr = collName ? String(collName) : "";
                                          const isBarrierHit = collNameStr.includes("_dun") || collNameStr.includes("shieldbody");
                                          
                                          if (isBarrierHit) {
                                              const heroId = Number(target.currentHeroId);
                                              const barrierSpec = heroCatalog.getHeroBarrierSpec(heroId);
                                              let barrierHp = 0;
                                              let barrierMaxHp = barrierSpec ? barrierSpec.maxHp : 0;
                                              let barrierName = barrierSpec ? barrierSpec.name : "";
                                              
                                              // Determina qual barreira foi atingida
                                              if (heroId === 1006 && barrierSpec) {
                                                  barrierHp = (target.tagaloaShieldHp !== undefined) ? target.tagaloaShieldHp : barrierSpec.maxHp;
                                              } else if (heroId === 1032 && barrierSpec) {
                                                  barrierHp = (target.venusBarrierHp !== undefined) ? target.venusBarrierHp : 0;
                                                  barrierMaxHp = target.venusBarrierMaxHp || barrierSpec.maxHp;
                                              } else if ((heroId === 1033 || heroId === 1037) && barrierSpec) {
                                                  barrierHp = barrierSpec.maxHp;
                                              }
                                              
                                              if (barrierHp > 0) {
                                                  // Aplica dano NA BARREIRA (não no jogador!)
                                                  const barrierDmg = Math.min(barrierHp, damageAmount);
                                                  if (barrierSpec && barrierSpec.destructible) {
                                                      barrierHp -= barrierDmg;
                                                  }
                                                  
                                                  // Atualiza HP da barreira no socket
                                                  if (heroId === 1006) {
                                                        const p2010000 = utils.encodeInt(2010000);
                                                        const p2010002 = utils.encodeInt(2010002);
                                                        const brStateEnc = utils.encodeInt(barrierHp > 0 ? 1 : 0);
                                                        const brEnergyEnc = utils.encodeInt(barrierHp);
                                                        const brMaxEnergyEnc = utils.encodeInt(barrierMaxHp);
                                                        const brResp = "e" + targetId + ":SendMsgToClient,(" + p2010000 + ",(" + p2010002 + ",(" + brStateEnc + "," + brEnergyEnc + "," + brMaxEnergyEnc + ")))\n";
                                                        if (target && target.write) {
                                                            target.write(brResp);
                                                        }
                                                        target.tagaloaShieldHp = barrierHp;
                                                         if (barrierHp <= 0) {
                                                             target.tagaloaShieldActive = false;
                                                             const brPlayerIdEnc = utils.encodeInt(Number(target.playerIndex || targetId || 1));
                                                             const animJvDun = `e${targetId}:CA,((('JvDun',0,0),1,${brPlayerIdEnc}))\n`;
                                                             const animSheildState = `e${targetId}:CA,((('SheildState',2,0),1,${brPlayerIdEnc}))\n`;
                                                             activeSockets.forEach(s => {
                                                                 if (sceneScope.canReceive(socket, s) && !s.destroyed && s !== target) {
                                                                     s.write(animJvDun);
                                                                     s.write(animSheildState);
                                                                 }
                                                             });
                                                             console.log(`[💥 TAGALOA ESCUDO DESTRUÍDO] Barreira de ${targetId} quebrada (0 HP)! (Dano: ${barrierDmg})`);
                                                         }
                                                   } else if (heroId === 1032) {
                                                       target.venusBarrierHp = barrierHp;
                                                   } else if (heroId === 1033) {
                                                       target.lilyBarrierHp = barrierHp;
                                                   }
                                                  
                                                  console.log(`[🧱 BARREIRA ${barrierName}] Hit na barreira de ${targetId} (Parte: ${collNameStr}). Dano: ${barrierDmg}. HP da barreira: ${barrierHp}/${barrierMaxHp}`);
                                                  
                                                  // Broadcast atualização visual da barreira (2010002/2210001)
                                                    if (heroId === 1032) {
                                                       const p2010002 = utils.encodeInt(2010002);
                                                       const playerIdEnc = utils.encodeInt(targetId);
                                                       const typeEnc = utils.encodeInt(barrierHp > 0 ? 1 : 0);
                                                       const energyEnc = utils.encodeInt(barrierHp);
                                                       const maxEnergyEnc = utils.encodeInt(barrierMaxHp);
                                                       const pos = target.venusBarrierPos || [0, 0, 0];
                                                       const relesPos = target.venusBarrierRelesPos || [0, 0, 0];
                                                       const posEnc = "(" + utils.encodeInt(pos[0]) + "," + utils.encodeInt(pos[1]) + "," + utils.encodeInt(pos[2]) + ")";
                                                       const relesPosEnc = "(" + utils.encodeInt(relesPos[0]) + "," + utils.encodeInt(relesPos[1]) + "," + utils.encodeInt(relesPos[2]) + ")";
                                                       const resp = "e" + targetId + ":SendMsgToAllClient,(" + p2010002 + ",(" + playerIdEnc + ",(" + typeEnc + "," + energyEnc + "," + maxEnergyEnc + ")," + posEnc + "," + relesPosEnc + "))\n";
                                                       activeSockets.forEach(s => {
                                                           if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                                               s.write(resp);
                                                           }
                                                       });
                                                   } else if (heroId === 1033) {
                                                       const p2210001 = utils.encodeInt(2210001);
                                                       const targetIdEnc = utils.encodeInt(targetId);
                                                       const shieldIdEnc = utils.encodeInt(20000 + Number(targetId));
                                                       const hpEnc = utils.encodeInt(barrierHp);
                                                       const maxHpEnc = utils.encodeInt(barrierMaxHp);
                                                       const dateStr = "'2026-05-18 12:00:00'";
                                                       const creatorIdEnc = utils.encodeInt(Number(targetId));
                                                       
                                                       const xEnc = utils.encodeInt(target.lilyBarrierPos ? target.lilyBarrierPos[0] : 0);
                                                       const yEnc = utils.encodeInt(target.lilyBarrierPos ? target.lilyBarrierPos[1] : 0);
                                                       const zEnc = utils.encodeInt(target.lilyBarrierPos ? target.lilyBarrierPos[2] : 0);
                                                       const rxEnc = utils.encodeInt(target.lilyBarrierRot ? target.lilyBarrierRot[0] : 0);
                                                       const ryEnc = utils.encodeInt(target.lilyBarrierRot ? target.lilyBarrierRot[1] : 0);
                                                       const rzEnc = utils.encodeInt(target.lilyBarrierRot ? target.lilyBarrierRot[2] : 0);
                                                       
                                                       const resp = "e" + targetId + ":SendMsgToAllClient,(" + p2210001 + ",(" + targetIdEnc + ",(" + shieldIdEnc + "," + hpEnc + "," + maxHpEnc + "," + dateStr + "," + creatorIdEnc + ",(" + xEnc + "," + yEnc + "," + zEnc + "),(" + rxEnc + "," + ryEnc + "," + rzEnc + "))))\n";
                                                       activeSockets.forEach(s => {
                                                           if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                                                               s.write(resp);
                                                           }
                                                       });
                                                   }
                                                  
                                                  if (barrierHp <= 0) {
                                                       console.log(`[🧱💥 BARREIRA ${barrierName} DESTRUÍDA] Barreira de ${targetId} foi destruída!`);
                                                       if (heroId === 1032) {
                                                           target.venusBarrierActive = false;
                                                       } else if (heroId === 1033) {
                                                           target.lilyBarrierActive = false;
                                                       }
                                                   }
                                                  
                                                  // Envia feedback de dano na barreira (o jogador vê o hit marker)
                                                  gameLogic.sendEntityDamageUpdate(targetId, target, barrierDmg, attackerId, 2, weaponId);
                                                  
                                                  // PULA toda a lógica de HP/morte do jogador - a barreira é uma entidade separada
                                                  continue;
                                              }
                                              // Se barrierHp <= 0, barreira já quebrada, dano passa para o jogador normalmente
                                              console.log(`[🧱 BARREIRA ${barrierName}] Barreira de ${targetId} já destruída. Dano vai para o HP do jogador.`);
                                          }

                                          
                                           // 2.5. Aplicador dinâmico de buffs/debuffs do weapon.xml ao atingir o corpo do jogador (NÃO barreira)
                                           if (loopWeaponData && loopWeaponData.buff > 0) {
                                               const buffId = loopWeaponData.buff;
                                               const buffInfo = buffsMap[buffId];
                                               if (buffInfo) {
                                                   const isDebuff = buffInfo.isdebuff === 1;
                                                   const isSameTeam = combat.sameSide(attackerSocket || attackerNpc, target, Number(attackerForce) === Number(targetForce));
                                                   if ((isDebuff && !isSameTeam) || (!isDebuff && isSameTeam)) {
                                                       const buffDuration = buffInfo.time || 5000;
                                                       gameLogic.applyBuffToEntity(socket, targetId, buffId, buffDuration);
                                                   }
                                               }
                                           }
// ===== SELF-SHIELDS (Valkyria, buffs, ult Venus) =====
                                          // Estes SIM absorvem dano do HP do jogador (são escudos pessoais)
                                          if (target.shield && target.shield > 0) {
                                              shieldAbsorbed = Math.min(target.shield, damageAmount);
                                              target.shield -= shieldAbsorbed;
                                              damageAmount -= shieldAbsorbed;
                                              if (target.shield === 0) {
                                                  target.maxShield = 0;
                                              }
                                              place = 2;
                                              console.log("[🛡️ SELF-SHIELD] " + shieldAbsorbed + " absorvido pelo escudo pessoal de " + targetId + ". Restante: " + target.shield + ". Sobrou dano: " + damageAmount);
                                          }

                                          if (damageAmount > 0 && place !== 2) {
                                              if (boomHead === 1 && canHeadshot) {
                                                  place = 1;
                                              } else {
                                                  place = 0;
                                              }
                                          }

                                           // 3. Aplica dano restante no HP (com checagem de Imortalidade da Ult do Khan)
                                           if (damageAmount > 0) {
                                               const isKhanUlt = (Number(target.currentHeroId) === 1034 || Number(target.heroId) === 1034) && (target.isKhanUltActive || (target.activeBuffs && (target.activeBuffs[1046] || target.activeBuffs[1047])));
                                               if (isKhanUlt) {
                                                   const prevHp = (target.hp !== undefined ? target.hp : (target.maxHp || 2200));
                                                   target.hp = Math.max(1, prevHp - damageAmount);
                                                   console.log(`[🛡️ KHAN IRON IMMORTALITY] Khan ${targetId} protegido pela Ultimate Iron! HP mantido em no mínimo 1. HP atual: ${target.hp}`);
                                               } else {
                                                   target.hp = Math.max(0, (target.hp !== undefined ? target.hp : (target.maxHp || 2000)) - damageAmount);
                                               }
                                           }

                                            // 🔥 Brigit (1023) Passiva Ignite (DoT de Queimadura após 3 hits da Brigit ou da Torreta)
                                            const isBrigitAttacker = (attackerSocket && (Number(attackerSocket.currentHeroId) === 1023 || Number(attackerHeroId) === 1023)) || (attackerNpc && (attackerNpc.index === 1001 || String(attackerNpc.index) === "1001"));
                                            if (isBrigitAttacker && originalDamage > 0 && target && !target.isDead) {
                                                const hasIgniteBuff = target.activeBuffs && target.activeBuffs[1017];
                                                if (!hasIgniteBuff) {
                                                    const nowTime = Date.now();
                                                    if (!target.brigitHitTimer || (nowTime - target.brigitHitTimer > 3000)) {
                                                        target.brigitHitCount = 0;
                                                    }
                                                    target.brigitHitTimer = nowTime;
                                                    target.brigitHitCount = (target.brigitHitCount || 0) + 1;
                                                    
                                                    if (target.brigitHitCount >= 3) {
                                                        target.brigitHitCount = 0;
                                                        const casterSock = attackerSocket || socket;
                                                        gameLogic.applyBuffToEntity(casterSock, targetId, 1017, 4000);
                                                        console.log(`[🔥 BRIGIT IGNITE PASSIVE] Alvo ${targetId} atingido 3x por fogo! Aplicado Ignite DoT (Buff 1017: 4 ticks de 50 = 200 de dano total)`);
                                                    }
                                                }
                                            }

                                            // 🧛 Dragos (1026) Lifesteal ao causar dano durante a Ultimate (Blood Rage / Shapeshift)
                                            if (attackerSocket && (Number(attackerSocket.currentHeroId) === 1026 || Number(attackerHeroId) === 1026)) {
                                                const isDragosUlt = attackerSocket.isDragosUltActive || (attackerSocket.activeBuffs && attackerSocket.activeBuffs[1023]) || String(loopShootname) === "4";
                                                if (isDragosUlt && originalDamage > 0) {
                                                    const healAmt = Math.round(originalDamage * 0.35);
                                                    const maxHp = attackerSocket.maxHp || 2000;
                                                    attackerSocket.hp = Math.min(maxHp, (attackerSocket.hp || 0) + healAmt);
                                                    gameLogic.sendEntityHpUpdate(attackerSocket, attackerSocket.playerId || "1", attackerSocket, true);
                                                    console.log(`[🩸 DRAGOS BLOODTHIRSTY LIFESTEAL] Dragos ${attackerId} roubou +${healAmt} HP! Novo HP: ${attackerSocket.hp}/${maxHp}`);
                                                }
                                            }

                                           // 5. Aplica Repulsão Calibrada por Categoria
                                           let forceDir = null;
                                           let forceMagnitude = 0;

                                           // 1. Lily M2 Push (Arma 9985 ou Shootname 2 da Lily ou NPC 1024)
                                           const isLilyPush = (
                                               Number(loopWeaponId) === 9985 || 
                                               Number(resolvedWeaponId) === 9985 || 
                                               Number(weaponId) === 9985 ||
                                               (attackerNpc && (attackerNpc.index === 1024 || String(attackerNpc.index) === "1024")) ||
                                               ((attackerHeroId === 1033 || Number(attackerHeroId) === 1033) && String(loopShootname) === "2")
                                           );

                                           // 2. Shogun Flecha de Repulsão (Arma 1024, launcherNo 1 / E skill)
                                           // NOTA: launcherNo 0 é flecha comum (sem empurrão), shootname 3 é soco melee (sem empurrão)
                                           const isShogunRepulsion = (
                                                (Number(loopWeaponId) === 1024 || Number(resolvedWeaponId) === 1024) && 
                                                Number(launcherNo) === 1 && String(loopShootname) === "1"
                                            );

                                           if (isShogunRepulsion) {
                                               // Ajusta dano base da flecha de repulsão para 125 (conforme XML)
                                               loopBaseDamage = 125;
                                           }

                                            // 3. Loki Dinamite / Remote Bomb Repulsão (Arma 9990 ou NPC 1019)
                                            const isLokiDynamite = (
                                                Number(loopWeaponId) === 9990 || 
                                                Number(resolvedWeaponId) === 9990 || 
                                                Number(weaponId) === 9990 ||
                                                (attackerNpc && (attackerNpc.index === 1019 || String(attackerNpc.index) === "1019"))
                                             );

                                             if (typeof isShogunSelfJump !== 'undefined' && isShogunSelfJump) {
                                                 let fX = 0, fZ = 0;
                                                 if (clientForceDir && (clientForceDir[0] !== 0 || clientForceDir[2] !== 0)) {
                                                     fX = clientForceDir[0];
                                                     fZ = clientForceDir[2];
                                                 }
                                                 forceDir = [fX * 0.5, 0.85, fZ * 0.5];
                                                 forceMagnitude = 2400; // Impulso vertical forte de salto para movimentação
                                                 console.log(`[🚀 SHOGUN ROCKET JUMP] Shogun ${targetId} impulsionado no ar com flecha nos pés! Vetor: [${forceDir.map(v=>Number(v).toFixed(2))}], Força: ${forceMagnitude}`);
                                             } else if (isLilyPush || isShogunRepulsion || isLokiDynamite) {
                                                let fX = 0, fZ = 0;
                                                if (clientForceDir && (clientForceDir[0] !== 0 || clientForceDir[2] !== 0)) {
                                                    const hLen = Math.hypot(clientForceDir[0], clientForceDir[2]) || 1;
                                                    fX = clientForceDir[0] / hLen;
                                                    fZ = clientForceDir[2] / hLen;
                                                } else {
                                                    const aX = attackerSocket ? (attackerSocket.lastX || 0) : (attackerNpc ? (attackerNpc.lastX || 0) : 0);
                                                    const aZ = attackerSocket ? (attackerSocket.lastZ || 0) : (attackerNpc ? (attackerNpc.lastZ || 0) : 0);
                                                    const tX = target ? (target.lastX || 0) : 0;
                                                    const tZ = target ? (target.lastZ || 0) : 0;
                                                    let dx = tX - aX;
                                                    let dz = tZ - aZ;
                                                    let dLen = Math.hypot(dx, dz) || 1;
                                                    fX = dx / dLen;
                                                    fZ = dz / dLen;
                                                }
                                                // Sustentação vertical de 0.35 para levantar o alvo do chão em arco fluido
                                                forceDir = [fX, 0.35, fZ];
                                                forceMagnitude = isLokiDynamite ? 2400 : 1800; // Empurrão forte de habilidade
                                                console.log(`[💨 SKILL KNOCKBACK] Habilidade (${isLokiDynamite ? 'Loki Dinamite' : isLilyPush ? 'Lily M2' : 'Shogun Flecha E'}) em ${targetId}! Vetor: [${forceDir.map(v=>Number(v).toFixed(2))}], Força: ${forceMagnitude}`);
                                            } else {
                                                forceDir = null;
                                                forceMagnitude = 0;
                                            }
                                           // 5. Envia HaveDamage para feedbacks ricos e física de knockback
                                           gameLogic.sendEntityDamageUpdate(targetId, target, originalDamage, attackerId, place, loopWeaponId, forceDir, forceMagnitude, launcherNo, Number(loopShootname));

                                           // 5b. Acumula dano no atacante para estatísticas
                                           if (attackerSocket && originalDamage > 0) {
                                                attackerSocket.matchDamage = (attackerSocket.matchDamage || 0) + Math.round(originalDamage);
                                                // Carrega energia de Ultimate ao causar dano conforme dazhao.xml (energyadddamage)
                                                const attHero = attackerSocket.currentHeroId || 1015;
                                                const dzList = db.heroDazhaoMap[attHero] || [];
                                                const dmgMult = (dzList.length > 0 && dzList[0].energyAddDamage) ? dzList[0].energyAddDamage : 150;
                                                const chargeDamage = Number(attHero) === 2000 ? Math.min(originalDamage, lamsterDamageCapacity) : originalDamage;
                                                const gainedEnergy = Math.round((chargeDamage * dmgMult) / 200);
                                                addPlayerUltEnergy(attackerSocket, gainedEnergy);
                                            }
                                           
                                          if (target.hp <= 0) {
                                              gameLogic.handlePlayerDeath(target, attackerSocket);
                                          }
                                      } else {
                                          let place = 0;
                                          if (boomHead === 1 && canHeadshot) {
                                              place = 1;
                                          } 
                                          const beforeHp = Math.max(0, target.hp || 0), beforeShield = Math.max(0, target.shield || 0);
                                          gameLogic.handleNpcHurt(target, targetId, damageAmount, attackerId, place, weaponId);
                                          if (Number(attackerSocket?.currentHeroId) === 2000) {
                                              const dealt = Math.max(0, beforeHp - (target.hp || 0)) + Math.max(0, beforeShield - (target.shield || 0));
                                              const rate = db.heroDazhaoMap[2000]?.[0]?.energyAddDamage || 100;
                                              addPlayerUltEnergy(attackerSocket, dealt * rate / 200);
                                          }
                                      }
                                }
                            }
                        } catch (e) {
                            console.error("[⚠️ ERRO ao processar SDG packet]:", e);
                        }
                    }
                } else if (line.includes(":SSP,")) {
                    // Sincronização de posição do RobotAI enviada pelo NavMesh do cliente
                    try {
                        const sspMatch = line.match(/^e(\w+):(?:.*?:)?SSP,\((.+)\)/);
                        if (sspMatch) {
                            const parsed = utils.parseArrayList(sspMatch[2]);
                            if (parsed && parsed.length > 0) {
                                const arr = parsed[0];
                                const pos = arr[0]; // [x, y, z]
                                const isDead = arr[1];
                                const botId = arr[2];
                                if (pos && botId !== undefined) {
                                    const bx = utils.decodeInt(pos[0]);
                                    const by = utils.decodeInt(pos[1]);
                                    const bz = utils.decodeInt(pos[2]);
                                    const decodedBotId = utils.decodeInt(botId);
                                    if (state.activeBots && state.activeBots[decodedBotId]) {
                                        state.activeBots[decodedBotId].lastX = bx;
                                        state.activeBots[decodedBotId].lastY = by;
                                        state.activeBots[decodedBotId].lastZ = bz;
                                    }
                                    // Retransmite SCP para outros clientes humanos na partida
                                    activeSockets.forEach(s => {
                                        if (sceneScope.canReceive(socket, s) && s !== socket && !s.destroyed) {
                                            s.write(`e${utils.encodeInt(decodedBotId)}:SCP,(((${bx},${by},${bz}),${isDead || 0}))\n`);
                                        }
                                    });
                                }
                            }
                        }
                    } catch (e) {
                        console.error("[⚠️ ERRO ao processar SSP]:", e);
                    }
                } else if (line.includes(":NSP,")) {
                    // ❄️🚗 Sincronização de Posição de NPCs Controláveis (Ex: Bola de Neve da Scarlett NPC 1013, Veículos, etc.)
                    try {
                        const nspMatch = line.match(/^e(\w+):(?:.*?:)?NSP,\((.+)\)/);
                        if (nspMatch) {
                            let d = utils.parseArrayList(nspMatch[2]);
                            while (Array.isArray(d) && d.length === 1 && Array.isArray(d[0])) {
                                d = d[0];
                            }
                            if (Array.isArray(d) && d.length >= 4) {
                                const npcId = Number(d[0]);
                                const posX = Number(d[1]);
                                const posY = Number(d[2]);
                                const posZ = Number(d[3]);

                                if (spawnedNpcs[npcId]) {
                                    spawnedNpcs[npcId].x = posX;
                                    spawnedNpcs[npcId].y = posY;
                                    spawnedNpcs[npcId].z = posZ;
                                    spawnedNpcs[npcId].lastX = posX;
                                    spawnedNpcs[npcId].lastY = posY;
                                    spawnedNpcs[npcId].lastZ = posZ;
                                }

                                const nspPacket = `e${utils.encodeInt(npcId)}:NSP,((${utils.encodeInt(posX)},${utils.encodeInt(posY)},${utils.encodeInt(posZ)}))\n`;
                                activeSockets.forEach(s => {
                                    if (sceneScope.canReceive(socket, s) && s !== socket && !s.destroyed) {
                                        s.write(nspPacket);
                                    }
                                });
                            }
                        }
                    } catch (e) {
                        console.error("[⚠️ ERRO ao processar NSP]:", e);
                    }
                } else if (line.includes(":NSR,")) {
                    // ❄️ Sincronização de Rotação de NPCs Controláveis
                    try {
                        const nsrMatch = line.match(/^e(\w+):(?:.*?:)?NSR,\((.+)\)/);
                        if (nsrMatch) {
                            let d = utils.parseArrayList(nsrMatch[2]);
                            while (Array.isArray(d) && d.length === 1 && Array.isArray(d[0])) {
                                d = d[0];
                            }
                            if (Array.isArray(d) && d.length >= 4) {
                                const npcId = Number(d[0]);
                                const rotX = Number(d[1]);
                                const rotY = Number(d[2]);
                                const rotZ = Number(d[3]);

                                if (spawnedNpcs[npcId]) {
                                    spawnedNpcs[npcId].rx = rotX;
                                    spawnedNpcs[npcId].ry = rotY;
                                    spawnedNpcs[npcId].rz = rotZ;
                                }

                                const nsrPacket = `e${utils.encodeInt(npcId)}:NSR,((${utils.encodeInt(rotX)},${utils.encodeInt(rotY)},${utils.encodeInt(rotZ)}))\n`;
                                activeSockets.forEach(s => {
                                    if (sceneScope.canReceive(socket, s) && s !== socket && !s.destroyed) {
                                        s.write(nsrPacket);
                                    }
                                });
                            }
                        }
                    } catch (e) {
                        console.error("[⚠️ ERRO ao processar NSR]:", e);
                    }
                } else if (line.includes(":NSS,")) {
                    // Sincronização de Escala de NPCs
                    try {
                        const nssMatch = line.match(/^e(\w+):(?:.*?:)?NSS,\((.+)\)/);
                        if (nssMatch) {
                            let d = utils.parseArrayList(nssMatch[2]);
                            while (Array.isArray(d) && d.length === 1 && Array.isArray(d[0])) {
                                d = d[0];
                            }
                            if (Array.isArray(d) && d.length >= 4) {
                                const npcId = Number(d[0]);
                                const sx = Number(d[1]);
                                const sy = Number(d[2]);
                                const sz = Number(d[3]);

                                const nssPacket = `e${utils.encodeInt(npcId)}:NSS,((${utils.encodeInt(sx)},${utils.encodeInt(sy)},${utils.encodeInt(sz)}))\n`;
                                activeSockets.forEach(s => {
                                    if (sceneScope.canReceive(socket, s) && s !== socket && !s.destroyed) {
                                        s.write(nssPacket);
                                    }
                                });
                            }
                        }
                    } catch (e) {
                        console.error("[⚠️ ERRO ao processar NSS]:", e);
                    }
                } else if (line.includes(":NSTP,")) {
                    // Sincronização de Posição Alvo de NPCs
                    try {
                        const nstpMatch = line.match(/^e(\w+):(?:.*?:)?NSTP,\((.+)\)/);
                        if (nstpMatch) {
                            let d = utils.parseArrayList(nstpMatch[2]);
                            while (Array.isArray(d) && d.length === 1 && Array.isArray(d[0])) {
                                d = d[0];
                            }
                            if (Array.isArray(d) && d.length >= 5) {
                                const npcId = Number(d[0]);
                                const targetNetId = Number(d[1]);
                                const tx = Number(d[2]);
                                const ty = Number(d[3]);
                                const tz = Number(d[4]);

                                const nstpPacket = `e${utils.encodeInt(npcId)}:NSTP,(${utils.encodeInt(targetNetId)},(${utils.encodeInt(tx)},${utils.encodeInt(ty)},${utils.encodeInt(tz)}))\n`;
                                activeSockets.forEach(s => {
                                    if (sceneScope.canReceive(socket, s) && s !== socket && !s.destroyed) {
                                        s.write(nstpPacket);
                                    }
                                });
                            }
                        }
                    } catch (e) {
                        console.error("[⚠️ ERRO ao processar NSTP]:", e);
                    }
                } else if (line.includes(":SA_Npc,")) {
                    // Sincronização de Animação de NPCs
                    try {
                        const saMatch = line.match(/^e(\w+):(?:.*?:)?SA_Npc,\((.+)\)/);
                        if (saMatch) {
                            let d = utils.parseArrayList(saMatch[2]);
                            while (Array.isArray(d) && d.length === 1 && Array.isArray(d[0])) {
                                d = d[0];
                            }
                            if (Array.isArray(d) && d.length >= 4) {
                                const aniData = d[0];
                                const sendtype = d[1];
                                const npcNetId = Number(d[2]);
                                if (Array.isArray(aniData) && aniData.length >= 3) {
                                    const ani = aniData[0];
                                    const type = aniData[1];
                                    const parm = aniData[2];
                                    const caPacket = `e${utils.encodeInt(npcNetId)}:CA,(('${ani}',${type},${parm}),${sendtype})\n`;
                                    activeSockets.forEach(s => {
                                        if (sceneScope.canReceive(socket, s) && s !== socket && !s.destroyed) {
                                            s.write(caPacket);
                                        }
                                    });
                                }
                            }
                        }
                    } catch (e) {
                        console.error("[⚠️ ERRO ao processar SA_Npc]:", e);
                    }
                } else if (line.includes(":NCD,")) {
                    // Sincronização do Carro de Escolta (Chariot_Controller.SendNetData -> Chariot_Entity.NCD)
                    try {
                        const ncdMatch = line.match(/^e(\w+):(?:.*?:)?NCD,\((.+)\)/);
                        if (ncdMatch) {
                            let d = utils.parseArrayList(`(${ncdMatch[2]})`);
                            while (Array.isArray(d) && d.length === 1 && Array.isArray(d[0])) {
                                d = d[0];
                            }
                            if (Array.isArray(d) && d.length >= 6) {
                                const pos = d[0];
                                const rot = d[1];
                                const vel = d[2];
                                const angVel = d[3];
                                const speed = Number(d[4] || 0);
                                const carNetId = Number(d[5] || 8002);
                                const fmtVec = v => Array.isArray(v) ? `(${v.map(n => utils.encodeInt(Number(n || 0))).join(',')})` : `(0,0,0)`;
                                const ncdPacket = `e${utils.encodeInt(carNetId)}:NCD,((${fmtVec(pos)},${fmtVec(rot)},${fmtVec(vel)},${fmtVec(angVel)},${utils.encodeInt(speed)}))\n`;
                                activeSockets.forEach(s => {
                                    if (sceneScope.canReceive(socket, s) && s !== socket && !s.destroyed) {
                                        s.write(ncdPacket);
                                    }
                                });
                            }
                        }
                    } catch (e) {
                        console.error("[⚠️ ERRO ao processar NCD]:", e);
                    }
                } else if (!line.includes("SN,")) {
                    // Loga qualquer outro pacote recebido que não seja PING/PONG (Ex: pacotes de movimento 'ST')
                    console.log(`[📡 RECEBIDO DO CLIENTE] ` + line);
                }
            }
            updateSessionFromSocket(socket);
        }
    }

    socket.on('close', () => {
        lifecycle.event(socket, 'CONNECTION_CLOSE');
        lifecycle.transition(socket, 'closed', 'tcp-close');
        const departingRoomUID = socket.currentRoomUID;
        const departingMatch = !socket.sceneRetired ? gameLogic.getTdmMatch(socket, false) : null;
        const departingPeers = activeSockets.filter(s => s !== socket && sceneScope.canReceive(socket, s));
        const socketIdx = activeSockets.indexOf(socket);
        if (socketIdx !== -1) {
            activeSockets.splice(socketIdx, 1);
        }

        // Remove da fila de matchmaking se desconectar
        matchmakingQueue = matchmakingQueue.filter(entry => entry.socket !== socket);
        if (socketType === 'LOBBY' && socket.playerName) {
            socialServer.removePlayerFromTeam(socket.playerName, activeSockets);
            socialServer.notifyFriendsState(socket.playerName, activeSockets);
        }

        // Remove do container de sala se desconectar
        const currentContainerRoom = !socket.sceneRetired ? getSocketRoom(socket) : null;
        if (currentContainerRoom) {
            removePlayerFromRoom(currentContainerRoom, socket);
        }

        if (socket.csharpBridge) {
            try { socket.csharpBridge.destroy(); } catch (e) {}
            socket.csharpBridge = null;
        }
        if (socketType === 'SCENE' && !socket.sceneRetired) {
            socket.inMatch = false;
            
            // Broadcast to other players in the same scene container to destroy this player's entity
            const disconnectedIndex = socket.playerIndex || (global.userIndexes && global.userIndexes[socket.playerName]) || (Number(socket.playerId) || 1);
            const roomUID = departingRoomUID;
            const sceneTargets = departingPeers;
            sceneTargets.forEach(s => {
                if (s !== socket && !s.destroyed) {
                    // Segurança absoluta: Nunca enviar destroy com o índice do próprio socket destinatário!
                    if (s.playerIndex && s.playerIndex === disconnectedIndex) return;
                    s.write(`d${utils.encodeInt(disconnectedIndex)}\n`);
                    console.log(`[🌐 SCENE Room ${roomUID || 'Global'}] Sent destroy signal d${disconnectedIndex} (Player Disconnected: ${socket.playerName}) to ${s.playerName}`);
                }
            });
            
            const lobbyMatch = activeSockets.find(s => s.socketType === "LOBBY" && s.playerName === socket.playerName && !s.destroyed);
            if (lobbyMatch) {
                lobbyMatch.inMatch = false;
            }

            // Remove da partida TDM ativa
            const tdm = departingMatch;
            if (tdm && tdm.players) {
                const pIdx = tdm.players.indexOf(socket);
                if (pIdx !== -1) {
                    tdm.players.splice(pIdx, 1);
                    console.log(`[🏆 TDM Sala ${tdm.roomUID}] Jogador ${socket.playerName || 'Jogador'} desconectou e foi removido do TDM.`);
                }
                if (tdm.players.length === 0 && !(currentContainerRoom && currentContainerRoom.autoDestroyTimer)) {
                    if (tdm.timer) clearInterval(tdm.timer);
                    tdm.timer = null;
                    if (tdm.endTimeout) clearTimeout(tdm.endTimeout);
                    tdm.state = 'ended';
                    if (tdm.roomUID && roomContainers.has(Number(tdm.roomUID))) {
                        const r = roomContainers.get(Number(tdm.roomUID));
                        if (r) r.tdmMatch = null;
                    }
                    if (global.activeTdmMatch === tdm) global.activeTdmMatch = null;
                }
            }
            cleanPlayerMatchState(socket, socket.playerName);
        }
        if (socket.activeBuffs) {
            for (const buffId in socket.activeBuffs) {
                if (socket.activeBuffs[buffId]) {
                    clearTimeout(socket.activeBuffs[buffId]);
                }
            }
            socket.activeBuffs = null;
        }
        socket.activeUltimateBuffs = null;

        if (socket.carmenHealTargetId) {
            gameLogic.cleanupBuff(null, socket.carmenHealTargetId, 1021, true);
            socket.carmenHealTargetId = null;
        }
        if (socket.carmenBeaconTargetId) {
            gameLogic.cleanupBuff(null, socket.carmenBeaconTargetId, 1005, true);
            gameLogic.cleanupBuff(null, socket.carmenBeaconTargetId, 1010, true);
            socket.carmenBeaconTargetId = null;
        }

        if (socket.activeTargetBuffs) {
            for (const key in socket.activeTargetBuffs) {
                if (socket.activeTargetBuffs[key]) {
                    clearTimeout(socket.activeTargetBuffs[key]);
                }
            }
            socket.activeTargetBuffs = null;
        }

        if (socket.invisibilityTimer) {
            clearTimeout(socket.invisibilityTimer);
            socket.invisibilityTimer = null;
        }
        if (socket.ultimateTimer) {
            clearTimeout(socket.ultimateTimer);
            socket.ultimateTimer = null;
        }
        if (socket.respawnTimeout) {
            clearTimeout(socket.respawnTimeout);
            socket.respawnTimeout = null;
        }
    });
    socket.on('error', (err) => {
        lifecycle.event(socket, 'CONNECTION_ERROR', { code: err.code, message: err.message });
        console.log(`[⚠️ ERRO]:`, err.message);
    });
});

tcpServer.listen(config.tcpPort, () => {
    console.log(`[🎮 SERVIDOR DO JOGO] Ouvindo TCP na porta ${config.tcpPort}...`);
});

// Intervalo global de 1 segundo para cura passiva da Angel (Círculo de Cura), Regeneração de Escudo do Tagaloa e Regeneração Passiva Fora de Combate (5% HP/s)
setInterval(() => {
    const now = Date.now();
    activeSockets.forEach(socket => {
        if (socket.socketType !== "SCENE" || socket.destroyed || socket.sceneRetired || !socket.isCombatReady) return;

        // 1. Regeneração de Escudo do Tagaloa (1006)
        if (socket.currentHeroId === 1006 || String(socket.currentHeroId) === "1006") {
            const tagaloaBarrier = heroCatalog.getHeroBarrierSpec(1006);
            const lastDamage = socket.lastDamageTime || 0;
            // Regenera quando não está sofrendo dano por pelo menos regenDelayMs
            if (now - lastDamage >= tagaloaBarrier.regenDelayMs) {
                const healAmount = tagaloaBarrier.regenPerSec;
                const currentBarrierHp = (socket.tagaloaShieldHp !== undefined) ? socket.tagaloaShieldHp : tagaloaBarrier.maxHp;
                if (currentBarrierHp < tagaloaBarrier.maxHp) {
                    socket.tagaloaShieldHp = Math.min(tagaloaBarrier.maxHp, currentBarrierHp + healAmount);
                    
                    // Broadcast visual da regeneração (2010000 -> 2010002)
                    const p2010000 = utils.encodeInt(2010000);
                    const p2010002 = utils.encodeInt(2010002);
                    const stEnc = utils.encodeInt(socket.tagaloaShieldActive ? 1 : 0);
                    const enEnc = utils.encodeInt(socket.tagaloaShieldHp);
                    const mxEnc = utils.encodeInt(tagaloaBarrier.maxHp);
                    const regenResp = `e${utils.encodeInt(Number(socket.playerId))}:SendMsgToClient,(${p2010000},(${p2010002},(${stEnc},${enEnc},${mxEnc})))\n`;
                    socket.write(regenResp);
                    console.log(`[🛡️ TAGALOA BARREIRA REGEN] HP da barreira: ${socket.tagaloaShieldHp}/${tagaloaBarrier.maxHp} (+${healAmount} HP/s)`);
                }
            }
        }

        // 2. Cura passiva da Angel (1029)
        if (socket.currentHeroId === 1029 || String(socket.currentHeroId) === "1029") {
            if (socket.angelAuraTargets && socket.angelAuraTargets.length > 0) {
                const angelSpec = heroCatalog.getHeroSpec(1029);
                const angelForce = Number(socket.force !== undefined ? socket.force : (playerSessions[socket.playerName]?.force || 1));
                socket.angelAuraTargets.forEach(targetId => {
                    let target = null;
                    let isPlayerTarget = false;
                    
                    let foundPlayer = activeSockets.find(s => s.socketType === "SCENE" && !s.destroyed && (s.playerId === targetId || String(s.playerId) === String(targetId)));
                    if (foundPlayer) {
                        target = foundPlayer;
                        isPlayerTarget = true;
                    } else if (spawnedNpcs[targetId]) {
                        target = spawnedNpcs[targetId];
                    }
                    
                    if (target && !target.destroyed && (target.hp !== undefined && target.hp > 0)) {
                        const targetForce = Number(target.force !== undefined ? target.force : (playerSessions[target.playerName]?.force || (target.nForce !== undefined ? target.nForce : 1)));
                        // Garante estritamente que a cura só se aplica a aliados
                        if (!combat.sameSide(socket, target, targetForce === angelForce)) {
                            return;
                        }
                        const healAmount = angelSpec.circleHealPerSec;
                        target.hp = Math.min(target.maxHp || 2000, target.hp + healAmount);
                        gameLogic.sendEntityHpUpdate(target, targetId, target, isPlayerTarget);
                        console.log(`[💖 ANGEL CIRCLE HEAL] Angel ${socket.playerId} curou Alvo: ${targetId} em +${healAmount} HP. Novo HP: ${target.hp}`);
                    }
                });
            }
        }

        // 3. Regeneração Passiva Global Fora de Combate (5% HP/s após 6 segundos sem sofrer dano)
        if (!socket.isDead && socket.hp !== undefined && socket.hp > 0) {
            const lastDamage = socket.lastDamageTime || 0;
            const maxHp = socket.maxHp || 2000;
            if (socket.hp < maxHp && (now - lastDamage >= heroCatalog.COMBAT_DEFAULTS.outOfCombatRegenDelayMs)) {
                const regenHpAmount = Math.max(1, Math.round(maxHp * heroCatalog.COMBAT_DEFAULTS.outOfCombatRegenPctPerSec)); // 5% do HP Máximo por segundo
                const oldHp = socket.hp;
                socket.hp = Math.min(maxHp, socket.hp + regenHpAmount);
                if (socket.hp !== oldHp) {
                    gameLogic.sendEntityHpUpdate(socket, socket.playerId, socket, true);
                    console.log(`[💚 REGEN PASSIVA 5%/s] Jogador ${socket.playerId} (${socket.playerName || 'Player'}) regenerou +${socket.hp - oldHp} HP fora de combate. HP: ${socket.hp}/${maxHp}`);
                }
            }
        }
    });

    // 4. Regeneração Passiva para Bots Ativos
    if (state.activeBots) {
        Object.values(state.activeBots).forEach(bot => {
            if (bot && !bot.isDead && bot.hp !== undefined && bot.hp > 0) {
                const lastDamage = bot.lastDamageTime || 0;
                const maxHp = bot.maxHp || 2000;
                if (bot.hp < maxHp && (now - lastDamage >= 6000)) {
                    const regenHpAmount = Math.max(1, Math.round(maxHp * 0.05));
                    const oldHp = bot.hp;
                    bot.hp = Math.min(maxHp, bot.hp + regenHpAmount);
                    if (bot.hp !== oldHp) {
                        const masterSocket = activeSockets.find(s => s.socketType === "SCENE" && !s.destroyed);
                        if (masterSocket) {
                            gameLogic.sendEntityHpUpdate(masterSocket, bot.playerIndex, bot, false);
                        }
                    }
                }
            }
        });
    }
}, 1000);
