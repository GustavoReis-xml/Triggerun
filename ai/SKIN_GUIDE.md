# Guia Definitivo de Adaptação de Skins (Tripo AI -> Trigger Run `.mo`)

Este guia documenta o pipeline padronizado para adaptar modelos `.glb` gerados no **Tripo AI** aos esqueletos `.mo` (Unity 5 AssetBundles) do Trigger Run com **precisão de 1ª tentativa**, evitando retrabalho com braços finos, cotovelos fora de eixo, mangas virando asas de morcego ou costuras rasgadas.

---

## 1. Ferramentas Padrão do Repositório

Antes de escrever qualquer `build.py` em `scratch/`, **sempre importe e utilize**:
- **`tools/hero_rig_profiles.json`**: Catálogo extraído diretamente dos `.mo` de todos os **24 heróis jogáveis**, contendo contagens de ossos, `path_id` dos `SkinnedMeshRenderer`, presença de `UpArmTwist`/`ForeTwist`, coordenadas exatas (`Pelvis`, `Spine2`, `Neck`, `Head`, `Clavicle`, `UpperArm`, `Forearm`, `Hand`, `Thigh`, `Calf`, `Foot`) e comprimentos dos membros.
- **`tools/skin_rig_helper.py`**: Módulo Python reutilizável com as funções matemáticas testadas:
  - `load_hero_profile(hero_id_or_name)`: Carrega o perfil completo do herói.
  - `measure_arm_landmarks(V_torso_aligned, S_raw, arm_mask, sh, el, hand)`: Mede **automaticamente** o centroide real do braço na malha do Tripo (punho, cotovelo e topo do braço) — **NUNCA chute coordenadas `su, se, sw` no olho nem copie de outro herói!**
  - `recenter_and_thicken_arm(...)`: Centraliza cada seção transversal do braço coaxialmente sobre os ossos `UpperArm -> Forearm -> Hand` e aplica ganho radial (`+20%` a `+24%` no bíceps/antebraço, suavizando para `1.0x` no punho para preservar mãos e unhas, e `0.86x` dentro do topo oculto da manga para nunca esbarrar no tecido externo).
  - `tailor_bell_sleeve(...)`: Suaviza a abertura da manga e reduz o excesso de aba/poncho coaxialmente ao redor do eixo `sh -> el`, impedindo que mangas boca-de-sino virem asas gigantes quando o braço levanta.
  - `compute_longitudinal_arm_weights(...)`: Aplica gradiente longitudinal real de Biped (`UpperArm -> UpArmTwist` e `Forearm -> ForeTwist`) sincronizado no topo com a boca da manga.
  - `compute_torso_sleeve_weights(...)`: Distribui pesos `Spine2 -> Clavicle -> UpperArm` na manga/ombreira sem rasgar as costas nem puxar a gola.
  - `rebuild_cylindrical_undersleeve_bridge(...)`: Constrói o tubo oculto do braço dentro da manga com volume anatômico (`row_tapers = (0.76, 0.45)`) alinhado entre `center` e `cap_center`, ancorando a tampa oculta dentro da cavidade do ombro (`maxOutside < 0.001`).

---

## 2. As 5 Regras de Ouro (Por que skins anteriores davam defeito e como evitar)

### Regra 1: Nunca chute `su, se, sw` manualmente
- **Causa do bug antigo:** Nos scripts antigos (`lily050`), as coordenadas do ombro/cotovelo fonte (`su, se, sw`) foram estimadas manualmente (`se_x = -0.258` quando o centro real da malha estava em `-0.210`). Isso deslocou o braço **4.8 cm para dentro e 3.8 cm para trás** do osso do cotovelo, fazendo o braço dobrar fora de centro e atravessar a manga.
- **Solução:** Sempre use `measure_arm_landmarks(...)` e `recenter_and_thicken_arm(...)` de `tools/skin_rig_helper.py`.

### Regra 2: Preservação e reforço de volume radial nos braços (`1.20x–1.24x`)
- **Causa do bug antigo:** O Tripo gera braços femininos/estilizados ligeiramente finos, e os scripts antigos aplicavam escala transversal `0.97` ao esticar o comprimento do braço.
- **Solução:** Use `upper_radial_scale=1.24` e `forearm_radial_scale=1.20` em `recenter_and_thicken_arm(...)`, com transição suave (`smooth`) para `1.0` abaixo de `wrist_s_z_hi` para não alterar mãos, dedos ou unhas pintadas.

### Regra 3: Ponte interna da manga (`undersleeve bridge`) cilíndrica até a cápsula do ombro (`sh`), nunca na parede lateral da manga
- **Causa do bug antigo:** Para esconder o topo do braço dentro da manga (`lily052`), usou-se `taper = 1 - 0.95*t` e escolheu-se o triângulo do casaco mais próximo da boca da manga (`Z = 1.22`), colando o fim do braço na **parede lateral da manga** no meio do bíceps em vez de levá-lo até o ombro (`sh`, `Z = 1.31`). Na pose `expression_A50` (braços a `116°`), o braço parecia sair/flutuar da lateral da manga em vez de sair do ombro.
- **Solução:** Use `rebuild_cylindrical_undersleeve_bridge(..., row_tapers=(0.72, 0.42))` — ele seleciona o triângulo âncora na **cápsula superior do ombro** diretamente sobre `sh` (`|S_x| in [0.115, 0.175], S_z in [1.265, 1.325]`), leva `rows[1..2]` coaxialmente pelo centro oco da manga até o ombro e sincroniza os pesos do topo oculto do braço com a manga (`maxOutside < 0.001`).

### Regra 4: Mangas curtas / boca-de-sino exigem ajuste coaxial (`tailor_bell_sleeve`) + peso de `Clavicle` na cápsula do ombro
- **Causa do bug antigo:** Em poses como `expression_A50`, o osso `Bip001 L/R Clavicle` eleva o ombro `11 cm` acima do `Bip001 Spine2`. Se a cápsula do ombro da roupa tiver apenas peso de `Spine2` + `UpperArm`, o ombro da roupa fica `11 cm` abaixo da articulação do braço quando os braços levantam.
- **Solução:**
  1. Ajuste a geometria de repouso da manga com `tailor_bell_sleeve(...)` para reduzir o excesso de aba (`radial_pull=0.42`, `min_radius=0.052`) coaxialmente ao redor do segmento `sh -> el`.
  2. Use `compute_torso_sleeve_weights(..., max_arm_weight=0.98, clavicle_weight=0.10, twist_share=0.50)` — que aplica `UpperArm/UpArmTwist` no corpo da manga e distribui `Bip001 L/R Clavicle` na cápsula do ombro (`1.15 < S_z < 1.33`), garantindo que a manga suba junto com o ombro em `expression_A50` sem rasgar as costas nem puxar a gola.

### Regra 5: Validação obrigatória em 5 poses críticas antes de instalar
- Sempre renderize e inspecione visualmente:
  1. `view-0.png` (T/A-pose de repouso)
  2. `stand` e `R_fire` (cotovelos trazidos à frente do peito / mira)
  3. `expression_A50` e `expression_D200` (braços erguidos `80°–116°` acima da cabeça)
  4. `expression_C51` (perfil lateral / costas da roupa)
- Verifique com `verify.py` que apenas `meshIds` (e `textureId` quando houver textura nova) mudaram no `.mo`, que `maxRootOutsideGarment < 0.001` e que os demais objetos e `DynamicBone` permanecem intactos.

---

## 3. Tabela Resumo dos 24 Heróis Jogáveis

