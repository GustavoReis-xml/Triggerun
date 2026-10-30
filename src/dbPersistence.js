const fs = require('fs');
const path = require('path');
const { encodeInt } = require('./utils');

const PLAYABLE_HEROES = [
    { id: 1003, name: 'Alice', career: 2 },
    { id: 1005, name: 'Glider', career: 1 },
    { id: 1006, name: 'Tagaloa', career: 3 },
    { id: 1015, name: 'Lars', career: 1 },
    { id: 1016, name: 'Valkyrie', career: 3 },
    { id: 1017, name: 'Celina', career: 1 },
    { id: 1021, name: 'Carmen', career: 4 },
    { id: 1022, name: 'Scarlett', career: 2 },
    { id: 1023, name: 'Brigit', career: 2 },
    { id: 1024, name: 'Shogun', career: 2 },
    { id: 1025, name: 'Eva', career: 4 },
    { id: 1026, name: 'Dragos', career: 1 },
    { id: 1027, name: 'Jack', career: 1 },
    { id: 1028, name: 'Loki', career: 2 },
    { id: 1029, name: 'Angel', career: 4 },
    { id: 1030, name: 'Vincent', career: 1 },
    { id: 1031, name: 'Johnny', career: 1 },
    { id: 1032, name: 'DaJi', career: 4 },
    { id: 1033, name: 'Lilith', career: 4 },
    { id: 1034, name: 'HuangZhong', career: 3 },
    { id: 1035, name: 'NeZha', career: 1 },
    { id: 1036, name: 'Medusa', career: 1 },
    { id: 1037, name: 'DaWei', career: 3 },
    { id: 2000, name: 'Lamster', career: 4 }
];

const DATA_DIR = path.join(process.cwd(), 'data');
const PLAYERS_DIR = path.join(DATA_DIR, 'players');
const GUILDS_FILE = path.join(DATA_DIR, 'guilds.json');

function computeDeterministicCid(username) {
    const clean = String(username || 'player').trim().toLowerCase();
    let hash = 5381;
    for (let i = 0; i < clean.length; i++) {
        hash = ((hash << 5) + hash) + clean.charCodeAt(i);
        hash = hash & 0x7fffffff;
    }
    return 10000 + (hash % 9000000);
}

// Ensure directories exist
function ensureDirs() {
    if (!fs.existsSync(DATA_DIR)) {
        fs.mkdirSync(DATA_DIR);
    }
    if (!fs.existsSync(PLAYERS_DIR)) {
        fs.mkdirSync(PLAYERS_DIR);
    }
}

// Default profile for new accounts
function createDefaultProfile(username, password) {
    return {
        cid: computeDeterministicCid(username),
        username: username,
        password: password, // Store password in plain text for admin convenience (private server)
        createdAt: new Date().toISOString(),
        lastLogin: new Date().toISOString(),
        gold: 999999,      // High wealth by default to match existing state.js
        diamonds: 999999,
        level: 1,
        exp: 0,
        face: 2009,         // Default equipped avatar/face
        paint: 1019,        // Default equipped spray/paint
        popularity: 0,
        skyTier: 1,
        friends: [],
        friendRequests: [],
        lately: [],
        guildId: 0,
        guildRank: 0,
        runes: { step: 0, star: 0 }, // Default gene tree progress
        parts: {            // Default weapon parts levels/exp
            "1": { lvl: 1, exp: 0 },
            "2": { lvl: 1, exp: 0 },
            "3": { lvl: 1, exp: 0 },
            "4": { lvl: 1, exp: 0 },
            "5": { lvl: 1, exp: 0 },
            "6": { lvl: 1, exp: 0 },
            "7": { lvl: 1, exp: 0 },
            "8": { lvl: 1, exp: 0 }
        },
        weaponParts: {},    // [HeroId][WeaponId][SlotId] -> PartId
        items: {},          // Item ID -> quantity
        unlockedHeroes: [1015], // Default unlocked hero Lars
        unlockedFaces: [2009],   // Default avatar
        unlockedSprays: [1019],  // Default spray
        unlockedCosmetics: {
            "1015": { skins: [1], weapons: [1], emotes: [1], poses: [0] }
        },
        equipment: {
            "1015": { skin: 1, weapon: 1, emote: 1, pose: 0 } // default starting equipment for hero 1015
        },
        career: {
            totalMatches: 0,
            wins: 0,
            mvps: 0,
            kills: 0,
            deaths: 0,
            assists: 0,
            damage: 0,
            healing: 0,
            playTimeSeconds: 0,
            goldMedals: 0,
            silverMedals: 0,
            bronzeMedals: 0,
            rankName: 'Bronze',
            rankSmallIcon: 1,
            maxRankName: 'Bronze',
            maxRankSmallIcon: 1,
            heroes: {}
        },
        settings: ["", "", "", "", ""] // 5 settings segments matching [arg0, arg1, arg2, arg3, arg4]
    };
}

