using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class GuiWalldrawing : GuiScript<GuiWalldrawing>
{


	IEnumerator OnAnyClick( IGuiControl control )
	{
		G.Walldrawing.Visible = false;
		C.player_invis.Say("I remember drawing this");
        yield return E.Break;
	}
}