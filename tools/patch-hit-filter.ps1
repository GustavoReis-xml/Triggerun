param([Parameter(Mandatory=$true)][string]$InputDll,[Parameter(Mandatory=$true)][string]$OutputDll,[Parameter(Mandatory=$true)][string]$HelperDll)
$ErrorActionPreference='Stop'
$expected='A45B19AA1D03F8C45E057C6884683A93F19DFD06F0A22644A36284C0B3F620F5'
if((Get-FileHash -LiteralPath $InputDll).Hash -ne $expected){throw 'Unexpected input DLL; audit this version first.'}
if(Test-Path -LiteralPath $OutputDll){throw 'Output exists; choose a fresh candidate path.'}
Add-Type -Path "$env:USERPROFILE/.nuget/packages/microsoft.net.illink.tasks/8.0.10/tools/net8.0/Mono.Cecil.dll"
$resolver=[Mono.Cecil.DefaultAssemblyResolver]::new()
$resolver.AddSearchDirectory('C:/trig2/GAME/TRIG_Data/Managed')
$resolver.AddSearchDirectory([IO.Path]::GetDirectoryName([IO.Path]::GetFullPath($HelperDll)))
$parameters=[Mono.Cecil.ReaderParameters]::new();$parameters.AssemblyResolver=$resolver
$module=[Mono.Cecil.ModuleDefinition]::ReadModule([IO.Path]::GetFullPath($InputDll),$parameters)
$helper=[Mono.Cecil.ModuleDefinition]::ReadModule([IO.Path]::GetFullPath($HelperDll),$parameters)
$helperType=$helper.Types | Where-Object Name -eq TriggerunHitCollision
$ray=$module.ImportReference(($helperType.Methods | Where-Object Name -eq Raycast))
$aim=$module.ImportReference(($helperType.Methods | Where-Object Name -eq AimRaycast))
$counts=@{CameraControl=1;CBulletNormalMe=1;CBulletLaserMe=1;CBulletBezierMe=2;CBulletContraMe=2}
foreach($typeName in $counts.Keys){
 $type=$module.Types | Where-Object Name -eq $typeName
 $calls=0
 foreach($method in $type.Methods){if($method.HasBody){foreach($ins in $method.Body.Instructions){
  if($ins.OpCode.Name -ne 'call' -or $ins.Operand.DeclaringType.FullName -ne 'UnityEngine.Physics'){continue}
  $old=$ins.Operand
  if($typeName -eq 'CameraControl'){
   if($method.Name -ne 'setPlayerTarget' -or $old.Name -ne 'RaycastNonAlloc'){continue}
   $replacement=$aim
  }else{
   if($old.Name -ne 'Raycast'){continue}
   $replacement=$ray
  }
  if($old.Parameters.Count -ne $replacement.Parameters.Count -or $old.ReturnType.FullName -ne $replacement.ReturnType.FullName){throw 'Query signature mismatch'}
  for($i=0;$i -lt $old.Parameters.Count;$i++){if($old.Parameters[$i].ParameterType.FullName -ne $replacement.Parameters[$i].ParameterType.FullName){throw 'Query argument mismatch'}}
  $ins.Operand=$replacement;$calls++
 }}}
 if($calls -ne $counts[$typeName]){throw "Unexpected query count in $typeName : $calls"}
}
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
$allowed=@('System.Void CameraControl::setPlayerTarget()','System.Void CBulletNormalMe::FixedUpdate()','System.Void CBulletLaserMe::FixedUpdate()','System.Void CBulletBezierMe::FixedUpdate()','System.Void CBulletContraMe::FixedUpdate()')
if($checked -ne $before.Count -or $changed.Count -ne 5 -or @($changed | Where-Object {$_ -notin $allowed}).Count){throw "Unexpected changed methods ($($changed.Count)): $($changed | Select-Object -First 5)"}
$original.Dispose();$candidate.Dispose()
[pscustomobject]@{MethodsVerified=$checked;ChangedMethods=$changed;InputSHA256=$expected;OutputSHA256=(Get-FileHash -LiteralPath $OutputDll -Algorithm SHA256).Hash;UnityTest='PENDING'} | ConvertTo-Json




