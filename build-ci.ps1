$ErrorActionPreference = 'Stop'

$dotnet = Get-Command dotnet

$rawVersionInfo = & $dotnet gitversion
If ($LASTEXITCODE) {
    Write-Host 'GitVersion:'
    Write-Host $rawVersionInfo

    Exit 1
}

$versionInfo = $rawVersionInfo | ConvertFrom-Json

$versionPrefix = $versionInfo.MajorMinorPatch
$versionSuffix = $versionInfo.NuGetPreReleaseTagV2
$informationalVersion = $versionInfo.InformationalVersion

If ($versionSuffix) {
    Write-Host "Build version is $versionPrefix-$versionSuffix"
} Else {
    Write-Host "Build version is $versionPrefix"
}

Write-Host '====================='
Write-Host 'Restoring packages...'
Write-Host '====================='
& $dotnet restore /p:VersionPrefix="$versionPrefix" /p:VersionSuffix="$versionSuffix" /p:AssemblyInformationalVersion="$informationalVersion"

Write-Host '==========='
Write-Host 'Building...'
Write-Host '==========='
& $dotnet build /p:VersionPrefix="$versionPrefix" /p:VersionSuffix="$versionSuffix" /p:AssemblyInformationalVersion="$informationalVersion" --no-restore

Write-Host '=========='
Write-Host 'Packing...'
Write-Host '=========='
& $dotnet pack /p:VersionPrefix="$versionPrefix" /p:VersionSuffix="$versionSuffix" /p:AssemblyInformationalVersion="$informationalVersion" --no-restore --include-symbols -o "$PWD\out"
