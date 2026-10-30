# Passagem de trabalho — SKIN-049

- Responsável / destinatário / data: Codex / usuário e próxima IA /24/10/2026.
- Objetivo: corrigir gola/sobretudo na referência de costas048 relatada estranha pelo usuário.
- Estado: nova referência gerada e conferida; avaliação do usuário pendente.
- Base:048 em `candidate-assets/lily-modern-witch/crimson/multiview/`, preservada integralmente.
- Arquivos: novos derivados em `candidate-assets/lily-modern-witch/crimson/multiview-v2/`; frente e perfis copiados sem edição.
- Fatos visuais048: gola com pontas laterais parecendo lapelas frontais, enorme abertura traseira desde a cintura e abas muito separadas.
- Decisão: editar somente costas, usando frente e perfil como guias; gola acompanha nuca, casaco fechado sobre quadril/coxa e abertura menor.
- Implementação: image_gen integrada, `back-prompt.txt` persistido; novo back.png e pacote quatro vistas com prompts/metadados.
- Inspeção: sem pontas laterais exageradas, painel traseiro contínuo sobre quadril/coxa, fenda e separação das abas reduzidas; mesma A-pose/paleta/identidade.
- Limite: fenda final começa na coxa, não no joelho solicitado no prompt; vistas ilustradas não calibradas. Nenhuma garantia de reconstruçãoTripo semGLB.
- Testes:4RGBA1024×1536/alpha/contornos internos, SHA256 das3vistas preservadas e ZIPCRC conferidos.
- Cliente real: não aplicável; referências apenas, nenhum rig/poseUnity validado.
- Oracle: não alterada; sem execução/DLL/manifesto/cadastro.
- Evidências: README/metadata/prompts no pacotev2; geração original preservada em image_gen.
- Próximo passo: avaliação visual do usuário e geraçãoGLBTripo para adaptaçãoLily1033.
- Bloqueio: nenhum para entrega.
