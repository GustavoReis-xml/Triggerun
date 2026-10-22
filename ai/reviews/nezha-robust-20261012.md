# Passagem de trabalho — SKIN-029

- Responsável / destinatário / horário: Codex / próxima IA / 13/10/2026 02:52:42 UTC (12/10 23:52 BRT).
- Objetivo: tornar o Soberano Ígneo mais robusto, conforme comparação real do usuário com Nezha original.
- Estado: implementação, testes simulados e instalação concluídos; nova versão ainda não testada no cliente real.
- Base: SKIN-028 instalada, hero1035/skin5; GLB e candidata028 preservados.
- Arquivos: `scratch/nezha029/`, `candidate-assets/nezha-rpg/robust/`, recurso `nezha_tps_rpg.mo`, `files_dir.xml`.
- Alteração: tórax/costas até16% mais largos e4,5% mais profundos; espessura radial dos braços até18%, antebraços até20%, com transição antes do punho. Mãos/altura/cabeça permanecem iguais.
- Ombreiras deslocadas8mm para fora. Onze componentes das placas da cintura receberam critérios simétricos de pesos, abertura reduzida até16%, profundidade até12%, pontas encurtadas até22mm.
- Coxas: retiradas influências residuais de bacia/perna oposta abaixo da transição do quadril; mistura suave na virilha, joelhos preservados. A nova pose sintética expôs esse problema já existente na028.
- Testes: `build.py` + `verify.py` aprovados; oito poses sintéticas, p99 de alongamento de arestas≤1,188 e máximo≤3,314. Pose com perna levantada: máximo caiu de4,804 na028 para2,763. Não equivale à animação nativa decodificada.
- Estrutura:31525v/21156tri/62bones; UV/triângulos/bindposes/hierarquia mantidos; mãos/cabeça com posições e pesos byte-equivalentes nas matrizes. Textura/material Unity byte-idênticos à028; apenas mesh/limites de renderers mudaram.
- FPS, armas, física e mesh auxiliar da ultimate intactos. Estratégia existente de usar a mesh completa em11renderers/LODs mantida.
- Prévia: `candidate-assets/nezha-rpg/robust/view-0.png` e `selection.png`, renderizações externas para diagnóstico.
- Cliente real: screenshots confirmam renderização da028; não declarar029 validada no Unity.
- Oracle: dois arquivos atualizados; CRC26101208,5558 entradas. Caminho/versão iguais; `src/http.js` envia os arquivos atuais e `database.js` armazena só caminho/versão, portanto sem reinício. PID701709 mantido.
- Verificação externa: quatro hashes HTTP em1.1.1/1.3.5 + TCP7777 aprovados; cópia local sincronizada sob guarda de hashes. Evidências em `scratch/nezha029/release/`.
- Backup local: `backups/nezha029-before/`; remoto: `/home/ubuntu/backups/nezha-rpg-skin029-20261013T025242Z/runtime.tar.gz`.
- Próximo passo: usuário reiniciar cliente e comparar seleção, movimento/tiros/ultimate. Se editar novamente, tomar029 como base publicada;028 permanece histórica. Nenhum bloqueio.