// Load a player profile
function loadPlayer(username) {
    ensureDirs();
    const cleanName = username.trim().toLowerCase();
    const filePath = path.join(PLAYERS_DIR, `${cleanName}.json`);
    
    if (fs.existsSync(filePath)) {
        try {
            const content = fs.readFileSync(filePath, 'utf8');
            const data = JSON.parse(content);
            // Auto-fill missing fields if version updates
            if (!data.cid) data.cid = computeDeterministicCid(data.username || cleanName);
            if (!data.equipment) data.equipment = {};
            if (!data.settings || !Array.isArray(data.settings)) data.settings = ["", "", "", "", ""];
            if (!data.items) data.items = {};
            if (data.gold === undefined) data.gold = 999999;
            if (data.diamonds === undefined) data.diamonds = 999999;
            if (data.face === undefined) data.face = 2009;
            if (data.paint === undefined) data.paint = 1019;
            if (data.popularity === undefined) data.popularity = 0;
            if (data.skyTier === undefined) data.skyTier = 1;
            if (!Array.isArray(data.friends)) data.friends = [];
            if (!Array.isArray(data.friendRequests)) data.friendRequests = [];
            if (!Array.isArray(data.lately)) data.lately = [];
            if (data.guildId === undefined) data.guildId = 0;
            if (data.guildRank === undefined) data.guildRank = 0;
            if (!data.runes) data.runes = { step: 0, star: 0 };
            if (!data.unlockedHeroes) data.unlockedHeroes = [1015];
            if (!data.unlockedFaces) data.unlockedFaces = [2009];
            if (!data.unlockedSprays) data.unlockedSprays = [1019];
            if (!data.unlockedCosmetics) {
                data.unlockedCosmetics = {
                    "1015": { skins: [1], weapons: [1], emotes: [1], poses: [0] }
                };
            }
            if (!data.parts) {
                data.parts = {
                    "1": { lvl: 1, exp: 0 },
                    "2": { lvl: 1, exp: 0 },
                    "3": { lvl: 1, exp: 0 },
                    "4": { lvl: 1, exp: 0 },
                    "5": { lvl: 1, exp: 0 },
                    "6": { lvl: 1, exp: 0 },
                    "7": { lvl: 1, exp: 0 },
                    "8": { lvl: 1, exp: 0 }
                };
            }
            if (!data.weaponParts) data.weaponParts = {};
            if (!data.career) {
                data.career = {
                    totalMatches: 0,
                    wins: 0,
                    mvps: 0,
                    kills: 0,
                    deaths: 0,
                    assists: 0,
                    damage: 0,
                    healing: 0,
                    playTimeSeconds: 0,
                    goldMedals: 0,
                    silverMedals: 0,
                    bronzeMedals: 0,
                    rankName: 'Bronze',
                    rankSmallIcon: 1,
                    maxRankName: 'Bronze',
                    maxRankSmallIcon: 1,
                    heroes: {}
                };
            }
            return data;
        } catch (e) {
            console.error(`[⚠️ DB persistence error] Failed parsing JSON for player ${username}:`, e);
            return null;
        }
    }
    return null;
}

