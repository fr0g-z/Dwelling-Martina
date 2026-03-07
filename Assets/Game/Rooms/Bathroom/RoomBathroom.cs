using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomBathroom : RoomScript<RoomBathroom>
{


    IEnumerator OnInteractHotspotHallway(IHotspot hotspot)
    {
        Audio.Stop("drippingwater");
        Audio.Play("Dooropen");
        yield return C.Plr.ChangeRoom(R.Hallway_2);
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotPillbox( IHotspot hotspot )
	{
        Audio.Play("pills");
        G.Pills.Visible = true;

       yield return E.Break;
	}

	IEnumerator OnLookAtHotspotPillbox( IHotspot hotspot )
    {
		
        yield return E.Break;
	}

	IEnumerator OnInteractHotspotSink( IHotspot hotspot )
	{

        Audio.Play("drippingwater");
        yield return C.player_invis.Say("The water isnt turning on...");
        yield return E.Break;
	}

	IEnumerator OnInteractHotspotShower( IHotspot hotspot )
	{
        yield return C.player_invis.Say("The water splashed me...i need to change now");
		ShowerSplash.ShowerSplashed = true;
        yield return E.Break;
	}

}