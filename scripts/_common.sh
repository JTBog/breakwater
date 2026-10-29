#!/usr/bin/env bash
# Shared settings for the helper scripts. Override GODOT to point at your Godot .NET binary.
set -euo pipefail
ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
GODOT="${GODOT:-godot}"

if ! command -v "$GODOT" >/dev/null 2>&1; then
  echo "Godot not found. Set GODOT=/path/to/Godot_v4.x-stable_mono binary." >&2
  exit 1
fi

# Godot must import assets once before the first run, and the C# assembly must be built.
prepare_game() {
  if [ ! -d "$ROOT/game/.godot" ]; then
    "$GODOT" --headless --path "$ROOT/game" --import >/dev/null 2>&1 || true
  fi
  dotnet build "$ROOT/game/Breakwater.csproj" -nologo -v quiet
}

# Headless cloud machines have no display: run under a virtual X server when one is needed.
with_display() {
  if [ -z "${DISPLAY:-}" ] && [ "$(uname)" = "Linux" ] && command -v xvfb-run >/dev/null 2>&1; then
    xvfb-run -a -s "-screen 0 1920x1080x24" "$@"
  else
    "$@"
  fi
}