// Save a player profile
function savePlayer(username, data) {
    ensureDirs();
    const cleanName = username.trim().toLowerCase();
    const filePath = path.join(PLAYERS_DIR, `${cleanName}.json`);
    try {
        fs.writeFileSync(filePath, JSON.stringify(data, null, 4), 'utf8');
        return true;
    } catch (e) {
        console.error(`[⚠️ DB persistence error] Failed saving JSON for player ${username}:`, e);
        return false;
    }
}

// Authenticate user, auto-register if they don't exist
function authenticate(username, password) {
    ensureDirs();
    const cleanName = username.trim().toLowerCase();
    const existing = loadPlayer(cleanName);
    
    if (existing) {
        if (existing.password === password) {
            existing.lastLogin = new Date().toISOString();
            savePlayer(cleanName, existing);
            return { ok: true, player: existing, isNew: false };
        } else {
            return { ok: false, error: 'Senha incorreta!' };
        }
    }
    
    // Auto-registration
    const newProfile = createDefaultProfile(username, password);
    savePlayer(cleanName, newProfile);
    console.log(`[🔐 AUTO-REGISTRO] Novo jogador cadastrado: ${username}`);
    return { ok: true, player: newProfile, isNew: true };
}

// Calculate required EXP to level up from level nlvl (matches Fun.GetUserLvlupExp in C# client)
function getUserLvlupExp(nlvl) {
    const lvl = Math.max(1, parseInt(nlvl, 10) || 1);
    return Math.min(Math.floor(lvl / 5) * 1250 + lvl * 250, 50000);
}

// Calculate required EXP for a hero to level up from level nlvl (matches Fun.GetHeroLvlupExp in C# client)
function getHeroLvlupExp(nlvl) {
    const lvl = Math.max(1, parseInt(nlvl, 10) || 1);
    return Math.min(Math.floor(lvl / 5) * 500 + lvl * 100, 20000);
}

