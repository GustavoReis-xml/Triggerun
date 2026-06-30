# Dicionário do Protocolo de Rede - Trigger Run (Master Reference)

Este documento serve como um guia completo de referência para todos os IDs de pacotes (NetID), sub-comandos e canais de comunicação utilizados pelo cliente (Unity) e pelo servidor do jogo. Ele foi extraído diretamente das classes C# do cliente (`CNetMsgManager.cs`, `HallUser.cs`, `RoomManage.cs`, etc.).

---

## 1. Estrutura Principal do Lobby (`HallUser.cs`)

O loop principal do lobby interpreta os seguintes IDs de pacotes vindos do servidor:

| ID | Nome / Função | Descrição |
| :--- | :--- | :--- |
| ❌ `1205` | `FindOtherUser` | Resultado de busca de outros usuários por ID/Nome. |
| ✅ `1301` | `Init_Lobby_Data` | Inicializa dados do jogador (riqueza, inventário, conquistas, nível, rank e gemas). |
| ❌ `1302` | `Lobby_Error_Tip` | Exibe uma mensagem de dica/erro vinda do servidor. |
| ✅ `1304` | `Scene_Redirect` | Redireciona o cliente para o Scene Server (conecta na partida/arena real). |
| ✅ `1306` | `Hero_Data_Redirect` | Redireciona para o `HeroDataManager` para controle de heróis e cosméticos. |
| ✅ `1307` | `Fcm_Data_Redirect` | Redireciona para o controle de anti-vício (FCM / Fang Chen Mi). |
| ❌ `1308` | `Hall_To_Web` | Comunicação interna/web do lobby. |
| ❌ `1309` | `Login_Date_Sync` | Sincroniza a data do último login. |
| ❌ `1310` | `Gm_Action` | Executa ações administrativas de GM. |
| ✅ `1313` | `Show_Learn_Msg` | Exibe dicas de tutorial/aprendizado. |
| ❌ `1314` | `WenJuan_Poll` | Exibe formulários de pesquisa de satisfação. |
| ❌ `1315` | `LiBao_Msg` | Mensagens de pacotes de recompensa (Gift Packs). |
| ❌ `1318` | `Force_Quit` | Fecha o jogo exibindo um aviso de desconexão. |
| ❌ `1601` | `Http_Request` | Direciona o cliente a fazer requisições HTTP complementares. |
| ❌ `1604` | `Web_HTTP_Req_1604` | Requisição HTTP inicial de dados de login. |
| ❌ `1607` | `Web_HTTP_Req_1607` | Requisição HTTP para validação/status de entrada. |
| ✅ `3001` | `Radio_Broadcast` | Exibe transmissão global de rádio (alto-falante) no topo da UI. |
| ❌ `3002` | `Init_Up_Msg` | Inicializa parâmetros de sincronização adicionais no login. |
| ❌ `3004` | `Close_Radio` | Encerra ou limpa a transmissão global de rádio. |
| ✅ `3310` | `SetRoomID` | Vincula o ID da sala atual ao lobby do jogador. |
| ❌ `3400` | `Mail_Redirect` | Redireciona para o gerenciador de emails (`MailManager`). |
| ❌ `3450` | `QQ_Gift_Data_1` | Inicializa dados de recompensas/presentes do QQ Games. |
| ❌ `3451` | `QQ_Gift_Data_2` | Sincroniza dados alternativos de privilégios do QQ Games. |
| ❌ `4002` | `Web_Ban_Alert_FT` | Alerta de desconexão por banimento de conta (FT_User). |
| ❌ `4013` | `Web_Ban_Alert_MAC` | Alerta de desconexão por banimento de UDID/MAC. |
| ✅ `10025` | `Update_Money` | Atualiza ouro e diamantes do jogador. |
| ❌ `10096` | `OnChangeNickBack` | Confirma se a alteração de apelido foi bem sucedida. |
| ❌ `10097` | `OnResetChangeNick` | Reseta a janela de interface de mudança de apelido. |
| ❌ `11006` | `GetPlayerOldRewardBack` | Resgate de recompensas acumuladas de sessões anteriores. |
| ✅ `16100` | `Activity_Redirect` | Redireciona para o gerenciador de atividades/eventos (ActivityManager). |
| ❌ `17000` | `Rank_Redirect` | Redireciona para o gerenciador de tabelas de liderança (RankManager). |
| ✅ `20058` | `Time_Sync` | Atualiza a hora do servidor no cliente. |
| ❌ `21002` | `Update_Exp` | Sincroniza e atualiza a experiência (nível) do jogador. |
| ✅ `60000` | `Team_Redirect` | Redireciona para o gerenciador de equipes/grupos (TeamManager). |
| ✅ `80001` | `Match_Redirect` | Redireciona para o matchmaking e preparação (MatchManager). |
| ✅ `100001` | `Sync_Career` | Sincroniza o histórico de carreira / perfil do jogador. |
| ✅ `3010001` | `Init_Settings` | Carrega configurações iniciais salvas (áudio, vídeo, controle). |
| ❌ `3010014` | `Save_Settings_Ok` | Confirmação de salvamento de configurações no servidor. |

