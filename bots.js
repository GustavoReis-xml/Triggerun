const fs = require('fs');

// Definição das Lanes/Caminhos no mapa 5204 (King Hall)
// Spawns: Force 1 (Blue): (9802, 3820, 4602), Force 2 (Red): (1797, 3820, 5999)
const LANES_5204 = {
    // Force 1 (Blue) se deslocando em direção à base da Force 2 (Red)
    "1": [
        // Lane 0: Flanco Superior (Z alto)
        [
            { x: 8100, y: 3820, z: 7000 },
            { x: 5800, y: 3820, z: 7000 },
            { x: 3500, y: 3820, z: 7000 },
            { x: 2000, y: 3820, z: 5999 }
        ],
        // Lane 1: Flanco Inferior (Z baixo)
        [
            { x: 8100, y: 3820, z: 3600 },
            { x: 5800, y: 3820, z: 3600 },
            { x: 3500, y: 3820, z: 3600 },
            { x: 2000, y: 3820, z: 5999 }
        ],
        // Lane 2: Rota Central com desvio de colisão do bloco central
        [
            { x: 8100, y: 3820, z: 5000 },
            { x: 5800, y: 3820, z: 3500 }, // Desvia pelo Z inferior
            { x: 3500, y: 3820, z: 5000 },
            { x: 2000, y: 3820, z: 5999 }
        ]
    ],
    // Force 2 (Red) se deslocando em direção à base da Force 1 (Blue)
    "2": [
        // Lane 0: Flanco Superior (Z alto)
        [
            { x: 3500, y: 3820, z: 7000 },
            { x: 5800, y: 3820, z: 7000 },
            { x: 8100, y: 3820, z: 7000 },
            { x: 9802, y: 3820, z: 4602 }
        ],
        // Lane 1: Flanco Inferior (Z baixo)
        [
            { x: 3500, y: 3820, z: 3600 },
            { x: 5800, y: 3820, z: 3600 },
            { x: 8100, y: 3820, z: 3600 },
            { x: 9802, y: 3820, z: 4602 }
        ],
        // Lane 2: Rota Central com desvio de colisão do bloco central
        [
            { x: 3500, y: 3820, z: 5600 },
            { x: 5800, y: 3820, z: 7100 }, // Desvia pelo Z superior
            { x: 8100, y: 3820, z: 5600 },
            { x: 9802, y: 3820, z: 4602 }
        ]
    ]
};

// Definição geométrica dos obstáculos (caixas/paredes/pilares centrais) no mapa 5204
// Formato: { x, z, w, h } onde (x, z) é o canto mínimo e w, h são largura e profundidade
const MAP_OBSTACLES = [
    // 1. Grande estrutura/barreira central do King Hall
    { x: 4400, z: 4100, w: 2800, h: 2400 },

    // 2. Paredes de Spawn da Force 1 (Blue) - Bloqueios na direita x = 8800
    { x: 8700, z: 6000, w: 200, h: 2000 }, // Superior
    { x: 8700, z: 2000, w: 200, h: 2600 }, // Inferior

    // 3. Paredes de Spawn da Force 2 (Red) - Bloqueios na esquerda x = 2800
    { x: 2700, z: 6000, w: 200, h: 2000 }, // Superior
    { x: 2700, z: 2000, w: 200, h: 2600 }  // Inferior
];

// Helper para verificar se um segmento de reta entre dois pontos intercepta uma caixa
function lineIntersectsRect(x1, z1, x2, z2, rx, rz, rw, rh) {
    const minX = rx;
    const maxX = rx + rw;
    const minZ = rz;
    const maxZ = rz + rh;

    // Se ambos os pontos estão inteiramente de um mesmo lado, não há colisão
    if ((x1 < minX && x2 < minX) || (x1 > maxX && x2 > maxX) ||
        (z1 < minZ && z2 < minZ) || (z1 > maxZ && z2 > maxZ)) {
        return false;
    }

    const checkT = (t, ptX, ptZ) => {
        return t >= 0 && t <= 1 && ptX >= minX && ptX <= maxX && ptZ >= minZ && ptZ <= maxZ;
    };

    if (x2 !== x1) {
        // Testa colisão na borda esquerda x = minX
        let t = (minX - x1) / (x2 - x1);
        if (checkT(t, minX, z1 + t * (z2 - z1))) return true;

        // Testa colisão na borda direita x = maxX
        t = (maxX - x1) / (x2 - x1);
        if (checkT(t, maxX, z1 + t * (z2 - z1))) return true;
    }

    if (z2 !== z1) {
        // Testa colisão na borda inferior z = minZ
        let t = (minZ - z1) / (z2 - z1);
        if (checkT(t, x1 + t * (x2 - x1), minZ)) return true;

        // Testa colisão na borda superior z = maxZ
        t = (maxZ - z1) / (z2 - z1);
        if (checkT(t, x1 + t * (x2 - x1), maxZ)) return true;
    }

    return false;
}

