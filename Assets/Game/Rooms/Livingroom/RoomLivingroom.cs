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
            yield return C.Plr.ChangeRoom(R.END);
            yield return E.Break;
        }
        else
        {
            yield return C.player_invis.Say("The door is locked.");
        }
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotDollhouse(IHotspot hotspot)
    {
        if (DollHouseDone.DollhouseDone == true)
        {
            yield return C.player_invis.Say("I dont have anymore time to play");
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

        yield return E.Break;
    }

    IEnumerator OnInteractHotspotUnderCouch(IHotspot hotspot)
    {
        yield return E.ChangeRoom(R.UnderCouch);
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotNOTE_TABLE(IHotspot hotspot)
    {
        {
            G.BedNote.Visible = true;
            yield return E.Break;
        }

    }

    IEnumerator OnLookAtHotspotNOTE_TABLE(IHotspot hotspot)
    {

        yield return E.Break;
    }

    IEnumerator OnInteractHotspotBloodynote(IHotspot hotspot)
    {
        G.Bloodynote.Visible = true;
        yield return E.Break;
    }

    IEnumerator OnLookAtHotspotPlant(IHotspot hotspot)
    {
        yield return E.Break;

    }
}