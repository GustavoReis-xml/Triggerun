# Ciclo de partidas e diagnóstico

Implementação inicial: 03/10/2026. Estados por conexão em src/matchLifecycle.js; sala e sessão persistente continuam com seus responsáveis atuais. As conexões Hall e Scene de uma conta têm IDs diferentes, correlacionadas por player/room/runId.

| Fase | Entrada | Saída esperada |
| --- | --- | --- |
| connected | conexão TCP aceita | lobby ou seleção/carregamento da cena |
| lobby | Hall identificado ou partida limpa | queued ou selecting |
| queued | pedido de matchmaking | selecting/loading, ou lobby ao cancelar |
| selecting | admissão PvP / sala / JIP | loading ao confirmar herói |
| loading | confirmação ou conexão sem seleção pendente | playing após mapa pronto e snapshot |
| playing | spawn concluído | leaving ou ended |
| leaving | pedido de saída/limpeza | lobby no Hall, closed na cena |
| ended | TDM terminou | saída; callbacks de carregamento invalidados |
| closed | conexão encerrada ou aposentada | terminal para gameplay nessa conexão |

As transições centralizam flags de seleção e prontidão. É uma migração gradual: sala/membership e modos Control/PvE ainda têm lógica antiga; o módulo não substitui integralmente esses controladores. Conexão destruída/aposentada não retorna a selecionar/carregar/jogar. Encerrada não retorna diretamente a playing.

## Contratos do cliente confirmados no Assembly

- MatchManager.OnMatchMsg(90001): prepara lista e seleção; não equivale a mapa pronto.
- SelectHeroUICon/MatchManager: 20014/90002 carrega heroId e locked; 20014/90004 sinaliza OnEnterGame ao expirar contador.
- MatchManager.OnShowLoad: 90004 do servidor inicia carga de modelos lendo roster; roster deve refletir herói confirmado antes desse envio.
- BaseSocket.LoadMapOKMsg: 9002 é diagnóstico; loadmapok:True informa bGoToHall, não prontidão. Apenas RPC SLoadMapOKMsg entra no caminho de spawn.
- fUIManager.GotoHall / CNetMsgManager: saída20001 exige confirmação20001; Hall20002 pode chegar primeiro. Hall1310 informa retorno já concluído.
- sceneScope.capture/invalidate conserva geração de cena e cancela timers de carregamento/respawn. Fim de TDM invalida também jogadores ainda carregando.

## Logs persistentes

- logs/ai_telemetry.log: JSON por linha, arquivo ativo10MiB + cinco arquivos anteriores (.1 mais recente, .5 mais antigo). Reinícios acrescentam SERVER_START com runId único; não apagam o arquivo.
- server_log.txt: ativo50MiB + três anteriores. Mantém saída textual do servidor. Arquivos legados maiores podem aparecer no primeiro arquivo rotacionado; vão sair pela retenção normal.
- Eventos: MATCH_TRANSITION, MATCH_TRANSITION_REJECTED, CONNECTION_OPEN/CLOSE/ERROR, MAP_READY, HERO_SELECTION, SERVER_ERROR e CLIENT_ERROR.
- Contexto: tUTC, runId, seq, connectionId, player, playerId, channel, room, map, epoch, phase, previous/next/reason.
- Erros exibidos no painel ainda usam buffer em memória; o histórico persistente está na telemetria. Limpar o painel não apaga o arquivo.
- Movimento não é adicionado à telemetria por esta mudança. Registros estruturados maiores que64KiB viram resumo marcado truncated, preservando contexto básico. Falha de disco não interrompe o jogo; avisa stderr no máximo uma vez/minuto por escritor.
- Rotação pressupõe um processo escritor (PM2 fork atual). PM2 mantém seus próprios logs: estes limites são dos dois arquivos da aplicação.

Consulta local ou na Oracle, na raiz do projeto:

```text
node tools/trace-player.cjs --player king --limit 200
node tools/trace-player.cjs --room 1007 --dir logs
node --test tests/logging.test.js tests/lamster.test.js tests/match-lifecycle.test.js
```

A consulta lê os arquivos retidos por streaming, ignora cabeçalhos antigos/linhas incompletas e mostra os últimos eventos correspondentes. Para reproduções Unity, guardar um output_log por processo (-logFile) e anotar conta, horário e sala.
