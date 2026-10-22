# Passagem de trabalho — MODEL-001

- Responsável: Codex, 07/10/2026 03:25 UTC. Estado: implementado e instalado; Unity real pendente. Reservas liberadas.
- Objetivo: usar aparência de monstro mantendo herói, armas e controles originais.
- Arquivos: src/modelSwap.js, src/http.js, public/admin/index.html; client-patches/ModelSwap.cs, tools/patch-model-swap.ps1; tests/model-swap.test.js, tests/ModelSwapFixture.cs.
- Evidência: PlayerCommon.GetNowModelData consultava AllHeroUnlock; monstros usam HeroDataManager.GetNpcSkinData(createIndex)/AllRobotUnlock. GamePlayer.LoadModelTpsFinish escolhia classe de animação do herói base.
- Decisão: NVM negativo representa robot.xml, positivo mantém compatibilidade com skins anteriores; zero restaura. GetNowModelData original calcula FPS/base; helper substitui apenas TPS/modelo e controlador. Classe de animação TPS vem do heroclass do monstro.
- NVM limpa rigidbodyAll antes de destruir o modelo, para setTpsLayer reconstruir referências de ragdoll em vez de guardar objetos destruídos. Não altera funções de tiro/mira nem cápsula de movimento.
- Servidor: envio somente de NVM a peers prontos da mesma cena, sem recriar jogador e sem alterar HP/herói. Valida IDs e exige jogador vivo/pronto. Spawn já reenvia visualHeroId negativo; troca de herói limpa override pelo comportamento existente.
- Painel: Modo Zueira > Trocar Aparência; 11 modelos externos distintos de monstros, deduplicados por modelo. Aceita chave robot:1109 manual, botão Resetar Original.
- Testes: 93/93 servidor local/Oracle; 7 verificações simuladas do helper (FPS preservado, original imutável, troca de animação, reset, desconhecido/overflow). Compilado contra mscorlib net35 e DLLs reais do cliente. Sintaxe JS/página OK.
- IL: 14.780 métodos comparados, exatamente 3 alterados: PlayerCommon.GetNowModelData, GamePlayer.LoadModelTpsFinish e GamePlayer.NVM; saltos curtos verificados. Preserva HIT-002. Fontes decompiladas Assembly/ não foram reescritas; patch/helper são a referência desta mudança.
- Cliente instalado com jogo fechado: C:/trig2/GAME/TRIG_Data/Managed/Assembly-CSharp.dll SHA256 CC5C79635AB825C040F247703CE34E1733DFB9732C30E39BA44C2364B63C0B89; TriggerunModelSwap.dll SHA256 C9152C7F63D136099359170E698C421B9603E73C4FB8F826D97D2C9EE78F2E28. Manter também TriggerunHitCollision.dll existente.
- Backup local: backups/model-20261007-before (cliente + runtime). Candidatos/relatório/testes/screenshot: scratch/model-swap. Use somente Assembly-CSharp.ready.dll como candidato final; outras tentativas não são entregáveis.
- Oracle instalada 07/10 03:25UTC com zero conexões; PM2 PID571978/restart183; 93/93 testes; HTTP200 e TCP7777 OK. Backup /home/ubuntu/backups/model-20261007T032514Z/{runtime,logs}.tar.gz; stage /home/ubuntu/model-stage-20261007.
- Navegador: seletor preenche robot:4112 corretamente; screenshot scratch/model-swap/admin-model-swap.png. Sem envio de transformação a jogadores durante teste.
- Cliente real: NÃO testado. Todos os clientes que precisam ver o monstro devem receber o Assembly atualizado e ambos helpers. Cliente anterior interpreta negativo como aparência original, sem suporte ao monstro.
- Limites: animação, materiais/encaixe de arma e efeitos de habilidades variam por rig e precisam de Unity. Não implementa habilidades próprias/posse de RobotAI. Troca reconstrói modelos assincronamente; evitar cliques repetidos durante carregamento. Não declarar todos os 11 modelos compatíveis antes de teste.
- Próximo passo: abrir cliente, entrar com Celina/Eva; admin Modo Zueira, selecionar conta, escolher Oracle Assault Soldier (robot:4112 ou manual1109), Transformar. Conferir andar/pular/atirar/dano, visual em segundo cliente atualizado, morte/renascimento e reset. Coletar output_log se falhar.
- Reversão cliente: fechar jogo e restaurar backups/model-20261007-before/client/Assembly-CSharp.dll; helper novo pode permanecer sem referência. Reversão servidor: restaurar runtime.tar.gz remoto e reiniciar sem jogadores.
