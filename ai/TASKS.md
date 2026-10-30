# Quadro de tarefas

Estados: proposta → em andamento → revisão → validada. Marcar instalação e teste com cliente separadamente. Propostas abaixo não são autorização automática para executar tudo.

| ID | Prioridade | Objetivo / aceitação | Estado | Responsável / arquivos previstos |
| --- | --- | --- | --- | --- |
| AI-001 | Atual | Criar memória compartilhada, fontes e fluxo de evidências | validada (documentação) | Codex; AGENTS.md, ai/ |
| OBS-001 | Alta | Preservar telemetria e erros após reinício; correlacionar cliente/sala/cena; testar retenção e limites de volume | instalada; 44 testes; cliente pendente | Codex; 03/10; ai/reviews/lifecycle-logs-20261003.md; arquivos liberados |
| JIP-001 | Alta | Auditar matchmaking e entrada em salas iniciadas contra Assembly | análise concluída; falha real ainda não resolvida | Codex; 2026-09-26; ai/reviews/matchmaking-2026-09-26.md; ferramenta tools/audit-matchmaking.cjs; arquivos de execução liberados, não editados |
| TDM-002 | Atual | Corrigir seleção inicial de quem entra em partida iniciada: prévia, confirmação e fim do contador levam ao carregamento sem reiniciar os demais | validada pelo usuário no cliente; instalada na Oracle | Codex; 2026-09-29; alterados src/socketServer.js e tests/match-lifecycle.test.js; gameLogic.js sem alteração; passagem ai/reviews/jip-selection-2026-09-29.md; arquivos liberados |
| LAM-001 | Alta | Validar Lamster no cliente real: dois aliados e inimigo, HP cheio/parcial, fora do raio, morte e troca de sala; não curar inimigos nem causar dano com cura | proposta | livre; inicialmente coleta, src/lamster.js e XMLs somente se evidência exigir |
| ROOM-001 | Média | Auditar isolamento Control/PvE com duas salas simultâneas e critério explícito por modo | proposta | livre; delimitar src/gameLogic.js e src/socketServer.js após leitura |
| DEV-001 | Baixa | Trocar placeholder de npm test pelos testes existentes e documentar um comando único | proposta | livre; package.json |
| HERO-001 | Média | Analisar lógica de heróis hardcoded no servidor e sistema de skills do cliente (Assembly) para viabilizar heróis orientados a dados | análise concluída (somente leitura) | Antigravity; 2026-10-03; revisão em ai/reviews/hero-data-driven-2026-10-03.md; nenhum arquivo de execução alterado; arquivos liberados |
| MODE-001 | Alta | Implementar XP/nível por herói, corrigir desconexão fim de jogo, imortalidade FFA e modos funcionais (Controle, Escolta, Recursos, Arena, PvE) | instalada na Oracle; 106 testes | Antigravity; 29/10; alterados src/dbPersistence.js, src/database.js, src/gameLogic.js, src/socketServer.js, tests/; backup remoto; arquivos liberados |
| LAMSTER-GUIDE-001 | Alta | Guia de arquitetura e implementação completa para Codex transformar Lamster em herói nativo de 1ª classe (HUD nativa, habilidades ativas E/Shift/RMB/Q, animações e FX) | validada (documentação) | Antigravity; 29/10; ai/LAMSTER_FULL_HERO_GUIDE.md; arquivos liberados |
| LAMSTER-002 | Alta | Implementar kit nativo do Lamster (XMLs, habilidades 1201-1204, dazhao 2000, HUD nativa, remoção OnGUI, partículas e animações) | proposta / handoff para Codex | Codex; ver ai/LAMSTER_FULL_HERO_GUIDE.md |

## Como reservar a próxima tarefa

Atualize a linha com responsável, data/hora UTC e arquivos previstos. Acrescente abaixo apenas o plano curto da tarefa ativa: comportamento esperado, evidência inicial, teste de aceitação e o que fica fora do escopo. Se houver conflito de edição, resolva antes de continuar.

Próxima sequência após a auditoria: corrigir os defeitos reproduzidos de TDM (TDM-002), coletando evidências por conexão para validar com dois clientes; depois LAM-001 e isolamento dos demais modos. OBS-001 continua relevante para preservar a primeira falha. Evitar ajustes de balanceamento enquanto a entrega de eventos estiver incerta.

## TDM-002 — conferência do teste real de 29/09

Responsável: Codex. Escopo reservado: análise de output_log.txt e logs/hashes Oracle, documentação em ai/reviews/jip-client-log-2026-09-29.md; sem alteração de execução. Concluída a coleta: Oracle ainda usa versão anterior à correção local.

## Instalação TDM-002 autorizada
Codex — em andamento. Escopo: src/socketServer.js e tests/match-lifecycle.test.js na Oracle; backup remoto e logs antes do reinício; conferir hashes, sintaxe, testes e HTTP/TCP. Autorização explícita do usuário nesta sessão.

Instalação concluída em 30/09 01:34 UTC. Responsável Codex; arquivos liberados. Backup e resultado: ai/reviews/jip-oracle-install-20260930.md.

## TDM-003 — saída e equipes
Codex responsável; em andamento. Arquivos reservados src/socketServer.js e tests/match-lifecycle.test.js. Aceitação: saída Hall/Scene em qualquer ordem confirma retorno e remove entidade uma vez; fila e entrada tardia equilibram times. JIP anterior confirmado funcionando pelo usuário.

TDM-003: implementação e instalação concluídas, 35 testes aprovados locais/Oracle; teste real pendente. Arquivos liberados. Passagem ai/reviews/tdm-exit-teams-20260930.md.

## LIFE-001 / OBS-001 — prioridade autorizada em 03/10
Codex responsável, em andamento. Reservados src/logger.js, src/index.js, src/socketServer.js, src/gameLogic.js, src/http.js, módulos novos de ciclo/log, testes e documentação. Critério: transições explícitas e correlacionadas, logs preservados/limitados entre reinícios, cancelamento de callbacks obsoletos e regressões JIP/saída aprovadas. Antigravity HERO-001 concluída, sem conflito de execução.

LIFE-001/OBS-001: concluídas implementação, testes e instalação; 44/44 na Oracle. Cliente real pendente; reservas liberadas. Passagem ai/reviews/lifecycle-logs-20261003.md.

## HIT-001 — tiros e headshots
Codex em andamento. Reservados src/socketServer.js, novo src/sdgProtocol.js, tests/match-lifecycle.test.js e revisão ai. Assembly apenas leitura: alterações anteriores de CBulletMe preservadas. Critério: interpretar envelope nativo para todas as armas sem confundir metadados/vetores com acertos; investigar colisão lateral/próxima sem ampliar hitbox por hipótese.

HIT-001 — 04/10: Codex amplia reserva para Assembly/Assembly-CSharp/CameraControl.cs, CBulletNormalMe.cs e ferramenta tools/patch-hit-client.ps1. DLL real conferido por IL (SHA256 084c0454fa4541142bb037bd6fb833c0000a94d75f7a553429ccdbb7e439c25d). Preparar candidato separado, preservar binário instalado e todas as alterações anteriores; teste Unity real permanece obrigatório.

HIT-001: correções de protocolo e dois métodos do cliente implementadas e instaladas em 04/10. 53 testes local/Oracle; comparação IL de 14.780 métodos. Validação real de HS lateral/curta distância pendente. Reservas liberadas; passagem ai/reviews/hit-celina-20261004.md.

## HIT-002 — regressão global de colisão após alteração anterior
Codex reservado: novo helper cliente em client-patches/HitCollision.cs, ferramenta de build/patch, testes de seleção; CameraControl e famílias CBulletNormalMe/CBulletLaserMe/CBulletBezierMe/CBulletContraMe, DLL candidato. Fonte pura em C:/trig2/GAME/Dumps/assembly. Não alterar camadas físicas/rigidbodies: histórico de queda pelo mapa. Critério: ignorar cápsulas auxiliares sem saltar 10cm dentro da cabeça, mira e tiros usando superfície válida mais próxima, paredes/escudos preservados; teste Unity separado.
HIT-002: incluir referência do helper no Assembly-CSharp.csproj; backup preservado antes da edição.
HIT-002: filtro global implementado/instalado no cliente, 9 verificações simuladas e 14.780 métodos comparados. Oracle não alterada. Teste real pendente; arquivos liberados. ai/reviews/hit-regression-20261004.md.

HIT-002 — 04/10: usuário relatou "parece ter funcionado!" após HIT-002. Validação inicial positiva no cliente; não implica cobertura de todos os heróis, distâncias ou colisão física. Registro documental concluído por Codex; execução sem novas alterações.

## FFA-001 — individual (todos contra todos)
Codex responsável, 2026-10-04; em andamento. Reservados src/gameLogic.js, src/socketServer.js, src/lamster.js, novos módulos/dados FFA, testes e ai/reviews/free-for-all-20261004.md. Aceitação: mapas 5301–5305 iniciam HUD 19701, todos os participantes são adversários independentemente do slot, spawns do XML, pontuação individual até 20/600s, renascimento e encerramento isolados por sala; entrada tardia preserva relógio/placar. Cliente real separado dos testes automatizados. Pesquisa Ongame suspensa a pedido do usuário.
FFA-001: implementação instalada em 04/10 22:57 UTC (19:57 BRT), 67/67 testes local e Oracle; backup remoto /home/ubuntu/backups/ffa-20261004T225725Z. Cliente real: usuário relata placar funcional, mas aliado na sala criada e opção individual ausente do matchmaking; não validada integralmente.

## FFA-002 — menu e identificação de adversários
Codex em andamento, 2026-10-04. Reservados src/gameLogic.js, src/socketServer.js, src/combatRules.js, src/ffaMaps.json, src/database.js, public/baxi/MatchModel.xml, files_dir.xml, tests/match-lifecycle.test.js e documentação. Evidências: MatchModel isopen=0; CCOpyNameManager e sons/contorno comparam m_nShiliServer, preservado como time do slot; fbset contém também 5306/5307 ausentes de MAP_CONFIG. Critério: opção visível após atualização de cache, todos os outros humanos inimigos por destinatário, todos os 7 mapas nativos de individual cobertos. Usuário autorizou reinício.

FFA-002: instalada em 04/10 23:06 UTC (20:06 BRT), 70/70 testes locais/Oracle. Inclui 90009 na primeira entrada, equipes por destinatário, menu/cache e cobertura dos 7 mapas. Cliente real final pendente. Arquivos liberados; passagem ai/reviews/free-for-all-20261004.md.

## PVE-001 — bots e carregamento do Aventura
Codex responsável, 2026-10-04; em andamento. Reservados src/gameLogic.js, src/socketServer.js, novo módulo/dados PvE, testes e revisão ai. Evidência: controlador cria Npc de treino em vez de RobotAI de robot.xml; movimento reto artificial e referência socket inexistente nas ondas. Aceitação: monstros/modelos/rotas nativos, autoridade única de IA por cena, sincronização e combate, carregamento com tipo correto e controlador isolado por sala. Teste automatizado separado do cliente real. Preservar alterações FFA e demais tarefas.
PVE-001: ampliar reserva para src/database.js, public/baxi/fbset.xml, zh/fbset.xml e files_dir.xml: mapas 5105/5115 incorretamente fbtype=7. Corrigir tipo/cache necessário ao carregamento nativo.

PVE-001: implementação e instalação concluídas em 06/10 20:43 UTC (17:43 BRT), 81/81 testes locais/Oracle, HTTP/CDN/TCP OK. Cliente real pendente; mantém protótipo de 3 ondas. Reservas liberadas. Passagem ai/reviews/adventure-bots-20261006.md.

## PVE-002 — seleção e loading travado em 0%
Codex responsável, 2026-10-06; reservados src/socketServer.js, tests/match-lifecycle.test.js e documentação. Relato/log real: 90004 imediato, waitingForHeroLock=false; confirmações 90002 não carregam cena. Aceitação: fila PvE passa pela seleção, só carrega destino após confirmação/timeout, início das ondas somente após mapa pronto. Backup antes da edição.

PVE-002: instalado em06/10 20:49UTC, 85/85 testes locais/Oracle, HTTP/TCP OK. Cliente real pendente. Reservas liberadas. ai/reviews/adventure-loading-20261006.md.

## ADMIN-001 — catálogo e spawn de entidades
Codex responsável, 2026-10-06; reservados src/http.js, src/gameLogic.js, novo catálogo/spawner, public/admin/index.html, testes e documentação. Listar todas as definições de Npc.xml, robot.xml e heroinfo.xml, com IDs separados por origem, busca e spawn na cena do jogador. Preservar ações existentes e bots de ondas. Backup antes de editar.

ADMIN-001 concluída/instalada em07/10 03:06UTC; reservas liberadas.90/90 testes e navegador; cliente real pendente. Passagem: ai/reviews/admin-catalog-20261007.md.

## MODEL-001 — monstros como aparência jogável
Codex responsável; reservados src/http.js, public/admin/index.html, novo módulo/testes de model swap, helper cliente e ferramenta de patch. Preservar herói/arma/FPS; usar robot.xml para aparência TPS, reset reversível e envio apenas à cena. Teste Unity separado. Backup antes de editar.

MODEL-001 implementada/instalada em07/10 03:25UTC; reservas liberadas.93 testes servidor e7 simulados cliente; teste Unity pendente. Passagem ai/reviews/model-swap-20261007.md.

## HUD-001 — mira e feedback de combate
Codex responsável, 07/10. Reservados src/gameLogic.js, src/socketServer.js, helper/patch cliente, testes e documentação. Aceitação: mira local configurável/persistida, restauração nativa, eliminação após confirmação e IDs de arma/disparo preservados para falas. Preservar HIT/MODEL. Unity real separado.

