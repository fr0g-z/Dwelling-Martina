using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomLivingroom : RoomScript<RoomLivingroom>
{


    IEnumerator OnInteractHotspotKitchen(IHotspot hotspot)
    {
        Audio.Play("hallwayfootsteps");
        yield return C.Plr.ChangeRoom(R.Kitchen);
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotThe_Door(IHotspot hotspot)
    {
        if (ItemsPlaced.AllItemsPlaced)
        {
            Audio.Play("Dooropen");
            yield return C.Plr.ChangeRoom(R.END);
            yield return E.Break;
        }
        else
        {
            Audio.Play("lockeddoor");
            yield return C.player_invis.Say("The door is locked.");
        }
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotDollhouse(IHotspot hotspot)
    {
        if (DollHouseDone.DollhouseDone == true)
        {
            yield return C.player_invis.Say("I don't have anymore time to play");
            yield return E.Break;
        }
        else
        {
            yield return E.ChangeRoom(R.Insidedollhouse);
        }
        yield return E.Break;
    }

    IEnumerator OnLookAtHotspotDollhouse(IHotspot hotspot)
    {
        yield return C.player_invis.Say("I spent so much time playing with this..Im too old now");
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotUnderCouch(IHotspot hotspot)
    {
        yield return E.ChangeRoom(R.UnderCouch);
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotBloodynote(IHotspot hotspot)
    {
        G.Bloodynote.Visible = true;
        yield return E.Break;
    }

    IEnumerator OnLookAtHotspotPlant(IHotspot hotspot)
    {
        //florain
        yield return E.Break;

    }

	IEnumerator OnInteractHotspotWindows( IHotspot hotspot )
	{
        Audio.Play("window");
		yield return E.Break;
	}

	IEnumerator OnLookAtHotspotWindows( IHotspot hotspot )
	{
        yield return C.player_invis.Say("It's probably locked");
        yield return E.Break;
	}

	IEnumerator OnInteractHotspotLight( IHotspot hotspot )
	{
        Audio.Play("");
		yield return E.Break;
	}

	IEnumerator OnLookAtHotspotLight( IHotspot hotspot )
	{
        yield return C.player_invis.Say("I don't think this will turn on");
        yield return E.Break;
	}

	IEnumerator OnInteractHotspotBox( IHotspot hotspot )
	{
        Audio.Play("");
		yield return E.Break;
	}

	IEnumerator OnLookAtHotspotBox( IHotspot hotspot )
	{
        yield return C.player_invis.Say("We haven't bought anything in ages..why is this here?");
        yield return E.Break;
	}

	IEnumerator OnInteractHotspotCouch( IHotspot hotspot )
	{
        Audio.Play("");
		yield return E.Break;
	}

	IEnumerator OnLookAtHotspotCouch( IHotspot hotspot )
	{
        yield return C.player_invis.Say("Me and dad used to watch so much TV here..i wonder how he is doing now");
        yield return E.Break;
	}

	IEnumerator OnInteractHotspotPlant( IHotspot hotspot )
	{
        //floarian
		yield return E.Break;
	}
}