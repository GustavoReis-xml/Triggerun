"""
Generate ai/SKIN_GUIDE.md from tools/hero_rig_profiles.json.
Provides the universal Tripo -> Trigger Run (.mo) workflow + per-hero rigging
reference for all 24 playable heroes.
"""

from pathlib import Path
import json

ROOT = Path(__file__).resolve().parents[1]
profiles = json.loads((ROOT / "tools/hero_rig_profiles.json").read_text(encoding="utf-8"))

CAREER_NAMES = {
    1: "Assault / Rifle",
    2: "Sniper / Marksman",
    3: "Defense / Heavy",
    4: "Stealth / Melee-SMG",
}

SPECIAL_NOTES = {
    "1003": "Alice (`jvjishou_tps.mo`): Sniper rig (`72` bones, `ForeTwist` only, no `UpArmTwist`). Has hair/ponytail helper bones (`Bone001`..`Bone019`). Keep sniper rifle shoulder stock zone (`R Clavicle`/`R UpperArm`) clean and avoid bulky right-chest pads.",
    "1005": "Glider (`feixing_tps.mo`): Jetpack/flight assault rig (`UpArmTwist` + `ForeTwist`). Preserve jetpack/wing attachment bones if replacing body mesh.",
    "1006": "Tagaloa (`xt_tps.mo`): Heavy tank rig (`98` bones). **CRITICAL**: `taluo_wuqi` / weapon mesh is embedded inside the TPS bundle alongside the body mesh. Never overwrite weapon renderers when replacing the body mesh.",
    "1015": "Lars (`tuling_tps.mo`): Tech/turret rig (`ForeTwist`). Multiple LOD/state renderers (`gao_zhengchang`, `zhong_zhengchang`, `di_zhengchang`, `fuhuo_wudi`) must have their `m_Mesh`, `m_Bones`, and `m_Materials` updated together.",
    "1016": "Valkyrie (`nv_tps.mo`): Dual-submesh winged/valkyrie rig with extensive cloth/skirt/hair helper bones. Ideal candidate for `DynamicBone` skirt/cape chains parented to `Bip001 Pelvis`.",
    "1017": "Celina (`babi_tps.mo`): Agile female rig (`UpArmTwist` + `ForeTwist`). Slim shoulder span; always measure source arm centroids automatically (`measure_arm_landmarks`).",
    "1021": "Carmen (`kamei_tps.mo`): Female caster/marksman rig (`UpArmTwist` + `ForeTwist`). Check waist/hip Z-interpolation knots carefully.",
    "1022": "Scarlett (`sikalei_tps.mo`): Stealth/assassin rig (`UpArmTwist` + `ForeTwist`). Fast spin/roll animations require clean longitudinal twist gradients.",
    "1023": "Brigit (`bulijite_tps.mo`): Heavy support/shield rig (`UpArmTwist` + `ForeTwist`).",
    "1024": "Shogun (`jiangjun_tps.mo`): Armored melee samurai rig (`75`+ bones). Has tasset/pauldron helper bones (`Bone001`..`Bone018`). Extreme upper-body twist and shoulder elevation during katana slash animations — always use full-radius cylindrical shoulder sockets and `Clavicle` cushioning on pauldrons (see `scratch/shogun040`).",
    "1025": "Eva (`yiwa_tps.mo`): Agile female rig (`UpArmTwist` + `ForeTwist`).",
    "1026": "Dragos (`delagusi_tps.mo`): Tall heavy proportions. Verify shoulder span (`shoulderSpan`) and arm length scaling.",
    "1027": "Jack (`jieke_tps.mo`): Standard male assault proportions (`UpArmTwist` + `ForeTwist`).",
    "1028": "Loki (`luoji_tps.mo`): Trickster/mage rig with cape/coat helper bones.",
    "1029": "Angel (`tianshi_tps.mo`): Winged/healer rig with wing/skirt bones. Preserve wing renderer/bones when adapting body skins.",
    "1030": "Vincent (`wensente_tps.mo`): Male marksman/coat rig (`UpArmTwist` + `ForeTwist`).",
    "1031": "Johnny (`qiangni_tps.mo`): Rocker/guitarist rig (`UpArmTwist` + `ForeTwist`). Guitar/rifle grip poses (`scratch/johnny034/grip.py`) require accurate `L/R Hand` and finger weight transfer.",
    "1032": "Venus / DaJi (`daji_tps.mo`): Fox-spirit/tail rig (`UpArmTwist` + `ForeTwist`) with tail/skirt helper bones.",
    "1033": "Lily / Lilith (`lilisi_tps.mo`): Gothic/witch vampire rig (`76` native bones + `UpArmTwist` + `ForeTwist`, `3` body LOD renderers `-7280344775147488012`, `-6825634022250907522`, `4739482402592737565`). Rest pose `stand` and `R_fire` bring both elbows forward across the chest (`~43°–64°`) while `expression_A50` and `expression_D200` raise arms `80°–116°` overhead. Short bell sleeves must be tailored coaxially around `UpperArm -> Forearm` (`tailor_bell_sleeve`) and synchronized with `UpperArm` weights so the black arms neither poke through nor flip the sleeves into giant bat-wings (`scratch/lily068`).",
    "1034": "Khan / HuangZhong (`hz_tps.mo`): Heavy bow/cannon veteran rig. Broad shoulders and bow-draw poses.",
    "1035": "NeZha (`nezha_tps.mo`): **MULTI-RENDERER SPECIAL**: Has `5` SkinnedMeshRenderers including `4` extra Ultimate arms (`nezha_ult_hand` x4) + sash/spear meshes. Only replace the main body mesh (`1557941023447926386`) and leave the 4 Ultimate arm renderers intact (`scratch/nezha029`).",
    "1036": "Gaya / Medusa (`medusa_tps.mo`): Serpent-hair/tail female rig (`UpArmTwist` + `ForeTwist`) with snake-hair helper bones.",
    "1037": "David / DaWei (`dawei_tps.mo`): **BUILT-IN BOW/QUIVER RENDERERS**: Bundle contains separate renderers for `Gong_skin` (bow), `JianDai_skin` (quiver), `Jian_skin` (arrow), and `TouFa_skin` (hair) alongside `dawei_skin` (body). Only replace `dawei_skin` (and `TouFa_skin` if head/hair is included).",
    "2000": "Lamster (`lamster_tps.mo`): Cloned from Johnny (`1031`, `qiangni_tps.mo`) skeleton. Shares Johnny's exact bone hierarchy and world bind poses.",
}

