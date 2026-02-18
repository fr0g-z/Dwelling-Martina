using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomUnderTable : RoomScript<RoomUnderTable>
{


	IEnumerator OnLookAtHotspotAboveTable( IHotspot hotspot )
	{

		yield return E.Break;
	}

	IEnumerator OnInteractHotspotAboveTable( IHotspot hotspot )
	{
        yield return C.Plr.ChangeRoom(R.Kitchen);
        yield return E.Break;
	}

	IEnumerator OnLookAtHotspotBasement( IHotspot hotspot )
	{

		yield return E.Break;
	}

	IEnumerator OnInteractHotspotBasement( IHotspot hotspot )
	{
        yield return C.Plr.ChangeRoom(R.Basement);
        yield return E.Break;
	}
}