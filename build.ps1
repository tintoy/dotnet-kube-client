$ErrorActionPreference = 'Stop'

$versionInfo = .\tools\GitVersion\GitVersion.exe | ConvertFrom-Json
Write-Host $versionInfo

Dir -Recurse $PWD

$dotnet = Get-Command dotnet
& $dotnet restore "/p:VersionPrefix=$versionInfo.MajorMinorPatch" "/p:VersionSuffix=$versionInfo.PreReleaseTag" "/p:AssemblyInformationalVersion=$versionInfo.InformationalVersion" /v:d
& $dotnet build "/p:VersionPrefix=$versionInfo.MajorMinorPatch" "/p:VersionSuffix=$versionInfo.PreReleaseTag" "/p:AssemblyInformationalVersion=$versionInfo.InformationalVersion" /v:d
& $dotnet pack "/p:VersionPrefix=$versionInfo.MajorMinorPatch" "/p:VersionSuffix=$versionInfo.PreReleaseTag" "/p:AssemblyInformationalVersion=$versionInfo.InformationalVersion" /v:d -o $PWD\out

Dir -Recurse $PWD
