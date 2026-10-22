# Passagem de trabalho — SKIN-008

- Responsável: Codex;08/10/2026. Objetivo: melhorar textura e UVs da Aurora, mostrar prévia antes de instalar.
- Estado: candidato pronto para avaliação visual do usuário. Oracle não alterada.
- Base: SKIN007; usuário achou a textura ruim. Rig/roupa3D existentes mantidos.
- Alteração:16áreas UV dedicadas(frente/costas/mangas separadas, ombros, cotovelos, punhos, gola, barra, bolsos/abas/painéis/ziper/acabamento).26vértices duplicados apenas para costuras UV, superfície e pesos exatamente iguais;3419vértices roupa/6327total.
- Textura: imagegen built-in editou guia4x4 em duas etapas; segunda reduziu trama/fotorrealismo para tecido estilizado suave, mantendo dobras/costuras e materiais diferenciados. PNG original1254quadrado adaptado2048/DXT5/10mips para o bundle. Fontes geradas copiadas para o projeto comoatlas-first.png/atlas-art.png.
- Shader confirmado: material original é toon,_MainTex e_ToonShade; não há mapas normal/roughness declarados. Não prometer acabamento PBR a partir desta pintura.
- Testes: build_candidate.py reabre1211objetos; somente Mesh(duplicação deUV) eTexture2D da roupa mudaram. Triângulos por posição/normais e pesos reproduzem exatamente a007;62bones/materiais/colliders/animações preservados, UVs finitos dentrodoatlas. Verificação dos3hashes de execução local passou.
- Prévia: render_candidate.py usa textura comprimida real, filtro bilinear/mip aproximado; inspecionadas frente/lado/costas. Não reproduz exatamente o shader/iluminação Unity.
- Cliente real: não testado; aguarda avaliação da prévia, conforme combinado antes desta revisão.
- Oracle: não alterada nem reiniciada; permanece007CRC26100801. Sem novo backup remoto porque não houve instalação.
- Arquivos: candidate-assets/celina-aurora-texture/xingjilieren_tps_aurora.mo,validation.json,atlas-art.png,jacket-atlas.png,preview.png,comparison.png,Celina-Aurora.glb; scripts emscratch/celina-aurora-texture.
- Fonte: atlasGuide/prontuario16slots emremap.py; scripts/source geometry007. Alterações só em candidatos, sem alteração de manifesto/catálogo executável.
- Próximo passo: usuário avaliarcomparison.png; se aprovado, backup local/remoto do bundle/ícone/manifesto, novoCRC e instalaçãoestática semreinício, depois testeUnity.
- Limite: seleção/roupaTPS revisadas; FPS/armas/calça/cabelo não modificados nesta etapa. Reservas de candidatos liberadas, preservar os resultados.

