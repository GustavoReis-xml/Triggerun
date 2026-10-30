const utils = require('./utils');
const dbPersistence = require('./dbPersistence');

const { encodeInt, encodeHex, decodeHex } = utils;

// Estado em memória para Esquadrões (Party/Team - Opcode 60000)
let nextTeamId = 1;
const activeTeams = new Map(); // teamId -> { teamId, captainName, members: [username, ...] }
const playerTeamMap = new Map(); // usernameLower -> teamId

function decodeClientText(raw) {
    if (raw === null || raw === undefined) return '';
    let s = String(raw).trim();
    if (s.startsWith("'") && s.endsWith("'")) s = s.slice(1, -1);
    const miMatch = s.match(/^\[mi\]([0-9a-fA-F]+)\[\/mi\]$/i);
    if (miMatch) {
        try {
            return Buffer.from(miMatch[1], 'hex').toString('utf8');
        } catch (_) {}
    }
    if (/^[0-9a-fA-F]+$/.test(s) && s.length % 2 === 0 && s.length >= 2) {
        try {
            const decoded = Buffer.from(s, 'hex').toString('utf8');
            if (/^[\x20-\x7E\u00A0-\uFFFF]+$/.test(decoded)) {
                return decoded;
            }
        } catch (_) {}
    }
    return decodeHex(s) || s;
}

function findPlayerProfileByName(rawName) {
    if (!rawName && rawName !== 0) return null;
    const rawStr = String(rawName).trim();
    const decoded = decodeClientText(rawStr);

    let profile = dbPersistence.loadPlayer(decoded);
    if (profile) return profile;

    if (rawStr !== decoded) {
        profile = dbPersistence.loadPlayer(rawStr);
        if (profile) return profile;
    }

    const all = dbPersistence.getAllPlayers();
    const targetLower = decoded.toLowerCase();
    return all.find(p => String(p.username || '').toLowerCase() === targetLower) || null;
}

function getLobbySocketByUsername(activeSockets, username) {
    if (!username || !Array.isArray(activeSockets)) return null;
    const target = String(username).trim().toLowerCase();
    return activeSockets.find(
        s => s && !s.destroyed && s.socketType === 'LOBBY' && String(s.playerName || '').trim().toLowerCase() === target
    ) || null;
}

function getPlayerOnlineState(activeSockets, username) {
    if (!username || !Array.isArray(activeSockets)) return 0;
    const target = String(username).trim().toLowerCase();
    const matching = activeSockets.filter(
        s => s && !s.destroyed && String(s.playerName || '').trim().toLowerCase() === target
    );
    if (matching.length === 0) return 0;
    const inGame = matching.some(
        s => s.inMatch || (s.matchPhase && s.matchPhase !== 'lobby' && s.matchPhase !== 'connected')
    );
    return inGame ? 2 : 1;
}

function formatDateForClient(isoStr) {
    if (!isoStr) return utils.Fun.GetServerDate();
    try {
        const d = new Date(isoStr);
        if (isNaN(d.getTime())) return utils.Fun.GetServerDate();
        const pad = n => String(n).padStart(2, '0');
        return `${d.getFullYear()}/${pad(d.getMonth() + 1)}/${pad(d.getDate())} ${pad(d.getHours())}:${pad(d.getMinutes())}:${pad(d.getSeconds())}`;
    } catch (_) {
        return utils.Fun.GetServerDate();
    }
}

function getTop3HeroesStr(profile) {
    const heroesMap = (profile && profile.career && profile.career.heroes) || {};
    const entries = Object.entries(heroesMap)
        .map(([heroId, stats]) => ({
            heroId: Number(heroId),
            matches: Number((stats && stats.matches) || 0)
        }))
        .filter(e => e.heroId > 0 && e.matches > 0)
        .sort((a, b) => b.matches - a.matches)
        .slice(0, 3);

    if (entries.length === 0) {
        const fallbackMatches = Number((profile && profile.career && profile.career.totalMatches) || 1);
        return `1015,${fallbackMatches}`;
    }
    return entries.map(e => `${e.heroId},${e.matches}`).join(',');
}

function getTop3HeroesEncodedTuple(profile) {
    const heroesMap = (profile && profile.career && profile.career.heroes) || {};
    const entries = Object.entries(heroesMap)
        .map(([heroId, stats]) => ({
            heroId: Number(heroId),
            matches: Number((stats && stats.matches) || 0)
        }))
        .filter(e => e.heroId > 0 && e.matches > 0)
        .sort((a, b) => b.matches - a.matches)
        .slice(0, 3);

    if (entries.length === 0) {
        const fallbackMatches = Number((profile && profile.career && profile.career.totalMatches) || 1);
        return `(${encodeInt(1015)},${encodeInt(fallbackMatches)})`;
    }
    const parts = [];
    for (const e of entries) {
        parts.push(`${encodeInt(e.heroId)},${encodeInt(e.matches)}`);
    }
    return `(${parts.join(',')})`;
}

