// Central de Estado Global do Servidor Trigger Run

const activeSockets = [];
const spawnedNpcs = {};
const activeBots = {};
const ipNames = {};
const ipIndexes = {};
const playerSessions = {};
const playerInventory = {};
const playerEquipment = {};



module.exports = {
    activeSockets,
    spawnedNpcs,
    activeBots,
    ipNames,
    ipIndexes,
    playerSessions,
    playerInventory,
    playerEquipment,
    
    // Getters e setters/incrementadores para tipos primitivos
    getGlobalPlayerCounter() {
        return global.globalPlayerCounter;
    },
    incrementGlobalPlayerCounter() {
        global.globalPlayerCounter++;
        return global.globalPlayerCounter;
    },
    getLastLobbySocket() {
        return global.lastLobbySocket;
    },
    setLastLobbySocket(socket) {
        global.lastLobbySocket = socket;
    },
    getLastMapId() {
        return global.lastMapId;
    },
    setLastMapId(id) {
        global.lastMapId = id;
    }
};
