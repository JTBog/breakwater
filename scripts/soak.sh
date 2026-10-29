#!/usr/bin/env bash
# Run the headless sim at max speed. Usage: scripts/soak.sh <scenario> <calendar-hours> [seed]
set -euo pipefail
ROOT="$(cd "$(dirname "$0")/.." && pwd)"
dotnet run -c Release --project "$ROOT/tools/SimRunner" -- "${1:-empty}" "${2:-1}" "${3:-1}"
