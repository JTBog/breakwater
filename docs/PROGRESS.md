# Progress log

Newest entries at the top. Each entry: date, task ID, what was done, measurements, known issues, next task.

## 2026-10-29: P0.1 Bootstrap ✅
- Repo layout per PLAN.md §6.2: `sim/` (pure .NET library), `sim.tests/` (xUnit), `sim.bench/` (BenchmarkDotNet), `tools/SimRunner/` (headless soak runner), `game/` (Godot 4.7.2 .NET project referencing `sim/`), `scripts/`, `Breakwater.sln`.
- Sim core: `Rng` (xoshiro256**, seedable, save/restore), `Clock` (20 Hz fixed tick plus calendar at `CalendarRate` = 30 calendar-s per sim-s), `SimWorld` with `StateHash()` for determinism checks.
- Game: `Boot` autoload parses `--scenario/--preset/--shot/--seconds`. `SimHost` runs the fixed tick with capped catch-up and an interpolation alpha. `Main` builds the `empty` scenario (ground, sun with shadows, placeholder blocks) and a debug overlay with day, time, tick and fps. Camera presets: default, close, ops.
- Scripts: build, test, bench, run, shot, soak.
- **Checks:** `scripts/test.sh` gives 9/9 passing. `scripts/shot.sh empty` writes `shots/empty-default.png` (verified visually: blocks, shadows and overlay all correct). `scripts/soak.sh empty 48` reaches day 2 with a stable hash.
- Notes: the cloud box renders with software Vulkan (lavapipe), at about 6 fps, so fps numbers only count on the owner's machine.
- **Next: P0.2, the crowd-rendering spike** (VAT + MultiMesh, 30k instances). This needs a fps measurement on the owner's GPU.

## 2026-10-28: Plan pack created
- Research done: Steam store page, developer FAQ, five official clips analysed (see reference/).
- Next: **Phase 0, task 0.1 (Bootstrap)**.
