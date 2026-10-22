# Evidências que economizam investigação

## O que existe, confirmado no código em 26/09/2026

- `Assembly/Assembly-CSharp/BaseSocket.cs`, método `LogCallback`: escuta `Application.logMessageReceived` e envia `LOG:` pela conexão ativa; erros/exceções incluem stack. Depende de conexão e estado de login. Isso não comprova que o cliente instalado use exatamente esse binário.
- `src/socketServer.js`: reconhece linhas `LOG:` e escreve `[CLIENT LOG]` no console. O stack é multilinha; investigar sua preservação completa antes de assumir que todos os detalhes chegaram. Não há confirmação nesta análise de defeito causado por esse caminho.
- `src/logger.js`: `logs/ai_telemetry.log` é sobrescrito na inicialização. Possui cabeçalho de texto seguido de linhas JSON; não tratar o arquivo inteiro como JSONL puro. A busca atual encontrou chamada de telemetria de spawn de bot, não cobertura completa da partida.
- `src/http.js`: `/api/client-crash` aceita mensagem/stack; `/api/crash-logs` consulta memória. Nesse caminho de inclusão há limite de 50 entradas e horário local sem data. Reiniciar perde o conteúdo. A existência do endpoint não prova que o cliente o chama.
- `src/packetSniffer.js`: inspeciona pacotes e usa `data/packet_map.json` quando disponível; confirmar associação entre captura e sala. `/api/packet-stream` oferece fluxo ao vivo, não arquivo histórico garantido.

## Captura mínima por ocorrência

1. Crie um identificador único, por exemplo `JIP-001-20260926T194500Z`. Anote horário UTC e diferença conhecida dos relógios cliente/servidor.
2. Registre versões/hashes do servidor e cliente, CRC dos XMLs envolvidos, modo, mapa, sala, heróis e passos exatos. Não invente campos que a captura não fornece: use `desconhecido`.
3. Preserve os logs antes de reiniciar. Copie os arquivos já existentes; exporte a lista de crashes e o trecho correspondente do console/PM2. Localize o log do cliente real antes de orientar coleta por um caminho presumido.
4. Colete uma janela curta antes e depois da falha (por exemplo 30s antes e 15s depois). Inclua os dois clientes quando o problema envolve entrada tardia. Mantenha os arquivos brutos fora de `ai/`, por exemplo em `scratch/evidence/<id>/`, e registre caminhos e hashes. Essa pasta é ignorada pelo Git; arquivos locais não chegam automaticamente à outra máquina.
5. Entregue à IA a primeira exceção completa, a sequência curta de eventos relevantes, estado da sala antes/depois e resultado esperado/observado. Vídeo curto ajuda quando a interface fica presa sem exceção; não substitui o log.

## Instrumentação proposta, ainda não implementada

- Persistir eventos com rotação e limite de tamanho, sem truncar a sessão anterior; controlar volume de movimento/heartbeats.
- Para eventos relevantes, registrar `timestampUtc`, `runId`, `event`, `connectionId`, `roomId`, `sceneEpoch`, `mapId`, `heroId`, `opcode` e resultado. Campos só quando disponíveis; usar nomes/pseudônimos consistentes para correlacionar as capturas.
- Priorizar entrada/saída/carregamento, criação/destruição de jogador, transição para lobby, cura/dano rejeitados e exceções. Registrar motivo de rejeição evita adivinhar por que uma habilidade não funcionou.
- Diferenciar ordem de recebimento no servidor de horário informado pelo cliente. Preservar mensagem e stack como um evento completo.
- Só criar coletor automático depois de confirmar onde o cliente executado grava os logs e quais caminhos já chegam ao servidor.

## Modelo de incidente

Copie os campos abaixo para a passagem da tarefa ou um arquivo específico quando houver uma ocorrência real:

```text
ID / horário UTC / responsável:
Versões e hashes / CRC / ambiente:
Modo / sala / mapa / jogadores-heróis:
Passos para reproduzir:
Esperado / observado:
Primeiro erro e sequência relevante:
Arquivos de evidência e hashes:
Fato confirmado:
Hipótese ainda não comprovada:
Próximo teste que diferencia as hipóteses:
```
