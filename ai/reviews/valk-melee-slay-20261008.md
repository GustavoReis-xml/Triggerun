# Passagem — VALK-001 dano físico e Slay

- Responsável: Codex,08/10/2026. Estado: instalada no cliente local em12/10/2026; teste real pendente.
- Relato: Valk1016 sem primário e morre ao tocar chão no Mausoleum; usuário confirma melhora na Base Profunda.
- Evidência Oracle: morte recebe1000000/Slay antes do servidor matar; SDG primário frequentemente vazio; acertos presentes na sessão Base Profunda.
- Captura local atual já está em sidou05 (Base Profunda); não captura a falha anterior dz_jjc. Não inferir precisão pela proporção de SDGs vazios.
- Fonte pura Slay/CMuzzlePhysicalMe confirma código original; modificação antiga colocou cápsulas control_IgnoreDamge/defaultMesh_IgnoreDamge na camada Default.
- Caminho problemático1: primário físico ainda usa Raycast/RaycastAll sem filtro HIT-002 e pode ser bloqueado pelas cápsulas auxiliares.
- Caminho problemático2: Slay aceita qualquer colisor com root do jogador, incluindo arma CMuzzlePhysicalTrigger; arma tocando volume pode matar dono.
- Hipótese não confirmada: esses caminhos explicam o evento relatado no Mausoleum. Falta identificar se era golpe no piso/salto especial/pulo normal e o colisor do evento real.
- Candidata: ReSetphysicalDir delega consulta ao filtro HIT-002; OnHitByPhysical filtra somente auxiliares da lista de bloqueios; Slay ignora apenas colisor de arma física.
- Log novo [TRIG Slay] registra zona,colisor,camada,posição e weapon; corpo/NPC/abismos legítimos preservados.
- Sem alteração de tamanho/camada/física dos colisores, assets de mapa, servidor ou dano nominal285.
- Verificações:11simulados novos+9HIT-002 passaram (processos separados);14.780 métodos comparados, somente3previstos alterados; Roslyn contra UnityEngine real.
- Backups: backups/valk001-before/client/Assembly-CSharp.dll; patch-validation.json contém hashes.
- Arquivos: client-patches/MeleeCollision.cs,tools/patch-melee-collision.ps1,tests/{melee-collision.ps1,MeleeCollisionFixture.cs}.
- Candidatos: scratch/valk-investigation/candidate/{Assembly-CSharp.dll,TriggerunMeleeCollision.dll}; dependência TriggerunHitCollision.dll instalada anteriormente.
- Cliente real: não testado nesta correção; Usuário fechou TRIG; instalação concluída com hashes conferidos. Rotina de instalação em scratch/valk-investigation/install.ps1.
- Oracle: somente leitura/coleta; nenhum reinício/alteração. Dados brutos em scratch/valk-investigation (client log,tail35000servidor,telemetria).
- Próximo: reabrir dois clientes e testar Mausoleum/primário/contato arma no piso/salto/abismo real; comparar Base Profunda. Se persistir, usar log novo para localizar volume/contato.