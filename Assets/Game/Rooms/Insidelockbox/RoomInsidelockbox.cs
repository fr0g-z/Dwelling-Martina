using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomInsidelockbox : RoomScript<RoomInsidelockbox>
{


	IEnumerator OnLookAtHotspotDeathdoc( IHotspot hotspot )
	{

		yield return E.Break;
	}

	IEnumerator OnInteractHotspotDeathdoc( IHotspot hotspot )
	{
        G.Deathdoc.Visible = true;
        yield return E.Break;
	}

	IEnumerator OnInteractHotspotLeavescene( IHotspot hotspot )
	{
		yield return E.ChangeRoom(R.Closet);
		yield return E.Break;
	}

	IEnumerator OnLookAtHotspotLeavescene( IHotspot hotspot )
	{

		yield return E.Break;
	}

	IEnumerator OnUseInvHotspotLeavescene( IHotspot hotspot, IInventory item )
	{

		yield return E.Break;
	}

	IEnumerator OnInteractHotspotAlbum( IHotspot hotspot )
	{
        G.Albumletter.Visible = true;
        yield return E.Break;
	}
}