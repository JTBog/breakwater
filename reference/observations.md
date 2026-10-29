# What the official clips show (frame-by-frame notes)

Source: the five gameplay clips and one screenshot on the Steam page. Frames are in `frames/`.
**Reference only: never ship these images or trace them.**

## Art style
- **Stylized low-poly 3D, rendered at low resolution and upscaled nearest-neighbor.** Edges stair-step, and shadows are crisp and pixelated. Textures are flat and painterly (asphalt with cracks, lane markings, crosswalks, grass tufts, palm trees, stucco buildings with awnings).
- The **pixel size grows when the camera zooms in** (FOB close-ups have chunky pixels). This suggests a fixed low-res render target while the camera moves.
- **High-angle perspective camera, about 50–60° pitch, rotatable.** The minimap shows "VIEW 304° NW".
- **Time of day matters:** night scenes are dark blue with strong headlight and siren light pools, and the wall-siege scene is a dusk-orange grade. The clock widget reads "DAY 650 · 11:42 · Dusk in 6:18".
- **The UI is crisp and high-res, not pixelated:** dark translucent panels, a small clean sans-serif, olive/green accents for ready states and amber for selection. **Section titles use a pixel font** ("PLAN YOUR NEXT OPERATION").
- Every unit has a **floating callsign tag** (PC-1, TK-1, UH-1, AH-1, SQ-1). Selected units get amber or blue **selection rings** on the ground.
- Zombies wear **varied clothing colors**, including many **orange jumpsuits**, which help the crowd read as individuals. **Blood decals** pile up on the asphalt. Explosions are bright orange pixel bursts.
- Visible zombie counts on screen look like **several thousand to over 10,000**, and the ops map says "HORDE 10,000".

## Battlefield (`battle-*.png`, `sheet-battlefield-units.jpg`)
- A police cruiser drifting through zombies at night leaves smoke and tire marks, with lightbar glow.
- An Abrams-style tank on a suburban street, with zombies flung by impacts.
- A Black Hawk-style utility helicopter low over a crosswalk with door-gun tracers. A Little Bird/Viper-style light attack helicopter strafes a horde with explosions.
- **A huge X-shaped horde fills an intersection** while three attack helicopters orbit and fire rockets and Hellfires. Parked cars, a parking lot and a concrete channel (an LA River-style storm channel) are visible.
- **Command card (bottom right), as buttons with hotkeys and per-unit counts like 3/3:**
  - Advance, Atk-Move, Queue, Zone, Clear, Policy, MG
  - Hellfire, Rockets, Orbit, Atk-Orbit, Orbit Dir, Stop
  - Land, Take Off, V Form, Line, Column, Spacing
- Selection panel: "3 units · 0 squads · 3 vehicles · STORES Hellfire 24 | Rockets 114 | Salvo up to 8 · Auto uses guns, Hellfire/rockets require designation".
- Resource strip (bottom left): four resource icons with numbers, plus "COMBAT PERSONNEL 2,400".
- **Support wheel (K):** Orbit support: AC-130J 1/1, MQ-9 2/2. Fleet: 5-in gun 1200 rounds. Deck: FA ×8.
- Unit bar: tabs "All 11", "Ungroup", and unit portrait cards.

## Operations map (`opsmap-zoom-to-battlefield.png`, `sheet-opsmap-to-battle-zoom.jpg`)
- **The same 3D world, drawn as flat grey extruded building blocks** on a grey ground with green parks. Street names are drawn on the ground ("Arbor Vitae St", "S Prairie Ave"). District name chips ("INGLEWOOD").
- A **thick red stepped line**, likely a district or front-line boundary.
- **Unit pins** are amber pills with an icon and name ("AH-64E Apache 2"). Dashed movement lines with **ETA labels**.
- A **"LIVE" bracket marker** shows where a battle is running.
- A prompt reads **"TO BATTLEFIELD · WHEEL ▼ ×3"** with a progress bar, so you scroll three more notches to drop into the battle. Another reads "Operation in progress, the battle keeps running. Zoom fully in and push the wheel once more to return to the battlefield."
- Top-left mission card: "**CUT OFF S01 · 45h 23m left on the ops clock · No rescue underway**", a rescue deadline.
- Area info panel: HORDE 10,000 · DEF 0 · UNITS 0 · "City-wide Z 999,763 · Defense". Chips: distance "15.2 km", zombie counter "26,400".
- **Air support roster panel:** MQ-9 Reaper 2/2, F-35B 4/4, A-10C 4/4, F/A-18F 8/8, with "Ready" and "Alert 5" states.
- Unit roster tabs: All 47 · Air 12 · Ground 32 · Naval 3. Cards show "IDLE · Inglewood" or "IDLE · Del Aire".
- "Field order" panel: Moving / Orders in progress / Direct · Allied · Support counts, "Cancel all", "Route end: stop".
- **Helicopter formation** buttons on the ops map: V Form, Line, Column.

## FOB construction (`fob-*.png`, `sheet-fob-construction.jpg`)
- Tabs: **Select · Walls · Modify · Defense · Fire support · Supply · Lighting · Blueprints**.
- Wall types: **HESCO, Concrete, Scaffold, Sandbags**. Height in **levels**, shape **Line / Box**, with tooltips like "Line · 14 slots × 1 levels".
- Walls are **drawn as blueprint ghosts (blue)** in a polygon, then built up out of **modular blocks**.
- A warning reads "**1 walled areas without an entrance: troops and vehicles get trapped. Make entrance**", so the game runs a flood-fill check.
- Defense: **MG nest, Ground CIWS, Guard tower, Minigun nest**, and a "Protective ring" option (a ring of blocks around a turret).
- Turrets have a **firing sector** drawn as an arc ("Sector · centre −49° · 50° wide"). The panel shows HP 600/600, Ammo 1500/1500 and fire modes **Hold / Sector / Narrow / Widen / Auto**.
- A "Plans" panel lists blocks and facilities with a **Build** button, so plans are queued and then built.
- The minimap has filters for Infantry / Vehicle / Air / Enemy.
- A music-player widget at the top ("Electric Drive", Repeat, a "Combat" playlist).

## Wall siege (`steam-screenshot-wall-siege.jpg`, `wall-pileup-climb.png`)
- A long HESCO wall with soldiers on top firing down. **Zombies stack into a sloped mound** against the wall and spill over the top, and a few get thrown into the air.
- A dusk-orange color grade, muzzle flashes and blood particles.

## World map (`world-map-campaign.png`, `sheet-world-map.jpg`)
- A 3D globe with satellite-style terrain and cyan country borders. Infection is shown as **red hatched circles**, and bases as icons.
- Top bar: date "Y2029 · M3 · D3 · Spring". Resources: **Food 7,520 · Med 3,090 · Comp 5,550 · Ammo 4,700 · Fuel 6,210 · Pop 20,160 · Rsrch 142 · Nav 422**. **INFECTED 1.335B**. Screens: Roster, Armory, Tech, War Room, Settings. Speed 1x/2x/4x.
- Base chips: TF-1 ARK, Camp Pendleton, Osan Air Base, Sydney, Silicon Valley, Yokohama.
- Event cards: "Quarantine Request · Pearl Harbor". A story-operation tracker: "A Signal Through the Ash".
