// Lógica de Partida, Buffs, Combate e HP do servidor Trigger Run

const state = require('./state');
const { encodeInt, decodeInt, encodeHex, decodeHex, Fun } = require('./utils');
const db = require('./database');
const dbPersistence = require('./dbPersistence');
const bots = require('../bots');
const logger = require('./logger');
const sceneScope = require('./sceneScope');
const lifecycle = require('./matchLifecycle');
const combat = require('./combatRules');
const heroCatalog = require('./heroCatalog');
const ffaMaps = require('./ffaMaps.json');

function getFreeForAllMatch(socket) {
    if (!combat.isFreeForAll(socket)) return null;
    return global.roomContainers?.get(Number(socket.currentRoomUID))?.ffaMatch || null;
}

function prepareFreeForAll(socket) {
    if (!combat.isFreeForAll(socket)) return null;
    const room = global.roomContainers?.get(Number(socket.currentRoomUID));
    if (!room || room.state !== 4) return null;
    let match = room.ffaMatch;
    if (!match) {
        match = room.ffaMatch = { roomUID: room.roomUID, mapId: Number(socket.targetMapId),
            state: 'fight', players: [], startedAt: Date.now(), startDate: Fun.GetServerDate(),
            duration: ffaMaps[socket.targetMapId].duration, targetScore: 20 };
        match.timer = setInterval(() => tickFreeForAll(match), 1000);
    }
    if (match.state !== 'fight') return null;
    if (!match.players.includes(socket)) {
        resetMatchStats(socket);
        socket.isDead = false;
        socket.ffaDeathHandled = false;
        match.players.push(socket);
    }
    return match;
}

function startFreeForAll(socket) {
    const match = prepareFreeForAll(socket);
    if (!match) return;
    const id = encodeInt(socket.playerIndex || 1);
    socket.write(`e${id}:SendMsgToClient,(${encodeInt(20014)},(${encodeInt(90005)},(0,'${match.startDate}')))\n`);
    socket.write(`e${id}:SendMsgToClient,(${encodeInt(19700)},(${encodeInt(19701)},(${encodeInt(match.duration)},${encodeInt(match.targetScore)})))\n`);
}

function freeForAllPlayers(match) {
    return match.players.filter(p => !p.destroyed && !p.sceneRetired && p.inMatch !== false &&
        Number(p.currentRoomUID) === Number(match.roomUID) && Number(p.targetMapId) === match.mapId);
}

function tickFreeForAll(match) {
    if (match.state !== 'fight') return;
    match.players = freeForAllPlayers(match);
    const winner = match.players.find(p => (p.matchKills || 0) >= match.targetScore);
    if (!match.players.length || winner || Date.now() - match.startedAt >= match.duration * 1000) {
        endFreeForAll(match, winner);
    }
}

function endFreeForAll(match, winner = null) {
    if (!match || match.state !== 'fight') return;
    match.state = 'ended';
    clearInterval(match.timer);
    match.timer = null;
    const players = freeForAllPlayers(match);
    // Same ordering as PersonListDataSort: kills descending, deaths ascending,
    // then stable player ID (sent as the third aryWarData field).
    const ranked = [...players].sort((a, b) => (b.matchKills || 0) - (a.matchKills || 0) ||
        (a.matchDeaths || 0) - (b.matchDeaths || 0) || Number(a.playerIndex) - Number(b.playerIndex));
    if (!winner && ranked.length && (ranked[0].matchKills || 0) > 0) winner = ranked[0];
    match.winner = winner;
    const room = global.roomContainers?.get(Number(match.roomUID));
    if (room?.ffaMatch === match) {
        room.state = 5;
        if (room.isMatchmaking) global.roomContainers.delete(Number(match.roomUID));
    }
    const current = new Map();
    // Includes participants still selecting/loading: no pending callback may
    // resurrect an ended room. Persistent lobby connections are matched by name.
    for (const p of state.activeSockets) {
        if (Number(p.currentRoomUID) !== Number(match.roomUID)) continue;
        if (p.socketType === 'SCENE') {
            sceneScope.invalidate(p);
            lifecycle.transition(p, 'ended', 'ffa-ended');
            current.set(p, sceneScope.capture(p));
        }
        p.inMatch = false;
        p.waitingForHeroLock = false;
        p.hasConfirmedHero = false;
        p.finalMapId = null;
        const session = state.playerSessions[p.playerName];
        if (session && Number(session.currentRoomUID) === Number(match.roomUID)) {
            session.inMatch = false;
            session.waitingForHeroLock = false;
            session.hasConfirmedHero = false;
            session.finalMapId = null;
        }
    }
    for (const p of players) {
        p.write(`e${encodeInt(p.playerIndex || 1)}:SendMsgToClient,(${encodeInt(19700)},(${encodeInt(19703)},(${winner ? 1 : 0},'${encodeHex(winner?.playerName || '')}')))\n`);
    }
    setTimeout(() => {
        const eligible = players.filter(p => current.get(p)?.());
        sendGameResult(eligible, winner ? 1 : 0, winner);
    }, 3000);
    setTimeout(() => {
        for (const [p, stillCurrent] of current) if (stillCurrent()) {
            p.write(`e${encodeInt(p.playerIndex || 1)}:SendMsgToClient,(${encodeInt(20001)},(0,'',0))\n`);
        }
    }, 35000);
}

function getPlayerSpawnCoordinates(socket) {
    const config = ffaMaps[socket.targetMapId];
    if (!config) return getSpawnCoordinates(socket.targetMapId, socket.force);
    const peers = state.activeSockets.filter(p => p !== socket && sceneScope.canReceive(socket, p) && !p.isDead);
    const points = config.spawns;
    // Randomize equal candidates, then maximize distance to the closest player.
    let best = points[Math.floor(Math.random() * points.length)];
    let distance = -1;
    for (const point of [best, ...points]) {
        const nearest = Math.min(...peers.map(p => Math.hypot(point.sx - p.lastX, point.sy - p.lastY, point.sz - p.lastZ)));
        if (nearest > distance) { best = point; distance = nearest; }
    }
    return { ...best };
}

// Envia HaveDamage para feedbacks ricos na UI/crosshair (hit markers / sons)
function sendEntityDamageUpdate(targetId, target, damage, attackerId, nPlace = 0, weaponId = 0, forceDir = null, forceMagnitude = 0, launcherNo = 0, attackName = 0) {
    const hpEnc = encodeInt(target.hp !== undefined ? target.hp : (target.maxHp || 2000));
    const maxHpEnc = encodeInt(target.maxHp || 2000);
    const shieldEnc = encodeInt(target.shield || 0);
    const maxShieldEnc = encodeInt(target.maxShield || 0);
    const dmgEnc = encodeInt(damage);
    const zero = encodeInt(0);

    let fx = 0, fy = 0, fz = 0;
    let forceVal = 0;
    if (forceDir && forceMagnitude > 0) {
        fx = Math.round(forceDir[0] * 100);
        fy = Math.round((forceDir[1] || 0) * 100);
        fz = Math.round(forceDir[2] * 100);
        forceVal = Math.round(forceMagnitude);
    }

    const fxEnc = encodeInt(fx);
    const fyEnc = encodeInt(fy);
    const fzEnc = encodeInt(fz);
    const forceEnc = encodeInt(forceVal);

    // Acumula Blood Anger no Khan ao tomar dano e envia no índice 15 de szDamage
    if (target && (target.currentHeroId === 1034 || Number(target.currentHeroId) === 1034)) {
        target.bloodAnger = Math.min(100, (target.bloodAnger || 0) + Math.max(1, Math.round(damage / 15)));
    }
    const baVal = (target && target.bloodAnger) ? target.bloodAnger : 0;
    const baEnc = encodeInt(baVal);
    // CWeaponControl usa shootname (1/2/3/4), não o ID do herói, para falas.
    const szDamageStr = `(${dmgEnc},(${fxEnc},${fyEnc},${fzEnc}),${forceEnc},${encodeInt(attackerId)},${encodeInt(targetId)},${encodeInt(nPlace)},${zero},${zero},${encodeInt(weaponId)},${zero},(${zero},${zero},${zero}),${zero},${encodeInt(1)},${encodeInt(attackName)},${encodeInt(launcherNo)},(${zero},${baEnc}))`;
    const szHpStr = `(${hpEnc},${maxHpEnc},${shieldEnc},${maxShieldEnc},${zero},${zero},${zero},(${dmgEnc},${zero},${zero},${zero}),${encodeInt(2)},${zero})`;

    const damagePacket = `e${encodeInt(targetId)}:HaveDamage,(${szDamageStr},${szHpStr})\n`;
    state.activeSockets.forEach(s => {
        if (sceneScope.canReceive(target, s) && !s.destroyed) {
            s.write(damagePacket);
        }
    });

    // Registra o último atacante e o timestamp do dano para atribuição de kills ambientais (quedas/abismo/espinhos)
    if (target && attackerId) {
        const attSocket = state.activeSockets.find(s => s.socketType === "SCENE" && !s.destroyed && (Number(s.playerId) === Number(attackerId) || Number(s.playerIndex) === Number(attackerId)));
        if (attSocket && !combat.sameSide(attSocket, target)) {
            target.lastAttackerSocket = attSocket;
            target.lastAttackerTime = Date.now();
            target.lastAttackerWeapon = weaponId;
            target.lastAttackerAttack = attackName;
            target.lastAttackerLauncher = launcherNo;
        } else if (state.activeBots && state.activeBots[attackerId]) {
            const botAtt = state.activeBots[attackerId];
            if (!combat.sameSide(botAtt, target)) {
                target.lastAttackerSocket = botAtt;
                target.lastAttackerTime = Date.now();
                target.lastAttackerWeapon = weaponId;
                target.lastAttackerAttack = attackName;
                target.lastAttackerLauncher = launcherNo;
            }
        }
    }
}

// Função para processar danos em NPCs
function handleNpcHurt(socket, npcId, damage, attackerId = 0, nPlace = 0, weaponId = 0) {
    const npc = state.spawnedNpcs[npcId];
    if (!npc) return;
    if (socket && !sceneScope.sameScene(socket, npc)) return;

    // 🪤 Se for a Trap do Johnny ativada (girando), é imune a danos durante seus 6s
    if ((npc.index === 1021 || String(npc.index) === "1021") && npc.isActivated) {
        console.log(`[🛡️ JOHNNY TRAP INVULNERABLE] NPC Trap ${npcId} está em rotação ativada (6s) e é imune a danos!`);
        return;
    }
    
    let place = nPlace;
    let shieldAbsorbed = 0;
    
    // Absorve com escudo se o NPC tiver escudo
    if (npc.shield && npc.shield > 0) {
        shieldAbsorbed = Math.min(npc.shield, damage);
        npc.shield -= shieldAbsorbed;
        damage -= shieldAbsorbed;
        place = 2; // Shield hit
    }
    
    if (damage > 0) {
        npc.hp = Math.max(0, npc.hp - damage);
    }
    
    console.log(`[🤖 NPC Hurt] NPC ${npcId} took ${damage} HP damage (${shieldAbsorbed} absorbed by shield). Current HP: ${npc.hp}/${npc.maxHp}`);
    
    if (attackerId > 0) {
        sendEntityDamageUpdate(npcId, npc, damage + shieldAbsorbed, attackerId, place, weaponId);
    } else {
        // Fallback DoT / SetHp
        const hpEnc = encodeInt(npc.hp);
        const maxHpEnc = encodeInt(npc.maxHp);
        const shieldEnc = encodeInt(npc.shield || 0);
        const maxShieldEnc = encodeInt(npc.maxShield || npc.shield || 0);
        const zero = encodeInt(0);
        const setHpPacket = `e${encodeInt(npcId)}:SetHp,((${hpEnc},${maxHpEnc},${shieldEnc},${maxShieldEnc},${zero},${zero},${zero},(),${zero},${zero}))\n`;
        state.activeSockets.forEach(s => {
            if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                s.write(setHpPacket);
            }
        });
    }
    
    if (npc.hp <= 0) {
        console.log(`[🤖 NPC DEAD] NPC ${npcId} morreu.`);
        
        // Se for dummy de treino, ressuscita após 3 segundos
        if (npc.isPracticeDummy) {
            setTimeout(() => {
                npc.hp = npc.maxHp || 1000;
                npc.shield = npc.maxShield || 0;
                console.log(`[🤖 NPC RESPAWN] NPC Dummy de Treino ${npcId} ressuscitou.`);
                sendEntityHpUpdate(null, npcId, npc, false);
            }, 3000);
        } else if (npc.isCrystal) {
            console.log(`[💥 PVE CRISTAL DESTRUÍDO] O Cristal foi destruído! Missão falhou.`);
            sendGlobalSystemChat("💥 [MODO AVENTURA] O CRISTAL FOI DESTRUÍDO! MISSÃO FALHOU!", npc.currentRoomUID);
            finishPveMatch(npc.pveMatch, false);
        } else {
            // Remove dos NPCs spawnados
            delete state.spawnedNpcs[npcId];
            const destroyPacket = `d${encodeInt(npcId)}\n`;
            state.activeSockets.forEach(s => {
                if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                    s.write(destroyPacket);
                }
            });
        }
    }
}

// Motor de buffs e debuffs
function applyBuffToEntity(socket, targetId, buffId, customDuration) {
    if (!socket || socket.destroyed) return;
    const playerId = socket.playerId || "1";
    const buff = db.buffsMap[buffId];
    if (!buff) {
        console.log(`[⚠️ BUFF ENGINE] Buff ${buffId} não encontrado no buffsMap.`);
        return;
    }

    const duration = customDuration !== undefined ? customDuration : (buff.time || 5000);
    const buffDateStr = Fun.GetServerDate();

    // Determina a entidade alvo (Jogador, Bot ou NPC)
    let target = null;
    let isPlayer = false;
    
    // Procura primeiro por jogador em activeSockets, depois em activeBots, depois em spawnedNpcs
    const foundPlayer = state.activeSockets.find(s => s.socketType === "SCENE" && !s.destroyed && (s.playerId === targetId || String(s.playerId) === String(targetId) || Number(s.playerIndex) === Number(targetId) || String(s.playerIndex) === String(targetId)));
    if (foundPlayer) {
        target = foundPlayer;
        isPlayer = true;
    } else if (state.activeBots && (state.activeBots[targetId] || state.activeBots[String(targetId)])) {
        target = state.activeBots[targetId] || state.activeBots[String(targetId)];
        isPlayer = true;
    } else if (state.spawnedNpcs[targetId] || state.spawnedNpcs[String(targetId)]) {
        target = state.spawnedNpcs[targetId] || state.spawnedNpcs[String(targetId)];
    }
    
    if (!target) {
        console.log(`[⚠️ BUFF ENGINE] Entidade alvo ${targetId} não encontrada.`);
        return;
    }
    if (socket && !sceneScope.sameScene(socket, target)) return;

    if (isPlayer && target.isDead) {
        console.log(`[🚫 BUFF ENGINE] Não é possível aplicar buff em jogador morto ${targetId}`);
        return;
    }

    const isDebuffOrCC = buff.isdebuff === 1 || buff.type === 11 || buff.type === 12 || buff.type === 14;
    if (isDebuffOrCC && isPlayer) {
        const activeBuffs = target.activeBuffs || {};
        const isImmuneToCC = (
            target.godMode ||
            target.isInvincible ||
            activeBuffs[1006] || // Valkyrie - Energy Shield
            activeBuffs[1039] || // Venus - Fortify / Personal Shield
            activeBuffs[1040] || // Venus - Mitigation / Shield
            activeBuffs[1047] || // 🦊 Venus - Ultimate Mass Shield (Fox Phantom / 4000 Shield)
            (target.shield && target.shield > 0) || // Qualquer escudo pessoal ativo
            target.isVenusUltActive ||
            activeBuffs[1025] || // Jack - Energy Shield
            activeBuffs[1008] || // Carmen - Unbeatable
            activeBuffs[1015] || // Brigit - Born from Fire
            activeBuffs[1045] || // Khan - Veteran Soldiers
            target.lokiShieldActive || // Loki - Shield Absorb
            target.isKhanUltActive
        );
        if (isImmuneToCC) {
            console.log(`[🛡️ CC/DEBUFF NEGADO] Alvo ${targetId} está com Escudo/Fortify/Imunidade ativa e ignorou o debuff/stun "${buff.name}" (${buffId})!`);
            return;
        }
    }


    target.activeBuffs = target.activeBuffs || {};

    const isStackingBuff = buff.overlaytype === 1 || buffId === 1056;
    if (target.activeBuffs[buffId]) {
        if (isStackingBuff) {
            const existing = target.activeBuffs[buffId];
            existing.stacks = Math.min(10, (existing.stacks || 1) + 1);
            if (existing.timeout) clearTimeout(existing.timeout);
            existing.timeout = setTimeout(() => {
                cleanupBuff(target, targetId, buffId, isPlayer);
            }, duration);
            console.log(`[🧪 BUFF STACK] Buff "${buff.name}" (${buffId}) acumulou para ${existing.stacks} stacks em ${isPlayer ? 'Jogador' : 'NPC'} ${targetId}`);
            
            const addBuffPacket = `e${encodeInt(targetId)}:AddBuff,(('${buffDateStr}',${encodeInt(buffId)},${encodeInt(playerId)}))\n`;
            state.activeSockets.forEach(s => {
                if (sceneScope.canReceive(target, s) && !s.destroyed) {
                    s.write(addBuffPacket);
                }
            });
            return;
        } else {
            cleanupBuff(target, targetId, buffId, isPlayer);
        }
    }

    // Envia o pacote de AddBuff para todos os clientes na cena
    const addBuffPacket = `e${encodeInt(targetId)}:AddBuff,(('${buffDateStr}',${encodeInt(buffId)},${encodeInt(playerId)}))\n`;
    state.activeSockets.forEach(s => {
        if (sceneScope.canReceive(target, s) && !s.destroyed) {
            s.write(addBuffPacket);
        }
    });
    console.log(`[🛡️ BUFF ENGINE] Buff "${buff.name}" (${buffId}) aplicado em ${isPlayer ? 'Jogador' : 'NPC'} ${targetId} | Caster: ${playerId} | Duração: ${duration}ms`);

    // Efeitos imediatos no Servidor
    if (buffId === 1043) {
        target.khanProtectorSocket = socket;
        target.khanProtectorId = playerId;
        console.log(`[🛡️ KHAN ASYLUM] Alvo ${targetId} protegido pelo Khan ${playerId}`);
    }

    if (buffId === 1015 && isPlayer && target.currentHeroId === 1023) {
        const targetIdEnc = encodeInt(Number(targetId));
        const enterCocoonPacket = `e${encodeInt(targetId)}:CA,((('SkillE',0,1),1,${targetIdEnc}))\n`;
        state.activeSockets.forEach(s => { if (sceneScope.canReceive(target, s) && !s.destroyed) s.write(enterCocoonPacket); });
        console.log(`[🛡️ COCOON ENTER] Brigit ${targetId} visual casulo ativado: ${enterCocoonPacket.trim()}`);
    }

    if (buff.type === 7) { // Unbeatable / Invencível
        target.isInvincible = true;
    } else if (buff.type === 5) { // Shield / Escudo de Energia
        target.shield = (target.shield || 0) + buff.value;
        target.maxShield = Math.max(target.maxShield || 0, target.shield);
        sendEntityHpUpdate(target, targetId, target, isPlayer);
    } else if (buff.type === 10) { // Invisibility / Invisibilidade
        target.isInvis = true;
    } else if (buff.type === 11) { // Freeze / Imprison (CC)
        target.isFrozen = true;
        console.log(`[❄️ CC STATE] Alvo ${targetId} está CONGELADO.`);
    } else if (buff.type === 12) { // Stun (CC)
        target.isStunned = true;
        console.log(`[💫 CC STATE] Alvo ${targetId} está ATORDOADO.`);
    }

    // Ticks contínuos (Cura ou Dano Periódico / DoT)
    let intervalId = null;
    if (buff.type === 1 && buff.interval > 0 && buff.value !== 0) {
        intervalId = setInterval(() => {
            if ((isPlayer && target.destroyed) || (target.hp !== undefined && target.hp <= 0) || (!isPlayer && !state.spawnedNpcs[targetId])) {
                clearInterval(intervalId);
                return;
            }
            if (buff.isdebuff === 1) { // Debuff (Dano sobre tempo - ex: veneno/fogo)
                if (target.godMode || target.isInvincible) return; // Invencível cancela o dano
                let stacks = (target.activeBuffs && target.activeBuffs[buffId] && target.activeBuffs[buffId].stacks) ? target.activeBuffs[buffId].stacks : 1;
                let dmg = Math.abs(buff.value) * stacks;
                
                // Se tiver escudo, absorve o DoT antes do HP!
                if (target.shield && target.shield > 0) {
                    const absorbed = Math.min(target.shield, dmg);
                    target.shield -= absorbed;
                    dmg -= absorbed;
                    sendEntityHpUpdate(target, targetId, target, isPlayer);
                }
                
                if (dmg > 0) {
                    if (isPlayer) {
                        target.hp = Math.max(0, (target.hp || target.maxHp || 2000) - dmg);
                        sendEntityHpUpdate(target, targetId, target, isPlayer);
                        if (target.hp <= 0) handlePlayerDeath(target);
                    } else {
                        handleNpcHurt(target, targetId, dmg);
                    }
                }
            } else { // Buff (Cura sobre tempo - ex: regeneração)
                const heal = buff.value;
                if (isPlayer) {
                    target.hp = Math.min(target.maxHp || 2000, (target.hp || target.maxHp || 2000) + heal);
                    sendEntityHpUpdate(target, targetId, target, isPlayer);
                } else {
                    target.hp = Math.min(target.maxHp || 500, target.hp + heal);
                    sendEntityHpUpdate(target, targetId, target, isPlayer);
                }
                if (socket && socket.socketType === "SCENE" && heal > 0) {
                    socket.matchHeal = (socket.matchHeal || 0) + heal;
                    sendTabScoreboard(socket);
                }
            }
        }, buff.interval);
    } else if (buff.type === 1 && (buff.interval === 0 || !buff.interval) && buff.value !== 0) {
        // Cura ou Dano instantâneo
        if (buff.isdebuff === 1) {
            if (!(target.godMode || target.isInvincible)) {
                let dmg = Math.abs(buff.value);
                if (target.shield && target.shield > 0) {
                    const absorbed = Math.min(target.shield, dmg);
                    target.shield -= absorbed;
                    dmg -= absorbed;
                    sendEntityHpUpdate(target, targetId, target, isPlayer);
                }
                if (dmg > 0) {
                    if (isPlayer) {
                        target.hp = Math.max(0, (target.hp || target.maxHp || 2000) - dmg);
                        sendEntityHpUpdate(target, targetId, target, isPlayer);
                        if (target.hp <= 0) handlePlayerDeath(target);
                    } else {
                        handleNpcHurt(target, targetId, dmg);
                    }
                }
            }
        } else {
            const heal = buff.value;
            if (isPlayer) {
                    target.hp = Math.min(target.maxHp || 2000, (target.hp || target.maxHp || 2000) + heal);
                    sendEntityHpUpdate(target, targetId, target, isPlayer);
                } else {
                    target.hp = Math.min(target.maxHp || 500, target.hp + heal);
                    sendEntityHpUpdate(target, targetId, target, isPlayer);
                }
                if (socket && socket.socketType === "SCENE" && heal > 0) {
                    socket.matchHeal = (socket.matchHeal || 0) + heal;
                    sendTabScoreboard(socket);
                }
        }
    }

    // Agenda a expiração e limpeza do Buff diretamente no alvo
    const timeout = setTimeout(() => {
        cleanupBuff(target, targetId, buffId, isPlayer);
    }, duration);

    target.activeBuffs[buffId] = { timeout, intervalId, casterId: playerId, stacks: 1 };
}

