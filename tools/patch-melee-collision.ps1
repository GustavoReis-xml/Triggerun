param([string]$InputDll='backups/valk001-before/client/Assembly-CSharp.dll',[string]$OutputDll='scratch/valk-investigation/candidate/Assembly-CSharp.dll')
$ErrorActionPreference='Stop'
if(Test-Path $OutputDll){throw 'Candidate already exists'}
Add-Type -Path "$env:USERPROFILE/.nuget/packages/microsoft.net.illink.tasks/8.0.10/tools/net8.0/Mono.Cecil.dll"
$resolver=[Mono.Cecil.DefaultAssemblyResolver]::new();$resolver.AddSearchDirectory('C:/trig2/GAME/TRIG_Data/Managed');$parameters=[Mono.Cecil.ReaderParameters]::new();$parameters.AssemblyResolver=$resolver
$m=[Mono.Cecil.ModuleDefinition]::ReadModule([IO.Path]::GetFullPath($InputDll),$parameters)
$h=[Mono.Cecil.ModuleDefinition]::ReadModule([IO.Path]::GetFullPath('scratch/valk-investigation/candidate/TriggerunMeleeCollision.dll'))
$hit=[Mono.Cecil.ModuleDefinition]::ReadModule('C:/trig2/GAME/TRIG_Data/Managed/TriggerunHitCollision.dll')
$ht=$h.Types|Where-Object Name -eq TriggerunMeleeCollision
$block=$m.ImportReference(($ht.Methods|Where-Object Name -eq MeleeBlockers));$guard=$m.ImportReference(($ht.Methods|Where-Object Name -eq MayEnterSlay))
$ray=$m.ImportReference((($hit.Types|Where-Object Name -eq TriggerunHitCollision).Methods|Where-Object Name -eq Raycast))
$t=$m.Types|Where-Object Name -eq CMuzzlePhysicalMe;$counts=@{}
foreach($method in $t.Methods){
 if($method.Name -notin @('ReSetphysicalDir','OnHitByPhysical')){continue}
 foreach($ins in $method.Body.Instructions){
  if($ins.OpCode.Name-ne'call' -or $ins.Operand.DeclaringType.FullName-ne'UnityEngine.Physics'){continue}
  if($method.Name-eq'ReSetphysicalDir' -and $ins.Operand.Name-eq'Raycast'){$replacement=$ray}
  elseif($method.Name-eq'OnHitByPhysical' -and $ins.Operand.Name-eq'RaycastAll'){$replacement=$block}else{continue}
  $original=$ins.Operand
  if($original.ReturnType.FullName-ne$replacement.ReturnType.FullName -or $original.Parameters.Count-ne$replacement.Parameters.Count){throw 'Signature mismatch'}
  for($i=0;$i-lt$original.Parameters.Count;$i++){if($original.Parameters[$i].ParameterType.FullName-ne$replacement.Parameters[$i].ParameterType.FullName){throw 'Argument mismatch'}}
  $ins.Operand=$replacement;$counts[$method.Name]++
 }
}
if($counts['ReSetphysicalDir']-ne1 -or $counts['OnHitByPhysical']-ne1){throw 'Unexpected query count'}
$slay=(($m.Types|Where-Object Name -eq Slay).Methods|Where-Object Name -eq OnTriggerEnter);$il=$slay.Body.GetILProcessor();$first=$slay.Body.Instructions[0]
foreach($ins in @([Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Ldarg_0),[Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Ldarg_1),[Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Call,$guard),[Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Brtrue,$first),[Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Ret))){$il.InsertBefore($first,$ins)}
$m.Write([IO.Path]::GetFullPath($OutputDll));$m.Dispose();$h.Dispose();$hit.Dispose()
$a=[Mono.Cecil.ModuleDefinition]::ReadModule([IO.Path]::GetFullPath($InputDll));$b=[Mono.Cecil.ModuleDefinition]::ReadModule([IO.Path]::GetFullPath($OutputDll));$changed=@();$count=0
function AllTypes($types){foreach($t in $types){$t;AllTypes $t.NestedTypes}}
$types=@{};foreach($t in(AllTypes $b.Types)){$types[$t.FullName]=$t}
foreach($t in(AllTypes $a.Types)){$bt=$types[$t.FullName];if(!$bt -or $bt.Methods.Count-ne$t.Methods.Count){throw 'Type/method mismatch'};foreach($f in $t.Methods){$bf=$bt.Methods[$t.Methods.IndexOf($f)];if($bf.FullName-ne$f.FullName){throw 'Method order changed'};if($f.HasBody){$x=($f.Body.Instructions|ForEach-Object{$_.ToString()})-join "`n";$y=($bf.Body.Instructions|ForEach-Object{$_.ToString()})-join "`n";if($x-ne$y){$changed+=$f.FullName}};$count++}}
$allowed=@('System.Void CMuzzlePhysicalMe::ReSetphysicalDir()','System.Void CMuzzlePhysicalMe::OnHitByPhysical(UnityEngine.Collision)','System.Void Slay::OnTriggerEnter(UnityEngine.Collider)')
if($changed.Count-ne3 -or @($changed|Where-Object{$_ -notin $allowed}).Count){throw ('Unexpected changes: '+($changed-join ', '))}
@{methodsCompared=$count;changed=$changed;inputSHA256=(Get-FileHash $InputDll).Hash;outputSHA256=(Get-FileHash $OutputDll).Hash;UnityTested=$false} | ConvertTo-Json | Set-Content scratch/valk-investigation/patch-validation.json
$a.Dispose();$b.Dispose();Write-Output "PASS $count methods compared; 3 intended methods changed"