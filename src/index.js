// Ponto de entrada unificador do servidor Trigger Run
const fs = require('fs');
const config = require('./config');

const logFile = config.logFile;
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

global.crashLogs = [];
global.lastMapId = 5205;
global.lastMapName = 'xinshouyindao_3';
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
    const msg = args.map(arg => {
        if (arg instanceof Error) {
            return `${arg.message}\n${arg.stack}`;
        }
        return typeof arg === 'object' ? JSON.stringify(arg) : String(arg);
    }).join(' ');
    
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