function cleanupBuff(targetSocketOrNpc, targetId, buffId, isPlayer) {
    let target = null;
    if (isPlayer) {
        if (targetSocketOrNpc && typeof targetSocketOrNpc === 'object' && targetSocketOrNpc.activeBuffs) {
            target = targetSocketOrNpc;
        } else {
            target = state.activeSockets.find(s => s.socketType === "SCENE" && !s.destroyed && (s.playerId === targetId || String(s.playerId) === String(targetId))) ||
                     (state.activeBots ? (state.activeBots[targetId] || state.activeBots[String(targetId)]) : null);
        }
    } else if (state.spawnedNpcs[targetId] || state.spawnedNpcs[String(targetId)]) {
        target = state.spawnedNpcs[targetId] || state.spawnedNpcs[String(targetId)];
    }
    if (!target || !target.activeBuffs || !target.activeBuffs[buffId]) return;

    const buff = db.buffsMap[buffId];
    if (buff) {
        if (buff.type === 7) {
            target.isInvincible = target.godMode || false;
        } else if (buff.type === 5) {
            target.shield = Math.max(0, (target.shield || 0) - buff.value);
            if (target.shield === 0) {
                target.maxShield = 0;
            }
            sendEntityHpUpdate(target, targetId, target, isPlayer);
        } else if (buff.type === 10) {
            target.isInvis = false;
        } else if (buff.type === 11) {
            target.isFrozen = false;
            console.log(`[❄️ CC STATE END] Alvo ${targetId} não está mais CONGELADO.`);
        } else if (buff.type === 12) {
            target.isStunned = false;
            console.log(`[💫 CC STATE END] Alvo ${targetId} não está mais ATORDOADO.`);
        }
    }
    if (buffId === 1043) {
        target.khanProtectorSocket = null;
        target.khanProtectorId = null;
        console.log(`[🛡️ KHAN ASYLUM END] Proteção do Khan finalizada para alvo ${targetId}`);
    }

    const casterId = target.activeBuffs[buffId].casterId || targetId;

    if (target.activeBuffs[buffId].timeout) {
        clearTimeout(target.activeBuffs[buffId].timeout);
    } else {
        clearTimeout(target.activeBuffs[buffId]);
    }
    if (target.activeBuffs[buffId].intervalId) {
        clearInterval(target.activeBuffs[buffId].intervalId);
    }
    delete target.activeBuffs[buffId];

    const delBuffPacket = `e${encodeInt(targetId)}:DeleteBuff,((${encodeInt(buffId)},${encodeInt(0)},${encodeInt(casterId)}))\n`;
    state.activeSockets.forEach(s => {
        if (sceneScope.canReceive(target, s) && !s.destroyed) {
            s.write(delBuffPacket);
        }
    });
    console.log(`[🛡️ BUFF ENGINE] Expira/Remove Buff ${buffId} de ${isPlayer ? 'Jogador' : 'NPC'} ${targetId}`);

    // Notifica reset de alvo único para Carmen (Heal 1021 / Beacon 1005|1010)
    if (buffId === 1021) {
        state.activeSockets.forEach(s => {
            if (s.carmenHealTargetId && String(s.carmenHealTargetId) === String(targetId)) {
                console.log(`[🔮 CARMEN CUT-HEAL] Resetando carmenHealTargetId da Carmen ${s.playerId} pois Buff 1021 foi removido do alvo ${targetId}`);
                s.carmenHealTargetId = null;
            }
        });
    }
    if (buffId === 1005 || buffId === 1010) {
        state.activeSockets.forEach(s => {
            if (s.carmenBeaconTargetId && String(s.carmenBeaconTargetId) === String(targetId)) {
                s.carmenBeaconTargetId = null;
            }
        });
    }

    // Se for o Buff de Casulo da Brigit expirando, garante reset visual
    if (buffId === 1015 && isPlayer) {
        target.isInvincible = false;
        target.godMode = false;
        if (target.currentHeroId === 1023) {
            const targetIdEnc = encodeInt(Number(targetId));
            const exitCocoonPacket = `e${encodeInt(targetId)}:CA,((('SkillE',0,0),1,${targetIdEnc}))\n`;
            state.activeSockets.forEach(s => {
                if (sceneScope.canReceive(target, s) && !s.destroyed) {
                    s.write(exitCocoonPacket);
                }
            });
        }
    }
}

function sendEntityHpUpdate(socket, targetId, target, isPlayer) {
    const hpEnc = encodeInt(target.hp !== undefined ? target.hp : (target.maxHp || 2000));
    const maxHpEnc = encodeInt(target.maxHp || 2000);
    const shieldEnc = encodeInt(target.shield || 0);
    const maxShieldEnc = encodeInt(target.maxShield !== undefined ? target.maxShield : (target.shield || 0));
    const zero = encodeInt(0);
    
    const setHpPacket = `e${encodeInt(targetId)}:SetHp,((${hpEnc},${maxHpEnc},${shieldEnc},${maxShieldEnc},${zero},${zero},${zero},(),${zero},${zero}))\n`;
    state.activeSockets.forEach(s => {
        if (sceneScope.canReceive(target, s) && !s.destroyed) {
            s.write(setHpPacket);
        }
    });
}

function sendSystemChat(socket, text) {
    if (!socket || socket.destroyed) return;
    const p1006 = encodeInt(1006);
    const pCid = encodeInt(1);
    const hexMsg = Buffer.from(text).toString('hex');
    const sysNetMsg = `(${encodeInt(8)},'','[mi]${hexMsg}[/mi]','',(),(),${pCid},('','',0),())`;
    socket.write(`e${encodeInt(socket.playerIndex || 1)}:SendMsgToClient,(${p1006},${sysNetMsg})\n`);
}

function sendGlobalSystemChat(text, roomUID = null) {
    state.activeSockets.forEach(s => {
        if (s.socketType === "SCENE" && !s.destroyed) {
            if (roomUID !== null && roomUID !== undefined && s.currentRoomUID !== roomUID) return;
            sendSystemChat(s, text);
        }
    });
}

function getSafeMapId(mapId) {
    const id = parseInt(mapId);
    return id || 5202;
}

function getSpawnCoordinates(mapId, force) {
    const defaultCoords = { sx: 5000, sy: 2010, sz: 2413 };
    let targetMapId = Number(mapId);
    if (!targetMapId) return defaultCoords;
    
    if (targetMapId === 5301) targetMapId = 5030;
    if (targetMapId === 5302) targetMapId = 5074;
    if (targetMapId === 5303) targetMapId = 5068;
    if (targetMapId === 5304) targetMapId = 5050;
    if (targetMapId === 5305) targetMapId = 5055;
    if ((targetMapId >= 5105 && targetMapId <= 5114) || targetMapId === 5310 || targetMapId === 5311) targetMapId = 5105;
    if (targetMapId >= 5115 && targetMapId <= 5126) targetMapId = 5115;
    
    const forceStr = force ? force.toString() : "1";
    
    const mapSpawns = {
      "5004": { "1": { x: 7362, y: 2091, z: 11101 }, "2": { x: 6334, y: 2091, z: 11101 } },
      "5010": { "1": { x: 12147, y: 2610, z: 19003 }, "2": { x: 12184, y: 2610, z: 27788 } },
      "5022": { "1": { x: 24800, y: 10000, z: 22700 }, "2": { x: 16350, y: 10000, z: 20850 } },
      "5024": { "1": { x: 14540, y: 10200, z: 10000 }, "2": { x: 5460, y: 10200, z: 10000 } },
      "5030": { "1": { x: 141190, y: 14695, z: 106980 }, "2": { x: 127840, y: 14695, z: 106960 } },
      "5045": { "1": { x: 19000, y: 10150, z: 24400 }, "2": { x: 19000, y: 10150, z: 18000 } },
      "5049": { "1": { x: 25700, y: 15300, z: 17800 }, "2": { x: 14800, y: 15300, z: 24200 } },
      "5050": { "1": { x: 58048, y: 18034, z: 45374 }, "2": { x: 42291, y: 18043, z: 45056 } },
      "5055": { "1": { x: 115688, y: 79010, z: 107410 }, "2": { x: 114998, y: 79010, z: 122992 } },
      "5056": { "1": { x: 10500, y: 2014, z: 12000 }, "2": { x: 19500, y: 2014, z: 12000 } },
      "5058": { "1": { x: 12147, y: 2610, z: 19003 }, "2": { x: 12184, y: 2610, z: 27788 } },
      "5059": { "1": { x: 9802, y: 3820, z: 4602 }, "2": { x: 1797, y: 3820, z: 5999 } },
      "5063": { "1": { x: 9600, y: 9911, z: 18050 }, "2": { x: 13750, y: 9400, z: 7000 } },
      "5068": { "1": { x: 6700, y: 970, z: 23550 }, "2": { x: 22800, y: 970, z: 23572 } },
      "5074": { "1": { x: 63829, y: 6892, z: 72949 }, "2": { x: 42280, y: 6892, z: 73318 } },
      "5099": { "1": { x: 500, y: 100, z: 500 }, "2": { x: 1000, y: 100, z: 1000 } },
      "5100": { "1": { x: 141190, y: 14695, z: 106980 }, "2": { x: 127840, y: 14695, z: 106960 } },
      "5102": { "1": { x: 6700, y: 970, z: 23550 }, "2": { x: 22800, y: 970, z: 23572 } },
      "5103": { "1": { x: 9802, y: 3820, z: 4602 }, "2": { x: 1797, y: 3820, z: 5999 } },
      "5104": { "1": { x: 12147, y: 2610, z: 19003 }, "2": { x: 12184, y: 2610, z: 27788 } },
      "5105": { "1": { x: 11779, y: 10766, z: 5534 }, "2": { x: 11410, y: 10766, z: 5534 } },
      "5115": { "1": { x: 36700, y: 3135, z: 5136 }, "2": { x: 36500, y: 3135, z: 5136 } },
      "5202": { "1": { x: 5000, y: 2010, z: 2413 }, "2": { x: 5000, y: 2010, z: 2413 } },
      "5204": { "1": { x: 9802, y: 3820, z: 4602 }, "2": { x: 1797, y: 3820, z: 5999 } },
      "5205": { "1": { x: 24900, y: 21150, z: 23200 }, "2": { x: 23400, y: 21150, z: 23200 } },
      "5208": { "1": { x: 16800, y: 5220, z: 9600 }, "2": { x: 3200, y: 5220, z: 9600 } },
      "5210": { "1": { x: 5400, y: 5100, z: 9000 }, "2": { x: 4600, y: 5100, z: 1000 } },
      "5212": { "1": { x: 5400, y: 5000, z: 9300 }, "2": { x: 4600, y: 5000, z: 700 } },
      "5214": { "1": { x: 10400, y: 10500, z: 14600 }, "2": { x: 9600, y: 10500, z: 5400 } },
      "5215": { "1": { x: -38620, y: 2500, z: -7410 }, "2": { x: 9790, y: 2500, z: 42710 } },
      "5216": { "1": { x: 15987, y: 2815, z: 19113 }, "2": { x: 20098, y: 2815, z: 14893 } },
      "5217": { "1": { x: 21300, y: 10300, z: 29100 }, "2": { x: 19300, y: 10300, z: 29100 } },
      "5218": { "1": { x: 21300, y: 10300, z: 29100 }, "2": { x: 19300, y: 10300, z: 29100 } },
      "5260": { "1": { x: 16700, y: 5020, z: 4500 }, "2": { x: 3300, y: 5020, z: 4500 } }
    };
    
    const spawns = mapSpawns[targetMapId.toString()];
    if (spawns) {
        let spawn = spawns[forceStr];
        if (!spawn) {
            spawn = spawns["1"] || spawns["2"];
        }
        if (spawn) {
            return { sx: spawn.x, sy: spawn.y, sz: spawn.z };
        }
    }
    return defaultCoords;
}

function resetMatchStats(socket) {
    socket.matchKills    = 0;
    socket.matchDeaths   = 0;
    socket.matchAssists  = 0;
    socket.matchDamage   = 0;
    socket.matchHeal     = 0;
    socket.matchStartTime = Date.now();
}

function handleEntityDeath(target, targetId, attackerSocket, attackerId, weaponId = 0) {
    if (!target) return;
    handlePlayerDeath(target, attackerSocket);
}

function handlePlayerDeath(socket, attackerSocket = null) {
    if (!socket || socket.destroyed || socket.sceneRetired) return;
    if (combat.isFreeForAll(socket) && (socket.ffaDeathHandled || !socket.isCombatReady || getFreeForAllMatch(socket)?.state !== 'fight')) return;
    
    // Auto-revive para bonecos de treino
    if (socket.isPracticeDummy) {
        console.log(`[🎯 DUMMY MORREU] Boneco de treino #${socket.playerIndex} (${socket.playerName}) atingiu 0 HP. Auto-ressuscitando em 1.5s...`);
        socket.isDead = true;
        socket.hp = 0;
        sendEntityHpUpdate(socket, socket.playerIndex, socket, true);

        setTimeout(() => {
            if (state.activeBots && state.activeBots[socket.playerIndex]) {
                socket.isDead = false;
                socket.hp = socket.maxHp || 2000;
                socket.shield = socket.maxShield || 0;
                socket.lastX = socket.spawnX;
                socket.lastY = socket.spawnY;
                socket.lastZ = socket.spawnZ;
                sendEntityHpUpdate(socket, socket.playerIndex, socket, true);

                const pIdxEnc = encodeInt(socket.playerIndex);
                const reviveAnim = `e${pIdxEnc}:CA,((('Dead',2,0)))\n`;
                state.activeSockets.forEach(s => {
                    if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                        s.write(reviveAnim);
                    }
                });
                console.log(`[🎯 DUMMY RESSUSCITOU] Boneco de treino #${socket.playerIndex} pronto no mapa!`);
            }
        }, 1500);
        return;
    }

    // Morte de monstros do Modo Aventura (PVE)
    if (socket.isPveMonster) {
        console.log(`[⚔️ PVE MONSTRO DERROTADO] Monstro #${socket.playerIndex} (${socket.playerName}) atingiu 0 HP.`);
        socket.isDead = true;
        socket.hp = 0;
        sendEntityHpUpdate(socket, socket.playerIndex, socket, true);

        const pIdxEnc = encodeInt(socket.playerIndex);
        const deathAnim = `e${pIdxEnc}:CA,((('Dead',1,0)))\n`;
        state.activeSockets.forEach(s => {
            if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                s.write(deathAnim);
            }
        });

        removePveMonster(socket);
        return;
    }

    const heroId = socket.currentHeroId || 1015;
    const now = Date.now();
    const cooldown = 60000; 
    if (heroId === 1023 && (!socket.lastRebirthTime || now - socket.lastRebirthTime >= cooldown)) {
        socket.lastRebirthTime = now;
        socket.hp = 1;
        socket.isInvincible = true;
        socket.godMode = true;
        sendEntityHpUpdate(socket, socket.playerId || "1", socket, true);
        
        console.log(`[🔥 BRIGIT REBIRTH] Brigit (${socket.playerId}) atingiu 0 HP, ativando casulo passivo.`);
        
        const playerId = socket.playerId || "1";
        applyBuffToEntity(socket, playerId, 1015, 4000);
        applyBuffToEntity(socket, playerId, 1016, 4000);
        
        const skillIdEnc = encodeInt(1068);
        const xEnc = encodeInt(socket.lastX || 0);
        const yEnc = encodeInt(socket.lastY || 0);
        const zEnc = encodeInt(socket.lastZ || 0);
        const targetIdEnc = encodeInt(Number(playerId));
        const skillDoPacket = `e${encodeInt(Number(playerId))}:SkillDo,((${skillIdEnc},(${xEnc},${yEnc},${zEnc})),(${targetIdEnc}))\n`;
        state.activeSockets.forEach(s => {
            if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                s.write(skillDoPacket);
            }
        });

        const enterCocoonPacket = `e${encodeInt(Number(playerId))}:CA,((('SkillE',0,1),1,${targetIdEnc}))\n`;
        state.activeSockets.forEach(s => {
            if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                s.write(enterCocoonPacket);
            }
        });

        sendGlobalSystemChat("🔥 [Sistema] Casulo de Brigit Ativado! Renascendo das cinzas...");
        return;
    }

    // Atribuição de Kill Ambiental / Queda no Abismo / Espinhos (Último atacante nos últimos 10 segundos)
    let isEnvironmentalKill = false;
    if (!attackerSocket && socket.lastAttackerSocket && (Date.now() - (socket.lastAttackerTime || 0)) <= 10000) {
        const recentAttacker = socket.lastAttackerSocket;
        if (!recentAttacker.destroyed && sceneScope.canReceive(socket, recentAttacker) && !combat.sameSide(recentAttacker, socket)) {
            attackerSocket = recentAttacker;
            isEnvironmentalKill = true;
        }
    }

    console.log(`[💀 PLAYER DEAD] O jogador ${socket.playerName || 'Jogador'} morreu! (${isEnvironmentalKill ? 'Atribuição Ambiental/Abismo' : (attackerSocket ? 'Morto por Inimigo' : 'Suicídio')}) Entrando em estado de morte por 10 segundos...\n`);
    const wasReloading = Boolean(socket.isReloading || socket.reloadTimer);
    sceneScope.clearCombatTimers(socket);
    socket.isDead = true;
    if (combat.isFreeForAll(socket)) socket.ffaDeathHandled = true;
    socket.hp = 0;
    socket.shield = 0;
    socket.maxShield = 0;
    socket.tagaloaShieldHp = heroCatalog.getTagaloaShieldMaxHp();
    socket.venusBarrierHp = 0;
    socket.venusBarrierMaxHp = 0;
    socket.venusBarrierActive = false;
    socket.lilyBarrierHp = 0;
    socket.lilyBarrierMaxHp = 0;
    socket.lilyBarrierActive = false;
    socket.isInvincible = false;
    socket.godMode = false;

    if (wasReloading && !socket.destroyed) {
        const reloadWeapon = db.weaponsMap[socket.currentHeroId || 1015];
        const reloadMaxAmmo = reloadWeapon?.cartridgePublic ?? 30;
        const reloadTargetEnc = encodeInt(Number(socket.playerId || socket.playerIndex || 1));
        const reloadShootName = socket.lastReloadShootName ?? 0;
        socket.write(`e${reloadTargetEnc}:OnFillBullet,((${reloadShootName},0,${encodeInt(reloadMaxAmmo)},${encodeInt(reloadMaxAmmo)}))\n`);
    }

    // Limpa a marca de cura e beacon da Carmen caso a própria Carmen morra
    if (socket.carmenHealTargetId) {
        cleanupBuff(null, socket.carmenHealTargetId, 1021, true);
        socket.carmenHealTargetId = null;
    }
    if (socket.carmenBeaconTargetId) {
        cleanupBuff(null, socket.carmenBeaconTargetId, 1005, true);
        cleanupBuff(null, socket.carmenBeaconTargetId, 1010, true);
        socket.carmenBeaconTargetId = null;
    }

    // Limpa todos os buffs do jogador que acabou de morrer
    if (socket.activeBuffs) {
        Object.keys(socket.activeBuffs).forEach(bId => {
            cleanupBuff(socket, socket.playerId, parseInt(bId), true);
        });
    }
    
    sendEntityHpUpdate(socket, socket.playerId || "1", socket, true);

    // Calcula força do Ragdoll empurrando para trás do atacante
    let fx = 0;
    let fy = 0;
    let fz = 0;
    let fmax = 0;
    if (attackerSocket) {
        let attX = attackerSocket.lastX || 0;
        let attY = attackerSocket.lastY || 0;
        let attZ = attackerSocket.lastZ || 0;
        let tarX = socket.lastX || 0;
        let tarY = socket.lastY || 0;
        let tarZ = socket.lastZ || 0;

        let dx = tarX - attX;
        let dy = tarY - attY;
        let dz = tarZ - attZ;

        let lenXZ = Math.sqrt(dx * dx + dz * dz);
        if (lenXZ > 0) {
            dx /= lenXZ;
            dz /= lenXZ;
        } else {
            dx = 0;
            dz = 1;
        }
        dy = 0.3; // Um leve empurrão para cima para criar um efeito de voo

        let len3D = Math.sqrt(dx * dx + dy * dy + dz * dz);
        if (len3D > 0) {
            dx /= len3D;
            dy /= len3D;
            dz /= len3D;
        }

        fx = Math.round(dx * 100);
        fy = Math.round(dy * 100);
        fz = Math.round(dz * 100);
        fmax = 2000; // Multiplicador de 20.0 de força
    } else {
        // Empurrão aleatório na morte
        let angle = Math.random() * Math.PI * 2;
        let dx = Math.cos(angle);
        let dy = 0.3;
        let dz = Math.sin(angle);
        let len3D = Math.sqrt(dx * dx + dy * dy + dz * dz);
        fx = Math.round((dx / len3D) * 100);
        fy = Math.round((dy / len3D) * 100);
        fz = Math.round((dz / len3D) * 100);
        fmax = 1500;
    }

    if (combat.isFreeForAll(socket) && attackerSocket && (attackerSocket === socket || !sceneScope.canReceive(socket, attackerSocket))) attackerSocket = null;
    const targetNetId = Number(socket.playerId || socket.playerIndex || 1);
    const attackerNetId = attackerSocket ? Number(attackerSocket.playerId || attackerSocket.playerIndex || 0) : 0;
    const targetPlayerIdEnc = encodeInt(targetNetId);
    const attackerPlayerIdEnc = encodeInt(attackerNetId);
    const p10 = encodeInt(10); // 10 segundos de duração da morte
    const zero = encodeInt(0);

    // Track per-player stats
    socket.matchDeaths = (socket.matchDeaths || 0) + 1;
    if (attackerSocket) {
        attackerSocket.matchKills = (attackerSocket.matchKills || 0) + 1;
    }

    // Monta aryWarData oficial para atualizar o TAB Scoreboard no cliente C# (NetLiving.cs:966)
    const warDataTarget = `(${encodeInt(socket.matchKills || 0)},${encodeInt(socket.matchDeaths || 0)},${encodeInt(combat.isFreeForAll(socket) ? Number(socket.playerIndex || socket.playerId) : (socket.matchDamage || 0))})`;
    const warDataAttacker = `(${encodeInt(attackerSocket ? (attackerSocket.matchKills || 0) : 0)},${encodeInt(attackerSocket ? (attackerSocket.matchDeaths || 0) : 0)},${encodeInt(attackerSocket ? (combat.isFreeForAll(socket) ? Number(attackerSocket.playerIndex || attackerSocket.playerId) : (attackerSocket.matchDamage || 0)) : 0)})`;
    const warDataParam = `(${warDataTarget},${warDataAttacker})`;

    const weaponIdEnc = encodeInt(attackerSocket ? (socket.lastAttackerAttack || 0) : 0);
    const launcherEnc = encodeInt(attackerSocket ? (socket.lastAttackerLauncher || 0) : 0);
    const deadPacket = `e${targetPlayerIdEnc}:OnDeath,(${attackerPlayerIdEnc},${p10},(${encodeInt(fx)},${encodeInt(fy)},${encodeInt(fz)}),${encodeInt(fmax)},${weaponIdEnc},${launcherEnc},${zero},(),${zero},${warDataParam})\n`;
    
    state.activeSockets.forEach(s => {
        if (sceneScope.canReceive(socket, s) && !s.destroyed) {
            s.write(deadPacket);
            sendTabScoreboard(s);
        }
    });
    
    const sysMsgHex = Buffer.from("[Sistema] Você morreu! Aguardando ressurreição ou renascendo em 10 segundos...").toString('hex');
    const sysNetMsg = `(${encodeInt(8)},'','[mi]${sysMsgHex}[/mi]','',(),(),${encodeInt(1)},('','',0),())`;
    socket.write(`e${encodeInt(socket.playerIndex || 1)}:SendMsgToClient,(${encodeInt(1006)},${sysNetMsg})\n`);

    const match = getTdmMatch(socket);
    const matchRoomUID = match ? match.roomUID : (combat.isFreeForAll(socket) ? socket.currentRoomUID : null);

    if (attackerSocket) {
        if (isEnvironmentalKill) {
            sendGlobalSystemChat(`💀 [Combate] ${attackerSocket.playerName || 'Inimigo'} derrubou ${socket.playerName || 'Jogador'} no abismo / perigos do mapa!`, matchRoomUID);
        } else {
            sendGlobalSystemChat(`💀 [Combate] ${attackerSocket.playerName || 'Inimigo'} derrotou ${socket.playerName || 'Jogador'}!`, matchRoomUID);
        }
        
        if (match && match.state === 'fight') {
            const killerForce = Number(attackerSocket.force);
            if (killerForce === 1 || killerForce === 2) {
                incrementTdmScore(attackerSocket, killerForce, 1);
            }
        }
    } else {
        // Suicídio puro (se jogou no abismo ou nos espinhos sem combate recente)
        sendGlobalSystemChat(`💀 [Combate] ${socket.playerName || 'Jogador'} se suicidou no mapa!`, matchRoomUID);
        
        // REGRA COMPETITIVA DE TDM: Suicídio não nega ponto para o time adversário! O time inimigo recebe o ponto da eliminação.
        if (match && match.state === 'fight') {
            const victimForce = Number(socket.force);
            const enemyForce = victimForce === 1 ? 2 : 1;
            incrementTdmScore(socket, enemyForce, 1);
            sendGlobalSystemChat(`🚩 [TDM] +1 ponto para a Equipe ${enemyForce === 1 ? 'Azul' : 'Vermelha'} por suicídio do adversário!`, matchRoomUID);
        }
    }

    // Limpa o registro de último atacante para o próximo ciclo de vida
    socket.lastAttackerSocket = null;
    socket.lastAttackerTime = 0;

    const arenaMatch = getArenaMatch(socket);
    if (arenaMatch && arenaMatch.state === 'fight') {
        sendArenaFightData(arenaMatch);
        checkArenaRoundState(arenaMatch);
        return;
    }

    const ffa = getFreeForAllMatch(socket);
    if (ffa) {
        tickFreeForAll(ffa);
        if (ffa.state !== 'fight') return;
        socket.ffaRespawnAt = Date.now() + 10000;
    }
    const deathIsCurrent = sceneScope.capture(socket);
    if (socket.respawnTimeout) {
        clearTimeout(socket.respawnTimeout);
    }
    socket.respawnTimeout = setTimeout(() => {
        if (!deathIsCurrent()) return;
        if (socket.isDead) {
            respawnPlayer(socket);
        }
    }, 10000);
}

