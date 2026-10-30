# Passagem de trabalho — SKIN-057

- Responsável / destinatário / data: Codex / usuário e próxima IA /24/10/2026.
- Objetivo: converter conceito056 aprovado em quatro vistas individuais para Tripo, com proporções guiadas pela Valk nativa.
- Estado: referências entregues; geraçãoGLB/adaptação ao jogo pendentes.
- Correção de direção expressa pelo usuário durante esta tarefa: cabelo igual Saber, loiro claro/franja/coque baixo/laço preto; substitui bob assimétrico do conceito056.
- Base: `scratch/valk-skin-inspection/inventory.json`, waerjili_tps/72bones/altura1,774m; pivôs extraídos para orientar proporções e braços.
- Proporções: Pelvis1,044m/UpperArm1,443/Forearm1,295/Hand1,128/Calf.671/Foot.215; usados como guia visual, sem deformar malha nativa.
- Entrega: `candidate-assets/valk-eclipse/multiview/{front,back,left,right}.png`,4RGBA1024x1536, A-pose/corpo inteiro/sem espada, fundo transparente.
- Frente autoritativa para face/roupa/proporções;perfís complementam espessura e cabelo, costas definem coque/laço/placa posterior.
- Roupa: negro/violeta/carmesim do conceito;saia em abas, frente aberta, tecido em repouso. Placas frontais de joelho não se repetem atrás; flexão posterior em tecido preto.
- Geração: image_gen integrado;frente corrigida em uma segunda passagem de alpha;primeiras costas tinham joelheiras frontais atrás e não entram no pacote.
- Refinamento traseiro inicial recusado pela ferramenta;nova geração completa de costas usando frente obteve resultado utilizável. Nenhuma edição manual artística foi feita.
- QA:4arquivos decodificados/alpha/bordas/hashes/ZIP verificados; visual de todas as saídas conferido. Alturas variam alguns pixels; ornamentos/perfis são interpretados, não vistas calibradas de um mesmo modelo3D.
- Evidências: `metadata.json`, prompts completos e README na pasta multiview. Rascunhos preservados fora do ZIP, não enviar ao Tripo.
- Pacote: `valkyrie-eclipse-tripo.zip`,11arquivos, contém somente4imagens finais/README/metadata/prompts utilizados.
- Cliente real/Oracle/runtime: não alterados, não instalados; teste de imagem não valida rig/animação/física.
- Próximo passo: usuário enviar quatro vistas ao Tripo, exportarGLB texturizado; adaptar72bones nativos, conferir coque/gola/ombros/joelhos e separar tecido da perna antes física.
- Limite: espada do conceito não integra as referências corporais; manter arma nativa inicialmente, sem promessa de arma nova nesta etapa.
