# Estado resumido

Atualizado: 2026-10-22. Responsável pelo registro: Codex.

## Fontes e interpretação

- Código atual e execução observada têm precedência sobre resumos antigos. `project_summary.md` é contexto histórico, não prova de conclusão.
- `Assembly/Assembly-CSharp/` contém C# decompilado e é a principal referência da lógica do cliente disponível aqui. Não equivale, por si só, ao projeto Unity original completo; conferir se corresponde ao binário usado pelo jogador.
- Oracle consultada em modo somente leitura na auditoria de 26/09: hashes de socketServer.js/gameLogic.js iguais aos locais, logs coletados. Instalação do Lamster abaixo continua sendo registro da sessão anterior.

## Mapa para buscas direcionadas

| Assunto | Ponto de partida |
| --- | --- |
| Inicialização efetiva | `package.json` → `src/index.js` |
| Protocolo e conexões | `src/socketServer.js`, `src/protocol.js`, `src/utils.js` |
| Escopo de sala/cena e fluxo TCP | `src/sceneScope.js`, `src/clientStream.js` |
| Partidas e bots | `src/gameLogic.js` |
| Lamster suporte, ID 2000 | `src/lamster.js`, `tests/lamster.test.js` |
| Dados entregues ao cliente | `public/baxi/`, `zh/`, manifesto `files_dir.xml` |
| Entrada tardia no cliente | `BaseSocket.cs`, `HeroPlayer.cs`, `GamePlayer.cs`, `MatchManager.cs`, `PlayerCommon.cs` dentro de Assembly/Assembly-CSharp |
| Diagnóstico existente | `src/logger.js`, `src/packetSniffer.js`, `src/http.js` |

## Partidas

- Correções de entrada tardia e ciclo de cena registradas como instaladas na Oracle em 11/09. Detalhes: `docs/partidas-jip-2026-09-11.md`.
- Validação com dois clientes reais ainda pendente; o relato original envolvia retorno ao lobby mantendo estado de partida.
- Control e PvE ainda precisam de verificação de isolamento dos controladores globais. Não declarar todos os modos concluídos.
- Relato corrigido pelo usuário em 29/09: quem ENTRA na partida em andamento fica preso na seleção de herói; não consegue escolher e o fim do contador não avança. Substitui a hipótese anterior de queda dos jogadores já presentes.
- Auditoria de 26/09: `ai/reviews/matchmaking-2026-09-26.md`. Os 21 testes existentes passaram; diagnóstico adicional encontrou 16 invariantes violadas em 17 verificações. Nenhuma correção de execução aplicada nesta auditoria.
- Teste real de 29/09 analisado: Oracle permanece no hash anterior ao patch; king2 recebe confirmado=1 e envia oito desbloqueios0 sem avançar. Sessões king/king2 distintas apesar do mesmo IP. Evidência: `ai/reviews/jip-client-log-2026-09-29.md`.
- Correção local de 29/09: seleção pendente no JIP, retorno de prévia para quem carrega, confirmação inicial/carregamento e tratamento de 90004 ao expirar contador. 29 testes passaram também na Oracle; instalada em 29/09 às 22:34 BRT (30/09 01:34 UTC), usuário confirmou resolução do travamento de seleção no cliente real. Passagem: `ai/reviews/jip-selection-2026-09-29.md`.
- Prioridades demonstradas: fila antiga transfere novamente participante já admitido; redirecionamento sobrevive à saída; herói é publicado antes do mapa pronto; carregamento pendente recria controlador encerrado. Causa exata da queda atual ainda exige captura com dois clientes.

## Lamster

- Implementado como suporte. Código local confirmado em 26/09: HP 1400; cura primária 180/1s; secundária 360/5s; raios nominais de 6m/8m com tolerância do servidor. Cura apenas aliados vivos na mesma cena; não cura a si diretamente. Retorno de cura limitado a 45 por aliado; soco mantido.
- Sessão anterior registrou instalação em 26/09, 21 testes aprovados e verificação HTTP/TCP. Não foram repetidos nesta tarefa de documentação.
- Falta validar animação, área real dos ataques, cura e ausência de dano em aliados/inimigos com os clientes usados pelos jogadores.
- Backup remoto registrado: `/home/ubuntu/backups/lamster-20260926-preinstall.tar.gz`; backup local anterior: `backups/lamster-20260912-before/`.