function respawnPlayer(socket) {
    if (!socket || socket.destroyed || socket.sceneRetired) return;
    if (combat.isFreeForAll(socket) && (!socket.isDead || !socket.isCombatReady || getFreeForAllMatch(socket)?.state !== 'fight' || Date.now() < (socket.ffaRespawnAt || 0))) return;
    
    sceneScope.clearCombatTimers(socket);
    socket.isDead = false;
    socket.ffaDeathHandled = false;
    socket.lastRespawnTime = Date.now();
    
    socket.hp = socket.maxHp || 2000;
    socket.shield = 0;
    socket.maxShield = 0;
    socket.tagaloaShieldHp = heroCatalog.getTagaloaShieldMaxHp();
    socket.venusBarrierHp = 0;
    socket.venusBarrierMaxHp = 0;
    socket.venusBarrierActive = false;
    socket.lilyBarrierHp = 0;
    socket.lilyBarrierMaxHp = 0;
    socket.lilyBarrierActive = false;
    socket.isInvincible = false;
    socket.godMode = false;
    
    const currentMapId = socket.targetMapId || state.getLastMapId() || 5202;
    const coords = combat.isFreeForAll(socket) ? getPlayerSpawnCoordinates(socket) : getSpawnCoordinates(currentMapId, socket.force);
    let sx = coords.sx, sy = coords.sy, sz = coords.sz;
    
    socket.lastX = sx;
    socket.lastY = sy;
    socket.lastZ = sz;
    socket.spawnYaw = coords.yaw;
    
    // Restaura HP antes do evento que reativa o personagem e toca sua fala.
    sendEntityHpUpdate(socket, socket.playerId || "1", socket, true);

    // O cliente já restaura modelo, colisores e câmera em OnRevive.
    const targetPlayerIdEnc = encodeInt(Number(socket.playerId || 1));
    const sxEnc = encodeInt(Math.round(sx));
    const syEnc = encodeInt(Math.round(sy));
    const szEnc = encodeInt(Math.round(sz));
    const revivePacket = `e${targetPlayerIdEnc}:OnRevive,((${sxEnc},${szEnc},${syEnc},${encodeInt(coords.yaw ?? 180)},0,0,''))\n`;
    state.activeSockets.forEach(s => {
        if (sceneScope.canReceive(socket, s) && !s.destroyed) {
            s.write(revivePacket);
        }
    });

    // Não reenviar PlayerCreate/PlayerSelect: recriam áudio/HUD após reviver.
    // Reabastece pela mensagem nativa sem reconstruir o personagem.
    const weapon = db.weaponsMap[socket.currentHeroId || 1015];
    const maxAmmo = weapon?.cartridgePublic ?? 30;
    socket.write(`e${targetPlayerIdEnc}:OnFillBullet,((0,0,${encodeInt(maxAmmo)},${encodeInt(maxAmmo)}))\n`);
    
    const sysMsgHex = Buffer.from(combat.isFreeForAll(socket) ? "[Sistema] Você renasceu!" : "[Sistema] Você renasceu na base da sua equipe!").toString('hex');
    const sysNetMsg = `(${encodeInt(8)},'','[mi]${sysMsgHex}[/mi]','',(),(),${encodeInt(1)},('','',0),())`;
    socket.write(`e${encodeInt(socket.playerIndex || 1)}:SendMsgToClient,(${encodeInt(1006)},${sysNetMsg})\n`);
    
    console.log(`[🌱 PLAYER RESPAWN] Jogador ${socket.playerName || 'Jogador'} renasceu na base.`);
}

function getValidRobotCreateIndex(heroId, mapId) {
    const map = Number(mapId);
    if (map === 5204 || map === 5103) {
        if (heroId === 1003) return 1031;
        if (heroId === 1015 || heroId === 1016) return 1032;
        if (heroId === 1017) return 1033;
        if (heroId === 1021) return 1034;
        return 1035;
    } else if (map === 5024) {
        if (heroId === 1003) return 1011;
        if (heroId === 1015 || heroId === 1016) return 1012;
        if (heroId === 1017) return 1013;
        if (heroId === 1021) return 1014;
        return 1015;
    } else if (map === 5030 || map === 5100 || map === 5102 || map === 5208) {
        if (heroId === 1003) return 1021;
        if (heroId === 1015 || heroId === 1016) return 1022;
        if (heroId === 1017) return 1023;
        if (heroId === 1021) return 1024;
        return 1025;
    } else if (map === 5050 || map === 5104) {
        if (heroId === 1003) return 1041;
        if (heroId === 1015 || heroId === 1016) return 1042;
        if (heroId === 1017) return 1043;
        if (heroId === 1021) return 1044;
        return 1045;
    } else {
        if (heroId === 1003) return 1001;
        if (heroId === 1015 || heroId === 1016) return 1002;
        if (heroId === 1017) return 1003;
        if (heroId === 1021) return 1004;
        return 1005;
    }
}

function getPlayerSpawnPacket(s, receiverSocket) {
    if (receiverSocket && s !== receiverSocket && !sceneScope.sameScene(s, receiverSocket)) return '';
    const heroId = s.currentHeroId || 1015;
    const actualHeroId = (heroId === 1018 || heroId === 1019) ? 1015 : heroId;
    const heroIdEnc = encodeInt(actualHeroId);
    const one = encodeInt(1);
    const zero = encodeInt(0);
    const forceEnc = encodeInt(combat.visibleForce(s, receiverSocket));
    const profile = dbPersistence.loadPlayer(s.playerName);
    const playerEq = state.playerEquipment[s.playerName] || (profile && profile.equipment) || {};
    const eq = playerEq[actualHeroId] || { skin: 1, weapon: 1, emote: 1, pose: 0 };
    const heroInfo = db.generateFullHeroData(
        actualHeroId,
        s.currentSkinId || eq.skin || 1,
        s.currentWeaponId || eq.weapon || 1,
        eq.emote || 1,
        eq.pose || 0,
        profile
    ).split(',').slice(1).join(',');
    
    const pIdx = s.playerIndex || (global.userIndexes && s.playerName && global.userIndexes[s.playerName]) || 1;
    const faceId = profile && profile.face ? profile.face : 2009;
    const paintId = profile && profile.paint ? profile.paint : 1019;
    const playerLevel = profile && profile.level ? profile.level : (s.level || 1);
    const playerExp = profile && profile.exp ? profile.exp : 0;
    const skinId = s.currentSkinId || eq.skin || 1;
    const weaponId = s.currentWeaponId || eq.weapon || 1;

    const faceEnc = encodeInt(faceId);
    const paintEnc = encodeInt(paintId);
    const skinEnc = encodeInt(skinId);
    const weaponEnc = encodeInt(weaponId);
    const levelEnc = encodeInt(playerLevel);
    const expEnc = encodeInt(playerExp);
    
    // PlayerCommon.cs InitBase(szBase):
    // 0:m_nCid, 1:m_sName, 2:m_nShili, 3:m_nModelIndex, 4:m_nXingTai(Stance=0), 5:m_nWeaponGroup(WeaponGroup=0),
    // 6:m_nSex, 7:m_nFace, 8:m_nLevel, 9:m_nExp, 10:m_nGM, 11:UpdateOneHero, 12:m_nInJidi, 13:m_szGMTran, 14:m_nUsePaint, 15:nLoadID
    const baseInfo = `(${encodeInt(pIdx)},'${s.playerNameEnc}',${forceEnc},${heroIdEnc},${zero},${zero},${one},${faceEnc},${levelEnc},${expEnc},${zero},(${heroIdEnc},${heroInfo}),${zero},(),${paintEnc},${one})`;
    
    const maxHp = s.maxHp || db.getHeroMaxHp(actualHeroId);
    const maxHpEnc = encodeInt(maxHp);
    const battleAttr = `(${one},${encodeInt(s.hp ?? maxHp)},${maxHpEnc},${one},${one},${one},${one},${one},${one},${encodeInt(10000)},${one},${one},${one},${one},${one},${one},${one},${one},${one},${one})`;
    
    const currentMapId = s.targetMapId || state.getLastMapId() || 5202;
    const defaultCoords = getSpawnCoordinates(currentMapId, s.force);
    let sx = s.lastX ?? defaultCoords.sx;
    let sy = s.lastY ?? defaultCoords.sy;
    let sz = s.lastZ ?? defaultCoords.sz;
    const scnInfo = `(${encodeInt(getSafeMapId(currentMapId))},${encodeInt(sx)},${encodeInt(sz)},${encodeInt(sy)},${zero},${zero},${zero},${encodeInt(s.spawnYaw ?? 180)})`;
    const geneDataEnc = '()';
    const msg_part1 = `(${baseInfo},${battleAttr},scnInfo,(),(),('Local',${one},${one},${one},${one},${zero},'',${one}),(),${geneDataEnc})`;
    
    const finalMsgPart1 = msg_part1.replace('scnInfo', scnInfo);
    
    const arySkillServerData = db.generateHeroSkillsData(actualHeroId);
    const heroDazhaoData = db.generateHeroDazhaoData(actualHeroId, s.ultEnergy || 0);
    
    const maxAmmo = db.weaponsMap[actualHeroId] && db.weaponsMap[actualHeroId].cartridgePublic !== undefined ? db.weaponsMap[actualHeroId].cartridgePublic : 30;
    const superData = (actualHeroId === 1005) ? `(${forceEnc},'',${zero},(${encodeInt(7000)}))` : `()`;
    const msg_part2 = `(${one},zero,((0),(),(${encodeInt(maxAmmo)})),${encodeInt(actualHeroId)},${heroDazhaoData},${arySkillServerData},${superData},${combat.isFreeForAll(s) ? `(${encodeInt(s.matchKills || 0)},${encodeInt(s.matchDeaths || 0)},${encodeInt(pIdx)})` : '()'})`.replace('zero', zero);
    
    let packet = '';
    if (receiverSocket === s) {
        packet = `e${encodeInt(pIdx)}:SendMsgToClient,(${encodeInt(10001)},(${finalMsgPart1},${msg_part2}))\n`;
        // Envia Tab inicial (5025) com stats zerados para que szTabWinInfo nunca seja null
        const p5000 = encodeInt(5000);
        const p5025 = encodeInt(5025);
        const zero  = encodeInt(0);
        const timeSec = encodeInt(Math.floor((Date.now() - (s.matchStartTime || Date.now())) / 1000));
        const kills   = encodeInt(s.matchKills   || 0);
        const deaths  = encodeInt(s.matchDeaths  || 0);
        const assists = encodeInt(s.matchAssists || 0);
        const damage  = encodeInt(s.matchDamage  || 0);
        const heal    = encodeInt(s.matchHeal    || 0);
        const heroIdForTab = encodeInt(actualHeroId || s.currentHeroId || 1000);
        // [0] userStats: kills, assists, timeSec, damage, heal, deaths
        const userStats = `(${kills},${assists},${timeSec},${damage},${heal},${deaths})`;
        // [1] heroStats: heroId, kills, assists, timeSec, damage, heal, deaths
        const heroStats = `(${heroIdForTab},${kills},${assists},${timeSec},${damage},${heal},${deaths})`;
        // [2] rankList: 5 medals (0=gold)
        const rankList  = `(${zero},${zero},${zero},${zero},${zero})`;
        packet += `e${encodeInt(pIdx)}:SendMsgToClient,(${p5000},(${p5025},(${userStats},${heroStats},${rankList})))\n`;
    } else {
        if (s.isBot) {
            const xEnc = encodeInt(Math.round(sx));
            const yEnc = encodeInt(Math.round(sy));
            const zEnc = encodeInt(Math.round(sz));
            const rxEnc = zero;
            const ryEnc = encodeInt(180);
            const rzEnc = zero;
            const createIndex = s.createIndex || getValidRobotCreateIndex(actualHeroId, currentMapId);
            const createIndexEnc = encodeInt(createIndex);
            packet = `d${encodeInt(pIdx)}\ncRobotAI:${encodeInt(pIdx)},("RobotCreate",(${xEnc},${zEnc},${yEnc}),(${rxEnc},${ryEnc},${rzEnc}),${heroIdEnc},${finalMsgPart1},${msg_part2},${one},${createIndexEnc})\n`;
            logger.logTelemetry('BOT_SPAWN_PACKET', {
                pIdx,
                heroId: actualHeroId,
                createIndex,
                mapId: currentMapId,
                coords: { x: sx, y: sy, z: sz },
                packet: packet.trim()
            });
        } else {
            packet = `d${encodeInt(pIdx)}\ncGamePlayer:${encodeInt(pIdx)},("PlayerSelect",(${finalMsgPart1},${msg_part2}))\n`;
        }
    }

    if (s.visualHeroId) {
        const vIdEnc = encodeInt(s.visualHeroId);
        packet += `e${encodeInt(pIdx)}:NVM,((${vIdEnc}))\n`;
    }
    return packet;
}

function switchPlayerHero(socket, selectedHeroId) {
    if (!socket || socket.destroyed || socket.sceneRetired) return;
    if (Number(socket.currentHeroId) === 2000 || Number(selectedHeroId) === 2000) socket.ultEnergy = 0;
    if (Number(socket.currentHeroId) === 2000 || Number(selectedHeroId) === 2000) socket.ultEnergy = 0;
    socket.visualHeroId = null;
    if (socket.carmenHealTargetId) {
        cleanupBuff(null, socket.carmenHealTargetId, 1021, true);
        socket.carmenHealTargetId = null;
    }
    if (socket.carmenBeaconTargetId) {
        cleanupBuff(null, socket.carmenBeaconTargetId, 1005, true);
        cleanupBuff(null, socket.carmenBeaconTargetId, 1010, true);
        socket.carmenBeaconTargetId = null;
    }
    try {
        if (socket.waitingForHeroLock) {
            sceneScope.invalidate(socket);
            lifecycle.transition(socket, 'loading', 'hero-confirmed');
            console.log(`[🎭 SCENE HERO LOCK] Jogador ${socket.playerName || 'Jogador'} confirmou o herói inicial: ${selectedHeroId}. Iniciando carregamento de mapa...`);
            
            socket.waitingForHeroLock = false;
            socket.currentHeroId = selectedHeroId;
            socket.hasConfirmedHero = true;

            // Reset estatísticas da partida
            resetMatchStats(socket);

            const profile = dbPersistence.loadPlayer(socket.playerName);
            const playerEq = state.playerEquipment[socket.playerName] || (profile && profile.equipment) || {};
            const eq = playerEq[selectedHeroId] || { skin: 1, weapon: 1, emote: 1, pose: 0 };
            socket.currentSkinId = eq.skin || 1;
            socket.currentWeaponId = eq.weapon || 1;
            
            // Sincroniza herói e cosméticos selecionados na sessão persistente e no lobby socket
            if (state.playerSessions && state.playerSessions[socket.playerName]) {
                state.playerSessions[socket.playerName].currentHeroId = selectedHeroId;
                state.playerSessions[socket.playerName].currentSkinId = socket.currentSkinId;
                state.playerSessions[socket.playerName].currentWeaponId = socket.currentWeaponId;
                state.playerSessions[socket.playerName].hasConfirmedHero = true;
            }
            if (socket.lobbySocket) {
                socket.lobbySocket.currentHeroId = selectedHeroId;
                socket.lobbySocket.currentSkinId = socket.currentSkinId;
                socket.lobbySocket.currentWeaponId = socket.currentWeaponId;
                socket.lobbySocket.hasConfirmedHero = true;
            }
            
            const maxHp = db.getHeroMaxHp(selectedHeroId);
            socket.maxHp = maxHp;
            socket.hp = maxHp;
            socket.shield = socket.godMode ? 50000 : 0;
            socket.maxShield = socket.godMode ? 50000 : 0;
            socket.isInvincible = socket.godMode || false;
            socket.isDead = false;
            socket.ffaDeathHandled = false;
            socket.ffaRespawnAt = 0;
            if (selectedHeroId === 1006) {
                socket.tagaloaShieldHp = heroCatalog.getTagaloaShieldMaxHp();
            }
            
            // Envia o pacote de loading screen (s90004 ou 90009) antes de iniciar a transição de mapa
            const p20014 = encodeInt(20014);
            const s90004 = encodeInt(90004);
            const destination = Number(socket.finalMapId || socket.targetMapId);
            const destMapConfig = db.MAP_CONFIG[destination];
            if (destMapConfig && (ffaMaps[destination] || [2, 3, 4, 5, 9, 11, 12].includes(destMapConfig.type))) {
                // 90004 only opens the loading UI; CLoadScene only swaps assets.
                // 90009 updates MatchManager.m_pFbData from staging to the destination mode
                // before GamePlayer.setHeroForce chooses collision layers and mode HUDs.
                socket.write(`e${encodeInt(socket.playerIndex || 1)}:SendMsgToClient,(${p20014},(${encodeInt(90009)},(${encodeInt(destination)},'${destMapConfig.name}')))\n`);
            } else {
                socket.write(`e${encodeInt(socket.playerIndex || 1)}:SendMsgToClient,(${p20014},(${s90004}))\n`);
            }
            
            // 1. Envia 12002 para carregar o novo herói na memória do cliente
            const prefix = `e${socket.playerIndex || 1}`;
            const p1306 = encodeInt(1306);
            const p12002 = encodeInt(12002);
            const heroIdEnc = encodeInt(selectedHeroId);
            const heroInfo = db.generateFullHeroData(selectedHeroId, socket.currentSkinId, socket.currentWeaponId, eq.emote || 1, eq.pose || 0, profile);
            socket.write(`${prefix}:SendMsgToClient,(${p1306},(${p12002},(${heroIdEnc},${heroInfo.split(',').slice(1).join(',')})))\n`);
            
            // 2. Agora sim, envia o CLoadScene para carregar o mapa de combate real!
            const fMapId = socket.finalMapId || global.lastFinalMapId;
            if (fMapId) {
                socket.targetMapId = fMapId;
                socket.targetMapName = db.MAP_CONFIG[fMapId] ? db.MAP_CONFIG[fMapId].name : 'bachang_baxi';
                global.lastFinalMapId = fMapId;
            }
            const scnId = getSafeMapId(socket.targetMapId || state.getLastMapId() || 5202);
            const scnName = socket.targetMapName || global.lastMapName || 'bachang_baxi';
            const loadIsCurrent = sceneScope.capture(socket);
            socket.sceneLoadTimeout = setTimeout(() => {
                if (!loadIsCurrent()) return;
                socket.write(`e:CLoadScene,((0,'${scnName}',0,${encodeInt(scnId)}))\n`);
            }, 1000);
            return;
        }

        console.log(`[🎭 SCENE HERO CHANGE] Executando troca de herói para: ${selectedHeroId}`);
        
        const prefix = `e${socket.playerIndex || 1}`;

        // 1. Envia 12002 para carregar o novo herói na memória do cliente
        const p1306 = encodeInt(1306);
        const p12002 = encodeInt(12002);
        const heroIdEnc = encodeInt(selectedHeroId);
        
        const profile = dbPersistence.loadPlayer(socket.playerName);
        const playerEq = state.playerEquipment[socket.playerName] || (profile && profile.equipment) || {};
        const eq = playerEq[selectedHeroId] || { skin: 1, weapon: 1, emote: 1, pose: 0 };
        socket.currentSkinId = eq.skin || 1;
        socket.currentWeaponId = eq.weapon || 1;
        socket.visualHeroId = null;
        socket.write(`${prefix}:SendMsgToClient,(${p1306},(${p12002},(${heroIdEnc},${db.generateFullHeroData(selectedHeroId, socket.currentSkinId, socket.currentWeaponId, eq.emote || 1, eq.pose || 0, profile).split(',').slice(1).join(',')})))\n`);

        // Mantém o selectedHeroId na sessão do socket
        socket.currentHeroId = selectedHeroId;
        socket.hasConfirmedHero = true;
        if (state.playerSessions && state.playerSessions[socket.playerName]) {
            state.playerSessions[socket.playerName].currentHeroId = selectedHeroId;
            state.playerSessions[socket.playerName].hasConfirmedHero = true;
        }
        if (socket.lobbySocket) {
            socket.lobbySocket.currentHeroId = selectedHeroId;
            socket.lobbySocket.hasConfirmedHero = true;
        }
        const maxHp = db.getHeroMaxHp(selectedHeroId);
        socket.maxHp = maxHp;
        socket.hp = maxHp;
        socket.shield = socket.godMode ? 50000 : 0;
        socket.maxShield = socket.godMode ? 50000 : 0;
        socket.isInvincible = socket.godMode || false;
        socket.isDead = false;
        socket.ffaDeathHandled = false;
        socket.ffaRespawnAt = 0;
        socket.venusBarrierHp = 0;
        socket.venusBarrierMaxHp = 0;
        socket.venusBarrierActive = false;
        socket.lilyBarrierHp = 0;
        socket.lilyBarrierMaxHp = 0;
        socket.lilyBarrierActive = false;
        socket.lastAttackerSocket = null;
        socket.lastAttackerTime = 0;
        socket.lastRespawnTime = Date.now();
        if (selectedHeroId === 1006) {
            socket.tagaloaShieldHp = heroCatalog.getTagaloaShieldMaxHp();
        }
        sceneScope.clearCombatTimers(socket);

        // Ao trocar de herói em jogo, resetar coordenadas para a base de spawn correspondente
        const resetMapId = socket.targetMapId || state.getLastMapId() || 5202;
        const coords = combat.isFreeForAll(socket) ? getPlayerSpawnCoordinates(socket) : getSpawnCoordinates(resetMapId, socket.force);
        socket.lastX = coords.sx;
        socket.lastY = coords.sy;
        socket.lastZ = coords.sz;

        // 2. Envia 10001 (Spawn/Update do Herói com a nova classe/habilidades)
        state.activeSockets.forEach(s => {
            if (sceneScope.canReceive(socket, s) && !s.destroyed) {
                s.write(getPlayerSpawnPacket(socket, s));
            }
        });

        // 3. Destrava tudo (90008)
        const p20014 = encodeInt(20014);
        const s90008 = encodeInt(90008);
        const one = encodeInt(1);
        socket.write(`${prefix}:SendMsgToClient,(${p20014},(${s90008},(),${one}))\n`);
        
        socket.isInvincible = socket.godMode || false;

        // Limpa todos os buffs e timers ativos para evitar que velocidade/efeitos persistam no novo herói
        const playerId = socket.playerId || "1";
        if (socket.activeBuffs) {
            for (const buffId in socket.activeBuffs) {
                const b = socket.activeBuffs[buffId];
                if (b) {
                    if (b.timeout) clearTimeout(b.timeout);
                    else clearTimeout(b);
                    if (b.intervalId) clearInterval(b.intervalId);
                }
            }
            socket.activeBuffs = null;
        }
        socket.activeUltimateBuffs = null;

        if (socket.activeTargetBuffs) {
            for (const key in socket.activeTargetBuffs) {
                const b = socket.activeTargetBuffs[key];
                if (b) {
                    if (b.timeout) clearTimeout(b.timeout);
                    else clearTimeout(b);
                    if (b.intervalId) clearInterval(b.intervalId);
                }
            }
            socket.activeTargetBuffs = null;
        }

        if (socket.invisibilityTimer) {
            clearTimeout(socket.invisibilityTimer);
            socket.invisibilityTimer = null;
        }
        if (socket.ultimateTimer) {
            clearTimeout(socket.ultimateTimer);
            socket.ultimateTimer = null;
        }
        console.log(`[🎭 SCENE HERO CHANGE] Cleared all active buff timers and sent DeleteBuff packets dynamically for player ${playerId}`);

        // Spawn/Atualização de NPCs na Arena de Treino se o mapa for bachang_baxi
        const currentMapId = getSafeMapId(socket.targetMapId || state.getLastMapId() || 5202);
        const currentMapName = socket.targetMapName || global.lastMapName || 'bachang_baxi';
        if (currentMapId === 5202 || currentMapName === 'bachang_baxi') {
            spawnPracticeNpcs(socket, true);
        }
    } catch (e) {
        console.error("[⚠️ ERRO em switchPlayerHero]:", e);
    }
}

