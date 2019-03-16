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

if [[ "${TRAVIS_PULL_REQUEST}" != "false" ]]; then
    echo "Current build is for a pull request; packages will not be published."

    exit 0
fi

# Build outputs go here.
ARTIFACTS_DIRECTORY="$PWD/artifacts"
if [ ! -d $ARTIFACTS_DIRECTORY ]; then
	echo "Cannot find package publishing directory '$ARTIFACTS_DIRECTORY'."

    exit 1
fi

echo 'travis_fold:start:publish_packages_myget'

if [[ "$TRAVIS_PULL_REQUEST" == "false" ]]; then
    echo "Publishing packages to MyGet package feed..."
    for PACKAGE in $(find $ARTIFACTS_DIRECTORY -name '*.nupkg' \! -name '*.symbols.nupkg'); do
        dotnet nuget push "$PACKAGE" --source "$MYGET_FEED_URL" --api-key "$MYGET_API_KEY"
    done

    for SYMBOL_PACKAGE in $(find $ARTIFACTS_DIRECTORY -name '*.symbols.nupkg'); do
        dotnet nuget push "$SYMBOL_PACKAGE" --source "$MYGET_SYMBOL_FEED_URL" --api-key "$MYGET_API_KEY"
    done
else
    echo "Not publishing packages for pull request '$TRAVIS_PULL_REQUEST' to MyGet package feed."
fi

echo 'travis_fold:end:publish_packages_myget'

echo 'travis_fold:start:publish_packages_nuget'

if [[ "$TRAVIS_BRANCH" == "master" ]]; then
    echo "Publishing packages for branch '$TRAVIS_BRANCH' to NuGet package feed..."

    for PACKAGE in $(find $ARTIFACTS_DIRECTORY -name '*.nupkg' \! -name '*.symbols.nupkg'); do
        dotnet nuget push "$PACKAGE" --source "$NUGET_FEED_URL" --api-key "$NUGET_API_KEY"
    done
else
    echo "Not publishing packages for branch '$TRAVIS_BRANCH' to NuGet package feed."
fi

echo 'travis_fold:end:publish_packages_nuget'

echo ''
echo 'Done.'
echo ''
