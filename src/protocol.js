/**
 * Trigger Run Private Server - Network Protocol Constants
 * 
 * This file maps all client packet IDs (NetID) and sub-commands
 * to human-readable constants. Import this in socket handlers and logic.
 */

const Protocol = {
    // 1. Core Lobby Systems (HallUser / socketServer)
    Lobby: {
        INIT_DATA: 1301,
        ERR_TIP: 1302,
        SCENE_REDIRECT: 1304,
        HERO_DATA_REDIRECT: 1306,
        FCM_DATA_REDIRECT: 1307,
        HALL_TO_WEB: 1308,
        LOGIN_DATE_SYNC: 1309,
        GM_ACTION: 1310,
        LOBBY_ACK: 1313,
        WENJUAN_POLL: 1314,
        GIFT_PACKAGE: 1315,
        QUIT_CONFIRM: 1318,
        HTTP_REQUEST: 1601,
        RADIO_BROADCAST: 3001,
        CLOSE_RADIO: 3004,
        MAIL_REDIRECT: 3400,
        UPDATE_MONEY: 10025,
        TIME_SYNC: 20058,
        UPDATE_EXP: 21002,
        INIT_SETTINGS: 3010001,
        SAVE_SETTINGS_OK: 3010014,
        SYNC_CAREER: 100001,
        TEAM_REDIRECT: 60000,
        FRIEND_REDIRECT: 60100,
        MATCH_REDIRECT: 80001,
        SHOP_REDIRECT: 70000,
        TASK_REDIRECT: 71000,
        GUILD_REDIRECT: 91000,
        CHAT_REDIRECT: 1006
    },

    // 2. Custom Room Management (nNetId: 20013)
    Room: {
        JOIN: 3301,
        GAME_OVER: 3302,
        JOIN_BACK: 3303,
        LEAVE_BACK: 3304,
        GET_PAGE_INFO: 3306,
        ROOM_INFO_BACK: 3307,
        PLAYER_CHANGE_BACK: 3308,
        CHANGE_TEAM: 3318,
        SELECT_HERO_OPEN: 3320,
        MODIFY_INFO: 3324,
        GET_SOME_INFO: 3325,
        QUICK_JOIN_BACK: 3326,
        CHANGE_USER_DATA: 3329,
        USER_LEAVE: 3331,
        GAME_START_ERR: 3333,
        SET_PLAYER_INFO: 3335,
        JOIN_MATCH: 3336,
        GET_GROUP_DATA: 3337,
        TEAM_JOIN_BACK: 3338
    },

    // 3. Matchmaking & Preparation (nNetId: 20014 & 80001)
    Match: {
        SEARCH_START: 80006,
        SEARCH_CANCEL: 80007,
        TEAM_NUM_UPDATE: 80011,
        MODEL_NUM_UPDATE: 80012,
        MATCH_OK: 80013,
        CHECK_ENTER_FB: 80016,
        MATCH_CHECK_TIP: 80021,
        MATCH_START: 90001,
        CHANGE_USER_DATA: 90002,
        ENTER_GAME: 90004,
        UPDATE_GAME_DATA: 90005,
        SET_USER_LIST: 90006,
        USER_LEAVE: 90007,
        ENTER_GAME_CONFIRM: 90008,
        SHOW_LOAD: 90009
    },

    // 4. Chat Channels (nPinDao)
    ChatChannel: {
        WORLD: 1,      // shijie
        TEAM: 2,       // duiwu
        FACTION: 3,    // zhenying
        MATCH: 4,      // bisai
        WHISPER: 5,    // siliao
        ROOM: 6,       // room
        GUILD: 7,      // guild
        SYSTEM: 8,     // system
        MEGAPHONE: 10,  // laba
        ALERT_MUTE: 999
    },

    // 5. Hero Customization & Skins (nNetId: 1306)
    Hero: {
        SET_DATA: 12001,
        INIT_MEMORY: 12002,
        LEVEL_UP: 12005,
        UPDATE_STATUS: 12006,
        UNLOCK: 12008,
        EQUIP_COSMETIC: 12009,
        LEVEL_UP_AWARD: 12010,
        UPDATE_CHIPS: 12011,
        GIFT_OPEN: 12016,
        GIFT_OPEN_BACK: 12017,
        SELECT_UI_LOCK_END: 12018,
        INIT_PARTS: 12020,
        PARTS_LEVEL_UP: 12021,
        PARTS_REMOVE: 12023,
        PARTS_OPEN: 12024,
        INIT_FACE: 11001,
        OPEN_FACE: 11002,
        SET_FACE: 11003,
        INIT_PAINT: 11008,
        OPEN_PAINT: 11009,
        GIFT_OPEN_PAINT: 11011,
        GIFT_OPEN_PAINT_BACK: 11012,
        GIFT_OPEN_FACE: 11013,
        GIFT_OPEN_FACE_BACK: 11014
    },

    // 6. Friends List (nNetId: 60100)
    Friend: {
        REQUEST_RECEIVED: 60102,
        UPDATE_INFO: 60104,
        ADD_SUCCESS_ALERT: 60106,
        STATUS_UPDATE: 60107,
        ADD_DEL_BLOCK: 60109,
        FIND_APPLY_RETURN: 60114,
        ADD_ERROR_TIP: 60115,
        INIT_DATA: 60118,
        RECENT_PLAYERS_UPDATE: 60200,
        FRIEND_ELO_UPDATE: 60204
    },

    // 7. Guild System (nNetId: 91000)
    Guild: {
        CREATE_OK: 91001,
        DISSOLVE: 91002,
        ADD: 91003,
        LEAVE: 91004,
        KICK: 91005,
        UPGRADE: 91006,
        PASS_LEADERSHIP: 91007,
        CHANGE_RANK: 91008,
        LIMIT_REACHED: 91012,
        GET_LIST: 91013,
        ACCEPT_REQUEST: 91015,
        REFUSE_REQUEST: 91016,
        GET_REQUEST_LIST: 91017,
        GET_INFO: 91018,
        SAVE_NOTICE: 91019,
        SET_USER_GUILD: 91020,
        SET_AUTO_JOIN: 91022,
        INVITE: 91023,
        ADD_ACTIVE: 91025,
        UPDATE_REQ_NUM: 91027,
        FIND_BY_NAME: 91031,
        CHANGE_NAME: 91033,
        IMPEACH: 91034,
        IMPEACH_RESULT: 91035,
        BENEFITS: 91036
    },

    // 8. TDM - Team Deathmatch Arena (nNetId: 110300)
    TDM: {
        SHOW_UI_SCORE: 110301,
        DISPLAY_FIGHT_DATA: 110302,
        FIGHT_START: 110303,
        GOTO_HALL: 110304,
        COUNTDOWN_DATA: 110305,
        SCORE_INC: 110306,
        FIGHT_PREPARE: 110307,
        ROUND_END: 110308,
        ROUND_START: 110309
    },

    // 9. Control Point Mode (nNetId: 110100)
    Control: {
        CREATE_POINT: 110101,
        COUNTDOWN: 110102,
        USER_DATA: 110103,
        FIGHT_SCORE: 110104,
        FIGHT_DATA: 110105,
        FIGHT_PREPARE: 110106,
        FIGHT_START: 110107,
        FIGHT_RESULT: 110108,
        FIGHT_END: 110109
    },

    // 10. Mail Box System (nNetId: 3400)
    Mail: {
        GET_LIST: 3401,
        READ: 3403,
        ADD: 3404,
        DELETE: 3405,
        CLAIM_ATTACH: 3406,
        GET_UNREAD_COUNT: 3407,
        UPDATE_STATE_BATCH: 3408,
        DELETE_READ: 3409,
        CLAIM_ALL: 3410
    },

    // 11. Shop & Inventory (nNetId: 70000)
    Shop: {
        UPDATE_BOX_AVAILABILITY: 70005,
        OPEN_BOX: 70006,
        UPDATE_XP_GOLD_BUFFS: 70007,
        BUY_BACK: 70008,
        BUY_SUCCESS: 70009,
        OPEN_BOX_BATCH: 70010,
        RECEIVE_ADMIN_ITEM: 70012,
        UPDATE_INVENTORY: 70018
    },

    // 12. PVE Campaign & Adventure (nNetId: 19500)
    PVE: {
        INIT_MAP_DATA: 19501,
        ENHANCE_EQUIPMENT: 19502,
        START_MATCHMAKING: 19503,
        REFINE_WEAPON: 19504,
        FLIP_CARD: 19507,
        ENERGY_SYNC: 19511,
        MATCHMAKING_ERROR: 19512,
        RECHARGE_TIMER: 19513,
        AUTO_CLEAR_STAGE: 19514,
        STAGE_BUTTONS_STATE: 19515,
        UPDATE_STARS: 19516,
        GENE_UPGRADE_FAIL: 19517,
        GENE_TREE_SYNC: 19518,
        FIRST_CLEAR_REWARD: 19520,
        UPDATE_FLOWERS: 19521
    },

    // 13. Training & Dungeon Modes (nNetId: 19000, 19100, 19600)
    KingTrain: {
        INIT_TRAINING: 19001,
        QUIT_TRAINING: 19002,
        FIGHT_START: 19003,
        ROUND_END: 19004,
        ROUND_PREPARE: 19005,
        UPDATE_PLAYER: 19007,
        MONSTER_COUNT_UPDATE: 19008,
        PLAYER_LEAVE: 19011,
        PLAYER_JOIN: 19013,
        TEAM_DEFEAT_SYNC: 19014
    },
    Dungeon: {
        INIT_STAGE: 19004,
        ROUND_SYNC: 19005,
        SPAWN_POSITION: 19003,
        UPDATE_MEMBER_KDA: 19007,
        USE_HEALTH_BOX: 19008,
        RESULTS: 19009,
        UPDATE_POTIONS: 19010,
        PICK_ITEM: 19011,
        CANCEL_TIMEOUT: 19012
    },

    // 14. Battle Royale / Narrow Area Mode (nNetId: 110000)
    BattleRoyale: {
        SHOW_UI: 110001,
        FIGHT_DATA: 110002,
        SHRINK_ZONE: 110003,
        FIGHT_START: 110004,
        GOTO_HALL: 110005,
        COUNTDOWN: 110006,
        ROUND_PREPARE: 110009,
        ROUND_END: 110010,
        ROUND_START: 110011
    },

    // 15. Real-Time Combat Sync (nNetId: 2010000 & 1010000)
    CombatSync: {
        POSITION_SYNC: 1010100,
        SHIELD_DATA: 2010002,
        FORCE_VELOCITY: 2010105
    }
};

module.exports = Protocol;
