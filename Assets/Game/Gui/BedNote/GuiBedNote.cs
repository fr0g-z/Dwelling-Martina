using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class GuiBedNote : GuiScript<GuiBedNote>
{


	IEnumerator OnAnyClick( IGuiControl control )
	{
        G.BedNote.Visible = false;
        yield return C.player_invis.Say("Another letter..");
        yield return E.Break;
       
	}

	void OnShow()
	{
	}
}