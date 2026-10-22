// Módulo de Mapeamentos Estáticos e Leitura de XMLs do Trigger Run

const fs = require('fs');
const path = require('path');
const config = require('./config');
const { encodeInt, decodeInt, encodeHex, decodeHex, Fun } = require('./utils');

const filesVersionMap = {};
const npcData = {};
let heroCosmeticsMap = {};
const heroSkillsMap = {};
const heroDazhaoMap = {};
const playerSkillsMap = {};
const buffDurationsMap = {};
const buffsMap = {};
const weaponsMap = {};
const allFaceIds = [];
const allPaintIds = [];
const heroWeaponGrids = {};
const faceUnlockPrices = {};
const paintUnlockPrices = {};
const heroUnlockPrices = {};
const cosmeticUnlockPrices = {};

const mapBases = {};

// Mapeamento de Mapas (baseado em fbset.xml)
const MAP_CONFIG = {
    // 训练 / Tutorial
    5205: { name: 'xinshouyindao_3', type: 7 }, // Space Base
    5202: { name: 'bachang_baxi', type: 6 },
    
    // 团队死斗 (Type 7)
    5204: { name: 'jiaodouchang_02_3', type: 7 }, // 王者大厅
    5010: { name: 'dz_jjc', type: 7 },           // 法老陵墓
    5024: { name: 'sidou03', type: 7 },          // 远航者号
    5210: { name: 'sidou04', type: 7 },          // 血狱工厂
    5212: { name: 'sidou05', type: 7 },          // 深井基地
    5214: { name: 'sidou06', type: 7 },          // Mata-Mata 06
    5215: { name: 'pintu', type: 7 },            // Pintu (Puzzle)
    5216: { name: 'conglinyiji', type: 7 },      // Ruínas da Selva
    5217: { name: 'fengbaozhanqu', type: 7 },    // Zona de Tempestade
    5218: { name: 'xinfengbaozhanqu', type: 7 }, // Nova Zona de Tempestade
    5004: { name: 'csjj_dataosha_gai', type: 7 },// Batalha Royale
    5099: { name: 'test', type: 7 },             // Sandbox de Teste
    
    // 争夺模式 (Type 3)
    5030: { name: 'wudingzhengduo_baked_3_xiugai', type: 3 }, // 云中天台
    5074: { name: 'shuiba_smc_3_xiugai', type: 3 },           // 深谷水坝
    5068: { name: 'persian_3_xiugai', type: 3 },              // 沙漠王宫
    5050: { name: 'degulagubao_3_xiugai', type: 3 },          // 德古拉城堡
    5055: { name: 'fengducheng_3_xgjg', type: 3 },            // 幽冥地宫
    5208: { name: 'zhengduo_06_anse_3', type: 3 },            // 英魂祭坛
    5045: { name: 'qab02', type: 3 },                         // Dominação A/B
    
    // 护送模式 (Type 2)
    5063: { name: 'husong01_new_03', type: 2 },  // 荣耀之城
    5022: { name: 'husong02', type: 2 },         // 星空之城
    
    // 资源模式 (Type 5)
    5049: { name: 'xiudaoyuan_3_xiugai_huzi', type: 5 }, // 雪山修道院
    5260: { name: 'ziyuan_02_baitian', type: 5 },        // 天使庄园
    
    // 资源模式 (Type 4)
    5056: { name: 'jiaodouchang_3_smc_low_xiugai', type: 4 }, // 勇者竞技场
    5058: { name: 'dz_jjc', type: 4 },                        // 法老陵墓
    5059: { name: 'bachang_baxi', type: 4 },             // 王者大厅
    
    // 个人死斗 (Type 11)
    5301: { name: 'wudingzhengduo_baked_3_xiugai', type: 11 },
    5302: { name: 'shuiba_smc_3_xiugai', type: 11 },
    5303: { name: 'persian_3_xiugai', type: 11 },
    5304: { name: 'degulagubao_3_xiugai', type: 11 },
    5305: { name: 'fengducheng_3_xgjg', type: 11 },
    5306: { name: 'husong01_new_03', type: 11 },
    5307: { name: 'husong02', type: 11 },

    // PVE 冒险模式 - 水晶矿洞 / Mina de Cristal (Type 9)
    5105: { name: 'shuijingkuangdong', type: 9 },
    5106: { name: 'shuijingkuangdong', type: 9 },
    5107: { name: 'shuijingkuangdong', type: 9 },
    5108: { name: 'shuijingkuangdong', type: 9 },
    5109: { name: 'shuijingkuangdong', type: 9 },
    5110: { name: 'shuijingkuangdong', type: 9 },
    5111: { name: 'shuijingkuangdong', type: 9 },
    5112: { name: 'shuijingkuangdong', type: 9 },
    5113: { name: 'shuijingkuangdong', type: 9 },
    5114: { name: 'shuijingkuangdong', type: 9 },
    5310: { name: 'shuijingkuangdong', type: 9 },
    5311: { name: 'shuijingkuangdong', type: 9 },

    // PVE 冒险模式 - 黑暗之地 / Terra das Trevas (Type 12)
    5115: { name: 'hasd_qingshen', type: 12 },
    5116: { name: 'hasd_qingshen', type: 12 },
    5117: { name: 'hasd_qingshen', type: 12 },
    5118: { name: 'hasd_qingshen', type: 12 },
    5119: { name: 'hasd_qingshen', type: 12 },
    5120: { name: 'hasd_qingshen', type: 12 },
    5121: { name: 'hasd_qingshen', type: 12 },
    5122: { name: 'hasd_qingshen', type: 12 },
    5123: { name: 'hasd_qingshen', type: 12 },
    5124: { name: 'hasd_qingshen', type: 12 },
    5125: { name: 'hasd_qingshen', type: 12 },
    5126: { name: 'hasd_qingshen', type: 12 }
};