| ID | Herói | Bundle TPS | Carreira | Ossos | Renderers | Twist Sup. | Twist Ant. | Alt. Pelve | Alt. Ombro | Envergadura Ombros |
|---:|---|---|---|---:|---:|:---:|:---:|---:|---:|---:|
| `1003` | **Alice** (`dianna`) | `jvjishou_tps.mo` | Sniper / Marksman | 72 | 16 | Não | Sim | `1.067m` | `1.416m` | `0.252m` |
| `1005` | **Glider** (`leizhenzi`) | `leizhenzi_tps.mo` | Assault / Rifle | 54 | 17 | Não | Não | — | — | — |
| `1006` | **Tagaloa** (`xingtian`) | `xt_tps.mo` | Defense / Heavy | 36 | 22 | Não | Sim | `1.186m` | `1.851m` | `0.502m` |
| `1015` | **Lars** (`zhongjiezhe`) | `76_tps.mo` | Assault / Rifle | 54 | 18 | Não | Sim | `1.004m` | `1.456m` | `0.432m` |
| `1016` | **Valkyrie** (`waerjili`) | `waerjili_tps.mo` | Defense / Heavy | 72 | 11 | Não | Sim | `1.044m` | `1.443m` | `0.243m` |
| `1017` | **Celina** (`mulan`) | `xingjilieren_tps.mo` | Assault / Rifle | 62 | 15 | Não | Sim | `1.034m` | `1.425m` | `0.271m` |
| `1021` | **Carmen** (`taliya`) | `jipusainvlang_tps.mo` | Stealth / Melee-SMG | 81 | 23 | Não | Sim | `1.019m` | `1.426m` | `0.244m` |
| `1022` | **Scarlett** (`xuenv`) | `xuenv_tps.mo` | Sniper / Marksman | 84 | 12 | Não | Sim | `0.987m` | `1.387m` | `0.170m` |
| `1023` | **Brigit** (`zhurong`) | `zhurong_tps.mo` | Sniper / Marksman | 71 | 13 | Não | Sim | `0.960m` | `1.338m` | `0.299m` |
| `1024` | **Shogun** (`houyi`) | `houyi_tps.mo` | Sniper / Marksman | 56 | 12 | Não | Sim | `1.019m` | `1.512m` | `0.326m` |
| `1025` | **Eva** (`linglong`) | `linglong_tps.mo` | Stealth / Melee-SMG | 54 | 11 | Não | Sim | `0.999m` | `1.390m` | `0.223m` |
| `1026` | **Dragos** (`aidehua`) | `vampire_tps.mo` | Assault / Rifle | 29 | 17 | Não | Não | — | — | — |
| `1027` | **Jack** (`shawang`) | `shawang_tps.mo` | Assault / Rifle | 58 | 17 | Não | Sim | `1.029m` | `1.575m` | `0.561m` |
| `1028` | **Loki** (`luoji`) | `luoji_tps.mo` | Sniper / Marksman | 48 | 22 | Não | Sim | `0.850m` | `1.583m` | `0.689m` |
| `1029` | **Angel** (`luolita`) | `luoji_tps.mo` | Stealth / Melee-SMG | 48 | 22 | Não | Sim | `0.850m` | `1.583m` | `0.689m` |
| `1030` | **Vincent** (`jingke`) | `luoji_tps.mo` | Assault / Rifle | 48 | 22 | Não | Sim | `0.850m` | `1.583m` | `0.689m` |
| `1031` | **Johnny** (`lanbo`) | `76_tps.mo` | Assault / Rifle | 54 | 18 | Não | Sim | `1.004m` | `1.456m` | `0.432m` |
| `1032` | **Venus (DaJi)** (`daji`) | `jipusainvlang_tps.mo` | Stealth / Melee-SMG | 81 | 23 | Não | Sim | `1.019m` | `1.426m` | `0.244m` |
| `1033` | **Lily (Lilith)** (`lilisi`) | `lilisi_tps.mo` | Stealth / Melee-SMG | 76 | 12 | Sim | Sim | `0.974m` | `1.318m` | `0.258m` |
| `1034` | **Khan (HuangZhong)** (`huangzhong`) | `huangzhong_tps.mo` | Defense / Heavy | 0 | 0 | Não | Não | — | — | — |
| `1035` | **NeZha** (`nezha`) | `nezha_tps.mo` | Assault / Rifle | 62 | 13 | Sim | Sim | `0.932m` | `1.375m` | `0.355m` |
| `1036` | **Gaya (Medusa)** (`Medusa`) | `Medusa_tps.mo` | Assault / Rifle | 0 | 0 | Não | Não | — | — | — |
| `1037` | **David (DaWei)** (`dawei`) | `dawei_tps.mo` | Defense / Heavy | 62 | 13 | Não | Não | `1.100m` | `1.615m` | `0.656m` |
| `2000` | **Lamster** (`lamster`) | `male_tps.mo` | Stealth / Melee-SMG | 3 | 5 | Não | Não | — | — | — |

---

## 4. Guia Detalhado por Herói (Todos os 24 Heróis)

### `1003` — Alice (`jvjishou_tps.mo` / `Jvjishou`)
- **Renderer Principal:** `gao_zhengchang` (`meshPathId=-2156204517402758518`, `72` ossos)
- **Renderers no Bundle:** `gao_zhengchang` (`path_id=-2156204517402758518`, 2973v/4020t, 72b), `zhong_hongxian` (`path_id=-5531228231095352660`, 2214v/1841t, 28b), `hongxian` (`path_id=-2156204517402758518`, 2973v/4020t, 72b), `di_zhengchang` (`path_id=2305910227543358753`, 622v/564t, 22b), `kakuxi_1` (`path_id=2305910227543358753`, 622v/564t, 22b), `honwai_silhouetted ` (`path_id=2305910227543358753`, 622v/564t, 22b), ... (+10 renderers)
- **Torção nos Braços:** `UpArmTwist`: **`False`** | `ForeTwist`: **`True`** (`Bip001 L ForeTwist, Bip001 R ForeTwist`)
- **Métricas Anatômicas:** Pelve `Z=1.067m` | Ombro `Z=1.416m` | Cabeça `Z=1.526m` | Envergadura `X=0.252m` | Braço Sup. `0.219m` | Antebraço `0.209m`
- **Articulações Chave (L):** `UpperArm=[-0.126, 0.0496, 1.4158]` -> `Forearm=[-0.2607, 0.0429, 1.2435]` -> `Hand=[-0.3827, -0.0369, 1.0938]` | `Pelvis=[-0.0, -0.0004, 1.067]` | `Head=[0.0, 0.0491, 1.5264]`
- **Ossos Extras (Física/Cabelo/Capa/Armas):** `Bone003`, `Bone006`, `Bone007`, `Bone005`, `Bone004`, `Bone011`, `Bone012`, `Bone013`, `Bone009`, `Bone010`, `Bone016`, `Bone017`, `Bone015`, `Bone019`, `Bone018`, `Point003`, ... (+2 ossos)
- **Diretriz Específica:** Alice (`jvjishou_tps.mo`): Sniper rig (`72` bones, `ForeTwist` only, no `UpArmTwist`). Has hair/ponytail helper bones (`Bone001`..`Bone019`). Keep sniper rifle shoulder stock zone (`R Clavicle`/`R UpperArm`) clean and avoid bulky right-chest pads.

