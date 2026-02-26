using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomHallway : RoomScript<RoomHallway>
{


	IEnumerator OnInteractHotspotBedroom( IHotspot hotspot )
	{
        Audio.Play("Dooropen");
        yield return C.Plr.ChangeRoom(R.Bedroom);
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotVanity( IHotspot hotspot )
	{
		yield return C.Plr.ChangeRoom(R.Vanity);
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotHallway( IHotspot hotspot )
	{
		yield return C.Plr.ChangeRoom(R.Hallway_2);
		yield return E.Break;
	}
}