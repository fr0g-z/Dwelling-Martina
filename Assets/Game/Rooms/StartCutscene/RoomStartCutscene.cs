using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomStartCutscene : RoomScript<RoomStartCutscene>
{

    IEnumerator OnInteractHotspotContinue( IHotspot hotspot )
	{
        yield return E.ChangeRoom(R.Bedroom);
        yield return E.Break;
	}


    IEnumerator OnEnterRoomAfterFade()
    {
        yield return new WaitForSeconds(31.5f);
        yield return E.ChangeRoom(R.Bedroom);
    }
}