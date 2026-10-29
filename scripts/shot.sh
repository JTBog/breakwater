#!/usr/bin/env bash
# Run a scenario, wait, save a screenshot and quit.
# Usage: scripts/shot.sh <scenario> [camera-preset] [seconds]  ->  shots/<scenario>-<preset>.png
source "$(dirname "$0")/_common.sh"
SCENARIO="${1:-empty}"
PRESET="${2:-default}"
SECONDS_TO_RUN="${3:-2}"
OUT="$ROOT/shots/${SCENARIO}-${PRESET}.png"
mkdir -p "$ROOT/shots"
rm -f "$OUT"
prepare_game
with_display "$GODOT" --path "$ROOT/game" --resolution 1920x1080 -- \
  "--scenario=$SCENARIO" "--preset=$PRESET" "--shot=$OUT" "--seconds=$SECONDS_TO_RUN"
if [ -f "$OUT" ]; then echo "$OUT"; else echo "Screenshot was not written" >&2; exit 1; fi
