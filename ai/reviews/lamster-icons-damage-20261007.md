# Passagem de trabalho — LAMSTER-005

- Responsável: Codex; 2026-10-07. Estado: implementada/instalada; teste Unity pendente.
- Objetivo: retratos/ícones exclusivos e investigar secundário sem dano contra jogador/bot, confirmado pelo usuário.
- Base/backups locais: backups/lamster-icons-20261007-before; cópias de runtime/XML/manifesto/testes/DLLs. Não reverter alterações alheias.
- Visual: referência do usuário; retrato e dois ícones gerados, originais em public/lamster/*-source.png. PNGs de produção com dimensões nativas: seleção90x90, retrato200x308, HUD109x158, kill44x44.
- Recursos: src/lamsterAssets.js whitelist de12URLs Icon/*.txt; src/http.js entrega PNG local. Dados baxi/zh e manifesto novos nomes lamster_1/lamster_primary/lamster_rally/lamster_passive; cache XML26100705. Modelos/animadores nativos preservados.
- Evidência servidor: logs recentes de pulso250 mostram alvo2 recebendo1007 (aliado); registros anteriores contra alvo5 tinham baseDamage0, não provam falha da versão004. Log preservado scratch/lamster-icons/server-tail.txt; cliente-output-before.txt.
- Falha confirmada: canStrikeEnemy exigia lastX/Y/Z; spawnPracticeNpcs grava x/y/z. Agora fallback somente quando posição atual ausente, rejeitando coordenadas inválidas/fora do alcance; teste inclui preferência pela posição atual.
- Assembly: CBulletMe.boom usa RaycastAll para oclusão de área e bloqueia tags não can_damage. Alteração antiga colocou cápsulas _IgnoreDamge em camadas consultadas; HIT002 filtrava tiros normais, não oclusão de área. Mecanismo compatível com aliados atingidos/inimigos omitidos; confirmação em Unity pendente.
- Cliente: novo client-patches/LamsterArea.cs filtra somente _IgnoreDamge na oclusão do secundário de2000; preserva paredes, escudos, máscara e demais heróis/ataques. tools/patch-lamster-area.ps1 altera somente CBulletMe.boom;14.780 métodos comparados/saltos válidos.
- Diagnóstico adicional: rejeições de dano secundário registram [LAMSTER SECONDARY REJECT] com IDs/equipes/HP/prontidão/posições, para não inferir motivo de descarte silencioso.
- Testes:104/104 locais e Oracle (logging,lamster,match-lifecycle,entity-catalog,model-swap,lamster-assets); XMLs parseados/sintaxe OK;5 verificações simuladas de colisão em tests/lamster-area.ps1. Não são teste Unity.
- Cliente instalado fechado: Assembly-CSharp C1CC01BCD523DB2857B9F9879450000974765B3426908D041598D6D2A719A3AA; TriggerunCombatHud 2C37D854E9DF8F933EC10AB2581AAE3C6F5660274C4B3ED8120153E39B859DBC. Helpers HIT/MODEL preservados.
- Oracle: ícones instalados23:20UTC, correção23:25UTC, zero conexões antes dos reinícios; PM2pid595517/restart189. Backups /home/ubuntu/backups/lamster-icons-20261007T232048Z e lamster-damage-20261007T232536Z (runtime+logs).
- Verificação externa:12URLs HTTP200/image/png; TCP7777 disponível.
- Próximo passo: reiniciar cliente; verificar seleção/HUD/skills e pulso contra inimigo/bot em campo aberto até8m, aliado recebe cura/buff, parede/escudo bloqueiam. Se falhar, capturar novo output_log e [LAMSTER SECONDARY REJECT]/SDG com equipes/posições; não declarar resolvido antecipadamente.
- Reservas liberadas; aguarda teste real do usuário.
