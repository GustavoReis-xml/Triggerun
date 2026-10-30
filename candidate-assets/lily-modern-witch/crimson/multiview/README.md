# Lily — bruxa vinho e preto / referências multivista

SKIN-048, 23/10/2026. Extensão da frente aprovada SKIN-047. Imagens produzidas com a ferramenta integrada image_gen; prompts completos incluídos nos quatro arquivos `*-prompt.txt`.

## Imagens

- `front.png`: frente principal aprovada, copiada sem alteração.
- `back.png`: costas.
- `left.png`: perfil com o nariz apontando para a esquerda da imagem.
- `right.png`: perfil com o nariz apontando para a direita da imagem.

PNG individuais com fundo transparente, corpo inteiro e a mesma A-pose. Os nomes dos perfis descrevem a orientação visual, não o lado anatômico. Na prévia, o programa pode exibir cores RGB de pixels transparentes; o canal alpha foi conferido.

## Uso no Tripo

Use a frente como referência principal e as demais nos campos correspondentes da multivista. Se a interface mostrar uma orientação específica para os perfis, siga a direção do rosto na miniatura. Não envie uma montagem das quatro imagens num único campo nem misture com as versões roxas anteriores.

Estas são vistas ilustradas coerentes, não projeções calibradas de um mesmo modelo 3D. Podem existir pequenas diferenças na gola, nos recortes do casaco, no cabelo ou nos saltos; a frente prevalece. A A-pose mostra os braços abertos de frente e encurtados em profundidade de perfil.

Depois da geração, envie o GLB para conferência de geometria, mãos, rosto e pesos. O casaco longo ainda precisará de adaptação ao rig da Lily; nenhuma física ou compatibilidade de animação foi validada nesta etapa.

Nenhum arquivo de execução, cadastro de skin, DLL ou recurso da Oracle foi alterado. `metadata.json` registra origem, dimensões, alpha e hashes.
