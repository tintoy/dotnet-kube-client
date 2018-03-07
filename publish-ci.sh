#!/bin/bash

set -euo pipefail

######################################
# Package-publish script for Travis CI
######################################

# Feed URLs
export MYGET_FEED_URL=https://www.myget.org/F/dotnet-kube-client/api/v2/package
export MYGET_SYMBOL_FEED_URL=https://www.myget.org/F/dotnet-kube-client/symbols/api/v2/package

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

for PACKAGE in $(find $ARTIFACTS_DIRECTORY -name '*.nupkg'); do
    echo "Publishing package '$(basename $PACKAGE)'..."
    dotnet nuget push "$PACKAGE" --source "$MYGET_FEED_URL" --symbol-source "$MYGET_SYMBOL_FEED_URL" --api-key "$MYGET_API_KEY"
done

echo ''
echo 'Done.'