HUD-001 implementada/instalada em07/10 21:52UTC; reservas liberadas.95 testes servidor e7 simulados cliente; UI/falas em Unity pendentes. ai/reviews/crosshair-feedback-20261007.md.

## VOICE-001 — falas e renascimento
Codex responsável, 07/10; reservados src/gameLogic.js, tests/match-lifecycle.test.js e documentação. Mira/feedback HUD confirmados pelo usuário; falas sem melhora. Investigar gatilhos nativos, remover reinicialização após OnRevive se redundante, testar isolamento/estado e preservar HUD/HIT/MODEL. Cliente real separado.


VOICE-001 implementada/instalada em07/10 22:05UTC;96/96 testes locais/Oracle. Cliente real pendente. Reserva liberada; demais gatilhos auditados com limites em ai/reviews/voices-respawn-20261007.md.

## VOICE-002 / LAMSTER-002 — batalha, kills e suporte selecionável
Codex responsável, 07/10. Reservados src/lamster.js, src/socketServer.js, src/database.js, XMLs de heróis/armas/skills/seleção e manifesto, helpers/patches cliente e testes pertinentes. Renascimento VOICE-001 confirmado pelo usuário. Aceitação: explicar/corrigir falas interrompidas, Lamster disponível na seleção, ataques com função de cura/dano clara e HUD correspondente, preservar HUD/HIT/MODEL; testes reais separados.

VOICE-002/LAMSTER-002 implementadas/instaladas em07/10 22:24UTC.99 testes locais/Oracle,7 simulados feedback, helper compilado/instalado no cliente fechado. Cliente real pendente; reservas liberadas. ai/reviews/lamster-voices-20261007.md.


## LAMSTER-003 — loading por material vazio
Codex responsável; reservados novo helper/ferramenta de patch, DLL candidato e testes. Log real: HeroPlayer->LoadWeaponMat baixa HeroAnimator/.mo após cadastro arma sem materiais. Filtrar entradas vazias somente para Lamster, preservar materiais embutidos e todos patches anteriores. Backup antes de instalar; teste Unity separado.


LAMSTER-003 implementada/testada/instalada somente no cliente.6 verificações simuladas;14.780 métodos comparados,1 alterado. Unity pendente; Oracle preservada. Reservas liberadas. ai/reviews/lamster-loading-20261007.md.


## LAMSTER-004 — secundário de inspiração
Codex responsável; src/lamster.js, src/socketServer.js, XMLs weapon/buff/skillinfo, manifesto, HUD/helper cliente e testes. Manter cura360, aplicar1007 self/aliados8m(25%/5s nativo),250 dano inimigos8m,CD6s; áudio apenas heróis com recurso existente. Backup e instalação verificada; Unity separado.


LAMSTER-004 implementada/testada:101 testes locais+6 simulados voz; instalação pendente por cliente aberto/4 conexões Oracle. Arquivos continuam reservados. Candidatos e passagem:ai/reviews/lamster-rally-20261007.md.


LAMSTER-004 instalação concluída07/10 22:58UTC após fechar clientes.101 testes locais/Oracle e6 simulados áudio. Backup remoto lamster-rally-20261007T225833Z;HTTP200/TCP7777 confirmados. Cliente real pendente; reservas liberadas.


## LAMSTER-005 — dano real do secundário e identidade visual
Codex responsável; investigar log cliente+Oracle/SDG antes de corrigir dano; criar retrato/ícones a partir da referência e servir recursos exclusivos Lamster. Reservados src/socketServer.js, src/http.js, dados/manifesto, assets/helpers e testes conforme necessários. Preservar patches anteriores; backups antes de editar; teste Unity separado.


LAMSTER-005 implementada/instalada cliente+Oracle23:25UTC.104 testes local/Oracle e5simulados área. Falha NPC confirmada; hipótese de oclusão cliente corrigida, validação Unity pendente. Reservas liberadas. ai/reviews/lamster-icons-damage-20261007.md.

## SKIN-001 — viabilidade de skins/modelos Lamster
Codex responsável; somente inspeção e documentação de bundles, rig, materiais e carregamento. Reservado ai/reviews/lamster-skin-feasibility-20261007.md; sem alterações no jogo/Oracle.

SKIN-001 verificação concluída:4bundles UnityFS5.5.2f1 legíveis/reempacotados em cópias, rig/texturas/animadores mapeados; ramo2000 fixo no Assembly atual confirmado. Sem mudanças runtime/cliente/Oracle. ai/reviews/lamster-skin-feasibility-20261007.md; reserva liberada.

## SKIN-002 — protótipo físico Lamster
Codex responsável; malhas TPS/FPS e bundles candidatos, manifesto, testes/inspeção visual e instalação com backup. Preservar bones, pesos, animações, UVs, materiais e colisores; deformação somente visual.

SKIN-002 protótipo implementado/instaladoOracle07/10 23:59UTC, backupremoto lamster-shape-20261007T235909Z. Somente2bundles+manifesto; validações estrutura/geometria/HTTP concluídas, cliente real pendente. Reserva liberada. ai/reviews/lamster-shape-20261007.md.

## SKIN-003 — definição do peitoral Lamster
Codex responsável; usuário confirmou aparência/gameplay doSKIN002. Refinar somente forma do peitoral/torso emTPS/FPS, preservar rig/hitbox/animadores; candidatos, manifesto e instalação com backup.

SKIN-003 implementado/instalado07/10 21:15BRT. Backup da versão002 aprovada,6malhas/300objetos eHTTP verificados; revisão003 aguardaUnity. Reservas liberadas. ai/reviews/lamster-chest-20261007.md.

## SKIN-004 — corrigir volume e trabalhar textura
Codex responsável; usuário rejeitou peitoral003 exagerado. Reduzir protrusão e criar texturaUV refinada guiada pelo retrato, preservar rig/UV/hitbox/animadores; bundles+manifesto/candidatos/testes/instalação com backup.

SKIN-004 implementada/instalada07/10 21:25BRT; correção volume e textura manual expressamente autorizada, backups das duasetapas. Geometria/textura/HTTP verificados; cliente real pendente. Reserva liberada. ai/reviews/lamster-texture-20261007.md.

## SKIN-005 — Celina experimental Aurora
Codex responsável; criar textura experimental corpo/FPS/arma clássica, preservar meshes/rig/hitbox/controllers. Reservados bundles correspondentes, manifesto, assets/scripts/testes; backups antes da instalação.

SKIN-005 implementada/instaladaOracle08/10 00:37UTC, seisbundles/manifesto26100710;1541objetos/HTTP verificados, sem reinício. Cliente real pendente. Reserva liberada. ai/reviews/celina-aurora-20261007.md.

## SKIN-006 — Aurora independente e roupa nova
Codex responsável. Restaurar seisbundles clássicos, criar recursos exclusivos Aurora com roupa refinada, cadastrar skin/arma/ícone/desbloqueio. Reservados herounlock/manifestos/bundles e código de catálogo necessário; backup antes de alterações. Aceitação: default restaurada, seleção independente e recursos íntegros; cliente real separado dos testes.

SKIN-006 implementada/instalada08/10 00:49UTC: Aurora skin6/arma5 gratuitas, roupa ajustada, clássicos restaurados, CAB exclusivos; catálogo/geometria/16HTTP/TCP OK. Reinício autorizado, cliente real pendente. Reserva liberada. ai/reviews/celina-aurora-independent-20261007.md.

## SKIN-007 — modelagem real da roupa Aurora
Codex responsável. Usuário rejeitou006 por pintura sem geometria. Criar jaqueta com vértices/faces/UV próprios, mangas/gola/barra/painéis e skinning no rig existente; prévias frente/lado/costas e teste de deformação antes da instalação. Reservados bundleTPS Aurora/ícone/manifesto, scripts/candidatos. Preservar clássica, hitbox e animadores; backups local/remoto antes de trocar execução.

SKIN-007 implementada/instalada08/10 00:44BRT:25partes/3393vértices/5544faces novos, jaqueta com material/UV próprios, skinning62bones.3poses sintéticas/estrutura/HTTP/TCP verificados; cliente real pendente. Sem reinício; reserva liberada. ai/reviews/celina-aurora-model-20261008.md.

## SKIN-008 — UV e textura artística da Aurora
Codex responsável. Refazer UVs por peças e textura específica com imagegen; gerar prévias no modelo antes de qualquer instalação. Reservados somente candidatos/scripts/relatório desta revisão. Manter execução Oracle e clássica intactas até avaliação do usuário.

SKIN-008 candidato pronto:UV16áreas/texturaimagegenrefinada, superfície/pesos preservados,1211objetos/3hashes verificados. Aguardando avaliação visual; NÃO instalado naOracle. ai/reviews/celina-aurora-texture-20261008.md. Reserva liberada.

## SKIN-009 — refino geométrico da Aurora
Codex responsável. Arredondar bolsos/painéis, reduzir reforços rígidos e suavizar mangas/caimento sobre a textura008. Reservados candidatos/scripts/relatório; prévia antes de instalar. Preservar bones/hitbox e execuçãoOracle.

SKIN-009 candidato pronto:bolsos arredondados/menos salientes, ombros reduzidos, mangas suavizadas. SóMesh difere de008 em1211objetos; pesos/faces/3poses sintéticas/3hashes execuçãoOK. Não instalado, aguardando avaliação. ai/reviews/celina-aurora-refined-20261008.md. Reserva liberada.

## SKIN-010 — ombros contínuos e remoção da roupa antiga
Codex responsável. Remover faces antigas do colarinho/ombros que aparecem e substituir reforço rígido por transição arredondada entre manga e corpo. Reservados candidatos/scripts/relatório; manter textura009, bones e execuçãoOracle. Prévia/testes antes de instalação.

SKIN-010 candidato pronto: colarinho antigo removido38faces, reforços de ombro eliminados, mangas arredondadas/junção superior fechada. SomenteMesh diferente em1211objetos;62bones/3poses/3hashes execuçãoOK. Não instalado, aguardando avaliação. ai/reviews/celina-aurora-shoulders-20261008.md. Reserva liberada.

## SKIN-011 — inspeção Scarlet externa
Codex responsável. Inspecionar GLB baixado e viabilidade de adaptação à Celina; reservados scratch/scarlet-inspection e relatório. Aceitação: inventário de malhas/texturas/rig, limites e estratégia de adaptação. Sem alteração de execução ou instalação.

SKIN-011 inspeção concluída:GLB íntegro,93bones,28551tri/setePNG1K; viável candidatoCelina com conversão rig/material. Adaptação e cliente real pendentes;Oracle intacta. ai/reviews/scarlet-inspection-20261008.md. Reserva liberada.

## SKIN-012 — Scarlet candidata no rig Celina
Codex responsável. Construir candidato separado com geometria/texturas Scarlet, rig62bones Celina e arma original; reservados scratch/scarlet-adapt e candidate-assets/celina-scarlet. Aceitação: prévias, pesos/índices/poses e preservação de execução. Sem instalar antes de avaliação.

SKIN-012 candidata pronta:Scarlet corpo/cabelo17494v/30704tri adaptados ao rig62bonesCelina; materiais legados,semkatana.1209objetos/3poses/3hashes execuçãoOK. Prévia para avaliação;Unity/cadastro/Oracle pendentes. ai/reviews/scarlet-celina-candidate-20261008.md. Reserva liberada.

## SKIN-013 — Scarlet com proporções preservadas
Codex responsável. Refazer candidata com escala global uniforme, torso/pernas/cabeça preservados e braços reposicionados por rotações rígidas; reservados scratch/scarlet-proportions e candidate-assets/celina-scarlet-proportions. Comparar silhueta original, checar pesos/poses; execução intacta.

SKIN-013 candidata revisada:torso/pernas/cabeça com escala global1,07; braços ajustados e pivôs das mãos coincidentesCelina.13916vértices fora dos braços preservam fonte<2e-7m;1209objetos/3poses/3hashesOK. Cliente real/instalação pendentes. ai/reviews/scarlet-proportions-20261008.md. Reserva liberada.

## SKIN-014 — cadastro e instalação Scarlet
Codex responsável. Usuário aprovou013 e autorizou prosseguir: criar skinCelina7 independente, FPS compatível/ícone/bundlesCAB únicos/catálogo/manifesto; backups local/remoto, verificar e instalarOracle. Reservados arquivos novosScarlet,herounlock.xml(baxi/zh),files_dir.xml,scratch/scarlet-release. Preservar clássica/Aurora. Verificar conexões antes de reiniciar.

SKIN-014 instaladaOracle08/10 20:30BRT:CelinaScarletskin7 gratuita/TPSCAB próprio/ícone/CRC26100802;FPS e arma nativos. Backup local/remoto;catálogo/7HTTP/TCP/PM2OK,zero conexões ao reinício. Clássica/Aurora intactas. Unity pendente. ai/reviews/scarlet-installed-20261008.md. Reserva liberada.

## SKIN-015 — portrait ilustrado Scarlet
Codex responsável. Criar portrait sem legenda/texto usando imagemScarlet como referência; instalar ícone exclusivo com backup local/remoto e novoCRC, sem reinício. Reservados íconecelina_scarlet.txt,files_dir.xml e scratch/scarlet-portrait. Usuário confirmou model swap funcional, animações aceitáveis mas estranhas.

SKIN-015 escopo confirmado pelo usuário: ícone circular HUD/loja. NewHeroHpUi.setHead força109x158; preparar ajuste scopedScarlet emCombatHud.cs/helper com backup, instalar cliente apenas fechado. ReservadosCombatHud.cs/TriggerunCombatHud.dll.

SKIN-015 instalada:portraitimagegen semtexto,circularRGBA256 eCRC26100804Oracle semreinício;helperHUD local mantémScarletquadrada109x109,24métodoscomparados. Usuário fechouclientes;backup/hash/3HTTPOK. Unitypendente. ai/reviews/scarlet-portrait-20261008.md. Reserva liberada.

