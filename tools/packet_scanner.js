/**
 * Packet & Protocol Scanner for Triggerun Unity Client
 * Analyzes Assembly/Assembly-CSharp to extract:
 * 1. Every SendMessageToServer call (Client -> Server)
 * 2. Every MSGClient / OnMatchMsg / RPC handler (Server -> Client)
 */

const fs = require('fs');
const path = require('path');
const utils = require('../src/utils');

const ASSEMBLY_DIR = path.resolve(__dirname, '../Assembly/Assembly-CSharp');

function scanAssembly() {
    if (!fs.existsSync(ASSEMBLY_DIR)) {
        console.error("Assembly dir not found:", ASSEMBLY_DIR);
        return;
    }

    const files = fs.readdirSync(ASSEMBLY_DIR).filter(f => f.endsWith('.cs'));
    console.log(`Analyzing ${files.length} C# source files in Assembly-CSharp...`);

    const clientSent = [];

    // Regexes
    const sendRegex = /SendMessageToServer\s*\(\s*([0-9a-zA-Z_]+)\s*,\s*([^;\n]+)\)/g;
    const hallSendRegex = /SendMsgToHallServer\s*\(\s*([0-9a-zA-Z_]+)\s*,\s*([^;\n]+)\)/g;

    files.forEach(file => {
        const fullPath = path.join(ASSEMBLY_DIR, file);
        const content = fs.readFileSync(fullPath, 'utf8');
        const lines = content.split('\n');

        lines.forEach((line, lineIdx) => {
            let match;
            while ((match = sendRegex.exec(line)) !== null) {
                const op = match[1];
                let numOp = parseInt(op, 10);
                if (isNaN(numOp)) numOp = op;
                const enc = typeof numOp === 'number' ? utils.encodeInt(numOp) : '?';
                clientSent.push({
                    file,
                    line: lineIdx + 1,
                    opcode: numOp,
                    encoded: enc,
                    snippet: line.trim()
                });
            }

            while ((match = hallSendRegex.exec(line)) !== null) {
                const op = match[1];
                let numOp = parseInt(op, 10);
                if (isNaN(numOp)) numOp = op;
                const enc = typeof numOp === 'number' ? utils.encodeInt(numOp) : '?';
                clientSent.push({
                    file,
                    line: lineIdx + 1,
                    type: 'Hall',
                    opcode: numOp,
                    encoded: enc,
                    snippet: line.trim()
                });
            }
        });
    });

    console.log(`\n=== 📤 CLIENT TO SERVER PACKETS DETECTED: ${clientSent.length} calls ===`);
    const uniqueOps = {};
    clientSent.forEach(s => {
        if (!uniqueOps[s.opcode]) uniqueOps[s.opcode] = [];
        uniqueOps[s.opcode].push(`${s.file}:${s.line}`);
    });

    const sortedOps = Object.keys(uniqueOps).sort((a, b) => {
        const na = parseInt(a, 10) || 0;
        const nb = parseInt(b, 10) || 0;
        return na - nb;
    });

    sortedOps.forEach(op => {
        const num = parseInt(op, 10);
        const enc = !isNaN(num) ? utils.encodeInt(num) : '?';
        const callers = uniqueOps[op].slice(0, 3).join(', ');
        const extra = uniqueOps[op].length > 3 ? ` (+${uniqueOps[op].length - 3} more)` : '';
        console.log(`[Opcode ${op.toString().padEnd(10)}] Encoded: ${enc.padEnd(6)} | Calls: ${uniqueOps[op].length.toString().padEnd(3)} | Used in: ${callers}${extra}`);
    });

    const outputPath = path.resolve(__dirname, '../data/packet_map.json');
    fs.writeFileSync(outputPath, JSON.stringify({ totalCalls: clientSent.length, uniqueOpcodesCount: sortedOps.length, uniqueOps, clientSent }, null, 2));
    console.log(`\n✅ Packet map saved to: ${outputPath}`);
}

scanAssembly();
