using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomInsidedollhouse : RoomScript<RoomInsidedollhouse>
{

    IEnumerator OnInteractHotspotBack(IHotspot hotspot)
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
        yield return C.player_invis.Say("There's something hidden behind this.");
        yield return E.Break;
    }

    public IEnumerator puzzleComplete()
    {
        if (DollHouseDone.DollhouseDone == true)
        {
            Prop("DollFlap").Hide();
            yield return C.player_invis.Say("That's how we were.");
            yield return C.player_invis.Say("Before everything changed.");
            yield return E.Wait(1.5f);
            yield return C.player_invis.Say("There's something hidden inside.");
            yield return E.Wait(1.0f);
            yield return C.player_invis.Say("A doll.");
            yield return C.player_invis.Say("I made this. I carved her face from wood and painted it to look like Mum.");
            yield return E.Wait(1.5f);
            yield return C.player_invis.Say("I made it so she'd always have someone.");
            yield return C.player_invis.Say("So she'd never be alone, even when I wasn't there.");
            yield return E.Wait(1.5f);
            yield return C.player_invis.Say("I didn't know then what 'not being there' would really mean.");
            I.SecretDoll.AddAsActive();
            yield return E.Break;
        }
    }

    IEnumerator OnInteractPropDollReward(IProp prop)
    {
        yield return E.Break;
    }
}