---

## 2. Sistema de Customização de Heróis (`1306` -> `HeroDataManager.cs`)

O pacote `1306` envia sub-comandos para gerenciar heróis, suas skins, armas, emotes, sprays e acessórios.

| Sub-ID | Função C# | Descrição |
| :--- | :--- | :--- |
| ❌ `12001` | `SetOpenHeroData` | Atualiza dados dinâmicos de heróis abertos. |
| ✅ `12002` | `InitOpenHeroData` | Inicializa a lista de heróis liberados na memória (Unlock). |
| ❌ `12005` | `OneHeroLvlUp` | Sobe o nível de maestria de um herói específico. |
| ✅ `12006` | `UpdateOneHero` | Atualiza o estado/status de um herói. |
| ✅ `12008` | `HeroUnLockOpen` | Libera/compra um novo herói. |
| ✅ `12009` | `OnChangeUseOk` | Confirmação de troca de Skin ou Arma do herói. |
| ❌ `12010` | `HeroLvlUpAwardTip` | Exibe a notificação de prêmios por nível de herói. |
| ❌ `12011` | `UpdateEquipChip` | Atualiza fragmentos de skins/armas (chips). |
| ❌ `12016` | `OnGiftOpen` | Abre caixa de presentes de heróis. |
| ❌ `12017` | `OnGiftOpenBack` | Retorno do conteúdo recebido da caixa de presentes. |
| ❌ `12018` | `OnSelectHeroUILockEnd`| Destrava a seleção do herói ao expirar o tempo da tela. |
| ❌ `12020` | `InitHeroParts` | Inicializa acessórios / partes customizáveis da arma do herói. |
| ❌ `12021` | `HeroPartsLvlUp` | Melhora o nível de um acessório de arma. |
| ❌ `12023` | `HeroPartsRemove` | Remove um acessório equipado da arma. |
| ❌ `12024` | `HeroPartsOpen` | Desbloqueia uma parte de arma. |
| ❌ `11001` | `InitPlayerFace` | Inicializa emotes / faces do jogador. |
| ✅ `11002` | `OpenPlayerFace` | Desbloqueia uma nova face/emote. |
| ❌ `11003` | `SetPlayerFace` | Equipar emote em um slot rápido. |
| ❌ `11008` | `InitPlayerPaint` | Inicializa sprays (paints) do jogador. |
| ❌ `11009` | `OpenPlayerPaint` | Desbloqueia spray no inventário. |
| ❌ `11011` | `OnGiftOpenPaint` | Abre caixa contendo sprays. |
| ❌ `11012` | `OnGiftOpenPaintBack` | Retorno do conteúdo da caixa de sprays. |
| ❌ `11013` | `OnGiftOpenFace` | Abre caixa contendo emotes. |
| ❌ `11014` | `OnGiftOpenFaceBack` | Retorno do conteúdo da caixa de emotes. |

---

## 3. Sistema de Salas Personalizadas (`20013` -> `RoomManage.cs`)

Responsável pela organização e estado das salas criadas manualmente pelos jogadores.

| Sub-ID | Função C# | Descrição |
| :--- | :--- | :--- |
| ✅ `3301` | `JoinRoom` | Notifica que o jogador entrou em uma sala criada. |
| ✅ `3302` | `OnGameOver` | Limpa os dados de sala ao fim de uma partida personalizada. |
| ✅ `3303` | `OnJoinRoomBack` | Resposta do servidor ao tentar entrar em uma sala específica (Erros e Sucessos). |
| ✅ `3304` | `OnLeaveRoomBack` | Jogador saiu da sala (kicado, voluntário ou disband total). |
| ✅ `3306` | `OnGetRoomPageInfo` | Retorna a lista de salas disponíveis para o painel de busca. |
| ✅ `3307` | `OnRoomInfoBack` | Recebe informações completas da sala atual (jogadores, mapa, senhas, etc.). |
| ✅ `3308` | `OnPlayerChangeBack` | Sincroniza alteração de status de pronto (`Ready`). |
| ✅ `3318` | `OnChangeTeam` | Atualiza a equipe (Blue/Red/Spectator) que o jogador selecionou. |
| ✅ `3320` | `SelectHeroOpen` | Abre a tela de seleção de heróis (início do match). |
| ✅ `3324` | `OnModifyRoomInfo` | O capitão alterou configurações da sala (nome, mapa, modo, limite). |
| ❌ `3325` | `OnGetSomeRoomInfo` | Pede dados parciais de uma lista corta de salas. |
| ❌ `3326` | `OnQuickJoinBack` | Retorno de uma tentativa de entrada rápida em qualquer sala disponível. |
| ❌ `3329` | `OnChangeUserData` | Atualiza dados visuais dos jogadores na sala (Skin, Arma e Borda). |
| ❌ `3331` | `OnUserLeave` | Notifica que um usuário específico desconectou da sala. |
| ❌ `3333` | `OnGameStartErr` | Trata erros ao tentar iniciar o jogo (ex: jogadores não prontos). |
| ❌ `3335` | `OnSetPlayerInfo` | Atualiza dados rápidos de um jogador. |
| ❌ `3336` | `OnJoinMatch` | Redireciona a sala personalizada para buscar partida. |
| ❌ `3337` | `OnGetGroupData` | Retorna dados de grupos / patentes autorizadas. |
| ❌ `3338` | `OnTeamJoinRoomBack` | Equipe inteira entrando em uma sala. |

