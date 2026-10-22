# 📖 Projeto Triggerun Server — Sumário Executivo e Técnico Completo

## 1. Visão Geral do Projeto
- **Nome do Projeto:** Triggerun Server (Servidor Privado Emulado de Trigger Run).
- **Objetivo:** Recriar e re-arquitetar o backend completo do jogo de tiro em primeira pessoa multiplayer **Trigger Run** (desenvolvido na engine **Unity**, versões de cliente `1.3.5` e `1.1.1`), cujos servidores oficiais foram desligados.
- **Linguagem / Stack do Servidor:** Node.js (JavaScript puro / CommonJS), Express para HTTP/CDN, TCP nativo (`net`), SQLite / JSON para persistência de dados.
- **Ambientes:**
  - **Desenvolvimento Local:** `e:\server tr` (Windows).
  - **Servidor de Produção:** VPS Oracle Linux (`ubuntu@136.248.105.85`), gerenciado via **PM2** (`process: triggerun`, rodando com usuário root).
  - **Repositório do Código do Cliente (Engenharia Reversa):** `e:\server tr\Assembly\Assembly-CSharp\` (código C# descompilado da Unity).
  - **Configurações e Tabelas do Jogo:** `e:\server tr\XML\` (arquivos XML originais de armas, heróis, ultimates, mapas, buffs, lojas e balanceamento).

---

## 2. Arquitetura de Rede e Protocolo Customizado

### A. Multiplexação de Portas e Conexões TCP (Porta 7777)
O jogo se conecta por TCP na porta `7777`. Um único servidor gerencia os 3 ciclos de vida de conexão do jogador através do handshake `NGOL`:
1. **Login Socket:** Autenticação de credenciais, validação de versão e hash.
2. **Hall / Lobby Socket:** Interface principal, amigos, loja, chat, inventário, equipamentos, lista de salas personalizadas e fila de Matchmaking.
3. **Scene / Combat Socket:** Conexão de baixa latência para gameplay, movimentação, sincronização de tiro, danos, habilidades, bots e HUD de combate.

### B. Protocolo de Mensagens e Serialização Proprietária
O protocolo do Trigger Run não utiliza JSON puro nem Protobuf padrão; ele usa um protocolo textual customizado em ASCII/Base64:
- **Codificação de Inteiros (`encodeInt` / `decodeInt`):**
  Alfabeto Base64 customizado de 64 caracteres:
  `"0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ_\`abcdefghijklmnopqrstuvwxyz"`
  *Exemplo:* `0` -> `'0'`, `1` -> `'1'`, `13` -> `'D'`, `20013` -> `'4sh'`.
  *(Atenção: Números enviados como strings numéricas cruas são interpretados pelo decodificador Base64 da Unity como dígitos de base 64, o que causa estouro de índice).*
- **Codificação Hexadecimal (`encodeHex` / `decodeHex`):** Usada para strings e nomes UTF-8 (`"king"` -> `'6b696e67'`).
- **Estruturas de Dados:** `ArrayList` serializadas em parênteses aninhados, ex: `(arg1,arg2,(sub1,sub2))`.
- **Roteamento de Entidades:** Prefixos como `e1:`, `e2:`, etc., representam o NetID/PlayerIndex da entidade remetente ou alvo.
- **Canal de Diagnóstico de Crashes da Unity (Opcode 2110):**
  O cliente Unity intercepta suas próprias exceções não-tratadas em `CCheckDelay.cs` e reporta ao servidor pelo opcode `2110`. O servidor decodifica e exibe em tempo real como `[CRASH NA INTERFACE]`, fornecendo stacktraces exatos de C# da Unity para depuração.

---

## 3. Principais Módulos do Servidor (`src/`)

- [`src/socketServer.js`](file:///e:/server%20tr/src/socketServer.js): Núcleo de rede TCP. Gerencia conexões, protocolo de pacotes, login, saguão, salas personalizadas, fila de matchmaking, sessões persistentes (`playerSessions`), containers de salas e roteamento de combate na cena.
- [`src/gameLogic.js`](file:///e:/server%20tr/src/gameLogic.js): Lógica de combate, cálculo de HP/escudos, sistema de mortes e renascimentos, spawn de bases/barreiras (`spawnTdmBases`), modos de jogo (TDM, Control, PVE), geração de pacotes de spawn de jogadores (`10001`/`10094`), bots e controle de autoridade de IA NavMesh.
- [`src/database.js`](file:///e:/server%20tr/src/database.js): Carregamento e indexação dos bancos de dados XML (`XML/`):
  - `heroDazhaoMap` (`dazhao.xml`): Ultimates, custos de energia, efeitos.
  - `weaponsMap` (`weapon.xml`): Armas, taxas de dano, dispersão, munição.
  - `buffsMap` (`buff.xml`): Buffs, debuffs, curas, escudos temporários.
  - `MAP_CONFIG` (`fbset.xml`): Configurações de mapas e modos (TDM tipo 7, Control tipo 3, Treino tipo 6, PVE tipos 9 e 12).
- [`src/utils.js`](file:///e:/server%20tr/src/utils.js): Codificadores/decodificadores Base62/Base64, parseadores de listas, manipuladores de pacotes assíncronos (`sendPacket`, `sleep`).
- [`src/http.js`](file:///e:/server%20tr/src/http.js): Servidor HTTP Express (porta 80). Atua como proxy de CDN para texturas e assets de streaming, endpoints de status `/api/state`, telemetria de crashes e dashboards.
- [`src/dbPersistence.js`](file:///e:/server%20tr/src/dbPersistence.js): Persistência em disco de contas, perfis, ouro, diamantes, cosméticos desbloqueados, níveis e equipamentos.

---

## 4. Sistemas Concluídos e Regras Críticas

### 1. Sistema de Salas Personalizadas ("Criar Sala")
- **Container Isolado (`roomContainers`):** Cada sala possui seu UID numérico, mapa escolhido, lista de 10 slots (Time 1 / Azul nos slots 0–4, Time 2 / Vermelho nos slots 5–9).
- **Independência de Host & Capitão:** Se o criador/anfitrião sair, o próximo jogador é promovido automaticamente a capitão sem fechar a sala.
- **Salas em Combate Visíveis no Saguão (`3306`):** Salas com partida em andamento (`state === 4`) permanecem visíveis na lista de salas públicas com o status "Em Jogo", permitindo entrada tardia ou reconexão.
- **Tolerância de Queda / Reconexão:** Se todos os jogadores caírem ou saírem durante uma partida, a sala **não é destruída imediatamente**; um timer de 45 segundos (`autoDestroyTimer`) aguarda a reconexão.
- **Entrada em Partida em Andamento (JIP / Join-in-Progress - Opcode `3303`):** Se a sala estiver em combate (`state === 4`), o jogador que entrar recebe slot e NetID dedicado, e é transicionado imediatamente para o mapa de combate real via pacotes `90001` + `1304` sem passar pela tela de seleção.

### 2. Matchmaking Inteligente (Partida Rápida - Opcode `80001` / `80002`)
- **Backfill Instantâneo:** Ao clicar em Partida Rápida, se houver qualquer partida em combate (`state === 4`) com vagas livres (em salas de matchmaking ou salas públicas sem senha), o jogador entra **instantaneamente no combate ativo**, equilibrando a equipe com menos jogadores, sem ficar preso na fila esperando um 2º jogador.
- **Fila Coordenada & Balanceamento:** Quando não há partida ativa:
  - Com 2+ jogadores: pareamento rápido em 1.5s.
  - Com 1 jogador solo: aguarda uma janela de 10s para encontrar outro desafiante humano antes de iniciar a partida com bots complementares.

### 3. Seleção de Heróis & Ciclo de Carregamento (Staging 5010)
- **Fase de Staging:** O mapa `5010` é usado como tela de seleção de heróis. Enquanto `socket.waitingForHeroLock === true`, o servidor **não dispara timers de combate, HUD nem spawns de barreiras de TDM**.
- **Flag `socket.isCombatReady`:** Flag essencial que indica que o modelo 3D do herói já foi instanciado e os pacotes de inicialização (`10094`, `10001`, `90008`) foram processados. Broadcasts de movimento (`CP`) e rotação (`CR`) **só são enviados para sockets que já estão com `isCombatReady = true`**, prevenindo falhas de `NullReferenceException` na Unity.

### 4. Modos de Jogo Suportados
- **TDM (Mata-Mata em Equipe - Tipo 7):** Pontuação até 50 abates, HUD sincronizado (`110300`), spawn de bases e barreiras invulneráveis (`boxjidi`), abertura automática das portas após o aquecimento de 15s (`close_open`).
- **Control / Dominação (Tipo 3):** Zonas de captura A/B/C, contagem percentual de dominação e prorrogação.
- **PVE / Co-op (Tipos 9 e 12):** Mapas de masmorras cooperativas com monstros e chefes.
- **Treinamento (Tipo 6 - `bachang_baxi`):** Spawn de NPCs passivos/ativos para teste de dano e alcance de tiro.
- **Bots com IA NavMesh Nativa (Opcode `4010001` / `4010002`):** A navegação por malha poligonal é delegada ao cliente hospedeiro através de autoridade concedida pelo servidor.

---

## 5. Diretrizes de Desenvolvimento e Regras de Segurança
Ao interagir ou modificar este repositório:
1. **Sem Afobação e Precisão Cirúrgica:** Nunca sobrescrever arquivos inteiros nem utilizar scripts automáticos que possam truncar o código. Usar edições cirúrgicas por correspondência exata de bloco.
2. **Backups Obrigatórios:** Antes de qualquer alteração, criar cópia de segurança local em `backups/` e na VPS em `/home/ubuntu/backups/`.
3. **Verificação de Sintaxe Obrigatória:** Sempre rodar `node -c <arquivo>` localmente e remotamente via SSH antes de reiniciar qualquer serviço no PM2.
4. **Preservar Codificação de Protocolo:** Sempre usar `utils.encodeInt(valor)` para números que serão lidos pelo cliente Unity em chamadas de RPC ou tabelas de dados.