## SKIN-016 — inspeção Hércules para Lamster
Codex responsável. Inspecionar Downloads/hercules.glb e comparar rig/proporções/materiais com Lamster; reservados scratch/hercules-inspection e relatório. Somente análise/prévias, sem alterar jogo ou instalar.

SKIN-016 inspeção concluída:Hércules8564v/13388tri semarmas,93joints/pesosnormalizados/atlas1024compartilhado. Visualrecomendado paraLamster;préviafonte disponível,adaptação/Unitypendentes. Cliente/Oracleintactos. ai/reviews/hercules-inspection-20261008.md. Reserva liberada.

## SKIN-017 — Hércules skin Lamster
Codex responsável. Usuário autorizou adaptar modeloHércules como skin independenteLamster semarmas, preservar gameplay/default. Reservados scratch/hercules-adapt,candidate-assets/lamster-hercules e relatório; construir/validar rig/prévias antes de cadastro/instalação.

SKIN-017 extensão necessária: PlayerCommon.GetNowModelData força male_tps para2000; preparar hook restrito aLamster emAssembly/helper e circularHUD paraHércules. Reservados client-patches/LamsterMaterials.cs,CombatHud.cs,duasDLLs,cadastrobaxi/zh/manifesto/recursos novos; backup antes da edição.

SKIN-017 instalada: Lamster Hércules skin2 gratuita/CRC26100805, duas DLLs locais e Oracle atualizados. Geometria/3poses/7simulados/14780métodos/6HTTP/TCP OK; reinício zero conexões. Unity pendente. ai/reviews/hercules-skin-20261008.md. Reserva liberada.

## SKIN-018 — Rider para Gaya
Codex responsável. Inspecionar Downloads/psp_-_fate_unlimited_codes_-_rider_2.glb, comparar malhas/rig/texturas com Gaya1036 e preparar avaliação. Reservados scratch/rider-inspection e relatório; jogo e Oracle intactos nesta inspeção.

SKIN-018 inspeção concluída: Rider40partes/8982v/11026tri,sem rig/pesos; Gaya1036 rig anatômico disponível. Adaptação exige skinning e cabelo separado. Sem instalação. ai/reviews/rider-gaya-inspection-20261008.md. Reserva liberada.

## SKIN-019 — adaptar Rider para Gaya
Codex responsável. Autorizado prosseguir: criar pesos e alinhar Rider ao rig Gaya1036, prévias/poses e skin independente preservando default/gameplay. Reservados scratch/rider-adapt,candidate-assets/gaya-rider; cadastro/instalação somente após validar candidata.

SKIN-019 extensão: cadastro Gaya skin5, recursos novos/CRC, helper CombatHud aceita ícone gaya_rider para manter círculo na HUD. Reservados public/baxi/herounlock.xml,zh/herounlock.xml,files_dir.xml,client-patches/CombatHud.cs,TriggerunCombatHud.dll; backups local/remoto antes de instalar.

SKIN-019 instaladaOracle/cliente08/10 21:26BRT: Gaya Rider5 gratuita/TPS independente/CRC26100806/retrato circular;64bones/3LODs/976objetos/3poses/29métodoshelper/6HTTP/manifesto/TCP OK. Gameplay/FPS/default intactos; cabelo simplificado e Unity pendente. ai/reviews/rider-gaya-skin-20261008.md. Reserva liberada.

## VALK-001 — primário e morte ao aterrissar
Codex responsável. Investigar Valk1016 sem dano primário e morte ao tocar chão no Mausoleum; correlacionar logs Oracle/cliente com servidor e Assembly, corrigir causa comprovada com backups/testes. Reservados scratch/valk-investigation e relatório; arquivos de execução serão delimitados antes de editar.

VALK-001 correção candidata delimitada: filtrar _IgnoreDamge nas duas consultas do primário físico e impedir Slay de matar pelo colisor da arma CMuzzlePhysicalTrigger; registrar zona/colisor para validar hipótese no Mausoleum. Reservados client-patches/MeleeCollision.cs,tools/patch-melee-collision.ps1,tests/MeleeCollisionFixture.cs,tests/melee-collision.ps1,Assembly-CSharp.dll e helper novo. Física/tamanhos/servidor/mapas preservados.

VALK-001 candidata pronta: filtro HIT-002 estendido às2consultas de melee; Slay ignora arma física e registra contato. 11novos+9anteriores simulados/14780métodos3alterados OK. Oracle intacta; aguardando usuário fechar TRIG para instalar. Causalidade específica Mausoleum/Unity pendentes. ai/reviews/valk-melee-slay-20261008.md. Reserva mantida.

VALK-001 instalada no cliente local12/10/2026 após usuário fechar TRIG; entrada/candidata/cópias conferidas por hash. Assembly+TriggerunMeleeCollision.dll, dependência HIT-002 presente. 20simulados já aprovados; Unity/Mausoleum pendentes. Oracle intacta. scratch/valk-investigation/installation.json. Reserva liberada.

## SKIN-020 — skins deslocam ao aproximar jogadores
Codex responsável. Comparar Hércules/Rider afetadas com Scarlet funcional, investigar rig/LODs/culling e componentes de física; preservar recursos existentes antes de corrigir. Reservados scratch/skin-proximity e relatório; delimitar correção após evidência.
`nSKIN-020 diagnóstico delimitado: registrar posição do control/modelo/Bip001, alvo remoto e contatos entre jogadores apenas para Hércules/Rider/Scarlet. Reservados client-patches/SkinProximity.cs,client-patches/CombatHud.cs,TriggerunCombatHud.dll,scratch/skin-proximity. Sem mudança de física/servidor; backup antes de editar.
`nSKIN-020 diagnóstico instalado no cliente12/10: hierarquias próprias confirmadas,29métodos helper comparados/sóEnsure alterado. [TRIG SkinNear]/[TRIG SkinContact] distinguem física, sincronização e ossos; nenhuma correção aplicada ainda. Aguardar reprodução~10s/doisclientes e analisar output_log. Oracle intacta. ai/reviews/skin-proximity-20261012.md. Reserva mantida.

## SKIN-021 — Celina blusa vermelha Tripo
Codex responsável. Inspecionar GLB enviado, diagnosticar olhos e preparar correção preservando original. Reservados scratch/celina-red-inspection e candidate-assets/celina-red; jogo/Oracle intactos até validar candidata.

SKIN-021 inspeção concluída:10881v/10105tri,1textura4K,semrig; olhos indefinidos/lentes opacas no material único confirmados visualmente. Correção/adaptação ainda pendentes;cliente/Oracle intactos. ai/reviews/celina-red-inspection-20261012.md. Reserva mantida.

SKIN-021 autorização para prosseguir: corrigir olhos em textura/rosto numa cópia, transferir pesos para62bonesCelina e preparar TPS independente/prévias. Reservados scratch/celina-red-adapt,candidate-assets/celina-red; sem alterar execução até validar.

SKIN-021 candidata preparada: olhos projetados12914pixels/restante preservado;GLB reaberto/geometria idêntica;TPS62bones/1209objetos/3poses validados. Cabelo e saia com pesos suavizados. Sem cadastro/instalação/Unity; cliente eOracle intactos. ai/reviews/celina-red-candidate-20261012.md. Reserva mantida para próxima instalação.

SKIN-021 instalação autorizada: reservar public/baxi/herounlock.xml,zh/herounlock.xml,files_dir.xml,novoTPS/íconeCelina8,scratch/celina-red-release; backup local/remoto e conferência de hashes antes de publicar. Reinício somente sem jogadores ou com autorização específica.

SKIN-021 extensão: retrato circular celina_red e helperCombatHud para manter proporçãoHUD; reservados client-patches/CombatHud.cs e TriggerunCombatHud.dll. Backup inclui diagnósticoSKIN-020 já instalado.

SKIN-021 instalada12/10~21:25BRT:CelinaExecutiva8gratuita/TPSindependente/retratocircular/CRC26101201. Backup local/remoto;33métodoshelper/sóLateUpdate;catálogo/6HTTP/manifesto/TCP/PM2OK. Reinício1conexãoautorizado. TesteUnitypendente;SKIN-020preservado. ai/reviews/celina-red-installed-20261012.md. Reserva liberada.

## SKIN-022 — FPS Executiva
Codex responsável. Inspecionar FPSskin3/mãossem luvas, adaptar mangas/punhos/relógio à Executiva8 e validar rig/arma/animação. Reservados scratch/celina-red-fps,candidate-assets/celina-red-fps; publicação após validar com backup local/remoto. Não alterar skinsoriginais.

SKIN-022 candidataFPS validada203objetos/40bones/pontasdedospreservadas. Reservar public/baxi/herounlock.xml,zh/herounlock.xml,files_dir.xml,novoFPS xingjilieren_fps_red; somenteExecutiva8 altera skinname1. Backups local/remoto antes de instalar; sem DLLnova.

SKIN-022 instalada12/10~21:41BRT:FPSExecutiva8/mãosnativasskin3/mangasvermelhas/punhosbrancos/relógio. 203objetos/40bones/926vérticesdedos/6HTTP/manifesto/TCP/PM2OK;CRC26101202/5556entradas. Reinício1conexãoautorizado;semDLLnova. Unitypendente. ai/reviews/celina-executiva-fps-20261012.md. Reserva liberada.

## SKIN-023 — FPS Executiva nativo sem pulseira
Codex responsável. Relato real022: braços invisíveis, só mãos visíveis. Usar bundle originalskin3 e remover apenas faces dos acessórios; preservar vértices, materiais, bones e animação. Reservados scratch/celina-fps-simple,candidate-assets/celina-fps-simple,files_dir.xml e xingjilieren_fps_red.mo; backups local/remoto antes de publicar.

SKIN-023 instalada12/10~21:50BRT: FPSnativoskin3 sempulseira,022rejeitado porbraçosinvisíveis.203objetoscomparados/sóMesh+AssetBundle;4HTTP/TCP/PM2OK,CRC26101203. Reiníciozeroconexões;novoUnitypendente. ai/reviews/celina-fps-simple-20261012.md. Reserva liberada.

## SKIN-024 — Venus Guardiã de Jade
Codex responsável; usuário delegou escolha do conceito. Preparar referência T-pose para Tripo: branco/jade/dourado, cabelo preso, roupa de combate sem saia longa. Reservados candidate-assets/venus-jade e documentação. Sem alterar jogo ou Oracle; adaptação depende do GLB gerado pelo usuário.

SKIN-024 referênciaTpose pronta:VenusGuardiãdeJade,PNG1024x1536RGBA+prompt emcandidate-assets/venus-jade;rig principal79bonesinspecionado. Jogo/Oracleintactos;aguardaGLBTripo. ai/reviews/venus-jade-concept-20261012.md. Reserva de código liberada.

SKIN-024 revisão autorizada: usuário aponta orelhas e cauda da Venus/kitsune humanoide. Reservados scratch/venus-native-inspection e nova referênciavenus-jade-v2; examinar geometria/textura/bones antes de gerar. Não publicar conceito humano anterior.

SKIN-024 revisão concluída: originalrenderizado/orelhas+caudaconfirmadas;Bone001–005cauda/Bone023–024orelhas. Referênciav2restaura loirocurto/olhosroxos/orelhas/uma cauda. Usarvenus-jade-tpose-v2.png;v1descartada. ai/reviews/venus-jade-revised-20261012.md. Jogo/Oracleintactos;aguardaGLB/reservaliberada.

SKIN-024 multivistas autorizado: Codex reserva candidate-assets/venus-jade/multiview e nova passagem. Frente v2 aprovada + laterais esquerda/direita e costas consistentes, PNGs separados para Tripo. Somente imagens; nenhuma alteração de execução.

SKIN-024 multivistas entregue:4PNG1024x1536RGBA+ZIP emcandidate-assets/venus-jade;frenteaprovadapreservada/lateraiscostasimage_gen,transparência/boundsconferidos. PequenasvariaçõesIAregistradas;Jogo/Oracleintactos. ai/reviews/venus-jade-multiview-20261012.md. Reserva liberada/aguardaGLB.

SKIN-024 GLB recebido: Codex reserva scratch/venus-jade-inspection,candidate-assets/venus-jade e scratch/venus-jade-adapt. Inspecionar modelo/textura/componentescauda; preparar candidata no rig79bones sem alterar originais/jogo/Oracle até validar.

SKIN-024 GLBinspecionado/candidataprincipal preparada:13582v/21759tri/79bones/cauda658vrotacionada/cadeia5bones. Quatroposessintéticas conferidas,aimestiramentoslocalizados/LODsoriginaispendentes;semcadastro/instalação. ai/reviews/venus-jade-candidate-20261012.md. Reserva candidata mantida.

SKIN-024 instalação autorizada: Codex reserva build/verify Venusjade,public/baxi/herounlock.xml,zh/herounlock.xml,files_dir.xml,novoTPSdaji_tps_jade.mo,scratch/venus-jade-release. Corrigir UVrootcauda sem editaroriginal; todosLODsrenders usarão meshadaptado. Backup local/remoto e testesantespublicação.

SKIN-024 instalada12/10~22:22BRT:VenusGuardiãdeJade5gratuita,148UVsrootcaudacorrigidos/79bones/LODsmesmameshadaptada. Quatroposessintéticas/6HTTP/TCP/catálogo/PM2OK;CRC26101204/5557entradas;reiníciozeroconexões. Ícone/FPSoriginais;Unitypendente. ai/reviews/venus-jade-installed-20261012.md. Reserva liberada.

## SKIN-025 — Venus braços/ombros/pescoço
Codex responsável. Relato cliente024deforma regiões superiores. Reservados scratch/venus-jade-adapt/build.py,verify.py,candidate-assets/venus-jade,scratch/venus025 e recursoTPS/manifesto após validar; preservar024anteseditar. Investigar alinhamento/pesos regionais, poses locais e instalação semjogadores.