// Verifica se existe linha de visão direta (Line of Sight - LOS) sem paredes no caminho
function hasLineOfSight(x1, z1, x2, z2) {
    for (const obs of MAP_OBSTACLES) {
        if (lineIntersectsRect(x1, z1, x2, z2, obs.x, obs.z, obs.w, obs.h)) {
            return false; // Bloqueado por parede!
        }
    }
    return true; // Linha de visão direta livre!
}

// Inicializa variáveis e estados estendidos para os bots humanos
function initBotMetadata(bot) {
    if (bot.lane === undefined) {
        // Escolhe uma rota aleatória (0, 1 ou 2)
        bot.lane = Math.floor(Math.random() * 3);
        bot.waypointIndex = 0;
        bot.isStuck = false;
        bot.stuckTicks = 0;
        bot.stuckSlideDir = Math.random() < 0.5 ? 1 : -1;
        bot.prevX = bot.lastX;
        bot.prevZ = bot.lastZ;
        
        // Cooldowns e munição
        bot.isAttacking = false;
        bot.isReloading = false;
        bot.ammo = 30; 
        bot.reloadTicks = 0;
        bot.jumpTicks = 0;
        bot.strafeDir = Math.random() < 0.5 ? 1 : -1;
        bot.strafeTicks = 0;
    }
}

