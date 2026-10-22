# Passagem de trabalho — SKIN-015

- Responsável: Codex;08/10/2026. Estado: arte/ícone instaladosOracle, helperHUD instalado local;Unity pendente.
- Objetivo: portrait ilustradoScarlet sem texto, corrigido pelo usuário para formato circularHUD/loja.
- Arte: image_gen built-in usando préviaScarlet como referência; portrait original e versão circular emscratch/scarlet-portrait/{scarlet-portrait.png,round/scarlet-portrait.png}. Sem nome/legenda; identidade máscara/cabelo/cores preservada.
- Ícone: PNG RGBA256x256 com círculo e transparência exterior, emIcon/herounlock/skin/celina_scarlet.txt. ManifestoCRC26100804; primeiroportrait retangular26100803 foi substituído.
- Cliente: NewHeroHpUi.Refresh/setHead usamSkinUnlock.sIconName→herounlock/skin/. HeroInfoUICon usa mesmoícone. setHead força109x158, distorcendo círculo.
- Correção: CombatHud.cs adicionaTriggerunRoundSkinPortrait viaEnsure; somentecelina_scarlet ajusta109x109 mantendo largura original. Restaura dimensões ao mudar skin/herói/UI; nada modifica a loja ou outros heróis.
- Compilação: Roslyn8.0.403/net35 contraUnity/Assembly reais; incluiCombatHud,CombatFeedback,LamsterMaterials,LamsterArea,BuffVoices. Todas as funções anteriores preservadas;24métodos comparados, somenteEnsure altera para anexarcomponente.
- Instalação local: usuário fechou doisclientes; gateTRIGzero/hashbase conferidos;TriggerunCombatHud.dll copiado e hash confirmado. Assembly-CSharp.dll não alterado. Outros computadores precisam receber essehelper para corrigir o aspectoHUD; arte chega pelaOracle.
- Backups locais: backups/scarlet-portrait015-before para primeiroportrait;backups/scarlet-round015-before para ícone/manifesto/CombatHud.cs/helper atual.
- Oracle: sem reinício; primeirobackup/home/ubuntu/backups/scarlet-portrait015-20261008T234124Z/runtime.tar.gz; versãofinal/home/ubuntu/backups/scarlet-round015-20261008T234513Z/runtime.tar.gz. Ícone/manifesto hashesbase/stage/final conferidos.
- Testes: PNG256x256/RGBA/cantosalpha0/centroalpha253 (leve transparência do gerador; teste exige>=250);2URLs ícone1.1.1/1.3.5 e manifestoHTTP bytesexatos;24métodoshelper preservados;compilação/gatehashOK. Unity real pendente; não afirmar círculoHUD confirmado apenas pela arte.
- Evidências: scratch/scarlet-portrait/{helper-validation.json,install_helper.ps1,TriggerunCombatHud.dll};round/{validation.json,base.sha256,new.sha256,install.sh,stage};prompt.txt e ferramenta built-in. Nenhuma mudança de modelo/gameplay.
- Fonte original: ScarletMoonMoon/CC-BY-SA4.0 conformecréditosSKIN014. Usuário confirmou model swap funcional, animaçõesestranhas mas aceitáveis.
- Próximo: reabrircliente, conferirScarlet na loja eHUD; manter retrato grande para usos futuros. Reserva liberada.

