using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomBasement : RoomScript<RoomBasement>
{


	IEnumerator OnLookAtHotspotUpstairs( IHotspot hotspot )
	{

		yield return E.Break;
	}

	IEnumerator OnInteractHotspotUpstairs( IHotspot hotspot )
	{
        yield return C.Plr.ChangeRoom(R.UnderTable);
        yield return E.Break;
	}
}