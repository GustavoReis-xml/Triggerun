# Passagem — LAMSTER-003

- Responsável: Codex, 07/10/2026; objetivo: destravar loading após selecionar Lamster.
- Estado: implementação, testes simulados e instalação cliente concluídos; Unity pendente.
- Relato real LAMSTER-002: Lamster não carrega; URL HeroAnimator/.mo retorna403. Não considerar seleção/combate validados.
- Causa confirmada: output_log registra HeroPlayer->LoadWeaponMat. PlayerCommon.GetNowModelWeaponMat inclui strings vazias de WeaponUnlock; GamePlayer.LoadWeaponMat concatena URL sem validar e só avança callback se www.error=null.
- Falha introduzida por LAMSTER-002: cadastro de arma com lowmat_1st/highmat_1st/commat_3rd vazios. CHeroWeapon aceita vazio, mas o pré-carregador GamePlayer não aceita.
- Decisão: preservar materiais embutidos do modelo desarmado; não substituir por material de outro herói nem criar arquivo fictício .mo.
- Alteração: helper TriggerunMaterialFilter.ForPlayer filtra null/vazio/espaços/0 somente se m_nModelIndex=2000; mantém materiais válidos e lista dos demais heróis intactos.
- Patch: postfix em PlayerCommon.GetNowModelWeaponMat. Helper incorporado a TriggerunCombatHud.dll já obrigatório; novas fontes client-patches/LamsterMaterials.cs e tools/patch-lamster-materials.ps1.
- Testes: helper compilado net35 contra bibliotecas reais; tests/LamsterMaterialsFixture.cs com6 verificações simuladas. Comparação de14.780 métodos, somente1 alterado e desvios curtos válidos. Sem testes servidor repetidos: execução Oracle não mudou.
- Cliente: instalado com jogo fechado e hashes de base conferidos. Assembly SHA256 0373341552C5D8E1497E34494630733F7587704665EACBEE2B87F5EA75F5BDA6; helper A5A5B306770CD420995B1659F6579A4EBAB61E322C95B15176CD1DAF0ACF1481.
- Base/backups: backups/lamster-loading-20261007-before/client/{Assembly-CSharp,TriggerunCombatHud}.dll. Log em scratch/lamster-loading/output_log-before.txt; auditoria patch-result.json na mesma pasta.
- Cliente real: não testado após patch; confirmar seleção→mapa, visão FPS/TPS, socos/cura/HUD e ausência de HeroAnimator/.mo no novo log.
- Oracle: não alterada, sem reinício; mantém LAMSTER-002 e99 testes anteriores.
- Distribuição: outros clientes precisam das DUAS DLLs atualizadas, mantendo helpers HIT/MODEL. Não recompilar CombatHud sem incluir LamsterMaterials.cs agora.
- Próximo passo: abrir jogo e entrar com Lamster; se outro recurso falhar, capturar nome e método do log antes de alterar dados adicionais.
