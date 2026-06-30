const express = require('express');
const fs = require('fs');

// Altere para o seu IP do Radmin VPN caso queira jogar em rede com amigos!
const SERVER_IP = process.env.SERVER_IP || "26.1.99.14"


const logFile = 'e:\\server tr\\server_log.txt';
fs.writeFileSync(logFile, `=== SERVER RESTART AT ${new Date().toISOString()} ===\n`, 'utf8');

const originalLog = console.log;
const originalError = console.error;

console.log = function(...args) {
    originalLog.apply(console, args);
    const msg = args.map(x => typeof x === 'object' ? JSON.stringify(x) : String(x)).join(' ');
    fs.appendFileSync(logFile, msg + '\n', 'utf8');
};

console.error = function(...args) {
    originalError.apply(console, args);
    const msg = args.map(x => {
        if (x instanceof Error) return x.stack || x.message;
        return typeof x === 'object' ? JSON.stringify(x) : String(x);
    }).join(' ');
    fs.appendFileSync(logFile, `[⚠️ ERRO] ${msg}\n`, 'utf8');
};

const filesVersionMap = {};
try {
    const xmlContent = fs.readFileSync('e:\\\\server tr\\\\files_dir.xml', 'utf8');
    const matches = xmlContent.matchAll(/<f path="([^"]+)"[^>]*version="([^"]+)"/g);
    for (const match of matches) {
        const exactPath = match[1];
        const version = match[2];
        filesVersionMap[exactPath.toLowerCase()] = {
            version: version,
            exactPath: exactPath
        };
    }
    console.log(`[📦 CDN MAP] Mapeadas ${Object.keys(filesVersionMap).length} versões e caminhos exatos de arquivos com sucesso!`);
} catch (e) {
    console.error(`[⚠️ ERRO] Falha ao carregar files_dir.xml:`, e);
}

// 📖 CARREGADOR DINÂMICO DE DADOS DE NPCS (Npc.xml)
const npcData = {};
try {
    const npcContent = fs.readFileSync('e:\\\\server tr\\\\downloads\\\\baxi\\\\Npc.xml', 'utf8');
    const npcMatches = npcContent.matchAll(/<i\s+([^>]+)>/g);
    for (const match of npcMatches) {
        const attribs = match[1];
        const indexMatch = attribs.match(/Index="(\d+)"/);
        const hpMatch = attribs.match(/HP="(\d+)"/);
        const shieldMatch = attribs.match(/Shield="(\d+)"/);
        const nameMatch = attribs.match(/Name="([^"]+)"/);
        const timeMatch = attribs.match(/Time="(\d+)"/);
        const typeMatch = attribs.match(/Type="(\d+)"/);
        const weaponMatch = attribs.match(/Weapon_Index="(\d+)"/);
        if (indexMatch) {
            const index = parseInt(indexMatch[1]);
            const hp = hpMatch ? parseInt(hpMatch[1]) : 0;
            const shield = shieldMatch ? parseInt(shieldMatch[1]) : 0;
            const name = nameMatch ? nameMatch[1] : `NPC_${index}`;
            const time = timeMatch ? parseInt(timeMatch[1]) : 0;
            const type = typeMatch ? parseInt(typeMatch[1]) : 1;
            const weaponIndex = weaponMatch ? parseInt(weaponMatch[1]) : 0;
            npcData[index] = { hp, shield, name, time, type, weaponIndex };
        }
    }
    console.log(`[🤖 NPC DATABASE] Carregados ${Object.keys(npcData).length} NPCs do Npc.xml com sucesso!`);
} catch (e) {
    console.error(`[⚠️ ERRO] Falha ao carregar Npc.xml:`, e);
}

// 🎯 HELPER PARA CONSEGUIR O NOME DA CLASSE EXATA DA ENTIDADE (Npc_Entity Subclasses)
function getEntityClassName(npcIndex) {
    // Casos especiais baseados no npcIndex ou no Type do Npc.xml
    if (npcIndex === 1019) return 'Radiobomb_Entity'; // Loki Remote Bomb manual trigger
    if (npcIndex === 1007 || npcIndex === 1025 || npcIndex === 1005 || npcIndex === 1011 || npcIndex === 1012) return 'Area_Entity'; // Heal Zone, Gaya's Stare, Thunder Bomb, Snow Fairy, Live Fire City
    if (npcIndex === 1021 || npcIndex === 1023) return 'Trap_Entity'; // Johnny trap, Lily heal ball
    if (npcIndex >= 1026 && npcIndex <= 1035) return 'Sentry_Entity'; // Eva/Lei Zhenzi turrets
    
    // Robôs de Treino (Dummies) e bots humanoides devem ser instanciados como Npc_Entity para física, gravidade e hitboxes
    if (npcIndex >= 1101 && npcIndex <= 1125) {
        return 'Npc_Entity';
    }

    const npcConf = npcData[npcIndex] || { type: 1 };
    const type = parseInt(npcConf.type) || 1;
    
    switch (type) {
        case 1:
            return 'Sentry_Entity';
        case 2:
        case 3:
        case 13:
            return 'Landmine_Entity';
        case 4:
            return 'Chariot_Entity';
        case 5:
            return 'Elevator_Entity';
        case 6:
            return 'Timebomb_Entity';
        case 8:
            return 'Steerable_Entity';
        case 9:
        case 14:
            return 'Trap_Entity';
        case 11:
            return 'Main_Transmission_Entity';
        case 12:
            return 'Sub_Transmission_Entity';
        default:
            return 'Npc_Entity';
    }
}

const app = express();
const httpPort = 80;

// ==========================================
// 🚨 GESTOR DE CRASHES & BUGS DO JOGO
// ==========================================
global.crashLogs = [];
const originalConsoleError = console.error;
console.error = function (...args) {
    originalConsoleError.apply(console, args);
    const msg = args.map(arg => {
        if (arg instanceof Error) {
            return `${arg.message}\n${arg.stack}`;
        }
        return typeof arg === 'object' ? JSON.stringify(arg) : String(arg);
    }).join(' ');
    
    // Filtramos apenas logs relevantes de erros/crashes
    global.crashLogs.push({
        timestamp: new Date().toLocaleTimeString(),
        message: msg,
        type: msg.includes('ReferenceError') || msg.includes('TypeError') || msg.includes('Exception') || msg.includes('Error') ? 'Critical Crash' : 'Bug/Warning'
    });
    if (global.crashLogs.length > 50) global.crashLogs.shift();
};

process.on('uncaughtException', (err) => {
    console.error('UNCAUGHT EXCEPTION:', err);
});
process.on('unhandledRejection', (reason, promise) => {
    console.error('UNHANDLED REJECTION:', reason);
});

let lastLobbySocket = null;
const spawnedNpcs = {};
const activeSockets = [];
const ipNames = {};
const ipIndexes = {};

// SESSÕES PERSISTENTES POR NOME DE JOGADOR (Para mapeamento multiplayer LAN/Multi-client confiável)
const playerSessions = {};
let globalPlayerCounter = 0;

