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
        if (!saidLine)
        {
            saidLine = true;
            yield return C.player_invis.Say("Mum's room.");
            yield return C.player_invis.Say("Why's the window boarded shut..?");
        }

        yield return E.Break;
    }

    IEnumerator OnInteractHotspotHallway(IHotspot hotspot)
    {
        Audio.Play("Dooropen");
        yield return C.Plr.ChangeRoom(R.Hallway_2);
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotCloset(IHotspot hotspot)
    {
        Audio.Play("closeto");
        yield return C.Plr.ChangeRoom(R.Closet);
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotNote(IHotspot hotspot)
    {
        G.BedNote.Visible = true;
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotLight(IHotspot hotspot)
    {
        Audio.Play("lightnotworking");
        yield return E.Break;
    }

    IEnumerator OnLookAtHotspotLight(IHotspot hotspot)
    {
        yield return C.player_invis.Say("She's been sitting in the dark.");
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotWindow(IHotspot hotspot)
    {
        Audio.Play("window");
        yield return E.Break;
    }

    IEnumerator OnLookAtHotspotWindow(IHotspot hotspot)
    {
        yield return C.player_invis.Say("Locked from the inside. She doesn't want to see out.");
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotLoxkchest(IHotspot hotspot)
    {
        Audio.Play("Lockeddoor");
        yield return E.Break;
    }

    IEnumerator OnLookAtHotspotLoxkchest(IHotspot hotspot)
    {
        yield return C.player_invis.Say("Locked. Whatever's in there, she doesn't want anyone finding it.");
        yield return E.Break;
    }
}