### `1005` — Glider (`leizhenzi_tps.mo` / `LeiZhenZi`)
- **Renderer Principal:** `dazhaohon` (`meshPathId=3169982718253439877`, `54` ossos)
- **Renderers no Bundle:** `dazhaohon4` (`path_id=-3720819294990169310`, 1367v/2278t, 2b), `shenti_500` (`path_id=3826980825347068173`, 686v/587t, 21b), `di` (`path_id=3826980825347068173`, 686v/587t, 21b), `hongxian_di` (`path_id=3826980825347068173`, 686v/587t, 21b), `fuhuo_wudi_zhong` (`path_id=-2678317731008639354`, 1999v/2125t, 27b), `hongxian` (`path_id=-812111660441857929`, 3036v/3118t, 57b), ... (+11 renderers)
- **Torção nos Braços:** `UpArmTwist`: **`False`** | `ForeTwist`: **`False`** (`nenhum`)
- **Métricas Anatômicas:** Pelve `Z=0.000m` | Ombro `Z=0.000m` | Cabeça `Z=0.000m` | Envergadura `X=0.000m` | Braço Sup. `0.000m` | Antebraço `0.000m`
- **Articulações Chave (L):** `UpperArm=[0, 0, 0]` -> `Forearm=[0, 0, 0]` -> `Hand=[0, 0, 0]` | `Pelvis=[0, 0, 0]` | `Head=[0, 0, 0]`
- **Ossos Extras (Física/Cabelo/Capa/Armas):** ``, ``, ``, ``, ``, ``, ``, ``, ``, ``, ``, ``, ``, ``, ``, ``, ... (+38 ossos)
- **Diretriz Específica:** Glider (`feixing_tps.mo`): Jetpack/flight assault rig (`UpArmTwist` + `ForeTwist`). Preserve jetpack/wing attachment bones if replacing body mesh.

### `1006` — Tagaloa (`xt_tps.mo` / `XingTian`)
- **Renderer Principal:** `fuhuo_wudi` (`meshPathId=-8612445080646183882`, `36` ossos)
- **Renderers no Bundle:** `hongwai_fri` (`path_id=4948551417530887962`, 439v/506t, 16b), `zhong_hongxian` (`path_id=-9152136482510348210`, 1180v/1565t, 21b), `xingtian_shenti_2500` (`path_id=-9152136482510348210`, 1180v/1565t, 21b), `fuhuo_wudi` (`path_id=4149678304044407739`, 1641v/1452t, 18b), `di_fuhuo_wudi` (`path_id=4948551417530887962`, 439v/506t, 16b), `di_hongxian` (`path_id=1243117303247619976`, 369v/282t, 3b), ... (+16 renderers)
- **Torção nos Braços:** `UpArmTwist`: **`False`** | `ForeTwist`: **`True`** (`Bip001 L ForeTwist`)
- **Métricas Anatômicas:** Pelve `Z=1.186m` | Ombro `Z=1.851m` | Cabeça `Z=1.983m` | Envergadura `X=0.502m` | Braço Sup. `0.291m` | Antebraço `0.322m`
- **Articulações Chave (L):** `UpperArm=[-0.2511, 0.049, 1.8511]` -> `Forearm=[-0.4622, 0.0556, 1.6512]` -> `Hand=[-0.7104, -0.0656, 1.4865]` | `Pelvis=[-0.0, -0.0092, 1.1856]` | `Head=[0.0, 0.0215, 1.9834]`
- **Ossos Extras (Física/Cabelo/Capa/Armas):** Nenhum
- **Diretriz Específica:** Tagaloa (`xt_tps.mo`): Heavy tank rig (`98` bones). **CRITICAL**: `taluo_wuqi` / weapon mesh is embedded inside the TPS bundle alongside the body mesh. Never overwrite weapon renderers when replacing the body mesh.

### `1015` — Lars (`76_tps.mo` / `Terminator`)
- **Renderer Principal:** `gao` (`meshPathId=8583054048263222505`, `54` ossos)
- **Renderers no Bundle:** `zhong` (`path_id=8897686108552446754`, 1255v/1518t, 25b), `zhong_hongxian` (`path_id=8897686108552446754`, 1255v/1518t, 25b), `zhong_fuhuo_wudi` (`path_id=8897686108552446754`, 1255v/1518t, 25b), `gao` (`path_id=8583054048263222505`, 2610v/3469t, 54b), `500_shenti` (`path_id=-4373225545098368539`, 649v/730t, 19b), `jiatelin` (`path_id=3314971309117747277`, 1370v/882t, 8b), ... (+12 renderers)
- **Torção nos Braços:** `UpArmTwist`: **`False`** | `ForeTwist`: **`True`** (`Bip001 R ForeTwist1, Bip001 R ForeTwist, Bip001 L ForeTwist1`)
- **Métricas Anatômicas:** Pelve `Z=1.004m` | Ombro `Z=1.456m` | Cabeça `Z=1.616m` | Envergadura `X=0.432m` | Braço Sup. `0.294m` | Antebraço `0.252m`
- **Articulações Chave (L):** `UpperArm=[-0.2161, 0.0871, 1.456]` -> `Forearm=[-0.3818, 0.1047, 1.2132]` -> `Hand=[-0.521, 0.0738, 1.0053]` | `Pelvis=[0.0, 0.0437, 1.0038]` | `Head=[0.0, 0.0588, 1.6156]`
- **Ossos Extras (Física/Cabelo/Capa/Armas):** Nenhum
- **Diretriz Específica:** Lars (`tuling_tps.mo`): Tech/turret rig (`ForeTwist`). Multiple LOD/state renderers (`gao_zhengchang`, `zhong_zhengchang`, `di_zhengchang`, `fuhuo_wudi`) must have their `m_Mesh`, `m_Bones`, and `m_Materials` updated together.

### `1016` — Valkyrie (`waerjili_tps.mo` / `Valkyries`)
- **Renderer Principal:** `hongxian` (`meshPathId=8740939414059929440`, `72` ossos)
- **Renderers no Bundle:** `hongxian` (`path_id=8740939414059929440`, 2633v/3473t, 72b), `waerjili_shenti_2500` (`path_id=-8482005214222911495`, 1710v/1968t, 25b), `zhong_hongxian` (`path_id=-8482005214222911495`, 1710v/1968t, 25b), `zhong_fuhuo_wudi` (`path_id=-8482005214222911495`, 1710v/1968t, 25b), `di_hongxian` (`path_id=5102470105445538803`, 587v/518t, 19b), `honwai_silhouetted ` (`path_id=5102470105445538803`, 587v/518t, 19b), ... (+5 renderers)
- **Torção nos Braços:** `UpArmTwist`: **`False`** | `ForeTwist`: **`True`** (`Bip001 R ForeTwist, Bip001 L ForeTwist`)
- **Métricas Anatômicas:** Pelve `Z=1.044m` | Ombro `Z=1.443m` | Cabeça `Z=1.564m` | Envergadura `X=0.243m` | Braço Sup. `0.224m` | Antebraço `0.257m`
- **Articulações Chave (L):** `UpperArm=[-0.1215, 0.0376, 1.4431]` -> `Forearm=[-0.2896, 0.0439, 1.2946]` -> `Hand=[-0.48, -0.001, 1.1284]` | `Pelvis=[-0.0, -0.0303, 1.0439]` | `Head=[0.0, 0.0194, 1.5641]`
- **Ossos Extras (Física/Cabelo/Capa/Armas):** `Bone014`, `Bone018`, `Bone013`, `Bone014(mirrored)`, `Bone018(mirrored)`, `Bone013(mirrored)`, `Bone015`, `Bone015(mirrored)`, `Bone012`, `Bone009`, `Bone001`, `Bone002`, `Bone003`, `Bone007`, `Bone004`, `Bone006`, ... (+2 ossos)
- **Diretriz Específica:** Valkyrie (`nv_tps.mo`): Dual-submesh winged/valkyrie rig with extensive cloth/skirt/hair helper bones. Ideal candidate for `DynamicBone` skirt/cape chains parented to `Bip001 Pelvis`.

