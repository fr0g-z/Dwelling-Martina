using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class GuiPills : GuiScript<GuiPills>
{
  
    // This runs when the GUI is shown
    IEnumerator OnShow()
    {
        // Make the player “say” the line when the GUI appears
        yield return C.player_invis.Say("This expiry date looks familiar");

        yield return E.Break;
    }

    // Close GUI when any control is clicked
    IEnumerator OnAnyClick(IGuiControl control)
    {
        G.Pills.Visible = false;
        yield return E.Break;
    }
}