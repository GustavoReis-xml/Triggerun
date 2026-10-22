// Ponto de entrada unificador do servidor Trigger Run
const { createRotatingLog } = require('./rotatingLog');
const logger = require('./logger');
const { format } = require('util');
const config = require('./config');

const logFile = config.logFile;
const appendLog = createRotatingLog(logFile, { maxBytes: 50 * 1024 * 1024, backups: 3 });
appendLog(`=== SERVER RESTART ${logger.runId} AT ${new Date().toISOString()} ===\n`);

const originalLog = console.log;
const originalError = console.error;

console.log = function(...args) {
    originalLog.apply(console, args);
    const msg = format(...args);
    appendLog(msg.slice(0, 65536) + '\n');
};

console.error = function(...args) {
    originalError.apply(console, args);
    const msg = format(...args);
    appendLog(`[⚠️ ERRO] ${msg.slice(0, 65536)}\n`);
};

global.crashLogs = [];
global.lastMapId = null;
global.lastMapName = null;
global.lastFinalMapName = 'jiaodouchang_02_3';
global.lastFinalMapId = 5204;
global.lastLobbySocket = null;
global.inMatch = false;
global.currentRoom = null;
global.globalPlayerCounter = 0;
global.PLAYER_NAME = "Admin";
global.PLAYER_NAME_ENC = "41646d696e";
global.SERVER_IP = config.SERVER_IP;

const originalConsoleError = console.error;
console.error = function (...args) {
    originalConsoleError.apply(console, args);
    const msg = format(...args);
    
    logger.logTelemetry('SERVER_ERROR', { message: msg });
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

// 1. Inicializa o Banco de Dados Estático (XMLs)
const db = require('./database');
db.initializeDatabase();

// 2. Inicializa o servidor Web (Express)
const app = require('./http');
app.listen(config.httpPort, () => {
    console.log(`===================================================`);
    console.log(`[🌐 WEB SERVER] Express rodando na porta ${config.httpPort}...`);
    console.log(`===================================================`);
});

// 3. Inicializa o servidor de Sockets TCP e Loop de IA
const socketServer = require('./socketServer');
