param([string]$InputDll='backups/lamster-rally-20261007-before/client/Assembly-CSharp.dll',[string]$OutputDll='scratch/lamster-rally/Assembly-CSharp.dll',[string]$HelperDll='scratch/lamster-rally/TriggerunCombatHud.dll')
$ErrorActionPreference='Stop'
$expected='0373341552C5D8E1497E34494630733F7587704665EACBEE2B87F5EA75F5BDA6'
if((Get-FileHash $InputDll).Hash -ne $expected){throw 'Unreviewed client version'}
if(Test-Path $OutputDll){throw 'Candidate already exists'}
Add-Type -Path "$env:USERPROFILE/.nuget/packages/microsoft.net.illink.tasks/8.0.10/tools/net8.0/Mono.Cecil.dll"
$resolver=[Mono.Cecil.DefaultAssemblyResolver]::new();$resolver.AddSearchDirectory('C:/trig2/GAME/TRIG_Data/Managed')
$parameters=[Mono.Cecil.ReaderParameters]::new();$parameters.AssemblyResolver=$resolver
$module=[Mono.Cecil.ModuleDefinition]::ReadModule([IO.Path]::GetFullPath($InputDll),$parameters)
$helper=[Mono.Cecil.ModuleDefinition]::ReadModule([IO.Path]::GetFullPath($HelperDll))
$ht=$helper.Types | Where-Object Name -eq TriggerunBuffVoices
$hook=$module.ImportReference(($ht.Methods | Where-Object Name -eq TryLoad))
$type=$module.Types | Where-Object Name -eq CPlayerSound
$method=$type.Methods | Where-Object Name -eq LoadBuffSound
$il=$method.Body.GetILProcessor();$first=$method.Body.Instructions[0]
foreach($op in @([Mono.Cecil.Cil.OpCodes]::Ldarg_0,[Mono.Cecil.Cil.OpCodes]::Ldarg_1,[Mono.Cecil.Cil.OpCodes]::Ldarg_2)){$il.InsertBefore($first,[Mono.Cecil.Cil.Instruction]::Create($op))}
$il.InsertBefore($first,[Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Call,$hook))
$il.InsertBefore($first,[Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Brfalse,$first))
$il.InsertBefore($first,[Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Ret))
$module.Write([IO.Path]::GetFullPath($OutputDll));$module.Dispose();$helper.Dispose()
$original=[Mono.Cecil.ModuleDefinition]::ReadModule([IO.Path]::GetFullPath($InputDll))
$candidate=[Mono.Cecil.ModuleDefinition]::ReadModule([IO.Path]::GetFullPath($OutputDll))
function Signature($method){
 if(!$method.HasBody){return ''}
 $instructions=$method.Body.Instructions
 $lines=foreach($ins in $instructions){
  $operand=$ins.Operand
  if($operand -is [Mono.Cecil.Cil.Instruction]){$operand=$instructions.IndexOf($operand)}
  elseif($operand -is [Mono.Cecil.Cil.Instruction[]]){$operand=($operand | ForEach-Object {$instructions.IndexOf($_)}) -join ','}
  elseif($operand -is [Mono.Cecil.Cil.VariableDefinition]){$operand='var'+$operand.Index}
  elseif($operand -is [Mono.Cecil.ParameterDefinition]){$operand='arg'+$operand.Index}
  "$($ins.OpCode.Name) $operand"
 }
 return $lines -join "`n"
}
function AllTypes($types){foreach($t in $types){$t;AllTypes $t.NestedTypes}}
$before=@{};foreach($t in (AllTypes $original.Types)){foreach($m in $t.Methods){$before[($t.FullName+":"+$t.Methods.IndexOf($m))]=Signature $m}}
$changed=@();$checked=0
foreach($t in (AllTypes $candidate.Types)){foreach($m in $t.Methods){
 $checked++
 if(!$before.ContainsKey(($t.FullName+":"+$t.Methods.IndexOf($m)))){throw "Unexpected new method: $($m.FullName)"}
 if((Signature $m) -cne $before[($t.FullName+":"+$t.Methods.IndexOf($m))]){$changed+=$m.FullName}
 if($m.HasBody){foreach($ins in $m.Body.Instructions){
  if($ins.OpCode.OperandType -eq [Mono.Cecil.Cil.OperandType]::ShortInlineBrTarget){
   $distance=$ins.Operand.Offset-($ins.Offset+$ins.GetSize())
   if($distance -lt -128 -or $distance -gt 127){throw "Short branch overflow: $($m.FullName)"}
  }
 }}
}}
$allowed=@('System.Void CPlayerSound::LoadBuffSound(System.String,System.Int32)')
if($checked -ne $before.Count -or $changed.Count -ne 1 -or @($changed | Where-Object {$_ -notin $allowed}).Count){throw "Unexpected changed methods ($($changed.Count)): $($changed | Select-Object -First 5)"}
$original.Dispose();$candidate.Dispose()
[pscustomobject]@{MethodsVerified=$checked;ChangedMethods=$changed;InputSHA256=$expected;OutputSHA256=(Get-FileHash -LiteralPath $OutputDll -Algorithm SHA256).Hash;UnityTest='PENDING'} | ConvertTo-Json