SKIN-025 instalada12/10~22:30BRT:pesosregionaisbraços/ombros/pescoço,cabeça abaixoqueixo ajustada. GeometriaUVrig024idênticos;6posessintéticas/mirap99 2,30→1,109/max6,31→1,673/4HTTP/TCP/PM2OK. CRC26101205/reiníciozeroconexões;Unitypendente. ai/reviews/venus-upperbody-20261012.md. Reserva liberada.

## SKIN-026 — Venus encaixe geométrico
Codex responsável.025relato aindaombrosesticados/pescoçoestranho/caudafora. Reservados scratch/venus026,candidate-assets/venus-jade,build/verify e recurso/manifesto apósvalidação. Rever pivôs/geometria e animaçãooriginal; não repetir só ajuste pesos. Backup025anteseditar;aguardando detalhe parado vs movimento.

SKIN-026 instalada12/10~22:39BRT:encaixegeométricoombros/pescoço,cauda6cmparadentrocomUV/pesos/formapreservados.6posessintéticas/4HTTP/TCP/PM2OK;CRC26101206/reiníciozeroconexões. Relato025confirmadoestranhoparado+movimento;Unity026pendente. ai/reviews/venus-geometry-fit-20261012.md. Reserva liberada.

## SKIN-027 — Nezha referência RPG
Codex responsável. Analisar anatomia/rig/armas/efeitosnativos e referência anexada antes de planejar skin. Reservados scratch/nezha-inspection,candidate-assets/nezha-rpg e passagem. Somente análise/prévia, jogo/Oracleintactos.

SKIN-027 análiseconcluída:Nezha1035principal62bones/faixaBone002–007/FPS43bones/ultimate8bones comMeshpolySurface10duplicado. Renders/inventáriosprontos;referênciaRPGviávelcomarmaduracompacta/peçasrígidas. Semcandidata/instalação. ai/reviews/nezha-rpg-analysis-20261012.md. Reserva liberada.

SKIN-027 referências autorizadas: Codex reserva candidate-assets/nezha-rpg/multiview e passagem. Gerar frente T-pose e vistas consistentes para Tripo, corpo humano com dois braços; braços nativos da ultimate preservados por decisão do usuário. Aceitação: PNGs separados, corpo completo/transparência/anatomia legível. Sem alterações no jogo/Oracle.

SKIN-027 multivistas entregues: quatro PNG1024x1536RGBA/transparência real+ZIP; frente autoritativa, laterais/costas derivadas e costas corrigidas. Prompts/hashes registrados; pequenas diferenças IA documentadas. Braços originais ultimate serão preservados por instrução direta. Sem modelo/cadastro/Oracle. ai/reviews/nezha-rpg-multiview-20261012.md. Reserva liberada; aguardaGLBTripo.

SKIN-028 — Codex responsável: inspecionar GLB Nezha recebido, corrigir rosto/proporções conforme evidências e preparar candidata no rig62bones. Reservados scratch/nezha-rpg-inspection,scratch/nezha-rpg-adapt,candidate-assets/nezha-rpg (novos derivados),passagem. Original GLB preservado; braços nativos ultimate/FPS/armas/colliders não alterados nesta etapa. Critério: textura/geometria conferidas, rig e poses sintéticas validados, prévia revisável antes de instalar.

SKIN-028 candidata validada:31525v/21156tri/62bones;face5275UVpixels,braços1.22/1.72ajuste de comprimentos,pauldronsrígidos e7poses. Reservar public/baxi/herounlock.xml,zh/herounlock.xml,files_dir.xml,novo nezha_tps_rpg.mo e scratch/nezha-rpg-release para skin5independente gratuita. Backup local/remoto e hashesguardados; FPS/ultimate/armas/DLL intactos. Reiniciar somente sem conexões ou com autorização específica.

SKIN-028 instalada12/10~23:30BRT: NezhaSoberanoÍgneo5gratuita/62bones/21156tri/face5275UVpixels/braçosajustados/ultimateFPSoriginais.7poses/estrutura/catálogo/8HTTP/TCP/PM2OK;CRC26101207.1conexãosurgiu,restartautorizadoexplicitamente. ClienteUnitypendente;backup/handoff ai/reviews/nezha-rpg-installed-20261012.md. Reserva liberada.

SKIN-029 — Codex responsável: relato real028com screenshots, corpo menos robusto e placas da cintura abertas na perna levantada. Reservados scratch/nezha029,candidate-assets/nezha-rpg/robust,passagem e recurso nezha_tps_rpg.mo/manifesto após validar. Aumentar tórax/costas e espessura braços sem mover mãos/pivôs/altura; revisar pesos das placas com pose de seleção específica. Preservar028local/remoto antespublicar. FPS/ultimate/textura semalteração.

SKIN-029 instalada em12/10 23:52 BRT: tórax/braços mais robustos, placas simétricas e pesos das coxas corrigidos; mãos/cabeça/textura/FPS/ultimate preservados. Oito poses simuladas e quatro hashes HTTP/TCP aprovados, CRC26101208. Oracle sem reinício, PID701709 mantido. Teste real029 pendente; passagem `ai/reviews/nezha-robust-20261012.md`. Reserva liberada.

SKIN-029: usuário confirmou que a versão ficou muito boa no cliente real em13/10. Cobertura completa das animações não informada.

SKIN-030 — Codex responsável: analisar Johnny1031 (interpretação da preferência por braços expostos) para conceito de skin com musculatura visível e tema original. Reservados `scratch/johnny-inspection/`, passagem e documentação; sem alteração de execução/Oracle. Aceitação: recurso efetivo identificado, meshes/bones/poses relevantes inspecionados, proporções e limites de roupa documentados, proposta visual fundamentada.

SKIN-030 análise concluída: Johnny usa lanbo_tps/54 referências de ossos, FPS39, torção de braços e IK nativos. Oito bundles/meshes/rig/controllers inspecionados, previews reais e hashes preservados. Proposta favorita Campeão Solar com tronco descoberto; sem referênciaTripo/modelo novo/instalação. `ai/reviews/johnny-skin-analysis-20261013.md`. Reserva liberada.

SKIN-031 — Codex responsável: gerar referências Johnny Campeão Solar em T-pose conforme pedido explícito (prevalece sobre A-pose sugerida). Reservados `candidate-assets/johnny-solar/multiview/`, documentação/passagem. Frente autoritativa, costas e dois perfis consistentes com proporções do Johnny nativo, musculatura exposta, transparência e arquivos separados para Tripo. Sem alteração no jogo/Oracle.

SKIN-031 concluída13/10: quatroPNG1254×1254RGBA+ZIP, T-pose/multivistas/identidade e musculatura conferidas. Frente autoritativa, perfis orientam profundidade; não são projeções calibradas de uma única mesh. Prompts/hashes em `candidate-assets/johnny-solar/multiview/`; passagem `ai/reviews/johnny-solar-multiview-20261013.md`. Sem jogo/Oracle alterados; aguardaGLBTripo. Reserva liberada.

SKIN-032 — Codex responsável: GLB Campeão Solar recebido, adaptar Johnny1031 e cadastrar skin independente. Reservados `scratch/johnny-solar-inspection/`, `scratch/johnny-solar-adapt/`, `scratch/johnny-solar-release/`, `candidate-assets/johnny-solar/` novos derivados, catálogo1031 e manifesto/recurso após validação. Preservar GLB nativo, hierarquia/IK/arma/física e gameplay; conferir textura/proporções/pesos e poses antes de instalar com backups locais/remotos e guarda de hashes. FPS inicialmente nativo. Reiniciar somente sem jogadores ou mediante autorização específica.

SKIN-032 instalada13/10 10:00BRT: JohnnyCampeãoSolar5 gratuita/TripoTPS54bones/25678v/21270tri/CRC26101301. Rosto/textura originaisTripo, braços ajustados e hierarquia/pesos suaves; FPS/ícone/arma/gameplay nativos. Oito poses sintéticas/catálogo/8HTTP/TCP/PM2 aprovados; reiníciozero conexões. Unitypendente; backups/handoff `ai/reviews/johnny-solar-installed-20261013.md`. Reserva liberada.

SKIN-033 — Codex responsável: relato real032, queixo e ombros/braços ruins parado e em movimento. Reservados `scratch/johnny033/`, `candidate-assets/johnny-solar/refined/`, recursoTPSsolar e manifesto após validação. Medir região queixo/cabeça/pescoço e articulação clavícula/braço, pesos/deformação em poses e varredura de ângulos; usuário permite tolerância medida para pesos/bones se necessário. Preferir malha/pesos e preservar pivôs/IK/gameplay, fonte032 antes de editar e backup remoto antes instalar. Sem aprovação extra para atualização de recurso sem reinício.

SKIN-033 instalada13/10 10:16BRT: mandíbula segueHead/ombros comClavicle/antebraços comForeTwist; somente pesos alterados, toda geometria/textura/bones/IK preservados.34poses/varredura/4HTTP/TCP aprovados, CRC26101302, semreinício/PID709544 mantido. Queixo simulado perdeu erro~6cm relativo àcabeça; novo testeUnity pendente. `ai/reviews/johnny-jaw-shoulders-20261013.md`. Reserva liberada.

SKIN-034 — Codex responsável: queixo033 confirmado bom, screenshots mostram dobra posterior da axila/ombro afinada na pose de arma. Reservados `scratch/johnny034/`, `candidate-assets/johnny-solar/shoulders/`, recursoTPSsolar/manifesto após validar. Reproduzir pose de mãos à frente porIK sintético, medir compressão/volume/transição da malha e ajustar ombros sem alterar cabeça033/mãos/ossos/IK/gameplay. Preservar033local/remoto antes publicar; semreinício para mesma rota/recurso.

SKIN-034 instalada13/10 10:34BRT: transição geométrica dos ombros porrotação interpolada/1576v/deslocamento≤3,3cm, pesos equeixo033 preservados.37poses incluindo3gripsIK/4HTTP/TCP/CRC26101303 aprovados, semreinício/PID709544. Nova validaçãoUnity pendente. `ai/reviews/johnny-axilla-20261013.md`. Reserva liberada.

SKIN-034 usuário relatou melhora parcial em13/10; queixo aprovado, axila ainda não considerada perfeita. Não repetir ajustes Johnny sem novo pedido.

SKIN-035 — Codex responsável: iniciar skin infernal doDavid1037; analisar modelo/rig/partes móveis/armas/animação e propor conceito compatível antes de gerar referênciaTripo. Reservados `scratch/david-inspection/`, `candidate-assets/david-infernal/` novos derivados e passagem. Sem alteração de execução/Oracle; preservar modelos nativos. Critério: identificar geometrias e ossos efetivos, forma especial/armas e restrições de reconstrução, conceito fundamentado no tema pedido.

SKIN-035 direção confirmada pelo usuário: autômato demoníaco de metal negro e bronze. Gerar referênciasTripo doColosso doAbismo com juntas livres/peito emplacas/62bones corporais e arma20bones preservada depois. Reservar `candidate-assets/david-infernal/multiview/` e prompts; somente imagens, sem reconstruirGLB ou alterarjogo.

SKIN-035 concluída13/10: nove bundles/rig/arma/escudo/portal analisados, DavidColosso doAbismo com4PNG1254×1254RGBA+ZIP e prompts/hashes. Corpo62bones e arma20bones exigem placas/partes independentes; escudo `body2_dun` obrigatório eportal separado. Semcadastro/Oracle; aguardaGLBTripo. `ai/reviews/david-infernal-analysis-20261013.md`. Reserva liberada.

SKIN-036 — Codex responsável: GLB Colosso doAbismo recebido; inspecionar textura/partes, adaptar TPSDavid1037 e cadastrar skin independente gratuita após validar. Reservados `scratch/david-infernal-inspection/`, `scratch/david-infernal-adapt/`, `scratch/david-infernal-release/`, `candidate-assets/david-infernal/` derivados, catálogo1037/manifesto/recurso após testes. Preservar hierarquia62bones e auxiliares, arma20bones, escudo/portal/IK/gameplay; peças rígidas devem manter movimento. Backups locais/remotos e guarda de hashes antes publicação. FPS nativo inicialmente; reinício somente sem conexões ou com autorização específica.

SKIN-036 instalada13/10 11:34BRT: DavidColosso doAbismo5gratuita/TripoTPS62bones/26826v/20992tri/placas rígidas/CRC26101304; arma/escudo10colliders/portal/IK/FPS/ícone nativos preservados.20poses simuladas/catálogo5560/8HTTP/TCP/PM2 aprovados; reiníciozero conexões/PID711369. LODcorporal completo, abertura de juntas sobrotações sintéticas fortes; testeUnity pendente. `ai/reviews/david-infernal-installed-20261013.md`. Reserva liberada.

SKIN-037 — Codex responsável22/10: analisar Shogun1024/HouYi nativo, rig/poses/arma/acessórios e gerar referênciaTripo da skin corvo baseada na imagem do usuário. Reservados `scratch/shogun-inspection/`, `candidate-assets/shogun-raven/` e passagem/documentação. Somente inspeção e imagens; não alterar execução/Oracle. Critério: referência corpo inteiro em pose de modelagem, anatomia compatível com arqueiro, bico/mãos/articulações/roupa/asas orientados pelas limitações reais; multivistas se úteis, arquivos individuais e prompts preservados.

SKIN-037 concluída22/10: Shogun1024/HouYi analisado (corpo56bones/arco6/clips44TPS+23FPS/IK+escala);4PNG1254RGBA transparentes+ZIP/promptsem candidate-assets/shogun-raven/multiview. Corvo emposeT, mãos5dedos, asasrecolhidas/bico moderado; frente autoritativa, vistas não calibradas. Semruntime/Oraclealterados; aguardaGLBTripo. ai/reviews/shogun-raven-reference-20261022.md. Reserva liberada.