// Tick principal da Inteligência Artificial dos Bots
// Frequência ideal de chamada: 150ms ~ 200ms para movimentação 100% lisa
function tickBotAI(activeSockets, activeBots, activeMatch, helpers) {
    if (!activeBots || !activeMatch || activeMatch.state !== 'fight') return;

    const { encodeInt, sendEntityDamageUpdate, handlePlayerDeath } = helpers;

    for (const key in activeBots) {
        const bot = activeBots[key];
        if (!bot || bot.destroyed || bot.hp <= 0) {
            // Se o bot morreu, garante que ele pare de atirar
            if (bot.isAttacking) {
                bot.isAttacking = false;
                const stopAnim = `e${bot.playerIndex}:CA,((('AttackState',2,0),1,${encodeInt(bot.playerIndex)})))\n`;
                activeSockets.forEach(s => {
                    if (s.socketType === "SCENE" && !s.destroyed) s.write(stopAnim);
                });
            }
            continue;
        }

        // Garante que o bot tem todas as variáveis de controle humano
        initBotMetadata(bot);

        // Se estiver recarregando munição
        if (bot.isReloading) {
            bot.reloadTicks--;
            if (bot.reloadTicks <= 0) {
                bot.isReloading = false;
                bot.ammo = 30;
                // Para animação de recarga (Reload = 0) - NOTA: C# CA exige 3 parênteses fechados )))
                const stopReload = `e${bot.playerIndex}:CA,((('Reload',0,0),1,${encodeInt(bot.playerIndex)})))\n`;
                activeSockets.forEach(s => {
                    if (s.socketType === "SCENE" && !s.destroyed) s.write(stopReload);
                });
            }
            continue;
        }

        // 1. Procurar o inimigo vivo mais próximo
        let closestEnemy = null;
        let minDistance = Infinity;

        // Jogadores reais ativos
        activeSockets.forEach(s => {
            if (s.socketType === "SCENE" && !s.destroyed && s.hp > 0 && s.force !== bot.force) {
                const dx = s.lastX - bot.lastX;
                const dy = (s.lastY || 3820) - bot.lastY;
                const dz = s.lastZ - bot.lastZ;
                const dist = Math.sqrt(dx*dx + dy*dy + dz*dz);
                if (dist < minDistance) {
                    minDistance = dist;
                    closestEnemy = s;
                }
            }
        });

        // Outros bots
        for (const otherKey in activeBots) {
            const otherBot = activeBots[otherKey];
            if (otherBot && otherBot.playerIndex !== bot.playerIndex && otherBot.hp > 0 && otherBot.force !== bot.force) {
                const dx = otherBot.lastX - bot.lastX;
                const dy = otherBot.lastY - bot.lastY;
                const dz = otherBot.lastZ - bot.lastZ;
                const dist = Math.sqrt(dx*dx + dy*dy + dz*dz);
                if (dist < minDistance) {
                    minDistance = dist;
                    closestEnemy = otherBot;
                }
            }
        }

        // Determina velocidade de movimento adaptável
        let step = 75;
        let moveX = 0;
        let moveZ = 0;
        let targetAngle = 0;

        // Verifica se há linha de visão direta para o inimigo mais próximo
        const hasLos = closestEnemy ? hasLineOfSight(bot.lastX, bot.lastZ, closestEnemy.lastX, closestEnemy.lastZ) : false;

        // 2. Tomada de Decisão (Combate vs Navegação de Rotas)
        if (closestEnemy && minDistance <= 3000 && hasLos) {
            // --- MODO COMBATE COMPLETO (Inimigo à vista, distância < 30m e linha de visão direta) ---
            step = 55; // Velocidade tática mais baixa durante esquiva/tiro
            
            const dx = closestEnemy.lastX - bot.lastX;
            const dy = (closestEnemy.lastY || 3820) - bot.lastY;
            const dz = closestEnemy.lastZ - bot.lastZ;

            // Mira física (Yaw) direcionada ao inimigo
            targetAngle = Math.atan2(dx, dz);
            const angleDeg = Math.round(targetAngle * (180 / Math.PI));
            const rotEnc = encodeInt(angleDeg);
            const zero = encodeInt(0);

            // Sincroniza Olhar Cabeça/Arma (CT) e Rotação Corpo (CR) voltados ao inimigo
            const enemyXEnc = encodeInt(closestEnemy.lastX);
            const enemyYEnc = encodeInt(closestEnemy.lastY || 3820);
            const enemyZEnc = encodeInt(closestEnemy.lastZ);
            const lookPacket = `e${bot.playerIndex}:CT,(((${enemyXEnc},${enemyYEnc},${enemyZEnc}),${zero}))\n`;
            const rotPacket = `e${bot.playerIndex}:CR,((${rotEnc},${zero}))\n`;

            activeSockets.forEach(s => {
                if (s.socketType === "SCENE" && !s.destroyed) {
                    s.write(lookPacket);
                    s.write(rotPacket);
                }
            });

            // Lógica Humana de Esquiva Lateral (Strafing)
            bot.strafeTicks--;
            if (bot.strafeTicks <= 0) {
                bot.strafeDir = Math.random() < 0.5 ? 1 : -1;
                bot.strafeTicks = 5 + Math.floor(Math.random() * 10);
            }

            const perpX = -dz;
            const perpZ = dx;
            const perpDist = Math.sqrt(perpX*perpX + perpZ*perpZ);
            
            if (perpDist > 0) {
                moveX += (perpX / perpDist) * bot.strafeDir * step * 0.7;
                moveZ += (perpZ / perpDist) * bot.strafeDir * step * 0.7;
                
                // Mantém distância ideal (12 metros)
                if (minDistance > 1200) {
                    moveX += (dx / minDistance) * step * 0.7;
                    moveZ += (dz / minDistance) * step * 0.7;
                } else if (minDistance < 600) {
                    moveX -= (dx / minDistance) * step * 0.7;
                    moveZ -= (dz / minDistance) * step * 0.7;
                }
            } else {
                moveX = (dx / minDistance) * step;
                moveZ = (dz / minDistance) * step;
            }

            // Atirar se tiver munição
            if (bot.ammo > 0) {
                // Inicia animação de atirar (AttackState = 1) - NOTA: C# CA exige 3 parênteses fechados )))
                if (!bot.isAttacking) {
                    bot.isAttacking = true;
                    const startAnim = `e${bot.playerIndex}:CA,((('AttackState',2,1),1,${encodeInt(bot.playerIndex)})))\n`;
                    activeSockets.forEach(s => {
                        if (s.socketType === "SCENE" && !s.destroyed) s.write(startAnim);
                    });
                }

                bot.ammo--;

                // Chance realista de acerto (30% de chance por tick de 200ms)
                if (Math.random() < 0.30) {
                    const damage = 60 + Math.floor(Math.random() * 30);
                    let place = 0; 

                    let damageRemaining = damage;
                    if (closestEnemy.shield && closestEnemy.shield > 0) {
                        const shieldAbsorbed = Math.min(closestEnemy.shield, damageRemaining);
                        closestEnemy.shield -= shieldAbsorbed;
                        damageRemaining -= shieldAbsorbed;
                        place = 2; 
                    }

                    if (damageRemaining > 0) {
                        closestEnemy.hp = Math.max(0, (closestEnemy.hp !== undefined ? closestEnemy.hp : closestEnemy.maxHp) - damageRemaining);
                    }

                    sendEntityDamageUpdate(closestEnemy.playerId || closestEnemy.playerIndex, closestEnemy, damage, bot.playerIndex, place, bot.currentHeroId);

                    console.log(`[🤖 BOT COMBATE] Bot ${bot.playerName} acertou ${closestEnemy.playerName || 'Bot'}! HP Restante: ${closestEnemy.hp}`);

                    if (closestEnemy.hp <= 0) {
                        handlePlayerDeath(closestEnemy, bot);
                        bot.isAttacking = false;
                        const stopAnim = `e${bot.playerIndex}:CA,((('AttackState',2,0),1,${encodeInt(bot.playerIndex)})))\n`;
                        activeSockets.forEach(s => {
                            if (s.socketType === "SCENE" && !s.destroyed) s.write(stopAnim);
                        });
                    }
                }
            } else {
                // Fim de munição: inicia recarga (Reload = 1) de 2 segundos (10 ticks)
                bot.isReloading = true;
                bot.reloadTicks = 10;
                bot.isAttacking = false;

                const stopAnim = `e${bot.playerIndex}:CA,((('AttackState',2,0),1,${encodeInt(bot.playerIndex)})))\n`;
                const reloadAnim = `e${bot.playerIndex}:CA,((('Reload',0,1),1,${encodeInt(bot.playerIndex)})))\n`;

                activeSockets.forEach(s => {
                    if (s.socketType === "SCENE" && !s.destroyed) {
                        s.write(stopAnim);
                        s.write(reloadAnim);
                    }
                });

                console.log(`[🤖 BOT RELOAD] Bot ${bot.playerName} está recarregando.`);
            }

        } else {
            // --- MODO NAVEGAÇÃO DE ROTAS (Sem inimigo à vista ou bloqueado por parede) ---
            if (bot.isAttacking) {
                bot.isAttacking = false;
                const stopAnim = `e${bot.playerIndex}:CA,((('AttackState',2,0),1,${encodeInt(bot.playerIndex)})))\n`;
                activeSockets.forEach(s => {
                    if (s.socketType === "SCENE" && !s.destroyed) s.write(stopAnim);
                });
            }

            step = 95; // Velocidade de sprint ao atravessar o mapa

            // Obtém os waypoints da rota selecionada do bot
            const mapLanes = LANES_5204[String(bot.force)];
            if (!mapLanes) continue;
            
            const laneWaypoints = mapLanes[bot.lane];
            if (!laneWaypoints) continue;

            // Se o bot estiver perseguindo um alvo oculto por parede, redirecionamos para o waypoint mais próximo do inimigo
            let targetWp = laneWaypoints[bot.waypointIndex];
            if (!targetWp) {
                bot.lane = Math.floor(Math.random() * 3);
                bot.waypointIndex = 0;
                continue;
            }

            const dx = targetWp.x - bot.lastX;
            const dz = targetWp.z - bot.lastZ;
            const distToWp = Math.sqrt(dx*dx + dz*dz);

            // Avança para o próximo waypoint se estiver perto
            if (distToWp < 250) {
                bot.waypointIndex = (bot.waypointIndex + 1) % laneWaypoints.length;
                continue;
            }

            // Calcula direção ao waypoint
            targetAngle = Math.atan2(dx, dz);
            moveX = (dx / distToWp) * step;
            moveZ = (dz / distToWp) * step;
        }

        // --- SISTEMA FÍSICO DE DETECÇÃO E EVITAÇÃO DE COLISÃO GLOBAL (STUCK PREVENTION) ---
        // Calcula a movimentação física real desde o último tick
        const distActuallyMoved = Math.sqrt(Math.pow(bot.lastX - bot.prevX, 2) + Math.pow(bot.lastZ - bot.prevZ, 2));
        bot.prevX = bot.lastX;
        bot.prevZ = bot.lastZ;

        // Se está tentando se mover e não sai do lugar, está colidindo com parede física
        if (Math.abs(moveX) > 5 || Math.abs(moveZ) > 5) {
            if (distActuallyMoved < 15) {
                bot.stuckTicks++;
            } else {
                bot.stuckTicks = Math.max(0, bot.stuckTicks - 1);
            }
        } else {
            bot.stuckTicks = 0;
        }

        if (bot.stuckTicks > 3) {
            // Salto físico de emergência para caixas/obstáculos - NOTA: C# CA exige 3 parênteses fechados )))
            bot.jumpTicks--;
            if (bot.jumpTicks <= 0) {
                const jumpAnim = `e${bot.playerIndex}:CA,((('onjump',0,1),0,${encodeInt(bot.playerIndex)})))\n`;
                activeSockets.forEach(s => {
                    if (s.socketType === "SCENE" && !s.destroyed) s.write(jumpAnim);
                });
                bot.jumpTicks = 15; // Cooldown de pulo
            }

            // Corrige a movimentação aplicando vetor de escorregamento 90 graus lateral para contornar a barreira física
            const slideAngle = targetAngle + (Math.PI / 2) * bot.stuckSlideDir;
            moveX = Math.cos(slideAngle) * step;
            moveZ = Math.sin(slideAngle) * step;

            // Inverte a direção de escorregamento a cada 8 ticks para não ficar preso
            if (bot.stuckTicks % 8 === 0) {
                bot.stuckSlideDir *= -1;
            }
        }

        // Aplica o deslocamento físico final livre de colisões
        bot.lastX += Math.round(moveX);
        bot.lastY = (bot.lastY || 3820);
        bot.lastZ += Math.round(moveZ);

        // Sincroniza Rotação Corpo (CR) e Posição Física (CP)
        const faceAngleRad = Math.atan2(moveX, moveZ);
        const faceAngleDeg = Math.round(faceAngleRad * (180 / Math.PI));
        const rotEnc = encodeInt(faceAngleDeg);
        const zero = encodeInt(0);

        const xEnc = encodeInt(bot.lastX);
        const yEnc = encodeInt(bot.lastY);
        const zEnc = encodeInt(bot.lastZ);
        
        const cpPacket = `e${bot.playerIndex}:CP,(((${xEnc},${yEnc},${zEnc}),${zero},${rotEnc},${zero}))\n`;
        const crPacket = `e${bot.playerIndex}:CR,((${rotEnc},${zero}))\n`;

        activeSockets.forEach(s => {
            if (s.socketType === "SCENE" && !s.destroyed) {
                s.write(cpPacket);
                s.write(crPacket);
            }
        });

        // Saltos espontâneos de corrida (3% de chance livre de stuck)
        if (Math.random() < 0.03 && bot.stuckTicks === 0 && !closestEnemy) {
            const jumpAnim = `e${bot.playerIndex}:CA,((('onjump',0,1),0,${encodeInt(bot.playerIndex)})))\n`;
            activeSockets.forEach(s => {
                if (s.socketType === "SCENE" && !s.destroyed) s.write(jumpAnim);
            });
        }
    }
}

module.exports = {
    tickBotAI
};
