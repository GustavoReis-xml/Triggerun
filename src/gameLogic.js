// Lógica de Partida, Buffs, Combate e HP do servidor Trigger Run

const state = require('./state');
const { encodeInt, decodeInt, encodeHex, decodeHex, Fun } = require('./utils');
const db = require('./database');
const dbPersistence = require('./dbPersistence');
const bots = require('../bots');
const logger = require('./logger');

// Envia HaveDamage para feedbacks ricos na UI/crosshair (hit markers / sons)
function sendEntityDamageUpdate(targetId, target, damage, attackerId, nPlace = 0, weaponId = 0, forceDir = null, forceMagnitude = 0) {
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
    const szDamageStr = `(${dmgEnc},(${fxEnc},${fyEnc},${fzEnc}),${forceEnc},${encodeInt(attackerId)},${encodeInt(targetId)},${encodeInt(nPlace)},${zero},${zero},${encodeInt(weaponId)},${zero},(${zero},${zero},${zero}),${zero},${encodeInt(1)},${encodeInt(weaponId)},${zero},(${zero},${baEnc}))`;
    const szHpStr = `(${hpEnc},${maxHpEnc},${shieldEnc},${maxShieldEnc},${zero},${zero},${zero},(${dmgEnc},${zero},${zero},${zero}),${encodeInt(2)},${zero})`;

    const damagePacket = `e${encodeInt(targetId)}:HaveDamage,(${szDamageStr},${szHpStr})\n`;
    state.activeSockets.forEach(s => {
        if (s.socketType === "SCENE" && !s.destroyed) {
            s.write(damagePacket);
        }
    });

    // Registra o último atacante e o timestamp do dano para atribuição de kills ambientais (quedas/abismo/espinhos)
    if (target && attackerId) {
        const attSocket = state.activeSockets.find(s => s.socketType === "SCENE" && !s.destroyed && (Number(s.playerId) === Number(attackerId) || Number(s.playerIndex) === Number(attackerId)));
        if (attSocket && Number(attSocket.force) !== Number(target.force)) {
            target.lastAttackerSocket = attSocket;
            target.lastAttackerTime = Date.now();
            target.lastAttackerWeapon = weaponId;
        } else if (state.activeBots && state.activeBots[attackerId]) {
            const botAtt = state.activeBots[attackerId];
            if (Number(botAtt.force) !== Number(target.force)) {
                target.lastAttackerSocket = botAtt;
                target.lastAttackerTime = Date.now();
                target.lastAttackerWeapon = weaponId;
            }
        }
    }
}

