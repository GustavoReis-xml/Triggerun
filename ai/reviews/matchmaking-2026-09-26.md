# Auditoria de partidas e entrada tardia — 26/09/2026

Responsável: Codex. Escopo: análise, reprodução isolada e plano de correção. Código de execução e Oracle não foram modificados.

## Conclusão

O relato principal é queda da conexão de quem já estava no TDM quando outro jogador entra; a sala parece ser refeita. Há defeitos reproduzíveis no ciclo de entrada, carregamento e saída, mas a causa exata dessa queda específica ainda não foi capturada com dois clientes na versão atual. Não confundir inconsistências reproduzidas no servidor com crashes executados na Unity.

O caminho normal de entrada tardia no TDM preservou o controlador, placar e tempo dos antigos participantes na simulação. Portanto, não há evidência de que toda entrada execute um reinício global. Existem caminhos de corrida que efetivamente transferem novamente um participante ou recriam o controlador; eles são prioritários, junto com a confirmação de herói antes do carregamento.

## Base verificada

- Commit local: `de0610a8ce01cd13de40fca21729e8ac0d7bd6e6`. Há mudanças locais; o commit não representa sozinho os arquivos analisados.
- Hash SHA256 de `src/socketServer.js`: `b7eef42550d4e4180336048fe32def88a7a1b437af046d0d130de58bce095194`.
- Hash de `src/gameLogic.js`: `db8b17855abdee1dc30f9f54d1ef7592478f2568d43eff8b26f14a21ad07d368`.
- Ambos conferidos com os arquivos da Oracle nesta auditoria. Não houve instalação ou reinício.
- `node --test tests/lamster.test.js tests/match-lifecycle.test.js`: 21/21 passaram.
- `node tools/audit-matchmaking.cjs`: 17 verificações adicionais; 16 invariantes violadas e um cenário normal preservado. Algumas verificações são manifestações relacionadas do mesmo problema, não 16 causas independentes de desconexão.
- Evidência compacta compartilhável: `ai/reviews/matchmaking-2026-09-26.results.json`.

## Contrato do cliente

Referências abaixo estão em `Assembly/Assembly-CSharp/`.

| Etapa | O que o Assembly faz | Consequência para o servidor |
| --- | --- | --- |
| 90001 | `MatchManager.OnMatchMsg`: instala lista/mapa, inicia pré-carregamento e abre seleção de herói | Confirmar herói antecipadamente no servidor não impede a UI de enviar 90002 |
| 1304 | `HallUser.SendMsgToHallClient`: configura destino e chama `ConnectToScn` | É uma reconexão efetiva, não apenas atualização de lista |
| Reconectar cena aberta | `UserSocket.ConnectServer`: chama `OnReturnHallOK` antes de reconectar se a cena ainda está aberta | Um 1304 atrasado ou repetido pode provocar retorno ao lobby e reconstrução da cena |
| Mapa pronto | `GameRoot.OnSceneCreate` → `CReadModel.finishRead` → `BaseSocket.LoadMapOKMsg` → RPC `SLoadMapOKMsg` | Texto de diagnóstico não substitui a confirmação de carregamento |
| Diagnóstico loadmapok | `BaseSocket.LoadMapOKMsg` escreve o valor de `bGoToHall`; só envia o RPC quando esse valor é falso | `loadmapok:True` significa caminho de retorno ao Hall, não autorização de spawn |
| 10094 / 10001 | `HeroPlayer.MatchUserInfo` inicializa informações; `PlayerCreate` cria o personagem | Preservar ordem e não publicar um participante ainda em transição |
| PlayerSelect remoto | `GamePlayer.RemoteCreate` → `NetLiving.InitPlayerInfo` → `CreatePlayer` | Snapshot precisa conter vida/estado/modelo e dados coerentes |
| Estado de morte | `NetLiving.InitPlayerInfo`: `isDead = msg2[0] == 0` | HP zero sozinho não informa ao cliente que o personagem está morto |
| Lista | `UserData.InitUserData` usa índice 14 como NetID; `MatchManager.SetUserList` atualiza os usuários | Lista, entidade e sessão precisam concordar sobre identidade e time |
| Retorno ao Hall | `fGameManager.OnReturnHallOK` fecha cena, limpa estado local e envia 1310 pelo Hall | O servidor precisa reconciliar esse aviso sem depender exclusivamente do fechamento TCP |

## Defeitos reproduzidos

As linhas são as da base acima. Cada ID corresponde ao resultado do diagnóstico.

