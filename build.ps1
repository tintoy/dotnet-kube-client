$ErrorActionPreference = 'Stop'

$versionInfo = .\tools\GitVersion\GitVersion.exe | ConvertFrom-Json
Write-Host $versionInfo

$dotnet = Get-Command dotnet
& $dotnet restore "/p:VersionPrefix=$versionInfo.MajorMinorPatch" "/p:VersionSuffix=$versionInfo.PreReleaseTag" "/p:AssemblyInformationalVersion=$versionInfo.InformationalVersion" /v:n
& $dotnet build "/p:VersionPrefix=$versionInfo.MajorMinorPatch" "/p:VersionSuffix=$versionInfo.PreReleaseTag" "/p:AssemblyInformationalVersion=$versionInfo.InformationalVersion" /v:n
& $dotnet pack "/p:VersionPrefix=$versionInfo.MajorMinorPatch" "/p:VersionSuffix=$versionInfo.PreReleaseTag" "/p:AssemblyInformationalVersion=$versionInfo.InformationalVersion" /v:n -o $PWD\out
