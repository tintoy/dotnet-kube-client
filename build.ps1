$ErrorActionPreference = 'Stop'

$rawVersionInfo = .\tools\GitVersion\GitVersion.exe
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

$dotnet = Get-Command dotnet
& $dotnet restore /p:VersionPrefix="$versionPrefix" /p:VersionSuffix="$versionSuffix" /p:AssemblyInformationalVersion="$informationalVersion"
& $dotnet build /p:VersionPrefix="$versionPrefix" /p:VersionSuffix="$versionSuffix" /p:AssemblyInformationalVersion="$informationalVersion"
& $dotnet pack /p:VersionPrefix="$versionPrefix" /p:VersionSuffix="$versionSuffix" /p:AssemblyInformationalVersion="$informationalVersion" --include-symbols -o "$PWD\out"