// Sobrescritas manuais para habilidades de grupo
const groupSkillsOverrides = {
    1153: { // Daji/Venus - Fox Phantom (Group Shield)
        buff_idx_me: [1047], // Fox Pantom (shield)
        buff_idx: [1047]
    },
    1122: { // Angel/Luolita - Sylph's Wind (Group Speed)
        buff_idx_me: [1035] // Sylph's Wind (speed up)
    },
    1103: { // Jack/Yinglong - Quicksand Vortex (Ultimate)
        longtime: 5000
    },
    1164: { // Lily/Lilith - Soul Chains (Ultimate)
        longtime: 5000
    }
};

// Carregadores
function initializeDatabase() {
    // 1. files_dir.xml
    try {
        const xmlContent = fs.readFileSync(config.filesDirXmlPath, 'utf8');
        const matches = xmlContent.matchAll(/<f path="([^"]+)"[^>]*version="([^"]+)"/g);
        for (const match of matches) {
            const exactPath = match[1];
            const version = match[2];
            filesVersionMap[exactPath.toLowerCase()] = {
                version: version,
                exactPath: exactPath
            };
        }
        console.log(`[📦 CDN MAP] Mapeadas ${Object.keys(filesVersionMap).length} versões e caminhos exatos de arquivos com sucesso!`);
    } catch (e) {
        console.error(`[⚠️ ERRO] Falha ao carregar files_dir.xml:`, e);
    }

    // 2. Npc.xml
    try {
        const npcContent = fs.readFileSync(config.npcXmlPath, 'utf8');
        const npcMatches = npcContent.matchAll(/<i\s+([^>]+)>/g);
        for (const match of npcMatches) {
            const attribs = match[1];
            const indexMatch = attribs.match(/Index="(\d+)"/);
            const hpMatch = attribs.match(/HP="(\d+)"/);
            const shieldMatch = attribs.match(/Shield="(\d+)"/);
            const nameMatch = attribs.match(/Name="([^"]+)"/);
            const timeMatch = attribs.match(/Time="(\d+)"/);
            const typeMatch = attribs.match(/Type="(\d+)"/);
            const weaponMatch = attribs.match(/Weapon_Index="(\d+)"/);
            if (indexMatch) {
                const index = parseInt(indexMatch[1]);
                const hp = hpMatch ? parseInt(hpMatch[1]) : 0;
                const shield = shieldMatch ? parseInt(shieldMatch[1]) : 0;
                const name = nameMatch ? nameMatch[1] : `NPC_${index}`;
                const time = timeMatch ? parseInt(timeMatch[1]) : 0;
                const type = typeMatch ? parseInt(typeMatch[1]) : 1;
                const weaponIndex = weaponMatch ? parseInt(weaponMatch[1]) : 0;
                npcData[index] = { hp, shield, name, time, type, weaponIndex };
            }
        }
        console.log(`[🤖 NPC DATABASE] Carregados ${Object.keys(npcData).length} NPCs do Npc.xml com sucesso!`);
    } catch (e) {
        console.error(`[⚠️ ERRO] Falha ao carregar Npc.xml:`, e);
    }

    // 3. herocosmetics.json
    try {
        const pathCosmetics = path.join(config.publicDir, 'baxi', 'herocosmetics.json');
        if (fs.existsSync(pathCosmetics)) {
            heroCosmeticsMap = JSON.parse(fs.readFileSync(pathCosmetics, 'utf8'));
            console.log(`[🎨 COSMETICS] Carregados ${Object.keys(heroCosmeticsMap).length} heróis com cosméticos.`);
        } else {
            console.warn('[⚠️ COSMETICS] herocosmetics.json não encontrado, usando padrão.');
        }
    } catch (e) {
        console.warn('[⚠️ COSMETICS] Falha ao ler herocosmetics.json: ', e);
    }

    // 4. heroinfo.xml
    try {
        const pathHeroInfo = path.join(config.publicDir, 'baxi', 'heroinfo.xml');
        const xmlContent = fs.readFileSync(pathHeroInfo, 'utf8');
        const matches = xmlContent.match(/<i\s+id="(\d+)"[^>]*showskill="([^"]*)"/g);
        if (matches) {
            for (const m of matches) {
                const idMatch = m.match(/id="(\d+)"/);
                const skillMatch = m.match(/showskill="([^"]*)"/);
                if (idMatch && skillMatch) {
                    const heroId = parseInt(idMatch[1]);
                    const skillStr = skillMatch[1].replace(/\|\|/g, '').replace(/\|/g, '');
                    const skills = skillStr.split(',').map(s => s.trim()).filter(s => s && !isNaN(s)).map(s => parseInt(s));
                    heroSkillsMap[heroId] = skills;
                }
            }
        }
        console.log(`[🎮 SKILLS DB] Inicializado com sucesso! ${Object.keys(heroSkillsMap).length} heróis mapeados.`);
    } catch (e) {
        console.error('[⚠️ ERRO AO CARREGAR HEROINFO.XML EM SERVER.JS]:', e);
    }

    // 5. dazhao.xml
    try {
        const pathDazhao = path.join(config.publicDir, 'baxi', 'dazhao.xml');
        const dzContent = fs.readFileSync(pathDazhao, 'utf8');
        const blocks = dzContent.match(/<i\s+id="(\d+)"[^>]*>([\s\S]*?)<\/i>/g);
        if (blocks) {
            const getAttr = (tag, attr) => {
                const m = tag.match(new RegExp(`${attr}=\"([^\"]+)\"`));
                return m ? m[1] : null;
            };
            blocks.forEach(b => {
                const idMatch = b.match(/<i\s+id="(\d+)"/);
                if (idMatch) {
                    const heroId = parseInt(idMatch[1]);
                    const skills = [];
                    const skillMatches = b.match(/<skill\s+[^>]+\/>/g);
                    if (skillMatches) {
                        skillMatches.forEach(sm => {
                            const sId = getAttr(sm, 'id');
                            const sMax = getAttr(sm, 'energymax');
                            const sCont = getAttr(sm, 'continuestime');
                            const sDoSkill = getAttr(sm, 'doskill');
                            const sAddTime = getAttr(sm, 'energyaddtime');
                            const sAddDamage = getAttr(sm, 'energyadddamage');
                            const sAddCure = getAttr(sm, 'energyaddcure');
                            if (sId && sMax) {
                                skills.push({
                                    id: parseInt(sId),
                                    maxEnergy: parseInt(sMax),
                                    continueTime: sCont ? parseInt(sCont) : 0,
                                    doskill: sDoSkill ? parseInt(sDoSkill) : 0,
                                    energyAddTime: sAddTime ? parseInt(sAddTime) : 120,
                                    energyAddDamage: sAddDamage ? parseInt(sAddDamage) : 150,
                                    energyAddCure: sAddCure ? parseInt(sAddCure) : 120
                                });
                            }
                        });
                    }
                    heroDazhaoMap[heroId] = skills;
                }
            });
        }
        console.log(`[🚀 DAZHAO DB] Inicializado com sucesso! ${Object.keys(heroDazhaoMap).length} heróis mapeados.`);
    } catch (e) {
        console.error('[⚠️ ERRO AO CARREGAR DAZHAO.XML EM SERVER.JS]:', e);
    }

    // 6. playerskill.xml
    try {
        const pathPlayerSkill = path.join(config.publicDir, 'baxi', 'playerskill.xml');
        const psContent = fs.readFileSync(pathPlayerSkill, 'utf8');
        const blocks = psContent.match(/<i\s+id="(\d+)"[^>]*\/>/g);
        if (blocks) {
            const getAttr = (tag, attr) => {
                const m = tag.match(new RegExp(`${attr}=\"([^\"]*)\"`));
                return m ? m[1] : null;
            };
            blocks.forEach(b => {
                const idMatch = b.match(/<i\s+id="(\d+)"/);
                if (idMatch) {
                    const skillId = parseInt(idMatch[1]);
                    const cd = getAttr(b, 'cd');
                    const longtime = getAttr(b, 'longtime');
                    const buffMe = getAttr(b, 'buff_idx_me');
                    const buffTarget = getAttr(b, 'buff_idx');
                    const maxSkillNumber = getAttr(b, 'maxskillnumber') || getAttr(b, 'maxSkillNumber');
                    const maxBuffNumber = getAttr(b, 'maxbuffnumber') || getAttr(b, 'maxBuffNumber');
                    playerSkillsMap[skillId] = {
                        cd: cd ? parseInt(cd) : 0,
                        longtime: longtime ? parseInt(longtime) : 0,
                        maxSkillNumber: maxSkillNumber ? parseInt(maxSkillNumber) : 1,
                        maxBuffNumber: maxBuffNumber ? parseInt(maxBuffNumber) : 0,
                        buff_idx_me: (buffMe && buffMe !== "0") ? buffMe.split('|').map(x => parseInt(x)) : [],
                        buff_idx: (buffTarget && buffTarget !== "0") ? buffTarget.split('|').map(x => parseInt(x)) : []
                    };
                }
            });
        }
        console.log(`[🔋 PLAYERSKILL DB] Inicializado com sucesso! ${Object.keys(playerSkillsMap).length} habilidades mapeadas.`);
    } catch (e) {
        console.error('[⚠️ ERRO AO CARREGAR PLAYERSKILL.XML EM SERVER.JS]:', e);
    }


    

    // 7. buff.xml
    try {
        const pathBuff = path.join(config.publicDir, 'baxi', 'buff.xml');
        const buffContent = fs.readFileSync(pathBuff, 'utf8');
        const blocks = buffContent.match(/<i\s+id="(\d+)"[^>]*\/>/g);
        if (blocks) {
            const getAttr = (tag, attr) => {
                const m = tag.match(new RegExp(`${attr}=\\s*"([^"]*)"`));
                return m ? m[1] : null;
            };
            blocks.forEach(b => {
                const idMatch = b.match(/<i\s+id="(\d+)"/);
                if (idMatch) {
                    const buffId = parseInt(idMatch[1]);
                    const time = getAttr(b, 'time');
                    const name = getAttr(b, 'name') || '';
                    const type = getAttr(b, 'type');
                    const value = getAttr(b, 'value');
                    const interval = getAttr(b, 'interval');
                    const isdebuff = getAttr(b, 'isdebuff');
                    const overlaytype = getAttr(b, 'overlaytype');

                    buffDurationsMap[buffId] = time ? parseInt(time) : 0;
                    buffsMap[buffId] = {
                        id: buffId,
                        name: name,
                        type: type ? parseInt(type) : 0,
                        time: time ? parseInt(time) : 0,
                        value: value ? parseInt(value) : 0,
                        interval: interval ? parseInt(interval) : 0,
                        isdebuff: isdebuff ? parseInt(isdebuff) : 0,
                        overlaytype: overlaytype ? parseInt(overlaytype) : 0
                    };
                }
            });
        }
        console.log(`[🛡️ BUFFS DB] Inicializado com sucesso! ${Object.keys(buffsMap).length} buffs mapeados no buffsMap.`);
    } catch (e) {
        console.error('[⚠️ ERRO AO CARREGAR BUFF.XML EM SERVER.JS]:', e);
    }

