using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomCabnetempty : RoomScript<RoomCabnetempty>
{

    bool saidLine = false;

    IEnumerator OnEnterRoomAfterFade()
    {
        if (!saidLine)
        {
            saidLine = true;
            yield return C.player_invis.Say("Empty. She cleared it out.");
            yield return C.player_invis.Say("I don't know when.");
        }
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotBack(IHotspot hotspot)
    {
        yield return C.Plr.ChangeRoom(R.Kitchen);
        yield return E.Break;
    }
}