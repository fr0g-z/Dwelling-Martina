using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class GuiINSTRUCTIONS : GuiScript<GuiINSTRUCTIONS>
{


	IEnumerator OnAnyClick( IGuiControl control )
	{
        G.Floordrawing.Visible = false;
        yield return E.Break;
	}
}