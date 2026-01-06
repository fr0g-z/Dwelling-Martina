using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomLivingroom : RoomScript<RoomLivingroom>
{


	IEnumerator OnInteractHotspotKitchen( IHotspot hotspot )
	{
		yield return C.Plr.ChangeRoom(R.Kitchen);
		yield return E.Break;
	}
}