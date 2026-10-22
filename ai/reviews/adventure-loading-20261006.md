# Passagem — PVE-002: seleção e loading

- Responsável: Codex, 2026-10-06; implementação e instalação concluídas; cliente real pendente.
- Objetivo: permitir selecionar herói no Aventura e sair do loading em0% pelo fluxo correto de cena.
- Evidência: output_log.txt do cliente, sessão20:46UTC: 90001 mapa5105/tipo9, seguido de90004 imediato, mHeroPlayer/90005 e várias confirmações90002 sem CLoadScene. Não houve conclusão de carregamento.
- Causa: processMatchmakingQueue usava PvE direto no destino, waitingForHeroLock=false e loading automático. Confirmação era tratada como troca em combate antes de isCombatReady, portanto não iniciava a transição inicial. 90004 apenas exibe UI de loading (MatchManager.OnShowLoad).
- Alteração: todos os modos da fila entram no staging5010 com seleção pendente, contador60 e sem90004 automático. Confirmação/timeout usa fluxo existente,90009 atualiza destino9/12 e CLoadScene carrega o mapa. Preservadas equipes cooperativas e dificuldades.
- Arquivos: src/socketServer.js, tests/match-lifecycle.test.js; reservas liberadas.
- Backup local: backups/pve-loading-20261006-before. Hashes remotos conferidos antes da instalação.
- Testes: sintaxe e85/85 local/Oracle; quatro regressões novas cobrem fila→NGOL→staging→prévia→confirmação/timeout→90009→CLoadScene→SLoadMapOK→spawn/PvE, nos tipos9/12. A verificação anterior de PvE foi corrigida: esperar waitingForHeroLock=false reproduzia o defeito, não o contrato desejado.
- Cliente real: ainda não testado após esta correção; teste anterior confirmou falha doPVE-001 antes de entrar no mapa, logo não validou bots.
- Oracle: instalada06/10 20:49UTC (17:49BRT), zero conexões antes de reiniciar; PM2PID566915/restart181 online; HTTP200 eTCP7777OK.
- Backup remoto: /home/ubuntu/backups/pve-loading-20261006T204943Z/{runtime,logs}.tar.gz.
- Evidências de instalação: scratch/pve-loading-deploy-20261006/{base.sha256,new.sha256,local-tests.tap,oracle-tests.tap,install.sh}.
- Próximo passo: reabrir cliente, fila Mina Fácil, escolher/confirmar herói e verificar carregamento e primeira onda. Depois testar Terra das Trevas e dois clientes; coletar log se falhar.
