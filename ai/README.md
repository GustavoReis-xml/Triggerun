# Memória compartilhada — Trigger Run

Esta pasta é o ponto de passagem entre Codex e Antigravity. A comunicação acontece quando cada ferramenta lê e atualiza os arquivos; não existe troca automática de mensagens entre os aplicativos. Ambos precisam abrir a mesma cópia do projeto ou receber estas alterações por sincronização explícita.

## Leitura mínima

1. `STATE.md`: fatos, limites e mapa do projeto.
2. `TASKS.md`: escolha e reserve uma tarefa dentro do pedido do usuário.
3. As referências dessa tarefa, ampliando a leitura para arquivos completos do Assembly e logs inteiros quando isso ajudar a precisão. Consulte `EVIDENCE.md` para investigar falhas.

## Fluxo econômico

Precisão e eficiência têm prioridade sobre velocidade. Não restringir a leitura necessária por custo presumido; tanto Codex quanto Antigravity podem examinar código e logs completos quando relevantes. Economizar significa evitar trabalho repetido e hipóteses sem evidência, não trabalhar com contexto insuficiente.

- Use o modelo mais econômico que consiga executar uma tarefa delimitada: localizar símbolos, implementar uma mudança definida, executar verificações e registrar resultados.
- Use Codex para causa raiz ambígua, interpretação do protocolo/Assembly, decisões que atravessam cliente e servidor e revisão das mudanças de maior impacto. Não precisa revisar cada ajuste de texto.
- Antes de editar, escreva objetivo, arquivos previstos, critério de aceitação e responsável em `TASKS.md`. Uma tarefa pequena deve resolver um comportamento observável.
- Uma IA por conjunto de arquivos sobrepostos. A reserva é um acordo, não um bloqueio automático. Se outra tarefa usa o mesmo arquivo, coordene antes de modificar; não considere uma reserva abandonada só pela idade.
- Registre o commit base quando disponível e os hashes/cópias dos arquivos afetados se houver mudanças locais. Não limpe o repositório nem reverta arquivos para obter uma base limpa.
- Busque o método do cliente que envia/recebe a mensagem; confirme a estrutura no servidor e os dados XML. Use a web quando faltar documentação externa relevante, sem substituir o comportamento observado no jogo por suposições.
- Teste uma hipótese por vez. Se duas tentativas falharem pela mesma razão, registre evidências e reformule a hipótese antes de insistir.
- Ao terminar, atualize o estado da tarefa e entregue um resumo no formato de `templates/HANDOFF.md`. Atualize `STATE.md` somente se mudou um fato duradouro.
- Não repita testes que já passaram sem mudança relevante. Não trate teste simulado como confirmação em dois clientes reais.

## Formato e tamanho

Markdown curto para decisões e passagens; logs brutos ficam fora desta pasta. Não precisamos de linguagem secreta: nomes claros, caminhos e evidências curtas reduzem ambiguidade e custo. Mantenha cada passagem em aproximadamente 30 linhas e o estado em até 100 linhas. Quando houver histórico demais, arquive a passagem concluída e mantenha um link; não releia o arquivo histórico inteiro a cada sessão.

Não monte agora um orquestrador, banco vetorial ou chat entre agentes. Primeiro use este fluxo; automatize somente o trabalho repetitivo comprovado.

## Prompt para a outra IA

> Leia AGENTS.md, ai/README.md e ai/STATE.md. Consulte ai/TASKS.md e trabalhe apenas na tarefa que eu indicar. Antes de editar, registre responsável e arquivos previstos. Ao finalizar, informe evidências, testes, limites e próximo passo usando ai/templates/HANDOFF.md. Não repita análises já documentadas sem apontar qual fato mudou.
