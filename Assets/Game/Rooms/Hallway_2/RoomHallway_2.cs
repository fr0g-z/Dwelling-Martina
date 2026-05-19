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
            yield return C.player_invis.Say("Mum never takes this pin off. Why was it just... left here?");
        }

        yield return E.Break;
    }

    IEnumerator OnInteractHotspotHallway(IHotspot hotspot)
    {
        Audio.Play("hallwayfootsteps");
        yield return C.Plr.ChangeRoom(R.Hallway);
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotMom_bedroom(IHotspot hotspot)
    {
        if (ShowerSplash.ShowerSplashed == false)
        {
            Audio.Play("lockeddoor");
            yield return C.player_invis.Say("I shouldn't go in like this. I need to freshen up first.");
        }
        else
        {
            Audio.Play("Dooropen");
            yield return C.Plr.ChangeRoom(R.Mom_room);
            yield return E.Break;
        }
    }

    IEnumerator OnInteractHotspotBathroom(IHotspot hotspot)
    {
        Audio.Play("Dooropen");
        yield return C.Plr.ChangeRoom(R.Bathroom);
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotKitchen(IHotspot hotspot)
    {
        Audio.Play("hallwayfootsteps");
        yield return C.Plr.ChangeRoom(R.Kitchen);
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotClock(IHotspot hotspot)
    {
        yield return C.Plr.ChangeRoom(R.Clock);
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotPainting(IHotspot hotspot)
    {
        Audio.Play("frame");
        G.Painting.Visible = true;
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotPlant(IHotspot hotspot)
    {
        Audio.Play("plant");
        yield return E.Break;
    }

    IEnumerator OnLookAtHotspotPlant(IHotspot hotspot)
    {
        yield return C.player_invis.Say("Don't steal that plant!");

        Prop("florian").Show();
        yield return E.Wait(2.0f);
        Prop("florian").Hide();
        yield return E.Break;
    }

    IEnumerator OnLookAtHotspotPainting(IHotspot hotspot)
    {
        yield return C.player_invis.Say("Mum painted this. Before everything got bad.");
        yield return C.player_invis.Say("She stopped painting after Dad left.");
        yield return E.Break;
    }
}