## Verificações disponíveis

- 
ode --test tests/logging.test.js tests/lamster.test.js tests/match-lifecycle.test.js`
- 
ode --check <arquivo.js>` para os arquivos modificados.
- 
pm test` ainda é um comando placeholder que falha: não usá-lo como prova de regressão.
- Logs agora persistem entre reinícios com rotação; não importar a aplicação apenas para inspecionar dados, pois isso inicia serviços. Consulta: 
ode tools/trace-player.cjs --player NOME`.

## Infraestrutura

- Servidor hospedado na Oracle; processo PM2 informado: `triggerun`, pasta `/home/ubuntu/server-tr`.
- Não instalar alterações só porque os testes passaram: respeitar o escopo autorizado pelo usuário e verificar alterações locais/remotas concorrentes.
- Coordenação e auditoria acrescentaram documentação e ferramenta isolada de diagnóstico. Não alteraram o jogo, o logger ou a execução na Oracle.

- Instalação JIP autorizada e concluída: backup remoto `/home/ubuntu/backups/jip-selection-20260930T013437Z/` (runtime e logs). Hash socketServer `fcca2443b481c4d785b79219918b751c72d3ae28fd513efefe046a5aca9c2ed2`; PM2 online e TCP7777 externo confirmado.

- TDM-003 instalado em 30/09 01:52 UTC: saída em qualquer ordem Hall/Scene confirma retorno e remove personagem; slots/times coerentes e entrada tardia equilibrada. 35 testes locais/Oracle aprovados, HTTP/TCP OK; teste real desta atualização pendente. Ver `ai/reviews/tdm-exit-teams-20260930.md`.

- LIFE-001/OBS-001 instalados em 03/10 23:45 BRT: ciclo principal explícito e telemetria por conta/conexão/sala/fase, logs persistentes com rotação. 44 testes local/Oracle, HTTP/TCP OK; cliente real pendente. Passagem `ai/reviews/lifecycle-logs-20261003.md`; contrato/uso `docs/match-lifecycle-and-logs.md`.

- HIT-001 instalado em 04/10 05:54 BRT: leitura SDG corrigida para todas as armas na Oracle; DLL local com busca de mira independente por máscara e deslocamento após colisores ignorados. 53 testes local/Oracle; cliente real pendente, não declarar hitbox resolvida. Backup e hashes em `ai/reviews/hit-celina-20261004.md`.

- HIT-001 não resolveu todo o relato real: Eva→Celina lateral/perto ainda falha. Fonte pura fornecida em `C:/trig2/GAME/Dumps/assembly`; mudanças antigas inseriram cápsula de movimento nas consultas de tiros. HIT-002 instalado somente no cliente: filtro sem salto de 10cm e sem alterar física, DLL auxiliar obrigatório. 9 verificações simuladas; usuário relatou melhora aparente no cliente em 04/10 (validação inicial, cobertura ampliada pendente). `ai/reviews/hit-regression-20261004.md`.

- Individual FFA-001/002 instalado em 04/10 20:06 BRT: 20 kills/600s, 7 mapas 5301–5307, spawn/respawn/resultado por sala e matchmaking separado. Corrigidos tipo ainda 7 após staging (90009), equipes visuais por destinatário e menu oculto (MatchModel isopen=1/cache100004). 70/70 testes locais/Oracle; relato real da versão inicial: reentrada funcionava, primeira entrada com equipes. Versão final requer reiniciar clientes e validar. Passagem ai/reviews/free-for-all-20261004.md.

