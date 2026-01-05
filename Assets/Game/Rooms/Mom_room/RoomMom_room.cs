using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomMom_room : RoomScript<RoomMom_room>
{


	IEnumerator OnInteractHotspotHallway( IHotspot hotspot )
	{
		yield return C.Plr.ChangeRoom(R.Hallway_2);
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotCloset( IHotspot hotspot )
	{
		yield return C.Plr.ChangeRoom(R.Closet);
		yield return E.Break;
	}
}