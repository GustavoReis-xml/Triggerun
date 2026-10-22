const path = require('path');
const { randomUUID } = require('crypto');
const { createRotatingLog } = require('./rotatingLog');
const runId = randomUUID();
const telemetryFile = path.join(process.env.TR_LOG_DIR || path.join(__dirname, '..', 'logs'), 'ai_telemetry.log');
const append = createRotatingLog(telemetryFile);
let sequence = 0;
function logTelemetry(type, details = {}) {
    try {
        let record = { ...details, t: new Date().toISOString(), type, runId, seq: ++sequence };
        let line = JSON.stringify(record);
        if (Buffer.byteLength(line) > 64 * 1024) {
            record = { t: record.t, type, runId, seq: sequence, player: record.player,
                connectionId: record.connectionId, room: record.room, phase: record.phase,
                truncated: true, preview: line.slice(0, 8000) };
            line = JSON.stringify(record);
        }
        return append(line + '\n');
    } catch (_) { return false; }
}
logTelemetry('SERVER_START', { pid: process.pid });
module.exports = { logTelemetry, telemetryFile, runId };
