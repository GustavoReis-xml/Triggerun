# Johnny — Campeão Solar

Referências geradas com a ferramenta integrada `image_gen`, em13/10/2026. Quatro PNG1254×1254RGBA, com transparência real, sem arma, legenda ou cenário.

| Campo no Tripo | Arquivo |
| --- | --- |
| Frente | 01-frente.png |
| Esquerda | 02-esquerda.png |
| Direita | 03-direita.png |
| Verso | 04-costas.png |

Use os PNGs individuais na multivisão. O ZIP reúne as quatro imagens e estas instruções.

A frente é o desenho principal. Costas e perfis foram derivados dela; o perfil direito também recebeu o esquerdo como referência de profundidade. Frente/costas têm braços horizontais emT-pose. Nos perfis, os braços continuam abertos lateralmente e aparecem encurtados pela direção da câmera.

As proporções foram orientadas pelo Johnny nativo (`lanbo_tps`, herói1031), com musculatura mais forte sem alongar os membros. São guias visuais gerados por IA, não projeções medidas de uma única mesh3D. Há pequenas diferenças de detalhes/contornos entre vistas; se surgir conflito, priorizar a frente para largura/proporções/roupa e os perfis para profundidade. Conferir articulações e comprimentos no GLB recebido antes de transferir pesos.

Traje: tronco e braços descobertos; calça grafite ajustada; cinturão solar de bronze; inserto vermelho frontal curto; proteção compacta de joelhos/canelas e braceletes curtos. Sem acessórios soltos na axila/ombro.

Prompts completos: `prompt-frente.txt`, `prompt-costas.txt`, `prompt-esquerda.txt`, `prompt-direita.txt`. Evidência técnica: `validation.json`, com dimensões, transparência, tamanho eSHA256 de cadaPNG. Originais da geração permanecem em `C:/Users/Gustavo/.codex/generated_images/01a08de3-117f-7df3-b34f-ee258a317ddb/`.

Nenhum arquivo de execução do cliente/servidor foi alterado nesta etapa. Próximo passo: gerar noTripo e analisar oGLB/textura antes de adaptar ao rig doJohnny.
