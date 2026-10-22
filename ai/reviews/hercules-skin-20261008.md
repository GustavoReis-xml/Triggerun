# Passagem — SKIN-017 Hércules

- Responsável: Codex; instalação 09/10/2026 00:02 UTC (08/10 21:02 BRT).
- Estado: implementado e instalado; teste com cliente real pendente.
- Lamster2000 skin2 gratuita, modelo TPS independente, retrato circular sem texto, CRC26100805.
- Fonte: Downloads/hercules.glb; autoria/licença registradas em hercules-inspection-20261008.md.
- Corpo sem armas adaptado ao rig original44bones/5renderers; atlas1024 e CAB exclusivo.
- Default, habilidades, colisores e punhos FPS preservados.
- PlayerCommon.GetNowModelData forçava male_tps: hook restrito2000 usa skin equipada, com fallback.
- Helper mantém hooks anteriores e ajusta aspecto circular também para lamster_hercules.
- Testes: geometria/pesos/3poses sintéticas; 7 casos simulados do seletor; 14780 métodos comparados, somente GetNowModelData alterado.
- Cliente local: duas DLLs instaladas após usuário fechar clientes; hashes conferidos.
- Oracle: catálogo validado, PM2 reiniciado com zero conexões; 6 URLs HTTP byte-idênticas e TCP7777 OK.
- Backup local: backups/hercules017-before; remoto: /home/ubuntu/backups/hercules-skin017-20261009T000243Z/runtime.tar.gz.
- Evidências: scratch/hercules-adapt e scratch/hercules-release/http-validation.json.
- Pacote para outros computadores: candidate-assets/lamster-hercules/client-update.zip.
- Próximo passo: testar seleção, spawn, socos/cura/buff, animações e visão por outro cliente atualizado.
- Limite: testes simulados não comprovam animações/renderização no Unity.