function updateSessionFromSocket(socket) {
    if (!socket || !socket.playerName) return;
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

// Helper para spawnar NPCs dummies de treino
function spawnPracticeNpcs(socket, force = false) {
    if (!socket || socket.destroyed) return;
    if (socket.practiceNpcsSpawned && !force) {
        console.log(`[🤖 SCENE] NPCs de treino já foram spawnados para este socket. Ignorando re-spawn.`);
        return;
    }
    socket.practiceNpcsSpawned = true;
    console.log(`[🤖 SCENE] Enviando/Atualizando NPCs de treino no mapa bachang_baxi...`);
    spawnedNpcs[2001] = { id: 2001, index: 1121, hp: 10, maxHp: 10, x: 50000, y: 20104, z: 26000, isPracticeDummy: true };
    spawnedNpcs[2002] = { id: 2002, index: 1122, hp: 1000, maxHp: 1000, x: 50500, y: 20104, z: 26000, isPracticeDummy: true };
    spawnedNpcs[2003] = { id: 2003, index: 1101, hp: 1500, maxHp: 1500, x: 49500, y: 20104, z: 26000, isPracticeDummy: true };
    
    for (const key in spawnedNpcs) {
        const npc = spawnedNpcs[key];
        const className = getEntityClassName(npc.index);
        const spawnPacket = `c${className}:${encodeInt(npc.id)},("Initialize",(${encodeInt(1)},${encodeInt(npc.id)},${encodeInt(npc.index)},${encodeInt(2)},(${encodeInt(npc.x)},${encodeInt(npc.y)},${encodeInt(npc.z)}),(0,0,0),(${encodeInt(100)},${encodeInt(100)},${encodeInt(100)}),(0,0,0),${encodeInt(-1)},${encodeInt(npc.maxHp)},${encodeInt(0)},${encodeInt(1)},${encodeInt(0)},""),((),()),0)\n`;
        socket.write(spawnPacket);
        console.log(`[🤖 NPC SPAWNED]: ${spawnPacket.trim()}`);
    }
}

// Configuração para ler dados de formulários e JSON (Útil para APIs futuras)
app.use(express.json());
app.use(express.urlencoded({ extended: true }));

// ==========================================
// 1. O ESPIONADOR DE REQUISIÇÕES (HTTP)
// Mostra tudo que o jogo tenta baixar da Web
// ==========================================
app.use((req, res, next) => {
    console.log(`[🌐 JOGO PEDIU HTTP]: ${req.method} ${req.url}`);
    next();
});

// ==========================================
// 2. MOCK DA API DA QQ (A Catraca do Admin)
// ==========================================
app.get('/qqapi/GameGetMsgFromadmin.aspx', (req, res) => {
    // O jogo espera o formato "IDs|Processos|MD5s"
    res.send("||");
});

// ==========================================
// 3. AS ROTAS ANTIGAS DO LAUNCHER (Opcional, mas bom manter)
// ==========================================
app.all('/portal/triggerun/api/', (req, res) => {
    const action = req.query.action || req.body.action;

    if (action === 'servers') {
        return res.json({
            result: true, error: "",
            servers: [{ id: 1, name: "Servidor Local", status: "ONLINE" }],
            banner1uri: "", banner2uri: "", status: "CDN e API Ativos!", build: "STEAM",
            open_msg: "", open_url: "", auto_login: true, upgrade_req: false
        });
    }

    if (action === 'launcher') {
        return res.json({ result: true, error: "", command: "teste" });
    }

    res.json({ result: false, error: "Ação desconhecida" });
});

// ==========================================
// 4. O CDN PROXY (Evitar travamento nos 403)
// ==========================================
const http = require('http');

// Proxy para o CDN (Assets do jogo)
app.use(['/res', '/scene'], (req, res) => {
    const basePath = req.baseUrl; // /res or /scene

    // Se o jogo pedir arquivos XML, serve localmente da pasta public/baxi/
    const pathLower = req.path.toLowerCase();
    if (pathLower.endsWith('.xml')) {
        const path = require('path');
        const parts = req.path.split('/');
        const filename = parts[parts.length - 1]; // ex: Npc.xml, config.xml

        const localPath = path.join(__dirname, 'public', 'baxi', filename);
        if (fs.existsSync(localPath)) {
            console.log(`[🎯 XML LOCAL SERVIDO]: ${req.path} -> ${localPath}`);
            res.setHeader('Content-Type', 'application/xml; charset=utf-8');
            return res.sendFile(localPath);
        }
    }

    let urlParts = req.url.split('?');
    let finalPath = basePath + urlParts[0];
    
    // Tenta mapear o caminho exato e a versão correta do arquivo
    const saParts = req.path.split('StreamingAssets/');
    if (saParts.length > 1) {
        const relativeFilePath = saParts[1].toLowerCase();
        const mapEntry = filesVersionMap[relativeFilePath];
        if (mapEntry) {
            // Reconstrói usando a versão exata do asset e o caminho com a capitalização exata registrada no files_dir.xml!
            finalPath = `${basePath}/${mapEntry.version}/StreamingAssets/${mapEntry.exactPath}`;
            console.log(`[📦 MAPPED ASSET] Traduzido ${req.path} -> Versão CDN: ${mapEntry.version} | Casing Exato: ${mapEntry.exactPath}`);
        }
    }

    const queryPart = urlParts[1] ? `?${urlParts[1]}` : '';
    let cdnUrl = `http://trig.2cdn.xyz/TRIG/res1${finalPath}${queryPart}`;

    const options = {
        headers: {
            'User-Agent': 'UnityPlayer/5.5.4f1 (UnityWebRequest/1.0, libcurl/7.51.0-DEV)',
            'Accept': '*/*',
            'Referer': 'http://trig.2cdn.xyz/TRIG/res1/',
            'X-Unity-Version': '5.5.4f1',
            'Connection': 'keep-alive'
        }
    };

    const fetchCdn = (targetUrl, attempt = 1) => {
        console.log(`[📦 ASSET] Baixando do CDN (Tentativa ${attempt}): ${targetUrl}`);
        http.get(targetUrl, options, (cdnRes) => {
            if (cdnRes.statusCode === 200) {
                res.set(cdnRes.headers);
                cdnRes.pipe(res);
            } else if ((cdnRes.statusCode === 403 || cdnRes.statusCode === 404) && attempt === 1) {
                console.warn(`[⚠️ CDN RETRY] Status ${cdnRes.statusCode} para ${targetUrl}. Tentando fallback 1.1.1...`);
                // Substitui a versão mantendo a capitalização exata de StreamingAssets/streamingassets
                const fallbackUrl = targetUrl.replace(/\/res\/[^\/]+\/(streamingassets)\//i, '/res/1.1.1/$1/');
                if (fallbackUrl !== targetUrl) {
                    fetchCdn(fallbackUrl, 2);
                } else {
                    if (req.path.toLowerCase().endsWith('.txt')) {
                        console.log(`[🩹 CDN FALLBACK] Retornando 200 OK vazio para arquivo texto não encontrado: ${req.path}`);
                        res.setHeader('Content-Type', 'text/plain; charset=utf-8');
                        return res.send('');
                    }
                    res.status(cdnRes.statusCode).end();
                }
            } else {
                console.error(`[❌ CDN ERRO]: ${cdnRes.statusCode} para ${targetUrl}`);
                if (req.path.toLowerCase().endsWith('.txt')) {
                    console.log(`[🩹 CDN FALLBACK] Retornando 200 OK vazio para arquivo texto não encontrado: ${req.path}`);
                    res.setHeader('Content-Type', 'text/plain; charset=utf-8');
                    return res.send('');
                }
                res.status(cdnRes.statusCode).end();
            }
        }).on('error', (err) => {
            console.error(`[❌ CDN FALHA]: ${err.message}`);
            res.status(500).end();
        });
    };

    fetchCdn(cdnUrl);
});


// ==========================================
// 5. PASTAS ESTÁTICAS LOCAIS
// ==========================================
app.use(express.static('public'));

// ==========================================
// 🎮 ADMIN CONTROL PANEL API
// ==========================================

// Helper: get all active scene players as JSON
function getPlayersSnapshot() {
    const playersMap = {};
    
    // Process first LOBBY sockets
    activeSockets
        .filter(s => s.socketType === 'LOBBY' && !s.destroyed)
        .forEach(s => {
            playersMap[s.playerId] = {
                playerId: s.playerId,
                playerName: s.playerName || 'Unknown',
                heroId: s.currentHeroId || 1015,
                skinId: s.currentSkinId || 1,
                weaponId: s.currentWeaponId || 1,
                hp: 1000,
                maxHp: 1000,
                shield: 0,
                force: s.force || 1,
                x: 0,
                y: 0,
                z: 0,
                godMode: !!s.godMode,
                nocd: !!s.nocd,
                superSpeed: !!s.superSpeed,
                isInvis: !!s.isInvis,
                mapId: s.targetMapId || null,
                mapName: s.targetMapName || 'Lobby',
            };
        });

    // Overwrite with SCENE sockets (which have higher priority and actual match positions/hp)
    activeSockets
        .filter(s => s.socketType === 'SCENE' && !s.destroyed)
        .forEach(s => {
            playersMap[s.playerId] = {
                playerId: s.playerId,
                playerName: s.playerName || 'Unknown',
                heroId: s.currentHeroId || 1015,
                skinId: s.currentSkinId || 1,
                weaponId: s.currentWeaponId || 1,
                hp: s.hp || 0,
                maxHp: s.maxHp || 2000,
                shield: s.shield || 0,
                force: s.force || 1,
                x: Math.round((s.lastX || 0) / 100),
                y: Math.round((s.lastY || 0) / 100),
                z: Math.round((s.lastZ || 0) / 100),
                godMode: !!s.godMode,
                nocd: !!s.nocd,
                superSpeed: !!s.superSpeed,
                isInvis: !!s.isInvis,
                mapId: s.targetMapId || null,
                mapName: s.targetMapName || null,
            };
        });

    return Object.values(playersMap);
}

// GET /api/state  — live snapshot of entire server state
app.get('/api/state', (req, res) => {
    const players = getPlayersSnapshot();
    const npcs = Object.values(spawnedNpcs).map(n => ({
        id: n.id,
        index: n.index,
        hp: n.hp,
        maxHp: n.maxHp,
        x: Math.round((n.x || 0) / 100),
        y: Math.round((n.y || 0) / 100),
        z: Math.round((n.z || 0) / 100),
    }));
    const tdm = global.activeTdmMatch ? {
        active: true,
        phase: global.activeTdmMatch.phase || 'warmup',
        blueScore: global.activeTdmMatch.blueScore || 0,
        redScore: global.activeTdmMatch.redScore || 0,
        timeLeft: global.activeTdmMatch.timeLeft || 0,
    } : { active: false };
    res.json({ players, npcs, tdm });
});

// POST /api/hero  — switch hero for a player
app.post('/api/hero', (req, res) => {
    const { playerId, heroId } = req.body;
    const socket = activeSockets.find(s => s.socketType === 'SCENE' && !s.destroyed && String(s.playerId) === String(playerId));
    if (!socket) return res.json({ ok: false, error: 'Jogador não encontrado ou offline.' });
    try {
        switchPlayerHero(socket, parseInt(heroId));
        res.json({ ok: true, message: `Herói trocado para ID ${heroId} para ${socket.playerName}.` });
    } catch(e) {
        res.json({ ok: false, error: e.message });
    }
});

// POST /api/spawn  — spawn NPC near a player
app.post('/api/spawn', (req, res) => {
    const { playerId, npcIndex, count } = req.body;
    const socket = activeSockets.find(s => s.socketType === 'SCENE' && !s.destroyed && String(s.playerId) === String(playerId));
    if (!socket) return res.json({ ok: false, error: 'Jogador não encontrado ou offline.' });
    try {
        const total = Math.max(1, Math.min(parseInt(count) || 1, 20));
        const idx = parseInt(npcIndex) || 1121;
        const npcConf = npcData[idx] || {};
        const npcMaxHp = parseInt(npcConf.hp) || 500;
        const spawnX = socket.lastX || 50000;
        const spawnY = socket.lastY || 20104;
        const spawnZ = socket.lastZ || 26000;
        const spawned = [];
        for (let i = 0; i < total; i++) {
            const botNetId = 15000 + Math.floor(Math.random() * 50000);
            spawnedNpcs[botNetId] = { id: botNetId, index: idx, hp: npcMaxHp, maxHp: npcMaxHp, x: spawnX, y: spawnY, z: spawnZ, isPracticeDummy: true };
            const className = getEntityClassName(idx);
            const spawnPacket = `c${className}:${encodeInt(botNetId)},("Initialize",(${encodeInt(1)},${encodeInt(botNetId)},${encodeInt(idx)},${encodeInt(2)},(${encodeInt(spawnX)},${encodeInt(spawnY)},${encodeInt(spawnZ)}),(0,0,0),(${encodeInt(100)},${encodeInt(100)},${encodeInt(100)}),(0,0,0),${encodeInt(-1)},${encodeInt(npcMaxHp)},${encodeInt(0)},${encodeInt(1)},${encodeInt(0)},""),((),()),0)\n`;
            // Broadcast to all scene sockets
            activeSockets.forEach(s => { if (s.socketType === 'SCENE' && !s.destroyed) s.write(spawnPacket); });
            spawned.push(botNetId);
        }
        res.json({ ok: true, message: `${total} entidade(s) NPC ${idx} spawnada(s).`, ids: spawned });
    } catch(e) {
        res.json({ ok: false, error: e.message });
    }
});

// POST /api/kill_npcs  — destroy all spawned NPCs
app.post('/api/kill_npcs', (req, res) => {
    let count = 0;
    for (const npcId in spawnedNpcs) {
        if (spawnedNpcs[npcId] && !spawnedNpcs[npcId].isPracticeDummy) {
            activeSockets.forEach(s => { if (s.socketType === 'SCENE' && !s.destroyed) s.write(`d${encodeInt(npcId)}\n`); });
            delete spawnedNpcs[npcId];
            count++;
        }
    }
    // Also clear practice dummies if requested
    const { includeDummies } = req.body || {};
    if (includeDummies) {
        for (const npcId in spawnedNpcs) {
            activeSockets.forEach(s => { if (s.socketType === 'SCENE' && !s.destroyed) s.write(`d${encodeInt(npcId)}\n`); });
            delete spawnedNpcs[npcId];
            count++;
        }
    }
    res.json({ ok: true, message: `${count} NPC(s) destruído(s).` });
});

// POST /api/buff  — apply/remove buff on player
app.post('/api/buff', (req, res) => {
    const { playerId, buffId, duration } = req.body;
    const socket = activeSockets.find(s => s.socketType === 'SCENE' && !s.destroyed && String(s.playerId) === String(playerId));
    if (!socket) return res.json({ ok: false, error: 'Jogador não encontrado.' });
    const bid = parseInt(buffId);
    const dur = parseInt(duration) || 10000;
    if (!buffsMap[bid]) return res.json({ ok: false, error: `Buff ID ${bid} não existe no buff.xml.` });
    try {
        applyBuffToEntity(socket, socket.playerId, bid, dur);
        res.json({ ok: true, message: `Buff "${buffsMap[bid].name}" (${bid}) aplicado por ${dur}ms em ${socket.playerName}.` });
    } catch(e) {
        res.json({ ok: false, error: e.message });
    }
});

// POST /api/godmode  — toggle god mode for player
app.post('/api/godmode', (req, res) => {
    const { playerId, enabled } = req.body;
    const socket = activeSockets.find(s => s.socketType === 'SCENE' && !s.destroyed && String(s.playerId) === String(playerId));
    if (!socket) return res.json({ ok: false, error: 'Jogador não encontrado.' });
    socket.godMode = enabled !== false && enabled !== 'false';
    if (socket.godMode) {
        socket.hp = socket.maxHp || 2000;
        socket.shield = 50000;
        socket.maxShield = 50000;
        socket.isInvincible = true;
    } else {
        socket.shield = 0;
        socket.maxShield = 0;
        socket.isInvincible = false;
    }
    res.json({ ok: true, godMode: socket.godMode, message: `God Mode ${socket.godMode ? 'ATIVADO' : 'DESATIVADO'} para ${socket.playerName}.` });
});

// POST /api/heal  — full heal player
app.post('/api/heal', (req, res) => {
    const { playerId } = req.body;
    const socket = activeSockets.find(s => s.socketType === 'SCENE' && !s.destroyed && String(s.playerId) === String(playerId));
    if (!socket) return res.json({ ok: false, error: 'Jogador não encontrado.' });
    socket.hp = socket.maxHp || 2000;
    socket.shield = socket.godMode ? 50000 : 0;
    socket.maxShield = socket.shield;
    try { sendEntityHpUpdate(socket, socket.playerId, socket, true); } catch(e) {}
    res.json({ ok: true, message: `${socket.playerName} foi curado ao máximo (HP: ${socket.hp}).` });
});

// POST /api/kill_player  — kill a player instantly
app.post('/api/kill_player', (req, res) => {
    const { playerId } = req.body;
    const socket = activeSockets.find(s => s.socketType === 'SCENE' && !s.destroyed && String(s.playerId) === String(playerId));
    if (!socket) return res.json({ ok: false, error: 'Jogador não encontrado.' });
    try {
        socket.hp = 0;
        handlePlayerDeath(socket);
        res.json({ ok: true, message: `${socket.playerName} foi eliminado.` });
    } catch(e) {
        res.json({ ok: false, error: e.message });
    }
});

// POST /api/chat  — send system chat message
app.post('/api/chat', (req, res) => {
    const { message } = req.body;
    if (!message) return res.json({ ok: false, error: 'Mensagem vazia.' });
    try {
        if (typeof sendGlobalSystemChat === 'function') {
            sendGlobalSystemChat(message);
        } else {
            activeSockets.forEach(s => {
                if (s.socketType === 'SCENE' && !s.destroyed) {
                    const msgHex = Buffer.from(message).toString('hex');
                    s.write(`e${s.playerIndex || 1}:SendMsgToClient,(${encodeInt(1006)},(${encodeInt(8)},'','${msgHex}','',(),(),${s.playerId},('','',0),()))\n`);
                }
            });
        }
        res.json({ ok: true, message: `Mensagem enviada: ${message}` });
    } catch(e) {
        res.json({ ok: false, error: e.message });
    }
});

// POST /api/map — Força a mudança de mapa em tempo real para um ou todos os jogadores
app.post('/api/map', (req, res) => {
    const { playerId, mapId } = req.body;
    const mid = parseInt(mapId);
    
    // Procura o mapa no MAP_CONFIG
    let mapConf = MAP_CONFIG[mid];
    
    // Fallback: se o ID não bater, tenta achar pelo nome do mapa no array (caso o HTML mande algo diferente)
    if (!mapConf) {
        for (const key in MAP_CONFIG) {
            if (String(MAP_CONFIG[key].id) === String(mapId)) {
                mapConf = MAP_CONFIG[key];
                break;
            }
        }
    }

    if (!mapConf) {
        return res.json({ ok: false, error: 'ID do mapa não encontrado na configuração do servidor.' });
    }

    let targets = [];
    if (playerId) {
        const socket = activeSockets.find(s => s.socketType === 'SCENE' && !s.destroyed && String(s.playerId) === String(playerId));
        if (socket) targets.push(socket);
    } else {
        targets = activeSockets.filter(s => s.socketType === 'SCENE' && !s.destroyed);
    }

    if (targets.length === 0) {
        return res.json({ ok: false, error: 'Nenhum jogador correspondente está ativo numa partida (Cena) no momento.' });
    }

    targets.forEach(socket => {
        // Correção CRÍTICA: Atualiza as variáveis de mapa do socket e as globais
        socket.currentMapId = mid; // Tem que setar o currentMapId e não targetMapId
        socket.mapName = mapConf.name;

        // Se a alteração for global, força o servidor a assumir este mapa como o principal
        if (!playerId) {
            global.lastMapId = mid;
            global.lastMapName = mapConf.name;
        }

        // Usa o sistema unificado de coordenadas para não nascer fora do mapa
        const defaultCoords = getSpawnCoordinates(mid, socket.force);
        const spawnX = defaultCoords.sx;
        const spawnY = defaultCoords.sy;
        const spawnZ = defaultCoords.sz;

        // Atualiza a posição interna do jogador
        socket.lastX = spawnX;
        socket.lastY = spawnY;
        socket.lastZ = spawnZ;

        // O comando de transição de mapa do Trigger Run é o 1304 (SLoadMapMsg) ou via CLoadScene nativo.
        // Vamos enviar exatamente a string que o jogo precisa ler no log:
        const hexMid = mid.toString(16).toUpperCase(); // Transforma 5202 em 1452 (que é o que o Unity pede)
        socket.write(`e:CLoadScene,((0,'${mapConf.name}',0,${encodeInt(getSafeMapId(mid))}))\n`);

        // Aviso no chat
        try {
            const msgHex = Buffer.from(`[Sistema] O mapa foi alterado para: ${mapConf.name}`).toString('hex');
            const sysNetMsg = `(${encodeInt(8)},'','${msgHex}','',(),(),${encodeInt(1)},('','',0),())`;
            socket.write(`e${socket.playerIndex || 1}:SendMsgToClient,(${encodeInt(1006)},${sysNetMsg})\n`);
        } catch (e) {
            console.error('[⚠️ ERRO MAP CHAT SYNC]:', e.message);
        }
    });

    const targetLabel = playerId ? `o jogador ID ${playerId}` : 'todos os jogadores';
    res.json({ ok: true, message: `Mapa alterado para "${mapConf.name}" para ${targetLabel}.` });
});

// GET /api/buffs  — list all available buffs
app.get('/api/buffs', (req, res) => {
    const list = Object.entries(buffsMap).map(([id, b]) => ({ id: parseInt(id), name: b.name || `Buff ${id}`, time: b.time }));
    res.json(list);
});

// GET /api/heroes  — list all heroes from heroinfo.xml
app.get('/api/heroes', (req, res) => {
    const heroes = Object.entries(heroSkillsMap || {}).map(([id]) => ({ id: parseInt(id) }));
    // Also pull names from heroinfo.xml via regex
    try {
        const heroInfoXml = fs.readFileSync('e:\\server tr\\public\\baxi\\heroinfo.xml', 'utf8');
        const matches = [...heroInfoXml.matchAll(/<i\s+id="(\d+)"\s+name="([^"]+)"/g)];
        const heroList = matches
            .filter(m => parseInt(m[1]) >= 1001 && parseInt(m[1]) <= 1099)
            .map(m => ({ id: parseInt(m[1]), name: m[2] }));
        return res.json(heroList);
    } catch(e) {
        return res.json(heroes);
    }
});

// GET /api/npcs  — list NPC types from npcData
app.get('/api/npcs', (req, res) => {
    const list = Object.entries(npcData)
        .map(([id, n]) => ({ id: parseInt(id), name: n.name || `NPC ${id}`, hp: n.hp, type: n.type }))
        .filter(n => n.id >= 1101 && n.id <= 1200)
        .sort((a, b) => a.id - b.id);
    res.json(list);
});

// GET /api/cosmetics — return all skins/weapons/emotions per hero
app.get('/api/cosmetics', (req, res) => {
    res.json(heroCosmeticsMap);
});

// GET /api/cosmetics/:heroId — return cosmetics for a specific hero
app.get('/api/cosmetics/:heroId', (req, res) => {
    const heroId = req.params.heroId;
    const data = heroCosmeticsMap[heroId] || heroCosmeticsMap[String(parseInt(heroId))] || null;
    if (!data) return res.json({ skins: [], weapons: [], emotions: [] });
    res.json(data);
});

// POST /api/equip_skin — equip a skin and/or weapon skin for a player and respawn with new look
app.post('/api/equip_skin', (req, res) => {
    const { playerId, skinId, weaponId } = req.body;
    const socket = activeSockets.find(s => s.socketType === 'SCENE' && !s.destroyed && String(s.playerId) === String(playerId));
    if (!socket) return res.json({ ok: false, error: 'Jogador não encontrado ou offline.' });
    try {
        const heroId = socket.currentHeroId || 1015;
        const newSkin = parseInt(skinId) || socket.currentSkinId || 1;
        const newWeapon = parseInt(weaponId) || socket.currentWeaponId || 1;
        socket.currentSkinId = newSkin;
        socket.currentWeaponId = newWeapon;

        const currentMapId = socket.targetMapId || lastMapId || 5202;
        const isSoloMap = (currentMapId === 5202 || currentMapId === 5205);
        const prefix = isSoloMap ? 'e1' : `e${socket.playerIndex || 1}`;
        const heroIdEnc = encodeInt(heroId);
        const p1306 = encodeInt(1306);
        const p12002 = encodeInt(12002);

        // 1. Envia novo pacote 1306→12002 com skin/weapon atualizados
        socket.write(`${prefix}:SendMsgToClient,(${p1306},(${p12002},(${heroIdEnc},${generateFullHeroData(heroId, newSkin, newWeapon).split(',').slice(1).join(',')})))\n`);

        // 2. Reenvia o pacote 10001 (spawn) com a nova skin para todos verem
        activeSockets.forEach(s => {
            if (s.socketType === 'SCENE' && !s.destroyed) {
                s.bypassInterceptor = true;
                s.write(getPlayerSpawnPacket(socket, s));
                s.bypassInterceptor = false;
            }
        });

        // Nomes da skin e weapon para resposta
        const cosm = heroCosmeticsMap[heroId] || heroCosmeticsMap[String(heroId)] || {};
        const skinInfo = (cosm.skins || []).find(s => s.id === newSkin);
        const wpInfo = (cosm.weapons || []).find(w => w.id === newWeapon);
        const skinName = skinInfo ? skinInfo.name : `Skin ${newSkin}`;
        const wpName = wpInfo ? wpInfo.name : `Weapon ${newWeapon}`;

        console.log(`[🎨 SKIN EQUIP] ${socket.playerName}: Skin ${newSkin} (${skinName}), Weapon ${newWeapon} (${wpName})`);
        res.json({ ok: true, message: `Skin "${skinName}" e arma "${wpName}" equipadas em ${socket.playerName}.`, skinId: newSkin, weaponId: newWeapon });
    } catch(e) {
        res.json({ ok: false, error: e.message });
    }
});

// POST /api/play_emote — force a player to play an emote (animparm)
app.post('/api/play_emote', (req, res) => {
    const { playerId, animparm, heroId: overrideHeroId } = req.body;
    const socket = activeSockets.find(s => s.socketType === 'SCENE' && !s.destroyed && String(s.playerId) === String(playerId));
    if (!socket) return res.json({ ok: false, error: 'Jogador não encontrado ou offline.' });
    try {
        const anim = String(parseInt(animparm) || 50);
        const pid = socket.playerIndex || 1;
        const p2000 = encodeInt(2000);
        const p2001 = encodeInt(2001);
        const one = encodeInt(1);
        const zero = encodeInt(0);
        const force = encodeInt(socket.force || 1);
        const netId = encodeInt(pid);

        // Build the emote packet — pacote 2000→2001 (mesmo formato do cliente)
        // szNetMsg: (nPinDao, senderNameEnc, msgEnc, target, (), (), cid, ucp, ())
        const szNetMsg = `(${zero},'','','',(),('','','${anim}',${zero},''),${zero},(zero,'',${zero}),())`.replace('zero', zero);
        // szData: (szNetMsg, sound, anim, type, type, force, netId, ico, cdReady, onGround, bePlayActive, nHuDongState)
        const szData = `(${szNetMsg},'','${anim}',${zero},${zero},${force},${netId},'',${one},${one},${one},${zero})`;
        const emotePacket = `e${pid}:SendMsgToClient,(${p2000},(${p2001},${szData}))\n`;

        // Broadcast para todos na cena
        activeSockets.forEach(s => {
            if (s.socketType === 'SCENE' && !s.destroyed) s.write(emotePacket);
        });

        // Try to find emote name for logging
        const heroId = socket.currentHeroId || 1015;
        const cosm = heroCosmeticsMap[heroId] || heroCosmeticsMap[String(heroId)] || {};
        const emoteInfo = (cosm.emotions || []).find(e => e.animparm === parseInt(animparm));
        const emoteName = emoteInfo ? emoteInfo.name : `Emote animparm=${animparm}`;
        console.log(`[🎭 EMOTE PLAY] ${socket.playerName}: ${emoteName} (animparm: ${anim})`);
        res.json({ ok: true, message: `Emote "${emoteName}" executado para ${socket.playerName}.` });
    } catch(e) {
        res.json({ ok: false, error: e.message });
    }
});

// ==========================================
// 🚨 API DE CRASHES & BUGS
// ==========================================
app.get('/api/crash-logs', (req, res) => {
    res.json({ ok: true, logs: global.crashLogs || [] });
});

app.post('/api/clear-crash-logs', (req, res) => {
    global.crashLogs = [];
    res.json({ ok: true });
});

app.post('/api/client-crash', (req, res) => {
    const { message, stack } = req.body;
    if (message) {
        global.crashLogs.push({
            timestamp: new Date().toLocaleTimeString(),
            message: `${message}\n${stack || ''}`,
            type: 'Client Crash'
        });
        if (global.crashLogs.length > 50) global.crashLogs.shift();
    }
    res.json({ ok: true });
});

// ==========================================
// INICIANDO O SERVIDOR WEB (Porta 80)
// ==========================================
app.listen(httpPort, () => {
    console.log(`===================================================`);
    console.log(`[🚀 API/CDN INICIADO] Rodando em http://127.0.0.1:${httpPort}`);
    console.log(`[🎮 ADMIN PANEL] http://127.0.0.1:${httpPort}/admin/`);
    console.log(`===================================================`);
});


// ==========================================
// 5. O CÉREBRO DO JOGO (Servidor TCP Sockets - Porta 7777)
// ==========================================
const net = require('net');
const tcpPort = 7777;

// Alfabeto Base64 customizado do jogo (BaseSocket.cs: GetFromInt64)
// 0-9, A-Z, _, `, a-z
const ALPHABET = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ_`abcdefghijklmnopqrstuvwxyz";

function encodeInt(number) {
    if (number === 0) return '0';
    let result = '';
    let n = Math.abs(Math.floor(number));
    while (n > 0) {
        result = ALPHABET[n % 64] + result;
        n = Math.floor(n / 64);
    }
    return (number < 0 ? '-' : '') + result;
}

function decodeInt(str) {
    if (typeof str === 'number') return str;
    const chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ_`abcdefghijklmnopqrstuvwxyz";
    if (!str || str === "") return 0;
    let s = str.toString();
    if (s.startsWith("'") && s.endsWith("'")) s = s.slice(1, -1);
    let neg = false;
    if (s[0] === '-') { neg = true; s = s.substring(1); }
    let num = 0;
    for (let i = 0; i < s.length; i++) {
        let val = chars.indexOf(s[i]);
        if (val !== -1) num = num * 64 + val;
    }
    return neg ? -num : num;
}

// Utilitário para gerar listas de cosméticos liberados (Skins/Armas)
function generateUnlockList(range = 15) {
    let list = [];
    for (let i = 1; i <= range; i++) {
        const id = i;
        // nLockState = 1 (Liberado para uso)
        const info = `(${encodeInt(id)},1,0,0,'2099/01/01 12:00:00',0,())`;
        list.push(`${encodeInt(id)},${info}`);
    }
    return `(${list.join(',')})`;
}


// Decodificador de Strings Hex do Unity (Fun.DecodingUTF8)
function decodeHex(s) {
    if (!s || typeof s !== 'string') return "";
    let str = s;
    if (str.startsWith("'") && str.endsWith("'")) str = str.slice(1, -1);
    try {
        // Se for um hex válido
        if (/^[0-9a-fA-F]+$/.test(str)) {
            return Buffer.from(str, 'hex').toString('utf8');
        }
        return str;
    } catch (e) {
        return str;
    }
}

// Codificador de Strings Hex do Unity
function encodeHex(s) {
    if (!s) return "";
    return Buffer.from(s, 'utf8').toString('hex');
}

// Os NetIDs agora são absolutos e estáticos de acordo com o playerIndex (sem transposição dinâmica)

// Envia HaveDamage para feedbacks ricos na UI/crosshair (hit markers / sons)
function sendEntityDamageUpdate(targetId, target, damage, attackerId, nPlace = 0, weaponId = 0) {
    const hpEnc = encodeInt(target.hp !== undefined ? target.hp : (target.maxHp || 2000));
    const maxHpEnc = encodeInt(target.maxHp || 2000);
    const shieldEnc = encodeInt(target.shield || 0);
    const maxShieldEnc = encodeInt(target.maxShield || 0);
    const dmgEnc = encodeInt(damage);
    const zero = encodeInt(0);

    const szDamageStr = `(${dmgEnc},(${zero},${zero},${zero}),${dmgEnc},${encodeInt(attackerId)},${encodeInt(targetId)},${encodeInt(nPlace)},${zero},${zero},${encodeInt(weaponId)},${zero},(${zero},${zero},${zero}),${zero},${encodeInt(1)},${encodeInt(weaponId)},${zero})`;
    const szHpStr = `(${hpEnc},${maxHpEnc},${shieldEnc},${maxShieldEnc},${zero},${zero},${zero},(${dmgEnc},${zero},${zero},${zero}),${encodeInt(2)},${zero})`;

    const damagePacket = `e${encodeInt(targetId)}:HaveDamage,(${szDamageStr},${szHpStr})\n`;
    activeSockets.forEach(s => {
        if (s.socketType === "SCENE" && !s.destroyed) {
            s.write(damagePacket);
        }
    });
}

// Função para processar danos em NPCs
function handleNpcHurt(socket, npcId, damage, attackerId = 0, nPlace = 0, weaponId = 0) {
    const npc = spawnedNpcs[npcId];
    if (!npc) return;
    
    let place = nPlace;
    let shieldAbsorbed = 0;
    
    // Absorve com escudo se o NPC tiver escudo
    if (npc.shield && npc.shield > 0) {
        shieldAbsorbed = Math.min(npc.shield, damage);
        npc.shield -= shieldAbsorbed;
        damage -= shieldAbsorbed;
        place = 2; // Shield hit
    }
    
    if (damage > 0) {
        npc.hp = Math.max(0, npc.hp - damage);
    }
    
    console.log(`[🤖 NPC Hurt] NPC ${npcId} took ${damage} HP damage (${shieldAbsorbed} absorbed by shield). Current HP: ${npc.hp}/${npc.maxHp}`);
    
    if (attackerId > 0) {
        sendEntityDamageUpdate(npcId, npc, damage + shieldAbsorbed, attackerId, place, weaponId);
    } else {
        // Fallback DoT / SetHp
        const hpEnc = encodeInt(npc.hp);
        const maxHpEnc = encodeInt(npc.maxHp);
        const shieldEnc = encodeInt(npc.shield || 0);
        const maxShieldEnc = encodeInt(npc.maxShield || npc.shield || 0);
        const zero = encodeInt(0);
        const setHpPacket = `e${encodeInt(npcId)}:SetHp,((${hpEnc},${maxHpEnc},${shieldEnc},${maxShieldEnc},${zero},${zero},${zero},(),${zero},${zero}))\n`;
        activeSockets.forEach(s => {
            if (s.socketType === "SCENE" && !s.destroyed) {
                s.write(setHpPacket);
            }
        });
        console.log(`[🤖 NPC HP Broadcasted]: ${setHpPacket.trim()}`);
    }
    
    // 2. Se o NPC morreu, manda o pacote de destroy 'd[NetID]' e agenda o respawn se for Dummy de treino!
                                     if (npc.hp <= 0) {
                                         console.log(`[💀 NPC Dead] NPC ${npcId} died. Destroying...`);
                                         const destroyPacket = `d${encodeInt(npcId)}\n`;
                                         activeSockets.forEach(s => {
                                             if (s.socketType === "SCENE" && !s.destroyed) {
                                                 s.write(destroyPacket);
                                             }
                                         });

                                         // Scarlett (1022) Ultimate snowball (1013) camera tracking reset upon death
                                          if (npc.index === 1013) {
                                              const creatorSocket = activeSockets.find(s => s.socketType === "SCENE" && !s.destroyed && Number(s.playerId) === Number(npc.masterId));
                                              if (creatorSocket) {
                                                  creatorSocket.write(`e${creatorSocket.playerId}:SuperSkillFinish\n`);
                                                  creatorSocket.write(`e1:SendMsgToClient,(${encodeInt(20014)},(${encodeInt(90008)},(),${encodeInt(1)}))\n`);
                                                  console.log(`[❄️ SNOWBALL RESET] Scarlett Camera reset: e${creatorSocket.playerId}:SuperSkillFinish`);
                                              }
                                          }
                                         
                                         if (npc.isPracticeDummy) {
            console.log(`[♻️ NPC Respawn Scheduled] Practice dummy NPC ${npcId} will respawn in 5 seconds.`);
            setTimeout(() => {
                npc.hp = npc.maxHp;
                if (npc.shield !== undefined) npc.shield = npc.maxShield || 0;
                const className = getEntityClassName(npc.index);
                const respawnPacket = `c${className}:${encodeInt(npc.id)},("Initialize",(${encodeInt(1)},${encodeInt(npc.id)},${encodeInt(npc.index)},${encodeInt(2)},(${encodeInt(npc.x)},${encodeInt(npc.y)},${encodeInt(npc.z)}),(0,0,0),(${encodeInt(100)},${encodeInt(100)},${encodeInt(100)}),(0,0,0),${encodeInt(-1)},${encodeInt(npc.maxHp)},${encodeInt(0)},${encodeInt(1)},${encodeInt(0)},""),((),()),0)\n`;
                activeSockets.forEach(s => {
                    if (s.socketType === "SCENE" && !s.destroyed) {
                        s.write(respawnPacket);
                    }
                });
                console.log(`[♻️ NPC Respawned] Practice dummy NPC ${npcId} has respawned!`);
            }, 5000);
        } else {
            console.log(`[🗑️ NPC Cleaned] Dynamic skill NPC ${npcId} removed from registry.`);
            delete spawnedNpcs[npcId];
        }
    }
}

// ==========================================
// 🛡️ MOTOR GENÉRICO DE BUFFS & DEBUFFS DO SERVIDOR
// ==========================================
function applyBuffToEntity(socket, targetId, buffId, customDuration) {
    if (!socket || socket.destroyed) return;
    const playerId = socket.playerId || "1";
    const buff = buffsMap[buffId];
    if (!buff) {
        console.log(`[⚠️ BUFF ENGINE] Buff ${buffId} não encontrado no buffsMap.`);
        return;
    }

    const duration = customDuration !== undefined ? customDuration : (buff.time || 5000);
    const buffDateStr = Fun.GetServerDate();

    // Determina a entidade alvo (Jogador ou NPC)
    let target = null;
    let isPlayer = false;
    
    // Procura primeiro por outro jogador em activeSockets para abranger PvP e suporte a buffs em aliados
    const foundPlayer = activeSockets.find(s => s.socketType === "SCENE" && !s.destroyed && (s.playerId === targetId || String(s.playerId) === String(targetId)));
    if (foundPlayer) {
        target = foundPlayer;
        isPlayer = true;
    } else if (spawnedNpcs[targetId]) {
        target = spawnedNpcs[targetId];
    }
    
    if (!target) {
        console.log(`[⚠️ BUFF ENGINE] Entidade alvo ${targetId} não encontrada.`);
        return;
    }

    target.activeBuffs = target.activeBuffs || {};

    // Se já houver um timer para o mesmo Buff, executa a limpeza prévia para evitar acúmulos
    if (target.activeBuffs[buffId]) {
        cleanupBuff(target, targetId, buffId, isPlayer);
    }

    // Envia o pacote de AddBuff para todos os clientes na cena
    const addBuffPacket = `e${encodeInt(targetId)}:AddBuff,(('${buffDateStr}',${encodeInt(buffId)},${encodeInt(playerId)}))\n`;
    activeSockets.forEach(s => {
        if (s.socketType === "SCENE" && !s.destroyed) {
            s.write(addBuffPacket);
        }
    });
    console.log(`[🛡️ BUFF ENGINE] Buff "${buff.name}" (${buffId}) aplicado em ${isPlayer ? 'Jogador' : 'NPC'} ${targetId} | Caster: ${playerId} | Duração: ${duration}ms`);

    // Efeitos imediatos no Servidor
    if (buffId === 1015 && isPlayer && target.currentHeroId === 1023) {
        const targetIdEnc = encodeInt(Number(targetId));
        const enterCocoonPacket = `e${encodeInt(targetId)}:CA,((('SkillE',0,1),1,${targetIdEnc}))\n`;
        activeSockets.forEach(s => { if (s.socketType === "SCENE" && !s.destroyed) s.write(enterCocoonPacket); });
        console.log(`[🛡️ COCOON ENTER] Brigit ${targetId} visual casulo ativado: ${enterCocoonPacket.trim()}`);
    }

    if (buff.type === 7) { // Unbeatable / Invencível
        target.isInvincible = true;
    } else if (buff.type === 5) { // Shield / Escudo de Energia
        target.shield = (target.shield || 0) + buff.value;
        target.maxShield = Math.max(target.maxShield || 0, target.shield);
        sendEntityHpUpdate(target, targetId, target, isPlayer);
    } else if (buff.type === 10) { // Invisibility / Invisibilidade
        target.isInvis = true;
    } else if (buff.type === 11) { // Freeze / Imprison (CC)
        target.isFrozen = true;
        console.log(`[❄️ CC STATE] Alvo ${targetId} está CONGELADO.`);
    } else if (buff.type === 12) { // Stun (CC)
        target.isStunned = true;
        console.log(`[💫 CC STATE] Alvo ${targetId} está ATORDOADO.`);
    }

    // Ticks contínuos (Cura ou Dano Periódico / DoT)
    let intervalId = null;
    if (buff.type === 1 && buff.interval > 0 && buff.value !== 0) {
        intervalId = setInterval(() => {
            if ((isPlayer && target.destroyed) || (target.hp !== undefined && target.hp <= 0) || (!isPlayer && !spawnedNpcs[targetId])) {
                clearInterval(intervalId);
                return;
            }
            if (buff.isdebuff === 1) { // Debuff (Dano sobre tempo - ex: veneno/fogo)
                if (target.godMode || target.isInvincible) return; // Invencível cancela o dano
                let dmg = Math.abs(buff.value);
                
                // Se tiver escudo, absorve o DoT antes do HP!
                if (target.shield && target.shield > 0) {
                    const absorbed = Math.min(target.shield, dmg);
                    target.shield -= absorbed;
                    dmg -= absorbed;
                    sendEntityHpUpdate(target, targetId, target, isPlayer);
                }
                
                if (dmg > 0) {
                    if (isPlayer) {
                        target.hp = Math.max(0, (target.hp || target.maxHp || 2000) - dmg);
                        sendEntityHpUpdate(target, targetId, target, isPlayer);
                        if (target.hp <= 0) handlePlayerDeath(target);
                    } else {
                        handleNpcHurt(target, targetId, dmg);
                    }
                }
            } else { // Buff (Cura sobre tempo - ex: regeneração)
                const heal = buff.value;
                if (isPlayer) {
                    target.hp = Math.min(target.maxHp || 2000, (target.hp || target.maxHp || 2000) + heal);
                    sendEntityHpUpdate(target, targetId, target, isPlayer);
                } else {
                    target.hp = Math.min(target.maxHp || 500, target.hp + heal);
                    sendEntityHpUpdate(target, targetId, target, isPlayer);
                }
            }
        }, buff.interval);
    } else if (buff.type === 1 && (buff.interval === 0 || !buff.interval) && buff.value !== 0) {
        // Cura ou Dano instantâneo (ex: Bolsa de Sangue Grande 1002, Pequena 1003, Vincent Heal Ball 1038, Khan self-harm 1046)
        if (buff.isdebuff === 1) {
            if (!(target.godMode || target.isInvincible)) {
                let dmg = Math.abs(buff.value);
                if (target.shield && target.shield > 0) {
                    const absorbed = Math.min(target.shield, dmg);
                    target.shield -= absorbed;
                    dmg -= absorbed;
                    sendEntityHpUpdate(target, targetId, target, isPlayer);
                }
                if (dmg > 0) {
                    if (isPlayer) {
                        target.hp = Math.max(0, (target.hp || target.maxHp || 2000) - dmg);
                        sendEntityHpUpdate(target, targetId, target, isPlayer);
                        if (target.hp <= 0) handlePlayerDeath(target);
                    } else {
                        handleNpcHurt(target, targetId, dmg);
                    }
                }
            }
        } else {
            const heal = buff.value;
            if (isPlayer) {
                target.hp = Math.min(target.maxHp || 2000, (target.hp || target.maxHp || 2000) + heal);
                sendEntityHpUpdate(target, targetId, target, isPlayer);
            } else {
                target.hp = Math.min(target.maxHp || 500, target.hp + heal);
                sendEntityHpUpdate(target, targetId, target, isPlayer);
            }
        }
    }

    // Agenda a expiração e limpeza do Buff diretamente no alvo
    const timeout = setTimeout(() => {
        cleanupBuff(target, targetId, buffId, isPlayer);
    }, duration);

    target.activeBuffs[buffId] = { timeout, intervalId, casterId: playerId };
}

function cleanupBuff(targetSocketOrNpc, targetId, buffId, isPlayer) {
    let target = null;
    if (isPlayer) {
        if (targetSocketOrNpc && typeof targetSocketOrNpc === 'object' && targetSocketOrNpc.activeBuffs) {
            target = targetSocketOrNpc;
        } else {
            target = activeSockets.find(s => s.socketType === "SCENE" && !s.destroyed && (s.playerId === targetId || String(s.playerId) === String(targetId)));
        }
    } else if (spawnedNpcs[targetId]) {
        target = spawnedNpcs[targetId];
    }
    if (!target || !target.activeBuffs || !target.activeBuffs[buffId]) return;

    const buff = buffsMap[buffId];
    if (buff) {
        if (buff.type === 7) {
            target.isInvincible = target.godMode || false;
        } else if (buff.type === 5) {
            target.shield = Math.max(0, (target.shield || 0) - buff.value);
            if (target.shield === 0) {
                target.maxShield = 0;
            }
            sendEntityHpUpdate(target, targetId, target, isPlayer);
        } else if (buff.type === 10) {
            target.isInvis = false;
        } else if (buff.type === 11) {
            target.isFrozen = false;
            console.log(`[❄️ CC STATE END] Alvo ${targetId} não está mais CONGELADO.`);
        } else if (buff.type === 12) {
            target.isStunned = false;
            console.log(`[💫 CC STATE END] Alvo ${targetId} não está mais ATORDOADO.`);
        }
    }

    const casterId = target.activeBuffs[buffId].casterId || targetId;

    if (target.activeBuffs[buffId].timeout) {
        clearTimeout(target.activeBuffs[buffId].timeout);
    } else {
        clearTimeout(target.activeBuffs[buffId]);
    }
    if (target.activeBuffs[buffId].intervalId) {
        clearInterval(target.activeBuffs[buffId].intervalId);
    }
    delete target.activeBuffs[buffId];

    const delBuffPacket = `e${encodeInt(targetId)}:DeleteBuff,((${encodeInt(buffId)},${encodeInt(0)},${encodeInt(casterId)}))\n`;
    activeSockets.forEach(s => {
        if (s.socketType === "SCENE" && !s.destroyed) {
            s.write(delBuffPacket);
        }
    });
    console.log(`[🛡️ BUFF ENGINE] Expira/Remove Buff ${buffId} de ${isPlayer ? 'Jogador' : 'NPC'} ${targetId}`);

    // Se for o Buff de Casulo da Brigit expirando, garante reset visual (SkillE = false) e reset de invencibilidade/godMode
    if (buffId === 1015 && isPlayer) {
        target.isInvincible = false;
        target.godMode = false;
        if (target.currentHeroId === 1023) {
            const targetIdEnc = encodeInt(Number(targetId));
            const exitCocoonPacket = `e${encodeInt(targetId)}:CA,((('SkillE',0,0),1,${targetIdEnc}))\n`;
            activeSockets.forEach(s => {
                if (s.socketType === "SCENE" && !s.destroyed) {
                    s.write(exitCocoonPacket);
                }
            });
        }
    }
}

function sendEntityHpUpdate(socket, targetId, target, isPlayer) {
    const hpEnc = encodeInt(target.hp !== undefined ? target.hp : (target.maxHp || 2000));
    const maxHpEnc = encodeInt(target.maxHp || 2000);
    const shieldEnc = encodeInt(target.shield || 0);
    const maxShieldEnc = encodeInt(target.maxShield !== undefined ? target.maxShield : (target.shield || 0));
    const zero = encodeInt(0);
    
    const setHpPacket = `e${encodeInt(targetId)}:SetHp,((${hpEnc},${maxHpEnc},${shieldEnc},${maxShieldEnc},${zero},${zero},${zero},(),${zero},${zero}))\n`;
    activeSockets.forEach(s => {
        if (s.socketType === "SCENE" && !s.destroyed) {
            s.write(setHpPacket);
        }
    });
}

function sendGlobalSystemChat(text) {
    activeSockets.forEach(s => {
        if (s.socketType === "SCENE" && !s.destroyed) {
            sendSystemChat(s, text);
        }
    });
}

function getSafeMapId(mapId) {
    const id = parseInt(mapId);
    // Permite que os mapas de TDM clássicos rodem nativamente sem redirecionamento indevido
    if (id === 5260) return 5202; 
    if (id === 5063) return 5022; // Cidade da Glória -> Cidade Estelar (Escort)
    if (id === 5208) return 5030; // 英魂祭坛 -> Terraço das Nuvens (Control)
    return id;
}

function getSpawnCoordinates(mapId, force) {
    const defaultCoords = { sx: 5000, sy: 2010, sz: 2413 }; // bachang_baxi default
    
    let targetMapId = Number(mapId);
    if (!targetMapId) return defaultCoords;
    
    // Resolve os IDs de mapas reais/customizados para coordenadas de spawn de mapas válidos equivalentes
    if (targetMapId === 5260) targetMapId = 5049; // Mansão do Anjo uses Snow Mountain coordinates
    if (targetMapId === 5301) targetMapId = 5030;
    if (targetMapId === 5302) targetMapId = 5074;
    if (targetMapId === 5303) targetMapId = 5068;
    if (targetMapId === 5304) targetMapId = 5050;
    if (targetMapId === 5305) targetMapId = 5055;
    
    const forceStr = force ? force.toString() : "1";
    
    // Coordinates from fbmodel.xml revivepoint1 (format: x,z,y,rot per slot)
    // Server sends: szScn[1]=sx, szScn[2]=sz, szScn[3]=sy
    // Client does: pos = Vector3(sx/100, sy/100, sz/100)
    // So: x=sx, y=sy, z=sz (matching fbmodel x,y,z order)
    const mapSpawns = {
      // Pharaoh's Mausoleum (fbmodel 1003, type 7 DeathMatch)
      "5010": {
        "1": { x: 12147, y: 2610, z: 19003 },
        "2": { x: 12184, y: 2610, z: 27788 }
      },
      // Escort 2 (fbmodel 1017, type 2)
      "5022": {
        "1": { x: 24800, y: 10000, z: 22700 },
        "2": { x: 16350, y: 10000, z: 20850 }
      },
      // Cloud Rooftop (fbmodel 1005, type 3 Control)
      "5030": {
        "1": { x: 141190, y: 14695, z: 106980 },
        "2": { x: 127840, y: 14695, z: 106960 }
      },
      // Snow Mountain Monastery (fbmodel 1011, type 5 Resource)
      "5049": {
        "1": { x: 25700, y: 15300, z: 17800 },
        "2": { x: 14800, y: 15300, z: 24200 }
      },
      // Castelo Dracula (fbmodel 1010, type 3 Control)
      "5050": {
        "1": { x: 58048, y: 18034, z: 45374 },
        "2": { x: 42291, y: 18043, z: 45056 }
      },
      // Underground Palace (fbmodel 1012, type 3 Control)
      "5055": {
        "1": { x: 115688, y: 79010, z: 107410 },
        "2": { x: 114998, y: 79010, z: 122992 }
      },
      // Warrior Arena (fbmodel 1006, type 4 Arena)
      "5056": {
        "1": { x: 10500, y: 2014, z: 12000 },
        "2": { x: 19500, y: 2014, z: 12000 }
      },
      // Pharaoh's DM (fbmodel 1019, type 4 Arena - same scene as 5010)
      "5058": {
        "1": { x: 12147, y: 2610, z: 19003 },
        "2": { x: 12184, y: 2610, z: 27788 }
      },
      // KingHall DM (fbmodel 1018, type 4 Arena - same scene as 5204)
      "5059": {
        "1": { x: 9802, y: 3820, z: 4602 },
        "2": { x: 1797, y: 3820, z: 5999 }
      },
      // City of Glory (fbmodel 1013, type 2 Escort)
      "5063": {
        "1": { x: 9600, y: 9911, z: 18050 },
        "2": { x: 13750, y: 9400, z: 7000 }
      },
      // Desert Royal Palace (fbmodel 1009, type 3 Control)
      "5068": {
        "1": { x: 6700, y: 970, z: 23550 },
        "2": { x: 22800, y: 970, z: 23572 }
      },
      // Dam (fbmodel 1004, type 3 Control)
      "5074": {
        "1": { x: 63829, y: 6892, z: 72949 },
        "2": { x: 42280, y: 6892, z: 73318 }
      },
      // Robot Cloud Rooftop (same coords as 5030)
      "5100": {
        "1": { x: 141190, y: 14695, z: 106980 },
        "2": { x: 127840, y: 14695, z: 106960 }
      },
      // Robot Desert (same coords as 5068)
      "5102": {
        "1": { x: 6700, y: 970, z: 23550 },
        "2": { x: 22800, y: 970, z: 23572 }
      },
      // Robot KingHall DM (fbmodel 2013, same coords as 5204)
      "5103": {
        "1": { x: 9802, y: 3820, z: 4602 },
        "2": { x: 1797, y: 3820, z: 5999 }
      },
      // Robot Pharaoh DM (fbmodel 2014, same coords as 5010)
      "5104": {
        "1": { x: 12147, y: 2610, z: 19003 },
        "2": { x: 12184, y: 2610, z: 27788 }
      },
      // Training (bachang_baxi default)
      "5202": {
        "1": { x: 5000, y: 2010, z: 2413 },
        "2": { x: 5000, y: 2010, z: 2413 }
      },
      // King Hall (fbmodel 1007, type 7 DeathMatch)
      "5204": {
        "1": { x: 9802, y: 3820, z: 4602 },
        "2": { x: 1797, y: 3820, z: 5999 }
      },
      // Training 2 (bachang_baxi)
      "5205": {
        "1": { x: 5000, y: 2010, z: 2413 },
        "2": { x: 5000, y: 2010, z: 2413 }
      },
      // Control 06 (fbmodel 1016, type 3 Control)
      "5208": {
        "1": { x: 16800, y: 5220, z: 9600 },
        "2": { x: 3200, y: 5220, z: 9600 }
      },
      // DM 04 (fbmodel 1021)
      "5210": {
        "1": { x: 5400, y: 5100, z: 9000 },
        "2": { x: 4600, y: 5100, z: 1000 }
      },
      // DM 05 (fbmodel 1022)
      "5212": {
        "1": { x: 5400, y: 5000, z: 9300 },
        "2": { x: 4600, y: 5000, z: 700 }
      },
      // Angel's Manor (fbmodel 1015, type 5 Resource)
      "5260": {
        "1": { x: 16700, y: 5020, z: 4500 },
        "2": { x: 3300, y: 5020, z: 4500 }
      }
    };
    
    const spawns = mapSpawns[targetMapId.toString()];
    if (spawns) {
        let spawn = spawns[forceStr];
        if (!spawn) {
            spawn = spawns["1"] || spawns["2"];
        }
        if (spawn) {
            return { sx: spawn.x, sy: spawn.y, sz: spawn.z };
        }
    }
    return defaultCoords;
}

function handlePlayerDeath(socket, attackerSocket = null) {
    if (!socket || socket.destroyed) return;
    
    // 🛡️ REBIRTH PASSIVO DA BRIGIT (Casulo 1015/1016)
    const heroId = socket.currentHeroId || 1015;
    const now = Date.now();
    const cooldown = 60000; // 60 segundos de recarga
    if (heroId === 1023 && (!socket.lastRebirthTime || now - socket.lastRebirthTime >= cooldown)) {
        socket.lastRebirthTime = now;
        socket.hp = 1;
        socket.isInvincible = true;
        socket.godMode = true;
        sendEntityHpUpdate(socket, socket.playerId || "1", socket, true);
        
        console.log(`[🔥 BRIGIT REBIRTH] Brigit (${socket.playerId}) atingiu 0 HP, ativando casulo passivo.`);
        
        // Aplica os buffs de casulo: 1015 (Unbeatable stasis) e 1016 (Self heal) por 4s (4000ms)
        const playerId = socket.playerId || "1";
        applyBuffToEntity(socket, playerId, 1015, 4000);
        applyBuffToEntity(socket, playerId, 1016, 4000);
        
        // Dispara animações de casulo e transição de câmera simulando a ativação do casulo 1068
        const skillIdEnc = encodeInt(1068);
        const xEnc = encodeInt(socket.lastX || 0);
        const yEnc = encodeInt(socket.lastY || 0);
        const zEnc = encodeInt(socket.lastZ || 0);
        const targetIdEnc = encodeInt(Number(playerId));
        const skillDoPacket = `e${playerId}:SkillDo,((${skillIdEnc},(${xEnc},${yEnc},${zEnc})),(${targetIdEnc}))\n`;
        activeSockets.forEach(s => {
            if (s.socketType === "SCENE" && !s.destroyed) {
                s.write(skillDoPacket);
            }
        });

        // Broadcast adicional da animação de entrada do casulo (SkillE = true) para garantir sync visual de outros players
        const enterCocoonPacket = `e${playerId}:CA,((('SkillE',0,1),1,${targetIdEnc}))\n`;
        activeSockets.forEach(s => {
            if (s.socketType === "SCENE" && !s.destroyed) {
                s.write(enterCocoonPacket);
            }
        });

        // Aviso global para todos os jogadores na cena
        sendGlobalSystemChat("🔥 [Sistema] Casulo de Brigit Ativado! Renascendo das cinzas...");
        return;
    }

    console.log(`[💀 PLAYER DEAD] O jogador morreu! Teleportando para o spawn da equipe...\n`);
    socket.hp = socket.maxHp || 2000;
    socket.shield = 0;
    socket.maxShield = 0;
    socket.isInvincible = false;
    socket.godMode = false;
    
    // Determina as coordenadas do spawn baseadas no mapa e equipe
    const currentMapId = socket.targetMapId || lastMapId || 5202;
    const coords = getSpawnCoordinates(currentMapId, socket.force);
    let sx = coords.sx, sy = coords.sy, sz = coords.sz;
    
    socket.lastX = sx;
    socket.lastY = sy;
    socket.lastZ = sz;
    
    // Atualiza HP/Escudo
    sendEntityHpUpdate(socket, socket.playerId || "1", socket, true);
    
   // Broadcast para todos os jogadores na cena (incluindo o próprio jogador falecido) com o spawnPacket correto para cada um
    activeSockets.forEach(s => {
        if (s.socketType === "SCENE" && !s.destroyed) {
            s.bypassInterceptor = true;
            s.write(getPlayerSpawnPacket(socket, s));
            s.bypassInterceptor = false;
        }
    });
    
    // Envia mensagem informativa amarela para o próprio jogador
    const sysMsgHex = Buffer.from("[Sistema] Você morreu e renasceu na base da sua equipe!").toString('hex');
    const sysNetMsg = `(${encodeInt(8)},'','${sysMsgHex}','',(),(),${encodeInt(1)},('','',0),())`;
    socket.write(`e${socket.playerIndex || 1}:SendMsgToClient,(${encodeInt(1006)},${sysNetMsg})\n`);

    // --- Integração com Placar de Team Deathmatch ---
    if (attackerSocket) {
        sendGlobalSystemChat(`💀 [Combate] ${attackerSocket.playerName || 'Inimigo'} derrotou ${socket.playerName || 'Jogador'}!`);
        
        if (global.activeTdmMatch && global.activeTdmMatch.state === 'fight') {
            const killerForce = attackerSocket.force;
            if (killerForce === 1 || killerForce === 2) {
                incrementTdmScore(killerForce, 1);
            }
        }
    } else {
        sendGlobalSystemChat(`💀 [Combate] ${socket.playerName || 'Jogador'} foi eliminado.`);
    }
}

function sendSystemChat(socket, text) {
    if (!socket || socket.destroyed) return;
    const p1006 = encodeInt(1006);
    const pCid = encodeInt(1);
    const hexMsg = Buffer.from(text).toString('hex');
    const sysNetMsg = `(${encodeInt(8)},'','${hexMsg}','',(),(),${pCid},('','',0),())`;
    socket.write(`e${socket.playerIndex || 1}:SendMsgToClient,(${p1006},${sysNetMsg})\n`);
}

function sendGlobalSystemChat(text) {
    activeSockets.forEach(s => {
        if (s.socketType === "SCENE" && !s.destroyed) {
            sendSystemChat(s, text);
        }
    });
}

// Objeto Fun para compatibilidade com o protocolo do jogo
const Fun = {
    GetServerDate: () => {
        const d = new Date();
        const pad = (n) => n.toString().padStart(2, '0');
        // Usar barras (/) garante que o DateTime.Parse do C# trate de forma correta e invariante em qualquer idioma de Windows (inclusive pt-BR)
        return `${d.getFullYear()}/${pad(d.getMonth() + 1)}/${pad(d.getDate())} ${pad(d.getHours())}:${pad(d.getMinutes())}:${pad(d.getSeconds())}`;
    },
    DecodingUTF8: (s) => decodeHex(s),
    EncodingUTF8: (s) => encodeHex(s),
    Now: () => new Date().toISOString().replace(/T/, ' ').replace(/\..+/, '')
};

// Helper para enviar pacotes com atraso (Simula latência e evita sobrecarga)
function logPacket(type, direction, data) {
    if (!data || typeof data !== 'string') return;
    // Tenta capturar o ID da mensagem no formato (ID,...)
    const idMatch = data.match(/\(([0-9A-Z_`a-z-]+),/);
    const idStr = idMatch ? idMatch[1] : '??';
    let idLabel = idStr;
    try {
        const idNum = decodeInt(idStr);
        idLabel = `${idNum} (${idStr})`;
    } catch (e) { }

    console.log(`[${type}] ${direction === 'IN' ? '⬅️' : '➡️'} ID: ${idLabel} | DATA: ${data.trim().substring(0, 150)}...`);
}

async function sendPacket(socket, data, delay = 0) {
    if (delay > 0) await new Promise(r => setTimeout(r, delay));
    if (socket && !socket.destroyed) {
        socket.write(data);
        logPacket(socket.socketType || 'HALL', 'OUT', data);
    }
}

// Constantes de Pacotes (IDs convertidos para Alfabeto Unity)
const p80001 = encodeInt(80001);
const s90001 = encodeInt(90001);
const s90002 = encodeInt(90002);
const s90004 = encodeInt(90004);
const s90008 = encodeInt(90008);
const p10001_spawn = encodeInt(10001);
const p1304 = encodeInt(1304);

// Parser para as listas do Unity: (item1,item2,(subitem1,subitem2))
// Converte a string do Unity em um Array do JavaScript
function parseArrayList(str) {
    if (!str) return [];
    str = str.trim();
    // Se não começa com (, envolvemos para processar como uma lista de um item
    let content = str;
    if (str.startsWith('(') && str.endsWith(')')) {
        content = str.slice(1, -1);
    }

    let result = [];
    let current = "";
    let depth = 0;
    let inQuotes = false;

    for (let i = 0; i < content.length; i++) {
        const char = content[i];
        if (char === "'" && (i === 0 || content[i - 1] !== "\\")) inQuotes = !inQuotes;
        if (!inQuotes) {
            if (char === "(") depth++;
            if (char === ")") depth--;
            if (char === "," && depth === 0) {
                result.push(parseValue(current));
                current = "";
                continue;
            }
        }
        current += char;
    }
    if (current.trim() !== "") result.push(parseValue(current));
    return result;
}

function parseValue(val) {
    val = val.trim();
    if (val === "()") return [];
    if (val.startsWith("(") && val.endsWith(")")) return parseArrayList(val);
    if (val.startsWith("'") && val.endsWith("'")) return val.slice(1, -1);
    
    // Decodifica qualquer string unquoted alfanumérica (que a Unity envia como Base64)
    if (/^-?[0-9A-Z_`a-z-]+$/.test(val)) {
        return decodeInt(val);
    }
    
    // Se for um float bruto/decimal, retorna como número
    if (!isNaN(val) && val !== "") return Number(val);
    
    return val;
}

// Codifica string para o formato UTF8 que a Unity espera
// Fun.EncodingUTF8 no C#: cada byte convertido para HEX (base 16) e CONCATENADO sem separador
// Ex: "Admin" → bytes [65,100,109,105,110] → hex "41646d696e"
function encodingUTF8(str) {
    const bytes = Buffer.from(str, 'utf8');
    const hex = Array.from(bytes).map(b => b.toString(16).padStart(2, '0')).join('');
    return `[mi]${hex}[/mi]`;
}

// Nome do jogador — usado em todos os payloads
const PLAYER_NAME = "Admin";
const PLAYER_NAME_ENC = encodeHex(PLAYER_NAME);

// Carrega o mapa de cosméticos por herói gerado do herounlock.xml
let heroCosmeticsMap = {};
try {
    heroCosmeticsMap = JSON.parse(fs.readFileSync('e:\\server tr\\public\\baxi\\herocosmetics.json', 'utf8'));
    console.log(`[🎨 COSMETICS] Carregados ${Object.keys(heroCosmeticsMap).length} heróis com cosméticos.`);
} catch(e) {
    console.warn('[⚠️ COSMETICS] herocosmetics.json não encontrado, usando padrão.');
}

// Função global para gerar dados de heróis
// useSkinId: ID da skin equipada (1-15). useWeaponId: ID da arma equipada (1-15).
const generateFullHeroData = (id, useSkinId = 1, useWeaponId = 1) => {
    const one = encodeInt(1);
    const zero = encodeInt(0);
    const dateStr = Fun.GetServerDate();
    const idEnc = encodeInt(id);
    
    let skinList = [];
    let weaponList = [];
    let emoteList = [];
    let poseList = [];
    for (let i = 1; i <= 15; i++) {
        skinList.push(`${encodeInt(i)},(${encodeInt(i)},${one},${zero},${zero},'${dateStr}',${zero})`);
        weaponList.push(`${encodeInt(i)},(${encodeInt(i)},${one},${zero},${zero},'${dateStr}',${zero},())`);
        emoteList.push(`${encodeInt(i)},(${encodeInt(i)},${one},${zero},${zero},'${dateStr}',${zero})`);
        poseList.push(`${encodeInt(i)},(${encodeInt(i)},${one},${zero},${zero},'${dateStr}',${zero})`);
    }
    const unlockedSkin = `(${skinList.join(',')})`;
    const unlockedWeapon = `(${weaponList.join(',')})`;
    const unlockedEmote = `(${emoteList.join(',')})`;
    const unlockedPose = `(${poseList.join(',')})`;
    
    // Posições 11,12,13,14 = UseSkin, UseWeapon, UseEmotion, UsePose (índices do client)
    const useSkinEnc = encodeInt(Math.max(1, parseInt(useSkinId) || 1));
    const useWeaponEnc = encodeInt(Math.max(1, parseInt(useWeaponId) || 1));
    return `${idEnc},(${one},${zero},${one},'${dateStr}',${zero},(),${unlockedSkin},${unlockedWeapon},${unlockedEmote},${unlockedPose},${zero},${useSkinEnc},${useWeaponEnc},${one},${one},${zero})`;
};

function getPlayerSpawnPacket(s, receiverSocket) {
    const heroId = s.currentHeroId || 1015;
    const heroIdEnc = encodeInt(heroId);
    const one = encodeInt(1);
    const zero = encodeInt(0);
    const forceEnc = encodeInt(s.force || 1);
    const heroInfo = generateFullHeroData(heroId, s.currentSkinId || 1, s.currentWeaponId || 1).split(',').slice(1).join(',');
    
    // Absolute Player Index
    const pIdx = s.playerIndex || 1;
    
    const baseInfo = `(${encodeInt(pIdx)},'${s.playerNameEnc}',${forceEnc},${heroIdEnc},${zero},${zero},${one},${one},${one},${zero},${zero},(${heroIdEnc},${heroInfo}),${zero},(),${one},${one})`;
    
    const maxHp = s.maxHp || getHeroMaxHp(heroId);
    const maxHpEnc = encodeInt(maxHp);
    const battleAttr = `(${one},${maxHpEnc},${maxHpEnc},${one},${one},${one},${one},${one},${one},${encodeInt(10000)},${one},${one},${one},${one},${one},${one},${one},${one},${one},${one})`;
    
    const currentMapId = s.targetMapId || lastMapId || 5202;
    const defaultCoords = getSpawnCoordinates(currentMapId, s.force);
    let sx = s.lastX || defaultCoords.sx;
    let sy = s.lastY || defaultCoords.sy;
    let sz = s.lastZ || defaultCoords.sz;
    const scnInfo = `(${encodeInt(getSafeMapId(currentMapId))},${encodeInt(sx)},${encodeInt(sz)},${encodeInt(sy)},${zero},${zero},${zero},${encodeInt(180)})`;
    const msg_part1 = `(${baseInfo},${battleAttr},scnInfo,(),(),('Local',${one},${one},${one},${one},${zero},'',${one}),(),())`;
    
    const finalMsgPart1 = msg_part1.replace('scnInfo', scnInfo);
    
    const arySkillServerData = generateHeroSkillsData(heroId);
    const heroDazhaoData = generateHeroDazhaoData(heroId);
    
    const maxAmmo = weaponsMap[heroId] && weaponsMap[heroId].cartridgePublic !== undefined ? weaponsMap[heroId].cartridgePublic : 30;
    const force = s.force || 1;
    const superData = (heroId === 1005) ? `(${encodeInt(force)},'',${zero},(${encodeInt(7000)}))` : `()`;
    const msg_part2 = `(${one},zero,((0),(),(${encodeInt(maxAmmo)})),${heroIdEnc},${heroDazhaoData},${arySkillServerData},${superData},())`.replace('zero', zero);
    
    if (receiverSocket && typeof receiverSocket === 'object' && s.playerName === receiverSocket.playerName) {
        // Spawn own HeroPlayer
        return `e${pIdx}:SendMsgToClient,(${encodeInt(10001)},(${finalMsgPart1},${msg_part2}))\n`;
    } else {
        // Spawn remote GamePlayer
        return `d${encodeInt(pIdx)}\ncGamePlayer:${encodeInt(pIdx)},("PlayerSelect",(${finalMsgPart1},${msg_part2}))\n`;
    }
}

function switchPlayerHero(socket, selectedHeroId) {
    if (!socket || socket.destroyed) return;
    try {
        if (socket.waitingForHeroLock) {
            console.log(`[🎭 SCENE HERO LOCK] Jogador ${socket.playerName || 'Jogador'} confirmou o herói inicial: ${selectedHeroId}. Iniciando carregamento de mapa...`);
            
            socket.waitingForHeroLock = false;
            socket.currentHeroId = selectedHeroId;
            socket.hasConfirmedHero = true;
            
            const maxHp = getHeroMaxHp(selectedHeroId);
            socket.maxHp = maxHp;
            socket.hp = maxHp;
            socket.shield = socket.godMode ? 50000 : 0;
            socket.isInvincible = socket.godMode || false;
            
            // Envia o pacote de loading screen (s90004) antes de iniciar a transição de mapa
            const p20014 = encodeInt(20014);
            const s90004 = encodeInt(90004);
            socket.write(`e${socket.playerIndex || 1}:SendMsgToClient,(${p20014},(${s90004}))\n`);
            
            // 1. Envia 12002 para carregar o novo herói na memória do cliente
            const prefix = `e${socket.playerIndex || 1}`;
            const p1306 = encodeInt(1306);
            const p12002 = encodeInt(12002);
            const heroIdEnc = encodeInt(selectedHeroId);
            socket.write(`${prefix}:SendMsgToClient,(${p1306},(${p12002},(${heroIdEnc},${generateFullHeroData(selectedHeroId, 1, 1).split(',').slice(1).join(',')})))\n`);
            
            // 2. Agora sim, envia o CLoadScene para carregar o mapa de combate real!
            const fMapId = socket.finalMapId || lastFinalMapId;
            if (fMapId) {
                socket.targetMapId = fMapId;
                socket.targetMapName = MAP_CONFIG[fMapId] ? MAP_CONFIG[fMapId].name : 'bachang_baxi';
                lastFinalMapId = fMapId;
            }
            const scnId = getSafeMapId(socket.targetMapId || lastMapId || 5202);
            const scnName = socket.targetMapName || lastMapName || 'bachang_baxi';
            setTimeout(() => {
                socket.write(`e:CLoadScene,((0,'${scnName}',0,${encodeInt(scnId)}))\n`);
            }, 1000);
            return;
        }

        console.log(`[🎭 SCENE HERO CHANGE] Executando troca de herói para: ${selectedHeroId}`);
        
        const prefix = `e${socket.playerIndex || 1}`;

        // 1. Envia 12002 para carregar o novo herói na memória do cliente
        const p1306 = encodeInt(1306);
        const p12002 = encodeInt(12002);
        const heroIdEnc = encodeInt(selectedHeroId);
        // Ao trocar de herói, resetar skin/weapon para a padrão (1) a menos que já tenha sido definido via admin
        socket.currentSkinId = 1;
        socket.currentWeaponId = 1;
        socket.write(`${prefix}:SendMsgToClient,(${p1306},(${p12002},(${heroIdEnc},${generateFullHeroData(selectedHeroId, socket.currentSkinId, socket.currentWeaponId).split(',').slice(1).join(',')})))\n`);

        // Mantém o selectedHeroId na sessão do socket
        socket.currentHeroId = selectedHeroId;
        const maxHp = getHeroMaxHp(selectedHeroId);
        socket.maxHp = maxHp;
        socket.hp = maxHp;
        socket.shield = socket.godMode ? 50000 : 0;
        socket.isInvincible = socket.godMode || false;

        // Ao trocar de herói em jogo, resetar coordenadas para a base de spawn correspondente
        const resetMapId = socket.targetMapId || lastMapId || 5202;
        const coords = getSpawnCoordinates(resetMapId, socket.force);
        socket.lastX = coords.sx;
        socket.lastY = coords.sy;
        socket.lastZ = coords.sz;

        // 2. Envia 10001 (Spawn/Update do Herói com a nova classe/habilidades)
        activeSockets.forEach(s => {
            if (s.socketType === "SCENE" && !s.destroyed) {
                s.write(getPlayerSpawnPacket(socket, s));
            }
        });

        // 3. Destrava tudo (90008)
        const p20014 = encodeInt(20014);
        const s90008 = encodeInt(90008);
        const one = encodeInt(1);
        socket.write(`${prefix}:SendMsgToClient,(${p20014},(${s90008},(),${one}))\n`);
        
        socket.isInvincible = socket.godMode || false;

        // Limpa todos os buffs e timers ativos para evitar que velocidade/efeitos persistam no novo herói
        const playerId = socket.playerId || "1";
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

        if (socket.invisibilityTimer) {
            clearTimeout(socket.invisibilityTimer);
            socket.invisibilityTimer = null;
        }
        if (socket.ultimateTimer) {
            clearTimeout(socket.ultimateTimer);
            socket.ultimateTimer = null;
        }
        console.log(`[🎭 SCENE HERO CHANGE] Cleared all active buff timers and sent DeleteBuff packets dynamically for player ${playerId}`);

        // Spawn/Atualização de NPCs na Arena de Treino se o mapa for bachang_baxi
        const currentMapId = getSafeMapId(socket.targetMapId || lastMapId || 5202);
        const currentMapName = socket.targetMapName || lastMapName || 'bachang_baxi';
        if (currentMapId === 5202 || currentMapName === 'bachang_baxi') {
            spawnPracticeNpcs(socket, true);
        }
    } catch (e) {
        console.error("[⚠️ ERRO em switchPlayerHero]:", e);
    }
}

// ==========================================
// 📖 CARREGADOR DINÂMICO DE HABILIDADES (heroinfo.xml)
// ==========================================
const heroSkillsMap = {};
try {
    const xmlContent = fs.readFileSync('e:\\server tr\\public\\baxi\\heroinfo.xml', 'utf8');
    const matches = xmlContent.match(/<i\s+id="(\d+)"[^>]*showskill="([^"]*)"/g);
    if (matches) {
        for (const m of matches) {
            const idMatch = m.match(/id="(\d+)"/);
            const skillMatch = m.match(/showskill="([^"]*)"/);
            if (idMatch && skillMatch) {
                const heroId = parseInt(idMatch[1]);
                const skillStr = skillMatch[1].replace(/\|\|/g, '').replace(/\|/g, '');
                const skills = skillStr.split(',').map(s => s.trim()).filter(s => s && !isNaN(s)).map(s => parseInt(s));
                heroSkillsMap[heroId] = skills;
            }
        }
    }
    console.log(`[🎮 SKILLS DB] Inicializado com sucesso! ${Object.keys(heroSkillsMap).length} heróis mapeados.`);
} catch (e) {
    console.error('[⚠️ ERRO AO CARREGAR HEROINFO.XML EM SERVER.JS]:', e);
}

// ==========================================
// 📖 CARREGADOR DINÂMICO DE ULTIMATES (dazhao.xml)
// ==========================================
const heroDazhaoMap = {};
try {
    const dzContent = fs.readFileSync('e:\\server tr\\public\\baxi\\dazhao.xml', 'utf8');
    const blocks = dzContent.match(/<i\s+id="(\d+)"[^>]*>([\s\S]*?)<\/i>/g);
    if (blocks) {
        const getAttr = (tag, attr) => {
            const m = tag.match(new RegExp(`${attr}=\"([^\"]+)\"`));
            return m ? m[1] : null;
        };
        blocks.forEach(b => {
            const idMatch = b.match(/<i\s+id="(\d+)"/);
            if (idMatch) {
                const heroId = parseInt(idMatch[1]);
                const skills = [];
                const skillMatches = b.match(/<skill\s+[^>]+\/>/g);
                if (skillMatches) {
                    skillMatches.forEach(sm => {
                        const sId = getAttr(sm, 'id');
                        const sMax = getAttr(sm, 'energymax');
                        const sCont = getAttr(sm, 'continuestime');
                        const sDoSkill = getAttr(sm, 'doskill');
                        if (sId && sMax) {
                            skills.push({
                                id: parseInt(sId),
                                maxEnergy: parseInt(sMax),
                                continueTime: sCont ? parseInt(sCont) : 0,
                                doskill: sDoSkill ? parseInt(sDoSkill) : 0
                            });
                        }
                    });
                }
                heroDazhaoMap[heroId] = skills;
            }
        });
    }
    console.log(`[🚀 DAZHAO DB] Inicializado com sucesso! ${Object.keys(heroDazhaoMap).length} heróis mapeados.`);
} catch (e) {
    console.error('[⚠️ ERRO AO CARREGAR DAZHAO.XML EM SERVER.JS]:', e);
}

// ==========================================
// 📖 CARREGADOR DINÂMICO DE PLAYERSKILL (playerskill.xml)
// ==========================================
const playerSkillsMap = {};
try {
    const psContent = fs.readFileSync('e:\\server tr\\public\\baxi\\playerskill.xml', 'utf8');
    const blocks = psContent.match(/<i\s+id="(\d+)"[^>]*\/>/g);
    if (blocks) {
        const getAttr = (tag, attr) => {
            const m = tag.match(new RegExp(`${attr}=\"([^\"]*)\"`));
            return m ? m[1] : null;
        };
        blocks.forEach(b => {
            const idMatch = b.match(/<i\s+id="(\d+)"/);
            if (idMatch) {
                const skillId = parseInt(idMatch[1]);
                const cd = getAttr(b, 'cd');
                const longtime = getAttr(b, 'longtime');
                const buffMe = getAttr(b, 'buff_idx_me');
                const buffTarget = getAttr(b, 'buff_idx');
                playerSkillsMap[skillId] = {
                    cd: cd ? parseInt(cd) : 0,
                    longtime: longtime ? parseInt(longtime) : 0,
                    buff_idx_me: (buffMe && buffMe !== "0") ? buffMe.split('|').map(x => parseInt(x)) : [],
                    buff_idx: (buffTarget && buffTarget !== "0") ? buffTarget.split('|').map(x => parseInt(x)) : []
                };
            }
        });
    }
    console.log(`[🔋 PLAYERSKILL DB] Inicializado com sucesso! ${Object.keys(playerSkillsMap).length} habilidades mapeadas.`);
} catch (e) {
    console.error('[⚠️ ERRO AO CARREGAR PLAYERSKILL.XML EM SERVER.JS]:', e);
}

// ==========================================
// 📖 CARREGADOR DINÂMICO DE BUFFS (buff.xml)
// ==========================================
const buffDurationsMap = {};
const buffsMap = {};
try {
    const buffContent = fs.readFileSync('e:\\server tr\\public\\baxi\\buff.xml', 'utf8');
    const blocks = buffContent.match(/<i\s+id="(\d+)"[^>]*\/>/g);
    if (blocks) {
        const getAttr = (tag, attr) => {
            const m = tag.match(new RegExp(`${attr}=\\s*\"([^\"]*)\"`));
            return m ? m[1] : null;
        };
        blocks.forEach(b => {
            const idMatch = b.match(/<i\s+id="(\d+)"/);
            if (idMatch) {
                const buffId = parseInt(idMatch[1]);
                const time = getAttr(b, 'time');
                const name = getAttr(b, 'name') || '';
                const type = getAttr(b, 'type');
                const value = getAttr(b, 'value');
                const interval = getAttr(b, 'interval');
                const isdebuff = getAttr(b, 'isdebuff');

                buffDurationsMap[buffId] = time ? parseInt(time) : 0;
                buffsMap[buffId] = {
                    id: buffId,
                    name: name,
                    type: type ? parseInt(type) : 0,
                    time: time ? parseInt(time) : 0,
                    value: value ? parseInt(value) : 0,
                    interval: interval ? parseInt(interval) : 0,
                    isdebuff: isdebuff ? parseInt(isdebuff) : 0
                };
            }
        });
    }
    console.log(`[🛡️ BUFFS DB] Inicializado com sucesso! ${Object.keys(buffsMap).length} buffs mapeados no buffsMap.`);
} catch (e) {
    console.error('[⚠️ ERRO AO CARREGAR BUFF.XML EM SERVER.JS]:', e);
}

// ==========================================
// 📖 CARREGADOR DINÂMICO DE ARMAS (weapon.xml)
// ==========================================
const weaponsMap = {};
try {
    const weaponContent = fs.readFileSync('e:\\server tr\\public\\baxi\\weapon.xml', 'utf8');
    
    // Parseia blocos <i> para capturar as IDs parentes e seus respectivos <weapon> e <launcher>
    const iBlockRegex = /<i\s+id="(\d+)"[^>]*>([\s\S]*?)<\/i>/g;
    let match;
    let totalWeaponsLoaded = 0;
    
    while ((match = iBlockRegex.exec(weaponContent)) !== null) {
        const parentId = parseInt(match[1]);
        const blockContent = match[2];
        
        const cartridgeMatch = match[0].match(/cartridgepublic="([^"]*)"/);
        const cartridgePublic = cartridgeMatch ? parseInt(cartridgeMatch[1]) : 30;
        
        weaponsMap[parentId] = {
            cartridgePublic: cartridgePublic
        };
        
        const weaponRegex = /<weapon\s+shootname="([^"]*)"[^>]*>([\s\S]*?)<\/weapon>/g;
        let weaponMatch;
        
        while ((weaponMatch = weaponRegex.exec(blockContent)) !== null) {
            const shootname = weaponMatch[1];
            const weaponBlock = weaponMatch[2];
            
            const launcherRegex = /<launcher\s+[^>]*>/g;
            const launcherMatch = launcherRegex.exec(weaponBlock);
            if (launcherMatch) {
                const launcherTag = launcherMatch[0];
                
                const getAttr = (tag, attr) => {
                    const m = tag.match(new RegExp(`${attr}=\\s*\"([^\"]*)\"`));
                    return m ? m[1] : null;
                };
                
                const damageEnemy = getAttr(launcherTag, 'damageparamenemy');
                const damageHead = getAttr(launcherTag, 'damagehead');
                const dampBegin = getAttr(launcherTag, 'damagedampdisbegin');
                const dampEnd = getAttr(launcherTag, 'damagedampdisend');
                const dampParm = getAttr(launcherTag, 'damagedampparm');
                const boomType = getAttr(launcherTag, 'boomdamagetype');
                
                let boomRange = 0;
                const bulletRegex = /<bullet\s+[^>]*>/g;
                const bulletMatch = bulletRegex.exec(weaponBlock);
                if (bulletMatch) {
                    const bulletTag = bulletMatch[0];
                    const bulletRangeAttr = getAttr(bulletTag, 'bulletboomrange');
                    if (bulletRangeAttr) {
                        boomRange = parseInt(bulletRangeAttr);
                    }
                }
                
                weaponsMap[parentId][shootname] = {
                    damage: damageEnemy ? parseInt(damageEnemy) : 0,
                    damageHead: damageHead ? parseInt(damageHead) : 0,
                    dampBegin: dampBegin ? parseInt(dampBegin) : 0,
                    dampEnd: dampEnd ? parseInt(dampEnd) : 0,
                    dampParm: dampParm ? parseInt(dampParm) : 0,
                    boomType: boomType ? parseInt(boomType) : 0,
                    boomRange: boomRange
                };
                totalWeaponsLoaded++;
            }
        }
    }
    
    // Sobrescritas manuais para equilibrar armas e ultimates (Brigit 1023 "4" ultimate tick damage)
    if (!weaponsMap[1023]) weaponsMap[1023] = {};
    weaponsMap[1023]["4"] = { damage: 180, damageHead: 0 };
    
    console.log(`[🔫 WEAPONS DB] Inicializado com sucesso! ${Object.keys(weaponsMap).length} heróis/blocos e ${totalWeaponsLoaded} sub-armas mapeadas. (Brigit Ult Overridden)`);
} catch (e) {
    console.error('[⚠️ ERRO AO CARREGAR WEAPON.XML EM SERVER.JS]:', e);
}

