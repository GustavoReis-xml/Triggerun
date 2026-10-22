$ErrorActionPreference='Stop'
$root=Split-Path $PSScriptRoot -Parent
Add-Type -TypeDefinition ((Get-Content "$root/client-patches/LamsterArea.cs" -Raw)+"`n"+(Get-Content "$PSScriptRoot/LamsterAreaFixture.cs" -Raw))
[LamsterAreaTests]::Run()
