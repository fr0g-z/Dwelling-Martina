using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class GuiCounternotes : GuiScript<GuiCounternotes>
{


    IEnumerator OnAnyClick(IGuiControl control)
    {

        G.Counternotes.Visible = false;
        yield return C.player_invis.Say("what are these...?");
        yield return E.Break;
    }
}