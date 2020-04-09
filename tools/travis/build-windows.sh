#!/bin/sh

set -e

echo "Running CI build for Windows"
(
    dotnet clean -v m
    dotnet build
)