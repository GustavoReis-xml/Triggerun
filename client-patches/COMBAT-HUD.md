# Compilar HUD-001 em uma pasta nova

A partir da raiz, use Roslyn SDK8.0.403 com /target:library /nostdlib /optimize+ e /out:<pasta-nova>/TriggerunCombatHud.dll. Referências: mscorlib.dll do pacote Microsoft.NETFramework.ReferenceAssemblies.net35/1.0.3, UnityEngine.dll, UnityEngine.UI.dll e Assembly-CSharp.dll do cliente auditado. Fontes: client-patches/CombatHud.cs e client-patches/CombatFeedback.cs. Preserve o nome de assembly TriggerunCombatHud.

Execute tools/patch-combat-hud.ps1 -InputDll backups/hud-20261007-before/client/Assembly-CSharp.dll -OutputDll <pasta-nova>/Assembly-CSharp.dll -HelperDll <pasta-nova>/TriggerunCombatHud.dll. A ferramenta exige hash conhecido e recusa sobrescrever candidato. Comparação deve listar somente 3 métodos.

Compile tests/CombatFeedbackFixture.cs com client-patches/CombatFeedback.cs, como executável net35 /nostdlib com mscorlib acima; 7 verificações simuladas. Não inclui UI/Unity real. Servidor: node --test tests/logging.test.js tests/lamster.test.js tests/match-lifecycle.test.js tests/entity-catalog.test.js tests/model-swap.test.js.

Distribuir Assembly-CSharp + TriggerunCombatHud.dll, mantendo TriggerunHitCollision.dll e TriggerunModelSwap.dll. Instalar somente com jogo fechado e backup. F2 dentro da partida configura a mira; preferências são locais ao Windows/cliente. Ver ai/reviews/crosshair-feedback-20261007.md para hashes, instalação, limites e reversão.

Atualização LAMSTER-002: CombatHud.cs inclui painel de funções/alcance do suporte, anexado por TriggerunHudSettings.Ensure. Recompilar somente TriggerunCombatHud.dll mantendo identidade e Assembly já instalado; não executar patch-combat-hud.ps1 sobre DLL com HUD já aplicado. SHA/helper e instalação em ai/reviews/lamster-voices-20261007.md. Demais computadores precisam desse helper atualizado para ver o painel.

LAMSTER-003: incluir também client-patches/LamsterMaterials.cs em toda compilação do helper TriggerunCombatHud.dll. Assembly agora chama TriggerunMaterialFilter.ForPlayer (filtro de material vazio para2000). Distribuir as duas DLLs novas juntas; hashes/reversão em ai/reviews/lamster-loading-20261007.md. O patch inicial HUD não se aplica sobre o Assembly atualizado.

LAMSTER-004 candidato: compilação do helper inclui também client-patches/BuffVoices.cs (além de LamsterMaterials.cs). Assembly candidato intercepta CPlayerSound.LoadBuffSound somente1007; instalar as duasDLLs juntas após fechar jogo. Estado de instalação em ai/reviews/lamster-rally-20261007.md.
