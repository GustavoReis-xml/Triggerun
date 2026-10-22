# Passagem de trabalho — SKIN-002

- Responsável: Codex, 2026-10-07. Estado: protótipo implementado/instalado; teste real pendente.
- Objetivo: físico mais próximo do retrato, preservando esqueleto/animações/hitbox. Usuário autorizou testar.
- Base: bundles originais UnityFS5.5.2f1; backups/lamster-shape-20261007-before guarda male_tps.mo, male_fps.mo e files_dir.xml anteriores. Demais alterações do projeto preservadas.
- Implementação: scratch/lamster-shape/build-shape.py lê originais viaUnityPy, deforma vértices no bindspaceZ-up; peito/ombros, volume radial de braços ao longo de segmentos do rig, cintura reduzida suavemente. TPS/FPS possuem segmentos próprios.
- Normais por inversa-transposta doJacobian; tangentes transformadas/ortogonalizadas preservando handedness. Atualiza bounds de malha/submeshes; expande somente bounds de culling de renderers afetados. Não altera colisores físicos/hitboxes.
- TPS: malha corpo com deslocamento máximo4,64cm; pequenas correções de continuidade mãos0,13cm/pernas0,10cm. Cabeça/pés intactos. FPS: máximo3,37cm. Mesma contagem de vértices/triângulos; texturas e shaders originais.
- Preservados por comparação serializada: bones/Transforms, hierarquia, bindposes, pesos, Avatars, animadores, índices, UV/colorstream, materiais/texturas, scripts, colliders/joints/rigidbodies/LOD. Somente3Mesh+3SkinnedMeshRenderer(bounds) emTPS e1+1 emFPS alterados.
- Verificação: candidatos reabertos porUnityPy;190objetosTPS/110FPS comparados; checagem de todas chaves exceto atributos explicitamente alterados. Seis malhas avaliadas sem triângulos invertidos, coords finitas, área válida; Jacobian mínimo0,506TPS/0,856FPS. Normais/tangentes atualizadas.
- Visual: comparação geométrica original/protótipo em scratch/lamster-shape/shape-comparison.png; render estático sem texturas/sem transformações de prefab/animação. Não representa a aparência finalUnity.
- Candidatos: candidate-assets/lamster-shape/male_tps.mo SHA256 9e0072c9931af3dfe8a44d7e162c08bef1c001b44d8d6d921995872de28d364e; male_fps.mo 999129ba277f475ba2cb10651a1d213e684c2ff05fa7e5e06e5d246454ec390a.
- Instalados: downloads/cdn_cache/res/1.1.1/StreamingAssets/HeroAnimator/male_{tps,fps}.mo; manifesto somente essas2entradas crc26100706 (versão1.1.1 preservada). Atualização da baseLamster, não skin opcional selecionável.
- Oracle: instalado07/10 23:59UTC (20:59BRT), backup /home/ubuntu/backups/lamster-shape-20261007T235909Z/runtime.tar.gz. Guardas hashbase, pacotes verificados, substituições por arquivos temporários, manifesto por último. Sem reinício/sem alteração de lógica.
- HTTP: ambos bundles baixados do servidor e hashes iguais aos candidatos; manifesto servido confirma26100706.
- Assembly/DLLs: nenhuma alteração nesta tarefa. Cliente real: NÃO testado. Testes de lógica servidor não repetidos pois somente bundles/versão de recursos mudaram.
- Próximo passo: fechar/abrir cliente para atualizar cache; selecionarLamster, conferir modeloTPS e braçosFPS, socos/corrida/pulo/morte/respawn e desaparecimento porculling. Capturar output_log se houver falha deAssetBundle.
- Limite: não é modelo reconstruído nem textura nova/realista; primeira deformação proporcional. Validação de deformações em animações depende deUnity. Parte do volume visual pode exceder hitbox original, intencionalmente preservada; observar discrepância no teste.
- Reversão: restaurar2bundles originais e manifesto com nova chavecache seclientesjábaixaram26100706; somente voltarCRC antigo pode reutilizar cache original conhecido, mas não reutilizar26100706 para bytesdiferentes.
- Reserva liberada; aguarda avaliação real do usuário.