// Add match rewards (EXP, Gold, Diamonds) and update career statistics
function addMatchRewards(username, rewards = {}) {
    ensureDirs();
    const cleanName = username.trim().toLowerCase();
    const profile = loadPlayer(cleanName);
    if (!profile) return null;

    const originalLevel = profile.level || 1;
    const originalExp = profile.exp || 0;
    const originalGold = profile.gold || 0;

    const expGained = Math.max(0, rewards.exp || 0);
    const goldGained = Math.max(0, rewards.gold || 0);
    const diamondsGained = Math.max(0, rewards.diamonds || 0);

    let currentLevel = originalLevel;
    let currentExp = originalExp + expGained;
    let numLvlUp = 0;

    // Process potential multiple level-ups
    while (currentExp >= getUserLvlupExp(currentLevel)) {
        currentExp -= getUserLvlupExp(currentLevel);
        currentLevel++;
        numLvlUp++;
    }

    profile.level = currentLevel;
    profile.exp = currentExp;
    profile.gold = originalGold + goldGained;
    profile.diamonds = (profile.diamonds || 0) + diamondsGained;

    // Initialize & update career statistics
    if (!profile.career) {
        profile.career = {
            totalMatches: 0,
            wins: 0,
            losses: 0,
            draws: 0,
            kills: 0,
            deaths: 0,
            assists: 0,
            totalDamage: 0,
            totalHealing: 0,
            playTimeSeconds: 0,
            mvps: 0,
            goldMedals: 0,
            silverMedals: 0,
            bronzeMedals: 0,
            heroes: {}
        };
    }

    profile.career.totalMatches = (profile.career.totalMatches || 0) + 1;
    if (rewards.isWin) {
        profile.career.wins = (profile.career.wins || 0) + 1;
    } else if (rewards.isDraw) {
        profile.career.draws = (profile.career.draws || 0) + 1;
    } else {
        profile.career.losses = (profile.career.losses || 0) + 1;
    }

    const duration = rewards.durationSeconds || 180;
    profile.career.playTimeSeconds = (profile.career.playTimeSeconds || 0) + duration;
    profile.career.kills = (profile.career.kills || 0) + (rewards.kills || 0);
    profile.career.deaths = (profile.career.deaths || 0) + (rewards.deaths || 0);
    profile.career.assists = (profile.career.assists || 0) + (rewards.assists || 0);
    profile.career.totalDamage = (profile.career.totalDamage || profile.career.damage || 0) + (rewards.damage || 0);
    profile.career.damage = profile.career.totalDamage;
    profile.career.totalHealing = (profile.career.totalHealing || profile.career.healing || 0) + (rewards.healing || 0);
    profile.career.healing = profile.career.totalHealing;
    if (rewards.isMvp) {
        profile.career.mvps = (profile.career.mvps || 0) + 1;
    }
    profile.career.goldMedals = (profile.career.goldMedals || 0) + (rewards.goldMedals || 0);
    profile.career.silverMedals = (profile.career.silverMedals || 0) + (rewards.silverMedals || 0);
    profile.career.bronzeMedals = (profile.career.bronzeMedals || 0) + (rewards.bronzeMedals || 0);

    let heroOldLevel = 1;
    let heroOriginalExp = 0;
    let heroNewLevel = 1;
    let heroNewExp = 0;
    let heroNumLvlUp = 0;
    const heroExpGained = Math.max(0, rewards.heroExp !== undefined ? rewards.heroExp : expGained);

    if (rewards.heroId) {
        if (!profile.career.heroes) profile.career.heroes = {};
        const hId = String(rewards.heroId);
        if (!profile.career.heroes[hId]) {
            profile.career.heroes[hId] = {
                level: 1,
                exp: 0,
                matches: 0,
                wins: 0,
                kills: 0,
                deaths: 0,
                assists: 0,
                damage: 0,
                healing: 0,
                playTimeSeconds: 0,
                headshots: 0,
                critKills: 0,
                totalShots: 0,
                maxKills: 0,
                maxDamage: 0,
                maxHealing: 0,
                maxAssists: 0,
                maxKillStreak: 0
            };
        }
        const hStats = profile.career.heroes[hId];
        heroOldLevel = Math.max(1, parseInt(hStats.level, 10) || 1);
        heroOriginalExp = Math.max(0, parseInt(hStats.exp, 10) || 0);
        heroNewLevel = heroOldLevel;
        heroNewExp = heroOriginalExp + heroExpGained;
        while (heroNewLevel < 100 && heroNewExp >= getHeroLvlupExp(heroNewLevel)) {
            heroNewExp -= getHeroLvlupExp(heroNewLevel);
            heroNewLevel++;
            heroNumLvlUp++;
        }
        hStats.level = heroNewLevel;
        hStats.exp = heroNewExp;

        hStats.matches = (hStats.matches || 0) + 1;
        if (rewards.isWin) hStats.wins = (hStats.wins || 0) + 1;
        hStats.kills = (hStats.kills || 0) + (rewards.kills || 0);
        hStats.deaths = (hStats.deaths || 0) + (rewards.deaths || 0);
        hStats.assists = (hStats.assists || 0) + (rewards.assists || 0);
        hStats.damage = (hStats.damage || 0) + (rewards.damage || 0);
        hStats.healing = (hStats.healing || 0) + (rewards.healing || 0);
        hStats.playTimeSeconds = (hStats.playTimeSeconds || 0) + duration;
        hStats.headshots = (hStats.headshots || 0) + (rewards.headshots || 0);
        hStats.critKills = (hStats.critKills || 0) + (rewards.critKills || 0);
        hStats.totalShots = (hStats.totalShots || 0) + (rewards.totalShots || (rewards.headshots ? rewards.headshots * 3 : 10));

        // Atualiza recordes máximos em partida única
        hStats.maxKills = Math.max(hStats.maxKills || 0, rewards.kills || 0);
        hStats.maxDamage = Math.max(hStats.maxDamage || 0, rewards.damage || 0);
        hStats.maxHealing = Math.max(hStats.maxHealing || 0, rewards.healing || 0);
        hStats.maxAssists = Math.max(hStats.maxAssists || 0, rewards.assists || 0);
        hStats.maxKillStreak = Math.max(hStats.maxKillStreak || 0, rewards.kills || 0);
    }

    savePlayer(cleanName, profile);
    console.log(`[🏆 RECOMPENSAS] Jogador ${username}: +${expGained} EXP, +${goldGained} Ouro. Nível: ${originalLevel} -> ${currentLevel} (Subiu ${numLvlUp}x, EXP: ${currentExp}/${getUserLvlupExp(currentLevel)}) | Herói ${rewards.heroId || '-'}: Nível ${heroOldLevel} -> ${heroNewLevel} (+${heroExpGained} EXP)`);

    return {
        originalLevel,
        originalExp,
        newLevel: currentLevel,
        newExp: currentExp,
        numLvlUp,
        expGained,
        goldGained,
        newGold: profile.gold,
        heroExpGained,
        heroOldLevel,
        heroOriginalExp,
        heroNewLevel,
        heroNewExp,
        heroNumLvlUp
    };
}

