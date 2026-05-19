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
        yield return C.player_invis.Say("My Mum read this to me every night before I slept.");
        yield return E.Break;
        
	}
}