// 8. weapon.xml
    try {
        const pathWeapon = path.join(config.publicDir, 'baxi', 'weapon.xml');
        const weaponContent = fs.readFileSync(pathWeapon, 'utf8');
        
        const iBlockRegex = /<i\s+id="(\d+)"[^>]*>([\s\S]*?)<\/i>/g;
        let match;
        let totalWeaponsLoaded = 0;
        
        while ((match = iBlockRegex.exec(weaponContent)) !== null) {
            const parentId = parseInt(match[1]);
            const blockContent = match[2];
            
            const cartridgeMatch = match[0].match(/cartridgepublic="([^"]*)"/);
            const cartridgePublic = cartridgeMatch ? parseInt(cartridgeMatch[1]) : 30;
            
            weaponsMap[parentId] = {
                cartridgePublic: cartridgePublic
            };
            
            const weaponRegex = /<weapon\s+shootname="([^"]*)"[^>]*>([\s\S]*?)<\/weapon>/g;
            let weaponMatch;
            
            while ((weaponMatch = weaponRegex.exec(blockContent)) !== null) {
                const shootname = weaponMatch[1];
                const weaponBlock = weaponMatch[2];
                
                const launcherRegex = /<launcher\s+[^>]*>/g;
                const launcherMatch = launcherRegex.exec(weaponBlock);
                if (launcherMatch) {
                    const launcherTag = launcherMatch[0];
                    
                    const getAttr = (tag, attr) => {
                        const m = tag.match(new RegExp(`${attr}=\\s*\"([^\"]*)\"`));
                        return m ? m[1] : null;
                    };
                    
                    const damageEnemy = getAttr(launcherTag, 'damageparamenemy');
                    const damageHead = getAttr(launcherTag, 'damagehead');
                    const dampBegin = getAttr(launcherTag, 'damagedampdisbegin');
                    const dampEnd = getAttr(launcherTag, 'damagedampdisend');
                    const dampParm = getAttr(launcherTag, 'damagedampparm');
                    const boomType = getAttr(launcherTag, 'boomdamagetype');
                    const damageForce = getAttr(launcherTag, 'damageforce');
                    const buffAttr = getAttr(launcherTag, 'buff');
                    const buffId = buffAttr ? parseInt(buffAttr) : 0;
                    
                    let boomRange = 0;
                    let cullBack = 0;
                    const bulletRegex = /<bullet\s+[^>]*>/g;
                    const bulletMatch = bulletRegex.exec(weaponBlock);
                    if (bulletMatch) {
                        const bulletTag = bulletMatch[0];
                        const bulletRangeAttr = getAttr(bulletTag, 'bulletboomrange');
                        if (bulletRangeAttr) {
                            boomRange = parseInt(bulletRangeAttr);
                        }
                        const cullBackAttr = getAttr(bulletTag, 'bulletboomcullback');
                        if (cullBackAttr) {
                            cullBack = parseInt(cullBackAttr);
                        }
                    }
                    
                    weaponsMap[parentId][shootname] = {
                        damage: damageEnemy ? parseInt(damageEnemy) : 0,
                        damageHead: damageHead ? parseInt(damageHead) : 0,
                        dampBegin: dampBegin ? parseInt(dampBegin) : 0,
                        dampEnd: dampEnd ? parseInt(dampEnd) : 0,
                        dampParm: dampParm ? parseInt(dampParm) : 0,
                        boomType: boomType ? parseInt(boomType) : 0,
                        boomRange: boomRange,
                        cullBack: cullBack,
                        damageForce: damageForce ? parseInt(damageForce) : 0,
                        buff: buffId
                    };
                    totalWeaponsLoaded++;
                }
            }
        }
        
        // Sobrescrita manual (Brigit 1023 "4" ultimate tick damage)
        if (!weaponsMap[1023]) weaponsMap[1023] = {};
        weaponsMap[1023]["4"] = { damage: 180, damageHead: 0 };
        
        console.log(`[🔫 WEAPONS DB] Inicializado com sucesso! ${Object.keys(weaponsMap).length} heróis/blocos e ${totalWeaponsLoaded} sub-armas mapeadas. (Brigit Ult Overridden)`);
    } catch (e) {
        console.error('[⚠️ ERRO AO CARREGAR WEAPON.XML EM SERVER.JS]:', e);
    }

    // 9. playerface.xml
    try {
        const pathFace = config.playerFaceXmlPath;
        if (fs.existsSync(pathFace)) {
            const faceContent = fs.readFileSync(pathFace, 'utf8');
            const faceMatches = faceContent.matchAll(/<i\s+([^>]+)>/g);
            for (const match of faceMatches) {
                const attribs = match[1];
                const idMatch = attribs.match(/faceid="(\d+)"/);
                if (idMatch) {
                    const id = parseInt(idMatch[1]);
                    if (!allFaceIds.includes(id)) allFaceIds.push(id);
                    
                    let gold = 0, diamond = 0;
                    const unlockMatch = attribs.match(/unlock="([^"]*)"/);
                    if (unlockMatch) {
                        const parts = unlockMatch[1].split('|');
                        for (let i = 0; i < parts.length; i += 2) {
                            if (!parts[i] || !parts[i+1]) continue;
                            const type = parseInt(parts[i]);
                            const val = parseInt(parts[i+1]);
                            if (type === 5) gold = val;
                            if (type === 2) diamond = val;
                        }
                    }
                    faceUnlockPrices[id] = { gold, diamond };
                }
            }
            console.log(`[🎭 FACES DB] Carregados ${allFaceIds.length} avatares do playerface.xml`);
        }
    } catch (e) {
        console.error(`[⚠️ ERRO] Falha ao carregar playerface.xml:`, e);
    }

    // 10. playerpaint.xml
    try {
        const pathPaint = config.playerPaintXmlPath;
        if (fs.existsSync(pathPaint)) {
            const paintContent = fs.readFileSync(pathPaint, 'utf8');
            const paintMatches = paintContent.matchAll(/<i\s+([^>]+)>/g);
            for (const match of paintMatches) {
                const attribs = match[1];
                const idMatch = attribs.match(/id="(\d+)"/);
                if (idMatch) {
                    const id = parseInt(idMatch[1]);
                    if (!allPaintIds.includes(id)) allPaintIds.push(id);
                    
                    let gold = 0, diamond = 0;
                    const udMatch = attribs.match(/unlockdata="([^"]*)"/);
                    if (udMatch) {
                        const parts = udMatch[1].split('|');
                        for (let i = 0; i < parts.length; i += 2) {
                            if (!parts[i] || !parts[i+1]) continue;
                            const type = parseInt(parts[i]);
                            const val = parseInt(parts[i+1]);
                            if (type === 5) gold = val;
                            if (type === 2) diamond = val;
                        }
                    }
                    paintUnlockPrices[id] = { gold, diamond };
                }
            }
            console.log(`[🎨 PAINTS DB] Carregados ${allPaintIds.length} sprays do playerpaint.xml`);
        }
    } catch (e) {
        console.error(`[⚠️ ERRO] Falha ao carregar playerpaint.xml:`, e);
    }

    // 11. herounlock.xml
    try {
        const pathHeroUnlock = config.heroUnlockXmlPath;
        if (fs.existsSync(pathHeroUnlock)) {
            const content = fs.readFileSync(pathHeroUnlock, 'utf8');
            const iBlockRegex = /<i\s+heroid="(\d+)"([^>]*)>([\s\S]*?)<\/i>/g;
            let match;
            let totalWeapons = 0;
            while ((match = iBlockRegex.exec(content)) !== null) {
                const heroId = parseInt(match[1]);
                const heroAttribs = match[2];
                const blockContent = match[3];
                
                // Parse hero unlock price
                let heroGold = 0, heroDiamond = 0;
                const udMatch = heroAttribs.match(/unlockdata="([^"]*)"/);
                if (udMatch) {
                    const parts = udMatch[1].split('|');
                    for (let i = 0; i < parts.length; i += 2) {
                        if (!parts[i] || !parts[i+1]) continue;
                        const type = parseInt(parts[i]);
                        const val = parseInt(parts[i+1]);
                        if (type === 5) heroGold = val;
                        if (type === 2) heroDiamond = val;
                    }
                }
                heroUnlockPrices[heroId] = { gold: heroGold, diamond: heroDiamond };

                // Parse skins
                const skinRegex = /<skin\s+id="(\d+)"([^>]*)/g;
                let skinMatch;
                while ((skinMatch = skinRegex.exec(blockContent)) !== null) {
                    const skinId = parseInt(skinMatch[1]);
                    const skinAttribs = skinMatch[2];
                    let gold = 0, diamond = 0;
                    const skinUdMatch = skinAttribs.match(/unlockdata="([^"]*)"/);
                    if (skinUdMatch) {
                        const parts = skinUdMatch[1].split('|');
                        for (let i = 0; i < parts.length; i += 2) {
                            if (!parts[i] || !parts[i+1]) continue;
                            const type = parseInt(parts[i]);
                            const val = parseInt(parts[i+1]);
                            if (type === 5) gold = val;
                            if (type === 2) diamond = val;
                        }
                    }
                    cosmeticUnlockPrices[`${heroId}_1_${skinId}`] = { gold, diamond };
                }

                // Parse weapons
                const weaponRegex = /<weapon\s+id="(\d+)"([^>]*)/g;
                let weaponMatch;
                while ((weaponMatch = weaponRegex.exec(blockContent)) !== null) {
                    const weaponId = parseInt(weaponMatch[1]);
                    const weaponAttribs = weaponMatch[2];
                    
                    // Grid num
                    const gridNumMatch = weaponAttribs.match(/gridnum="(\d+)"/);
                    if (gridNumMatch) {
                        const gridNum = parseInt(gridNumMatch[1]);
                        heroWeaponGrids[`${heroId}_${weaponId}`] = gridNum;
                        totalWeapons++;
                    }

                    let gold = 0, diamond = 0, arenaPoints = 0;
                    const weaponUdMatch = weaponAttribs.match(/unlockdata="([^"]*)"/);
                    if (weaponUdMatch) {
                        const parts = weaponUdMatch[1].split('|');
                        for (let i = 0; i < parts.length; i += 2) {
                            if (!parts[i] || !parts[i+1]) continue;
                            const type = parseInt(parts[i]);
                            const val = parseInt(parts[i+1]);
                            if (type === 5) gold = val;
                            if (type === 2) diamond = val;
                            if (type === 7) arenaPoints = val;
                        }
                    }
                    cosmeticUnlockPrices[`${heroId}_2_${weaponId}`] = { gold, diamond, arenaPoints };
                }

                // Parse emotions (emotes)
                const emotionRegex = /<emotion\s+id="(\d+)"([^>]*)/g;
                let emotionMatch;
                while ((emotionMatch = emotionRegex.exec(blockContent)) !== null) {
                    const emotionId = parseInt(emotionMatch[1]);
                    const emotionAttribs = emotionMatch[2];
                    let gold = 0, diamond = 0;
                    const emotionUdMatch = emotionAttribs.match(/unlockdata="([^"]*)"/);
                    if (emotionUdMatch) {
                        const parts = emotionUdMatch[1].split('|');
                        for (let i = 0; i < parts.length; i += 2) {
                            if (!parts[i] || !parts[i+1]) continue;
                            const type = parseInt(parts[i]);
                            const val = parseInt(parts[i+1]);
                            if (type === 5) gold = val;
                            if (type === 2) diamond = val;
                        }
                    }
                    cosmeticUnlockPrices[`${heroId}_3_${emotionId}`] = { gold, diamond };
                }
            }
            console.log(`[🎭 HERO UNLOCK DB] Carregados gridnum e preços para heróis e cosméticos.`);
        }
    } catch (e) {
        console.error(`[⚠️ ERRO] Falha ao carregar herounlock.xml:`, e);
    }

    // 12. iteminfo.xml
    try {
        const pathItemInfo = config.itemInfoXmlPath;
        if (fs.existsSync(pathItemInfo)) {
            const xmlContent = fs.readFileSync(pathItemInfo, 'utf8');
            const matches = xmlContent.matchAll(/<i\s+([^>]+)\/>/g);
            for (const match of matches) {
                const attribs = match[1];
                const idMatch = attribs.match(/id="(\d+)"/);
                const nameMatch = attribs.match(/name="([^"]*)"/);
                const typeMatch = attribs.match(/type="(\d+)"/);
                const heroIdMatch = attribs.match(/heroid="(\d+)"/);
                const typeIdMatch = attribs.match(/typeid="(\d*)"/);
                const goldPriceMatch = attribs.match(/glodprice="(\d+)"/);
                const diamondPriceMatch = attribs.match(/diamondprice="(\d+)"/);
                const buffTypeMatch = attribs.match(/bufftype="(\d+)"/);
                const buffBeishuMatch = attribs.match(/buffbeishu="(\d+)"/);
                const buffTimeMatch = attribs.match(/bufftime="(\d+)"/);
                
                if (idMatch) {
                    const id = parseInt(idMatch[1]);
                    GAME_DB.items[id] = {
                        name: nameMatch ? nameMatch[1] : `Item ${id}`,
                        type: typeMatch ? parseInt(typeMatch[1]) : 0,
                        heroid: heroIdMatch ? parseInt(heroIdMatch[1]) : 0,
                        typeid: (typeIdMatch && typeIdMatch[1]) ? parseInt(typeIdMatch[1]) : 0,
                        goldPrice: goldPriceMatch ? parseInt(goldPriceMatch[1]) : 0,
                        diamondPrice: diamondPriceMatch ? parseInt(diamondPriceMatch[1]) : 0,
                        bufftype: buffTypeMatch ? parseInt(buffTypeMatch[1]) : 0,
                        buffbeishu: buffBeishuMatch ? parseInt(buffBeishuMatch[1]) : 0,
                        bufftime: buffTimeMatch ? parseInt(buffTimeMatch[1]) : 0
                    };
                }
            }
            console.log(`[🎒 ITEMS DB] Carregados ${Object.keys(GAME_DB.items).length} itens do iteminfo.xml`);
        }
    } catch (e) {
        console.error(`[⚠️ ERRO] Falha ao carregar iteminfo.xml:`, e);
    }

    // 13. shopdata.xml
    try {
        const pathShopData = config.shopDataXmlPath;
        if (fs.existsSync(pathShopData)) {
            const xmlContent = fs.readFileSync(pathShopData, 'utf8');
            const matches = xmlContent.matchAll(/<i\s+([^>]+)\/>/g);
            for (const match of matches) {
                const attribs = match[1];
                const idMatch = attribs.match(/id="(\d+)"/);
                const itemIdMatch = attribs.match(/itemid="(\d+)"/);
                if (idMatch && itemIdMatch) {
                    const id = parseInt(idMatch[1]);
                    const itemId = parseInt(itemIdMatch[1]);
                    GAME_DB.shop[id] = { itemId: itemId };
                }
            }
            console.log(`[🛒 SHOP DB] Carregados ${Object.keys(GAME_DB.shop).length} produtos do shopdata.xml`);
        }
    } catch (e) {
        console.error(`[⚠️ ERRO] Falha ao carregar shopdata.xml:`, e);
    }

    // 14. Mapeamento de Bases e Barreiras Físicas (mapeffect.xml + fbset.xml)
    try {
        const pathFbset = path.join(config.publicDir, 'baxi', 'fbset.xml');
        const pathMapEffect = path.join(config.publicDir, 'baxi', 'mapeffect.xml');
        
        if (fs.existsSync(pathFbset) && fs.existsSync(pathMapEffect)) {
            const fbsetContent = fs.readFileSync(pathFbset, 'utf8');
            const mapeffectContent = fs.readFileSync(pathMapEffect, 'utf8');
            
            // A) Carrega detalhes de todas as bases de mapeffect.xml
            const basesMap = {};
            const effectMatches = mapeffectContent.matchAll(/<i\s+([^>]+)>/g);
            for (const match of effectMatches) {
                const attribs = match[1];
                const idMatch = attribs.match(/id="(\d+)"/);
                if (idMatch) {
                    const id = parseInt(idMatch[1]);
                    const xMatch = attribs.match(/x="(-?\d+)"/);
                    const yMatch = attribs.match(/y="(-?\d+)"/);
                    const zMatch = attribs.match(/z="(-?\d+)"/);
                    const modelMatch = attribs.match(/model="([^"]*)"/);
                    const shiliMatch = attribs.match(/shili="(\d+)"/);
                    const nameMatch = attribs.match(/name="([^"]*)"/);
                    
                    const name = nameMatch ? nameMatch[1] : '';
                    const shili = shiliMatch ? parseInt(shiliMatch[1]) : (name.includes('二') || name.includes('Red') || name.includes('def') || name.includes('防守') ? 2 : 1);
                    
                    if (xMatch && yMatch && zMatch) {
                        basesMap[id] = {
                            id: id,
                            x: parseInt(xMatch[1]),
                            y: parseInt(yMatch[1]),
                            z: parseInt(zMatch[1]),
                            model: modelMatch ? modelMatch[1] : 'boxjidi',
                            shili: shili,
                            name: name
                        };
                    }
                }
            }
            
            // B) Associa as bases a cada mapa com base no atributo jidis de fbset.xml
            const fbsetMatches = fbsetContent.matchAll(/<i\s+([^>]+)>/g);
            for (const match of fbsetMatches) {
                const attribs = match[1];
                const idMatch = attribs.match(/id="(\d+)"/);
                const jidisMatch = attribs.match(/jidis="([^"]*)"/);
                
                if (idMatch && jidisMatch && jidisMatch[1].trim()) {
                    const mapId = parseInt(idMatch[1]);
                    const jidis = jidisMatch[1].split('|').map(Number).filter(Boolean);
                    
                    mapBases[mapId] = [];
                    jidis.forEach(baseId => {
                        if (basesMap[baseId]) {
                            mapBases[mapId].push(basesMap[baseId]);
                        }
                    });
                }
            }
            console.log(`[🏰 BASE SHIELDS DB] Carregados mapeamentos de bases para ${Object.keys(mapBases).length} mapas do fbset.xml/mapeffect.xml`);
        }
    } catch (e) {
        console.error(`[⚠️ ERRO] Falha ao mapear bases de mapeffect.xml/fbset.xml:`, e);
    }
}