---

## 4. Matchmaking e Preparação (`20014` -> `MatchManager.cs`)

Lida com a busca de partidas públicas e a transição física para a cena 3D.

| Sub-ID | Função C# | Descrição |
| :--- | :--- | :--- |
| ✅ `80006` | `OnSearchStart` | Inicia animação e contagem de busca de partida rápida. |
| ✅ `80007` | `OnSearchCancel` | Interrompe a busca de partida e fecha a UI de matchmaking. |
| ❌ `80011` | `OnUpMatchTeamNum` | Atualiza contadores visuais de times na fila. |
| ✅ `80012` | `OnSetMatchModelNum` | Atualiza dados de estatística de busca do mapa/modo. |
| ❌ `80013` | `OnMatchOk` | Partida encontrada, aguardando aceitação. |
| ❌ `80016` | `OnCheckEnterFb` | Confirma se o cliente pode carregar o mapa. |
| ❌ `80021` | `OnMatchCheckTip` | Alerta de punição por recusar ou desmarcar busca. |
| ✅ `90001` | `OnMatchStart` | Inicializa carregamento (Scene) do mapa selecionado e envia dados dos oponentes. |
| ✅ `90002` | `OnChangeUserData` | Sincroniza prévia de qual herói cada aliado está escolhendo. |
| ✅ `90004` | `OnEnterGame` | Destrava o carregamento e inicia o player na partida 3D. |
| ✅ `90005` | `UpDataGameData` | Envia atualizações do status da rodada/tempo durante a seleção. |
| ❌ `90006` | `SetUserList` | Atualiza a lista completa de jogadores da partida activa. |
| ❌ `90007` | `OnUserLeave` | Notifica que um jogador saiu/desconectou durante a tela de seleção. |
| ✅ `90008` | `OnEnterGameConfirm` | Confirmação de entrada física na partida tridimensional. |
| ❌ `90009` | `OnShowLoad` | Força a abertura da tela de carregamento de mapa. |

---

## 5. Canais de Chat (`1006` -> `ChatManager.cs`)

O chat do jogo divide-se em canais numéricos (`nPinDao`) para organização das caixas de mensagem:

| Canal ID | Nome (PName) | Canal de Destino / Escopo |
| :---: | :--- | :--- |
| ✅ `1` | `shijie` | **World Chat (Mundial):** Visível para todos os jogadores no lobby. |
| ✅ `2` | `duiwu` | **Team Chat (Equipe/Duo/Trio):** Apenas para membros do grupo atual. |
| ✅ `3` | `zhenying` | **Faction Chat (Facção/Campo):** Chat da guilda ou força aliada. |
| ✅ `4` | `bisai` | **Match Chat (Partida):** Comunicação entre todos na partida ativa (All). |
| ✅ `5` | `siliao` | **Private Chat (Sussurro):** Chat privado direto entre dois jogadores. |
| ✅ `6` | `room` | **Room Chat (Sala):** Apenas para jogadores dentro da sala personalizada atual. |
| ✅ `7` | `guild` | **Guild Chat (Clã):** Visível apenas para membros da mesma guilda. |
| ✅ `8` | `system` | **System Chat (Sistema):** Mensagens do sistema escritas em amarelo. |
| ✅ `10` | `laba` | **Megaphone (Alto-falante):** Transmissão global colorida paga. |
| ✅ `999` | `system` | **Mutes/Alerts:** Mensagens críticas de banimento, silenciamento ou avisos de GM. |

---

## 6. Sistema de Amigos (`60100` -> `FriendManager.cs`)

Coordena a lista de amizade, status online/offline, solicitações e lista de jogadores recentes.

