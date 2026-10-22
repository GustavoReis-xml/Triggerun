# David — Colosso do Abismo

Direção escolhida pelo usuário: autômato demoníaco de metal negro e bronze. Referências geradas com a ferramenta integrada `image_gen` em13/10/2026, a partir do modelo nativo David1037/dawei_tps. Não são modelos já instalados no jogo.

| Campo da multivisão | PNG |
| --- | --- |
| Frente | 01-frente.png |
| Esquerda | 02-esquerda.png |
| Direita | 03-direita.png |
| Verso | 04-costas.png |

Quatro imagens1254×1254RGBA com transparência real, sem arma/cenário/legenda. Use osPNG individuais; o ZIP reúne essas imagens e as instruções. Frente e costas emT-pose; nos perfis os braços continuam abertos lateralmente e ficam encurtados pela direção da câmera.

Frente autoritativa para proporções e detalhes do traje; perfis orientam profundidade. Costas têm joelhos traseiros articulados/calcanhares corrigidos para não repetir placas frontais. Há pequenas diferenças entre vistas geradas porIA; não são projeções calibradas de uma mesh única.

Projeto: carcaça negra/bronze envelhecido/crimson, cabeça demoníaca mecânica com chifres curtos, placas bilaterais no peito, ombreiras independentes, juntas de cotovelo/joelho livres, mãos mecânicas de cinco dedos e pés largos. Vents/brasa representados visualmente; o desenho não garante emissão real no shader do jogo.

Adaptação planejada: preservar hierarquia e62 referências dos ossos corporais; separar/influenciar placas como peças rígidas, respeitando ossos auxiliares do peito/ombros/pés. Arma tem outra mesh/20bones; não incluí-la fundida noGLB corporal. Escudo funcional depende de objeto `body2_dun`; portal é recurso separado `dawei_csm.mo`. Essas estruturas nativas devem continuar presentes na futura skin.

Próximo passo: gerar o modelo texturizado noTripo usando as quatro referências e enviar oGLB para inspeção/proporções/segmentação/pesos. Nenhuma validação de rig ou instalação aconteceu nesta etapa.

Prompts completos: `prompt-frente.txt`, `prompt-costas.txt`, `prompt-costas-correcao.txt`, `prompt-esquerda.txt`, `prompt-direita.txt`. Dimensões/transparência/hashes em `validation.json`; análise nativa em `E:/server tr/scratch/david-inspection/`.
