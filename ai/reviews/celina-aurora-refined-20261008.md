# Passagem de trabalho — SKIN-009

- Responsável: Codex;08/10/2026. Objetivo: refinar geometria quadrada apontada pelo usuário, preservando textura008.
- Estado: candidato pronto para avaliação visual; não instalado.
- Alteração: perímetros de bolsos/painéis arredondados, faces tesselladas sobre a curvatura do peito eliminam atravessamentos do fan plano antigo; espessuras reduzidas(pocket24→8mm, yoke9→4mm). Bolsos superiores estreitados.
- Ombros: reforços10→3,5mm com perfil arredondado; cotovelos6→2,5mm. Mangas mais ajustadas e caminhos Hermite suaves; pequenas variações de raio perto dos cotovelos. Corpo/barra menos largos e interpolação suave entre seções.
- Malha:7284vértices de roupa após UVs,12910triângulos de roupa,10192vértices/15628triângulos totais;25partes/62bones. Mais geometria que008 para bordas/curvatura; desempenho no jogo ainda não medido.
- Fatos: comparação1211objetos entre008/final009 comprovou SOMENTE Mesh diferente. Texturas/materiais/rig/efeitos idênticos; física/colisores/código/manifesto intactos.
- Testes: verify_base.py verificou pesos normalizados/bones/índices/bounds/faces sem degeneração e três poses sintéticas. UV remapeado preserva exatamente superfície/normais/pesos dessa base. Primeira tentativa encontrou128faces degeneradas por seçõesZ coincidentes em float32; deduplicação arredondada corrigiu e testes passaram.
- Inspeção: frente/lado/costas, roupa sem textura e pose de braços inspecionados; prévia usa atlas comprimido real e filtragem aproximada. Não é renderUnity.
- Cliente real: não testado. Não declarar animações/clipping/desempenho validados no jogo.
- Oracle: não alterada/não reiniciada, permanece007CRC26100801. Trêshashes locais de execução conferidos intactos; candidatos novos não exigiram backup remoto.
- Arquivos: candidate-assets/celina-aurora-refined/{xingjilieren_tps_aurora.mo,comparison.png,preview.png,pose-preview.png,validation.json,Celina-Aurora.glb,Aurora-Jacket.obj}; base/model-report.json e base/validation.json; scripts emscratch/celina-aurora-refined.
- Fontes: build_base.py é gerador final com correção de seções; não reexecutar prepare.py sem aplicar fix_rings.py (o preparo captura estágio anterior). Atlas original008 copiado, não gerado novamente.
- Próximo passo: usuário avaliar comparison.png; depois backup local/remoto, novoCRC e instalação estática com ícone atualizado se aprovada. Validar corrida/mira/habilidades/respawn com clientes reais.
- Reservas: resultados preservados; reserva liberada para revisão.
