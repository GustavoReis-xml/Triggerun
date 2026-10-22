# Shogun Corvo — referências para Tripo

Quatro imagens individuais geradas com a ferramenta integrada image_gen em 22/10/2026. Fundo transparente e corpo inteiro; nenhuma imagem é um modelo já adaptado ao jogo.

## Multivisão

| Entrada | Arquivo |
| --- | --- |
| Frente (principal) | 01-frente.png |
| Esquerda — bico aponta para esquerda da imagem | 02-esquerda.png |
| Direita — bico aponta para direita da imagem | 03-direita.png |
| Verso | 04-costas.png |

Use a frente como referência principal. Os perfis orientam o comprimento do bico, aba do chapéu e profundidade das asas; o verso define as duas massas de penas separadas. São imagens geradas separadamente, não projeções calibradas de uma única malha; pequenos detalhes de ornamentos, mãos e perspectiva variam. A frente tem precedência em caso de conflito.

## O que gerar e enviar

- Corpo humanoide de corvo, vestido, com as duas asas recolhidas. Não adicionar arco, flecha, aljava, cenário ou efeitos: o equipamento do jogo é separado.
- Manter as mãos com cinco dedos e polegar, pernas humanas com botas, cabeça e pescoço articuláveis. Asas não substituem os braços.
- Gerar a textura e exportar GLB com os materiais/texturas incluídos. Não é necessário comprar/gerar animações: o encaixe usará o rig e as animações do Shogun. Rig Tripo é opcional e não substitui o ajuste dos pesos nativos.
- Antes de enviar, conferir se mãos/asas/casaco não viraram uma peça fundida aos braços; as duas pernas devem continuar separadas.

## Decisões de compatibilidade

Shogun1024/HouYi: corpo original esguio,56bones no corpo principal, dedos completos/ForeTwist; arco com rig próprio6bones. Poses de carga/disparo, escalada e interações requerem ombros/cotovelos/punhos livres. A cabeça de corvo e o bico precisam seguir Head sem influência da mandíbula/pescoço do corpo na ponta. Chapéu deve permitir virar a cabeça e aproximar a mão da corda.

As asas desta primeira proposta são recolhidas e decorativas; não há promessa de abertura/batimento com o rig corporal original. Os efeitos de fênix da ultimate pertencem a hierarquias próprias e devem ser tratados separadamente. Casaco, asas, bico e aba ainda exigirão inspeção de colisões visuais quando o GLB chegar; imagens não comprovam ausência de clipping.

Prompts completos usados estão nos quatro arquivos prompt-*.txt. Fontes/medidas/análise: `ai/reviews/shogun-raven-reference-20261022.md`; evidências técnicas em `scratch/shogun-inspection/`.
