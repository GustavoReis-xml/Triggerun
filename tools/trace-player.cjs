const fs = require('fs');
const path = require('path');
const readline = require('readline');

async function main() {
    const args = process.argv.slice(2), options = {};
    for (let i = 0; i < args.length; i += 2) {
        if (!['--player', '--room', '--dir', '--limit'].includes(args[i]) || !args[i + 1]) throw new Error('Use --player NAME and/or --room ID [--dir logs] [--limit 200]');
        options[args[i]] = args[i + 1];
    }
    if (!options['--player'] && !options['--room']) throw new Error('Choose --player or --room');
    const limit = Number(options['--limit'] || 200);
    if (!Number.isInteger(limit) || limit < 1 || limit > 10000) throw new Error('Limit must be 1..10000');
    const dir = path.resolve(options['--dir'] || path.join(__dirname, '..', 'logs'));
    const files = fs.readdirSync(dir).filter(n => /^ai_telemetry\.log(?:\.\d+)?$/.test(n))
        .sort((a,b) => Number(b.split('.')[2] || 0) - Number(a.split('.')[2] || 0));
    const rows = [];
    for (const name of files) {
        const input = fs.createReadStream(path.join(dir, name));
        const lines = readline.createInterface({ input, crlfDelay: Infinity });
        for await (const line of lines) {
            let event; try { event = JSON.parse(line); } catch (_) { continue; }
            if (options['--player'] && event.player !== options['--player']) continue;
            if (options['--room'] && String(event.room) !== options['--room']) continue;
            rows.push(event); if (rows.length > limit) rows.shift();
        }
    }
    for (const row of rows) process.stdout.write(JSON.stringify(row) + '\n');
}
main().catch(e => { process.stderr.write(e.message + '\n'); process.exitCode = 1; });
