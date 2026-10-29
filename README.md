# Breakwater: plan pack for a local Claude Code session

This folder is a ready-to-start project seed for recreating *Ebbtide* as a personal project.

| File | What it is |
|---|---|
| `PLAN.md` | The full design and build plan: systems, architecture, and a task-by-task roadmap with exit criteria |
| `CLAUDE.md` | Standing instructions Claude Code loads automatically in this folder |
| `reference/ebbtide-store-text.md` | The Ebbtide store page and developer FAQ, for reference only |
| `reference/observations.md` | Frame-by-frame notes on the official clips (art, UI, mechanics) |
| `reference/frames/` | Frames extracted from the official clips, for visual comparison only. Never shipped |
| `docs/PROGRESS.md` | The running progress log Claude updates after every task |

## 1. Install prerequisites (on your machine)
- **Godot 4.x, .NET edition** (latest stable) from godotengine.org. Put the binary on your PATH as `godot`, or tell Claude where it is.
- **.NET SDK**: the version your Godot release requires (8 or newer).
- **Python 3.11+** (for the OpenStreetMap tools later).
- **git**. Optionally **ffmpeg**.
- **Claude Code** (CLI, desktop or IDE extension).

A mid-range GPU is recommended, since the crowd tests target 30k animated zombies.

## 2. Start
```bash
mkdir breakwater && cd breakwater
# unzip this pack here so PLAN.md and CLAUDE.md sit at the root
git init && git add -A && git commit -m "Plan pack"
claude
```

## 3. First message to paste into Claude Code
> Read CLAUDE.md, PLAN.md and reference/observations.md, then start Phase 0, task 0.1. My OS is **<Windows/macOS/Linux>** and Godot is at **<path or "on PATH">**. Before writing code, list anything you need me to install or confirm.

After each task, the follow-up is simply:
> Continue with the next task in PLAN.md.

Some tasks need you to look at the result: frame-rate numbers on your machine, shimmer or motion while panning, or "is this fun to watch". Claude will ask when it gets to those.
