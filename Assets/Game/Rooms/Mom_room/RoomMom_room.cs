using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomMom_room : RoomScript<RoomMom_room>
{


    bool saidLine = false;

    IEnumerator OnEnterRoomAfterFade()
    {

               if (!saidLine)
        {
            saidLine = true;
            yield return C.player_invis.Say("maybe mom has something i can change into..");
        }

        yield return E.Break;
    }

    IEnumerator OnInteractHotspotHallway(IHotspot hotspot)
    {
        Audio.Play("Dooropen");
        yield return C.Plr.ChangeRoom(R.Hallway_2);
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotCloset(IHotspot hotspot)
    {
        Audio.Play("closeto");
        yield return C.Plr.ChangeRoom(R.Closet);
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotNote(IHotspot hotspot)
    {
        
        G.BedNote.Visible = true;
        yield return E.Break;
    }

	IEnumerator OnInteractHotspotLight( IHotspot hotspot )
	{
        Audio.Play("lightnotworking");
        yield return E.Break;
	}

	IEnumerator OnLookAtHotspotLight( IHotspot hotspot )
	{
        yield return C.player_invis.Say("It doesn't turn on...");
        yield return E.Break;
	}

	IEnumerator OnInteractHotspotWindow( IHotspot hotspot )
	{
        Audio.Play("window");
		yield return E.Break;
	}

	IEnumerator OnLookAtHotspotWindow( IHotspot hotspot )
	{

        yield return C.player_invis.Say("It's locked..like everything else");
        yield return E.Break;
	}

	IEnumerator OnInteractHotspotLoxkchest( IHotspot hotspot )
	{
        Audio.Play("Lockeddoor");
		yield return E.Break;
	}

	IEnumerator OnLookAtHotspotLoxkchest( IHotspot hotspot )
	{
        yield return C.player_invis.Say("This is probably locked too..");
        yield return E.Break;
	}
}