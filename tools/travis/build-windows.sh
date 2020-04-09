#!/bin/sh

set -e

echo "Running CI build for Windows"
(
    choco install dotnetcore-sdk
    dotnet clean -v m
    dotnet build
)