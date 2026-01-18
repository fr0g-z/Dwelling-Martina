using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class GuiPills : GuiScript<GuiPills>
{   
	IEnumerator OnClickInspectdate( IGuiControl control )
	{
       
        yield return E.Break;
	} 
    
    IEnumerator OnAnyClick(IGuiControl control)
    {
        G.Pills.Visible = false;
        yield return C.player_invis.Say("What an odd way to sort pills...");
        yield return E.Break;
    }
}