lines = [
    "# Guia Definitivo de Adaptação de Skins (Tripo AI -> Trigger Run `.mo`)",
    "",
    "Este guia documenta o pipeline padronizado para adaptar modelos `.glb` gerados no **Tripo AI** aos esqueletos `.mo` (Unity 5 AssetBundles) do Trigger Run com **precisão de 1ª tentativa**, evitando retrabalho com braços finos, cotovelos fora de eixo, mangas virando asas de morcego ou costuras rasgadas.",
    "",
    "---",
    "",
    "## 1. Ferramentas Padrão do Repositório",
    "",
    "Antes de escrever qualquer `build.py` em `scratch/`, **sempre importe e utilize**:",
    "- **`tools/hero_rig_profiles.json`**: Catálogo extraído diretamente dos `.mo` de todos os **24 heróis jogáveis**, contendo contagens de ossos, `path_id` dos `SkinnedMeshRenderer`, presença de `UpArmTwist`/`ForeTwist`, coordenadas exatas (`Pelvis`, `Spine2`, `Neck`, `Head`, `Clavicle`, `UpperArm`, `Forearm`, `Hand`, `Thigh`, `Calf`, `Foot`) e comprimentos dos membros.",
    "- **`tools/skin_rig_helper.py`**: Módulo Python reutilizável com as funções matemáticas testadas:",
    "  - `load_hero_profile(hero_id_or_name)`: Carrega o perfil completo do herói.",
    "  - `measure_arm_landmarks(V_torso_aligned, S_raw, arm_mask, sh, el, hand)`: Mede **automaticamente** o centroide real do braço na malha do Tripo (punho, cotovelo e topo do braço) — **NUNCA chute coordenadas `su, se, sw` no olho nem copie de outro herói!**",
    "  - `recenter_and_thicken_arm(...)`: Centraliza cada seção transversal do braço coaxialmente sobre os ossos `UpperArm -> Forearm -> Hand` e aplica ganho radial (`+20%` a `+24%` no bíceps/antebraço, suavizando para `1.0x` no punho para preservar mãos e unhas, e `0.86x` dentro do topo oculto da manga para nunca esbarrar no tecido externo).",
    "  - `tailor_bell_sleeve(...)`: Suaviza a abertura da manga e reduz o excesso de aba/poncho coaxialmente ao redor do eixo `sh -> el`, impedindo que mangas boca-de-sino virem asas gigantes quando o braço levanta.",
    "  - `compute_longitudinal_arm_weights(...)`: Aplica gradiente longitudinal real de Biped (`UpperArm -> UpArmTwist` e `Forearm -> ForeTwist`) sincronizado no topo com a boca da manga.",
    "  - `compute_torso_sleeve_weights(...)`: Distribui pesos `Spine2 -> Clavicle -> UpperArm` na manga/ombreira sem rasgar as costas nem puxar a gola.",
    "  - `rebuild_cylindrical_undersleeve_bridge(...)`: Constrói o tubo oculto do braço dentro da manga com volume anatômico (`row_tapers = (0.76, 0.45)`) alinhado entre `center` e `cap_center`, ancorando a tampa oculta dentro da cavidade do ombro (`maxOutside < 0.001`).",
    "",
    "---",
    "",
    "## 2. As 5 Regras de Ouro (Por que skins anteriores davam defeito e como evitar)",
    "",
    "### Regra 1: Nunca chute `su, se, sw` manualmente",
    "- **Causa do bug antigo:** Nos scripts antigos (`lily050`), as coordenadas do ombro/cotovelo fonte (`su, se, sw`) foram estimadas manualmente (`se_x = -0.258` quando o centro real da malha estava em `-0.210`). Isso deslocou o braço **4.8 cm para dentro e 3.8 cm para trás** do osso do cotovelo, fazendo o braço dobrar fora de centro e atravessar a manga.",
    "- **Solução:** Sempre use `measure_arm_landmarks(...)` e `recenter_and_thicken_arm(...)` de `tools/skin_rig_helper.py`.",
    "",
    "### Regra 2: Preservação e reforço de volume radial nos braços (`1.20x–1.24x`)",
    "- **Causa do bug antigo:** O Tripo gera braços femininos/estilizados ligeiramente finos, e os scripts antigos aplicavam escala transversal `0.97` ao esticar o comprimento do braço.",
    "- **Solução:** Use `upper_radial_scale=1.24` e `forearm_radial_scale=1.20` em `recenter_and_thicken_arm(...)`, com transição suave (`smooth`) para `1.0` abaixo de `wrist_s_z_hi` para não alterar mãos, dedos ou unhas pintadas.",
    "",
    "### Regra 3: Ponte interna da manga (`undersleeve bridge`) cilíndrica até a cápsula do ombro (`sh`), nunca na parede lateral da manga",
    "- **Causa do bug antigo:** Para esconder o topo do braço dentro da manga (`lily052`), usou-se `taper = 1 - 0.95*t` e escolheu-se o triângulo do casaco mais próximo da boca da manga (`Z = 1.22`), colando o fim do braço na **parede lateral da manga** no meio do bíceps em vez de levá-lo até o ombro (`sh`, `Z = 1.31`). Na pose `expression_A50` (braços a `116°`), o braço parecia sair/flutuar da lateral da manga em vez de sair do ombro.",
    "- **Solução:** Use `rebuild_cylindrical_undersleeve_bridge(..., row_tapers=(0.72, 0.42))` — ele seleciona o triângulo âncora na **cápsula superior do ombro** diretamente sobre `sh` (`|S_x| in [0.115, 0.175], S_z in [1.265, 1.325]`), leva `rows[1..2]` coaxialmente pelo centro oco da manga até o ombro e sincroniza os pesos do topo oculto do braço com a manga (`maxOutside < 0.001`).",
    "",
    "### Regra 4: Mangas curtas / boca-de-sino exigem ajuste coaxial (`tailor_bell_sleeve`) + peso de `Clavicle` na cápsula do ombro",
    "- **Causa do bug antigo:** Em poses como `expression_A50`, o osso `Bip001 L/R Clavicle` eleva o ombro `11 cm` acima do `Bip001 Spine2`. Se a cápsula do ombro da roupa tiver apenas peso de `Spine2` + `UpperArm`, o ombro da roupa fica `11 cm` abaixo da articulação do braço quando os braços levantam.",
    "- **Solução:**",
    "  1. Ajuste a geometria de repouso da manga com `tailor_bell_sleeve(...)` para reduzir o excesso de aba (`radial_pull=0.42`, `min_radius=0.052`) coaxialmente ao redor do segmento `sh -> el`.",
    "  2. Use `compute_torso_sleeve_weights(..., max_arm_weight=0.98, clavicle_weight=0.10, twist_share=0.50)` — que aplica `UpperArm/UpArmTwist` no corpo da manga e distribui `Bip001 L/R Clavicle` na cápsula do ombro (`1.15 < S_z < 1.33`), garantindo que a manga suba junto com o ombro em `expression_A50` sem rasgar as costas nem puxar a gola.",
    "",
    "### Regra 5: Validação obrigatória em 5 poses críticas antes de instalar",
    "- Sempre renderize e inspecione visualmente:",
    "  1. `view-0.png` (T/A-pose de repouso)",
    "  2. `stand` e `R_fire` (cotovelos trazidos à frente do peito / mira)",
    "  3. `expression_A50` e `expression_D200` (braços erguidos `80°–116°` acima da cabeça)",
    "  4. `expression_C51` (perfil lateral / costas da roupa)",
    "- Verifique com `verify.py` que apenas `meshIds` (e `textureId` quando houver textura nova) mudaram no `.mo`, que `maxRootOutsideGarment < 0.001` e que os demais objetos e `DynamicBone` permanecem intactos.",
    "",
    "---",
    "",
    "## 3. Tabela Resumo dos 24 Heróis Jogáveis",
    "",
    "| ID | Herói | Bundle TPS | Carreira | Ossos | Renderers | Twist Sup. | Twist Ant. | Alt. Pelve | Alt. Ombro | Envergadura Ombros |",
    "|---:|---|---|---|---:|---:|:---:|:---:|---:|---:|---:|",
]