- Aventura PVE-001 instalado em 06/10 17:43 BRT: bots RobotAI com modelos/rotas nativos, autoridade única e transferência, sync/combate por sala; mapas 5105/5115 corrigidos para tipos9/12 e cache100005. 81/81 testes local/Oracle; cliente real pendente, exige reiniciar clientes. Mantém protótipo de3ondas, não campanha/recompensas original. Ver ai/reviews/adventure-bots-20261006.md.

- PVE-002 instalado06/10 17:49BRT: relato real doPVE-001 travou antes da partida; bots ainda não validados. Fila Aventura agora usa seleção5010/lock antes de90009+CLoadScene, remove loading automático imediato.85/85 testes locais/Oracle; cliente real pendente. ai/reviews/adventure-loading-20261006.md.

- ADMIN-001 instalado07/10 03:06UTC: catálogo803 definições (180modelos/58NPCs/565bots), busca, spawn por cena e limpeza de testes; IA nativa opcional sem ondas/HUD.90/90 testes local/Oracle e navegador OK; cliente real pendente. ai/reviews/admin-catalog-20261007.md.

- MODEL-001 instalado07/10 03:25UTC: model swap experimental de monstros por NVM negativo/robot.xml, FPS/arma originais; patch em3métodos + TriggerunModelSwap.dll no cliente local, admin Oracle atualizado.93 testes servidor/7 simulados cliente; Unity pendente. ai/reviews/model-swap-20261007.md.

- HUD-001 instalado07/10 21:52UTC: F2 configura mira local/persistente (cruz/ponto, cor, tamanho/gap), feedback nativo de kill/HS após confirmação e weapon/launcher preservados para falas. Cliente+Oracle,95 testes servidor/7 simulados feedback; Unity pendente. ai/reviews/crosshair-feedback-20261007.md.

- HUD-001: usuário confirmou mira/feedback no cliente real; falas sem melhora. VOICE-001 instalado07/10 22:05UTC: respawn nativo sem recriar personagem, HP e munição mantidos;96 testes local/Oracle. Falas/respawn Unity pendentes. ai/reviews/voices-respawn-20261007.md.

- VOICE-001 renascimento confirmado pelo usuário. VOICE-002/LAMSTER-002 instalado07/10 22:24UTC: falas usam shootname, Lamster na seleção, esquerdo120 dano/180 cura(2,2m/1s), direito360 cura(8m/5s), HUD local.99 testes local/Oracle; Unity pendente. ai/reviews/lamster-voices-20261007.md.

- LAMSTER-002 teste real travou por HeroAnimator/.mo: material vazio no cadastro. LAMSTER-003 cliente instalado: filtra materiais vazios apenas2000,6 testes simulados/1 método alterado. Oracle sem mudança; novo teste Unity pendente. ai/reviews/lamster-loading-20261007.md.

- LAMSTER-004 instalado07/10 19:58BRT: secundário8m cura360/buff1007 self+aliados(+25%/5s)/250 dano inimigos,CD6s;falas nativas1007 para6classes.101 testes locais/Oracle+6simulados áudio;Unity pendente. ai/reviews/lamster-rally-20261007.md.

- LAMSTER-005 instalado07/10 23:25UTC: retratos/ícones exclusivos via12URLs PNG; dano em NPC lê x/y/z, oclusão secundário filtra cápsulas auxiliares somente2000.104 testes servidor+5simulados colisão;14.780 métodos comparados/1 alterado. HTTP200/TCP7777 OK; Unity pendente. ai/reviews/lamster-icons-damage-20261007.md.

- SKIN-002 instalado07/10 23:59UTC: protótipo físicoLamster TPS/FPS, malhas mais musculosas com rig/pesos/texturas/animações/hitbox intactos; recursoscrc26100706. Bundles reabertos/300objetos comparados, geometria sem inversões eHTTP hashesOK. Sem mudançasDLL/reinícioOracle; testeUnity pendente. ai/reviews/lamster-shape-20261007.md.

