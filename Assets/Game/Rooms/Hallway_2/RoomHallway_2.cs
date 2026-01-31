using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomHallway_2 : RoomScript<RoomHallway_2>
{

    bool m_mumsPinDialogueShown = false;


    IEnumerator OnEnterRoomAfterFade()
    {
        if (I.MumsPin.Owned && m_mumsPinDialogueShown == false)
        {
            m_mumsPinDialogueShown = true;
            yield return C.player_invis.Say("My mums hairpin...why is this here?");
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