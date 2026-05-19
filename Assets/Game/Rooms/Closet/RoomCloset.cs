using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomCloset : RoomScript<RoomCloset>
{

    bool saidLine = false;

    IEnumerator OnEnterRoomAfterFade()
    {
        if (!saidLine)
        {
            saidLine = true;
            yield return C.player_invis.Say("There's a lockbox in here.");
            yield return C.player_invis.Say("She hid something. Something she needed a code to keep safe.");
        }

        yield return E.Break;
    }

    IEnumerator OnInteractHotspotMoms_room(IHotspot hotspot)
    {
        Audio.Play("closetc");
        yield return C.Plr.ChangeRoom(R.Mom_room);
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotLock(IHotspot hotspot)
    {
        if (updatelockbox.LockboxUnlocked)
        {
            yield return C.player_invis.Say("It's already open.");
            yield return E.ChangeRoom(R.Insidelockbox);
            yield return E.Break;
        }
        else
        {
            Audio.Play("lock");
            G.CombLock.Visible = true;
            yield return E.Break;
        }
    }

    IEnumerator OnLookAtHotspotLock(IHotspot hotspot)
    {
        yield return E.Break;
    }

    IEnumerator OnUseInvHotspotLock(IHotspot hotspot, IInventory item)
    {
        yield return E.Break;
    }
}