- SKIN-002 confirmado pelo usuário: aparência e gameplay normais. SKIN-003 instalado07/10 21:15BRT: peitoral refinado emTPS/FPS, recursos26100707, rig/hitbox intactos;300objetos/6malhas eHTTP verificados, sem reinício. Teste real da revisão pendente. ai/reviews/lamster-chest-20261007.md.

- SKIN-003 rejeitado por volume exagerado. SKIN-004 instalado07/10 21:25BRT: peito reduzido, textura manual512/DXT5 com definição suave (autorizada após falhaimagegen), TPS26100709/FPS26100708;rig/hitbox preservados. Geometria/serialização/renderUV/HTTP verificados, sem reinício. Unity pendente. ai/reviews/lamster-texture-20261007.md.

- SKIN-004: usuário aprovou aparência. SKIN-005 instalada08/10 00:37UTC: Celina Aurora experimental substitui skin/arma clássicas, roupa navy/turquesa e arma combinando;6bundles/CRC26100710,1541objetos/HTTP verificados, rig/gameplay intactos, sem reinício. Cliente real pendente. ai/reviews/celina-aurora-20261007.md.

- SKIN-005 aprovada pelo usuário. SKIN-006 instalada08/10 00:49UTC: Celina clássica restaurada, Aurora independente skin6/arma5 gratuitas; jaqueta ajustada/mangas/calça, cincobundles comCAB exclusivos+ícones, manifesto26100711. Rig/hitbox preservados; catálogo/geometria/16HTTP/TCP OK; reinício autorizado. Cliente real pendente. ai/reviews/celina-aurora-independent-20261007.md.

- SKIN-006 rejeitada pelo usuário por pintura sem roupa real. SKIN-007 instalada08/10 00:44BRT: Aurora6 com jaqueta3D independente(3393vértices/5544faces novos,25partes,62bones), segunda submesh/material, efeitos preservados;CRC26100801. Estrutura/3poses sintéticas/HTTP/TCP OK; cliente real pendente. Clássica intacta;sem reinício. ai/reviews/celina-aurora-model-20261008.md.

- SKIN-007: usuário rejeitou acabamento da textura. SKIN-008 candidato comUVs16áreas e texturaimagegen refinada; posições/normais/pesos idênticos, validação1211objetos/3hashesOK. Prévia para avaliação antes de instalar;Oracle permanece007. ai/reviews/celina-aurora-texture-20261008.md.

- SKIN-009 candidato: geometriaAurora refinada sobretextura008, bolsos arredondados/espessuras reduzidas/mangas suaves;7284vértices/12910faces roupa. SóMesh diferente em1211objetos; testesestrutura/3poses sintéticasOK. Prévia aguardando avaliação, Oracle permanece007 semalteração. ai/reviews/celina-aurora-refined-20261008.md.

- SKIN-010 candidato:38faces do colarinho antigo removidas, cabeça de manga arredondada sem reforço elevado e junção da gola fechada. SóMesh diferente de009;62bones/3poses sintéticas/3hashesOK. Prévia aguardando avaliação;Oracle permanece007 intacta. ai/reviews/celina-aurora-shoulders-20261008.md.

- SKIN-012 candidataScarlet no rigCelina62bones:17494v/30704tri,corpo/cabelo semkatana,shader toon;1209objetos/3poses/3hashesOK. Sem física cabelo/PBR completo. Prévia aguardando avaliação;sem cadastro ou instalação,Oracle permanece007. ai/reviews/scarlet-celina-candidate-20261008.md.

- SKIN-012 rejeitada por distorção. SKIN-013 candidata preserva torso/pernas/cabeça com escala uniforme1,07 e ajusta alcance braços para mãosCelina. Proporções/rig/3poses/execução verificados;Unity pendente. Prévia,Oracle intacta007. ai/reviews/scarlet-proportions-20261008.md.

- SKIN-014 instaladaOracle08/10 20:30BRT:CelinaScarlet Proxy7 gratuita,candidata013/TPS exclusivo/ícone/CRC26100802;FPS nativo,arma selecionada original. Backups/catálogo/7HTTP/TCP/PM2OK;reinício sem jogadores. Clássica/Aurora intactas. TesteUnity pendente. ai/reviews/scarlet-installed-20261008.md.

