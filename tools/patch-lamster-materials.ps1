param([string]$InputDll='backups/lamster-loading-20261007-before/client/Assembly-CSharp.dll',[string]$OutputDll='scratch/lamster-loading/Assembly-CSharp.dll',[string]$HelperDll='scratch/lamster-loading/TriggerunCombatHud.dll')
$ErrorActionPreference='Stop'
$expected='FB3E8178790E30DE4E7CCAED0F29CCCA4286DD719B7288E30D152789EF5B24A7'
if((Get-FileHash $InputDll).Hash -ne $expected){throw 'Unreviewed client version'}
if(Test-Path $OutputDll){throw 'Candidate already exists'}
Add-Type -Path "$env:USERPROFILE/.nuget/packages/microsoft.net.illink.tasks/8.0.10/tools/net8.0/Mono.Cecil.dll"
$resolver=[Mono.Cecil.DefaultAssemblyResolver]::new();$resolver.AddSearchDirectory('C:/trig2/GAME/TRIG_Data/Managed')
$parameters=[Mono.Cecil.ReaderParameters]::new();$parameters.AssemblyResolver=$resolver
$module=[Mono.Cecil.ModuleDefinition]::ReadModule([IO.Path]::GetFullPath($InputDll),$parameters)
$helper=[Mono.Cecil.ModuleDefinition]::ReadModule([IO.Path]::GetFullPath($HelperDll))
$ht=$helper.Types | Where-Object Name -eq TriggerunMaterialFilter
$hook=$module.ImportReference(($ht.Methods | Where-Object Name -eq ForPlayer))
$type=$module.Types | Where-Object Name -eq PlayerCommon
$method=$type.Methods | Where-Object Name -eq GetNowModelWeaponMat
$returns=@($method.Body.Instructions | Where-Object {$_.OpCode.Name -eq 'ret'})
if($returns.Count -ne 1){throw 'Unexpected returns'}
$r=$returns[0];$r.OpCode=[Mono.Cecil.Cil.OpCodes]::Ldarg_0;$r.Operand=$null
$il=$method.Body.GetILProcessor();$call=[Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Call,$hook)
$il.InsertAfter($r,$call);$il.InsertAfter($call,[Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Ret))
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
$allowed=@('System.Collections.Generic.List`1<System.String> PlayerCommon::GetNowModelWeaponMat(System.Int32)')
if($checked -ne $before.Count -or $changed.Count -ne 1 -or @($changed | Where-Object {$_ -notin $allowed}).Count){throw "Unexpected changed methods ($($changed.Count)): $($changed | Select-Object -First 5)"}
$original.Dispose();$candidate.Dispose()
[pscustomobject]@{MethodsVerified=$checked;ChangedMethods=$changed;InputSHA256=$expected;OutputSHA256=(Get-FileHash -LiteralPath $OutputDll -Algorithm SHA256).Hash;UnityTest='PENDING'} | ConvertTo-Json







