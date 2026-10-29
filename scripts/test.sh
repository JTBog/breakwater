#!/usr/bin/env bash
# Run the simulation unit tests.
set -euo pipefail
ROOT="$(cd "$(dirname "$0")/.." && pwd)"
dotnet test "$ROOT/sim.tests" -nologo "$@"
