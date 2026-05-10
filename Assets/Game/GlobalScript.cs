using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PowerScript;
using PowerTools.Quest;

/// <summary>
/// Global Script — contains game-wide logic, variables, and unhandled interaction fallbacks.
/// Accessible from any room or script via Globals.
/// Equivalent to AGS's Global Script.
/// </summary>
public partial class GlobalScript : GlobalScriptBase<GlobalScript>
{
    ////////////////////////////////////////////////////////////////////////////////////
    // Enums

    /// <summary>Tracks overall game progression state.</summary>
    public enum eProgress
    {
        None,
        GotWater,
        DrankWater,
        WonGame
    }

    ////////////////////////////////////////////////////////////////////////////////////
    // Global Variables
    // All public variables here are automatically saved by PowerQuest.

    public eProgress m_progressExample = eProgress.None;
    public bool m_spokeToBarney = false;

    ////////////////////////////////////////////////////////////////////////////////////
    // Game Lifecycle

    /// <summary>Called once when the game first starts.</summary>
    public void OnGameStart()
    {
    }

    /// <summary>Called after a save game is restored. Re-initialize any non-saved references here.</summary>
    public void OnPostRestore(int version)
    {
    }

    /// <summary>Called before fade-in when entering any room. Non-blocking only.</summary>
    public void OnEnterRoom()
    {
    }

    /// <summary>Called after fade-in completes when entering any room.</summary>
    public IEnumerator OnEnterRoomAfterFade()
    {
        yield return E.Break;
    }

    /// <summary>Called as any room fades out on exit.</summary>
    public IEnumerator OnExitRoom(IRoom oldRoom, IRoom newRoom)
    {
        yield return E.Break;
    }

    ////////////////////////////////////////////////////////////////////////////////////
    // Update Loops

    /// <summary>Blocking update — runs every frame when nothing else is blocking the script.</summary>
    public IEnumerator UpdateBlocking()
    {
        yield return E.Break;
    }

    /// <summary>Non-blocking update — runs every frame.</summary>
    public void Update()
    {
    }

    /// <summary>Non-blocking update — runs every frame, even when the game is paused.</summary>
    public void UpdateNoPause()
    {
        UpdateInput();
    }

    ////////////////////////////////////////////////////////////////////////////////////
    // Input

    /// <summary>Handles all keyboard shortcuts and debug controls.</summary>
    void UpdateInput()
    {
        bool debugKeyHeld = E.IsDebugBuild &&
            (Input.GetKey(KeyCode.BackQuote) || Input.GetKey(KeyCode.Backslash));

        if (!E.Paused)
        {
            // Skip cutscene on Escape release (release allows it to also skip dialog while held)
            if (Input.GetKeyUp(KeyCode.Escape))
                E.SkipCutscene();

            // Skip dialog — left click has a built-in delay to avoid accidental skips
            if (Input.GetMouseButtonDown(0))
                E.SkipDialog(true);
            if (Input.GetKey(KeyCode.Escape) || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space))
                E.SkipDialog(false);
        }

        if (!E.GetBlocked() && !E.Paused)
        {
            // F1 — show menu (hook up your menu GUI here)
            // if (Input.GetKeyDown(KeyCode.F1))
            //     G.YourMenuGui.Visible = true;

            if (Input.GetKeyDown(KeyCode.F5))
                E.Save(1, "Quicksave");

            if (Input.GetKeyDown(KeyCode.F7))
                E.RestoreSave(1);

            if (Input.GetKeyDown(KeyCode.F9))
            {
                if (debugKeyHeld)
                    E.Restart(E.GetCurrentRoom(), E.GetCurrentRoom().Instance.m_debugStartFunction);
                else
                    E.Restart();
            }
        }

        // GUI keyboard navigation
        if (Input.GetKey(KeyCode.UpArrow)) E.NavigateGui(eGuiNav.Up);
        if (Input.GetKey(KeyCode.DownArrow)) E.NavigateGui(eGuiNav.Down);
        if (Input.GetKey(KeyCode.RightArrow)) E.NavigateGui(eGuiNav.Right);
        if (Input.GetKey(KeyCode.LeftArrow)) E.NavigateGui(eGuiNav.Left);
        if (Input.GetKeyDown(KeyCode.Return)) E.NavigateGui(eGuiNav.Ok);
        if (Input.GetKeyDown(KeyCode.Escape)) E.NavigateGui(eGuiNav.Cancel);

        // Debug-only shortcuts
        if (debugKeyHeld)
        {
            // Give all inventory items
            if (Input.GetKeyDown(KeyCode.I))
                PowerQuest.Get.GetInventoryItems_SaveFlagNotDirtied().ForEach(item => item.Owned = true);

            // Time scale controls
            if (Input.GetKeyDown(KeyCode.PageDown))
                Systems.Time.SetDebugTimeMultiplier(Systems.Time.GetDebugTimeMultiplier() * 0.8f);
            if (Input.GetKeyDown(KeyCode.PageUp))
                Systems.Time.SetDebugTimeMultiplier(Systems.Time.GetDebugTimeMultiplier() + 0.2f);
            if (Input.GetKeyDown(KeyCode.End))
                Systems.Time.SetDebugTimeMultiplier(1.0f);
        }

