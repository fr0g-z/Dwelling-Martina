using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class GuiBathmirror : GuiScript<GuiBathmirror>
{


	IEnumerator OnAnyClick( IGuiControl control )
	{
        G.Bathmirror.Visible = false;
        yield return C.player_invis.Say("why are there newspapers here...");
        yield return E.Break;
       
	}

 
}