// Sobrescritas manuais para habilidades de grupo que não têm buffs mapeados nos XMLs mas dão buffs em aliados/caster
const groupSkillsOverrides = {
    1153: { // Daji/Venus - Fox Phantom (Group Shield)
        buff_idx_me: [1047], // Fox Pantom (shield)
        buff_idx: [1047]
    },
    1122: { // Angel/Luolita - Sylph's Wind (Group Speed)
        buff_idx_me: [1035], // Sylph's Wind (speed up)
        buff_idx: [1035]
    }
};

const getHeroMaxHp = (heroId) => {
    const hpMap = {
        1003: 1000, // Alice (Diana)
        1005: 1000, // Glider (Leizhenzi)
        1006: 2000, // Tagaloa (XingTian)
        1015: 1000, // Lars / Terminator
        1016: 1250, // Valkyries
        1017: 1000, // Celina / Mulan
        1019: 1800, // Guanyin
        1021: 1000, // Carmen / Taliya
        1022: 1250, // Scarlett / Xuenv
        1023: 1250, // Brigit / Zhurong
        1024: 1000, // Shogun / Houyi
        1025: 1000, // Eva / Linglong
        1026: 1000, // Dragos / Edward
        1027: 1500, // Jack / Yinglong
        1028: 1750, // Loki / Lokji
        1029: 1000, // Angel / Lolita
        1030: 1000, // Vincent / Jingke
        1031: 1000, // Johnny / Lanbo
        1032: 1000, // Venus / Daji
        1033: 1000, // Lily / Lilith
        1034: 2200, // Khan / Huangzhong
        1035: 1250, // Nezha
        1036: 1000, // Gaya / Medusa
        1037: 2500  // David
    };
    return hpMap[heroId] || 2000; // Default fallback to 2000 HP
};

