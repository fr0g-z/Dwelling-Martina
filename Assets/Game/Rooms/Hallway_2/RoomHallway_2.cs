using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomHallway_2 : RoomScript<RoomHallway_2>
{


    bool saidLine = false;

    IEnumerator OnEnterRoomAfterFade()
    {
	
			if (I.MumsPin.Owned)
			{
				yield return C.player_invis.Say("My mums hairpin...why is this here?");
			}
		    else
		    {
			yield return C.player_invis.Say("");
		    }
			yield return E.Break;
    }

    IEnumerator OnInteractHotspotHallway( IHotspot hotspot )
	{
		yield return C.Plr.ChangeRoom(R.Hallway);
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotMom_bedroom( IHotspot hotspot )
	{
       
        yield return C.Plr.ChangeRoom(R.Mom_room);
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotBathroom( IHotspot hotspot )
	{
		yield return C.Plr.ChangeRoom(R.Bathroom);
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotKitchen( IHotspot hotspot )
	{
		yield return C.Plr.ChangeRoom(R.Kitchen);
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotClock( IHotspot hotspot )
	{
        yield return C.Plr.ChangeRoom(R.Clock);
        yield return E.Break;
	}
}