| Sub-ID | Função C# | Descrição |
| :--- | :--- | :--- |
| ❌ `60102` | `RequsttoMe` | Recebimento de uma nova solicitação de amizade. |
| ❌ `60104` | `UpDataFriendInfo` | Atualiza dados gerais de um amigo (nível, clã, avatar). |
| ❌ `60106` | `OnAddFriendRs` | Notificação flutuante de sucesso ao adicionar amigo. |
| ❌ `60107` | `UpDataFriendData` | Atualiza se o amigo está Online, Offline, em Partida ou Ausente. |
| ❌ `60109` | `Add_Del_User` | Confirmação de amigo adicionado, removido ou bloqueado. |
| ❌ `60114` | `OnFindApplyReturn` | Retorno do painel de solicitações enviadas/recebidas. |
| ❌ `60115` | `OnGameAddUnMsg` | Trata erros ao tentar adicionar amigos que não existem. |
| ❌ `60118` | `InitFriendData` | Carrega a lista completa de amigos, pendências e lista negra no login. |
| ❌ `60200` | `OnUpdateFriendLatelyData`| Atualiza a lista de "Jogadores Recentes" com quem você jogou. |
| ❌ `60204` | `UpdateSkyLevel` | Atualiza a patente/elo competitivo dos amigos. |

---

## 7. Modo Team Deathmatch - TDM (`110300` -> `Arena_Score_Message_Handle.cs`)

Coordena o HUD e fluxo de placares em partidas focadas em abates (TDM).

| Sub-ID | Função C# | Descrição |
| :--- | :--- | :--- |
| ✅ `110301` | `Show_Arena_Score` | Instancia a UI de placar TDM no topo da tela do jogo. |
| ✅ `110302` | `Display_Fight_Data` | Abre a tabela geral de pontuações de todos os jogadores (KDA/Dano). |
| ✅ `110303` | `Fight_Start` | Encerra a fase de preparação, remove barreiras e inicia o round. |
| ✅ `110304` | `GotoHall` | Finaliza a partida de vez e desconecta o jogador de volta para o lobby. |
| ✅ `110305` | `Countdown_Data` | Sincroniza contagens regressivas na tela (timers). |
| ✅ `110306` | `Fight_Result` | Envia atualizações de mortes para atualizar o placar (Kills Blue vs Red). |
| ✅ `110307` | `Fight_Prepare` | Inicia o aquecimento / tempo de preparação antes do combate livre. |
| ❌ `110308` | `Round_End` | Notifica o fim do round atual e exibe a equipe vencedora do round. |
| ❌ `110309` | `Round_Start` | Inicializa um novo round no mapa. |

---

## 8. Modo Ponto de Controle - Control (`110100` -> `Scramble_Message_Handle.cs`)

Coordena a captura de zonas e pontuações percentuais.

| Sub-ID | Nome Constante C# | Descrição |
| :--- | :--- | :--- |
| ✅ `110101` | `SEND_SCRAMBLE_CREATE` | Cria e exibe o marcador visual do ponto no mapa. |
| ✅ `110102` | `SEND_SCRAMBLE_COUNTDOWN` | Sincroniza o relógio da rodada e abertura de portas. |
| ❌ `110103` | `SEND_SCRAMBLE_USER_DATA` | Atualiza dados e pontuações dos participantes na UI. |
| ✅ `110104` | `SEND_SCRAMBLE_FIGHT_SCORE` | Atualiza o percentual de captura da equipe Blue/Red (0% a 100%). |
| ❌ `110105` | `SEND_SCRAMBLE_FIGHT_DATA` | Sincroniza status em tempo real (quem está pisando na zona ativa). |
| ✅ `110106` | `SEND_SCRAMBLE_FIGHT_PREPARE` | Prepara a UI de disputa para a rodada seguinte. |
| ✅ `110107` | `SEND_SCRAMBLE_FIGHT_START` | Libera a zona de controle para disputa de captura (Ponto Aberto). |
| ✅ `110108` | `SEND_SCRAMBLE_FIGHT_RESULT` | Notifica a equipe que capturou/finalizou a rodada. |
| ✅ `110109` | `SEND_SCRAMBLE_FIGHT_END` | Fim da partida de controle. Exibe tela de vitória/derrota. |

---

## 9. Sistema de Guildas / Clãs (`91000` -> `GuildManager.cs`)

Responsável por todas as ações administrativas e sociais de clãs de jogadores.

