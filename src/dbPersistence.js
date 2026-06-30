const fs = require('fs');
const path = require('path');

const DATA_DIR = path.join(process.cwd(), 'data');
const PLAYERS_DIR = path.join(DATA_DIR, 'players');

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
            if (!data.equipment) data.equipment = {};
            if (!data.settings || !Array.isArray(data.settings)) data.settings = ["", "", "", "", ""];
            if (!data.items) data.items = {};
            if (data.gold === undefined) data.gold = 999999;
            if (data.diamonds === undefined) data.diamonds = 999999;
            if (data.face === undefined) data.face = 2009;
            if (data.paint === undefined) data.paint = 1019;
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

module.exports = {
    loadPlayer,
    savePlayer,
    authenticate
};
