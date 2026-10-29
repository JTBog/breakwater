# CLAUDE.md: Breakwater

Breakwater is a personal zombie-survival RTS inspired by *Ebbtide* (ProjectAlive). **`PLAN.md` is the source of truth** for design, architecture and the task roadmap. Read it at the start of every session, along with `docs/PROGRESS.md`.

## How to work
- Work the roadmap **in order** (PLAN.md §7). Do one task per session unless it's tiny. Phase exit criteria are hard gates.
- At the start: read `docs/PROGRESS.md` and state which task you're on. At the end: update `docs/PROGRESS.md` (what was done, measurements, known issues, next task) and commit with a message like `P1.3: infection — dead units reanimate`.
- Significant or irreversible choices go in `docs/decisions/NNN-title.md` (a short ADR: context, decision, consequences).
- If a task turns out much bigger than expected, split it in PROGRESS.md and tell the owner rather than silently widening scope.
- The owner wants a **commander and spectator game**. Don't add direct unit control (possess/TAB) or per-soldier micro.

## Architecture rules (non-negotiable)
- `sim/` is a **pure .NET library**. It never references Godot. All gameplay rules live there. `game/` only renders, plays audio, handles input and turns input into **commands**.
- The sim uses a fixed 20 Hz tick, is deterministic per seed, and uses **only** `Rng` from `sim/Core` (never `System.Random` or `GD.Randf` in the sim).
- Hot data is structure-of-arrays in pools. No per-agent classes, LINQ or allocations in tick loops.
- Gameplay numbers live in `game/data/*.json`, not hard-coded. Names come from `data/names.json`.
- The view reads snapshots and interpolates. It never mutates sim state directly.

## Commands
(Create these in task 0.1 and keep this section accurate.)
- `scripts/test.sh` runs `dotnet test` for `sim.tests`.
- `scripts/bench.sh [filter]` runs BenchmarkDotNet benches. Paste key numbers into PROGRESS.md.
- `scripts/run.sh [scenario]` launches the game.
- `scripts/shot.sh <scenario> <camera-preset> <seconds>` launches, waits, saves `shots/<scenario>-<preset>.png` and quits.
- `scripts/soak.sh <scenario> <hours>` runs the headless sim at max speed and reports stats and asserts.

## Verification
- Sim changes need xUnit tests. Run `scripts/test.sh` before every commit.
- Performance-sensitive changes: run the relevant bench and compare it with the last numbers in PROGRESS.md. A regression over 15% has to be fixed or justified.
- **Visual changes: always run `scripts/shot.sh`, open the PNG and look at it**, comparing it with the matching image in `reference/frames/` and the notes in `reference/observations.md`. Describe what you see honestly. Ask the owner to eyeball motion (shimmer, animation, feel), because a screenshot can't show it.

## Copyright and assets
- `reference/` is for study only. **Never copy, trace, import or ship anything from it.** Never reuse Ebbtide's text or names. Use our names from PLAN.md §2.
- Only add assets that are CC0, CC-BY or under an equally permissive license (Mixamo animations are OK, baked into VATs). **Every asset file gets a line in `CREDITS.md`** (path, source URL, author, license) in the same commit.
- Prefer procedural or in-code art when a free asset doesn't fit. Fetch scripts for downloadable packs go in `tools/fetch_assets/`. Don't commit huge raw downloads; commit the processed outputs.
- OpenStreetMap data is ODbL: keep the "© OpenStreetMap contributors" credit visible on the ops map and in CREDITS.md.

## Style
- C#: file-scoped namespaces, nullable enabled, `readonly struct` for small value types, and descriptive names (`agentPosX`, not `px`). Comments explain *why*, not *what*.
- Python tools: 3.11+, type hints, CLI via `argparse`, requirements in `tools/requirements.txt`.
- Shaders: a header comment stating inputs and the reference look they're aiming for.
