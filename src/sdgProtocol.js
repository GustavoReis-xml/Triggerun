// CBulletMe.sendDamageToServer / HeroPlayer.DamageMsgToServer.
// The envelope contains 15 fields; only field 0 contains hit records.
function decodeDamageEnvelope(parsed) {
    while (Array.isArray(parsed) && parsed.length === 1 && Array.isArray(parsed[0])) parsed = parsed[0];
    if (!Array.isArray(parsed) || parsed.length < 15 || !Array.isArray(parsed[0])) return null;
    const hits = parsed[0].filter(hit => Array.isArray(hit) && hit.length >= 10 &&
        typeof hit[0] === 'number' && typeof hit[1] === 'number' &&
        typeof hit[8] === 'string' && Array.isArray(hit[4]) && Array.isArray(hit[6]) && Array.isArray(hit[9]));
    return { payload: parsed, hits };
}
module.exports = { decodeDamageEnvelope };
