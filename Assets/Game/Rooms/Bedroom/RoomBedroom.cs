using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomBedroom : RoomScript<RoomBedroom>
{


	IEnumerator OnLookAtHotspotWindow( IHotspot hotspot )
	{
		if ( hotspot.FirstLook )
			yield return C.player_invis.Say("I cant see anything through the window");
		else
			yield return C.player_invis.Say("nothing to see here");
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotWindow( IHotspot hotspot )
	{
		yield return C.player_invis.Say("The Window is rusted shut");
		yield return E.Break;
	}

	IEnumerator OnEnterRoomAfterFade()
	{
		C.Plr.SetPosition(Point("Character"));
		yield return E.Break;
	}

	IEnumerator OnLookAtPropFeather( IProp prop )
	{
		yield return C.player_invis.Say("A Feather");
		
		yield return E.Break;
	}

	IEnumerator OnInteractPropFeather( IProp prop )
	{
		yield return C.Display("You pick up the feather");
		Audio.Play("Bucket");
		prop.Disable();
		I.Feather.AddAsActive();
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotHallway( IHotspot hotspot )
	{
		yield return C.Plr.ChangeRoom(R.Hallway);
		yield return E.Break;
	}
}