// Helpers baseados na base de dados carregada
const getHeroMaxHp = (heroId) => {
    const hpMap = {
        1003: 1000, // Alice (Diana)
        1005: 1000, // Glider (Leizhenzi)
        1006: 2000, // Tagaloa (XingTian)
        1015: 1000, // Lars / Terminator
        1016: 1250, // Valkyries
        1017: 1000, // Celina / Mulan
        1019: 1800, // Guanyin
        1021: 1000, // Carmen / Taliya
        1022: 1250, // Scarlett / Xuenv
        1023: 1250, // Brigit / Zhurong
        1024: 1000, // Shogun / Houyi
        1025: 1000, // Eva / Linglong
        1026: 1000, // Dragos / Edward
        1027: 1500, // Jack / Yinglong
        1028: 1750, // Loki / Lokji
        1029: 1000, // Angel / Lolita
        1030: 1000, // Vincent / Jingke
        1031: 1000, // Johnny / Lanbo
        1032: 1000, // Venus / Daji
        1033: 1000, // Lily / Lilith
        1034: 2200, // Khan / Huangzhong
        1035: 1250, // Nezha
        1036: 1000, // Gaya / Medusa
        1037: 2500, // David
        2000: require('./lamster').MAX_HP // Lamster: suporte de cura em área
    };
    return hpMap[heroId] || 2000;
};

