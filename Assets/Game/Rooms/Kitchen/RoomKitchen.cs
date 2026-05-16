using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomKitchen : RoomScript<RoomKitchen>
{


    IEnumerator OnInteractHotspotHallway(IHotspot hotspot)
    {
        Audio.Play("hallwayfootsteps");
        yield return C.Plr.ChangeRoom(R.Hallway_2);
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotLivingroom(IHotspot hotspot)
    {
        Audio.Play("hallwayfootsteps");
        yield return C.Plr.ChangeRoom(R.Livingroom);
        yield return E.Break;
    }

    IEnumerator OnInteractPropTeddyBear(IProp prop)
    {
        yield return C.Display("You pick up the Teddy Bear");
        Audio.Play("Bucket");
        prop.Disable();
        yield return C.player_invis.Say("My childhood teddy...i thought i lost this");
        I.TeddyBear.AddAsActive();
        yield return E.Break;
    }

    IEnumerator OnLookAtHotspotUndertable(IHotspot hotspot)
    {
       
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotUndertable(IHotspot hotspot)
    {
		
        yield return C.Plr.ChangeRoom(R.UnderTable);
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotCage(IHotspot hotspot)
    {
        yield return C.Plr.ChangeRoom(R.Cage);
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotNotes(IHotspot hotspot)
    {
		Audio.Play("paper");
        G.Counternotes.Visible = true;
        yield return E.Break;
    }

    IEnumerator OnLookAtHotspotNotes(IHotspot hotspot)
    {
        yield return C.player_invis.Say("Letters from Aunt Gertrude?");
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotCabinet(IHotspot hotspot)
    {
		Audio.Play("DoorOpen");
        yield return C.Plr.ChangeRoom(R.KitchenCabinet);
        yield return E.Break;
    }

	IEnumerator OnInteractHotspotFruitbowl( IHotspot hotspot )
	{
        Audio.Play("fruit");
        yield return E.Break;
	}

	IEnumerator OnLookAtHotspotFruitbowl( IHotspot hotspot )
	{
        yield return C.player_invis.Say("Im surprised this is full..");
        yield return E.Break;
	}

	IEnumerator OnInteractHotspotChairs( IHotspot hotspot )
	{
        Audio.Play("pillow");
        yield return E.Break;
	}

	IEnumerator OnLookAtHotspotChairs( IHotspot hotspot )
	{
        yield return C.player_invis.Say("Always so comfy!");
        yield return E.Break;
	}

	IEnumerator OnInteractHotspotCooker( IHotspot hotspot )
	{
        Audio.Play("stove");
        yield return E.Break;
	}

	IEnumerator OnLookAtHotspotCooker( IHotspot hotspot )
	{
        yield return C.player_invis.Say("I shouldnt touch that..");
        yield return E.Break;
	}

	IEnumerator OnInteractHotspotSink( IHotspot hotspot )
	{
        Audio.Play("drippingwater");
        yield return E.Break;
	}

	IEnumerator OnLookAtHotspotSink( IHotspot hotspot )
	{
        yield return C.player_invis.Say("It probably doesn't turn on");
        yield return E.Break;
	}

	IEnumerator OnInteractHotspotWindow( IHotspot hotspot )
	{
        Audio.Play("window");
        yield return E.Break;
	}

	IEnumerator OnLookAtHotspotWindow( IHotspot hotspot )
	{
        yield return C.player_invis.Say("It's probably locked");
        yield return E.Break;
	}

	IEnumerator OnInteractHotspotPlates( IHotspot hotspot )
	{
        Audio.Play("plate");
        yield return E.Break;
	}

	IEnumerator OnLookAtHotspotPlates( IHotspot hotspot )
	{
        yield return C.player_invis.Say("I broke so many of these..");
        yield return E.Break;
	}

	IEnumerator OnInteractHotspotCabinets( IHotspot hotspot )
	{
        yield return C.Plr.ChangeRoom(R.Cabnetempty);
        Audio.Play("DoorOpen");
        yield return E.Break;
	}

	IEnumerator OnLookAtHotspotCabinets( IHotspot hotspot )
	{
        yield return C.player_invis.Say("I could never reach up there..Im taller now!");
        yield return E.Break;
	}
}