| Sub-ID | Função C# | Descrição |
| :--- | :--- | :--- |
| ❌ `91001` | `OnCreateGuild` | Retorno de sucesso/falha na criação da guilda. |
| ❌ `91002` | `OnDissolveGuild` | Dissolve e remove todos os membros da guilda. |
| ❌ `91003` | `OnAddGuild` | Solicitação para entrar em uma guilda. |
| ❌ `91004` | `OnExitGuild` | Jogador saiu voluntariamente da guilda. |
| ❌ `91005` | `OnKickUser` | Membro foi expulso da guilda por um oficial. |
| ❌ `91006` | `OnUpGuildLv` | Sobe o nível de desenvolvimento da guilda. |
| ❌ `91007` | `OnDemiseGuild` | Passa a liderança da guilda (Capitão) para outro membro. |
| ❌ `91008` | `OnSetRank` | Altera cargos e permissões (Líder, Vice, Ancião, Membro). |
| ❌ `91012` | `Guild_Limit_Tip` | Notifica limite máximo de membros atingido. |
| ❌ `91013` | `OnGuildList` | Envia páginas da lista de guildas ativas para busca. |
| ❌ `91015` | `OnAcceptGuildRequest` | Líder aceita um novo membro que solicitou entrada. |
| ❌ `91016` | `OnRefuseGuildRequest` | Líder recusa uma solicitação pendente. |
| ❌ `91017` | `OnGetGuildRequestList` | Carrega a lista de jogadores querendo entrar na guilda. |
| ❌ `91018` | `OnGuildInfo` | Carrega informações completas da guilda (membros, lema, nível). |
| ❌ `91019` | `OnSaveNotice` | Atualiza o painel de avisos / anúncio da guilda. |
| ❌ `91020` | `OnSetUserGuildInfo` | Associa a guilda ao perfil do jogador. |
| ❌ `91022` | `OnSetGuildAutoJoin` | Liga/Desliga aprovação automática de entrada. |
| ❌ `91023` | `OnInvite` | Envia convite de guilda para um jogador específico. |
| ❌ `91025` | `OnGuildAddActive` | Incrementa pontos de atividade da guilda. |
| ❌ `91027` | `OnUpdateGuildRequestNum` | Atualiza o balão vermelho de notificações pendentes. |
| ❌ `91031` | `OnFindGuildByName` | Filtra guildas com base no nome pesquisado. |
| ❌ `91033` | `OnChangeGuildName` | Altera o nome da guilda (consome consumível especial). |
| ❌ `91034` | `OnGuildTanhe` | Inicia/Vota em impeachment do líder inativo. |
| ❌ `91035` | `OnCheckTanheOk` | Confirma o resultado do impeachment. |
| ❌ `91036` | `SetGuildFuliData` | Carrega os prêmios/benefícios diários da guilda. |

---

## 10. Sistema de Quests e Tarefas (`71000` -> `TaskManager.cs`)

Garante a atualização de metas diárias, conquistas gerais e tarefas de temporada.

| Sub-ID | Função C# | Descrição |
| :--- | :--- | :--- |
| ❌ `71002` | `UpdateTaskData` | Atualiza o progresso atual das tarefas (ex: 2/5 partidas concluídas). |
| ❌ `71007` | `OnTaskRewardTip` | Notifica que uma tarefa foi concluída e entrega os prêmios na tela. |
| ❌ `71008` | `OnTaskTiShi` | Exibe lembrete/alerta de tarefa em andamento. |
| ❌ `71009` | `UpdateChapterData` | Atualiza capítulos de missões da história do jogo. |

---

## 11. Sistema de Caixa de Entrada / Correios (`3400` -> `MailManager.cs`)

| Sub-ID | Função C# | Descrição |
| :--- | :--- | :--- |
| ❌ `3401` | `GetMailDataList` | Carrega a lista de emails ativos no login. |
| ❌ `3403` | `OnReadMail` | Abre e marca o email como lido. |
| ❌ `3404` | `OnAddMail` | Recebe um novo email em tempo real (notificação visual de carta). |
| ❌ `3405` | `OnDelMailBack` | Confirma exclusão de um email específico. |
| ❌ `3406` | `GetMailDrawBack` | Resgata anexo (moedas/chips/skins) de um email específico. |
| ❌ `3407` | `GetUnReadMail` | Solicita contagem de emails não lidos. |
| ❌ `3408` | `GetUserMailStateList`| Atualiza estados em lote dos emails. |
| ❌ `3409` | `OnDelAllMailBack` | Deleta todos os emails lidos e sem anexos. |
| ❌ `3410` | `GetAllMailItem` | Coleta todos os anexos de todos os emails de uma vez. |

---

## 12. Sistema da Loja e Inventário (`70000` -> `ShopManager.cs`)

| Sub-ID | Função C# | Descrição |
| :--- | :--- | :--- |
| ❌ `70005` | `UpdateShopItem` | Atualiza a quantidade e disponibilidade de caixas/itens na loja. |
| ❌ `70006` | `OnLiBaoOpen` | Retorna o conteúdo de uma caixa/pacote aberta. |
| ✅ `70007` | `UpdateItemBuff` | Atualiza boosts de EXP/Ouro buffs ativos na conta. |
| ❌ `70008` | `OnHuiGouData` | Atualiza itens vendidos de volta / recompra de emergência. |
| ✅ `70009` | `OnBuyItemOk` | Confirmação de compra bem-sucedida na loja. |
| ❌ `70010` | `OnLiBaoOpenAll` | Retorna conteúdo da abertura em lote de caixas (abrir 10x). |
| ✅ `70012` | `OnGiveUserItemData` | Notificação recebida de itens dados por admin/evento. |
| ✅ `70018` | `UpdateUserItemData` | Atualiza a quantidade de itens no inventário geral. |

