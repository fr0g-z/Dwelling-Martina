using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomUnderCouch : RoomScript<RoomUnderCouch>
{


	IEnumerator OnInteractHotspotLeaveRoom( IHotspot hotspot )
	{
		yield return E.ChangeRoom(R.Livingroom);
		yield return E.Break;
	}

	IEnumerator OnInteractPropKey( IProp prop )
	{
		yield return C.Display("You pick up a key");
        prop.Disable();
        yield return C.player_invis.Say("I wonder whats this for?");
        I.Keyundercouch.AddAsActive();
        yield return E.Break;
	}
}