# BREAKWATER: Design and Build Plan

A personal-project recreation of **Ebbtide** (ProjectAlive, unreleased) as closely as practical, with original names and freely licensed assets.
Reference material is in `reference/`: the store text, frame-by-frame notes from the official clips, and the extracted frames. **Read `reference/observations.md` before any visual or UI work.**

**Scope decisions (from the owner):**
- Personal project. Stay copyright-clean anyway (§2).
- **Real Los Angeles** map from OpenStreetMap.
- Art is **procedural or made in code, or downloaded from free, permissively licensed packs**.
- **No direct control (no TAB-to-possess).** This is a commander's and spectator's game: give orders to **units and groups**, then watch them fight. No twitch micro of individual soldiers is needed, though selecting a squad or vehicle and ordering it is fine.
- Technical experiments (Phase 0) come first.

---

## 1. The target, in one paragraph

You command the last carrier task force off Los Angeles. A **single continuous real-time world** is viewed at two altitudes. The **operations map** shows the whole city as grey extruded blocks with unit pins, district states and Nexus warnings. Scroll down into any street and it becomes the **3D pixel-art battlefield**, with no loading and no auto-resolve. Fifteen districts slide from **Hold → Crisis → Lost** while a hive mind with a million zombies **ambushes convoys, cuts roads, besieges bases and hunts stranded units**, always with a warning first. You win with **logistics** (every bullet, litre of fuel and wall block is physically hauled by truck or helicopter), **forward operating bases** built block by block, and **combined arms**: police, sheriffs, Guard and Marines on the ground, attack and transport helicopters, artillery and rockets, naval guns and cruise missiles, carrier jets, gunship and drone orbits. Chapter 1 ends with what you evacuate to the Port of LA, which becomes your starting force for an (optional, later) world campaign.

---

## 2. Copyright-clean naming and assets

Mechanics aren't protected, but names, art, text and trademarks are. All names live in **data files** (`data/names.json`, unit JSON), so renaming takes one edit.

| Ebbtide / real-world | Breakwater (ours) |
|---|---|
| Ebbtide | **Breakwater** |
| TF-1 "Ark" | **TF-7 "Haven"** (carrier CV-7 *Haven*) |
| The Nexus (hive mind) | **The Choir** (its hidden "command nodes" become **Chorus Nodes**) |
| LAPD | **City Police (CPD)**, with no real badges or livery text |
| LA County Sheriff | **County Sheriff (CSD)** |
| National Guard | **State Guard** |
| Marines from Camp Pendleton | **Marines**, staging from Camp Pendleton (a place name, fine) |
| AH-64 Apache | **AH-9 Kestrel** (heavy attack helicopter) |
| AH-1Z Viper / Little Bird | **AH-5 Wasp** (light attack helicopter) |
| UH-60 Black Hawk | **UH-8 Heron** (utility, fast-rope, sling) |
| CH-47 / CH-53 | **CH-12 Ox** (heavy lift, big sling loads) |
| M1 Abrams | **MBT-4 Bulwark** |
| M2 Bradley | **IFV-3 Badger** |
| LAV-25 | **LAV-8 Coyote** |
| Humvee | **MUV Jackal** |
| MRAP | **MRAP Bison** |
| Hooklift truck (PLS/HEMTT) | **HL-10 Hooklift** |
| M777 howitzer | **H155 Longbow** (towed 155 mm) |
| HIMARS | **RS-6 Rainmaker** (rocket truck) |
| Destroyer 5-in gun | "5-inch naval gun" on destroyer **DD *Marlin*** (a generic term) |
| Tomahawk | **CM-2 Lance** cruise missile |
| F/A-18 | **F-40 Talon** (carrier strike fighter) |
| F-15E | **F-27 Striker** (land-based heavy strike) |
| F-35B | **F-50 Wraith** (STOVL stealth strike) |
| A-10 | **A-12 Boar** (gun run, close air support) |
| AC-130J | **AC-90 Halo** (gunship orbit) |
| MQ-9 Reaper | **MQ-11 Vigil** (drone orbit, light missiles) |
| Hellfire | **LGM** (laser-guided missile) |
| CIWS | **PDG** (ground point-defense gun) |
| HESCO | **Gabion barrier** |

**Rules:**
- **Never copy, trace or ship anything from `reference/`.** Don't use Ebbtide's text, UI layout pixel-for-pixel, logo or audio. Matching the *function* of the UI is fine.
- Use only assets with **CC0, CC-BY or similarly permissive** licenses, and record each one in `CREDITS.md` (source URL, author, license).
- OSM map data is **ODbL**: credit "© OpenStreetMap contributors" in the credits and on the ops map.

---

## 3. Design pillars

1. **The map is the battlefield.** One world, one clock, no auto-resolve. Every loss persists.
2. **Scale you can feel.** About 1M zombies city-wide (abstract), 10k–30k rendered in a fight.
3. **Logistics is the real enemy.** Ammo, fuel and materials have to physically move.
4. **A horde that fights back.** The Choir targets weaknesses and always telegraphs its attacks.
5. **Command, then watch.** Units are competent on their own. The fun is planning, then watching combined arms play out, with good cameras to enjoy it.
6. **Readable 3D pixel art.** A crisp, stable pixel look, high-res UI on top, a strong day/night mood.

---

## 4. Core loops

**Operations loop (minutes):** read the ops map (district states, visible hordes, warnings, deadlines) → set priorities (hold Inglewood, evacuate Koreatown, open the 110) → give orders (hold, delay, withdraw, move, escort) → schedule convoys and sling lifts → set fire-support policies → run time at 1x to 4x, pausing to react.

