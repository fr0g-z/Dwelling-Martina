using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class GuiCombLock : GuiScript<GuiCombLock>
{
	
	IEnumerator OnClickClose( IGuiControl control )
	{
		Gui.Hide();
		yield return E.Break;
	}

	void OnShow()
	{
		Label("Input").Text = "";
	}

	IEnumerator OnClickB1( IGuiControl control )
	{
		Label("Input").Text += "1";
		yield return E.Break;
	}

	IEnumerator OnClickB2( IGuiControl control )
	{
		Label("Input").Text += "2";
		yield return E.Break;
	}

	IEnumerator OnClickB3( IGuiControl control )
	{
		Label("Input").Text += "3";
		yield return E.Break;
	}

	IEnumerator OnClickB4( IGuiControl control )
	{
		Label("Input").Text += "4";
		yield return E.Break;
	}

	IEnumerator OnClickB5( IGuiControl control )
	{
		Label("Input").Text += "5";
		yield return E.Break;
	}

	IEnumerator OnClickB6( IGuiControl control )
	{
		Label("Input").Text += "6";
		yield return E.Break;
	}

	IEnumerator OnClickB7( IGuiControl control )
	{
		Label("Input").Text += "7";
		yield return E.Break;
	}

	IEnumerator OnClickB8( IGuiControl control )
	{
		Label("Input").Text += "8";
		yield return E.Break;
	}

	IEnumerator OnClickB9( IGuiControl control )
	{
		Label("Input").Text += "9";
		yield return E.Break;
	}

	IEnumerator OnClickenter( IGuiControl control )
	{
		if (Label("Input").Text == "1234")
		{
			Label("Input").Text = ("*UNLOCKED*");
			yield return E.WaitSkip(1.0f);
			Gui.Hide();
			yield return C.player_invis.Say("Its unlocked");
		}
		else
		{
			Label("Input").Text = "";
			yield return E.WaitSkip(1.0f);
			Audio.Play("Bucket");
			Gui.Hide();
			yield return C.player_invis.Say("hmmm that wasn't it");
			yield return E.WaitSkip();
			Gui.Show();
		}
		yield return E.Break;
	}
}