SKIN-038 — Codex responsável22/10: usuário aprovou conceito037 e pediu limpeza das texturas/ruído visual. Reservar `candidate-assets/shogun-raven/clean/` para quatro vistas derivadas, prompts e pacote; preservar originais037. Alterar somente acabamento para materiais limpos/estilizados, sem mudar identidade, pose, proporções, asas/roupa/equipamento. Sem runtime/Oracle. Critério: detalhes de penas/couro/bronze legíveis sem poeira, partículas, granulação e desgaste excessivo, transparência e consistência entre vistas.

SKIN-038 concluída22/10: quatro referênciasShogun limpas viaimage_gen, couro/penas/bronze semgrunge e paletavinho escuro/ameixa conforme steering. Entrega final1254RGBA+ZIP/prompts em candidate-assets/shogun-raven/clean/vinho/;037 preservada, semruntime/Oracle, aguardaGLB. ai/reviews/shogun-raven-clean-20261022.md. Reserva liberada.

SKIN-039 — Codex responsável22/10: GLB corvo recebido; corrigir pupila ausente e adaptar/cadastrar skin independente Shogun1024. Reservados `scratch/shogun-raven-inspection/`, `scratch/shogun-raven-adapt/`, `scratch/shogun-raven-release/`, novos derivados `candidate-assets/shogun-raven/`, catálogo1024/manifesto/recurso após validação. Preservar fonteGLB, rig/IK/arco/flechas/efeitos de ultimate e gameplay, remover apenas meshes/acessórios humanos substituídos na nova skin. Conferir mão da corda/bico/chapéu/penas/casaco em poses; arquivosruntime com backup local/remoto e guarda de hashes antes instalação. FPS inicialmente nativo, sem DLL; reinício apenas sem jogadores ou mediante autorização específica.

SKIN-039 instalada22/10 18:31BRT: ShogunCorvoNoturno7gratuita/TPS56bones/30598v/21611tri/pupila geométrica corrigida/CRC26102201; arco/flechas/ultimate/IK/rig/FPS nativos preservados,8MRhumanos ocultos.22poses simuladas comcontrole nativo/catálogo5561/8HTTP/TCP/PM2 aprovados; reiníciozero conexões/PID886212. LODmeshcompleta/íconeoriginal/asasdecorativas, Unitypendente. ai/reviews/shogun-raven-installed-20261022.md. Reserva liberada.

SKIN-039 relato real: skin funciona, mas pernas afinam demais em certas poses e sobretudo/asas excessivamente rígidos; substituir expectativa de aprovação visual integral. Usuário autorizou correção e física controlada.

SKIN-040 — Codex responsável22/10: corrigir compressão das pernas039 e acrescentar ossos/movimento secundário controlado a abas e asas. Reservados `scratch/shogun040/`, `candidate-assets/shogun-raven/dynamic/`, bundleTPSraven/manifesto após testes; patch cliente/DynamicBone se necessário para reset específico em teleporte/respawn. Preservar039 em backup local e remoto antes substituição, demais heróis/skins/rig de combate/arco/pupila/textura/gameplay. Critérios: medir e reduzir compressão no joelho/coxa, isolar pesos de roupa/pernas, simular limites e colisões cosméticas, reset/isolamento de duas instâncias e custo; compilação/validação estrutural antes instalar, distinguir simulação e cliente real. Reinício somente sem conexões ou com autorização específica.

SKIN-040 instalada22/10 19:03BRT: pesos das pernas corrigidos por conectividade, seis cadeias cosméticas/80bones, DynamicBone nativo com volumes privados/reset específico via auxiliar cliente.22poses e solver real sob substituto matemático30/60/120FPS/235227asserções passaram;14780métodos comparados/1alterado;4HTTP/TCP/CRC26102202/semreinícioOracle. Cliente instalado após confirmação de fechamento; testeUnity pendente. `ai/reviews/shogun-raven-dynamics-20261022.md`. Reserva liberada.

SKIN-040 relato real: usuário aprovou visual e movimento do Corvo; pediu considerar futuramente uma DLL universal para skins. Registrar como direção futura, sem generalizar o patch nesta tarefa.

SKIN-041 — Codex responsável22/10: analisar Tagaloa1006/xt, rig/poses/arma/escudo e criar referências Tripo da skin Lobo Mau, usando as duas imagens como ideias. Reservados `scratch/tagaloa-inspection/`, `candidate-assets/tagaloa-wolf/` e passagem/documentação. Sem modificar execução/Oracle nesta etapa. Critérios: porte/silhueta compatíveis com modelo real, focinho/mãos/pés/cauda e roupa adequados ao rig; referência limpa em pose de modelagem, multivistas individuais consistentes e prompts preservados. Fonte/nativos intactos.

SKIN-041 concluída22/10: Tagaloa1006 analisado (corpo36bones/braço-direito-arma18,53nomes únicos/escudo preservável/51clipsTPS). Lobo Mau/Alfa da Lua Negra com quatroPNG individuais+ZIP/README/prompts em `candidate-assets/tagaloa-wolf/multiview/`; frente/costas transparentes, perfis cinza neutro para contorno limpo. Inspeção visual/arquivos/ZIP aprovados, vistas não calibradas. Sem execução/Oracle/DLL alteradas; aguardaGLBTripo. `ai/reviews/tagaloa-wolf-reference-20261022.md`. Reserva liberada.

SKIN-042 — Codex responsável22/10: GLB Lobo Mau recebido; inspecionar e adaptar Tagaloa1006, cadastrar skin independente gratuita após validar. Reservados `scratch/tagaloa-wolf-inspection/`, `scratch/tagaloa-wolf-adapt/`, `scratch/tagaloa-wolf-release/`, novos derivados `candidate-assets/tagaloa-wolf/`, catálogo1006/manifesto/bundles após testes. Preservar fonte e nativos, hierarquia corpo/braço-direito-arma/escudo/emissores/IK/animação/gameplay; tratar cauda separadamente. Backup local/remoto e guarda de hashes antes substituir execução. Se física exigir auxiliar, preservar suporte Corvo040 e limitar opt-in por skin. Testes estruturais/poses/contornos/cauda antes publicar; DLL só com cliente fechado, reinícioOracle somente zero conexões ou autorização específica.

SYNC-001 — Antigravity responsável22/10: corrigir bloqueio de soco rápido (`key="Fight"` / `_fillCartridge`) em recarga interrompida por morte/ragdoll (opcode 31203 + `killPlayer`), sincronização de visibilidade no spawn/late-join (`SLoadMapOKMsg` e relay de movimento sem comparação estrita de tipo), proteção contra pacotes `SP` atrasados pós-morte/respawn (`isDeadFlag=1` / `lastRespawnTime`) e limpeza centralizada de timers de combate (`sceneScope.invalidate`, morte e troca de herói). Reservados `src/sceneScope.js`, `src/socketServer.js`, `src/gameLogic.js` e `tests/match-lifecycle.test.js`. Backup local em `backups/sync001-before/`. Não tocar em arquivos de `SKIN-042`.

SYNC-001 concluída localmente22/10: implementados Itens 1–4 em `src/sceneScope.js`, `src/socketServer.js` e `src/gameLogic.js` com backup em `backups/sync001-before/` e 4 novos testes automatizados em `tests/match-lifecycle.test.js` (99/99 testes passando localmente). Pendente instalação na Oracle e teste em cliente Unity real. Reserva liberada.

SYNC-002 — Antigravity responsável22/10: adicionar comandos utilitários `/sync` (`/vis`) e `/fix` no chat em `src/socketServer.js`, criar `src/heroCatalog.js` para centralizar atributos/regras por herói (tempos de recarga, aliases, escudos/barreiras/passivas e limites de invocações) desacoplando `src/socketServer.js` e `src/gameLogic.js`, adicionar testes automatizados em `tests/match-lifecycle.test.js` e instalar `SYNC-001`+`SYNC-002` na Oracle conforme autorizado pelo usuário. Reservados `src/heroCatalog.js`, `src/socketServer.js`, `src/gameLogic.js` e `tests/match-lifecycle.test.js`. Backup local em `backups/sync002-before/`. Não tocar em arquivos de `SKIN-042`.

SYNC-002 instalada22/10 23:15BRT: criados `src/heroCatalog.js` e comandos `/sync` (`/vis`) + `/fix` em `src/socketServer.js`, integrados com `src/gameLogic.js` e `src/sceneScope.js`. 101/101 testes locais e na Oracle aprovados; backup local `backups/sync002-before/`, backup remoto `/home/ubuntu/backups/sync-001-002-20261023T021502Z/runtime.tar.gz`; PM2 online (PID 890306, zero conexões no restart), HTTP `/api/maps` 200 e TCP 7777 ativo. Validação em cliente Unity real pendente. Reserva liberada.

SKIN-042 instalada23/10 00:14BRT: TagaloaLoboMau6gratuita/28596v/21585tri/59bones, braços e ombros proporcionados/cauda5bones comDynamicBone e opt-in noauxiliarCorvo.23poses/652objetos/48747asserçõescauda+235227regressãoCorvo/8HTTP/TCP/catálogo aprovados, CRC26102301/5562recursos. UmaDLLlocal atualizada comcliente fechado;Assembly intacta. Oracle reiníciozero conexões/PID891337; arquivosAntigravity preservados/verificados. FPS/íconeoriginais, testeUnitypendente. `ai/reviews/tagaloa-wolf-installed-20261023.md`. Reserva liberada.

SKIN-043 — Codex responsável23/10: relato real/foto042 mostra peito comprimido, axilas pinçadas e proporção de tronco/braços artificial. Reservados `scratch/tagaloa043/`, `candidate-assets/tagaloa-wolf/refined/`, bundleTPSwolf/manifesto após validar. Investigar geometria e pesos com referência à pose real (decodificar animações nativas se viável), preservar cabeça/textura/cauda/física/rig/gameplay e alteraçõesAntigravity. Backup042 local/remoto e guardas de hashes antes instalar; semDLL/reinício se somente recurso. Critério: peito com volume contínuo, ombros/axilas sem pinçamento, proporções melhores em repouso e poses; distinguir simulação de Unity real.

SKIN-043 instalada23/10 01:16BRT após foto042: peitoral recuperado/torso mais amplo/axila e braços repintados, sóMesh em666objetos; cauda/rig/DLL/servidor preservados.23poses sintéticas aprovadas;51clipes/255amostras nativas auditadas com métricas/limites registrados, prévias conferidas.4HTTP/TCP/CRC26102302/semreinício/PID891337. Fonte atual `candidate-assets/tagaloa-wolf/refined/`; Unitypendente. `ai/reviews/tagaloa-chest-shoulders-20261023.md`. Reserva liberada.

SKIN-044 — Codex responsável23/10: peito043 aprovado pelo usuário; engrossar ombros/braços e restaurar manopla original doTagaloa1006 na skinLoboMau6. Reservados `scratch/tagaloa044/`, `candidate-assets/tagaloa-wolf/armed/`, bundleTPSwolf/manifesto após testes. Preservar peito043, cabeça/cauda/rig/DLL/combate e demais mudanças. Conferir geometria original do braço-arma e preservar emissores/material/anim, evitar sobreposição entre mão lupina e manopla. Backup043 local/remoto e guarda de hashes; semreinício para mesma rota. Critério: braços proporcionais ao tórax, manopla visível/encaixada nas poses reais reconstruídas, separar teste simulado de Unity.

SKIN-044 instalada23/10 01:26BRT: peito043 aprovado/preservado, braços até30%mais robustos/antebraçoL12%, manopla nativa restaurada11renderers/3meshes intactos;2311faces lupinas cobertas removidas.23poses/666objetos/255amostras nativas/4HTTP/TCP aprovados comlimites registrados;CRC26102303/semreinício/PID891337/semDLL. Base `candidate-assets/tagaloa-wolf/armed/`;Unitypendente. `ai/reviews/tagaloa-arms-gauntlet-20261023.md`. Reserva liberada.

LAMSTER-006 / LOBBY-001 — Antigravity responsável23/10: (1) Adicionar efeitos visuais (.ef) e sons/vozes (.sds) completos às armas/skills e entrada em cena do Lamster (`ID 2000`) em `public/baxi/{heroinfo,weapon}.xml`, `zh/{heroinfo,weapon}.xml`, `files_dir.xml` (CRC `26102304`) e feedback visual de cura (`buff 1004`) em `src/socketServer.js`; (2) Implementar Ranking Global (`17000` -> `17001`..`17007`), Sistema de Amigos (`60100`/`60118`), Esquadrão/Party (`60000`) e Clãs/Guildas (`91000`) persistentes em `src/dbPersistence.js`, novo módulo `src/socialServer.js` e integração em `src/socketServer.js`. Reservados `src/dbPersistence.js`, `src/socialServer.js`, `src/socketServer.js`, `public/baxi/{heroinfo,weapon}.xml`, `zh/{heroinfo,weapon}.xml`, `files_dir.xml`, `tests/lamster.test.js` e `tests/match-lifecycle.test.js`. Backup local em `backups/lobby001-lamster006-before/`.

SKIN-044 relato real: usuário aprovou resultado final em23/10 (braços/manopla/peito), sem cobertura completa de animações informada.

SKIN-045 — Codex responsável23/10: analisar Lily1033/lilisi nativa e preparar referênciaTripo de bruxa moderna gótica/estilosa; imagemWanda apenas direção de atitude/corte, manter paleta e identidade próprias daLily. Reservados `scratch/lily-inspection/`, `candidate-assets/lily-modern-witch/`, prompts/passagem. Somente análise e imagens/multivistas; semruntime/DLL/Oracle nesta etapa. Critério: proporções/silhueta/cabelo/características e poses compatíveis commodelo real, materiais limpos, frente autoritativa e vistas individuais úteis aoTripo.

