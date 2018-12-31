#!/bin/bash

set -euo pipefail

############################
# Build script for Travis CI
############################

# Disable .NET Core first-time usage experience and telemetry.
export DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1
export DOTNET_CLI_TELEMETRY_OPTOUT=1

echo 'travis_fold:start:fetch_tags'

echo ''
echo 'Fetching tags...'
git fetch origin --tags
echo ''

echo 'travis_fold:end:fetch_tags'

echo 'travis_fold:start:compute_build_version'

echo 'Computing build version...'
dotnet gitversion | tee $PWD/version-info.json

echo 'travis_fold:end:compute_build_version'

BUILD_BASEVERSION=$(cat $PWD/version-info.json | jq -r .MajorMinorPatch)
BUILD_VERSION_SUFFIX=$(cat $PWD/version-info.json | jq -r .NuGetPreReleaseTagV2)
BUILD_INFORMATIONAL_VERSION=$(cat $PWD/version-info.json | jq -r .InformationalVersion)

echo ''
if [ -z "$BUILD_VERSION_SUFFIX" ]; then
	echo "Build version is '$BUILD_BASEVERSION'."
else
	echo "Build version is '$BUILD_BASEVERSION-$BUILD_VERSION_SUFFIX'."
fi
echo ''

# Build outputs go here.
ARTIFACTS_DIRECTORY="$PWD/artifacts"
if [ -d $ARTIFACTS_DIRECTORY ]; then
	rm -rf $ARTIFACTS_DIRECTORY
fi

echo ''
echo 'Restoring packages...'
echo ''
echo 'travis_fold:start:dotnet_restore'
dotnet restore /p:VersionPrefix="$BUILD_BASEVERSION" /p:VersionSuffix="$BUILD_VERSION_SUFFIX" /p:AssemblyInformationalVersion="$BUILD_INFORMATIONAL_VERSION"
echo 'travis_fold:end:dotnet_restore'

echo ''
echo 'Building...'
echo ''
echo 'travis_fold:start:dotnet_build'
dotnet build /p:VersionPrefix="$BUILD_BASEVERSION" /p:VersionSuffix="$BUILD_VERSION_SUFFIX" /p:AssemblyInformationalVersion="$BUILD_INFORMATIONAL_VERSION" --no-restore
echo 'travis_fold:end:dotnet_build'

echo ''
echo 'Testing...'
echo ''
echo 'travis_fold:start:dotnet_test'
testProjects=$(find ./test -name 'KubeClient*.Tests.csproj')
for testProject in $testProjects; do
	dotnet test $testProject --no-build --no-restore
done
echo 'travis_fold:end:dotnet_test'

echo ''
echo "Packing into '$ARTIFACTS_DIRECTORY'..."
echo ''
echo 'travis_fold:start:dotnet_pack'
dotnet pack /p:VersionPrefix="$BUILD_BASEVERSION" /p:VersionSuffix="$BUILD_VERSION_SUFFIX" /p:AssemblyInformationalVersion="$BUILD_INFORMATIONAL_VERSION" -o $ARTIFACTS_DIRECTORY --include-symbols --no-restore --no-build
echo 'travis_fold:end:dotnet_pack'
