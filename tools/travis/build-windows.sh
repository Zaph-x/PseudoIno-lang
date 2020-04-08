#!/bin/sh

set -e

echo "Running CI build for Windows"
(
    choco.exe install dotnetcore-sdk
    dotnet restore
    dotnet clean -v m
    dotnet build
)