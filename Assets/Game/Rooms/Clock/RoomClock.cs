using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomClock : RoomScript<RoomClock>
{ 
	IEnumerator OnLookAtHotspotLeave( IHotspot hotspot )
	{
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotLeave( IHotspot hotspot )
	{
        yield return E.ChangeRoom(R.Hallway_2);
        yield return E.Break;
	}
}
