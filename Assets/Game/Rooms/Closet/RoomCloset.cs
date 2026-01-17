using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomCloset : RoomScript<RoomCloset>
{


	IEnumerator OnInteractHotspotMoms_room( IHotspot hotspot )
	{
		yield return C.Plr.ChangeRoom(R.Mom_room);
		yield return E.Break;
	}

    IEnumerator OnInteractHotspotLock(IHotspot hotspot)
    {
        // If the lock is already unlocked, go straight to the room
        if (updatelockbox.LockboxUnlocked)
        {
            yield return C.player_invis.Say("It's already unlocked");
            yield return E.ChangeRoom(R.Insidelockbox);
            yield return E.Break;
        }
        else
        {
            G.CombLock.Visible = true;
            yield return E.Break;

        }
    }

    IEnumerator OnLookAtHotspotLock( IHotspot hotspot )
	{

		yield return E.Break;
	}

	IEnumerator OnUseInvHotspotLock( IHotspot hotspot, IInventory item )
	{

		yield return E.Break;
	}
}