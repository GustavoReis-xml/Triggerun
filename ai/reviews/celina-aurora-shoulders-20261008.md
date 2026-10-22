# Passagem de trabalho — SKIN-010

- Responsável: Codex, 08/10/2026. Candidato pronto para avaliação visual; não instalado.
- Removidas38faces visíveis do colarinho cobre antigo (ilha de vértices1941..2097). Lenço/cabelo preservados;947faces originais cobertas no total.
- Eliminados os dois reforços elevados dos ombros. Cabeça de manga remodelada em domo com cinco seções; corpo superior elevado/alargado para fechar a junção com gola e mangas.
- Malha final:7086vértices/12654triângulos de roupa,9994vértices totais,23partes,62bones originais.
- Comparação009→010: somente Mesh diferente entre1211objetos. Texturas, materiais, bones, bindposes e efeitos preservados; código/hitbox/manifesto intactos.
- Testes automatizados: estrutura/índices/pesos/faces sem degeneração e três poses sintéticas passaram; UV preserva superfície e pesos da nova base. Três hashes dos arquivos instalados permanecem iguais.
- Inspeção visual: frente/lado/costas e pose de mira conferidos. Primeira prévia revelou abertura na junção superior; contorno do corpo corrigido e reconstruído. Renderizador aproximado, não Unity.
- Cliente real: pendente; animações e clipping em jogo ainda não validados.
- Oracle: permanece007CRC26100801, não alterada nem reiniciada. Nenhum arquivo de execução foi modificado.
- Artefatos: candidate-assets/celina-aurora-shoulders/{comparison.png,preview.png,pose-preview.png,xingjilieren_tps_aurora.mo,Celina-Aurora.glb,Aurora-Jacket.obj,comparison-validation.json}; scripts scratch/celina-aurora-shoulders.
- Reproduzir com build_base.py,verify_base.py,remap.py,build_candidate.py,render_candidate.py,export_candidate.py,check_pose.py,finish.py. prepare.py é histórico e não contém o último ajuste superior; não reexecutá-lo sobre os geradores finais.
- Próximo: avaliação do usuário; instalação somente após aprovação da prévia, com backups local/remoto e novoCRC. Reserva liberada.