- SKIN-014 confirmada pelo usuário funcional,animaçõesestranhas masaceitáveis. SKIN-015 íconeScarlet circularilustrado semtextoRGBA256/CRC26100804Oracle;helperCombatHudlocal ajustaaspectoHUDsomenteScarlet. Backup/compilação/24métodos/3HTTPOK;Unitynovoíconependente. ai/reviews/scarlet-portrait-20261008.md.

- SKIN-017 instalada08/10 21:02BRT: Lamster Hércules skin2 gratuita/TPS independente/retrato circular/CRC26100805; hook cliente2000 respeita skin equipada. Duas DLLs locais, Oracle/catálogo/6HTTP/TCP OK; zero conexões ao reiniciar. Gameplay/FPS/default preservados; Unity pendente. ai/reviews/hercules-skin-20261008.md.

- SKIN-017 relato real: Hércules funciona, com imperfeições visuais toleradas pelo usuário. SKIN-019 instalada08/10 21:26BRT:Gaya Rider5 gratuita/CRC26100806,64bones/3LODs/retrato circular/helper local; peso criado a partir da Gaya, cabelo cabeça/tronco sem física. 976objetos/3poses/29métodos/6HTTP/manifesto/TCP OK; Unity pendente. ai/reviews/rider-gaya-skin-20261008.md.

- VALK-001 cliente instalado12/10/2026: primário físico filtra cápsulas auxiliares; Slay ignora contato da arma física e registra zona/colisor. 11novos+9HIT-002 simulados/14780métodos3alterados; backup/hashesOK. Relato melhora na Base Profunda; teste da correção no Mausoleum pendente. Oracle intacta. ai/reviews/valk-melee-slay-20261008.md.

- SKIN-021 instalada12/10~21:25BRT:CelinaExecutiva8gratuita/TripoTPS62bones/retratocircular/CRC26101201;helperHUDlocal somenteaspecto novoícone,diagnósticoSKIN-020preservado. Backup/catálogo/6HTTP/TCP/PM2OK;reinícioautorizado1conexão. Unitypendente/lentesopacas. ai/reviews/celina-red-installed-20261012.md.

- SKIN-022 instalada12/10~21:41BRT:FPSExecutiva8 exclusivo com mãosskin3/mangasvermelhas/punhosbrancos/relógio,CRC26101202;40bones/926vérticesdedospreservados/6HTTP/TCP/PM2OK. Reinícioautorizado1conexão;semDLL. Cliente realpendente. ai/reviews/celina-executiva-fps-20261012.md.

- SKIN-022 rejeitada no cliente: braços invisíveis/mãos visíveis. SKIN-023 substitui FPSExecutiva8 pororiginalskin3 sempulseira, semrelógio/mangasextras;somenteíndicesmeshalterados/40bonesintactos. Oracle12/10~21:50BRT CRC26101203/4HTTP/TCP/PM2OK;reiníciozeroconexões;Unitypendente. ai/reviews/celina-fps-simple-20261012.md.

- SKIN-023 confirmada pelo usuário: FPSExecutiva sempulseira aprovado no cliente real.

- SKIN-024 instalada12/10~22:22BRT:VenusGuardiãdeJade5gratuita/TPS79bones/cauda5bones/148UVsrootcorrigidos/CRC26101204;LODsusammeshcompleta21759tri/FPSíconeoriginais. Backup/catálogo/6HTTP/TCP/PM2OK;reiníciozeroconexões;Unitypendente. ai/reviews/venus-jade-installed-20261012.md.

- SKIN-025 instalada12/10~22:30BRT após relato024braços/ombros/pescoçoruins:pesosregionaiscontínuos/cabeçaacimaqueixo;geometriaUVrigpreservados.6posessintéticas/miramax6,31→1,673/4HTTP/TCP/PM2OK/CRC26101205;reiníciozeroconexões. Unitypendente. ai/reviews/venus-upperbody-20261012.md.