---

## 13. Ranqueada - Sky Ladder (`16200` -> `SkyLadderManager.cs`)

| Sub-ID | Função C# | Descrição |
| :--- | :--- | :--- |
| ❌ `16200` | `InitSkyLadderMatch` | Inicializa dados da ranqueada do jogador (vitórias, derrotas, elo). |
| ❌ `16202` | `InitSkyLadderReward` | Lista de prêmios por patente ao final da temporada. |
| ❌ `16203` | `GetSkyLadderReward` | Confirmação de prêmio de patente coletado. |
| ❌ `16205` | `GetSkyLadderSeason` | Retorna dados e datas da temporada competitiva ativa. |
| ❌ `16208` | `SetSkyRelink` | Lida com reconexão a uma partida ranqueada ativa após queda. |
| ❌ `16210` | `ShowSkyScoreTip` | Exibe a pontuação de elo recebida/perdida ao final do match. |

---

## 14. Campanha PVE (`19500` -> `PveManager.cs`)

Coordenador das missões offline e de co-op cooperativo de aventura.

| Sub-ID | Função C# | Descrição |
| :--- | :--- | :--- |
| ❌ `19501` | `SetPveDataByFbType` | Inicializa dados de progresso no mapa da campanha selecionada. |
| ❌ `19502` | `OnQinagHua` | Fortalecimento/Melhoria de equipamentos do modo de aventura. |
| ❌ `19503` | `InitPveMatch` | Busca fila de jogadores para aventura coop. |
| ❌ `19504` | `OnJinJie` | Evolução/Refinamento de armas PVE. |
| ❌ `19507` | `OnFanPai` | Resgata cartas de prêmio ao final do estágio (sistema de flip-card). |
| ❌ `19511` | `UpdateTiLi` | Sincroniza energia/tili restante (necessário para jogar missões). |
| ❌ `19512` | `PveMatchError` | Trata erros e cancelamentos de matchmaking PVE. |
| ❌ `19513` | `UpdateByTiliNum` | Atualiza os timers de recarga de energia da conta. |
| ❌ `19514` | `OnSaoDangData` | Retorna resultados de "varredura automática" (Instantly Clear stages). |
| ❌ `19515` | `OnSetMaoXianBtnState`| Habilita botões com base no progresso das estrelas. |
| ❌ `19516` | `SetPveDataByStage` | Atualiza o status de conclusão das estrelas de um estágio. |
| ❌ `19517` | `GeneUpdateLose` | Falha na fusão de genes PVE. |
| ❌ `19518` | `SetGeneData` | Sincroniza a árvore de melhorias genéticas (Gene Tree). |
| ❌ `19520` | `PveFirstReward` | Notifica que a recompensa por completar o mapa pela 1ª vez foi resgatada. |
| ❌ `19521` | `UpdatePveFlower` | Flores de suporte / decorações do modo PVE. |

---

## 15. Treinamento e Masmorra (`19000` / `19100` / `19600`)

Lida com os modos Sandbox de teste, boss battle e PVE avançado:

* **Treinamento e Dark Training (`19000` / `19100` -> `KingTrainManager.cs`)**:
  * `19001`: Inicializa o mapa de treino e instancia a UI do modo `King`.
  * `19002`: Encerra o treinamento de volta para o lobby.
  * `19003`: Começo do combate livre no modo treino.
  * `19004`: Fim do round no modo treino.
  * `19005`: Preparação do round / spawn de monstros.
  * `19007`: Atualiza dados dinâmicos do jogador no treino.
  * `19008`: Atualiza a quantidade de monstros vivos restantes no mapa.
  * `19011`: Jogador sai do servidor de treino.
  * `19013`: Novo jogador adicionado à instância do treino.
  * `19014`: Sincroniza se todos os jogadores morreram.

* **Dungeon Mode / Masmorra (`19600` -> `DtsBatteManager.cs`)**:
  * `19004`: Inicializa dados da masmorra cooperativa e do estágio.
  * `19005`: Sincroniza dados da rodada cooperativa.
  * `19003`: Sincroniza spawn e posições dos jogadores.
  * `19007`: Atualiza o KDA e dano de um membro do grupo.
  * `19008`: Coleta ou ativa caixas de cura no cenário.
  * `19009`: Exibe resultados parciais ou finais da dungeon.
  * `19010`: Atualiza o estado das poções/itens ativos.
  * `19011`: Sincroniza drop e coleta de itens especiais do chão.
  * `19012`: Cancela e fecha a UI de masmorra por erro ou time out.

---

## 16. Modo Sobrevivência / Battle Royale (`110000` -> `Arena_Message_Handle.cs`)

Gerencia as mecânicas específicas de arenas de sobrevivência (como a névoa/gás que encolhe a área útil).

