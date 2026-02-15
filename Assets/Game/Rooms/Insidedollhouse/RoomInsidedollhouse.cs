using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomInsidedollhouse : RoomScript<RoomInsidedollhouse>
{


	IEnumerator OnInteractHotspotBack( IHotspot hotspot )
	{

        yield return E.ChangeRoom(R.Livingroom);
        yield return E.Break;
	}

	IEnumerator OnLookAtHotspotBack( IHotspot hotspot )
	{

		yield return E.Break;
	}
}