const generateHeroDazhaoData = (heroId, currentEnergy = 0) => {
    const list = heroDazhaoMap[heroId] || [];
    if (list.length === 0) {
        return '()';
    }
    const elements = list.map(dz => `(${encodeInt(dz.id)},${encodeInt(currentEnergy)})`);
    return `(0,(${elements.join(',')}))`;
};

const generateHeroSkillsData = (heroId) => {
    const one = encodeInt(1);
    const skills = heroSkillsMap[heroId] || [];
    const skillList = [];
    for (const skId of skills) {
        let skConfig = playerSkillsMap[skId];
        if (groupSkillsOverrides[skId]) {
            skConfig = Object.assign({}, skConfig, groupSkillsOverrides[skId]);
        }
        const maxCharges = (skConfig && skConfig.maxSkillNumber > 1) ? skConfig.maxSkillNumber : 1;
        const longtime = (skConfig && skConfig.longtime) ? skConfig.longtime : 0;
        const cd = (skConfig && skConfig.cd) ? skConfig.cd : 0;
        const chargesEnc = encodeInt(maxCharges);
        const longtimeEnc = encodeInt(longtime);
        const cdEnc = encodeInt(cd);
        skillList.push(`(${encodeInt(skId)},0,${chargesEnc},0,${one},0,0,0,0,${longtimeEnc},${cdEnc})`);
    }
    return `(${skillList.join(',')})`;
};

