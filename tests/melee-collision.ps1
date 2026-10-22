$ErrorActionPreference='Stop'
$root=Split-Path $PSScriptRoot -Parent
Add-Type -TypeDefinition ((Get-Content (Join-Path $root 'client-patches/MeleeCollision.cs') -Raw)+"`n"+(Get-Content (Join-Path $PSScriptRoot 'MeleeCollisionFixture.cs') -Raw))
[MeleeCollisionTests]::Run()