// ============================================================================
// 1. RANKING GLOBAL (Opcode 17000 - RankManager.cs / RankUICon.cs)
// ============================================================================
function handleRankCommand(socket, rawArgs) {
    const parsed = utils.parseArrayList(rawArgs);
    const subCmd = Number(parsed[0] || 17001);
    const p17000 = encodeInt(17000);
    const sDate = utils.Fun.GetServerDate();
    const allPlayers = dbPersistence.getAllPlayers();
    const callerProfile = (socket.playerName && dbPersistence.loadPlayer(socket.playerName)) || null;
    const callerCid = callerProfile ? Number(callerProfile.cid) : Number(parsed[1] || 0);

    // 17001: USER_RANK (Nível Geral / Partidas)
    if (subCmd === 17001) {
        const nPage = Math.max(1, Number(parsed[2] || 1));
        const nIsPro = Number(parsed[3] || 0);
        const sorted = [...allPlayers].sort((a, b) => {
            const lvlDiff = (b.level || 1) - (a.level || 1);
            if (lvlDiff !== 0) return lvlDiff;
            const expDiff = (b.exp || 0) - (a.exp || 0);
            if (expDiff !== 0) return expDiff;
            return ((b.career && b.career.totalMatches) || 0) - ((a.career && a.career.totalMatches) || 0);
        });

        const pageSize = 8;
        const maxPages = Math.max(1, Math.ceil(sorted.length / pageSize));
        const safePage = Math.min(nPage, maxPages);
        const slice = sorted.slice((safePage - 1) * pageSize, safePage * pageSize);

        const myIdx = sorted.findIndex(p => Number(p.cid) === callerCid);
        const myRank0Based = myIdx >= 0 ? myIdx : 0;

        const rankItems = slice.map((p, idx) => {
            const rank1Based = (safePage - 1) * pageSize + idx + 1;
            const nameHex = encodeHex(p.username || 'Jogador');
            const lvl = encodeInt(p.level || 1);
            const exp = encodeInt(p.exp || 0);
            const face = encodeInt(p.face || 2009);
            const matches = encodeInt((p.career && p.career.totalMatches) || 0);
            const top3 = getTop3HeroesStr(p);
            return `(${encodeInt(p.cid)},'${nameHex}',${lvl},${exp},${face},${matches},'${top3}',${encodeInt(rank1Based)})`;
        }).join(',');

        const inner = `(${encodeInt(17001)},'${sDate}',(${rankItems}),${encodeInt(myRank0Based)},${encodeInt(safePage)},${encodeInt(maxPages)},${encodeInt(nIsPro)},0)`;
        socket.write(`e1:SendMsgToHallClient,(${p17000},(${encodeInt(17001)},${inner}))\n`);
        return;
    }

    // 17002: HERO_RANK (Ranking por Herói)
    if (subCmd === 17002) {
        const nHeroID = Number(parsed[2] || 1015);
        const nPage = Math.max(1, Number(parsed[3] || 1));
        const nIsPro = Number(parsed[4] || 0);
        const nSeason = Number(parsed[5] || 0);
        const hKey = String(nHeroID);

        const sorted = [...allPlayers].sort((a, b) => {
            const hA = (a.career && a.career.heroes && a.career.heroes[hKey]) || {};
            const hB = (b.career && b.career.heroes && b.career.heroes[hKey]) || {};
            const winsDiff = (hB.wins || 0) - (hA.wins || 0);
            if (winsDiff !== 0) return winsDiff;
            const matchesDiff = (hB.matches || 0) - (hA.matches || 0);
            if (matchesDiff !== 0) return matchesDiff;
            return (b.level || 1) - (a.level || 1);
        });

        const pageSize = 8;
        const maxPages = Math.max(1, Math.ceil(sorted.length / pageSize));
        const safePage = Math.min(nPage, maxPages);
        const slice = sorted.slice((safePage - 1) * pageSize, safePage * pageSize);

        const myIdx = sorted.findIndex(p => Number(p.cid) === callerCid);
        const myRank0Based = myIdx >= 0 ? myIdx : 0;

        const heroItems = slice.map((p, idx) => {
            const rank1Based = (safePage - 1) * pageSize + idx + 1;
            const hStats = (p.career && p.career.heroes && p.career.heroes[hKey]) || {};
            const matches = Number(hStats.matches || 0);
            const wins = Number(hStats.wins || 0);
            const winRate = matches > 0 ? Math.round((wins / matches) * 100) : 0;
            const playTime = Number(hStats.playTimeSeconds || matches * 180);
            const nameHex = encodeHex(p.username || 'Jogador');
            return `(${encodeInt(p.cid)},'${nameHex}',${encodeInt(p.face || 2009)},${encodeInt(nHeroID)},${encodeInt(winRate)},${encodeInt(matches)},${encodeInt(rank1Based)},${encodeInt(playTime)},${encodeInt(p.level || 1)})`;
        }).join(',');

        const inner = `(${encodeInt(17002)},'${sDate}',${encodeInt(nHeroID)},(${heroItems}),${encodeInt(myRank0Based)},${encodeInt(safePage)},${encodeInt(maxPages)},${encodeInt(nIsPro)},0,${encodeInt(nSeason)})`;
        socket.write(`e1:SendMsgToHallClient,(${p17000},(${encodeInt(17002)},${inner}))\n`);
        return;
    }

    // 17003: SKYLADDER_RANK (Competitivo / Ranked)
    if (subCmd === 17003) {
        const nSeason = Number(parsed[2] || 0);
        const nPage = Math.max(1, Number(parsed[3] || 1));

        const sorted = [...allPlayers].sort((a, b) => {
            const winsA = (a.career && a.career.wins) || 0;
            const winsB = (b.career && b.career.wins) || 0;
            if (winsB !== winsA) return winsB - winsA;
            return (b.level || 1) - (a.level || 1);
        });

        const pageSize = 8;
        const maxPages = Math.max(1, Math.ceil(sorted.length / pageSize));
        const safePage = Math.min(nPage, maxPages);
        const slice = sorted.slice((safePage - 1) * pageSize, safePage * pageSize);

        const myIdx = sorted.findIndex(p => Number(p.cid) === callerCid);
        const myRank1Based = myIdx >= 0 ? myIdx + 1 : 0;

        const skyItems = slice.map((p, idx) => {
            const rank1Based = (safePage - 1) * pageSize + idx + 1;
            const nameHex = encodeHex(p.username || 'Jogador');
            const matches = Number((p.career && p.career.totalMatches) || 0);
            const wins = Number((p.career && p.career.wins) || 0);
            const skyTier = Number(p.skyTier || Math.min(25, 1 + Math.floor(wins / 3)));
            const top3Tuple = getTop3HeroesEncodedTuple(p);
            return `(${encodeInt(p.cid)},'${nameHex}',${encodeInt(p.face || 2009)},${encodeInt(skyTier)},${encodeInt(matches)},${encodeInt(skyTier)},${encodeInt(wins)},${top3Tuple},${encodeInt(rank1Based)},${encodeInt(1)})`;
        }).join(',');

        const inner = `(${encodeInt(17003)},${encodeInt(nSeason)},${encodeInt(safePage)},(${encodeInt(myRank1Based)},${encodeInt(maxPages)},(${skyItems})))`;
        socket.write(`e1:SendMsgToHallClient,(${p17000},(${encodeInt(17003)},${inner}))\n`);
        return;
    }

    // 17005: PVE_RANK (Modo Aventura / Fortalecimento)
    if (subCmd === 17005) {
        const nPage = Math.max(1, Number(parsed[2] || 1));
        const nIsPro = Number(parsed[3] || 0);
        const nHeroType = Number(parsed[4] || 1);

        const sorted = [...allPlayers].sort((a, b) => (b.level || 1) - (a.level || 1));
        const pageSize = 8;
        const maxPages = Math.max(1, Math.ceil(sorted.length / pageSize));
        const safePage = Math.min(nPage, maxPages);
        const slice = sorted.slice((safePage - 1) * pageSize, safePage * pageSize);

        const myIdx = sorted.findIndex(p => Number(p.cid) === callerCid);
        const myRank0Based = myIdx >= 0 ? myIdx : 0;

        const pveItems = slice.map((p, idx) => {
            const rank1Based = (safePage - 1) * pageSize + idx + 1;
            const nameHex = encodeHex(p.username || 'Jogador');
            const matches = encodeInt((p.career && p.career.totalMatches) || 0);
            const top3 = getTop3HeroesStr(p);
            const qhScore = encodeInt(350 + (p.level || 1) * 10);
            return `(${encodeInt(p.cid)},'${nameHex}',${encodeInt(p.level || 1)},0,${encodeInt(p.face || 2009)},${matches},'${top3}',${encodeInt(rank1Based)},${qhScore},${encodeInt(nHeroType)})`;
        }).join(',');

        const inner = `(${encodeInt(17005)},'${sDate}',(${pveItems}),${encodeInt(myRank0Based)},${encodeInt(safePage)},${encodeInt(maxPages)},${encodeInt(nIsPro)},0,${encodeInt(nHeroType)})`;
        socket.write(`e1:SendMsgToHallClient,(${p17000},(${encodeInt(17005)},${inner}))\n`);
        return;
    }

    // 17006: DTS_RANK (Battle Royale) & 17007: FLOWER_RANK (Popularidade)
    if (subCmd === 17006 || subCmd === 17007) {
        const nPage = Math.max(1, Number(parsed[2] || 1));
        const nIsPro = Number(parsed[3] || 0);

        const sorted = [...allPlayers].sort((a, b) => {
            if (subCmd === 17007) {
                const popDiff = (b.popularity || 0) - (a.popularity || 0);
                if (popDiff !== 0) return popDiff;
            }
            const killsA = (a.career && a.career.kills) || 0;
            const killsB = (b.career && b.career.kills) || 0;
            if (killsB !== killsA) return killsB - killsA;
            return (b.level || 1) - (a.level || 1);
        });

        const pageSize = 8;
        const maxPages = Math.max(1, Math.ceil(sorted.length / pageSize));
        const safePage = Math.min(nPage, maxPages);
        const slice = sorted.slice((safePage - 1) * pageSize, safePage * pageSize);

        const myIdx = sorted.findIndex(p => Number(p.cid) === callerCid);
        const myRank0Based = myIdx >= 0 ? myIdx : 0;

        const items = slice.map((p, idx) => {
            const rank1Based = (safePage - 1) * pageSize + idx + 1;
            const nameHex = encodeHex(p.username || 'Jogador');
            if (subCmd === 17006) {
                const wins = Number((p.career && p.career.wins) || 0);
                const kills = Number((p.career && p.career.kills) || 0);
                const matches = Number((p.career && p.career.totalMatches) || 0);
                const score = 1000 + wins * 50 + kills * 10;
                return `(${encodeInt(p.cid)},'${nameHex}',${encodeInt(p.level || 1)},0,${encodeInt(p.face || 2009)},${encodeInt(score)},${encodeInt(wins)},${encodeInt(kills)},${encodeInt(matches)},${encodeInt(rank1Based)})`;
            } else {
                const pop = Number(p.popularity || ((p.career && p.career.mvps) || 0) * 10 + (p.level || 1));
                const top3 = getTop3HeroesStr(p);
                return `(${encodeInt(p.cid)},'${nameHex}',${encodeInt(p.level || 1)},0,${encodeInt(p.face || 2009)},${encodeInt(pop)},'${top3}',${encodeInt(rank1Based)})`;
            }
        }).join(',');

        const inner = `(${encodeInt(subCmd)},'${sDate}',(${items}),${encodeInt(myRank0Based)},${encodeInt(safePage)},${encodeInt(maxPages)},${encodeInt(nIsPro)},0)`;
        socket.write(`e1:SendMsgToHallClient,(${p17000},(${encodeInt(subCmd)},${inner}))\n`);
    }
}

