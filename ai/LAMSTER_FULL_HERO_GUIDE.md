# Guia Definitivo: Transformando o Lamster em um Herói 100% Autêntico e Nativo

> **Atualização instalada LAMSTER-008 (29/10/2026,21:22BRT):** [passagem atual](reviews/lamster-polish-20261029.md). Qpassiva30/s,íconesdimensionados,controladoresFPS/TPSexclusivos com6clipes cada,FX/sons. Cliente agoraexige **Assembly-CSharp.dll + TriggerunCombatHud.dll juntos**,pacote `/updates/client-update-lamster008.zip`. Testesautomatizados passaram; teste realUnity008pendente. Preserve o kit de utilidade da007 descrito abaixo, sem voltar à proposta de área de cura.

> **Implementação concluída pelo Codex em 29/10/2026 — LAMSTER-007:** consulte a [passagem da versão instalada](reviews/lamster-full-hero-20261029.md) e `src/lamsterHero.js` antes de editar. O usuário substituiu o campo de cura por utilidade/mobilidade. O texto abaixo permanece como proposta histórica, não como especificação executável: IDs1201–1203 pertencem ao David; `SkillTreatmentMe` depende do Lars; buffs1003/1008/1010 e opcodes31205/31210 não têm as funções sugeridas abaixo. A versão instalada usa skills22001–22004, buffs22011–22016, classes nativas genéricas, Q via31208 e energia50001→50004. `secskill="0"` evita a dupla chamada de WeaponSec que o código nativo já realiza. Não restaurar a área de cura nem copiar os exemplos antigos sobre a implementação atual.

> **Destinatário:** Codex  
> **Autor:** Antigravity  
> **Data:** 29/10/2026  
> **Contexto:** Lamster (`ID 2000`, `heroclass="LuoMo"`) foi introduzido como um protótipo jogável com suporte a cura no servidor e uma HUD temporária via Unity `OnGUI` (`client-patches/CombatHud.cs`). Este guia estabelece a arquitetura completa e o passo a passo exato para transformá-lo em um herói de primeira classe com **HUD 100% nativa (NGUI/uGUI)**, **kit completo de habilidades ativas (LMB, RMB, E, Shift e Q Ultimate)**, **animações personalizadas**, **efeitos visuais (`.ef`)** e **sincronização autoritativa no servidor**.

---

## 1. Diagnóstico Técnico: Por que o Lamster Parece Improvisado Hoje?

### A. HUD de Depuração (`OnGUI`) vs. HUD Nativa do Jogo
1. **O problema visual atual:**
   - Em `client-patches/CombatHud.cs` (`TriggerunLamsterHud`, linhas 84–101), o cliente desenha uma caixa retangular cinza escura no canto inferior direito da tela (`GUI.Box` e `GUI.Label`):
     ```csharp
     GUI.Box(new Rect(x, y, width, height), GUIContent.none, boxStyle);
     GUI.Label(new Rect(x + 12f, y + 10f, width - 24f, 22f), "LAMSTER | SUPORTE DE COMBATE", titleStyle);
     GUI.Label(new Rect(x + 12f, y + 36f, width - 24f, 48f), ...);
     ```
   - Isso ocorre porque Lamster não possui nenhuma habilidade registrada em `heroinfo.xml` e `playerskill.xml`, fazendo com que a HUD nativa (`NewHeroSkillUi`) fique vazia.
2. **Como a HUD nativa realmente funciona (`NewHeroSkillUi.cs` e `OneSkilUI.cs`):**
   - No cliente Unity, a HUD de combate inferior direita é controlada por `NewHeroSkillUi`.
   - No método `NewHeroSkillUi.showSkillUi(_NetLiving)` (`NewHeroSkillUi.cs:191-246`), o cliente obtém as habilidades do herói através de `_NetLiving.heroSkill.skillsDict`.
   - Essas habilidades vêm do campo `showskill` em `heroinfo.xml` (ex: `showskill="1030|1031|1032"` no Lars).
   - Para cada habilidade cadastrada em `playerskill.xml` cujo ícone seja válido (`icon != "0"` e `icon != ""`), `NewHeroSkillUi` instancia um slot nativo `OneSkilUI` (`cloneskill`).
   - O `OneSkilUI` cuida automaticamente de:
     - Ícone da habilidade com máscara circular/quadrada nativa.
     - Badge da tecla correspondente: tecla `"E"` para `dokey="Skill1"`, `"Shift"` para `dokey="Skill2"`, e o sprite nativo do botão direito do mouse (`youjian` do atlas `ATLAS_NEWGAMEUI`) quando `dokey="WeaponSec"` ou `secSkill == dokey`!
     - Varredura radial cinza de cooldown (`lengque/lq`).
     - Contador numérico centralizado de cooldown (`up/jishu`).
     - Contador de cargas/estoque (`top/Text`, `top/fillcount` quando `maxskillnumber > 1`).
     - Brilho de prontidão quando o cooldown zera (`up/ui_lenque/lengque`).