const getPartsGrid = (heroId, weaponId, profile) => {
    const key = `${heroId}_${weaponId}`;
    const gridNum = heroWeaponGrids[key] !== undefined ? heroWeaponGrids[key] : 0;
    if (gridNum <= 0) {
        return '()';
    }
    let slots = [];
    const wp = profile && profile.weaponParts && profile.weaponParts[heroId] && profile.weaponParts[heroId][weaponId];
    for (let s = 0; s < gridNum; s++) {
        const partId = wp && wp[s];
        if (partId) {
            slots.push(`(${encodeInt(1)},${encodeInt(partId)})`);
        } else {
            slots.push(`(${encodeInt(0)},${encodeInt(0)})`);
        }
    }
    return `(${slots.join(',')})`;
};

const generateFullHeroData = (id, useSkinId = 1, useWeaponId = 1, useEmoteId = 1, usePoseId = 0, profile = null) => {
    const one = encodeInt(1);
    const two = encodeInt(2);
    const zero = encodeInt(0);
    const dateStr = Fun.GetServerDate();
    const idEnc = encodeInt(id);
    
    // Validador de desbloqueio para cosméticos
    const isUnlocked = (type, index) => {
        // Cosméticos padrão/iniciais estão sempre liberados
        if (type === 'skin' && index === 1) return true;
        if (type === 'weapon' && index === 1) return true;
        if (type === 'emote' && index === 1) return true;
        if (type === 'pose' && index === 0) return true;

        // Detectar se é item de evento/conquista (sem preço no XML ou preço Gold/Diamond/ArenaPoints = 0)
        let typeNum = 1;
        if (type === 'skin') typeNum = 1;
        else if (type === 'weapon') typeNum = 2;
        else if (type === 'emote') typeNum = 3;
        else if (type === 'pose') typeNum = 4;
        
        const priceKey = `${id}_${typeNum}_${index}`;
        const price = cosmeticUnlockPrices[priceKey];
        
        // Se o preço não existir nas tabelas de loja (preço === undefined),
        // ou se for explicitamente 0 em Gold, Diamond e ArenaPoints, então é item de evento e é liberado
        if (!price || (price.gold === 0 && price.diamond === 0 && (!price.arenaPoints || price.arenaPoints === 0))) {
            return true;
        }

        if (!profile) return false; // Sem perfil (fallback/bot), não libera cosméticos avançados
        
        // Verifica unlockedCosmetics no perfil
        if (!profile.unlockedCosmetics) profile.unlockedCosmetics = {};
        if (!profile.unlockedCosmetics[id]) {
            profile.unlockedCosmetics[id] = { skins: [1], weapons: [1], emotes: [1], poses: [0] };
        }
        
        const uc = profile.unlockedCosmetics[id];
        if (type === 'skin') return uc.skins && uc.skins.includes(index);
        if (type === 'weapon') return uc.weapons && uc.weapons.includes(index);
        if (type === 'emote') return uc.emotes && uc.emotes.includes(index);
        if (type === 'pose') return uc.poses && uc.poses.includes(index);
        return false;
    };

    // Validador de desbloqueio para o Herói em si
    const isHeroUnlocked = () => {
        if (id === 1015) return true; // herói inicial Lars sempre liberado
        
        // Verifica se é herói de evento (preço gold e diamond zerados no XML)
        const price = heroUnlockPrices[id];
        if (price && price.gold === 0 && price.diamond === 0) {
            return true; // Heróis de evento são liberados automaticamente
        }

        if (!profile) return false; // Sem perfil, heróis avançados ficam bloqueados
        
        if (!profile.unlockedHeroes) profile.unlockedHeroes = [1015];
        return profile.unlockedHeroes.includes(id);
    };

    let skinList = [];
    let weaponList = [];
    let emoteList = [];
    let poseList = [];
    for (let i = 1; i <= 15; i++) {
        const skinLock = isUnlocked('skin', i) ? two : zero;
        const weaponLock = isUnlocked('weapon', i) ? two : zero;
        const emoteLock = isUnlocked('emote', i) ? two : zero;
        const poseLock = isUnlocked('pose', i) ? two : zero;
        
        skinList.push(`${encodeInt(i)},(${encodeInt(i)},${skinLock},${zero},${zero},'${dateStr}',${zero})`);
        weaponList.push(`${encodeInt(i)},(${encodeInt(i)},${weaponLock},${zero},${zero},'${dateStr}',${zero},())`);
        emoteList.push(`${encodeInt(i)},(${encodeInt(i)},${emoteLock},${zero},${zero},'${dateStr}',${zero})`);
        poseList.push(`${encodeInt(i)},(${encodeInt(i)},${poseLock},${zero},${zero},'${dateStr}',${zero})`);
    }
    const unlockedSkin = `(${skinList.join(',')})`;
    const unlockedWeapon = `(${weaponList.join(',')})`;
    const unlockedEmote = `(${emoteList.join(',')})`;
    const unlockedPose = `(${poseList.join(',')})`;
    
    const openState = isHeroUnlocked() ? two : zero;
    
    const useSkinEnc = encodeInt(Math.max(1, parseInt(useSkinId) || 1));
    const useWeaponEnc = encodeInt(Math.max(1, parseInt(useWeaponId) || 1));
    const useEmoteEnc = encodeInt(Math.max(1, parseInt(useEmoteId) || 1));
    const usePoseEnc = encodeInt(parseInt(usePoseId) || 0);
    return `${idEnc},(${one},${zero},${openState},'${dateStr}',${zero},(),${unlockedSkin},${unlockedWeapon},${unlockedEmote},${unlockedPose},${zero},${useSkinEnc},${useWeaponEnc},${useEmoteEnc},${usePoseEnc},${zero})`;
};