// ============================================================================
// 2. SISTEMA DE AMIGOS (Opcodes 60118 & 60100 - FriendManager.cs)
// ============================================================================
function buildFriendTuple(profile, activeSockets) {
    const cid = encodeInt(profile.cid || 1);
    const nameHex = encodeHex(profile.username || 'Jogador');
    const level = encodeInt(profile.level || 1);
    const face = encodeInt(profile.face || 2009);
    const lastDate = formatDateForClient(profile.lastLogin);
    const state = encodeInt(getPlayerOnlineState(activeSockets, profile.username));
    const vip = encodeInt(1);
    const spread = encodeInt(0);
    const skyTier = encodeInt(profile.skyTier || 1);
    return `(${cid},'${nameHex}',${level},${face},'${lastDate}',${state},${vip},${spread},${skyTier})`;
}

function buildFriendsInitPacket(username, activeSockets) {
    const profile = findPlayerProfileByName(username);
    if (!profile) {
        return `e1:SendMsgToHallClient,(${encodeInt(60118)},((),(),()))\n`;
    }

    const friendPairs = [];
    for (const fName of (profile.friends || [])) {
        const fProf = findPlayerProfileByName(fName);
        if (fProf) {
            friendPairs.push(`'${encodeHex(fProf.username)}',${buildFriendTuple(fProf, activeSockets)}`);
        }
    }

    const latelyPairs = [];
    for (const lName of (profile.lately || [])) {
        const lProf = findPlayerProfileByName(lName);
        if (lProf && String(lProf.username).toLowerCase() !== String(profile.username).toLowerCase()) {
            latelyPairs.push(`'${encodeHex(lProf.username)}',${buildFriendTuple(lProf, activeSockets)}`);
        }
    }

    const unMsgPairs = [];
    for (const req of (profile.friendRequests || [])) {
        if (req && req.username) {
            const reqCid = encodeInt(req.cid || dbPersistence.computeDeterministicCid(req.username));
            unMsgPairs.push(`${reqCid},(${reqCid},'${encodeHex(req.username)}')`);
        }
    }

    return `e1:SendMsgToHallClient,(${encodeInt(60118)},((${friendPairs.join(',')}),(${latelyPairs.join(',')}),(${unMsgPairs.join(',')})))\n`;
}

function notifyFriendsState(username, activeSockets) {
    const profile = findPlayerProfileByName(username);
    if (!profile || !Array.isArray(profile.friends)) return;
    const myState = getPlayerOnlineState(activeSockets, profile.username);
    const p60100 = encodeInt(60100);
    const s60104 = encodeInt(60104);
    const myNameHex = encodeHex(profile.username);

    for (const fName of profile.friends) {
        const fSocket = getLobbySocketByUsername(activeSockets, fName);
        if (fSocket) {
            fSocket.write(`e1:SendMsgToHallClient,(${p60100},(${s60104},('${myNameHex}',${encodeInt(myState)})))\n`);
        }
    }
}

function recordRecentPlayers(usernames) {
    if (!Array.isArray(usernames) || usernames.length < 2) return;
    const cleanList = usernames.map(u => String(u || '').trim()).filter(Boolean);
    for (const u of cleanList) {
        const prof = findPlayerProfileByName(u);
        if (!prof) continue;
        const others = cleanList.filter(o => o.toLowerCase() !== prof.username.toLowerCase());
        const current = Array.isArray(prof.lately) ? prof.lately : [];
        for (const other of others) {
            const existingIdx = current.findIndex(x => String(x).toLowerCase() === other.toLowerCase());
            if (existingIdx >= 0) current.splice(existingIdx, 1);
            current.unshift(other);
        }
        prof.lately = current.slice(0, 10);
        dbPersistence.savePlayer(prof.username, prof);
    }
}

