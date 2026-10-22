# Passagem de trabalho — PVE-001

- Responsável: Codex; implementação/instalação concluídas em 2026-10-06 20:43 UTC (17:43 BRT); reservas liberadas.
- Objetivo: primeiro passo do Aventura — modelos, navegação/combate dos bots e carregamento coerente. Estado: implementação e testes automatizados validados; cliente real pendente.
- Base preservada: backups/pve-20261004-before; hashes coincidiram com a Oracle antes da instalação, sem sobrescrever trabalho concorrente.
- Causas: gameLogic.spawnPveWave usava Npc.xml 1105/1106 etc. (robôs de treino), confundindo com robot.xml; movimento reto artificial e socket inexistente interrompiam ondas.
- Carregamento: database e fbset.xml cadastravam 5105/5115 como type7. Corrigidos para 9/12; manifesto fbset crc100005; transição da seleção usa90009 antes de CLoadScene.
- Bots: RobotAI/RobotCreate com hero/createIndex corretos e origem da rota XML; dados gerados por tools/generate-pve-robots.cjs em src/pveRobots.json. O cliente usa sua NavMesh, percepção, armas e animações nativas.
- Evidência: RobotAI.RobotCreate usa XZY; RobotAIController.SyncPosition envia XYZ. Corrigida retransmissão SSP→SCP, RST→RCT, RCR e SA_Robot→CA com arrays/base62; SB mantém ator bot e loadID.
- Autoridade: CheckPlayer a cada2s somente enquanto modelo/autoridade pendente; 4010002 escolhe um cliente por bot/cena, 4010001 concede, saída/morte do controlador permite transferência. Não chamar CheckPlayer periodicamente no controlador ativo (limpa SyncNetObj).
- Combate: SDG existente reconhece activeBots; aceita dano do monstro somente pelo controlador; morte atualiza a onda/sala correta. Cristal único por sala; timers/entidades limpos ao sair; entrada tardia não recria cristal nem reinicia HUD dos presentes.
- Cooperação: humanos no time1 em fila/sala/entrada; backfill PvP exige tipo exato para não misturar PvE. TDM/FFA preservados.
- Arquivos: src/gameLogic.js, src/socketServer.js, src/database.js, src/pveRobots.json, public/baxi/fbset.xml, zh/fbset.xml, files_dir.xml, tools/generate-pve-robots.cjs, tests/match-lifecycle.test.js.
- Testes: node --check nos JS alterados; node --test tests/logging.test.js tests/lamster.test.js tests/match-lifecycle.test.js — 81/81 local e Oracle. Incluem autoridade, dano bidirecional, tiros, posição, entrada tardia, isolamento, ondas/vitória/derrota, morte/saída e cooperação.
- Cliente real: não executado. IL do Assembly instalado confirmou os métodos nativos; nenhum DLL alterado. Precisa fechar/reabrir TODOS os clientes para atualizar fbset antes do teste.
- Limites: mantém o protótipo de3ondas, vida/balanceamento aproximados e portas da Terra das Trevas abertas; não restaura campanha/recompensas/progressão original. Inimigos nativos sem rota (arqueiros/crocodilo/chefe) usam origens de patrulhas da zona como posições escolhidas pelo servidor. Geometria, assets e animações dependem do teste Unity.
- Oracle: instalada em /home/ubuntu/server-tr; PM2triggerun online PID566485, restart180, zero jogadores no início. HTTP/api/maps200, TCP7777 e CDN/res/fbset+manifesto conferidos.
- Backup remoto: /home/ubuntu/backups/pve-20261006T204337Z/{runtime,logs}.tar.gz.
- Evidências: scratch/pve-deploy-20261006/{base.sha256,new.sha256,local-tests.tap,oracle-tests.tap,install.sh}; stage remoto /home/ubuntu/pve-stage-20261006.
- Próximo passo: testar Mina Fácil com2clientes reiniciados: carregar, conferir modelos, seguir ondas (fuzileiros na2ª), dano/tiros, sair com controlador e confirmar continuidade. Testar Terra das Trevas separadamente; coletar output_log e eventos PVE_WAVE/PVE_BOT_AUTHORITY se falhar.
