#!/bin/sh

set -e

echo "Running Compiler Test for Linux"
(
    bash "$TRAVIS_BUILD_DIR"/tools/travis/CompileTest.sh
)