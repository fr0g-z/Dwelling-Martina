using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomKitchenCabinet : RoomScript<RoomKitchenCabinet>
{

    IEnumerator OnInteractHotspotBack(IHotspot hotspot)
    {
        yield return C.Plr.ChangeRoom(R.Kitchen);
        yield return E.Break;
    }

    IEnumerator OnInteractPropTeddybear(IProp prop)
    {
        yield return C.Display("You pick up the Teddy Bear");
        Audio.Play("Bucket");
        prop.Disable();
        yield return C.player_invis.Say("Mr. Bear.");
        yield return C.player_invis.Say("I used to bring him everywhere. Mum would pretend to be embarrassed.");
        yield return C.player_invis.Say("She kept him in here. Where she could see him from the kitchen.");
        I.TeddyBear.AddAsActive();
        yield return E.Break;
    }
}