**Battle loop (seconds to minutes, optional to watch):** a warning fires → scroll into the street → adjust a few group orders, designate missile targets, call strikes from the support wheel, place or repair FOB walls → watch the fight → scroll out.

**Chapter 1 arc:** the tutorial at a beachhead → the outbreak spreads district by district → Marines arrive → the final evacuation to the Port of LA. **Score and carry-over** = the people, troops, equipment and supplies at the port when the chapter ends.

**Lose:** the Port of LA falls, or Haven's combat personnel or supplies collapse below a threshold.

---

## 5. Systems specification

### 5.1 Clocks and time
- **A single simulation clock** with a fixed tick (**20 Hz**). Speeds are **pause, 1x, 2x, 4x**, run as more ticks per frame. Battles run at real scale (soldiers move at real speeds).
- **The calendar runs faster than physics:** it advances **`CALENDAR_RATE` (default 30) game-seconds per sim-second**. Deadlines ("45h left on the ops clock"), day/night, shifts and resupply schedules use the calendar, while movement and combat use physics time. It's tunable and gets prototyped in Phase 4.
- Day/night cycle: dusk and dawn color grades. At night zombies are faster and more aggressive, sight is shorter, and lights attract the horde.

### 5.2 World: Los Angeles
- **Playable area:** about 30 × 40 km, from the **Hollywood Hills to the Port of LA / San Pedro**, the Pacific coast (Santa Monica to Palos Verdes) to East LA. Exact bounds are set in Phase 0 task 0.5.
- **15 districts**, each a polygon built from OSM admin and neighborhood boundaries. Proposed: Hollywood, Hollywood Hills, Downtown, Koreatown/Mid-City, East LA, Westside (Santa Monica/Westwood), Venice/Marina, Culver City, Inglewood, LAX/Del Aire, South LA, Watts/Compton, Torrance, Long Beach, Port of LA/San Pedro.
- **District state:** `Hold → Crisis → Lost`, driven by a ratio of zombie density to defense, civilian losses and how many key sites are held (police stations, hospitals, fuel depots). Each district tracks civilians (alive, sheltered, infected), defenders, zombies (density) and key sites.
- **Map data pipeline** (§6.4): roads with class and lanes, building footprints with heights (from OSM `height`/`building:levels`, or defaults by building type), land use (parks, water, the LA River channel), freeways, the coastline and SRTM elevation for the hills.
- **Key real locations** as gameplay sites: LAX, the Port of LA, Camp Pendleton (off-map staging), the Santa Monica pier, Dodger Stadium, Union Station, hospitals and police stations (from OSM POIs).

### 5.3 The seamless zoom (camera)
- **One camera** with a continuous altitude from about 25 m to about 40 km. The pitch eases from about 55° (battle) to about 75° (ops), and the camera rotates in 15° steps with Q/E or a middle-drag.
- **Render tiers by altitude** (crossfading inside each band):

| Tier | Altitude | Look | Zombie rendering |
|---|---|---|---|
| Battlefield | < 600 m | Full pixel-art 3D, textured buildings, props, VFX | Individual instanced agents |
| Transition | 600 m – 2 km | Simplified buildings, no props | Impostor dots, then a density splat |
| Operations | > 2 km | Flat grey extrusions on a grey ground, green parks, street labels, district borders (red when Lost/Crisis), unit pins | **Only hordes in line of sight**, as density or blob markers |

- **Entering a battle:** when zoomed fully into the ops tier over a point, show "TO BATTLEFIELD · WHEEL ▼ ×3" with a progress bar. Three more wheel notches commit and the camera dives in. There's no loading because the tiles are already streamed. Scrolling back out works the same way. "LIVE" markers show on the ops map wherever a battle bubble is active.

### 5.4 Zombie simulation: the hard core
**Three tiers, with the count conserved across tiers at all times.**

1. **Density field (city scale):** a grid of 64 m cells holding the zombie count, agitation (0–1) and a drift vector. Each calendar minute it **diffuses along the road network**, drifts toward stimuli (noise, light, people) and **converts civilians** in cells it overruns. This holds about 1M zombies cheaply.
2. **Hordes (operational):** discrete groups of 100–50,000 that the Choir commands. They travel the **road graph** as blobs, and split and merge. Groups pull from density when formed and dissolve back into it when released.
3. **Agents (battle bubbles):** individual zombies in **activation bubbles**, which are circles of about 300–600 m around any contact between player forces and zombies, plus wherever the camera is in the battlefield tier. **Every contact gets a real bubble**, so there's no auto-resolve. Bubbles away from the camera run the same rules without rendering, and may tick at 10 Hz.
   - **Promotion:** cells spawn agents at positions sampled inside their cell, preferring off-screen edges. **Demotion** folds agents back into the cells. Unit tests assert that the count is conserved.
   - **Budget:** a total agent cap across all bubbles (default **40,000**, a user setting). When over budget, distant bubbles coarsen: each agent stands for a weight of 2–4 zombies, with matching HP and damage.
   - **Data layout:** structure-of-arrays (`float[] posX, posZ, velX, velZ, hp; byte[] state, variant; ushort[] animFrame`) in a pool with free-lists. The update runs in parallel over chunks.
   - **Movement:** **flow fields** on a 2 m nav grid per bubble, built as an eikonal/Dijkstra wave from targets (soldiers, civilians, walls, noise points) and recomputed incrementally a few times per second. **Separation** comes from a spatial hash (2 m cells) with a capped neighbor count. Speeds: walker 1.2 m/s, runner 3.5 m/s (more at night).
   - **States:** Idle → Wander → Alerted → Chase → Attack(target) → Climb → Cling → Dead (ragdoll-lite: a flung impulse, then a corpse decal).
   - **Pile-up and climbing:** blocked agents add to a **pile-height field** (0.5 m cells) along obstacle edges. An agent's height equals the pile height under it, and it ramps upward. Once pile height ≥ wall or roof height, the obstacle edge becomes passable in the flow field, so zombies spill over, matching the siege screenshot. Piles decay when pressure drops.
   - **Helicopter clinging:** when a helicopter is below about 6 m AGL over a crowd, nearby agents get a jump-and-latch chance. Latched zombies add mass (slower climb, more fuel burn) and push crash risk above a threshold. Door gunners and climbing above 10 m shake them off.
   - **Infection:** killed soldiers and civilians **rise again** after 5–15 s and become new agents. This feeds the horde.
   - **Variants:** walker (base), runner, **brute** (high wall damage, slow), and maybe a crawler. There are 6–10 clothing palettes, including **orange jumpsuits** per the reference.
   - **Stimuli:** every weapon has a **noise radius** (rifle 150 m, MG 250 m, tank gun 600 m, explosions 800 m+, helicopter rotors 400 m), and lights at night reach 200 m. Noise pushes agitation and drift in both the density and agent tiers, so loud fire support draws more horde.

