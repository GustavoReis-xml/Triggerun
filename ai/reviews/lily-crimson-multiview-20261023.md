# Passagem de trabalho — SKIN-048

- Responsável / destinatário / data: Codex / usuário e próxima IA / 23/10/2026.
- Objetivo: completar a frente047 aprovada com costas e dois perfis individuais para Tripo.
- Estado: referências entregues e verificadas; modelo/adaptação ainda pendentes.
- Base: `candidate-assets/lily-modern-witch/crimson/front.png`, SHA256 `55e09f1e3f09be1e50c5f416a2a6aca391f1ff2cfba08fddad7f2b6e180f7201`.
- Arquivos: novos derivados em `candidate-assets/lily-modern-witch/crimson/multiview/`; original intacto.
- Fatos: usuário escolheu vinho/preto e referência de cabelo preto/mechasvermelhas, substituindo045/046; pediu todas as vistas.
- Decisão: frente autoritativa, mesma A-pose, geração sequencial costas→perfil esquerdo→direito com referências anteriores para reduzir diferenças.
- Implementação: quatroPNG1024×1536, prompts individuais, README, metadata com hashes e ZIP.
- Ferramenta: image_gen integrada; nenhuma API/CLI externa usada.
- Inspeção visual: cabelo/gola/mangas/boots/coattails compatíveis; perfis são90°, costas180°, corpo inteiro.
- Testes: PIL confirmou4RGBA, alpha0–254, contornoalpha≥32 dentro das margens e SHA dafrente preservado; ZIP conferido com CRC e conteúdo.
- Limites: vistas ilustradas, não projeções calibradas; pequenas diferenças de gola/cabelo/cauda do casaco/salto, frente prevalece. Nenhuma garantia da reconstruçãoTripo semGLB.
- Cliente real: não aplicável, referências apenas; rig/animação/coatphysics não testados.
- Oracle: não alterada; semDLL/runtime/cadastro/manifesto.
- Evidências: `metadata.json` e quatro `*-prompt.txt` dentro do pacote; arquivos gerados originais preservados no diretório image_gen.
- Próximo passo: usuário gerar GLB com multivista, enviar e então inspecionar geometria/rosto/mãos e adaptar ao rigLily1033 conforme análise045.
- Bloqueio: nenhum para entrega de imagens.
