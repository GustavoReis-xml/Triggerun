// Módulo de Servidor TCP (Lobby, Login, Scene e IA da Angel) do Trigger Run
const net = require('net');
const config = require('./config');
const { SERVER_IP } = config;
const PLAYER_NAME = "Admin";
const PLAYER_NAME_ENC = "41646d696e";
const state = require('./state');
const db = require('./database');
const utils = require('./utils');
const gameLogic = require('./gameLogic');
const dbPersistence = require('./dbPersistence');

// Desestruturação das referências globais mutáveis do estado
const { activeSockets, spawnedNpcs, ipNames, ipIndexes, playerSessions, playerInventory, playerEquipment } = state;
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
    const hardcoded = [
        1001, 1002, 1003, 1004, 1005, 1006, 1007, 1008, 1009, 1010,
        1011, 1012, 1013, 1014, 1015, 1016, 1017, 1018, 1019, 1020,
        1021, 1022, 1023, 1024, 1025, 1026, 1027, 1028, 1029, 1030,
        1031, 1032, 1033, 1034, 1035, 1036, 1037, 1038, 1039, 1040,
        1041, 1042, 1043, 1044, 1045, 2000
    ];
    const combined = Array.from(new Set([...fromDb, ...hardcoded])).filter(id => id > 0);
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

// Funções de sincronização de sessão de jogadores
function updateSessionFromSocket(socket) {
    if (!socket || !socket.playerName) return;
    if (socket.socketType !== "SCENE") return; // Apenas atualiza a sessão de gameplay a partir do socket de cena
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
        if (socket.userData !== undefined) session.userData = socket.userData;
    }
    if (socket.lobbySocket) {
        if (socket.currentHeroId !== undefined) socket.lobbySocket.currentHeroId = socket.currentHeroId;
        if (socket.targetMapId !== undefined) socket.lobbySocket.targetMapId = socket.targetMapId;
        if (socket.targetMapName !== undefined) socket.lobbySocket.targetMapName = socket.targetMapName;
        if (socket.finalMapId !== undefined) socket.lobbySocket.finalMapId = socket.finalMapId;
    }
}

function syncSocketFromSession(socket) {
    if (!socket || !socket.playerName) return;
    const session = playerSessions[socket.playerName];
    if (session) {
        socket.playerIndex = session.playerIndex;
        socket.playerId = session.playerId;
        socket.force = session.force;
        socket.targetMapId = session.targetMapId;
        socket.targetMapName = session.targetMapName;
        socket.currentHeroId = session.currentHeroId;
        socket.currentSkinId = session.currentSkinId;
        socket.currentWeaponId = session.currentWeaponId;
        socket.finalMapId = session.finalMapId;
        socket.userData = session.userData;
    }
}

let currentRoom = null;

function getRoomActivePlayersCount(room) {
    let count = 0;
    for (let i = 0; i < 10; i++) {
        if (room.players[i]) count++;
    }
    return count;
}

function buildRoomData(room) {
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
            const cust = getPlayerCustomizationData(p.socket.playerName, heroId);
            const userDataHeroInfo = `(${heroIdEnc},${utils.encodeInt(cust.skinId)},${utils.encodeInt(cust.emoteId)},${one},${utils.encodeInt(cust.weaponId)})`;
            
            const isCapOpt = utils.encodeInt(isCaptain);
            const prerogativeLobby = `(${isCapOpt},${isCapOpt},${isCapOpt},${isCapOpt},'${p.socket.playerNameEnc}',${zero},'',${zero})`;
            const uData = `('${p.socket.playerNameEnc}',${utils.encodeInt(99)},${one},${utils.encodeInt(force)},${utils.encodeInt(isReady)},${one},${heroIdEnc},${one},${one},${one},${utils.encodeInt(cust.faceId)},'Local',${zero},${userDataHeroInfo},${one},${zero},${zero},${utils.encodeInt(isCaptain)},${prerogativeLobby},())`;
            
            userListElements.push(`'${p.socket.playerNameEnc}',${uData}`);
        } else {
            userListElements.push(`'',()`);
        }
    }
    const userList = `(${userListElements.join(',')})`;
    
    return `(${roomInfo},${userList})`;
}

function buildRoomUserList(room) {
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
            const cust = getPlayerCustomizationData(p.socket.playerName, heroId);
            const userDataHeroInfo = `(${heroIdEnc},${utils.encodeInt(cust.skinId)},${utils.encodeInt(cust.emoteId)},${one},${utils.encodeInt(cust.weaponId)})`;
            
            const isCapOpt = utils.encodeInt(isCaptain);
            const prerogativeLobby = `(${isCapOpt},${isCapOpt},${isCapOpt},${isCapOpt},'${p.socket.playerNameEnc}',${zero},'',${zero})`;
            const uData = `('${p.socket.playerNameEnc}',${utils.encodeInt(99)},${one},${utils.encodeInt(force)},${utils.encodeInt(isReady)},${one},${heroIdEnc},${one},${one},${one},${utils.encodeInt(cust.faceId)},'Local',${zero},${userDataHeroInfo},${one},${zero},${zero},${utils.encodeInt(isCaptain)},${prerogativeLobby},())`;
            
            userListElements.push(`'${p.socket.playerNameEnc}',${uData}`);
        } else {
            userListElements.push(`'',()`);
        }
    }
    return `(${userListElements.join(',')})`;
}

function broadcastToRoom(room, packet) {
    for (let i = 0; i < 10; i++) {
        const p = room.players[i];
        if (p && p.socket && !p.socket.destroyed) {
            p.socket.write(packet);
        }
    }
}

