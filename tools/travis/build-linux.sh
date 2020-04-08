#!/bin/sh

set -e

echo "Running CI build for Linux"
(
    cd "$TRAVIS_BUILD_DIR" || exit
    dotnet clean -v m
    dotnet build
)