const generateHeroDazhaoData = (heroId) => {
    const list = heroDazhaoMap[heroId] || [];
    if (list.length === 0) {
        return '()';
    }
    const elements = list.map(dz => `(${encodeInt(dz.id)},${encodeInt(dz.maxEnergy)})`);
    return `(0,(${elements.join(',')}))`;
};

const generateHeroSkillsData = (heroId) => {
    const one = encodeInt(1);
    const skills = heroSkillsMap[heroId] || [];
    const skillList = [];
    for (const skId of skills) {
        skillList.push(`(${encodeInt(skId)},0,${one},0,${one},0,0,0,0,0,0)`);
    }
    return `(${skillList.join(',')})`;
};

// Helper para spawnar bots de treino (alvos)
function spawnBot(socket, botId, heroId, name, sx, sy, sz, force, lastMapId) {
    try {
        const botIdEnc = encodeInt(botId);
        const heroIdEnc = encodeInt(heroId);
        const forceEnc = encodeInt(force);
        const one = encodeInt(1);
        const zero = encodeInt(0);
        const nameEnc = encodeHex(name);
        const heroInfo = generateFullHeroData(heroId).split(',').slice(1).join(',');

        const baseInfo = `(${botIdEnc},'${nameEnc}',${forceEnc},${heroIdEnc},${zero},${zero},${one},${one},${one},${zero},${zero},(${heroIdEnc},${heroInfo}),${zero},(),${one},${one})`;
        const battleAttr = `(${one},${encodeInt(1500)},${encodeInt(1500)},${one},${one},${one},${one},${one},${one},${encodeInt(10000)},${one},${one},${one},${one},${one},${one},${one},${one},${one},${one})`;
        const scnInfo = `(${encodeInt(lastMapId || 5202)},${encodeInt(sx)},${encodeInt(sz)},${encodeInt(sy)},${zero},${zero},${zero},${encodeInt(180)})`;
        
        const msg_part1 = `(${baseInfo},${battleAttr},${scnInfo},(),(),('Local',${one},${one},${one},${one},${zero},'',${one}),(),())`;
        
        const arySkillServerData = generateHeroSkillsData(heroId);
        const heroDazhaoData = generateHeroDazhaoData(heroId);
        const maxAmmo = weaponsMap[heroId] && weaponsMap[heroId].cartridgePublic !== undefined ? weaponsMap[heroId].cartridgePublic : 30;
        const superData = (heroId === 1005) ? `(${encodeInt(force)},'',${zero},(${encodeInt(7000)}))` : `()`;
        const msg_part2 = `(${one},${zero},((0),(),(${encodeInt(maxAmmo)})),${heroIdEnc},${heroDazhaoData},${arySkillServerData},${superData},())`;

        const spawnPacket = `e${botId}:SendMsgToClient,(${encodeInt(10001)},(${msg_part1},${msg_part2}))\n// `;
        socket.write(spawnPacket);
        console.log(`[🤖 BOT SPAWNED] Spawned Bot ${name} (NetID: ${botId}, HeroID: ${heroId})`);
    } catch (e) {
        console.error(`[⚠️ ERRO ao spawnar bot ${name}]:`, e);
    }
}

let inMatch = false;
let currentRoom = null; // Estado da sala personalizada

// ==========================================
// 📦 MOCK DB: Simulação do shopdata.xml e iteminfo.xml
// ==========================================
const GAME_DB = {
    shop: {
        // ShopID -> itemId
        1001: { itemId: 1001 },
        1002: { itemId: 1002 }, // Comprado no log
        1003: { itemId: 1003 },
        1007: { itemId: 1007 }, // Comprado no log
        3505: { itemId: 4505 }  // Comprado no log
    },
    items: {
        // bufftype: 1 = Player XP/Moeda, 2 = Hero XP
        1001: { name: "XP Dupla - Jogador (7 dias)", goldPrice: 300, diamondPrice: 150, type: 2, bufftype: 1, buffbeishu: 200 },
        1002: { name: "XP Dupla - Herói (7 dias)", goldPrice: 300, diamondPrice: 150, type: 2, bufftype: 2, buffbeishu: 200 },
        1003: { name: "Moedas Duplas", goldPrice: 1000, diamondPrice: 100, type: 2, bufftype: 1, buffbeishu: 200 },
        1007: { name: "Caixa Misteriosa", goldPrice: 100, diamondPrice: 10, type: 1, bufftype: 0 },
        4505: { name: "Peça de Arma", goldPrice: 10, diamondPrice: 10, type: 4, bufftype: 0 }
    }
};

// Inventário do Jogador na memória do Servidor (Para testes)
let playerInventory = {
    gold: 999999,
    diamonds: 999999,
    items: {} // Aqui guardaremos a quantidade de itens comprados. Ex: { "1001": 2 }
};

const playerEquipment = {};

// Mapeamento de Mapas (baseado em fbset.xml)
const MAP_CONFIG = {
    // 训练 / Tutorial
    5205: { name: 'xinshouyindao_3', type: 8 },
    5202: { name: 'bachang_baxi', type: 6 },
    
    // 团队死斗 (Type 7)
    5202: { name: 'bachang_baxi', type: 6 }, // Matchmaking TDM
    5204: { name: 'jiaodouchang_02_3', type: 7 }, // 王者大厅
    5010: { name: 'dz_jjc', type: 7 },           // 法老陵墓
    5024: { name: 'sidou03', type: 7 },          // 远航者号
    5210: { name: 'sidou04', type: 7 },          // 血狱工厂
    5212: { name: 'sidou05', type: 7 },          // 深井基地
    
    // 争夺模式 (Type 3)
    5030: { name: 'wudingzhengduo_baked_3_xiugai', type: 3 }, // 云中天台
    5074: { name: 'shuiba_smc_3_xiugai', type: 3 },           // 深谷水坝
    5068: { name: 'persian_3_xiugai', type: 3 },              // 沙漠王宫
    5050: { name: 'degulagubao_3_xiugai', type: 3 },          // 德古拉城堡
    5055: { name: 'fengducheng_3_xgjg', type: 3 },            // 幽冥地宫
    5208: { name: 'zhengduo_06_anse_3', type: 3 },            // 英魂祭坛
    
    // 护送模式 (Type 2)
    5063: { name: 'husong01_new_03', type: 2 },  // 荣耀之城
    5022: { name: 'husong02', type: 2 },         // 星空之城
    
    // 资源模式 (Type 5)
    5049: { name: 'xiudaoyuan_3_xiugai_huzi', type: 5 }, // 雪山修道院
    5260: { name: 'ziyuan_02_baitian', type: 5 },        // 天使庄园
    
    // 角斗模式 (Type 4)
    5056: { name: 'jiaodouchang_3_smc_low_xiugai', type: 4 }, // 勇者竞技场
    5058: { name: 'dz_jjc', type: 4 },                        // 法老陵墓
    5059: { name: 'bachang_baxi', type: 4 },             // 王者大厅
    
    // 个人死斗 (Type 11)
    5301: { name: 'wudingzhengduo_baked_3_xiugai', type: 11 },
    5302: { name: 'shuiba_smc_3_xiugai', type: 11 },
    5303: { name: 'persian_3_xiugai', type: 11 },
    5304: { name: 'degulagubao_3_xiugai', type: 11 },
    5305: { name: 'fengducheng_3_xgjg', type: 11 },
    5306: { name: 'husong01_new_03', type: 11 },
    5307: { name: 'husong02', type: 11 },
    
    // 丛林行动 / 大逃杀 (Type 10)
    5002: { name: 'csjj_dataosha', type: 10 },
    5003: { name: 'csjj_dataosha01', type: 10 },
    5004: { name: 'csjj_dataosha_gai', type: 10 },
    5005: { name: 'csjj_dataosha', type: 10 },
    5006: { name: 'csjj_dataosha', type: 10 }
};
let lastMapId = 5205;
let lastMapName = 'xinshouyindao_3';
let lastFinalMapId = 5204;
let lastFinalMapName = 'jiaodouchang_02_3';

function getRoomActivePlayersCount(room) {
    let count = 0;
    for (let i = 0; i < 10; i++) {
        if (room.players[i]) count++;
    }
    return count;
}

function buildRoomData(room) {
    const one = encodeInt(1);
    const zero = encodeInt(0);
    const dateStr = Fun.GetServerDate();
    
    // 1. Build roomInfo array
    const roomInfo = `(${encodeInt(room.mapId)},'${encodeHex(room.creatorName)}','${encodeHex(room.roomName)}','${encodeHex(room.password)}',${encodeInt(room.mapId)},${encodeInt(room.mapType)},${zero},${encodeInt(room.maxPlayers)},${zero},${zero},${zero},${encodeInt(getRoomActivePlayersCount(room))},${zero},${encodeInt(room.state)},${zero},${encodeInt(7777)},'${room.createTime}',${encodeInt(room.roomUID)},${zero})`;
    
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
            const heroIdEnc = encodeInt(heroId);
            const userDataHeroInfo = `(${heroIdEnc},${one},${one},${one},${zero})`;
            
            const isCapOpt = encodeInt(isCaptain);
            const prerogativeLobby = `(${isCapOpt},${isCapOpt},${isCapOpt},${isCapOpt},'${p.socket.playerNameEnc}',${zero},'',${zero})`;
            const uData = `('${p.socket.playerNameEnc}',${encodeInt(99)},${one},${encodeInt(force)},${encodeInt(isReady)},${one},${heroIdEnc},${one},${one},${one},${one},'Local',${zero},${userDataHeroInfo},${one},${zero},${zero},${encodeInt(isCaptain)},${prerogativeLobby},())`;
            
            userListElements.push(`'${p.socket.playerNameEnc}',${uData}`);
        } else {
            userListElements.push(`'',()`);
        }
    }
    const userList = `(${userListElements.join(',')})`;
    
    return `(${roomInfo},${userList})`;
}

function buildRoomUserList(room) {
    const one = encodeInt(1);
    const zero = encodeInt(0);
    const userListElements = [];
    for (let i = 0; i < 10; i++) {
        const p = room.players[i];
        if (p) {
            const force = i < 5 ? 1 : 2;
            const isReady = p.isReady ? 1 : 0;
            const isCaptain = p.isCaptain ? 1 : 0;
            
            // Build player's heroInfo
            const heroId = p.socket.currentHeroId || 1015;
            const heroIdEnc = encodeInt(heroId);
            const userDataHeroInfo = `(${heroIdEnc},${one},${one},${one},${zero})`;
            
            const isCapOpt = encodeInt(isCaptain);
            const prerogativeLobby = `(${isCapOpt},${isCapOpt},${isCapOpt},${isCapOpt},'${p.socket.playerNameEnc}',${zero},'',${zero})`;
            const uData = `('${p.socket.playerNameEnc}',${encodeInt(99)},${one},${encodeInt(force)},${encodeInt(isReady)},${one},${heroIdEnc},${one},${one},${one},${one},'Local',${zero},${userDataHeroInfo},${one},${zero},${zero},${encodeInt(isCaptain)},${prerogativeLobby},())`;
            
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

// ==========================================
// 🏆 GESTOR DE PARTIDA TEAM DEATHMATCH (TDM)
// ==========================================
global.activeTdmMatch = null;

function spawnTdmBases(socket, mapId) {
    const blueCoords = getSpawnCoordinates(mapId, 1);
    const redCoords = getSpawnCoordinates(mapId, 2);
    
    // Spawna os itens de base (HQ/Jidi) do TDM para o jogador usando inteiros planos decodificados
    // Tipo: 1 = Base/HQ, Escala: 300 (3 metros de raio de trigger), Força: 1 (Blue) e 2 (Red)
    const baseBlueId = 9001;
    const baseRedId = 9002;
    
    const packetBlue = `cbuffitem:${baseBlueId},("createBuffItem",${blueCoords.sx},${blueCoords.sy},${blueCoords.sz},0,'buffitemui',1,'2024/01/01 12:00:00',1,300,300,300,0,0,0,1,0)\n`;
    const packetRed = `cbuffitem:${baseRedId},("createBuffItem",${redCoords.sx},${redCoords.sy},${redCoords.sz},0,'buffitemui',1,'2024/01/01 12:00:00',1,300,300,300,0,0,0,2,0)\n`;
    
    socket.write(packetBlue);
    socket.write(packetRed);
    console.log(`[🏰 TDM BASE SPAWN] Spawnei bases Blue (9001) e Red (9002) no mapa ${mapId} para o jogador.`);
}

function startTdmMatch(socket) {
    const currentMapId = socket.targetMapId || lastMapId || 5202;
    console.log(`[🏆 TDM] startTdmMatch chamado para ${socket.playerName || 'Jogador'} no mapa ${currentMapId}`);

    if (!global.activeTdmMatch) {
        global.activeTdmMatch = {
            state: 'warmup',
            warmupSeconds: 15,
            fightSeconds: 300, // 5 minutos padrão
            targetScore: 30,   // 30 abates para vencer
            blueScore: 0,
            redScore: 0,
            players: [],
            timer: null
        };
        console.log(`[🏆 TDM] Nova sessão TDM inicializada para o mapa ${currentMapId}.`);
    }

    const match = global.activeTdmMatch;
    if (!match.players.includes(socket)) {
        match.players.push(socket);
        console.log(`[🏆 TDM] Jogador ${socket.playerName || 'Jogador'} registrado na partida TDM.`);
    }

    // Inicializa a UI do Arena_Score para o jogador imediatamente
    const p110300 = encodeInt(110300);
    const p110301 = encodeInt(110301);
    const p110307 = encodeInt(110307);
    
    // Abre a UI
    socket.write(`e${socket.playerIndex || 1}:SendMsgToClient,(${p110300},(${p110301},()))\n`);
    // Inicializa o placar
    socket.write(`e${socket.playerIndex || 1}:SendMsgToClient,(${p110300},(${p110307},(${encodeInt(match.targetScore)},${encodeInt(match.blueScore)},${encodeInt(match.redScore)})))\n`);

    // Spawna as bases Blue e Red do TDM
    spawnTdmBases(socket, currentMapId);

    // Abre as portas da base fisicamente e libera o campo de força laser se a partida já estiver em andamento (late loader)
    if (match.state === 'fight') {
        const p110303 = encodeInt(110303);
        const p1030000 = encodeInt(1030000);
        socket.write(`e${socket.playerIndex || 1}:SendMsgToClient,(${p110300},(${p110303},()))\n`);
        socket.write(`e${socket.playerIndex || 1}:SendMsgToClient,(${p1030000},(${encodeInt(1)},'close_open'))\n`);
    }

    // Inicia o timer de contagem se não estiver rodando
    if (!match.timer) {
        console.log(`[🏆 TDM] Iniciando timer de atualização periódica...`);
        match.timer = setInterval(() => {
            tickTdmMatch();
        }, 1000);
    }
}

function tickTdmMatch() {
    const match = global.activeTdmMatch;
    if (!match) return;

    const p110300 = encodeInt(110300);
    const p110305 = encodeInt(110305);
    const p110303 = encodeInt(110303);

    // Filtra e limpa conexões destruídas
    match.players = match.players.filter(p => !p.destroyed);
    if (match.players.length === 0) {
        console.log(`[🏆 TDM] Nenhum jogador na partida. Encerrando sessão TDM.`);
        if (match.timer) clearInterval(match.timer);
        global.activeTdmMatch = null;
        return;
    }

    if (match.state === 'warmup') {
        match.warmupSeconds--;
        
        // Envia contagem regressiva de preparação (isFightState = 0 -> portas fechadas)
        match.players.forEach(p => {
            p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p110300},(${p110305},(${encodeInt(match.warmupSeconds)},${encodeInt(0)},${encodeInt(0)},'')))\n`);
        });

        if (match.warmupSeconds <= 0) {
            match.state = 'fight';
            console.log(`[🏆 TDM] Luta Iniciada! Abrindo portas.`);
            
            const p1030000 = encodeInt(1030000);
            // Envia o início da luta (Fight Start) e abre as portas fisicamente para todos os jogadores
            match.players.forEach(p => {
                p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p110300},(${p110303},()))\n`);
                p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p1030000},(${encodeInt(1)},'close_open'))\n`);
            });
            
            sendGlobalSystemChat("🟢 [TDM] A PARTIDA COMEÇOU! AS PORTAS DA BASE FORAM ABERTAS!");
        }
    } else if (match.state === 'fight') {
        match.fightSeconds--;

        // Envia contagem regressiva da partida (isFightState = 1 -> portas abertas)
        match.players.forEach(p => {
            p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p110300},(${p110305},(${encodeInt(match.fightSeconds)},${encodeInt(1)},${encodeInt(0)},'')))\n`);
        });

        if (match.fightSeconds <= 0) {
            console.log(`[🏆 TDM] Tempo limite esgotado!`);
            endTdmMatch();
        }
    }
}

