# Passagem de trabalho — SKIN-066

- Responsável/data: Codex,25/10/2026.
- Objetivo inicial: quatro vistas da CarmenGrandeIlusionista065 paraTripo; conceito aprovado.
- Escopo final: usuário dispensou vistas adicionais e decidiu gerar somente pela frente. Trabalho restante encerrado a pedido, não declarar multiview completo.
- Entrega selecionada: `candidate-assets/carmen-zatanna/multiview/front.png` — frente original emApose,roupa065,semcartas,fundoRGBA1024x1536. NÃO é a variante posterior comcapa fechada.
- QA:frente corpo/rosto/cartola/sapatos inteiros,mãosvazias,rosa/cartacartolaàesquerdaanatômica;fundoalpha0 nospontosamostrados. HaloRGBvisível napréviadaferramenta étransparente noPNG.
- Prompts:front-prompt.txt+front-alpha-prompt.txt no mesmo diretório,geração image_gen integrado. Fonteantescapa preservada também emscratch/carmen066/front-before-cape.png.
- Ocorrências:duas costas euma atualizaçãofrontal rejeitadaspela ferramenta porcategoria sexual/etapaoutput. IDs/prompts emscratch/carmen066/progress.json e histórico. Não usadoCLI.
- Usuário sugeriu cobrir comcapa:costas opacas geradas emmultiview/back.png. Essa vista tem capa atépanturrilha e não forma conjunto final coerentecomfrente selecionada;NÃOenviarjuntas aoTripo semrevisão.
- Uma variante frontal comcapa fechada terminou durantecancelamento;previewgerado não selecionado,nãosubstitui front.png e nãofoipromovido paraassetfinal.
- Perfisesquerdo/direito:nãogerados;ZIPquatrovistas:nãocriado.
- Estado:frenteselecionada pronta, multiview dispensado pelousuário. Nãoalteradojogo/Oracle/rig.
- Próximo passo:aguardarGLBTripo gerado a partir dafrente original;adaptargeometria/pesosCarmen emtarefa futura.
- Reserva liberada.
