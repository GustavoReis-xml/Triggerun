$ErrorActionPreference='Stop'
$root=Split-Path $PSScriptRoot -Parent
$source=Get-Content (Join-Path $root 'client-patches/HitCollision.cs') -Raw
$stub=Get-Content (Join-Path $PSScriptRoot 'HitCollisionFixture.cs') -Raw
Add-Type -TypeDefinition ($source+"`n"+$stub)
[HitCollisionTests]::Run()
