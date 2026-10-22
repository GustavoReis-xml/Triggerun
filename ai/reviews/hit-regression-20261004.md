# Passagem — HIT-002: regressão global após mudança anterior de hitbox

- Responsável: Codex, 2026-10-04. Estado: implementada e instalada localmente; cliente real pendente.
- Relato real após HIT-001: Celina aparentemente normal; Eva atravessa visualmente cabeça lateral da Celina, tiro a queima-roupa parece desviar para chão. Usuário informa alteração antiga global no Assembly para HS e histórico de quedas pelo mapa.
- Fonte pura fornecida: `C:/trig2/GAME/Dumps/assembly`. Inspecionados GamePlayer, CBulletNormalMe; comparados fontes atuais e DLLs anteriores de Managed.
- Confirmado: GamePlayer original põe cápsula control em Ignore Raycast. Alteração anterior cria control_IgnoreDamge e o insere nas camadas consultadas pelos tiros; também muda defaultMesh e física de morte. Não reverter GamePlayer inteiro.
- Mecanismo compatível: CBulletNormalMe salta 0,1m após atingir `_IgnoreDamge`. Um colisor real sobreposto pode ficar atrás da nova origem; mira também escolhia cápsulas auxiliares. Não há código de reflexão no trajeto normal analisado; aparência de ricochete ainda exige teste visual.
- Referência do motor: Unity documenta que o raio não detecta colisor contendo sua origem: https://docs.unity.cn/6000.7/Documentation/ScriptReference/Physics.Raycast.html . Consulta externa explica comportamento, não prova geometria da versão testada.
- Log preservado: `scratch/eva-hit-20261004/output_log.capture.txt`; Eva primária 228 SDGs, 204 vazios, 24 acertos incluindo 10 HS. Não interpretar vazios como taxa de erro: não há registro da mira pretendida.
- Decisão: filtrar consulta sem mudar física. Novo `client-patches/HitCollision.cs`: escolher superfície válida mais próxima dentre todos os resultados; ignorar auxiliares pelo sufixo sem deslocar origem. Buffer cresce se cheio (32–1024; fallback completo). Paredes e escudos mantidos; mira mantém exclusão de escudos original.
- Integração: substituição de chamadas em CameraControl.setPlayerTarget e CBulletNormalMe/CBulletLaserMe/CBulletBezierMe/CBulletContraMe.FixedUpdate. Ramos antigos de avanço tornam-se inalcançáveis quando dependem de retorno `_IgnoreDamge`; mudanças anteriores preservadas.
- Arquivos: fontes citadas, referência no Assembly-CSharp.csproj, `tools/patch-hit-filter.ps1`, `tests/hit-collision.ps1`, `tests/HitCollisionFixture.cs`, `client-patches/README.md`.
- Backup prévio: `backups/hit-regression-20261004/` contém DLL a45b19aa… e fontes afetadas/projeto. Nenhum método físico foi alterado.
- Compilação: Roslyn SDK8.0.403 com /nostdlib e mscorlib net35 + UnityEngine real; auxiliar runtime v2.0.50727, dependências mscorlib2 e UnityEngine0.
- Verificação IL: 14.780 corpos de métodos comparados; somente os 5 métodos previstos mudaram, 7 chamadas redirecionadas com assinaturas idênticas; saltos curtos válidos. Nenhuma recompilação do Assembly inteiro.
- Testes: 9 verificações aprovadas com código real do helper e física simulada: cabeça 4cm após auxiliar; origem/máscara; parede mais próxima; escudo para tiro; exclusão escudo na mira; apenas auxiliar; buffer cheio; fallback completo; consulta vazia limpa acerto anterior. Não equivale a execução Unity.
- Instalados com cliente fechado em `C:/trig2/GAME/TRIG_Data/Managed`: Assembly-CSharp.dll SHA256 `40d48646831def80332fa2bad94b7e9220911585252129c74134bc453aa3a092`; novo TriggerunHitCollision.dll SHA256 `8ebf02af9ceea7fd69bd7a45969327392dbe2f648c71a8ecae59665b4ecc5f3d`.
- Candidato/relatório: `scratch/eva-hit-20261004/candidate/`, `patch-result.json`; instruções reproduzíveis em client-patches/README.md. Ambos DLLs precisam acompanhar distribuição futura.
- Oracle: não alterada nesta rodada; HIT-001/53 testes permanecem base anterior. Testes servidor não repetidos pois execução servidor não mudou.
- Cliente real: pendente. Próximo teste: Eva→Celina, cabeça de lado e queima-roupa; confirmar dano/HS, trajetória, movimentação/colisão com chão e paredes. Comparar após reiniciar ambos clientes.
- Se persistir: capturar origem/direção de CreateBulletObject, lista de colisores com distâncias e cabeça; não seguir ampliando hitbox. Ainda não medidos posição/tamanho de colisor da cabeça, pontos de nascimento dentro do alvo e diferença visual de efeitos.
- Reversão: restaurar DLL do backup com jogo fechado; auxiliar deixa de ser referenciado. Reservas liberadas, investigação aguarda resultado real.

## Retorno do teste real — 2026-10-04

04/10: usuário relatou "parece ter funcionado!" após HIT-002. Validação inicial positiva no cliente; não implica cobertura de todos os heróis, distâncias ou colisão física. Preservar esta versão como referência; não acrescentar ajustes de hitbox sem novo defeito reproduzido. Instalação permanece apenas no cliente local, com os dois DLLs necessários.
