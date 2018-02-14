$versionInfo = .\tools\GitVersion\GitVersion.exe | ConvertFrom-Json
Write-Host $versionInfo

$dotnet = Get-Command dotnet
& $dotnet restore "/p:VersionPrefix=$versionInfo.MajorMinorPatch" "/p:VersionSuffix=$versionInfo.PreReleaseTag" "/p:AssemblyInformationalVersion=$versionInfo.InformationalVersion"
& $dotnet build "/p:VersionPrefix=$versionInfo.MajorMinorPatch" "/p:VersionSuffix=$versionInfo.PreReleaseTag" "/p:AssemblyInformationalVersion=$versionInfo.InformationalVersion"
& $dotnet pack "/p:VersionPrefix=$versionInfo.MajorMinorPatch" "/p:VersionSuffix=$versionInfo.PreReleaseTag" "/p:AssemblyInformationalVersion=$versionInfo.InformationalVersion" -o $PWD\out
