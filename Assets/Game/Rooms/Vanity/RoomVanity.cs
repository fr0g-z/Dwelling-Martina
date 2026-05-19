using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomVanity : RoomScript<RoomVanity>
{
    ////////////////////////////////////////////////////////////////////////////////////
    // Room Entry

    IEnumerator OnEnterRoomAfterFade()
    {
        Prop("Feather").Visible = ItemsPlaced.FeatherPlaced;
        Prop("SecretDoll").Visible = ItemsPlaced.SecretDollPlaced;
        Prop("Teddybear").Visible = ItemsPlaced.TeddyPlaced;
        Prop("MumsPin").Visible = ItemsPlaced.PinPlaced;

        if (ItemsPlaced.FeatherPlaced || ItemsPlaced.SecretDollPlaced)
            Hotspot("Feather").Disable();
        else if (!ItemsPlaced.TeddyPlaced || !ItemsPlaced.PinPlaced)
            Hotspot("Feather").Disable(); // locked until Past and Present are placed first

        if (ItemsPlaced.TeddyPlaced)
            Hotspot("TeddyBear").Disable();

        if (ItemsPlaced.PinPlaced)
            Hotspot("Pin").Disable();

        if (!ItemsPlaced.AllItemsPlaced && !ItemsPlaced.SecretSolution)
        {
            if (!ItemsPlaced.TeddyPlaced && !ItemsPlaced.PinPlaced)
            {
                yield return C.player_invis.Say("Mum's vanity. She used to sit here every single morning.");
                yield return C.player_invis.Say("She'd hum while she got ready. I'd watch from the doorway.");
                yield return C.player_invis.Say("There are three empty spaces. Like she was waiting for something to fill them.");
            }
            else if (ItemsPlaced.TeddyPlaced && ItemsPlaced.PinPlaced)
            {
                yield return C.player_invis.Say("Just one space left.");
                yield return C.player_invis.Say("This is the part I've been avoiding.");
            }
            else
            {
                yield return C.player_invis.Say("Something still feels unfinished.");
            }
        }

        yield return E.Break;
    }

    ////////////////////////////////////////////////////////////////////////////////////
    // TeddyBear Slot — Past

    IEnumerator OnUseInvHotspotTeddyBear(IHotspot hotspot, IInventory item)
    {
        if (item.ScriptName == "TeddyBear")
        {
            item.Remove();
            ItemsPlaced.TeddyPlaced = true;
            Prop("Teddybear").Show();
            hotspot.Disable();

            yield return C.player_invis.Say("Mr. Bear.");
            yield return C.player_invis.Say("I used to bring him everywhere. Mum would pretend to be embarrassed.");
            yield return C.player_invis.Say("She kept him. She kept everything.");
            yield return E.Wait(1.5f);
            yield return C.player_invis.Say("Past.");

            // If Pin is already placed, both are done — unlock the final slot
            if (ItemsPlaced.PinPlaced)
                Hotspot("Feather").Enable();

            yield return CheckPuzzleComplete();
        }
        else
        {
            yield return C.player_invis.Say("That doesn't belong here.");
        }

        yield return E.Break;
    }

    IEnumerator OnInteractHotspotTeddyBear(IHotspot hotspot)
    {
        if (!ItemsPlaced.TeddyPlaced)
            yield return C.player_invis.Say("Something from before. Something she refused to let go of.");
        yield return E.Break;
    }

    ////////////////////////////////////////////////////////////////////////////////////
    // Pin Slot — Present

    IEnumerator OnUseInvHotspotPin(IHotspot hotspot, IInventory item)
    {
        if (item.ScriptName == "MumsPin")
        {
            item.Remove();
            ItemsPlaced.PinPlaced = true;
            Prop("MumsPin").Show();
            hotspot.Disable();

            yield return C.player_invis.Say("Her pin. She wore this every day for as long as I can remember.");
            yield return C.player_invis.Say("She's not wearing it now. She's not wearing anything.");

            if (ItemsPlaced.TeddyPlaced)
            {
                yield return C.player_invis.Say("She stopped getting dressed. Stopped eating. Stopped everything.");
                yield return E.Wait(1.5f);
                yield return C.player_invis.Say("One more space. The one I keep walking away from.");
            }
            else
            {
                yield return C.player_invis.Say("She's still in there. Still waiting.");
            }

            yield return C.player_invis.Say("Present.");

            // If Teddy is already placed, both are done — unlock the final slot
            if (ItemsPlaced.TeddyPlaced)
                Hotspot("Feather").Enable();

            yield return CheckPuzzleComplete();
        }
        else
        {
            yield return C.player_invis.Say("That doesn't belong here.");
        }

        yield return E.Break;
    }

    IEnumerator OnInteractHotspotPin(IHotspot hotspot)
    {
        if (!ItemsPlaced.PinPlaced)
            yield return C.player_invis.Say("Something of hers. Something she stopped needing.");
        yield return E.Break;
    }

    ////////////////////////////////////////////////////////////////////////////////////
    // Feather Slot — Future. The final choice.
    // First placement: shows the item, says the lines, returns item to inventory.
    // Second placement: the real decision. Says "I have made my choice." Triggers ending.

    bool m_featherPreviewed = false;
    bool m_dollPreviewed = false;

    IEnumerator OnUseInvHotspotFeather(IHotspot hotspot, IInventory item)
    {
        if (item.ScriptName == "Feather")
        {
            if (!m_featherPreviewed)
            {
                // ── FIRST PLACEMENT — preview only, item returned ──
                m_featherPreviewed = true;
                Prop("Feather").Show();

                yield return C.player_invis.Say("The feather.");
                yield return C.player_invis.Say("From the bird that never got out.");
                yield return E.Wait(1.5f);
                yield return C.player_invis.Say("It died in there. Locked away until there was nothing left.");
                yield return C.player_invis.Say("But it left something behind.");
                yield return E.Wait(1.5f);
                yield return C.player_invis.Say("If I place this... I'm choosing to go.");
                yield return C.player_invis.Say("I'm choosing to stop being the thing that keeps me here.");
                yield return E.Wait(1.5f);
                yield return C.player_invis.Say("I'm not ready yet.");
                yield return C.player_invis.Say("Let me think.");

                // Return item — prop stays visible as a preview
                Prop("Feather").Hide();
                // item was never removed, so it stays in inventory automatically
            }
            else
            {
                // ── SECOND PLACEMENT — the real choice ──
                item.Remove();
                ItemsPlaced.FeatherPlaced = true;
                Prop("Feather").Show();
                hotspot.Disable();

                yield return C.player_invis.Say("I have made my choice.");
                yield return E.Wait(1.5f);
                yield return C.player_invis.Say("Future.");
                yield return CheckPuzzleComplete();
            }
        }
        else if (item.ScriptName == "SecretDoll")
        {
            if (!m_dollPreviewed)
            {
                // ── FIRST PLACEMENT — preview only, item returned ──
                m_dollPreviewed = true;
                Prop("SecretDoll").Show();

                yield return C.player_invis.Say("The doll.");
                yield return C.player_invis.Say("I made her look like Mum. So Mum would never feel alone.");
                yield return E.Wait(1.5f);
                yield return C.player_invis.Say("If I place this... I'm choosing to stay.");
                yield return C.player_invis.Say("Not alive. Not free. Just... here. Caught.");
                yield return E.Wait(2f);  
                yield return C.player_invis.Say("Let me think.");

                // Return item — prop hides
                Prop("SecretDoll").Hide();
                // item was never removed, stays in inventory
            }
            else
            {
                // ── SECOND PLACEMENT — the real choice ──
                item.Remove();
                ItemsPlaced.SecretDollPlaced = true;
                Prop("SecretDoll").Show();
                hotspot.Disable();

                yield return C.player_invis.Say("I have made my choice.");
                yield return E.Wait(1.5f);
                yield return C.player_invis.Say("Future.");
                yield return CheckPuzzleComplete();
            }
        }
        else
        {
            yield return C.player_invis.Say("That doesn't belong here.");
        }

        yield return E.Break;
    }

    IEnumerator OnInteractHotspotFeather(IHotspot hotspot)
    {
        if (ItemsPlaced.TeddyPlaced && ItemsPlaced.PinPlaced)
        {
            if (m_featherPreviewed || m_dollPreviewed)
                yield return C.player_invis.Say("I know what I need to do. I just have to do it.");
            else
                yield return C.player_invis.Say("The last space. Whatever I leave here decides everything.");
        }
        else
        {
            yield return C.player_invis.Say("I can't think about this yet. Not until the other two are in place.");
        }
        yield return E.Break;
    }

    ////////////////////////////////////////////////////////////////////////////////////
    // Prop Interactions — picking items back up

    IEnumerator OnInteractPropFeather(IProp prop)
    {
        if (ItemsPlaced.AllItemsPlaced || ItemsPlaced.SecretSolution)
        {
            yield return C.player_invis.Say("I shouldn't disturb this.");
            yield return E.Break;
            yield break;
        }

        ItemsPlaced.FeatherPlaced = false;
        prop.Hide();
        Hotspot("Feather").Enable();
        I.Feather.AddAsActive();
        yield return C.player_invis.Say("No. Not yet. I'm not ready.");
        yield return E.Break;
    }

    IEnumerator OnInteractPropSecretDoll(IProp prop)
    {
        if (ItemsPlaced.AllItemsPlaced || ItemsPlaced.SecretSolution)
        {
            yield return C.player_invis.Say("I shouldn't disturb this.");
            yield return E.Break;
            yield break;
        }

        ItemsPlaced.SecretDollPlaced = false;
        prop.Hide();
        Hotspot("Feather").Enable();
        I.SecretDoll.AddAsActive();
        yield return C.player_invis.Say("No. Not yet. I'm not ready.");
        yield return E.Break;
    }

    IEnumerator OnInteractPropTeddybear(IProp prop)
    {
        if (ItemsPlaced.AllItemsPlaced || ItemsPlaced.SecretSolution)
        {
            yield return C.player_invis.Say("I shouldn't disturb this.");
            yield return E.Break;
            yield break;
        }

        ItemsPlaced.TeddyPlaced = false;
        prop.Hide();
        Hotspot("TeddyBear").Enable();
        I.TeddyBear.AddAsActive();
        yield return C.player_invis.Say("I picked Mr. Bear back up. I'm not done yet.");
        yield return E.Break;
    }

    IEnumerator OnInteractPropMumsPin(IProp prop)
    {
        if (ItemsPlaced.AllItemsPlaced || ItemsPlaced.SecretSolution)
        {
            yield return C.player_invis.Say("I shouldn't disturb this.");
            yield return E.Break;
            yield break;
        }

        ItemsPlaced.PinPlaced = false;
        prop.Hide();
        Hotspot("Pin").Enable();
        I.MumsPin.AddAsActive();
        yield return C.player_invis.Say("I picked the pin back up. Not yet.");
        yield return E.Break;
    }

    ////////////////////////////////////////////////////////////////////////////////////
    // Puzzle Completion

    IEnumerator CheckPuzzleComplete()
    {
        if (ItemsPlaced.SecretSolution)
        {
            // Bad ending — the child chooses to stay, tethered by the mother's grief
            yield return C.player_invis.Say("Something shifted. Deep in the house.");
            yield return E.Wait(1.5f);
            yield return C.player_invis.Say("Like a door closing.");
            yield return C.player_invis.Say("That's okay. I didn't want to leave anyway.");
            Audio.Play("DoorOpen");
        }
        else if (ItemsPlaced.AllItemsPlaced)
        {
            // Good ending — acceptance, release
            yield return C.player_invis.Say("Something opened.");
            yield return E.Wait(1.5f);
            yield return C.player_invis.Say("It feels like the first breath after a very long time underwater.");
            Audio.Play("DoorOpen");
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////
    // Navigation & Misc

    IEnumerator OnInteractHotspotHallway(IHotspot hotspot)
    {
        yield return C.Plr.ChangeRoom(R.Hallway);
        yield return E.Break;
    }

    IEnumerator OnInteractHotspotNote(IHotspot hotspot)
    {
        G.VanityNote.Visible = true;
        yield return E.Break;
    }
}