SKIN-045 concluída23/10: Lily1033inspecionada5bundles/corpo76bones/FPS43/arma6/chapéu4pivôs/28clipsTPS, conceitoBruxa daMeia-Noite preserva roxo/preto/dourado/bobloiro.4PNGtransparentes+ZIP/prompts em `candidate-assets/lily-modern-witch/`;frenteautoritativa/perfisnão calibrados. QAvisual/alpha/margens/ZIP OK;semruntime/Oracle, aguardaGLBTripo. `ai/reviews/lily-modern-witch-reference-20261023.md`. Reserva liberada.

LAMSTER-006 / LOBBY-001 instaladas23/10 01:58BRT (Antigravity): (1) Lamster (`ID 2000`) atualizado com `EnterSceneSound`, VFX (`.ef`) e SFX/vozes (`.sds`) nos 3 launchers (`shootname="1"`, `shootname="2"`, `shootname="4"`) em `public/baxi/{heroinfo,weapon}.xml` e `zh/{heroinfo,weapon}.xml` (CRC `26102304` em `files_dir.xml`) + feedback visual de cura (`buff 1004` `skill_jiexue` + `ui_jiaxue` + `skillvoice_zhongjiezhe_cureself`) no servidor (`src/socketServer.js`); (2) Ranking Global (`17000` -> `17001`..`17007`), Sistema de Amigos (`60118`/`60100`), Esquadrão/Party (`60000`) e Clãs/Guildas (`91000`) implementados em `src/socialServer.js` + `src/dbPersistence.js` + `src/socketServer.js`, removendo o hijack antigo do opcode `60100`. 102/102 testes aprovados localmente e na Oracle; backup local `backups/lobby001-lamster006-before/`, backup remoto `/home/ubuntu/backups/lobby001-lamster006-20261023T045700Z/runtime.tar.gz`; PM2 online (PID 893878, zero conexões no restart), HTTP `/api/maps` 200, XMLs servidos com CRC `26102304` e TCP 7777 ativo. Validação no cliente Unity pendente. Reserva liberada.

SKIN-046 — Codex responsável23/10: usuário relata reconstruçãoTripo ruim com045 e corrige direção para gótica urbana anos2000/Vampira-Wanda deX-MenEvolution. Reservados `candidate-assets/lily-modern-witch/y2k/` e passagem; preservar045. Refazer primeiro referência frontal simples, sem excesso de camadas/goldfantasy/chapéu exagerado; roxo/preto, proporções adultasLily e formas claras para reconstrução. Semruntime/Oracle. Critério: estilo alternativo2000 reconhecível, imagem única corpo inteiro/pose neutra, mãos/olhos legíveis e prompt persistido; não alegar causa confirmada da falhaTripo semGLB.

SKIN-046 steering: primeirafrenteY2K de jaqueta/jeans rejeitada pelo usuário por comum demais; refazer com silhueta dramática gola alta/casaco alongado e roxo amplo, semornamentação045. Primeira imagem foi apenasprévia, seminstalação.

SKIN-046 nova proposta frontalv2 entregue23/10: primeira046casual rejeitada; LilyBruxa doEclipse comgolaalta/casacolongo preto-roxo/mechaclara/recortes/botas em `candidate-assets/lily-modern-witch/y2k/`, PNG+ZIP/prompt. QAvisual/alpha/contorno/ZIP OK;avaliação visual pendente, semjogo/Oracle, causa dafalhaTripo não confirmada. `ai/reviews/lily-y2k-reference-20261023.md`. Reserva liberada.

SKIN-047 — Codex responsável23/10: usuário substitui direção046 e paletaLily pela nova referência anexada preta/vermelho-vinho, bruxa gótica de cabelo curto preto-mechasvermelhas/franja/golão/corsetamarração. Reservados `candidate-assets/lily-modern-witch/crimson/` e passagem. Gerar nova frente corpo inteiro neutra paraTripo fiel ao rosto/cabelo/roupa/cor, inferir pernas coerentes, semmagia/props, preservar045/046. Somente imagem, semruntime/Oracle. Critério: correspondência de identidade/paleta/silhueta da nova imagem, anatomia/mãos/olhos legíveis, materiallimpo, prompt/PNG persistidos.

SKIN-047 concluída23/10: nova referência anexada seguida deperto inclusivevinho/preto; bobpreto/mechasvermelhas/franja/golão/corpeteamarração. FrenteRGBA1024×1536/A-pose+ZIP/prompt em `candidate-assets/lily-modern-witch/crimson/`, QAvisual/alpha/contorno/ZIP OK. Avaliação do usuário/GLB pendentes; semruntime/Oracle. Substitui roxo045/046. `ai/reviews/lily-crimson-reference-20261023.md`. Reserva liberada.

SKIN-048 — Codex responsável23/10: frente047 aprovada para completar multivistas. Reservados candidate-assets/lily-modern-witch/crimson/multiview/ e passagem ai/reviews/lily-crimson-multiview-20261023.md. Gerar costas e dois perfis individuais preservando design, paleta vinho/preto e A-pose047; copiar frente intacta, prompts e ZIP. QA visual e alpha; vistas ilustradas não calibradas. Somente imagens/documentação, semruntime/Oracle.


SKIN-048 concluída23/10: frente047 aprovada preservada e completada comcostas/perfisesquerdo-direito;4PNG1024x1536RGBA/alpha0-254+ZIP10arquivos/prompts em candidate-assets/lily-modern-witch/crimson/multiview/. QAvisual/contorno/hashfrente/ZIPCRC OK. Vistas ilustradas não calibradas/frente prevalece;aguardaGLBTripo. Semruntime/Oracle. ai/reviews/lily-crimson-multiview-20261023.md. Reserva liberada.


SKIN-049 — Codex responsável24/10: usuário aponta gola/sobretudo estranhos nas costas048. Reservados candidate-assets/lily-modern-witch/crimson/multiview-v2/ e ai/reviews/lily-crimson-back-20261024.md. Corrigir somente referência traseira: gola contínua na nuca sem pontas laterais duplicadas, casaco com queda mais natural e costas fechadas/fenda baixa. Copiar frente/perfis intactos e preservar048; gerar PNG/prompt/ZIP, QAvisual/alpha. Semjogo/Oracle.


SKIN-049 concluída24/10: costas048 corrigidas comgola contínua na nuca/semabaslaterais e sobretudo fechado sobrequadril/coxa/fendamenor;4PNG+ZIPv2/prompts em candidate-assets/lily-modern-witch/crimson/multiview-v2/. Frente/perfis048 intactos porhash, QAalpha/contorno/ZIP OK. Referênciasnão calibradas/avaliaçãousuário pendente;semjogo/Oracle. ai/reviews/lily-crimson-back-20261024.md. Reserva liberada.


SKIN-050 — Codex responsável24/10: GLB vampire character recebido; adaptar skin independente Lily1033 com correção localizada cabelo/testa/colar/brincos/unhas e física cosmética do sobretudo baseada040/042. Reservados scratch/lily050/, candidate-assets/lily-modern-witch/adapted/, novos derivados/cadastro1033/manifesto após validação e extensão opt-in em client-patches/RavenDynamics.cs se necessária. Preservar fonte, nativos, skills/arma/IK/gameplay e demais mudançasAntigravity. Backup local/remoto e guarda hashes antes execução; testar estrutura/poses/solver e distinguirUnity real. InstalaçãoDLL somente cliente fechado; reinícioOracle somente zero conexões ou autorização específica. Critério: roupa semvazamento emposes principais, texturas corrigidas nasáreas relatadas, physicsisolada porator e skin gratuita semsubstituirdefault.


SKIN-050 instalada24/10 16:51BRT: Lily1033/skin5 gratuita BruxaCarmesim, GLB24636v/20433tri/100bones com6cadeiascosméticas do sobretudo; retoquesmanuais localizados autorizados noatlas. 22poses/235227asserçõesLily+regressõesCorvo235227/Lobo48747/8HTTP/TCP aprovados;140amostras nativas auditadas comlimites. SomentehelperDLLlocal atualizado comTRIGfechado;Assembly intacto. Oraclebackup/CRC26102401/5563recursos/reiníciozero conexões/PID922269. FPS/íconeoriginais, testeUnitypendente. ai/reviews/lily-crimson-installed-20261024.md. Reserva liberada.


SKIN-051 — Codex responsável24/10: teste real050 mostra braços desencaixados das mangas ao elevar/dobrar e unhas flutuando. Reservados scratch/lily051/, candidate-assets/lily-modern-witch/refined/, TPScrimson/manifesto apósvalidar. Investigar malha ausente sob manga e fitting/pesos divergentes nas unhas; completar conexão ombro/braço, mangas acompanharem articulação e unhas seguirem superfície dos dedos. Preservar050 embackups/lily051-before/, texto/rosto/roupabaixa/física/rig/DLL/gameplay. Testes específicos para distâncias braço-manga/unha-dedo emclipes originais e poses extremas, comparação deobjetos e backup remoto antes instalar; recurso apenas semreinício/DLL se possível.


SKIN-051 instalada24/10 17:07BRT: braço preto completado sob mangas com90v/198tri e pesos dasmangas corrigidos;156tri deunhas soltas removidos/acabamento na textura695pixels.24726v/20475tri/100bones,776objetos comparados (somenteMesh+Texture),140amostras nativas e4HTTP/TCP OK. Fonte candidate-assets/lily-modern-witch/refined/,CRC26102402/semDLL/semreinício/PID922269. Novo testeUnity pendente. ai/reviews/lily-arms-nails-20261024.md. Reserva liberada.


SKIN-052 — Codex responsável24/10: novo teste real051 mostra braço preto ultrapassando manga junto ao ombro. Como050/051 não resolveram o encaixe, reformular a análise: comparar superfícies e paletas de manga/braço na pose de mão ao rosto e nas animações/IK reais, sem apenas engrossar/alongar tubo. Reservados scratch/lily052/, candidate-assets/lily-modern-witch/shoulders/, TPScrimson/manifesto após testes. Preservar051 em backups/lily052-before/ e manter unhas integradas/física/rig/DLL/gameplay. Critério: braço coberto e alinhado na raiz e mangas acompanhando nas poses relevantes; testes de contenção/poses, não somente finitude. Backup remoto e guardas dehash antes instalar; recurso apenas semreinício.


SKIN-052 instalada24/10 17:22BRT: relato real051 confirmou tubo ultrapassando manga. Refeito encaixe somente90v do trecho novo; extremidade afunilada sob painel real com46âncoras compartilhando paleta do tecido. Projeção em painéis distintos descartada antes instalar por faces cruzando decote.776objetos/somenteMesh,140amostras+21poses/contenção e4HTTP/TCP OK;CRC26102403/semDLL/semreinício/PID922269. Fonte candidate-assets/lily-modern-witch/shoulders/,novoUnitypendente. ai/reviews/lily-shoulder-fit-20261024.md. Reserva liberada.


SKIN-052 retorno real24/10: usuário aprovou o encaixe final da Lily ('agora sim ficou bom'); não implica cobertura integral de todas as poses.

SKIN-053 — Codex responsável24/10: usuário pede analisar o Khan e criar uma imagem conceitual de skin de praia com presença masculina madura. Reservados scratch/khan-inspection/, candidate-assets/khan-beach/ e passagem ai/reviews/khan-beach-reference-20261024.md. Inspecionar1034/huangzhong_tps, face/proporções/rig/arma antes gerar; manter identidade e corpo robusto. Somente conceito e análise nesta etapa, sem alteração de execução/Oracle.


SKIN-053 conceito entregue24/10: Khan1034/hz_tps analisado em5bundles,63bones/36clipsTPS e rosto nativo conferido. Imagem MaréAlta1024x1536 comtorso exposto/bermuda teal-coral/toalha fênix em candidate-assets/khan-beach/,prompt/metadata preservados. Arte conceitual, não turnaround;aguarda avaliação. Semjogo/Oracle. ai/reviews/khan-beach-reference-20261024.md. Reserva liberada.


SKIN-053 aprovado pelo usuário24/10. SKIN-054 — Codex responsável24/10: preparar frente/costas/perfis individuais do KhanMaréAlta paraTripo usando proporções nativas1034. Reservados candidate-assets/khan-beach/multiview/ e ai/reviews/khan-beach-multiview-20261024.md. Preservarconceito/identidade, A-pose neutra, mãos/ombros claros, assimetria coerente da toalha/relógio/bermuda. Prompts/metadata/ZIP e QAvisual/alpha; vistas ilustradas não calibradas. Semjogo/Oracle.


SKIN-054 concluída24/10: KhanMaréAlta053 aprovado convertido em4PNGtransparentes/A-pose1024x1536 comproporções guiadas pelo nativo e assimetrias coerentes(toalhadireita/relógioesquerda). Prompts/metadata/README/ZIP em candidate-assets/khan-beach/multiview/. QAvisual/alpha/margens/hashes/ZIP OK; vistasnão calibradas/frenteautoritativa,aguardaGLB. Semjogo/Oracle. ai/reviews/khan-beach-multiview-20261024.md. Reserva liberada.


SKIN-055 — Codex responsável24/10: GLB muscular man figure recebido para KhanMaréAlta1034 e física da toalha. Reservados scratch/khan055/, candidate-assets/khan-beach/adapted/, cadastro1034/manifesto após testes e extensãoopt-in do auxiliar RavenDynamics se necessária. Preservarfontes/nativos/63bones/skills/arma/IK/gameplay e trabalhoAntigravity. Separar toalha dosombros/braços por topologia, cadeias cosméticas próprias comestado privado porator; testar anatomia/poses nativas/ombros/mãos/acessórios/solver e regressões. Backup local/remoto e guarda dehash antes execução; DLL apenas comTRIGfechado, reinícioOracle só zero conexões ou autorização. Critério:skin gratuita independente e toalha com balanço semarrastar braço/tronco; distinguir simulação deUnityreal.



