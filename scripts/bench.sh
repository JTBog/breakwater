#!/usr/bin/env bash
# Run BenchmarkDotNet benches. Usage: scripts/bench.sh [filter], e.g. scripts/bench.sh '*Horde*'
set -euo pipefail
ROOT="$(cd "$(dirname "$0")/.." && pwd)"
FILTER="${1:-*}"
dotnet run -c Release --project "$ROOT/sim.bench" -- --filter "$FILTER"