### `1017` — Celina (`xingjilieren_tps.mo` / `XingJiLieRen`)
- **Renderer Principal:** `shenti` (`meshPathId=5244958689840648268`, `62` ossos)
- **Renderers no Bundle:** `mulan_tps_R_qiang` (`path_id=-2325301838246981905`, 2352v/1568t, 7b), `shenti` (`path_id=5244958689840648268`, 2908v/3627t, 62b), `di_fuhuo_wudi` (`path_id=3483829446691016553`, 570v/595t, 23b), `jian_mulan_tps_500` (`path_id=3483829446691016553`, 570v/595t, 23b), `shanxian` (`path_id=3483829446691016553`, 570v/595t, 23b), `hongwai_fri` (`path_id=3483829446691016553`, 570v/595t, 23b), ... (+9 renderers)
- **Torção nos Braços:** `UpArmTwist`: **`False`** | `ForeTwist`: **`True`** (`Bip001 L ForeTwist, Bip001 R ForeTwist`)
- **Métricas Anatômicas:** Pelve `Z=1.034m` | Ombro `Z=1.425m` | Cabeça `Z=1.555m` | Envergadura `X=0.271m` | Braço Sup. `0.000m` | Antebraço `0.000m`
- **Articulações Chave (L):** `UpperArm=[-0.1354, 0.01, 1.4254]` -> `Forearm=[0, 0, 0]` -> `Hand=[-0.3824, -0.1609, 1.098]` | `Pelvis=[0.0, -0.0369, 1.034]` | `Head=[0.0, 0.0033, 1.5547]`
- **Ossos Extras (Física/Cabelo/Capa/Armas):** `Bone019`, `Bone013`, `Bone009`, `Bone017`, `Bone015`, `Bone001`, `Bone004`, `Bone003`, `Bone006`, `Bone007`
- **Diretriz Específica:** Celina (`babi_tps.mo`): Agile female rig (`UpArmTwist` + `ForeTwist`). Slim shoulder span; always measure source arm centroids automatically (`measure_arm_landmarks`).

### `1021` — Carmen (`jipusainvlang_tps.mo` / `Jipusai`)
- **Renderer Principal:** `fuhuo_wudi` (`meshPathId=2843771506193642324`, `81` ossos)
- **Renderers no Bundle:** `st_zhong` (`path_id=4866008246306770827`, 1439v/1712t, 44b), `st_di_fuhuo_wudi` (`path_id=1647511339973746218`, 487v/450t, 18b), `st_zhong_fuhuo_wudi` (`path_id=4866008246306770827`, 1439v/1712t, 44b), `honwai_silhouetted ` (`path_id=1647511339973746218`, 487v/450t, 18b), `honwai_silhouetted ` (`path_id=8248768182824305329`, 57v/64t, 2b), `fuhuo_wudi` (`path_id=2843771506193642324`, 3486v/4587t, 81b), ... (+17 renderers)
- **Torção nos Braços:** `UpArmTwist`: **`False`** | `ForeTwist`: **`True`** (`Bip001 L ForeTwist1, Bip001 L ForeTwist, Bip001 R ForeTwist1, Bip001 R ForeTwist`)
- **Métricas Anatômicas:** Pelve `Z=1.019m` | Ombro `Z=1.426m` | Cabeça `Z=1.538m` | Envergadura `X=0.244m` | Braço Sup. `0.000m` | Antebraço `0.000m`
- **Articulações Chave (L):** `UpperArm=[-0.1218, 0.048, 1.4262]` -> `Forearm=[0, 0, 0]` -> `Hand=[-0.4712, -0.0108, 1.0944]` | `Pelvis=[-0.0, -0.0175, 1.019]` | `Head=[0.0, 0.0109, 1.5378]`
- **Ossos Extras (Física/Cabelo/Capa/Armas):** `Bone033`, `Bone034`, `Bone038`, `Bone035`, `Bone030`, `Bone041`, `Bone029`, `Bone040`, `Bone037`, `Bone039`, `Bone011`, `Bone028`, `Bone031`, `Bone042`, `Bone043`, `Bone045`, ... (+11 ossos)
- **Diretriz Específica:** Carmen (`kamei_tps.mo`): Female caster/marksman rig (`UpArmTwist` + `ForeTwist`). Check waist/hip Z-interpolation knots carefully.

### `1022` — Scarlett (`xuenv_tps.mo` / `YukiOnna`)
- **Renderer Principal:** `fuhuo_wudi` (`meshPathId=3871185905290990773`, `84` ossos)
- **Renderers no Bundle:** `hongwai_fri` (`path_id=-3778504882761922069`, 569v/502t, 24b), `di_hongxian` (`path_id=-3778504882761922069`, 569v/502t, 24b), `qiangshen` (`path_id=8892121545115576622`, 1901v/1559t, 4b), `fuhuo_wudi` (`path_id=3871185905290990773`, 3057v/3709t, 84b), `zhong_hongxian` (`path_id=-3809769049413947693`, 1282v/1290t, 40b), `honwai_silhouetted ` (`path_id=-3778504882761922069`, 569v/502t, 24b), ... (+6 renderers)
- **Torção nos Braços:** `UpArmTwist`: **`False`** | `ForeTwist`: **`True`** (`Bip001 L ForeTwist, Bip001 R ForeTwist`)
- **Métricas Anatômicas:** Pelve `Z=0.987m` | Ombro `Z=1.387m` | Cabeça `Z=1.483m` | Envergadura `X=0.170m` | Braço Sup. `0.289m` | Antebraço `0.237m`
- **Articulações Chave (L):** `UpperArm=[-0.0851, 0.0329, 1.3868]` -> `Forearm=[-0.3288, 0.0325, 1.2322]` -> `Hand=[-0.5257, -0.0014, 1.104]` | `Pelvis=[-0.0, -0.026, 0.9869]` | `Head=[0.0, 0.0064, 1.4827]`
- **Ossos Extras (Física/Cabelo/Capa/Armas):** `Bone012`, `Bone027`, `Bone014`, `Bone015`, `Bone006`, `Bone007`, `Bone008`, `Bone006(mirrored)`, `Bone007(mirrored)`, `Bone008(mirrored)`, `Bone026(mirrored)`, `Bone026`, `Bone001(mirrored)`, `Bone002(mirrored)`, `Bone003(mirrored)`, `Bone004(mirrored)`, ... (+16 ossos)
- **Diretriz Específica:** Scarlett (`sikalei_tps.mo`): Stealth/assassin rig (`UpArmTwist` + `ForeTwist`). Fast spin/roll animations require clean longitudinal twist gradients.

