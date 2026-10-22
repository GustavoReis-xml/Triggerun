/**
 * Complete Packet & RPC Dissector for Triggerun
 * Extracts both Client -> Server and Server -> Client packets with payloads and parameters.
 */

const fs = require('fs');
const path = require('path');
const utils = require('../src/utils');

const ASSEMBLY_DIR = path.resolve(__dirname, '../Assembly/Assembly-CSharp');

function analyzeProtocols() {
    const files = fs.readdirSync(ASSEMBLY_DIR).filter(f => f.endsWith('.cs'));
    const serverReceivers = [];
    const rpcMethods = [];

    files.forEach(file => {
        const fullPath = path.join(ASSEMBLY_DIR, file);
        const content = fs.readFileSync(fullPath, 'utf8');
        const lines = content.split('\n');

        // Look for switch cases on opcodes (e.g. switch(nNetID), switch(subCmd))
        lines.forEach((line, idx) => {
            const caseMatch = /case\s+([0-9]+)\s*:/g.exec(line);
            if (caseMatch) {
                const op = parseInt(caseMatch[1], 10);
                if (op > 100) {
                    const enc = utils.encodeInt(op);
                    serverReceivers.push({
                        file,
                        line: idx + 1,
                        opcode: op,
                        encoded: enc,
                        context: lines.slice(Math.max(0, idx - 2), Math.min(lines.length, idx + 5)).map(l => l.trim()).join(' ')
                    });
                }
            }

            // Look for public RPC methods (methods with ArrayList or (ArrayList, ArrayList))
            const rpcMatch = /public\s+(?:override\s+|virtual\s+)?void\s+([a-zA-Z0-9_]+)\s*\((ArrayList\s+[a-zA-Z0-9_]+(?:,\s*ArrayList\s+[a-zA-Z0-9_]+)?|object\s+[a-zA-Z0-9_]+)\)/g.exec(line);
            if (rpcMatch && (file.includes('Player') || file.includes('Living') || file.includes('Hero') || file.includes('Npc'))) {
                rpcMethods.push({
                    file,
                    line: idx + 1,
                    method: rpcMatch[1],
                    params: rpcMatch[2]
                });
            }
        });
    });

    console.log(`\n=== 📥 CLIENT PACKET RECEIVERS & HANDLERS DETECTED: ${serverReceivers.length} cases ===`);
    const uniqueRecv = {};
    serverReceivers.forEach(r => {
        if (!uniqueRecv[r.opcode]) uniqueRecv[r.opcode] = [];
        uniqueRecv[r.opcode].push(`${r.file}:${r.line}`);
    });

    Object.keys(uniqueRecv).sort((a,b) => a-b).forEach(op => {
        const enc = utils.encodeInt(parseInt(op, 10));
        console.log(`[Handler ${op.padEnd(8)}] Encoded: ${enc.padEnd(6)} | Receivers: ${uniqueRecv[op].join(', ')}`);
    });

    console.log(`\n=== ⚡ CLIENT RPC METHODS (e1:MethodName): ${rpcMethods.length} methods ===`);
    const uniqueMethods = {};
    rpcMethods.forEach(m => {
        if (!uniqueMethods[m.method]) uniqueMethods[m.method] = [];
        uniqueMethods[m.method].push(`${m.file} (${m.params})`);
    });

    Object.keys(uniqueMethods).sort().forEach(name => {
        console.log(`[RPC ${name.padEnd(20)}] Signatures: ${uniqueMethods[name].slice(0, 2).join(' | ')}`);
    });

    const fullMap = {
        receiversCount: serverReceivers.length,
        rpcCount: rpcMethods.length,
        receivers: uniqueRecv,
        rpcMethods: uniqueMethods
    };

    fs.writeFileSync(path.resolve(__dirname, '../data/protocol_full_map.json'), JSON.stringify(fullMap, null, 2));
    console.log(`\n✅ Full protocol map written to data/protocol_full_map.json`);
}

analyzeProtocols();
