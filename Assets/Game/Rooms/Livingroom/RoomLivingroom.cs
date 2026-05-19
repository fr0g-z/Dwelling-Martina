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
            Audio.Play("escape");
            yield return C.Plr.ChangeRoom(R.END);
            yield return E.Break;
        }
        else
        {
            Audio.Play("lock");
            yield return C.player_invis.Say("It won't open.");
            yield return C.player_invis.Say("It never opened. Not really.");
        }
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotDollhouse(IHotspot hotspot)
    {
        if (DollHouseDone.DollhouseDone == true)
        {
            yield return C.player_invis.Say("Everything is in its place now.");
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
        yield return C.player_invis.Say("Mum bought this for me. She said I could play house inside it.");
        yield return C.player_invis.Say("Safe, she said. In here you're always safe.");
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotUnderCouch(IHotspot hotspot)
    {
        yield return E.ChangeRoom(R.UnderCouch);
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

    IEnumerator OnInteractHotspotWindows(IHotspot hotspot)
    {
        Audio.Play("window");
        yield return E.Break;
    }

    IEnumerator OnLookAtHotspotWindows(IHotspot hotspot)
    {
        yield return C.player_invis.Say("I can see the street from here.");
        yield return C.player_invis.Say("Mum never let me go out there.");
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotLight(IHotspot hotspot)
    {
        Audio.Play("lightnotworking");
        yield return E.Break;
    }

    IEnumerator OnLookAtHotspotLight(IHotspot hotspot)
    {
        yield return C.player_invis.Say("She stopped replacing the bulbs.");
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotBox(IHotspot hotspot)
    {
        Audio.Play("box");
        yield return E.Break;
    }

    IEnumerator OnLookAtHotspotBox(IHotspot hotspot)
    {
        yield return C.player_invis.Say("A box of my old things. She's been going through them.");
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotCouch(IHotspot hotspot)
    {
        Audio.Play("pillow");
        yield return E.Break;
    }

    IEnumerator OnLookAtHotspotCouch(IHotspot hotspot)
    {
        yield return C.player_invis.Say("Me and Dad used to watch TV here for hours.");
        yield return C.player_invis.Say("He's not coming back. I know that now.");
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotPlant(IHotspot hotspot)
    {
        yield return C.player_invis.Say("Don't steal that plant!");

        Prop("florian").Show();
        yield return E.Wait(2.0f);
        Prop("florian").Hide();
        yield return E.Break;
    }
}