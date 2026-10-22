# Shogun Corvo — versão limpa em vinho escuro

Esta é a revisão escolhida para gerar no Tripo. Substitui o acabamento granulado das referências037; não mistura imagens antigas no mesmo conjunto.

| Entrada multivisão | Imagem |
| --- | --- |
| Frente, principal | 01-frente.png |
| Esquerda, bico aponta à esquerda | 02-esquerda.png |
| Direita, bico aponta à direita | 03-direita.png |
| Verso | 04-costas.png |

Materiais limpos de jogo3D: couro e tecido de cor uniforme, penas em camadas legíveis, bronze acetinado. Removidos grunge, poeira, partículas, arranhões e microtextura excessiva. Paleta das áreas anteriormente vermelhas passa para vinho escuro com fundo ameixa/arroxeado; olhos continuam rubi, penas principais azul-preto e metais bronze.

Manter corpo, mãos de cinco dedos, duas pernas separadas, asas recolhidas nas costas e pose de modelagem. Arco e flechas não fazem parte do corpo. Gerar com textura e exportar GLB com materiais incluídos; o rig/animações do jogo serão adaptados depois, sem necessidade de comprar animações Tripo.

Frente autoritativa; demais vistas orientam profundidade e costas. As imagens são geradas separadamente, com pequenas variações, e não são projeções calibradas de uma malha3D. A compatibilidade final depende da análise do GLB e dos testes de poses.

Produção: ferramenta integrada image_gen, sem edição manual de pixels. Limpeza inicial usa o original de cada vista e frente limpa como guia; depois edição seletiva de cor em cada imagem. Prompts de limpeza copiados nesta pasta em prompt-*.txt; prompt de cor completo em prompt-paleta.txt. As referências originais continuam em ../../multiview/.