| Sub-ID | Função C# | Descrição |
| :--- | :--- | :--- |
| ❌ `110001` | `Show_Arena_UI` | Instancia a interface da rodada de Battle Royale. |
| ❌ `110002` | `Display_Fight_Data` | Abre a tabela de KDA e dados da partida de sobrevivência. |
| ❌ `110003` | `Narrow_Area` | Notifica o encolhimento e deslocamento da zona segura (Gas/Storm Circle). |
| ✅ `110004` | `Fight_Start` | Encerra a contagem de aquecimento e libera o combate. |
| ❌ `110005` | `GotoHall` | Finaliza a partida de sobrevivência e retorna o jogador ao lobby. |
| ✅ `110006` | `Countdown_Data` | Sincroniza os contadores regressivos na tela. |
| ❌ `110009` | `Fight_Prepare` | Inicia o aquecimento antes da rodada começar. |
| ❌ `110010` | `Round_End` | Determina o fim de uma rodada na partida. |
| ❌ `110011` | `Round_Start` | Começo oficial da rodada do BR. |

---

## 17. Sincronização de Combate em Tempo Real (`1010000` / `2010000` -> `Hero_Message_Handle.cs`)

Lida com eventos de física de alta frequência e estados ativos das habilidades no espaço 3D.

| ID / Sub-ID | Função C# | Descrição |
| :--- | :--- | :--- |
| ❌ `1010100` | `Position_Sync` | **Sincronização de Posição:** Atualiza coordenadas do jogador 3D (X, Y, Z divididos por 100f). |
| ✅ `2010002` | `Shield_Data_Sync` | **Atualização de Escudo:** Sincroniza a resistência/vida do escudo ativo de suporte (Taliya/1021). |
| ❌ `2010105` | `Velocity_Force` | **Força de Física:** Aplica empurrões (Knockbacks) ou impulsos de velocidade aos personagens. |

---

## 18. Sincronização Geral de Partida e Eventos da Arena (`CNetMsgManager.cs`)

Estes pacotes são enviados pelo servidor de partida para controlar o fluxo da rodada, spawn de entidades, interações com portas e efeitos visuais no cenário 3D:

| ID | Nome / Função | Descrição |
| :--- | :--- | :--- |
| ✅ `10001` | `Player_Spawn` | Instancia ou atualiza o personagem do jogador na arena 3D com herói e armas selecionadas. |
| ✅ `10094` | `Match_User_Info` | Limpa a UI do lobby e carrega a interface de combate (HUD) antes do spawn. |
| ✅ `2000` | `Interactive_Emote` | Processa interações de emotes/pose e bolhas de chat interativo. |
| ✅ `20001` | `Return_To_Lobby` | Sinaliza que a partida acabou e desconecta o jogador com segurança de volta para o lobby. |
| ✅ `52002` | `Base_Barrier_Sync` | Sincroniza se o jogador está na área segura (jidi/base) para ativar a barreira de cura. |
| ✅ `40001` | `Map_Effect_Begin` | Inicia um efeito visual ou zona específica no mapa (como portal ou ponto de captura). |
| ❌ `40004` | `Map_Effect_End` | Finaliza e remove um efeito visual/área ativa do mapa. |
| ✅ `1030000` | `Door_Control` | Controla abertura/fechamento das portas de spawn do mapa. |
| ❌ `5011` | `PlayerSelect` | Sincroniza seleção de personagem no lobby da arena. |
| ❌ `5031` | `Team_Exit` | Sinaliza que a equipe foi encerrada ou que o jogador saiu. |
| ❌ `5033` | `Player_Hp_Full` | Notifica que a vida do jogador foi completamente restaurada (usado no tutorial). |
| ❌ `9003` | `User_Join_Fb_Log` | Registra e valida o log de conexão do cliente à partida. |
| ❌ `10095` | `Paint_Info_Sync` | Sincroniza a posição e imagem de sprays (paints) desenhados pelas paredes. |
| ❌ `16001` | `System_Chat_Broadcast` | Exibe mensagens automáticas do sistema no chat durante a partida. |
| ❌ `16204` | `SkyLadder_Exit` | Trata a desconexão ou saída de partidas competitivas (ranqueadas). |
| ❌ `19700` | `War_Person_Msg` | Comunicação específica do modo de guerra de guildas / arena personalizada. |
| ❌ `110200` | `Training_Msg` | Mensagens e comandos do modo de treinamento livre da arena. |
| ❌ `302000` | `Prerogative_Msg` | Mensagens de privilégios e buffs especiais durante o combate. |
| ❌ `1020000` | `Npc_Network_Msg` | Distribui atualizações e comportamentos de rede para NPCs/bots. |
| ❌ `2050001` | `LuoJi_Hurt_Accumulate`| Sincroniza o acúmulo de dano do herói Luo Ji. |
| ❌ `3010000` | `InGame_Setting_Sync` | Sincroniza configurações de áudio/gráficos no meio da partida. |

