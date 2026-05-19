using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomUnderTable : RoomScript<RoomUnderTable>
{

    IEnumerator OnLookAtHotspotAboveTable(IHotspot hotspot)
    {
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotAboveTable(IHotspot hotspot)
    {
        yield return C.Plr.ChangeRoom(R.Kitchen);
        yield return E.Break;
    }

    IEnumerator OnLookAtHotspotBasement(IHotspot hotspot)
    {
        yield return C.player_invis.Say("There's something down there. I can feel it.");
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotBasement(IHotspot hotspot)
    {
        if (ItemsPlaced.SecretSolution)
        {
            Prop("BasementDoorClosed").Hide();
            Prop("BasementDoorOpen").Show();
            yield return C.player_invis.Say("The latch is open now.");
            yield return C.Plr.ChangeRoom(R.Basement);
            yield return E.Break;
        }
        else
        {
            yield return C.player_invis.Say("It's latched from the other side.");
            yield return C.player_invis.Say("She doesn't want anyone going down there.");
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