### 5.5 The Choir (hive-mind director)
- Runs every few calendar minutes on the **operational graph** (road graph plus districts plus player assets).
- **Threat model of the player:** bases (wall HP per segment, garrison, ammo and fuel stocks, walls missing an entrance), convoys (route, escort strength, ETA at chokepoints), **stranded units** (fuel = 0, ammo low, cut off), noise history and civilian clusters.
- **Operation types** (all from the store page):
  - **Ambush convoy:** mass a horde at a chokepoint ahead of a convoy's ETA.
  - **Cut road:** occupy a key intersection, bridge or freeway ramp to isolate a district or base.
  - **Besiege base:** encircle it, probe, then mass on the weakest wall segment, with brutes leading.
  - **Hunt stranded:** send runners toward isolated units and survivors.
  - Plus **district surge** (push a Crisis district to Lost) and **rescue bait** (an attack aimed at whatever responds).
- **Scoring:** a utility score `value × P(success) − cost`, within a **pressure budget** that grows over time and with noise, and is shaped by difficulty.
- **Warnings (mandatory):** when an operation commits, an alert fires with type, location, estimated size (±, depending on whether you have line of sight) and ETA. The alert buttons let you **Reroute** (convoys), **Retreat**, or **Fire support**. Lead time comes from difficulty (default ≥ 90 s of sim time).
- **Fog:** hordes show on the ops map **only inside the line of sight** of your units, bases, drones and recon flights. Elsewhere you see last-known estimates that fade over time.

### 5.6 Player forces
Units are **autonomous and competent**. Orders go to **groups** (a squad, a vehicle section or a helicopter flight), and to individual vehicles when you want.

| Faction | Units (ours) | Notes |
|---|---|---|
| City Police | Patrol car (drives, lightbar, siren noise), SWAT squad, armored rescue truck | Early. Many weak units. Morale breaks easily |
| County Sheriff | Deputy squad, patrol SUV, Sheriff helicopter (light, unarmed spotter) | Sturdier police |
| State Guard | Rifle squad, MG team, MUV Jackal, MRAP Bison, cargo truck, **HL-10 Hooklift**, fuel tanker, **UH-8 Heron** | Backbone and logistics |
| Marines (called in) | Rifle squads, LAV-8 Coyote, IFV-3 Badger, MBT-4 Bulwark, H155 battery, RS-6 Rainmaker, AH-9 Kestrel, AH-5 Wasp, CH-12 Ox | Arrive by sea or air on call, with cooldowns and costs |
| TF-7 Haven | Carrier deck (F-40 Talon ×8), DD *Marlin* (5-inch gun, CM-2 Lance missiles), land-based strikes (F-27, F-50, A-12), orbits (AC-90 Halo, MQ-11 Vigil) | Fire support only. **Finite magazines** |

- **Unit data** (JSON per type): HP, armor class, speed (road/offroad/air), crew or squad size, sight range, weapons (damage, rate, range, AoE, **noise**, ammo type, **rounds per shot**), **ammo capacity**, **fuel capacity and burn** (per km and per idle hour; aircraft per minute), cargo (seats, cargo mass, sling capacity), morale, cost and availability.
- **Group orders (operational):** **Hold** (dig in, never retreat), **Delay** (fight, then bound back along a path to a fallback point), **Withdraw** (disengage to a named base), Move (with a road preference), Escort (attach to a convoy), Patrol, Evacuate (go to civilians, load, return).
- **Battlefield orders** (the command card from the reference): Advance, Attack-move, Queue, **Zone** (defend an area), Clear, **Policy** (rules of engagement), MG on/off. Helicopters also get **LGM (designate)**, Rockets, **Orbit / Attack-orbit / Orbit direction**, Stop, **Land / Take off**, and formations **V / Line / Column / Spacing**.
- **Formations and stances:** squads use line, wedge or column, and the stances are Aggressive / Defensive / Hold fire, which trade **noise against safety**.
- **Helicopter actions:** **fast-rope** squads (hover at about 10 m, which risks clinging under 6 m), **land on rooftops** and deploy squads, and **sling-load** cargo.
- **Auto-behaviors** so watching is fun: units auto-target by threat, retreat to resupply at 15% ammo or fuel (a toggle), medics revive downed soldiers before they turn, and vehicles avoid getting swarmed (reverse, then move).
- **Morale:** police break into a rout under pressure, which panics civilians. Guard units are steadier and Marines very steady.

