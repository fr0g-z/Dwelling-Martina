using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomCabnetempty : RoomScript<RoomCabnetempty>
{

    IEnumerator OnInteractHotspotBack(IHotspot hotspot)
    {
        yield return C.Plr.ChangeRoom(R.Kitchen);
        yield return E.Break;
    }
   
}