### `1023` — Brigit (`zhurong_tps.mo` / `Zhurong`)
- **Renderer Principal:** `hongxian` (`meshPathId=2850531138092633242`, `71` ossos)
- **Renderers no Bundle:** `zr_shenti_2500` (`path_id=677933975846846559`, 1673v/1724t, 28b), `hongxian` (`path_id=2850531138092633242`, 3751v/4467t, 71b), `fuhuo_wudi` (`path_id=2850531138092633242`, 3751v/4467t, 71b), `zhurong_shenti` (`path_id=2850531138092633242`, 3751v/4467t, 71b), `hongwai_fri` (`path_id=-3553445517087851969`, 521v/465t, 19b), `hongxian_zhong` (`path_id=677933975846846559`, 1673v/1724t, 28b), ... (+7 renderers)
- **Torção nos Braços:** `UpArmTwist`: **`False`** | `ForeTwist`: **`True`** (`Bip001 R ForeTwist, Bip001 L ForeTwist`)
- **Métricas Anatômicas:** Pelve `Z=0.960m` | Ombro `Z=1.338m` | Cabeça `Z=1.482m` | Envergadura `X=0.299m` | Braço Sup. `0.000m` | Antebraço `0.000m`
- **Articulações Chave (L):** `UpperArm=[-0.1497, 0.0786, 1.3383]` -> `Forearm=[0, 0, 0]` -> `Hand=[-0.4477, 0.0489, 1.0417]` | `Pelvis=[0.0, 0.018, 0.9603]` | `Head=[0.0, 0.0581, 1.4823]`
- **Ossos Extras (Física/Cabelo/Capa/Armas):** `Bone006`, `Bone021`, `Bone009`, `Bone008`, `Bone023`, `Bone022`, `Bone019`, `Bone020`, `Bone018`, `Bone017`, `Bone011`, `Bone016`, `Bone014`, `Bone013`, `Bone012`, `Bone015`, ... (+3 ossos)
- **Diretriz Específica:** Brigit (`bulijite_tps.mo`): Heavy support/shield rig (`UpArmTwist` + `ForeTwist`).

### `1024` — Shogun (`houyi_tps.mo` / `HouYi`)
- **Renderer Principal:** `hongxian` (`meshPathId=-4379871602250762842`, `56` ossos)
- **Renderers no Bundle:** `hongxian` (`path_id=-4379871602250762842`, 6090v/8520t, 56b), `gong_mod` (`path_id=7877495169409509238`, 5885v/4024t, 6b), `2500_hongxian` (`path_id=-6582764892968356270`, 1270v/1434t, 25b), `gongjianshou_mod` (`path_id=-4379871602250762842`, 6090v/8520t, 56b), `170301hyjc_shenti_500` (`path_id=6359143567409260530`, 483v/483t, 25b), `fuhuo_wudi` (`path_id=-4379871602250762842`, 6090v/8520t, 56b), ... (+6 renderers)
- **Torção nos Braços:** `UpArmTwist`: **`False`** | `ForeTwist`: **`True`** (`Bip001 L ForeTwist, Bip001 R ForeTwist`)
- **Métricas Anatômicas:** Pelve `Z=1.019m` | Ombro `Z=1.512m` | Cabeça `Z=1.621m` | Envergadura `X=0.326m` | Braço Sup. `0.265m` | Antebraço `0.226m`
- **Articulações Chave (L):** `UpperArm=[-0.1632, 0.0369, 1.5121]` -> `Forearm=[-0.2883, 0.0132, 1.28]` -> `Hand=[-0.3939, -0.0475, 1.0899]` | `Pelvis=[0.0, -0.0259, 1.0194]` | `Head=[0.0, 0.0066, 1.6213]`
- **Ossos Extras (Física/Cabelo/Capa/Armas):** `Bone005`, `Bone006`
- **Diretriz Específica:** Shogun (`jiangjun_tps.mo`): Armored melee samurai rig (`75`+ bones). Has tasset/pauldron helper bones (`Bone001`..`Bone018`). Extreme upper-body twist and shoulder elevation during katana slash animations — always use full-radius cylindrical shoulder sockets and `Clavicle` cushioning on pauldrons (see `scratch/shogun040`).

### `1025` — Eva (`linglong_tps.mo` / `LingLong`)
- **Renderer Principal:** `LOD_0_shenti` (`meshPathId=4263895525124096782`, `54` ossos)
- **Renderers no Bundle:** `LOD_2500_shenti` (`path_id=-120489982989183397`, 2004v/2026t, 19b), `di_hongxian` (`path_id=-3129833646021474542`, 523v/504t, 20b), `hongwai_fri` (`path_id=-3129833646021474542`, 523v/504t, 20b), `zhong_hongxian` (`path_id=-120489982989183397`, 2004v/2026t, 19b), `LOD_0_shenti` (`path_id=4263895525124096782`, 3972v/4462t, 54b), `di_fuhuo_wudi` (`path_id=-3129833646021474542`, 523v/504t, 20b), ... (+5 renderers)
- **Torção nos Braços:** `UpArmTwist`: **`False`** | `ForeTwist`: **`True`** (`Bip001 L ForeTwist, Bip001 R ForeTwist`)
- **Métricas Anatômicas:** Pelve `Z=0.999m` | Ombro `Z=1.390m` | Cabeça `Z=1.503m` | Envergadura `X=0.223m` | Braço Sup. `0.000m` | Antebraço `0.000m`
- **Articulações Chave (L):** `UpperArm=[-0.1115, -0.0177, 1.3899]` -> `Forearm=[0, 0, 0]` -> `Hand=[-0.3141, -0.0411, 0.9942]` | `Pelvis=[0.0, -0.0812, 0.9994]` | `Head=[0.0, -0.047, 1.5028]`
- **Ossos Extras (Física/Cabelo/Capa/Armas):** `Bone001(mirrored)`, `Bone001`
- **Diretriz Específica:** Eva (`yiwa_tps.mo`): Agile female rig (`UpArmTwist` + `ForeTwist`).

### `1026` — Dragos (`vampire_tps.mo` / `Vampire`)
- **Renderer Principal:** `Bat_Skin` (`meshPathId=-2101445641561532781`, `29` ossos)
- **Renderers no Bundle:** `Bat_Skin` (`path_id=-2101445641561532781`, 3844v/6220t, 29b), `hongxian` (`path_id=-2101445641561532781`, 3844v/6220t, 29b), `fuhuo_wudi` (`path_id=-7609149127213735202`, 2913v/4299t, 90b), `hongwai_fri` (`path_id=6385994820080081972`, 623v/647t, 29b), `honwai_silhouetted` (`path_id=6385994820080081972`, 623v/647t, 29b), `aidehua001_jian` (`path_id=-7609149127213735202`, 2913v/4299t, 90b), ... (+11 renderers)
- **Torção nos Braços:** `UpArmTwist`: **`False`** | `ForeTwist`: **`False`** (`nenhum`)
- **Métricas Anatômicas:** Pelve `Z=0.000m` | Ombro `Z=0.000m` | Cabeça `Z=0.000m` | Envergadura `X=0.000m` | Braço Sup. `0.000m` | Antebraço `0.000m`
- **Articulações Chave (L):** `UpperArm=[0, 0, 0]` -> `Forearm=[0, 0, 0]` -> `Hand=[0, 0, 0]` | `Pelvis=[0, 0, 0]` | `Head=[0, 0, 0]`
- **Ossos Extras (Física/Cabelo/Capa/Armas):** `Bone005`, `Bone007`, `Bone004`, `Bone017(mirrored)`, `Bone003`, `Bone018`, `Bone002`, `Bone009(mirrored)`, `Bone009`, `Bone010(mirrored)`, `Bone010`, `Bone017`, `Bone024`, `Bone019`, `Bone022`, `Bone020`, ... (+13 ossos)
- **Diretriz Específica:** Dragos (`delagusi_tps.mo`): Tall heavy proportions. Verify shoulder span (`shoulderSpan`) and arm length scaling.

