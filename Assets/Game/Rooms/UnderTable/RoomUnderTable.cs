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
        if (ItemsPlaced.SecretSolution)
        {
            Prop("BasementDoorClosed").Hide();
            Prop("BasementDoorOpen").Show();

            C.Plr.ChangeRoom(R.Basement);
            yield return E.Break;
        }
        else
        {
            C.player_invis.Say("The Latch is locked.");
        }
        yield return E.Break;
    }

    void OnEnterRoom()
    {
        if (ItemsPlaced.SecretSolution)
        {
            Prop("BasementDoorClosed").Hide();
            Prop("BasementDoorOpen").Show();
        }

    }
}