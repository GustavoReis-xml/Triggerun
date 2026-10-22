// Utilitários de codificação e helpers de protocolo do servidor Trigger Run

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

function decodeHex(s) {
    if (!s || typeof s !== 'string') return "";
    let str = s;
    if (str.startsWith("'") && str.endsWith("'")) str = str.slice(1, -1);
    try {
        if (/^[0-9a-fA-F]+$/.test(str)) {
            return Buffer.from(str, 'hex').toString('utf8');
        }
        return str;
    } catch (e) {
        return str;
    }
}

function encodeHex(s) {
    if (!s) return "";
    return Buffer.from(s, 'utf8').toString('hex');
}

function generateUnlockList(range = 15) {
    let list = [];
    for (let i = 1; i <= range; i++) {
        const id = i;
        const info = `(${encodeInt(id)},1,0,0,'2099/01/01 12:00:00',0,())`;
        list.push(`${encodeInt(id)},${info}`);
    }
    return `(${list.join(',')})`;
}

function parseArrayList(str) {
    if (!str) return [];
    str = str.trim();
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
    if (/^-?[0-9A-Z_`a-z-]+$/.test(val)) {
        return decodeInt(val);
    }
    if (!isNaN(val) && val !== "") return Number(val);
    return val;
}

function encodingUTF8(str) {
    const bytes = Buffer.from(str, 'utf8');
    const hex = Array.from(bytes).map(b => b.toString(16).padStart(2, '0')).join('');
    return `[mi]${hex}[/mi]`;
}

function logPacket(type, direction, data) {
    if (!data || typeof data !== 'string') return;

    // Filtro de alta frequência para evitar poluição no terminal do servidor
    const isSync = data.includes(':SP,') || 
                   data.includes(':SN,') || 
                   data.includes(':CP,') || 
                   data.includes(':CN,') ||
                   data.includes(':ST,') ||
                   data.includes(':SR,') ||
                   data.includes(':SV,') ||
                   data.includes(':SA,') ||
                   data.includes(':CT,') ||
                   data.includes(':CR,') ||
                   data.includes(':CV,') ||
                   data.includes(':CA,') ||
                   data.includes('SendMsgToAllClient,(CDH') ||
                   data.includes('SendMsgToClient,(CDH');
    if (isSync) return;

    const idMatch = data.match(/\(([0-9A-Z_`a-z-]+),/);
    const idStr = idMatch ? idMatch[1] : '??';
    let idLabel = idStr;
    try {
        const idNum = decodeInt(idStr);
        idLabel = `${idNum} (${idStr})`;
    } catch (e) { }

    console.log(`[${type}] ${direction === 'IN' ? '⬅️' : '➡️'} ID: ${idLabel} | DATA: ${data.trim().substring(0, 150)}...`);
}

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

async function sendPacket(socket, data, delay = 0) {
    const isCurrent = socket ? require('./sceneScope').capture(socket) : () => false;
    if (delay > 0) await new Promise(r => setTimeout(r, delay));
    if (socket && isCurrent()) {
        let finalData = data;
        if (socket.socketType === "SCENE" && !socket.bypassInterceptor && (data.includes('SendMsgToHallClient') || data.includes('SendMsgToClient'))) {
            const idxPrefix = `e${encodeInt(socket.playerIndex || 1)}:`;
            finalData = data.replace(/^e[0-9A-Z_`a-z]+:(?=SendMsgTo(?:Hall)?Client,)/gm, idxPrefix);
        }
        socket.write(finalData);
        logPacket(socket.socketType || 'HALL', 'OUT', finalData);
    }
}

const Fun = {
    GetServerDate: () => {
        const d = new Date();
        const pad = (n) => n.toString().padStart(2, '0');
        return `${d.getFullYear()}/${pad(d.getMonth() + 1)}/${pad(d.getDate())} ${pad(d.getHours())}:${pad(d.getMinutes())}:${pad(d.getSeconds())}`;
    },
    DecodingUTF8: (s) => decodeHex(s),
    EncodingUTF8: (s) => encodeHex(s),
    Now: () => new Date().toISOString().replace(/T/, ' ').replace(/\..+/, '')
};

class RawString {
    constructor(str) {
        this.str = str;
    }
    toString() {
        return this.str;
    }
}

const raw = (str) => new RawString(str);

// Helper para formatar argumentos no estilo C# do jogo
function formatArg(arg) {
    if (arg instanceof RawString) return arg.toString();
    if (typeof arg === 'number') return encodeInt(arg);
    if (typeof arg === 'string') return `'${arg}'`;
    if (Array.isArray(arg)) return `(${arg.map(formatArg).join(',')})`;
    if (arg === null || arg === undefined) return '';
    return arg.toString();
}

class PacketBuilder {
    // Cria chamada de evento: e{netId}:{funcao}(arg1, arg2...)
    static rpc(netId, functionName, args = []) {
        const idEnc = netId ? encodeInt(netId) : '';
        const formattedArgs = args.map(formatArg).join(',');
        return `e${idEnc}:${functionName},(${formattedArgs})\n`;
    }

    // Cria criacao de objeto: c{classe}:{netId},({funcao},{arg1},{arg2}...)
    static create(className, netId, functionName = null, args = []) {
        const idEnc = encodeInt(netId);
        let packet = `c${className}:${idEnc}`;
        if (functionName) {
            const allArgs = [functionName, ...args];
            packet += `,(${allArgs.map(formatArg).join(',')})`;
        }
        return packet + '\n';
    }

    // Destroi objeto: d{netId}
    static destroy(netId) {
        return `d${encodeInt(netId)}\n`;
    }
}

function fixSbToCbArgs(str, fallbackLoadId) {
    if (!str) return str;
    let result = [];
    let current = '';
    let depth = 0;
    let inQuotes = false;
    for (let i = 0; i < str.length; i++) {
        const char = str[i];
        if (char === "'" && (i === 0 || str[i - 1] !== '\\')) inQuotes = !inQuotes;
        if (!inQuotes) {
            if (char === '(') depth++;
            if (char === ')') depth--;
            if (char === ',' && depth === 0) {
                result.push(current.trim());
                current = '';
                continue;
            }
        }
        current += char;
    }
    if (current.trim() !== '') result.push(current.trim());
    
    // NetLiving.CB(int weaponName, int launcherNo, string muzzleName, int bullId, int bullettype, ArrayList born, ArrayList target, ArrayList parm, int numberBullet, int _cartridge, int _loadid)
    // Expects EXACTLY 11 arguments!
    if (result.length >= 11) {
        const cartridge = result[10] !== undefined ? result[10] : '0';
        const realLoadId = (result.length > 11 && result[result.length - 1] !== undefined)
            ? result[result.length - 1]
            : (fallbackLoadId !== undefined ? encodeInt(fallbackLoadId) : '1');
        
        const cbParams = result.slice(0, 9);
        cbParams.push(cartridge);
        cbParams.push(realLoadId);
        return cbParams.join(',');
    }
    return result.join(',');
}

module.exports = {
    fixSbToCbArgs,
    ALPHABET,
    encodeInt,
    decodeInt,
    decodeHex,
    encodeHex,
    generateUnlockList,
    parseArrayList,
    parseValue,
    encodingUTF8,
    logPacket,
    sendPacket,
    sleep,
    Fun,
    RawString,
    raw,
    formatArg,
    PacketBuilder
};
