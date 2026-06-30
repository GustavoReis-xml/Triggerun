# Lista de Heróis & Status de Implementação

Este documento lista todos os **24 heróis jogáveis** do Trigger Run extraídos do arquivo [`heroinfo.xml`](file:///E:/server%20tr/zh/heroinfo.xml). Ele servirá como um checklist vivo para documentarmos o funcionamento de suas habilidades, ultimates e bugs associados para corrigirmos um por um.

---

## Tabela Geral de Heróis

| ID | Nome (Original) | Nome (Traduzido/Aprox.) | Classe C# (Client) | Arquétipo / Estilo de Jogo | Habilidades | Ultimate | Status / Bugs |
| :--- | :--- | :--- | :--- | :--- | :---: | :---: | :--- |
| **1003** | 狄安娜 | Diana / Diana Sniper | `Jvjishou` | Atiradora de Elite (Sniper / estilo Widowmaker) | ▢ Pendente | ▢ Pendente | Nenhum erro relatado ainda |
| **1005** | 雷震子 | Lei Zhenzi | `LeiZhenZi` | Mago de Raio / Eletricidade | ▢ Pendente | ▢ Pendente | Nenhum erro relatado ainda |
| **1006** | 刑天 | Xing Tian | `XingTian` | Tank Melee com Escudo (estilo Winston) | ▢ Pendente | ▢ Pendente | Nenhum erro relatado ainda |
| **1015** | 终结者 | Terminator | `Terminator` | Soldado de Assalto (Rifle / estilo Soldier: 76) | ▢ Pendente | ▢ Pendente | Nenhum erro relatado ainda |
| **1016** | 瓦尔基里 | Valkyrie | `Valkyries` | Soldado Voador com Lançador (estilo Pharah) | ▢ Pendente | ▢ Pendente | Nenhum erro relatado ainda |
| **1017** | 木兰 | Mulan | `XingJiLieRen` | Caçadora Estelar (Star Hunter / estilo Hanzo/Genji) | ▢ Pendente | ▢ Pendente | Nenhum erro relatado ainda |
| **1021** | 塔利亚 | Taliya | `Jipusai` | Cigana (Suporte / estilo Symmetra) | ▢ Pendente | ▢ Pendente | Nenhum erro relatado ainda |
| **1022** | 雪女 | Yuki Onna (Mulher das Neves) | `YukiOnna` | Maga de Gelo / Controle de Grupo (estilo Mei) | ▢ Pendente | ▢ Pendente | Nenhum erro relatado ainda |
| **1023** | 祝融 | Zhurong | `Zhurong` | Maga de Fogo / Pyro | ▢ Pendente | ▢ Pendente | Nenhum erro relatado ainda |
| **1024** | 后羿 | Hou Yi | `HouYi` | Arqueiro Imperial (estilo Hanzo) | ▢ Pendente | ▢ Pendente | Nenhum erro relatado ainda |
| **1025** | 玲珑 | Linglong | `LingLong` | Suporte com Portais e Lasers | ▢ Pendente | ▢ Pendente | Nenhum erro relatado ainda |
| **1026** | 爱德华 | Edward / Vampiro | `Vampire` | Vampiro / Roubo de Vida | ▢ Pendente | ▢ Pendente | Nenhum erro relatado ainda |
| **1027** | 应龙 | Ying Long (Sand King) | `Shawang` | Rei da Areia / Controle de Terra | ▢ Pendente | ▢ Pendente | Nenhum erro relatado ainda |
| **1028** | 洛基 | Loki | `LuoJi` | Trapaceiro / Clones e Invisibilidade | ▢ Pendente | ▢ Pendente | Nenhum erro relatado ainda |
| **1029** | 洛丽塔 | Lolita / Alice | `LuoLi` | Defensora / Alice (Empurrões e Escudo) | ▢ Pendente | ▢ Pendente | Nenhum erro relatado ainda |
| **1030** | 荆轲 | Jing Ke | `JingKe` | Assassina Ágil / Invisibilidade e Adagas | ▢ Pendente | ▢ Pendente | Nenhum erro relatado ainda |
| **1031** | 兰博 | Rambo | `_Rambo_` | Atirador de Metralhadora Pesada | ▢ Pendente | ▢ Pendente | Nenhum erro relatado ainda |
| **1032** | 妲己 | Daji | `DaJi` | Maga / Suporte (Barreiras de Proteção) | ▢ Pendente | ▢ Pendente | Nenhum erro relatado ainda |
| **1033** | 莉莉丝 | Lilith | `Lilisi` | Maga de Sangue / Escudos de Sangue | ▢ Pendente | ▢ Pendente | Nenhum erro relatado ainda |
| **1034** | 黄忠 | Huang Zhong | `HuangZhong` | Artilheiro (Canhões / estilo Torbjörn/Bastion) | ▢ Pendente | ▢ Pendente | Nenhum erro relatado ainda |
| **1035** | 哪吒 | Nezha | `NeZha` | Lutador de Lança / Nezha | ▢ Pendente | ▢ Pendente | Nenhum erro relatado ainda |
| **1036** | 美杜莎 | Medusa | `Medusa` | Maga / Petrificação e Veneno | ▢ Pendente | ▢ Pendente | Nenhum erro relatado ainda |
| **1037** | 大卫 | David | `DaWei` | Tank Protetor com Escudo Gigante (estilo Reinhardt) | ▢ Pendente | ▢ Pendente | Nenhum erro relatado ainda |
| **2000** | 逃亡者 | Luo Mo / Fugitivo | `LuoMo` | Fugitivo / Personagem de Combate Tático | ▢ Pendente | ▢ Pendente | Nenhum erro relatado ainda |

---

## Como Usar Este Documento
À medida que formos testando e encontrando bugs em cada herói, atualizaremos a coluna **Status / Bugs** com:
- O comportamento esperado no cliente.
- O pacote ou comportamento que está falhando no servidor (`server.js`).
- A resolução adotada.

> [!TIP]
> Você pode abrir o jogo com o seu `.bat` usando um herói específico para testar suas habilidades e me passar o feedback de qual está com problema para começarmos a correção!