function incrementTdmScore(killerForce, amount = 1) {
    const match = global.activeTdmMatch;
    if (!match || match.state !== 'fight') return;

    if (killerForce === 1) {
        match.blueScore += amount;
    } else if (killerForce === 2) {
        match.redScore += amount;
    }

    console.log(`[🏆 TDM] Placar Atualizado: Blue ${match.blueScore} - Red ${match.redScore} (Alvo: ${match.targetScore})`);

    // Envia atualização de placar (110302) para todos
    const p110300 = encodeInt(110300);
    const p110302 = encodeInt(110302);
    match.players.forEach(p => {
        p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p110300},(${p110302},(${encodeInt(1)},${encodeInt(match.targetScore)},${encodeInt(match.blueScore)},${encodeInt(match.redScore)})))\n`);
    });

    // Condição de vitória por pontuação
    if (match.blueScore >= match.targetScore || match.redScore >= match.targetScore) {
        console.log(`[🏆 TDM] Equipe atingiu a pontuação máxima! Encerrando...`);
        endTdmMatch();
    }
}

function endTdmMatch() {
    const match = global.activeTdmMatch;
    if (!match || match.state === 'ended') return;

    match.state = 'ended';
    if (match.timer) {
        clearInterval(match.timer);
        match.timer = null;
    }

    let winnerForce = 0;
    if (match.blueScore > match.redScore) {
        winnerForce = 1;
        sendGlobalSystemChat("🏆 [TDM] FIM DE PARTIDA! A EQUIPE AZUL VENCEU!");
    } else if (match.redScore > match.blueScore) {
        winnerForce = 2;
        sendGlobalSystemChat("🏆 [TDM] FIM DE PARTIDA! A EQUIPE VERMELHA VENCEU!");
    } else {
        winnerForce = 0;
        sendGlobalSystemChat("🤝 [TDM] FIM DE PARTIDA! A PARTIDA TERMINOU EM EMPATE!");
    }

    // Envia o resultado da luta (110306)
    const p110300 = encodeInt(110300);
    const p110306 = encodeInt(110306);
    const p110304 = encodeInt(110304);

    match.players.forEach(p => {
        p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p110300},(${p110306},(${encodeInt(winnerForce)})))\n`);
    });

    // Aguarda 10 segundos e retorna todos ao lobby
    setTimeout(() => {
        match.players.forEach(p => {
            if (!p.destroyed) {
                // Envia o retorno ao Lobby (110304 -> GotoHall)
                p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p110300},(${p110304},()))\n`);
                p.inMatch = false;
                
                const lobbyMatch = activeSockets.find(s => s.socketType === "LOBBY" && s.remoteAddress === p.remoteAddress);
                if (lobbyMatch) {
                    lobbyMatch.inMatch = false;
                }
            }
        });
        global.activeTdmMatch = null;
        console.log(`[🏆 TDM] Partida limpa com sucesso.`);
    }, 10000);
}

function startControlMatch(socket) {
    const currentMapId = socket.targetMapId || lastMapId || 5202;
    console.log(`[🏆 CONTROL] startControlMatch chamado para ${socket.playerName || 'Jogador'} no mapa ${currentMapId}`);

    if (!global.activeControlMatch) {
        global.activeControlMatch = {
            state: 'warmup',
            warmupSeconds: 15,
            fightSeconds: 600, // 10 minutos padrão
            blueScore: 0,
            redScore: 0,
            players: [],
            timer: null
        };
        console.log(`[🏆 CONTROL] Nova sessão Control inicializada para o mapa ${currentMapId}.`);
    }

    const match = global.activeControlMatch;
    if (!match.players.includes(socket)) {
        match.players.push(socket);
        console.log(`[🏆 CONTROL] Jogador ${socket.playerName || 'Jogador'} registrado na partida Control.`);
    }

    // Inicializa a UI do Scramble para o jogador imediatamente
    const p110100 = encodeInt(110100);
    const p110101 = encodeInt(110101); // SEND_SCRAMBLE_CREATE
    const p110104 = encodeInt(110104); // SEND_SCRAMBLE_FIGHT_SCORE
    const p110106 = encodeInt(110106); // SEND_SCRAMBLE_FIGHT_PREPARE

    // 1. Abre a UI (110101)
    socket.write(`e${socket.playerIndex || 1}:SendMsgToClient,(${p110100},(${p110101},()))\n`);
    // 2. Coloca em modo de Preparação (110106)
    socket.write(`e${socket.playerIndex || 1}:SendMsgToClient,(${p110100},(${p110106},()))\n`);
    // 3. Inicializa o placar (110104)
    socket.write(`e${socket.playerIndex || 1}:SendMsgToClient,(${p110100},(${p110104},(${encodeInt(match.blueScore)},${encodeInt(match.redScore)})))\n`);

    // Spawna as bases do Control (podemos reutilizar spawnTdmBases ou criar similar)
    spawnTdmBases(socket, currentMapId);

    // Se a partida já começou (late loader), abre portas e avisa o cliente
    if (match.state === 'fight') {
        const p110107 = encodeInt(110107); // SEND_SCRAMBLE_FIGHT_START
        const p1030000 = encodeInt(1030000);
        socket.write(`e${socket.playerIndex || 1}:SendMsgToClient,(${p110100},(${p110107},()))\n`);
        socket.write(`e${socket.playerIndex || 1}:SendMsgToClient,(${p1030000},(${encodeInt(1)},'close_open'))\n`);
    }

    // Inicia o timer de contagem se não estiver rodando
    if (!match.timer) {
        console.log(`[🏆 CONTROL] Iniciando timer de atualização periódica...`);
        match.timer = setInterval(() => {
            tickControlMatch();
        }, 1000);
    }
}

function tickControlMatch() {
    const match = global.activeControlMatch;
    if (!match) return;

    const p110100 = encodeInt(110100);
    const p110102 = encodeInt(110102); // COUNTDOWN

    // Filtra e limpa conexões destruídas
    match.players = match.players.filter(p => !p.destroyed);
    if (match.players.length === 0) {
        console.log(`[🏆 CONTROL] Nenhum jogador na partida. Encerrando sessão Control.`);
        if (match.timer) clearInterval(match.timer);
        global.activeControlMatch = null;
        return;
    }

    if (match.state === 'warmup') {
        match.warmupSeconds--;
        
        // Envia contagem regressiva de preparação (isFightState = 0)
        match.players.forEach(p => {
            p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p110100},(${p110102},(${encodeInt(match.warmupSeconds)},${encodeInt(0)},${encodeInt(0)},'')))\n`);
        });

        if (match.warmupSeconds <= 0) {
            match.state = 'fight';
            console.log(`[🏆 CONTROL] Luta Iniciada! Abrindo portas.`);
            
            const p110107 = encodeInt(110107); // FIGHT_START
            const p1030000 = encodeInt(1030000);
            match.players.forEach(p => {
                p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p110100},(${p110107},()))\n`);
                p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p1030000},(${encodeInt(1)},'close_open'))\n`);
            });
            
            sendGlobalSystemChat("🟢 [CONTROL] A PARTIDA COMEÇOU! AS PORTAS DA BASE FORAM ABERTAS!");
        }
    } else if (match.state === 'fight') {
        match.fightSeconds--;

        // Envia contagem regressiva da partida (isFightState = 1)
        match.players.forEach(p => {
            p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p110100},(${p110102},(${encodeInt(match.fightSeconds)},${encodeInt(1)},${encodeInt(0)},'')))\n`);
        });

        if (match.fightSeconds <= 0) {
            console.log(`[🏆 CONTROL] Tempo limite esgotado!`);
            endControlMatch();
        }
    }
}

function endControlMatch() {
    const match = global.activeControlMatch;
    if (!match || match.state === 'ended') return;

    match.state = 'ended';
    if (match.timer) {
        clearInterval(match.timer);
        match.timer = null;
    }

    let winnerForce = 0;
    if (match.blueScore > match.redScore) {
        winnerForce = 1;
        sendGlobalSystemChat("🏆 [CONTROL] FIM DE PARTIDA! A EQUIPE AZUL VENCEU!");
    } else if (match.redScore > match.blueScore) {
        winnerForce = 2;
        sendGlobalSystemChat("🏆 [CONTROL] FIM DE PARTIDA! A EQUIPE VERMELHA VENCEU!");
    } else {
        winnerForce = 0;
        sendGlobalSystemChat("🤝 [CONTROL] FIM DE PARTIDA! A PARTIDA TERMINOU EM EMPATE!");
    }

    // Envia o resultado da luta (110108 - RESULT)
    const p110100 = encodeInt(110100);
    const p110108 = encodeInt(110108);
    const p110109 = encodeInt(110109); // END

    match.players.forEach(p => {
        p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p110100},(${p110108},(${encodeInt(winnerForce)})))\n`);
    });

    // Aguarda 10 segundos e retorna todos ao lobby
    setTimeout(() => {
        match.players.forEach(p => {
            if (!p.destroyed) {
                // Envia o fim da partida / retorno ao lobby (110109 - END)
                p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p110100},(${p110109},()))\n`);
                p.inMatch = false;
                
                const lobbyMatch = activeSockets.find(s => s.socketType === "LOBBY" && s.remoteAddress === p.remoteAddress);
                if (lobbyMatch) {
                    lobbyMatch.inMatch = false;
                }
            }
        });
        global.activeControlMatch = null;
        console.log(`[🏆 CONTROL] Partida limpa com sucesso.`);
    }, 10000);
}

function getPlayerNameFromNgol(texto) {
    if (!texto) return null;
    const parts = texto.split(",");
    if (parts.length >= 5) {
        let namePart = parts[4].trim();
        // Remove single quotes, double quotes, parentheses, newlines
        namePart = namePart.replace(/['"\(\)\r\n]/g, "");
        
        // Se parecer hexadecimal (apenas caracteres 0-9 a-f A-F), decodificamos
        if (/^[0-9a-fA-F]+$/.test(namePart) && namePart.length % 2 === 0) {
            try {
                const dec = decodeHex(namePart);
                if (dec) {
                    return dec;
                }
            } catch(e) {}
        }
        return namePart;
    }
    return null;
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
    socket.playerNameEnc = encodeHex(socket.playerName);
    socket.playerIndex = ipIndexes[ip];
    socket.playerId = String(socket.playerIndex);

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
            
            let extractedName = getPlayerNameFromNgol(textoRecebido);
            if (extractedName) {
                socket.playerName = extractedName;
                socket.playerNameEnc = encodeHex(extractedName);
                
                // Pré-popula o playerIndex e playerId a partir do nome único "Jogador_X"
                const idxMatch = extractedName.match(/\d+/);
                const parsedIdx = idxMatch ? parseInt(idxMatch[0]) : 1;
                socket.playerIndex = parsedIdx;
                socket.playerId = String(parsedIdx);
                
                lobbyMatch = activeSockets.find(s => s.socketType === "LOBBY" && s.playerName === extractedName && !s.destroyed);
                
                if (playerSessions[extractedName]) {
                    syncSocketFromSession(socket);
                    if (lobbyMatch) {
                        socket.lobbySocket = lobbyMatch;
                        lastLobbySocket = lobbyMatch;
                    }
                    console.log(`[🌐 SCENE] Sessão persistente recuperada com sucesso para: ${extractedName}`);
                } else if (lobbyMatch) {
                    socket.playerId = lobbyMatch.playerId || String(lobbyMatch.playerIndex || 1);
                    socket.playerIndex = lobbyMatch.playerIndex || 1;
                    socket.force = lobbyMatch.force !== undefined ? lobbyMatch.force : 1;
                    socket.currentHeroId = lobbyMatch.currentHeroId || 1015;
                    socket.targetMapId = lobbyMatch.targetMapId || 5202;
                    socket.targetMapName = lobbyMatch.targetMapName || 'bachang_baxi';
                    socket.finalMapId = lobbyMatch.finalMapId;
                    socket.lobbySocket = lobbyMatch;
                    lastLobbySocket = lobbyMatch;
                    
                    playerSessions[extractedName] = {
                        playerIndex: socket.playerIndex,
                        playerId: socket.playerId,
                        playerName: extractedName,
                        force: socket.force,
                        targetMapId: socket.targetMapId,
                        targetMapName: socket.targetMapName,
                        currentHeroId: socket.currentHeroId,
                        finalMapId: socket.finalMapId
                    };
                    console.log(`[🌐 SCENE] Mapeamento via LOBBY ativo bem-sucedido! Nome: ${socket.playerName}, PlayerID: ${socket.playerId}, Force: ${socket.force}`);
                }
            }
            
            if (!socket.playerIndex) {
                lobbyMatch = activeSockets.find(s => s.socketType === "LOBBY" && s.remoteAddress === socket.remoteAddress && !s.destroyed);
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
                    lastLobbySocket = lobbyMatch;
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

        logPacket(socketType, 'IN', textoRecebido);

        // --- CENA 1: TENTATIVA DE LOGIN ---
        if (socketType === "LOGIN" && textoRecebido.includes("NGOL")) {
            console.log(`[🔐 LOGIN] Autenticando...`);
            socket.isEncrypted = true;
            
            const ip = socket.remoteAddress;
            global.totalLogins = (global.totalLogins || 0) + 1;
            const resolvedName = `Jogador_${global.totalLogins}`;
            ipNames[ip] = resolvedName; // Mantém compatibilidade
            ipIndexes[ip] = global.totalLogins; // Mantém compatibilidade
            console.log(`[🔐 LOGIN] IP: ${ip} associado a Conexão Sequencial Única: ${resolvedName}`);
            
            const portEncoded = encodeInt(7777);
            socket.write(`cLoginUser:1,('OnLoginOK',1,1,'${resolvedName}',('${SERVER_IP}',${portEncoded},'HallGroup'))\n`);
        }

        // --- CENA 2: ENTRANDO NO SALÃO (LOBBY) ---
        else if (socketType === "LOBBY" && textoRecebido.includes("NGOL")) {
            console.log(`[🏰 LOBBY] Carregando a tela principal...`);
            const extractedName = getPlayerNameFromNgol(textoRecebido);
            if (extractedName) {
                socket.playerName = extractedName;
                socket.playerNameEnc = encodeHex(extractedName);
                const idxMatch = extractedName.match(/\d+/);
                socket.playerIndex = idxMatch ? parseInt(idxMatch[0]) : 1;
                socket.playerId = String(socket.playerIndex);
                console.log(`[🏰 LOBBY] Identificado com sucesso pelo pacote: ${socket.playerName} (Index: ${socket.playerIndex})`);
            }
            socket.isEncrypted = true;
            socket.write(`mHallUser:1\n`);

            // Fila de pacotes a enviar, cada um com seu delay
            const sendQueue = async () => {
                console.log(`[📤 LOBBY] Injetando dados do jogador...`);

                // ══════════════════════════════════════
                // PACOTE 1: Dados do Jogador (1301)
                // ══════════════════════════════════════
                const arr0 = `(1,'${socket.playerNameEnc}',1,1,1,1,${encodeInt(99)},${encodeInt(9999)},1,0,0,0,0,0,'2024/01/01 12:00:00',0,0,0,0,0,0,0,0,-1,(),0,0,0,0,0,0)`;
                // Moedas: Gold, Gold_Bind, Gems, Gems_Bind, VIP, VIP_Gold, SkinChip, EmoteChip, MatchMoney...
                const wealth = encodeInt(999999);
                const arr1 = `(${wealth},${wealth},${wealth},0,${encodeInt(10)},0,${wealth},${wealth},${wealth},0,${wealth},0,0,0)`;
                const arr6 = `((),(),())`;
                const arr7 = `(0,1,0,(0))`;
                const arr8 = `((),'',0,${encodeInt(100)},(),(),(),())`;
                const arr9 = `(0,0,0,0,0,0,0,0,0,0)`;
                const arr13 = `('','','','','','','','','','','','','','','')`;
                const arr15 = `(0,0,0,0,0,0,0,0,0,0,0,0,0,0,())`;
                const arr16 = `()`;
                const szMsg = `(${arr0},${arr1},(),(),(),(),${arr6},${arr7},${arr8},${arr9},(),(),(),${arr13},(),${arr15},${arr16},(),(),())`;
                await sendPacket(socket, `e1:SendMsgToHallClient,(KL,${szMsg})\n`, 1000);

                // Sincronização de tempo inicial
                const serverTime = Fun.GetServerDate();
                const zero = encodeInt(0);
                await sendPacket(socket, `e1:SendMsgToHallClient,(${encodeInt(20058)},('${serverTime}',${zero}))\n`, 200);

                // ══════════════════════════════════════
                // PACOTE 2: Configurações de Áudio (3010001)
                // ══════════════════════════════════════
                const porta3010001 = encodeInt(3010001);
                await sendPacket(socket, `e1:SendMsgToHallClient,(${porta3010001},('','','','',''))\n`, 200);

                // ══════════════════════════════════════
                // PACOTE 3: Lista de Heróis (1306 → 12002) - UNLOCK ALL
                // ══════════════════════════════════════
                const porta1306 = encodeInt(1306);
                const respostaHerois = encodeInt(12002);

                // ADICIONADOS: 1029 (Luolita/Angel) e 1030 (Jingke/Vincent)
                const heroIds = [1003, 1005, 1006, 1015, 1016, 1017, 1021, 1022, 1023, 1024, 1025, 1026, 1027, 1028, 1029, 1030, 1031, 1032, 1033, 1034, 1035, 1036, 1037, 2000];

                // A CHAVE MESTRA: Como não sabemos a posição exata do LockState (se é o 1º, 2º ou 3º int),
                // definimos todos os três como '2' (Permanente). Isso quebra o cadeado de qualquer item!
                const skinVal = `(2,2,2,'2099/01/01 12:00:00',2,2,2,2)`;

                // ARMA: Mantemos os três '2' no início, e cravamos o () na posição 6 para as peças de arma não darem crash!
                const wpVal = `(2,2,2,'2099/01/01 12:00:00',2,2,(),2,2)`;

                const emVal = `(2,2,2,'2099/01/01 12:00:00',2,2,2,2)`;

                // Força Bruta: Cria a permissão para os itens do ID 1 ao 10 para todos os heróis
                let skinUnlockArr = [];
                let weaponUnlockArr = [];
                let emoteUnlockArr = [];

                for (let i = 1; i <= 10; i++) {
                    skinUnlockArr.push(`${i},${skinVal}`);
                    weaponUnlockArr.push(`${i},${wpVal}`);
                    emoteUnlockArr.push(`${i},${emVal}`);
                }

                // Desbloqueia também a skin, arma e emote correspondentes a cada ID de herói
                for (const hId of heroIds) {
                    skinUnlockArr.push(`${hId},${skinVal}`);
                    weaponUnlockArr.push(`${hId},${wpVal}`);
                    emoteUnlockArr.push(`${hId},${emVal}`);
                }

                // Junta tudo nas listas finais
                const skinUnlock = `(${skinUnlockArr.join(',')})`;
                const weaponUnlock = `(${weaponUnlockArr.join(',')})`;
                const emoteUnlock = `(${emoteUnlockArr.join(',')})`;

                // Pose continua vazio () porque quase nenhum herói tem pose no XML, forçar isso dá tela preta.
                const poseUnlock = `()`;

                const heroDataArray = heroIds.map(id => {
                    const encId = encodeInt(id);

                    if (!playerEquipment[id]) {
                        playerEquipment[id] = { skin: 1, weapon: 1, emote: 1, pose: 0 };
                    }
                    const eq = playerEquipment[id];

                    // Usa as variáveis eq.skin, eq.weapon no final da string!
                    return `${encId},(1,0,2,'2099/01/01 12:00:00',0,(),${skinUnlock},${weaponUnlock},${emoteUnlock},${poseUnlock},0,${eq.skin},${eq.weapon},${eq.emote},${eq.pose},0)`;
                }).join(',');

                // Envia a lista colossal para o Unity
                socket.write(`e1:SendMsgToHallClient,(${porta1306},(${respostaHerois},(${heroDataArray})))\n`);

                // PACOTE 4: FCM Anti-Vício (1307 → 13001)
                // ══════════════════════════════════════
                const porta1307 = encodeInt(1307);
                const fcmSubCmd = encodeInt(13001);
                await sendPacket(socket, `e1:SendMsgToHallClient,(${porta1307},(${fcmSubCmd},(0,0,0)))\n`, 200);

                const porta1313 = encodeInt(1313);
                const tutorialDone = encodeInt(1);
                await sendPacket(socket, `e1:SendMsgToHallClient,(${porta1313},(${tutorialDone}))\n`, 200);

                // ==========================================
                // PACOTE 5: Atividades (16100 → 16101)
                // ==========================================
                const p16100 = encodeInt(16100);
                // Usa data segura que o C# consegue fazer DateTime.Parse na cultura pt-BR
                const dateVal = `'${Fun.GetServerDate()}'`;
                const dateEnd = `'2099/01/01 12:00:00'`;
                const buildActivityData = (actId, actType, xmlId) => {
                    const fields = [
                        encodeInt(actId),
                        encodeInt(0),
                        encodeInt(0),
                        encodeInt(0),
                        encodeInt(0),
                        encodeInt(actType),
                        '()',
                        '()',
                        encodeInt(0),
                        dateVal,
                        dateEnd,
                        encodeInt(0),
                        encodeInt(0),
                        `'LimitAct'`,
                        encodeInt(xmlId),
                        encodeInt(1)
                    ];
                    return `(${fields.join(',')})`;
                };
                const act1 = buildActivityData(1, 41, 101);
                const act2 = buildActivityData(2, 42, 102);
                const arrayList = `(${act1},${act2})`;
                await sendPacket(socket, `e1:SendMsgToHallClient,(${p16100},(${p16100},(${arrayList},${dateVal},${dateEnd})))\n`, 200);

                console.log(`[✅ LOBBY] Todos os pacotes de inicialização enviados!`);
            };
            sendQueue().catch(err => console.error('[❌ ERRO no envio de pacotes]', err));
        }

        // --- CENA 3: JOGADOR CLICANDO EM BOTÕES ---
        else if (socketType === "LOBBY" && textoRecebido.includes("SendMsgToHallServer")) {
            const match = textoRecebido.match(/SendMsgToHallServer,\(([^,]+),(.+)\)\s*$/);
            if (match) {
                const cmdId = decodeInt(match[1]); // Traduz a letra para número
                const args = match[2]; // Dados brutos do comando

                // ==========================================
                // 🎮 MATCHMAKING TDM MULTIPLAYER (60100)
                // ==========================================
                
                // ==========================================
                // 🕹️ QUICK MATCH (80001) E MATCHMAKING UI
                // ==========================================
                if (cmdId === 80001) {
                    const subCmd = parseArrayList(args);
                    const subId = subCmd[0];
                    
                    if (subId === 80012) {
                        // 📊 O cliente quer saber quantos players estao nas filas!
                        // Resposta: (80012, (fbType, count, fbType, count...))
                        const fakeCounts = "(1,2543,2,1432,4,890)"; // 1=TDM, 2=Ranked, 4=DTS
                        socket.write(`e1:SendMsgToHallClient,(${encodeInt(80001)},(${encodeInt(80012)},${fakeCounts}))\n`);
                        console.log(`[📊 MATCHMAKING] Atualizando numeros de jogadores nas filas para o cliente!`);
                    }
                    else if (subId === 80002) {
                        // 🎯 O cliente clicou em uma Partida Rapida e quer entrar na fila!
                        const fbType = subCmd[1]; // 1, 2 ou 4
                        const playerName = subCmd[2];
                        const mapId = 5204;
                        const mapIdEnc = encodeInt(mapId);
                        const mapConf = MAP_CONFIG[mapId];
                        const mapTypeEnc = encodeInt(mapConf.type);
                        const startTime = Fun.GetServerDate();
                        const zero = encodeInt(0);
                        const one = encodeInt(1);

                        socket.targetMapId = mapId;
                        socket.targetMapName = mapConf.name;
                        socket.inMatch = true;
                        socket.force = 1;
                        socket.playerIndex = 1;
                        socket.playerId = "1";
                        lastMapId = mapId;
                        lastMapName = mapConf.name;
                        lastFinalMapId = 5204;
                        lastFinalMapName = MAP_CONFIG[5204].name;
                        
                        console.log(`[🎯 MATCHMAKING] Jogador ${playerName} entrou na fila do modo ${fbType}!`);
                        
                        // 1. Enviar pacote 80006: "Procurando Partida..."
                        // Formato arrayList: (0, yjTime, div, matchNum, nowNum, maxNum, allModelNum, 0, fbType)
                        const searchArr = `(0,60,1,0,1,10,(),0,${fbType})`;
                        socket.write(`e1:SendMsgToHallClient,(${encodeInt(80001)},(${encodeInt(80006)},(1,${searchArr})))\n`);
                        
                        // 2. Esperar 3 segundos para simular a fila, depois enviar 80007 "Partida Encontrada!" e o 90001!
                        setTimeout(() => {
                            // Envia 80007 para fechar a tela de busca
                            socket.write(`e1:SendMsgToHallClient,(${encodeInt(80001)},(${encodeInt(80007)},()))\n`);
                            
                            // Agora simula o 60100 para criar a sala e jogar todo mundo pra dentro
                            const p80001 = encodeInt(80001);
                            const s90001 = encodeInt(90001);
                            const s90002 = encodeInt(90002);
                            const s90004 = encodeInt(90004);
                            const p1304 = encodeInt(1304);
                            
                            const mapId = 5010; // STAGING MAP FOR ARENA UI
                            socket.finalMapId = 5204; // REAL COMBAT MAP
                            socket.targetMapId = mapId;
                            socket.waitingForHeroLock = true; // IMPEDE O SPAWN 10001 AQUI, ESPERA ELE ESCOLHER HERÓI!
                            socket.hasConfirmedHero = false;
                            const mapIdEnc = encodeInt(mapId);
                            const mapConf = MAP_CONFIG[mapId];
                            const mapTypeEnc = encodeInt(mapConf.type);
                            const startTime = Fun.GetServerDate();
                            const zero = encodeInt(0);
                            const one = encodeInt(1);
                            
                            // Gera os Bots para a partida
                            const BOT_NAMES = ['Bot_Lars', 'Bot_Gaya', 'Bot_Tanya', 'Bot_Lupin', 'Bot_Chaser', 'Bot_Buster', 'Bot_Windy', 'Bot_Kahn', 'Bot_Raptor', 'Bot_Slayer', 'Bot_Viper'];
                            
                            const isCapEnc = encodeInt(1);
                            const myHeroIdEnc = encodeInt(socket.currentHeroId || 1015);
                            const myUserDataHeroInfo = `(${myHeroIdEnc},${one},${one},${one},${zero})`;
                            const prerogativeLobbyMe = `(${isCapEnc},${isCapEnc},${isCapEnc},${isCapEnc},'${socket.playerNameEnc}',${zero},'',${zero})`;
                            const myUserData = `('${socket.playerNameEnc}',${encodeInt(99)},${one},${one},${one},${one},${myHeroIdEnc},${one},${one},${zero},${one},'Local',${zero},${myUserDataHeroInfo},${one},${zero},${zero},${isCapEnc},${prerogativeLobbyMe},())`;
                            
                            let userListElements = [`'${socket.playerNameEnc}',${myUserData}`];
                            
                            for (let i = 1; i < 10; i++) {
                                const botName = BOT_NAMES[i % BOT_NAMES.length];
                                const forceEnc = encodeInt(i < 5 ? 1 : 2); // 5 Blue, 5 Red
                                const isReadyEnc = encodeInt(1);
                                const heroIdEnc = encodeInt(1015 + (i % 5));
                                
                                const prerogativeLobby = `(${encodeInt(0)},${encodeInt(0)},${encodeInt(0)},${encodeInt(0)},'${encodeHex(botName)}',${encodeInt(0)},'',${encodeInt(0)})`;
                                const userDataHeroInfo = `(${heroIdEnc},${encodeInt(1)},${encodeInt(1)},${encodeInt(1)},${encodeInt(0)})`;
                                const botUserData = `('${encodeHex(botName)}',${encodeInt(99)},${encodeInt(1)},${forceEnc},${isReadyEnc},${encodeInt(1)},${heroIdEnc},${encodeInt(1)},${encodeInt(1)},${encodeInt(1)},${encodeInt(1)},'Local',${encodeInt(0)},${userDataHeroInfo},${encodeInt(1)},${encodeInt(0)},${encodeInt(0)},${encodeInt(0)},${prerogativeLobby},())`;
                                
                                userListElements.push(`'${encodeHex(botName)}',${botUserData}`);
                            }
                            
                            const userList = userListElements.join(',');
                            const isChangeHeroEnc = mapConf.type === 7 ? encodeInt(1) : zero;
                            const matchMsg = `(${s90001},(${userList}),${mapIdEnc},${mapTypeEnc},'${mapConf.name}',${encodeInt(60)},${isChangeHeroEnc},'${startTime}',${zero},${zero})`;
                            
                            (async () => {
                                await sendPacket(socket, `e1:SendMsgToHallClient,(${p80001},${matchMsg})\n`, 400);
                                await sendPacket(socket, `e1:SendMsgToHallClient,(${p80001},(${s90002},'${socket.playerNameEnc}',${myUserData}))\n`, 400);
                                
                                setTimeout(() => {
                                    socket.write(`e1:SendMsgToHallClient,(${encodeInt(1304)},(${encodeInt(1)},'${socket.playerNameEnc}','${SERVER_IP}',${encodeInt(7777)},'SceneGroup',${encodeInt(1)}))\n`);
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
                    
                    const one = encodeInt(1);
                    const zero = encodeInt(0);
                    const dateStr = Fun.GetServerDate();
                    const ninetyNine = encodeInt(99);
                    
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
                        const heroIdEnc = encodeInt(heroId);
                        const heroInfo = generateFullHeroData(heroId).split(',').slice(1).join(',');
                        
                        const unlockedSkin = `(${one},(${one},${one},${zero},${zero},'${dateStr}',${zero}))`;
                        const unlockedWeapon = `(${one},(${one},${one},${zero},${zero},'${dateStr}',${zero},()))`;
                        const heroInfoData = `(${one},${zero},${encodeInt(2)},'${dateStr}',${zero},(),${unlockedSkin},${unlockedWeapon},(),(),${zero},${one},${one},${zero},${zero},${zero})`;
                        
                        const userDataHeroInfo = `(${heroIdEnc},${one},${one},${one},${zero})`;
                        const isCap = idx === 0 ? 1 : 0;
                        const isCapEnc = encodeInt(isCap);
                        const prerogativeLobby = `(${isCapEnc},${isCapEnc},${isCapEnc},${isCapEnc},'${s.playerNameEnc}',${zero},'',${zero})`;
                        
                        s.userData = `('${s.playerNameEnc}',${ninetyNine},${one},${one},${one},${one},${heroIdEnc},${one},${one},${zero},${one},'Local',${zero},${userDataHeroInfo},${one},${zero},${zero},${isCapEnc},${prerogativeLobby},())`;
                    });
                    
                    
                          const BOT_NAMES2 = ['Bot_Lars', 'Bot_Gaya', 'Bot_Tanya', 'Bot_Lupin', 'Bot_Chaser', 'Bot_Buster', 'Bot_Windy', 'Bot_Kahn', 'Bot_Raptor', 'Bot_Slayer', 'Bot_Viper'];
                          let userListElements2 = lobbySockets.map(s => `('${s.playerNameEnc}',${s.userData})`);
                          
                          // Preenche com bots at fechar 10 jogadores (5v5)
                          for (let i = lobbySockets.length; i < 10; i++) {
                              const botName = BOT_NAMES2[i % BOT_NAMES2.length];
                              const forceEnc = encodeInt(i < 5 ? 1 : 2); // 5 Blue, 5 Red
                              const isReadyEnc = encodeInt(1);
                              const heroIdEnc = encodeInt(1015 + (i % 5));
                              
                              const prerogativeLobby = `(${encodeInt(0)},${encodeInt(0)},${encodeInt(0)},${encodeInt(0)},'${encodeHex(botName)}',${encodeInt(0)},'',${encodeInt(0)})`;
                              const userDataHeroInfo = `(${heroIdEnc},${encodeInt(1)},${encodeInt(1)},${encodeInt(1)},${encodeInt(0)})`;
                              const botUserData = `('${encodeHex(botName)}',${encodeInt(99)},${encodeInt(1)},${forceEnc},${isReadyEnc},${encodeInt(1)},${heroIdEnc},${encodeInt(1)},${encodeInt(1)},${encodeInt(1)},${encodeInt(1)},'Local',${encodeInt(0)},${userDataHeroInfo},${encodeInt(1)},${encodeInt(0)},${encodeInt(0)},${encodeInt(0)},${prerogativeLobby},())`;
                              
                              userListElements2.push(`('${encodeHex(botName)}',${botUserData})`);
                          }
                          const userList = userListElements2.join(',');

                    
                    const mapId = 5204; // bachang_baxi (loadmap ID real)
                    const mapIdEnc = encodeInt(mapId);
                    const mapConf = MAP_CONFIG[mapId];
                    lastMapId = mapId;
                    lastMapName = mapConf.name;
                    
                    const mapTypeEnc = encodeInt(mapConf.type);
                    const startTime = Fun.GetServerDate();
                    
                    const p80001 = encodeInt(80001);
                    const s90001 = encodeInt(90001);
                    const s90002 = encodeInt(90002);
                    const s90004 = encodeInt(90004);
                    const p1304 = encodeInt(1304);
                    
                    const isChangeHeroEnc = mapConf.type === 7 ? encodeInt(1) : zero;
                    const matchMsg = `(${s90001},(${userList}),${mapIdEnc},${mapTypeEnc},'${mapConf.name}',${encodeInt(60)},${isChangeHeroEnc},'${startTime}',${zero},${zero})`;
                    
                    // Envia os pacotes de transição para cada um dos sockets do Lobby
                    lobbySockets.forEach(s => {
                        (async () => {
                            await sendPacket(s, `e1:SendMsgToHallClient,(${p80001},${matchMsg})\n`, 400);
                            await sendPacket(s, `e1:SendMsgToHallClient,(${p80001},(${s90002},'${s.playerNameEnc}',${s.userData}))\n`, 400);
                            await sendPacket(s, `e1:SendMsgToHallClient,(${p80001},(${s90004}))\n`, 400);
                            
                            setTimeout(() => {
                                s.write(`e1:SendMsgToHallClient,(${p1304},(${encodeInt(1)},'${s.playerNameEnc}','${SERVER_IP}',${encodeInt(7777)},'SceneGroup',${encodeInt(1)}))\n`);
                            }, 1500);
                        })().catch(err => console.error('[❌ ERRO no envio do Lobby Match]', err));
                    });
                    
                    return;
                }

                // ==========================================
                // 🏠 SALA PERSONALIZADA (20013)
                // ==========================================
                if (cmdId === 20013) {
                    const roomData = parseArrayList(args);
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
                            createTime: Fun.GetServerDate(),
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
                        socket.write(`e1:SendMsgToHallClient,(${encodeInt(3310)},(${encodeInt(roomUID)}))\n`);
                        
                        // 2. Envia 3301 com (1) indicando sucesso
                        socket.write(`e1:SendMsgToHallClient,(${encodeInt(20013)},(${encodeInt(3301)},(${encodeInt(1)})))\n`);
                        return;
                    }

                    // 3307: Obter informações da sala
                    if (subCmd === 3307) {
                        if (!currentRoom) {
                            socket.write(`e1:SendMsgToHallClient,(${encodeInt(20013)},(${encodeInt(3307)},(-1,())))\n`);
                            return;
                        }
                        const rData = buildRoomData(currentRoom);
                        socket.write(`e1:SendMsgToHallClient,(${encodeInt(20013)},(${encodeInt(3307)},(1,${rData})))\n`);
                        return;
                    }

                    // 3306: Lista de salas
                    if (subCmd === 3306) {
                        if (!currentRoom) {
                            socket.write(`e1:SendMsgToHallClient,(${encodeInt(20013)},(${encodeInt(3306)},(0,1,())))\n`);
                            return;
                        }
                        const roomInfo = `(${encodeInt(currentRoom.mapId)},'${encodeHex(currentRoom.creatorName)}','${encodeHex(currentRoom.roomName)}','${encodeHex(currentRoom.password)}',${encodeInt(currentRoom.mapId)},${encodeInt(currentRoom.mapType)},0,${encodeInt(currentRoom.maxPlayers)},0,0,0,${encodeInt(getRoomActivePlayersCount(currentRoom))},0,${encodeInt(currentRoom.state)},0,${encodeInt(7777)},'${currentRoom.createTime}',${encodeInt(currentRoom.roomUID)},0)`;
                        socket.write(`e1:SendMsgToHallClient,(${encodeInt(20013)},(${encodeInt(3306)},(1,1,(${roomInfo}))))\n`);
                        return;
                    }

                    // 3303: Entrar na sala
                    if (subCmd === 3303) {
                        if (!currentRoom) {
                            socket.write(`e1:SendMsgToHallClient,(${encodeInt(20013)},(${encodeInt(3303)},(-1,1)))\n`);
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
                            socket.write(`e1:SendMsgToHallClient,(${encodeInt(20013)},(${encodeInt(3303)},(-2,1)))\n`);
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
                        
                        socket.write(`e1:SendMsgToHallClient,(${encodeInt(3310)},(${encodeInt(currentRoom.roomUID)}))\n`);
                        
                        const rDataJoin = buildRoomData(currentRoom);
                        const joinSuccessMsg = `(1,(1,${rDataJoin},'${encodeHex(currentRoom.creatorName)}'))`;
                        socket.write(`e1:SendMsgToHallClient,(${encodeInt(20013)},(${encodeInt(3303)},${joinSuccessMsg}))\n`);
                        
                        broadcastToRoom(currentRoom, `e1:SendMsgToHallClient,(${encodeInt(20013)},(${encodeInt(3318)},(1,${buildRoomUserList(currentRoom)})))\n`);
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
                            const disbandPacket = `e1:SendMsgToHallClient,(${encodeInt(20013)},(${encodeInt(3304)},(1,${encodeInt(currentRoom.roomUID)},${rDataDisband},'${p.socket.playerNameEnc}',0,3)))\n`;
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
                            socket.write(`e1:SendMsgToHallClient,(${encodeInt(20013)},(${encodeInt(3304)},(1,${encodeInt(currentRoom.roomUID)},${rDataLeave},'${p.socket.playerNameEnc}',0,1)))\n`);
                            
                            broadcastToRoom(currentRoom, `e1:SendMsgToHallClient,(${encodeInt(20013)},(${encodeInt(3318)},(1,${buildRoomUserList(currentRoom)})))\n`);
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
                            
                            broadcastToRoom(currentRoom, `e1:SendMsgToHallClient,(${encodeInt(20013)},(${encodeInt(3318)},(1,${buildRoomUserList(currentRoom)})))\n`);
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
                        const changesStr = changes.map(v => typeof v === 'number' ? encodeInt(v) : v).join(',');
                        broadcastToRoom(currentRoom, `e1:SendMsgToHallClient,(${encodeInt(20013)},(${encodeInt(3324)},(${changesStr})))\n`);
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
                        broadcastToRoom(currentRoom, `e1:SendMsgToHallClient,(${encodeInt(20013)},(${encodeInt(3308)},(1,${rDataReady},'${p.socket.playerNameEnc}')))\n`);
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
                        
                        const targetNameDec = decodeHex(subArgs[1]);
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
                                targetSocket.write(`e1:SendMsgToHallClient,(${encodeInt(20013)},(${encodeInt(3304)},(1,${encodeInt(currentRoom.roomUID)},${rDataKick},'${targetSocket.playerNameEnc}',0,1)))\n`);
                            }
                            
                            broadcastToRoom(currentRoom, `e1:SendMsgToHallClient,(${encodeInt(20013)},(${encodeInt(3318)},(1,${buildRoomUserList(currentRoom)})))\n`);
                        }
                        return;
                    }

                    // 3320: Iniciar Partida da Sala
                    if (subCmd === 3320) {
                        if (!currentRoom) return;
                        const mapId = currentRoom.mapId;
                        const mapConf = MAP_CONFIG[mapId] || { name: 'bachang_baxi', type: 6 };
                        currentRoom.state = 4; // Jogando
                        
                        lastMapId = mapId;
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
                        });
                        
                        const one = encodeInt(1);
                        const zero = encodeInt(0);
                        const dateStr = Fun.GetServerDate();
                        const ninetyNine = encodeInt(99);
                        
                        roomPlayers.forEach((p, idx) => {
                            const playerIndex = idx + 1;
                            p.socket.playerIndex = playerIndex;
                            p.socket.playerId = String(playerIndex);
                            
                            const heroId = p.socket.currentHeroId || 1015;
                            const heroIdEnc = encodeInt(heroId);
                            const heroInfo = generateFullHeroData(heroId).split(',').slice(1).join(',');
                            
                            const userDataHeroInfo = `(${heroIdEnc},${one},${one},${one},${zero})`;
                            const isCaptain = p.isCaptain ? 1 : 0;
                            const isCaptainEnc = encodeInt(isCaptain);
                            const prerogativeLobby = `(${isCaptainEnc},${isCaptainEnc},${isCaptainEnc},${isCaptainEnc},'${p.socket.playerNameEnc}',${zero},'',${zero})`;
                            p.socket.userData = `('${p.socket.playerNameEnc}',${ninetyNine},${one},${encodeInt(p.force)},${one},${one},${heroIdEnc},${one},${one},${zero},${one},'Local',${zero},${userDataHeroInfo},${one},${zero},${zero},${isCaptainEnc},${prerogativeLobby},())`;
                        });
                        
                        const BOT_NAMES = ['Bot_Lars', 'Bot_Gaya', 'Bot_Tanya', 'Bot_Lupin', 'Bot_Chaser', 'Bot_Buster', 'Bot_Windy', 'Bot_Kahn', 'Bot_Raptor', 'Bot_Slayer', 'Bot_Viper'];
                        let userListElements = [];
                        roomPlayers.forEach(p => {
                            userListElements.push(`'${p.socket.playerNameEnc}',${p.socket.userData}`);
                        });
                        
                        // Preenche com bots até fechar 10 jogadores (5v5)
                        for (let i = roomPlayers.length; i < 10; i++) {
                            const botName = BOT_NAMES[i % BOT_NAMES.length];
                            const forceEnc = encodeInt(i < 5 ? 1 : 2); // 5 Blue, 5 Red
                            const isReadyEnc = encodeInt(1);
                            const heroIdEnc = encodeInt(1015 + (i % 5)); // Heróis diferentes
                            
                            const prerogativeLobby = `(${encodeInt(0)},${encodeInt(0)},${encodeInt(0)},${encodeInt(0)},'${encodeHex(botName)}',${encodeInt(0)},'',${encodeInt(0)})`;
                            const userDataHeroInfo = `(${heroIdEnc},${encodeInt(1)},${encodeInt(1)},${encodeInt(1)},${encodeInt(0)})`;
                            const botUserData = `('${encodeHex(botName)}',${encodeInt(99)},${encodeInt(1)},${forceEnc},${isReadyEnc},${encodeInt(1)},${heroIdEnc},${encodeInt(1)},${encodeInt(1)},${encodeInt(1)},${encodeInt(1)},'Local',${encodeInt(0)},${userDataHeroInfo},${encodeInt(1)},${encodeInt(0)},${encodeInt(0)},${encodeInt(0)},${prerogativeLobby},())`;
                            
                            userListElements.push(`'${encodeHex(botName)}',${botUserData}`);
                        }
                        const userList = userListElements.join(',');
                        
                        const p80001 = encodeInt(80001);
                        const s90001 = encodeInt(90001);
                        const s90002 = encodeInt(90002);
                        const p1304 = encodeInt(1304);
                        
                        const matchMsg = `(${s90001},(${userList}),${encodeInt(stagingMapId)},${encodeInt(stagingConf.type)},'${stagingConf.name}',${encodeInt(60)},${zero},'${dateStr}',${encodeInt(1)},${zero})`;
                        
                        roomPlayers.forEach(p => {
                            (async () => {
                                await sendPacket(p.socket, `e1:SendMsgToHallClient,(${p80001},${matchMsg})\n`, 400);
                                await sendPacket(p.socket, `e1:SendMsgToHallClient,(${p80001},(${s90002},'${p.socket.playerNameEnc}',${p.socket.userData}))\n`, 400);
                                
                                setTimeout(() => {
                                    p.socket.write(`e1:SendMsgToHallClient,(${p1304},(${one},'${p.socket.playerNameEnc}','${SERVER_IP}',${encodeInt(7777)},'SceneGroup',${one}))\n`);
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
                    const chatData = parseArrayList(args);
                    const channel = chatData[0];
                    const msgHex = chatData[1]; // A mensagem vem em Hex
                    let msg = decodeHex(msgHex); // Traduz para texto normal

                    console.log(`[💬 CHAT] Canal: ${channel} | Mensagem: ${msg}`);

                    // 1. Processar Comandos de Admin
                    if (msg.startsWith('/')) {
                        const parts = msg.split(' ');
                        const command = parts[0].toLowerCase();

                        if (command === '/start') {
                            socket.targetMapId = 5202;
                            socket.targetMapName = 'bachang_baxi';
                            startTdmMatch(socket);
                            return;
                        }

                        if (command === '/start') {
                            startTdmMatch(socket);
                            return;
                        }

                        const val = parts[1] ? parseInt(parts[1]) : 999999;
                        const valEnc = encodeInt(val);
                        const moneyList = `(${valEnc},${valEnc},${valEnc},0,${encodeInt(10)},0,${valEnc},${valEnc},${valEnc},0,${valEnc},0,0,0)`;

                        if (command === '/gold' || command === '/gem' || command === '/money') {
                            socket.write(`e1:SendMsgToHallClient,(${encodeInt(10025)},(0,${moneyList}))\n`);
                            console.log(`[💰 MOEDA] Riqueza injetada: ${val}`);

                            const sysMsg = encodingUTF8(`Comando aplicado! Riqueza de ${val} injetada.`);
                            socket.write(`e1:SendMsgToHallClient,(${encodeInt(1006)},(8,'system','${sysMsg}','','',${encodeInt(0)},${encodeInt(0)},${encodeInt(0)},()))\n`);
                        }

                        if (command === '/unlockall' || command === '/cheat') {
                            const porta1306 = encodeInt(1306);
                            const respostaHerois = encodeInt(12002);
                            const heroIds = [1003, 1005, 1006, 1015, 1016, 1017, 1021, 1022, 1023, 1024, 1025, 1026, 1027, 1028, 1031, 1032, 1033, 1034, 1035, 1036, 1037, 2000];
                            const heroDataArray = heroIds.map(id => generateFullHeroData(id)).join(',');
                            socket.write(`e1:SendMsgToHallClient,(${porta1306},(${respostaHerois},(${heroDataArray})))\n`);

                            const sysMsg = encodingUTF8(`TUDO LIBERADO! Reinicie a tela de heróis.`);
                            socket.write(`e1:SendMsgToHallClient,(${encodeInt(1006)},(8,'system','${sysMsg}','','',${encodeInt(0)},${encodeInt(0)},${encodeInt(0)},()))\n`);
                        }

                        if (command === '/map') {
                            const mapId = parts[1] ? parseInt(parts[1]) : 5205;
                            let mapName = 'learnfb';
                            if (mapId === 1) mapName = 'fb001';

                            lastMapId = mapId;
                            lastMapName = mapName;

                            console.log(`[🚀 MAPA] Forçando carregamento: ${mapName} (${mapId})`);

                            const p80001 = encodeInt(80001);
                            const s90001 = encodeInt(90001);
                            const userData = `('Admin',${encodeInt(1)},1,1,1,1,${encodeInt(1003)},1,1,1,1,'Local',0,())`;
                            const userList = `('Admin',${userData})`;

                            const matchMsg = `(${s90001},${userList},${mapId},1,'${mapName}',30,1,'2024-01-01 12:00:00',0,0)`;
                            socket.write(`e1:SendMsgToHallClient,(${p80001},${matchMsg})\n`);

                            const p1304 = encodeInt(1304);
                            const one = encodeInt(1);
                            socket.write(`e1:SendMsgToHallClient,(${p1304},(${one},'${PLAYER_NAME_ENC}','${SERVER_IP}',${encodeInt(7777)},'SceneGroup',${one}))\n`);

                            const sysMsg = encodingUTF8(`Teleportando para ${mapName}...`);
                            socket.write(`e1:SendMsgToHallClient,(${encodeInt(1006)},(8,'system','${sysMsg}','','',0,0,0,()))\n`);
                        }

                        if (command === '/item') {
                            const itemId = parts[1] ? parseInt(parts[1]) : 1001;
                            const count = parts[2] ? parseInt(parts[2]) : 1;
                            const p70000 = encodeInt(70000);
                            socket.write(`e1:SendMsgToHallClient,(${p70000},(${encodeInt(70018)},(${itemId},${count},0)))\n`);
                            socket.write(`e1:SendMsgToHallClient,(${p70000},(${encodeInt(70012)},(1,${itemId},${count},'Comando Admin',(),'Admin')))\n`);
                            const sysMsg = encodingUTF8('O Item apareceu na sua mochila!');
                            socket.write(`e1:SendMsgToHallClient,(${encodeInt(1006)},(8,'system','${sysMsg}','','',0,0,0,()))\n`);
                        }

                        if (command === '/team' || command === '/time' || command === '/lado') {
                            if (!currentRoom) {
                                const sysMsg = encodingUTF8('Você precisa estar em uma sala para usar este comando!');
                                socket.write(`e1:SendMsgToHallClient,(${encodeInt(1006)},(8,'system','${sysMsg}','','',0,0,0,()))\n`);
                            } else {
                                let pSlot = -1;
                                for (let i = 0; i < 10; i++) {
                                    if (currentRoom.players[i] && currentRoom.players[i].socket === socket) {
                                        pSlot = i;
                                        break;
                                    }
                                }
                                if (pSlot === -1) {
                                    const sysMsg = encodingUTF8('Erro: Você não foi encontrado na sala!');
                                    socket.write(`e1:SendMsgToHallClient,(${encodeInt(1006)},(8,'system','${sysMsg}','','',0,0,0,()))\n`);
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
                                        broadcastToRoom(currentRoom, `e1:SendMsgToHallClient,(${encodeInt(20013)},(${encodeInt(3318)},(1,${rDataChange})))\n`);
                                        
                                        const teamName = targetIsRed ? 'Vermelho' : 'Azul';
                                        const sysMsg = encodingUTF8(`Você foi movido para o Time ${teamName} (Vaga ${absoluteIdx})!`);
                                        socket.write(`e1:SendMsgToHallClient,(${encodeInt(1006)},(8,'system','${sysMsg}','','',0,0,0,()))\n`);
                                    } else {
                                        const teamName = targetIsRed ? 'Vermelho' : 'Azul';
                                        const sysMsg = encodingUTF8(`O Time ${teamName} está cheio!`);
                                        socket.write(`e1:SendMsgToHallClient,(${encodeInt(1006)},(8,'system','${sysMsg}','','',0,0,0,()))\n`);
                                    }
                                }
                            }
                        }
                    } else {
                        // Eco de Chat Normal (Faz a mensagem aparecer pra você)
                        const p1006 = encodeInt(1006);
                        socket.write(`e1:SendMsgToHallClient,(${p1006},(${channel},'[mi]${PLAYER_NAME_ENC}[/mi]','[mi]${msgHex}[/mi]','','',0,0,0,()))\n`);
                    }
                }

                // ==========================================
                // 🛒 LOJA (ID 70000)
                // ==========================================
                if (cmdId === 70000) {
                    const shopData = parseArrayList(args);
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
                        const shopEntry = GAME_DB.shop[shopId];

                        // Fallback: se o item não estiver no nosso Mock DB, inventamos um para o jogo não quebrar
                        const itemId = shopEntry ? shopEntry.itemId : (1000 + shopId);
                        const itemEntry = GAME_DB.items[itemId] || { name: `Item Desconhecido ${itemId}`, goldPrice: 100, diamondPrice: 10, type: 2 };

                        // 2. Deduzir o valor da conta (Supondo que payType 0 seja diamante e 1 seja ouro)
                        const totalCost = (payType === 1 ? itemEntry.goldPrice : itemEntry.diamondPrice) * count;
                        if (payType === 1) {
                            playerInventory.gold -= totalCost;
                        } else {
                            playerInventory.diamonds -= totalCost;
                        }

                        // Atualizar inventário na memória do servidor
                        if (!playerInventory.items[itemId]) playerInventory.items[itemId] = 0;
                        playerInventory.items[itemId] += count;

                        console.log(`[🛒 LOJA] Vendido! Item: ${itemEntry.name} (ID: ${itemId}) por ${totalCost}. Saldo: G:${playerInventory.gold} D:${playerInventory.diamonds}`);

                        // 3. Enviar pacote OnBuyItemOk (70009) para fechar a telinha de "Comprando..."
                        // A estrutura esperada no C# é: (shopId, count, desconto/bônus, erro)
                        const p70000 = encodeInt(70000);
                        const p70009 = encodeInt(70009);
                        socket.write(`e1:SendMsgToHallClient,(${p70000},(${p70009},(${shopId},${count},0,0)))\n`);

                        // 4. Atualizar o dinheiro na HUD do jogador (10025)
                        const goldEnc = encodeInt(playerInventory.gold);
                        const diaEnc = encodeInt(playerInventory.diamonds);
                        // A lista gigantesca de moedas do jogo: Gold, GoldBind, Gems, GemsBind...
                        const moneyList = `(${goldEnc},${goldEnc},${diaEnc},0,${encodeInt(10)},0,${goldEnc},${goldEnc},${goldEnc},0,${goldEnc},0,0,0)`;
                        socket.write(`e1:SendMsgToHallClient,(${encodeInt(10025)},(0,${moneyList}))\n`);

                        // 5. ENTREGAR O ITEM (Verificando se é Buff ou Item Físico)
                        if (itemEntry.bufftype > 0) {
                            // É um BUFF (XP/Moeda). O cliente espera o pacote 70007.
                            // Estrutura: (70007, ((TipoDoBuff, (Multiplicador, 'DataFinal', ItemId))), FlagAtualizarUI)
                            const p70007 = encodeInt(70007);
                            const buffData = `(${itemEntry.bufftype},(${itemEntry.buffbeishu},'2026/12/31 12:00:00',${itemId}))`;
                            socket.write(`e1:SendMsgToHallClient,(${p70000},(${p70007},((${buffData}),1)))\n`);
                            console.log(`[✨ BUFF] Ativado: ${itemEntry.name}`);
                        } else {
                            // Item normal (Peça de arma, Material). Usa o pacote 70018.
                            const p70018 = encodeInt(70018);
                            const newTotal = playerInventory.items[itemId];
                            socket.write(`e1:SendMsgToHallClient,(${p70000},(${p70018},(${itemId},${newTotal},1)))\n`);
                            console.log(`[🎒 ITEM] Entregue: ${itemEntry.name}`);
                        }

                        // Mensagem opcional no chat
                        const sysMsg = encodingUTF8(`Você obteve ${count}x ${itemEntry.name}!`);
                        socket.write(`e1:SendMsgToHallClient,(${encodeInt(1006)},(8,'system','${sysMsg}','','',${encodeInt(0)},${encodeInt(0)},${encodeInt(0)},()))\n`);
                    }
                }
                // ==========================================
                // 🎒 SISTEMA DE INVENTÁRIO E HERÓIS (ID 1306)
                // ==========================================
                if (cmdId === 1306) {
                    const heroData = parseArrayList(args);
                    const subCmdId = heroData[0];
                    const subArgs = heroData[1] || [];
                    const porta1306 = encodeInt(1306);

                    if (subCmdId === 11002) {
                        console.log("[🎭] Cliente solicitou lista de heróis desbloqueados...");
                        const respostaHerois = encodeInt(12002);
                        const heroIds = [1001, 1002, 1003, 1004, 1005, 1006, 1007, 1008, 1009, 1010, 1011, 1012, 1013, 1014, 1015, 1016, 1017, 1018, 1019, 1020, 1021, 1022, 1023, 1024, 1025, 1026, 1027, 1028, 1030, 1031, 1032, 1033, 1034, 1035, 1036, 1037, 1038, 1039, 1040, 1041, 1042, 1043, 1044, 1045];
                        
                        const one = encodeInt(1);
                        const zero = encodeInt(0);
                        const dateStr = Fun.GetServerDate();

                        const heroDataArray = heroIds.map(id => generateFullHeroData(id)).join(',');
                        socket.write(`e1:SendMsgToHallClient,(${porta1306},(${respostaHerois},(${heroDataArray})))\n`);
                    }
                    if (subCmdId === 12013 || subCmdId === 12014 || subCmdId === 12015) {

                        socket.write(`e1:SendMsgToHallClient,(${porta1306},(${encodeInt(subCmdId)},(${subArgs[0]},${encodeInt(1)})))\n`);
                    }
                    // 12009: Mudar Equipamento (Skin, Arma, Emote)
                    else if (subCmdId === 12009) {
                        const params = heroData[1];
                        const heroId = params[0]; // Isso é 1021 (Decodificado)
                        const type = params[1];
                        const itemId = params[2];
                        console.log(`[👕 EQUIPE] Herói ${heroId} | Tipo ${type} | ID ${itemId}`);

                        // 1. Atualiza a memória
                        if (!playerEquipment[heroId]) {
                            playerEquipment[heroId] = { skin: 1, weapon: 1, emote: 1, pose: 0 };
                        }

                        if (type === 1) playerEquipment[heroId].skin = itemId;
                        else if (type === 2) playerEquipment[heroId].weapon = itemId;
                        else if (type === 3) playerEquipment[heroId].emote = itemId;
                        else if (type === 4) playerEquipment[heroId].pose = itemId;

                        // 2. Chave Mestra para manter tudo liberado na memória 3D
                        const skinVal = `(2,2,2,'2099/01/01 12:00:00',2,2,2,2)`;
                        const wpVal = `(2,2,2,'2099/01/01 12:00:00',2,2,(),2,2)`;
                        const emVal = `(2,2,2,'2099/01/01 12:00:00',2,2,2,2)`;

                        let skinArr = []; let wpArr = []; let emArr = [];
                        for (let i = 1; i <= 10; i++) {
                            skinArr.push(`${i},${skinVal}`);
                            wpArr.push(`${i},${wpVal}`);
                            emArr.push(`${i},${emVal}`);
                        }

                        // 3. Monta a nova ficha do herói com as roupas novas
                        const eq = playerEquipment[heroId];
                        const newData = `(1,0,2,'2099/01/01 12:00:00',0,(),(${skinArr.join(',')}),(${wpArr.join(',')}),(${emArr.join(',')}),(),0,${eq.skin},${eq.weapon},${eq.emote},${eq.pose},0)`;

                        // 4. O SEGREDO: Re-codificar o HeroID (1021 -> Fx) antes de devolver!
                        const encHeroId = encodeInt(heroId);

                        // 5. Envia 12006 (UpdateOneHero) -> Atualiza a memória 3D do Unity
                        const p12006 = encodeInt(12006);
                        socket.write(`e1:SendMsgToHallClient,(${encodeInt(1306)},(${p12006},(${encHeroId},${newData})))\n`);

                        // 6. Envia 12009 (OnChangeUseOk) -> Toca o som de sucesso na tela
                        const p12009 = encodeInt(12009);
                        socket.write(`e1:SendMsgToHallClient,(${encodeInt(1306)},(${p12009},(1,${encHeroId},${type},${itemId})))\n`);
                    }
                }

                // ==========================================
                // 💓 HEARTBEAT / PING (ID 100001)
                // ==========================================
                if (cmdId === 100001) {
                    socket.write(`e1:SendMsgToHallClient,(${encodeInt(100001)},((0),(0),(0)))\n`);
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
                            selectedHeroId = decodeInt(match[1]);
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

                    const defaultCoords = getSpawnCoordinates(socket.targetMapId || lastMapId || 5202, socket.force);
                    socket.lastX = defaultCoords.sx;
                    socket.lastY = defaultCoords.sy;
                    socket.lastZ = defaultCoords.sz;

                    // 2. Envia pacote 12002 para liberar herói/cosméticos na memória do cliente
                    const p1306 = encodeInt(1306);
                    const p12002 = encodeInt(12002);
                    const heroIdEnc = encodeInt(selectedHeroId);
                    socket.write(`e1:SendMsgToClient,(${p1306},(${p12002},(${heroIdEnc},${generateFullHeroData(selectedHeroId, 1, 1).split(',').slice(1).join(',')})))\n`);

                    // 3. Envia o spawn (10001) para si mesmo
                    socket.bypassInterceptor = true;
                    socket.write(getPlayerSpawnPacket(socket, socket));
                    socket.bypassInterceptor = false;

                    // 4. Broadcast do novo spawn para todos os outros jogadores da cena
                    activeSockets.forEach(s => {
                        if (s.socketType === "SCENE" && s !== socket && !s.destroyed) {
                            s.bypassInterceptor = true;
                            s.write(getPlayerSpawnPacket(socket, s));
                            s.bypassInterceptor = false;
                        }
                    });

                    // 5. Fecha a tela de seleção de heróis no cliente (90008)
                    const p20014 = encodeInt(20014);
                    const s90008 = encodeInt(90008);
                    const one = encodeInt(1);
                    socket.write(`e1:SendMsgToClient,(${p20014},(${s90008},(),${one}))\n`);
                    return;
                }

                // ==========================================
                // 🏃 MOVIMENTO (ID 20001 - SP/SN)
                // ==========================================
                if (textoRecebido.includes("SP") || textoRecebido.includes("SN")) {
                    socket.write(`e1:SendMsgToClient,(${encodeInt(20058)},('${Fun.GetServerDate()}',${encodeInt(0)}))\n`);
                    return;
                }

                // ==========================================
                // 🎓 ENTRAR NO TREINAMENTO (ID 5030) ou TUTORIAL (ID 5032)
                // ==========================================
                if (cmdId === 5030 || cmdId === 5032) {
                    if (socket.inMatch) return;
                    socket.inMatch = true;
                    
                    const mapId = cmdId === 5032 ? 5205 : 5202;
                    const mapIdEnc = encodeInt(mapId);
                    const mapConf = MAP_CONFIG[mapId] || { name: 'xinshouyindao_3', type: 8 };
                    lastMapId = mapId;
                    lastMapName = mapConf.name;
                    socket.targetMapId = mapId;
                    socket.targetMapName = mapConf.name;
                    
                    const heroId = 1015;
                    const heroIdEnc = encodeInt(heroId);
                    const one = encodeInt(1);
                    const zero = encodeInt(0);
                    const dateStr = Fun.GetServerDate();
                    const ninetyNine = encodeInt(99);

                    // Unlocked items inner list structure (SkinUnlock.SetData):
                    // 0:?, 1:nLockState, 2:nPieceNum, 3:?, 4:overTime, 5:nNewState
                    const unlockedSkin = `(${one},(${one},${one},${zero},${zero},'${dateStr}',${zero}))`;
                    // WeaponUnlock.SetData: index 6 is szPartsGrid (ArrayList)
                    const unlockedWeapon = `(${one},(${one},${one},${zero},${zero},'${dateStr}',${zero},()))`;

                    // OpenHeroData structure:
                    // 0:Lvl, 1:Exp, 2:OpenState, 3:overTime, 4:ChipNum, 5:szAward, 6:szSkin, 7:szWeapon, 8:szEmotion, 9:szPose, 10:Zero, 11:UseSkin, 12:UseWeapon, 13:UseEmotion, 14:UsePose
                    const heroInfo = `(${one},${zero},${encodeInt(2)},'${dateStr}',${zero},(),${unlockedSkin},${unlockedWeapon},(),(),${zero},${one},${one},${zero},${zero},${zero})`;

                    const userDataHeroInfo = `(${heroIdEnc},${one},${one},${one},${zero})`;
                    const isCapEnc = encodeInt(1); // O próprio jogador de lobby é o capitão local da sua sessão até entrar em salas
                    const prerogativeLobby = `(${isCapEnc},${isCapEnc},${isCapEnc},${isCapEnc},'${socket.playerNameEnc}',${zero},'',${zero})`;

                    const userData = `('${socket.playerNameEnc}',${ninetyNine},${one},${one},${one},${one},${heroIdEnc},${one},${one},${one},${one},'Local',${zero},${userDataHeroInfo},${one},${zero},${zero},${isCapEnc},${prerogativeLobby},())`;
                    const userList = `('${socket.playerNameEnc}',${userData})`;

                    const mapTypeEnc = encodeInt(mapConf.type);
                    const startTime = Fun.GetServerDate();

                    const isChangeHeroEnc = mapConf.type === 7 ? encodeInt(1) : zero;
                    const matchMsg = `(${s90001},${userList},${mapIdEnc},${mapTypeEnc},'${mapConf.name}',${encodeInt(60)},${isChangeHeroEnc},'${startTime}',${encodeInt(1)},${zero})`;

                    lastLobbySocket = socket;
                    (async () => {
                        await sendPacket(socket, `e1:SendMsgToHallClient,(${p80001},${matchMsg})\n`, 400);
                        await sendPacket(socket, `e1:SendMsgToHallClient,(${p80001},(${s90002},'${socket.playerNameEnc}',${userData}))\n`, 400);
                        await sendPacket(socket, `e1:SendMsgToHallClient,(${p80001},(${s90004}))\n`, 400);

                        setTimeout(() => {
                            socket.write(`e1:SendMsgToHallClient,(${p1304},(${one},'${socket.playerNameEnc}','${SERVER_IP}',${encodeInt(7777)},'SceneGroup',${one}))\n`);
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
            const scnId = getSafeMapId(socket.targetMapId || lastMapId || 5202);
            const mapConf = MAP_CONFIG[scnId];
            const isChangeHero = (mapConf && (mapConf.type === 6 || mapConf.type === 7)) ? 1 : 0;
            const isChangeHeroEnc = encodeInt(isChangeHero);
            const startTime = Fun.GetServerDate();
            const p20014 = encodeInt(20014);
            const s90005 = encodeInt(90005);
            socket.write(`e1:SendMsgToClient,(${p20014},(${s90005},(${isChangeHeroEnc},'${startTime}')))\n`);
        }
        else if (socketType === "SCENE" && (textoRecebido.includes("SLoadMapOKMsg") || textoRecebido.includes("loadmapok:True"))) {
            console.log(`[🚀 SCENE] Mapa pronto! Spawnando herói padrão e abrindo seleção...`);

            const p1306 = encodeInt(1306);
            const p12002 = encodeInt(12002);
            const p20014 = encodeInt(20014);
            const s90005 = encodeInt(90005);
            const s90008 = encodeInt(90008);
            const p5000 = encodeInt(5000);
            const s80005 = encodeInt(80005);
            
            // Herói padrão inicial
            const heroId = socket.currentHeroId || 1015;
            socket.currentHeroId = heroId;
            socket.maxHp = getHeroMaxHp(heroId);
            socket.hp = socket.maxHp;
            socket.shield = 0;
            socket.isInvincible = false;
            socket.godMode = false;
            
            const heroIdEnc = encodeInt(heroId);
            const one = encodeInt(1);
            const zero = encodeInt(0);
            const dateStr = Fun.GetServerDate();

            // Determina as coordenadas do spawn baseadas no mapa e equipe (Usando o mapa original!)
            const originalMapId = socket.targetMapId || lastMapId || 5202;
            const currentMapId = getSafeMapId(originalMapId);
            const mapConf = MAP_CONFIG[currentMapId];
            const isChangeHero = (mapConf && (mapConf.type === 6 || mapConf.type === 7)) ? 1 : 0;
            const isChangeHeroEnc = encodeInt(isChangeHero);

            // SE AINDA ESTIVERMOS AGUARDANDO O LOCK DO HERÓI (Fase de Staging/Seleção), APENAS ABRA A UI (80005) E SUSPENDA O RESTO!
            if (socket.waitingForHeroLock) {
                socket.write(`e1:SendMsgToClient,(${p20014},(${s90005},(${isChangeHeroEnc},'${dateStr}')))\n`);
                socket.write(`e1:SendMsgToClient,(${p5000},(${s80005},()))\n`);
                console.log(`[🚀 ARENA UI] Mapa de Staging carregado. UI 80005 enviada com sucesso! Aguardando jogador dar Lock...`);
                return; // NÃO SPAWNA NINGUÉM AQUI!
            }

            // SE NÃO FOR 5010 (Ex: É 5204), INICIA A PARTIDA NORMALMENTE!
            const coords = getSpawnCoordinates(originalMapId, socket.force);
            let sx = coords.sx, sy = coords.sy, sz = coords.sz;
            socket.lastX = sx;
            socket.lastY = sy;
            socket.lastZ = sz;

            // 1. Inicia a partida e libera o herói na memória
            socket.write(`e1:SendMsgToClient,(${p20014},(${s90005},(${isChangeHeroEnc},'${dateStr}')))\n`);
            socket.write(`e1:SendMsgToClient,(${p1306},(${p12002},(${heroIdEnc},${generateFullHeroData(heroId).split(',').slice(1).join(',')})))\n`);

            // 2. Envia MatchUserInfo (10094) para destruir a Lobby UI e fechar popups, e depois o Spawn (10001)
            socket.bypassInterceptor = true;
            const spawnPacketMe = getPlayerSpawnPacket(socket, socket);
            const matchUserInfoPacket = spawnPacketMe.replace(`(${encodeInt(10001)},`, `(${encodeInt(10094)},`);
            socket.write(matchUserInfoPacket);
            socket.write(spawnPacketMe);
            socket.bypassInterceptor = false;

            // Broadcast do spawn para todos os outros jogadores na cena (verão como oponente index 2)
            activeSockets.forEach(s => {
                if (s.socketType === "SCENE" && s !== socket && !s.destroyed) {
                    s.bypassInterceptor = true; // Protege o pacote
                    s.write(getPlayerSpawnPacket(socket, s));
                    s.bypassInterceptor = false; // Liga novamente
                }
            });

            // Proativamente envia os spawns de todos os outros jogadores conectados para o novo jogador
            activeSockets.forEach(s => {
                if (s.socketType === "SCENE" && s !== socket && !s.destroyed) {
                    socket.bypassInterceptor = true; // Protege o pacote
                    socket.write(getPlayerSpawnPacket(s, socket));
                    socket.bypassInterceptor = false; // Liga novamente
                }
            });

            // 3. NÃO ABRIR A SELEÇÃO 80005 NOVAMENTE! (Já foi lockado no lobby/pré-jogo!)
            // if (isChangeHero === 1) { socket.write(`e1:SendMsgToClient,(${p5000},(${s80005},()))\n`); }
            
            // 4. Destrava tudo (90008) - Fecha telas de seleção e loading no cliente
            socket.write(`e1:SendMsgToClient,(${p20014},(${s90008},(),${one}))\n`);

            // const pLobby = socket.lobbySocket || lastLobbySocket;
            // if (pLobby && !pLobby.destroyed) {
            //     pLobby.write(`e${socket.playerIndex || 1}:SendMsgToHallClient,(${encodeInt(80001)},(${s90008},(),${one}))\n`);
            // }

            // Spawn/Atualização de NPCs na Arena de Treino se o mapa for bachang_baxi
            const currentMapName = socket.targetMapName || lastMapName || 'bachang_baxi';
            if (currentMapId === 5202 || currentMapName === 'bachang_baxi') {
                spawnPracticeNpcs(socket, true);
            }

            // Inicializa a partida TDM ou Control com delay seguro de 1s (Pós-carregamento estável de cena)
            if (MAP_CONFIG[originalMapId] && MAP_CONFIG[originalMapId].type === 7) {
                setTimeout(() => {
                    startTdmMatch(socket);
                }, 1000);
            } else if (MAP_CONFIG[originalMapId] && MAP_CONFIG[originalMapId].type === 3) {
                setTimeout(() => {
                    startControlMatch(socket);
                }, 1000);
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
                                const entId = decodeInt(parts[0]);
                                // Apenas atualiza a posição com base no jogador (NetID 1, 10001, 10002 ou >= 524289)
                                if (entId === 1 || entId === 10001 || entId === 10002 || entId >= 524289) {
                                    const rawX = decodeInt(parts[1]);
                                    const rawY = decodeInt(parts[2]);
                                    const rawZ = decodeInt(parts[3]);
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

                    // A função C# public void CN(int no) recebe UM ÚNICO INT PURO (Sem parênteses)!
                    if (syncType === 'SN') {
                        const pingVal = syncArgs.split(',')[1] || "0";
                        socket.write(`e${playerId}:CN,${pingVal}\n`);

                        // Envia periodicamente recarga do Dazhao (habilidade Ultimate Q) para garantir 100% de energia
                        try {
                            const p50001 = encodeInt(50001);
                            const p50004 = encodeInt(50004);
                            const zero = encodeInt(0);
                            const maxEnergyEnc = encodeInt(10000);
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
                        const cmdId = decodeInt(actionMatch[1]);
                        const argsStr = actionMatch[2];
                        console.log(`[⚔️ AÇÃO DE BATALHA] ID: ${cmdId} (Base62: ${actionMatch[1]}) | DADOS: ${argsStr}`);

                        // Se for 31203 (Reload / Fill Cartridge)
                        if (cmdId === 31203) {
                            // Extrai o nome da arma (o primeiro item do ArrayList 'argsStr')
                            // argsStr vem como: (shootname, netid) -> exemplo: (1, 1)
                            const argParts = argsStr.replace(/\(/g, "").replace(/\)/g, "").split(",");
                            const shootName = argParts[0] || 1;
                            
                            // Responde imediatamente liberando a trava de recarregamento e enchendo balas no grupo 0
                            const currentHeroId = socket.currentHeroId || 1015;
                            const maxAmmo = weaponsMap[currentHeroId] && weaponsMap[currentHeroId].cartridgePublic !== undefined ? weaponsMap[currentHeroId].cartridgePublic : 30;
                            
                            const fillBulletPacket = `e${playerId}:OnFillBullet,((${shootName},0,${encodeInt(maxAmmo)},${encodeInt(maxAmmo)}))\n`;
                            socket.write(fillBulletPacket);
                            console.log(`[✅ SERVER REPLIES]: ${fillBulletPacket.trim()}`);
                        }

                        // Se for 8475 (MSG_MATCH_CLIENTREADY / Entrada de Jogo Confirmada)
                        if (cmdId === 8475) {
                            console.log(`[🤖 CLIENT READY] O jogador carregou o mapa de treino! Spawnando NPCs...`);
                            spawnPracticeNpcs(socket);
                        }

                        // Se for 31207 (ChangeWeaponGroup)
                        if (cmdId === 31207) {
                            try {
                                const parsed = parseArrayList(argsStr);
                                const xingtai = parsed[0] || 0;
                                const changeGroupPacket = `e${playerId}:ChangeWeaponGroupUpdate,((${encodeInt(xingtai)}))\n`;
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
                                let skillParmsRaw = parseArrayList(argsStr);
                                if (skillParmsRaw.length === 1 && Array.isArray(skillParmsRaw[0])) {
                                    skillParmsRaw = skillParmsRaw[0];
                                }
                                console.log(`[⚔️ SKILLS PARM RAW RECEIVED]:`, skillParmsRaw);
                                
                                // NÃO decodificamos mais aqui, pois parseArrayList já realiza a decodificação de Base62 para inteiros automaticamente!
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
                                    const cdValEnc = encodeInt(cdVal);
                                    const skIdEnc = encodeInt(1100);
                                    const skillUpdatePacket = `e${playerId}:SkillDataUpdate,(${encodeInt(31102)},((${skIdEnc},0,0,0,${encodeInt(1)},${cdValEnc},0,0,0,0,${cdValEnc})))\n`;
                                    socket.write(skillUpdatePacket);
                                    console.log(`[🔋 POST-TRANSFORMATION CD STARTED] Started CD of ${cdVal}ms for skill 1100 (Player: ${playerId})`);
                                }

                                // Broadcast SkillParmsUpdate para sincronizar estado de bat de Dragos ou outras habilidades customizadas nos outros clientes
                                const paramsEnc = skillParmsRaw.map(val => {
                                    if (typeof val === 'number') return encodeInt(val);
                                    if (Array.isArray(val)) return `(${val.map(v => typeof v === 'number' ? encodeInt(v) : v).join(',')})`;
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
                            if (socket.isStunned || socket.isFrozen) {
                                console.log(`[🚫 CAST BLOCKED] Jogador ${playerId} está atordoado/congelado e não pode usar habilidades!`);
                                sendSystemChat(socket, "⚠️ Você está atordoado/congelado e não pode usar habilidades!");
                                return;
                            }
                            if (socket.activeBuffs && socket.activeBuffs[1015]) {
                                console.log(`[🚫 CAST BLOCKED] Jogador ${playerId} está no casulo e não pode usar habilidades!`);
                                sendSystemChat(socket, "⚠️ Você está no casulo e não pode usar habilidades!");
                                return;
                            }
                            try {
                                let skillArgs = parseArrayList(argsStr);
                                if (skillArgs.length === 1 && Array.isArray(skillArgs[0])) {
                                    skillArgs = skillArgs[0];
                                }
                                
                                // NÃO decodificamos mais aqui, pois parseArrayList já realiza a decodificação de Base62 para inteiros automaticamente!
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
                                
                                const skillIdEnc = encodeInt(skillId);
                                const xEnc = encodeInt(vec[0] || 0);
                                const yEnc = encodeInt(vec[1] || 0);
                                const zEnc = encodeInt(vec[2] || 0);
                                
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

                                        const shieldIdEnc = encodeInt(shieldId);
                                        const hpEnc = encodeInt(hp);
                                        const maxHpEnc = encodeInt(maxHp);
                                        const creatorIdEnc = encodeInt(creatorId);
                                        const veCreartPosEnc = `(${encodeInt(veCreartPos[0])},${encodeInt(veCreartPos[1])},${encodeInt(veCreartPos[2])})`;
                                        const quaposEnc = `(${encodeInt(quapos[0])},${encodeInt(quapos[1])},${encodeInt(quapos[2])})`;
                                        
                                        targetsStr = `${shieldIdEnc},${hpEnc},${maxHpEnc},${dateStr},${creatorIdEnc},${veCreartPosEnc},${quaposEnc}`;
                                    } else {
                                        const netIdEnc = encodeInt(netId);
                                        const skIdEnc = encodeInt(skId);
                                        const veCreartPosEnc = `(${encodeInt(veCreartPos[0])},${encodeInt(veCreartPos[1])},${encodeInt(veCreartPos[2])})`;
                                        const quaposEnc = `(${encodeInt(quapos[0])},${encodeInt(quapos[1])},${encodeInt(quapos[2])})`;
                                        targetsStr = `${netIdEnc},${skIdEnc},0,0,0,${veCreartPosEnc},${quaposEnc}`;
                                    }
                                    socket.pendingSkillParms = null; // limpa o cache
                                } else {
                                    if (Array.isArray(targets)) {
                                        targetsStr = targets.map(t => encodeInt(t)).join(",");
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
                                     const buffDateStr = Fun.GetServerDate();

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
                                             applyBuffToEntity(socket, playerId, buffId, duration);
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
                                                 applyBuffToEntity(socket, targetId, buffId, duration);
                                             });
                                         });
                                     }
                                 }

                                 // 2. Envia o SkillDataUpdate para iniciar o cooldown e travar spam
                                 let actualCd = 10000; // Fallback default de 10s
                                 if (playerSkillsMap[skillId] !== undefined) {
                                     actualCd = playerSkillsMap[skillId].cd;
                                 }

                                 if (socket.nocd) {
                                     actualCd = 0;
                                 }

                                 const actualCdEnc = encodeInt(actualCd);
                                 const skillUpdatePacket = `e${playerId}:SkillDataUpdate,(${encodeInt(31102)},((${skillIdEnc},0,0,0,${encodeInt(1)},${actualCdEnc},0,0,0,0,${actualCdEnc})))\n`;

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
                                     const zeroEnc = encodeInt(0);
                                     const nocdUpdatePacket = `e${playerId}:SkillDataUpdate,(${encodeInt(31102)},((${skillIdEnc},0,${encodeInt(1)},0,${encodeInt(1)},${zeroEnc},0,0,0,0,${zeroEnc})))\n`;
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
                                const cdArgs = parseArrayList(argsStr);
                                // NÃO decodificamos mais aqui, pois parseArrayList já realiza a decodificação de Base62 para inteiros automaticamente!
                                const skillId = cdArgs[0];
                                const skillIdEnc = encodeInt(skillId);
                                const one = encodeInt(1);
                                
                                const skillUpdatePacket = `e${playerId}:SkillDataUpdate,(${encodeInt(31102)},((${skillIdEnc},0,${one},0,${one},0,0,0,0,0,0)))\n`;
                                socket.write(skillUpdatePacket);
                                console.log(`[🔋 SKILL CD END REFILLED]: ${skillUpdatePacket.trim()}`);
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 31104 skill CD end]:", e);
                            }
                        }

                        // Se for 31208 (MSG_PLAYERSKILL_SUPPERSKILL - Ativação da Ultimate Q)
                        if (cmdId === 31208) {
                            if (socket.isStunned || socket.isFrozen) {
                                console.log(`[🚫 ULT CAST BLOCKED] Jogador ${playerId} está atordoado/congelado e não pode usar a Ultimate!`);
                                sendSystemChat(socket, "⚠️ Você está atordoado/congelado e não pode usar a Ultimate!");
                                return;
                            }
                            if (socket.activeBuffs && socket.activeBuffs[1015]) {
                                console.log(`[🚫 ULT CAST BLOCKED] Jogador ${playerId} está no casulo e não pode usar a Ultimate!`);
                                sendSystemChat(socket, "⚠️ Você está no casulo e não pode usar a Ultimate!");
                                return;
                            }
                            try {
                                console.log(`[🚀 ULTIMATE START] Jogador ativou a Ultimate! Agradecendo e ativando CDazhao...`);
                                const d50001 = encodeInt(50001);
                                const d50005 = encodeInt(50005);
                                const one = encodeInt(1);
                                
                                // Determina dinamicamente a duração da Ultimate baseada no HeroID do socket
                                const currentHero = socket.currentHeroId || 1015;
                                const dazhaoList = heroDazhaoMap[currentHero] || [];
                                let duration = 3000; // Fallback default
                                if (dazhaoList.length > 0) {
                                    // Pega o continuestime do primeiro dazhao
                                    duration = dazhaoList[0].continueTime || 3000;
                                }
                                
                                const durationEnc = encodeInt(duration); 
                                
                                // 1. Mostra a barra de duração do Dazhao no cliente de todos
                                const ultStartPacket = `e${playerId}:SendMsgToAllClient,(${d50001},(${d50005},(${one},'',${durationEnc})))\n`;
                                activeSockets.forEach(s => {
                                    if (s.socketType === "SCENE" && !s.destroyed) {
                                        s.write(ultStartPacket);
                                    }
                                });
                                
                                // 2. Aciona a execução da Ultimate (SuperSkillUpdate) no cliente para animar e castar a habilidade!
                                const zero = encodeInt(0);
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
                                             applyBuffToEntity(socket, playerId, buffId, duration);
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
                                                      applyBuffToEntity(socket, s.playerId, 1005, duration);
                                                  }
                                              }
                                          });
                                          
                                          // 2. NPCs Inimigos
                                          for (const npcId in spawnedNpcs) {
                                              const npc = spawnedNpcs[npcId];
                                              if (npc && !npc.destroyed) {
                                                  const enemyForce = npc.force !== undefined ? npc.force : 2;
                                                  if (Number(casterForce) !== Number(enemyForce)) {
                                                      applyBuffToEntity(socket, npcId, 1005, duration);
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
                                        cleanupBuff(socket, playerId, buffId, true);
                                    });
                                    socket.activeUltimateBuffs = [];
                                }
                                const d50001 = encodeInt(50001);
                                const d50005 = encodeInt(50005);
                                const zero = encodeInt(0);
                                
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
                                const parsed = parseArrayList(argsStr);
                                const state = parsed[0];
                                const p2010000 = encodeInt(2010000);
                                const p2010002 = encodeInt(2010002);
                                const stateEnc = encodeInt(state);
                                const energyEnc = encodeInt(10000);
                                const maxEnergyEnc = encodeInt(10000);
                                
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
                                 const parsed = parseArrayList(argsStr);
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
                        if (cmdId === 2100001) {
                            try {
                                const parsed = parseArrayList(argsStr);
                                const type = parsed[0];
                                const p2010002 = encodeInt(2010002);
                                const playerIdEnc = encodeInt(playerId);
                                const typeEnc = encodeInt(type);
                                const energyEnc = encodeInt(4000);
                                const maxEnergyEnc = encodeInt(4000);
                                
                                const resp = `e${playerId}:SendMsgToAllClient,(${p2010002},(${playerIdEnc},(${typeEnc},${energyEnc},${maxEnergyEnc})))\n`;
                                activeSockets.forEach(s => {
                                    if (s.socketType === "SCENE" && !s.destroyed) {
                                        s.write(resp);
                                    }
                                });
                                console.log(`[🦊 DAJI SHIELD SYNC]: ${resp.trim()}`);
                            } catch (e) {
                                console.error("[⚠️ ERRO ao processar 2100001 DaJi Shield]:", e);
                            }
                        }

                        // Se for 1010001 (MSG_CREATENPC - Criação de Entidades/NPCs via Habilidade)
                        if (cmdId === 1010001) {
                            try {
                                const parsed = parseArrayList(argsStr);
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
                                const npcConf = npcData[npcIndex] || { hp: 100, shield: 0, name: "NPC", time: 0, weaponIndex: 0 };
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
                                const spawnPacket = `c${className}:${encodeInt(npcId)},("Initialize",(${encodeInt(momId)},${encodeInt(npcId)},${encodeInt(npcIndex)},${encodeInt(npcForce)},(${encodeInt(x)},${encodeInt(y)},${encodeInt(z)}),(${encodeInt(rx)},${encodeInt(ry)},${encodeInt(rz)}),(${encodeInt(sx)},${encodeInt(sy)},${encodeInt(sz)}),(${encodeInt(fx)},${encodeInt(fy)},${encodeInt(fz)}),${encodeInt(targetId)},${encodeInt(maxHp)},${encodeInt(npcTime)},${encodeInt(1)},${encodeInt(momType)},'${colliderName}'),((),()),0)\n`;
                                
                                activeSockets.forEach(s => {
                                    if (s.socketType === "SCENE" && !s.destroyed) {
                                        s.write(spawnPacket);
                                    }
                                });
                                console.log(`[🤖 ENTITY SPAWNED]: ${spawnPacket.trim()}`);

                                // ⏳ LIFETIME TIMER PARA ENTIDADES COM TEMPO DEFINIDO (Time > 0)
                                if (npcTime > 0) {
                                    setTimeout(() => {
                                        if (spawnedNpcs[npcId] !== undefined) {
                                            const targetNpc = spawnedNpcs[npcId];
                                            delete spawnedNpcs[npcId];
                                            const destPacket = `d${encodeInt(npcId)}\n`;
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
                                                    creatorSocket.write(`e${creatorSocket.playerIndex || 1}:SendMsgToClient,(${encodeInt(20014)},(${encodeInt(90008)},(),${encodeInt(1)}))\n`);
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
                                const parsed = parseArrayList(argsStr);
                                const npcId = parsed[0];
                                console.log(`[🤖 ENTITY SUICIDE] Npc ID: ${npcId} suicided/detonated!`);
                                
                                // Remove from tracking if present
                                if (spawnedNpcs[npcId] !== undefined) {
                                    const targetNpc = spawnedNpcs[npcId];
                                    if (targetNpc.index === 1013) {
                                        const creatorSocket = activeSockets.find(s => s.socketType === "SCENE" && !s.destroyed && Number(s.playerId) === Number(targetNpc.masterId));
                                        if (creatorSocket) {
                                            creatorSocket.write(`e${creatorSocket.playerId}:SuperSkillFinish\n`);
                                            creatorSocket.write(`e1:SendMsgToClient,(${encodeInt(20014)},(${encodeInt(90008)},(),${encodeInt(1)}))\n`);
                                            console.log(`[❄️ SNOWBALL RESET] Scarlett Camera reset on suicide: e${creatorSocket.playerId}:SuperSkillFinish`);
                                        }
                                    }
                                    delete spawnedNpcs[npcId];
                                }

                                // Envia a destruição da entidade diretamente para todos os clientes ativos
                                const destPacket = `d${encodeInt(npcId)}\n`;
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
                                const parsed = parseArrayList(argsStr);
                                const npcId = parsed[0];
                                console.log(`[🤖 ENTITY SYNC] Validando sincronização do NPC ID: ${npcId}`);

                                // Envia a confirmação de validação de sucesso de volta
                                const p1020002 = encodeInt(1020002);
                                const npcIdEnc = encodeInt(npcId);
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
                                const parsed = parseArrayList(argsStr);
                                const subCmd = parsed[0]; // 90002
                                if (subCmd === 90002) {
                                    const selectedHeroId = parsed[1];
                                    const isLocked = parsed[2] || 0; // 0 = preview, 1 = locked
                                    
                                    console.log(`[🎭 SELEÇÃO DE HERÓI] Jogador ${socket.playerNameEnc} selecionou HeroID ${selectedHeroId} (Locked: ${isLocked})`);
                                    
                                    if (isLocked === 1) {
                                        switchPlayerHero(socket, selectedHeroId);
                                    } else {
                                        // Apenas atualiza a visualização/preview do herói na tela
                                        socket.currentHeroId = selectedHeroId;
                                        
                                        const p20014 = encodeInt(20014);
                                        const s90002 = encodeInt(90002);
                                        const zero = encodeInt(0);
                                        const one = encodeInt(1);
                                        const forceEnc = encodeInt(socket.force || 1);
                                        const isReadyEnc = encodeInt(0); // 0 porque é apenas preview
                                        const heroIdEnc = encodeInt(selectedHeroId);
                                        const isCapEnc = encodeInt(socket.playerIndex === 1 ? 1 : 0);
                                        
                                        const prerogativeLobby = `(${isCapEnc},${isCapEnc},${isCapEnc},${isCapEnc},'${socket.playerNameEnc}',${zero},'',${zero})`;
                                        const userDataHeroInfo = `(${heroIdEnc},${one},${one},${one},${zero})`;
                                        const myUserData = `('${socket.playerNameEnc}',${encodeInt(99)},${one},${forceEnc},${isReadyEnc},${one},${heroIdEnc},${one},${one},${zero},${one},'Local',${zero},${userDataHeroInfo},${one},${zero},${zero},${isCapEnc},${prerogativeLobby},())`;
                                        
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
                                const parsed = parseArrayList(argsStr);
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
                                const p52002 = encodeInt(52002);
                                const stateEnc = encodeInt(state);
                                
                                activeSockets.forEach(s => {
                                    if (s.socketType === "SCENE" && !s.destroyed) {
                                        const playerNetIdEnc = encodeInt(playerNetId);
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
                                const parsed = parseArrayList(argsStr);
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

                                const p2000 = encodeInt(2000);
                                const p2001 = encodeInt(2001);
                                const force = encodeInt(socket.force || 1); // Faction dinâmica do jogador
                                const netId = encodeInt(socket.playerIndex || 1); // NetID absoluto dinâmico

                                // msg: (text_hex, sound, anim, type, ico)
                                const msgList = `('${msgArray[0] || ""}','${sound}','${anim}',${encodeInt(type)},'${ico}')`;

                                // value: (cdReady, onGround, bePlayActive, nHuDongState)
                                const valList = `(${encodeInt(cdReady)},${encodeInt(onGround)},${encodeInt(bePlayActive)},${encodeInt(nHuDongState)})`;

                                // userChatParam
                                const ucp = parsed[6] || [];
                                const ucpList = `('${ucp[0] || ""}','${ucp[1] || ""}',${encodeInt(ucp[2] || 0)})`;

                                // szNetMsg = (nPinDao, msg_0, msg_1, (), msg, value, userChatParam)
                                const szNetMsg = `(${encodeInt(nPinDao)},'${parsed[1] || ""}','${parsed[2] || ""}',(),${msgList},${valList},${ucpList})`;

                                // szData = (szNetMsg, sound, anim, type, type, force, netId, ico, cdReady, onGround, bePlayActive, nHuDongState)
                                const szData = `(${szNetMsg},'${sound}','${anim}',${encodeInt(type)},${encodeInt(type)},${force},${netId},'${ico}',${encodeInt(cdReady)},${encodeInt(onGround)},${encodeInt(bePlayActive)},${encodeInt(nHuDongState)})`;

                                activeSockets.forEach(s => {
                                    if (s.socketType === "SCENE" && !s.destroyed) {
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
                                const parsed = parseArrayList(argsStr);
                                const nPinDao = parsed[0];
                                const hexMsg = parsed[1] || "";
                                const targetUser = parsed[2] || "";
                                const ucp = parsed[3] || [];

                                const p1006 = encodeInt(1006);
                                const pPinDao = encodeInt(nPinDao);
                                const pCid = encodeInt(socket.playerIndex || 1); // NetID absoluto dinâmico do remetente

                                const senderNameHex = socket.playerNameEnc || PLAYER_NAME_ENC;
                                const msgHex = hexMsg;

                                const ucpList = `('${ucp[0] || ""}','${ucp[1] || ""}',${encodeInt(ucp[2] || 0)})`;
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
                                             const currentMapId = socket.targetMapId || lastMapId || 5202;
                                             const defaultCoords = getSpawnCoordinates(currentMapId, socket.force);
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
                                                const npcConf = npcData[npcIndex] || {};
                                                npcMaxHp = parseInt(npcConf.hp) || 500;
                                            }

                                            spawnedNpcs[botNetId] = { id: botNetId, index: npcIndex, hp: npcMaxHp, maxHp: npcMaxHp, x: spawnX, y: spawnY, z: spawnZ, isPracticeDummy: true };
                                            const className = getEntityClassName(npcIndex);
                                            const spawnPacket = `c${className}:${encodeInt(botNetId)},("Initialize",(${encodeInt(1)},${encodeInt(botNetId)},${encodeInt(npcIndex)},${encodeInt(2)},(${encodeInt(spawnX)},${encodeInt(spawnY)},${encodeInt(spawnZ)}),(0,0,0),(${encodeInt(100)},${encodeInt(100)},${encodeInt(100)}),(0,0,0),${encodeInt(-1)},${encodeInt(npcMaxHp)},${encodeInt(0)},${encodeInt(1)},${encodeInt(0)},""),((),()),0)\n`;
                                            socket.write(spawnPacket);
                                            console.log(`[🤖 NPC SPAWN VIA COMANDO]: ${spawnPacket.trim()}`);

                                            // Envia mensagem do sistema amarela no canal 8 confirmando o spawn
                                            const sysHex = Buffer.from(`[Sistema] Bot ${botNetId} (HP: ${npcMaxHp}) spawnado na sua frente!`).toString('hex');
                                            const sysNetMsg = `(${encodeInt(8)},'','${sysHex}','',(),(),${pCid},('','',0),())`;
                                            const sysBroadcast = `e1:SendMsgToClient,(${p1006},${sysNetMsg})\n`;
                                            socket.write(sysBroadcast);
                                        } else if (cmdName === 'hero' || cmdName === 'heroi' || cmdName === 'char') {
                                             const selectedHeroId = parseInt(cmdArg);
                                             if (selectedHeroId) {
                                                 switchPlayerHero(socket, selectedHeroId);
                                                 
                                                 const sysHex = Buffer.from(`[Sistema] Mudando de personagem para o ID ${selectedHeroId}!\n`).toString('hex');
                                                 const sysNetMsg = `(${encodeInt(8)},'','${sysHex}','',(),(),${pCid},('','',0),())`;
                                                 const sysBroadcast = `e1:SendMsgToClient,(${p1006},${sysNetMsg})\n`;
                                                 socket.write(sysBroadcast);
                                             }
                                         } else if (cmdName === 'god') {
                                             socket.godMode = !socket.godMode;
                                             if (socket.godMode) {
                                                 socket.hp = socket.maxHp || 2000;
                                                 socket.shield = 50000;
                                                 socket.isInvincible = true;
                                                 sendEntityHpUpdate(socket, playerId, socket, true);
                                                 applyBuffToEntity(socket, playerId, 1008, 3600000);
                                                 sendSystemChat(socket, "🟢 [Mod Menu] MODO DEUS ATIVADO! Você está invencível e com escudo infinito.");
                                             } else {
                                                 socket.shield = 0;
                                                 socket.isInvincible = false;
                                                 sendEntityHpUpdate(socket, playerId, socket, true);
                                                 cleanupBuff(socket, playerId, 1008, true);
                                                 sendSystemChat(socket, "🔴 [Mod Menu] MODO DEUS DESATIVADO! Vulnerabilidade restaurada.");
                                             }
                                          } else if (cmdName === 'invisible' || cmdName === 'inv') {
                                              socket.isInvis = !socket.isInvis;
                                              if (socket.isInvis) {
                                                  applyBuffToEntity(socket, playerId, 1012, 3600000);
                                                  sendSystemChat(socket, "🟢 [Mod Menu] INVISIBILIDADE ATIVADA! Os inimigos não podem te ver.");
                                              } else {
                                                  cleanupBuff(socket, playerId, 1012, true);
                                                  sendSystemChat(socket, "🔴 [Mod Menu] INVISIBILIDADE DESATIVADA!");
                                              }
                                          } else if (cmdName === 'cd' || cmdName === 'nocd') {
                                              socket.nocd = !socket.nocd;
                                              if (socket.nocd) {
                                                  sendSystemChat(socket, "🟢 [Mod Menu] ZERO COOLDOWN ATIVADO! Use suas habilidades sem limites.");
                                              } else {
                                                  sendSystemChat(socket, "🔴 [Mod Menu] ZERO COOLDOWN DESATIVADO!");
                                              }
                                          } else if (cmdName === 'heal') {
                                              socket.hp = socket.maxHp || 2000;
                                              socket.shield = 1000; socket.maxShield = 1000;
                                              sendEntityHpUpdate(socket, playerId, socket, true);
                                              sendSystemChat(socket, "💖 [Mod Menu] VIDA E ESCUDO RESTAURADOS COMPLETAMENTE!");
                                          } else if (cmdName === 'speed') {
                                              socket.superSpeed = !socket.superSpeed;
                                              if (socket.superSpeed) {
                                                  applyBuffToEntity(socket, playerId, 1023, 3600000);
                                                  sendSystemChat(socket, "⚡ [Mod Menu] SUPER VELOCIDADE ATIVADA! (Buff 1023 aplicado)");
                                              } else {
                                                  cleanupBuff(socket, playerId, 1023, true);
                                                  sendSystemChat(socket, "⚡ [Mod Menu] VELOCIDADE RESTAURADA AO NORMAL.");
                                              }
                                          } else if (cmdName === 'kill' || cmdName === 'clear') {
                                              let count = 0;
                                              for (const npcId in spawnedNpcs) {
                                                  if (spawnedNpcs[npcId]) {
                                                      socket.write(`d${encodeInt(npcId)}\n`);
                                                      delete spawnedNpcs[npcId];
                                                      count++;
                                                  }
                                              }
                                              sendSystemChat(socket, `💀 [Mod Menu] Todos os ${count} bot(s) de treino foram eliminados/limpos!\n`);
                                          } else if (cmdName === 'team' || cmdName === 'time' || cmdName === 'lado') {
                                             if (!currentRoom) {
                                                 sendSystemChat(socket, "⚠️ Você precisa estar em uma sala para usar este comando!");
                                             } else {
                                                 let pSlot = -1;
                                                 for (let i = 0; i < 10; i++) {
                                                     if (currentRoom.players[i] && currentRoom.players[i].name === socket.playerName) {
                                                         pSlot = i;
                                                         break;
                                                     }
                                                 }
                                                 if (pSlot === -1) {
                                                     sendSystemChat(socket, "⚠️ Erro: Você não foi encontrado na sala!");
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
                                                         broadcastToRoom(currentRoom, `e1:SendMsgToHallClient,(${encodeInt(20013)},(${encodeInt(3318)},(1,${buildRoomUserList(currentRoom)})))\n`);
                                                         
                                                         const teamName = targetIsRed ? 'Vermelho' : 'Azul';
                                                         sendSystemChat(socket, `🟢 Você foi movido para o Time ${teamName} (Vaga ${absoluteIdx})!`);
                                                     } else {
                                                         const teamName = targetIsRed ? 'Vermelho' : 'Azul';
                                                         sendSystemChat(socket, `🔴 O Time ${teamName} está cheio!`);
                                                     }
                                                 }
                                             }
                                          } else if (cmdName === 'buff') {
                                             const buffId = parseInt(cmdArg);
                                             const customDur = parseInt(commandParts[2]) || 10000;
                                             if (!isNaN(buffId)) {
                                                 if (buffsMap[buffId]) {
                                                     applyBuffToEntity(socket, playerId, buffId, customDur);
                                                     sendSystemChat(socket, `💪 [Mod Menu] Aplicou Buff "${buffsMap[buffId].name}" (${buffId}) por ${customDur}ms.`);
                                                 } else {
                                                     sendSystemChat(socket, `⚠️ [Mod Menu] Buff ID ${buffId} não existe no buff.xml!`);
                                                 }
                                             } else {
                                                 sendSystemChat(socket, `⚠️ [Mod Menu] Uso correto: /buff [ID] [Duração em MS]`);
                                             }
                                         } else if (cmdName === 'debuff') {
                                             const debuffId = parseInt(cmdArg);
                                             const customDur = parseInt(commandParts[2]) || 10000;
                                             if (!isNaN(debuffId)) {
                                                 if (buffsMap[debuffId]) {
                                                     applyBuffToEntity(socket, playerId, debuffId, customDur);
                                                     sendSystemChat(socket, `🔥 [Mod Menu] Aplicou Debuff "${buffsMap[debuffId].name}" (${debuffId}) por ${customDur}ms.`);
                                                 } else {
                                                     sendSystemChat(socket, `⚠️ [Mod Menu] Debuff ID ${debuffId} não existe no buff.xml!`);
                                                 }
                                             } else {
                                                 sendSystemChat(socket, `⚠️ [Mod Menu] Uso correto: /debuff [ID] [Duração em MS]`);
                                             }
                                         } else if (cmdName === 'help' || cmdName === 'ajuda' || cmdName === '?') {
                                             const godState = socket.godMode ? "🟢 ATIVO" : "🔴 INATIVO";
                                             const cdState = socket.nocd ? "🟢 ATIVO" : "🔴 INATIVO";
                                             const speedState = socket.superSpeed ? "🟢 ATIVO" : "🔴 INATIVO";
                                             const invisState = socket.isInvis ? "🟢 ATIVO" : "🔴 INATIVO";

                                             sendSystemChat(socket, "========================================");
                                             sendSystemChat(socket, "🛠️ MOD MENU GM - COMANDOS DISPONÍVEIS:");
                                             sendSystemChat(socket, `• /god        - Modo Deus (Invencível + Escudo) [Status: ${godState}]`);
                                             sendSystemChat(socket, `• /nocd       - Sem Cooldown de Habilidades  [Status: ${cdState}]`);
                                             sendSystemChat(socket, `• /speed      - Super Velocidade (Buff 1023) [Status: ${speedState}]`);
                                             sendSystemChat(socket, `• /invisible  - Invisibilidade (Buff 1012)   [Status: ${invisState}]`);
                                             sendSystemChat(socket, "• /heal       - Recupera Vida e Escudo ao Máximo");
                                             sendSystemChat(socket, "• /bot [ID]   - Spawna Bot de Treino na sua frente");
                                             sendSystemChat(socket, "• /hero [ID]  - Troca de Herói instantaneamente");
                                             sendSystemChat(socket, "• /kill /clear - Remove/Elimina todos os Bots spawnados");
                                             sendSystemChat(socket, "• /buff [ID] [MS] - Aplica qualquer Buff do buff.xml");
                                             sendSystemChat(socket, "• /debuff [ID] [MS] - Aplica qualquer Debuff do buff.xml");
                                             sendSystemChat(socket, "• /help       - Mostra esta lista de ajuda");
                                             sendSystemChat(socket, "========================================");
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
                            const parsed = parseArrayList(argsStr);
                            
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
                            
                            let resolvedWeaponId = rawWeaponId ? decodeInt(rawWeaponId) : null;
                            let resolvedShootname = rawShootname ? decodeInt(rawShootname).toString() : null;

                            // Normalização/mapeamento para NPCs/entidades invocadas por habilidades
                            const npcMappingId = resolvedWeaponId || (resolvedShootname ? Number(resolvedShootname) : null);
                            if (npcMappingId === 1005 && resolvedShootname === "1") {
                                resolvedWeaponId = 9996;
                                resolvedShootname = "1";
                            } else if (npcMappingId === 1025 && resolvedShootname === "1") {
                                resolvedWeaponId = 9984;
                                resolvedShootname = "1";
                            } else if (npcMappingId === 1021 && resolvedShootname === "1") {
                                resolvedWeaponId = 9988;
                                resolvedShootname = "1";
                            } else if (npcMappingId === 1011 && resolvedShootname === "1") {
                                resolvedWeaponId = 9992;
                                resolvedShootname = "1";
                            } else if (npcMappingId === 1012 && resolvedShootname === "1") {
                                resolvedWeaponId = 9980;
                                resolvedShootname = "1";
                            } else if (npcMappingId === 1014 && resolvedShootname === "1") {
                                resolvedWeaponId = 9993;
                                resolvedShootname = "1";
                            } else if (npcMappingId === 1023 && resolvedShootname === "4") {
                                resolvedWeaponId = 1023;
                                resolvedShootname = "4";
                            } else if (npcMappingId >= 1026 && npcMappingId <= 1035 && resolvedShootname === "1") {
                                resolvedWeaponId = 9000 + (npcMappingId - 1026);
                                resolvedShootname = "1";
                            }

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

                             console.log(`[🔬 SDG DIAG] playerId=${playerId} | weaponId=${weaponId} | sn=${shootname} | baseDamage=${baseDamage} | hits=${hits.length} | raw14=${parsed[14]} | raw1=${parsed[1]}`);
                            for (const dmg of hits) {
                                 const rawAttackerId = dmg[0];
                                 const rawTargetId = dmg[1];
                                 const rawBoomHead = dmg[2];
                                 const rawDistance = dmg[3];
                                 const collName = dmg[8];

                                 const attackerId = rawAttackerId !== undefined ? decodeInt(rawAttackerId) : null;
                                 const targetId = rawTargetId !== undefined ? decodeInt(rawTargetId) : null;
                                 const boomHead = rawBoomHead !== undefined ? decodeInt(rawBoomHead) : 0;
                                 const distance = rawDistance !== undefined ? decodeInt(rawDistance) : 0;
                                 
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
                                        sendEntityHpUpdate(target, targetId, target, isPlayerTarget);
                                        console.log(`[💖 VENUS HEAL] Curou o Alvo: ${targetId} em +${healAmount} HP. Novo HP: ${target.hp}`);
                                        continue;
                                    }

                                    if (isLilyHeal) {
                                        const healAmount = 250;
                                        target.hp = Math.min(target.maxHp || 2000, (target.hp !== undefined ? target.hp : (target.maxHp || 2000)) + healAmount);
                                        sendEntityHpUpdate(target, targetId, target, isPlayerTarget);
                                        console.log(`[💖 LILY HEAL] Curou o Alvo: ${targetId} em +${healAmount} HP. Novo HP: ${target.hp}`);
                                        continue;
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
                                            sendEntityHpUpdate(target, targetId, target, isPlayerTarget);
                                            console.log(`[💖 ANGEL ACTIVE HEAL] Curou o Aliado: ${targetId} em +${healAmount} HP (Shootname: ${loopShootname}). Novo HP: ${target.hp}`);
                                            continue;
                                        } else {
                                            console.log(`[💥 ANGEL ACTIVE DAMAGE] Bola de cura atingiu Inimigo: ${targetId} | WeaponId: ${loopWeaponId} | Shootname: ${loopShootname}. Causando dano.`);
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
                                        let place = 0;
                                        let originalDamage = damageAmount;
                                        let shieldAbsorbed = 0;

                                        // 2. Absorção do Escudo (Shield) para Jogadores
                                        if (target.shield && target.shield > 0) {
                                             shieldAbsorbed = damageAmount;
                                             target.shield = Math.max(0, target.shield - damageAmount);
                                             if (target.shield === 0) {
                                                 target.maxShield = 0;
                                             }
                                             damageAmount = 0; // Invulnerabilidade de HP enquanto o escudo durar
                                             place = 2; // Shield hit marker
                                            console.log(`[🛡️ ESCUDO ABSORVEU] ${shieldAbsorbed} de dano absorvido pelo escudo de ${targetId}. Restante do escudo: ${target.shield}`);
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
                                        sendEntityDamageUpdate(targetId, target, originalDamage, attackerId, place, weaponId);
                                        
                                        if (target.hp <= 0) {
                                            handlePlayerDeath(target, attackerSocket);
                                        }
                                    } else {
                                         let place = 0;
                                         if (boomHead === 1 && canHeadshot) {
                                             place = 1;
                                         }
                                         handleNpcHurt(target, targetId, damageAmount, attackerId, place, weaponId);
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
                    s.write(`d${encodeInt(relativePlayerIndex)}\n`);
                    console.log(`[🌐 SCENE] Sent destroy signal d${relativePlayerIndex} (Player Disconnected: ${socket.playerName}) to ${s.playerName}`);
                }
            });
            
            const lobbyMatch = activeSockets.find(s => s.socketType === "LOBBY" && s.remoteAddress === socket.remoteAddress);
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
    });
    socket.on('error', (err) => {
        console.log(`[⚠️ ERRO]:`, err.message);
    });
});

tcpServer.listen(tcpPort, () => {
    console.log(`[🎮 SERVIDOR DO JOGO] Ouvindo TCP na porta ${tcpPort}...`);
});

// Intervalo global de 1 segundo para cura passiva da Angel (Círculo de Cura - estilo Lucio do Overwatch)
setInterval(() => {
    activeSockets.forEach(socket => {
        if (socket.socketType === "SCENE" && !socket.destroyed && (socket.currentHeroId === 1029 || String(socket.currentHeroId) === "1029")) {
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
                        sendEntityHpUpdate(target, targetId, target, isPlayerTarget);
                        console.log(`[💖 ANGEL CIRCLE HEAL] Angel ${socket.playerId} curou Alvo: ${targetId} em +${healAmount} HP. Novo HP: ${target.hp}`);
                    }
                });
            }
        }
    });
}, 1000);