### 5.7 Logistics
- **City-level resources** (shown in the top or bottom bar): **Ammo, Fuel, Materials, Medical, Combat Personnel**, plus Food and Population for civilians. These come from the reference's resource strip and globe bar.
- **Physical stock only:** each base and vehicle has an inventory. Units **rearm and refuel only at held bases** (ammo depot, fuel point), or from an adjacent supply truck or fuel tanker acting as a mobile point.
- **Sources:** Haven offshore (lands supplies at the Port, a beach landing site or a helipad), plus captured depots (gas stations give fuel, armories give ammo, hardware stores and lumber yards give materials). These come from OSM POIs.
- **Convoys:** pick cargo (supplies, troops or refugees), a source and a destination, then a route (auto, using road risk, or manual waypoints), plus an escort. They can be **recurring**. **Helicopter convoys** ignore roads but cost fuel, make rotor noise and are exposed to clinging when hovering.
- **Construction materials** move **only** by HL-10 Hooklift (one block module or pallet per trip) or helicopter **sling** (Heron small, Ox large).
- **Overlays:** routes colored by Choir risk, stock gauges on bases, and unit ammo and fuel pips on pins.
- **Stranding:** fuel = 0 means a unit is immobile. It becomes a "CUT OFF" event with a deadline on the ops clock, and the Choir hunt targets it.

### 5.8 Forward operating bases
- **Choose a site:** clear it (no zombies within a radius for N seconds), then place the FOB core (a command tent). This gives a build radius.
- **Build UI tabs:** Select · Walls · Modify · Defense · Fire support · Supply · Lighting · Blueprints.
- **Walls:** **Gabion, Concrete, Scaffold** (a firing step on the inside), **Sandbags**. Height is in **levels**, and the shapes are **Line** or **Box** (drag a polygon). Walls are **modular 1 m-ish blocks**: placing creates **blueprint ghosts**, and blocks become real once materials arrive and builders work.
- **Validation:** a **flood fill** warns "walled area without an entrance: troops get trapped" and offers "Make entrance". **Gates** open for friendlies.
- **Defense:** MG nest, **PDG** (point-defense gun), guard tower, minigun nest, a "protective ring" of blocks, and **mortar pits**. Turrets have a **sector arc** (centre plus width) and fire modes **Hold / Sector / Narrow / Widen / Auto**, and show HP and ammo.
- **Supply:** ammo depot, fuel point, helipad, field hospital, barracks.
- **Lighting:** floodlights, which help you see at night **and attract zombies**.
- **Blueprints:** save and load base layouts.
- **Wall HP and damage states.** Brutes and pile-ups are the main threats, and repair costs materials.

### 5.9 Fire support (support wheel, hotkey K, plus the ops map)
| Asset | Behavior | Constraint |
|---|---|---|
| H155 Longbow battery | HE, smoke or illumination on an area. Flight time 10–40 s | Base-held battery, range ring, shells from base stock |
| RS-6 Rainmaker | Rocket salvo over a large area | Long reload, from truck stock |
| 5-inch naval gun (DD *Marlin*) | Fast, accurate, coastal range only | **Finite ship magazine** (e.g. 1,200 rounds) |
| CM-2 Lance | A single cruise missile that **collapses buildings** | Very finite (e.g. 12), long flight |
| F-40 Talon (deck) | Bomb, missile or strafe sortie. Launch → transit → strike → return → turnaround | Deck has ×8, ready/alert states, fuel and ordnance from carrier stock |
| F-27 / F-50 / A-12 | Heavy strike, precision strike, gun run | Few sorties per day |
| AC-90 Halo orbit | Circles a point for N minutes and auto-engages | 1 available, long cooldown |
| MQ-11 Vigil orbit | Persistent LOS (fog reveal), light missiles | 2 available |

- **Building collapse:** each building's mesh is generated in **floor slabs or chunks**. Heavy weapons destroy chunks, upper floors fall, and the result is a **rubble footprint** that blocks the road graph and nav grid and kills agents underneath.
- **Noise:** all fire support is very loud, which is the core tradeoff.

### 5.10 Civilians and evacuation
- Civilians start as shelter clusters in buildings (OSM residential density). They appear as agents in bubbles and as counts on the ops map.
- **Evacuate:** a unit or convoy reaches them, loads them (with a capacity), and takes them to the Port of LA or a held base. **"You can't save everyone":** prioritization is the moral and strategic choice.
- Overrun civilians become zombies, and the district slides toward Lost.

### 5.11 Spectator features (because the focus is watching)
- **A director camera** ("Watch" toggle): auto-cuts to the most interesting active bubble (score = kills/sec + explosions + units in danger + player focus), with smooth fly-ins and slow orbits.
- **Follow** any unit, convoy, aircraft or strike (an "eye on the strike" camera rides with the cruise missile or jet).
- **Slow-motion** at 0.25x/0.5x in battles, and a **cinematic mode** that hides the UI.
- **Kill feed, a statistics overlay** and an after-action timeline per battle.
- A **photo mode** that saves a PNG.