### B. Falta da Barra de Ultimate Nativa (`CPlayerDazhao.cs` e `dazhao.xml`)
- No centro inferior da tela, todo herói nativo possui um anel circular de Ultimate (`Q`) controlado por `CPlayerDazhao`.
- Em `CPlayerDazhao.initDazhao` (`CPlayerDazhao.cs:149`), o cliente busca `fGameManager.mloadConfig.AllDazhaoObj[player.playerCommon.m_nModelIndex]`.
- Lamster (`2000`) **não possui entrada em `dazhao.xml`**, portanto `CPlayerDazhao` desativa o anel e a tecla `Q` não faz nada.

### C. Ausência de Teclas Ativas (`E`, `Shift`, `Q`)
- Hoje o Lamster só tem:
  - `LMB` (Soco primário de curto alcance que cura aliados).
  - `RMB` (Pulso em área disparado como tiro secundário da arma).
- Ele não possui:
  - Habilidade em `E`.
  - Habilidade em `Shift` (mobilidade/reposicionamento).
  - Habilidade em `Q` (Ultimate).

---

## 2. Design do Kit Completo do Herói (Lamster — Especialista Tático / Suporte de Vanguarda)

Propomos um kit coeso e divertido que aproveita 100% dos componentes nativos já existentes na engine C# (`SkillTreatment`, `SkillSpeedUp`, `cMapEffect`, `dazhao.xml`):

```mermaid
flowchart TD
    subgraph Kit do Lamster (ID 2000)
        LMB["LMB: Golpe Bio-Cinético (Weapon 2000, shoot 1)<br/>Alcance 6m | Dano a inimigos, cura 180 a aliados"]
        RMB["RMB: Pulso de Inspiração (Weapon 2000, shoot 2 + Skill 1204)<br/>Raio 8m | Cura 360, dano 150, aplica Buff 1007 (+20% mov) | CD 6s"]
        E["Skill E: Totem de Campo Vital (Skill 1201, dokey='Skill1')<br/>Instala um campo regenerativo que cura 100 HP/s por 6s | CD 14s"]
        Shift["Skill Shift: Salto de Vanguarda (Skill 1202, dokey='Skill2')<br/>Arrancada veloz com redução de dano de 30% por 3s | CD 8s"]
        Q["Ultimate Q: Santuário Heroico (Dazhao 2000 -> Skill 1203)<br/>Carrega com Dano e Cura | Emite aura de invulnerabilidade/sobrecura de 600 HP | CD 60s"]
    end
```

---

## 3. Passo a Passo: Integrando o Lamster 100% na HUD Nativa

### Passo 1: Atualizar os XMLs de Configuração do Cliente (`public/baxi/` e `zh/`)

#### 1. `heroinfo.xml` (`id="2000"`)
Defina as habilidades que a HUD nativa (`NewHeroSkillUi`) deve desenhar:
```xml
<h id="2000" heroid="2000" name="Lamster" heroclass="LuoMo" ...
   showskill="1201|1202|1204"
   secskill="WeaponSec"
   ... />
```
- `showskill="1201|1202|1204"` instrui `NewHeroSkillUi.showSkillUi` a instanciar 3 caixas de habilidade:
  - `1201`: Skill E (`dokey="Skill1"`).
  - `1202`: Skill Shift (`dokey="Skill2"`).
  - `1204`: Cooldown visual do RMB (`dokey="WeaponSec"`).
- `secskill="WeaponSec"` faz o `OneSkilUI` anexar automaticamente o ícone de botão direito (`youjian`) ao slot da habilidade 1204!

