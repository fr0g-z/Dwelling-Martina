# DWELLING

A psychological point-and-click horror/narrative adventure game built in Unity using the [PowerQuest](https://github.com/SweetRogsoftware/PowerQuest) framework.

You play as the spirit of a child who died after being hit by a car — though you don't know this yet. The game begins in the child's bedroom with a feeling that something is wrong but nothing you can name. As you move through the house, the truth of what happened, what the mother has done in grief, and what keeps the child's spirit anchored here slowly emerges through environmental storytelling, documents, and puzzle solutions.

The game features approximately 22 explorable rooms, an inventory-based puzzle system, document and artwork close-ups, a drag-and-drop minigame, and two distinct endings determined entirely by the player's final choice at the vanity table.

**Tone:** Quiet. Melancholic. Oppressive. Emotionally disturbing rather than jump-scare driven.  
**Inspirations:** Silent Hill 2, Devotion, What Remains of Edith Finch, Spiritfarer, P.T.

---

## Table of Contents

- [Narrative Overview](#narrative-overview)
- [Project Structure](#project-structure)
- [Scripts Reference](#scripts-reference)
  - [Global & State Scripts](#global--state-scripts)
  - [GUI Scripts](#gui-scripts)
  - [Inventory Items](#inventory-items)
  - [Room Scripts](#room-scripts)
  - [Minigame Scripts](#minigame-scripts--dollhouse-puzzle)
- [Puzzle Flow](#puzzle-flow)
- [Ending System](#ending-system)
- [Script Interconnections](#script-interconnections)
- [Developer Notes](#developer-notes)
- [Built With](#built-with)

---

## Narrative Overview

The mother struggled for years to have a child. When the child was finally born, her attachment became obsessive — she rarely allowed them outside, controlling their environment out of terror of losing them. This caused the father to leave, which deepened her instability further.

One day, for the first time, she let the child go outside.

That day, the child was hit and killed by a car.

Unable to process this, the mother fell into denial. She preserved the child's body in the basement and attempted to reconstruct them, believing she could reverse death. The house became a shrine — rooms untouched, time frozen, memories on loop.

The child's spirit wanders these memories. The player pieces together the truth through puzzle solutions, documents, and the child's own narration. The horror is emotional: the child reading their own death certificate, freeing a bird that's already dead, choosing whether to let go of a mother whose grief has become a cage.

**The two endings hinge on a single choice at the vanity table — feather or doll — but the weight of that choice is built across the entire game.**

---

## Project Structure

```
Assets/Game/
├── GlobalScript.cs               — Game-wide logic, state flags, input handling
├── PowerQuestExtensions.cs       — Framework extension hooks
├── QuestScriptAutos.cs           — Auto-generated shorthand accessors (C, I, G, R)
├── Scripts/
│   ├── ItemsPlaced.cs            — Vanity puzzle state flags
│   ├── ShowerSplash.cs           — Shower gate flag
│   ├── DollHouseDone.cs          — Dollhouse completion flag
│   ├── updatelockbox.cs          — Lockbox unlock flag
│   └── ClockSolved.cs            — Clock puzzle flag
├── Characters/
│   └── player_invis/             — The invisible player character
├── Gui/
│   ├── InventoryBar/             — Slide-in inventory display
│   ├── HoverText/                — Cursor description label
│   ├── SpeechBox/                — Dialogue display
│   ├── Prompt/                   — Confirmation dialog
│   ├── Save/                     — Save/Load screen
│   ├── CombLock/                 — Combination lock puzzle UI
│   ├── INSTRUCTIONS/             — Opening control screen
│   └── [document close-up GUIs] — Albumletter, Bathmirror, BedNote, etc.
├── Inventory/
│   ├── Bucket/
│   ├── Feather/
│   ├── Hook/
│   ├── Keyundercouch/
│   ├── MumsPin/
│   ├── SecretDoll/
│   └── TeddyBear/
└── Rooms/
    ├── Title/
    ├── StartCutscene/
    ├── Bedroom/
    ├── Hallway/
    ├── Hallway_2/
    ├── Bathroom/
    ├── Mom_room/
    ├── Closet/
    ├── Insidelockbox/
    ├── Clock/
    ├── Vanity/
    ├── Kitchen/
    ├── KitchenCabinet/
    ├── Cabnetempty/
    ├── Cage/
    ├── Livingroom/
    ├── Insidedollhouse/
    │   └── Scriptsforminigame/   — DollDrag, DollPuzzleManager, SlotMarker
    ├── UnderCouch/
    ├── UnderTable/
    ├── Basement/
    ├── END/
    └── BADending/
```

---

## Scripts Reference

### Global & State Scripts

---

#### `GlobalScript.cs`

**Type:** PowerQuest `GlobalScriptBase<GlobalScript>` — runs continuously for the entire game session.

The central nervous system of the game. Everything that needs to happen globally — input, fallbacks, lifecycle hooks — lives here.

**Input & Click Handling (`OnMouseClick`, `UpdateInput`):**

The game uses a two-click interface:
- **Left click on a hotspot/prop/character** → Use/Interact (or use active inventory item)
- **Left click on empty space** → Walk (suppressed — this is a static room game, player doesn't move)
- **Right click on anything** → Look
- **Left click on inventory item** → Select as active item
- **Right click / clicking empty space while holding inventory** → Deselect inventory item

Keyboard shortcuts: Escape skips cutscenes; left click and spacebar skip dialogue; F5 quicksaves; F7 restores quicksave; F9 restarts.

Debug shortcuts (held backtick/backslash in debug builds): I gives all inventory items; Page Up/Down adjusts time scale; period key fast-forwards dialogue.

**Fallback Responses (`UnhandledInteract`, `UnhandledLookAt`):**

When the player clicks something with no room-specific handler:
- `UnhandledInteract`: Rotates through "You can't use that" / "That doesn't work" / "Nothing happened" using `E.Occurrence()` to avoid repetition. Suppressed when a GUI is open, when `G.Deathdoc` or `G.Pills` is visible, and when clicking GUI elements.
- `UnhandledLookAt`: Returns one of three look responses randomly. Suppressed on the title screen.
- `UnhandledUseInvInv` and `UnhandledUseInv`: Generic "can't combine" responses.

**Important — Static State Classes:**

Originally the five state tracker classes were nested inside `GlobalScript` as inner static classes. This caused a critical WebGL/IL2CPP bug: the compiler's code stripping treated nested static classes inside `partial` classes as unreferenced and removed them at build time, causing a NullReferenceException loop from frame one on WebGL. They were moved to standalone `.cs` files to fix this. See individual entries below.

---

#### `ItemsPlaced.cs`

**Type:** Standalone top-level static class.

Tracks the state of every item placed on the vanity table. All fields are static so they persist across room transitions without needing a GameObject or save integration.

| Field | Type | Set by | Read by | Meaning |
|---|---|---|---|---|
| `TeddyPlaced` | bool | `RoomVanity` | `RoomVanity` | Teddy bear placed in the Past slot |
| `PinPlaced` | bool | `RoomVanity` | `RoomVanity` | Mum's pin placed in the Present slot |
| `FeatherPlaced` | bool | `RoomVanity` | `RoomVanity`, `RoomLivingroom` | Feather placed in the Future slot |
| `SecretDollPlaced` | bool | `RoomVanity` | `RoomVanity`, `RoomUnderTable` | Secret doll placed in the Future slot |
| `AllItemsPlaced` | bool (computed) | — | `RoomVanity`, `RoomLivingroom` | True when Teddy + Pin + Feather all placed — good ending condition |
| `SecretSolution` | bool (computed) | — | `RoomVanity`, `RoomUnderTable` | True when Teddy + Pin + SecretDoll all placed — bad ending condition |

---

#### `ShowerSplash.cs`

**Type:** Standalone top-level static class.

Single boolean flag: `ShowerSplashed`. Set to `true` when the player clicks the shower in `RoomBathroom`. Read by `RoomHallway_2` to gate the mother's bedroom door.

The gate is narrative: the child says they should "freshen up" before going to see Mum. This directs players to the bathroom first without an arbitrary locked door, and ensures they encounter the pillbox (the first major puzzle clue) before progressing.

---

#### `DollHouseDone.cs`

**Type:** Standalone top-level static class.

Single boolean flag: `DollhouseDone`. Set to `true` by `DollPuzzleManager` when the dollhouse puzzle is solved. Read by `RoomInsidedollhouse` to determine whether the reward dialogue should fire, and by `RoomLivingroom` to prevent re-entering the dollhouse after completion.

---

#### `updatelockbox.cs`

**Type:** Standalone top-level static class.

Single boolean flag: `LockboxUnlocked`. Set to `true` when the player correctly solves the `GuiCombLock` combination puzzle. Read by `RoomCloset` — once true, clicking the lock hotspot skips the combination UI and goes directly into `RoomInsidelockbox`.

---

#### `ClockSolved.cs`

**Type:** Standalone top-level static class.

Single boolean flag: `ClockSolved_`. Set to `true` when the player correctly sets the clock to the time found on the death certificate in `RoomInsidelockbox`. Used by the clock puzzle system to gate the Mumspin reward.

---

### GUI Scripts

#### System / Interactive GUIs

---

##### `GuiInventoryBar.cs`

The slide-in inventory panel at the top of the screen. Powered by PowerQuest's `GuiDropDownBar` component which handles the proximity-based slide animation. Items are displayed as clickable sprites. Selecting an item sets it as the active inventory item for use on hotspots.

---

##### `GuiHoverText.cs`

A small floating label that follows the cursor and displays the `Description` property of whatever the mouse is currently hovering over (hotspots, props, inventory items, characters). Clamps itself to screen edges so it never renders off-screen.

---

##### `GuiSpeechBox.cs`

The dialogue text display. Renders character speech in the game's stylised font. Behaviour and positioning are controlled by PowerQuest's framework. The child character (`C.player_invis`) speaks all in-game lines through this GUI.

---

##### `GuiPrompt.cs`

A configurable confirmation dialog. Supports custom text, button labels, and an optional callback on confirmation. Used for actions that need explicit player confirmation. Supports an async wait-for-response coroutine pattern.

---

##### `GuiSave.cs`

The save/load screen. Displays six screenshot slots with descriptions and timestamps. Slot 1 is designated as the quicksave slot and is protected from deletion. The most recently written save is marked as "Latest". All slot interactions (save, load, delete) are handled internally.

---

##### `GuiCombLock.cs`

**The combination lock puzzle UI.** Displays a numeric keypad. Accepts up to four digits and compares the entered sequence against the hardcoded correct code `2154`. 

The code `2154` is derived from counting the pills in the mother's pillbox by colour — each colour group contains a specific number of pills that, read in order, produce the code. Players must notice the pillbox in the bathroom, count the pills, and apply the result here.

On an incorrect code: plays a failure sound, resets the display.  
On the correct code: sets `updatelockbox.LockboxUnlocked = true`, closes the GUI, and the player can now enter `RoomInsidelockbox`.

---

##### `GuiINSTRUCTIONS.cs`

The control instructions screen shown at game start, accessible again via a note in the bedroom. Dismissed by any click. Explains the two-click interface (left click to use/interact, right click to look).

---

#### Document / Close-up GUIs

All of these follow the same pattern: the GUI becomes visible when the player clicks a specific hotspot, renders a full-screen close-up of an in-world object or document, plays a one-time voiceover line, and closes on click. They exist to show handwritten notes, letters, drawings, and photographs in readable detail without needing in-world resolution.

| GUI Script | Object Shown | Narrative Purpose |
|---|---|---|
| `GuiAlbumletter` | Photograph from the family album | Reveals the aunt's relationship with the family — she believed the child deserved a normal childhood outside the house |
| `GuiBathmirror` | Newspaper-covered bathroom mirror | All mirrors in the house are covered — a grief custom. The child initially just finds it strange. |
| `GuiBedNote` | Letter found on the mother's bed | A personal letter written by or to the mother — deepens understanding of her mental state |
| `GuiBloodynote` | A disturbing, blood-stained document | Found in the living room — one of the more overtly unsettling environmental details |
| `GuiCounternotes` | Aunt Gertrude's unopened letters | The aunt sent letters that were never opened. She tried to maintain contact and was shut out. |
| `GuiDeathdoc` | The death certificate | The central narrative document. The child's name is on it. The time of death is legible — it's the solution to the clock puzzle. The player reads this and the implications slowly register. |
| `GuiFloordrawing` | Child's drawing on the bedroom floor | Shows three figures — the family as it was. The father is present here. |
| `GuiPainting` | A painting in the hallway | Made by the mother. She stopped painting after the father left. |
| `GuiPills` | The mother's pill collection | The pillbox is divided by colour. Each colour section contains a specific number of pills. These numbers, read in order, are the combination lock code. |
| `GuiVanityNote` | A bedtime note read by the mother nightly | Found at the vanity — suggests the mother's nightly ritual and the depth of her grief |
| `GuiWalldrawing` | Child's drawing on the bedroom wall | Another family drawing — shows the mother and child, no father |

---

### Inventory Items

All inventory items are PowerQuest `InventoryScript<T>` objects. Their scripts contain look-at and use hooks which are mostly stubs — actual item behaviour is handled by the room scripts via `OnUseInvHotspot[Name]` and `OnUseInvProp[Name]` methods.

| Item | Script Class | Where Found | Used In | Narrative Role |
|---|---|---|---|---|
| Bucket | `InventoryBucket` | — | — | Unused puzzle remnant |
| Feather | `InventoryFeather` | `RoomCage` (from the dead bird) | `RoomVanity` (Future slot) | Symbolises death and leaving something behind — the good ending choice |
| Hook | `InventoryHook` | — | — | Unused puzzle remnant |
| Key (under couch) | `InventoryKeyundercouch` | `RoomUnderCouch` | `RoomCage` (unlocks cage door) | Unlocks the birdcage — the bird inside is already dead |
| Mum's Pin | `InventoryMumsPin` | Found in `RoomHallway_2` | `RoomVanity` (Present slot) | The mother's brooch — she wore it every day and has stopped. Represents the present state of grief. |
| Secret Doll | `InventorySecretDoll` | `RoomInsidedollhouse` | `RoomVanity` (Future slot) | Carved by the child to look like the mother, so she'd never be alone. The bad ending choice — choosing to stay caught in the mother's grief. |
| Teddy Bear | `InventoryTeddyBear` | `RoomKitchenCabinet` or `RoomKitchen` | `RoomVanity` (Past slot) | "Mr. Bear" — the child's childhood toy, kept by the mother. Represents the past. |

---

### Room Scripts

All room scripts inherit from PowerQuest's `RoomScript<T>`. Method names follow the convention `OnInteractHotspot[Name]`, `OnLookAtHotspot[Name]`, `OnUseInvHotspot[Name]`, `OnInteractProp[Name]`, etc.

The `bool saidLine` pattern used in several rooms prevents intro dialogue from replaying every time the player re-enters. This is an instance variable on the room script and resets if the scene is fully reloaded.

---

#### Navigation / Hub Rooms

---

##### `RoomTitle.cs`

The title screen. Fades in the game logo and menu. Shows a "Continue" option only when existing saves are detected. Plays title music.

---

##### `RoomStartCutscene.cs`

The opening cutscene — approximately 31.5 seconds of animated frames establishing the game's atmosphere. Skippable by clicking. Transitions directly to `RoomBedroom` on completion or skip.

---

##### `RoomHallway.cs`

The first hallway, connecting the bedroom, the vanity room, and the second hallway. Sparse interactions by design — the player is meant to move through here quickly. The photo frames hotspot reveals that in every family photograph, the father's face has been turned away. This is the first environmental hint of the family collapse.

**Connects to:** `RoomBedroom`, `RoomVanity`, `RoomHallway_2`

---

##### `RoomHallway_2.cs`

The central hub of the house. Connects to the bathroom, mother's bedroom (gated), kitchen, and the clock room. 

The mother's bedroom door is locked by `ShowerSplash.ShowerSplashed` — clicking it before showering plays a locked door sound and the child says they should freshen up first. Once the shower has been triggered, clicking the same hotspot plays an opening sound and transitions normally.

If the player owns `I.MumsPin` when entering this room for the first time, a one-time dialogue fires: *"Mum never takes this pin off. Why was it just... left here?"*

The painting hotspot, on look, reveals the mother stopped painting after the father left.

The plant hotspot triggers the Florian easter egg — a gif prop appears for 2 seconds then hides.

**Connects to:** `RoomHallway`, `RoomBathroom`, `RoomMom_room` (gated by `ShowerSplash`), `RoomKitchen`, `RoomClock`

---

#### Main Exploration Rooms

---

##### `RoomBedroom.cs`

The starting room. The child wakes feeling something is wrong but frames it as just a strange morning. Everything here is designed to feel normal while being subtly wrong — a boarded window the child notices but doesn't question, toys that haven't been touched, an outlet the mother always said was dangerous.

On first entry: plays the game soundtrack, positions the player character at `Point("Character")`, delivers the opening monologue.

The window hotspot tracks how many times it has been looked at. On the third look, an easter egg triggers: the child says *"...wait what?"*, a cat gif prop appears for 2 seconds with audio, then hides.

The floor drawing, on interact, opens `G.Floordrawing` — a drawing of three people. The wall drawing opens `G.Walldrawing`. Both reinforce the presence of a father figure who is now absent. The instruction note opens `G.INSTRUCTIONS`.

**Connects to:** `RoomHallway`

---

##### `RoomBathroom.cs`

Contains the first major puzzle clue. 

The **pillbox** hotspot opens `G.Pills` — a close-up of the mother's pill organiser. Each coloured section of the pillbox contains a specific number of pills. Players must count them: the numbers in colour order form `2154`, the combination for the closet lockbox.

The **shower** interaction sets `ShowerSplash.ShowerSplashed = true` and plays the splash audio. The child says the water is freezing and that they should go change in Mum's room — naturally directing the player to `RoomHallway_2` and toward `RoomMom_room`.

The **mirror** opens `G.Bathmirror`. On look: *"She covered it with newspaper. Every single mirror in the house."* — a grief custom (covering mirrors after death) that the child treats as a curiosity rather than a sign.

**Connects to:** `RoomHallway_2`

---

##### `RoomMom_room.cs`

The mother's bedroom. Every detail reinforces her collapse: curtains still closed, lights that don't work, a locked window (locked from the inside — she doesn't want to see out), a locked chest. The child's narration notices all of this but interprets it charitably.

On first entry: *"Mum's room. The curtains are still closed. Has she been in bed all day?"*

The note hotspot opens `G.BedNote`. The closet hotspot leads to `RoomCloset`.

**Connects to:** `RoomHallway_2`, `RoomCloset`

---

##### `RoomCloset.cs`

A small closet containing the lockbox. On first entry the child says they need a code. 

The lock hotspot checks `updatelockbox.LockboxUnlocked`:
- If false: plays a lock sound and opens `G.CombLock` for the combination puzzle
- If true: plays a dialogue line ("It's already open") and transitions to `RoomInsidelockbox`

**Connects to:** `RoomMom_room`, `RoomInsidelockbox`

---

##### `RoomInsidelockbox.cs`

The inside of the unlocked lockbox. Contains two interactable objects:

The **death certificate** (`G.Deathdoc`): The central narrative revelation. The child's own name is on the document. The time of death is visible — this is the solution to the clock puzzle. The implication takes time to register.

The **photo album** (`G.Albumletter`): Contains a letter or photograph that reveals information about the aunt's relationship with the family — her belief that the child deserved a normal life outside the house, and the conflict this caused.

**Connects to:** `RoomCloset`

---

##### `RoomClock.cs`

The grandfather clock room. The clock puzzle UI is managed by a separate clock component. When the player sets the clock hands to the time found on the death certificate, `ClockSolved.ClockSolved_` is set to true and the Mumspin item is granted.

The script itself contains only the leave-room hotspot interaction.

**Connects to:** `RoomHallway_2`

---

##### `RoomKitchen.cs`

A large, detailed room with multiple environmental storytelling beats and sub-room access points.

Environmental details reinforce the mother's grief: only one plate has been used recently, two chairs instead of three, half-empty cabinets, a cold cooker, dishes sitting for days, a fruit bowl full of food she bought and never ate.

The window look dialogue is significant: *"I used to press my face against this glass and watch the street. Mum would always pull me away."* — one of the clearest references to the mother's controlling behaviour before the accident.

Aunt Gertrude's letters on the counter are unopened — she tried to maintain contact and was shut out entirely.

Sub-room access: cage (leads to `RoomCage`), under-table (leads to `RoomUnderTable`), main cabinet (leads to `RoomKitchenCabinet`), empty cabinets (leads to `RoomCabnetempty`).

**Connects to:** `RoomHallway_2`, `RoomLivingroom`, `RoomCage`, `RoomUnderTable`, `RoomKitchenCabinet`, `RoomCabnetempty`

---

##### `RoomKitchenCabinet.cs`

A small sub-room inside one of the kitchen cabinets. Contains the teddy bear prop. On interact: grants `I.TeddyBear` with extended dialogue establishing the bear's history — the child carried it everywhere, the mother kept it in the kitchen where she could see it every day.

**Connects to:** `RoomKitchen`

---

##### `RoomCabnetempty.cs`

An empty cabinet. One-time entry line: *"Empty. She cleared it out. I don't know when."* No items, no puzzle interactions. Exists as an environmental beat — the house is being gradually emptied or preserved selectively.

**Connects to:** `RoomKitchen`

---

##### `RoomLivingroom.cs`

The living room. Contains the dollhouse, the front exit door, the couch (with the under-couch sub-room), and several environmental hotspots.

The **front door** is the good ending exit. It checks `ItemsPlaced.AllItemsPlaced` — if false, it plays a lock sound and the child says it won't open. If true, it plays the escape audio and transitions to `RoomEND`.

The **dollhouse** transitions to `RoomInsidedollhouse` unless `DollHouseDone.DollhouseDone` is true, in which case the child says everything is in its place now. Look dialogue: *"Mum bought this for me. She said I could play house inside it. Safe, she said. In here you're always safe."*

The **couch** look dialogue references the father directly: *"Me and Dad used to watch TV here for hours. He's not coming back. I know that now."*

The **windows** look dialogue: *"I can see the street from here. Mum never let me go out there."* — the only view of the outside world the child ever had.

The plant interaction triggers the Florian easter egg (same as `RoomHallway_2`).

**Connects to:** `RoomKitchen`, `RoomInsidedollhouse`, `RoomUnderCouch`, `RoomEND` (gated by `ItemsPlaced.AllItemsPlaced`)

---

##### `RoomMom_room.cs` *(see above under Main Exploration Rooms)*

---

##### `RoomVanity.cs`

**The most complex and narratively significant room in the game.**

The vanity table has three item slots representing a timeline:
- **Past slot** — accepts `I.TeddyBear`. Placement dialogue establishes the bear's history and the mother's attachment to the past.
- **Present slot** — accepts `I.MumsPin`. Placement dialogue establishes the mother's current state of grief and collapse.
- **Future slot** — accepts either `I.Feather` (good ending) or `I.SecretDoll` (bad ending). **Disabled until both Past and Present are filled.**

The Future slot uses a **two-placement system** to give the choice genuine weight:

**First placement:** The item appears briefly on the vanity. The child speaks at length about what choosing this item actually means — for the Feather, understanding that choosing to go means letting the mother grieve properly; for the Doll, understanding that choosing to stay means remaining caught, like the bird in the cage. Then the prop hides and the item returns to inventory. The player now holds both items and understands both choices fully.

**Second placement:** No extended dialogue. The child says only *"I have made my choice."* then *"Future."* The ending triggers. The brevity is intentional — the reflection is done. This is the act.

**Slot unlock logic:** When Teddy is placed, the script checks if Pin is already placed — if so, it immediately enables the Feather hotspot. The same logic runs in reverse when Pin is placed. This means the Future slot unlocks the moment the second of the two prerequisite items is placed, without requiring a room re-entry.

**Picking up placed items:** Before the puzzle is complete, clicking a placed item removes it from the slot, re-enables the slot's hotspot, and returns the item to inventory with a short hesitation line. After completion, clicking any item plays *"I shouldn't disturb this."*

`CheckPuzzleComplete()` fires after every successful placement and checks both ending conditions, triggering the appropriate completion dialogue.

Entry dialogue varies based on current puzzle state — three different lines for fresh entry, partial completion, and final-slot ready.

**Connects to:** `RoomHallway`, endings

---

#### Secondary / Hidden Rooms

---

##### `RoomCage.cs`

The birdcage room, accessed from the kitchen. The cage door prop requires `I.Keyundercouch` to open.

When the correct key is used, the cage opens — and the bird inside is already dead. The child reflects that it was locked in and forgotten, or maybe the mother couldn't bring herself to open the door. This directly mirrors the game's central metaphor: the mother locked what she loved away until it died. The child takes a feather left behind.

This interaction recontextualises the feather as a relic from something that didn't survive being kept rather than a symbol of freedom from something that flew away.

**Connects to:** `RoomKitchen`

---

##### `RoomUnderCouch.cs`

Under the living room couch. A small sub-room. The key prop interaction grants `I.Keyundercouch`. Dialogue: *"A small key. Hidden under here on purpose. What was she keeping locked up?"*

**Connects to:** `RoomLivingroom`

---

##### `RoomUnderTable.cs`

Under the kitchen table. Provides access back to the kitchen above and, if `ItemsPlaced.SecretSolution` is true, to the basement below.

The basement door prop checks `ItemsPlaced.SecretSolution`:
- If false: plays a dialogue line about the latch being locked from the other side — *"She doesn't want anyone going down there."*
- If true: swaps the door prop from closed to open and transitions to `RoomBasement`.

The `OnEnterRoom()` void method (non-coroutine) also checks `ItemsPlaced.SecretSolution` and performs the prop swap immediately on room load so the door is already open if the player re-enters.

**Connects to:** `RoomKitchen`, `RoomBasement` (gated by `ItemsPlaced.SecretSolution`)

---

##### `RoomInsidelockbox.cs` *(see above under Main Exploration Rooms)*

---

##### `RoomInsidedollhouse.cs`

The dollhouse puzzle room. On enter, activates the `DollhousePuzzle` GameObject (which contains the `DollPuzzleManager` and all doll/slot GameObjects). On exit, deactivates it.

Both `GameObject.Find("DollhousePuzzle")` calls are null-checked before calling `SetActive` — this prevents a WebGL crash that occurred when the object wasn't found during scene transitions.

The `puzzleComplete()` public coroutine is available for external calls. It checks `DollHouseDone.DollhouseDone` before running — if true, hides the `DollFlap` prop and runs the full doll origin narrative sequence before granting `I.SecretDoll`.

Note: `DollPuzzleManager.PuzzleCompleteDialogue()` also handles this sequence and grants the item. Both exist for redundancy across room entry timing edge cases.

**Connects to:** `RoomLivingroom`

---

##### `RoomBasement.cs`

The basement — only reachable via the bad ending path after `ItemsPlaced.SecretSolution` is true.

On entry: stops the game soundtrack (`Audio.Stop("Gamesoundtrack")`), plays the mother breathing audio (`Audio.Play("mombreathing")`). This audio change is the first signal that something is fundamentally different about this space.

The mother hotspot plays two lines of dialogue then transitions to `RoomBADending`. This is where the revelation of what the mother has done with the child's body occurs.

**Connects to:** `RoomUnderTable`, `RoomBADending`

---

#### Ending Rooms

---

##### `RoomEND.cs`

The good ending room. Stops music and hides the inventory bar on entry. Plays the good ending animation/cutscene sequence.

---

##### `RoomBADending.cs`

The bad ending room. Hides the inventory bar on entry. Plays the bad ending animation/cutscene sequence.

---

### Minigame Scripts — Dollhouse Puzzle

The dollhouse puzzle is a drag-and-drop minigame embedded within `RoomInsidedollhouse`. It uses three scripts independent of the PowerQuest framework, operating as standard Unity MonoBehaviours.

---

#### `DollDrag.cs`

Attached to each draggable doll GameObject. Handles all drag input for a single doll.

**Drag behaviour:**
- On `OnMouseDown`: detaches the doll from its current slot (calls `UpdateDollSlot(dollID, null)` on the manager), begins dragging.
- During `Update`: follows the mouse position while dragging.
- On `Input.GetMouseButtonUp(0)`: calls `SnapToSlot()`.

**Snap behaviour (`SnapToSlot`):**
1. Finds all GameObjects tagged `"Slot"` in the scene
2. Finds the closest slot by distance
3. If within `snapDistance`: checks whether another doll already occupies that slot (by comparing `lastSlot` references across all `DollDrag` instances) — if occupied, returns to `lastPosition`; if free, snaps and registers with the manager
4. If outside `snapDistance`: returns to `lastPosition`

**WebGL null safety:** Both `cam` (Camera.main) and `DollPuzzleManager.Instance` are null-checked before every access. `cam` re-fetches itself if null on each frame to handle WebGL's delayed camera initialisation.

| Field | Default | Description |
|---|---|---|
| `dollID` | Set in Inspector | Unique integer identifying this doll to the manager |
| `snapDistance` | 3f | Max world-space distance to trigger a slot snap |

---

#### `DollPuzzleManager.cs`

Singleton MonoBehaviour managing the entire puzzle. Accessed via `DollPuzzleManager.Instance`.

**State tracking:**
- `currentDollSlots`: Dictionary mapping `dollID → slot GameObject` for the current session
- `persistedDollSlots`: Same structure, but survives room re-entry (because the manager is on a GameObject that gets deactivated/reactivated, not destroyed)
- `puzzleComplete` / `puzzleCompletedPersist`: Local and persistent completion flags

**Awake:** Initialises `targetSlots` from the Inspector-assigned `slot1` and `slot5` references. Null-checks both before building the dictionary to prevent crashes if references are missing. Restores doll positions from `persistedDollSlots` if returning to the room.

**`UpdateDollSlot(dollID, slot)`:** Called by `DollDrag` whenever a doll is picked up (slot = null) or placed (slot = GameObject). Updates both dictionaries and calls `CheckPuzzleComplete`.

**`CheckPuzzleComplete`:** Iterates `targetSlots` and counts how many target slots have the correct doll. If count >= `totalCorrectNeeded`, triggers `PuzzleComplete()`.

**`PuzzleComplete`:** Hides the flap sprite, sets `DollHouseDone.DollhouseDone = true`, starts `HideDollAfterDelay(4f)` and `PuzzleCompleteDialogue()` coroutines.

**`PuzzleCompleteDialogue`:** The full narrative sequence about the doll's origin — the child reveals they carved it to look like the mother, so she'd never be alone, and didn't know what "not being there" would really mean. Grants `I.SecretDoll` at the end of the sequence.

| Inspector Field | Description |
|---|---|
| `totalCorrectNeeded` | Number of correct placements to complete puzzle (default: 2) |
| `slot1` | Target slot for doll ID 1 |
| `slot5` | Target slot for doll ID 3 |
| `flapSprite` | Covers the reward area — hidden on completion |
| `dollsprite` | Doll prop shown during puzzle — hidden 4 seconds after completion |

---

#### `SlotMarker.cs`

Lightweight marker MonoBehaviour placed on each slot GameObject. Stores `correctDollID` to identify which doll should go here. A value of `-1` means any doll is accepted in this slot. Read by `DollPuzzleManager` when building `targetSlots`.

---

## Puzzle Flow

```
[Title / StartCutscene]
    ↓
[Bedroom]
  Child wakes, senses something wrong
  → Hallway
    ↓
[Hallway 2]
  Mother's bedroom locked — child says "freshen up first"
    ↓
[Bathroom]
  Pillbox close-up — count pills by colour → code 2154
  Shower → ShowerSplash = true → bedroom door unlocks
    ↓
[Mom's Room]
  Environmental grief beats
  → Closet
    ↓
[Closet]
  Combination lock → enter 2154 → LockboxUnlocked = true
  → Inside Lockbox
    ↓
[Inside Lockbox]
  Death certificate → child's name → time of death
  Album photo → family history revealed
    ↓
[Clock Room]  (via Hallway 2)
  Set clock to time from certificate → Mumspin granted → ClockSolved = true
    ↓
[Kitchen]  (via Hallway 2)
  Environmental beats, aunt's letters
  → Kitchen Cabinet → Teddy Bear granted
  → Cage room → use key → dead bird → Feather granted
    ↓
[Living Room]  (via Kitchen)
  → Under Couch → Key granted
  → Dollhouse → place dolls correctly → Secret Doll granted + narrative
    ↓
[Vanity]  (via Hallway → Hallway area)
  Place Teddy Bear (Past) → Place Mum's Pin (Present) → Future slot unlocks
  First placement of Feather or Doll: preview dialogue, item returned
  Second placement: "I have made my choice."
    ↓
  FEATHER → AllItemsPlaced = true
    → Living Room front door opens → RoomEND (Good Ending)
    
  SECRET DOLL → SecretSolution = true
    → Basement door unlocks → RoomUnderTable → RoomBasement → RoomBADending
```

---

## Ending System

The game has two endings. Both are determined by a single choice at the vanity table, but the weight of that choice is built across the entire game's narrative.

---

### Good Ending — The Feather

**Trigger:** `ItemsPlaced.AllItemsPlaced` is true (Teddy + Pin + Feather all placed).

The child accepts their death. They understand that loving someone means letting them go rather than holding them trapped. The front door in the living room unlocks and leads to `RoomEND`.

The feather comes from the dead bird in the cage — not from something that flew free, but from something that didn't survive being locked away. The child chooses not to be that bird. They leave something behind and go.

Completion dialogue: *"Something opened. It feels like the first breath after a very long time underwater."*

---

### Bad Ending — The Secret Doll

**Trigger:** `ItemsPlaced.SecretSolution` is true (Teddy + Pin + SecretDoll all placed).

The child refuses to leave. They choose to remain caught in the mother's grief, tethered to the house alongside her obsession. `ItemsPlaced.SecretSolution` unlocks the basement door. The player descends to find the mother and the body she has been preserving.

The doll was made by the child to look like the mother — an act of love before the accident, now a symbol of the child choosing to stay rather than move on.

Completion dialogue: *"Something shifted. Deep in the house. Like a door closing. That's okay. I didn't want to leave anyway."*

---

## Script Interconnections

```
RoomBathroom
  └── Sets: ShowerSplash.ShowerSplashed = true

RoomHallway_2
  └── Reads: ShowerSplash.ShowerSplashed
             → gates Mom_room door

GuiCombLock
  └── Sets: updatelockbox.LockboxUnlocked = true

RoomCloset
  └── Reads: updatelockbox.LockboxUnlocked
             → skips UI on re-entry

RoomInsidelockbox
  └── Opens: G.Deathdoc (reveals time for clock puzzle)

RoomClock
  └── Sets: ClockSolved.ClockSolved_ = true
             → grants I.MumsPin

RoomKitchenCabinet
  └── Grants: I.TeddyBear

RoomUnderCouch
  └── Grants: I.Keyundercouch

RoomCage
  └── Reads: I.Keyundercouch
  └── Grants: I.Feather

DollPuzzleManager
  └── Sets: DollHouseDone.DollhouseDone = true
  └── Grants: I.SecretDoll

RoomVanity
  └── Reads: ItemsPlaced (all flags)
  └── Sets: ItemsPlaced.TeddyPlaced
            ItemsPlaced.PinPlaced
            ItemsPlaced.FeatherPlaced
            ItemsPlaced.SecretDollPlaced
  └── On AllItemsPlaced → Good Ending path
  └── On SecretSolution → Bad Ending path

RoomLivingroom
  └── Reads: ItemsPlaced.AllItemsPlaced
             → unlocks front door → RoomEND

RoomUnderTable
  └── Reads: ItemsPlaced.SecretSolution
             → unlocks basement door → RoomBasement
```

---

## Developer Notes

**PowerQuest coroutine conventions:**
- Every `IEnumerator` interaction method must end with `yield return E.Break` — PowerQuest uses this to know the sequence has finished
- `yield return E.Wait(seconds)` is the correct pause method — `E.WaitForInput` does not exist in this version of PowerQuest
- `yield break` inside coroutines stops execution but should be paired with `yield return E.Break` before it for clean PowerQuest handling

**Static state and save system:**
- All five state classes (`ItemsPlaced`, `ShowerSplash`, etc.) use static fields which reset on application restart
- They are not integrated into PowerQuest's save system — if save/load functionality is added, these will need to be serialised and restored in `GlobalScript.OnPostRestore()`
- The `bool saidLine` and `int m_windowLookCount` patterns in room scripts are instance variables — they reset on scene reload but persist across room re-entries during a session

**The `bool saidLine` pattern:**
Used in `RoomBedroom`, `RoomMom_room`, `RoomCloset`, and `RoomCabnetempty` to prevent entry dialogue from replaying. This is an instance variable on the RoomScript, not a static flag — it resets if Unity reloads the scene.

**Vanity preview flags:**
`m_featherPreviewed` and `m_dollPreviewed` in `RoomVanity` are instance variables. If the player leaves and returns to the vanity before completing the puzzle, these reset and the preview can be seen again. This is intentional — the player may need to revisit the preview.

**WebGL/IL2CPP notes:**
The five state classes were originally nested inside `GlobalScript` as inner static classes. IL2CPP's code stripping on WebGL treated them as unreferenced and removed them, causing a NullReferenceException loop from frame one. Moving them to standalone top-level classes resolves this. If any new state classes are added, make them top-level rather than nested.

The `GameObject.Find()` calls in `RoomInsidedollhouse` and `DollDrag`'s `Camera.main` access are null-checked for the same reason — WebGL initialises some references asynchronously and accesses that work immediately in the editor can return null for one or more frames at WebGL startup.

**Audio:**
All 51 audio clips are set to Streaming + Vorbis + Quality 60 to minimise WebGL memory usage. Long music tracks (Gameplay OST, Menu Music Extended, Basement SFX) benefit most from Streaming as they are read from disk rather than loaded entirely into memory.

---

## Built With

- [Unity 2020.3](https://unity.com/) — Game engine
- [PowerQuest](https://github.com/SweetRogsoftware/PowerQuest) — Quest-style adventure game framework for Unity, providing room/character/inventory/GUI management, dialogue sequencing, and the coroutine-based scripting interface
