const path = require('path');

const SERVER_IP = process.env.SERVER_IP || "26.1.99.14";
const logFile = 'e:\\server tr\\server_log.txt';
const npcXmlPath = 'e:\\server tr\\downloads\\baxi\\Npc.xml';
const filesDirXmlPath = 'e:\\server tr\\files_dir.xml';
const publicDir = 'e:\\server tr\\public';
const itemInfoXmlPath = 'e:\\server tr\\zh\\iteminfo.xml';
const shopDataXmlPath = 'e:\\server tr\\zh\\shopdata.xml';
const heroUnlockXmlPath = 'e:\\server tr\\zh\\herounlock.xml';
const playerFaceXmlPath = 'e:\\server tr\\public\\baxi\\playerface.xml';
const playerPaintXmlPath = 'e:\\server tr\\public\\baxi\\playerpaint.xml';
const cdnCacheDir = 'e:\\server tr\\downloads\\cdn_cache';
const customSpraysDir = 'e:\\server tr\\public\\sprays_custom';

const httpPort = 80;
const tcpPort = 7777;

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