/**
 * Constrói o pacote de sincronização de carreira (Opcode 100001 - CAREER_GETDATA)
 * Retorna dados detalhados para exibição nas abas de Carreira e Perfil de Heróis no Lobby.
 */
function buildCareerDataPacket(username) {
    const profile = loadPlayer(username) || { career: {} };
    const c = profile.career || {};
    const one = encodeInt(1);
    const zero = encodeInt(0);

    // 1. szUserCareerData: Lista de pares intercalados (mode, userCareerData, mode, userCareerData, ...)
    // Modos: 1 (Casual/TDM), 2 (Ranked/Control)
    const userModes = [1, 2];
    const userCareerParts = [];

    userModes.forEach(modeId => {
        const modeEnc = encodeInt(modeId);
        const totalMatches = encodeInt(c.totalMatches || 0);
        const wins = encodeInt(c.wins || 0);
        const playTime = encodeInt(c.playTimeSeconds || (c.totalMatches ? c.totalMatches * 180 : 0));
        const mvps = encodeInt(c.mvps || 0);
        const kills = encodeInt(c.kills || 0);
        const deaths = encodeInt(c.deaths || 0);
        const lifeTime = encodeInt(c.playTimeSeconds || (c.totalMatches ? c.totalMatches * 180 : 0));
        const damage = encodeInt(c.totalDamage || 0);
        const healing = encodeInt(c.totalHealing || 0);
        const assists = encodeInt(c.assists || 0);
        const goldMedals = encodeInt(c.goldMedals || 0);
        const silverMedals = encodeInt(c.silverMedals || 0);
        const bronzeMedals = encodeInt(c.bronzeMedals || 0);

        const modeData = `(${zero},${modeEnc},${totalMatches},${wins},${zero},${zero},${playTime},${mvps},${kills},${deaths},${lifeTime},${damage},${healing},${assists},${zero},${goldMedals},${silverMedals},${bronzeMedals},'Bronze',${one},'Bronze',${one})`;
        userCareerParts.push(`${modeEnc},${modeData}`);
    });

    const szUserCareerData = `(${userCareerParts.join(',')})`;

    // 2. szHeroCareerData: Lista de pares intercalados (mode, heroCareerData, mode, heroCareerData, ...)
    const heroCareerParts = [];
    userModes.forEach(modeId => {
        const modeEnc = encodeInt(modeId);
        PLAYABLE_HEROES.forEach(h => {
            const hId = String(h.id);
            const hData = (c.heroes && c.heroes[hId]) || {};
            const heroIdEnc = encodeInt(h.id);
            const heroWins = encodeInt(hData.wins || 0);
            const heroMatches = encodeInt(hData.matches || 0);
            const heroTime = encodeInt(hData.playTimeSeconds || (hData.matches ? hData.matches * 180 : 0));
            const headshots = encodeInt(hData.headshots || 0);
            const critKills = encodeInt(hData.critKills || 0);
            const heroKills = encodeInt(hData.kills || 0);
            const heroDeaths = encodeInt(hData.deaths || 0);
            const heroLifeTime = encodeInt(hData.playTimeSeconds || (hData.matches ? hData.matches * 180 : 0));
            const heroDamage = encodeInt(hData.damage || 0);
            const heroHealing = encodeInt(hData.healing || 0);
            const heroAssists = encodeInt(hData.assists || 0);
            const totalShots = encodeInt(hData.totalShots || Math.max(1, (hData.headshots || 0) * 3));
            const heroTag = encodeInt(h.career);

            const heroData = `(${heroIdEnc},'${h.name}',${heroWins},${heroMatches},${heroTime},${zero},${zero},${headshots},${critKills},${heroKills},${heroDeaths},${heroLifeTime},${heroDamage},${heroHealing},${heroAssists},${zero},${totalShots},${zero},${heroTag})`;
            heroCareerParts.push(`${modeEnc},${heroData}`);
        });
    });

    const szHeroCareerData = `(${heroCareerParts.join(',')})`;

    // 3. szHeroCareerHighData: Lista de pares intercalados (mode, heroHighData, mode, heroHighData, ...)
    const heroHighParts = [];
    userModes.forEach(modeId => {
        const modeEnc = encodeInt(modeId);
        PLAYABLE_HEROES.forEach(h => {
            const hId = String(h.id);
            const hData = (c.heroes && c.heroes[hId]) || {};
            const heroIdEnc = encodeInt(h.id);
            const maxKills = encodeInt(hData.maxKills || hData.kills || 0);
            const maxDamage = encodeInt(hData.maxDamage || hData.damage || 0);
            const maxHealing = encodeInt(hData.maxHealing || hData.healing || 0);
            const maxAssists = encodeInt(hData.maxAssists || hData.assists || 0);
            const maxKillStreak = encodeInt(hData.maxKillStreak || hData.kills || 0);

            const highData = `(${heroIdEnc},${maxKills},${maxDamage},${maxHealing},${maxAssists},${maxKillStreak})`;
            heroHighParts.push(`${modeEnc},${highData}`);
        });
    });

    const szHeroCareerHighData = `(${heroHighParts.join(',')})`;

    return `e1:SendMsgToHallClient,(${encodeInt(100001)},(${szUserCareerData},${szHeroCareerData},${szHeroCareerHighData}))\n`;
}

