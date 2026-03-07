using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomSink : RoomScript<RoomSink>
{
    IEnumerator OnEnterRoomAfterFade()
    {
        yield return C.player_invis.Say("The water isnt turning on...");
    }
	IEnumerator OnInteractHotspotBack( IHotspot hotspot )
	{
        yield return E.ChangeRoom(R.Bathroom);
        yield return E.Break;
	}
}