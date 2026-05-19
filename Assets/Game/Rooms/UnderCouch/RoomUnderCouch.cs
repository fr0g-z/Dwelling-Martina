using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomUnderCouch : RoomScript<RoomUnderCouch>
{

    IEnumerator OnInteractHotspotLeaveRoom(IHotspot hotspot)
    {
        yield return E.ChangeRoom(R.Livingroom);
        yield return E.Break;
    }

    IEnumerator OnInteractPropKey(IProp prop)
    {
        yield return C.Display("You pick up a key");
        prop.Disable();
        yield return C.player_invis.Say("A small key. Hidden under here on purpose.");
        yield return C.player_invis.Say("What was she keeping locked up?");
        I.Keyundercouch.AddAsActive();
        yield return E.Break;
    }
}