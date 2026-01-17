using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class GuiPills : GuiScript<GuiPills>
{


	IEnumerator OnClickNewButtonEmpty( IGuiControl control )
	{
		
		yield return E.Break;
	}
}