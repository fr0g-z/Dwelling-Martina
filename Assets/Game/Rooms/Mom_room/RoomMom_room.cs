using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomMom_room : RoomScript<RoomMom_room>
{
    

    bool saidLine = false;

    IEnumerator OnEnterRoomAfterFade()
    {

        // Say the line only once
        if (!saidLine)
        {
            saidLine = true;
            yield return C.player_invis.Say("maybe mom has something i can change into..");
        }

        yield return E.Break;
    }

    IEnumerator OnInteractHotspotHallway( IHotspot hotspot )
	{
		yield return C.Plr.ChangeRoom(R.Hallway_2);
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotCloset( IHotspot hotspot )
	{
		yield return C.Plr.ChangeRoom(R.Closet);
		yield return E.Break;
	}
}