### `1027` — Jack (`shawang_tps.mo` / `Shawang`)
- **Renderer Principal:** `fuhuo_wudi` (`meshPathId=-4964938224233599180`, `58` ossos)
- **Renderers no Bundle:** `zhong_fuhuo_wudi` (`path_id=-2048669288817816991`, 828v/850t, 20b), `sw_shenti_500` (`path_id=7753821235379253286`, 798v/810t, 20b), `hongwai_fri` (`path_id=7753821235379253286`, 798v/810t, 20b), `di_fuhuo_wudi` (`path_id=7753821235379253286`, 798v/810t, 20b), `sw_shenti_2500` (`path_id=-2048669288817816991`, 828v/850t, 20b), `fuhuo_wudi` (`path_id=-4964938224233599180`, 4128v/4752t, 58b), ... (+11 renderers)
- **Torção nos Braços:** `UpArmTwist`: **`False`** | `ForeTwist`: **`True`** (`Bip001 R ForeTwist, Bip001 L ForeTwist`)
- **Métricas Anatômicas:** Pelve `Z=1.029m` | Ombro `Z=1.575m` | Cabeça `Z=1.752m` | Envergadura `X=0.561m` | Braço Sup. `0.306m` | Antebraço `0.232m`
- **Articulações Chave (L):** `UpperArm=[-0.2803, 0.072, 1.5754]` -> `Forearm=[-0.436, 0.0884, 1.3128]` -> `Hand=[-0.5523, 0.0091, 1.1282]` | `Pelvis=[0.0, -0.0114, 1.0293]` | `Head=[0.0, 0.0272, 1.752]`
- **Ossos Extras (Física/Cabelo/Capa/Armas):** `jianjia_001`, `Bone_zui`, `R_yanpi`, `L_yanpi`
- **Diretriz Específica:** Jack (`jieke_tps.mo`): Standard male assault proportions (`UpArmTwist` + `ForeTwist`).

### `1028` — Loki (`luoji_tps.mo` / `LuoJi`)
- **Renderer Principal:** `fuhuo_wudi` (`meshPathId=-2969532686483608554`, `48` ossos)
- **Renderers no Bundle:** `fuhuo_wudi_di` (`path_id=-6253671820892971597`, 770v/945t, 19b), `500_luoji_body` (`path_id=-6253671820892971597`, 770v/945t, 19b), `fuhuo_wudi` (`path_id=-2329130734746905305`, 527v/740t, 16b), `2500_luoj_shou` (`path_id=3675193943887156995`, 406v/560t, 6b), `500_luoj_shou` (`path_id=8438130373633644003`, 231v/264t, 1b), `2500_luoji_body` (`path_id=8294473766367524794`, 1693v/2245t, 24b), ... (+16 renderers)
- **Torção nos Braços:** `UpArmTwist`: **`False`** | `ForeTwist`: **`True`** (`Bip001 L ForeTwist, Bip001 R ForeTwist`)
- **Métricas Anatômicas:** Pelve `Z=0.850m` | Ombro `Z=1.583m` | Cabeça `Z=1.757m` | Envergadura `X=0.689m` | Braço Sup. `0.410m` | Antebraço `0.318m`
- **Articulações Chave (L):** `UpperArm=[-0.3446, 0.1314, 1.5828]` -> `Forearm=[-0.6137, 0.1064, 1.2748]` -> `Hand=[-0.8217, 0.0739, 1.0368]` | `Pelvis=[0.0, 0.0, 0.8498]` | `Head=[0.0, 0.1112, 1.757]`
- **Ossos Extras (Física/Cabelo/Capa/Armas):** `Bone004(mirrored)`, `Bone004`, `Bone006`, `Bone002(mirrored)`, `Bone002`, `Bone003`, `Bone003(mirrored)`, `Bone008`, `Bone008(mirrored)`, `Bone010`
- **Diretriz Específica:** Loki (`luoji_tps.mo`): Trickster/mage rig with cape/coat helper bones.

### `1029` — Angel (`luoji_tps.mo` / `LuoLi`)
- **Renderer Principal:** `fuhuo_wudi` (`meshPathId=-2969532686483608554`, `48` ossos)
- **Renderers no Bundle:** `fuhuo_wudi_di` (`path_id=-6253671820892971597`, 770v/945t, 19b), `500_luoji_body` (`path_id=-6253671820892971597`, 770v/945t, 19b), `fuhuo_wudi` (`path_id=-2329130734746905305`, 527v/740t, 16b), `2500_luoj_shou` (`path_id=3675193943887156995`, 406v/560t, 6b), `500_luoj_shou` (`path_id=8438130373633644003`, 231v/264t, 1b), `2500_luoji_body` (`path_id=8294473766367524794`, 1693v/2245t, 24b), ... (+16 renderers)
- **Torção nos Braços:** `UpArmTwist`: **`False`** | `ForeTwist`: **`True`** (`Bip001 L ForeTwist, Bip001 R ForeTwist`)
- **Métricas Anatômicas:** Pelve `Z=0.850m` | Ombro `Z=1.583m` | Cabeça `Z=1.757m` | Envergadura `X=0.689m` | Braço Sup. `0.410m` | Antebraço `0.318m`
- **Articulações Chave (L):** `UpperArm=[-0.3446, 0.1314, 1.5828]` -> `Forearm=[-0.6137, 0.1064, 1.2748]` -> `Hand=[-0.8217, 0.0739, 1.0368]` | `Pelvis=[0.0, 0.0, 0.8498]` | `Head=[0.0, 0.1112, 1.757]`
- **Ossos Extras (Física/Cabelo/Capa/Armas):** `Bone004(mirrored)`, `Bone004`, `Bone006`, `Bone002(mirrored)`, `Bone002`, `Bone003`, `Bone003(mirrored)`, `Bone008`, `Bone008(mirrored)`, `Bone010`
- **Diretriz Específica:** Angel (`tianshi_tps.mo`): Winged/healer rig with wing/skirt bones. Preserve wing renderer/bones when adapting body skins.

### `1030` — Vincent (`luoji_tps.mo` / `JingKe`)
- **Renderer Principal:** `fuhuo_wudi` (`meshPathId=-2969532686483608554`, `48` ossos)
- **Renderers no Bundle:** `fuhuo_wudi_di` (`path_id=-6253671820892971597`, 770v/945t, 19b), `500_luoji_body` (`path_id=-6253671820892971597`, 770v/945t, 19b), `fuhuo_wudi` (`path_id=-2329130734746905305`, 527v/740t, 16b), `2500_luoj_shou` (`path_id=3675193943887156995`, 406v/560t, 6b), `500_luoj_shou` (`path_id=8438130373633644003`, 231v/264t, 1b), `2500_luoji_body` (`path_id=8294473766367524794`, 1693v/2245t, 24b), ... (+16 renderers)
- **Torção nos Braços:** `UpArmTwist`: **`False`** | `ForeTwist`: **`True`** (`Bip001 L ForeTwist, Bip001 R ForeTwist`)
- **Métricas Anatômicas:** Pelve `Z=0.850m` | Ombro `Z=1.583m` | Cabeça `Z=1.757m` | Envergadura `X=0.689m` | Braço Sup. `0.410m` | Antebraço `0.318m`
- **Articulações Chave (L):** `UpperArm=[-0.3446, 0.1314, 1.5828]` -> `Forearm=[-0.6137, 0.1064, 1.2748]` -> `Hand=[-0.8217, 0.0739, 1.0368]` | `Pelvis=[0.0, 0.0, 0.8498]` | `Head=[0.0, 0.1112, 1.757]`
- **Ossos Extras (Física/Cabelo/Capa/Armas):** `Bone004(mirrored)`, `Bone004`, `Bone006`, `Bone002(mirrored)`, `Bone002`, `Bone003`, `Bone003(mirrored)`, `Bone008`, `Bone008(mirrored)`, `Bone010`
- **Diretriz Específica:** Vincent (`wensente_tps.mo`): Male marksman/coat rig (`UpArmTwist` + `ForeTwist`).

