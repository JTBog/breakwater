#!/usr/bin/env bash
# Build every .NET project (sim, tests, benches, runner, game).
source "$(dirname "$0")/_common.sh"
dotnet build "$ROOT/Breakwater.sln" -nologo -v quiet
