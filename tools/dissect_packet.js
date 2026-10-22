/**
 * CLI Tool: Dissect and analyze any raw packet string
 * Usage: node tools/dissect_packet.js "<packet_string>"
 */

const utils = require('../src/utils');
const protocol = require('../src/protocol');
const fs = require('fs');
const path = require('path');

const rawInput = process.argv[2];

if (!rawInput) {
    console.log("Usage: node tools/dissect_packet.js \"<raw_packet_or_payload>\"");
    console.log("Example: node tools/dissect_packet.js \"e1:SendMsgToClient,(4si,(LyI,('6b696e67',1,VZ,1,0,1,Fr,1,0,0,VZ,'',0,(),1,0,0,0,(),())))\"");
    process.exit(0);
}

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

console.log("\n==========================================");
console.log("🔍 TRIGGERUN PACKET DISSECTOR");
console.log("==========================================");
console.log("Raw Input:", rawInput);
console.log("------------------------------------------");

// 1. Check RPC
const rpcMatch = /^e([0-9]*):([a-zA-Z0-9_]+),?(.*)/.exec(rawInput);
if (rpcMatch) {
    const pIdx = rpcMatch[1] || '0';
    const method = rpcMatch[2];
    const args = rpcMatch[3];
    console.log(`📡 Type: RPC CALL`);
    console.log(`👤 Target/Sender Player Index: ${pIdx}`);
    console.log(`⚡ Method Name: ${method}`);
    console.log(`📦 Arguments: ${args}`);
}

// 2. Token extraction (Radix64 numbers and hex strings)
const tokens = rawInput.match(/[a-zA-Z0-9_\-]+/g) || [];
console.log("\n--- Decoded Tokens ---");
tokens.forEach(tok => {
    const decInt = utils.decodeInt(tok);
    const opName = opcodeNames[decInt] ? ` [${opcodeNames[decInt]}]` : '';
    
    // Check if valid hex UTF-8
    let hexDec = null;
    if (tok.length >= 4 && /^[0-9a-fA-F]+$/.test(tok) && tok.length % 2 === 0) {
        try {
            const str = Buffer.from(tok, 'hex').toString('utf8');
            if (/^[\x20-\x7E\u00A0-\u024F\u1E00-\u1EFF]+$/.test(str)) {
                hexDec = str;
            }
        } catch (e) {}
    }

    if (hexDec) {
        console.log(`- Token '${tok}' => Hex String: "${hexDec}" | Radix64 Int: ${decInt}${opName}`);
    } else if (decInt > 0 && decInt < 10000000) {
        console.log(`- Token '${tok}' => Radix64 Int: ${decInt}${opName}`);
    }
});

// 3. Structured ArrayList parsing
try {
    const parenIdx = rawInput.indexOf('(');
    if (parenIdx !== -1) {
        const arrayStr = rawInput.substring(parenIdx);
        const parsed = utils.parseArrayList(arrayStr);
        console.log("\n--- Parsed ArrayList Structure ---");
        console.dir(parsed, { depth: null, colors: true });
    }
} catch (e) {
    console.log("Could not parse as nested ArrayList:", e.message);
}

console.log("==========================================\n");