function getPlayersSnapshot() {
    const playersMap = {};
    
    state.activeSockets
        .filter(s => s.socketType === 'LOBBY' && !s.destroyed)
        .forEach(s => {
            playersMap[s.playerId] = {
                playerId: s.playerId,
                playerName: s.playerName || 'Unknown',
                heroId: s.currentHeroId || 1015,
                skinId: s.currentSkinId || 1,
                weaponId: s.currentWeaponId || 1,
                hp: 1000,
                maxHp: 1000,
                shield: 0,
                force: s.force || 1,
                x: 0,
                y: 0,
                z: 0,
                godMode: !!s.godMode,
                nocd: !!s.nocd,
                superSpeed: !!s.superSpeed,
                isInvis: !!s.isInvis,
                mapId: s.targetMapId || null,
                mapName: s.targetMapName || 'Lobby',
            };
        });

    state.activeSockets
        .filter(s => s.socketType === 'SCENE' && !s.destroyed)
        .forEach(s => {
            playersMap[s.playerId] = {
                playerId: s.playerId,
                playerName: s.playerName || 'Unknown',
                heroId: s.currentHeroId || 1015,
                skinId: s.currentSkinId || 1,
                weaponId: s.currentWeaponId || 1,
                hp: s.hp || 0,
                maxHp: s.maxHp || 2000,
                shield: s.shield || 0,
                force: s.force || 1,
                x: Math.round((s.lastX || 0) / 100),
                y: Math.round((s.lastY || 0) / 100),
                z: Math.round((s.lastZ || 0) / 100),
                godMode: !!s.godMode,
                nocd: !!s.nocd,
                superSpeed: !!s.superSpeed,
                isInvis: !!s.isInvis,
                mapId: s.targetMapId || null,
                mapName: s.targetMapName || null,
            };
        });

    return Object.values(playersMap);
}

function spawnTdmBases(socket, mapId) {
    if (socket.basesSpawned) return;
    socket.basesSpawned = true;

    const bases = db.mapBases[mapId];
    if (bases && bases.length > 0) {
        bases.forEach(base => {
            const packet = `cbuffitem:${base.id},("createBuffItem",${base.x},${base.y},${base.z},0,'${base.model}',1,'2024/01/01 12:00:00',1,300,300,300,0,0,0,${base.shili},0)\n`;
            socket.write(packet);
            console.log(`[🏰 BASE SHIELD SPAWN] Spawnei base shield ID ${base.id} (Model: ${base.model}, Team: ${base.shili}) no mapa ${mapId} para o jogador.`);
        });
    } else {
        const blueCoords = getSpawnCoordinates(mapId, 1);
        const redCoords = getSpawnCoordinates(mapId, 2);
        const baseBlueId = 9001;
        const baseRedId = 9002;
        
        const packetBlue = `cbuffitem:${baseBlueId},("createBuffItem",${blueCoords.sx},${blueCoords.sy},${blueCoords.sz},0,'buffitemui',1,'2024/01/01 12:00:00',1,300,300,300,0,0,0,1,0)\n`;
        const packetRed = `cbuffitem:${baseRedId},("createBuffItem",${redCoords.sx},${redCoords.sy},${redCoords.sz},0,'buffitemui',1,'2024/01/01 12:00:00',1,300,300,300,0,0,0,2,0)\n`;
        
        socket.write(packetBlue);
        socket.write(packetRed);
        console.log(`[🏰 BASE SHIELD FALLBACK] Spawnei bases fallback 9001/9002 no mapa ${mapId}.`);
    }
}

function createTdmMatchInstance(roomUID, mapId) {
    return {
        roomUID: roomUID,
        mapId: mapId,
        state: 'warmup',
        warmupSeconds: 15,
        fightSeconds: 900,
        targetScore: 50,
        blueScore: 0,
        redScore: 0,
        players: [],
        timer: null,
        botInterval: null,
        endTimeout: null
    };
}

function getTdmMatch(source, createIfMissing = true) {
    let roomUID = null;
    if (source && typeof source === 'object') {
        if (source.roomUID !== undefined && source.state !== undefined) {
            return source; // Já é uma instância válida de match
        }
        roomUID = source.currentRoomUID;
    } else if (source !== undefined && source !== null) {
        roomUID = source;
    }

    if (roomUID != null && global.roomContainers && global.roomContainers.has(Number(roomUID))) {
        const room = global.roomContainers.get(Number(roomUID));
        if (room) {
            if (db.MAP_CONFIG[room.finalMapId || room.mapId]?.type !== 7) return null;
            if (!room.tdmMatch && createIfMissing) {
                room.tdmMatch = createTdmMatchInstance(room.roomUID, room.mapId);
            }
            if (room.tdmMatch) {
                global.activeTdmMatch = room.tdmMatch; // Atualiza ponte de compatibilidade
            }
            return room.tdmMatch || null;
        }
    }
    // Only the admin's explicit no-source calls may use the compatibility pointer.
    // A missing/stale room must never select a different running match.
    if (source == null) return global.activeTdmMatch || null;
    return null;
}

function initializeTdmClient(socket) {
    if (socket.tdmInitialized) return;
    
    const match = getTdmMatch(socket);
    if (!match) return;

    socket.tdmInitialized = true;
    const currentMapId = socket.targetMapId || state.getLastMapId() || 5202;

    const p110300 = encodeInt(110300);
    const p110301 = encodeInt(110301);
    const p110307 = encodeInt(110307);
    
    socket.write(`e${encodeInt(socket.playerIndex || 1)}:SendMsgToClient,(${p110300},(${p110301},()))\n`);
    socket.write(`e${encodeInt(socket.playerIndex || 1)}:SendMsgToClient,(${p110300},(${p110307},(${encodeInt(match.targetScore)},${encodeInt(match.blueScore)},${encodeInt(match.redScore)})))\n`);

    spawnTdmBases(socket, currentMapId);

    if (match.state === 'fight') {
        const p110303 = encodeInt(110303);
        const p1030000 = encodeInt(1030000);
        socket.write(`e${encodeInt(socket.playerIndex || 1)}:SendMsgToClient,(${p110300},(${p110303},()))\n`);
        socket.write(`e${encodeInt(socket.playerIndex || 1)}:SendMsgToClient,(${p1030000},(${encodeInt(1)},'close_open'))\n`);
        setTimeout(() => {
            if (!socket.destroyed) {
                socket.write(`e${encodeInt(socket.playerIndex || 1)}:SendMsgToClient,(${p110300},(${p110303},()))\n`);
                socket.write(`e${encodeInt(socket.playerIndex || 1)}:SendMsgToClient,(${p1030000},(${encodeInt(1)},'close_open'))\n`);
            }
        }, 500);
    }
}

function startTdmMatch(socket) {
    const currentMapId = socket.targetMapId || state.getLastMapId() || 5202;
    const match = getTdmMatch(socket);
    if (!match || socket.sceneRetired || !socket.isCombatReady) return;
    console.log(`[🏆 TDM Sala ${match.roomUID}] startTdmMatch chamado para ${socket.playerName || 'Jogador'} no mapa ${currentMapId}`);

    if (!match.players.includes(socket)) {
        match.players.push(socket);
        console.log(`[🏆 TDM Sala ${match.roomUID}] Jogador ${socket.playerName || 'Jogador'} registrado na partida TDM.`);
    }

    initializeTdmClient(socket);

    if (!match.timer) {
        console.log(`[🏆 TDM Sala ${match.roomUID}] Iniciando timer de atualização periódica...`);
        match.timer = setInterval(() => {
            tickTdmMatch(match);
        }, 1000);
    }
}

function tickTdmMatch(match) {
    if (!match) return;

    const p110300 = encodeInt(110300);
    const p110305 = encodeInt(110305);
    const p110303 = encodeInt(110303);

    match.players = match.players.filter(p => !p.destroyed);
    if (match.players.length === 0) {
        const waitingRoom = global.roomContainers && global.roomContainers.get(Number(match.roomUID));
        if (waitingRoom && waitingRoom.autoDestroyTimer) return;
        console.log(`[🏆 TDM Sala ${match.roomUID}] Nenhum jogador na partida. Encerrando sessão TDM.`);
        if (match.timer) clearInterval(match.timer);
        match.timer = null;
        if (match.roomUID && global.roomContainers && global.roomContainers.has(Number(match.roomUID))) {
            const room = global.roomContainers.get(Number(match.roomUID));
            if (room) room.tdmMatch = null;
        }
        if (global.activeTdmMatch === match) global.activeTdmMatch = null;
        return;
    }

    if (match.state === 'warmup') {
        match.warmupSeconds--;
        
        match.players.forEach(p => {
            p.write(`e${encodeInt(Number(p.playerIndex || 1))}:SendMsgToClient,(${p110300},(${p110305},(${encodeInt(match.warmupSeconds)},${encodeInt(0)},${encodeInt(0)},'')))\n`);
        });

        if (match.warmupSeconds <= 0) {
            match.state = 'fight';
            console.log(`[🏆 TDM Sala ${match.roomUID}] Luta Iniciada! Abrindo portas.`);
            
            const p1030000 = encodeInt(1030000);
            match.players.forEach(p => {
                p.write(`e${encodeInt(Number(p.playerIndex || 1))}:SendMsgToClient,(${p110300},(${p110303},()))\n`);
                p.write(`e${encodeInt(Number(p.playerIndex || 1))}:SendMsgToClient,(${p1030000},(${encodeInt(1)},'close_open'))\n`);
            });
            
            sendGlobalSystemChat("🟢 [TDM] A PARTIDA COMEÇOU! AS PORTAS DA BASE FORAM ABERTAS!", match.roomUID);
        }
    } else if (match.state === 'fight') {
        match.fightSeconds--;

        if (!match.botInterval) {
            console.log(`[🏆 TDM Sala ${match.roomUID}] Bots controlados nativamente via Unity NavMesh (Opcode 4010001/4010002).`);
            match.botInterval = true;
        }

        match.players.forEach(p => {
            p.write(`e${encodeInt(Number(p.playerIndex || 1))}:SendMsgToClient,(${p110300},(${p110305},(${encodeInt(match.fightSeconds)},${encodeInt(1)},${encodeInt(0)},'')))\n`);
        });

        if (match.fightSeconds <= 0) {
            console.log(`[🏆 TDM Sala ${match.roomUID}] Tempo limite esgotado!`);
            endTdmMatch(match);
        }
    }
}

function incrementTdmScore(source, killerForce, amount = 1) {
    let match = null;
    let actualForce = killerForce;
    let actualAmount = amount;

    // Compatibilidade com chamada legada (force, amount)
    if (source === 1 || source === 2) {
        actualForce = source;
        actualAmount = typeof killerForce === 'number' ? killerForce : 1;
        match = getTdmMatch(null, false);
    } else {
        match = getTdmMatch(source, false);
    }

    if (!match || match.state !== 'fight') return;

    if (actualForce === 1) {
        match.blueScore += actualAmount;
    } else if (actualForce === 2) {
        match.redScore += actualAmount;
    }

    console.log(`[🏆 TDM Sala ${match.roomUID}] Placar Atualizado: Blue ${match.blueScore} - Red ${match.redScore} (Alvo: ${match.targetScore})`);

    const p110300 = encodeInt(110300);
    const p110302 = encodeInt(110302);
    match.players.forEach(p => {
        p.write(`e${encodeInt(Number(p.playerIndex || 1))}:SendMsgToClient,(${p110300},(${p110302},(${encodeInt(1)},${encodeInt(match.targetScore)},${encodeInt(match.blueScore)},${encodeInt(match.redScore)})))\n`);
        // Atualiza o Tab (5025) de cada jogador com seus stats atuais
        sendTabScoreboard(p);
    });

    if (match.blueScore >= match.targetScore || match.redScore >= match.targetScore) {
        console.log(`[🏆 TDM Sala ${match.roomUID}] Equipe atingiu a pontuação máxima! Encerrando...`);
        endTdmMatch(match);
    }
}

// ─── TAB SCOREBOARD (5025) ──────────────────────────────────────────────────
function sendTabScoreboard(socket) {
    if (!socket || socket.destroyed) return;
    const p5000  = encodeInt(5000);
    const p5025  = encodeInt(5025);
    const zero   = encodeInt(0);

    const kills   = encodeInt(socket.matchKills  || 0);
    const deaths  = encodeInt(socket.matchDeaths || 0);
    const assists = encodeInt(socket.matchAssists|| 0);
    const damage  = encodeInt(socket.matchDamage || 0);
    const heal    = encodeInt(socket.matchHeal   || 0);
    const timeSec = encodeInt(Math.floor((Date.now() - (socket.matchStartTime || Date.now())) / 1000));
    const heroId  = encodeInt(socket.currentHeroId || 1015);

    // [0] userStats: kills, assists, timeSec, damage, heal, deaths
    const userStats = `(${kills},${assists},${timeSec},${damage},${heal},${deaths})`;
    // [1] heroStats: heroId, kills, assists, timeSec, damage, heal, deaths
    const heroStats = `(${heroId},${kills},${assists},${timeSec},${damage},${heal},${deaths})`;

    // Ranking de medalhas da partida (0=Ouro, 1=Prata, 2=Bronze, -1=Sem medalha)
    const match = getTdmMatch(socket);
    const allPlayers = (match && match.players && match.players.length > 0)
        ? match.players.filter(s => !s.destroyed)
        : (state.activeSockets || []).filter(s => sceneScope.canReceive(socket, s));
    const maxKills = Math.max(...allPlayers.map(p => p.matchKills || 0), 0);
    const maxAssists = Math.max(...allPlayers.map(p => p.matchAssists || 0), 0);
    const maxDamage = Math.max(...allPlayers.map(p => p.matchDamage || 0), 0);
    const maxHeal = Math.max(...allPlayers.map(p => p.matchHeal || 0), 0);

    const killRank = encodeInt((socket.matchKills || 0) === maxKills && maxKills > 0 ? 0 : -1);
    const astRank = encodeInt((socket.matchAssists || 0) === maxAssists && maxAssists > 0 ? 0 : -1);
    const timeRank = zero;
    const dmgRank = encodeInt((socket.matchDamage || 0) === maxDamage && maxDamage > 0 ? 0 : -1);
    const healRank = encodeInt((socket.matchHeal || 0) === maxHeal && maxHeal > 0 ? 0 : -1);

    // [2] rankList: kills_rank, assists_rank, time_rank, damage_rank, heal_rank
    const rankList  = `(${killRank},${astRank},${timeRank},${dmgRank},${healRank})`;

    const tabData = `(${userStats},${heroStats},${rankList})`;
    socket.write(`e${encodeInt(socket.playerIndex || 1)}:SendMsgToClient,(${p5000},(${p5025},${tabData}))\n`);
}

