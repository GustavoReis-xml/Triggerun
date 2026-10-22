# Passagem — SKIN-019 Rider para Gaya

- Responsável: Codex; instalação 09/10/2026 00:26UTC (08/10 21:26BRT).
- Estado: implementada e instalada; cliente real pendente.
- Gaya1036 skin5 Rider gratuita, TPS independente/CAB próprio/CRC26100806; clássica e demais skins intactas.
- Fonte/atribuição: candidate-assets/gaya-rider/CREDITS.txt; pve771309, licença declarada CC-BY-4.0.
- Arquivo estático:20 cópias idênticas removidas;4491vértices/5463triângulos finais, atlas1024x512.
- UVs negativos normalizados antes de atlas; texturas faciais orientadas; olhos extras sob venda removidos, boca preservada.
- Braços alinhados à pose Gaya; pesos interpolados da superfície original, costuras compartilhadas;64bones nativos.
- Cabelo vinculado à cabeça/tronco, sem física própria; não herda pesos das pernas/braços.
- Três LODs/effect-renderers usam a superfície Rider; corda/Cylinder002 e gao_tps preservados.
- Gameplay, animações, colliders, arma e FPS originais; nenhuma mudança Assembly-CSharp nesta tarefa.
- Helper CombatHud: somente LateUpdate do retrato mudou;29métodos comparados, backup e instalação com TRIG fechado.
- Testes automatizados:976objetos comparados,16permitidos modificados+2novos; malhas reabertas/pesos normalizados/índices/bones intactos;3poses sintéticas passaram.
- Limites: poses sintéticas não confirmam Unity; máximos de alongamento de pequenas arestas até3,44x, p99 até1,27x; verificar ombros e cabelo em movimento real.
- Oracle: catálogo gratuito validado, PM2 online após reinício com zero conexões;6HTTP byte-idênticos/manifesto/TCP7777 OK.
- Backup local: backups/rider019-before; remoto: /home/ubuntu/backups/rider-skin019-20261009T002602Z/runtime.tar.gz.
- Evidências: candidate-assets/gaya-rider/{report.json,validation.json,poses-preview.png};scratch/rider-release/{helper-validation.json,http-validation.json}.
- Outros clientes: candidate-assets/gaya-rider/client-update.zip (helper mantém aspecto circular; DLL Assembly atual incluída por compatibilidade com skins anteriores).
- Próximo passo: reabrir cliente, equipar Gaya/Rider5 e testar ataque, corrida, habilidades, respawn e visão remota.