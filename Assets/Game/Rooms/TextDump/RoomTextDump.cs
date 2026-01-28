using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomTextDump : RoomScript<RoomTextDump>
{


	IEnumerator OnInteractHotspotContinue( IHotspot hotspot )
	{
		E.ChangeRoomBG(R.Bedroom);
		yield return E.Break;
	}
}