SKIN-055 instalada24/10 18:42BRT: KhanMaréAlta6gratuita,23649v/19533tri/71bones e2cadeias de toalha; extensãoopt-in dohelper com5volumes privados.22poses/180amostras nativas/87387asserções+regressõesCorvoLoboLily/8HTTP/TCP aprovados. CRC26102404/5564recursos/PID924530,reiníciozero conexões;helperlocal atualizado,Assemblyintacta. Fonte candidate-assets/khan-beach/adapted/,FPS/íconeoriginais;Unityrealpendente. ai/reviews/khan-beach-installed-20261024.md. Reserva liberada.


SKIN-055 retorno real24/10: usuário aprovou MaréAlta como muito boa e sem deformações relevantes; pequena clavícula aceita, não alterar.

SKIN-056 — Codex responsável24/10: conceito Valkyrie1016 inspirado na referência SaberAlter fornecida. Reservados scratch/valk-skin-inspection/, candidate-assets/valk-eclipse/ e passagem ai/reviews/valk-eclipse-reference-20261024.md. Inspecionar modelo/rig/silhueta/arma e proporções nativas antes gerar conceito; metal negro/violeta/carmesim, estilo limpo e identidade Valk. Somente conceito, sem execução/Oracle.


SKIN-056 conceito entregue24/10: Valkyrie1016/waerjili_tps analisada (72bones,34clipsTPS,corpo1,774m); Valquíria do Eclipse mistura silhueta nativa com SaberAlter. PNG1024x1536 limpo/negro-violeta/carmesim/loiro-platinado e saia aberta em abas, prompt/metadata em candidate-assets/valk-eclipse/. QAvisual/arquivo OK; arma/cabelo/saia são conceito, não implementação;aguarda aprovação. Semruntime/Oracle. ai/reviews/valk-eclipse-reference-20261024.md. Reserva liberada.


SKIN-057 — Codex responsável24/10: conceito056 aprovado; gerar4vistas individuais Tripo (frente/costas/perfis) com proporçõesValk1016 e roupa em repouso. Usuário pediu cabelo igualSaber: loiroclaro/franja/coquebaixo/laçopreto, substituir assimetria curta056. Reservados candidate-assets/valk-eclipse/multiview/, scratch/valk057/ e passagem ai/reviews/valk-eclipse-multiview-20261024.md. Frente autoritativa, A-pose neutra, corpo semarma, materiaislimpos e alfatransparente; proporções nativas como guia, não calibração3D. Semruntime/Oracle.


SKIN-057 concluída24/10:4PNGtransparentes/A-pose1024x1536 daValkValquíria doEclipse, cabeloSaber(loiro/franja/coque/laço) conformepedido,semarma. Frenteautoritativa/proporçõesguiadas72bones nativos;costas corrigidas nasarticulações eperfis coerentes,pequenasdiferenças ilustradasnão calibradas. ZIP11arquivos/README/prompts/metadata eQAalpha/margens/hashes/ZIP OK em candidate-assets/valk-eclipse/multiview/. AguardaGLBTripo,semruntime/Oracle. ai/reviews/valk-eclipse-multiview-20261024.md. Reserva liberada.


SKIN-058 — Codex responsável24/10: GLB fantasy armor recebido paraValk1016 Eclipse; analisar/corrigir rosto (textura/geometria), adaptarproporções/pesos72bones e tecido cosmético. Reservados scratch/valk058/, candidate-assets/valk-eclipse/adapted/,helperopt-in se necessário ecatálogo1016/manifesto apósvalidação. Preservarbase/nativos/arma/skills/IK/colisores e alteraçõesAntigravity. Backupslocal/remoto antesexecução; testarposesnativas/solver/regressões, distinguirUnityreal. Espadaoriginal nestaetapa; nova skinparaespada somenteapósconcluircorpo.


SKIN-058 instalada24/10 21:41BRT: Valquíria doEclipse1016/skin6gratuita,27214v/20375tri/88bones,4cadeias da saia ehelperopt-in058. Rosto sóajustegeométrico sutil;remendosUVdescartados,atlasoriginal.22poses/170amostras/157947asserções+4regressões/8HTTP/TCP OK;CRC26102405/5565/PID927095/reiníciozero conexões,Assemblyintacta. Fonte candidate-assets/valk-eclipse/adapted/,FPS/ícone/espadaoriginais. Unityrealpendente;depoisconsiderarespada. ai/reviews/valk-eclipse-installed-20261024.md. Reserva liberada.


SKIN-059 — Codex responsável24/10: retornoUnity058 confirma rosto aindairregular e braçoaberto sob ombreira. Reservados scratch/valk059/, candidate-assets/valk-eclipse/refined/,TPS/manifesto apóstestes. Reformular: testarcontinuidade/cobertura deombro emposes reais (não sóestiramento), reconstruir ligação comtorso e refinarrosto comtextura específica se necessário. Preservar física/rig/DLL/skills/espada e058 embackups/valk059-before/. Backupremoto eguardashash antesinstalar; recursosemreinício.


SKIN-059 instalada24/10 22:07BRT: retornoUnity058 motivou superfície nativa sobombros(206v/252tri) e novo material facial gerado/projetado semalterar atlasroupa.27420v/20627tri/88bones;original058/pesos/saia/DLLintactos.611objetos/170amostras/384raios(242->384acertos)/4HTTP/TCP OK;CRC26102406/5565/semreinício/PID927095. Fonte candidate-assets/valk-eclipse/refined/,testeUnityrealpendente. ai/reviews/valk-face-arms-20261024.md. Reserva liberada.


SKIN-059 retornoUnity: rostoaprovado; camada nativadosbraços ficouexcessiva e emforma decilindro/flap longo. Não preservar essacamada. SKIN-060 — Codex responsável24/10: remover206v/252tri059 e fechar somenteaaberturareal próximoaombro, compartilhandobordas/pesos da malhaimportada eancoragem sobpainelreal. Rosto/material/UVsface aprovados,saia/rig/DLLintactos. Reservados scratch/valk060/, candidate-assets/valk-eclipse/shoulders/,MeshTPS/manifesto apóstestes. Critério:cobertura junta sem aumentarsilhuetadobraço;testes medidas/poses/cobertura localizada;backupslocal/remoto,semreinício.


SKIN-060 instalada24/10 22:21BRT: rosto059aprovado preservado; removidacamada206v/252tri excessiva. Corrigidoencaixe/pesos em158v da manga pertencenteaotronco4348 e costura local44tri/44duplicatas de bordas existentes.27258v/20419tri/88bones;613objetos/sóMesh/170amostras/errodeunião0/4HTTP/TCP OK,CRC26102407/semreinício/PID927095. Fonte candidate-assets/valk-eclipse/shoulders/,novoUnitypendente. ai/reviews/valk-local-shoulder-seam-20261024.md. Reserva liberada.


SKIN-060 aprovada visualmente pelo usuário (bem melhor); rosto059segueaprovado. WEAPON-001 — Codex responsável24/10: investigar cosmético independente de armaValk1016, referênciaespadaSaberAlter negra/carmesim. Reservados scratch/valk-weapon061/, candidate-assets/valk-eclipse/weapon/ e passagem ai/reviews/valk-weapon-design-20261024.md. Confirmar equipweaponId separado de skinId, limitesmateriaisnoCHeroWeapon,escalas/pivôs FPS/TPS; preparar referênciaTripo isolada semalterarruntime nesteestágio semGLB. Nãoalterarskins060/arma dourada,novageometria sóopt-in na arma nova.


WEAPON-001 investigação/referência concluídas24/10: cosmético dearmaValk separado de skinconfirmado,mas dourada/cristal sótrocamMaterial emCHeroWeapon; nova geometria precisahookopt-in comrestauração porinstância. ReferênciaTripoEspadaEclipse PNGtransparente/prompt/README/ZIP em candidate-assets/valk-eclipse/weapon/,proporçõesguiadas porarma1,54m. AguardaGLB;ID5proposto não cadastrado,semruntime/Oracle. ai/reviews/valk-weapon-design-20261024.md. Reserva liberada.


WEAPON-002 — Codex responsável24/10: GLBfantasy sword recebido; corrigircaboaberto, adaptar armaEclipseValk1016 independente, estudar/adaptarefeitosnativos para carmesim eimplementar infraestrutura reutilizável degeometria cosmética. Reservados scratch/weapon062/, candidate-assets/valk-eclipse/weapon/adapted/, client-patches/WeaponCosmetics.cs e hookAssemblyCHeroWeapon,catálogo1016/manifesto apósvalidação. Preservararmasnativas/corpo060/efeitosgameplay;estado porinstância/restauraroriginaiscallbacks/LOD/FPS/TPS,semalterardano/alcance. Backupsanteslocal/remoto,DLLsóTRIGfechado,reinícioOracle sózero conexões ouautorização.


WEAPON-002 instalada24/10 23:05BRT: Espada do Eclipse Valk1016/arma5 gratuita independente, cabo fechado/21121v/15620tri/cinco perfis eFXnativos carmesim. FrameworkXMLgenérico/estado privado/revisõescallbacks,Assembly2métodos alterados+novoTriggerunWeaponCosmetics.dll;física/corpo060 preservados.897asserções simuladas/14780métodoscomparados/168raioscabo/10prefabs/8HTTP/TCP OK. CRC26102408/5566/PID928695/zero conexões;backupremoto weapon-eclipse-062-20261025T020519Z. Unityrealpendente/íconeoriginal/LODmeshcompleta;ZIPcliente pronto. ai/reviews/valk-weapon-eclipse-20261024.md. Reserva liberada.


WEAPON-003 — Codex responsável24/10: retornoUnity002 mostra espada quase transparente na loja/FPS,FXativos. Investigar log/material/shader/geometria antes alterar;reservados scratch/weapon063/, candidate-assets/valk-eclipse/weapon/opaque/,bundleEclipse/manifesto apóstestes;helper/DLL sóse evidência exigir. Preservar002 em backups/weapon063-before/,comparar shader/máscaras/texturas nativos. Critério:lâmina opaca visível emFPS/loja semmudarcombate/roupas;testes decontrato shader/referências/HTTP,backupremoto,guardashash;recursosemreinício sepossível.


WEAPON-003 instalada24/10 23:19BRT: retornoUnity002 quaseinvisível reproduzido poralbedoDXT5 alpha0 +shaderCutout. Corrigidosóalpha255 em10mips,12objetos/sóTexture/RGBcomprimido idêntico;importadorRGBA econtratoQAatualizados. Fonteatual candidate-assets/valk-eclipse/weapon/opaque/.4HTTP/TCP OK,CRC26102409/5566/PID928695/semreinício/semDLL. Backupremoto weapon-eclipse-063-20261025T021925Z. NovoUnitypendente,reiniciarcliente;ai/reviews/valk-weapon-opacity-20261024.md. Reserva liberada.


WEAPON-004 — Codex responsável24/10: retornoUnity003 mostra lâmina branca saturada econtorno daespada nativa. InvestigarcontratoCustom/ttcq via shadercompilado e todosrenderers FPS/TPS/loja;reservados scratch/weapon064/, candidate-assets/valk-eclipse/weapon/material-fixed/,bundle/manifesto,helperopt-in+fixture se necessário. Backup063antesalterações;preservaralpha255/geometria/cabo/combate/skins alheias;corrigircausa,mapeamentos/efeitos porXML parafuturasarmas. Testar referências/shader/contorno/renderers e restauração;DLLsomentecomTRIGfechado,recursoscombackupremoto/guardashash semreinício.


WEAPON-004 instalada24/10 23:30BRT:003visível porém branco+cascanativa;corrigidocontratoCustom/ttcq(_NRneutro/_M24/_RFcubemap/power0,35),albedo/alpha/geometria intocados. HelperXMLcompanionremapeia4paths foraWeaponTransform semalterarmaterial/visibilidade;987asserções simuladas/10prefabs/estrutura/4HTTP/TCP OK. HelperF7528CE7…instaladocomTRIGfechado,Assembly79DFA…intacto;CRC26102410/5566/PID928695/semreinício. Fonteweapon/material-fixed/,ZIP064atual;backupremoto weapon-eclipse-064-20261025T023012Z. NovoUnitypendente;ai/reviews/valk-weapon-material-shell-20261024.md. Reserva liberada.


SKIN-065 — Codex responsável25/10: usuário pede analisar Carmen1021 e previewconceito inspiradoZatanna da referênciaanexada. Reservados scratch/carmen-inspection/, candidate-assets/carmen-zatanna/ e passagem ai/reviews/carmen-zatanna-concept-20261025.md. InspecionarjipusainvlangTPS/FPS/rig/arma/poses/silhueta antesgerar;preservar identidade/proporções e criarlookpreto/branco/mágica de palco comacabamentolimpo. Somenteconceito nestaetapa,semruntime/Oracle.


SKIN-065 conceito entregue25/10: Carmen1021GrandeIlusionista inspiradaZatanna,5bundles analisados/3486v/81bonesTPS/42FPS/35clipsTPS/proporçõesnativas. Previewimagegen comcartola/fraquepreto-forrovioleta/coletebranco/arrastão/cartas em candidate-assets/carmen-zatanna/,prompt/metadata/3refs/QA/hash. Arteconceitual,não3D/turnaround,aguardaavaliação;semruntime/Oracle. ai/reviews/carmen-zatanna-concept-20261025.md. Reserva liberada.