// ─── END-GAME RESULT SCREEN (5026) ───────────────────────────────────────────
function sendGameResult(players, winnerForce, individualWinner = undefined) {
    if (!players || players.length === 0) return;
    const p5000 = encodeInt(5000);
    const p5026 = encodeInt(5026);
    const zero  = encodeInt(0);

    // Determina MVP (melhor da equipe vencedora) e ACE (melhor da equipe derrotada)
    const won = p => individualWinner !== undefined ? p === individualWinner : p.force === winnerForce;
    const winPlayers = players.filter(p => winnerForce === 0 || won(p));
    const losePlayers = players.filter(p => winnerForce !== 0 && !won(p));

    const getBestPlayer = (plist) => {
        if (!plist || plist.length === 0) return null;
        return plist.reduce((best, p) => {
            const pKills = p.matchKills || 0;
            const bKills = best.matchKills || 0;
            if (pKills > bKills) return p;
            if (pKills === bKills && (p.matchDamage || 0) > (best.matchDamage || 0)) return p;
            return best;
        }, plist[0]);
    };

    const mvpSocket = getBestPlayer(winPlayers);
    const aceSocket = getBestPlayer(losePlayers);

    // Calcula valores máximos da partida para cálculo de medalhas (Ouro/Prata/Bronze)
    const maxKills = Math.max(...players.map(p => p.matchKills || 0), 0);
    const maxDamage = Math.max(...players.map(p => p.matchDamage || 0), 0);
    const maxHeal = Math.max(...players.map(p => p.matchHeal || 0), 0);
    const maxAssists = Math.max(...players.map(p => p.matchAssists || 0), 0);

    // Monta ResultUserList: intercalado [nameHex, userData, nameHex, userData, ...]
    const userListParts = players.map(p => {
        const nameHex = encodeHex(p.playerName || 'Player');
        const netId   = encodeInt(p.playerIndex || 1);
        const force   = encodeInt(p.force || 1);
        const heroId  = encodeInt(p.currentHeroId || 1015);

        let nResult = 0;
        let nDraw = 0;
        let nFail = 0;
        if (winnerForce === 0) {
            nDraw = 1;
        } else if (won(p)) {
            nResult = 1;
        } else {
            nFail = 1;
        }

        const isWin   = (nResult === 1);
        const isDraw  = (nDraw === 1);
        const isMvp   = (mvpSocket && p === mvpSocket) ? 1 : 0;
        const isAce   = (aceSocket && p === aceSocket) ? 1 : 0;

        // Conforme OneGameResultCon.cs linhas 206-222:
        // Se szData[9] == 1 (hasBadge):
        //   Se szData[5] == 1 || szData[7] == 1 -> Badge "mvp" com particulas animadas
        //   Se szData[6] == 1 -> Badge "ace"
        const mvpKill = encodeInt(isMvp);
        const ace     = encodeInt(isAce);
        const mvpAst  = zero;
        const unk8    = zero;
        const showMvp = encodeInt((isMvp || isAce) ? 1 : 0);
        const kills   = encodeInt(p.matchKills   || 0);
        const assists = encodeInt(p.matchAssists || 0);
        const timeSec = encodeInt(Math.floor((Date.now() - (p.matchStartTime || Date.now())) / 1000));
        const damage  = encodeInt(p.matchDamage  || 0);
        const heal    = encodeInt(p.matchHeal    || 0);
        const deaths  = encodeInt(p.matchDeaths  || 0);

        let finalLevel = encodeInt(1);
        let expGain    = zero;
        let lvlUp      = zero;
        let nowExp     = zero;

        const isHuman = p.playerName && !p.playerName.startsWith('Bot_');
        if (isHuman) {
            // Calcula recompensa de EXP: Base + Vitória + Kills + Assists + Dano + MVP
            const calculatedExp = 150 + (isWin ? 100 : (isDraw ? 50 : 0)) + 
                                  ((p.matchKills || 0) * 20) + 
                                  ((p.matchAssists || 0) * 10) + 
                                  Math.floor((p.matchDamage || 0) / 100) + 
                                  (isMvp ? 50 : 0);

            // Calcula recompensa de Ouro
            const calculatedGold = 100 + (isWin ? 150 : (isDraw ? 50 : 0)) + 
                                   ((p.matchKills || 0) * 10) + 
                                   (isMvp ? 50 : 0);

            // Sub-arrays de rank/medalhas (0=Ouro, 1=Prata, 2=Bronze, -1=Sem medalha)
            const killRank = (p.matchKills || 0) === maxKills && maxKills > 0 ? 0 : -1;
            const dmgRank = (p.matchDamage || 0) === maxDamage && maxDamage > 0 ? 0 : -1;
            const healRank = (p.matchHeal || 0) === maxHeal && maxHeal > 0 ? 0 : -1;
            const objKillRank = (p.matchAssists || 0) === maxAssists && maxAssists > 0 ? 0 : -1;

            const durationSec = Math.max(60, Math.floor((Date.now() - (p.matchStartTime || (Date.now() - 180000))) / 1000));
            const goldMedalsCount = (killRank === 0 ? 1 : 0) + (dmgRank === 0 ? 1 : 0) + (healRank === 0 ? 1 : 0) + (objKillRank === 0 ? 1 : 0);

            const res = dbPersistence.addMatchRewards(p.playerName, {
                exp: calculatedExp,
                gold: calculatedGold,
                kills: p.matchKills || 0,
                deaths: p.matchDeaths || 0,
                assists: p.matchAssists || 0,
                damage: p.matchDamage || 0,
                healing: p.matchHeal || 0,
                durationSeconds: durationSec,
                goldMedals: goldMedalsCount,
                silverMedals: 0,
                bronzeMedals: 0,
                isWin,
                isDraw,
                isMvp: (isMvp === 1),
                heroId: p.currentHeroId || 1015
            });

            if (res) {
                finalLevel = encodeInt(res.newLevel);
                expGain    = encodeInt(res.expGained);
                lvlUp      = encodeInt(res.numLvlUp);
                nowExp     = encodeInt(res.originalExp);

                p.lastExpGained   = res.expGained;
                p.lastLvlUp       = res.numLvlUp;
                p.lastOriginalExp = res.originalExp;

                p.lastHeroExpGained   = res.heroExpGained;
                p.lastHeroLvlUp       = res.heroNumLvlUp;
                p.lastHeroOriginalExp = res.heroOriginalExp;

                const updatedProfile = dbPersistence.loadPlayer(p.playerName);
                const hIdNum = p.currentHeroId || 1015;
                const playerEq = (updatedProfile && updatedProfile.equipment) || {};
                const eq = playerEq[hIdNum] || { skin: p.currentSkinId || 1, weapon: p.currentWeaponId || 1, emote: 1, pose: 0 };
                const heroFullInfo = db.generateFullHeroData(hIdNum, eq.skin || p.currentSkinId || 1, eq.weapon || p.currentWeaponId || 1, eq.emote || 1, eq.pose || 0, updatedProfile);
                const p1306 = encodeInt(1306);
                const p12002 = encodeInt(12002);
                const hIdEnc = encodeInt(hIdNum);
                const heroTail = heroFullInfo.split(',').slice(1).join(',');

                if (!p.destroyed) {
                    p.write(`e${encodeInt(Number(p.playerIndex || 1))}:SendMsgToClient,(${p1306},(${p12002},(${hIdEnc},${heroTail})))\n`);
                }

                // Roteamento direcionado: envia 21002 (EXP/Level), 12002 (Hero Level/EXP) e 10025 (Saldo de Ouro) para o Lobby específico deste jogador
                const targetLobbySocket = state.activeSockets.find(s => 
                    s.socketType === "LOBBY" && 
                    ((p.playerName && s.playerName === p.playerName) || s.remoteAddress === p.remoteAddress) && 
                    !s.destroyed
                );

                if (targetLobbySocket) {
                    targetLobbySocket.write(`e1:SendMsgToHallClient,(${encodeInt(21002)},(0,0,${encodeInt(res.newExp)},${encodeInt(res.newLevel)},0))\n`);
                    targetLobbySocket.write(`e1:SendMsgToHallClient,(${p1306},(${p12002},(${hIdEnc},${heroTail})))\n`);

                    // Atualiza saldo em tempo real na UI do Saguão (10025: SetMoneyData)
                    const goldEnc = encodeInt(res.newGold);
                    const diaEnc = encodeInt(res.diamonds !== undefined ? res.diamonds : 999999);
                    const szMoney = `(${goldEnc},${goldEnc},${diaEnc},0,${encodeInt(10)},0,${goldEnc},${goldEnc},${goldEnc},0,${goldEnc},0,0,0)`;
                    targetLobbySocket.write(`e1:SendMsgToHallClient,(${encodeInt(10025)},(0,${szMoney}))\n`);
                    console.log(`[💰 SYNC LOBBY] Atualização enviada ao Lobby de ${p.playerName}: Conta Nv.${res.newLevel} (+${res.expGained} EXP), Herói ${hIdNum} Nv.${res.heroNewLevel} (+${res.heroExpGained} EXP), Saldo ${res.newGold} Ouro.`);
                }
            }
        }

        const killRank = (p.matchKills || 0) === maxKills && maxKills > 0 ? 0 : -1;
        const dmgRank = (p.matchDamage || 0) === maxDamage && maxDamage > 0 ? 0 : -1;
        const healRank = (p.matchHeal || 0) === maxHeal && maxHeal > 0 ? 0 : -1;
        const objKillRank = (p.matchAssists || 0) === maxAssists && maxAssists > 0 ? 0 : -1;

        const rankArr = `(${encodeInt(killRank)},${encodeInt(objKillRank)},${encodeInt(-1)},${encodeInt(dmgRank)},${encodeInt(healRank)})`;
        const avgArr  = `(${encodeInt(Math.floor(maxKills * 50))},${zero},${zero},${encodeInt(Math.floor(maxDamage * 50))},${zero})`;
        const maxArr  = `(${encodeInt(killRank === 0 ? 1 : 0)},${encodeInt(objKillRank === 0 ? 1 : 0)},${zero},${encodeInt(dmgRank === 0 ? 1 : 0)},${encodeInt(healRank === 0 ? 1 : 0)})`;

        const userData = `(${netId},'${nameHex}',${force},${finalLevel},${heroId},${mvpKill},${ace},${mvpAst},${unk8},${showMvp},${kills},${assists},${timeSec},${damage},${heal},${deaths},${expGain},${lvlUp},${rankArr},${avgArr},${maxArr},${nowExp})`;
        return `'${nameHex}',${userData}`;
    });

    // ResultUserList: pares intercalados [nameHex, userData, nameHex, userData, ...]
    const resultUserList = `(${userListParts.join(',')})`;

    // ResultHeroList: GameResultUICon.GetHeroData espera apenas os cartões dos heróis jogados pelo PRÓPRIO jogador!
    players.forEach(p => {
        if (!p.destroyed) {
            const hId    = encodeInt(p.currentHeroId || 1015);
            const k      = encodeInt(p.matchKills || 0);
            const d      = encodeInt(p.matchDeaths || 0);
            const a      = encodeInt(p.matchAssists || 0);
            const dmg    = encodeInt(p.matchDamage || 0);
            const addExp = encodeInt(p.lastHeroExpGained !== undefined ? p.lastHeroExpGained : (p.lastExpGained || 150));
            const addLvl = encodeInt(p.lastHeroLvlUp !== undefined ? p.lastHeroLvlUp : 0);
            const oldExp = encodeInt(p.lastHeroOriginalExp !== undefined ? p.lastHeroOriginalExp : 0);
            const myHeroCard = `(${zero},${hId},${k},${d},${a},${dmg},${addExp},${addLvl},${oldExp})`;
            const szNetMsg = `(${resultUserList},(${myHeroCard}))`;
            p.write(`e${encodeInt(Number(p.playerIndex || 1))}:SendMsgToClient,(${p5000},(${p5026},${szNetMsg}))\n`);
        }
    });
    console.log(`[🏆 RESULT] Tela de resultado 5026 enviada para ${players.length} jogador(es). Vencedor: força ${winnerForce}`);
}

function endTdmMatch(source = null) {
    const match = getTdmMatch(source, false);
    if (!match || match.state === 'ended') return;

    match.state = 'ended';
    for (const pending of state.activeSockets.filter(p => p.currentRoomUID != null && String(p.currentRoomUID) === String(match.roomUID))) {
        sceneScope.invalidate(pending);
        lifecycle.transition(pending, 'ended', 'tdm-ended');
    }
    if (match.timer) {
        clearInterval(match.timer);
        match.timer = null;
    }

    if (match.botInterval) {
        clearInterval(match.botInterval);
        match.botInterval = null;
        console.log(`[🏆 TDM Sala ${match.roomUID}] Interval de IA dos bots cancelado.`);
    }

    // Limpa apenas bots pertencentes a este roomUID
    if (state.activeBots) {
        for (const bId in state.activeBots) {
            if (state.activeBots[bId].currentRoomUID === match.roomUID) {
                delete state.activeBots[bId];
            }
        }
    }

    // Marca imediatamente todos os jogadores desta partida como fora da partida para que o matchmaking e listas fiquem limpos
    match.players.forEach(p => {
        p.inMatch = false;
        p.tdmInitialized = false;
        p.waitingForHeroLock = false;
        p.hasConfirmedHero = false;
        p.finalMapId = null;

        if (p.playerName && state.playerSessions[p.playerName]) {
            state.playerSessions[p.playerName].inMatch = false;
            state.playerSessions[p.playerName].finalMapId = null;
            state.playerSessions[p.playerName].targetMapId = 5202;
            state.playerSessions[p.playerName].targetMapName = 'bachang_baxi';
            state.playerSessions[p.playerName].waitingForHeroLock = false;
            state.playerSessions[p.playerName].hasConfirmedHero = false;
        }

        const lobbyMatch = state.activeSockets.find(s => s.socketType === "LOBBY" && ((p.playerName && s.playerName === p.playerName) || s.remoteAddress === p.remoteAddress) && !s.destroyed);
        if (lobbyMatch) {
            lobbyMatch.inMatch = false;
            lobbyMatch.finalMapId = null;
            lobbyMatch.waitingForHeroLock = false;
            lobbyMatch.hasConfirmedHero = false;
        }
    });

    // 🏠 CONTÊINERES DE SALA: Restaura o estado para 5 (Encerrada) ou auto-destrói salas de matchmaking / vazias
    if (match.roomUID && global.roomContainers && global.roomContainers.has(Number(match.roomUID))) {
        const r = global.roomContainers.get(Number(match.roomUID));
        if (r) {
            let activeCount = 0;
            if (r.players) {
                for (let i = 0; i < 10; i++) {
                    if (r.players[i] && r.players[i].socket && !r.players[i].socket.destroyed) {
                        activeCount++;
                    }
                }
            }
            if (activeCount === 0 || r.isMatchmaking) {
                global.roomContainers.delete(Number(match.roomUID));
                console.log(`[🗑️ CONTAINER] Container ${match.roomUID} (${r.roomName}) auto-destruído no término da partida.`);
            } else if (r.state === 4) {
                r.state = 5; // Estado 5 = Encerrada
                console.log(`[🏠 CONTAINER] Sala ${match.roomUID} (${r.roomName}) marcada como encerrada.`);
            }
            r.tdmMatch = null;
        }
    }

    let winnerForce = 0;
    if (match.blueScore > match.redScore) {
        winnerForce = 1;
        sendGlobalSystemChat("🏆 [TDM] FIM DE PARTIDA! A EQUIPE AZUL VENCEU!", match.roomUID);
    } else if (match.redScore > match.blueScore) {
        winnerForce = 2;
        sendGlobalSystemChat("🏆 [TDM] FIM DE PARTIDA! A EQUIPE VERMELHA VENCEU!", match.roomUID);
    } else {
        winnerForce = 0;
        sendGlobalSystemChat("🤝 [TDM] FIM DE PARTIDA! A PARTIDA TERMINOU EM EMPATE!", match.roomUID);
    }

    const p110300 = encodeInt(110300);
    const p110302 = encodeInt(110302);
    const p110306 = encodeInt(110306);
    const p110304 = encodeInt(110304);

    match.players.forEach(p => {
        p.write(`e${encodeInt(Number(p.playerIndex || 1))}:SendMsgToClient,(${p110300},(${p110302},(${encodeInt(0)},${encodeInt(match.targetScore)},${encodeInt(match.blueScore)},${encodeInt(match.redScore)})))\n`);
        p.write(`e${encodeInt(Number(p.playerIndex || 1))}:SendMsgToClient,(${p110300},(${p110306},(${encodeInt(winnerForce)})))\n`);
    });

    // Envia tela de resultado (5026) após 3s para dar tempo do 110306 aparecer
    setTimeout(() => sendGameResult(match.players, winnerForce), 3000);

    // Timeout confortável de tolerância (35s) para o jogador ver suas estatísticas ou fechar no próprio ritmo
    match.endTimeout = setTimeout(() => {
        match.players.forEach(p => {
            if (p && !p.destroyed) {
                try {
                    const pIdxEnc = encodeInt(Number(p.playerIndex || 1));
                    p.write(`e${pIdxEnc}:SendMsgToClient,(${p110300},(${p110304},()))\n`);
                    p.write(`e${pIdxEnc}:SendMsgToClient,(${encodeInt(20001)},(0,'',0))\n`);
                } catch (err) {
                    // Socket já desconectado/fechado pelo jogador
                }
            }
        });
        if (global.activeTdmMatch === match) global.activeTdmMatch = null;
        console.log(`[🏆 TDM Sala ${match.roomUID}] Partida finalizada e limpa com sucesso após timeout.`);
    }, 35000);
}

function handleControlZonePlayersUpdate(socket, parsed) {
    const match = global.activeControlMatch;
    if (!match) return;

    // parsed: [subCmd, nState, userlist]
    // userlist é uma lista de playerIndexes (ex: [1, 2])
    const nState = parsed[1];
    const userlist = parsed[2] || [];

    match.userList = userlist;
    match.occupyForce = nState;

    // Conta dinamicamente os jogadores de cada time na zona
    let blueInZone = 0;
    let redInZone = 0;

    userlist.forEach(pIdx => {
        const pSocket = state.activeSockets.find(s => s.socketType === "SCENE" && s.playerIndex === pIdx && !s.destroyed);
        if (pSocket) {
            if (pSocket.force === 1) blueInZone++;
            else if (pSocket.force === 2) redInZone++;
        }
    });

    match.blueInZone = blueInZone;
    match.redInZone = redInZone;

    console.log(`[🏆 CONTROL ZONE UPDATE] Blue: ${blueInZone} | Red: ${redInZone} | Occupying Force: ${nState} | Userlist: ${userlist.join(',')}`);
}

function handleControlZoneCaptureUpdate(socket, parsed) {
    const match = global.activeControlMatch;
    if (!match) return;

    // parsed: [subCmd, nState, _Over]
    const nState = parsed[1];
    const _Over = parsed[2];

    if (_Over === 1) {
        match.owningForce = nState;
        match.progress = 0;
        match.occupyForce = 0;
        console.log(`[🏆 CONTROL CAPTURED] Point captured by team ${nState}!`);
        sendGlobalSystemChat(`🏆 [CONTROL] O Ponto de Controle foi capturado pela Equipe ${nState === 1 ? 'Azul' : 'Vermelha'}!`);
    }
}

const CONTROL_POINTS = {
    5030: { id: 225, x: 134518, y: 107024, z: 14430 }, // Cloud Rooftop
    5100: { id: 225, x: 134518, y: 107024, z: 14430 }, // Robot Cloud Rooftop
    5208: { id: 118, x: 10000, y: 10000, z: 5400 },   // Altar Sagrado
    5074: { id: 212, x: 53035, y: 73764, z: 6710 },   // Barranca Dam
    5068: { id: 215, x: 14760, y: 26140, z: 1140 },   // Desert Royal Palace
    5102: { id: 215, x: 14760, y: 26140, z: 1140 },   // Robot Desert
    5050: { id: 228, x: 50011, y: 40127, z: 18283 },  // Dracula's Castle
    5055: { id: 231, x: 117150, y: 115175, z: 78675 }, // Underground Palace
    5049: { id: 109, x: 18130, y: 18200, z: 15233 },  // Lost Ancient City (Resource Capture)
    5260: { id: 116, x: 10000, y: 8000, z: 5300 }     // Angel's Manor (Resource Capture)
};

function spawnControlPoint(socket, mapId) {
    const cp = CONTROL_POINTS[mapId];
    if (!cp) {
        console.log(`[🏆 CONTROL CP SPAWN] Nenhum ponto de controle configurado para o mapa ${mapId}`);
        return;
    }
    const netId = 8001; // ID de rede único e fixo para o ponto de controle
    
    // Codifica todos os argumentos numéricos
    const netIdEnc = encodeInt(netId);
    const idEnc = encodeInt(cp.id);
    const xEnc = encodeInt(cp.x);
    const yEnc = encodeInt(cp.y);
    const zEnc = encodeInt(cp.z);
    const forceEnc = encodeInt(0);
    const targetIdEnc = encodeInt(-1);
    const zeroEnc = encodeInt(0);
    
    const packet = `cMapEffect:${netIdEnc},("InitMapEffect",${idEnc},${xEnc},${yEnc},${zEnc},${forceEnc},${targetIdEnc},${zeroEnc},${zeroEnc},${zeroEnc})\n`;
    
    socket.write(packet);
    console.log(`[🏆 CONTROL CP SPAWN] Spawnado MapEffect ${cp.id} no mapa ${mapId} para o jogador (X: ${cp.x}, Y: ${cp.y}, Z: ${cp.z})`);
    
    // Liga o jogador local ao MapEffect para o MapEffectBox detectar colisões
    const p40001 = encodeInt(40001);
    socket.write(`e${encodeInt(socket.playerIndex || 1)}:SendMsgToClient,(${p40001},(${netIdEnc}))\n`);
    console.log(`[🏆 CONTROL CP BIND] Enviado BeginEffectForUser (40001) do MapEffect ${netId} para jogador ${socket.playerIndex}`);
}

function handleControlZonePlayers5007(socket, mapEffectId, playerNetIds) {
    const match = global.activeControlMatch;
    if (!match) return;

    // Inicializa a estrutura de zonas se não existir
    match.zoneDetections = match.zoneDetections || {};
    
    // Armazena a detecção deste cliente
    match.zoneDetections[socket.playerIndex] = playerNetIds;

    // Faz a união de todas as detecções ativas dos clientes
    const allDetected = new Set();
    match.players.forEach(p => {
        if (!p.destroyed && match.zoneDetections[p.playerIndex]) {
            match.zoneDetections[p.playerIndex].forEach(id => {
                allDetected.add(id);
            });
        }
    });

    const consolidatedList = Array.from(allDetected);
    match.userList = consolidatedList;

    // Conta dinamicamente os jogadores de cada time na zona
    let blueInZone = 0;
    let redInZone = 0;

    consolidatedList.forEach(pIdx => {
        const pSocket = state.activeSockets.find(s => s.socketType === "SCENE" && s.playerIndex === pIdx && !s.destroyed);
        if (pSocket) {
            if (pSocket.force === 1) blueInZone++;
            else if (pSocket.force === 2) redInZone++;
        }
    });

    match.blueInZone = blueInZone;
    match.redInZone = redInZone;

    console.log(`[🏆 CONTROL 5007] MapEffect: ${mapEffectId} | Blue in zone: ${blueInZone} | Red in zone: ${redInZone} | Userlist (Consolidada): ${consolidatedList.join(',')}`);
}

function buildScrambleUserData(match) {
    const entries = [];
    match.players.forEach(p => {
        if (!p.destroyed) {
            const pId = Number(p.playerIndex || p.playerId || 1);
            const hId = Number(p.currentHeroId || 1015);
            const stateInt = p.isDead ? 0 : 1;
            const forceInt = Number(p.force || 1);
            entries.push(`'${pId}-${hId}-${stateInt}-${forceInt}'`);
        }
    });
    if (state.activeBots) {
        for (const botId in state.activeBots) {
            const bot = state.activeBots[botId];
            if (bot && !bot.destroyed && !bot.isPracticeDummy && !bot.isPveMonster) {
                const bId = Number(bot.playerIndex || bot.playerId || 800);
                const hId = Number(bot.currentHeroId || 1015);
                const stateInt = bot.hp > 0 ? 1 : 0;
                const forceInt = Number(bot.force || 1);
                entries.push(`'${bId}-${hId}-${stateInt}-${forceInt}'`);
            }
        }
    }
    return `(${entries.join(',')})`;
}

function initializeControlClient(socket) {
    if (socket.controlInitialized) return;
    
    const match = global.activeControlMatch;
    if (!match) return;

    socket.controlInitialized = true;
    const currentMapId = socket.targetMapId || state.getLastMapId() || 5202;
    const mapType = db.MAP_CONFIG[currentMapId] ? db.MAP_CONFIG[currentMapId].type : 3;
    match.mapType = mapType;

    spawnTdmBases(socket, currentMapId);
    spawnControlPoint(socket, currentMapId);

    const pIdxEnc = encodeInt(socket.playerIndex || 1);

    if (mapType === 5) {
        // Resource Capture Mode (Scramble_Message_Handle: 110101..110109)
        const p110100 = encodeInt(110100);
        const p110101 = encodeInt(110101); 
        const p110103 = encodeInt(110103);
        const p110104 = encodeInt(110104); 
        const p110105 = encodeInt(110105);
        const p110106 = encodeInt(110106); 

        socket.write(`e${pIdxEnc}:SendMsgToClient,(${p110100},(${p110101},()))\n`);
        socket.write(`e${pIdxEnc}:SendMsgToClient,(${p110100},(${p110103},${buildScrambleUserData(match)}))\n`);
        socket.write(`e${pIdxEnc}:SendMsgToClient,(${p110100},(${p110106},()))\n`);
        socket.write(`e${pIdxEnc}:SendMsgToClient,(${p110100},(${p110104},(${encodeInt(Math.floor(match.blueScore / 50))},${encodeInt(Math.floor(match.redScore / 50))})))\n`);
        socket.write(`e${pIdxEnc}:SendMsgToClient,(${p110100},(${p110105},('1-1-0-0-0-1-0-210-0-0-')))\n`);

        if (match.state === 'fight') {
            const p110107 = encodeInt(110107); 
            const p1030000 = encodeInt(1030000);
            socket.write(`e${pIdxEnc}:SendMsgToClient,(${p110100},(${p110107},()))\n`);
            socket.write(`e${pIdxEnc}:SendMsgToClient,(${p1030000},(${encodeInt(1)},'close_open'))\n`);
        }
        return;
    }

    // Control Mode (ZhanDianCon: 5000 -> 5001, 5028, 5002, 5008, 5022, 5006)
    const p5000 = encodeInt(5000);
    const subCmd5001 = encodeInt(5001);
    const stateVal = match.state === 'warmup' ? 0 : 1;
    const bluePercentage = Math.floor(match.blueScore / 50);
    const redPercentage = Math.floor(match.redScore / 50);
    const arrayList = [
        encodeInt(stateVal),                    // 0: m_nGameState
        encodeInt(match.warmupSeconds),         // 1: warmup time
        encodeInt(match.fightSeconds),          // 2: fight time
        encodeInt(0),                           // 3
        encodeInt(0),                           // 4
        encodeInt(match.owningForce || 0),      // 5: owningForce
        encodeInt(match.occupyForce || 0),      // 6: occupyForce
        encodeInt(bluePercentage),              // 7: bluePercentage
        encodeInt(redPercentage),               // 8: redPercentage
        encodeInt(0),                           // 9: blue rounds won
        encodeInt(0),                           // 10: red rounds won
        encodeInt(bluePercentage),              // 11: attnum (InitScrollRob blue percentage)
        encodeInt(redPercentage),               // 12: defnum (InitScrollRob red percentage)
        encodeInt(0),                           // 13: m_nLockTime
        encodeInt(0),                           // 14: att wins
        encodeInt(0),                           // 15: def wins
        encodeInt(match.blueInZone || 0),       // 16: blueInZone
        encodeInt(match.redInZone || 0),        // 17: redInZone
        encodeInt(1),                           // 18: nIsPlaySound
        encodeInt(1),                           // 19: nIsChangeHero
        `'${Fun.GetServerDate()}'`,             // 20: sStartChangeTime
        encodeInt(0),                           // 21
        encodeInt(0),                           // 22
        encodeInt(0)                            // 23: general door handle
    ];
    socket.write(`e${pIdxEnc}:SendMsgToClient,(${p5000},(${subCmd5001},((${arrayList.join(',')}),0,0)))\n`);
    console.log(`[🏆 CONTROL HUD INIT] Enviado subcomando 5001 para inicializar o HUD ZhanDian do jogador ${socket.playerIndex}`);

    if (match.state === 'fight') {
        const p1030000 = encodeInt(1030000);
        socket.write(`e${pIdxEnc}:SendMsgToClient,(${p1030000},(${encodeInt(1)},'close_open'))\n`);
    }
}

