using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomCloset : RoomScript<RoomCloset>
{


	IEnumerator OnInteractHotspotMoms_room( IHotspot hotspot )
	{
		yield return C.Plr.ChangeRoom(R.Mom_room);
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotLock( IHotspot hotspot )
	{
		G.CombLock.Show();
		yield return E.Break;
	}

	IEnumerator OnLookAtHotspotLock( IHotspot hotspot )
	{

		yield return E.Break;
	}

	IEnumerator OnUseInvHotspotLock( IHotspot hotspot, IInventory item )
	{

		yield return E.Break;
	}
}