for hid in sorted(profiles.keys(), key=lambda x: int(x)):
    p = profiles[hid]
    mm = p.get("mainMesh", {})
    m = mm.get("proportions", {})
    tw = mm.get("twistBones", [])
    career = CAREER_NAMES.get(p["career"], str(p["career"]))
    up_tw = "Sim" if any("UpArm" in t for t in tw) else "Não"
    fore_tw = "Sim" if any("Fore" in t for t in tw) else "Não"
    pel_h = f"`{m.get('pelvisHeight', 0):.3f}m`" if "pelvisHeight" in m else "—"
    sh_h = f"`{m.get('shoulderHeight', 0):.3f}m`" if "shoulderHeight" in m else "—"
    sh_sp = f"`{m.get('shoulderSpan', 0):.3f}m`" if "shoulderSpan" in m else "—"
    lines.append(
        f"| `{hid}` | **{p['name']}** (`{p['internalName']}`) | `{p['tpsBundle']}` | {career} | {mm.get('boneCount', 0)} | {len(p.get('skinnedRenderers', []))} | {up_tw} | {fore_tw} | {pel_h} | {sh_h} | {sh_sp} |"
    )

lines.extend([
    "",
    "---",
    "",
    "## 4. Guia Detalhado por Herói (Todos os 24 Heróis)",
    "",
])