function startControlMatch(socket) {
    const currentMapId = socket.targetMapId || state.getLastMapId() || 5202;
    const mapType = db.MAP_CONFIG[currentMapId] ? db.MAP_CONFIG[currentMapId].type : 3;
    console.log(`[🏆 CONTROL] startControlMatch chamado para ${socket.playerName || 'Jogador'} no mapa ${currentMapId} (tipo ${mapType})`);

    if (!global.activeControlMatch) {
        global.activeControlMatch = {
            mapId: currentMapId,
            mapType: mapType,
            state: 'warmup',
            warmupSeconds: 15,
            fightSeconds: 600, 
            blueScore: 0,
            redScore: 0,
            players: [],
            timer: null,
            owningForce: 0,
            occupyForce: 0,
            progress: 0,
            userList: [],
            blueInZone: 0,
            redInZone: 0,
            zoneDetections: {},
            botInterval: null
        };
        console.log(`[🏆 CONTROL] Nova sessão Control inicializada para o mapa ${currentMapId}.`);

        // Inicializa bots ativos na partida a partir do lobby / matchBots salvos
        state.activeBots = {};
        if (global.currentMatchBots && global.currentMatchBots.length > 0) {
            global.currentMatchBots.forEach(bot => {
                const coords = getSpawnCoordinates(currentMapId, bot.force);
                state.activeBots[bot.playerIndex] = {
                    playerIndex: bot.playerIndex,
                    playerId: bot.playerIndex,
                    currentRoomUID: socket.currentRoomUID,
                    targetMapId: currentMapId,
                    sceneOwnerName: socket.playerName,
                    playerName: bot.playerName,
                    playerNameEnc: bot.playerNameEnc,
                    force: bot.force,
                    currentHeroId: bot.currentHeroId,
                    hp: 300,
                    maxHp: 300,
                    lastX: coords.sx,
                    lastY: coords.sy,
                    lastZ: coords.sz,
                    write: () => {},
                    socketType: "SCENE",
                    destroyed: false,
                    inMatch: true,
                    isBot: true
                };
            });
            console.log(`[🏆 CONTROL] Inicializados ${Object.keys(state.activeBots).length} bots na partida.`);
        }
    }

    const match = global.activeControlMatch;
    if (!match.players.includes(socket)) {
        match.players.push(socket);
        console.log(`[🏆 CONTROL] Jogador ${socket.playerName || 'Jogador'} registrado na partida Control.`);
    }

    // Garante o envio proativo dos pacotes de spawn de todos os bots para o jogador na partida
    if (state.activeBots && socket && !socket.destroyed) {
        for (const botId in state.activeBots) {
            const bot = state.activeBots[botId];
            socket.bypassInterceptor = true;
            const botPacket = getPlayerSpawnPacket(bot, socket);
            socket.write(botPacket);
            socket.bypassInterceptor = false;
            console.log(`[🤖 BOT SPAWN CONTROL] Pacote cRobotAI do Bot #${bot.playerIndex} (${bot.playerName}) enviado para o jogador ${socket.playerName} em pos (${bot.lastX}, ${bot.lastY}, ${bot.lastZ})!`);
        }
    }

    initializeControlClient(socket);

    if (!match.timer) {
        console.log(`[🏆 CONTROL] Iniciando timer de atualização periódica...`);
        match.timer = setInterval(() => {
            tickControlMatch();
        }, 1000);
    }
}

function tickControlMatch() {
    const match = global.activeControlMatch;
    if (!match) return;

    const p110100 = encodeInt(110100);
    const p110102 = encodeInt(110102); 
    const p5000 = encodeInt(5000);
    const subCmd5028 = encodeInt(5028);
    const isScramble = (match.mapType === 5);

    match.players = match.players.filter(p => !p.destroyed);
    if (match.players.length === 0) {
        console.log(`[🏆 CONTROL] Nenhum jogador na partida. Encerrando sessão Control.`);
        if (match.timer) clearInterval(match.timer);
        global.activeControlMatch = null;
        return;
    }

    if (match.state === 'warmup') {
        match.warmupSeconds--;
        
        match.players.forEach(p => {
            const pIdxEnc = encodeInt(Number(p.playerIndex || 1));
            if (isScramble) {
                p.write(`e${pIdxEnc}:SendMsgToClient,(${p110100},(${p110102},(${encodeInt(match.warmupSeconds)},${encodeInt(0)},${encodeInt(0)},'')))\n`);
            } else {
                p.write(`e${pIdxEnc}:SendMsgToClient,(${p5000},(${subCmd5028},(0,${encodeInt(match.warmupSeconds)},${encodeInt(1)})))\n`);
            }
        });

        if (match.warmupSeconds <= 0) {
            match.state = 'fight';
            console.log(`[🏆 CONTROL] Luta Iniciada! Abrindo portas.`);
            
            const p110107 = encodeInt(110107); 
            const p1030000 = encodeInt(1030000);
            match.players.forEach(p => {
                const pIdxEnc = encodeInt(Number(p.playerIndex || 1));
                if (isScramble) {
                    p.write(`e${pIdxEnc}:SendMsgToClient,(${p110100},(${p110107},()))\n`);
                }
                p.write(`e${pIdxEnc}:SendMsgToClient,(${p1030000},(${encodeInt(1)},'close_open'))\n`);
            });

            if (!isScramble) {
                // Transiciona o HUD ZhanDian para o estado de luta (subcomando 5002)
                const subCmd5002 = encodeInt(5002);
                const startArray = [
                    encodeInt(1),                           // 0: m_nGameState = 1 (fight)
                    encodeInt(match.warmupSeconds),         // 1
                    encodeInt(match.fightSeconds),          // 2: fightSeconds = 600
                    encodeInt(0),                           // 3
                    encodeInt(0),                           // 4
                    encodeInt(0),                           // 5: owningForce
                    encodeInt(0),                           // 6: occupyForce
                    encodeInt(0),                           // 7: bluePercentage
                    encodeInt(0),                           // 8: redPercentage
                    encodeInt(0),                           // 9
                    encodeInt(0),                           // 10
                    encodeInt(0),                           // 11
                    encodeInt(0),                           // 12: defnum (int)
                    encodeInt(0),                           // 13: m_nLockTime = 0
                    encodeInt(0),                           // 14
                    encodeInt(0),                           // 15
                    encodeInt(0),                           // 16
                    encodeInt(0),                           // 17
                    encodeInt(1),                           // 18
                    encodeInt(1),                           // 19
                    `'${Fun.GetServerDate()}'`,             // 20
                    encodeInt(0),                           // 21
                    encodeInt(0),                           // 22
                    encodeInt(0)                            // 23
                ];
                match.players.forEach(p => {
                    p.write(`e${encodeInt(Number(p.playerIndex || 1))}:SendMsgToClient,(${p5000},(${subCmd5002},(${startArray.join(',')})))\n`);
                });
                console.log(`[🏆 CONTROL HUD START] Enviado subcomando 5002 para transição para o modo combate.`);
            }
            
            sendGlobalSystemChat("🟢 [CONTROL] A PARTIDA COMEÇOU! AS PORTAS DA BASE FORAM ABERTAS!");
        }
    } else if (match.state === 'fight') {
        match.fightSeconds--;

        if (!match.botInterval) {
            console.log(`[🏆 CONTROL] Bots controlados nativamente via Unity NavMesh (Opcode 4010001/4010002).`);
            match.botInterval = true;
        }

        let botBlueInZone = 0;
        let botRedInZone = 0;
        if (state.activeBots) {
            for (const botId in state.activeBots) {
                const bot = state.activeBots[botId];
                if (bot && !bot.destroyed && bot.hp > 0) {
                    if (bot.lastX >= 4400 && bot.lastX <= 7200 && bot.lastZ >= 4100 && bot.lastZ <= 6500) {
                        if (bot.force === 1) botBlueInZone++;
                        else if (bot.force === 2) botRedInZone++;
                    }
                }
            }
        }
        
        const blueInZone = (match.blueInZone || 0) + botBlueInZone;
        const redInZone = (match.redInZone || 0) + botRedInZone;

        let ntype = 0;
        let nSpeed = 0;

        if (blueInZone > 0 && redInZone === 0) {
            if (match.owningForce !== 1) {
                if (match.occupyForce === 2) {
                    match.progress = Math.max(0, match.progress - 30);
                    ntype = 2; // Draining red progress
                    nSpeed = 30;
                    if (match.progress === 0) match.occupyForce = 1;
                } else {
                    match.occupyForce = 1;
                    match.progress = Math.min(210, match.progress + 30);
                    ntype = 1; // Filling blue progress
                    nSpeed = 30;
                    if (match.progress >= 210) {
                        match.owningForce = 1;
                        match.occupyForce = 0;
                        match.progress = 0;
                        ntype = 0;
                        nSpeed = 0;
                        sendGlobalSystemChat("🏆 [CONTROL] O Ponto de Controle foi capturado pela Equipe Azul!");

                        if (!isScramble) {
                            const subCmd5019 = encodeInt(5019);
                            match.players.forEach(p => {
                                p.write(`e${encodeInt(Number(p.playerIndex || 1))}:SendMsgToClient,(${p5000},(${subCmd5019},(${encodeInt(1)})))\n`);
                            });
                        }
                    }
                }
            } else {
                match.progress = 210;
            }
        } else if (redInZone > 0 && blueInZone === 0) {
            if (match.owningForce !== 2) {
                if (match.occupyForce === 1) {
                    match.progress = Math.max(0, match.progress - 30);
                    ntype = 2; // Draining blue progress
                    nSpeed = 30;
                    if (match.progress === 0) match.occupyForce = 2;
                } else {
                    match.occupyForce = 2;
                    match.progress = Math.min(210, match.progress + 30);
                    ntype = 1; // Filling red progress
                    nSpeed = 30;
                    if (match.progress >= 210) {
                        match.owningForce = 2;
                        match.occupyForce = 0;
                        match.progress = 0;
                        ntype = 0;
                        nSpeed = 0;
                        sendGlobalSystemChat("🏆 [CONTROL] O Ponto de Controle foi capturado pela Equipe Vermelha!");

                        if (!isScramble) {
                            const subCmd5019 = encodeInt(5019);
                            match.players.forEach(p => {
                                p.write(`e${encodeInt(Number(p.playerIndex || 1))}:SendMsgToClient,(${p5000},(${subCmd5019},(${encodeInt(2)})))\n`);
                            });
                        }
                    }
                }
            } else {
                match.progress = 210;
            }
        } else if (blueInZone > 0 && redInZone > 0) {
            ntype = 0;
            nSpeed = 0;
        } else {
            if (match.progress > 0 && match.occupyForce > 0) {
                match.progress = Math.max(0, match.progress - 15);
                ntype = 2; // Draining progress
                nSpeed = 15;
                if (match.progress === 0) {
                    match.occupyForce = 0;
                }
            }
        }

        // Acumula pontuação a cada segundo para quem controla o ponto (50 pontos = 1% do Slider, total de 5000/100%)
        if (match.owningForce === 1) {
            match.blueScore = Math.min(5000, match.blueScore + 50);
        } else if (match.owningForce === 2) {
            match.redScore = Math.min(5000, match.redScore + 50);
        }

        let occupyState = 0;
        if (blueInZone > 0 && redInZone > 0) {
            occupyState = 3; // Contested / ZhengDuo
        } else if (blueInZone > 0) {
            occupyState = 1;
        } else if (redInZone > 0) {
            occupyState = 2;
        }

        const bluePercentage = Math.floor(match.blueScore / 50);
        const redPercentage = Math.floor(match.redScore / 50);

        if (isScramble) {
            // Resource Capture Mode (Scramble_Message_Handle: 110102, 110103, 110104, 110105)
            const p110103 = encodeInt(110103);
            const p110104 = encodeInt(110104);
            const p110105 = encodeInt(110105);
            const occA = match.occupyForce === 1 ? match.progress : (match.owningForce === 1 ? 210 : 0);
            const occB = match.occupyForce === 2 ? match.progress : (match.owningForce === 2 ? 210 : 0);
            const scrambleState = occupyState === 3 ? 3 : (match.owningForce > 0 ? 4 : (occupyState > 0 ? 2 : 1));
            const userIdsStr = (match.userList || []).join(':');
            const fightDataEntry = `'1-1-${match.occupyForce || match.owningForce || 0}-${blueInZone}-${redInZone}-${scrambleState}-${match.owningForce || 0}-210-${occA}-${occB}-${userIdsStr}'`;
            const userDataList = buildScrambleUserData(match);

            match.players.forEach(p => {
                const pIdxEnc = encodeInt(Number(p.playerIndex || 1));
                p.write(`e${pIdxEnc}:SendMsgToClient,(${p110100},(${p110102},(${encodeInt(match.fightSeconds)},${encodeInt(1)},${encodeInt(0)},'')))\n`);
                p.write(`e${pIdxEnc}:SendMsgToClient,(${p110100},(${p110104},(${encodeInt(bluePercentage)},${encodeInt(redPercentage)})))\n`);
                p.write(`e${pIdxEnc}:SendMsgToClient,(${p110100},(${p110105},(${fightDataEntry})))\n`);
                p.write(`e${pIdxEnc}:SendMsgToClient,(${p110100},(${p110103},${userDataList}))\n`);
            });
        } else {
            // Control Mode (ZhanDianCon: 5008, 5022, 5027, 5028)
            const subCmd5008 = encodeInt(5008);
            const userListStr = match.userList && match.userList.length > 0 
                ? `(${match.userList.map(encodeInt).join(',')})` 
                : '()';

            const updateArray = [
                encodeInt(1),                           // 0: m_nGameState = 1 (fight)
                encodeInt(occupyState),                 // 1: occupyForce/occupyState
                encodeInt(0),                           // 2
                encodeInt(blueInZone),                  // 3: blue players in zone
                encodeInt(redInZone),                   // 4: red players in zone
                userListStr,                            // 5: userList (ArrayList of NetIDs)
                encodeInt(0),                           // 6
                encodeInt(bluePercentage),              // 7: bluePercentage
                encodeInt(redPercentage),               // 8: redPercentage
                encodeInt(0),                           // 9: blue rounds won
                encodeInt(0),                           // 10: red rounds won
                encodeInt(match.owningForce),           // 11: owningForce
                encodeInt(0),                           // 12
                encodeInt(0)                            // 13
            ];
            
            const subCmd5022 = encodeInt(5022);
            const natt = match.occupyForce === 1 ? 1 : 0;
            const ndef = match.occupyForce === 2 ? 1 : 0;
            const nstate = match.occupyForce || match.owningForce || 0;
            const syncProgressArray = [
                encodeInt(natt),                        // 0
                encodeInt(ndef),                        // 1
                encodeInt(nstate),                      // 2
                encodeInt(0),                           // 3
                encodeInt(ntype),                       // 4: 1 = fill, 2 = drain, 0 = stop
                encodeInt(nSpeed)                       // 5: speed
            ];

            match.players.forEach(p => {
                const pIdxEnc = encodeInt(Number(p.playerIndex || 1));
                p.write(`e${pIdxEnc}:SendMsgToClient,(${p5000},(${subCmd5028},(0,${encodeInt(match.fightSeconds)},${encodeInt(3)})))\n`);
                p.write(`e${pIdxEnc}:SendMsgToClient,(${p5000},(${subCmd5008},(${updateArray.join(',')})))\n`);
                p.write(`e${pIdxEnc}:SendMsgToClient,(${p5000},(${subCmd5022},(${syncProgressArray.join(',')})))\n`);
            });

            if ((bluePercentage > 85 && match.owningForce === 1) || (redPercentage > 85 && match.owningForce === 2)) {
                const subCmd5027 = encodeInt(5027);
                const countdownArray = [
                    encodeInt(0), encodeInt(0), encodeInt(0), encodeInt(0), encodeInt(0), encodeInt(0), encodeInt(0),
                    encodeInt(bluePercentage),              // 7
                    encodeInt(redPercentage),               // 8
                    encodeInt(0), encodeInt(0),
                    encodeInt(match.owningForce)            // 11
                ];
                match.players.forEach(p => {
                    p.write(`e${encodeInt(Number(p.playerIndex || 1))}:SendMsgToClient,(${p5000},(${subCmd5027},(${countdownArray.join(',')})))\n`);
                });
            }
        }

        if (match.blueScore >= 5000 || match.redScore >= 5000 || match.fightSeconds <= 0) {
            console.log(`[🏆 CONTROL] Tempo ou pontuação de vitória atingida!`);
            endControlMatch();
        }
    }
}

function endControlMatch() {
    const match = global.activeControlMatch;
    if (!match || match.state === 'ended') return;

    match.state = 'ended';
    if (match.timer) {
        clearInterval(match.timer);
        match.timer = null;
    }

    if (match.botInterval) {
        clearInterval(match.botInterval);
        match.botInterval = null;
        console.log(`[🏆 CONTROL] Interval de IA dos bots cancelado.`);
    }
    state.activeBots = {};

    // Marca imediatamente todos os jogadores como fora da partida para que o matchmaking e listas fiquem limpos
    match.players.forEach(p => {
        p.inMatch = false;
        p.controlInitialized = false;
        p.waitingForHeroLock = false;
        p.hasConfirmedHero = false;
        p.finalMapId = null;

        if (p.playerName && state.playerSessions[p.playerName]) {
            state.playerSessions[p.playerName].inMatch = false;
            state.playerSessions[p.playerName].finalMapId = null;
            state.playerSessions[p.playerName].targetMapId = 5202;
            state.playerSessions[p.playerName].targetMapName = 'bachang_baxi';
            state.playerSessions[p.playerName].waitingForHeroLock = false;
            state.playerSessions[p.playerName].hasConfirmedHero = false;
        }

        const lobbyMatch = state.activeSockets.find(s => s.socketType === "LOBBY" && ((p.playerName && s.playerName === p.playerName) || s.remoteAddress === p.remoteAddress) && !s.destroyed);
        if (lobbyMatch) {
            lobbyMatch.inMatch = false;
            lobbyMatch.finalMapId = null;
            lobbyMatch.waitingForHeroLock = false;
            lobbyMatch.hasConfirmedHero = false;
        }
    });

    let winnerForce = 0;
    if (match.blueScore > match.redScore) {
        winnerForce = 1;
        sendGlobalSystemChat("🏆 [CONTROL] FIM DE PARTIDA! A EQUIPE AZUL VENCEU!");
    } else if (match.redScore > match.blueScore) {
        winnerForce = 2;
        sendGlobalSystemChat("🏆 [CONTROL] FIM DE PARTIDA! A EQUIPE VERMELHA VENCEU!");
    } else {
        winnerForce = 0;
        sendGlobalSystemChat("🤝 [CONTROL] FIM DE PARTIDA! A PARTIDA TERMINOU EM EMPATE!");
    }

    const p110100 = encodeInt(110100);
    const p110108 = encodeInt(110108);
    const p110109 = encodeInt(110109); 
    const isScramble = (match.mapType === 5);

    if (isScramble) {
        match.players.forEach(p => {
            p.write(`e${encodeInt(Number(p.playerIndex || 1))}:SendMsgToClient,(${p110100},(${p110108},(${encodeInt(winnerForce)})))\n`);
        });
    } else {
        // Transiciona o HUD ZhanDian para o estado final de GameOver (subcomando 5006)
        const p5000 = encodeInt(5000);
        const subCmd5006 = encodeInt(5006);
        const endArray = [
            encodeInt(3),                           // 0: m_nGameState = 3 (GameOver)
            encodeInt(0),                           // 1
            encodeInt(0),                           // 2
            encodeInt(0),                           // 3
            encodeInt(0),                           // 4
            encodeInt(0),                           // 5
            encodeInt(0),                           // 6
            encodeInt(0),                           // 7
            encodeInt(0),                           // 8
            encodeInt(0),                           // 9
            encodeInt(0),                           // 10
            encodeInt(0),                           // 11
            encodeInt(0),                           // 12
            encodeInt(0),                           // 13
            encodeInt(winnerForce === 1 ? 2 : 0),   // 14: m_nAttVinnerNum
            encodeInt(winnerForce === 2 ? 2 : 0),   // 15: m_nDefVinnerNum
            encodeInt(0),                           // 16
            encodeInt(0),                           // 17
            encodeInt(1),                           // 18
            encodeInt(1),                           // 19
            `'${Fun.GetServerDate()}'`,             // 20
            `(${encodeInt(0)},${encodeInt(0)},${encodeInt(0)})`, // 21: szResult
            encodeInt(winnerForce),                 // 22: m_nNowWin
            encodeInt(0)                            // 23
        ];
        match.players.forEach(p => {
            p.write(`e${encodeInt(Number(p.playerIndex || 1))}:SendMsgToClient,(${p5000},(${subCmd5006},(${endArray.join(',')})))\n`);
        });
        console.log(`[🏆 CONTROL HUD END] Enviado subcomando 5006 para encerrar o HUD ZhanDian.`);
    }

    // Envia tela de resultado (5026) após 3s
    setTimeout(() => sendGameResult(match.players, winnerForce), 3000);

    setTimeout(() => {
        match.players.forEach(p => {
            if (!p.destroyed) {
                const pIdxEnc = encodeInt(Number(p.playerIndex || 1));
                if (isScramble) {
                    p.write(`e${pIdxEnc}:SendMsgToClient,(${p110100},(${p110109},()))\n`);
                }
                p.write(`e${pIdxEnc}:SendMsgToClient,(${encodeInt(20001)},(0,'',0))\n`);
            }
        });
        global.activeControlMatch = null;
        console.log(`[🏆 CONTROL] Partida limpa com sucesso.`);
    }, 35000);
}

// =========================================================================
// MODO ESCOLTA / PUSH CAR (type === 2: maps 5063, 5022)
// =========================================================================
const ESCORT_CARS = {
    5063: { npcId: 1004, x: 10297, y: 9850, z: 16039, rx: 0, ry: 0, rz: 0 },
    5022: { npcId: 1020, x: 24481, y: 10050, z: 20700, rx: 0, ry: 18000, rz: 0 }
};

function spawnEscortPayload(socket, match) {
    const carCfg = ESCORT_CARS[match.mapId] || ESCORT_CARS[5063];
    const netIdEnc = encodeInt(match.carNetId);
    const npcIdEnc = encodeInt(carCfg.npcId);
    const masterId = match.players[0] ? Number(match.players[0].playerIndex || 1) : Number(socket.playerIndex || 1);
    const masterEnc = encodeInt(masterId);
    const xEnc = encodeInt(carCfg.x);
    const yEnc = encodeInt(carCfg.y);
    const zEnc = encodeInt(carCfg.z);
    const rxEnc = encodeInt(carCfg.rx || 0);
    const ryEnc = encodeInt(carCfg.ry || 0);
    const rzEnc = encodeInt(carCfg.rz || 0);
    const s100 = encodeInt(100);
    const zero = encodeInt(0);
    const one = encodeInt(1);
    const hpEnc = encodeInt(10000);

    const packet = `cChariot_Entity:${netIdEnc},("Initialize",(${one},${netIdEnc},${npcIdEnc},${one},(${xEnc},${yEnc},${zEnc}),(${rxEnc},${ryEnc},${rzEnc}),(${s100},${s100},${s100}),(${zero},${zero},${zero}),${masterEnc},${hpEnc},${zero},${one},${zero},""),((),()),0)\n`;
    socket.write(packet);
    console.log(`[🚛 ESCORT PAYLOAD] Spawnado Chariot_Entity #${match.carNetId} (NPC ${carCfg.npcId}, Master ${masterId}) para ${socket.playerName}`);
}

