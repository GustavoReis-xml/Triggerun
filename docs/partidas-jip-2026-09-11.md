# Correções de partidas e entrada tardia — 11/09/2026

Status: implementadas e instaladas na Oracle em 11/09/2026, às 20:56 UTC (17:56 de Brasília), após autorização do usuário. PM2 `triggerun` reiniciado e online.

## Problema relatado

Ao entrar em uma partida em andamento, o jogo fica inconsistente, retorna ao lobby e ainda é tratado como participante pelo servidor. Ações posteriores provocam novas falhas. Não foi reproduzido com dois clientes Unity nesta sessão.

## O que foi corrigido

- Roteamento de combate, dano, buffs, movimento e criação de jogadores filtrado por sala e mapa, sem fallback de sala vazia para todos os jogadores.
- Clientes em carregamento não recebem os eventos de combate desses caminhos.
- Entrada tardia pela lista e pela Partida Rápida usa a lista atual de participantes da própria sala e o mapa final. O herói aparece confirmado nos dados de entrada.
- O mapa 5010 também pode ser um mapa real de combate; sua identificação isolada não força mais a seleção inicial.
- Identificadores numéricos de jogadores nas mensagens modificadas são codificados no alfabeto do cliente. O interceptor respeita os pacotes de criação preparados para cada destinatário.
- Snapshot de entrada conserva HP atual e coordenadas zero dos jogadores existentes.
- Confirmações duplicadas de carregamento não iniciam vários spawns. Timers de carregamento são invalidados ao sair ou trocar de cena.
- A saída identifica a partida antes de apagar o vínculo com a sala. Conexões antigas ficam inativas e não podem regravar a sessão do lobby ou remover uma conexão substituta.
- A busca de uma partida TDM inexistente não usa outra partida global como fallback.
- Mensagens TCP são reconstruídas pelo delimitador CRLF, inclusive quando fragmentadas, agrupadas ou na transição de criptografia do NGOL.
- NPCs de habilidades recebem vínculo de sala. NPCs de treino possuem IDs distintos por sessão.

## Referências do cliente

- `Assembly/Assembly-CSharp/BaseSocket.cs`: criação, destruição e RPC consultam IDs via `GetFromInt64`; `SendMessageStr` termina mensagens com CRLF.
- `Assembly/Assembly-CSharp/HeroPlayer.cs`: `MatchUserInfo` (10094) inicializa dados; `PlayerCreate` (10001) cria o jogador local.
- `Assembly/Assembly-CSharp/GamePlayer.cs`: `PlayerSelect` cria os jogadores remotos.
- `Assembly/Assembly-CSharp/MatchManager.cs`: estrutura dos pacotes 90001, 90002 e 90008.
- `Assembly/Assembly-CSharp/PlayerCommon.cs`: HP atual/máximo e demais atributos recebidos no spawn.

## Verificação

`node --test tests/match-lifecycle.test.js`: 13 testes, cobrindo salas distintas no mesmo mapa, entrada tardia por ambos os caminhos, saída durante carregamento, atraso de mensagens, reconexão/saída, NetID 10+, NPCs de treino e mensagens TCP.

`node scratch/smoke-match-server.js`: inicialização dos módulos reais e XMLs, resposta HTTP de estado e abertura TCP verificadas em portas locais temporárias. O teste usa um log próprio e não inicia uma partida.

Verificação de sintaxe dos arquivos JavaScript modificados.

## Limites da validação

Os testes de partidas simulam conexões e temporizadores, com dados de jogo simplificados. Não executam a renderização, o carregamento de modelos ou a interface da Unity. Os logs antigos encontrados não provam a causa específica da ocorrência atual. A correção precisa de confirmação com os clientes reais.

A revisão e os testes de estado de partida se concentraram em TDM. Control e PvE ainda contêm controladores globais anteriores a esta alteração; isolamento completo de duas instâncias desses modos e sincronização de todas as habilidades/NPCs já ativos na entrada tardia não foram certificados.

## Arquivos e backups

Modificados: `src/socketServer.js`, `src/gameLogic.js`, `src/utils.js`.

Adicionados: `src/sceneScope.js`, `src/clientStream.js`, `tests/match-lifecycle.test.js`.

Backup local anterior: `backups/match-jip-20260911/`.

Backup na Oracle anterior: `/home/ubuntu/backups/match-jip-20260911-before.tar.gz`.

Backup completo imediatamente anterior à instalação: `/home/ubuntu/backups/match-jip-20260911-preinstall-full.tar.gz` (src, index.js e package.json). A produção já continha uma versão intermediária das correções; as diferenças foram revisadas antes da instalação final.

Os 13 testes passaram também na Oracle, com Node 18.19.1, e a sintaxe dos cinco arquivos de produção foi validada antes do reinício. Os arquivos instalados conferem byte a byte com a versão candidata. Após o reinício, `/api/state` respondeu normalmente e a conexão TCP na porta 7777 retornou `B1A_6CDA_GEMINI`. A confirmação da entrada tardia com clientes Unity reais continua pendente.
