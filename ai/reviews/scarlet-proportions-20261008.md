# Passagem de trabalho — SKIN-013

- Responsável: Codex,08/10/2026. Estado: candidata revisada para avaliação, não instalada.
- Objetivo: corrigir distorção visual deSKIN012, priorizando funcionalidade solicitada pelo usuário.
- Reservados/novos: scratch/scarlet-proportions e candidate-assets/celina-scarlet-proportions. Candidata anterior preservada; execução intacta.
- Alteração: eliminadas escalas/translações independentes do torso/pernas/cabeça. Escala global uniforme1,07. Braços reposicionados; alcance ajustado longitudinalmente para cotovelos/mãosCelina, espessura uniforme. Dedos usam uma transformação rígida por mão, sem alongar cada falange.
- Critério funcional: pivô de ambas mãos coincide comHandCelina por assert no gerador; orientação de palma alinhada ao eixoHand→MiddleFinger. Pegada real da arma ainda precisaUnity.
- Proporções: verify_proportions.py comprova11046vértices de corpo e2870de cabelo fora das influências de braço coincidentes com fonte*1,07, erro máximo<2e-7m. Demais vértices misturam influência dos braços.
- FonteGLB/texturas/índices/esqueleto alvo são os mesmos deSKIN012;17.494vértices/30.704triângulos/62bones, cabelo de duas faces. Shader toon com diffuse; semPBR completo/física de cabelo.
- Testes: build.py,verify.py,verify_proportions.py passaram:1209objetos comparados, somente cinco existentes modificados/quatro adicionados, bindposes/bones originais, pesosnormalizados/índices/sem triângulos degenerados/três poses sintéticas; três hashes de execução intactos.
- Inspeção: mira e frente/lado/costas; torso/peito mantêm silhueta de origem. Prévia aproximada não equivale aUnity.
- Cliente real: não testado; proximidade de alguns pivôs perna/torso difere da fonte e requer teste correndo/agachando/atirando/respawn. Não prometer funcionalidade confirmada apenas pelas poses sintéticas.
- Oracle: não alterada nem reiniciada, permaneceSKIN007CRC26100801. Nenhum cadastro/catálogo/ícone novo instalado.
- Artefatos: xingjilieren_tps_scarlet.mo,Celina-Scarlet.glb,geometry.npz,report.json,validation.json,proportions-validation.json,preview.png,pose-preview.png,comparison.png,CREDITS.md.
- Reproduzir: build.py,verify.py,verify_proportions.py,render.py,export.py,comparison.py. prepare.py captura estágio anterior ao alinhamento final das mãos; não reexecutar sobre gerador final sem align_hands.py.
- Próximo: avaliar prévia, então preparar skinindependente/CAB/cadastro/FPS compatível e backups para testeUnity. Instalação não autorizada nesta etapa de prévia.
- Reserva liberada para revisão. Créditos/licença original preservados emCREDITS.md; condições da página divergiam dos metadados, verSKIN011.