#### 2. `playerskill.xml` (`AllPlayerSkill`)
Adicione as definições das habilidades ativas e do Ultimate do Lamster:
```xml
<!-- Skill E: Totem/Campo Vital -->
<s id="1201" name="Campo Vital" skillclass="SkillTreatment" cd="14000" longtime="6000" 
   buff_idx="1008" buff_idx_me="1008" dokey="Skill1" icon="lamster_e" 
   animation="Skill_Treatment" soundbegin="sound_lamster_e" />

<!-- Skill Shift: Arrancada Tática -->
<s id="1202" name="Arrancada Tática" skillclass="SkillSpeedUp" cd="8000" longtime="3000" 
   buff_idx="1003" buff_idx_me="1003" dokey="Skill2" icon="lamster_shift" 
   animation="Skill2" soundbegin="sound_lamster_shift" />

<!-- Ultimate Q: Santuário Heroico -->
<s id="1203" name="Santuário Heroico" skillclass="SkillTreatment" cd="0" longtime="8000" 
   buff_idx="1010" buff_idx_me="1010" dokey="0" icon="lamster_ult" 
   animation="SuperSkillDo" soundbegin="sound_lamster_ult" />

<!-- Cooldown Visual RMB: Pulso de Inspiração -->
<s id="1204" name="Pulso de Inspiração" skillclass="SkillTreatment" cd="6000" longtime="500" 
   buff_idx="1007" buff_idx_me="1007" dokey="WeaponSec" icon="lamster_sec" 
   animation="Skill1" soundbegin="" />
```

> **Por que essas `skillclass` foram escolhidas?**  
> `SkillTreatment` e `SkillSpeedUp` são classes já compiladas no executável C# que herdam de `SkillBaseMe` e funcionam para **qualquer** modelo de herói sem necessidade de recompilar classes filhas exclusivas! Elas gerenciam temporizadores, envio automático de pacote `31205` (`SkillDo`) ao servidor, execução de animação via `SkillInfo.SkillAnimation` e exibição de cooldown no `OneSkilUI`.

#### 3. `dazhao.xml` (Ultimate Ring de Centro de Tela)
Adicione o Lamster na lista de Ultimates nativos:
```xml
<i id="2000" name="Lamster" type="0">
    <skill id="36" doskill="1203" energymax="10000" energyaddtime="120" 
           energyadddamage="100" energyaddcure="150" icon="lamster_ult" 
           continuestime="8000" />
</i>
```
- `energymax="10000"`: valor padrão para 100% de carga.
- `energyaddtime="120"`: carga passiva por segundo.
- `energyadddamage="100"`: carga obtida por ponto de dano causado.
- `energyaddcure="150"`: **carga obtida por ponto de cura concedida a aliados**!
- `icon="lamster_ult"`: ícone renderizado no centro do anel quando a Ultimate estiver pronta (brilha e pulsa quando chega a 100%).

#### 4. `heroskill.xml` e `heroskillinfo.xml` (Telas de Saguão e Menu F1)
Atualize os arquivos que descrevem o herói na interface de seleção e no overlay F1 (`HeroSkillInfoUICon.cs`):
- `heroskill.xml`:
  ```xml
  <h id="2000">
      <s id="1" name="Golpe Bio-Cinético" icon="lamster_primary" skillindex="0"/>
      <s id="2" name="Pulso de Inspiração" icon="youjian" skillindex="1204"/>
      <s id="3" name="Campo Vital" icon="e" skillindex="1201"/>
      <s id="4" name="Arrancada Tática" icon="shift" skillindex="1202"/>
      <s id="5" name="Santuário Heroico" icon="q" skillindex="1203"/>
  </h>
  ```
- `heroskillinfo.xml`: Atualize com os textos descritivos detalhados em português para exibição quando o jogador pressionar F1 durante a partida.

---

### Passo 2: Registro de Ativos de Textura e Ícones (`src/lamsterAssets.js`)

O cliente busca os ícones das habilidades em `StreamingAssets/Icon/skillicon/<icon>.txt`.  
Em `src/lamsterAssets.js`, registre os novos ícones texturizados em PNG/TXT para o cache HTTP:
- `lamster_e.txt` (Ícone de cruz médica bio-energética / totem para a tecla E)
- `lamster_shift.txt` (Ícone de pegadas / arrancada com linhas de velocidade para Shift)
- `lamster_sec.txt` (Ícone do pulso sônico / onda expansiva para o RMB)
- `lamster_ult.txt` (Ícone de brasão dourado brilhante para o anel central de Ultimate)