- SKIN-026 instalada12/10~22:39BRT:025continuavaestranhoparada/emmovimento; encaixegeométricoombros e pescoçodepth+4cm/clavícula+2,2cm;cauda6cmparadentrocomformaUVpesospreservados.6posessintéticas/4HTTP/TCP/PM2OK/CRC26101206;zeroconexões/Unitypendente. ai/reviews/venus-geometry-fit-20261012.md.

- SKIN-026: usuário considera resultado aceitável, ainda imperfeito; cauda/encaixe tolerados. Próxima skin solicitada: Nezha baseado em retratoRPG.

- SKIN-027 referências NezhaRPG entregues:4PNG transparentes+ZIP emcandidate-assets/nezha-rpg; usuário decidiu preservar braços originais da ultimate. AguardaGLBTripo; jogo/Oracle intactos. ai/reviews/nezha-rpg-multiview-20261012.md.

- SKIN-028 instalada12/10~23:30BRT:NezhaSoberanoÍgneo5gratuita/TripoTPS62bones/rostoretocado/braçosalongados/CRC26101207;ultimate eFPSoriginais.7poses/8HTTP/TCP/PM2OK;restart1conexãoautorizado. Unitypendente/íconeoriginal/LODsmeshcompleta. ai/reviews/nezha-rpg-installed-20261012.md.

- SKIN-029 instalada12/10 23:52BRT: usuário comparou028 com original; tórax/braços ampliados, placas da cintura simétricas/compactadas e pesos das coxas corrigidos. Cabeça/mãos/textura/FPS/ultimate preservados. Oito poses simuladas/4HTTP/TCP aprovados; CRC26101208, sem reinício. Nova versão Unity pendente. Candidata atual `candidate-assets/nezha-rpg/robust/`; passagem `ai/reviews/nezha-robust-20261012.md`.

- SKIN-029 confirmada pelo usuário em13/10: resultado considerado muito bom no cliente real; cobertura completa de animações não informada. SKIN-030 análise Johnny concluída: corpo lanbo_tps54 referências/FPS39/torção de braços/IK; proposta com tronco exposto, sem geração/instalação. `ai/reviews/johnny-skin-analysis-20261013.md`.

- SKIN-031 referências JohnnyCampeãoSolar entregues13/10: quatroPNG1254×1254 transparentes/T-pose+ZIP em `candidate-assets/johnny-solar/multiview/`. Frente orientada peloJohnny nativo, corpo musculoso/tronco exposto; aguardandoGLBTripo. Sem alteração de execução/Oracle. `ai/reviews/johnny-solar-multiview-20261013.md`.

- SKIN-032 instalada13/10 10:00BRT: JohnnyCampeãoSolar5gratuita/TripoTPS54bones/rosto e textura preservados/25678v/21270tri; braçosT→pose nativa/pesos contínuos/CRC26101301. FPS/arma/ícone nativos, LODmeshcompleta. Oito poses simuladas/catálogo/8HTTP/TCP/PM2OK; reiníciozero conexões. Unitypendente. `ai/reviews/johnny-solar-installed-20261013.md`.

- SKIN-033 instalada13/10 10:16BRT após032queixo/ombros ruins parado/emmovimento: mandíbula rígidaHead, transiçãoClavicle eForeTwist restaurada; somente pesos, malha/textura/bones/IK intactos.34poses/varredura/4HTTP/TCP/CRC26101302/semreinício aprovados; Unitypendente. Base atual `candidate-assets/johnny-solar/refined/`; `ai/reviews/johnny-jaw-shoulders-20261013.md`.

- SKIN-034 instalada13/10 10:34BRT: queixo033 confirmado bom; fotos mostram axila posterior fina. Ajuste geométrico ombros1576v/≤3,3cm, rotação interpolada/normaisJacobiano; todos os pesos e cabeça/mãos033 preservados.37poses/3gripsIK/4HTTP/TCP/CRC26101303 semreinício aprovados; Unitypendente. Base atual `candidate-assets/johnny-solar/shoulders/`; `ai/reviews/johnny-axilla-20261013.md`.

