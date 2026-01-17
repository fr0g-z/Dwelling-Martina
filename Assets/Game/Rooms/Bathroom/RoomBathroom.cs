using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomBathroom : RoomScript<RoomBathroom>
{


	IEnumerator OnInteractHotspotHallway( IHotspot hotspot )
	{
		yield return C.Plr.ChangeRoom(R.Hallway_2);
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotPillbox( IHotspot hotspot )
	{

       G.Pills.Visible = true;
        // yield return C.player_invis.Say("This expiry date looks familiar");
      //  yield return E.WaitSkip(0);
       // yield return (C.player_invis.Say("This expiry date looks familiar"));
       yield return E.Break;
	}

	IEnumerator OnLookAtHotspotPillbox( IHotspot hotspot )
	{
		
		yield return E.Break;
	}
}