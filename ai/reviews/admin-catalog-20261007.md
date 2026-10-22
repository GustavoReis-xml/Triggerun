# Passagem de trabalho — ADMIN-001

- Responsável: Codex; concluído em 07/10/2026 03:07 UTC; reservas liberadas.
- Objetivo: listar e permitir criar as definições de entidades existentes pelo painel admin.
- Estado: implementação instalada; validação automatizada e navegador concluídas; cliente real pendente.
- Base/backups locais: backups/admin-20261006-before; hashes em scratch/admin-deploy-20261007/base.sha256.
- Arquivos: src/entityCatalog.js (novo), src/http.js, src/gameLogic.js, public/admin/index.html, tests/entity-catalog.test.js (novo), tests/match-lifecycle.test.js.
- Catálogo: 803 chaves únicas = 180 modelos de heroinfo.xml + 58 NPCs de Npc.xml + 565 definições de robot.xml. Npc1125 duplicado resolve para última definição. IDs separados por origem, busca, categorias e lista completa em /api/entities.
- Spawn: NPC via Initialize, modelos/bots parados via dummy existente, bots nativos via RobotAI/createIndex e autoridade PvE compartilhada. Controller admin separado das ondas/HUD reais. Broadcast restrito à cena selecionada, coordenadas zero preservadas, IDs sem colisão.
- Limpeza: /api/entities/clear remove apenas isAdminSpawn da cena escolhida; entidades legítimas/ondas não são alvos.
- Limites: NPCs de habilidades podem exigir contexto original; rotas nativas dependem do mapa; escala só aplicada inicialmente a NPCs. Catálogo cobre as três tabelas, não afirma listar cada prefab genérico dos assets.
- Testes automatizados: 90/90 local e Oracle, sintaxe OK. Cobrem catálogo, IDs/origens, spawn e isolamento de cena, rejeições, controller nativo separado e regressões existentes.
- Navegador: busca robot:1101 retorna 1/803, seleção preenche chave/dropdown; tabela corrigida e captura scratch/admin-catalog.png. Preview sem jogadores; não simulou renderização Unity.
- Cliente real: não testado. Validar um NPC, modelo parado e bot nativo no mapa correspondente; limpar testes e verificar outra sala intacta.
- Oracle: instalada 07/10 03:06 UTC; zero conexões antes do reinício, PM2 PID571424/restart182 online, hashes conferidos, 90 testes aprovados.
- Backup remoto: /home/ubuntu/backups/admin-20261007T030658Z/{runtime,logs}.tar.gz.
- Pacote/reprodução: scratch/admin-deploy-20261007; remoto /home/ubuntu/admin-stage-20261007, tests.tap contém execução remota.
- Próximo passo: teste real do usuário pelo menu Spawnar Entidades; registrar chave, mapa e output_log se entidade específica falhar.