function getEntityClassName(npcIndex) {
    if (npcIndex === 1019) return 'Radiobomb_Entity';
    if (npcIndex === 1007 || npcIndex === 1025 || npcIndex === 1005 || npcIndex === 1011 || npcIndex === 1012 || npcIndex === 1022) return 'Area_Entity';
    if (npcIndex === 1021) return 'Sentry_Entity';
    if (npcIndex === 1021 || npcIndex === 1023 || npcIndex === 1024) return 'Trap_Entity';
    if (npcIndex >= 1026 && npcIndex <= 1035) return 'Sentry_Entity';
    
    if (npcIndex >= 1101 && npcIndex <= 1130) {
        return 'Idiocy_Entity';
    }

    const npcConf = npcData[npcIndex] || { type: 1 };
    const type = parseInt(npcConf.type) || 1;
    
    switch (type) {
        case 1: return 'Sentry_Entity';
        case 2:
        case 3:
        case 13: return 'Landmine_Entity';
        case 4: return 'Chariot_Entity';
        case 5: return 'Elevator_Entity';
        case 6: return 'Timebomb_Entity';
        case 8: return 'Steerable_Entity';
        case 9:
        case 14: return 'Trap_Entity';
        case 11: return 'Main_Transmission_Entity';
        case 12: return 'Sub_Transmission_Entity';
        default: return 'Npc_Entity';
    }
}

