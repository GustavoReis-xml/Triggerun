# Filtro de colisão do cliente (HIT-002)

`HitCollision.cs` é compilado em `TriggerunHitCollision.dll` para o runtime .NET 2.0/3.5 usado pelo jogo. O patch de Assembly-CSharp troca somente chamadas de consulta da mira e de quatro famílias de projéteis. Camadas físicas, dimensões, Rigidbody, animações e cálculo de dano não são modificados.

Compilar no PowerShell, a partir da raiz do repositório (caminhos correspondem ao ambiente auditado):

```powershell
$ref='C:/Users/Gustavo/.nuget/packages/microsoft.netframework.referenceassemblies.net35/1.0.3/build/.NETFramework/v3.5/mscorlib.dll'
New-Item -ItemType Directory -Force scratch/novo-candidato | Out-Null
dotnet 'C:/Program Files/dotnet/sdk/8.0.403/Roslyn/bincore/csc.dll' /nologo /target:library /nostdlib /optimize+ /out:scratch/novo-candidato/TriggerunHitCollision.dll "/reference:$ref" /reference:C:/trig2/GAME/TRIG_Data/Managed/UnityEngine.dll client-patches/HitCollision.cs
if($LASTEXITCODE -ne 0){throw 'Falha na compilação'}
& tools/patch-hit-filter.ps1 -InputDll backups/hit-regression-20261004/Assembly-CSharp.dll -OutputDll scratch/novo-candidato/Assembly-CSharp.dll -HelperDll scratch/novo-candidato/TriggerunHitCollision.dll
& tests/hit-collision.ps1
```

O patch exige o hash da versão anterior conhecida e recusa saída existente. O teste usa respostas de física simuladas para testar a seleção; não executa Unity nem mede a geometria dos modelos. Antes de instalar, conferir ausência de clientes abertos, hash da versão instalada e backup. Instalar ambos os DLLs, auxiliar primeiro, em `TRIG_Data/Managed`. Não distribuir apenas Assembly-CSharp.

O buffer de consultas cresce quando cheio e usa consulta completa no limite: não pressupõe que resultados estejam ordenados. Colisores `_IgnoreDamge` não consomem distância do tiro. Paredes e escudos continuam válidos para projéteis; a mira mantém sua exclusão preexistente de escudos.

Para reverter esta instalação: fechar o jogo e restaurar `backups/hit-regression-20261004/Assembly-CSharp.dll` em Managed. A versão anterior não referencia o auxiliar, que pode permanecer sem uso. Não substituir pela versão pura completa: isso também desfaria modificações alheias à colisão.

## Lamster: oclusão do pulso (LAMSTER-005)
Ao reconstruir TriggerunCombatHud.dll, incluir também LamsterArea.cs, além de CombatHud.cs, CombatFeedback.cs, LamsterMaterials.cs e BuffVoices.cs. O Assembly atual referencia TriggerunLamsterArea.RaycastAll em CBulletMe.boom. Nunca instalar um helper antigo sem esse símbolo. Patch reproduzível: tools/patch-lamster-area.ps1; teste simulado: tests/lamster-area.ps1. Detalhes/hashes/backup: ai/reviews/lamster-icons-damage-20261007.md.

### VALK-001 — primário físico e contato com Slay
Candidata em scratch/valk-investigation/candidate; requer TriggerunMeleeCollision.dll e TriggerunHitCollision.dll ao lado do Assembly-CSharp.dll. Preserva filtros e patches anteriores. Instalar somente com TRIG fechado usando scratch/valk-investigation/install.ps1; backup em backups/valk001-before. Reprodução do patch: tools/patch-melee-collision.ps1; verificações: tests/melee-collision.ps1 e tests/hit-collision.ps1 em processos separados. Evidência e limites: ai/reviews/valk-melee-slay-20261008.md.

### SKIN-040 — movimento cosmético do Corvo
`RavenDynamics.cs` gera `TriggerunRavenDynamics.dll` (.NET3.5, referências mscorlib/System/UnityEngine/Assembly-CSharp). O prefixo reproduzível em `scratch/shogun040/patch.ps1` modifica somente DynamicBone.UpdateDynamicBones e limita o auxiliar às raízes `TRRaven040_`. Instalar auxiliar e Assembly juntos com o TRIG fechado; não substituir por uma DLL pura. Volumes são DynamicBoneCollider próprios do ator, sem UnityCollider ou tráfego de rede. Evidências, hashes e reversão por backup: `ai/reviews/shogun-raven-dynamics-20261022.md`. Os testes do solver usam matemática substituta e não confirmam o resultado visual no Unity.
