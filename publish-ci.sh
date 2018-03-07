#!/bin/bash

set -euo pipefail

############################
# Build script for Travis CI
############################

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

PACKAGES=$(find $ARTIFACTS_DIRECTORY -name '*.nupkg' \! -name '*.symbols.nupkg')
SYMBOL_PACKAGES=$(find $ARTIFACTS_DIRECTORY -name '*.symbols.nupkg')

echo ''
echo 'Would publish packages:'
echo $PACKAGES
echo ''
echo 'Would publish symbol packages:'
echo $SYMBOL_PACKAGES
echo ''
