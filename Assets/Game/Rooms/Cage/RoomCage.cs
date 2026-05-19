using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;
using JetBrains.Annotations;

public class RoomCage : RoomScript<RoomCage>
{

    IEnumerator OnInteractHotspotBack(IHotspot hotspot)
    {
        yield return C.Plr.ChangeRoom(R.Kitchen);
        yield return E.Break;
    }

    public IEnumerator OnUseInvPropDoorclose(IProp prop, IInventory item)
    {
        if (item == I.Keyundercouch)
        {
            prop.Disable();
            Audio.Play("cageopen");
            Prop("Dooropen").Enable();
            yield return C.player_invis.Say("The cage is open.");
            yield return E.Wait(1.5f);
            yield return C.player_invis.Say("Oh.");
            yield return C.player_invis.Say("It's been dead for a long time.");
            yield return E.Wait(1.5f);
            yield return C.player_invis.Say("She locked it in here and forgot about it.");
            yield return C.player_invis.Say("Or maybe she didn't forget. Maybe she just couldn't open the door.");
            yield return E.Wait(1.5f);
            yield return C.player_invis.Say("There's a feather left.");
            item.Remove();
            I.Feather.AddAsActive();
        }
        else
        {
            yield return C.player_invis.Say("That won't open this.");
        }
    }
}