function getAllPlayers() {
    ensureDirs();
    try {
        const files = fs.readdirSync(PLAYERS_DIR).filter(f => f.endsWith('.json'));
        const players = [];
        for (const file of files) {
            const name = file.slice(0, -5);
            const p = loadPlayer(name);
            if (p) players.push(p);
        }
        return players;
    } catch (e) {
        console.error('[⚠️ DB persistence error] Failed listing players:', e);
        return [];
    }
}

function getPlayerByCid(cid) {
    const targetCid = Number(cid);
    if (!Number.isFinite(targetCid)) return null;
    const all = getAllPlayers();
    return all.find(p => Number(p.cid) === targetCid) || null;
}

function loadGuilds() {
    ensureDirs();
    if (fs.existsSync(GUILDS_FILE)) {
        try {
            const raw = fs.readFileSync(GUILDS_FILE, 'utf8');
            const parsed = JSON.parse(raw);
            if (parsed && typeof parsed === 'object' && parsed.guilds) {
                return parsed;
            }
        } catch (e) {
            console.error('[⚠️ DB persistence error] Failed reading guilds.json:', e);
        }
    }
    const initial = {
        nextGuildId: 1001,
        guilds: {}
    };
    saveGuilds(initial);
    return initial;
}

function saveGuilds(guildStore) {
    ensureDirs();
    try {
        fs.writeFileSync(GUILDS_FILE, JSON.stringify(guildStore, null, 4), 'utf8');
        return true;
    } catch (e) {
        console.error('[⚠️ DB persistence error] Failed saving guilds.json:', e);
        return false;
    }
}

module.exports = {
    loadPlayer,
    savePlayer,
    authenticate,
    getUserLvlupExp,
    getHeroLvlupExp,
    addMatchRewards,
    buildCareerDataPacket,
    computeDeterministicCid,
    getAllPlayers,
    getPlayerByCid,
    loadGuilds,
    saveGuilds,
    PLAYABLE_HEROES
};