function handleFriendCommand(socket, rawArgs, activeSockets) {
    const parsed = utils.parseArrayList(rawArgs);
    const subCmd = Number(parsed[0] || 0);
    const p60100 = encodeInt(60100);
    const myProfile = findPlayerProfileByName(socket.playerName);
    if (!myProfile) return;

    // 60101: Adicionar amigo [60101, targetNameEncoded]
    if (subCmd === 60101) {
        const rawTarget = parsed[1];
        const targetProfile = findPlayerProfileByName(rawTarget);
        const rawTargetHex = encodeHex(decodeClientText(rawTarget));

        if (!targetProfile) {
            socket.write(`e1:SendMsgToHallClient,(${p60100},(${encodeInt(60114)},'${rawTargetHex}',${encodeInt(-1)}))\n`);
            return;
        }
        if (String(targetProfile.username).toLowerCase() === String(myProfile.username).toLowerCase()) {
            socket.write(`e1:SendMsgToHallClient,(${p60100},(${encodeInt(60114)},'${encodeHex(targetProfile.username)}',${encodeInt(3)}))\n`);
            return;
        }
        const alreadyFriend = (myProfile.friends || []).some(
            f => String(f).toLowerCase() === String(targetProfile.username).toLowerCase()
        );
        if (alreadyFriend) {
            socket.write(`e1:SendMsgToHallClient,(${p60100},(${encodeInt(60114)},'${encodeHex(targetProfile.username)}',${encodeInt(1)}))\n`);
            return;
        }

        if (!Array.isArray(targetProfile.friendRequests)) targetProfile.friendRequests = [];
        const alreadyReq = targetProfile.friendRequests.some(
            r => String(r.username || '').toLowerCase() === String(myProfile.username).toLowerCase()
        );
        if (!alreadyReq) {
            targetProfile.friendRequests.push({ cid: myProfile.cid, username: myProfile.username });
            dbPersistence.savePlayer(targetProfile.username, targetProfile);
        }

        // 4 = Pedido enviado com sucesso (Language 10128)
        socket.write(`e1:SendMsgToHallClient,(${p60100},(${encodeInt(60114)},'${encodeHex(targetProfile.username)}',${encodeInt(4)}))\n`);

        // Se o alvo estiver online no Lobby, envia notificação em tempo real (60102)
        const targetSocket = getLobbySocketByUsername(activeSockets, targetProfile.username);
        if (targetSocket) {
            targetSocket.write(`e1:SendMsgToHallClient,(${p60100},(${encodeInt(60102)},(${encodeInt(myProfile.cid)},'${encodeHex(myProfile.username)}')))\n`);
        }
        return;
    }

    // 60105: Aceitar (1) ou Recusar (0) pedido de amizade [60105, [reqCid, reqName], nState]
    if (subCmd === 60105) {
        const reqInfo = Array.isArray(parsed[1]) ? parsed[1] : [];
        const nState = Number(parsed[2] || 0);
        const reqProfile = findPlayerProfileByName(reqInfo[1]) || dbPersistence.getPlayerByCid(reqInfo[0]);

        if (reqProfile) {
            myProfile.friendRequests = (myProfile.friendRequests || []).filter(
                r => String(r.username || '').toLowerCase() !== String(reqProfile.username).toLowerCase()
            );

            if (nState === 1) {
                if (!myProfile.friends.some(f => String(f).toLowerCase() === String(reqProfile.username).toLowerCase())) {
                    myProfile.friends.push(reqProfile.username);
                }
                if (!reqProfile.friends.some(f => String(f).toLowerCase() === String(myProfile.username).toLowerCase())) {
                    reqProfile.friends.push(myProfile.username);
                }
                dbPersistence.savePlayer(myProfile.username, myProfile);
                dbPersistence.savePlayer(reqProfile.username, reqProfile);

                const reqTuple = buildFriendTuple(reqProfile, activeSockets);
                socket.write(`e1:SendMsgToHallClient,(${p60100},(${encodeInt(60109)},'${encodeHex(reqProfile.username)}',${reqTuple},${encodeInt(1)}))\n`);

                const reqSocket = getLobbySocketByUsername(activeSockets, reqProfile.username);
                if (reqSocket) {
                    const myTuple = buildFriendTuple(myProfile, activeSockets);
                    reqSocket.write(`e1:SendMsgToHallClient,(${p60100},(${encodeInt(60109)},'${encodeHex(myProfile.username)}',${myTuple},${encodeInt(1)}))\n`);
                }
            } else {
                dbPersistence.savePlayer(myProfile.username, myProfile);
            }
        }
        return;
    }

    // 60103: Remover amigo [60103, targetNameEncoded]
    if (subCmd === 60103) {
        const targetProfile = findPlayerProfileByName(parsed[1]);
        if (!targetProfile) return;

        myProfile.friends = (myProfile.friends || []).filter(
            f => String(f).toLowerCase() !== String(targetProfile.username).toLowerCase()
        );
        targetProfile.friends = (targetProfile.friends || []).filter(
            f => String(f).toLowerCase() !== String(myProfile.username).toLowerCase()
        );
        dbPersistence.savePlayer(myProfile.username, myProfile);
        dbPersistence.savePlayer(targetProfile.username, targetProfile);

        const targetTuple = buildFriendTuple(targetProfile, activeSockets);
        socket.write(`e1:SendMsgToHallClient,(${p60100},(${encodeInt(60109)},'${encodeHex(targetProfile.username)}',${targetTuple},0))\n`);

        const targetSocket = getLobbySocketByUsername(activeSockets, targetProfile.username);
        if (targetSocket) {
            const myTuple = buildFriendTuple(myProfile, activeSockets);
            targetSocket.write(`e1:SendMsgToHallClient,(${p60100},(${encodeInt(60109)},'${encodeHex(myProfile.username)}',${myTuple},0))\n`);
        }
    }
}

// ============================================================================
// 3. ESQUADRÃO / PARTY (Opcode 60000 - TeamManager.cs)
// ============================================================================
function buildTeamMemberUserData(profile, isCap) {
    const nameHex = encodeHex(profile.username || 'Jogador');
    const lvl = encodeInt(profile.level || 1);
    const cid = encodeInt(profile.cid || 1);
    const face = encodeInt(profile.face || 2009);
    const capEnc = encodeInt(isCap ? 1 : 0);
    const one = encodeInt(1);
    const zero = encodeInt(0);
    const h1015 = encodeInt(1015);
    return `('${nameHex}',${lvl},${one},${one},${one},${one},${h1015},${cid},${one},${zero},${face},'Local',${zero},(${h1015},${one},${one},${one},${one}),${cid},${zero},${zero},${capEnc},(0,0,0,0,'${nameHex}',0,'',0),())`;
}

function buildTeamDataArrayStr(team) {
    const pairs = [];
    for (const memberName of team.members) {
        const prof = findPlayerProfileByName(memberName);
        if (!prof) continue;
        const isCap = String(prof.username).toLowerCase() === String(team.captainName).toLowerCase();
        pairs.push(`'${encodeHex(prof.username)}',${buildTeamMemberUserData(prof, isCap)}`);
    }
    return `(${pairs.join(',')})`;
}

function getTeamForPlayer(username) {
    if (!username) return null;
    const tId = playerTeamMap.get(String(username).trim().toLowerCase());
    if (!tId) return null;
    return activeTeams.get(tId) || null;
}

