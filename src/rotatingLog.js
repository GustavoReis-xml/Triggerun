const fs = require('fs');
const path = require('path');

// One writer per file/process; serialized appends cannot race rotation.
function createRotatingLog(file, { maxBytes = 10 * 1024 * 1024, backups = 5, onError = e => process.stderr.write(`[log] ${e.message}\n`) } = {}) {
    if (!Number.isInteger(maxBytes) || maxBytes < 128 || !Number.isInteger(backups) || backups < 1) throw new Error('Invalid log retention');
    let lastError = 0;
    return function append(line) {
        try {
            fs.mkdirSync(path.dirname(file), { recursive: true });
            const bytes = Buffer.byteLength(line);
            if (bytes > maxBytes) throw new Error('Log record exceeds file limit');
            const size = fs.existsSync(file) ? fs.statSync(file).size : 0;
            if (size && size + bytes > maxBytes) {
                const oldest = `${file}.${backups}`;
                if (fs.existsSync(oldest)) fs.unlinkSync(oldest);
                for (let i = backups - 1; i >= 1; i--) {
                    if (fs.existsSync(`${file}.${i}`)) fs.renameSync(`${file}.${i}`, `${file}.${i + 1}`);
                }
                fs.renameSync(file, `${file}.1`);
            }
            fs.appendFileSync(file, line, 'utf8');
            return true;
        } catch (err) {
            if (Date.now() - lastError > 60000) {
                lastError = Date.now();
                try { onError(err); } catch (_) { /* Logging must not stop gameplay. */ }
            }
            return false;
        }
    };
}
module.exports = { createRotatingLog };
