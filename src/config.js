const path = require('path');

const ROOT_DIR = path.resolve(__dirname, '..');
const SERVER_IP = process.env.SERVER_IP || "136.248.105.85";
const logFile = path.join(ROOT_DIR, 'server_log.txt');
const npcXmlPath = path.join(ROOT_DIR, 'downloads', 'baxi', 'Npc.xml');
const filesDirXmlPath = path.join(ROOT_DIR, 'files_dir.xml');
const publicDir = path.join(ROOT_DIR, 'public');
const itemInfoXmlPath = path.join(ROOT_DIR, 'zh', 'iteminfo.xml');
const shopDataXmlPath = path.join(ROOT_DIR, 'zh', 'shopdata.xml');
const heroUnlockXmlPath = path.join(ROOT_DIR, 'zh', 'herounlock.xml');
const playerFaceXmlPath = path.join(ROOT_DIR, 'public', 'baxi', 'playerface.xml');
const playerPaintXmlPath = path.join(ROOT_DIR, 'public', 'baxi', 'playerpaint.xml');
const cdnCacheDir = path.join(ROOT_DIR, 'downloads', 'cdn_cache');
const customSpraysDir = path.join(ROOT_DIR, 'public', 'sprays_custom');

const httpPort = parseInt(process.env.HTTP_PORT, 10) || 80;
const tcpPort = parseInt(process.env.TCP_PORT, 10) || 7777;

module.exports = {
    SERVER_IP,
    logFile,
    npcXmlPath,
    filesDirXmlPath,
    publicDir,
    itemInfoXmlPath,
    shopDataXmlPath,
    heroUnlockXmlPath,
    playerFaceXmlPath,
    playerPaintXmlPath,
    cdnCacheDir,
    customSpraysDir,
    httpPort,
    tcpPort
};

