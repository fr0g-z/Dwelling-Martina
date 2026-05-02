using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;
using ClipperLib;

public class GuiPainting : GuiScript<GuiPainting>
{


	IEnumerator OnAnyClick( IGuiControl control )
	{
		G.Painting.Visible = false;
		C.player_invis.Say("I bought this with my dad as a surprise for mom...i miss him");
		yield return E.Break;
	}
}