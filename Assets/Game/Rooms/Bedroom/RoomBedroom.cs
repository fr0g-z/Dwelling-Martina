using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomBedroom : RoomScript<RoomBedroom>
{
    bool saidLine = false;
    int m_windowLookCount = 0;

    IEnumerator OnEnterRoomAfterFade()
    {
        Audio.Play("Gamesoundtrack");

        C.Plr.SetPosition(Point("Character"));

        if (!saidLine)
        {
            saidLine = true;
            yield return C.player_invis.Say("Everything feels... strange. Like I've been asleep for a long time.");
            yield return C.player_invis.Say("I should go freshen up.");
        }

        yield return E.Break;
    }

    IEnumerator OnLookAtHotspotWindow(IHotspot hotspot)
    {
        m_windowLookCount++;

        if (hotspot.FirstLook)
        {
            yield return C.player_invis.Say("It's so dark out. What time is it?");
        }
        else
        {
            yield return C.player_invis.Say("I can't see anything. The glass is too dirty.");
        }

        if (m_windowLookCount == 3)
        {
            yield return C.player_invis.Say("...there's nothing out there. Nothing at all.");

            Prop("Cateasteregg").Show();
            Audio.Play("pipe");
            yield return E.Wait(2.0f);
            Prop("Cateasteregg").Hide();
        }

        yield return E.Break;
    }

    IEnumerator OnInteractHotspotWindow(IHotspot hotspot)
    {
        Audio.Play("window");
        yield return C.player_invis.Say("It's boarded up. When did Mum do that?");
        yield return E.Break;
    }

    IEnumerator OnLookAtPropFeather(IProp prop)
    {
        yield return C.player_invis.Say("A white feather. It shouldn't be in here.");
        yield return E.Break;
    }

    IEnumerator OnInteractPropFeather(IProp prop)
    {
        yield return C.Display("You pick up the feather");
        Audio.Play("Bucket");
        prop.Disable();
        yield return C.player_invis.Say("It's so clean. Too clean for somewhere this dusty.");
        yield return C.player_invis.Say("I'll hold onto it.");
        I.Feather.AddAsActive();
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotHallway(IHotspot hotspot)
    {
        Audio.Play("Dooropen");
        yield return C.Plr.ChangeRoom(R.Hallway);
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotFloordrawing(IHotspot hotspot)
    {
        Audio.Play("paper");
        G.Floordrawing.Visible = true;
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotHanginpic(IHotspot hotspot)
    {
        Audio.Play("paper");
        G.Walldrawing.Visible = true;
        yield return E.Break;
    }

    IEnumerator OnLookAtHotspotLights(IHotspot hotspot)
    {
        yield return C.player_invis.Say("It won't turn on. Nothing in this house seems to work anymore.");
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotLights(IHotspot hotspot)
    {
        Audio.Play("lightnotworking");
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotPillow(IHotspot hotspot)
    {
        Audio.Play("pillow");
        yield return E.Break;
    }

    IEnumerator OnLookAtHotspotPillow(IHotspot hotspot)
    {
        yield return C.player_invis.Say("Mum used to watch me sleep to make sure I was safe...it got strange the more I grew up");
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotOutlet(IHotspot hotspot)
    {
        Audio.Play("outletshock");
        yield return E.Break;
    }

    IEnumerator OnLookAtHotspotOutlet(IHotspot hotspot)
    {
        yield return C.player_invis.Say("Mum always said that outlet was dangerous. She taped over it once.");
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotChest(IHotspot hotspot)
    {
        Audio.Play("lockeddoor");
        yield return E.Break;
    }

    IEnumerator OnLookAtHotspotChest(IHotspot hotspot)
    {
        yield return C.player_invis.Say("My toys. She kept everything exactly where I left it.");
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotInstructionnote(IHotspot hotspot)
    {
        G.INSTRUCTIONS.Visible = true;
        yield return E.Break;
    }

    IEnumerator OnLookAtHotspotInstructionnote(IHotspot hotspot)
    {
        yield return C.player_invis.Say("I should read this. Left click.");
        yield return E.Break;
    }

    IEnumerator OnLookAtHotspotHanginpic(IHotspot hotspot)
    {
        yield return C.player_invis.Say("I made that when I was little. Mum said she'd keep it forever.");
        yield return E.Break;
    }

    IEnumerator OnLookAtHotspotFloordrawing(IHotspot hotspot)
    {
        yield return C.player_invis.Say("Three of us. Me, Mum, and Dad. We were happy then.");
        yield return E.Break;
    }
}