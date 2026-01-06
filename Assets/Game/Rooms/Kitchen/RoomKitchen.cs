using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomKitchen : RoomScript<RoomKitchen>
{


	IEnumerator OnInteractHotspotHallway( IHotspot hotspot )
	{
		yield return C.Plr.ChangeRoom(R.Hallway_2);
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotLivingroom( IHotspot hotspot )
	{
		yield return C.Plr.ChangeRoom(R.Livingroom);
		yield return E.Break;
	}
}