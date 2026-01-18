using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class GuiDeathdoc : GuiScript<GuiDeathdoc>
{


	IEnumerator OnAnyClick( IGuiControl control )
	{
        G.Deathdoc.Visible = false;
        yield return C.player_invis.Say("what is this document?...I can only read the TIME");
        yield return E.Break;
	}
}