### `1031` — Johnny (`76_tps.mo` / `_Rambo_`)
- **Renderer Principal:** `gao` (`meshPathId=8583054048263222505`, `54` ossos)
- **Renderers no Bundle:** `zhong` (`path_id=8897686108552446754`, 1255v/1518t, 25b), `zhong_hongxian` (`path_id=8897686108552446754`, 1255v/1518t, 25b), `zhong_fuhuo_wudi` (`path_id=8897686108552446754`, 1255v/1518t, 25b), `gao` (`path_id=8583054048263222505`, 2610v/3469t, 54b), `500_shenti` (`path_id=-4373225545098368539`, 649v/730t, 19b), `jiatelin` (`path_id=3314971309117747277`, 1370v/882t, 8b), ... (+12 renderers)
- **Torção nos Braços:** `UpArmTwist`: **`False`** | `ForeTwist`: **`True`** (`Bip001 R ForeTwist1, Bip001 R ForeTwist, Bip001 L ForeTwist1`)
- **Métricas Anatômicas:** Pelve `Z=1.004m` | Ombro `Z=1.456m` | Cabeça `Z=1.616m` | Envergadura `X=0.432m` | Braço Sup. `0.294m` | Antebraço `0.252m`
- **Articulações Chave (L):** `UpperArm=[-0.2161, 0.0871, 1.456]` -> `Forearm=[-0.3818, 0.1047, 1.2132]` -> `Hand=[-0.521, 0.0738, 1.0053]` | `Pelvis=[0.0, 0.0437, 1.0038]` | `Head=[0.0, 0.0588, 1.6156]`
- **Ossos Extras (Física/Cabelo/Capa/Armas):** Nenhum
- **Diretriz Específica:** Johnny (`qiangni_tps.mo`): Rocker/guitarist rig (`UpArmTwist` + `ForeTwist`). Guitar/rifle grip poses (`scratch/johnny034/grip.py`) require accurate `L/R Hand` and finger weight transfer.

### `1032` — Venus (DaJi) (`jipusainvlang_tps.mo` / `DaJi`)
- **Renderer Principal:** `fuhuo_wudi` (`meshPathId=2843771506193642324`, `81` ossos)
- **Renderers no Bundle:** `st_zhong` (`path_id=4866008246306770827`, 1439v/1712t, 44b), `st_di_fuhuo_wudi` (`path_id=1647511339973746218`, 487v/450t, 18b), `st_zhong_fuhuo_wudi` (`path_id=4866008246306770827`, 1439v/1712t, 44b), `honwai_silhouetted ` (`path_id=1647511339973746218`, 487v/450t, 18b), `honwai_silhouetted ` (`path_id=8248768182824305329`, 57v/64t, 2b), `fuhuo_wudi` (`path_id=2843771506193642324`, 3486v/4587t, 81b), ... (+17 renderers)
- **Torção nos Braços:** `UpArmTwist`: **`False`** | `ForeTwist`: **`True`** (`Bip001 L ForeTwist1, Bip001 L ForeTwist, Bip001 R ForeTwist1, Bip001 R ForeTwist`)
- **Métricas Anatômicas:** Pelve `Z=1.019m` | Ombro `Z=1.426m` | Cabeça `Z=1.538m` | Envergadura `X=0.244m` | Braço Sup. `0.000m` | Antebraço `0.000m`
- **Articulações Chave (L):** `UpperArm=[-0.1218, 0.048, 1.4262]` -> `Forearm=[0, 0, 0]` -> `Hand=[-0.4712, -0.0108, 1.0944]` | `Pelvis=[-0.0, -0.0175, 1.019]` | `Head=[0.0, 0.0109, 1.5378]`
- **Ossos Extras (Física/Cabelo/Capa/Armas):** `Bone033`, `Bone034`, `Bone038`, `Bone035`, `Bone030`, `Bone041`, `Bone029`, `Bone040`, `Bone037`, `Bone039`, `Bone011`, `Bone028`, `Bone031`, `Bone042`, `Bone043`, `Bone045`, ... (+11 ossos)
- **Diretriz Específica:** Venus / DaJi (`daji_tps.mo`): Fox-spirit/tail rig (`UpArmTwist` + `ForeTwist`) with tail/skirt helper bones.

### `1033` — Lily (Lilith) (`lilisi_tps.mo` / `Lilisi`)
- **Renderer Principal:** `hongxian` (`meshPathId=-7280344775147488012`, `76` ossos)
- **Renderers no Bundle:** `honwai_silhouetted ` (`path_id=4739482402592737565`, 1025v/1141t, 19b), `di_fuhuo_wudi` (`path_id=4739482402592737565`, 1025v/1141t, 19b), `hongxian` (`path_id=-7280344775147488012`, 3385v/4902t, 76b), `hongwai_fri` (`path_id=4739482402592737565`, 1025v/1141t, 19b), `zhong_hongxian` (`path_id=-6825634022250907522`, 1863v/2371t, 38b), `di_hongxian` (`path_id=4739482402592737565`, 1025v/1141t, 19b), ... (+6 renderers)
- **Torção nos Braços:** `UpArmTwist`: **`True`** | `ForeTwist`: **`True`** (`Bip001 RUpArmTwist, Bip001 R ForeTwist, Bip001 LUpArmTwist, Bip001 L ForeTwist`)
- **Métricas Anatômicas:** Pelve `Z=0.974m` | Ombro `Z=1.318m` | Cabeça `Z=1.439m` | Envergadura `X=0.258m` | Braço Sup. `0.245m` | Antebraço `0.194m`
- **Articulações Chave (L):** `UpperArm=[-0.1288, 0.0646, 1.3176]` -> `Forearm=[-0.2302, 0.0657, 1.0948]` -> `Hand=[-0.3173, 0.0363, 0.9243]` | `Pelvis=[-0.0, -0.0073, 0.9737]` | `Head=[0.0, 0.0278, 1.4387]`
- **Ossos Extras (Física/Cabelo/Capa/Armas):** `Bone011`, `Bone011(mirrored)`, `Bone001(mirrored)`, `Bone001`, `Bone005(mirrored)`, `Bone005`, `Bone009`, `Bone009(mirrored)`, `Bone007`, `Bone006`, `Bone010`, `Bone010(mirrored)`, `Bone006(mirrored)`, `Bone007(mirrored)`, `Bone008(mirrored)`, `Bone008`, ... (+4 ossos)
- **Diretriz Específica:** Lily / Lilith (`lilisi_tps.mo`): Gothic/witch vampire rig (`76` native bones + `UpArmTwist` + `ForeTwist`, `3` body LOD renderers `-7280344775147488012`, `-6825634022250907522`, `4739482402592737565`). Rest pose `stand` and `R_fire` bring both elbows forward across the chest (`~43°–64°`) while `expression_A50` and `expression_D200` raise arms `80°–116°` overhead. Short bell sleeves must be tailored coaxially around `UpperArm -> Forearm` (`tailor_bell_sleeve`) and synchronized with `UpperArm` weights so the black arms neither poke through nor flip the sleeves into giant bat-wings (`scratch/lily068`).

### `1034` — Khan (HuangZhong) (`huangzhong_tps.mo` / `HuangZhong`)
- **Renderer Principal:** `` (`meshPathId=`, `0` ossos)
- **Renderers no Bundle:** 
- **Torção nos Braços:** `UpArmTwist`: **`False`** | `ForeTwist`: **`False`** (`nenhum`)
- **Métricas Anatômicas:** Pelve `Z=0.000m` | Ombro `Z=0.000m` | Cabeça `Z=0.000m` | Envergadura `X=0.000m` | Braço Sup. `0.000m` | Antebraço `0.000m`
- **Articulações Chave (L):** `UpperArm=[0, 0, 0]` -> `Forearm=[0, 0, 0]` -> `Hand=[0, 0, 0]` | `Pelvis=[0, 0, 0]` | `Head=[0, 0, 0]`
- **Ossos Extras (Física/Cabelo/Capa/Armas):** Nenhum
- **Diretriz Específica:** Khan / HuangZhong (`hz_tps.mo`): Heavy bow/cannon veteran rig. Broad shoulders and bow-draw poses.

