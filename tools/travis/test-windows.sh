#!/bin/sh

set -e

echo "Running Compiler Test for Windows"
(
    powershell.exe -c "Set-ExecutionPolicy -ExecutionPolicy Unrestricted -Scope LocalMachine"
    powershell.exe "$TRAVIS_BUILD_DIR"/tools/travis/CompileTest.ps1
)