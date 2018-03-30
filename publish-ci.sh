#!/bin/bash

set -euo pipefail

######################################
# Package-publish script for Travis CI
######################################

# Feed URLs
export MYGET_FEED_URL=https://www.myget.org/F/dotnet-kube-client/api/v2/package
export MYGET_SYMBOL_FEED_URL=https://www.myget.org/F/dotnet-kube-client/symbols/api/v2/package

export NUGET_FEED_URL=https://www.nuget.org/api/v2/package
export NUGET_SYMBOL_FEED_URL=http://www.symbolsource.org/Public/Metadata/NuGet

# Disable .NET Core first-time usage experience and telemetry.
export DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1
export DOTNET_CLI_TELEMETRY_OPTOUT=1

echo ''
echo 'Publishing packages...'
echo ''

if [[ "$TRAVIS_BRANCH" == "master" && "$TRAVIS_TAG" == "" ]]; then
    echo 'Skipping publishing of packages for untagged build in the master branch.'

    exit 0
fi

# Build outputs go here.
ARTIFACTS_DIRECTORY="$PWD/artifacts"
if [ ! -d $ARTIFACTS_DIRECTORY ]; then
	echo "Cannot find package publishing directory '$ARTIFACTS_DIRECTORY'."

    exit 1
fi

echo 'travis_fold:start:publish_packages'

echo "Publishing packages for tag '$TRAVIS_TAG' to MyGet package feed..."
for PACKAGE in $(find $ARTIFACTS_DIRECTORY -name '*.nupkg' \! -name '*.symbols.nupkg'); do
    dotnet nuget push "$PACKAGE" --source "$MYGET_FEED_URL" --api-key "$MYGET_API_KEY"
done

for SYMBOL_PACKAGE in $(find $ARTIFACTS_DIRECTORY -name '*.symbols.nupkg'); do
    dotnet nuget push "$SYMBOL_PACKAGE" --source "$MYGET_SYMBOL_FEED_URL" --api-key "$MYGET_API_KEY"
done

if [[ "$TRAVIS_BRANCH" == "master" && "$TRAVIS_TAG" != "" ]]; then
    echo "Publishing packages for tag '$TRAVIS_TAG' to NuGet package feed..."

    for PACKAGE in $(find $ARTIFACTS_DIRECTORY -name '*.nupkg' \! -name '*.symbols.nupkg'); do
        dotnet nuget push "$PACKAGE" --source "$NUGET_FEED_URL" --api-key "$NUGET_API_KEY"
    done

    for SYMBOL_PACKAGE in $(find $ARTIFACTS_DIRECTORY -name '*.symbols.nupkg'); do
        dotnet nuget push "$SYMBOL_PACKAGE" --source "$NUGET_SYMBOL_FEED_URL" --api-key "$NUGET_API_KEY"
    done
fi

echo 'travis_fold:end:publish_packages'

echo ''
echo 'Done.'
echo ''

