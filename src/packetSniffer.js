/**
 * Triggerun Smart Packet Sniffer & Dissector
 * Provides real-time decoding, human-readable formatting,
 * and automatic Assembly-CSharp cross-referencing.
 */

const utils = require('./utils');
const protocol = require('./protocol');
const fs = require('fs');
const path = require('path');

// In-memory protocol opcode reverse dictionary
const opcodeNames = {};

function buildOpcodeLookup() {
    function recurse(obj, prefix = '') {
        for (const key in obj) {
            const val = obj[key];
            if (typeof val === 'number') {
                opcodeNames[val] = prefix ? `${prefix}.${key}` : key;
            } else if (typeof val === 'object' && val !== null) {
                recurse(val, prefix ? `${prefix}.${key}` : key);
            }
        }
    }
    recurse(protocol);
}
buildOpcodeLookup();

// Load static packet map if available
let packetMap = null;
try {
    const mapPath = path.resolve(__dirname, '../data/packet_map.json');
    if (fs.existsSync(mapPath)) {
        packetMap = JSON.parse(fs.readFileSync(mapPath, 'utf8'));
    }
} catch (e) { }

class PacketSniffer {
    constructor() {
        this.enabled = true;
        this.logHeartbeats = false; // Filter out high-frequency movement/ping spam unless needed
    }

    /**
     * Inspect incoming or outgoing packet
     * @param {string} direction 'RECV' | 'SEND'
     * @param {string} channel 'LOBBY' | 'SCENE'
     * @param {string} rawPacket The raw string packet
     * @param {object} socket Socket metadata (playerName, playerIndex)
     */
    inspect(direction, channel, rawPacket, socket = {}) {
        if (!this.enabled || !rawPacket) return;

        const lines = rawPacket.split('\n').map(l => l.trim()).filter(Boolean);
        for (const line of lines) {
            this._dissectLine(direction, channel, line, socket);
        }
    }

    _dissectLine(direction, channel, line, socket) {
        const isRecv = direction === 'RECV';
        const arrow = isRecv ? '⬅️' : '➡️';
        const pName = socket.playerName || (socket.playerIndex ? `Player_${socket.playerIndex}` : 'Guest');

        // Check if movement/ping packet (filter spam if disabled)
        if (!this.logHeartbeats) {
            if (line.includes(':SP,') || line.includes(':ST,') || line.includes(':SR,') || line.includes(':CP,') || line.includes(':SN,') || line.includes(':CN,')) {
                return;
            }
        }

        // 1. Check for SendMsgToServer / SendMsgToHallServer
        const sendMsgMatch = /SendMsgTo(?:Hall)?Server,\(([a-zA-Z0-9_\-]+)(?:,(.*))?\)/.exec(line);
        if (sendMsgMatch) {
            const rawOp = sendMsgMatch[1];
            const rawArgs = sendMsgMatch[2] || '()';
            const numOp = utils.decodeInt(rawOp);
            const opName = opcodeNames[numOp] || `Unknown_${numOp}`;
            const callerLoc = (packetMap && packetMap.uniqueOps && packetMap.uniqueOps[numOp])
                ? ` [Assembly: ${packetMap.uniqueOps[numOp][0]}]`
                : '';

            const decodedArgs = this._decodePayload(rawArgs);
            console.log(`\x1b[36m[${channel}] ${arrow} [OPCODE ${numOp} (${rawOp}): ${opName}]\x1b[0m ${pName}${callerLoc}`);
            console.log(`   \x1b[90mPayload:\x1b[0m`, decodedArgs);
            return;
        }

        // 2. Check for SendMsgToClient / SendMsgToHallClient
        const clientMsgMatch = /SendMsgTo(?:Hall)?Client,\(([a-zA-Z0-9_\-]+),\(?([a-zA-Z0-9_\-]+)?(?:,(.*))?\)?\)/.exec(line);
        if (clientMsgMatch) {
            const netIdRaw = clientMsgMatch[1];
            const subCmdRaw = clientMsgMatch[2];
            const rawArgs = clientMsgMatch[3] || '';

            const netId = utils.decodeInt(netIdRaw);
            const subCmd = subCmdRaw ? utils.decodeInt(subCmdRaw) : null;

            const netIdName = opcodeNames[netId] || `NetID_${netId}`;
            const subCmdName = subCmd ? (opcodeNames[subCmd] || `SubCmd_${subCmd}`) : '';

            console.log(`\x1b[32m[${channel}] ${arrow} [CLIENT MSG ${netId} (${netIdRaw}) -> ${subCmd ? `${subCmd} (${subCmdRaw}): ${subCmdName}` : netIdName}]\x1b[0m`);
            return;
        }

        // 3. Check for Direct RPC (e1:MethodName,(params))
        const rpcMatch = /^e[0-9]*:([a-zA-Z0-9_]+),?(.*)/.exec(line);
        if (rpcMatch) {
            const methodName = rpcMatch[1];
            const rawParams = rpcMatch[2] || '';
            console.log(`\x1b[35m[${channel}] ${arrow} [⚡ RPC: ${methodName}]\x1b[0m ${pName}`);
            if (rawParams && rawParams.length > 2) {
                console.log(`   \x1b[90mParams:\x1b[0m ${rawParams.substring(0, 160)}${rawParams.length > 160 ? '...' : ''}`);
            }
            return;
        }

        // Default: general line
        if (!line.startsWith('NGOL')) {
            console.log(`\x1b[33m[${channel}] ${arrow} [RAW]\x1b[0m ${line.substring(0, 120)}`);
        }
    }

    _decodePayload(payloadStr) {
        if (!payloadStr) return '';
        // If hex encoded string, try decoding preview
        const hexMatches = payloadStr.match(/'([0-9a-fA-F]{4,})'/g);
        let preview = payloadStr;
        if (hexMatches) {
            hexMatches.forEach(h => {
                const cleanHex = h.replace(/'/g, '');
                try {
                    const text = Buffer.from(cleanHex, 'hex').toString('utf8');
                    if (/^[\x20-\x7E\u00A0-\u024F\u1E00-\u1EFF]+$/.test(text)) {
                        preview = preview.replace(h, `'${cleanHex}' ("${text}")`);
                    }
                } catch (e) {}
            });
        }
        return preview;
    }
}

module.exports = new PacketSniffer();