---

## 19. Sub-Comandos do Sistema de Partida (`5000` -> `FBManager.cs`)

O pacote `5000` controla as mecânicas das arenas, instâncias de partida (Dungeons) e modos como disputa de ponto (ZhanDian):

| Sub-ID | Função C# | Descrição |
| :--- | :--- | :--- |
| ✅ `5001` | `ZhanDian_Init` | Inicializa dados visuais e de spawn da disputa de captura de ponto. |
| ✅ `5002` | `Game_Start_Info` | Notifica o início oficial do round e ativa elementos dinâmicos. |
| ✅ `5003` | `Game_One_Over` | Fim de um round intermediário da partida. |
| ✅ `5006` | `Game_Over` | Fim total da partida e redirecionamento dos resultados. |
| ❌ `5037` | `Fb_Error_Kick` | Exibe notificação de erro ou desconexão da instância e retorna ao lobby. |
| ✅ `5008` | `Update_Scrollbar` | Atualiza o progresso visual de captura do ponto no slider da HUD. |
| ✅ `5027` | `Big_Scroll_Point` | Trata eventos e marcadores visuais dinâmicos no ponto ativo. |
| ❌ `5028` | `Timer_Sync_Detail` | Sincroniza tempos detalhados (preparação, travamento, tempo total). |
| ❌ `5013` | `Payload_Cart_Distance` | Atualiza a distância física do carrinho de carga (Payload) até o objetivo. |
| ❌ `5014` | `Payload_Active_Players` | Sincroniza a quantidade de jogadores empurrando/bloqueando a carga. |
| ✅ `5017` | `Payload_Cart_State` | Sincroniza o estado atual do carrinho (andando, parado, voltando). |
| ❌ `5018` | `Round_End_Payload` | Fim do round com base no objetivo de escolta de carga. |
| ✅ `5019` | `Respawn_State_Update` | Sincroniza o estado de ressurreição e timers de spawn dos jogadores. |
| ✅ `5020` | `Round_Start_Payload` | Inicializa round de escolta de carga. |
| ❌ `5021` | `Add_Time_UI` | Adiciona bônus de tempo à rodada e exibe na interface. |
| ✅ `5022` | `Sync_Progress_Bar` | Sincronização secundária e forçada do slider de captura de ponto (TongBuScr). |
| ✅ `5024` | `Sound_Mute_Control` | Liga/desliga reprodução de efeitos sonoros específicos de combate. |
| ❌ `5025` | `Open_Game_Tab` | Abre a tela de estatísticas/KDA (Tab) durante o gameplay. |
| ❌ `5026` | `Start_Result_Screen` | Carrega e exibe a tela de vitória/derrota e dados da rodada (JieSuan). |
| ❌ `5029` | `Force_Quit_Arena` | Desconecta o jogador da partida enviando um alerta do sistema. |
| ✅ `80005` | `Close_All_UI_Select` | Fecha todas as UIs ativas do lobby e abre a tela de seleção de herói em jogo. |
| ❌ `5035` | `Push_Msg_Tip` | Envia e exibe uma notificação/dica flutuante na interface. |
| ❌ `5038` | `Sync_Force_Team` | Sincroniza a força/time atribuída (`nForce`/`m_nShili`) entre cliente/servidor. |

---

## 20. Sistema de Gemas e Síntese (`3020100` -> `Gem_Data.cs`)

Gerencia a organização de gemas no inventário, equipagem nos slots de armas e o sistema de fusão/síntese para aumento de nível.

| Sub-ID | Função C# | Descrição |
| :--- | :--- | :--- |
| ❌ `3020102` | `Update_Package_Gem` | Atualiza a quantidade e o inventário de gemas do jogador. |
| ❌ `3020104` | `Update_Package_Gem_Alt`| Sincronização de alterações no inventário de gemas. |
| ❌ `3020106` | `Gem_Sell_Result` | Retorna o ouro obtido ao vender uma gema do inventário. |
| ❌ `3020108` | `Gem_Equip_Result` | Confirmação de equipagem/remoção de gema em uma arma. |
| ❌ `3020110` | `Gem_Equip_Update` | Atualiza o estado dos slots de gemas do jogador. |
| ❌ `3020112` | `Gem_Synthesis_Result` | Retorna o resultado da fusão de gemas (sucesso/falha e novo item). |
| ❌ `3020114` | `Gem_Synthetic_Refresh` | Atualiza a prévia visual e custos da janela de síntese. |
| ❌ `3020116` | `Gem_Batch_Synthetic` | Retorna o resultado de sínteses de gemas feitas em lote. |
| ❌ `3020117` | `Gem_New_Data_Alert` | Sinaliza que novas gemas foram obtidas no inventário. |
| ❌ `3020119` | `Gem_Success_Rate_Sync` | Sincroniza a taxa de sucesso/probabilidade dinâmica da fusão. |
