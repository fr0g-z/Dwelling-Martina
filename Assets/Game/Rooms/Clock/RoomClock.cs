using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomClock : RoomScript<RoomClock>
{ 
	IEnumerator OnInteractHotspotLeaveroom( IHotspot hotspot )
	{
        yield return C.Plr.ChangeRoom(R.Hallway_2);
        yield return E.Break;
	}
}
