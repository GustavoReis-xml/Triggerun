# Khan — Maré Alta / referências para Tripo

SKIN-054,24/10/2026. Quatro imagens individuais1024×1536, fundo transparente real, pose neutra emA. Geradas pela ferramenta integrada image_gen; quatro prompts completos e metadata com hashes incluídos.

- `front.png`: frente principal, prioridade para rosto/proporções/design.
- `back.png`: costas.
- `left.png`: perfil com nariz apontando para a esquerda da imagem; lado com relógio visível.
- `right.png`: perfil com nariz apontando para a direita da imagem; lado com toalha e painel coral visíveis.

No envio multivista, coloque cada PNG em seu campo correspondente, seguindo a orientação do rosto nas miniaturas. Envie as imagens separadas; o conceito anterior com cenário serve para aprovação artística, não para substituir esta frente neutra.

Direção de proporções baseada no modelo nativo do Khan1034/hz_tps:altura≈1,865m, pelve≈53,5%, ombros≈81% e pivô da cabeça≈88,5% da altura. Preservei o porte forte aprovado e comprimentos de membros equilibrados. Esses valores guiaram a geração; as imagens não são projeções calibradas de uma única malha nem garantem escala métrica exata no resultado do Tripo.

A toalha e o painel coral ficam no lado anatômico direito; o relógio, no esquerdo. A estampa de folha pertence à frente da coxa direita. A toalha se apoia no ombro e cai junto ao corpo, sem manga ou tecido enrolado no braço. É um único tecido dobrado; dobras, pequenos detalhes dos óculos e sandálias podem variar entre as vistas. Em caso de diferença, a frente prevalece.

Nos perfis, o braço aberto lateralmente fica encurtado em profundidade; não é outra pose. Os arquivos têm alpha verdadeiro, embora algumas prévias exibam o RGB dos pixels transparentes como um fundo luminoso.

Após a geração, envie o GLB com a textura. Ainda será necessário conferir anatomia sob a toalha, dedos, pés, acessórios e encaixar ao rig do jogo; não há skin instalada nesta etapa.
