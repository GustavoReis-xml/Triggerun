# Passagem de trabalho — VOICE-001

- Responsável: Codex; 2026-10-07 22:05 UTC.
- Objetivo: recuperar fala de renascimento sem recriar o personagem; auditar demais gatilhos.
- Estado: implementação/testes/instalação concluídos; validação real pendente.
- Base: backups/voices-20261007-before/{src/gameLogic.js,tests/match-lifecycle.test.js}; hashes em scratch/voices/deploy/base.sha256.
- Arquivos alterados: src/gameLogic.js, tests/match-lifecycle.test.js. DLLs do cliente preservadas.
- Confirmado: HeroPlayer.OnRevive toca PlayLoadReviveInBaseSound (fora de MultiRoundBegin); GamePlayer.OnRevive restaura modelo/colisores/posição; NetLiving.OnRevive chama heroBase.Revive.
- Confirmado: respawnPlayer enviava OnRevive seguido de 10001/PlayerCreate para o dono e d+cGamePlayer/PlayerSelect para os pares. PlayerCreate inicializa/carrega tudo novamente. Log real capturado confirma sequência (scratch/voices/output_log-before.txt).
- Hipótese causal: recriação reinicializa/substitui o áudio recém-disparado. Correção precisa de confirmação Unity, não é prova de todas as falas restauradas.
- Alteração: HP antes do OnRevive; remove recriação no respawn; reposição via OnFillBullet nativo, seguindo o formato da recarga existente.
- Auditoria: entrada usa LoadEnterSceneSoundPrefab, que descarta callback se playerHaveCreate=false; possível corrida com cache, ainda não modificada. PlayEnterSceneSound também interrompe fala de renascimento, tornando recarga indevida especialmente suspeita.
- Auditoria: NetLiving.OnDeath dispara voz da arma, multikill, sequência a cada 5 kills e equipe eliminada; cura/recuperação vêm de HeroPlayer.HaveDamage; habilidade pronta/ultimate têm gatilhos próprios. Não forçar falas aleatórias para simular restauração.
- Testes: sintaxe dos dois arquivos; 96/96 testes servidor locais e Oracle (inclui ordem HP/OnRevive, ausência de recreação, refill e isolamento por cena).
- Cliente real: usuário confirmou mira/feedback HUD-001 funcionando; falas HUD-001 sem diferença. VOICE-001 ainda não testada no Unity.
- Oracle: instalada 22:05 UTC, zero conexões no gate; PM2 online PID592867/restart185; porta7777 escutando. GET / retorna404 (rota ausente; não declarar HTTP200).
- Backup remoto: /home/ubuntu/backups/voices-20261007T220517Z/{runtime,logs}.tar.gz.
- Próximo passo: morrer/renascer com Eva/Celina; conferir fala, movimento, câmera, tiros, munição e aparência no segundo cliente. Depois investigar entrada/cache e restantes falas com captura específica caso persistam.