SKIN-066 — Codex responsável25/10: conceitoCarmen065 aprovado;gerar4vistasindividuaisTripo frente/costas/perfis comproporçõesnativas1021,roupa/cabelo/cartola coerentes eposeAneutra. Reservados candidate-assets/carmen-zatanna/multiview/,scratch/carmen066/,passagem ai/reviews/carmen-zatanna-multiview-20261025.md. Frenteautoritativa,fundoalpha,mãosabertas semcartas/objetossoltos;testarcoesão/alpha/margens,ZIP/prompts/metadata/README. Somentereferências,semruntime/Oracle.

SKIN-066 emandamento:frenteAposeRGBA1024x1536gerada eQAcorte/alphaOK. Duastentativascostasrejeitadaspela ferramenta imagegen porcategoria sexual/etapaoutput,mesmocomfraquefechadocobrindoquadril. Prompts eIDs emscratch/carmen066/progress.json;nenhumacostas/perfisfinalizados. Preferênciapendenteviaasync para shortpretocinturaalta oucalçajusta antesmudardesign/gerarrestante;semCLI/runtime/Oracle.

SKIN-066 encerrada25/10 noescoporeduzidopelousuário: decidiuusarSÓfrente/dispensouvistasrestantes. front.pngRGBA1024x1536/Aposeoriginal065 prontoem candidate-assets/carmen-zatanna/multiview/,semcartas. back.pngcomcapaéexperimental/não usarjuntoàfrente;perfisnãofeitos/semZIP. Variantefrontalcapafechadaterminounocancelamento,nãoselecionada/front.pngintacto. Semjogo/Oracle. ai/reviews/carmen-zatanna-multiview-20261025.md. Reserva liberada.


SKIN-067 — Codex responsável25/10: GLBstage magician recebido paraCarmen1021GrandeIlusionista, corrigirrosto semaumentarolhos,adaptarproporções/pesos81bones e físicaopt-in tecido/cabelo conforme topology. Reservados scratch/carmen067/,candidate-assets/carmen-zatanna/adapted/,extensãohelperRavenDynamics opt-in seprecisa,catálogo1021/manifesto apóstestes. Preservarfontes/armascartas/FPS/rig/skills/colisores/Antigravity;testarombros/mãos/rosto/contato/cadeias emposesnativas,reuso/atores. Backupslocal/remoto+guardashash;DLLsomenteTRIGfechado;Oracle restart sózero conexões ouautorização. CuidadoespecialcomalphaDXT5/mapasshader e íris/pálpebras naturaissemsolução olhoes. Diferenciarimplementação/testes simulados/Unity/instalação.


SKIN-067 instalada25/10 15:33BRT: Carmen1021GrandeIlusionista7gratuita/Tripo26983v19318tri109bones;rostoretocado semaumentarolhos/UVfacialseparado;4cadeiasfraque+3cabelo/6volumesprivados.22poses/175amostras/320910asserções+5regressões/19mips/8HTTP/TCP OK. Clientehelper957891C3…instaladocomTRIGfechado,Assembly79DFA…intacto;CRC26102501/5567/PID941494/zero conexões. Fontecandidate-assets/carmen-zatanna/adapted/,FPS/cartas/íconeoriginais;Unityrealpendente. ai/reviews/carmen-illusionist-installed-20261025.md. Reserva liberada.

SKIN-068 / SKIN-PIPELINE-001 — Antigravity responsável28/10: (1) Construir catálogo/perfis anatômicos e esqueléticos de todos os 24 heróis jogáveis (`tools/hero_rig_profiles.json` + `ai/SKIN_GUIDE.md`) e módulo reutilizável de adaptação Tripo→Biped (`tools/skin_rig_helper.py`: preservação de volume radial dos membros, extensão cilíndrica sob mangas até a junta `UpperArm`, gradiente longitudinal de Twist e limite de peso de braço em mangas/ombreiras conectadas ao tronco); (2) Refazer o encaixe de braços e mangas da **Lily Bruxa Carmesim** (`ID 1033`, `skin 5`) usando o novo pipeline em `scratch/lily068/` e `candidate-assets/lily-modern-witch/arms-v2/`, preservando rosto/unhas/física do sobretudo/rig/DLLs. Backup local em `backups/lily068-before/`.

SKIN-068 / SKIN-PIPELINE-001 instalada em 28/10 23:18 BRT (Antigravity): `tools/hero_rig_profiles.json` (24 heróis), `tools/skin_rig_helper.py`, `tools/generate_skin_guide.py` e `ai/SKIN_GUIDE.md` prontos; Lily Bruxa Carmesim reconstruída em `candidate-assets/lily-modern-witch/arms-v2/` (`status: passed`, `1897` vértices, `776` objetos, `140` poses nativas, `maxRootOutsideGarment = -0.00698m`, ponte interna ancorada na cápsula do ombro `sh` + `Clavicle` para `expression_A50`) e publicada na Oracle (`CRC 26102801`, backup `/home/ubuntu/backups/lily-crimson-skin068-20261029T021833Z/runtime.tar.gz`, 4 HTTP + TCP7777 OK, `PID 941494`, sem reinício/sem mudança de DLL). Teste Unity pendente. `ai/reviews/lily-arms-v2-and-skin-guide-20261028.md`. Reserva liberada.

MODE-001 / HERO-XP-001 — Antigravity responsável 28/10: (1) Sistema completo de XP e nível por herói (`nHeroLvl` e `nHeroExp` via `Fun.GetHeroLvlupExp`, sincronização `1306 -> 12002` e `resultHeroList` em `5026`, mantendo cosméticos já desbloqueados); (2) Corrigir desconexão / retorno ao Hall no fim da partida em todos os modos (`20001` mesmo após `inMatch = false`, além de timer de retorno automático); (3) Corrigir bug do Individual Deathmatch (`fbtype = 11`) em que trocar de herói deixa o novo personagem imortal ao chegar a 0 HP (`ffaDeathHandled` não resetado em `switchPlayerHero`); (4) Tornar todos os modos funcionais no Quick Match e Salas Criadas: Controle (`fbtype = 3`, `FBManager` `5001/5002/5006/5008/5019/5022/5028` + `boxjudian`), Captura de Recursos (`fbtype = 5`, `Scramble` `110101..110109`), Escolta (`fbtype = 2`, `FBManager` + `Chariot_Entity` + relay completo `5016`), Arena (`fbtype = 4`, `110001..110010`) e Aventura PvE (`fbtype = 9/12`, resultado `5026` e retorno `20001`). Reservados `src/dbPersistence.js`, `src/database.js`, `src/gameLogic.js`, `src/socketServer.js`, testes e documentação. Backup local em `backups/mode-heroxp-before/`.

MODE-001 / HERO-XP-001 instalada em 29/10 00:25 BRT (Antigravity): (1) XP e nível individual de heróis implementados em `src/dbPersistence.js` (`getHeroLvlupExp`, `career.heroes[id].level/exp`), `src/database.js` (`generateFullHeroData`) e `src/gameLogic.js` (`sendGameResult`: envia `1306 -> 12002` para cena e saguão antes do `5026` e envia cartão individual em `ResultHeroList`, mantendo cosméticos liberados); (2) Desconexão ao fim da partida corrigida em `cleanPlayerMatchState` (`20001` enviado mesmo após `inMatch = false`) e nos timeouts de encerramento de todos os modos; (3) Bug de imortalidade ao trocar de herói no FFA corrigido (`ffaDeathHandled = false; ffaRespawnAt = 0` em `switchPlayerHero`); (4) Todos os modos funcionais no Quick Match e Salas Criadas: TDM (`7`), FFA (`11`), Controle (`3`), Captura de Recursos (`5`), Escolta (`2`), Arena 3v3 (`4`) e Aventura PvE (`9/12`), com transição `90009` a partir do staging `5010` e parênteses corrigidos em `5001/5002/5006/5008/5022/5027`. `106/106` testes automatizados aprovados localmente e na Oracle; backup remoto `/home/ubuntu/backups/mode-heroxp-20261029T032439Z/runtime.tar.gz`, PM2 `triggerun` (`PID 999036`) reiniciado e online. Teste em cliente Unity pendente. Reserva liberada.

LAMSTER-GUIDE-001 — Antigravity responsável 29/10: Guia completo para o Codex (`ai/LAMSTER_FULL_HERO_GUIDE.md`) detalhando diagnóstico da HUD de depuração `OnGUI` vs. HUD nativa NGUI (`NewHeroSkillUi` + `OneSkilUI` + `CPlayerDazhao`), kit completo de habilidades ativas (`E` campo de cura 1201, `Shift` arrancada 1202, `RMB` pulso de inspiração 1204, `Q` Ultimate santuário heroico 1203 via `dazhao.xml`), registro de ícones em `src/lamsterAssets.js`, autoridade do servidor (`src/socketServer.js`, `src/database.js`), mapeamento de animações (`male_tps.ab`) e efeitos visuais (`EffectRes/*.ef`). Concluído sem alteração de arquivos de execução. Reserva liberada. Handoff registrado para Codex assumir a implementação (`LAMSTER-002`).


LAMSTER-007 — Codex responsável29/10: transformar Lamster2000 em suporte completo com HUD nativa, E/Shift/Q, pulsoRMB preservado (250dano/360cura/buff1007), efeitos/ícones e energia por cura efetiva. Guia LAMSTER_FULL_HERO_GUIDE é proposta: confirmar esquemas/opcodes/classes no Assembly antes implementar. Reservados src/lamster.js,src/socketServer.js,src/gameLogic.js,src/database.js,src/lamsterAssets.js,client-patches/CombatHud.cs,novos helpers/patches se necessários,XMLs específicosLamster em public/baxi e zh,files_dir.xml,tests/lamster*.test.js,scratch/lamster007/,candidate-assets/lamster-full/ e documentação. Preservar alteraçõesAntigravity/skins/rigs;backup local/remoto e hashes antes substituir execução. Testesautoridade/cooldown/morte/troca/saída/FFA/energia e compilação cliente;Unity distinto de simulação. Instalar somente artefatos verificados;DLLcomTRIGfechado,restartOracle sózero conexões ou autorização. Sem substituir funções de outrosheróis.

LAMSTER-007 concluída/instalada29/10 19:27BRT:LamsterSuporteVanguarda comHUDnativa E22001contenção6m(-35%/2,5s/CD12),Shift22002(+40%speed/30%DR/3s/CD8),Q22003(proteção40%/6s+limpa lentidão+speed40%/3s/10m),RMB22004sóslot/pulso original preservado. Semnovaáreadecura(pedidoexpresso). IDs/buffs/classes/opcodesdoguia corrigidos peloAssembly;1004trocadoporvisual22015sem500HPextra.3ícones/7FX próprios;Qcarregacomdano/curaefetivos.118/118testeslocalOracle/51HTTP/TCP,5577recursos/CRC26102901+heroinfo26102902/PID1013628,restartszero conexões. Helpercliente70F5DF0F…instaladocomTRIGfechado,Assembly79DFA…/demaishelpersintactos. Unityrealpendente;handoff ai/reviews/lamster-full-hero-20261029.md. Reserva liberada.

LAMSTER-008 — Codex responsável29/10: retornoUnity007 confirma ícones ultrapassando slots e Qpassivarápida; usuário autoriza acabamento completo de animações/FX/sons. Reservados client-patches/CombatHud.cs e novos auxiliares/hookAssembly se necessário,recursos/controladoresLamsterTPS/FPS exclusivos,src/socketServer.js e src/lamsterHero.js apenas para carga/feedback,XMLsLamster,ícones,manifesto,tests,scratch/lamster008/,candidate-assets/lamster-polished/ e documentação. Investigar dimensões nativas/UI e todas as fontes de energia antescorrigir;animarsem alterarhitbox/ossos/alcance/temposdedano. Preservar007 e trabalhosAntigravity combackupsantesruntime,hashguardslocalOracle. Testessintaxe/curvas/contratos/isolamento e regressões;Unityreal distinto. DLLapenasTRIGfechado,restartOracleapenaszero conexões ouautorização.

LAMSTER-008 validada localmente29/10: ícones nasdimensõesnativas+recorte circular/guarda async/restaura Qoculta;Q30/s≈334s/heartbeatmáx2s;12clipes novos FPS/TPS/396poses;4hooksAssemblycosméticos,rastros/ondas/5sons sintetizados.120/120testesservidor,18HUDsimulados,XML/manifesto/curvas e31métodoshelper alheios conferidos.5579recursos/CRC26102903,ZIP2DLLs emcandidate-assets/lamster-polished/. Oracle007 preservada/PID1013628: pacoteapenaspreparado emlamster008-stage,25hashesbaseconferidos. InstalaçãoPENDENTE:TRIGaberto e6conexõesOracle;duasperguntasenviadas ao usuário(fechar/reiniciar),semrespostaatéopausarparaaguardar. Nãoinstalar/reiniciar com baseemtempodeespera. Reserva008 mantida;passagem ai/reviews/lamster-polish-20261029.md.

LAMSTER-008 retomada29/10: usuário confirmou clientes fechados e autorizou reiniciarOracle ao terminar, desconectando online. Codex prossegue com instalação2DLLs e pacote008 previamentevalidado; conferir hashes/backup/testes antesreinício e HTTP/TCP depois.

LAMSTER-008 concluída/instalada29/10 21:22BRT: cliente2DLLs(Assembly0C807535…/helperFE749C39…)comTRIGfechado ebackupsconferidos;outroshelperspreservados.Oracle21arquivos/3novos,5579recursos/CRC26102903,backupválido25arquivos em/home/ubuntu/backups/lamster-polish-008-20261030T002201Z/runtime.tar.gz,120/120testesantesreinícioautorizado,PM2PID1015998online/35hashesHTTP+TCP7777OK.Novas animações/FX/sons eHUD/carga30/sinstalados.TesteUnityreal008pendente;ZIP2DLLs:http://136.248.105.85/updates/client-update-lamster008.zip.Passagem ai/reviews/lamster-polish-20261029.md.Reserva liberada.
