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
        yield return E.Break;
	}
}