        // Hold '.' to speed through game during debug
        if (E.IsDebugBuild)
        {
            if (Input.GetKeyDown(KeyCode.Period))
                Systems.Time.SetDebugTimeMultiplier(4);
            else if (Input.GetKeyUp(KeyCode.Period))
                Systems.Time.SetDebugTimeMultiplier(1);

            if (Input.GetKey(KeyCode.Period))
                E.SkipDialog(false);
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////
    // Click Handling

    /// <summary>
    /// Called before any other click interaction fires.
    /// Block here to intercept all clicks globally.
    /// </summary>
    public IEnumerator OnAnyClick()
    {
        yield return E.Break;
    }

    /// <summary>Called whenever the player tries to walk, even if Moveable is false.</summary>
    public IEnumerator OnWalkTo()
    {
        yield return E.Break;
    }

    /// <summary>
    /// Main mouse click handler. Determines which verb to fire based on context.
    /// Two-click interface: left click = Use/Walk, right click = Look.
    /// </summary>
    public void OnMouseClick(bool leftClick, bool rightClick)
    {
        bool mouseOverSomething = E.GetMouseOverClickable() != null;

        if (C.Plr.HasActiveInventory &&
            (rightClick || (!mouseOverSomething && leftClick) || Cursor.NoneCursorActive))
        {
            // Deselect inventory on: right click, left click on empty space, or None cursor
            I.Active = null;
        }
        else if (Cursor.NoneCursorActive)
        {
            // None cursor active — suppress all interaction
        }
        else if (E.GetMouseOverType() == eQuestClickableType.Gui)
        {
            // GUI element clicked — let the GUI handle it
        }
        else if (leftClick)
        {
            if (mouseOverSomething)
            {
                if (C.Plr.HasActiveInventory && !Cursor.InventoryCursorOverridden)
                {
                    // Use active inventory item on the clicked target
                    E.ProcessClick(eQuestVerb.Inventory);
                }
                else if (E.GetMouseOverType() == eQuestClickableType.Inventory)
                {
                    // Select clicked inventory item as active
                    // Remove this block if you want left click to "use" inventory items instead
                    I.Active = (IInventory)E.GetMouseOverClickable();
                }
                else
                {
                    // Standard left click — interact/use
                    E.ProcessClick(eQuestVerb.Use);
                }
            }
            else
            {
                // Left click on empty space — walk
                E.ProcessClick(eQuestVerb.Walk);
            }
        }
        else if (rightClick)
        {
            if (mouseOverSomething)
                E.ProcessClick(eQuestVerb.Look);
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////
    // Unhandled Interaction Fallbacks

    /// <summary>
    /// Fires when the player interacts with something that has no specific OnInteract handler.
    /// Suppressed while certain GUIs are visible to avoid bleed-through responses.
    /// </summary>
    public IEnumerator UnhandledInteract(IQuestClickable mouseOver)
    {
        // Suppress fallback dialogue when blocking GUIs are open
        if (G.Deathdoc.Visible || G.Pills.Visible)
            yield break;

        // Rotate through three generic responses
        int option = E.Occurrence("unhandledInteract") % 3;
        if (option == 0)
            yield return C.Display("You can't use that");
        else if (option == 1)
            yield return C.Display("That doesn't work");
        else
            yield return C.Display("Nothing happened");
    }

    /// <summary>
    /// Fires when the player looks at something with no specific OnLookAt handler.
    /// Suppressed on the title screen.
    /// </summary>
    public IEnumerator UnhandledLookAt(IQuestClickable mouseOver)
    {
        if (R.Current.ScriptName == "Title")
            yield break;

        int option = Random.Range(0, 3);
        if (option == 0)
            yield return C.Display("It's nothing interesting");
        else if (option == 1)
            yield return C.Display("You don't see anything");
        else
            yield return C.Display($"The {mouseOver.Description.ToLower()} isn't very interesting");
    }

    /// <summary>Fires when the player uses one inventory item on another with no specific handler.</summary>
    public IEnumerator UnhandledUseInvInv(Inventory invA, Inventory invB)
    {
        yield return C.Display("You can't use those together");
    }

    /// <summary>Fires when the player uses an inventory item on something with no specific handler.</summary>
    public IEnumerator UnhandledUseInv(IQuestClickable mouseOver, Inventory item)
    {
        yield return C.Display("That doesn't go there");
    }

    ////////////////////////////////////////////////////////////////////////////////////
    // Persistent State Trackers
    // Using static classes to share state across rooms without saving to PowerQuest's save system.
    // NOTE: Static variables reset on game restart — initialise them in OnGameStart if needed.

    /// <summary>Tracks whether the lockbox puzzle has been solved.</summary>
    public static class updatelockbox
    {
        public static bool LockboxUnlocked = false;
    }

    /// <summary>Tracks whether the clock puzzle has been solved.</summary>
    public static class ClockSolved
    {
        public static bool ClockSolved_ = false;
    }

    /// <summary>
    /// Tracks items placed in the vanity puzzle.
    /// AllItemsPlaced — checks the three required items.
    /// SecretSolution — checks the alternate secret combination.
    /// </summary>
    public static class ItemsPlaced
    {
        public static bool PinPlaced = false;
        public static bool TeddyPlaced = false;
        public static bool FeatherPlaced = false;
        public static bool SecretDollPlaced = false;

        public static bool AllItemsPlaced =>
            PinPlaced && TeddyPlaced && FeatherPlaced;

        public static bool SecretSolution =>
            SecretDollPlaced && TeddyPlaced && PinPlaced;
    }

    /// <summary>Tracks whether the shower splash event has been triggered.</summary>
    public static class ShowerSplash
    {
        public static bool ShowerSplashed = false;
    }

    /// <summary>Tracks whether the doll house puzzle has been completed.</summary>
    public static class DollHouseDone
    {
        public static bool DollhouseDone = false;
    }
}
