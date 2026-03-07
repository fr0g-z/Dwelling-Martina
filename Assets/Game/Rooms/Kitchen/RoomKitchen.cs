using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomKitchen : RoomScript<RoomKitchen>
{


	IEnumerator OnInteractHotspotHallway( IHotspot hotspot )
	{
        Audio.Play("hallwayfootsteps");
        yield return C.Plr.ChangeRoom(R.Hallway_2);
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotLivingroom( IHotspot hotspot )
	{
        Audio.Play("hallwayfootsteps");
        yield return C.Plr.ChangeRoom(R.Livingroom);
		yield return E.Break;
	}

	IEnumerator OnInteractPropTeddyBear( IProp prop )
	{
        yield return C.Display("You pick up the Teddy Bear");
        Audio.Play("Bucket");
        prop.Disable();
        yield return C.player_invis.Say("My childhood teddy...i thought i lost this");
        I.TeddyBear.AddAsActive();
        yield return E.Break;
	}

	IEnumerator OnLookAtHotspotUndertable( IHotspot hotspot )
	{
        
        yield return E.Break;
	}

	IEnumerator OnInteractHotspotUndertable( IHotspot hotspot )
	{
        yield return C.Plr.ChangeRoom(R.UnderTable);
        yield return E.Break;
	}
}