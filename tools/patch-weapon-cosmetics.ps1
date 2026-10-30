param(
 [string]$InputDll='backups/weapon062-before/Assembly-CSharp.dll',
 [string]$OutputDll='scratch/weapon062/Assembly-CSharp.dll',
 [string]$HelperDll='scratch/weapon062/TriggerunWeaponCosmetics.dll'
)
$ErrorActionPreference='Stop'
if(Test-Path -LiteralPath $OutputDll){throw 'Candidate already exists'}
if((Get-FileHash -LiteralPath $InputDll).Hash-ne'CDBAF1B3EBD342C059B9825E7B4481D270B647AD55D2A806D8DC3BD7F439AFA7'){throw 'Unexpected Assembly baseline'}
Add-Type -Path "$env:USERPROFILE/.nuget/packages/microsoft.net.illink.tasks/8.0.10/tools/net8.0/Mono.Cecil.dll"
$resolver=[Mono.Cecil.DefaultAssemblyResolver]::new();$resolver.AddSearchDirectory('C:/trig2/GAME/TRIG_Data/Managed')
$parameters=[Mono.Cecil.ReaderParameters]::new();$parameters.AssemblyResolver=$resolver
$m=[Mono.Cecil.ModuleDefinition]::ReadModule([IO.Path]::GetFullPath($InputDll),$parameters)
$h=[Mono.Cecil.ModuleDefinition]::ReadModule([IO.Path]::GetFullPath($HelperDll))
$ht=$h.Types|Where-Object Name -eq TriggerunWeaponCosmetics
$hooks=@{};foreach($name in @('Prepare','EndPrepare','Wrap','Accept','Apply')){$hooks[$name]=$m.ImportReference(($ht.Methods|Where-Object Name -eq $name))}
$t=$m.Types|Where-Object Name -eq CHeroWeapon
$set=$t.Methods|Where-Object Name -eq SetHeroWeapon
$callback=$t.Methods|Where-Object Name -eq LoadWeaponMatOk
# Expanded branches make later insertion independent of short-branch limits.
$opcodes=@{};foreach($field in [Mono.Cecil.Cil.OpCodes].GetFields()){$op=$field.GetValue($null);$opcodes[$op.Name]=$op}
foreach($method in @($set,$callback)){foreach($ins in $method.Body.Instructions){if($ins.OpCode.OperandType-eq[Mono.Cecil.Cil.OperandType]::ShortInlineBrTarget){$ins.OpCode=$opcodes[$ins.OpCode.Name.Substring(0,$ins.OpCode.Name.Length-2)]}}}
$il=$set.Body.GetILProcessor();$first=$set.Body.Instructions[0]
foreach($ins in @([Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Ldarg_0),[Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Ldarg_3),[Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Call,$hooks.Prepare))){$il.InsertBefore($first,$ins)}
$wrapped=0;$returns=0
foreach($ins in @($set.Body.Instructions)){
 if($ins.OpCode.Code-eq[Mono.Cecil.Cil.Code]::Call -and (($ins.Operand.DeclaringType.FullName-eq'GameRoot' -and $ins.Operand.Name-eq'LoadAsset') -or ($ins.Operand.DeclaringType.FullName-eq'CHeroWeapon' -and $ins.Operand.Name-eq'LoadWeaponMatOk'))){
  $il.InsertBefore($ins,[Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Ldarg_0));$il.InsertBefore($ins,[Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Call,$hooks.Wrap));$wrapped++
 }
 if($ins.OpCode.Code-eq[Mono.Cecil.Cil.Code]::Ret){$ins.OpCode=[Mono.Cecil.Cil.OpCodes]::Ldarg_0;$ins.Operand=$null;$call=[Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Call,$hooks.EndPrepare);$il.InsertAfter($ins,$call);$il.InsertAfter($call,[Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Ret));$returns++}
}
if($wrapped-ne2 -or $returns-lt2){throw 'Unexpected cached/download callback shape'}
$il=$callback.Body.GetILProcessor();$first=$callback.Body.Instructions[0];$originalReturns=@($callback.Body.Instructions|Where-Object{$_.OpCode.Code-eq[Mono.Cecil.Cil.Code]::Ret})
foreach($ins in @([Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Ldarg_0),[Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Ldarg_1),[Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Ldarg_2),[Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Call,$hooks.Accept),[Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Brtrue,$first),[Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Ret))){$il.InsertBefore($first,$ins)}
foreach($ins in $originalReturns){$ins.OpCode=[Mono.Cecil.Cil.OpCodes]::Ldarg_0;$ins.Operand=$null;$p=$ins;foreach($next in @([Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Ldarg_1),[Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Ldarg_2),[Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Call,$hooks.Apply),[Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Ret))){$il.InsertAfter($p,$next);$p=$next}}
$m.Write([IO.Path]::GetFullPath($OutputDll));$m.Dispose();$h.Dispose()
$a=[Mono.Cecil.ModuleDefinition]::ReadModule([IO.Path]::GetFullPath($InputDll));$b=[Mono.Cecil.ModuleDefinition]::ReadModule([IO.Path]::GetFullPath($OutputDll));$changed=@();$count=0
function AllTypes($types){foreach($type in $types){$type;AllTypes $type.NestedTypes}}
$types=@{};foreach($type in(AllTypes $b.Types)){$types[$type.FullName]=$type}
foreach($type in(AllTypes $a.Types)){$bt=$types[$type.FullName];if(!$bt -or $bt.Methods.Count-ne$type.Methods.Count -or $bt.Fields.Count-ne$type.Fields.Count){throw 'Type/member mismatch'};foreach($f in $type.Methods){$bf=$bt.Methods[$type.Methods.IndexOf($f)];if($bf.FullName-ne$f.FullName){throw 'Method signature changed'};if($f.HasBody){$x=($f.Body.Instructions|ForEach-Object{$_.ToString()})-join "`n";$y=($bf.Body.Instructions|ForEach-Object{$_.ToString()})-join "`n";if($x-ne$y){$changed+=$f.FullName}};$count++}}
$allowed=@('System.Void CHeroWeapon::SetHeroWeapon(System.Int32,System.Int32,System.Int32)','System.Void CHeroWeapon::LoadWeaponMatOk(UnityEngine.WWW,System.Object)')
if($changed.Count-ne2 -or @($changed|Where-Object{$_ -notin$allowed}).Count){throw ('Unexpected changes: '+($changed-join ', '))}
@{methodsCompared=$count;changed=$changed;wrappedCallbacks=$wrapped;requestReturns=$returns;inputSHA256=(Get-FileHash $InputDll).Hash;outputSHA256=(Get-FileHash $OutputDll).Hash;UnityTested=$false}|ConvertTo-Json|Set-Content scratch/weapon062/patch-validation.json
$a.Dispose();$b.Dispose();Write-Output "PASS $count methods compared; only 2 weapon cosmetic methods changed"