for hid in sorted(profiles.keys(), key=lambda x: int(x)):
    p = profiles[hid]
    mm = p.get("mainMesh", {})
    m = mm.get("proportions", {})
    kp = mm.get("keyJoints", {})
    tw = mm.get("twistBones", [])
    rends = ", ".join(
        f"`{r['name']}` (`path_id={r['meshPathId']}`, {r['vertices']}v/{r['triangles']}t, {r['boneCount']}b)"
        for r in p.get("skinnedRenderers", [])[:6]
    )
    if len(p.get("skinnedRenderers", [])) > 6:
        rends += f", ... (+{len(p['skinnedRenderers']) - 6} renderers)"
    extra_list = mm.get("extraClothOrWeaponBones", [])
    extra = ", ".join(f"`{b}`" for b in extra_list[:16])
    if len(extra_list) > 16:
        extra += f", ... (+{len(extra_list) - 16} ossos)"
    if not extra:
        extra = "Nenhum"

    sh_l = kp.get("Bip001 L UpperArm", [0, 0, 0])
    el_l = kp.get("Bip001 L Forearm", [0, 0, 0])
    ha_l = kp.get("Bip001 L Hand", [0, 0, 0])
    pel = kp.get("Bip001 Pelvis", [0, 0, 0])
    head = kp.get("Bip001 Head", [0, 0, 0])

    has_up = any("UpArm" in t for t in tw)
    has_fore = any("Fore" in t for t in tw)

    lines.extend([
        f"### `{hid}` — {p['name']} (`{p['tpsBundle']}` / `{p['heroClass']}`)",
        f"- **Renderer Principal:** `{mm.get('rendererName', '')}` (`meshPathId={mm.get('meshPathId', '')}`, `{mm.get('boneCount', 0)}` ossos)",
        f"- **Renderers no Bundle:** {rends}",
        f"- **Torção nos Braços:** `UpArmTwist`: **`{has_up}`** | `ForeTwist`: **`{has_fore}`** (`{', '.join(tw) if tw else 'nenhum'}`)",
        f"- **Métricas Anatômicas:** Pelve `Z={m.get('pelvisHeight', 0):.3f}m` | Ombro `Z={m.get('shoulderHeight', 0):.3f}m` | Cabeça `Z={m.get('headPivotHeight', 0):.3f}m` | Envergadura `X={m.get('shoulderSpan', 0):.3f}m` | Braço Sup. `{m.get('upperArmLength', 0):.3f}m` | Antebraço `{m.get('forearmLength', 0):.3f}m`",
        f"- **Articulações Chave (L):** `UpperArm={sh_l}` -> `Forearm={el_l}` -> `Hand={ha_l}` | `Pelvis={pel}` | `Head={head}`",
        f"- **Ossos Extras (Física/Cabelo/Capa/Armas):** {extra}",
        f"- **Diretriz Específica:** {SPECIAL_NOTES.get(hid, 'Rig Biped padrão.')}",
        "",
    ])

out_path = ROOT / "ai/SKIN_GUIDE.md"
out_path.write_text("\n".join(lines), encoding="utf-8")
print("WROTE", out_path, "lines:", len(lines))