---

### Passo 3: Limpeza da HUD Temporária (`client-patches/CombatHud.cs`)

Assim que os XMLs forem lidos pelo cliente, `NewHeroSkillUi` e `CPlayerDazhao` assumirão 100% da renderização:
- Abra `client-patches/CombatHud.cs`.
- Em `TriggerunLamsterHud.OnGUI()`, remova a chamada de renderização dos labels cinzas (`GUI.Box` e `GUI.Label`) ou desative o script quando o herói possuir habilidades nativas detectadas em `NewHeroSkillUi`.
- **Resultado:** A tela fica totalmente limpa, com os slots nativos de alta resolução e as fontes oficiais do jogo.

---

## 4. Passo a Passo: Implementação no Servidor (`src/`)

### A. Geração de Dados de Habilidade e Ultimate (`src/database.js`)
Quando o jogador entra na partida com o Lamster, o servidor envia os pacotes `31201` (habilidades ativas) e `1306` (parâmetros do herói):
1. **`generateHeroSkillsData(2000)`:**
   - Retorne o array de habilidades registradas: `[1201, 1202, 1203, 1204]`.
2. **`generateHeroDazhaoData(2000)`:**
   - Mapeie a entrada de `dazhao.xml` para o ID 2000:
     ```javascript
     heroDazhaoMap[2000] = {
       id: 36,
       doskill: 1203,
       energymax: 10000,
       energyaddtime: 120,
       energyadddamage: 100,
       energyaddcure: 150,
       continuestime: 8000
     };
     ```

### B. Tratamento de Pacotes de Combate e Habilidade (`src/socketServer.js`)

1. **Pacote `31205` (`SkillDo` - Jogador pressiona E, Shift ou Q):**
   - O cliente envia `31205` com o ID da habilidade disparada.
   - O servidor deve:
     - Verificar tempo de recarga (cooldown).
     - Se `skillId === 1201` (Skill E - Campo Vital):
       - Disparar o pacote de animação e efeito para a sala: `broadcastScene(sceneId, 'e' + entityId + ':SkillDo,((skillId=1201))')`.
       - Criar o campo regenerativo na posição atual do jogador (`applyTotemAura` em `src/lamster.js`).
     - Se `skillId === 1202` (Skill Shift - Arrancada):
       - Aplicar buff `1003` (+35% velocidade de movimento e redução de dano) por 3 segundos via `applyBuffToEntity`.
       - Notificar peers da sala com `broadcastScene`.
     - Se `skillId === 1203` (Ultimate Q - Santuário Heroico):
       - Consumir a barra de Ultimate (`player.ultEnergy = 0`).
       - Enviar sincronização de energia `31210` (`EnergySync`).
       - Aplicar buff de sobrecura (`1010`) a todos os aliados no raio de 15m.
       - Tocar o efeito sonoro de Ultimate global do Lamster.

2. **Carga da Barra de Ultimate com Cura (`src/gameLogic.js` / `src/lamster.js`):**
   - No jogo base, a maioria dos heróis só ganha energia de Ultimate causando dano.
   - Como o Lamster é um suporte, em `handleLamsterRallyPulse` e no ataque primário de cura, adicione a chamada:
     ```javascript
     function addPlayerUltEnergy(player, amount) {
       if (!player || !player.heroDazhao) return;
       player.ultEnergy = Math.min(player.heroDazhao.energymax, (player.ultEnergy || 0) + amount);
       // Envia pacote nativo de atualização de energia para o cliente do jogador
       sendPacket(player.socket, 31210, [player.ultEnergy]);
     }
     ```
   - Cada ponto de cura concedido a um aliado vivo acumula energia no anel do HUD!

---

## 5. Passo a Passo: Animações e Efeitos Visuais (`.ef`)

### A. Controladores de Animação (`male_tps.ab` e `male_fps.ab`)
- Lamster utiliza `heroclass="LuoMo"`, com controladores `TBCharAnimationLuoMoTps.cs` e `TBCharAnimationLuoMoFps.cs`.
- Ele roda em cima dos rigs masculinos genéricos (`male_tps` e `male_fps`).
- **Estados de animação nativos disponíveis:**
  - `anihuifu="jump,skill1,skill2,ultimate,fall"` já está presente na configuração de `heroinfo.xml`!
  - `playerskill.xml` aceita os nomes dos clipes de animação nos campos `animation` e `animation_fps`:
    - Para `E`: `"Skill1"` ou `"Skill_Treatment"`.
    - Para `Shift`: `"Skill2"` (arrancada/dash).
    - Para `Ultimate`: `"SuperSkillDo"` ou `"Skill_Ultimate"`.