- SKIN-034 recebeu relato demelhora parcial. SKIN-035DavidColosso doAbismo: análise62bones corporais/arma20/escudo obrigatório/portal separado; usuário escolheu autômato negro/bronze.4referênciasT-pose transparentes+ZIP em `candidate-assets/david-infernal/multiview/`, aguardaGLBTripo; semexecução alterada. `ai/reviews/david-infernal-analysis-20261013.md`.

- SKIN-036 instalada13/10 11:34BRT: DavidColosso doAbismo5gratuita/TripoTPS62bones/26826v/20992tri/placas rígidas/CRC26101304; arma/escudo10colliders/portal/IK/FPS/ícone nativos preservados.20poses simuladas/catálogo5560/8HTTP/TCP/PM2 aprovados; reiníciozero conexões/PID711369. LODcorporal completo, abertura de juntas sobrotações sintéticas fortes; testeUnity pendente. `ai/reviews/david-infernal-installed-20261013.md`.

- SKIN-037 concluída22/10: Shogun1024/HouYi analisado (corpo56bones/arco6/clips44TPS+23FPS/IK+escala);4PNG1254RGBA transparentes+ZIP/promptsem candidate-assets/shogun-raven/multiview. Corvo emposeT, mãos5dedos, asasrecolhidas/bico moderado; frente autoritativa, vistas não calibradas. Semruntime/Oraclealterados; aguardaGLBTripo. ai/reviews/shogun-raven-reference-20261022.md. Reserva liberada.

- SKIN-038 concluída22/10: quatro referênciasShogun limpas viaimage_gen, couro/penas/bronze semgrunge e paletavinho escuro/ameixa conforme steering. Entrega final1254RGBA+ZIP/prompts em candidate-assets/shogun-raven/clean/vinho/;037 preservada, semruntime/Oracle, aguardaGLB. ai/reviews/shogun-raven-clean-20261022.md. Reserva liberada.

- SKIN-039 instalada22/10 18:31BRT: ShogunCorvoNoturno7gratuita/TPS56bones/30598v/21611tri/pupila geométrica corrigida/CRC26102201; arco/flechas/ultimate/IK/rig/FPS nativos preservados,8MRhumanos ocultos.22poses simuladas comcontrole nativo/catálogo5561/8HTTP/TCP/PM2 aprovados; reiníciozero conexões/PID886212. LODmeshcompleta/íconeoriginal/asasdecorativas, Unitypendente. ai/reviews/shogun-raven-installed-20261022.md.

- SKIN-040 instalada22/10 19:03BRT após relato real de pernas finas/roupa rígida039: calça separada do casaco por conectividade, pesos próprios das pernas,6cadeias/80bones para asas/abas. Cliente com `TriggerunRavenDynamics.dll` e1prefixo emDynamicBone para volumes privados/reset de teleporte;22poses/solver matemático30–120FPS/4HTTP/TCP/CRC26102202 aprovados, semreinícioOracle. TesteUnity pendente; base atual `candidate-assets/shogun-raven/dynamic/`. `ai/reviews/shogun-raven-dynamics-20261022.md`.

- SKIN-040: usuário aprovou visual e movimento do Corvo no cliente real em22/10. Pediu considerar uma DLL universal para skins futuras; generalização ainda não implementada. Manter escopo/volumes por instância e configurações por skin ao evoluir o auxiliar.

- SKIN-041 referências Tagaloa Lobo Mau entregues22/10: corpo36bones + conjunto direito/arma18 (53nomes únicos), escudo precisa ser preservado; quatroPNG/ZIP/prompts em `candidate-assets/tagaloa-wolf/multiview/`. Frente/costas transparentes, perfis cinza para pelos claros limpos; aguardaGLB, semexecução/Oracle/DLL alteradas. `ai/reviews/tagaloa-wolf-reference-20261022.md`.
