#!/usr/bin/env bash
# Launch the game. Usage: scripts/run.sh [scenario] [camera-preset]
source "$(dirname "$0")/_common.sh"
prepare_game
"$GODOT" --path "$ROOT/game" -- "--scenario=${1:-empty}" "--preset=${2:-default}"
