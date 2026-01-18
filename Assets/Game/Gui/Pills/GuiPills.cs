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
        yield return C.player_invis.Say("That expiry date looks familiar...");
        yield return E.Break;
    }
}