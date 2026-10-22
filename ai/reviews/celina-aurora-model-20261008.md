# Passagem de trabalho — SKIN-007

- Responsável: Codex; 08/10/2026 00:45BRT (03:45UTC).
- Objetivo: substituir a pintura006 rejeitada por roupa com geometria independente na Aurora6.
- Estado: implementada/instalada; revisão com cliente real pendente.
- Alteração: jaqueta bomber tática nova,25partes,3393vértices/5544triângulos adicionais: corpo, mangas, reforços dos ombros/cotovelos, gola, barra, punhos, bolsos/bevel/flaps, painéis e zíper segmentado. Malha final6301vértices/8262triângulos.
- Skinning: pesos interpolados dos quatro vértices mais próximos, normalizados em até4bones;62bones/bindposes originais, nenhum bone novo.909faces antigas cobertas ocultadas para evitar atravessamento. Pele/cabelo/calça retornam à textura005 aprovada; calça jeans preservada.
- Material: segunda submesh com material e atlas próprio1024x512DXT5;2objetos novos Texture2D/Material. UV/corpo/vértices originais mantidos, exceto faces ocultadas. Tangentes/normais novas para roupa.
- Efeitos: shenti usa material novo; fuhuo_wudi e hongxian reutilizam seus materiais originais nas duas submeshes. Colisores, físicas, animações, DLL e código servidor intactos.
- Base: candidate-assets/celina-aurora-v2; backups/celina-aurora-model-20261008-before preserva bundleTPS, ícone e manifesto atuais. Clássica não alterada.
- Testes automatizados: verify_model.py reabre bundle e compara1209objetos antigos, permite somente mesh/3renderers/textura/AssetBundle e adiciona2objetos. Contagens/índices/UV/pesos/bones/bounds e materiais válidos; sem faces degeneradas. Três poses sintéticas de braços/cotovelos finitas e sem deformação explosiva, prévias inspecionadas; não são clips Unity reais nem garantia contra qualquer clipping.
- Cliente real: não testado. FPS mantém braços da006; alteração geométrica desta etapa é TPS e modelo de seleção.
- Oracle: instalada08/10 00:44BRT sem reinício, três arquivos, CRC26100801. Backup /home/ubuntu/backups/celina-aurora-model-20261008T034443Z/runtime.tar.gz. Baseline remota/SHA antes da troca, HTTP/hashes dos3recursos eTCP7777 confirmados.
- Evidências: candidate-assets/celina-aurora-model/model-report.json e validation.json; preview.png/geometry-preview.png/jacket-only.png/pose-preview.png; scripts em scratch/celina-aurora-model.
- Fonte editável: candidate-assets/celina-aurora-model/Celina-Aurora.glb (personagem+roupa+62bones, GLTFYup), Aurora-Jacket.obj comjacket.mtl/jacket-atlas.png (somente roupa); geometry.npz contém pesos/bones/faces.
- Próximo passo: reiniciar clientes, selecionar Celina/Aurora6; verificar seleção, corrida, mira, habilidades, respawn/invulnerabilidade e visual por outro jogador. Ajustar pesos/folgas a partir de ocorrência real. ArmaAurora5 continua separada.
- Reversão: restaurar três arquivos do backup remoto, sem reiniciar; clientes devem reabrir. Não sobrescrever mudanças posteriores sem conferir hashes.
