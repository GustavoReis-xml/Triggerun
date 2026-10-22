# Passagem — SKIN-021

- Responsável: Codex. Objetivo: inspecionar Celina Tripo e definir correção dos olhos antes de adaptar.
- Estado: inspeção concluída; correção/adaptação não realizadas.
- Fonte preservada: C:/Users/Gustavo/Downloads/businesswoman 3d model.glb.
- Confirmado: GLB Tripo,1malha/10881vértices/10105triângulos,1material opaco/1textura JPEG4096²,sem skin/bones/animações. Posição verticalY,altura~1unidade.
- Prévia frontal: marcas dos olhos escuras e indefinidas acima da armação; região das lentes parece superfície opaca pintada como pele. Não há material separado transparente para lentes. Não afirmar que bastaria ativar transparência no material inteiro.
- Evidências: scratch/celina-red-inspection/report.json,gltf.json,texture-0.png,face-0.png,face-180.png,view-0.png.
- Próximo passo: tratar geometria/lentes e detalhes dos olhos em cópia antes de transferir pesos ao rig62bonesCelina. Não gerar novamente o corpo sem motivo; não usar transparência global no único material.
- Testes: parse GLB/extrair textura/render CPU concluídos; Unity não testado.
- Cliente/Oracle: intactos. Nenhuma skin cadastrada ou instalada.
- SKIN-020 permanece separada, aguardando reprodução/logs de proximidade.