function removePlayerFromTeam(username, activeSockets) {
    const team = getTeamForPlayer(username);
    if (!team) return;
    const clean = String(username).trim().toLowerCase();
    playerTeamMap.delete(clean);

    const removedProf = findPlayerProfileByName(username);
    const removedNameHex = encodeHex((removedProf && removedProf.username) || username);

    team.members = team.members.filter(m => String(m).toLowerCase() !== clean);
    const p60000 = encodeInt(60000);

    if (team.members.length === 0) {
        activeTeams.delete(team.teamId);
        return;
    }

    if (String(team.captainName).toLowerCase() === clean) {
        team.captainName = team.members[0];
    }

    const teamDataStr = buildTeamDataArrayStr(team);
    for (const memberName of team.members) {
        const mProf = findPlayerProfileByName(memberName);
        const mSocket = getLobbySocketByUsername(activeSockets, memberName);
        if (mSocket && mProf) {
            mSocket.write(`e1:SendMsgToHallClient,(${p60000},(${encodeInt(60003)},('${removedNameHex}',${teamDataStr},${encodeInt(mProf.cid)})))\n`);
        }
    }
}

function handleTeamCommand(socket, rawArgs, activeSockets) {
    const parsed = utils.parseArrayList(rawArgs);
    const subCmd = Number(parsed[0] || 0);
    const p60000 = encodeInt(60000);
    const myProfile = findPlayerProfileByName(socket.playerName);
    if (!myProfile) return;

    // 60005: Convidar jogador para o Esquadrão [60005, targetNameEncoded]
    if (subCmd === 60005) {
        const targetProfile = findPlayerProfileByName(parsed[1]);
        if (!targetProfile) {
            socket.write(`e1:SendMsgToHallClient,(${p60000},(${encodeInt(60015)},${encodeInt(1)}))\n`);
            return;
        }
        const targetSocket = getLobbySocketByUsername(activeSockets, targetProfile.username);
        if (!targetSocket) {
            socket.write(`e1:SendMsgToHallClient,(${p60000},(${encodeInt(60015)},${encodeInt(1)}))\n`);
            return;
        }
        if (getTeamForPlayer(targetProfile.username)) {
            socket.write(`e1:SendMsgToHallClient,(${p60000},(${encodeInt(60015)},${encodeInt(3)}))\n`);
            return;
        }

        let team = getTeamForPlayer(myProfile.username);
        if (!team) {
            const teamId = nextTeamId++;
            team = {
                teamId,
                captainName: myProfile.username,
                members: [myProfile.username]
            };
            activeTeams.set(teamId, team);
            playerTeamMap.set(myProfile.username.toLowerCase(), teamId);
            socket.write(`e1:SendMsgToHallClient,(${p60000},(${encodeInt(60001)},${encodeInt(teamId)},${encodeInt(1)}))\n`);
        }

        if (team.members.length >= 5) {
            socket.write(`e1:SendMsgToHallClient,(${p60000},(${encodeInt(60015)},${encodeInt(6)}))\n`);
            return;
        }

        // Notifica quem convidou que o convite foi enviado (8 = convite enviado)
        socket.write(`e1:SendMsgToHallClient,(${p60000},(${encodeInt(60015)},${encodeInt(8)}))\n`);
        // Envia pop-up de convite para o convidado (60005)
        targetSocket.write(`e1:SendMsgToHallClient,(${p60000},(${encodeInt(60005)},('${encodeHex(myProfile.username)}',${encodeInt(team.teamId)})))\n`);
        return;
    }

    // 60002: Aceitar convite de Esquadrão [60002, [inviterNameHex, teamId], 0]
    if (subCmd === 60002) {
        const invInfo = Array.isArray(parsed[1]) ? parsed[1] : [];
        const teamId = Number(invInfo[1] || 0);
        const team = activeTeams.get(teamId);
        if (!team) {
            socket.write(`e1:SendMsgToHallClient,(${p60000},(${encodeInt(60015)},${encodeInt(5)}))\n`);
            return;
        }
        if (team.members.length >= 5) {
            socket.write(`e1:SendMsgToHallClient,(${p60000},(${encodeInt(60015)},${encodeInt(6)}))\n`);
            return;
        }

        removePlayerFromTeam(myProfile.username, activeSockets);
        if (!team.members.some(m => m.toLowerCase() === myProfile.username.toLowerCase())) {
            team.members.push(myProfile.username);
        }
        playerTeamMap.set(myProfile.username.toLowerCase(), team.teamId);

        const teamDataStr = buildTeamDataArrayStr(team);
        const joinerHex = encodeHex(myProfile.username);
        for (const memberName of team.members) {
            const mProf = findPlayerProfileByName(memberName);
            const mSocket = getLobbySocketByUsername(activeSockets, memberName);
            if (mSocket && mProf) {
                mSocket.write(`e1:SendMsgToHallClient,(${p60000},(${encodeInt(60002)},('${joinerHex}',${teamDataStr},${encodeInt(team.teamId)},${encodeInt(mProf.cid)})))\n`);
            }
        }
        return;
    }

    // 60006: Recusar convite [60006, [inviterNameHex, teamId]]
    if (subCmd === 60006) {
        const invInfo = Array.isArray(parsed[1]) ? parsed[1] : [];
        const inviterProf = findPlayerProfileByName(invInfo[0]);
        if (inviterProf) {
            const invSocket = getLobbySocketByUsername(activeSockets, inviterProf.username);
            if (invSocket) {
                invSocket.write(`e1:SendMsgToHallClient,(${p60000},(${encodeInt(60006)},'${encodeHex(myProfile.username)}'))\n`);
            }
        }
        return;
    }

    // 60003 / 60025: Sair do Esquadrão
    if (subCmd === 60003 || subCmd === 60025) {
        const team = getTeamForPlayer(myProfile.username);
        if (!team) return;
        removePlayerFromTeam(myProfile.username, activeSockets);
        socket.write(`e1:SendMsgToHallClient,(${p60000},(${encodeInt(60003)},('${encodeHex(myProfile.username)}',(),${encodeInt(myProfile.cid)})))\n`);
        return;
    }

    // 60009: Expulsar membro [60009, targetNameEncoded]
    if (subCmd === 60009) {
        const team = getTeamForPlayer(myProfile.username);
        if (!team || String(team.captainName).toLowerCase() !== String(myProfile.username).toLowerCase()) return;
        const targetProf = findPlayerProfileByName(parsed[1]);
        if (!targetProf) return;

        removePlayerFromTeam(targetProf.username, activeSockets);
        const targetSocket = getLobbySocketByUsername(activeSockets, targetProf.username);
        if (targetSocket) {
            targetSocket.write(`e1:SendMsgToHallClient,(${p60000},(${encodeInt(60003)},('${encodeHex(targetProf.username)}',(),${encodeInt(targetProf.cid)})))\n`);
        }
        return;
    }

    // 60004: Passar liderança [60004, targetNameEncoded]
    if (subCmd === 60004) {
        const team = getTeamForPlayer(myProfile.username);
        if (!team || String(team.captainName).toLowerCase() !== String(myProfile.username).toLowerCase()) return;
        const targetProf = findPlayerProfileByName(parsed[1]);
        if (!targetProf) return;

        team.captainName = targetProf.username;
        const teamDataStr = buildTeamDataArrayStr(team);
        for (const memberName of team.members) {
            const mProf = findPlayerProfileByName(memberName);
            const mSocket = getLobbySocketByUsername(activeSockets, memberName);
            if (mSocket && mProf) {
                mSocket.write(`e1:SendMsgToHallClient,(${p60000},(${encodeInt(60004)},('${encodeHex(targetProf.username)}',${teamDataStr},${encodeInt(mProf.cid)})))\n`);
            }
        }
        return;
    }

    // 60010: Obter dados do Esquadrão atual
    if (subCmd === 60010) {
        const team = getTeamForPlayer(myProfile.username);
        if (!team) return;
        const teamDataStr = buildTeamDataArrayStr(team);
        const isCap = String(team.captainName).toLowerCase() === String(myProfile.username).toLowerCase();
        socket.write(`e1:SendMsgToHallClient,(${p60000},(${encodeInt(60010)},${teamDataStr},${encodeInt(isCap ? 1 : 0)}))\n`);
    }
}

