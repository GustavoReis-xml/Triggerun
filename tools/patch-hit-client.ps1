param(
 [Parameter(Mandatory=$true)][string]$InputDll,
 [Parameter(Mandatory=$true)][string]$OutputDll,
 [string]$ManagedDirectory="C:/trig2/GAME/TRIG_Data/Managed",
 [string]$CecilPath="$env:USERPROFILE/.nuget/packages/microsoft.net.illink.tasks/8.0.10/tools/net8.0/Mono.Cecil.dll"
)
$ErrorActionPreference='Stop'
$expected='084C0454FA4541142BB037BD6FB833C0000A94D75F7A553429CCDBB7E439C25D'
if((Get-FileHash -LiteralPath $InputDll -Algorithm SHA256).Hash -ne $expected){throw 'Unrecognized source DLL: inspect this version before patching.'}
if([IO.Path]::GetFullPath($InputDll) -eq [IO.Path]::GetFullPath($OutputDll)){throw 'Output must be a separate candidate.'}
if(Test-Path -LiteralPath $OutputDll){throw 'Output already exists; preserve it or choose another output.'}
Add-Type -Path $CecilPath
$resolver=[Mono.Cecil.DefaultAssemblyResolver]::new()
$resolver.AddSearchDirectory([IO.Path]::GetFullPath($ManagedDirectory))
$parameters=[Mono.Cecil.ReaderParameters]::new();$parameters.AssemblyResolver=$resolver
$module=[Mono.Cecil.ModuleDefinition]::ReadModule([IO.Path]::GetFullPath($InputDll),$parameters)
function Method($typeName,$methodName){
 $t=@($module.Types | Where-Object Name -eq $typeName)
 $m=@($t.Methods | Where-Object Name -eq $methodName)
 if($m.Count -ne 1){throw "Ambiguous method: $typeName.$methodName"}
 return $m[0]
}
# The initial branch and loop-back branch must both execute the new reset.
$camera=Method 'CameraControl' 'setPlayerTarget'
$entry=@($camera.Body.Instructions | Where-Object Offset -eq 0xC4)[0]
if($entry.OpCode.Name -ne 'ldarg.0' -or $camera.Body.Variables[1].VariableType.FullName -ne 'System.Single'){throw 'Camera IL mismatch'}
$il=$camera.Body.GetILProcessor()
$reset=[Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Ldc_R4,[single]1000000)
$save=[Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Stloc,$camera.Body.Variables[1])
$branches=@($camera.Body.Instructions | Where-Object { [object]::ReferenceEquals($_.Operand,$entry) })
if($branches.Count -ne 1){throw 'Unexpected camera loop entries'}
$il.InsertBefore($entry,$reset);$il.InsertBefore($entry,$save)
foreach($branch in $branches){$branch.Operand=$reset}
# Replace transform.position with the local ray origin. Keep the remaining arithmetic.
$bullet=Method 'CBulletNormalMe' 'FixedUpdate'
$a=@($bullet.Body.Instructions | Where-Object Offset -eq 0x101)[0]
$b=$a.Next;$c=$b.Next
if($a.OpCode.Name -ne 'ldarg.0' -or $b.Operand.Name -ne 'get_transform' -or $c.Operand.Name -ne 'get_position' -or $bullet.Body.Variables[1].VariableType.Name -ne 'Vector3'){throw 'Bullet IL mismatch'}
$a.OpCode=[Mono.Cecil.Cil.OpCodes]::Ldloc_1;$a.Operand=$null
$b.OpCode=[Mono.Cecil.Cil.OpCodes]::Nop;$b.Operand=$null
$c.OpCode=[Mono.Cecil.Cil.OpCodes]::Nop;$c.Operand=$null
# These edits do not cross any short-branch limit; still validate every short branch after writing.
$module.Write([IO.Path]::GetFullPath($OutputDll));$module.Dispose()
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
$allowed=@('System.Void CameraControl::setPlayerTarget()','System.Void CBulletNormalMe::FixedUpdate()')
if($checked -ne $before.Count -or $changed.Count -ne 2 -or @($changed | Where-Object {$_ -notin $allowed}).Count){throw "Unexpected changed methods ($($changed.Count)): $($changed | Select-Object -First 5)"}
$original.Dispose();$candidate.Dispose()
[pscustomobject]@{MethodsVerified=$checked;ChangedMethods=$changed;InputSHA256=$expected;OutputSHA256=(Get-FileHash -LiteralPath $OutputDll -Algorithm SHA256).Hash;UnityTest='PENDING'} | ConvertTo-Json



