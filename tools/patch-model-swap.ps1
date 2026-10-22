param([string]$InputDll='backups/model-20261007-before/client/Assembly-CSharp.dll',[string]$OutputDll='scratch/model-swap/Assembly-CSharp.dll',[string]$HelperDll='scratch/model-swap/TriggerunModelSwap.dll')
$ErrorActionPreference='Stop'
$expected='40D48646831DEF80332FA2BAD94B7E9220911585252129C74134BC453AA3A092'
if((Get-FileHash $InputDll).Hash -ne $expected){throw 'Unreviewed client version'}
if(Test-Path $OutputDll){throw 'Candidate already exists'}
Add-Type -Path "$env:USERPROFILE/.nuget/packages/microsoft.net.illink.tasks/8.0.10/tools/net8.0/Mono.Cecil.dll"
$resolver=[Mono.Cecil.DefaultAssemblyResolver]::new()
$resolver.AddSearchDirectory('C:/trig2/GAME/TRIG_Data/Managed')
$parameters=[Mono.Cecil.ReaderParameters]::new();$parameters.AssemblyResolver=$resolver
$module=[Mono.Cecil.ModuleDefinition]::ReadModule([IO.Path]::GetFullPath($InputDll),$parameters)
$helper=[Mono.Cecil.ModuleDefinition]::ReadModule([IO.Path]::GetFullPath($HelperDll))
$ht=$helper.Types | Where-Object Name -eq TriggerunModelSwap
$apply=$module.ImportReference(($ht.Methods | Where-Object Name -eq Apply))
$anim=$module.ImportReference(($ht.Methods | Where-Object Name -eq AnimationType))
$pc=$module.Types | Where-Object Name -eq PlayerCommon
$m=$pc.Methods | Where-Object Name -eq GetNowModelData
if(@($m).Count -ne 1 -or !($pc.Fields | Where-Object Name -eq m_nVisualModelIndex)){throw 'Missing existing NVM contract'}
$il=$m.Body.GetILProcessor()
$returns=@($m.Body.Instructions | Where-Object {$_.OpCode.Name -eq 'ret'})
if($returns.Count -ne 1){throw 'Unexpected model return shape'}
$r=$returns[0];$r.OpCode=[Mono.Cecil.Cil.OpCodes]::Ldarg_0
$c=[Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Call,$apply)
$il.InsertAfter($r,$c);$il.InsertAfter($c,[Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Ret))
$gp=$module.Types | Where-Object Name -eq GamePlayer
$m=$gp.Methods | Where-Object Name -eq LoadModelTpsFinish
$calls=@($m.Body.Instructions | Where-Object {$_.OpCode.Name -eq 'call' -and $_.Operand.DeclaringType.FullName -eq 'System.Type' -and $_.Operand.Name -eq 'GetType'})
if($calls.Count -ne 1){throw 'Unexpected animation factory'}
$il=$m.Body.GetILProcessor();$call=$calls[0]
$call.OpCode=[Mono.Cecil.Cil.OpCodes]::Ldarg_0;$call.Operand=$null
$il.InsertAfter($call,[Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Call,$anim))
# Rebuild ragdoll references for the new model; old objects are destroyed by NVM.
$nvm=$gp.Methods | Where-Object Name -eq NVM
$nl=$module.Types | Where-Object Name -eq NetLiving
$field=$nl.Fields | Where-Object Name -eq rigidbodyAll
$clear=[Mono.Cecil.MethodReference]::new('Clear',$module.TypeSystem.Void,$field.FieldType);$clear.HasThis=$true
$il=$nvm.Body.GetILProcessor();$first=$nvm.Body.Instructions[0]
$il.InsertBefore($first,[Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Ldarg_0))
$il.InsertBefore($first,[Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Ldfld,$field))
# Use existing mscorlib scope, not the host PowerShell runtime.
$clear.DeclaringType.Scope=$module.TypeSystem.Object.Scope
$il.InsertBefore($first,[Mono.Cecil.Cil.Instruction]::Create([Mono.Cecil.Cil.OpCodes]::Callvirt,$clear))
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
$allowed=@('System.Void GamePlayer::NVM(System.Collections.ArrayList)','System.String[] PlayerCommon::GetNowModelData()','System.Void GamePlayer::LoadModelTpsFinish(UnityEngine.WWW,System.Object)')
if($checked -ne $before.Count -or $changed.Count -ne 3 -or @($changed | Where-Object {$_ -notin $allowed}).Count){throw "Unexpected changed methods ($($changed.Count)): $($changed | Select-Object -First 5)"}
$original.Dispose();$candidate.Dispose()
[pscustomobject]@{MethodsVerified=$checked;ChangedMethods=$changed;InputSHA256=$expected;OutputSHA256=(Get-FileHash -LiteralPath $OutputDll -Algorithm SHA256).Hash;UnityTest='PENDING'} | ConvertTo-Json





