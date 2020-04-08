#!/bin/sh

set -e

echo "Running CI build for Windows"
(
    choco.exe install dotnetcore-sdk
    dotnet clean -v m
    dotnet build
)