// Função para processar danos em NPCs
function handleNpcHurt(socket, npcId, damage, attackerId = 0, nPlace = 0, weaponId = 0) {
    const npc = state.spawnedNpcs[npcId];
    if (!npc) return;
    
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
            if (s.socketType === "SCENE" && !s.destroyed) {
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
            sendGlobalSystemChat("💥 [MODO AVENTURA] O CRISTAL FOI DESTRUÍDO! MISSÃO FALHOU!");
            setTimeout(() => {
                endPveMatch();
            }, 4000);
        } else if (npc.isPveMonster) {
            console.log(`[⚔️ PVE MONSTRO DERROTADO] Monstro PVE #${npcId} (${npc.name || 'Invasor'}) derrotado.`);
            delete state.spawnedNpcs[npcId];
            if (global.activePveMatch && global.activePveMatch.monsters && global.activePveMatch.monsters[npcId]) {
                delete global.activePveMatch.monsters[npcId];
                global.activePveMatch.monsterNum = Object.keys(global.activePveMatch.monsters).length;
                updatePveMonsterCount(global.activePveMatch);

                console.log(`[⚔️ PVE] Monstros restantes na onda: ${global.activePveMatch.monsterNum}`);
                if (global.activePveMatch.monsterNum === 0 && global.activePveMatch.state === 'fight') {
                    handlePveWaveCleared(global.activePveMatch);
                }
            }
            const destroyPacket = `d${encodeInt(npcId)}\n`;
            state.activeSockets.forEach(s => {
                if (s.socketType === "SCENE" && !s.destroyed) {
                    s.write(destroyPacket);
                }
            });
        } else {
            // Remove dos NPCs spawnados
            delete state.spawnedNpcs[npcId];
            const destroyPacket = `d${encodeInt(npcId)}\n`;
            state.activeSockets.forEach(s => {
                if (s.socketType === "SCENE" && !s.destroyed) {
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
                if (s.socketType === "SCENE" && !s.destroyed) {
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
        if (s.socketType === "SCENE" && !s.destroyed) {
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
        state.activeSockets.forEach(s => { if (s.socketType === "SCENE" && !s.destroyed) s.write(enterCocoonPacket); });
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
        if (s.socketType === "SCENE" && !s.destroyed) {
            s.write(delBuffPacket);
        }
    });
    console.log(`[🛡️ BUFF ENGINE] Expira/Remove Buff ${buffId} de ${isPlayer ? 'Jogador' : 'NPC'} ${targetId}`);

    // Se for o Buff de Casulo da Brigit expirando, garante reset visual
    if (buffId === 1015 && isPlayer) {
        target.isInvincible = false;
        target.godMode = false;
        if (target.currentHeroId === 1023) {
            const targetIdEnc = encodeInt(Number(targetId));
            const exitCocoonPacket = `e${encodeInt(targetId)}:CA,((('SkillE',0,0),1,${targetIdEnc}))\n`;
            state.activeSockets.forEach(s => {
                if (s.socketType === "SCENE" && !s.destroyed) {
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
        if (s.socketType === "SCENE" && !s.destroyed) {
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
    socket.write(`e${socket.playerIndex || 1}:SendMsgToClient,(${p1006},${sysNetMsg})\n`);
}

function sendGlobalSystemChat(text) {
    state.activeSockets.forEach(s => {
        if (s.socketType === "SCENE" && !s.destroyed) {
            sendSystemChat(s, text);
        }
    });
}

function getSafeMapId(mapId) {
    const id = parseInt(mapId);
    if (id === 5260) return 5202; 
    if (id === 5063) return 5022; 
    if (id === 5208) return 5030; 
    return id;
}

function getSpawnCoordinates(mapId, force) {
    const defaultCoords = { sx: 5000, sy: 2010, sz: 2413 };
    let targetMapId = Number(mapId);
    if (!targetMapId) return defaultCoords;
    
    if (targetMapId === 5260) targetMapId = 5049; 
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
    if (!socket || socket.destroyed) return;
    
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
                    if (s.socketType === "SCENE" && !s.destroyed) {
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
            if (s.socketType === "SCENE" && !s.destroyed) {
                s.write(deathAnim);
            }
        });

        if (global.activePveMatch && global.activePveMatch.monsters && global.activePveMatch.monsters[socket.playerIndex]) {
            delete global.activePveMatch.monsters[socket.playerIndex];
            if (state.activeBots) delete state.activeBots[socket.playerIndex];
            global.activePveMatch.monsterNum = Object.keys(global.activePveMatch.monsters).length;
            updatePveMonsterCount(global.activePveMatch);

            console.log(`[⚔️ PVE] Monstros restantes na onda: ${global.activePveMatch.monsterNum}`);
            if (global.activePveMatch.monsterNum === 0 && global.activePveMatch.state === 'fight') {
                handlePveWaveCleared(global.activePveMatch);
            }
        }
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
        const skillDoPacket = `e${playerId}:SkillDo,((${skillIdEnc},(${xEnc},${yEnc},${zEnc})),(${targetIdEnc}))\n`;
        state.activeSockets.forEach(s => {
            if (s.socketType === "SCENE" && !s.destroyed) {
                s.write(skillDoPacket);
            }
        });

        const enterCocoonPacket = `e${playerId}:CA,((('SkillE',0,1),1,${targetIdEnc}))\n`;
        state.activeSockets.forEach(s => {
            if (s.socketType === "SCENE" && !s.destroyed) {
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
        if (!recentAttacker.destroyed && Number(recentAttacker.force) !== Number(socket.force)) {
            attackerSocket = recentAttacker;
            isEnvironmentalKill = true;
        }
    }

    console.log(`[💀 PLAYER DEAD] O jogador ${socket.playerName || 'Jogador'} morreu! (${isEnvironmentalKill ? 'Atribuição Ambiental/Abismo' : (attackerSocket ? 'Morto por Inimigo' : 'Suicídio')}) Entrando em estado de morte por 10 segundos...\n`);
    socket.isDead = true;
    socket.hp = 0;
    socket.shield = 0;
    socket.maxShield = 0;
    socket.tagaloaShieldHp = 8000;
    socket.venusBarrierHp = 0;
    socket.venusBarrierMaxHp = 0;
    socket.venusBarrierActive = false;
    socket.lilyBarrierHp = 0;
    socket.lilyBarrierMaxHp = 0;
    socket.lilyBarrierActive = false;
    socket.isInvincible = false;
    socket.godMode = false;
    
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
    const warDataTarget = `(${encodeInt(socket.matchKills || 0)},${encodeInt(socket.matchDeaths || 0)},${encodeInt(socket.matchDamage || 0)})`;
    const warDataAttacker = `(${encodeInt(attackerSocket ? (attackerSocket.matchKills || 0) : 0)},${encodeInt(attackerSocket ? (attackerSocket.matchDeaths || 0) : 0)},${encodeInt(attackerSocket ? (attackerSocket.matchDamage || 0) : 0)})`;
    const warDataParam = `(${warDataTarget},${warDataAttacker})`;

    const weaponIdEnc = encodeInt(socket.lastAttackerWeapon || 1000);
    const deadPacket = `e${targetPlayerIdEnc}:OnDeath,(${attackerPlayerIdEnc},${p10},(${encodeInt(fx)},${encodeInt(fy)},${encodeInt(fz)}),${encodeInt(fmax)},${weaponIdEnc},${zero},${zero},(),${zero},${warDataParam})\n`;
    
    state.activeSockets.forEach(s => {
        if (s.socketType === "SCENE" && !s.destroyed) {
            s.write(deadPacket);
            sendTabScoreboard(s);
        }
    });
    
    const sysMsgHex = Buffer.from("[Sistema] Você morreu! Aguardando ressurreição ou renascendo em 10 segundos...").toString('hex');
    const sysNetMsg = `(${encodeInt(8)},'','[mi]${sysMsgHex}[/mi]','',(),(),${encodeInt(1)},('','',0),())`;
    socket.write(`e${socket.playerIndex || 1}:SendMsgToClient,(${encodeInt(1006)},${sysNetMsg})\n`);

    if (attackerSocket) {
        if (isEnvironmentalKill) {
            sendGlobalSystemChat(`💀 [Combate] ${attackerSocket.playerName || 'Inimigo'} derrubou ${socket.playerName || 'Jogador'} no abismo / perigos do mapa!`);
        } else {
            sendGlobalSystemChat(`💀 [Combate] ${attackerSocket.playerName || 'Inimigo'} derrotou ${socket.playerName || 'Jogador'}!`);
        }
        
        if (global.activeTdmMatch && global.activeTdmMatch.state === 'fight') {
            const killerForce = Number(attackerSocket.force);
            if (killerForce === 1 || killerForce === 2) {
                incrementTdmScore(killerForce, 1);
            }
        }
    } else {
        // Suicídio puro (se jogou no abismo ou nos espinhos sem combate recente)
        sendGlobalSystemChat(`💀 [Combate] ${socket.playerName || 'Jogador'} se suicidou no mapa!`);
        
        // REGRA COMPETITIVA DE TDM: Suicídio não nega ponto para o time adversário! O time inimigo recebe o ponto da eliminação.
        if (global.activeTdmMatch && global.activeTdmMatch.state === 'fight') {
            const victimForce = Number(socket.force);
            const enemyForce = victimForce === 1 ? 2 : 1;
            incrementTdmScore(enemyForce, 1);
            sendGlobalSystemChat(`🚩 [TDM] +1 ponto para a Equipe ${enemyForce === 1 ? 'Azul' : 'Vermelha'} por suicídio do adversário!`);
        }
    }

    // Limpa o registro de último atacante para o próximo ciclo de vida
    socket.lastAttackerSocket = null;
    socket.lastAttackerTime = 0;

    if (socket.respawnTimeout) {
        clearTimeout(socket.respawnTimeout);
    }
    socket.respawnTimeout = setTimeout(() => {
        if (!socket || socket.destroyed) return;
        if (socket.isDead) {
            respawnPlayer(socket);
        }
    }, 10000);
}

function respawnPlayer(socket) {
    if (!socket || socket.destroyed) return;
    
    socket.isDead = false;
    if (socket.respawnTimeout) {
        clearTimeout(socket.respawnTimeout);
        socket.respawnTimeout = null;
    }
    
    socket.hp = socket.maxHp || 2000;
    socket.shield = 0;
    socket.maxShield = 0;
    socket.tagaloaShieldHp = 8000;
    socket.venusBarrierHp = 0;
    socket.venusBarrierMaxHp = 0;
    socket.venusBarrierActive = false;
    socket.lilyBarrierHp = 0;
    socket.lilyBarrierMaxHp = 0;
    socket.lilyBarrierActive = false;
    socket.isInvincible = false;
    socket.godMode = false;
    
    const currentMapId = socket.targetMapId || state.getLastMapId() || 5202;
    const coords = getSpawnCoordinates(currentMapId, socket.force);
    let sx = coords.sx, sy = coords.sy, sz = coords.sz;
    
    socket.lastX = sx;
    socket.lastY = sy;
    socket.lastZ = sz;
    
    // Envia OnRevive para limpar o estado de morte e fechar a UI de morte
    const targetPlayerIdEnc = encodeInt(Number(socket.playerId || 1));
    const sxEnc = encodeInt(Math.round(sx));
    const syEnc = encodeInt(Math.round(sy));
    const szEnc = encodeInt(Math.round(sz));
    const revivePacket = `e${targetPlayerIdEnc}:OnRevive,((${sxEnc},${szEnc},${syEnc},${encodeInt(180)},0,0,''))\n`;
    state.activeSockets.forEach(s => {
        if (s.socketType === "SCENE" && !s.destroyed) {
            s.write(revivePacket);
        }
    });

    sendEntityHpUpdate(socket, socket.playerId || "1", socket, true);
    
    state.activeSockets.forEach(s => {
        if (s.socketType === "SCENE" && !s.destroyed) {
            s.bypassInterceptor = true;
            s.write(getPlayerSpawnPacket(socket, s));
            s.bypassInterceptor = false;
        }
    });
    
    const sysMsgHex = Buffer.from("[Sistema] Você renasceu na base da sua equipe!").toString('hex');
    const sysNetMsg = `(${encodeInt(8)},'','[mi]${sysMsgHex}[/mi]','',(),(),${encodeInt(1)},('','',0),())`;
    socket.write(`e${socket.playerIndex || 1}:SendMsgToClient,(${encodeInt(1006)},${sysNetMsg})\n`);
    
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
    const heroId = s.currentHeroId || 1015;
    const actualHeroId = (heroId === 1018 || heroId === 1019) ? 1015 : heroId;
    const heroIdEnc = encodeInt(actualHeroId);
    const one = encodeInt(1);
    const zero = encodeInt(0);
    const forceEnc = encodeInt(s.force || 1);
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
    
    const pIdx = s.playerIndex || 1;
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
    const battleAttr = `(${one},${maxHpEnc},${maxHpEnc},${one},${one},${one},${one},${one},${one},${encodeInt(10000)},${one},${one},${one},${one},${one},${one},${one},${one},${one},${one})`;
    
    const currentMapId = s.targetMapId || state.getLastMapId() || 5202;
    const defaultCoords = getSpawnCoordinates(currentMapId, s.force);
    let sx = s.lastX || defaultCoords.sx;
    let sy = s.lastY || defaultCoords.sy;
    let sz = s.lastZ || defaultCoords.sz;
    const scnInfo = `(${encodeInt(getSafeMapId(currentMapId))},${encodeInt(sx)},${encodeInt(sz)},${encodeInt(sy)},${zero},${zero},${zero},${encodeInt(180)})`;
    const geneDataEnc = '()';
    const msg_part1 = `(${baseInfo},${battleAttr},scnInfo,(),(),('Local',${one},${one},${one},${one},${zero},'',${one}),(),${geneDataEnc})`;
    
    const finalMsgPart1 = msg_part1.replace('scnInfo', scnInfo);
    
    const arySkillServerData = db.generateHeroSkillsData(actualHeroId);
    const heroDazhaoData = db.generateHeroDazhaoData(actualHeroId);
    
    const maxAmmo = db.weaponsMap[actualHeroId] && db.weaponsMap[actualHeroId].cartridgePublic !== undefined ? db.weaponsMap[actualHeroId].cartridgePublic : 30;
    const superData = (actualHeroId === 1005) ? `(${encodeInt(s.force || 1)},'',${zero},(${encodeInt(7000)}))` : `()`;
    const msg_part2 = `(${one},zero,((0),(),(${encodeInt(maxAmmo)})),${encodeInt(actualHeroId)},${heroDazhaoData},${arySkillServerData},${superData},())`.replace('zero', zero);
    
    let packet = '';
    if (receiverSocket && typeof receiverSocket === 'object' && s.playerName === receiverSocket.playerName) {
        packet = `e${pIdx}:SendMsgToClient,(${encodeInt(10001)},(${finalMsgPart1},${msg_part2}))\n`;
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
        packet += `e${pIdx}:SendMsgToClient,(${p5000},(${p5025},(${userStats},${heroStats},${rankList})))\n`;
    } else {
        if (s.isBot) {
            const xEnc = encodeInt(Math.round(sx));
            const yEnc = encodeInt(Math.round(sy));
            const zEnc = encodeInt(Math.round(sz));
            const rxEnc = zero;
            const ryEnc = encodeInt(180);
            const rzEnc = zero;
            const createIndex = getValidRobotCreateIndex(actualHeroId, currentMapId);
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
        packet += `e${pIdx}:NVM,((${vIdEnc}))\n`;
    }
    return packet;
}

function switchPlayerHero(socket, selectedHeroId) {
    if (!socket || socket.destroyed) return;
    socket.visualHeroId = null;
    try {
        if (socket.waitingForHeroLock) {
            console.log(`[🎭 SCENE HERO LOCK] Jogador ${socket.playerName || 'Jogador'} confirmou o herói inicial: ${selectedHeroId}. Iniciando carregamento de mapa...`);
            
            socket.waitingForHeroLock = false;
            socket.currentHeroId = selectedHeroId;
            socket.hasConfirmedHero = true;

            // Reset estatísticas da partida
            resetMatchStats(socket);
            
            // Sincroniza herói selecionado na sessão persistente e no lobby socket
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
            socket.isInvincible = socket.godMode || false;
            if (selectedHeroId === 1006) {
                socket.tagaloaShieldHp = 8000;
            }
            
            // Envia o pacote de loading screen (s90004) antes de iniciar a transição de mapa
            const p20014 = encodeInt(20014);
            const s90004 = encodeInt(90004);
            socket.write(`e${socket.playerIndex || 1}:SendMsgToClient,(${p20014},(${s90004}))\n`);
            
            // 1. Envia 12002 para carregar o novo herói na memória do cliente
            const prefix = `e${socket.playerIndex || 1}`;
            const p1306 = encodeInt(1306);
            const p12002 = encodeInt(12002);
            const heroIdEnc = encodeInt(selectedHeroId);
            const profile = dbPersistence.loadPlayer(socket.playerName);
            const playerEq = state.playerEquipment[socket.playerName] || (profile && profile.equipment) || {};
            const eq = playerEq[selectedHeroId] || { skin: 1, weapon: 1, emote: 1, pose: 0 };
            const heroInfo = db.generateFullHeroData(selectedHeroId, eq.skin, eq.weapon, eq.emote, eq.pose, profile);
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
            setTimeout(() => {
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
        socket.isInvincible = socket.godMode || false;
        socket.isDead = false;
        if (selectedHeroId === 1006) {
            socket.tagaloaShieldHp = 8000;
        }
        if (socket.respawnTimeout) {
            clearTimeout(socket.respawnTimeout);
            socket.respawnTimeout = null;
        }

        // Ao trocar de herói em jogo, resetar coordenadas para a base de spawn correspondente
        const resetMapId = socket.targetMapId || state.getLastMapId() || 5202;
        const coords = getSpawnCoordinates(resetMapId, socket.force);
        socket.lastX = coords.sx;
        socket.lastY = coords.sy;
        socket.lastZ = coords.sz;

        // 2. Envia 10001 (Spawn/Update do Herói com a nova classe/habilidades)
        state.activeSockets.forEach(s => {
            if (s.socketType === "SCENE" && !s.destroyed) {
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

function initializeTdmClient(socket) {
    if (socket.tdmInitialized) return;
    
    const match = global.activeTdmMatch;
    if (!match) return;

    socket.tdmInitialized = true;
    const currentMapId = socket.targetMapId || state.getLastMapId() || 5202;

    const p110300 = encodeInt(110300);
    const p110301 = encodeInt(110301);
    const p110307 = encodeInt(110307);
    
    socket.write(`e${socket.playerIndex || 1}:SendMsgToClient,(${p110300},(${p110301},()))\n`);
    socket.write(`e${socket.playerIndex || 1}:SendMsgToClient,(${p110300},(${p110307},(${encodeInt(match.targetScore)},${encodeInt(match.blueScore)},${encodeInt(match.redScore)})))\n`);

    spawnTdmBases(socket, currentMapId);

    if (match.state === 'fight') {
        const p110303 = encodeInt(110303);
        const p1030000 = encodeInt(1030000);
        socket.write(`e${socket.playerIndex || 1}:SendMsgToClient,(${p110300},(${p110303},()))\n`);
        socket.write(`e${socket.playerIndex || 1}:SendMsgToClient,(${p1030000},(${encodeInt(1)},'close_open'))\n`);
        setTimeout(() => {
            if (!socket.destroyed) {
                socket.write(`e${socket.playerIndex || 1}:SendMsgToClient,(${p110300},(${p110303},()))\n`);
                socket.write(`e${socket.playerIndex || 1}:SendMsgToClient,(${p1030000},(${encodeInt(1)},'close_open'))\n`);
            }
        }, 500);
    }
}

function startTdmMatch(socket) {
    const currentMapId = socket.targetMapId || state.getLastMapId() || 5202;
    console.log(`[🏆 TDM] startTdmMatch chamado para ${socket.playerName || 'Jogador'} no mapa ${currentMapId}`);

    if (!global.activeTdmMatch) {
        global.activeTdmMatch = {
            state: 'warmup',
            warmupSeconds: 15,
            fightSeconds: 900, 
            targetScore: 50,   
            blueScore: 0,
            redScore: 0,
            players: [],
            timer: null,
            botInterval: null
        };
        console.log(`[🏆 TDM] Nova sessão TDM inicializada para o mapa ${currentMapId} (Duração: 15 min / 900s, Alvo: 50 pontos).`);
        state.activeBots = {};
    }

    const match = global.activeTdmMatch;
    if (!match.players.includes(socket)) {
        match.players.push(socket);
        console.log(`[🏆 TDM] Jogador ${socket.playerName || 'Jogador'} registrado na partida TDM.`);
    }

    initializeTdmClient(socket);

    if (!match.timer) {
        console.log(`[🏆 TDM] Iniciando timer de atualização periódica...`);
        match.timer = setInterval(() => {
            tickTdmMatch();
        }, 1000);
    }
}

function tickTdmMatch() {
    const match = global.activeTdmMatch;
    if (!match) return;

    const p110300 = encodeInt(110300);
    const p110305 = encodeInt(110305);
    const p110303 = encodeInt(110303);

    match.players = match.players.filter(p => !p.destroyed);
    if (match.players.length === 0) {
        console.log(`[🏆 TDM] Nenhum jogador na partida. Encerrando sessão TDM.`);
        if (match.timer) clearInterval(match.timer);
        global.activeTdmMatch = null;
        return;
    }

    if (match.state === 'warmup') {
        match.warmupSeconds--;
        
        match.players.forEach(p => {
            p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p110300},(${p110305},(${encodeInt(match.warmupSeconds)},${encodeInt(0)},${encodeInt(0)},'')))\n`);
        });

        if (match.warmupSeconds <= 0) {
            match.state = 'fight';
            console.log(`[🏆 TDM] Luta Iniciada! Abrindo portas.`);
            
            const p1030000 = encodeInt(1030000);
            match.players.forEach(p => {
                p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p110300},(${p110303},()))\n`);
                p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p1030000},(${encodeInt(1)},'close_open'))\n`);
            });
            
            sendGlobalSystemChat("🟢 [TDM] A PARTIDA COMEÇOU! AS PORTAS DA BASE FORAM ABERTAS!");
        }
    } else if (match.state === 'fight') {
        match.fightSeconds--;

        if (!match.botInterval) {
            console.log(`[🏆 TDM] Bots controlados nativamente via Unity NavMesh (Opcode 4010001/4010002).`);
            match.botInterval = true;
        }

        match.players.forEach(p => {
            p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p110300},(${p110305},(${encodeInt(match.fightSeconds)},${encodeInt(1)},${encodeInt(0)},'')))\n`);
        });

        if (match.fightSeconds <= 0) {
            console.log(`[🏆 TDM] Tempo limite esgotado!`);
            endTdmMatch();
        }
    }
}

function incrementTdmScore(killerForce, amount = 1) {
    const match = global.activeTdmMatch;
    if (!match || match.state !== 'fight') return;

    if (killerForce === 1) {
        match.blueScore += amount;
    } else if (killerForce === 2) {
        match.redScore += amount;
    }

    console.log(`[🏆 TDM] Placar Atualizado: Blue ${match.blueScore} - Red ${match.redScore} (Alvo: ${match.targetScore})`);

    const p110300 = encodeInt(110300);
    const p110302 = encodeInt(110302);
    match.players.forEach(p => {
        p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p110300},(${p110302},(${encodeInt(1)},${encodeInt(match.targetScore)},${encodeInt(match.blueScore)},${encodeInt(match.redScore)})))\n`);
        // Atualiza o Tab (5025) de cada jogador com seus stats atuais
        sendTabScoreboard(p);
    });

    if (match.blueScore >= match.targetScore || match.redScore >= match.targetScore) {
        console.log(`[🏆 TDM] Equipe atingiu a pontuação máxima! Encerrando...`);
        endTdmMatch();
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
    const allPlayers = (state.activeSockets || []).filter(s => s.socketType === "SCENE" && !s.destroyed);
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
    socket.write(`e${socket.playerIndex || 1}:SendMsgToClient,(${p5000},(${p5025},${tabData}))\n`);
}

// ─── END-GAME RESULT SCREEN (5026) ───────────────────────────────────────────
function sendGameResult(players, winnerForce) {
    if (!players || players.length === 0) return;
    const p5000 = encodeInt(5000);
    const p5026 = encodeInt(5026);
    const zero  = encodeInt(0);

    // Determina MVP (quem tem mais kills, ou maior dano em caso de empate)
    let mvpSocket = players.reduce((best, p) => {
        const pKills = p.matchKills || 0;
        const bKills = best.matchKills || 0;
        if (pKills > bKills) return p;
        if (pKills === bKills && (p.matchDamage || 0) > (best.matchDamage || 0)) return p;
        return best;
    }, players[0]);

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
        } else if (p.force === winnerForce) {
            nResult = 1;
        } else {
            nFail = 1;
        }

        const isWin   = (nResult === 1);
        const isDraw  = (nDraw === 1);
        const isMvp   = (p === mvpSocket) ? 1 : 0;
        const mvpKill = encodeInt(nResult);
        const ace     = encodeInt(nDraw);
        const mvpAst  = encodeInt(nFail);
        const unk8    = zero;
        const showMvp = encodeInt(isMvp);
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

                // Envia atualização de EXP e Level em tempo real (21002) para o Lobby
                if (global.lastLobbySocket && !global.lastLobbySocket.destroyed) {
                    global.lastLobbySocket.write(`e1:SendMsgToHallClient,(${encodeInt(21002)},(0,0,${encodeInt(res.newExp)},${encodeInt(res.newLevel)},0))\n`);
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
    const resultHeroList = `()`; // lista de heróis

    const szNetMsg = `(${resultUserList},${resultHeroList})`;

    players.forEach(p => {
        if (!p.destroyed) {
            p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p5000},(${p5026},${szNetMsg}))\n`);
        }
    });
    console.log(`[🏆 RESULT] Tela de resultado 5026 enviada para ${players.length} jogador(es). Vencedor: força ${winnerForce}`);
}

function endTdmMatch() {
    const match = global.activeTdmMatch;
    if (!match || match.state === 'ended') return;

    match.state = 'ended';
    if (match.timer) {
        clearInterval(match.timer);
        match.timer = null;
    }

    if (match.botInterval) {
        clearInterval(match.botInterval);
        match.botInterval = null;
        console.log(`[🏆 TDM] Interval de IA dos bots cancelado.`);
    }
    state.activeBots = {};

    let winnerForce = 0;
    if (match.blueScore > match.redScore) {
        winnerForce = 1;
        sendGlobalSystemChat("🏆 [TDM] FIM DE PARTIDA! A EQUIPE AZUL VENCEU!");
    } else if (match.redScore > match.blueScore) {
        winnerForce = 2;
        sendGlobalSystemChat("🏆 [TDM] FIM DE PARTIDA! A EQUIPE VERMELHA VENCEU!");
    } else {
        winnerForce = 0;
        sendGlobalSystemChat("🤝 [TDM] FIM DE PARTIDA! A PARTIDA TERMINOU EM EMPATE!");
    }

    const p110300 = encodeInt(110300);
    const p110302 = encodeInt(110302);
    const p110306 = encodeInt(110306);
    const p110304 = encodeInt(110304);

    match.players.forEach(p => {
        p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p110300},(${p110302},(${encodeInt(0)},${encodeInt(match.targetScore)},${encodeInt(match.blueScore)},${encodeInt(match.redScore)})))\n`);
        p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p110300},(${p110306},(${encodeInt(winnerForce)})))\n`);
    });

    // Envia tela de resultado (5026) após 3s para dar tempo do 110306 aparecer
    setTimeout(() => sendGameResult(match.players, winnerForce), 3000);

    setTimeout(() => {
        match.players.forEach(p => {
            if (!p.destroyed) {
                p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p110300},(${p110304},()))\n`);
                p.inMatch = false;
                p.tdmInitialized = false;
                
                const lobbyMatch = state.activeSockets.find(s => s.socketType === "LOBBY" && s.remoteAddress === p.remoteAddress);
                if (lobbyMatch) {
                    lobbyMatch.inMatch = false;
                }
            }
        });
        global.activeTdmMatch = null;
        console.log(`[🏆 TDM] Partida limpa com sucesso.`);
    }, 10000);
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
    5055: { id: 231, x: 117150, y: 115175, z: 78675 } // Underground Palace
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
    socket.write(`e${socket.playerIndex || 1}:SendMsgToClient,(${p40001},(${netIdEnc}))\n`);
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

function initializeControlClient(socket) {
    if (socket.controlInitialized) return;
    
    const match = global.activeControlMatch;
    if (!match) return;

    socket.controlInitialized = true;
    const currentMapId = socket.targetMapId || state.getLastMapId() || 5202;

    const p110100 = encodeInt(110100);
    const p110101 = encodeInt(110101); 
    const p110104 = encodeInt(110104); 
    const p110106 = encodeInt(110106); 

    socket.write(`e${socket.playerIndex || 1}:SendMsgToClient,(${p110100},(${p110101},()))\n`);
    socket.write(`e${socket.playerIndex || 1}:SendMsgToClient,(${p110100},(${p110106},()))\n`);
    socket.write(`e${socket.playerIndex || 1}:SendMsgToClient,(${p110100},(${p110104},(${encodeInt(match.blueScore)},${encodeInt(match.redScore)})))\n`);

    spawnTdmBases(socket, currentMapId);
    spawnControlPoint(socket, currentMapId);

    // Inicializa a janela ZhanDian (HUD) do modo Control no cliente
    const p5000 = encodeInt(5000);
    const subCmd5001 = encodeInt(5001);
    const stateVal = match.state === 'warmup' ? 0 : 1;
    const arrayList = [
        encodeInt(stateVal),                    // 0: m_nGameState
        encodeInt(match.warmupSeconds),         // 1: warmup time
        encodeInt(match.fightSeconds),          // 2: fight time
        encodeInt(0),                           // 3
        encodeInt(0),                           // 4
        encodeInt(0),                           // 5: owningForce
        encodeInt(0),                           // 6: occupyForce
        encodeInt(0),                           // 7: bluePercentage
        encodeInt(0),                           // 8: redPercentage
        encodeInt(0),                           // 9: blue rounds won
        encodeInt(0),                           // 10: red rounds won
        encodeInt(0),                           // 11: attnum (InitScrollRob blue percentage)
        encodeInt(0),                           // 12: defnum (InitScrollRob red percentage)
        encodeInt(0),                           // 13: m_nLockTime
        encodeInt(0),                           // 14: att wins
        encodeInt(0),                           // 15: def wins
        encodeInt(0),                           // 16: blueInZone
        encodeInt(0),                           // 17: redInZone
        encodeInt(1),                           // 18: nIsPlaySound
        encodeInt(1),                           // 19: nIsChangeHero
        `'${Fun.GetServerDate()}'`,             // 20: sStartChangeTime
        encodeInt(0),                           // 21
        encodeInt(0),                           // 22
        encodeInt(0)                            // 23: general door handle
    ];
    socket.write(`e${socket.playerIndex || 1}:SendMsgToClient,(${p5000},(${subCmd5001},((${arrayList.join(',')}),0,0))))\n`);
    console.log(`[🏆 CONTROL HUD INIT] Enviado subcomando 5001 para inicializar o HUD ZhanDian do jogador ${socket.playerIndex}`);

    if (match.state === 'fight') {
        const p110107 = encodeInt(110107); 
        const p1030000 = encodeInt(1030000);
        socket.write(`e${socket.playerIndex || 1}:SendMsgToClient,(${p110100},(${p110107},()))\n`);
        socket.write(`e${socket.playerIndex || 1}:SendMsgToClient,(${p1030000},(${encodeInt(1)},'close_open'))\n`);
    }
}

function startControlMatch(socket) {
    const currentMapId = socket.targetMapId || state.getLastMapId() || 5202;
    console.log(`[🏆 CONTROL] startControlMatch chamado para ${socket.playerName || 'Jogador'} no mapa ${currentMapId}`);

    if (!global.activeControlMatch) {
        global.activeControlMatch = {
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
            p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p110100},(${p110102},(${encodeInt(match.warmupSeconds)},${encodeInt(0)},${encodeInt(0)},'')))\n`);
        });

        if (match.warmupSeconds <= 0) {
            match.state = 'fight';
            console.log(`[🏆 CONTROL] Luta Iniciada! Abrindo portas.`);
            
            const p110107 = encodeInt(110107); 
            const p1030000 = encodeInt(1030000);
            match.players.forEach(p => {
                p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p110100},(${p110107},()))\n`);
                p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p1030000},(${encodeInt(1)},'close_open'))\n`);
            });

            // Transiciona o HUD ZhanDian para o estado de luta (subcomando 5002)
            const p5000 = encodeInt(5000);
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
                p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p5000},(${subCmd5002},(${startArray.join(',')}))))\n`);
            });
            console.log(`[🏆 CONTROL HUD START] Enviado subcomando 5002 para transição para o modo combate.`);
            
            sendGlobalSystemChat("🟢 [CONTROL] A PARTIDA COMEÇOU! AS PORTAS DA BASE FORAM ABERTAS!");
        }
    } else if (match.state === 'fight') {
        match.fightSeconds--;

        if (!match.botInterval) {
            console.log(`[🏆 CONTROL] Bots controlados nativamente via Unity NavMesh (Opcode 4010001/4010002).`);
            match.botInterval = true;
        }

        // Baseado em blueInZone/redInZone de match.players, precisamos também contar bots que estão na zona.
        // Vamos atualizar blueInZone/redInZone para incluir os bots que estão na zona de controle (x: 4400 a 7200, z: 4100 a 6500)
        let botBlueInZone = 0;
        let botRedInZone = 0;
        if (state.activeBots) {
            for (const botId in state.activeBots) {
                const bot = state.activeBots[botId];
                if (bot && !bot.destroyed && bot.hp > 0) {
                    // Verifica se o bot está dentro do retângulo da zona (mesmos limites aproximados)
                    if (bot.lastX >= 4400 && bot.lastX <= 7200 && bot.lastZ >= 4100 && bot.lastZ <= 6500) {
                        if (bot.force === 1) botBlueInZone++;
                        else if (bot.force === 2) botRedInZone++;
                    }
                }
            }
        }
        
        // Atualiza a barra de progresso do ponto de controle
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

                        // Envia 5019 para exibir visual de captura e tocar som
                        const p5000 = encodeInt(5000);
                        const subCmd5019 = encodeInt(5019);
                        match.players.forEach(p => {
                            p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p5000},(${subCmd5019},(${encodeInt(1)})))\n`);
                        });
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

                        // Envia 5019 para exibir visual de captura e tocar som
                        const p5000 = encodeInt(5000);
                        const subCmd5019 = encodeInt(5019);
                        match.players.forEach(p => {
                            p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p5000},(${subCmd5019},(${encodeInt(2)})))\n`);
                        });
                    }
                }
            } else {
                match.progress = 210;
            }
        } else if (blueInZone > 0 && redInZone > 0) {
            // Contestado: Para o progresso
            ntype = 0;
            nSpeed = 0;
        } else {
            // Ninguém na zona: Drena o progresso se não estiver totalmente capturado
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

        // Envia atualização de timer/countdown
        match.players.forEach(p => {
            p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p110100},(${p110102},(${encodeInt(match.fightSeconds)},${encodeInt(1)},${encodeInt(0)},'')))\n`);
        });

        // Envia os placares e progresso atualizados via subcomando 5008
        const p5000 = encodeInt(5000);
        const subCmd5008 = encodeInt(5008);
        
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
        
        match.players.forEach(p => {
            p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p5000},(${subCmd5008},(${updateArray.join(',')}))))\n`);
        });

        // Envia sincronização de captura (case 5022) se houver progresso ativo (ou drenando/parado)
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
            p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p5000},(${subCmd5022},(${syncProgressArray.join(',')}))))\n`);
        });

        // Toca som de contagem regressiva se atingir 85% de porcentagem de vitória de qualquer time
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
                p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p5000},(${subCmd5027},(${countdownArray.join(',')}))))\n`);
            });
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

    match.players.forEach(p => {
        p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p110100},(${p110108},(${encodeInt(winnerForce)})))\n`);
    });

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
        "''",                                   // 12
        encodeInt(0),                           // 13
        encodeInt(winnerForce === 1 ? 2 : 0),   // 14: m_nAttVinnerNum
        encodeInt(winnerForce === 2 ? 2 : 0),   // 15: m_nDefVinnerNum
        encodeInt(0),                           // 16
        encodeInt(0),                           // 17
        encodeInt(1),                           // 18
        encodeInt(1),                           // 19
        `'${Fun.GetServerDate()}'`,             // 20
        "()",                                   // 21: szResult
        encodeInt(winnerForce),                 // 22: m_nNowWin
        encodeInt(0)                            // 23
    ];
    match.players.forEach(p => {
        p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p5000},(${subCmd5006},(${endArray.join(',')}))))\n`);
    });
    console.log(`[🏆 CONTROL HUD END] Enviado subcomando 5006 para encerrar o HUD ZhanDian.`);

    // Envia tela de resultado (5026) após 3s
    setTimeout(() => sendGameResult(match.players, winnerForce), 3000);

    setTimeout(() => {
        match.players.forEach(p => {
            if (!p.destroyed) {
                p.write(`e${p.playerIndex || 1}:SendMsgToClient,(${p110100},(${p110109},()))\n`);
                p.inMatch = false;
                p.controlInitialized = false;
                
                const lobbyMatch = state.activeSockets.find(s => s.socketType === "LOBBY" && s.remoteAddress === p.remoteAddress);
                if (lobbyMatch) {
                    lobbyMatch.inMatch = false;
                }
            }
        });
        global.activeControlMatch = null;
        console.log(`[🏆 CONTROL] Partida limpa com sucesso.`);
    }, 10000);
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

        const spawnPacket = `e${botId}:SendMsgToClient,(${encodeInt(10001)},(${msg_part1},${msg_part2}))\n// `;
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
    state.spawnedNpcs[2001] = { id: 2001, index: 1121, hp: 10, maxHp: 10, x: 50000, y: 20104, z: 26000, force: dummyForce, isPracticeDummy: true };
    state.spawnedNpcs[2002] = { id: 2002, index: 1122, hp: 1000, maxHp: 1000, x: 50500, y: 20104, z: 26000, force: dummyForce, isPracticeDummy: true };
    state.spawnedNpcs[2003] = { id: 2003, index: 1101, hp: 1500, maxHp: 1500, x: 49500, y: 20104, z: 26000, force: dummyForce, isPracticeDummy: true };
    
    for (const key in state.spawnedNpcs) {
        const npc = state.spawnedNpcs[key];
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
        while (state.activeBots && state.activeBots[dummyIndex]) {
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
        
        const botPacket = getPlayerSpawnPacket(dummyBot, initiatorSocket);
        state.activeSockets.forEach(s => {
            if (s.socketType === "SCENE" && !s.destroyed) {
                s.write(botPacket);
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

function spawnPveCrystal(match) {
    const crystalX = 11665;
    const crystalY = 10776;
    const crystalZ = 2461;
    const crystalMaxHp = 10000;
    
    state.spawnedNpcs[1130] = {
        id: 1130,
        index: 1130,
        hp: crystalMaxHp,
        maxHp: crystalMaxHp,
        x: crystalX,
        y: crystalY,
        z: crystalZ,
        force: 1,
        isCrystal: true
    };
    
    const className = db.getEntityClassName(1130);
    const crystalPacket = `c${className}:${encodeInt(1130)},("Initialize",(${encodeInt(1)},${encodeInt(1130)},${encodeInt(1130)},${encodeInt(1)},(${encodeInt(crystalX)},${encodeInt(crystalY)},${encodeInt(crystalZ)}),(0,0,0),(${encodeInt(150)},${encodeInt(150)},${encodeInt(150)}),(0,0,0),${encodeInt(-1)},${encodeInt(crystalMaxHp)},${encodeInt(0)},${encodeInt(1)},${encodeInt(0)},""),((),()),0)\n`;
    
    if (match && match.players) {
        match.crystal = state.spawnedNpcs[1130];
        match.players.forEach(p => {
            if (!p.destroyed) p.write(crystalPacket);
        });
    }
    console.log(`[🏰 PVE CRISTAL] Cristal central (NPC 1130) spawnado em (${crystalX}, ${crystalY}, ${crystalZ}) com ${crystalMaxHp} HP via ${className}.`);
}

function spawnPveWave(match, round) {
    if (!match) return;
    match.round = round;
    match.monsters = {};

    const pveLv = match.pveLv || 0;
    const floorY = 10776;
    // Spawns nos túneis da caverna marchando em direção ao cristal central (z: 2461)
    const spawnPoints = [
        { x: 11665, y: floorY, z: 1200 },
        { x: 11200, y: floorY, z: 1000 },
        { x: 12100, y: floorY, z: 1000 },
        { x: 11665, y: floorY, z: 500 },
        { x: 10800, y: floorY, z: 0 },
        { x: 12500, y: floorY, z: 0 },
        { x: 11665, y: floorY, z: -500 },
        { x: 11200, y: floorY, z: -1000 },
        { x: 12100, y: floorY, z: -1000 },
        { x: 10500, y: floorY, z: -1500 },
        { x: 12800, y: floorY, z: -1500 },
        { x: 11665, y: floorY, z: -2000 }
    ];

    let monsterDefs = [];
    if (round === 1) {
        // Onda 1: 8 Mutantes Bípedes (Index 1105, monsterbq_tps)
        for (let i = 0; i < 8; i++) {
            monsterDefs.push({
                name: `Mutante #${i + 1}`,
                npcIndex: 1105,
                scale: 150,
                hp: 750 + pveLv * 150,
                damage: 30 + pveLv * 5,
                isBoss: false
            });
        }
        sendGlobalSystemChat("⚔️ [MODO AVENTURA] ONDA 1 INICIADA! DEFENDA O CRISTAL!");
    } else if (round === 2) {
        // Onda 2: 12 Invasores (6 Blindados e 6 Fuzileiros)
        for (let i = 0; i < 12; i++) {
            const isHeavy = (i % 2 === 0);
            monsterDefs.push({
                name: (isHeavy ? `Blindado #${i + 1}` : `Fuzileiro #${i + 1}`),
                npcIndex: isHeavy ? 1106 : 1109,
                scale: isHeavy ? 170 : 150,
                hp: isHeavy ? (1400 + pveLv * 250) : (1000 + pveLv * 200),
                damage: isHeavy ? (45 + pveLv * 10) : (40 + pveLv * 10),
                isBoss: false
            });
        }
        sendGlobalSystemChat("⚔️ [MODO AVENTURA] ONDA 2 INICIADA! INVASORES PESADOS SE APROXIMAM!");
    } else {
        // Onda 3 (Final): 6 Guardas Blindados + 1 GRANDE BOSS (Index 1111, monsterbp_tps_boos)
        for (let i = 0; i < 6; i++) {
            monsterDefs.push({
                name: `Guarda #${i + 1}`,
                npcIndex: 1110,
                scale: 160,
                hp: 1800 + pveLv * 300,
                damage: 50 + pveLv * 10,
                isBoss: false
            });
        }
        monsterDefs.push({
            name: "Lorde das Trevas (BOSS)",
            npcIndex: 1111,
            scale: 250,
            hp: 15500 + pveLv * 3500,
            damage: 120 + pveLv * 20,
            isBoss: true
        });
        sendGlobalSystemChat("🔥 [MODO AVENTURA] ONDA FINAL! O CHEFE SURGIU NA ARENA!");
    }

    monsterDefs.forEach((monDef, idx) => {
        const monId = 3001 + idx;
        const pt = spawnPoints[idx % spawnPoints.length];
        const sx = pt.x + Math.floor((Math.random() - 0.5) * 200);
        const sy = pt.y; // Altura exata do chão 10776
        const sz = pt.z + Math.floor((Math.random() - 0.5) * 200); // Profundidade nos túneis

        const monster = {
            id: monId,
            index: monDef.npcIndex,
            name: monDef.name,
            hp: monDef.hp,
            maxHp: monDef.hp,
            damage: monDef.damage,
            scale: monDef.scale,
            isBoss: monDef.isBoss,
            x: sx,
            y: sy,
            z: sz,
            force: 2, // Inimigo
            lastAttackTime: 0,
            isPveMonster: true
        };

        match.monsters[monId] = monster;
        state.spawnedNpcs[monId] = monster;

        const className = db.getEntityClassName(monDef.npcIndex);
        const spawnPacket = `d${encodeInt(monId)}\nc${className}:${encodeInt(monId)},("Initialize",(${encodeInt(1)},${encodeInt(monId)},${encodeInt(monDef.npcIndex)},${encodeInt(2)},(${encodeInt(sx)},${encodeInt(sy)},${encodeInt(sz)}),(0,0,0),(${encodeInt(monDef.scale)},${encodeInt(monDef.scale)},${encodeInt(monDef.scale)}),(0,0,0),${encodeInt(-1)},${encodeInt(monDef.hp)},${encodeInt(0)},${encodeInt(1)},${encodeInt(0)},""),((),()),0)\n`;

        match.players.forEach(p => {
            if (!p.destroyed) {
                p.write(spawnPacket);
            }
        });
    });

    match.monsterNum = Object.keys(match.monsters).length;
    updatePveMonsterCount(match);
    console.log(`[🏰 PVE ONDA ${round}] Spawnados ${match.monsterNum} monstros PVE na masmorra.`);
}

function updatePveMonsterCount(match) {
    if (!match || !match.players) return;
    const p19000 = encodeInt(19000);
    const p19008 = encodeInt(19008);
    const countEnc = encodeInt(match.monsterNum || 0);
    const packet = `e${socket.playerIndex || 1}:SendMsgToClient,(${p19000},(${p19008},(${countEnc})))\n`;
    match.players.forEach(p => {
        if (!p.destroyed) {
            p.write(packet);
        }
    });
}

function handlePveWaveCleared(match) {
    if (!match) return;
    if (match.round < match.maxRound) {
        match.state = 'round_cleared';
        sendGlobalSystemChat(`🛡️ [MODO AVENTURA] ONDA ${match.round} CONCLUÍDA! PRÓXIMA ONDA EM 5 SEGUNDOS...`);
        
        setTimeout(() => {
            if (!global.activePveMatch || global.activePveMatch !== match) return;
            match.round++;
            match.state = 'fight';

            // Atualiza HUD com o novo round (19003)
            const p19000 = encodeInt(19000);
            const p19003 = encodeInt(19003);
            const zero = encodeInt(0);
            const now = new Date();
            const pad = (n) => n.toString().padStart(2, '0');
            const formatDate = (d) => `${d.getFullYear()}/${pad(d.getMonth() + 1)}/${pad(d.getDate())} ${pad(d.getHours())}:${pad(d.getMinutes())}:${pad(d.getSeconds())}`;
            const roundEndTime = formatDate(new Date(now.getTime() + 1800 * 1000));
            const gameEndTime = formatDate(new Date(now.getTime() + 1800 * 1000));
            const dateStr = Fun.GetServerDate();
            const npcServerIdEnc = encodeInt(match.pveType === 9 ? 1130 : 0);

            let kingGameInfoFight;
            if (match.pveType === 12) {
                const openDoorData = "(1,1,2,2,3,3,4,4,5,5,6,6,7,7,8,8,9,9)";
                kingGameInfoFight = `(${encodeInt(1)},${encodeInt(1)},${zero},'${roundEndTime}',${zero},'${dateStr}',${encodeInt(match.round)},${encodeInt(match.maxRound)},${encodeInt(0)},'${gameEndTime}',${npcServerIdEnc},${encodeInt(match.pveType)},${openDoorData},'${dateStr}')`;
            } else {
                kingGameInfoFight = `(${encodeInt(1)},${encodeInt(1)},${zero},'${roundEndTime}',${zero},'${dateStr}',${encodeInt(match.round)},${encodeInt(match.maxRound)},${encodeInt(0)},'${gameEndTime}',${npcServerIdEnc},${encodeInt(match.pveType)},'${dateStr}')`;
            }
            const pveFightStartPacket = `e${socket.playerIndex || 1}:SendMsgToClient,(${p19000},(${p19003},${kingGameInfoFight}))\n`;
            match.players.forEach(p => {
                if (!p.destroyed) p.write(pveFightStartPacket);
            });

            spawnPveWave(match, match.round);
        }, 5000);
    } else {
        sendPveVictory(match);
    }
}

function sendPveVictory(match) {
    if (!match) return;
    match.state = 'victory';
    console.log(`[🏆 PVE VITÓRIA] Todas as ${match.maxRound} ondas de monstros foram derrotadas!`);
    sendGlobalSystemChat("🎉 [MODO AVENTURA] PARABÉNS! VOCÊ DERROTOU O CHEFE E COMPLETOU A MISSÃO!");

    // Fecha UI de combate e exibe resultado
    const p19000 = encodeInt(19000);
    const p19004 = encodeInt(19004); // KING_MSG_ROUNDEND
    const victoryMsg = `(${encodeInt(1)},${encodeInt(3)},${encodeInt(1000)})`;
    const roundEndPacket = `e${socket.playerIndex || 1}:SendMsgToClient,(${p19000},(${p19004},${victoryMsg}))\n`;

    match.players.forEach(p => {
        if (!p.destroyed) {
            p.write(roundEndPacket);
        }
    });

    setTimeout(() => {
        endPveMatch();
    }, 8000);
}

function tickPveMatch() {
    const match = global.activePveMatch;
    if (!match || match.state !== 'fight') return;

    const crystal = state.spawnedNpcs[1130];
    const targetX = 11665;
    const targetY = 10776;
    const targetZ = 2461;
    const now = Date.now();

    if (match.monsters) {
        for (const monId in match.monsters) {
            const monster = match.monsters[monId];
            if (!monster || monster.hp <= 0) continue;

            const curX = monster.x || targetX;
            const curZ = monster.z || (targetZ + 1200);
            const dx = targetX - curX;
            const dz = targetZ - curZ;
            const dist = Math.hypot(dx, dz);

            if (dist > 300) {
                // Avança suavemente em direção ao cristal
                const moveSpeed = monster.isBoss ? 70 : 100;
                const step = Math.min(dist - 250, moveSpeed);
                monster.x = Math.round(curX + (dx / dist) * step);
                monster.z = Math.round(curZ + (dz / dist) * step);

                // Envia pacote NSP de sincronização de posição para todos os clientes
                const nspPacket = `cNpc_Entity:${encodeInt(monId)},("NSP",(${encodeInt(monster.x)},${encodeInt(10776)},${encodeInt(monster.z)}))\n`;
                match.players.forEach(p => {
                    if (!p.destroyed) {
                        p.write(nspPacket);
                    }
                });
            } else {
                // Monstro está no alcance do cristal e desfere ataques
                if (crystal && crystal.hp > 0 && now - (monster.lastAttackTime || 0) >= 1500) {
                    monster.lastAttackTime = now;
                    const dmg = monster.damage || 30;
                    handleNpcHurt(null, 1130, dmg, Number(monId));
                    console.log(`[⚔️ PVE MONSTER HIT] Monstro #${monId} atacou o Cristal! Dano: ${dmg}, HP restante: ${crystal.hp}`);
                }
            }
        }
    }
}

function startPveMatch(socket, mapId) {
    const currentMapId = mapId || socket.targetMapId || state.getLastMapId() || 5105;
    const pveType = socket.pveFbType || (db.MAP_CONFIG[currentMapId] ? db.MAP_CONFIG[currentMapId].type : 9);
    const pveLv = socket.pveFbLv !== undefined ? socket.pveFbLv : 0;
    
    console.log(`[🏰 PVE MATCH] startPveMatch chamado para ${socket.playerName || 'Jogador'} no mapa ${currentMapId} (Tipo: ${pveType}, Lv: ${pveLv})`);

    if (!global.activePveMatch) {
        global.activePveMatch = {
            mapId: currentMapId,
            pveType: pveType,
            pveLv: pveLv,
            state: 'ready',
            round: 1,
            maxRound: 3,
            monsterNum: 0,
            monsters: {},
            crystal: null,
            players: [],
            timer: null,
            tickInterval: null,
            startTime: Date.now()
        };
        console.log(`[🏰 PVE MATCH] Nova sessão PVE inicializada para o mapa ${currentMapId}.`);
    }

    const match = global.activePveMatch;
    if (!match.players.includes(socket)) {
        match.players.push(socket);
        console.log(`[🏰 PVE MATCH] Jogador ${socket.playerName || 'Jogador'} registrado na sessão PVE.`);
    }

    // Se for Mina de Cristal, spawna o cristal de defesa
    if (pveType === 9) {
        spawnPveCrystal(match);
    }

    const now = new Date();
    const pad = (n) => n.toString().padStart(2, '0');
    const formatDate = (d) => `${d.getFullYear()}/${pad(d.getMonth() + 1)}/${pad(d.getDate())} ${pad(d.getHours())}:${pad(d.getMinutes())}:${pad(d.getSeconds())}`;

    const readyEndTime = formatDate(new Date(now.getTime() + 8 * 1000)); // 8s countdown
    const roundEndTime = formatDate(new Date(now.getTime() + 1800 * 1000)); // 30min
    const gameEndTime = formatDate(new Date(now.getTime() + 1800 * 1000));
    const dateStr = Fun.GetServerDate();

    const p19000 = encodeInt(19000);
    const p19001 = encodeInt(19001);
    const zero = encodeInt(0);
    const one = encodeInt(1);
    const gameTypeEnc = encodeInt(pveType);
    const gameStateEnc = encodeInt(1); // 1 = in progress
    const fightStateEnc = encodeInt(0); // 0 = ready / countdown
    const roundEnc = encodeInt(match.round);
    const maxRoundEnc = encodeInt(match.maxRound);
    const monsterNumEnc = encodeInt(match.monsterNum);
    const npcServerIdEnc = encodeInt(pveType === 9 ? 1130 : 0);

    let kingGameInfo;
    if (pveType === 12) {
        const openDoorData = "(1,1,2,2,3,3,4,4,5,5,6,6,7,7,8,8,9,9)";
        kingGameInfo = `(${gameStateEnc},${fightStateEnc},${zero},'${roundEndTime}',${zero},'${readyEndTime}',${roundEnc},${maxRoundEnc},${monsterNumEnc},'${gameEndTime}',${npcServerIdEnc},${gameTypeEnc},${openDoorData},'${dateStr}')`;
    } else {
        kingGameInfo = `(${gameStateEnc},${fightStateEnc},${zero},'${roundEndTime}',${zero},'${readyEndTime}',${roundEnc},${maxRoundEnc},${monsterNumEnc},'${gameEndTime}',${npcServerIdEnc},${gameTypeEnc},'${dateStr}')`;
    }

    const userListItems = match.players.map(p => {
        const pIdx = p.playerIndex || 1;
        const pIdxEnc = encodeInt(pIdx);
        const userDetails = `(${pIdxEnc},'${p.playerNameEnc || encodeHex(p.playerName || "Jogador")}',${zero},${one},${zero},${pIdxEnc},(),${encodeInt(1000)})`;
        return `${pIdxEnc},${userDetails}`;
    });
    const allUserList = `(${userListItems.join(',')})`;

    // Envia 19001 (GameStart) para inicializar a KingCon UI
    const pveGameStartPacket = `e${socket.playerIndex || 1}:SendMsgToClient,(${p19000},(${p19001},(${kingGameInfo},${allUserList})))\n`;
    match.players.forEach(p => {
        if (!p.destroyed) {
            p.write(pveGameStartPacket);
        }
    });
    console.log(`[🏰 PVE HUD] Pacote 19001 enviado! Interface KingCon aberta no cliente.`);

    // Inicia timer para transição de Contagem Regressiva -> Combate (19003) e Spawn da Onda 1
    if (!match.timer) {
        match.timer = setTimeout(() => {
            if (match.state === 'ready') {
                match.state = 'fight';
                const fightStateActiveEnc = encodeInt(1); // 1 = luta iniciada
                let kingGameInfoFight;
                if (pveType === 12) {
                    const openDoorData = "(1,1,2,2,3,3,4,4,5,5,6,6,7,7,8,8,9,9)";
                    kingGameInfoFight = `(${gameStateEnc},${fightStateActiveEnc},${zero},'${roundEndTime}',${zero},'${readyEndTime}',${roundEnc},${maxRoundEnc},${monsterNumEnc},'${gameEndTime}',${npcServerIdEnc},${gameTypeEnc},${openDoorData},'${dateStr}')`;
                } else {
                    kingGameInfoFight = `(${gameStateEnc},${fightStateActiveEnc},${zero},'${roundEndTime}',${zero},'${readyEndTime}',${roundEnc},${maxRoundEnc},${monsterNumEnc},'${gameEndTime}',${npcServerIdEnc},${gameTypeEnc},'${dateStr}')`;
                }
                const p19003 = encodeInt(19003);
                const pveFightStartPacket = `e${socket.playerIndex || 1}:SendMsgToClient,(${p19000},(${p19003},${kingGameInfoFight}))\n`;
                match.players.forEach(p => {
                    if (!p.destroyed) {
                        p.write(pveFightStartPacket);
                    }
                });
                console.log(`[🏰 PVE FIGHT START] Contagem regressiva concluída! Combate iniciado (19003).`);
                
                // Spawna Onda 1
                spawnPveWave(match, 1);

                // Inicia loop de IA de monstros
                if (!match.tickInterval) {
                    match.tickInterval = setInterval(() => tickPveMatch(), 400);
                }
            }
        }, 8000);
    }
}

function endPveMatch() {
    if (global.activePveMatch) {
        if (global.activePveMatch.timer) {
            clearTimeout(global.activePveMatch.timer);
        }
        if (global.activePveMatch.tickInterval) {
            clearInterval(global.activePveMatch.tickInterval);
        }
        // Limpa bots/monstros ativos da memória
        if (global.activePveMatch.monsters && state.activeBots) {
            for (const monId in global.activePveMatch.monsters) {
                delete state.activeBots[monId];
            }
        }
        delete state.spawnedNpcs[1130];
        global.activePveMatch = null;
        console.log(`[🏰 PVE MATCH] Sessão PVE finalizada e limpa.`);
    }
}

module.exports = {
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
