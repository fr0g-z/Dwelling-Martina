using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;
using JetBrains.Annotations;

public class RoomCage : RoomScript<RoomCage>
{


	IEnumerator OnInteractHotspotBack( IHotspot hotspot )
	{
        yield return C.Plr.ChangeRoom(R.Kitchen);
        yield return E.Break;
	}

    IEnumerator OnUseInvPropDoorclose(IProp prop, IInventory item)
    {
        if (item == I.Keyundercouch)
        {
            C.player_invis.Say("What The Dog Doin.");
            prop.Disable();
            Prop("Dooropen").Enable();
            item.Remove();
            I.Feather.AddAsActive();
            yield return E.Break;
        }
        else
        {
            C.player_invis.Say("That won't work.");
        }
    }
}
