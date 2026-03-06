using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomInsidedollhouse : RoomScript<RoomInsidedollhouse>
{


	IEnumerator OnInteractHotspotBack( IHotspot hotspot )
	{

        yield return E.ChangeRoom(R.Livingroom);
        yield return E.Break;
	}

    IEnumerator OnExitRoom()
    {
        GameObject.Find("DollhousePuzzle").SetActive(false);
        yield return E.Break;
    }

    IEnumerator OnEnterRoom()
    {
        GameObject.Find("DollhousePuzzle").SetActive(true);
        yield return E.Break;
    }



	IEnumerator OnInteractPropDollFlap( IProp prop )
	{

		yield return E.Break;
	}
}