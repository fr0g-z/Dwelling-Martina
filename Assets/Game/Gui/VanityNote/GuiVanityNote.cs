using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class GuiVanityNote : GuiScript<GuiVanityNote>
{
	IEnumerator OnAnyClick( IGuiControl control )
	{
        G.VanityNote.Visible = false;
        yield return C.player_invis.Say(" is this a poem..?");
        yield return E.Break;
        
	}
}