// ============================================================================
// 4. CLÃS / GUILDAS (Opcode 91000 - GuildManager.cs / OneGuildCon.cs)
// ============================================================================
function getGuildMaxPerson(lv) {
    return 50 + Math.max(0, (Number(lv || 1) - 1)) * 10;
}

function buildGuildLoginPacket(username) {
    const profile = findPlayerProfileByName(username);
    const p91000 = encodeInt(91000);
    const s91020 = encodeInt(91020);
    if (!profile || !profile.guildId) {
        return `e1:SendMsgToHallClient,(${p91000},(${s91020},(0,'',0,0,0,'','')))\n`;
    }
    const store = dbPersistence.loadGuilds();
    const guild = store.guilds[String(profile.guildId)];
    if (!guild) {
        profile.guildId = 0;
        profile.guildRank = 0;
        dbPersistence.savePlayer(profile.username, profile);
        return `e1:SendMsgToHallClient,(${p91000},(${s91020},(0,'',0,0,0,'','')))\n`;
    }
    const memberEntry = (guild.members || []).find(m => Number(m.cid) === Number(profile.cid));
    const zhiWei = memberEntry ? Number(memberEntry.rank || 1) : (profile.guildRank || 1);
    return `e1:SendMsgToHallClient,(${p91000},(${s91020},(${encodeInt(guild.id)},'${encodeHex(guild.name)}',${encodeInt(zhiWei)},${encodeInt(guild.level || 1)},0,'','')))\n`;
}

function buildOneGuildSummaryTuple(guild) {
    const gId = encodeInt(guild.id);
    const nameHex = encodeHex(guild.name || 'Cla');
    const leaderHex = encodeHex(guild.leaderName || 'Lider');
    const lv = encodeInt(guild.level || 1);
    const curPerson = encodeInt((guild.members && guild.members.length) || 1);
    const maxPerson = encodeInt(getGuildMaxPerson(guild.level || 1));
    const autoJoin = encodeInt(guild.autoJoin ? 1 : 0);
    const exp = encodeInt(guild.exp || 100);
    const badge = encodeInt(guild.badge || 1001);
    const noticeHex = encodeHex(guild.notice || 'Bem-vindo ao Cla!');
    return `(${gId},'${nameHex}','${leaderHex}',${lv},${curPerson},${maxPerson},${autoJoin},${exp},${badge},'${noticeHex}')`;
}

function buildGuildMemberTuple(member, activeSockets) {
    const prof = dbPersistence.getPlayerByCid(member.cid) || findPlayerProfileByName(member.username) || {
        cid: member.cid,
        username: member.username || 'Membro',
        level: 1,
        face: 2009,
        skyTier: 1,
        lastLogin: new Date().toISOString()
    };
    const cid = encodeInt(prof.cid);
    const nameHex = encodeHex(prof.username);
    const rank = encodeInt(member.rank || 1);
    const lv = encodeInt(prof.level || 1);
    const exp = encodeInt(member.contribution || 100);
    const sky = encodeInt(prof.skyTier || 1);
    const state = encodeInt(getPlayerOnlineState(activeSockets, prof.username));
    const lastDate = formatDateForClient(prof.lastLogin);
    const face = encodeInt(prof.face || 2009);
    return `(${cid},'${nameHex}',${rank},${lv},${exp},${sky},${state},'${lastDate}',${face},0,0)`;
}

function sendGuildFullData(socket, guild, activeSockets) {
    const p91000 = encodeInt(91000);
    const s91018 = encodeInt(91018);
    const gId = encodeInt(guild.id);
    const nameHex = encodeHex(guild.name);
    const leaderHex = encodeHex(guild.leaderName);
    const lv = encodeInt(guild.level || 1);
    const curPerson = encodeInt((guild.members && guild.members.length) || 1);
    const maxPerson = encodeInt(getGuildMaxPerson(guild.level || 1));
    const autoJoin = encodeInt(guild.autoJoin ? 1 : 0);
    const exp = encodeInt(guild.exp || 100);
    const funds = encodeInt(guild.funds || 1000);
    const badge = encodeInt(guild.badge || 1001);
    const noticeHex = encodeHex(guild.notice || 'Bem-vindo ao Cla!');

    const baseInfo = `(${gId},'${nameHex}','${leaderHex}',${lv},${curPerson},${maxPerson},${autoJoin},${exp},${funds},${badge},'${noticeHex}',0)`;
    const membersStr = (guild.members || []).map(m => buildGuildMemberTuple(m, activeSockets)).join(',');
    const reqCount = encodeInt((guild.requests && guild.requests.length) || 0);

    socket.write(`e1:SendMsgToHallClient,(${p91000},(${s91018},(${baseInfo},(${membersStr}),${reqCount})))\n`);
}

