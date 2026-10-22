# Passagem de trabalho — SKIN-027 referências Nezha

- Responsável: Codex; 12/10/2026 BRT. Objetivo: referências para geração Tripo de nova skin baseada no retrato RPG do usuário.
- Estado: referências entregues; modelo 3D ainda não gerado. Base: análise em nezha-rpg-analysis-20261012.md.
- Arquivos novos: candidate-assets/nezha-rpg/multiview/01-frente.png,02-esquerda.png,03-direita.png,04-costas.png; pacote nezha-rpg-multiview.zip no diretório pai.
- Decisão explícita do usuário: preservar braços originais da ultimate sem retrabalhar sua aparência. Gerar corpo com apenas dois braços e sem arma incorporada.
- Design: cabelo branco/vermelho, marcas faciais vermelhas, olhos laranja, armadura preta/bronze com dragões nos ombros e tecido vermelho. Pernas/costas complementam retrato original.
- Geração: ferramenta integrada image_gen; frente derivada do retrato, três vistas derivadas da frente. Prompts completos em multiview/prompts.json.
- Correção: costas iniciais repetiam rostos dos dragões/joias/tasséis frontais; regeneradas com carapaças traseiras, couraça e joelhos sem pedras frontais.
- Verificação: quatro PNGs 1024×1536 RGBA, transparência real, corpo inteiro presente; inspeção visual e validation.json com hashes/bounds. ZIP reempacota os quatro PNGs sem alteração.
- Limites: vistas geradas não são projeções de uma mesma malha; há pequenas diferenças em faixa/placas e leve perspectiva nas laterais. A frente é referência principal; avaliar resultado Tripo antes de adaptar. Anatomia/rig/compatibilidade ainda não testados.
- Cliente real: não testado; nenhuma alteração de execução. Oracle: intacta.
- Próximo passo: usuário gerar modelo usando slots frente/esquerda/direita/costas, exportar GLB com texturas; inspecionar ombros/pescoço e transferir ao rig original, preservando malhas auxiliares da ultimate por IDs/hierarquia.
- Reserva de imagens liberada. Não há cadastro, candidata de bundle ou instalação.
