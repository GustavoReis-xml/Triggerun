# Lamster — Suporte de Vanguarda (LAMSTER-007)

O kit usa a interface nativa do jogo: três slots com atalhos/recargas, medidor central de Q e descrições em F1.

| Comando | Habilidade | Funcionamento |
| --- | --- | --- |
| Esquerdo | Punho Solidário | 120 dano ou 180 cura em aliados; 2,2 m; intervalo 1 s. |
| Direito | Pulso de Inspiração | 360 cura em aliados / 250 dano em inimigos, raio 8 m; +25% dano para si e aliados por 5 s; recarga 6 s. |
| E | Pulso de Contenção | Inimigos em 6 m recebem -35% velocidade por 2,5 s; recarga 12 s. Sem dano ou cura. |
| Shift | Arrancada Tática | +40% velocidade e 30% redução do dano de armas por 3 s; recarga 8 s. |
| Q | Vanguarda Solidária | Si e aliados em 10 m recebem 40% redução do dano de armas por 6 s, remoção de lentidões e +40% velocidade por 3 s. |
| Passiva | Cuidado Compartilhado | Recupera 25% da cura efetiva concedida a cada aliado, até 45 HP por aliado. |

A proteção de Shift e Q usa o maior bônus, sem acumular as porcentagens. Perigos do mapa continuam perigosos. A arrancada acelera o deslocamento; não teleporta nem atravessa paredes.

O Q carrega passivamente e com dano/cura efetivos; vida cheia, autocura, excesso de dano e a própria ultimate não geram carga extra. A ultimate não adiciona cura. Os efeitos em aliados são aplicados no momento da ativação, sem obrigá-los a permanecer numa aura.

## Atualização de outros clientes

Feche o TRIG. Copie `TriggerunCombatHud.dll` do ZIP para `TRIG_Data/Managed`, preservando uma cópia da versão anterior. Abra o jogo novamente para baixar as configurações, ícones e efeitos.

O pacote é para clientes já atualizados deste projeto (Assembly-CSharp SHA256 `79DFA93D5B8C4A8C27BF21C8E1ADA3BDF90ECCE6DA4662A72587867EF0840ED1`). Mantenha as demais DLLs. A DLL principal do jogo não foi modificada nesta tarefa.

## Recursos e limites

Três ícones criados pela ferramenta integrada de imagens; fontes e prompts preservados nesta pasta. Sete efeitos derivados dos recursos nativos, com cores e partículas ajustadas e bundles independentes. Modelos, ossos e controladores de animação foram preservados; os ataques mantêm as animações nativas de soco.

Testes do servidor, dados, compilação e preservação dos outros métodos do cliente foram executados. A validação visual e de jogabilidade no Unity real depende de teste com jogadores. A seleção de alvos do E é por distância no servidor; o campo de contenção alcança inimigos dentro do raio mesmo através de obstáculos.