// MOCK DB: Simulação do shopdata.xml e iteminfo.xml (será preenchido dinamicamente)
const GAME_DB = {
    shop: {},
    items: {}
};

const isFaceUnlocked = (faceId, profile) => {
    if (faceId === 2009) return true; // Foto padrão
    const price = faceUnlockPrices[faceId];
    if (!price || (price.gold === 0 && price.diamond === 0)) {
        return true; // Avatares de evento são grátis/desbloqueados
    }
    if (!profile) return false;
    if (!profile.unlockedFaces) profile.unlockedFaces = [2009];
    return profile.unlockedFaces.includes(faceId);
};

const isPaintUnlocked = (paintId, profile) => {
    if (paintId === 1019) return true; // Spray padrão
    const price = paintUnlockPrices[paintId];
    if (!price || (price.gold === 0 && price.diamond === 0)) {
        return true; // Sprays de evento são grátis/desbloqueados
    }
    if (!profile) return false;
    if (!profile.unlockedSprays) profile.unlockedSprays = [1019];
    return profile.unlockedSprays.includes(paintId);
};

const getHeroName = (heroId) => {
    const cosm = heroCosmeticsMap[heroId] || heroCosmeticsMap[String(heroId)];
    if (cosm && cosm.name) return cosm.name;
    const HERO_NAMES = {
        1001: "Xing Tian", 1002: "D.Va", 1003: "Diana", 1004: "Mulán", 1005: "Hou Yi",
        1006: "Lei Zhenzi", 1007: "Medusa", 1008: "Talia", 1009: "Rambo", 1011: "Guan Yin",
        1012: "Pharaoh", 1013: "Ne Zha", 1014: "Jing Ke", 1015: "Lars", 1017: "Da Wei",
        1020: "Reinhardt", 1021: "Zhu Rong", 1023: "Brigit"
    };
    return HERO_NAMES[heroId] || `Herói ${heroId}`;
};

module.exports = {
    initializeDatabase,
    filesVersionMap,
    npcData,
    heroCosmeticsMap,
    heroSkillsMap,
    heroDazhaoMap,
    playerSkillsMap,
    buffDurationsMap,
    buffsMap,
    weaponsMap,
    groupSkillsOverrides,
    getHeroMaxHp,
    getHeroName,
    generateHeroDazhaoData,
    generateHeroSkillsData,
    generateFullHeroData,
    getPartsGrid,
    MAP_CONFIG,
    getEntityClassName,
    GAME_DB,
    allFaceIds,
    allPaintIds,
    faceUnlockPrices,
    paintUnlockPrices,
    heroUnlockPrices,
    cosmeticUnlockPrices,
    isFaceUnlocked,
    isPaintUnlocked,
    mapBases
};

