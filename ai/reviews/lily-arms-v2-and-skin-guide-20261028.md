# HANDOFF — SKIN-068 / SKIN-PIPELINE-001 (Lily Bruxa Carmesim Arms v2 + Guia de 24 Heróis)

- **Data:** 2026-10-28 (23:18 BRT / 2026-10-29T02:18:33Z)
- **Responsável:** Antigravity
- **Status:** Implementada, aprovada visualmente pelo usuário e instalada na Oracle (`CRC 26102801`); aguarda teste em cliente Unity real.

## 1. Causa Raiz Identificada (`SKIN-050` → `051` → `052`)
1. **Landmarks `su, se, sw` estimados manualmente fora de centro (`lily050`):** `se_x = -0.258` foi usado quando o centro real do cotovelo na malha era `-0.2105`, deslocando o braço `4.8 cm` para dentro e `3.8 cm` para trás do eixo dos ossos `UpperArm -> Forearm` e amassando a barra da manga vermelha.
2. **Braços afinados (`0.97x` em `050`) e ponte interna cônica de `5%` (`taper = 1 - 0.95*t` em `052`) ancorada na parede lateral da manga (`Z = 1.22`):** Faziam o braço parecer esquelético e terminavam o braço colado na parede lateral da manga (`10 cm` abaixo do ombro `sh`, `Z = 1.31`), causando o efeito de braço flutuando/saindo da lateral da manga em `expression_A50`.
3. **Elevação de `10.7 cm` de `Bip001 L/R Clavicle` em `expression_A50`:** Sem participação de `Clavicle` na cápsula do ombro da roupa (`1.15 < S_z < 1.33`), o ombro do casaco ficava preso `10.7 cm` abaixo no `Spine2` quando os braços subiam a `116°`.

## 2. Entregas Realizadas
- **Catálogo Esquelético dos 24 Heróis:** `tools/extract_hero_profiles.py` -> `tools/hero_rig_profiles.json` (todos os 24 heróis `1003`..`1037` e `2000`, incluindo fallbacks `hz_tps.mo` para `1034` e `medusa_tps.mo` para `1036`).
- **Módulo Reutilizável de Rigging:** `tools/skin_rig_helper.py` (`load_hero_profile`, `measure_arm_landmarks`, `recenter_and_thicken_arm`, `tailor_bell_sleeve`, `compute_longitudinal_arm_weights`, `compute_torso_sleeve_weights`, `rebuild_cylindrical_undersleeve_bridge`).
- **Guia Completo para o Codex:** `tools/generate_skin_guide.py` -> `ai/SKIN_GUIDE.md` (5 Regras de Ouro + tabela comparativa + seção individual detalhada para cada um dos 24 heróis).
- **Reconstrução dos Braços e Mangas da Lily (`SKIN-068`):** `scratch/lily068/build.py` -> `candidate-assets/lily-modern-witch/arms-v2/lilisi_tps_crimson.mo` (`+24%` volume radial no braço superior, `+20%` no antebraço, manga ajustada coaxialmente ao redor de `sh -> el`, `Clavicle` na cápsula do ombro e ponte cilíndrica `row_tapers=(0.72, 0.42)` ancorada diretamente sobre `sh`).

## 3. Verificações e Instalação
- **Testes automatizados (`scratch/lily068/verify.py`):** `status: passed`, `776/776` objetos preservados (apenas `meshId = -7280344775147488012` alterado), `1897` vértices ajustados, `140` amostras nativas verificadas com contenção `maxRootOutsideGarment = -0.00698m < 0.001m`. Rosto, cabelo, unhas pintadas (`051`) e física das 6 cadeias do sobretudo (`050`) 100% preservados.
- **Instalação na Oracle (`scratch/lily068/release/`):**
  - Backup local: `backups/lily068-before/`
  - Backup remoto: `/home/ubuntu/backups/lily-crimson-skin068-20261029T021833Z/runtime.tar.gz`
  - Manifesto `files_dir.xml` atualizado para `crc="26102801"` (`HeroAnimator/lilisi_tps_crimson.mo`)
  - Validação ao vivo (`scratch/lily068/release/check-live.py`): `4/4` hashes HTTP (`1.1.1` e `1.3.5`) + `TCP 7777` confirmados; processo PM2 `triggerun` (`PID 941494`) online sem reinício e sem alteração de DLL.
- **Teste no cliente Unity:** Pendente confirmação in-game pelo usuário.
