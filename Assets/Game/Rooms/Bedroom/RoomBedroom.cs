using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomBedroom : RoomScript<RoomBedroom>
{
	bool saidLine = false;

    IEnumerator OnEnterRoomAfterFade()
    {
		Audio.Play("Gamesoundtrack");

        // Move player to starting point
        C.Plr.SetPosition(Point("Character"));

        // Say the line only once
        if (!saidLine)
        {
            saidLine = true;
            yield return C.player_invis.Say("Weird dream… I should freshen up…");
        }

        yield return E.Break;
    }
    IEnumerator OnLookAtHotspotWindow( IHotspot hotspot )
	{
		
		if ( hotspot.FirstLook )
			yield return C.player_invis.Say("I cant see anything through the window");
		else
			yield return C.player_invis.Say("nothing to see here");
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotWindow( IHotspot hotspot )
	{
        Audio.Play("window");
        yield return C.player_invis.Say("The Window is boarded shut");
		yield return E.Break;
	}

	IEnumerator OnLookAtPropFeather( IProp prop )
	{
		yield return C.player_invis.Say("A Feather, why is this here?");
		
		yield return E.Break;
	}

	IEnumerator OnInteractPropFeather( IProp prop )
	{
		yield return C.Display("You pick up the feather");
		Audio.Play("Bucket");
		prop.Disable();
		I.Feather.AddAsActive();
        yield return C.player_invis.Say("A white Feather...its dirty i should wash my hands");
        yield return E.Break;
	}

	IEnumerator OnInteractHotspotHallway( IHotspot hotspot )
	{
        Audio.Play("Dooropen");
        yield return C.Plr.ChangeRoom(R.Hallway);
		yield return E.Break;
	}
}