const tcpServer = net.createServer((socket) => {
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
        if (socket.socketType === "SCENE") {
            if (typeof data === 'string') {
                if (data.includes('SendMsgToHallClient') || data.includes('SendMsgToClient')) {
                    const idxPrefix = `e${socket.playerIndex || 1}:`;
                    data = data.replace(/^e\d+:/, idxPrefix);
                }
            } else if (Buffer.isBuffer(data)) {
                let str = data.toString('utf8');
                if (str.includes('SendMsgToHallClient') || str.includes('SendMsgToClient')) {
                    const idxPrefix = `e${socket.playerIndex || 1}:`;
                    str = str.replace(/^e\d+:/, idxPrefix);
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

    socket.on('data', (data) => {
        let textoRecebido = "";

        if (socket.isEncrypted) {
            let decrypted = Buffer.alloc(data.length);
            for (let i = 0; i < data.length; i++) {
                let step1 = data[i] ^ 173;
                let step2 = step1 - 37;
                if (step2 < 0) step2 += 256;
                decrypted[i] = step2 ^ 227;
            }
            textoRecebido = decrypted.toString('utf8');
        } else {
            textoRecebido = data.toString('utf8');
        }

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
                        finalMapId: lobbyMatch ? lobbyMatch.finalMapId : null
                    };
                }
                
                if (lobbyMatch) {
                    playerSessions[extractedName].finalMapId = lobbyMatch.finalMapId;
                    playerSessions[extractedName].targetMapId = lobbyMatch.targetMapId;
                    playerSessions[extractedName].targetMapName = lobbyMatch.targetMapName;
                    playerSessions[extractedName].force = lobbyMatch.force !== undefined ? lobbyMatch.force : 1;
                    playerSessions[extractedName].currentHeroId = lobbyMatch.currentHeroId || 1015;
                }
                
                syncSocketFromSession(socket);
                
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
                            finalMapId: null
                        };
                    }
                    socket.playerIndex = playerSessions[extractedName].playerIndex;
                    socket.playerId = playerSessions[extractedName].playerId;
                }
                console.log(`[🏰 LOBBY] Identificado com sucesso: ${socket.playerName} (ID: ${socket.playerId})`);
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
                const arr0 = `(1,'${socket.playerNameEnc}',1,${faceIdEnc},1,1,${levelEnc},${expEnc},1,0,0,0,0,0,'${regDate}',0,0,0,0,0,0,0,0,-1,(),0,0,0,0,0,0)`;
                // Saldo real vindo do banco do jogador:
                const inv = state.playerInventory[socket.playerName];
                const goldEnc = utils.encodeInt(inv.gold);
                const diaEnc = utils.encodeInt(inv.diamonds);
                const arr1 = `(${goldEnc},${goldEnc},${diaEnc},0,${utils.encodeInt(10)},0,${goldEnc},${goldEnc},${goldEnc},0,${goldEnc},0,0,0)`;
                const arr6 = `((),(),())`;
                const arr7 = `(0,1,0,(0))`;
                const arr8 = `((),'',0,${utils.encodeInt(100)},(),(),(),())`;
                const arr9 = `(0,0,0,0,0,0,0,0,0,0)`;
                const arr13 = `('','','','','','','','','','','','','','','')`;
                
                const stepEnc = utils.encodeInt(profile.runes ? profile.runes.step : 0);
                const starEnc = utils.encodeInt(profile.runes ? profile.runes.star : 0);
                const arr15 = `(0,0,0,0,0,0,0,0,0,0,0,0,0,0,(${stepEnc},StarPlaceholder))`.replace('StarPlaceholder', starEnc);
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
                        // 🎯 O cliente clicou em uma Partida Rapida e quer entrar na fila!
                        const fbType = subCmd[1]; // 1, 2 ou 4
                        const playerName = subCmd[2];
                        const mapId = 5204;
                        const mapIdEnc = utils.encodeInt(mapId);
                        const mapConf = MAP_CONFIG[mapId];
                        const mapTypeEnc = utils.encodeInt(mapConf.type);
                        const startTime = utils.Fun.GetServerDate();
                        const zero = utils.encodeInt(0);
                        const one = utils.encodeInt(1);

                        socket.targetMapId = mapId;
                        socket.targetMapName = mapConf.name;
                        socket.inMatch = true;
                        socket.force = 1;
                        socket.playerIndex = 1;
                        socket.playerId = "1";
                        state.setLastMapId(mapId);
                        lastMapName = mapConf.name;
                        lastFinalMapId = 5204;
                        lastFinalMapName = MAP_CONFIG[5204].name;
                        
                        console.log(`[🎯 MATCHMAKING] Jogador ${playerName} entrou na fila do modo ${fbType}!`);
                        
                        // 1. Enviar pacote 80006: "Procurando Partida..."
                        // Formato arrayList: (0, yjTime, div, matchNum, nowNum, maxNum, allModelNum, 0, fbType)
                        const searchArr = `(0,60,1,0,1,10,(),0,${fbType})`;
                        socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(80001)},(${utils.encodeInt(80006)},(1,${searchArr})))\n`);
                        
                        // 2. Esperar 3 segundos para simular a fila, depois enviar 80007 "Partida Encontrada!" e o 90001!
                        setTimeout(() => {
                            // Envia 80007 para fechar a tela de busca
                            socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(80001)},(${utils.encodeInt(80007)},()))\n`);
                            
                            // Agora simula o 60100 para criar a sala e jogar todo mundo pra dentro
                            const p80001 = utils.encodeInt(80001);
                            const s90001 = utils.encodeInt(90001);
                            const s90002 = utils.encodeInt(90002);
                            const s90004 = utils.encodeInt(90004);
                            const p1304 = utils.encodeInt(1304);
                            
                            const mapId = 5010; // STAGING MAP FOR ARENA UI
                            socket.finalMapId = 5204; // REAL COMBAT MAP
                            socket.targetMapId = mapId;
                            socket.waitingForHeroLock = true; // IMPEDE O SPAWN 10001 AQUI, ESPERA ELE ESCOLHER HERÓI!
                            socket.hasConfirmedHero = false;
                            
                            const mapIdEnc = utils.encodeInt(mapId);
                            const mapConf = MAP_CONFIG[mapId];
                            
                            // Sincroniza a sessão persistente com os novos mapas de partida
                            if (socket.playerName && playerSessions[socket.playerName]) {
                                playerSessions[socket.playerName].finalMapId = 5204;
                                playerSessions[socket.playerName].targetMapId = mapId;
                                playerSessions[socket.playerName].targetMapName = mapConf.name;
                            }
                            const mapTypeEnc = utils.encodeInt(mapConf.type);
                            const startTime = utils.Fun.GetServerDate();
                            const zero = utils.encodeInt(0);
                            const one = utils.encodeInt(1);

                            const isCapEnc = utils.encodeInt(1);
                            const myHeroId = socket.currentHeroId || 1015;
                            const myHeroIdEnc = utils.encodeInt(myHeroId);
                            const cust = getPlayerCustomizationData(socket.playerName, myHeroId);
                            const myUserDataHeroInfo = `(${myHeroIdEnc},${utils.encodeInt(cust.skinId)},${utils.encodeInt(cust.emoteId)},${one},${utils.encodeInt(cust.weaponId)})`;
                            const prerogativeLobbyMe = `(${isCapEnc},${isCapEnc},${isCapEnc},${isCapEnc},'${socket.playerNameEnc}',${zero},'',${zero})`;
                            const myUserData = `('${socket.playerNameEnc}',${utils.encodeInt(99)},${one},${one},${one},${one},${myHeroIdEnc},${one},${one},${zero},${utils.encodeInt(cust.faceId)},'Local',${zero},${myUserDataHeroInfo},${one},${zero},${zero},${isCapEnc},${prerogativeLobbyMe},())`;
                            
                            let userListElements = [`'${socket.playerNameEnc}',${myUserData}`];
                            
                            /*
                            // Desativado: Preenchimento automático de bots para partida solo
                            for (let i = 1; i < 10; i++) {
                                const botName = BOT_NAMES[i % BOT_NAMES.length];
                                const forceEnc = utils.encodeInt(i < 5 ? 1 : 2); // 5 Blue, 5 Red
                                const isReadyEnc = utils.encodeInt(1);
                                const heroIdEnc = utils.encodeInt(1015 + (i % 5));
                                
                                const prerogativeLobby = `(${utils.encodeInt(0)},${utils.encodeInt(0)},${utils.encodeInt(0)},${utils.encodeInt(0)},'${utils.encodeHex(botName)}',${utils.encodeInt(0)},'',${utils.encodeInt(0)})`;
                                const userDataHeroInfo = `(${heroIdEnc},${utils.encodeInt(1)},${utils.encodeInt(1)},${utils.encodeInt(1)},${utils.encodeInt(0)})`;
                                const botUserData = `('${utils.encodeHex(botName)}',${utils.encodeInt(99)},${utils.encodeInt(1)},${forceEnc},${isReadyEnc},${utils.encodeInt(1)},${heroIdEnc},${utils.encodeInt(1)},${utils.encodeInt(1)},${utils.encodeInt(1)},${utils.encodeInt(1)},'Local',${utils.encodeInt(0)},${userDataHeroInfo},${utils.encodeInt(1)},${utils.encodeInt(0)},${utils.encodeInt(0)},${utils.encodeInt(0)},${prerogativeLobby},())`;
                                
                                userListElements.push(`'${utils.encodeHex(botName)}',${botUserData}`);
                            }
                            */
                            
                            const userList = userListElements.join(',');
                            const isChangeHeroEnc = mapConf.type === 7 ? utils.encodeInt(1) : zero;
                            const matchMsg = `(${s90001},(${userList}),${mapIdEnc},${mapTypeEnc},'${mapConf.name}',${utils.encodeInt(60)},${isChangeHeroEnc},'${startTime}',${zero},${zero})`;
                            
                            (async () => {
                                await utils.sendPacket(socket, `e1:SendMsgToHallClient,(${p80001},${matchMsg})\n`, 400);
                                await utils.sendPacket(socket, `e1:SendMsgToHallClient,(${p80001},(${s90002},'${socket.playerNameEnc}',${myUserData}))\n`, 400);
                                
                                setTimeout(() => {
                                    socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(1304)},(${utils.encodeInt(1)},'${socket.playerNameEnc}','${SERVER_IP}',${utils.encodeInt(7777)},'SceneGroup',${utils.encodeInt(1)}))\n`);
                                }, 1500);
                            })().catch(err => console.error('[🎯 ERRO no envio do Quick Match]', err));
                            
                        }, 3000);
                    }
                    return;
                }

                if (cmdId === 60100) {
                    console.log(`[🎮 MATCHMAKING] Jogador solicitou partida! Agrupando todos os jogadores conectados no LOBBY...`);
                    
                    const lobbySockets = activeSockets.filter(s => s.socketType === "LOBBY" && !s.destroyed);
                    if (lobbySockets.length === 0) return;
                    
                    inMatch = true;
                    
                    const one = utils.encodeInt(1);
                    const zero = utils.encodeInt(0);
                    const dateStr = utils.Fun.GetServerDate();
                    const ninetyNine = utils.encodeInt(99);
                    
                    // Mapeia e prepara cada jogador com ID único e Equipes alternadas (TDM Blue vs Red)
                    lobbySockets.forEach((s, idx) => {
                        const playerIndex = idx + 1;
                        s.playerIndex = playerIndex;
                        s.playerId = String(playerIndex);
                        s.force = (idx % 2 === 0) ? 1 : 2; // Team 1 (Blue) vs Team 2 (Red)
                        s.targetMapId = 5204;
                        s.targetMapName = 'bachang_baxi';
                        s.inMatch = true;
                        
                        const heroId = s.currentHeroId || 1015;
                        const heroIdEnc = utils.encodeInt(heroId);
                        const profile = dbPersistence.loadPlayer(s.playerName);
                        const playerEq = state.playerEquipment[s.playerName] || (profile && profile.equipment) || {};
                        const eq = playerEq[heroId] || { skin: 1, weapon: 1, emote: 1, pose: 0 };
                        const heroInfo = generateFullHeroData(heroId, eq.skin, eq.weapon, eq.emote, eq.pose, profile).split(',').slice(1).join(',');
                        
                        const unlockedSkin = `(${one},(${one},${one},${zero},${zero},'${dateStr}',${zero}))`;
                        const unlockedWeapon = `(${one},(${one},${one},${zero},${zero},'${dateStr}',${zero},()))`;
                        const heroInfoData = `(${one},${zero},${utils.encodeInt(2)},'${dateStr}',${zero},(),${unlockedSkin},${unlockedWeapon},(),(),${zero},${one},${one},${zero},${zero},${zero})`;
                        
                        const cust = getPlayerCustomizationData(s.playerName, heroId);
                        const userDataHeroInfo = `(${heroIdEnc},${utils.encodeInt(cust.skinId)},${utils.encodeInt(cust.emoteId)},${one},${utils.encodeInt(cust.weaponId)})`;
                        const isCap = idx === 0 ? 1 : 0;
                        const isCapEnc = utils.encodeInt(isCap);
                        const prerogativeLobby = `(${isCapEnc},${isCapEnc},${isCapEnc},${isCapEnc},'${s.playerNameEnc}',${zero},'',${zero})`;
                        
                        s.userData = `('${s.playerNameEnc}',${ninetyNine},${one},${one},${one},${one},${heroIdEnc},${one},${one},${zero},${utils.encodeInt(cust.faceId)},'Local',${zero},${userDataHeroInfo},${one},${zero},${zero},${isCapEnc},${prerogativeLobby},())`;
                    });
                    
                    
                          const BOT_NAMES2 = ['Bot_Lars', 'Bot_Gaya', 'Bot_Tanya', 'Bot_Lupin', 'Bot_Chaser', 'Bot_Buster', 'Bot_Windy', 'Bot_Kahn', 'Bot_Raptor', 'Bot_Slayer', 'Bot_Viper'];
                          let userListElements2 = lobbySockets.map(s => `('${s.playerNameEnc}',${s.userData})`);
                          
                          // Preenche com bots at fechar 10 jogadores (5v5)
                          // Desativado: Preenche com bots at fechar 10 jogadores (5v5)
                          /*
                          for (let i = lobbySockets.length; i < 10; i++) {
                              const botName = BOT_NAMES2[i % BOT_NAMES2.length];
                              const forceEnc = utils.encodeInt(i < 5 ? 1 : 2); // 5 Blue, 5 Red
                              const isReadyEnc = utils.encodeInt(1);
                              const heroIdEnc = utils.encodeInt(1015 + (i % 5));
                              
                              const prerogativeLobby = `(${utils.encodeInt(0)},${utils.encodeInt(0)},${utils.encodeInt(0)},${utils.encodeInt(0)},'${utils.encodeHex(botName)}',${utils.encodeInt(0)},'',${utils.encodeInt(0)})`;
                              const userDataHeroInfo = `(${heroIdEnc},${utils.encodeInt(1)},${utils.encodeInt(1)},${utils.encodeInt(1)},${utils.encodeInt(0)})`;
                              const botUserData = `('${utils.encodeHex(botName)}',${utils.encodeInt(99)},${utils.encodeInt(1)},${forceEnc},${isReadyEnc},${utils.encodeInt(1)},${heroIdEnc},${utils.encodeInt(1)},${utils.encodeInt(1)},${utils.encodeInt(1)},${utils.encodeInt(1)},'Local',${utils.encodeInt(0)},${userDataHeroInfo},${utils.encodeInt(1)},${utils.encodeInt(0)},${utils.encodeInt(0)},${utils.encodeInt(0)},${prerogativeLobby},())`;
                              
                              userListElements2.push(`('${utils.encodeHex(botName)}',${botUserData})`);
                          }
                          */
                          const userList = userListElements2.join(',');

                    
                    const mapId = 5204; // bachang_baxi (loadmap ID real)
                    const mapIdEnc = utils.encodeInt(mapId);
                    const mapConf = MAP_CONFIG[mapId];
                    state.setLastMapId(mapId);
                    lastMapName = mapConf.name;
                    
                    const mapTypeEnc = utils.encodeInt(mapConf.type);
                    const startTime = utils.Fun.GetServerDate();
                    
                    const p80001 = utils.encodeInt(80001);
                    const s90001 = utils.encodeInt(90001);
                    const s90002 = utils.encodeInt(90002);
                    const s90004 = utils.encodeInt(90004);
                    const p1304 = utils.encodeInt(1304);
                    
                    const isChangeHeroEnc = mapConf.type === 7 ? utils.encodeInt(1) : zero;
                    const matchMsg = `(${s90001},(${userList}),${mapIdEnc},${mapTypeEnc},'${mapConf.name}',${utils.encodeInt(60)},${isChangeHeroEnc},'${startTime}',${zero},${zero})`;
                    
                    // Envia os pacotes de transição para cada um dos sockets do Lobby
                    lobbySockets.forEach(s => {
                        (async () => {
                            await utils.sendPacket(s, `e1:SendMsgToHallClient,(${p80001},${matchMsg})\n`, 400);
                            await utils.sendPacket(s, `e1:SendMsgToHallClient,(${p80001},(${s90002},'${s.playerNameEnc}',${s.userData}))\n`, 400);
                            await utils.sendPacket(s, `e1:SendMsgToHallClient,(${p80001},(${s90004}))\n`, 400);
                            
                            setTimeout(() => {
                                s.write(`e1:SendMsgToHallClient,(${p1304},(${utils.encodeInt(1)},'${s.playerNameEnc}','${SERVER_IP}',${utils.encodeInt(7777)},'SceneGroup',${utils.encodeInt(1)}))\n`);
                            }, 1500);
                        })().catch(err => console.error('[❌ ERRO no envio do Lobby Match]', err));
                    });
                    
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
                        const roomUID = 999;
                        const roomName = subArgs[0] || "Sala de Treino";
                        const password = subArgs[1] || "";
                        const mapId = subArgs[2] || 5202; // Default to bachang_baxi
                        const mapType = subArgs[3] || 6;
                        const maxPlayers = subArgs[5] || 10;
                        const creatorName = socket.playerName;
                        
                        currentRoom = {
                            roomUID: roomUID,
                            roomName: roomName,
                            password: password,
                            mapId: mapId,
                            mapType: mapType,
                            maxPlayers: maxPlayers,
                            creatorName: creatorName,
                            state: 0, // 0 = Esperando
                            createTime: utils.Fun.GetServerDate(),
                            players: []
                        };
                        
                        // Adiciona criador na vaga 0
                        currentRoom.players[0] = {
                            socket: socket,
                            name: socket.playerName,
                            force: 1,
                            isReady: 1,
                            isCaptain: 1
                        };
                        
                        for (let i = 1; i < 10; i++) {
                            currentRoom.players[i] = null;
                        }
                        
                        socket.targetMapId = mapId;
                        const mapConf = MAP_CONFIG[mapId] || { name: 'bachang_baxi', type: 6 };
                        socket.targetMapName = mapConf.name;
                        
                        // 1. Envia 3310 com o ID da sala para atualizar o m_nRoomUID no cliente
                        socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(3310)},(${utils.encodeInt(roomUID)}))\n`);
                        
                        // 2. Envia 3301 com (1) indicando sucesso
                        socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3301)},(${utils.encodeInt(1)})))\n`);
                        return;
                    }

                    // 3307: Obter informações da sala
                    if (subCmd === 3307) {
                        if (!currentRoom) {
                            socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3307)},(-1,())))\n`);
                            return;
                        }
                        const rData = buildRoomData(currentRoom);
                        socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3307)},(1,${rData})))\n`);
                        return;
                    }

                    // 3306: Lista de salas
                    if (subCmd === 3306) {
                        if (!currentRoom) {
                            socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3306)},(0,1,())))\n`);
                            return;
                        }
                        const roomInfo = `(${utils.encodeInt(currentRoom.mapId)},'${utils.encodeHex(currentRoom.creatorName)}','${utils.encodeHex(currentRoom.roomName)}','${utils.encodeHex(currentRoom.password)}',${utils.encodeInt(currentRoom.mapId)},${utils.encodeInt(currentRoom.mapType)},0,${utils.encodeInt(currentRoom.maxPlayers)},0,0,0,${utils.encodeInt(getRoomActivePlayersCount(currentRoom))},0,${utils.encodeInt(currentRoom.state)},0,${utils.encodeInt(7777)},'${currentRoom.createTime}',${utils.encodeInt(currentRoom.roomUID)},0)`;
                        socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3306)},(1,1,(${roomInfo}))))\n`);
                        return;
                    }

                    // 3303: Entrar na sala
                    if (subCmd === 3303) {
                        if (!currentRoom) {
                            socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3303)},(-1,1)))\n`);
                            return;
                        }
                        let joinSlot = -1;
                        for (let i = 0; i < 10; i++) {
                            if (!currentRoom.players[i]) {
                                joinSlot = i;
                                break;
                            }
                        }
                        if (joinSlot === -1) {
                            socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3303)},(-2,1)))\n`);
                            return;
                        }
                        
                        currentRoom.players[joinSlot] = {
                            socket: socket,
                            name: socket.playerName,
                            force: joinSlot < 5 ? 1 : 2,
                            isReady: 0,
                            isCaptain: 0
                        };
                        
                        socket.targetMapId = currentRoom.mapId;
                        const mapConfJoin = MAP_CONFIG[currentRoom.mapId] || { name: 'bachang_baxi', type: 6 };
                        socket.targetMapName = mapConfJoin.name;
                        
                        socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(3310)},(${utils.encodeInt(currentRoom.roomUID)}))\n`);
                        
                        const rDataJoin = buildRoomData(currentRoom);
                        const joinSuccessMsg = `(1,(1,${rDataJoin},'${utils.encodeHex(currentRoom.creatorName)}'))`;
                        socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3303)},${joinSuccessMsg}))\n`);
                        
                        broadcastToRoom(currentRoom, `e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3318)},(1,${buildRoomUserList(currentRoom)})))\n`);
                        return;
                    }

                    // 3304: Sair da sala
                    if (subCmd === 3304) {
                        if (!currentRoom) return;
                        let pSlot = -1;
                        for (let i = 0; i < 10; i++) {
                            if (currentRoom.players[i] && currentRoom.players[i].socket === socket) {
                                pSlot = i;
                                break;
                            }
                        }
                        if (pSlot === -1) return;
                        
                        const p = currentRoom.players[pSlot];
                        if (p.isCaptain) {
                            // Disparar disband
                            const rDataDisband = buildRoomData(currentRoom);
                            const disbandPacket = `e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3304)},(1,${utils.encodeInt(currentRoom.roomUID)},${rDataDisband},'${p.socket.playerNameEnc}',0,3)))\n`;
                            broadcastToRoom(currentRoom, disbandPacket);
                            
                            for (let i = 0; i < 10; i++) {
                                const pl = currentRoom.players[i];
                                if (pl && pl.socket) {
                                    pl.socket.targetMapId = 5202;
                                    pl.socket.targetMapName = 'bachang_baxi';
                                }
                            }
                            currentRoom = null;
                        } else {
                            currentRoom.players[pSlot] = null;
                            socket.targetMapId = 5202;
                            socket.targetMapName = 'bachang_baxi';
                            
                            const rDataLeave = buildRoomData(currentRoom);
                            socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3304)},(1,${utils.encodeInt(currentRoom.roomUID)},${rDataLeave},'${p.socket.playerNameEnc}',0,1)))\n`);
                            
                            broadcastToRoom(currentRoom, `e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3318)},(1,${buildRoomUserList(currentRoom)})))\n`);
                        }
                        return;
                    }

                    // 3318: Mudar de Time (Swap Faction)
                    if (subCmd === 3318) {
                        if (!currentRoom) return;
                        let pSlot = -1;
                        for (let i = 0; i < 10; i++) {
                            if (currentRoom.players[i] && currentRoom.players[i].socket === socket) {
                                pSlot = i;
                                break;
                            }
                        }
                        if (pSlot === -1) return;
                        
                        const p = currentRoom.players[pSlot];
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
                            // Fallback: toggle to the opposite team
                            targetTeam = pSlot < 5 ? 2 : 1;
                        }

                        let absoluteIdx = -1;
                        if (toIdx !== -1) {
                            // Client sends list index (0,2,4,6,8 = Blue team; 10,12,14,16,18 = Red team)
                            // Convert to server slot by dividing by 2: 0-4 = Blue, 5-9 = Red
                            absoluteIdx = Math.floor(toIdx / 2);
                            if (absoluteIdx < 0 || absoluteIdx >= 10) absoluteIdx = -1;
                        } else {
                            // Dynamically find first empty slot on the target team
                            const startSlot = targetTeam === 1 ? 0 : 5;
                            const endSlot = targetTeam === 1 ? 5 : 10;
                            for (let i = startSlot; i < endSlot; i++) {
                                if (!currentRoom.players[i]) {
                                    absoluteIdx = i;
                                    break;
                                }
                            }
                        }
                        
                        if (absoluteIdx !== -1 && absoluteIdx !== pSlot && absoluteIdx < 10 && !currentRoom.players[absoluteIdx]) {
                            currentRoom.players[absoluteIdx] = p;
                            currentRoom.players[pSlot] = null;
                            p.force = absoluteIdx < 5 ? 1 : 2;
                            
                            console.log(`[🏠 SALA] Mudar de time bem-sucedida! ${p.name} agora está no slot ${absoluteIdx} (Time ${p.force})`);
                            
                            broadcastToRoom(currentRoom, `e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3318)},(1,${buildRoomUserList(currentRoom)})))\n`);
                        } else {
                            console.log(`[⚠️ SALA] Mudança de time rejeitada. Slot ${absoluteIdx} ocupado ou inválido.`);
                        }
                        return;
                    }

                    // 3324: Alterar mapa / Configurações da sala
                    if (subCmd === 3324) {
                        if (!currentRoom) return;
                        const changes = subArgs[2] || [];
                        for (let i = 0; i < changes.length; i += 2) {
                            const idx = changes[i];
                            const val = changes[i + 1];
                            if (idx === 4) {
                                currentRoom.mapId = val;
                                const mapConfMod = MAP_CONFIG[val] || { name: 'bachang_baxi', type: 6 };
                                for (let j = 0; j < 10; j++) {
                                    const pl = currentRoom.players[j];
                                    if (pl && pl.socket) {
                                        pl.socket.targetMapId = val;
                                        pl.socket.targetMapName = mapConfMod.name;
                                    }
                                }
                            } else if (idx === 5) {
                                currentRoom.mapType = val;
                            }
                        }
                        const changesStr = changes.map(v => typeof v === 'number' ? utils.encodeInt(v) : v).join(',');
                        broadcastToRoom(currentRoom, `e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3324)},(${changesStr})))\n`);
                        return;
                    }

                    // 3308: Alternar Preparado (Ready) / Confirmar herói
                    if (subCmd === 3308) {
                        if (!currentRoom) return;
                        let pSlot = -1;
                        for (let i = 0; i < 10; i++) {
                            if (currentRoom.players[i] && currentRoom.players[i].socket === socket) {
                                pSlot = i;
                                break;
                            }
                        }
                        if (pSlot === -1) return;
                        
                        const p = currentRoom.players[pSlot];
                        p.isReady = p.isReady ? 0 : 1;
                        
                        const rDataReady = buildRoomData(currentRoom);
                        broadcastToRoom(currentRoom, `e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3308)},(1,${rDataReady},'${p.socket.playerNameEnc}')))\n`);
                        return;
                    }

                    // 3313: Expulsar jogador
                    if (subCmd === 3313) {
                        if (!currentRoom) return;
                        let senderSlot = -1;
                        for (let i = 0; i < 10; i++) {
                            if (currentRoom.players[i] && currentRoom.players[i].socket === socket) {
                                senderSlot = i;
                                break;
                            }
                        }
                        if (senderSlot === -1 || !currentRoom.players[senderSlot].isCaptain) return;
                        
                        const targetNameDec = utils.decodeHex(subArgs[1]);
                        let targetSlot = -1;
                        for (let i = 0; i < 10; i++) {
                            if (currentRoom.players[i] && currentRoom.players[i].name === targetNameDec) {
                                targetSlot = i;
                                break;
                            }
                        }
                        
                        if (targetSlot !== -1) {
                            const targetSocket = currentRoom.players[targetSlot].socket;
                            currentRoom.players[targetSlot] = null;
                            
                            if (targetSocket) {
                                targetSocket.targetMapId = 5202;
                                targetSocket.targetMapName = 'bachang_baxi';
                                const rDataKick = buildRoomData(currentRoom);
                                targetSocket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3304)},(1,${utils.encodeInt(currentRoom.roomUID)},${rDataKick},'${targetSocket.playerNameEnc}',0,1)))\n`);
                            }
                            
                            broadcastToRoom(currentRoom, `e1:SendMsgToHallClient,(${utils.encodeInt(20013)},(${utils.encodeInt(3318)},(1,${buildRoomUserList(currentRoom)})))\n`);
                        }
                        return;
                    }

                    // 3320: Iniciar Partida da Sala
                    if (subCmd === 3320) {
                        if (!currentRoom) return;
                        const mapId = currentRoom.mapId;
                        const mapConf = MAP_CONFIG[mapId] || { name: 'bachang_baxi', type: 6 };
                        currentRoom.state = 4; // Jogando
                        
                        state.setLastMapId(mapId);
                        lastMapName = mapConf.name;
                        
                        const roomPlayers = currentRoom.players.filter(p => p !== null);
                        
                        const stagingMapId = 5010;
                        const stagingConf = MAP_CONFIG[stagingMapId];
                        
                        roomPlayers.forEach(p => {
                            p.socket.finalMapId = mapId; // MAPA DE COMBATE REAL
                            p.socket.targetMapId = stagingMapId; // STAGING MAP FOR SELECT HERO
                            p.socket.targetMapName = stagingConf.name;
                            p.socket.inMatch = true;
                            p.socket.force = p.force; // 1 ou 2
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
                            const playerIndex = idx + 1;
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
                            const prerogativeLobby = `(${isCaptainEnc},${isCaptainEnc},${isCaptainEnc},${isCaptainEnc},'${p.socket.playerNameEnc}',${zero},'',${zero})`;
                            p.socket.userData = `('${p.socket.playerNameEnc}',${ninetyNine},${one},${utils.encodeInt(p.force)},${one},${one},${heroIdEnc},${one},${one},${zero},${utils.encodeInt(cust.faceId)},'Local',${zero},${userDataHeroInfo},${one},${zero},${zero},${isCaptainEnc},${prerogativeLobby},())`;
                        });
                        
                        const BOT_NAMES = ['Bot_Lars', 'Bot_Gaya', 'Bot_Tanya', 'Bot_Lupin', 'Bot_Chaser', 'Bot_Buster', 'Bot_Windy', 'Bot_Kahn', 'Bot_Raptor', 'Bot_Slayer', 'Bot_Viper'];
                        let userListElements = [];
                        roomPlayers.forEach(p => {
                            userListElements.push(`'${p.socket.playerNameEnc}',${p.socket.userData}`);
                        });
                        
                        // Desativado: Preenche com bots até fechar 10 jogadores (5v5)
                        /*
                        for (let i = roomPlayers.length; i < 10; i++) {
                            const botName = BOT_NAMES[i % BOT_NAMES.length];
                            const forceEnc = utils.encodeInt(i < 5 ? 1 : 2); // 5 Blue, 5 Red
                            const isReadyEnc = utils.encodeInt(1);
                            const heroIdEnc = utils.encodeInt(1015 + (i % 5)); // Heróis diferentes
                            
                            const prerogativeLobby = `(${utils.encodeInt(0)},${utils.encodeInt(0)},${utils.encodeInt(0)},${utils.encodeInt(0)},'${utils.encodeHex(botName)}',${utils.encodeInt(0)},'',${utils.encodeInt(0)})`;
                            const userDataHeroInfo = `(${heroIdEnc},${utils.encodeInt(1)},${utils.encodeInt(1)},${utils.encodeInt(1)},${utils.encodeInt(0)})`;
                            const botUserData = `('${utils.encodeHex(botName)}',${utils.encodeInt(99)},${utils.encodeInt(1)},${forceEnc},${isReadyEnc},${utils.encodeInt(1)},${heroIdEnc},${utils.encodeInt(1)},${utils.encodeInt(1)},${utils.encodeInt(1)},${utils.encodeInt(1)},'Local',${utils.encodeInt(0)},${userDataHeroInfo},${utils.encodeInt(1)},${utils.encodeInt(0)},${utils.encodeInt(0)},${utils.encodeInt(0)},${prerogativeLobby},())`;
                            
                            userListElements.push(`'${utils.encodeHex(botName)}',${botUserData}`);
                        }
                        */
                        const userList = userListElements.join(',');
                        
                        const p80001 = utils.encodeInt(80001);
                        const s90001 = utils.encodeInt(90001);
                        const s90002 = utils.encodeInt(90002);
                        const p1304 = utils.encodeInt(1304);
                        
                        const matchMsg = `(${s90001},(${userList}),${utils.encodeInt(stagingMapId)},${utils.encodeInt(stagingConf.type)},'${stagingConf.name}',${utils.encodeInt(60)},${zero},'${dateStr}',${utils.encodeInt(1)},${zero})`;
                        
                        roomPlayers.forEach(p => {
                            (async () => {
                                await utils.sendPacket(p.socket, `e1:SendMsgToHallClient,(${p80001},${matchMsg})\n`, 400);
                                await utils.sendPacket(p.socket, `e1:SendMsgToHallClient,(${p80001},(${s90002},'${p.socket.playerNameEnc}',${p.socket.userData}))\n`, 400);
                                
                                setTimeout(() => {
                                    p.socket.write(`e1:SendMsgToHallClient,(${p1304},(${one},'${p.socket.playerNameEnc}','${SERVER_IP}',${utils.encodeInt(7777)},'SceneGroup',${one}))\n`);
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
                // 🧬 SISTEMA DE GENES / RUNAS (ID 19500)
                // ==========================================
                if (cmdId === 19500) {
                    const parsedArgs = utils.parseArrayList(textoRecebido.substring(textoRecebido.indexOf('(')));
                    const subCmdId = parsedArgs[0];
                    if (subCmdId === 19517) {
                        console.log(`[🧬 RUNE UPGRADE] Jogador: ${socket.playerName} solicitou upgrade de runa`);
                        const profile = dbPersistence.loadPlayer(socket.playerName);
                        if (profile) {
                            if (!profile.runes) profile.runes = { step: 0, star: 0 };
                            let runes = profile.runes;
                            
                            let nowStep = runes.step;
                            let nowStar = runes.star;
                            
                            let nextStep = nowStep;
                            let nextStar = nowStar + 1;
                            
                            if (nextStar > 10) {
                                nextStep = nowStep + 1;
                                nextStar = 1;
                            }
                            
                            // Limite de steps (0 a 49)
                            if (nextStep > 49) {
                                nextStep = 49;
                                nextStar = 10;
                            }
                            
                            runes.step = nextStep;
                            runes.star = nextStar;
                            
                            dbPersistence.savePlayer(socket.playerName, profile);
                            console.log(`[🧬 RUNE UPGRADE] Runa atualizada para: Step ${runes.step} | Star ${runes.star}`);
                            
                            const p19500 = utils.encodeInt(19500);
                            const p19517 = utils.encodeInt(19517);
                            const retOk = utils.encodeInt(1); // 1 = Sucesso
                            const stepEnc = utils.encodeInt(runes.step);
                            const starEnc = utils.encodeInt(runes.star);
                            const costEnc = utils.encodeInt(0); // Custo 0
                            
                            socket.write(`e1:SendMsgToHallClient,(${p19500},(${p19517},(${retOk},${stepEnc},${starEnc},${costEnc})))\n`);
                        }
                    }
                }

                // ==========================================
                // 💓 HEARTBEAT / PING (ID 100001)
                // ==========================================
                if (cmdId === 100001) {
                    socket.write(`e1:SendMsgToHallClient,(${utils.encodeInt(100001)},((0),(0),(0)))\n`);
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
                        if (s.socketType === "SCENE" && s !== socket && !s.destroyed) {
                            s.bypassInterceptor = true;
                            s.write(gameLogic.getPlayerSpawnPacket(socket, s));
                            s.bypassInterceptor = false;
                        }
                    });

                    // 5. Fecha a tela de seleção de heróis no cliente (90008)
                    const p20014 = utils.encodeInt(20014);
                    const s90008 = utils.encodeInt(90008);
                    const one = utils.encodeInt(1);
                    socket.write(`e1:SendMsgToClient,(${p20014},(${s90008},(),${one}))\n`);
                    return;
                }

                // ==========================================
                // 🏃 MOVIMENTO (ID 20001 - SP/SN)
                // ==========================================
                if (textoRecebido.includes("SP") || textoRecebido.includes("SN")) {
                    socket.write(`e1:SendMsgToClient,(${utils.encodeInt(20058)},('${utils.Fun.GetServerDate()}',${utils.encodeInt(0)}))\n`);
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

        // --- CENA 4: SERVIDOR DE CENA (PlayerUser) ---
        else if (socketType === "SCENE" && textoRecebido.includes("NGOL")) {
            console.log(`[🌐 SCENE] Jogador conectando ao socket de cena e aguardando seleção de herói...`);
            socket.isEncrypted = true;
            socket.waitingForHeroLock = true; // Flag essencial para suspender CLoadScene até o Lock!

            // 1. Força o Unity a entrar no modo de Jogo (HeroPlayer)
            socket.write(`mHeroPlayer:${socket.playerIndex || 1}\n`);

            // 2. NÃO ENVIAR 80005 AQUI! O 80005 DEVE SER ENVIADO QUANDO O MAPA STAGING (5010) CARREGAR!
            // Para não corromper o fundo da tela com bachang_baxi, esperamos o SLoadMapOKMsg do 5010.

            // 3. Envia o pacote 90005 (Início da Partida) via 20014
            const scnId = gameLogic.getSafeMapId(socket.targetMapId || state.getLastMapId() || 5202);
            const mapConf = MAP_CONFIG[scnId];
            const isChangeHero = (mapConf && (mapConf.type === 6 || mapConf.type === 7)) ? 1 : 0;
            const isChangeHeroEnc = utils.encodeInt(isChangeHero);
            const startTime = utils.Fun.GetServerDate();
            const p20014 = utils.encodeInt(20014);
            const s90005 = utils.encodeInt(90005);
            socket.write(`e1:SendMsgToClient,(${p20014},(${s90005},(${isChangeHeroEnc},'${startTime}')))\n`);
        }
        else if (socketType === "SCENE" && (textoRecebido.includes("SLoadMapOKMsg") || textoRecebido.includes("loadmapok:True"))) {
            console.log(`[🚀 SCENE] Mapa pronto! Spawnando herói padrão e abrindo seleção...`);
            global.activePaints = []; // Reset active sprays for the new map/match

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
            const mapConf = MAP_CONFIG[currentMapId];
            const isChangeHero = (mapConf && (mapConf.type === 6 || mapConf.type === 7)) ? 1 : 0;
            const isChangeHeroEnc = utils.encodeInt(isChangeHero);

            // SE AINDA ESTIVERMOS AGUARDANDO O LOCK DO HERÓI (Fase de Staging/Seleção), APENAS ABRA A UI (80005) E SUSPENDA O RESTO!
            if (socket.waitingForHeroLock) {
                socket.write(`e1:SendMsgToClient,(${p20014},(${s90005},(${isChangeHeroEnc},'${dateStr}')))\n`);
                socket.write(`e1:SendMsgToClient,(${p5000},(${s80005},()))\n`);
                console.log(`[🚀 ARENA UI] Mapa de Staging carregado. UI 80005 enviada com sucesso! Aguardando jogador dar Lock...`);
                return; // NÃO SPAWNA NINGUÉM AQUI!
            }

            // SE NÃO FOR 5010 (Ex: É 5204), INICIA A PARTIDA NORMALMENTE!
            const coords = gameLogic.getSpawnCoordinates(originalMapId, socket.force);
            let sx = coords.sx, sy = coords.sy, sz = coords.sz;
            socket.lastX = sx;
            socket.lastY = sy;
            socket.lastZ = sz;

            // 1. Inicia a partida e libera o herói na memória (com delay seguro de 1500ms para inicialização estável dos componentes da Unity no cliente local)
            setTimeout(() => {
                if (socket.destroyed) return;
                
                const pName = socket.playerName || (socket.remoteAddress && ipNames[socket.remoteAddress]);
                const profile = dbPersistence.loadPlayer(pName);
                if (!profile) return;

                const playerEq = state.playerEquipment[pName] || (profile && profile.equipment) || {};
                const eq = playerEq[heroId] || { skin: 1, weapon: 1, emote: 1, pose: 0 };
                const heroInfo = generateFullHeroData(heroId, eq.skin, eq.weapon, eq.emote, eq.pose, profile);

                socket.write(`e1:SendMsgToClient,(${p20014},(${s90005},(${isChangeHeroEnc},'${dateStr}')))\n`);
                socket.write(`e1:SendMsgToClient,(${p1306},(${p12002},(${heroIdEnc},${heroInfo.split(',').slice(1).join(',')})))\n`);

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

                // Broadcast do spawn para todos os outros jogadores na cena (verão como oponente index 2)
                activeSockets.forEach(s => {
                    if (s.socketType === "SCENE" && s !== socket && !s.destroyed) {
                        s.bypassInterceptor = true; // Protege o pacote
                        s.write(gameLogic.getPlayerSpawnPacket(socket, s));
                        s.bypassInterceptor = false; // Liga novamente
                    }
                });

                // Proativamente envia os spawns de todos os outros jogadores conectados para o novo jogador
                activeSockets.forEach(s => {
                    if (s.socketType === "SCENE" && s !== socket && !s.destroyed) {
                        socket.bypassInterceptor = true; // Protege o pacote
                        socket.write(gameLogic.getPlayerSpawnPacket(s, socket));
                        socket.bypassInterceptor = false; // Liga novamente
                    }
                });

                // 4. Destrava tudo (90008) - Fecha telas de seleção e loading no cliente
                socket.write(`e1:SendMsgToClient,(${p20014},(${s90008},(),${one}))\n`);
                console.log(`[🚀 SCENE] Spawn concluído com delay de 1500ms para o jogador ${socket.playerName}`);
                
                // Inicializa as barreiras e bases no mapa de forma genérica
                gameLogic.spawnTdmBases(socket, originalMapId);
            }, 1500);

            // const pLobby = socket.lobbySocket || state.getLastLobbySocket();
            // if (pLobby && !pLobby.destroyed) {
            //     pLobby.write(`e${socket.playerIndex || 1}:SendMsgToHallClient,(${utils.encodeInt(80001)},(${s90008},(),${one}))\n`);
            // }

            // Spawn/Atualização de NPCs na Arena de Treino se o mapa for bachang_baxi (Desativado)
            // const currentMapName = socket.targetMapName || lastMapName || 'bachang_baxi';
            // if (currentMapId === 5202 || currentMapName === 'bachang_baxi') {
            //     gameLogic.spawnPracticeNpcs(socket, true);
            // }

            // Inicializa a partida TDM ou Control com delay seguro de 1s (Pós-carregamento estável de cena)
            if (MAP_CONFIG[originalMapId] && MAP_CONFIG[originalMapId].type === 7) {
                setTimeout(() => {
                    gameLogic.startTdmMatch(socket);
                }, 1000);
            } else if (MAP_CONFIG[originalMapId] && MAP_CONFIG[originalMapId].type === 3) {
                setTimeout(() => {
                    gameLogic.startControlMatch(socket);
                }, 1000);
            } else {
                // Para todos os outros modos/mapas, abre a porta da base após um delay seguro de 5 segundos
                setTimeout(() => {
                    if (socket.destroyed) return;
                    const p1030000 = utils.encodeInt(1030000);
                    socket.write(`e${socket.playerIndex || 1}:SendMsgToClient,(${p1030000},(${utils.encodeInt(1)},'close_open'))\n`);
                    console.log(`[🚀 SCENE] Modo genérico: Portas da base abertas automaticamente (Mapa ID: ${originalMapId}, Tipo: ${MAP_CONFIG[originalMapId] ? MAP_CONFIG[originalMapId].type : 'unknown'})`);
                }, 5000);
            }
        }
        else if (socketType === "SCENE") {
            const lines = textoRecebido.split('\n');
            for (let line of lines) {
                line = line.trim();
                if (!line) continue;

                // Extrai o playerId (como e1, e2, etc.) de forma global no escopo da linha
                const idMatch = line.match(/^e(\w+):/);
                const packetPlayerId = idMatch ? idMatch[1] : "1";
                // CORREÇÃO CRÍTICA MULTIPLAYER: Não sobrescrever socket.playerId se já foi associado a partir do Lobby ou ipIndexes
                if (!socket.playerId) {
                    socket.playerId = String(socket.playerIndex || ipIndexes[socket.remoteAddress] || 1);
                }
                const playerId = socket.playerId;

                // 1. Tratamento do Ping/Sync (SN->CN, SP->CP, ST->CT, SR->CR, SA->CA)
                // Essencial para o anti-desync do cliente não ativar a flag 'isSleep' que trava a movimentação
                const syncMatch = line.match(/:(S[NPTRVA]),\((.+)\)/);
                if (syncMatch) {
                    const syncType = syncMatch[1]; // SN, SP, ST, SR, SV, SA
                    const syncArgs = syncMatch[2]; // ex: (Fkp,1c2,BNd),0,1c2 ou 0,0
                    
                    const clientType = "C" + syncType.charAt(1);

                    // Mantém as coordenadas do jogador atualizadas em tempo real para spawnar os bots à sua frente!
                    if (syncType === 'SP' || syncType === 'NSP') {
                        try {
                            const coordMatch = syncArgs.match(/\(([^)]+)\)/);
                            if (coordMatch) {
                                const parts = coordMatch[1].split(',');
                                const entId = utils.decodeInt(parts[0]);
                                // Apenas atualiza a posição com base no jogador (NetID 1, 10001, 10002 ou >= 524289)
                                if (entId === 1 || entId === 10001 || entId === 10002 || entId >= 524289) {
                                    const rawX = utils.decodeInt(parts[1]);
                                    const rawY = utils.decodeInt(parts[2]);
                                    const rawZ = utils.decodeInt(parts[3]);
                                    if (rawX && rawY && rawZ) {
                                        socket.lastX = rawX;
                                        socket.lastY = rawY;
                                        socket.lastZ = rawZ;
                                    }
                                }
                            }
                        } catch (e) {
                            // Silently ignore
                        }
                    }

                    // FAIL-SAFE: Se o jogador estiver em partida ativa mas não foi inicializado (devido a loading lento)
                    if (global.activeControlMatch && !socket.controlInitialized) {
                        console.log(`[🏆 CONTROL FAIL-SAFE] Jogador ${socket.playerName} ativo em cena. Inicializando fluxo do HUD...`);
                        gameLogic.startControlMatch(socket);
                    } else if (global.activeTdmMatch && !socket.tdmInitialized) {
                        console.log(`[🏆 TDM FAIL-SAFE] Jogador ${socket.playerName} ativo em cena. Inicializando fluxo do TDM...`);
                        gameLogic.startTdmMatch(socket);
                    }

                    // A função C# public void CN(int no) recebe UM ÚNICO INT PURO (Sem parênteses)!
                    if (syncType === 'SN') {
                        const pingVal = syncArgs.split(',')[1] || "0";
                        socket.write(`e${playerId}:CN,${pingVal}\n`);

                        // Envia periodicamente recarga do Dazhao (habilidade Ultimate Q) para garantir 100% de energia
                        try {
                            const p50001 = utils.encodeInt(50001);
                            const p50004 = utils.encodeInt(50004);
                            const zero = utils.encodeInt(0);
                            const maxEnergyEnc = utils.encodeInt(10000);
                            const chargePacket = `e${playerId}:SendMsgToAllClient,(${p50001},(${p50004},(${zero},${maxEnergyEnc},${zero})))\n`;
                            socket.write(chargePacket);
                        } catch (e) {
                            console.error("[⚠️ ERRO ao enviar recarga de Ultimate]:", e);
                        }
                    } 
                    // As funções C# public void CP(ArrayList arr), CT, CR, CV, CA recebem UMA ARRAYLIST!
                    // Os parênteses ao redor garantem que o CreateWithShortStr faça o wrap em um Arraylist.
                    else {
                        const packet = `e${playerId}:${clientType},((${syncArgs}))\n`;
                        activeSockets.forEach(s => {
                            if (s.socketType === "SCENE" && s !== socket && !s.destroyed) {
                                s.write(packet);
                            }
                        });
                        // CRITICAL: Echo CP back to the sender so playerCommon.pos stays updated.
                        // Without this, the anti-desync check in PlayerLocal.cs triggers isSleep=true
                        // because the distance between playerCommon.pos (never updated) and transform.position grows.
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
                        console.log(`[⚔️ AÇÃO DE BATALHA] ID: ${cmdId} (Base62: ${actionMatch[1]}) | DADOS: ${argsStr}`);

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
                                
                                // Processa atualizações de Control Point se houver partida ativa
                                if (global.activeControlMatch) {
                                    if (subCmd === 5017) {
                                        gameLogic.handleControlZonePlayersUpdate(socket, parsed);
                                    } else if (subCmd === 5015) {
                                        gameLogic.handleControlZoneCaptureUpdate(socket, parsed);
                                    }
                                }

                                // Sincroniza/retransmite para todos os outros SCENE sockets na sala
                                const subCmdEnc = utils.encodeInt(subCmd);
                                const argsListEnc = parsed.slice(1).map(val => {
                                    if (typeof val === 'number') return utils.encodeInt(val);
                                    if (Array.isArray(val)) return `(${val.map(v => typeof v === 'number' ? utils.encodeInt(v) : v).join(',')})`;
                                    return val;
                                });
                                activeSockets.forEach(s => {
                                    if (s.socketType === "SCENE" && s !== socket && !s.destroyed) {
                                        s.write(`e${s.playerIndex || 1}:SendMsgToClient,(${utils.encodeInt(5000)},(${subCmdEnc},(${argsListEnc.join(',')})))\n`);
                                    }
                                });
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 5016 packet]:", e);
                            }
                        }

                        // Se for 31203 (Reload / Fill Cartridge)
                        if (cmdId === 31203) {
                            if (socket.isDead || socket.isRagdoll) {
                                console.log(`[🚫 RELOAD BLOCKED] Jogador ${playerId} está morto e não pode recarregar!`);
                                return;
                            }
                            // Extrai o nome da arma (o primeiro item do ArrayList 'argsStr')
                            // argsStr vem como: (shootname, netid) -> exemplo: (1, 1)
                            const argParts = argsStr.replace(/\(/g, "").replace(/\)/g, "").split(",");
                            const shootName = argParts[0] || 1;
                            
                            // Responde imediatamente liberando a trava de recarregamento e enchendo balas no grupo 0
                            const currentHeroId = socket.currentHeroId || 1015;
                            const maxAmmo = weaponsMap[currentHeroId] && weaponsMap[currentHeroId].cartridgePublic !== undefined ? weaponsMap[currentHeroId].cartridgePublic : 30;
                            
                            const fillBulletPacket = `e${playerId}:OnFillBullet,((${shootName},0,${utils.encodeInt(maxAmmo)},${utils.encodeInt(maxAmmo)}))\n`;
                            socket.write(fillBulletPacket);
                            console.log(`[✅ SERVER REPLIES]: ${fillBulletPacket.trim()}`);
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
                                    
                                    const response = `e${socket.playerIndex || 1}:SendMsgToClient,(${utils.encodeInt(10095)},(${cidEnc},(${activePaintsList}),${subtypeEnc}))\n`;
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
                                            if (s.socketType === "SCENE" && !s.destroyed) {
                                                const broadcastPacket = `e${s.playerIndex || 1}:SendMsgToClient,(${utils.encodeInt(10095)},(${cidEnc},${sprayDataTuple},${subtypeEnc},${netIdEnc}))\n`;
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
                                const changeGroupPacket = `e${playerId}:ChangeWeaponGroupUpdate,((${utils.encodeInt(xingtai)}))\n`;
                                activeSockets.forEach(s => {
                                    if (s.socketType === "SCENE" && !s.destroyed) {
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
                                    const skillUpdatePacket = `e${playerId}:SkillDataUpdate,(${utils.encodeInt(31102)},((${skIdEnc},0,0,0,${utils.encodeInt(1)},${cdValEnc},0,0,0,0,${cdValEnc})))\n`;
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
                                    if (s.socketType === "SCENE" && !s.destroyed && s !== socket) {
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
                                const vec = skillArgs[1] || [0, 0, 0];
                                let targets = skillArgs[2] || [];
                                 
                                 // Correção de alvos para habilidades benéficas de grupo (Venus/Daji Ult 1153 e Angel Ult 1122)
                                 if (skillId === 1153 || skillId === 1122) {
                                     const casterForce = socket.force !== undefined ? socket.force : 1;
                                     targets = activeSockets
                                         .filter(s => s.socketType === "SCENE" && !s.destroyed && Number(s.force) === Number(casterForce) && String(s.playerId) !== String(playerId))
                                         .map(s => Number(s.playerId));
                                     console.log(`[🦊/🍃 GROUP SKILL FIX] Forçando alvos aliados (exceto caster) para a habilidade ${skillId}:`, targets);
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
                                        const shieldId = 20000 + Number(playerId);
                                        const hp = 1000;
                                        const maxHp = 1000;
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
                                        socket.lilyBarrierHp = 1000;
                                        socket.lilyBarrierMaxHp = 1000;
                                        socket.lilyBarrierActive = true;
                                        socket.lilyBarrierPos = veCreartPos;
                                        socket.lilyBarrierRot = quapos;
                                        console.log(`[🍃 LILY BARREIRA ATIVADA] Player ${playerId} - HP da barreira: 1000 | Pos: ${veCreartPos} | Rot: ${quapos}`);
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
                                const skillDoPacket = `e${playerId}:SkillDo,((${skillIdEnc},(${xEnc},${yEnc},${zEnc})),(${targetsStr}))\n`;
                                activeSockets.forEach(s => {
                                    if (s.socketType === "SCENE" && !s.destroyed) {
                                        s.write(skillDoPacket);
                                    }
                                });
                                console.log(`[⚔️ SKILL DO]: ${skillDoPacket.trim()}`);

                                 // Glider Ultimate Lock-on Start
                                 if (skillId === 1018) {
                                     socket.ultimateStart = Date.now();
                                     console.log(`[🚀 GLIDER ULTIMATE START] Registrado início da ult para o jogador ${playerId}`);
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
                                             targets.forEach(targetId => {
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

                                 // Intercepta Habilidades do Lamster (ID 2000) para aplicar o Buff 1007 (Inspiration)
                                  const currentHeroForSkill = socket.currentHeroId || 1015;
                                  if (currentHeroForSkill === 2000 && (skillId === 1121 || skillId === 1122)) {
                                      const inspirationDuration = 6000; // 6 segundos
                                      console.log(`[🔋 LAMSTER INSPIRATION] Aplicando Buff 1007 em si mesmo e nos alvos.`);
                                      
                                      // Aplica em si mesmo
                                      gameLogic.applyBuffToEntity(socket, playerId, 1007, inspirationDuration);
                                      
                                      // Aplica nos aliados atingidos pela habilidade
                                      if (Array.isArray(targets) && targets.length > 0) {
                                          targets.forEach(targetId => {
                                              gameLogic.applyBuffToEntity(socket, targetId, 1007, inspirationDuration);
                                          });
                                      }
                                      
                                      gameLogic.sendSystemChat(socket, `💪 [Inspiração] Buff "Inspiração" (+25% Dano) aplicado por 6 segundos!`);
                                  }

                                  // 2. Envia o SkillDataUpdate para iniciar o cooldown e travar spam
                                 let actualCd = 10000; // Fallback default de 10s
                                 if (playerSkillsMap[skillId] !== undefined) {
                                     actualCd = playerSkillsMap[skillId].cd;
                                 }

                                 if (socket.nocd) {
                                     actualCd = 0;
                                 }

                                 const actualCdEnc = utils.encodeInt(actualCd);
                                 const skillUpdatePacket = `e${playerId}:SkillDataUpdate,(${utils.encodeInt(31102)},((${skillIdEnc},0,0,0,${utils.encodeInt(1)},${actualCdEnc},0,0,0,0,${actualCdEnc})))\n`;

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
                                     const nocdUpdatePacket = `e${playerId}:SkillDataUpdate,(${utils.encodeInt(31102)},((${skillIdEnc},0,${utils.encodeInt(1)},0,${utils.encodeInt(1)},${zeroEnc},0,0,0,0,${zeroEnc})))\n`;
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
                                // NÃO decodificamos mais aqui, pois utils.parseArrayList já realiza a decodificação de Base62 para inteiros automaticamente!
                                const skillId = cdArgs[0];
                                const skillIdEnc = utils.encodeInt(skillId);
                                const one = utils.encodeInt(1);
                                
                                const skillUpdatePacket = `e${playerId}:SkillDataUpdate,(${utils.encodeInt(31102)},((${skillIdEnc},0,${one},0,${one},0,0,0,0,0,0)))\n`;
                                socket.write(skillUpdatePacket);
                                console.log(`[🔋 SKILL CD END REFILLED]: ${skillUpdatePacket.trim()}`);
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 31104 skill CD end]:", e);
                            }
                        }

                        // Se for 31208 (MSG_PLAYERSKILL_SUPPERSKILL - Ativação da Ultimate Q)
                        if (cmdId === 31208) {
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
                                // 🔮 GUANYIN / LAMSTER RESURRECTION ULTIMATE
                                // ==========================================
                                const currentHeroForRevive = socket.currentHeroId || 1015;
                                if (currentHeroForRevive === 2000 || currentHeroForRevive === 1019) {
                                    const reviveArgs = utils.parseArrayList(argsStr);
                                    console.log(`[🔮 GUANYIN REVIVE] Parsed szReliveId:`, JSON.stringify(reviveArgs));
                                    
                                    if (reviveArgs.length >= 2) {
                                        for (let ri = 0; ri < reviveArgs.length; ri += 2) {
                                            const reviveTargetId = reviveArgs[ri];
                                            const reviveCoords = reviveArgs[ri + 1]; // [x*100, y*100, z*100]
                                            
                                            if (!reviveTargetId || !Array.isArray(reviveCoords) || reviveCoords.length < 3) {
                                                console.log(`[⚠️ GUANYIN REVIVE] Dados inválidos no índice ${ri}:`, reviveTargetId, reviveCoords);
                                                continue;
                                            }
                                            
                                            // Encontra o socket do jogador alvo
                                            const targetSocket = activeSockets.find(s => 
                                                s.socketType === "SCENE" && !s.destroyed && 
                                                (s.playerId === reviveTargetId || String(s.playerId) === String(reviveTargetId) ||
                                                 s.playerIndex === reviveTargetId || String(s.playerIndex) === String(reviveTargetId))
                                            );
                                            
                                            if (!targetSocket) {
                                                console.log(`[⚠️ GUANYIN REVIVE] Jogador ${reviveTargetId} não encontrado nos sockets ativos.`);
                                                continue;
                                            }
                                            
                                            // Restaura HP total
                                            const maxHp = targetSocket.maxHp || 2000;
                                            targetSocket.isDead = false;
                                            if (targetSocket.respawnTimeout) {
                                                clearTimeout(targetSocket.respawnTimeout);
                                                targetSocket.respawnTimeout = null;
                                            }
                                            
                                            // Envia OnRevive para limpar o estado de morte no cliente
                                            const targetPlayerIdEnc = utils.encodeInt(Number(targetSocket.playerId || 1));
                                            const revivePacket = `e${targetPlayerIdEnc}:OnRevive,(())\n`;
                                            activeSockets.forEach(s => {
                                                if (s.socketType === "SCENE" && !s.destroyed) {
                                                    s.write(revivePacket);
                                                }
                                            });

                                            targetSocket.hp = maxHp;
                                            targetSocket.shield = 0;
                                            targetSocket.isInvincible = false;
                                            targetSocket.godMode = false;
                                            
                                            // Teleporta para a posição do corpo (coords são x*100, y*100, z*100)
                                            targetSocket.lastX = Math.round(reviveCoords[0] / 100);
                                            targetSocket.lastY = Math.round(reviveCoords[1] / 100);
                                            targetSocket.lastZ = Math.round(reviveCoords[2] / 100);
                                            
                                            // Envia HP update
                                            gameLogic.sendEntityHpUpdate(targetSocket, targetSocket.playerId || "1", targetSocket, true);
                                            
                                            // Broadcast spawn packet para todos os jogadores
                                            activeSockets.forEach(s => {
                                                if (s.socketType === "SCENE" && !s.destroyed) {
                                                    s.bypassInterceptor = true;
                                                    s.write(gameLogic.getPlayerSpawnPacket(targetSocket, s));
                                                    s.bypassInterceptor = false;
                                                }
                                            });
                                            
                                            console.log(`[✅ GUANYIN REVIVE] Ressuscitou jogador ${targetSocket.playerName || reviveTargetId} em (${targetSocket.lastX}, ${targetSocket.lastY}, ${targetSocket.lastZ}) com ${maxHp} HP!`);
                                        }
                                        
                                        gameLogic.sendGlobalSystemChat(`🔮 [Ressurreição] ${socket.playerName || 'Guanyin'} ressuscitou aliados caídos!`);
                                    }
                                    
                                    // Executa animação da ultimate no caster (SuperSkillUpdate)
                                    const zeroR = utils.encodeInt(0);
                                    const ultExecuteRevive = `e${playerId}:SuperSkillUpdate,(${zeroR})\n`;
                                    activeSockets.forEach(s => {
                                        if (s.socketType === "SCENE" && !s.destroyed) {
                                            s.write(ultExecuteRevive);
                                        }
                                    });
                                }
                                const d50001 = utils.encodeInt(50001);
                                const d50005 = utils.encodeInt(50005);
                                const one = utils.encodeInt(1);
                                
                                // Determina dinamicamente a duração da Ultimate baseada no HeroID do socket
                                const currentHero = socket.currentHeroId || 1015;
                                const dazhaoList = heroDazhaoMap[currentHero] || [];
                                let duration = 3000; // Fallback default
                                if (dazhaoList.length > 0) {
                                    // Pega o continuestime do primeiro dazhao
                                    duration = dazhaoList[0].continueTime || 3000;
                                }
                                
                                const durationEnc = utils.encodeInt(duration); 
                                
                                // 1. Mostra a barra de duração do Dazhao no cliente de todos
                                const ultStartPacket = `e${playerId}:SendMsgToAllClient,(${d50001},(${d50005},(${one},'',${durationEnc})))\n`;
                                activeSockets.forEach(s => {
                                    if (s.socketType === "SCENE" && !s.destroyed) {
                                        s.write(ultStartPacket);
                                    }
                                });
                                
                                // 2. Aciona a execução da Ultimate (SuperSkillUpdate) no cliente para animar e castar a habilidade!
                                const zero = utils.encodeInt(0);
                                const ultExecutePacket = `e${playerId}:SuperSkillUpdate,(${zero})\n`;
                                activeSockets.forEach(s => {
                                    if (s.socketType === "SCENE" && !s.destroyed) {
                                        s.write(ultExecutePacket);
                                    }
                                });
                                
                                console.log(`[✅ SERVER BROADCASTED ULTIMATE START & EXECUTE]: Hero ${currentHero} | Duration: ${duration}ms | Packet: ${ultStartPacket.trim()}`);

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
                                             // Registra para cancelamento prematuro
                                             if (!socket.activeUltimateBuffs.includes(buffId)) {
                                                 socket.activeUltimateBuffs.push(buffId);
                                             }

                                             // Aplica o buff dinamicamente através do Motor do Servidor
                                             gameLogic.applyBuffToEntity(socket, playerId, buffId, duration);
                                         });
                                      }
                                      if (doskill === 1013) {
                                          console.log(`[🎯 ALICE ULTIMATE ACTIVE] Applying Wallhack (Buff 1005) to all enemies for ${duration}ms!`);
                                          const casterForce = socket.force !== undefined ? socket.force : 1;
                                          
                                          // 1. Jogadores Inimigos
                                          activeSockets.forEach(s => {
                                              if (s.socketType === "SCENE" && !s.destroyed && s.playerId !== playerId) {
                                                  const enemyForce = s.force !== undefined ? s.force : 1;
                                                  if (Number(casterForce) !== Number(enemyForce)) {
                                                      gameLogic.applyBuffToEntity(socket, s.playerId, 1005, duration);
                                                  }
                                              }
                                          });
                                          
                                          // 2. NPCs Inimigos
                                          for (const npcId in spawnedNpcs) {
                                              const npc = spawnedNpcs[npcId];
                                              if (npc && !npc.destroyed) {
                                                  const enemyForce = npc.force !== undefined ? npc.force : 2;
                                                  if (Number(casterForce) !== Number(enemyForce)) {
                                                      gameLogic.applyBuffToEntity(socket, npcId, 1005, duration);
                                                  }
                                              }
                                          }
                                      }
                                 }

                                 
                                if (socket.ultimateTimer) {
                                    clearTimeout(socket.ultimateTimer);
                                }
                                socket.ultimateTimer = setTimeout(() => {
                                    if (!socket.destroyed) {
                                        const ultEndPacket = `e${playerId}:SendMsgToAllClient,(${d50001},(${d50005},(${zero},'',${zero})))\n`;
                                        activeSockets.forEach(s => {
                                            if (s.socketType === "SCENE" && !s.destroyed) {
                                                s.write(ultEndPacket);
                                            }
                                        });
                                        console.log(`[💀 ULTIMATE TIMER EXPIRED] Sending: ${ultEndPacket.trim()}`);
                                    }
                                    socket.ultimateTimer = null;
                                }, duration);
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 31208 Ultimate Start]:", e);
                            }
                        }

                        // Se for 31211 (MSG_PLAYERSKILL_SUPPERSKILLEND - Fim da Ultimate)
                        if (cmdId === 31211) {
                            try {
                                console.log(`[💀 ULTIMATE END] Ultimate terminou! Desativando CDazhao flag...`);
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
                                const ultEndPacket = `e${playerId}:SendMsgToAllClient,(${d50001},(${d50005},(${zero},'',${zero})))\n`;
                                activeSockets.forEach(s => {
                                    if (s.socketType === "SCENE" && !s.destroyed) {
                                        s.write(ultEndPacket);
                                    }
                                });
                                console.log(`[✅ SERVER BROADCASTED ULTIMATE END]: ${ultEndPacket.trim()}`);
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 31211 Ultimate End]:", e);
                            }
                        }

                        // Se for 2010001 (XingTian Shield State / MSG_SHIELD_STATE)
                        if (cmdId === 2010001) {
                            try {
                                const parsed = utils.parseArrayList(argsStr);
                                const state = Number(parsed[0]);
                                if (state === 1) {
                                    socket.shield = 10000;
                                    socket.maxShield = 10000;
                                    console.log(`[🛡️ TAGALOA SHIELD ACTIVATE] Player ${playerId} activated shield with HP 10000`);
                                } else {
                                    socket.tagaloaShieldHp = 10000;
                                    socket.shield = 0;
                                    socket.maxShield = 0;
                                    console.log(`[🛡️ TAGALOA SHIELD DEACTIVATE] Player ${playerId} deactivated shield. Saved HP: 10000`);
                                }
                                
                                // Send SetHp packet to update shield state for everyone
                                gameLogic.sendEntityHpUpdate(socket, playerId, socket, true);
                                
                                const p2010000 = utils.encodeInt(2010000);
                                const p2010002 = utils.encodeInt(2010002);
                                const stateEnc = utils.encodeInt(state);
                                const currentShieldHp = Math.round(state === 1 ? socket.shield : (socket.tagaloaShieldHp !== undefined ? socket.tagaloaShieldHp : 10000));
                                const energyEnc = utils.encodeInt(currentShieldHp);
                                const maxEnergyEnc = utils.encodeInt(10000);
                                
                                const resp = `e${playerId}:SendMsgToClient,(${p2010000},(${p2010002},(${stateEnc},${energyEnc},${maxEnergyEnc})))\n`;
                                socket.write(resp);
                                console.log(`[🛡️ XINGTIAN SHIELD SYNC]: ${resp.trim()}`);
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 2010001 XingTian Shield]:", e);
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
                                
                                const resp = `e${playerId}:SendMsgToAllClient,(${p2010002},(${playerIdEnc},(${typeEnc},${energyEnc},${maxEnergyEnc}),${posEnc},${relesPosEnc}))\n`;
                                activeSockets.forEach(s => {
                                    if (s.socketType === "SCENE" && !s.destroyed) {
                                        s.write(resp);
                                    }
                                });
                                console.log(`[🦊 VENUS BARREIRA SYNC]: ${resp.trim()}`);
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 2100001 DaJi Shield]:", e);
                            }
                        }

                        // Se for 2210002 (Lily Shield Position Sync from client)
                        if (cmdId === 2210002) {
                            try {
                                const parsed = utils.parseArrayList(argsStr);
                                const shieldId = Number(parsed[1]);
                                const pos = parsed[2] || [0, 0, 0];
                                
                                socket.lilyBarrierPos = pos;
                                
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
                                
                                const resp = `e${playerId}:SendMsgToAllClient,(${p2210001},(${targetIdEnc},(${shieldIdEnc},${hpEnc},${maxHpEnc},${dateStr},${creatorIdEnc},(${xEnc},${yEnc},${zEnc}),(${rxEnc},${ryEnc},${rzEnc})))))\n`;
                                activeSockets.forEach(s => {
                                    if (s.socketType === "SCENE" && s !== socket && !s.destroyed) {
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
                                const masterSocket = activeSockets.find(s => s.socketType === "SCENE" && !s.destroyed && (s.playerId === momId || String(s.playerId) === String(momId)));
                                const npcForce = masterSocket ? (masterSocket.force !== undefined ? masterSocket.force : force) : force;

                                // Busca dados do NPC no banco de dados xml
                                const npcConf = db.npcData[npcIndex] || { hp: 100, shield: 0, name: "NPC", time: 0, weaponIndex: 0 };
                                const maxHp = npcConf.hp;
                                const npcTime = npcConf.time || 0;

                                console.log(`[🤖 ENTITY SPAWN] Spawning Entity: ${npcConf.name} (Index: ${npcIndex}, ID: ${npcId}) with Lifetime: ${npcTime}ms for Player: ${momId} (Force: ${npcForce}) at (${x}, ${y}, ${z})`);

                                // Track the spawned NPC on the server
                                spawnedNpcs[npcId] = {
                                    id: npcId,
                                    index: npcIndex,
                                    hp: maxHp,
                                    maxHp: maxHp,
                                    x: x,
                                    y: y,
                                    z: z,
                                    masterId: momId,
                                    force: npcForce,
                                    weaponIndex: npcConf.weaponIndex || 0
                                };

                                global.npcHistory = global.npcHistory || {};
                                global.npcHistory[npcId] = spawnedNpcs[npcId];

                                // Envia a inicialização da entidade diretamente para todos os clientes ativos
                                const className = getEntityClassName(npcIndex);
                                 activeSockets.forEach(s => {
                                     if (s.socketType === "SCENE" && !s.destroyed) {
                                         const spawnPacket = `c${className}:${utils.encodeInt(npcId)},("Initialize",(${utils.encodeInt(momId)},${utils.encodeInt(npcId)},${utils.encodeInt(npcIndex)},${utils.encodeInt(npcForce)},(${utils.encodeInt(x)},${utils.encodeInt(y)},${utils.encodeInt(z)}),(${utils.encodeInt(rx)},${utils.encodeInt(ry)},${utils.encodeInt(rz)}),(${utils.encodeInt(sx)},${utils.encodeInt(sy)},${utils.encodeInt(sz)}),(${utils.encodeInt(fx)},${utils.encodeInt(fy)},${utils.encodeInt(fz)}),${utils.encodeInt(targetId)},${utils.encodeInt(maxHp)},${utils.encodeInt(npcTime)},${utils.encodeInt(1)},${utils.encodeInt(momType)},'${colliderName}'),((),()),0)\n`;
                                         s.write(spawnPacket);
                                     }
                                 });
                                 console.log(`[🤖 ENTITY SPAWNED] Index: ${npcIndex}, ID: ${npcId}`);
                                 
// ⏳ LIFETIME TIMER PARA ENTIDADES COM TEMPO DEFINIDO (Time > 0)
                                if (npcTime > 0) {
                                    setTimeout(() => {
                                        if (spawnedNpcs[npcId] !== undefined) {
                                            const targetNpc = spawnedNpcs[npcId];
                                            delete spawnedNpcs[npcId];
                                            const destPacket = `d${utils.encodeInt(npcId)}\n`;
                                            activeSockets.forEach(s => {
                                                if (s.socketType === "SCENE" && !s.destroyed) {
                                                    s.write(destPacket);
                                                }
                                            });
                                            console.log(`[⏳ ENTITY LIFETIME EXPIRED] NPC ID: ${npcId} (Index: ${npcIndex}, Name: ${npcConf.name}) destroyed after ${npcTime}ms.`);
                                            
                                            // Scarlett (1022) Ultimate snowball (1013) camera tracking reset upon lifetime expiration
                                            if (targetNpc.index === 1013) {
                                                const creatorSocket = activeSockets.find(s => s.socketType === "SCENE" && !s.destroyed && Number(s.playerId) === Number(targetNpc.masterId));
                                                if (creatorSocket) {
                                                    creatorSocket.write(`e${creatorSocket.playerId}:SuperSkillFinish\n`);
                                                    creatorSocket.write(`e${creatorSocket.playerIndex || 1}:SendMsgToClient,(${utils.encodeInt(20014)},(${utils.encodeInt(90008)},(),${utils.encodeInt(1)}))\n`);
                                                    console.log(`[❄️ SNOWBALL RESET] Scarlett Camera reset: e${creatorSocket.playerId}:SuperSkillFinish`);
                                                }
                                            }
                                        }
                                    }, npcTime);
                                }
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 1010001 Create Npc]:", e);
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
                                        const creatorSocket = activeSockets.find(s => s.socketType === "SCENE" && !s.destroyed && Number(s.playerId) === Number(targetNpc.masterId));
                                        if (creatorSocket) {
                                            creatorSocket.write(`e${creatorSocket.playerId}:SuperSkillFinish\n`);
                                            creatorSocket.write(`e1:SendMsgToClient,(${utils.encodeInt(20014)},(${utils.encodeInt(90008)},(),${utils.encodeInt(1)}))\n`);
                                            console.log(`[❄️ SNOWBALL RESET] Scarlett Camera reset on suicide: e${creatorSocket.playerId}:SuperSkillFinish`);
                                        }
                                    }
                                    delete spawnedNpcs[npcId];
                                }

                                // Envia a destruição da entidade diretamente para todos os clientes ativos
                                const destPacket = `d${utils.encodeInt(npcId)}\n`;
                                activeSockets.forEach(s => {
                                    if (s.socketType === "SCENE" && !s.destroyed) {
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

                                // Envia a confirmação de validação de sucesso de volta
                                const p1020002 = utils.encodeInt(1020002);
                                const npcIdEnc = utils.encodeInt(npcId);
                                const resp = `e${playerId}:SendMsgToClient,(${p1020002},(${npcIdEnc}))\n`;
                                socket.write(resp);
                                console.log(`[🤖 ENTITY SYNCED]: ${resp.trim()}`);
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 1020001 NPC Sync Validate]:", e);
                            }
                        }

                        // Se for 20014 (🎭 SELEÇÃO/TROCA DE HERÓI EM SCENE SOCKET - MUDANÇA DE PERSONAGEM EM JOGO)
                        if (cmdId === 20014) {
                            try {
                                const parsed = utils.parseArrayList(argsStr);
                                const subCmd = parsed[0]; // 90002
                                if (subCmd === 90002) {
                                    const selectedHeroId = parsed[1];
                                    const isLocked = parsed[2] || 0; // 0 = preview, 1 = locked
                                    
                                    console.log(`[🎭 SELEÇÃO DE HERÓI] Jogador ${socket.playerNameEnc} selecionou HeroID ${selectedHeroId} (Locked: ${isLocked})`);
                                    
                                    if (isLocked === 1) {
                                        gameLogic.switchPlayerHero(socket, selectedHeroId);
                                    } else {
                                        // Apenas atualiza a visualização/preview do herói na tela
                                        socket.currentHeroId = selectedHeroId;
                                        
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
                                        const myUserData = `('${socket.playerNameEnc}',${utils.encodeInt(99)},${one},${forceEnc},${isReadyEnc},${one},${heroIdEnc},${one},${one},${zero},${utils.encodeInt(cust.faceId)},'Local',${zero},${userDataHeroInfo},${one},${zero},${zero},${isCapEnc},${prerogativeLobby},())`;
                                        
                                        const resp = `e${playerId}:SendMsgToClient,(${p20014},(${s90002},'${socket.playerNameEnc}',${myUserData}))\n`;
                                        
                                        // Broadcast para todos os jogadores na cena verem o preview do herói
                                        activeSockets.forEach(s => {
                                            if (s.socketType === "SCENE" && !s.destroyed) {
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
                                const targetSocket = activeSockets.find(s => s.socketType === "SCENE" && s.playerIndex === playerNetId && !s.destroyed);
                                if (targetSocket) {
                                    targetSocket.inJidi = state;
                                }
                                
                                // Broadcast de 52002 usando NetID absoluto para cada cliente da cena
                                const p52002 = utils.encodeInt(52002);
                                const stateEnc = utils.encodeInt(state);
                                
                                activeSockets.forEach(s => {
                                    if (s.socketType === "SCENE" && !s.destroyed) {
                                        const playerNetIdEnc = utils.encodeInt(playerNetId);
                                        const resp = `e${s.playerIndex || 1}:SendMsgToClient,(${p52002},(${playerNetIdEnc},${stateEnc}))\n`;
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
                                    if (s.socketType === "SCENE" && !s.destroyed) {
                                        const szData = `(${szNetMsg},'${sound}','${anim}',${utils.encodeInt(type)},${utils.encodeInt(type)},${forceEnc},${netId},'${ico}',${utils.encodeInt(cdReady)},${utils.encodeInt(onGround)},${utils.encodeInt(bePlayActive)},${utils.encodeInt(nHuDongState)})`;
                                        const broadcastPacket = `e${s.playerIndex || 1}:SendMsgToClient,(${p2000},(${p2001},${szData}))\n`;
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
                                    if (s.socketType === "SCENE" && !s.destroyed) {
                                        const broadcastPacket = `e${s.playerIndex || 1}:SendMsgToClient,(${p1006},${szNetMsg})\n`;
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

                                        if (cmdName === 'bot' || cmdName === 'spawn' || cmdName === 'dummy') {
                                             const currentMapId = socket.targetMapId || state.getLastMapId() || 5202;
                                             const defaultCoords = gameLogic.getSpawnCoordinates(currentMapId, socket.force);
                                             const px = socket.lastX || defaultCoords.sx;
                                             const py = socket.lastY || defaultCoords.sy;
                                             const pz = socket.lastZ || defaultCoords.sz;

                                             let spawnX = px;
                                             let spawnY = py;
                                             let spawnZ = pz + 500;

                                             // Suporta coordenadas enviadas pelo mod menu do cliente C#
                                             if (commandParts.length >= 5) {
                                                 const parsedX = parseInt(commandParts[2]);
                                                 const parsedY = parseInt(commandParts[3]);
                                                 const parsedZ = parseInt(commandParts[4]);
                                                 if (!isNaN(parsedX) && !isNaN(parsedY) && !isNaN(parsedZ)) {
                                                     spawnX = parsedX;
                                                     spawnY = parsedY;
                                                     spawnZ = parsedZ;
                                                 }
                                             }

                                            const botNetId = 15000 + Math.floor(Math.random() * 5000);

                                            let npcIndex = parseInt(cmdArg) || 1121;
                                            let npcMaxHp = 10;
                                            if (npcIndex === 1122) {
                                                npcMaxHp = 1000;
                                            } else if (npcIndex === 1101) {
                                                npcMaxHp = 1500;
                                            } else {
                                                const npcConf = db.npcData[npcIndex] || {};
                                                npcMaxHp = parseInt(npcConf.hp) || 500;
                                            }

                                            const dummyForce = (socket.force === 1) ? 2 : 1;
                                            spawnedNpcs[botNetId] = { id: botNetId, index: npcIndex, hp: npcMaxHp, maxHp: npcMaxHp, x: spawnX, y: spawnY, z: spawnZ, force: dummyForce, isPracticeDummy: true };
                                            const className = getEntityClassName(npcIndex);
                                            activeSockets.forEach(s => {
                                                if (s.socketType === "SCENE" && !s.destroyed) {
                                                    const spawnPacket = `c${className}:${utils.encodeInt(botNetId)},("Initialize",(${utils.encodeInt(1)},${utils.encodeInt(botNetId)},${utils.encodeInt(npcIndex)},${utils.encodeInt(spawnedNpcs[botNetId].force)},(${utils.encodeInt(spawnX)},${utils.encodeInt(spawnY)},${utils.encodeInt(spawnZ)}),(0,0,0),(${utils.encodeInt(100)},(${utils.encodeInt(100)}),(${utils.encodeInt(100)})),(0,0,0),${utils.encodeInt(-1)},${utils.encodeInt(npcMaxHp)},${utils.encodeInt(0)},${utils.encodeInt(1)},${utils.encodeInt(0)},""),((),()),0)\n`;
                                                    s.write(spawnPacket);
                                                }
                                            });
                                            console.log(`[🤖 NPC SPAWN VIA COMANDO] Bot ID: ${botNetId}`);
                                            
                                            // Envia mensagem do sistema amarela no canal 8 confirmando o spawn
                                            const sysHex = Buffer.from(`[Sistema] Bot ${botNetId} (HP: ${npcMaxHp}) spawnado na sua frente!`).toString('hex');
                                            const sysNetMsg = `(${utils.encodeInt(8)},'','[mi]${sysHex}[/mi]','',(),(),${pCid},('','',0),())`;
                                            const sysBroadcast = `e1:SendMsgToClient,(${p1006},${sysNetMsg})\n`;
                                            socket.write(sysBroadcast);
                                         } else if (cmdName === 'hero' || cmdName === 'heroi' || cmdName === 'char') {
                                             const selectedHeroId = parseInt(cmdArg);
                                             if (selectedHeroId) {
                                                 gameLogic.switchPlayerHero(socket, selectedHeroId);
                                                 
                                                 const sysHex = Buffer.from(`[Sistema] Mudando de personagem para o ID ${selectedHeroId}!\n`).toString('hex');
                                                 const sysNetMsg = `(${utils.encodeInt(8)},'','[mi]${sysHex}[/mi]','',(),(),${pCid},('','',0),())`;
                                                 const sysBroadcast = `e1:SendMsgToClient,(${p1006},${sysNetMsg})\n`;
                                                 socket.write(sysBroadcast);
                                             }
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
                                                 const revivePacket = `e${targetPlayerIdEnc}:OnRevive,(())\n`;
                                                 activeSockets.forEach(s => {
                                                     if (s.socketType === "SCENE" && !s.destroyed) {
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
                                              const revivePacket = `e${targetPlayerIdEnc}:OnRevive,(())\n`;
                                              activeSockets.forEach(s => {
                                                  if (s.socketType === "SCENE" && !s.destroyed) {
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
                                                      if (s.socketType === "SCENE" && !s.destroyed) {
                                                          s.write(deadPacket);
                                                      }
                                                  });
                                                  gameLogic.sendSystemChat(socket, "🤪 [Mod Menu] MODO BONECO ATIVADO! Digite /ragdoll novamente para se levantar.");
                                              } else {
                                                  const revivePacket = `e${targetPlayerIdEnc}:OnRevive,(())\n`;
                                                  activeSockets.forEach(s => {
                                                      if (s.socketType === "SCENE" && !s.destroyed) {
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
                    const sdgMatch = line.match(/^e(\w+):(?:.*?:)?SDG,\((.+)\)/);
                    if (sdgMatch) {
                        try {
                            const playerId = sdgMatch[1];
                            const argsStr = sdgMatch[2];
                            const parsed = utils.parseArrayList(argsStr);
                            
                            // Safe dynamic hit extraction to handle any level of nested array wrapping
                            let hits = [];
                            function extractHits(arr) {
                                if (!Array.isArray(arr)) return;
                                if (arr.length >= 3 && !Array.isArray(arr[0]) && !Array.isArray(arr[1])) {
                                    hits.push(arr);
                                    return;
                                }
                                for (const item of arr) {
                                    if (Array.isArray(item)) {
                                        extractHits(item);
                                    }
                                }
                            }
                            extractHits(parsed);
                            console.log(`[🔬 SDG] parsed.slice(0,15)=${JSON.stringify(parsed.slice(0,15))} hits=${hits.length}`);

                            // Decodifica a ID da arma/herói e o shootname correspondente usando a base64 real
                            const rawWeaponId = parsed[14];
                            const rawShootname = parsed[1];
                            
                            let resolvedWeaponId = rawWeaponId ? utils.decodeInt(rawWeaponId) : null;
                            let resolvedShootname = rawShootname ? utils.decodeInt(rawShootname).toString() : null;

                            // Fallback robusto se resolvedWeaponId não for encontrado na base de dados
                            if (!resolvedWeaponId || !weaponsMap[resolvedWeaponId]) {
                                const attackerSocket = activeSockets.find(s => s.socketType === "SCENE" && !s.destroyed && (s.playerId === playerId || String(s.playerId) === String(playerId)));
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

                             // Shogun (1024) Arrow scaling
                             if (weaponId === 1024 && shootname === "1") {
                                 const chargeMs = (typeof parsed[5] === 'number') ? parsed[5] : (parsed[5] ? Number(parsed[5]) : 0);
                                 baseDamage = 150 + Math.round((900 - 150) * Math.min(1.0, Math.max(0.0, chargeMs / 1000.0)));
                                 canHeadshot = true;
                                 console.log(`[🏹 SHOGUN CHARGE] chargeMs: ${chargeMs} | baseDamage escalado: ${baseDamage}`);
                             }

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

                                 const attackerId = rawAttackerId !== undefined ? utils.decodeInt(rawAttackerId) : null;
                                 const targetId = rawTargetId !== undefined ? utils.decodeInt(rawTargetId) : null;
                                 const boomHead = rawBoomHead !== undefined ? utils.decodeInt(rawBoomHead) : 0;
                                 const distance = rawDistance !== undefined ? utils.decodeInt(rawDistance) : 0;
                                 
                                 // Diagnóstico detalhado de cada hit
                                 const _dbgPlayerTarget = activeSockets.find(s => s.socketType === "SCENE" && !s.destroyed && (s.playerId === targetId || String(s.playerId) === String(targetId)));
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
                                         if (attackerNpc.index === 1005 || String(attackerNpc.index) === "1005") {
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

                                 console.log(`[💥 HIT] Atacante: ${attackerId} (raw: ${rawAttackerId}) | Alvo: ${targetId} (raw: ${rawTargetId}) | WeaponId: ${loopWeaponId} | Shootname: ${loopShootname} | Parte: ${collName} | Headshot: ${boomHead === 1}`);
                                let target = null;
                                let isPlayerTarget = false;
                                
                                // 1. Procurar em activeSockets para PvP e suporte a hits em outros jogadores
                                let foundPlayer = activeSockets.find(s => s.socketType === "SCENE" && !s.destroyed && (s.playerId === targetId || String(s.playerId) === String(targetId)));
                                if (foundPlayer) {
                                    target = foundPlayer;
                                    isPlayerTarget = true;
                                } else if (spawnedNpcs[targetId]) {
                                    target = spawnedNpcs[targetId];
                                }

                                    if (target) {

                                        if (target.isDead) {
                                        console.log(`[🛡️ HIT BLOCKED] Alvo ${targetId} está morto e não pode ser afetado.`);
                                        continue;
                                    }
                                    // Resolvendo o Hero ID do atacante (suportando jogadores ativos e NPCs)
                                    const attackerSocket = activeSockets.find(s => s.socketType === "SCENE" && !s.destroyed && (s.playerId === attackerId || String(s.playerId) === String(attackerId)));
                                    let attackerHeroId = attackerSocket ? attackerSocket.currentHeroId : null;
                                    if (!attackerHeroId && spawnedNpcs[attackerId]) {
                                        const masterId = spawnedNpcs[attackerId].masterId;
                                        const masterSocket = activeSockets.find(s => s.socketType === "SCENE" && !s.destroyed && (s.playerId === masterId || String(s.playerId) === String(masterId)));
                                        if (masterSocket) {
                                            attackerHeroId = masterSocket.currentHeroId;
                                        }
                                    }

                                    const attackerNpc = spawnedNpcs[attackerId] || spawnedNpcs[String(attackerId)] || (global.npcHistory ? global.npcHistory[attackerId] || global.npcHistory[String(attackerId)] : null);
                                    
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
                                        console.log(`[💖 VENUS HEAL] Curou o Alvo: ${targetId} em +${healAmount} HP. Novo HP: ${target.hp}`);
                                        continue;
                                    }

                                    if (isLilyHeal) {
                                        const healAmount = 250;
                                        target.hp = Math.min(target.maxHp || 2000, (target.hp !== undefined ? target.hp : (target.maxHp || 2000)) + healAmount);
                                        gameLogic.sendEntityHpUpdate(target, targetId, target, isPlayerTarget);
                                        console.log(`[💖 LILY HEAL] Curou o Alvo: ${targetId} em +${healAmount} HP. Novo HP: ${target.hp}`);
                                        continue;
                                    }

                                    // Verifica se é Cura da Guanyin (Healing Beam - shootname 1)
                                    const isGuanyinHeal = (
                                        ((loopWeaponId === 2000 || String(loopWeaponId) === "2000" || loopWeaponId === 1019 || String(loopWeaponId) === "1019") && String(loopShootname) === "1") ||
                                        ((resolvedWeaponId === 2000 || String(resolvedWeaponId) === "2000" || resolvedWeaponId === 1019 || String(resolvedWeaponId) === "1019") && String(loopShootname) === "1")
                                    );

                                    if (isGuanyinHeal) {
                                        const healAmount = 80;
                                        target.hp = Math.min(target.maxHp || 2000, (target.hp !== undefined ? target.hp : (target.maxHp || 2000)) + healAmount);
                                        gameLogic.sendEntityHpUpdate(target, targetId, target, isPlayerTarget);
                                        console.log(`[💖 GUANYIN HEAL] Curou o Alvo: ${targetId} em +${healAmount} HP. Novo HP: ${target.hp}`);
                                        continue;
                                    }

                                    // Verifica se é Cura/Ataque Básico do Lamster (ID 2000)
                                    // Verifica se é Cura/Ataque Básico do Lamster (ID 2000)
                                    const isLamsterBasicHeal = (
                                        (loopWeaponId === 2000 || String(loopWeaponId) === "2000" || resolvedWeaponId === 2000 || String(resolvedWeaponId) === "2000") && 
                                        String(loopShootname) === "1"
                                    );

                                    if (isLamsterBasicHeal) {
                                        if (Number(attackerForce) === Number(targetForce)) {
                                            // Se atingir um aliado, cura ele!
                                            const healAmount = 150;
                                            target.hp = Math.min(target.maxHp || 2000, (target.hp !== undefined ? target.hp : (target.maxHp || 2000)) + healAmount);
                                            gameLogic.sendEntityHpUpdate(target, targetId, target, isPlayerTarget);
                                            
                                            // E aplica o Buff 1007 (Inspiração) por 3 segundos (3000ms) no aliado curado!
                                            gameLogic.applyBuffToEntity(socket, targetId, 1007, 3000);
                                            
                                            console.log(`[💖 LAMSTER HEAL & BUFF] Curou o Aliado: ${targetId} em +${healAmount} HP e aplicou Inspiração. Novo HP: ${target.hp}`);
                                            continue;
                                        } else {
                                            // Se atingir um inimigo, o fluxo continua e aplica o dano normal do XML (120 de dano)!
                                            console.log(`[💥 LAMSTER DAMAGE] Atingiu Inimigo: ${targetId}. Causando 120 de dano.`);
                                        }
                                    }

                                    // Verifica se é Cura Ativa da Angel
                                    let finalDamage = loopBaseDamage;

                                    // 1. Aplica Falloff de Distância (como Jack, Lars, Johnny, Khan)
                                    if (loopWeaponData && loopWeaponData.dampBegin > 0 && loopWeaponData.dampEnd > 0) {
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
                                        finalDamage = finalDamage * distFalloff;
                                    }

                                    // 2. Aplica Falloff de Explosão/AoE (como Lei Zhenzi, Loki, Dragos)
                                    if (loopWeaponData && loopWeaponData.boomRange > 0) {
                                        const distVal = Number(distance || 0);
                                        const radius = loopWeaponData.boomRange / 10.0;
                                        
                                        let aoeFalloff = 1.0;
                                        if (boomHead === 1 || boomHead === 3) {
                                            // Acertos diretos no corpo (3) ou cabeça (1) não sofrem decaimento de splash
                                            aoeFalloff = 1.0;
                                            console.log(`[💥 DIRECT HIT NO FALLOFF] WeaponId: ${loopWeaponId} | Target: ${targetId} | Part: ${collName} | boomHead: ${boomHead}`);
                                        } else if (distVal <= 0) {
                                            aoeFalloff = 1.0;
                                        } else if (distVal >= radius) {
                                            aoeFalloff = 0.50; // Dano mínimo de 50% até o residual
                                        } else {
                                            // Interpolação linear da distância do centro da explosão: 100% no centro, caindo até 50% na borda
                                            aoeFalloff = 1.0 - (distVal / radius) * 0.50;
                                        }
                                        finalDamage = finalDamage * aoeFalloff;
                                    }

                                    let damageAmount = finalDamage;
                                    if (boomHead === 1 && loopCanHeadshot) {
                                        damageAmount = Math.round(finalDamage * 2.0); // Multiplicador oficial de 2.0x para headshots
                                    } else {
                                        damageAmount = Math.round(finalDamage);
                                    }

                                    // Glider (1005) Ultimate dynamic lock-on damage scaling
                                    if (loopWeaponId === 1005 && loopShootname === "6") {
                                        let elapsedSec = 0;
                                        if (attackerSocket && attackerSocket.ultimateStart) {
                                            elapsedSec = (Date.now() - attackerSocket.ultimateStart) / 1000;
                                        }
                                        const chargeDamage = Math.min(4200, Math.round(elapsedSec * 700));
                                        
                                        // Target HP
                                        const targetHp = target.hp !== undefined ? target.hp : (target.maxHp || 2000);
                                        
                                        if (chargeDamage >= targetHp) {
                                            damageAmount = 4200; // Instantly kills
                                        } else {
                                            damageAmount = chargeDamage; // Scales but doesn't kill
                                        }
                                        console.log(`[🚀 GLIDER ULTIMATE HIT] targetId: ${targetId} | targetHp: ${targetHp} | chargeDamage: ${chargeDamage} | final damageAmount: ${damageAmount}`);
                                    }

                                    const isAngelActiveHeal = (
                                        (loopWeaponId === 1029 || String(loopWeaponId) === "1029") &&
                                        (String(loopShootname) === "3" || String(loopShootname) === "4")
                                    );

                                    if (isAngelActiveHeal) {
                                        if (Number(attackerForce) === Number(targetForce)) {
                                            const healAmount = String(loopShootname) === "3" ? 400 : 75;
                                            target.hp = Math.min(target.maxHp || 2000, (target.hp !== undefined ? target.hp : (target.maxHp || 2000)) + healAmount);
                                            gameLogic.sendEntityHpUpdate(target, targetId, target, isPlayerTarget);
                                            console.log(`[💖 ANGEL ACTIVE HEAL] Curou o Aliado: ${targetId} em +${healAmount} HP (Shootname: ${loopShootname}). Novo HP: ${target.hp}`);
                                            continue;
                                        } else {
                                            console.log(`[💥 ANGEL ACTIVE DAMAGE] Bola de cura atingiu Inimigo: ${targetId} | WeaponId: ${loopWeaponId} | Shootname: ${loopShootname}. Causando dano.`);
                                        }
                                    }

                                                                         // 2.5. Aplicador dinâmico de buffs/debuffs do weapon.xml ao atingir o alvo
                                     if (loopWeaponData && loopWeaponData.buff > 0) {
                                         const buffId = loopWeaponData.buff;
                                         const buffInfo = buffsMap[buffId];
                                         if (buffInfo) {
                                             const isDebuff = buffInfo.isdebuff === 1;
                                             const isSameTeam = Number(attackerForce) === Number(targetForce);
                                             if ((isDebuff && !isSameTeam) || (!isDebuff && isSameTeam)) {
                                                 const buffDuration = buffInfo.time || 5000;
                                                 gameLogic.applyBuffToEntity(socket, targetId, buffId, buffDuration);
                                             }
                                         }
                                     }

                                     // 1. Prevenção de Fogo Amigo (Teammate Hit Prevention) em Partidas de Equipe
                                     console.log(`[🔎 PRE-FF] attackerId=${attackerId} attackerSocket=${!!attackerSocket} attackerNpc=${attackerNpc?attackerNpc.index:'null'} attackerForce=${attackerForce} | targetId=${targetId} targetForce=${targetForce} | damageAmount=${damageAmount} loopBaseDamage=${loopBaseDamage} loopWeaponId=${loopWeaponId} loopShootname=${loopShootname} loopWeaponData=${JSON.stringify(loopWeaponData)}`);
                                     if (Number(attackerForce) === Number(targetForce)) {
                                         console.log(`[🛡️ FOGO AMIGO NEGADO] Alvo ${targetId} é aliado do Atacante ${attackerId} (Time ${attackerForce})! Dano bloqueado.`);
                                         continue;
                                     }

                                     // 2. Invencibilidade / God Mode
                                     if (target.godMode || target.isInvincible) {
                                         console.log(`[🛡️ HIT NEGADO] Alvo ${targetId} está invencível / godMode!`);
                                         continue;
                                     }

                                     if (isPlayerTarget) {
                                          // 1. Aplicação de Knockback se a arma/bala possuir cullBack
                                          if (loopWeaponData && loopWeaponData.cullBack && loopWeaponData.cullBack > 0) {
                                              try {
                                                  const force = loopWeaponData.cullBack;
                                                  const targetSocket = target;
                                                  
                                                  let attX = 0, attY = 0, attZ = 0;
                                                  if (attackerSocket) {
                                                      attX = attackerSocket.lastX || 0;
                                                      attY = attackerSocket.lastY || 0;
                                                      attZ = attackerSocket.lastZ || 0;
                                                  } else if (attackerNpc) {
                                                      attX = attackerNpc.x || 0;
                                                      attY = attackerNpc.y || 0;
                                                      attZ = attackerNpc.z || 0;
                                                  }
                                                  
                                                  const tarX = targetSocket.lastX || 0;
                                                  const tarY = targetSocket.lastY || 0;
                                                  const tarZ = targetSocket.lastZ || 0;
                                                  
                                                  let dx = tarX - attX;
                                                  let dy = tarY - attY;
                                                  let dz = tarZ - attZ;
                                                  
                                                  let lenXZ = Math.sqrt(dx * dx + dz * dz);
                                                  if (lenXZ > 0) {
                                                      dx /= lenXZ;
                                                      dz /= lenXZ;
                                                  } else {
                                                      dx = 0;
                                                      dz = 1;
                                                  }
                                                  dy = 0.25;
                                                  
                                                  let len3D = Math.sqrt(dx * dx + dy * dy + dz * dz);
                                                  if (len3D > 0) {
                                                      dx /= len3D;
                                                      dy /= len3D;
                                                      dz /= len3D;
                                                  }
                                                  
                                                  const scale = 10000;
                                                  const dirXEnc = utils.encodeInt(Math.round(dx * scale));
                                                  const dirYEnc = utils.encodeInt(Math.round(dy * scale));
                                                  const dirZEnc = utils.encodeInt(Math.round(dz * scale));
                                                  const forceEnc = utils.encodeInt(Math.round(force));
                                                  
                                                  const p2010000 = utils.encodeInt(2010000);
                                                  const p2010105 = utils.encodeInt(2010105);
                                                  
                                                  const knockbackPacket = "e" + targetId + ":SendMsgToClient,(" + p2010000 + ",(" + p2010105 + ",(" + dirXEnc + "," + dirYEnc + "," + dirZEnc + "," + forceEnc + ")))\n";
                                                  targetSocket.write(knockbackPacket);
                                                  console.log("[💥 KNOCKBACK] Aplicado a " + targetSocket.playerName + " com força " + force + ". Direção: (" + dx.toFixed(2) + ", " + dy.toFixed(2) + ", " + dz.toFixed(2) + ")");
                                              } catch (errKnock) {
                                                  console.error("[⚠️ ERRO ao aplicar knockback]:", errKnock);
                                              }
                                          }

                                          let place = 0;
                                          let originalDamage = damageAmount;
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
                                              let barrierHp = 0;
                                              let barrierMaxHp = 0;
                                              let barrierName = "";
                                              
                                              // Determina qual barreira foi atingida
                                              if (heroId === 1006) {
                                                  // Tagaloa - Barreira direcional (Reinhardt-like) - HP infinito
                                                  barrierHp = 10000;
                                                  barrierMaxHp = 10000;
                                                  barrierName = "TAGALOA";
                                              } else if (heroId === 1032) {
                                                  // Venus - Barreira implantada (Orisa-like)
                                                  barrierHp = (target.venusBarrierHp !== undefined) ? target.venusBarrierHp : 0;
                                                  barrierMaxHp = target.venusBarrierMaxHp || 4000;
                                                  barrierName = "VENUS";
                                              } else if (heroId === 1033) {
                                                   // Lily - Barreira projetada (Symmetra-like) - HP infinito
                                                   barrierHp = 1000;
                                                   barrierMaxHp = 1000;
                                                   barrierName = "LILY";
                                               }
                                              
                                              if (barrierHp > 0) {
                                                  // Aplica dano NA BARREIRA (não no jogador!)
                                                  const barrierDmg = Math.min(barrierHp, damageAmount);
                                                  if (heroId !== 1033) {
                                                      barrierHp -= barrierDmg;
                                                  }
                                                  
                                                  // Atualiza HP da barreira no socket
                                                  if (heroId === 1006) {
                                                       target.tagaloaShieldHp = barrierHp;
                                                   } else if (heroId === 1032) {
                                                       target.venusBarrierHp = barrierHp;
                                                   } else if (heroId === 1033) {
                                                       target.lilyBarrierHp = barrierHp;
                                                   }
                                                  
                                                  console.log(`[🧱 BARREIRA ${barrierName}] Hit na barreira de ${targetId} (Parte: ${collNameStr}). Dano: ${barrierDmg}. HP da barreira: ${barrierHp}/${barrierMaxHp}`);
                                                  
                                                  // Broadcast atualização visual da barreira (2010002/2210001)
                                                   if (heroId === 1006) {
                                                       const p2010002 = utils.encodeInt(2010002);
                                                       const brPlayerIdEnc = utils.encodeInt(targetId);
                                                       const brStateEnc = utils.encodeInt(barrierHp > 0 ? 1 : 0);
                                                       const brEnergyEnc = utils.encodeInt(barrierHp);
                                                       const brMaxEnergyEnc = utils.encodeInt(barrierMaxHp);
                                                       const brResp = "e" + targetId + ":SendMsgToAllClient,(" + p2010002 + ",(" + brPlayerIdEnc + ",(" + brStateEnc + "," + brEnergyEnc + "," + brMaxEnergyEnc + ")))\n";
                                                       activeSockets.forEach(s => {
                                                           if (s.socketType === "SCENE" && !s.destroyed) {
                                                               s.write(brResp);
                                                           }
                                                       });
                                                   } else if (heroId === 1032) {
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
                                                           if (s.socketType === "SCENE" && !s.destroyed) {
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
                                                           if (s.socketType === "SCENE" && !s.destroyed) {
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

                                          // 3. Aplica dano restante no HP
                                          if (damageAmount > 0) {
                                              target.hp = Math.max(0, (target.hp !== undefined ? target.hp : (target.maxHp || 2000)) - damageAmount);
                                          }

                                          // 4. Envia HaveDamage para feedbacks ricos
                                          gameLogic.sendEntityDamageUpdate(targetId, target, originalDamage, attackerId, place, weaponId);

                                          // 4b. Acumula dano no atacante para estatísticas
                                          if (attackerSocket && originalDamage > 0) {
                                              attackerSocket.matchDamage = (attackerSocket.matchDamage || 0) + Math.round(originalDamage);
                                          }
                                          
                                          if (target.hp <= 0) {
                                              gameLogic.handlePlayerDeath(target, attackerSocket);
                                          }
                                      } else {
                                          let place = 0;
                                          if (boomHead === 1 && canHeadshot) {
                                              place = 1;
                                          } 
                                          gameLogic.handleNpcHurt(target, targetId, damageAmount, attackerId, place, weaponId);
                                      }
                                }
                            }
                        } catch (e) {
                            console.error("[⚠️ ERRO ao processar SDG packet]:", e);
                        }
                    }
                } else if (!line.includes("SN,")) {
                    // Loga qualquer outro pacote recebido que não seja PING/PONG (Ex: pacotes de movimento 'ST')
                    console.log(`[📡 RECEBIDO DO CLIENTE] ` + line);
                }
            }
            updateSessionFromSocket(socket);
        }
    });

    socket.on('close', () => {
        const socketIdx = activeSockets.indexOf(socket);
        if (socketIdx !== -1) {
            activeSockets.splice(socketIdx, 1);
        }
        if (socketType === 'SCENE') {
            socket.inMatch = false;
            
            // Broadcast to other players in the scene to destroy this player's entity using dynamic relative index
            activeSockets.forEach(s => {
                if (s.socketType === "SCENE" && s !== socket && !s.destroyed) {
                    const receiverIndex = s.playerIndex || 1;
                    const disconnectedIndex = socket.playerIndex || 1;
                    let relativePlayerIndex = disconnectedIndex;
                    if (disconnectedIndex === receiverIndex) {
                        relativePlayerIndex = 1;
                    } else if (disconnectedIndex === 1) {
                        relativePlayerIndex = receiverIndex;
                    }
                    s.write(`d${utils.encodeInt(relativePlayerIndex)}\n`);
                    console.log(`[🌐 SCENE] Sent destroy signal d${relativePlayerIndex} (Player Disconnected: ${socket.playerName}) to ${s.playerName}`);
                }
            });
            
            const lobbyMatch = activeSockets.find(s => s.socketType === "LOBBY" && s.playerName === socket.playerName && !s.destroyed) ||
                               activeSockets.find(s => s.socketType === "LOBBY" && s.remoteAddress === socket.remoteAddress && !s.destroyed);
            if (lobbyMatch) {
                lobbyMatch.inMatch = false;
            }

            // Remove da partida TDM ativa
            if (global.activeTdmMatch) {
                const match = global.activeTdmMatch;
                const pIdx = match.players.indexOf(socket);
                if (pIdx !== -1) {
                    match.players.splice(pIdx, 1);
                    console.log(`[🏆 TDM] Jogador ${socket.playerName || 'Jogador'} desconectou e foi removido do TDM.`);
                }
            }
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
        console.log(`[⚠️ ERRO]:`, err.message);
    });
});

tcpServer.listen(config.tcpPort, () => {
    console.log(`[🎮 SERVIDOR DO JOGO] Ouvindo TCP na porta ${config.tcpPort}...`);
});

// Intervalo global de 1 segundo para cura passiva da Angel (Círculo de Cura) e Regeneração de Escudo do Tagaloa
setInterval(() => {
    activeSockets.forEach(socket => {
        if (socket.socketType !== "SCENE" || socket.destroyed) return;

        // 1. Regeneração de Escudo do Tagaloa (1006)
        if (socket.currentHeroId === 1006 || String(socket.currentHeroId) === "1006") {
            const now = Date.now();
            const lastDamage = socket.lastDamageTime || 0;
            if (now - lastDamage >= 2000) {
                const healAmount = 1125;
                // A barreira do Tagaloa é uma entidade separada - regenera tagaloaShieldHp
                const currentBarrierHp = (socket.tagaloaShieldHp !== undefined) ? socket.tagaloaShieldHp : 10000;
                if (currentBarrierHp < 10000) {
                    socket.tagaloaShieldHp = Math.min(10000, currentBarrierHp + healAmount);
                    
                    // Broadcast visual da regeneração (2010002)
                    const p2010002 = utils.encodeInt(2010002);
                    const plIdEnc = utils.encodeInt(socket.playerId);
                    const stEnc = utils.encodeInt(1);
                    const enEnc = utils.encodeInt(socket.tagaloaShieldHp);
                    const mxEnc = utils.encodeInt(10000);
                    const regenResp = `e${socket.playerId}:SendMsgToAllClient,(${p2010002},(${plIdEnc},(${stEnc},${enEnc},${mxEnc})))\n`;
                    activeSockets.forEach(s => {
                        if (s.socketType === "SCENE" && !s.destroyed) {
                            s.write(regenResp);
                        }
                    });
                    console.log(`[🛡️ TAGALOA BARREIRA REGEN] HP da barreira: ${socket.tagaloaShieldHp}/10000`);
                }
            }
        }

        // 2. Cura passiva da Angel (1029)
        if (socket.currentHeroId === 1029 || String(socket.currentHeroId) === "1029") {
            if (socket.angelAuraTargets && socket.angelAuraTargets.length > 0) {
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
                        const healAmount = 40;
                        target.hp = Math.min(target.maxHp || 2000, target.hp + healAmount);
                        gameLogic.sendEntityHpUpdate(target, targetId, target, isPlayerTarget);
                        console.log(`[💖 ANGEL CIRCLE HEAL] Angel ${socket.playerId} curou Alvo: ${targetId} em +${healAmount} HP. Novo HP: ${target.hp}`);
                    }
                });
            }
        }
    });
}, 1000);
