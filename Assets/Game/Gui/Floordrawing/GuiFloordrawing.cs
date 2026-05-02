using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class GuiFloordrawing : GuiScript<GuiFloordrawing>
{


	IEnumerator OnAnyClick( IGuiControl control )
	{
        G.Floordrawing.Visible = false;
        C.player_invis.Say("Dark times..let out my feelings through drawing");
        yield return E.Break;
	}
}