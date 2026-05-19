using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomBathroom : RoomScript<RoomBathroom>
{

    IEnumerator OnInteractHotspotHallway(IHotspot hotspot)
    {
        Audio.Stop("drippingwater");
        Audio.Play("Dooropen");
        yield return C.Plr.ChangeRoom(R.Hallway_2);
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotPillbox(IHotspot hotspot)
    {
        Audio.Play("pills");
        G.Pills.Visible = true;
        yield return E.Break;
    }

    IEnumerator OnLookAtHotspotPillbox(IHotspot hotspot)
    {
        yield return C.player_invis.Say("Mum's pills. There are so many of them now.");
        yield return C.player_invis.Say("She never used to need these.");
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotSink(IHotspot hotspot)
    {
        Audio.Play("drippingwater");
        yield return C.player_invis.Say("The water won't come on. Just that constant dripping.");
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotShower(IHotspot hotspot)
    {
        Audio.Play("splash");
        yield return C.player_invis.Say("The water is freezing cold.");
        yield return C.player_invis.Say("I should go change, Ithink theres some clothes in Mum's room.");
        ShowerSplash.ShowerSplashed = true;
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotMirror(IHotspot hotspot)
    {
        Audio.Play("mirror");
        G.Bathmirror.Visible = true;
        yield return E.Break;
    }

    IEnumerator OnLookAtHotspotMirror(IHotspot hotspot)
    {
        yield return C.player_invis.Say("She covered it with newspaper.");
        yield return C.player_invis.Say("Every single mirror in the house.");
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotToilet(IHotspot hotspot)
    {
        Audio.Play("toilet");
        yield return E.Break;
    }

    IEnumerator OnLookAtHotspotToilet(IHotspot hotspot)
    {
        yield return C.player_invis.Say("At least something still works in here.");
        yield return E.Break;
    }
}