# Passagem de trabalho — SKIN-006

- Responsável: Codex; 08/10/2026 00:50UTC.
- Objetivo: restaurar Celina clássica e cadastrar Aurora independente com roupa própria.
- Estado: implementada/instalada; cliente real pendente.
- Base: SKIN-005 aprovada pelo usuário; backups/celina-aurora-v2-before preserva nove arquivos existentes. Clássicos puros em backups/celina-aurora-20261007-before.
- Alteração: skin6 Aurora e arma5 Aurora gratuitas, sem szattr bônus, em public/baxi e zh/herounlock.xml. Ícones próprios PNG nas URLs .txt. Manifesto26100711,5547entradas.
- Roupa: jaqueta ajustada fechada com mangas, painéis/costuras turquesa, calça técnica sem rasgos. Reaproveita topologia/rig original: pintura por superfície/UV e deslocamento visual máximo2,5mm para tecido; não é uma malha de vestuário inteiramente reconstruída. FPS conserva luvas navy da005; arma turquesa005 independente.
- Recursos: cinco novos bundles *_aurora; seis clássicos restaurados byte a byte. CAB e AssetBundleName exclusivos evitam colisão entre default e Aurora carregadas juntas. Asset names internos mantidos: loaders usam GetAllAssetNames()[0]. Animadores originais compartilhados.
- Fatos do código: SkinUnlock.Init e HeroUnlockData.Init leem IDs/modelos do XML; HeroDataManager.GetUseSkinData consulta equipamento local ou dados do outro jogador; PlayerCommon.GetHeroModel usa o modelo da skin. Database.generateFullHeroData inclui IDs1–15; preços XML zerados tornam cosméticos gratuitos. Socket12007/12017 já persistem seleção.
- Testes: scratch/celina-aurora-v2/verify.py passou: seis defaults idênticas,1525objetos Aurora reabertos, dependências externas iguais, identidades distintas, pesos/bindposes/índices/UVs preservados, sem triângulos invertidos; catálogos/manifesto/referências válidos. Build compara objetos e só permite AssetBundle, uma textura e uma mesh adicionais sobre005.
- Teste servidor: initializeDatabase sem iniciar serviços confirmou preçoszero1017_1_6/1017_2_5, geração de equipamento6/5 e mapeamento novo; repetido catálogo na Oracle antes do reinício.
- Cliente real: não testado. Prévia é render estático do mesh, não prova animação ou sincronização de dois clientes.
- Oracle: instalada08/10 00:49UTC e reiniciada com autorização explícita (havia4conexões). PM2online;16URLs HTTP/hashes eTCP7777confirmados. Backup /home/ubuntu/backups/celina-aurora-v2-20261008T004947Z/runtime.tar.gz.
- Evidências: candidate-assets/celina-aurora-v2/checks.json, runtime-files.json; scripts/build/verify/register/deploy e preview.png em scratch/celina-aurora-v2.
- Próximo passo: reiniciar ambos os clientes; selecionar Celina/Aurora6 e armaAurora5, comparar com clássica1, conferir seleção/partida/respawn e observador inimigo. Arma é selecionada separadamente; modelo de arma exposto no menu continua sHeroWeapon clássico compartilhado (limite nativo do cadastro), materiais equipados em partida são exclusivos.
- Reversão: restaurar os nove arquivos do backup remoto e reiniciar; sete arquivos exclusivos podem permanecer sem referências. Não restaurar sobre mudanças concorrentes posteriores.
