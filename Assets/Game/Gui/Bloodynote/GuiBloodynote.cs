using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class GuiBloodynote : GuiScript<GuiBloodynote>
{




	IEnumerator OnAnyClick( IGuiControl control )
	{
        G.Bloodynote.Visible = false;
        yield return C.player_invis.Say("so much blood...what is this?!");
        yield return E.Break;
        
	}
}