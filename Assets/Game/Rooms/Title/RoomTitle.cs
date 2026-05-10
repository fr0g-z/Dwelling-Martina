using UnityEngine;
using System.Collections;
using PowerScript;
using PowerTools.Quest;
using static GlobalScript;

public class RoomTitle : RoomScript<RoomTitle>
{
    ////////////////////////////////////////////////////////////////////////////////////
    // Room Entry

    /// <summary>Called before fade-in. Hides inventory immediately so it never flashes.</summary>
    public void OnEnterRoom()
    {
        G.InventoryBar.Hide();
    }

    /// <summary>Runs the title screen intro sequence. Skippable via ESC.</summary>
    public IEnumerator OnEnterRoomAfterFade()
    {
        Audio.PlayMusic("titlemusic");

        E.StartCutscene();

        // Fade in title logo
        Prop("TestTitle").Visible = true;
        yield return Prop("TestTitle").Fade(0, 1, 1.0f);

        yield return E.Wait(0.5f);

        // Show Continue button only if a save exists
        if (E.GetSaveSlotData().Count > 0)
        {
            Prop("Continue").Enable();
            Prop("Continue").FadeBG(0, 1, 1.0f); 
        }

        // Show New Game button
        Prop("New").Enable();
        yield return Prop("New").Fade(0, 1, 1.0f);

        // If ESC was pressed, cutscene skips to here — ensure buttons are visible
        E.EndCutscene();

        Prop("New").Enable();
        Prop("New").Visible = true;
        if (E.GetSaveSlotData().Count > 0)
        {
            Prop("Continue").Enable();
            Prop("Continue").Visible = true;
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////
    // Button Interactions

    /// <summary>New Game button — stops music and loads the opening cutscene.</summary>
    public IEnumerator OnInteractPropNew(Prop prop)
    {
        Audio.Stop("titlemusic", 0.5f);
        E.ChangeRoomBG(R.StartCutscene);
        yield return E.ConsumeEvent;
    }

    /// <summary>Continue button — restores the most recent save.</summary>
    public IEnumerator OnInteractPropContinue(Prop prop)
    {
        E.RestoreLastSave();
        yield return E.ConsumeEvent;
    }
}