# Passagem de trabalho — SKIN-038

- Responsável/destinatário/data: Codex / usuário e próxima IA /22/10/2026BRT.
- Objetivo: usuário aprovou conceito037, pediu superfícies mais limpas sem granulação e depois vinho escuro ou roxo.
- Estado: quatro vistas limpas + paleta vinho/ameixa entregues; aguarda GLB. Sem runtime/Oracle, reserva liberada.
- Base: referências037 e seu inventário nativo permanecem intactos. Não repetir análise do rig, ver `shogun-raven-reference-20261022.md`.
- Decisão: couro/tecido uniformes, penas em camadas com gradientes suaves e bronze acetinado; remover grunge, poeira, arranhões, damasco e microdetalhe aleatório. Preservar desenho/pose/volumes/assimetria.
- Cor: usuário escolheu entre vinhoescuro/roxo durante a geração inicial; adotar vinho com fundo ameixa, prompt#452033/sombra#26131F/luz#67394F. Olhos rubi, penas azul-preto e bronze preservados.
- Produção: image_gen integrada emduas etapas: limpar frente e aplicar estilo às três vistas; depois recolorir seletivamente asquatro. Nenhuma manipulação manual de pixels. Limpeza inicial em `candidate-assets/shogun-raven/clean/`, entrega final em `clean/vinho/`.
- Imagens finais:01-frente,02-esquerda,03-direita,04-costas,1254²RGBA/transparência. Inspeção visual confirmou redução do ruído/material liso e cores consistentes; pequenas variações de ornamentos/pose entre imagens geradas, não são vistas calibradas de uma mesh.
- Verificações: quatroPNG<20MB,alpha0–255/ZIP íntegro/hashes emvalidation.json; pacote `shogun-corvo-vinho-tripo.zip` contém somente imagens finais +README e prompts. Script `scratch/shogun-inspection/package-clean.py`.
- Margem: ponta de pena no topo do perfil direito tem1pixel antialiasalpha157 na borda; não há pixels sólidosalpha≥250 tocando bordas. Medida registrada, sem recorte ou pintura manual para esconder resultado.
- Prompts:quatro limpeza +prompt-paleta.txt preservados na entrega final; fontes originais e etapas intermediárias também preservadas.
- Cliente/Oracle: não alterados, sem testeUnity porque não há novo modelo nesta etapa. Sem backup remoto/reinício necessários.
- Próximo passo: usar apenas asquatro vistas de `clean/vinho/` na multivisãoTripo, gerar textura/exportarGLB e avaliar encaixe noShogun; asas recolhidas decorativas, arco separado. Não misturar versões037/granulada ouclean/vermelho mais vivo.
- Bloqueio: aguardaGLB do usuário, sem pendência de geração.
