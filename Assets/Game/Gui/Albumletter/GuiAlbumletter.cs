using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class GuiAlbumletter : GuiScript<GuiAlbumletter>
{


	IEnumerator OnAnyClick( IGuiControl control )
	{
        G.Albumletter.Visible = false;
        yield return C.player_invis.Say("I remember my aunt taking this picture...");
        yield return E.Break;
       
	}
}