### 5.12 UI (matching the *function* of the reference, in our own visual design)
- Crisp, high-res UI over the pixel world: dark translucent panels, a sans-serif body (Inter, OFL) and **pixel-font headers** (Silkscreen or Press Start 2P, OFL). Ready = green, warning = amber, danger = red.
- **Ops map:** district chips with states, unit pins with ETA lines, **LIVE** markers, warning cards with action buttons, deadline cards ("CUT OFF S01 · 45h left"), an area info panel (horde / defense / units counts), the air-support roster with ready/alert states, unit roster tabs (All / Air / Ground / Naval), a field-order panel, and helicopter formation buttons.
- **Battlefield:** callsign tags (PC-1, SQ-1, AH-2), selection rings, a command card with hotkeys, a selection summary (stores, salvo), a resource strip, the support wheel, a minimap with filters and view heading, the day/time widget, and a music widget.
- Time controls are visible everywhere.
- All strings go through a translation table (English only for now).

### 5.13 Art pipeline: "3D pixel art"
- Render the 3D world into a **low-res SubViewport** (default **640×360**, options 480×270 and 960×540) with no MSAA, then upscale **nearest-neighbor** by an integer factor to fill the screen.
- **Stabilize:** snap the camera position to the low-res texel grid in view space, and shift the upscaled image by the sub-texel remainder for smooth scrolling without shimmer.
- **Shading:** a toon ramp (3–4 bands), hard directional shadows, optional **palette quantization** (LUT), and an **outline pass** from depth and normal discontinuities.
- **Grading:** per-time-of-day LUTs (day neutral, dusk orange, night blue), plus point lights (headlights, lightbars, muzzle flashes, floodlights).
- **VFX:** pixel particles (muzzle flash, tracers, blood, smoke, explosions, debris) and decals (blood, tire marks, scorch, cracks).
- **Ops-tier style:** an unlit flat-grey material on the same building meshes, plus an ortho-like high pitch and crisp vector overlays.

### 5.14 Audio
- A horde ambience bed whose loudness and density scale with nearby agent count, positional gunfire, rotor and jet loops, explosions and sirens.
- **Radio barks** for alerts, as text-to-speech or recorded lines, plus sound effects. Start with text plus radio-squelch sounds.
- A **music player** with "Calm" and "Combat" playlists that crossfade by threat, using CC0 or CC-BY tracks.

### 5.15 Save and load
- A full-state snapshot: calendar, density grid, hordes, bubbles (agents serialized compactly), units, inventories, structures, the Choir's state and the RNG state. Autosave every in-game 6 h, plus quicksave and quickload.

### 5.16 Out of scope for v1
- Direct control (owner decision), multiplayer, mod support, and the world campaign (Phase 9, optional).

---

## 6. Technical architecture

### 6.1 Engine: **Godot 4.x, .NET (C#) edition**
- Ebbtide uses **Unity 6 + C#**. We use Godot because it suits a Claude-driven local workflow: **text scene files, a full CLI (headless runs, scripted screenshots, exports), no editor login or license**, fast iteration, and MIT licensing.
- The simulation is a **plain .NET class library** with no engine types, so a later port to Unity would only mean rewriting the view layer.
- Crowd rendering uses **MultiMesh**, with the per-instance buffer written in one call via `RenderingServer.MultimeshSetBuffer(float[])`, plus **vertex-animation textures (VAT)**. Impostor sprites are the fallback for far LODs.
- Hot loops are C# with `System.Numerics`, SoA arrays and `Parallel.For` over chunks. If profiling demands it, we escalate to a GDExtension (C++) or a compute shader.

### 6.2 Repository layout
```
breakwater/
  CLAUDE.md  PLAN.md  CREDITS.md  README.md
  docs/PROGRESS.md            # running log, updated after every task
  docs/decisions/             # ADRs: one short file per significant decision
  reference/                  # Ebbtide reference material. NEVER shipped or imported into the game
  sim/                        # Breakwater.Sim: pure .NET class library (no Godot)
    Core/       SimWorld, Clock, Calendar, Rng, Ids, EventBus
    Map/        RoadGraph, NavGrid, Districts, Buildings, TileIndex
    Horde/      DensityField, Hordes, Bubbles, AgentPool, FlowField, SpatialHash, PileField, Infection
    Units/      UnitDefs, Units, Groups, Orders, Weapons, Combat, Morale, Sensors(LOS)
    Logistics/  Inventory, Convoys, Resupply, Cargo
    Bases/      Fob, Blocks, Blueprints, Construction, Turrets, FloodFillCheck
    Fire/       Artillery, Naval, AirOps (deck cycle, sorties), Orbits, Strikes
    Destruction/ BuildingChunks, Rubble
    Choir/      ThreatModel, Operations, Planner, Warnings
    Civilians/  Shelters, Evacuation
    Save/
  sim.tests/                  # xUnit tests + BenchmarkDotNet perf benches
  game/                       # Godot project (C#), references sim/
    project.godot
    scenes/ (Main, Battlefield, OpsOverlay, UI/*)
    view/   (CameraRig, PixelPipeline, CrowdRenderer, UnitViews, BuildingStreamer, VFX, Audio)
    ui/
    shaders/ (pixel_upscale, outline, toon, vat_crowd, ops_flat, decals)
    assets/  (models, textures, audio, fonts). Every file is listed in CREDITS.md
    data/    (units/*.json, weapons, structures, districts, names.json, difficulty)
  tools/                      # Python 3: OSM → tiles, VAT baker, asset fetch scripts, balance sims
    osm/  vat/  fetch_assets/
  scripts/                    # shell helpers: build, test, shot.sh (screenshot), bench, export
```

