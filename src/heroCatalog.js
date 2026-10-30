// Catálogo centralizado de atributos, regras especiais, recargas e limites por herói (Data-Driven)

const HERO_IDS_LIST = [
    1001, 1002, 1003, 1004, 1005, 1006, 1007, 1008, 1009, 1010,
    1011, 1012, 1013, 1014, 1015, 1016, 1017, 1018, 1019, 1020,
    1021, 1022, 1023, 1024, 1025, 1026, 1027, 1028, 1029, 1030,
    1031, 1032, 1033, 1034, 1035, 1036, 1037, 1038, 1039, 1040,
    1041, 1042, 1043, 1044, 1045, 2000
];

const DEFAULT_RELOAD_DURATION_MS = 1800;

// Durações de recarga por herói (em ms) para sincronizar com a animação do cliente
const HERO_RELOAD_DURATIONS = {
    1015: 2000, // Lars
    1017: 1500, // Mulan
    1021: 1500, // Carmen
    1022: 1500, // Yuki Onna
    1023: 2500, // Zhu Rong
    1025: 1500, // Linglong
    1026: 2000, // Edward
    1027: 1800, // Jack
    1028: 1500, // Loki
    1029: 1500, // Angel
    1030: 1500, // Vincent
    1031: 2000, // Rambo / Johnny
    1032: 2200, // Daji / Venus
    1033: 1500, // Lilith / Lily
    1034: 2500, // Huangzhong
    1036: 1800, // Medusa
    1037: 1800  // David
};

// Mapa de apelidos/nomes para comandos de chat (/bot, /hero)
const HERO_NAME_ALIASES = {
    'lars': 1015, 'terminator': 1015,
    'diana': 1003, 'sniper': 1003,
    'reinhardt': 1020, 'lobo': 1020,
    'xingtian': 1001, 'xing': 1001,
    'mulan': 1004,
    'houyi': 1005, 'hou': 1005,
    'daji': 1002, 'd.va': 1002, 'dva': 1002,
    'leizhenzi': 1006, 'lei': 1006, 'tagaloa': 1006,
    'medusa': 1007,
    'talia': 1008,
    'rambo': 1009, 'johnny': 1031,
    'guanyin': 1011,
    'pharaoh': 1012, 'farao': 1012,
    'nezha': 1013,
    'jingke': 1014,
    'dawei': 1017,
    'zhurong': 1021, 'carmen': 1021,
    'brigit': 1023, 'fenix': 1023,
    'shogun': 1024,
    'jack': 1027,
    'loki': 1028,
    'angel': 1029,
    'vincent': 1030,
    'venus': 1032,
    'lily': 1033, 'lilith': 1033,
    'david': 1037,
    'lamster': 2000
};

// Especificações especiais por herói (barreiras, passivas, skills e ultimates)
const HERO_SPECS = {
    1006: {
        id: 1006,
        key: 'tagaloa',
        barrier: {
            name: 'TAGALOA',
            maxHp: 8000,
            destructible: true,
            regenDelayMs: 3000,
            regenPerSec: 350
        }
    },
    1024: {
        id: 1024,
        key: 'shogun',
        repulsionArrowDamage: 125,
        chargeArrowMinDamage: 150,
        chargeArrowMaxDamage: 900,
        chargeFullMs: 1000
    },
    1027: {
        id: 1027,
        key: 'jack',
        dashStunBuffId: 1024,
        dashStunMs: 1500,
        dashDamage: 80,
        vortexPullReleaseMs: 4500
    },
    1028: {
        id: 1028,
        key: 'loki',
        retaliationSkillId: 1112,
        retaliationDurationMs: 2500
    },
    1029: {
        id: 1029,
        key: 'angel',
        circleHealPerSec: 40,
        sylphWindSkillId: 1122,
        sylphWindBuffId: 1035,
        sylphWindDurationMs: 6000,
        sylphWindRadius: 1200
    },
    1030: {
        id: 1030,
        key: 'vincent',
        soulOrbHeal: 250,
        soulOrbBuffs: [
            { id: 1038, durationMs: 100 },
            { id: 1036, durationMs: 2000 }
        ]
    },
    1032: {
        id: 1032,
        key: 'venus',
        barrier: {
            name: 'VENUS',
            maxHp: 4000,
            destructible: true
        }
    },
    1033: {
        id: 1033,
        key: 'lily',
        barrier: {
            name: 'LILY',
            maxHp: 1000,
            destructible: false,
            skillId: 1163,
            colliderBuffId: 1048,
            collisionDamage: 5,
            allyHeal: 300
        }
    },
    1037: {
        id: 1037,
        key: 'david',
        barrier: {
            name: 'DAVID',
            maxHp: 10000,
            destructible: false
        }
    }
};

// Limite de entidades/invocações simultâneas por mestre (NPC Index -> Max)
const NPC_SUMMON_LIMITS = {
    1001: 3, // Brigit Sentries
    1010: 1, // Scarlett Frozen Trap
    1014: 1, // Eva Auto Cannon
    1021: 1  // Johnny Claymore Mine
};

const COMBAT_DEFAULTS = {
    outOfCombatRegenDelayMs: 6000,
    outOfCombatRegenPctPerSec: 0.05,
    passiveUltEnergyPerSec: 50
};

function getReloadDuration(heroId) {
    return HERO_RELOAD_DURATIONS[Number(heroId)] || DEFAULT_RELOAD_DURATION_MS;
}

function resolveHeroAlias(input) {
    if (input === undefined || input === null) return null;
    const raw = String(input).trim().toLowerCase();
    if (!raw) return null;
    if (HERO_NAME_ALIASES[raw]) return HERO_NAME_ALIASES[raw];
    const parsed = parseInt(raw, 10);
    if (!Number.isNaN(parsed) && ((parsed >= 1000 && parsed <= 1045) || parsed === 2000)) {
        return parsed;
    }
    return null;
}

function getHeroSpec(heroId) {
    return HERO_SPECS[Number(heroId)] || null;
}

function getHeroBarrierSpec(heroId) {
    const spec = getHeroSpec(heroId);
    return spec && spec.barrier ? spec.barrier : null;
}

function getTagaloaShieldMaxHp() {
    return HERO_SPECS[1006].barrier.maxHp;
}

function getNpcSummonLimit(npcIndex) {
    return NPC_SUMMON_LIMITS[Number(npcIndex)] || null;
}

module.exports = {
    HERO_IDS_LIST,
    DEFAULT_RELOAD_DURATION_MS,
    HERO_RELOAD_DURATIONS,
    HERO_NAME_ALIASES,
    HERO_SPECS,
    NPC_SUMMON_LIMITS,
    COMBAT_DEFAULTS,
    getReloadDuration,
    resolveHeroAlias,
    getHeroSpec,
    getHeroBarrierSpec,
    getTagaloaShieldMaxHp,
    getNpcSummonLimit
};
