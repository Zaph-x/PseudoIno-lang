#!/bin/sh

set -e

echo "Running CI build for Windows"
(
    dotnet test
    powershell.exe -c "Set-ExecutionPolicy -ExecutionPolicy Unrestricted -Scope LocalMachine"
    powershell.exe "$TRAVIS_BUILD_DIR"/tools/travis/CompileTest.ps1
)