function handleGuildCommand(socket, rawArgs, activeSockets) {
    const parsed = utils.parseArrayList(rawArgs);
    const subCmd = Number(parsed[0] || 0);
    const p91000 = encodeInt(91000);
    const myProfile = findPlayerProfileByName(socket.playerName);
    if (!myProfile) return;

    const store = dbPersistence.loadGuilds();

    // 91013: Listar Clãs [91013, nPage, bAutoJoin]
    if (subCmd === 91013) {
        const nPage = Math.max(1, Number(parsed[1] || 1));
        const bAutoJoin = Number(parsed[2] || 0);
        let list = Object.values(store.guilds || {});
        if (bAutoJoin === 1) {
            list = list.filter(g => g.autoJoin);
        }
        list.sort((a, b) => (b.level || 1) - (a.level || 1));

        const pageSize = 6;
        const maxPages = Math.max(1, Math.ceil(list.length / pageSize));
        const safePage = Math.min(nPage, maxPages);
        const slice = list.slice((safePage - 1) * pageSize, safePage * pageSize);
        const itemsStr = slice.map(buildOneGuildSummaryTuple).join(',');

        socket.write(`e1:SendMsgToHallClient,(${p91000},(${encodeInt(91013)},(${encodeInt(safePage)},${encodeInt(maxPages)},(${itemsStr}))))\n`);
        return;
    }

    // 91031: Buscar Clã por nome [91031, searchNameHex]
    if (subCmd === 91031) {
        const query = decodeClientText(parsed[1]).toLowerCase();
        const found = Object.values(store.guilds || {}).find(
            g => String(g.name || '').toLowerCase().includes(query)
        );
        if (!found) {
            socket.write(`e1:SendMsgToHallClient,(${p91000},(${encodeInt(91031)},(0,0,())))\n`);
            return;
        }
        socket.write(`e1:SendMsgToHallClient,(${p91000},(${encodeInt(91031)},(${encodeInt(1)},${encodeInt(1)},(${buildOneGuildSummaryTuple(found)}))))\n`);
        return;
    }

    // 91001: Criar Clã [91001, nameHex, badgeId, noticeHex, payType]
    if (subCmd === 91001) {
        const guildName = decodeClientText(parsed[1]).trim() || `Cla_${myProfile.username}`;
        const badgeId = Number(parsed[2] || 1001);
        const notice = decodeClientText(parsed[3]).trim() || 'Bem-vindo ao Cla!';

        const nameExists = Object.values(store.guilds || {}).some(
            g => String(g.name || '').toLowerCase() === guildName.toLowerCase()
        );
        if (nameExists) {
            socket.write(`e1:SendMsgToHallClient,(${p91000},(${encodeInt(91001)},(0,${encodeInt(-3)})))\n`);
            return;
        }

        const newId = store.nextGuildId || 1001;
        store.nextGuildId = newId + 1;
        const newGuild = {
            id: newId,
            name: guildName,
            badge: badgeId,
            notice: notice,
            level: 1,
            autoJoin: 1,
            exp: 100,
            funds: 1000,
            leaderCid: myProfile.cid,
            leaderName: myProfile.username,
            members: [
                { cid: myProfile.cid, username: myProfile.username, rank: 4, contribution: 100 }
            ],
            requests: []
        };
        store.guilds[String(newId)] = newGuild;
        dbPersistence.saveGuilds(store);

        myProfile.guildId = newId;
        myProfile.guildRank = 4; // 4 = Líder
        dbPersistence.savePlayer(myProfile.username, myProfile);

        socket.write(`e1:SendMsgToHallClient,(${p91000},(${encodeInt(91001)},(${encodeInt(1)},${encodeInt(newId)},'${encodeHex(guildName)}',${encodeInt(4)})))\n`);
        sendGuildFullData(socket, newGuild, activeSockets);
        return;
    }

    // 91018: Abrir tela do meu Clã [91018]
    if (subCmd === 91018) {
        const guild = store.guilds[String(myProfile.guildId)];
        if (!guild) return;
        sendGuildFullData(socket, guild, activeSockets);
        return;
    }

    // 91003: Solicitar entrada / Entrar em Clã [91003, guildId]
    if (subCmd === 91003) {
        const targetGuildId = Number(parsed[1] || 0);
        const guild = store.guilds[String(targetGuildId)];
        if (!guild) {
            socket.write(`e1:SendMsgToHallClient,(${p91000},(${encodeInt(91003)},(0,${encodeInt(-1)})))\n`);
            return;
        }
        if (myProfile.guildId && store.guilds[String(myProfile.guildId)]) {
            socket.write(`e1:SendMsgToHallClient,(${p91000},(${encodeInt(91003)},(0,${encodeInt(-3)})))\n`);
            return;
        }

        if (guild.autoJoin) {
            guild.members.push({ cid: myProfile.cid, username: myProfile.username, rank: 1, contribution: 0 });
            dbPersistence.saveGuilds(store);

            myProfile.guildId = guild.id;
            myProfile.guildRank = 1;
            dbPersistence.savePlayer(myProfile.username, myProfile);

            socket.write(`e1:SendMsgToHallClient,(${p91000},(${encodeInt(91003)},(${encodeInt(1)},${encodeInt(1)},${encodeInt(guild.id)},'${encodeHex(guild.name)}',${encodeInt(1)})))\n`);
            sendGuildFullData(socket, guild, activeSockets);
        } else {
            if (!Array.isArray(guild.requests)) guild.requests = [];
            if (!guild.requests.some(r => Number(r.cid) === Number(myProfile.cid))) {
                guild.requests.push({
                    cid: myProfile.cid,
                    username: myProfile.username,
                    date: utils.Fun.GetServerDate()
                });
                dbPersistence.saveGuilds(store);
            }
            socket.write(`e1:SendMsgToHallClient,(${p91000},(${encodeInt(91003)},(${encodeInt(1)},${encodeInt(2)},${encodeInt(guild.id)},'${encodeHex(guild.name)}',0)))\n`);
        }
        return;
    }

    // 91004: Sair do Clã [91004]
    if (subCmd === 91004) {
        const guild = store.guilds[String(myProfile.guildId)];
        if (!guild) return;

        guild.members = (guild.members || []).filter(m => Number(m.cid) !== Number(myProfile.cid));
        if (guild.members.length === 0) {
            delete store.guilds[String(guild.id)];
        } else if (Number(guild.leaderCid) === Number(myProfile.cid)) {
            guild.members[0].rank = 4;
            guild.leaderCid = guild.members[0].cid;
            guild.leaderName = guild.members[0].username;
        }
        dbPersistence.saveGuilds(store);

        myProfile.guildId = 0;
        myProfile.guildRank = 0;
        dbPersistence.savePlayer(myProfile.username, myProfile);

        socket.write(`e1:SendMsgToHallClient,(${p91000},(${encodeInt(91004)},(${encodeInt(1)},${encodeInt(myProfile.cid)})))\n`);
        return;
    }

    // 91019: Salvar aviso do Clã [91019, noticeHex]
    if (subCmd === 91019) {
        const guild = store.guilds[String(myProfile.guildId)];
        if (!guild) return;
        const newNotice = decodeClientText(parsed[1]);
        guild.notice = newNotice;
        dbPersistence.saveGuilds(store);
        socket.write(`e1:SendMsgToHallClient,(${p91000},(${encodeInt(91019)},(${encodeInt(1)},'${encodeHex(newNotice)}')))\n`);
        return;
    }

    // 91022: Alterar entrada automática [91022, bAuto]
    if (subCmd === 91022) {
        const guild = store.guilds[String(myProfile.guildId)];
        if (!guild) return;
        guild.autoJoin = Number(parsed[1] || 0) === 1 ? 1 : 0;
        dbPersistence.saveGuilds(store);
        socket.write(`e1:SendMsgToHallClient,(${p91000},(${encodeInt(91022)},(${encodeInt(1)},${encodeInt(guild.autoJoin)})))\n`);
        return;
    }

    // 91006: Subir nível do Clã [91006]
    if (subCmd === 91006) {
        const guild = store.guilds[String(myProfile.guildId)];
        if (!guild) return;
        guild.level = Math.min(10, (guild.level || 1) + 1);
        dbPersistence.saveGuilds(store);
        socket.write(`e1:SendMsgToHallClient,(${p91000},(${encodeInt(91006)},(${encodeInt(1)},${encodeInt(guild.level)},${encodeInt(guild.exp || 100)},${encodeInt(guild.funds || 1000)},${encodeInt(getGuildMaxPerson(guild.level))})))\n`);
        return;
    }

    // 91017: Listar pedidos de entrada no Clã [91017, nPage]
    if (subCmd === 91017) {
        const guild = store.guilds[String(myProfile.guildId)];
        if (!guild) return;
        const reqs = guild.requests || [];
        const itemsStr = reqs.map(r => {
            const p = dbPersistence.getPlayerByCid(r.cid) || findPlayerProfileByName(r.username) || { cid: r.cid, username: r.username, level: 1, skyTier: 1, face: 2009 };
            return `(${encodeInt(p.cid)},'${encodeHex(p.username)}',${encodeInt(p.level || 1)},${encodeInt(p.skyTier || 1)},'${r.date || utils.Fun.GetServerDate()}',${encodeInt(p.face || 2009)})`;
        }).join(',');
        socket.write(`e1:SendMsgToHallClient,(${p91000},(${encodeInt(91017)},(${encodeInt(1)},${encodeInt(1)},(${itemsStr}))))\n`);
        return;
    }

    // 91015: Aceitar pedido de entrada [91015, targetCid]
    if (subCmd === 91015) {
        const guild = store.guilds[String(myProfile.guildId)];
        if (!guild) return;
        const targetCid = Number(parsed[1] || 0);
        const reqIdx = (guild.requests || []).findIndex(r => Number(r.cid) === targetCid);
        if (reqIdx < 0) return;
        const req = guild.requests.splice(reqIdx, 1)[0];
        const targetProf = dbPersistence.getPlayerByCid(targetCid) || findPlayerProfileByName(req.username);
        if (targetProf) {
            guild.members.push({ cid: targetProf.cid, username: targetProf.username, rank: 1, contribution: 0 });
            targetProf.guildId = guild.id;
            targetProf.guildRank = 1;
            dbPersistence.savePlayer(targetProf.username, targetProf);
            dbPersistence.saveGuilds(store);

            const memberTuple = buildGuildMemberTuple({ cid: targetProf.cid, username: targetProf.username, rank: 1, contribution: 0 }, activeSockets);
            socket.write(`e1:SendMsgToHallClient,(${p91000},(${encodeInt(91015)},(${encodeInt(1)},${memberTuple})))\n`);
        }
        return;
    }

    // 91016: Recusar pedido de entrada [91016, targetCid]
    if (subCmd === 91016) {
        const guild = store.guilds[String(myProfile.guildId)];
        if (!guild) return;
        const targetCid = Number(parsed[1] || 0);
        guild.requests = (guild.requests || []).filter(r => Number(r.cid) !== targetCid);
        dbPersistence.saveGuilds(store);
        socket.write(`e1:SendMsgToHallClient,(${p91000},(${encodeInt(91016)},(${encodeInt(1)},${encodeInt(targetCid)})))\n`);
        return;
    }

    // 91005: Expulsar membro do Clã [91005, targetCid]
    if (subCmd === 91005) {
        const guild = store.guilds[String(myProfile.guildId)];
        if (!guild) return;
        const targetCid = Number(parsed[1] || 0);
        guild.members = (guild.members || []).filter(m => Number(m.cid) !== targetCid);
        dbPersistence.saveGuilds(store);
        const targetProf = dbPersistence.getPlayerByCid(targetCid);
        if (targetProf) {
            targetProf.guildId = 0;
            targetProf.guildRank = 0;
            dbPersistence.savePlayer(targetProf.username, targetProf);
        }
        socket.write(`e1:SendMsgToHallClient,(${p91000},(${encodeInt(91005)},(${encodeInt(1)},${encodeInt(targetCid)})))\n`);
        return;
    }

    // 91008: Alterar cargo do membro [91008, targetCid, newRank]
    if (subCmd === 91008) {
        const guild = store.guilds[String(myProfile.guildId)];
        if (!guild) return;
        const targetCid = Number(parsed[1] || 0);
        const newRank = Number(parsed[2] || 1);
        const member = (guild.members || []).find(m => Number(m.cid) === targetCid);
        if (!member) return;
        member.rank = newRank;
        dbPersistence.saveGuilds(store);
        const targetProf = dbPersistence.getPlayerByCid(targetCid);
        if (targetProf) {
            targetProf.guildRank = newRank;
            dbPersistence.savePlayer(targetProf.username, targetProf);
        }
        socket.write(`e1:SendMsgToHallClient,(${p91000},(${encodeInt(91008)},(${encodeInt(1)},${encodeInt(targetCid)},${encodeInt(newRank)})))\n`);
        return;
    }

    // 91007: Transferir liderança do Clã [91007, targetCid]
    if (subCmd === 91007) {
        const guild = store.guilds[String(myProfile.guildId)];
        if (!guild || Number(guild.leaderCid) !== Number(myProfile.cid)) return;
        const targetCid = Number(parsed[1] || 0);
        const newLeader = (guild.members || []).find(m => Number(m.cid) === targetCid);
        const oldLeader = (guild.members || []).find(m => Number(m.cid) === Number(myProfile.cid));
        if (!newLeader) return;
        if (oldLeader) oldLeader.rank = 1;
        newLeader.rank = 4;
        guild.leaderCid = newLeader.cid;
        guild.leaderName = newLeader.username;
        dbPersistence.saveGuilds(store);

        myProfile.guildRank = 1;
        dbPersistence.savePlayer(myProfile.username, myProfile);
        const targetProf = dbPersistence.getPlayerByCid(targetCid);
        if (targetProf) {
            targetProf.guildRank = 4;
            dbPersistence.savePlayer(targetProf.username, targetProf);
        }
        socket.write(`e1:SendMsgToHallClient,(${p91000},(${encodeInt(91007)},(${encodeInt(1)},${encodeInt(myProfile.cid)},${encodeInt(targetCid)})))\n`);
        return;
    }

    // 91036: Resgatar recompensa diária do Clã (Fuli)
    if (subCmd === 91036) {
        socket.write(`e1:SendMsgToHallClient,(${p91000},(${encodeInt(91036)},(${encodeInt(1)},${encodeInt(100)})))\n`);
    }
}

module.exports = {
    handleRankCommand,
    handleFriendCommand,
    handleTeamCommand,
    handleGuildCommand,
    buildFriendsInitPacket,
    buildGuildLoginPacket,
    notifyFriendsState,
    recordRecentPlayers,
    removePlayerFromTeam,
    getTeamForPlayer
};