### 6.3 Simulation and view boundary
- `SimWorld.Tick()` is deterministic for a seed. The view reads **immutable snapshots** (double-buffered arrays) and interpolates between ticks.
- The view sends **commands** (`OrderGroup`, `PlaceBlueprint`, `CallStrike`…) through a queue applied at the start of a tick. This is replay-friendly and lets the headless sim run tests.
- A **headless runner** (`dotnet run --project tools/SimRunner`) can simulate N hours of a scenario at max speed and output stats. It's used for soak and balance tests.

### 6.4 Map pipeline (tools/osm)
1. Fetch a **Geofabrik SoCal extract** (`socal-latest.osm.pbf`) and **clip to the play bounds** with `osmium extract`. Use Overpass API for the small Phase 0 spike.
2. Use `pyrosm`/`osmium` + `shapely` + `pyproj` to project to local meters (UTM 11N, origin at a point Downtown).
3. Extract **roads** (class, lanes, oneway, bridges, freeways), **buildings** (footprint, height = `height` or `levels × 3.2 m`, or a default by type and land use), land use, water, the LA River channel, POIs (police, hospital, fuel, hardware stores, ports, airfields), district polygons and **SRTM elevation**.
4. Emit **512 m tiles** as compact binary files: building footprints plus heights, road segments, a 2 m nav-grid bitmap, POIs and a land-use raster. Also emit a global **road graph** and **district file**.
5. In Godot, the **BuildingStreamer** builds meshes per tile at runtime: extruded footprints, with procedural facades from a shader (window grid, floor bands, color variance by type), and roofs with AC units and parapets. **Chunked by floor** for destruction. Ops-tier tiles use merged flat-grey meshes.
6. Credit OSM and SRTM in CREDITS.md and on the ops map.

### 6.5 Performance budgets (target: mid-range PC, e.g. RTX 3060 or M2 Pro, 60 fps)
- Sim tick ≤ **8 ms** at 40k agents across bubbles plus 500 units, measured by a headless benchmark.
- Render ≤ **8 ms**: under 2,000 draw calls, crowd ≤ 2 ms, pixel pipeline ≤ 1 ms.
- No streaming hitch > 33 ms while zooming from 40 km to 25 m.
- RAM < 4 GB.

### 6.6 Testing and verification
- **xUnit** for sim rules: count conservation, flow-field correctness, ammo and fuel accounting, construction-needs-materials, flood-fill entrance check, Choir warning before every attack, determinism (same seed gives the same hash after N ticks).
- **BenchmarkDotNet** benches for the horde update, flow field and spatial hash. Results go to `docs/PROGRESS.md` and regressions over 15% fail.
- A **screenshot harness:** `scripts/shot.sh <scenario> <camera-preset> <seconds>` runs the game, waits, saves a PNG to `shots/` and quits. Claude **looks at the PNG** after visual changes and compares it with `reference/frames/`.
- A **soak runner:** simulate 48 in-game hours headless, checking for no exceptions, no NaNs, conserved counts and bounded memory.

### 6.7 Free asset sources (check each license; record in CREDITS.md)
- **Kenney.nl** (CC0): City Kit (roads, commercial, suburban), Car Kit (police, ambulance, taxi, van, truck), particle and UI packs, audio packs.
- **Quaternius** (CC0): animated low-poly humans (soldier and civilian bases for zombie variants), city and nature props, palm trees.
- **KayKit** by Kay Lousberg (CC0): characters and city builder bits.
- **Poly Pizza** (poly.pizza, CC0/CC-BY per model): helicopters, tanks, aircraft, ships. Check each model's license.
- **Mixamo** (free with an Adobe account, royalty-free in games): zombie walk, run, attack and fall animations, plus soldier shoot, run and crouch animations. These get baked into VATs, and the raw files are never redistributed.
- **Freesound** (filter CC0), **Sonniss GDC bundles** (royalty-free) and **OpenGameArt** (CC0 music and SFX).
- **Fonts:** Google Fonts (OFL): Inter, Silkscreen, Press Start 2P.
- **Fallback, made in code:** boxy procedural vehicles and helicopters from primitives. The pixel pipeline makes simple geometry look intentional. Buildings are always procedural from OSM.

---

## 7. Roadmap

Each task is sized for about one focused Claude session. Work top to bottom. **Exit criteria are hard gates:** don't start the next phase until they're met (or explicitly re-scoped in an ADR). After each task, update `docs/PROGRESS.md` and commit.

### Phase 0: Setup and technical experiments
- **0.1 Bootstrap.** Create the repo layout (§6.2), a Godot 4 .NET project, the `sim` + `sim.tests` solution, `scripts/` (build, test, run, shot.sh), and CREDITS.md. Add a `.gitignore` covering `.godot/`, `bin/`, `obj/`, `shots/` and raw OSM data.
  *Done when:* `dotnet test` passes a trivial test, and `scripts/shot.sh empty` produces a PNG of an empty scene.
- **0.2 Spike: crowd rendering.** Import a CC0 low-poly human, bake walk, run and attack into a **VAT** (`tools/vat`), and render **30k instances** in MultiMesh with per-instance animation phase and palette variant.
  *Done when:* ≥ 60 fps at 1080p on the owner's machine, with the measurement logged.
- **0.3 Spike: horde simulation.** In `sim`, build the agent pool + flow field + spatial hash + separation. 30k agents chase moving targets around box obstacles.
  *Done when:* the benchmark shows ≤ 6 ms per tick, and the visual test shows no jitter or tunnelling through obstacles.
