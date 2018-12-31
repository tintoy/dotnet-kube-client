#!/bin/bash

set -euo pipefail

################################
# Unit-test script for Travis CI
################################

# Disable .NET Core first-time usage experience and telemetry.
export DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1
export DOTNET_CLI_TELEMETRY_OPTOUT=1

echo ''
echo 'Testing...'
echo ''
echo 'travis_fold:start:dotnet_test'
testProjects=$(find ./test -name 'KubeClient*.Tests.csproj')
for testProject in $testProjects; do
	dotnet test $testProject --no-build --no-restore
done
echo 'travis_fold:end:dotnet_test'
