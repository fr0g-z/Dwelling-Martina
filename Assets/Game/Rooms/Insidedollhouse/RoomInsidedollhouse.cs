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

    IEnumerator OnLookAtPropDollFlap(IProp prop)
    {
        yield return E.Break;
    }

    public IEnumerator puzzleComplete()
    {

        if (DollHouseDone.DollhouseDone == true)
        {
            Prop("DollFlap").Hide();
            C.player_invis.Say("The Latch Opened!!");
            I.SecretDoll.AddAsActive();
            yield return E.Break; // stop the coroutine
        }
       
    }

	IEnumerator OnInteractPropDollReward( IProp prop )
	{
        
        yield return E.Break;
	}
}