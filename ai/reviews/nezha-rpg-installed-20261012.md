# Passagem de trabalho — SKIN-028 Nezha Soberano Ígneo

- Responsável: Codex; instalação 12/10/2026 23:30 BRT (13/10 02:30 UTC). Estado: instalada, teste real pendente.
- Objetivo: corrigir rosto/braços do GLB fornecido e adaptar ao Nezha1035 como skin5 gratuita independente.
- Fonte preservada: candidate-assets/nezha-rpg/source-tripo.glb, hash igual ao Downloads/fantasy armored character 3d model.glb. Tripo sem rig:31525v/21156tri/atlasJPEG4096.
- Evidências: scratch/nezha-rpg-inspection (inventário GLB, componentes soldados, vistas e render sem iluminação para distinguir textura de normais).
- Face: image_gen integrou retoque localizado de nariz/lábios; referência e prompt em candidate-assets/nezha-rpg/face-reference.png e face-prompt.txt. Projetados5275pixels no atlas; geometria/UVs do rosto e textura fora da máscara iguais. GLB de textura nezha-rpg-face.glb conserva todos4accessors.
- Braços: marcos medidos ombro(.185,.035,1.395)/cotovelo(.360,.035,1.333)/punho(.500,.020,1.293), espelhados; comprimento alinhado ao rig nativo (braço~1.22×, antebraço~1.71×). Mãos rígidas escala.90; dragões escala.82 e pesos100%clavícula.
- Rig:62bones originais; pesos anatômicos regionais+difusão12passes apenas corpo soldado, dedos nativos/peças independentes preservados. Cachecol usa Bone002–007; normais suavizadas somente pele frontal, bordas metálicas preservadas.
- Tentativa inicial descartada: classificações rígidas no pescoço e cor do cachecol contaminavam vértices das ombreiras; piores estiramentos~8.72×. Correção por componentes e difusão reduz máximos para1.86–3.33× nas7poses; p99≤1.166. Não confundir poses sintéticas com animação Unity.
- Candidata: candidate-assets/nezha-rpg/adapted/nezha_tps_rpg.mo, geometry.npz/report.json/validation.json/vistas.11renderers originais de corpo/LODs recebem mesh principal; LODs usam a malha completa de21156tri por renderer ativo, sem simplificação específica.
- Preservação: comparação integral dos objetos admite somente mesh principal,11renderers,AssetBundle e2novos objetos textura/material. Mesh ultimate7060996215422688193 e seus2renderers intactos; transforms/colliders/controllers/armas sem mudança. FPS original nezha_fps; semDLL.
- Catálogo: public/baxi/herounlock.xml e zh/herounlock.xml recebem apenas skin5 Soberano Ígneo (6|0, sem stats); ícone nativo nezha_1 nesta primeira versão. Manifesto5558entradas/CRC26101207; novo recurso na versão1.1.1.
- Testes:7poses sintéticas; limites p99<1.5/max<3.5; ombreiras rígidas/pesos normalizados/índices/normais finitos; comparação estrutural e textura; catálogo local eOracle gratuitos/rota válida;8HTTP com hashes nas versões1.1.1/1.3.5 eTCP7777 aprovados. Scripts emscratch/nezha-rpg-adapt e scratch/nezha-rpg-release.
- Oracle: instalação com backup concluída; conexão surgiu durante staging, reinício aguardou autorização explícita do usuário. Reinício autorizado13/10 02:30UTC; PM2onlinePID701709. Local sincronizado só após hashesHTTP/base aprovados.
- Backups: local backups/nezha028-before (3XML); candidata inicial backups/nezha028-first-candidate; remoto /home/ubuntu/backups/nezha-rpg-skin028-20261013T022940Z/runtime.tar.gz. Fonte original neutra não modificada.
- Cliente real: ainda não testado. Próximo passo: reiniciar cliente, selecionar Nezha/skin5 e observar seleção, corrida/mira/disparo/salto/ultimate. Primeira pessoa e braços extras mantêm aparência original por escopo decidido.
- Limites: textura comprimida2048DXT5, detalhes de face ainda limitados pelo UV; brilho de gemas pintado, sem novo canal emissivo; armadura rígida pode apresentar clipping em animações extremas. Registrar relato real antes de novos ajustes.
- Reserva liberada; nenhuma outra tarefa/skin/DLL alterada.