function initializeEscortClient(socket) {
    const match = global.activeEscortMatch;
    if (!match || socket.escortInitialized) return;
    socket.escortInitialized = true;

    const currentMapId = match.mapId;
    spawnTdmBases(socket, currentMapId);
    spawnEscortPayload(socket, match);

    const p5000 = encodeInt(5000);
    const subCmd5001 = encodeInt(5001);
    const stateVal = match.state === 'warmup' ? 0 : 1;
    const arrayList = [
        encodeInt(stateVal),                    // 0: m_nGameState (0=warmup, 1=fight)
        encodeInt(match.warmupSeconds),         // 1: warmupSeconds
        encodeInt(match.fightSeconds),          // 2: fightSeconds
        encodeInt(0),                           // 3
        encodeInt(0),                           // 4
        encodeInt(0),                           // 5
        encodeInt(0),                           // 6
        encodeInt(0),                           // 7
        encodeInt(match.carNetId),              // 8: m_nCarID
        encodeInt(0),                           // 9: nPoint (current checkpoint)
        encodeInt(1),                           // 10: nIsPlaySound
        encodeInt(1),                           // 11: nIsChangeHero
        `'${Fun.GetServerDate()}'`,             // 12: sStartChangeTime
        encodeInt(0),                           // 13
        encodeInt(0)                            // 14: door handle
    ];
    const pIdxEnc = encodeInt(Number(socket.playerIndex || 1));
    socket.write(`e${pIdxEnc}:SendMsgToClient,(${p5000},(${subCmd5001},((${arrayList.join(',')}),0,0)))\n`);

    if (match.state === 'fight') {
        const p1030000 = encodeInt(1030000);
        socket.write(`e${pIdxEnc}:SendMsgToClient,(${p1030000},(${encodeInt(1)},'close_open'))\n`);
    }
}

function startEscortMatch(socket) {
    const currentMapId = Number(socket.targetMapId || state.getLastMapId() || 5063);
    console.log(`[🚛 ESCORT] startEscortMatch chamado para ${socket.playerName || 'Jogador'} no mapa ${currentMapId}`);

    if (!global.activeEscortMatch || global.activeEscortMatch.state === 'ended') {
        global.activeEscortMatch = {
            mapId: currentMapId,
            state: 'warmup',
            warmupSeconds: 15,
            fightSeconds: 480,
            carNetId: 8002,
            carDis: 0,
            carAllDis: 100,
            players: [],
            timer: null
        };
    }

    const match = global.activeEscortMatch;
    if (!match.players.includes(socket)) {
        match.players.push(socket);
    }

    initializeEscortClient(socket);

    if (!match.timer) {
        match.timer = setInterval(() => {
            tickEscortMatch();
        }, 1000);
    }
}

function tickEscortMatch() {
    const match = global.activeEscortMatch;
    if (!match) return;

    match.players = match.players.filter(p => !p.destroyed);
    if (match.players.length === 0) {
        if (match.timer) clearInterval(match.timer);
        global.activeEscortMatch = null;
        return;
    }

    const p5000 = encodeInt(5000);
    const subCmd5028 = encodeInt(5028);

    if (match.state === 'warmup') {
        match.warmupSeconds--;
        match.players.forEach(p => {
            const pIdxEnc = encodeInt(Number(p.playerIndex || 1));
            p.write(`e${pIdxEnc}:SendMsgToClient,(${p5000},(${subCmd5028},(0,${encodeInt(match.warmupSeconds)},${encodeInt(1)})))\n`);
        });

        if (match.warmupSeconds <= 0) {
            match.state = 'fight';
            const subCmd5002 = encodeInt(5002);
            const p1030000 = encodeInt(1030000);
            const startArray = [
                encodeInt(1),
                encodeInt(0),
                encodeInt(match.fightSeconds),
                encodeInt(0),
                encodeInt(0),
                encodeInt(0),
                encodeInt(0),
                encodeInt(0),
                encodeInt(match.carNetId),
                encodeInt(0),
                encodeInt(1),
                encodeInt(1),
                `'${Fun.GetServerDate()}'`,
                encodeInt(0),
                encodeInt(0)
            ];
            match.players.forEach(p => {
                const pIdxEnc = encodeInt(Number(p.playerIndex || 1));
                p.write(`e${pIdxEnc}:SendMsgToClient,(${p5000},(${subCmd5002},(${startArray.join(',')})))\n`);
                p.write(`e${pIdxEnc}:SendMsgToClient,(${p1030000},(${encodeInt(1)},'close_open'))\n`);
            });
            sendGlobalSystemChat("🟢 [ESCOLTA] A PARTIDA COMEÇOU! ESCOLTE A CARGA ATÉ O DESTINO!");
        }
    } else if (match.state === 'fight') {
        match.fightSeconds--;
        match.players.forEach(p => {
            const pIdxEnc = encodeInt(Number(p.playerIndex || 1));
            p.write(`e${pIdxEnc}:SendMsgToClient,(${p5000},(${subCmd5028},(0,${encodeInt(match.fightSeconds)},${encodeInt(3)})))\n`);
        });

        if (match.fightSeconds <= 0) {
            endEscortMatch(2); // Defenders win when time runs out
        }
    }
}

function handleEscortCarPacket(socket, subCmd, parsed) {
    const match = global.activeEscortMatch;
    if (!match || match.state !== 'fight') return;
    if (subCmd === 5013 && Array.isArray(parsed) && parsed.length >= 3) {
        const nDis = Number(parsed[1] || 0);
        const nAllDis = Number(parsed[2] || 0);
        match.carDis = nDis;
        if (nAllDis > 0) match.carAllDis = nAllDis;
        if (nAllDis > 0 && nDis >= nAllDis - 2) {
            console.log(`[🚛 ESCORT] Carga chegou ao destino (${nDis}/${nAllDis})! Vitória dos Atacantes!`);
            endEscortMatch(1);
        }
    }
}

function endEscortMatch(winnerForce = 1) {
    const match = global.activeEscortMatch;
    if (!match || match.state === 'ended') return;
    match.state = 'ended';
    if (match.timer) {
        clearInterval(match.timer);
        match.timer = null;
    }

    match.players.forEach(p => {
        p.inMatch = false;
        p.escortInitialized = false;
        p.waitingForHeroLock = false;
        p.hasConfirmedHero = false;
        p.finalMapId = null;
        if (p.playerName && state.playerSessions[p.playerName]) {
            state.playerSessions[p.playerName].inMatch = false;
            state.playerSessions[p.playerName].finalMapId = null;
            state.playerSessions[p.playerName].targetMapId = 5202;
            state.playerSessions[p.playerName].targetMapName = 'bachang_baxi';
        }
        const lobbyMatch = state.activeSockets.find(s => s.socketType === "LOBBY" && ((p.playerName && s.playerName === p.playerName) || s.remoteAddress === p.remoteAddress) && !s.destroyed);
        if (lobbyMatch) {
            lobbyMatch.inMatch = false;
            lobbyMatch.finalMapId = null;
        }
    });

    sendGlobalSystemChat(`🏆 [ESCOLTA] FIM DE PARTIDA! A EQUIPE ${winnerForce === 1 ? 'AZUL (ATACANTES)' : 'VERMELHA (DEFENSORES)'} VENCEU!`);

    const p5000 = encodeInt(5000);
    const subCmd5006 = encodeInt(5006);
    const endStateVal = winnerForce === 1 ? 2 : 1; // PushGameOver: 2 => WinForce=1, 1 => WinForce=2
    const endArray = [
        encodeInt(endStateVal),
        encodeInt(0),
        encodeInt(0),
        encodeInt(0),
        encodeInt(0),
        encodeInt(winnerForce),
        encodeInt(0),
        encodeInt(0),
        encodeInt(match.carNetId),
        encodeInt(2),
        encodeInt(1),
        encodeInt(1),
        `'${Fun.GetServerDate()}'`,
        encodeInt(0),
        encodeInt(0)
    ];
    match.players.forEach(p => {
        if (!p.destroyed) {
            p.write(`e${encodeInt(Number(p.playerIndex || 1))}:SendMsgToClient,(${p5000},(${subCmd5006},(${endArray.join(',')})))\n`);
        }
    });

    setTimeout(() => sendGameResult(match.players, winnerForce), 3000);

    setTimeout(() => {
        match.players.forEach(p => {
            if (!p.destroyed) {
                p.write(`e${encodeInt(Number(p.playerIndex || 1))}:SendMsgToClient,(${encodeInt(20001)},(0,'',0))\n`);
            }
        });
        global.activeEscortMatch = null;
    }, 35000);
}

// =========================================================================
// MODO ARENA ELIMINAÇÃO 3v3 (type === 4: maps 5056, 5058, 5059)
// =========================================================================
function getArenaMatch(socket) {
    const match = global.activeArenaMatch;
    if (!match || match.state === 'ended') return null;
    if (!socket) return match;
    if (match.players.includes(socket)) return match;
    return null;
}

function sendArenaFightData(match) {
    if (!match) return;
    const p110000 = encodeInt(110000);
    const p110002 = encodeInt(110002);
    const userEntries = match.players.filter(p => !p.destroyed).map(p => {
        const id = Number(p.playerIndex || p.playerId || 1);
        const k = Number(p.matchKills || 0);
        const a = Number(p.matchAssists || 0);
        const d = Number(p.matchDeaths || 0);
        const hId = Number(p.currentHeroId || 1015);
        const st = p.isDead ? 0 : 1;
        const f = Number(p.force || 1);
        return `'${id}-${k}-${a}-${d}-${hId}-${st}-${f}'`;
    });
    const payload = `((${userEntries.join(',')}),${encodeInt(match.round)},${encodeInt(match.blueRounds)},${encodeInt(match.redRounds)})`;
    match.players.forEach(p => {
        if (!p.destroyed) {
            p.write(`e${encodeInt(Number(p.playerIndex || 1))}:SendMsgToClient,(${p110000},(${p110002},${payload}))\n`);
        }
    });
}

function startArenaMatch(socket) {
    const currentMapId = Number(socket.targetMapId || state.getLastMapId() || 5056);
    console.log(`[⚔️ ARENA] startArenaMatch chamado para ${socket.playerName || 'Jogador'} no mapa ${currentMapId}`);

    if (!global.activeArenaMatch || global.activeArenaMatch.state === 'ended') {
        global.activeArenaMatch = {
            mapId: currentMapId,
            state: 'warmup',
            warmupSeconds: 12,
            fightSeconds: 120,
            round: 1,
            blueRounds: 0,
            redRounds: 0,
            players: [],
            timer: null
        };
    }

    const match = global.activeArenaMatch;
    if (!match.players.includes(socket)) {
        match.players.push(socket);
    }

    spawnTdmBases(socket, currentMapId);

    const p110000 = encodeInt(110000);
    const p110001 = encodeInt(110001);
    const p110009 = encodeInt(110009);
    const pIdxEnc = encodeInt(Number(socket.playerIndex || 1));
    socket.write(`e${pIdxEnc}:SendMsgToClient,(${p110000},(${p110001},()))\n`);
    socket.write(`e${pIdxEnc}:SendMsgToClient,(${p110000},(${p110009},()))\n`);
    sendArenaFightData(match);

    if (match.state === 'fight') {
        const p110004 = encodeInt(110004);
        const p1030000 = encodeInt(1030000);
        socket.write(`e${pIdxEnc}:SendMsgToClient,(${p110000},(${p110004},()))\n`);
        socket.write(`e${pIdxEnc}:SendMsgToClient,(${p1030000},(${encodeInt(1)},'close_open'))\n`);
    }

    if (!match.timer) {
        match.timer = setInterval(() => {
            tickArenaMatch();
        }, 1000);
    }
}

function checkArenaRoundState(match) {
    if (!match || match.state !== 'fight') return;
    const activePlayers = match.players.filter(p => !p.destroyed);
    const bluePlayers = activePlayers.filter(p => Number(p.force) === 1);
    const redPlayers = activePlayers.filter(p => Number(p.force) === 2);

    // Se houver jogadores nos dois lados, verifica se um time inteiro foi eliminado
    if (bluePlayers.length > 0 && redPlayers.length > 0) {
        const blueAlive = bluePlayers.filter(p => !p.isDead).length;
        const redAlive = redPlayers.filter(p => !p.isDead).length;
        if (blueAlive === 0 || redAlive === 0) {
            const roundWinner = blueAlive > 0 ? 1 : (redAlive > 0 ? 2 : 0);
            finishArenaRound(match, roundWinner);
        }
    } else if (activePlayers.length > 0 && activePlayers.every(p => p.isDead)) {
        // Caso de teste solo (1 jogador na sala que se matou)
        const soloForce = Number(activePlayers[0].force) === 1 ? 2 : 1;
        finishArenaRound(match, soloForce);
    }
}

function finishArenaRound(match, roundWinner) {
    if (!match || match.state !== 'fight') return;
    match.state = 'round_end';

    if (roundWinner === 1) match.blueRounds++;
    else if (roundWinner === 2) match.redRounds++;

    sendArenaFightData(match);

    const p110000 = encodeInt(110000);
    const p110010 = encodeInt(110010);
    match.players.forEach(p => {
        if (!p.destroyed) {
            p.write(`e${encodeInt(Number(p.playerIndex || 1))}:SendMsgToClient,(${p110000},(${p110010},(${encodeInt(roundWinner)})))\n`);
        }
    });

    if (match.blueRounds >= 3 || match.redRounds >= 3) {
        const matchWinner = match.blueRounds > match.redRounds ? 1 : 2;
        setTimeout(() => endArenaMatch(match, matchWinner), 3000);
        return;
    }

    setTimeout(() => {
        if (!global.activeArenaMatch || match.state === 'ended') return;
        match.round++;
        match.state = 'warmup';
        match.warmupSeconds = 8;
        match.fightSeconds = 120;

        const p110009 = encodeInt(110009);
        const p1030000 = encodeInt(1030000);
        match.players.forEach(p => {
            if (!p.destroyed) {
                respawnPlayer(p);
                const pIdxEnc = encodeInt(Number(p.playerIndex || 1));
                p.write(`e${pIdxEnc}:SendMsgToClient,(${p110000},(${p110009},()))\n`);
                p.write(`e${pIdxEnc}:SendMsgToClient,(${p1030000},(${encodeInt(1)},'open_close'))\n`);
            }
        });
        sendArenaFightData(match);
    }, 5000);
}

function tickArenaMatch() {
    const match = global.activeArenaMatch;
    if (!match || match.state === 'ended') return;

    match.players = match.players.filter(p => !p.destroyed);
    if (match.players.length === 0) {
        if (match.timer) clearInterval(match.timer);
        global.activeArenaMatch = null;
        return;
    }

    const p110000 = encodeInt(110000);
    const p110006 = encodeInt(110006);

    if (match.state === 'warmup') {
        match.warmupSeconds--;
        match.players.forEach(p => {
            const pIdxEnc = encodeInt(Number(p.playerIndex || 1));
            p.write(`e${pIdxEnc}:SendMsgToClient,(${p110000},(${p110006},(${encodeInt(match.warmupSeconds)},${encodeInt(0)},${encodeInt(1)},'')))\n`);
        });

        if (match.warmupSeconds <= 0) {
            match.state = 'fight';
            const p110004 = encodeInt(110004);
            const p1030000 = encodeInt(1030000);
            match.players.forEach(p => {
                const pIdxEnc = encodeInt(Number(p.playerIndex || 1));
                p.write(`e${pIdxEnc}:SendMsgToClient,(${p110000},(${p110004},()))\n`);
                p.write(`e${pIdxEnc}:SendMsgToClient,(${p1030000},(${encodeInt(1)},'close_open'))\n`);
            });
            sendArenaFightData(match);
            sendGlobalSystemChat(`⚔️ [ARENA] RODADA ${match.round} INICIADA!`);
        }
    } else if (match.state === 'fight') {
        match.fightSeconds--;
        match.players.forEach(p => {
            const pIdxEnc = encodeInt(Number(p.playerIndex || 1));
            p.write(`e${pIdxEnc}:SendMsgToClient,(${p110000},(${p110006},(${encodeInt(match.fightSeconds)},${encodeInt(1)},${encodeInt(1)},'')))\n`);
        });

        if (match.fightSeconds <= 0) {
            const blueAlive = match.players.filter(p => Number(p.force) === 1 && !p.isDead).length;
            const redAlive = match.players.filter(p => Number(p.force) === 2 && !p.isDead).length;
            const winner = blueAlive > redAlive ? 1 : (redAlive > blueAlive ? 2 : 1);
            finishArenaRound(match, winner);
        }
    }
}

function endArenaMatch(match, winnerForce = 1) {
    if (!match || match.state === 'ended') return;
    match.state = 'ended';
    if (match.timer) {
        clearInterval(match.timer);
        match.timer = null;
    }

    match.players.forEach(p => {
        p.inMatch = false;
        p.waitingForHeroLock = false;
        p.hasConfirmedHero = false;
        p.finalMapId = null;
        if (p.playerName && state.playerSessions[p.playerName]) {
            state.playerSessions[p.playerName].inMatch = false;
            state.playerSessions[p.playerName].finalMapId = null;
            state.playerSessions[p.playerName].targetMapId = 5202;
            state.playerSessions[p.playerName].targetMapName = 'bachang_baxi';
        }
        const lobbyMatch = state.activeSockets.find(s => s.socketType === "LOBBY" && ((p.playerName && s.playerName === p.playerName) || s.remoteAddress === p.remoteAddress) && !s.destroyed);
        if (lobbyMatch) {
            lobbyMatch.inMatch = false;
            lobbyMatch.finalMapId = null;
        }
    });

    sendGlobalSystemChat(`🏆 [ARENA] FIM DE PARTIDA! A EQUIPE ${winnerForce === 1 ? 'AZUL' : 'VERMELHA'} VENCEU!`);

    setTimeout(() => sendGameResult(match.players, winnerForce), 2000);

    const p110000 = encodeInt(110000);
    const p110005 = encodeInt(110005);
    setTimeout(() => {
        match.players.forEach(p => {
            if (!p.destroyed) {
                const pIdxEnc = encodeInt(Number(p.playerIndex || 1));
                p.write(`e${pIdxEnc}:SendMsgToClient,(${p110000},(${p110005},()))\n`);
                p.write(`e${pIdxEnc}:SendMsgToClient,(${encodeInt(20001)},(0,'',0))\n`);
            }
        });
        if (global.activeArenaMatch === match) global.activeArenaMatch = null;
    }, 35000);
}

