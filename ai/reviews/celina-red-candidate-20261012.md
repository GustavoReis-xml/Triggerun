# Passagem — SKIN-021 candidata

- Responsável: Codex; usuário autorizou correção dos olhos/adaptação.
- Estado: candidata preparada e validada estruturalmente; instalação/cadastro não realizados.
- Fonte GLB Downloads preservada. Saídas exclusivas candidate-assets/celina-red; scripts scratch/celina-red-adapt.
- Olhos: image_gen editou a vista frontal, projetada para12914pixels nos UVs. Pixels externos à máscara permanecem idênticos. Detalhe visual de íris melhorado; lentes continuam geometria opaca integrada, sem transparência física. Não declarar reconstrução ocular completa.
- GLB corrigido celina-red-eyes.glb contém texturaPNG4K; todos os accessors de geometria/UVs/normais iguais à fonte, sem rig.
- TPS independente xingjilieren_tps_red.mo:10881v/10105tri,rig62bones originalCelina e paletas inalteradas. Pesos transferidos por superfície nativa, alcance braços ajustado, transição de cabelo/saia suavizada. Albedo2K/DXT5/shaderCelina.
- 1209objetos originais comparados; diferenças sóMesh/SMRs/AssetBundle autorizados e2objetos novosTexture/Material. Transforms/animator/colisores/scripts/armas preservados. CAB exclusivo.
- Bounds normais calculados no espaço rootBone; limites especiais999 preservados. Outros LODs nativos não substituídos nesta candidata.
- Três poses sintéticas: p99estiramento1,42mira/1,30corrida/1,24giro; máximos4,92/2,69/2,43. Não representam animações ou IK reais.
- Prévias: front-preview.png,poses-preview.png,face-0.png. Revisão visual feita. Cabelo sem física independente; saia justa ainda exige agachamento/corrida no cliente.
- Cliente real: não testado. Oracle/cliente/catálogos intactos; SKIN-020 diagnóstico preservado.
- Próximo passo: avaliar candidata e cadastrar skin independente1017, preservando skins existentes; instalar recursos com backup/CRC/HTTP e validar duas instâncias, movimento e mira em Unity.