| Prioridade | ID / evidência | Local e correção proposta |
| --- | --- | --- |
| Alta | `queued-then-jip`: jogador entra na sala 1000, mas a fila antiga o move para 1001 e deixa a inscrição em 1000 | `socketServer.js:541`, `:1258`, `:1623`. Toda admissão deve cancelar a busca anterior e ter uma geração de entrada; o processamento precisa revalidar inscrição atual |
| Alta | `queue-redirect-after-leave`: 1304 é enviado depois de limpar o estado e retornar ao Hall | `socketServer.js:675`. Callback do matchmaking normal verifica apenas `destroyed`; invalidar sequência inteira e destino original, inclusive após cada espera |
| Alta | `hero-lock-before-load`: cliente ainda não pronto envia 90002; servidor publica `cGamePlayer` aos antigos e envia 90008 ao novo antes de `SLoadMapOKMsg` | `socketServer.js:1313`, `:1718`, `:5822`; `gameLogic.js:1078`. Separar seleção inicial, carregamento e troca em combate. Antes de pronto, guardar escolha; não usar a troca em combate |
| Alta | `ended-during-load`: executado `endTdmMatch` durante carregamento; o timer depois cria outro controlador em warmup, com sala em estado 5 | `socketServer.js:3369`; `gameLogic.js:1372`, `:1434`, `:1782`. Validar identidade/geração e estado da partida no momento do spawn; nunca criar controlador a partir de uma admissão obsoleta |
| Alta | `duplicate-backfill`: repetir a busca inscreve a mesma pessoa em dois slots | `socketServer.js:1276`. Entrada precisa ser idempotente por identidade/sessão; reutilizar admissão atual, sem outro redirecionamento |
| Alta | `leave-ghost`: saída por 20001 seguida de fechamento TCP não envia destroy ao outro jogador | `socketServer.js:3683`, `:7428`. A limpeza marca `sceneRetired`, e o close deixa de notificar. Despublicar a entidade e a lista antes de invalidar, exatamente uma vez |
| Alta | `hall-return-1310`: aviso real do cliente mantém `inMatch`, sala e slot | `socketServer.js:1191`: comentário cita 1310, condição só aceita 20001/20002. Reconciliação deve reconhecer 1310 e distinguir aviso atrasado de nova entrada |
| Média | `diagnostic-as-ready`: texto dentro de 9002 dispara timer de spawn | `socketServer.js:3298`. Fazer parsing do RPC exato; remover decisão por `includes('loadmapok:True')` |
| Média | `incumbent-roster`: antigo recebe spawn do novo sem atualização 90006 nessa entrada | `socketServer.js:3402`; `MatchManager.cs:SetUserList`. Publicar lista coerente antes da entidade; não reutilizar cegamente o helper que também emite `OnFightStat` |
| Média | `dead-snapshot`: jogador morto é enviado com HP 0, mas estado de vida 1 | `gameLogic.js:1024`; `NetLiving.cs:441`. Serializar `isDead` e estado atual, sem ressuscitar visualmente o participante |
| Média | `late-tdm-retry`: após aposentadoria da cena, callback de 500 ms ainda envia dois pacotes | `gameLogic.js:1425`. Capturar geração de cena e partida também nos callbacks de inicialização |
| Alta, outros modos | `cross-mode-backfill`: busca PvP entra em sala PvE já criada | `socketServer.js:1258`. Validar modo efetivo, estado e capacidade; `state === 4` não basta |
| Alta, outros modos | `mixed-queue`: pedidos PvP e PvE viram a mesma partida, usando o primeiro pedido | `socketServer.js:554`. Particionar fila por critérios compatíveis e limitar grupo à capacidade |
| Média | `team-slot-mismatch`: fila alterna times, mas ocupa slots sequenciais; slot 1 tem força 2 e é contado como time 1 | `socketServer.js:604`, `:624`, `:1279`, `:405`. Uma fonte única para composição dos times e ocupação |
| Alta, PvE | `pve-jip-reset`: entrada restaura cristal de 1234 para 10000 HP e reenvia 19001 ao antigo jogador | `gameLogic.js:2776`, `:3050`. Separar criação da partida de sincronização do participante; snapshot de cristal/monstros e fase atual só ao novo |
| Alta, Control | `control-room-isolation`: salas 1000 e 1001 compartilham o mesmo controlador | `gameLogic.js:2087`. Controlador por sala; atualizar pontuação, zona, timers e limpeza dentro desse escopo |

