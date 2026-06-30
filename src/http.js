// Módulo HTTP e Express do servidor Trigger Run

const express = require('express');
const http = require('http');
const fs = require('fs');
const path = require('path');

const config = require('./config');
const state = require('./state');
const db = require('./database');
const { encodeInt, decodeInt, encodeHex, decodeHex, Fun } = require('./utils');
const gameLogic = require('./gameLogic');
const dbPersistence = require('./dbPersistence');

const app = express();

app.use(express.json());
app.use(express.urlencoded({ extended: true }));

// Log de requisições HTTP
app.use((req, res, next) => {
    console.log(`[🌐 JOGO PEDIU HTTP]: ${req.method} ${req.url}`);
    next();
});

// QQ API Mock (A Catraca do Admin)
app.get('/qqapi/GameGetMsgFromadmin.aspx', (req, res) => {
    res.send("||");
});

// Rotas antigas do Launcher
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

// CDN Proxy (Evitar travamento nos 403)
app.use(['/res', '/scene'], (req, res) => {
    const basePath = req.baseUrl; 

    // Se o jogo pedir arquivos XML, serve localmente da pasta public/baxi/
    const pathLower = req.path.toLowerCase();
    
    // Intercepta requisições de spray do Icon/painticon para servir arquivos personalizados locais (.png ou .jpg)
    if (pathLower.includes('icon/painticon/')) {
        const filename = path.basename(req.path, '.txt');
        const customPngPath = path.join(config.customSpraysDir, `${filename}.png`);
        const customJpgPath = path.join(config.customSpraysDir, `${filename}.jpg`);
        
        if (fs.existsSync(customPngPath)) {
            console.log(`[🎨 AUTOMATED SPRAY] Servindo spray customizado (PNG): ${customPngPath}`);
            return res.sendFile(customPngPath);
        }
        if (fs.existsSync(customJpgPath)) {
            console.log(`[🎨 AUTOMATED SPRAY] Servindo spray customizado (JPG): ${customJpgPath}`);
            return res.sendFile(customJpgPath);
        }
    }

    if (pathLower.endsWith('.xml')) {
        const parts = req.path.split('/');
        const filename = parts[parts.length - 1]; 

        const localPath = path.join(config.publicDir, 'baxi', filename);
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
        const mapEntry = db.filesVersionMap[relativeFilePath];
        if (mapEntry) {
            finalPath = `${basePath}/${mapEntry.version}/StreamingAssets/${mapEntry.exactPath}`;
            console.log(`[📦 MAPPED ASSET] Traduzido ${req.path} -> Versão CDN: ${mapEntry.version} | Casing Exato: ${mapEntry.exactPath}`);
        }
    }

    const queryPart = urlParts[1] ? `?${urlParts[1]}` : '';
    let cdnUrl = `http://trig.2cdn.xyz/TRIG/res1${finalPath}${queryPart}`;

    const localCachePath = path.join(config.cdnCacheDir, finalPath);

    // 1. Verifica cache local antes de ir ao CDN
    if (fs.existsSync(localCachePath)) {
        console.log(`[🎯 CACHE LOCAL SERVIDO]: ${req.path} -> ${localCachePath}`);
        return res.sendFile(localCachePath);
    }

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

                // Garante que o diretório de destino existe
                fs.mkdirSync(path.dirname(localCachePath), { recursive: true });
                // Nome temporário único para evitar colisões de concorrência
                const tempCachePath = `${localCachePath}.${Date.now()}_${Math.floor(Math.random() * 100000)}.tmp`;
                const writeStream = fs.createWriteStream(tempCachePath);

                cdnRes.pipe(writeStream);
                cdnRes.pipe(res);

                let downloadSuccessful = true;

                cdnRes.on('error', (err) => {
                    downloadSuccessful = false;
                    writeStream.destroy();
                    fs.unlink(tempCachePath, () => {});
                });

                writeStream.on('finish', () => {
                    if (downloadSuccessful) {
                        fs.rename(tempCachePath, localCachePath, (err) => {
                            if (err) {
                                console.error(`[❌ CACHE RENAME ERROR] Falha ao renomear ${tempCachePath} para ${localCachePath}:`, err);
                            } else {
                                console.log(`[💾 CACHE SALVO] ${finalPath} salvo localmente.`);
                            }
                        });
                    } else {
                        fs.unlink(tempCachePath, () => {});
                    }
                });

                writeStream.on('error', (err) => {
                    downloadSuccessful = false;
                    fs.unlink(tempCachePath, () => {});
                });

            } else if ((cdnRes.statusCode === 403 || cdnRes.statusCode === 404) && attempt === 1) {
                console.warn(`[⚠️ CDN RETRY] Status ${cdnRes.statusCode} para ${targetUrl}. Tentando fallback 1.1.1...`);
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

// Pastas estáticas locais
app.use(express.static(config.publicDir));

// ==========================================
// 🎮 ADMIN CONTROL PANEL API
// ==========================================

// GET /api/state — live snapshot of entire server state
app.get('/api/state', (req, res) => {
    const players = gameLogic.getPlayersSnapshot();
    const npcs = Object.values(state.spawnedNpcs).map(n => ({
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
        phase: global.activeTdmMatch.state || 'warmup',
        blueScore: global.activeTdmMatch.blueScore || 0,
        redScore: global.activeTdmMatch.redScore || 0,
        timeLeft: global.activeTdmMatch.fightSeconds || 0,
    } : { active: false };
    res.json({ players, npcs, tdm });
});

// POST /api/hero — switch hero for a player
app.post('/api/hero', (req, res) => {
    const { playerId, heroId } = req.body;
    const socket = state.activeSockets.find(s => s.socketType === 'SCENE' && !s.destroyed && String(s.playerId) === String(playerId));
    if (!socket) return res.json({ ok: false, error: 'Jogador não encontrado ou offline.' });
    try {
        gameLogic.switchPlayerHero(socket, parseInt(heroId));
        res.json({ ok: true, message: `Herói trocado para ID ${heroId} para ${socket.playerName}.` });
    } catch(e) {
        res.json({ ok: false, error: e.message });
    }
});

// POST /api/spawn — spawn NPC near a player
app.post('/api/spawn', (req, res) => {
    const { playerId, npcIndex, count, scale } = req.body;
    const socket = state.activeSockets.find(s => s.socketType === 'SCENE' && !s.destroyed && String(s.playerId) === String(playerId));
    if (!socket) return res.json({ ok: false, error: 'Jogador não encontrado ou offline.' });
    try {
        const total = Math.max(1, Math.min(parseInt(count) || 1, 20));
        const idx = parseInt(npcIndex) || 1121;
        const npcConf = db.npcData[idx] || {};
        const npcMaxHp = parseInt(npcConf.hp) || 500;
        const spawnX = socket.lastX || 50000;
        const spawnY = socket.lastY || 20104;
        const spawnZ = socket.lastZ || 26000;
        const spawned = [];
        const scaleVal = parseInt(scale) || 100;
        for (let i = 0; i < total; i++) {
            const botNetId = 15000 + Math.floor(Math.random() * 50000);
            state.spawnedNpcs[botNetId] = { id: botNetId, index: idx, hp: npcMaxHp, maxHp: npcMaxHp, x: spawnX, y: spawnY, z: spawnZ, isPracticeDummy: true };
            const className = db.getEntityClassName(idx);
            const spawnPacket = `c${className}:${encodeInt(botNetId)},("Initialize",(${encodeInt(1)},${encodeInt(botNetId)},${encodeInt(idx)},${encodeInt(2)},(${encodeInt(spawnX)},${encodeInt(spawnY)},${encodeInt(spawnZ)}),(0,0,0),(${encodeInt(100)},${encodeInt(100)},${encodeInt(100)}),(0,0,0),${encodeInt(-1)},${encodeInt(npcMaxHp)},${encodeInt(0)},${encodeInt(1)},${encodeInt(0)},""),((),()),0)\n`;
            
            state.activeSockets.forEach(s => { if (s.socketType === 'SCENE' && !s.destroyed) s.write(spawnPacket); });
            
            if (scaleVal !== 100) {
                const scalePacket = `c${className}:${encodeInt(botNetId)},("NSS",((${encodeInt(scaleVal)},${encodeInt(scaleVal)},${encodeInt(scaleVal)})))\n`;
                state.activeSockets.forEach(s => { if (s.socketType === 'SCENE' && !s.destroyed) s.write(scalePacket); });
            }
            spawned.push(botNetId);
        }
        res.json({ ok: true, message: `${total} entidade(s) NPC ${idx} spawnada(s) com escala de ${scaleVal}%.`, ids: spawned });
    } catch(e) {
        res.json({ ok: false, error: e.message });
    }
});

// POST /api/kill_npcs — destroy all spawned NPCs
app.post('/api/kill_npcs', (req, res) => {
    let count = 0;
    for (const npcId in state.spawnedNpcs) {
        if (state.spawnedNpcs[npcId] && !state.spawnedNpcs[npcId].isPracticeDummy) {
            state.activeSockets.forEach(s => { if (s.socketType === 'SCENE' && !s.destroyed) s.write(`d${encodeInt(npcId)}\n`); });
            delete state.spawnedNpcs[npcId];
            count++;
        }
    }
    const { includeDummies } = req.body || {};
    if (includeDummies) {
        for (const npcId in state.spawnedNpcs) {
            state.activeSockets.forEach(s => { if (s.socketType === 'SCENE' && !s.destroyed) s.write(`d${encodeInt(npcId)}\n`); });
            delete state.spawnedNpcs[npcId];
            count++;
        }
    }
    res.json({ ok: true, message: `${count} NPC(s) destruído(s).` });
});

// POST /api/buff — apply/remove buff on player
app.post('/api/buff', (req, res) => {
    const { playerId, buffId, duration } = req.body;
    const socket = state.activeSockets.find(s => s.socketType === 'SCENE' && !s.destroyed && String(s.playerId) === String(playerId));
    if (!socket) return res.json({ ok: false, error: 'Jogador não encontrado.' });
    const bid = parseInt(buffId);
    const dur = parseInt(duration) || 10000;
    if (!db.buffsMap[bid]) return res.json({ ok: false, error: `Buff ID ${bid} não existe no buff.xml.` });
    try {
        gameLogic.applyBuffToEntity(socket, socket.playerId, bid, dur);
        res.json({ ok: true, message: `Buff "${db.buffsMap[bid].name}" (${bid}) aplicado por ${dur}ms em ${socket.playerName}.` });
    } catch(e) {
        res.json({ ok: false, error: e.message });
    }
});

// POST /api/godmode — toggle god mode for player
app.post('/api/godmode', (req, res) => {
    const { playerId, enabled } = req.body;
    const socket = state.activeSockets.find(s => s.socketType === 'SCENE' && !s.destroyed && String(s.playerId) === String(playerId));
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

// POST /api/heal — full heal player
app.post('/api/heal', (req, res) => {
    const { playerId } = req.body;
    const socket = state.activeSockets.find(s => s.socketType === 'SCENE' && !s.destroyed && String(s.playerId) === String(playerId));
    if (!socket) return res.json({ ok: false, error: 'Jogador não encontrado.' });
    socket.hp = socket.maxHp || 2000;
    socket.shield = socket.godMode ? 50000 : 0;
    socket.maxShield = socket.shield;
    try { gameLogic.sendEntityHpUpdate(socket, socket.playerId, socket, true); } catch(e) {}
    res.json({ ok: true, message: `${socket.playerName} foi curado ao máximo (HP: ${socket.hp}).` });
});

// POST /api/kill_player — kill a player instantly
app.post('/api/kill_player', (req, res) => {
    const { playerId } = req.body;
    const socket = state.activeSockets.find(s => s.socketType === 'SCENE' && !s.destroyed && String(s.playerId) === String(playerId));
    if (!socket) return res.json({ ok: false, error: 'Jogador não encontrado.' });
    try {
        socket.hp = 0;
        gameLogic.handlePlayerDeath(socket);
        res.json({ ok: true, message: `${socket.playerName} foi eliminado.` });
    } catch(e) {
        res.json({ ok: false, error: e.message });
    }
});

// POST /api/chat — send system chat message
app.post('/api/chat', (req, res) => {
    const { message } = req.body;
    if (!message) return res.json({ ok: false, error: 'Mensagem vazia.' });
    try {
        gameLogic.sendGlobalSystemChat(message);
        res.json({ ok: true, message: `Mensagem enviada: ${message}` });
    } catch(e) {
        res.json({ ok: false, error: e.message });
    }
});

// POST /api/map — Força a mudança de mapa em tempo real
app.post('/api/map', (req, res) => {
    const { playerId, mapId } = req.body;
    const mid = parseInt(mapId);
    
    let mapConf = db.MAP_CONFIG[mid];
    
    if (!mapConf) {
        for (const key in db.MAP_CONFIG) {
            if (String(db.MAP_CONFIG[key].id) === String(mapId)) {
                mapConf = db.MAP_CONFIG[key];
                break;
            }
        }
    }

    if (!mapConf) {
        return res.json({ ok: false, error: 'ID do mapa não encontrado na configuração do servidor.' });
    }

    let targets = [];
    if (playerId) {
        const socket = state.activeSockets.find(s => s.socketType === 'SCENE' && !s.destroyed && String(s.playerId) === String(playerId));
        if (socket) targets.push(socket);
    } else {
        targets = state.activeSockets.filter(s => s.socketType === 'SCENE' && !s.destroyed);
    }

    if (targets.length === 0) {
        return res.json({ ok: false, error: 'Nenhum jogador correspondente está ativo numa partida (Cena) no momento.' });
    }

    targets.forEach(socket => {
        socket.currentMapId = mid; 
        socket.mapName = mapConf.name;

        if (!playerId) {
            state.setLastMapId(mid);
            global.lastMapName = mapConf.name;
        }

        const defaultCoords = gameLogic.getSpawnCoordinates(mid, socket.force);
        const spawnX = defaultCoords.sx;
        const spawnY = defaultCoords.sy;
        const spawnZ = defaultCoords.sz;

        socket.lastX = spawnX;
        socket.lastY = spawnY;
        socket.lastZ = spawnZ;

        socket.write(`e:CLoadScene,((0,'${mapConf.name}',0,${encodeInt(gameLogic.getSafeMapId(mid))}))\n`);

        try {
            const msgHex = Buffer.from(`[Sistema] O mapa foi alterado para: ${mapConf.name}`).toString('hex');
            const sysNetMsg = `(${encodeInt(8)},'','[mi]${msgHex}[/mi]','',(),(),${encodeInt(1)},('','',0),())`;
            socket.write(`e${socket.playerIndex || 1}:SendMsgToClient,(${encodeInt(1006)},${sysNetMsg})\n`);
        } catch (e) {
            console.error('[⚠️ ERRO MAP CHAT SYNC]:', e.message);
        }
    });

    const targetLabel = playerId ? `o jogador ID ${playerId}` : 'todos os jogadores';
    res.json({ ok: true, message: `Mapa alterado para "${mapConf.name}" para ${targetLabel}.` });
});

// GET /api/buffs — list all available buffs
app.get('/api/buffs', (req, res) => {
    const list = Object.entries(db.buffsMap).map(([id, b]) => ({ id: parseInt(id), name: b.name || `Buff ${id}`, time: b.time }));
    res.json(list);
});

// GET /api/heroes — list all heroes
app.get('/api/heroes', (req, res) => {
    const heroes = Object.entries(db.heroSkillsMap || {}).map(([id]) => ({ id: parseInt(id) }));
    try {
        const heroInfoXml = fs.readFileSync(path.join(config.publicDir, 'baxi', 'heroinfo.xml'), 'utf8');
        const matches = [...heroInfoXml.matchAll(/<i\s+id="(\d+)"\s+name="([^"]+)"/g)];
        const heroList = matches
            .filter(m => parseInt(m[1]) >= 1001 && parseInt(m[1]) <= 1099)
            .map(m => ({ id: parseInt(m[1]), name: m[2] }));
        return res.json(heroList);
    } catch(e) {
        return res.json(heroes);
    }
});

// GET /api/npcs — list NPC types
app.get('/api/npcs', (req, res) => {
    const list = Object.entries(db.npcData)
        .map(([id, n]) => ({ id: parseInt(id), name: n.name || `NPC ${id}`, hp: n.hp, type: n.type }))
        .filter(n => n.id >= 1101 && n.id <= 1200)
        .sort((a, b) => a.id - b.id);
    res.json(list);
});

// GET /api/cosmetics — return all cosmetics
app.get('/api/cosmetics', (req, res) => {
    res.json(db.heroCosmeticsMap);
});

// GET /api/cosmetics/:heroId — return cosmetics for a specific hero
app.get('/api/cosmetics/:heroId', (req, res) => {
    const heroId = req.params.heroId;
    const data = db.heroCosmeticsMap[heroId] || db.heroCosmeticsMap[String(parseInt(heroId))] || null;
    if (!data) return res.json({ skins: [], weapons: [], emotions: [] });
    res.json(data);
});

// POST /api/equip_skin — equip a skin/weapon
app.post('/api/equip_skin', (req, res) => {
    const { playerId, skinId, weaponId } = req.body;
    
    const lobbySocket = state.activeSockets.find(s => s.socketType === 'LOBBY' && !s.destroyed && String(s.playerId) === String(playerId));
    const sceneSocket = state.activeSockets.find(s => s.socketType === 'SCENE' && !s.destroyed && String(s.playerId) === String(playerId));
    
    if (!lobbySocket && !sceneSocket) {
        return res.json({ ok: false, error: 'Jogador não encontrado ou offline.' });
    }
    
    try {
        const socket = sceneSocket || lobbySocket;
        const username = socket.playerName;
        const heroId = socket.currentHeroId || 1015;
        const newSkin = parseInt(skinId) || socket.currentSkinId || 1;
        const newWeapon = parseInt(weaponId) || socket.currentWeaponId || 1;
        
        // Atualiza a memória de equipamentos por jogador
        if (!state.playerEquipment[username]) {
            state.playerEquipment[username] = {};
        }
        if (!state.playerEquipment[username][heroId]) {
            state.playerEquipment[username][heroId] = { skin: 1, weapon: 1, emote: 1, pose: 0 };
        }
        if (skinId != null) state.playerEquipment[username][heroId].skin = newSkin;
        if (weaponId != null) state.playerEquipment[username][heroId].weapon = newWeapon;
        
        // Persiste as alterações no banco de dados do jogador
        const profile = dbPersistence.loadPlayer(username);
        if (profile) {
            profile.equipment = state.playerEquipment[username];
            dbPersistence.savePlayer(username, profile);
        }
        
        // Sincroniza nos dois sockets se ambos existirem
        [lobbySocket, sceneSocket].forEach(s => {
            if (s && !s.destroyed) {
                s.currentSkinId = newSkin;
                s.currentWeaponId = newWeapon;
            }
        });

        const heroIdEnc = encodeInt(heroId);
        const porta1306 = encodeInt(1306);

        // Se o socket de LOBBY estiver ativo, envia o update de inventário do Hall
        if (lobbySocket && !lobbySocket.destroyed) {
            const p12006 = encodeInt(12006);
            const p12009 = encodeInt(12009);

            const eq = state.playerEquipment[username][heroId];
            const fullHeroData = db.generateFullHeroData(heroId, eq.skin, eq.weapon, eq.emote, eq.pose);

            // Atualiza o Unity no Lobby
            lobbySocket.write(`e1:SendMsgToHallClient,(${porta1306},(${p12006},(${fullHeroData})))\n`);

            if (skinId != null) {
                lobbySocket.write(`e1:SendMsgToHallClient,(${porta1306},(${p12009},(1,${heroIdEnc},${encodeInt(1)},${encodeInt(newSkin)})))\n`);
            }
            if (weaponId != null) {
                lobbySocket.write(`e1:SendMsgToHallClient,(${porta1306},(${p12009},(1,${heroIdEnc},${encodeInt(2)},${encodeInt(newWeapon)})))\n`);
            }
        }

        // Se o socket de SCENE (partida) estiver ativo, envia o pacote de carregamento da skin em jogo e dá respawn
        if (sceneSocket && !sceneSocket.destroyed) {
            const currentMapId = sceneSocket.targetMapId || state.getLastMapId() || 5202;
            const isSoloMap = (currentMapId === 5202 || currentMapId === 5205);
            const prefix = isSoloMap ? 'e1' : `e${sceneSocket.playerIndex || 1}`;
            const p12002 = encodeInt(12002);

            const eq = state.playerEquipment[username][heroId] || { emote: 1, pose: 0 };
            const fullHeroDataPart = db.generateFullHeroData(heroId, newSkin, newWeapon, eq.emote, eq.pose).split(',').slice(1).join(',');

            sceneSocket.write(`${prefix}:SendMsgToClient,(${porta1306},(${p12002},(${heroIdEnc},${fullHeroDataPart})))\n`);

            state.activeSockets.forEach(s => {
                if (s.socketType === 'SCENE' && !s.destroyed) {
                    s.bypassInterceptor = true;
                    s.write(gameLogic.getPlayerSpawnPacket(sceneSocket, s));
                    s.bypassInterceptor = false;
                }
            });
        }

        const cosm = db.heroCosmeticsMap[heroId] || db.heroCosmeticsMap[String(heroId)] || {};
        const skinInfo = (cosm.skins || []).find(s => s.id === newSkin);
        const wpInfo = (cosm.weapons || []).find(w => w.id === newWeapon);
        const skinName = skinInfo ? skinInfo.name : `Skin ${newSkin}`;
        const wpName = wpInfo ? wpInfo.name : `Weapon ${newWeapon}`;

        const activeName = (sceneSocket || lobbySocket).playerName;
        console.log(`[🎨 SKIN EQUIP] ${activeName}: Skin ${newSkin} (${skinName}), Weapon ${newWeapon} (${wpName})`);
        res.json({ ok: true, message: `Skin "${skinName}" e arma "${wpName}" equipadas em ${activeName}.`, skinId: newSkin, weaponId: newWeapon });
    } catch(e) {
        res.json({ ok: false, error: e.message });
    }
});

// POST /api/play_emote — force player to play emote
app.post('/api/play_emote', (req, res) => {
    const { playerId, animparm } = req.body;
    const socket = state.activeSockets.find(s => s.socketType === 'SCENE' && !s.destroyed && String(s.playerId) === String(playerId));
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

        const szNetMsg = `(${zero},'','','',(),('','','${anim}',${zero},''),${zero},(zero,'',${zero}),())`.replace('zero', zero);
        const szData = `(${szNetMsg},'','${anim}',${zero},${zero},${force},${netId},'',${one},${one},${one},${zero})`;
        const emotePacket = `e${pid}:SendMsgToClient,(${p2000},(${p2001},${szData}))\n`;

        state.activeSockets.forEach(s => {
            if (s.socketType === 'SCENE' && !s.destroyed) s.write(emotePacket);
        });

        const heroId = socket.currentHeroId || 1015;
        const cosm = db.heroCosmeticsMap[heroId] || db.heroCosmeticsMap[String(heroId)] || {};
        const emoteInfo = (cosm.emotions || []).find(e => e.animparm === parseInt(animparm));
        const emoteName = emoteInfo ? emoteInfo.name : `Emote animparm=${animparm}`;
        console.log(`[🎭 EMOTE PLAY] ${socket.playerName}: ${emoteName} (animparm: ${anim})`);
        res.json({ ok: true, message: `Emote "${emoteName}" executado para ${socket.playerName}.` });
    } catch(e) {
        res.json({ ok: false, error: e.message });
    }
});

// POST /api/tdm/time — modify TDM match time or end it
app.post('/api/tdm/time', (req, res) => {
    const { action } = req.body;
    const match = global.activeTdmMatch;
    if (!match) return res.json({ ok: false, error: 'Nenhuma partida TDM ativa.' });
    
    if (action === 'add') {
        if (match.state === 'warmup') {
            match.warmupSeconds += 60;
        } else {
            match.fightSeconds += 60;
        }
        return res.json({ ok: true, message: 'Tempo de partida aumentado em 1 minuto.' });
    } else if (action === 'sub') {
        if (match.state === 'warmup') {
            match.warmupSeconds = Math.max(0, match.warmupSeconds - 60);
        } else {
            match.fightSeconds = Math.max(0, match.fightSeconds - 60);
        }
        return res.json({ ok: true, message: 'Tempo de partida reduzido em 1 minuto.' });
    } else if (action === 'end') {
        try {
            gameLogic.endTdmMatch();
            return res.json({ ok: true, message: 'Partida TDM encerrada com sucesso.' });
        } catch(e) {
            return res.json({ ok: false, error: e.message });
        }
    }
    res.json({ ok: false, error: 'Ação inválida.' });
});

// POST /api/tdm/score — modify TDM team score
app.post('/api/tdm/score', (req, res) => {
    const { team, amount } = req.body;
    const match = global.activeTdmMatch;
    if (!match) return res.json({ ok: false, error: 'Nenhuma partida TDM ativa.' });
    
    try {
        const amt = parseInt(amount) || 1;
        const force = team === 'blue' ? 1 : 2;
        gameLogic.incrementTdmScore(force, amt);
        return res.json({ ok: true, message: `Adicionado ${amt} pontos para o time ${team === 'blue' ? 'Azul' : 'Vermelho'}.` });
    } catch(e) {
        return res.json({ ok: false, error: e.message });
    }
});

// POST /api/nocd — toggle no-cooldown mode
app.post('/api/nocd', (req, res) => {
    const { playerId, enabled } = req.body;
    const socket = state.activeSockets.find(s => s.socketType === 'SCENE' && !s.destroyed && String(s.playerId) === String(playerId));
    if (!socket) return res.json({ ok: false, error: 'Jogador não encontrado ou offline.' });
    socket.nocd = enabled !== false && enabled !== 'false';
    res.json({ ok: true, nocd: socket.nocd, message: `No Cooldown ${socket.nocd ? 'ATIVADO' : 'DESATIVADO'} para ${socket.playerName}.` });
});

// POST /api/visual_hero — swap character visual appearance
app.post('/api/visual_hero', (req, res) => {
    const { playerId, visualHeroId } = req.body;
    const socket = state.activeSockets.find(s => s.socketType === 'SCENE' && !s.destroyed && String(s.playerId) === String(playerId));
    if (!socket) return res.json({ ok: false, error: 'Jogador não encontrado ou offline.' });
    try {
        const vId = parseInt(visualHeroId);
        socket.visualHeroId = vId || null;
        
        // Broadcast the spawn packets dynamically to recreate player's appearance instantly!
        state.activeSockets.forEach(s => {
            if (s.socketType === 'SCENE' && !s.destroyed) {
                s.bypassInterceptor = true;
                s.write(gameLogic.getPlayerSpawnPacket(socket, s));
                s.bypassInterceptor = false;
            }
        });
        
        res.json({ ok: true, message: `Aparência visual do jogador alterada para o ID de Herói ${vId || 'original'}.` });
    } catch(e) {
        res.json({ ok: false, error: e.message });
    }
});

// POST /api/scale — modify scale of player or NPC
app.post('/api/scale', (req, res) => {
    const { targetId, targetType, scaleX, scaleY, scaleZ } = req.body;
    const sx = Math.max(10, Math.min(parseInt(scaleX) || 100, 1000));
    const sy = Math.max(10, Math.min(parseInt(scaleY) || 100, 1000));
    const sz = Math.max(10, Math.min(parseInt(scaleZ) || 100, 1000));
    
    try {
        if (targetType === 'player') {
            const socket = state.activeSockets.find(s => s.socketType === 'SCENE' && !s.destroyed && String(s.playerId) === String(targetId));
            if (!socket) return res.json({ ok: false, error: 'Jogador não encontrado ou offline.' });
            
            const pid = socket.playerIndex || 1;
            const scalePacket = `e${pid}:NSS,((${encodeInt(sx)},${encodeInt(sy)},${encodeInt(sz)}))\n`;
            
            state.activeSockets.forEach(s => {
                if (s.socketType === 'SCENE' && !s.destroyed) s.write(scalePacket);
            });
            
            return res.json({ ok: true, message: `Escala de ${socket.playerName} alterada para ${sx}%, ${sy}%, ${sz}%.` });
        } else if (targetType === 'npc') {
            const nid = parseInt(targetId);
            const npc = state.spawnedNpcs[nid];
            if (!npc) return res.json({ ok: false, error: 'NPC não encontrado ou inativo.' });
            
            const className = db.getEntityClassName(npc.index);
            const scalePacket = `c${className}:${encodeInt(nid)},("NSS",((${encodeInt(sx)},${encodeInt(sy)},${encodeInt(sz)})))\n`;
            
            state.activeSockets.forEach(s => {
                if (s.socketType === 'SCENE' && !s.destroyed) s.write(scalePacket);
            });
            
            return res.json({ ok: true, message: `Escala do NPC ID ${nid} alterada para ${sx}%, ${sy}%, ${sz}%.` });
        }
        res.json({ ok: false, error: 'Alvo inválido.' });
    } catch(e) {
        res.json({ ok: false, error: e.message });
    }
});

// GET /api/crash-logs — return crash logs
app.get('/api/crash-logs', (req, res) => {
    res.json({ ok: true, logs: global.crashLogs || [] });
});

// POST /api/clear-crash-logs
app.post('/api/clear-crash-logs', (req, res) => {
    global.crashLogs = [];
    res.json({ ok: true });
});

// POST /api/client-crash
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

// GET /api/packet-stream — SSE stream for real-time packet inspection
let packetClients = [];
app.get('/api/packet-stream', (req, res) => {
    res.setHeader('Content-Type', 'text/event-stream');
    res.setHeader('Cache-Control', 'no-cache');
    res.setHeader('Connection', 'keep-alive');
    res.flushHeaders();
    
    packetClients.push(res);
    
    req.on('close', () => {
        packetClients = packetClients.filter(c => c !== res);
    });
});

global.broadcastPacket = function(packet) {
    if (packetClients.length === 0) return;
    
    const payload = JSON.stringify({
        time: new Date().toLocaleTimeString(),
        direction: packet.direction,
        socketType: packet.socketType,
        playerId: packet.playerId,
        playerName: packet.playerName,
        raw: packet.raw
    });
    
    packetClients.forEach(c => {
        try {
            c.write(`data: ${payload}\n\n`);
        } catch (e) {
            // Client connection might have dropped
        }
    });
};

module.exports = app;