function getPlayerNameFromNgol(texto) {
    if (!texto) return null;
    const parts = texto.split(",");
    if (parts.length >= 5) {
        let namePart = parts[4].trim();
        namePart = namePart.replace(/['"\(\)\r\n]/g, "");
        if (/^[0-9a-fA-F]+$/.test(namePart) && namePart.length % 2 === 0) {
            try {
                const dec = decodeHex(namePart);
                if (dec) {
                    return dec;
                }
            } catch(e) {}
        }
        return namePart;
    }
    return null;
}

function spawnBot(socket, botId, heroId, name, sx, sy, sz, force, lastMapId) {
    try {
        const botIdEnc = encodeInt(botId);
        const heroIdEnc = encodeInt(heroId);
        const forceEnc = encodeInt(force || 1);
        const one = encodeInt(1);
        const zero = encodeInt(0);
        const nameEnc = encodeHex(name);
        const heroInfo = db.generateFullHeroData(heroId).split(',').slice(1).join(',');

        const faceEnc = encodeInt(2009); // Default face ID for bots
        const paintEnc = encodeInt(1019); // Default paint ID for bots
        const baseInfo = `(${botIdEnc},'${nameEnc}',${forceEnc},${heroIdEnc},${zero},${zero},${one},${faceEnc},${one},${zero},${zero},(${heroIdEnc},${heroInfo}),${zero},(),${paintEnc},${one})`;
        const battleAttr = `(${one},${encodeInt(1500)},${encodeInt(1500)},${one},${one},${one},${one},${one},${one},${encodeInt(10000)},${one},${one},${one},${one},${one},${one},${one},${one},${one},${one})`;
        const scnInfo = `(${encodeInt(lastMapId || 5202)},${encodeInt(sx)},${encodeInt(sz)},${encodeInt(sy)},${zero},${zero},${zero},${encodeInt(180)})`;
        
        const msg_part1 = `(${baseInfo},${battleAttr},${scnInfo},(),(),('Local',${one},${one},${one},${one},${zero},'',${one}),(),())`;
        
        const arySkillServerData = db.generateHeroSkillsData(heroId);
        const heroDazhaoData = db.generateHeroDazhaoData(heroId);
        const maxAmmo = db.weaponsMap[heroId] && db.weaponsMap[heroId].cartridgePublic !== undefined ? db.weaponsMap[heroId].cartridgePublic : 30;
        const superData = (heroId === 1005) ? `(${encodeInt(force || 1)},'',${zero},(${encodeInt(7000)}))` : `()`;
        const msg_part2 = `(${one},${zero},((0),(),(${encodeInt(maxAmmo)})),${heroIdEnc},${heroDazhaoData},${arySkillServerData},${superData},())`;

        const spawnPacket = `e${encodeInt(Number(botId))}:SendMsgToClient,(${encodeInt(10001)},(${msg_part1},${msg_part2}))\n// `;
        socket.write(spawnPacket);
        console.log(`[🤖 BOT SPAWNED] Spawned Bot ${name} (NetID: ${botId}, HeroID: ${heroId})`);
    } catch (e) {
        console.error(`[⚠️ ERRO ao spawnar bot ${name}]:`, e);
    }
}

function spawnPracticeNpcs(socket, force = false) {
    if (!socket || socket.destroyed) return;
    if (socket.practiceNpcsSpawned && !force) {
        console.log(`[🤖 SCENE] NPCs de treino já foram spawnados para este socket. Ignorando re-spawn.`);
        return;
    }
    socket.practiceNpcsSpawned = true;
    console.log(`[🤖 SCENE] Enviando/Atualizando NPCs de treino no mapa bachang_baxi...`);
    
    const dummyForce = (socket.force === 1) ? 2 : 1;
    for (const id of socket.practiceNpcIds || []) delete state.spawnedNpcs[id];
    socket.practiceNpcIds = [];
    for (const [index, hp, x] of [[1121, 10, 50000], [1122, 1000, 50500], [1101, 1500, 49500]]) {
        global.nextNpcId = Math.max(global.nextNpcId || 0, 100000);
        const id = global.nextNpcId++;
        socket.practiceNpcIds.push(id);
        state.spawnedNpcs[id] = sceneScope.tagEntity({ id, index, hp, maxHp: hp, x, y: 20104,
            z: 26000, force: dummyForce, isPracticeDummy: true }, socket);
    }
    
    for (const key in state.spawnedNpcs) {
        const npc = state.spawnedNpcs[key];
        if (!sceneScope.sameScene(socket, npc)) continue;
        const className = db.getEntityClassName(npc.index);
        const npcForce = npc.force || 2;
        const spawnPacket = `c${className}:${encodeInt(npc.id)},("Initialize",(${encodeInt(1)},${encodeInt(npc.id)},${encodeInt(npc.index)},${encodeInt(npcForce)},(${encodeInt(npc.x)},${encodeInt(npc.y)},${encodeInt(npc.z)}),(0,0,0),(${encodeInt(100)},${encodeInt(100)},${encodeInt(100)}),(0,0,0),${encodeInt(-1)},${encodeInt(npc.maxHp)},${encodeInt(0)},${encodeInt(1)},${encodeInt(0)},""),((),()),0)\n`;
        socket.write(spawnPacket);
        console.log(`[🤖 NPC SPAWNED]: ${spawnPacket.trim()}`);
    }
}

/**
 * Gera a estrutura serializada de dados de masmorra PvE (Fuben) esperada pela Unity
 */
function buildPveData(fbType, profile) {
    const now = new Date();
    const dateStr = now.toISOString().replace('T', ' ').substring(0, 19);
    
    // 12 fases com 3 estrelas cada
    const starsList = `(${encodeInt(3)},${encodeInt(3)},${encodeInt(3)},${encodeInt(3)},${encodeInt(3)},${encodeInt(3)},${encodeInt(3)},${encodeInt(3)},${encodeInt(3)},${encodeInt(3)},${encodeInt(3)},${encodeInt(3)})`;
    const sweepList = `(0,0,0,0,0,0,0,0,0,0,0,0)`;
    
    return `(${encodeInt(12)},${starsList},0,0,${encodeInt(1)},${encodeInt(fbType)},${encodeInt(99999)},0,'${dateStr}',0,0,'${dateStr}',0,${sweepList},0,0)`;
}

function buildGeneData(profile) {
    let step = 1;
    let star = 1;
    if (profile && profile.runes) {
        step = profile.runes.step || 1;
        star = profile.runes.star || 1;
    }
    return `(${encodeInt(step)},${encodeInt(star)})`;
}

function spawnPracticeHeroBot(initiatorSocket, options = {}) {
    try {
        const heroId = options.heroId || 1015;
        const mode = options.mode || 'static'; // 'static', 'strafe', 'follow', 'combat'
        const hpOption = options.hpOption || 'normal'; // 'normal', 'tank', 'immortal'
        const botTeam = options.team !== undefined ? options.team : (initiatorSocket && initiatorSocket.force === 1 ? 2 : 1);
        
        let sx = options.x;
        let sy = options.y;
        let sz = options.z;
        
        if (sx === undefined || sz === undefined) {
            const originX = initiatorSocket && initiatorSocket.lastX !== undefined ? initiatorSocket.lastX : 5000;
            const originY = initiatorSocket && initiatorSocket.lastY !== undefined ? initiatorSocket.lastY : 3820;
            const originZ = initiatorSocket && initiatorSocket.lastZ !== undefined ? initiatorSocket.lastZ : 5000;
            
            sx = originX;
            sy = originY;
            sz = originZ + 600;
        }
        
        let dummyIndex = 801;
        while (state.activeBots[dummyIndex] || state.spawnedNpcs[dummyIndex] || state.activeSockets.some(p=>Number(p.playerId)===dummyIndex)) {
            dummyIndex++;
        }
        
        const baseHeroHp = (db.getHeroMaxHp && db.getHeroMaxHp(heroId)) || 2000;
        const heroMaxShield = (heroId === 1020 ? 1000 : (heroId === 1001 ? 500 : 0));
        
        let maxHp = baseHeroHp;
        let shield = heroMaxShield;
        if (hpOption === 'tank') {
            maxHp = 10000;
            shield = 2000;
        }
        
        const heroName = db.getHeroName(heroId) || `Dummy ${heroId}`;
        const dummyName = `[TREINO] ${heroName} #${dummyIndex - 800}`;
        const dummyNameEnc = Fun.EncodingUTF8(dummyName);
        
        const currentMapId = (initiatorSocket && initiatorSocket.targetMapId) || state.getLastMapId() || 5202;
        
        const dummyBot = {
            playerIndex: dummyIndex,
            playerId: dummyIndex,
            playerName: dummyName,
            playerNameEnc: dummyNameEnc,
            force: botTeam,
            currentHeroId: heroId,
            createIndex: options.createIndex,
            currentSkinId: 1,
            currentWeaponId: 1,
            hp: maxHp,
            maxHp: maxHp,
            shield: shield,
            maxShield: shield,
            lastX: sx,
            lastY: sy,
            lastZ: sz,
            spawnX: sx,
            spawnY: sy,
            spawnZ: sz,
            targetMapId: currentMapId,
            write: () => {},
            socketType: "SCENE",
            destroyed: false,
            inMatch: true,
            isBot: true,
            isPracticeDummy: true,
            dummyMode: mode,
            hpOption: hpOption,
            strafeDir: 1,
            strafeTicks: 0,
            faceAngle: 0
        };
        
        state.activeBots = state.activeBots || {};
        state.activeBots[dummyIndex] = dummyBot;
        sceneScope.tagEntity(dummyBot, initiatorSocket);
        
        state.activeSockets.forEach(s => {
            if (sceneScope.canReceive(dummyBot, s)) {
                s.write(getPlayerSpawnPacket(dummyBot, s));
            }
        });
        
        console.log(`[🎯 DUMMY SPAWN] Criado Boneco de Treino #${dummyIndex} (${heroName} - ID ${heroId}, Modo: ${mode}, HP: ${maxHp}/${shield}) em (${sx}, ${sy}, ${sz})!`);
        return dummyBot;
    } catch (e) {
        console.error(`[⚠️ ERRO ao spawnar Practice Hero Bot]:`, e);
        return null;
    }
}

function clearPracticeHeroBots() {
    let count = 0;
    if (state.activeBots) {
        for (const botId in state.activeBots) {
            const bot = state.activeBots[botId];
            if (bot && bot.isPracticeDummy) {
                const pIdxEnc = encodeInt(bot.playerIndex);
                const destroyPacket = `d${pIdxEnc}\n`;
                state.activeSockets.forEach(s => {
                    if (s.socketType === "SCENE" && !s.destroyed) {
                        s.write(destroyPacket);
                    }
                });
                delete state.activeBots[botId];
                count++;
            }
        }
    }
    console.log(`[🎯 DUMMIES CLEARED] Removidos ${count} bonecos de treino da partida.`);
    return count;
}

// Adventure uses RobotAI/robot.xml, not the unrelated training Npc.xml IDs.
const pveRobots = require('./pveRobots.json');
const { PacketBuilder } = require('./utils');
const pveMatches = new Set();
let nextPveEntityId = 600000;
function pveId() {
    while (state.activeBots[nextPveEntityId] || state.spawnedNpcs[nextPveEntityId] || state.activeSockets.some(s => Number(s.playerIndex) === nextPveEntityId)) nextPveEntityId++;
    return nextPveEntityId++;
}
function getPveMatch(socket) {
    return [...pveMatches].find(m => !m.isAdmin && m.state !== 'ended' && sceneScope.sameScene(m, socket)) || null;
}
function pvePeers(match) { return match.players.filter(p => sceneScope.canReceive(match, p)); }
function pveMessage(player, opcode, data) {
    player.write(PacketBuilder.rpc(player.playerIndex, 'SendMsgToClient', [19000, [opcode, data]]));
}
function pveBroadcast(match, opcode, data) { pvePeers(match).forEach(p => pveMessage(p, opcode, data)); }
function pveDate(time) {
    const d = new Date(time), pad = n => String(n).padStart(2, '0');
    return `${d.getFullYear()}/${pad(d.getMonth()+1)}/${pad(d.getDate())} ${pad(d.getHours())}:${pad(d.getMinutes())}:${pad(d.getSeconds())}`;
}
function pveInfo(m) {
    const info = [1, m.state === 'fight' ? 1 : 0, 0, pveDate(m.deadline), 0, pveDate(m.readyAt), m.round, m.maxRound,
        m.monsterNum, pveDate(m.deadline), m.crystal?.id || 0, m.pveType];
    // Preserve the existing three-wave prototype; all Dark Land doors remain open.
    if (m.pveType === 12) info.push([1,1,2,2,3,3,4,4,5,5,6,6,7,7,8,8,9,9]);
    info.push(pveDate(m.startTime));
    return info;
}
function pveUser(p) { return [p.playerIndex, p.playerNameEnc || encodeHex(p.playerName), 0, 1, 0, p.playerIndex, [], 1000]; }
function crystalPacket(c) {
    return PacketBuilder.create(db.getEntityClassName(1130), c.id, 'Initialize',
        [[1,c.id,1130,1,[c.x,c.y,c.z],[0,0,0],[150,150,150],[0,0,0],-1,c.hp,0,1,0,''],[[],[]],0]);
}
function spawnPveCrystal(match) {
    if (!match.crystal) {
        match.crystal = sceneScope.tagEntity({id:pveId(), index:1130, hp:10000, maxHp:10000,
            x:11665,y:10776,z:2461,force:1,isCrystal:true,pveMatch:match},match);
        state.spawnedNpcs[match.crystal.id] = match.crystal;
    }
    return match.crystal;
}
function spawnPveWave(match, round) {
    if (!match || match.state === 'ended') return;
    match.round = round;
    logger.logTelemetry('PVE_WAVE', {roomUID:match.currentRoomUID,mapId:match.mapId,round});
    const lv = match.pveLv;
    const base = match.pveType === 9 ? ([1100,1200,1300,1400,1500,1600,1700,1800,1900,2000,4000,4100][lv]) : 2100 + lv*100;
    const offsets = match.pveType === 9
        ? (round===1 ? [1,2,3,4,1,2,3,4] : round===2 ? [5,9,6,10,7,11,8,12,5,9,6,10] : [5,6,7,8,13,14,17])
        : (round===1 ? [4,5,2,7,8,9,10,11] : round===2 ? [6,12,21,2] : [13,14,15,16,17,18,19]);
    for (const offset of offsets) {
        const createIndex = base+offset, def = pveRobots[createIndex];
        if (!def) throw new Error(`Missing native PvE robot ${createIndex}`);
        // Stationary native AI has no route; place it at a patrol origin in the current zone.
        const pos = def.pos || pveRobots[base + (round===1 ? 4 : round===2 ? 6 : 18)].pos;
        const id = pveId(), hp = def.boss ? 15500+lv*3500 : 750+lv*150+(round-1)*250;
        const bot = sceneScope.tagEntity({playerIndex:id,playerId:String(id),playerName:`PVE-${id}`,
            playerNameEnc:encodeHex(def.boss ? 'Chefe' : 'Invasor'),currentHeroId:def.hero,createIndex,
            lastX:pos[0],lastY:pos[1],lastZ:pos[2],force:2,hp,maxHp:hp,
            isBot:true,isPveMonster:true,isDead:false,inMatch:true,pveMatch:match,loadId:1,
            authority:null,readyClients:new Set(),write() {}}, match);
        match.monsters[id] = bot;
        state.activeBots[id] = bot;
        pvePeers(match).forEach(p => p.write(getPlayerSpawnPacket(bot,p)));
    }
    updatePveMonsterCount(match);
}
function updatePveMonsterCount(match) {
    match.monsterNum = Object.keys(match.monsters).length;
    if (match.isAdmin) return;
    pveBroadcast(match,19008,[match.monsterNum]);
}
function beginPveWave(match) {
    if (match.state === 'ended' || match.state === 'victory' || match.state === 'defeat') return;
    match.state='fight';
    pveBroadcast(match,19003,pveInfo(match));
    spawnPveWave(match,match.round);
}
function handlePveWaveCleared(match) {
    if (!match || match.isAdmin || match.state !== 'fight') return;
    pveBroadcast(match,19004,[1,match.round,1000,1,match.crystal?.hp>0 || match.pveType===12 ? 1 : 0]);
    if (match.round >= match.maxRound) { sendPveVictory(match); return; }
    match.state='round_cleared';
    match.readyAt=Date.now()+5000;
    match.timer=setTimeout(()=>{ if(match.state!=='round_cleared')return; match.round++; beginPveWave(match); },5000);
}
function finishPveMatch(match, won) {
    if (!match || ['ended','victory','defeat'].includes(match.state)) return;
    match.state=won?'victory':'defeat';
    clearTimeout(match.timer);
    clearInterval(match.tickInterval);
    pveBroadcast(match,19002,[0,won?1:0]);
    const peers = pvePeers(match);
    peers.forEach(p => {
        p.inMatch = false;
        p.waitingForHeroLock = false;
        p.hasConfirmedHero = false;
        p.finalMapId = null;
        if (p.playerName && state.playerSessions[p.playerName]) {
            state.playerSessions[p.playerName].inMatch = false;
            state.playerSessions[p.playerName].finalMapId = null;
            state.playerSessions[p.playerName].targetMapId = 5202;
            state.playerSessions[p.playerName].targetMapName = 'bachang_baxi';
        }
        const lobbyMatch = state.activeSockets.find(s => s.socketType === "LOBBY" && ((p.playerName && s.playerName === p.playerName) || s.remoteAddress === p.remoteAddress) && !s.destroyed);
        if (lobbyMatch) {
            lobbyMatch.inMatch = false;
            lobbyMatch.finalMapId = null;
        }
    });
    setTimeout(() => sendGameResult(peers, won ? 1 : 2), 3000);
    match.timer=setTimeout(()=>endPveMatch(match),8000);
}
function sendPveVictory(match) { finishPveMatch(match,true); }
function pveOwnsBot(socket,bot) {
    return !!(bot?.isPveMonster && !bot.isDead && bot.authority===socket && sceneScope.canReceive(bot,socket));
}
function pveAuthorityMessage(bot,player) {
    player.write(PacketBuilder.rpc(player.playerIndex,'SendMsgToClient',[4010001,[bot.playerIndex,0,bot.authority.playerIndex]]));
}
function selectPveBotAuthority(socket,id) {
    const bot=state.activeBots[Number(id)];
    if (!bot?.isPveMonster || bot.isDead || socket.isDead || !sceneScope.canReceive(bot,socket)) return false;
    bot.readyClients.add(socket);
    if (!bot.authority || !sceneScope.canReceive(bot,bot.authority)) {
        bot.authority=socket;
        logger.logTelemetry('PVE_BOT_AUTHORITY',{roomUID:bot.currentRoomUID,botId:bot.playerIndex,player:socket.playerName});
        bot.readyClients.forEach(p=>{ if(sceneScope.canReceive(bot,p)) pveAuthorityMessage(bot,p); });
    } else pveAuthorityMessage(bot,socket);
    return true;
}
function tickPveMatch(match) {
    if (!match) { [...pveMatches].forEach(tickPveMatch); return; }
    if (['ended','victory','defeat'].includes(match.state)) return;
    if (match.isAdmin) match.players=state.activeSockets.filter(p=>sceneScope.canReceive(match,p));
    if (!pvePeers(match).length) { endPveMatch(match); return; }
    if (Date.now()>=match.deadline) { finishPveMatch(match,false); return; }
    for (const bot of Object.values(match.monsters)) {
        if (bot.authority && (bot.authority.isDead || !sceneScope.canReceive(bot,bot.authority))) {
            if (sceneScope.canReceive(bot,bot.authority)) bot.authority.write(PacketBuilder.rpc(bot.authority.playerIndex,'SendMsgToClient',[4010004,[bot.playerIndex]]));
            bot.authority=null;
        }
        // CheckPlayer responds only once the model has loaded. Never poll an
        // established owner: the native method clears SyncNetObj on every call.
        for (const p of pvePeers(match)) {
            if (!p.isDead && (!bot.readyClients.has(p) || !bot.authority))
                p.write(PacketBuilder.rpc(bot.playerIndex,'CheckPlayer',[]));
        }
    }
}
function relayPveBot(socket,line) {
    const m=line.match(/^e(\w+):(SSP|RST|RCR|SA_Robot),\((.*)\)$/);
    if (!m) return false;
    let args=require('./utils').parseArrayList(`(${m[3]})`);
    if (args.length===1 && Array.isArray(args[0])) args=args[0];
    const bot=state.activeBots[Number(args[2])];
    if (!bot?.isPveMonster) return false;
    if (!pveOwnsBot(socket,bot)) return true;
    const data=args.slice(0,2);
    if (m[2]==='SSP') {
        if(!Array.isArray(data[0]) || data[0].length!==3 || !data[0].every(Number.isFinite))return true;
        [bot.lastX,bot.lastY,bot.lastZ]=data[0];
    }
    const method={SSP:'SCP',RST:'RCT',RCR:'RCR',SA_Robot:'CA'}[m[2]];
    const packet=PacketBuilder.rpc(bot.playerIndex,method,[data]);
    pvePeers(bot.pveMatch).forEach(p=>{if(p!==socket)p.write(packet);});
    return true;
}
function removePveMonster(bot) {
    const match=bot.pveMatch;
    if (!match || !match.monsters[bot.playerIndex]) return;
    delete match.monsters[bot.playerIndex];
    delete state.activeBots[bot.playerIndex];
    pvePeers(match).forEach(p=>p.write(PacketBuilder.destroy(bot.playerIndex)));
    updatePveMonsterCount(match);
    if (!match.monsterNum) handlePveWaveCleared(match);
}
function leavePveMatch(socket) {
    const match=getPveMatch(socket);
    if (!match) return;
    match.players=match.players.filter(p=>p!==socket);
    pveBroadcast(match,19011,[socket.playerIndex]);
    for(const bot of Object.values(match.monsters)) {
        bot.readyClients.delete(socket);
        if(bot.authority===socket) {
            bot.authority=null;
            const next=pvePeers(match).find(p=>bot.readyClients.has(p));
            if(next)selectPveBotAuthority(next,bot.playerIndex);
        }
    }
    if (!match.players.length) endPveMatch(match);
}
function startPveMatch(socket,mapId) {
    const id=Number(mapId||socket.targetMapId), type=Number(db.MAP_CONFIG[id]?.type);
    if(![9,12].includes(type))return;
    let match=getPveMatch(socket);
    if(!match) {
        const maps=type===9 ? [5105,5106,5107,5108,5109,5110,5111,5112,5113,5114,5310,5311] : Array.from({length:12},(_,i)=>5115+i);
        const now=Date.now();
        match=sceneScope.tagEntity({mapId:id,pveType:type,pveLv:Math.max(0,maps.indexOf(id)),state:'ready',
            round:1,maxRound:3,monsterNum:0,monsters:{},players:[],startTime:now,readyAt:now+8000,deadline:now+1800000},socket);
        pveMatches.add(match);
        if(type===9)spawnPveCrystal(match);
        match.timer=setTimeout(()=>beginPveWave(match),8000);
        match.tickInterval=setInterval(()=>tickPveMatch(match),2000);
    }
    if(match.players.includes(socket))return match;
    match.players.push(socket);
    if(match.crystal)socket.write(crystalPacket(match.crystal));
    const users=pvePeers(match).flatMap(p=>[p.playerIndex,pveUser(p)]);
    pveMessage(socket,19001,[pveInfo(match),users]);
    pvePeers(match).forEach(p=>{if(p!==socket)pveMessage(p,19013,pveUser(socket));});
    return match;
}
function endPveMatch(match) {
    if(!match) { [...pveMatches].forEach(endPveMatch); return; }
    const finishedMatch = (match.state === 'victory' || match.state === 'defeat');
    const peers = pvePeers(match);
    clearTimeout(match.timer); clearInterval(match.tickInterval);
    for(const bot of Object.values(match.monsters)) {
        peers.forEach(p=>p.write(PacketBuilder.destroy(bot.playerIndex)));
        delete state.activeBots[bot.playerIndex];
    }
    if(match.crystal) {
        peers.forEach(p=>p.write(PacketBuilder.destroy(match.crystal.id)));
        delete state.spawnedNpcs[match.crystal.id];
    }
    if (finishedMatch) {
        peers.forEach(p => {
            if (!p.destroyed) {
                p.write(`e${encodeInt(Number(p.playerIndex || 1))}:SendMsgToClient,(${encodeInt(20001)},(0,'',0))\n`);
            }
        });
    }
    match.state='ended'; match.monsters={}; match.players=[];
    pveMatches.delete(match);
}


function spawnAdminRobot(player, createIndex, pos, def) {
    if(!def)return null;
    let match=[...pveMatches].find(m=>m.isAdmin&&sceneScope.sameScene(m,player));
    if(!match) {
        match=sceneScope.tagEntity({isAdmin:true,state:'fight',monsters:{},players:[],deadline:Infinity},player);
        pveMatches.add(match);match.tickInterval=setInterval(()=>tickPveMatch(match),2000);
    }
    match.players=state.activeSockets.filter(p=>sceneScope.canReceive(match,p));
    const id=pveId(),hp=db.getHeroMaxHp(def.heroId);
    const bot=sceneScope.tagEntity({playerIndex:id,playerId:String(id),playerName:'Admin-'+id,playerNameEnc:encodeHex(def.name),
        currentHeroId:def.heroId,createIndex,lastX:pos.x,lastY:pos.y,lastZ:pos.z,force:2,hp,maxHp:hp,
        isBot:true,isPveMonster:true,isAdminSpawn:true,inMatch:true,isDead:false,pveMatch:match,loadId:1,
        authority:null,readyClients:new Set(),write(){}},player);
    state.activeBots[id]=bot;match.monsters[id]=bot;
    pvePeers(match).forEach(p=>p.write(getPlayerSpawnPacket(bot,p)));
    return bot;
}

module.exports = {
    spawnAdminRobot,
    getPveMatch, leavePveMatch, selectPveBotAuthority, pveOwnsBot, relayPveBot,
    getFreeForAllMatch, prepareFreeForAll, startFreeForAll, tickFreeForAll, endFreeForAll, getPlayerSpawnCoordinates,
    sendEntityDamageUpdate,
    handleNpcHurt,
    applyBuffToEntity,
    cleanupBuff,
    sendEntityHpUpdate,
    sendSystemChat,
    sendGlobalSystemChat,
    getSafeMapId,
    getSpawnCoordinates,
    handlePlayerDeath,
    handleEntityDeath,
    respawnPlayer,
    getPlayerSpawnPacket,
    switchPlayerHero,
    getPlayersSnapshot,
    spawnTdmBases,
    createTdmMatchInstance,
    getTdmMatch,
    startTdmMatch,
    tickTdmMatch,
    incrementTdmScore,
    resetMatchStats,
    sendTabScoreboard,
    sendGameResult,
    endTdmMatch,
    startControlMatch,
    tickControlMatch,
    endControlMatch,
    startEscortMatch,
    tickEscortMatch,
    endEscortMatch,
    handleEscortCarPacket,
    getArenaMatch,
    startArenaMatch,
    tickArenaMatch,
    checkArenaRoundState,
    endArenaMatch,
    getPlayerNameFromNgol,
    spawnBot,
    spawnPracticeNpcs,
    spawnPracticeHeroBot,
    clearPracticeHeroBots,
    handleControlZonePlayersUpdate,
    handleControlZoneCaptureUpdate,
    handleControlZonePlayers5007,
    buildPveData,
    buildGeneData,
    startPveMatch,
    endPveMatch,
    spawnPveCrystal,
    spawnPveWave,
    updatePveMonsterCount,
    handlePveWaveCleared,
    sendPveVictory,
    tickPveMatch,
    cleanupBuff,
    removeBuffFromEntity: cleanupBuff
};
