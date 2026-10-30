# Valkyrie — Valquíria do Eclipse / Tripo

Quatro PNGs individuais transparentes, 1024×1536, pose A, corpo inteiro e sem arma.
Cabelo atualizado: loiro claro, franja, coque trançado baixo e laço preto no estilo Saber.

## Enviar como múltiplas vistas

| Campo | Arquivo |
|---|---|
| Frente | front.png |
| Verso / costas | back.png |
| Esquerda | left.png — nariz aponta à esquerda da imagem |
| Direita | right.png — nariz aponta à direita da imagem |

A frente define proporções, rosto e desenho da roupa. Os perfis completam espessura da armadura/cabelo; as costas definem coque, laço e placas posteriores.
Envie as quatro vistas do MESMO personagem. Não inclua o conceito com espada: a arma será adaptada separadamente no jogo.
As abas da saia devem ser separadas das pernas e a gola separada do queixo. As mãos devem permanecer completas dentro das manoplas.
O esqueleto e os pesos do GLB ainda serão adaptados ao rig nativo da Valkyrie, mesmo se o arquivo já vier com rig.
Exporte em GLB com textura e preserve uma cópia do modelo original gerado.

## Proporções e limites

Guiadas pelo modelo nativo waerjili_tps: corpo esguio, pernas longas, ombros compactos e braços em pose neutra. Não são vistas renderizadas do mesmo modelo3D nem calibração geométrica exata; pequenas diferenças de ornamentos/contorno permanecem. A frente prevalece ao adaptar o GLB.
Saia em duas abas traseiras/laterais com fenda posterior e abertura frontal; a física será tratada na adaptação ao jogo.

## Prompts e origem

Imagens produzidas com image_gen integrado. Front-prompt + front-alpha-prompt geraram a frente; back-final-prompt gerou as costas finais; left-prompt e right-final-prompt geraram os perfis. Prompts completos e metadata.json incluídos.
Conceito aprovado preservado na pasta superior; o cabelo curto assimétrico foi substituído nas quatro referências atuais.
Nenhum arquivo de execução do jogo/servidor foi alterado nesta etapa.