O teste positivo `normal-tdm-preserves-match` manteve o mesmo controlador, placar 12 e tempo 321, sem pacote de reinício para o antigo cliente. Ele impede atribuir todo o relato a um reinício incondicional inexistente nesse caminho.

## Registros coletados

- Log local histórico: `server_log.txt`, 226134 linhas na leitura. Varredura integral das assinaturas de erro: inclui `PlayerSelect → GamePlayer.CreatePlayer`, `CB → CWeaponControlIt.BulletNumberChange`, `SendMsgToAllClient` e áudio. São registros de 02/09; não validam a versão atual.
- Log Oracle copiado: `scratch/jip-audit-20260926/oracle-out.log`, 51935 linhas na leitura. Contém diferentes sessões/versões; a data da última modificação não é a data de todas as ocorrências.
- Exemplo histórico relevante: linhas 16848–16938, entrada em sala seguida de `CR`/`CP` com referência nula antes do spawn concluído. Os relatos trazem 10/09/2026. Linhas 20204–20256 mostram entrada, retorno e `ECONNRESET`, mas não provam qual evento iniciou a falha.
- `GamePlayer.CP` e `CR` acessam `UserControl`; entidade incompleta é compatível com esses erros. Isso é compatibilidade de mecanismo, não prova da causa da ocorrência atual.
- Consulta atual `/api/crash-logs`: 16 entradas, todas avisos HTTP 403 de recursos; não trouxe stack de desconexão recente. Arquivo: `scratch/jip-audit-20260926/oracle-crashes.json`. Não atribuir automaticamente o TDM aos 403.
- Erros do PM2 copiados para `scratch/jip-audit-20260926/oracle-error.log`. A chave SSH temporária foi removida; a original não foi alterada.
- Os arquivos em `scratch/` não são sincronizados pelo Git. O resultado compacto acompanha esta análise; compartilhar trechos brutos explicitamente quando outra máquina precisar deles.

## Ordem de correção recomendada

1. **Admissão única por jogador:** centralizar fila, entrada pela lista e backfill. Uma geração de admissão, sala/partida de destino fixa e cancelamento de todas as ações anteriores. Deduplicar tentativas e não enviar 1304 novamente a uma admissão já concluída.
2. **Carregamento distinto de combate:** validar o RPC de mapa pronto; guardar escolha inicial de herói durante carregamento; revalidar partida antes de criar personagem. Sincronizar lista/snapshot e só então habilitar eventos pertinentes. O protocolo 9004 informa criação de modelo, mas contém também valor -2 e não deve ser tratado como ACK universal sem especificar esses casos.
3. **Saída simétrica:** remover publicação nos peers, membros do controlador, slot e sessão; invalidar timers e processar retorno ao Hall sem derrubar uma sessão nova por mensagem atrasada. Testar ambos os canais e as duas ordens de chegada.
4. **Depois, demais modos:** separar Control/PvE por sala e corrigir snapshots vivos de objetos. Não embutir essa reestruturação inteira no primeiro patch de TDM.

Cada etapa deve acrescentar testes de regressão no harness e preservar os 21 testes atuais. O diagnóstico é uma ferramenta exploratória: reaproveita o harness por extração de texto e falha explicitamente se a estrutura esperada mudar. Ao implementar correções, mover os cenários relevantes para testes normais, sem depender dessa extração permanentemente.

## Aceitação com clientes reais

- A permanece jogando; B entra por matchmaking e, em outra execução, pela lista de salas.
- A mantém conexão, cena, NetID, HP, posição, placar e tempo. Não recebe `mHeroPlayer`, `CLoadScene`, 90001 ou 1304 da entrada de B.
- B carrega lentamente e confirma herói durante a transição; não provoca troca prematura nem republicação indevida nos antigos clientes.
- Repetir pedido, cancelar busca antes do redirecionamento, sair/reentrar e terminar a partida durante o carregamento.
- Capturar os dois canais por jogador com identificador de conexão, sala, geração e horário UTC; registrar quem enviou o primeiro fechamento e o primeiro erro, não somente `ECONNRESET` ao final.
- Se aparecer `d<NetID do próprio receptor>` ou substituição do jogador local por `cGamePlayer`, rastrear alocação de IDs. Há contadores/fallbacks distintos no código, mas colisão não foi confirmada como causa deste relato.

Próxima entrega proposta: patch local das etapas 1–3 com regressões, seguido de execução com dois clientes. A auditoria não declara a desconexão resolvida.
