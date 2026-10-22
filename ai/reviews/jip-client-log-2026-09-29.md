# Passagem — TDM-002: evidência do teste real

- Responsável: Codex; 29/09/2026, teste com timestamp servidor 30/09 01:25 UTC (29/09 22:25 local).
- Escopo: leitura do cliente e Oracle; nenhum código alterado, nenhuma instalação/reinício.
- Evidência preservada: scratch/jip-log-20260929/output_log.txt (origem C:/trig2/GAME/TRIG_Data/output_log.txt), oracle-tail.txt.
- Versão remota confirmada por SHA256: socketServer b7eef42550d4e4180336048fe32def88a7a1b437af046d0d130de58bce095194; gameLogic db8b17855abdee1dc30f9f54d1ef7592478f2568d43eff8b26f14a21ad07d368. É a base ANTERIOR à correção local. Este teste não valida nem refuta o patch local.
- Cliente linhas 693/725: 90001/90002 de king2 trazem confirmação index9=1; mapa sidou05, 5212, seleção 60s.
- Cliente linhas 801–807: redirecionamento 1304, mHeroPlayer:1, 90005; nenhum 90004 ou CLoadScene posterior nesse ingresso.
- Oracle linhas 2418–2471: king2 admitido na sala1007/time2/slot5, identificado por NGOL com ID1; oito mensagens 20014/90002/1015/0; nenhuma confirmação1 nesse ingresso.
- Assembly SelectHeroUICon.OnQueDingBut: confirmado1 + troca permitida1 faz botão enviar0 (desbloqueio); sem retorno à própria seleção o botão repete0.
- Servidor remoto filtra a resposta da prévia por canReceive, excluindo o entrante ainda não pronto. A resposta aparece para o jogador já pronto (e2), não constitui prova de atualização da UI do entrante (e1).
- Cliente registra mensagens de king ID2 e king2 ID1 intercaladas. Tratar como captura com eventos de ambas as contas; não inferir colisão no servidor apenas por essa mistura.
- Oracle diferencia as sessões de cena: king ID2, king2 ID1, mesmo IP. Não há evidência de identificação trocada neste ingresso. Fallbacks por IP existem no código e continuam merecendo testes, mas não são a causa demonstrada aqui.
- O trecho desta tentativa não contém exceção de interface; ECONNRESET no servidor aparece depois dos pedidos de saída. Não usá-lo como causa inicial do bloqueio.
- Não há OnEnterGame/90004 registrado depois desse ingresso no trecho capturado; o efeito do contador relatado não pode ser confirmado por este log. O código remoto continua sem tratamento de90004 e sem fallback JIP.
- Testes: não repetidos, pois nenhum runtime foi alterado nesta análise; patch local mantém registro anterior de29 testes aprovados.
- Próximo passo: instalar patch local já documentado, preservando backup/logs remotos, e repetir teste com contas distintas e logs separados por processo (-logFile) para distinguir destinatários. Validar manual e contador; não afirmar correção no cliente antes disso.
