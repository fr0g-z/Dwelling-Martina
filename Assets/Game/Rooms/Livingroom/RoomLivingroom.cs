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

	IEnumerator OnInteractHotspotThe_Door( IHotspot hotspot )
	{
		if (ItemsPlaced.AllItemsPlaced)
		{
            C.Plr.ChangeRoom(R.END);
            yield return E.Break;
		}
		else
		{
			C.player_invis.Say("The door is locked.");
        }
        yield return E.Break;
	}
}