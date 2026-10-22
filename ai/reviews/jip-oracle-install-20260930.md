# Passagem — instalação TDM-002

- Responsável: Codex; autorização explícita do usuário; 2026-09-30 01:34 UTC / 29/09 22:34 BRT.
- Estado: instalação concluída; teste com cliente pendente.
- Escopo instalado: src/socketServer.js e tests/match-lifecycle.test.js. gameLogic.js não alterado.
- Conferência: hashes remotos anteriores coincidiram com a base do patch; arquivos enviados conferidos antes e depois da instalação.
- Backup remoto: /home/ubuntu/backups/jip-selection-20260930T013437Z/runtime.tar.gz e logs.tar.gz (inclui telemetria e logs PM2 anteriores ao reinício).
- SHA256 socketServer instalado: fcca2443b481c4d785b79219918b751c72d3ae28fd513efefe046a5aca9c2ed2.
- SHA256 teste instalado: cec346918a58b493a674f4b56c9e0a3d9aeb93dc3c4adae301d9a63cf6ec0745.
- Validação Oracle: sintaxe dos dois arquivos aprovada; node --test tests/lamster.test.js tests/match-lifecycle.test.js: 29 aprovados, zero falhas.
- Evidência local: scratch/jip-deploy-20260930/tests.tap e install.sh.
- PM2 triggerun: reiniciado uma vez, PID431377, contador172, online após22s, sem novo reinício observado.
- Saúde: porta7777 escutando e conexão TCP externa concluída. HTTP /api/maps respondeu200. /files_dir.xml na raiz retornou404 (rota inadequada para esta sondagem).
- Chave SSH temporária removida após uso.
- Cliente real: ainda não testado após instalar. Não declarar o travamento resolvido só pelos testes simulados.
- Próximo passo: dois clientes/contas, iniciar TDM e entrar depois via matchmaking e lista; verificar escolha manual e contador, conservando logs por processo.
- Reversão disponível: restaurar runtime.tar.gz no diretório server-tr, conferir sintaxe e reiniciar triggerun; preservar logs novos antes de reverter.
