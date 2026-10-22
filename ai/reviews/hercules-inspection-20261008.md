# Passagem de trabalho — SKIN-016

- Responsável: Codex;08/10/2026. Estado: inspeção concluída; sem adaptação/instalação.
- Fonte: C:/Users/Gustavo/Downloads/hercules.glb,3260300bytes. Arquivo somente lido. MetadadosMarco.41/SketchfabStandard; fonte incorporada no report.json.
- Estrutura: GLB2 válido, sem extensões de compressão; dezmalhas,93joints,nenhuma animação. Sete partes do personagem:8564vértices/13388triângulos; três partes adicionais de armas devem ficar fora do primeiroprotótipo.
- Skinning: corpo/cabelo/roupa têmJOINTS/WEIGHTS4, somas aproximadamente1. Bones anatômicos nomeadosHips/Spine/LeftArm/ForeArm/Hand/dedos/pernas e extras face/peitoral/armroll.
- Texturas: seisPNG; personagem usa atlasbasecolor1024+normal1024, compartilhados por sete materiais. Armas têm atlas separados512/256. Pode unificar materiais do corpo ao shader compatível sem multiplicar sete conjuntos de texturas.
- Lamster real: male_tps.mo tem cinco malhas renderizadas,4105vértices no total; bones por malha14/6/6/3/22. Corpo alto~1,748m. Hérculesfonte~2,10unidades no repouso; prévia usa escalauniforme0,86 apenas para enquadrar, ainda não é calibração ao rig.
- Visual: prévia semarmas confirma corpo robusto, mãosgrandes,roupa justa/sem capa,corpo/pele/cabelo já texturizados. Bom candidato temático ao combate de socos doLamster; mais polígonos que original, desempenho real não medido.
- Eixos: fonteupY,frente-X,larguraZ. Conversão de prévia(x,y,z)=(-sourceZ,sourceX,sourceY), winding invertido. Não assumir eixos deScarlet iguais.
- Decisão: recomendado protótipo independente, sem maça/espada, mantendo habilidades/socosLamster; preservar massa corporal com escalauniforme e ajustar braços/mãos. Não forçar comprimento de cada segmento do torso como primeiraScarlet.
- Riscos pendentes: transferir93joints para o rig distribuído doLamster, verificar punhos/ombros/joelhos, decidir FPS e efeitos especiais. NenhumtesteUnity oufuncionalidade confirmada paraHércules.
- Evidências: scratch/hercules-inspection/{report.json,gltf.json,texture-0.png..texture-5.png,source-preview.png};inspect_glb.py/preview_compare.py. Previewusa diffuse/iluminação aproximada, não shaderoriginal completo.
- Oracle/cliente: não alterados. Próximo: usuário aprovar protótipo; gerar skinindependente com bones originais e prévias/poses antes de instalar. Reserva liberada.
