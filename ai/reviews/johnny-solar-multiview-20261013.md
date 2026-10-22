# Passagem de trabalho — SKIN-031

- Responsável / destinatário / data: Codex / usuário e próxima IA / 13/10/2026 BRT.
- Objetivo: quatro referências do Campeão Solar emT-pose para guiar proporções noTripo.
- Estado: imagens geradas, inspecionadas e entregues; nenhum modelo novo/retargeting/cadastro criado.
- Base: Johnny1031/lanbo_tps, análiseSKIN-030. Pedido explícitoT-pose substitui sugestão préviaA-pose.
- Arquivos: `candidate-assets/johnny-solar/multiview/`, quatroPNG1254×1254RGBA, ZIP, README, prompts completos evalidation.json.
- Execução: ferramenta integradaimage_gen; frente usa render doJohnny nativo para identidade/proporções. Costas e perfis derivados da frente; perfil direito também usa esquerdo para profundidade.
- Direção: adulto musculoso, tronco e braços expostos, calça grafite, cinturão solar bronze/vermelho, proteções compactas e braceletes. Sem armas/capa/ombreiras volumosas.
- Inspeção: mesma identidade/paleta/traje/volume geral; T-pose frontal/traseira; braços foreshortened nos perfis sem serem baixados. Face/nariz/olhos legíveis, corpo e pés visíveis.
- Limite: referênciasIA não são projeções calibradas da mesma mesh. Pequenas diferenças nos detalhes de armadura/contornos; frente autoritativa e perfis guiam profundidade. Rig nativo exige conferirGLB depois.
- Testes: quatroPNG válidos/transparência real/tamanho<20MB cada/SHA256 registrados; fontes copiadas sem pintura/edição manual. Não há testeUnity nesta etapa.
- Cliente/Oracle: intactos. Sem backup remoto/reinício necessários.
- Evidências/originais: `validation.json`; arquivos originais preservados em `.codex/generated_images/01a08de3-117f-7df3-b34f-ee258a317ddb/`.
- Próximo passo: usuário subir quatroPNG na multivisão doTripo e enviarGLB; inspecionar anatomia/UV/olhos/dedos e adaptar à hierarquia/pesosJohnny54bones, preservandoIK/armas.
- Bloqueio: nenhum. Reserva liberada.
