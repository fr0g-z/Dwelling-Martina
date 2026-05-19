using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomBasement : RoomScript<RoomBasement>
{

    IEnumerator OnLookAtHotspotUpstairs(IHotspot hotspot)
    {
        yield return C.player_invis.Say("I could go back up. I could pretend I never came down here.");
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotUpstairs(IHotspot hotspot)
    {
        yield return C.Plr.ChangeRoom(R.UnderTable);
        yield return E.Break;
    }

    void OnEnterRoom()
    {
        Audio.Stop("Gamesoundtrack");
        Audio.Play("mombreathing");
    }

    IEnumerator OnInteractHotspotMom(IHotspot hotspot)
    {
        yield return C.player_invis.Say("Mum...");
        yield return C.player_invis.Say("What have you done.");
        yield return E.Wait(1.5f);
        yield return C.Plr.ChangeRoom(R.BADending);
        yield return E.Break;
    }
}