- **Estratégia recomendada para o Codex:**
  - Inspecione com `AssetStudio` os clipes exportados de `male_tps.ab` e compare com `zhongjiezhe_tps.ab` (Lars) e `dawei_tps.ab` (Bento).
  - Se algum estado estiver ausente no controller `male_tps`, vincule a ação aos gatilhos universais existentes (`Fire`, `Fight`, `Skill1`, `Skill2`) que já executam sem travar o boneco.

### B. Efeitos Visuais Exclusivos (`EffectRes/*.ef`)
- Os efeitos visuais do jogo são pacotes Unity AssetBundle compactados com extensão `.ef` em `StreamingAssets/EffectRes/`.
- Atualmente, Lamster reutiliza temporariamente os efeitos de Loki (`skill_jipusai_gongji`) e Bento (`skill_dawei_ddfw`).
- **Como dar uma identidade visual única ao Lamster:**
  1. No diretório `StreamingAssets/EffectRes/`, existem dezenas de efeitos nativos de bio-energia e cura:
     - `skill_zhongjiezhe_zhiliao.ef` (Partículas de cura verde do Lars).
     - `skill_luolita_huifu.ef` (Aura de anjo/regeneração da Angel).
     - `skill_lingjian_sudu.ef` (Linhas de vento/velocidade).
     - `skill_dawei_dazhao.ef` (Cúpula dourada protetora).
  2. Codex pode referenciar esses efeitos diretamente em:
     - `weapon.xml`: `bulletmodel_fps` e `bulletboommodel` para o projétil/impacto.
     - `buff.xml`: `effect_tps` e `effect_fps` para os auras visuais nos heróis afetados.
     - `mapeffect.xml`: para o totem/campo de cura deixado pela tecla `E`.
  3. Alternativamente, para criar efeitos com paleta verde-esmeralda/dourada própria, clone um `.ef` existente, edite os gradientes de cor do ParticleSystem e registre o bundle com um CAB único em `files_dir.xml`.

---

## 6. Checklist de Implementação e Validação para o Codex

```markdown
- [ ] 1. Configurar XMLs:
      - [ ] `heroinfo.xml`: adicionar `showskill="1201|1202|1204"` e `secskill="WeaponSec"`.
      - [ ] `playerskill.xml`: adicionar entradas completas para 1201, 1202, 1203 e 1204.
      - [ ] `dazhao.xml`: adicionar entrada de Ultimate com ID 2000.
      - [ ] `heroskill.xml` e `heroskillinfo.xml`: atualizar nomes, descrições e ícones.
- [ ] 2. Ícones e Ativos:
      - [ ] Adicionar `lamster_e.txt`, `lamster_shift.txt`, `lamster_sec.txt` e `lamster_ult.txt` em `src/lamsterAssets.js`.
      - [ ] Atualizar o manifesto de arquivos `files_dir.xml`.
- [ ] 3. Servidor:
      - [ ] Implementar `generateHeroSkillsData` e `generateHeroDazhaoData` em `src/database.js`.
      - [ ] Lidar com `SkillDo` (`31205`) e `SuperSkill` (`31211`) em `src/socketServer.js`.
      - [ ] Vincular ganho de energia de Ultimate à cura de aliados (`addPlayerUltEnergy`).
- [ ] 4. Limpeza de Interface:
      - [ ] Desativar `TriggerunLamsterHud.OnGUI` em `client-patches/CombatHud.cs`.
- [ ] 5. Testes e Validação:
      - [ ] Executar suíte de testes: `node --test tests/lamster.test.js tests/match-lifecycle.test.js`.
      - [ ] Verificar integridade de sintaxe: `node --check src/socketServer.js src/gameLogic.js src/lamster.js`.
      - [ ] Validar no cliente Unity: verificar se os slots de E, Shift, RMB e o medidor de Q aparecem com as artes oficiais e contadores de recarga funcionando perfeitamente.
```

---

*Com essa estrutura, Lamster deixa de ser uma casca com scripts de sobreposição e passa a se comportar de forma idêntica aos 24 heróis oficiais do jogo.*