### `1035` — NeZha (`nezha_tps.mo` / `NeZha`)
- **Renderer Principal:** `polySurface10` (`meshPathId=3783654109525315982`, `62` ossos)
- **Renderers no Bundle:** `fuhuo_wudi_di` (`path_id=-724283937033435013`, 702v/708t, 20b), `polySurface10` (`path_id=7060996215422688193`, 951v/1376t, 8b), `polySurface10` (`path_id=3783654109525315982`, 3397v/4724t, 62b), `polySurface10` (`path_id=7060996215422688193`, 951v/1376t, 8b), `hongxian_zhong` (`path_id=-2044725380077455383`, 1743v/2067t, 31b), `hongxian` (`path_id=3783654109525315982`, 3397v/4724t, 62b), ... (+7 renderers)
- **Torção nos Braços:** `UpArmTwist`: **`True`** | `ForeTwist`: **`True`** (`Bip001 R ForeTwist, Bip001 L ForeTwist, Bip001 RUpArmTwist, Bip001 LUpArmTwist`)
- **Métricas Anatômicas:** Pelve `Z=0.932m` | Ombro `Z=1.375m` | Cabeça `Z=1.534m` | Envergadura `X=0.355m` | Braço Sup. `0.227m` | Antebraço `0.251m`
- **Articulações Chave (L):** `UpperArm=[-0.1775, 0.0258, 1.3752]` -> `Forearm=[-0.2844, 0.0098, 1.1753]` -> `Hand=[-0.4056, -0.0722, 0.9711]` | `Pelvis=[0.0, -0.0, 0.9319]` | `Head=[0.0, 0.022, 1.5341]`
- **Ossos Extras (Física/Cabelo/Capa/Armas):** `Bone002`, `Bone003`, `Bone004`, `Bone005`, `Bone006`, `Bone007`
- **Diretriz Específica:** NeZha (`nezha_tps.mo`): **MULTI-RENDERER SPECIAL**: Has `5` SkinnedMeshRenderers including `4` extra Ultimate arms (`nezha_ult_hand` x4) + sash/spear meshes. Only replace the main body mesh (`1557941023447926386`) and leave the 4 Ultimate arm renderers intact (`scratch/nezha029`).

### `1036` — Gaya (Medusa) (`Medusa_tps.mo` / `Medusa`)
- **Renderer Principal:** `` (`meshPathId=`, `0` ossos)
- **Renderers no Bundle:** 
- **Torção nos Braços:** `UpArmTwist`: **`False`** | `ForeTwist`: **`False`** (`nenhum`)
- **Métricas Anatômicas:** Pelve `Z=0.000m` | Ombro `Z=0.000m` | Cabeça `Z=0.000m` | Envergadura `X=0.000m` | Braço Sup. `0.000m` | Antebraço `0.000m`
- **Articulações Chave (L):** `UpperArm=[0, 0, 0]` -> `Forearm=[0, 0, 0]` -> `Hand=[0, 0, 0]` | `Pelvis=[0, 0, 0]` | `Head=[0, 0, 0]`
- **Ossos Extras (Física/Cabelo/Capa/Armas):** Nenhum
- **Diretriz Específica:** Gaya / Medusa (`medusa_tps.mo`): Serpent-hair/tail female rig (`UpArmTwist` + `ForeTwist`) with snake-hair helper bones.

### `1037` — David (DaWei) (`dawei_tps.mo` / `DaWei`)
- **Renderer Principal:** `dawei_tps` (`meshPathId=7974980097542005084`, `62` ossos)
- **Renderers no Bundle:** `dawei_tps` (`path_id=7974980097542005084`, 7100v/7960t, 62b), `hongxian` (`path_id=7974980097542005084`, 7100v/7960t, 62b), `daweiwuqi_4500` (`path_id=2221897446212992416`, 2769v/1994t, 20b), `fuhuo_wudi` (`path_id=7974980097542005084`, 7100v/7960t, 62b), `daweiwuqi_2500` (`path_id=9117325694907160839`, 1641v/1030t, 8b), `zhong_hongxian` (`path_id=-7410381753614986528`, 3458v/2883t, 37b), ... (+7 renderers)
- **Torção nos Braços:** `UpArmTwist`: **`False`** | `ForeTwist`: **`False`** (`nenhum`)
- **Métricas Anatômicas:** Pelve `Z=1.100m` | Ombro `Z=1.615m` | Cabeça `Z=1.753m` | Envergadura `X=0.656m` | Braço Sup. `0.322m` | Antebraço `0.305m`
- **Articulações Chave (L):** `UpperArm=[-0.3282, 0.0029, 1.6154]` -> `Forearm=[-0.4775, 0.0359, 1.3318]` -> `Hand=[-0.6084, -0.0751, 1.0794]` | `Pelvis=[0.0, 0.0029, 1.1001]` | `Head=[-0.0, 0.0236, 1.7527]`
- **Ossos Extras (Física/Cabelo/Capa/Armas):** `Bone008`, `Bone008(mirrored)`, `Bone001(mirrored)`, `Bone003(mirrored)`, `Bone001`, `Bone003`, `Bone007(mirrored)`, `Bone005(mirrored)`, `Bone007`, `Bone005`
- **Diretriz Específica:** David / DaWei (`dawei_tps.mo`): **BUILT-IN BOW/QUIVER RENDERERS**: Bundle contains separate renderers for `Gong_skin` (bow), `JianDai_skin` (quiver), `Jian_skin` (arrow), and `TouFa_skin` (hair) alongside `dawei_skin` (body). Only replace `dawei_skin` (and `TouFa_skin` if head/hair is included).

### `2000` — Lamster (`male_tps.mo` / `LuoMo`)
- **Renderer Principal:** `male_head000` (`meshPathId=5954130870577656471`, `3` ossos)
- **Renderers no Bundle:** `male_body000` (`path_id=4264983615067625941`, 667v/1084t, 14b), `male_leg000` (`path_id=-7155198427025677249`, 822v/1242t, 6b), `male_foot000` (`path_id=-1642157611226703421`, 293v/418t, 6b), `male_head000` (`path_id=5954130870577656471`, 1707v/2306t, 3b), `male_hand000` (`path_id=7170271499033216896`, 616v/820t, 22b)
- **Torção nos Braços:** `UpArmTwist`: **`False`** | `ForeTwist`: **`False`** (`nenhum`)
- **Métricas Anatômicas:** Pelve `Z=0.000m` | Ombro `Z=0.000m` | Cabeça `Z=0.000m` | Envergadura `X=0.000m` | Braço Sup. `0.000m` | Antebraço `0.000m`
- **Articulações Chave (L):** `UpperArm=[0, 0, 0]` -> `Forearm=[0, 0, 0]` -> `Hand=[0, 0, 0]` | `Pelvis=[0, 0, 0]` | `Head=[0.0, 0.0087, 1.5276]`
- **Ossos Extras (Física/Cabelo/Capa/Armas):** Nenhum
- **Diretriz Específica:** Lamster (`lamster_tps.mo`): Cloned from Johnny (`1031`, `qiangni_tps.mo`) skeleton. Shares Johnny's exact bone hierarchy and world bind poses.