- **0.4 Spike: pixel pipeline.** Low-res SubViewport, integer nearest upscale, texel-snapped camera with sub-pixel offset, toon shading, an outline pass, crisp UI on top, and day/dusk/night LUTs.
  *Done when:* a screenshot set looks in the same family as `reference/frames/battle-tank-street.png`, and slow pans don't shimmer (the owner confirms).
- **0.5 Spike: OSM import.** Overpass-fetch an approximately 2 × 2 km area (Inglewood around the Forum, which matches the reference frames), build tiles and render buildings and roads in both styles (battle-textured and ops-grey).
  *Done when:* screenshots of both styles are good, with real street layout visible.
- **0.6 Spike: seamless zoom + bubble conservation.** Camera from 10 km to 30 m over the spike tiles with crossfading tiers. A density field promotes to agents when zoomed in and demotes when zoomed out.
  *Done when:* there's no visible pop, the count-conservation test is green, and no frame exceeds 33 ms during the zoom.
- **0.7 Decision record.** Write ADR-001 (engine and architecture confirmed or changed), with the measured numbers. Adjust the budgets if needed.
- **Phase exit:** all spikes meet their targets, or there's an ADR with a re-scope.

### Phase 1: Tactical sandbox (one neighborhood)
- **1.1** Zombie states, variants (walker, runner), palettes, attack and death, corpse and blood decals, and flung-on-impact impulses.
- **1.2** Infantry squads (State Guard rifle squad, MG team): auto-targeting, weapons with **ammo per shot**, reloading, squad formations, stances and noise radii.
- **1.3** **Infection:** dead soldiers and civilians rise. Civilians flee from zombies and toward soldiers.
- **1.4** A simple pre-placed base: an ammo depot that resupplies nearby units, and units that auto-return at low ammo.
- **1.5** Static walls (pre-placed gabions) with HP, plus the **pile-up and climbing field**. Soldiers on top of walls fire down.
- **1.6** Group selection and orders: Advance, Attack-move, Zone, Hold, Stop, formations. Callsign tags and selection rings.
- **1.7** A scenario framework (JSON-defined starting forces, horde waves, win/lose) and a **"Wall Siege"** scenario recreating the screenshot.
- **Exit:** a 15-minute wall-siege scenario at about 10k zombies that the owner finds fun to watch, at 60 fps.

### Phase 2: Vehicles, helicopters, combined arms
- **2.1** Vehicle movement on the road graph and nav grid (Ackermann-ish steering, reversing when swarmed, ramming and fling). Police car with lightbar, siren noise and headlights at night.
- **2.2** Armored vehicles: MUV Jackal, MRAP, Coyote, Badger, Bulwark. Turrets, main guns with AoE, and **fuel burn**.
- **2.3** Helicopters: flight model (cruise, hover, orbit), **group formations V/Line/Column + spacing**, door gunners, attack helicopter rockets, **LGM designation**, and Orbit / Attack-orbit / Orbit direction.
- **2.4** **Clinging** under 6 m AGL and shake-off, **fast-rope**, **rooftop landing + squad deploy**, and land and take off.
- **2.5** Morale and routing (police), and the vehicle "avoid being swarmed" auto-behavior.
- **2.6** The full command card UI with hotkeys and per-unit counts. Selection panel with stores, salvo and HP.
- **2.7** Scenario **"Intersection"**: an X-shaped horde at an Inglewood intersection with three attack helicopters, a tank and a police car (recreating the reference clip).
- **Exit:** combined-arms fights read clearly and look good. Helicopters, vehicles and infantry all behave believably on their own.

### Phase 3: FOB construction and logistics
- **3.1** The block-based wall system (gabion, concrete, scaffold, sandbags), levels, the Line/Box drag tool, blueprint ghosts and the build queue.
- **3.2** **Materials gate construction:** builders, build time, the **flood-fill entrance check** with a warning and "Make entrance", and gates.
- **3.3** Defenses: MG nest, PDG with **sector arc + Hold/Sector/Narrow/Widen/Auto**, guard tower, minigun nest, protective ring, mortar pit. Lighting that attracts zombies.
- **3.4** Supply facilities: ammo depot, fuel point, helipad, field hospital. Inventories, and resupply only at held bases or adjacent supply vehicles.
- **3.5** Logistics vehicles: cargo truck, fuel tanker, **HL-10 Hooklift** (block modules), **Heron/Ox sling loads**.
- **3.6** Convoys: cargo (supplies, troops, refugees), routing, escorts, recurring runs, and stranding at fuel 0.
- **3.7** The full FOB build UI (tabs per §5.8) and blueprint save/load.
- **3.8** Scenario **"Dig In"**: build a FOB from nothing, supplied by convoy, before a scripted siege at dusk.
- **Exit:** building and supplying a FOB is satisfying and readable, and running out of ammo and fuel is a real, visible threat.

### Phase 4: Operations map, LA at scale, seamless zoom
- **4.1** The full LA OSM pipeline (Geofabrik → clipped → tiles, road graph, districts, POIs, SRTM hills), with build-time and size limits documented.
- **4.2** Tile streaming at all altitudes, and the ops-tier flat style with street labels, district chips and borders.
- **4.3** City-scale **density field** (about 1M), with diffusion along roads, stimuli and civilian conversion. Visible only inside player line of sight (fog plus last-known estimates).
- **4.4** **Multiple simultaneous bubbles**, off-camera reduced-rate ticking, the agent budget and weighting, and "LIVE" markers.
- **4.5** "TO BATTLEFIELD · WHEEL ▼ ×3" entry and exit UX, smooth pitch and altitude easing, and camera rotation.
- **4.6** Ops-map orders: **Hold / Delay / Withdraw**, Move, Escort, Evacuate. Unit pins, ETA lines, the roster panel (All/Air/Ground/Naval) and the field-order panel.
- **4.7** Clocks: the calendar rate, day/night, and speed controls 1x/2x/4x and pause. Tune `CALENDAR_RATE`.
- **4.8** District states **Hold → Crisis → Lost**, with key sites.
- **Exit:** zoom from the whole city into an Inglewood street and back with no hitch while two other battles run off-screen, and all their results persist.

