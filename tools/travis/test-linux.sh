#!/bin/sh

set -e

echo "Running CI build for Windows"
(
    dotnet test
    bash "$TRAVIS_BUILD_DIR"/tools/travis/CompileTest.sh
)