### Phase 5: The Choir
- **5.1** The threat model of player assets (bases, convoys, stranded units, noise).
- **5.2** Horde groups on the road graph (form from density, move, split and merge, dissolve).
- **5.3** Operations: **ambush convoy, cut road, besiege base, hunt stranded**, plus district surge.
- **5.4** The utility planner, pressure budget and difficulty curves.
- **5.5** **Warnings:** alert cards with ETA and size, and the **Reroute / Retreat / Fire support** buttons. "CUT OFF" deadline cards and rescue events.
- **5.6** A soak test: 48 h headless. Assert that every attack had a warning ≥ lead time, and log the Choir's choices for review.
- **Exit:** playtests show the Choir finds and punishes weak points, and warnings always make a response possible.

### Phase 6: Fire support and destruction
- **6.1** The support wheel (K) and calling strikes from the ops map. Range rings and cooldowns.
- **6.2** Artillery (H155 HE, smoke, illumination) and rockets (RS-6), with flight time, dispersion and noise.
- **6.3** Fleet: the 5-inch gun and CM-2 Lance with **finite magazines**, coastal range and the "eye on the strike" camera.
- **6.4** Carrier air ops: the deck pool (Talon ×8), ready/alert states, the sortie cycle (launch → transit → strike → return → turnaround) and ordnance from carrier stock. Land-based strikes (Striker, Wraith, Boar gun run).
- **6.5** Orbits: AC-90 Halo gunship (circles and auto-engages) and MQ-11 Vigil (line of sight plus light missiles).
- **6.6** **Building chunk destruction:** floors collapse, rubble, road and nav blocking, and crush damage.
- **Exit:** fire support feels powerful and costly, buildings come down convincingly, and the noise draws hordes.

### Phase 7: Chapter 1: Los Angeles campaign
- **7.1** Civilians: shelters from OSM density, evacuation orders, capacity and refugees in convoys.
- **7.2** Factions and availability: police and sheriffs at the start, State Guard mobilization, **Marines called in from Camp Pendleton** with cooldowns and costs.
- **7.3** A tutorial (beachhead, first squad, first FOB, first convoy, first strike) as a scripted overlay on the same world.
- **7.4** The campaign script: 15 districts, outbreak progression, story beats, and the final **evacuation to the Port of LA**.
- **7.5** End of chapter: the carry-over summary (people, troops, equipment, supplies) saved for Phase 9.
- **7.6** Balance passes using the headless runner (1,000 seeded runs per difficulty).
- **Exit:** Chapter 1 is playable start to finish in about 3–6 hours, and is tense and winnable.

### Phase 8: Spectator features and polish
- **8.1** The director camera, follow cams, slow-motion, cinematic mode and photo mode.
- **8.2** Audio: horde bed, weapons, rotors, jets, sirens, radio barks and the music player with Calm/Combat playlists.
- **8.3** Save/load, autosave, settings (pixel scale, agent budget, keybinds, volume) and a colorblind-safe palette.
- **8.4** Performance pass to the §6.5 budgets on the owner's machine, and exports for Windows and macOS.
- **Exit:** a stable, good-looking build the owner would happily play and show.

### Phase 9 (optional): World campaign
- A globe (Natural Earth data, public domain), regions with infection, requests ("Quarantine Request: Pearl Harbor"), the carrier group moving between ports, friendly port resupply, recruiting rescued specialists, growing the fleet, Chorus Node hunts, and a top bar with Food / Med / Components / Ammo / Fuel / Pop / Research / Naval plus Roster, Armory, Tech and War Room screens. Coastal city battles reuse the LA pipeline on other OSM extracts.

---

## 8. Risks and mitigations
| Risk | Mitigation |
|---|---|
| Crowd performance in C#/Godot | Phase 0 spikes gate everything. SoA + Parallel.For, MultiMesh buffer writes, VAT. Escalate to GDExtension or compute shaders. Agent weighting under budget |
| Count popping across tiers | Conservation unit tests, edge spawning and crossfades |
| The LA dataset is huge | 512 m tiles, a binary format, altitude-based streaming, merged ops meshes. Start with a 2 × 2 km area, then grow |
| Pixel shimmer while zooming | Texel snapping, plus a fixed render target with integer scales |
| Logistics as busywork | Recurring convoys, auto-resupply, risk overlays, and alerts only when it matters |
| The Choir feels unfair | Mandatory warnings with lead time, readable telegraphs and soak-test asserts |
| Scope creep | Phase gates and ADRs. The world campaign is optional |
| Asset style mismatch | The pixel pipeline plus palette LUTs unify everything. Procedural fallbacks |

## 9. Open questions to settle during development (record each in an ADR)
1. The value of `CALENDAR_RATE`, and whether deadlines are tuned in calendar or sim time (Phase 4.7).
2. Default agent budget versus the owner's hardware (Phase 0.2/0.3).
3. Exact play bounds and district polygons (Phase 4.1).
4. How much the Choir should "cheat" on fog (Phase 5.4).
