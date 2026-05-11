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
        // Sync prop visibility with saved state on every room entry
        Prop("Feather").Visible = ItemsPlaced.FeatherPlaced;
        Prop("SecretDoll").Visible = ItemsPlaced.SecretDollPlaced;
        Prop("Teddybear").Visible = ItemsPlaced.TeddyPlaced;
        Prop("MumsPin").Visible = ItemsPlaced.PinPlaced;

        // Disable hotspots for slots that are already occupied
        if (ItemsPlaced.FeatherPlaced || ItemsPlaced.SecretDollPlaced)
            Hotspot("Feather").Disable();

        if (ItemsPlaced.TeddyPlaced)
            Hotspot("TeddyBear").Disable();

        if (ItemsPlaced.PinPlaced)
            Hotspot("Pin").Disable();

        // Only play intro line if puzzle is still fresh
        if (!ItemsPlaced.AllItemsPlaced && !ItemsPlaced.SecretSolution)
            yield return C.player_invis.Say("looks like i need three items to put here...but what?");

        yield return E.Break;
    }

    ////////////////////////////////////////////////////////////////////////////////////
    // Feather Slot — accepts Feather OR SecretDoll, never both

    IEnumerator OnUseInvHotspotFeather(IHotspot hotspot, IInventory item)
    {
        if (item.ScriptName == "Feather")
        {
            item.Remove();
            ItemsPlaced.FeatherPlaced = true;
            Prop("Feather").Show();
            hotspot.Disable(); // hide hotspot prompt now slot is filled
            yield return C.player_invis.Say("Future.");
            yield return CheckPuzzleComplete();
        }
        else if (item.ScriptName == "SecretDoll")
        {
            item.Remove();
            ItemsPlaced.SecretDollPlaced = true;
            Prop("SecretDoll").Show();
            hotspot.Disable(); // hide hotspot prompt now slot is filled
            yield return C.player_invis.Say("Future.");
            yield return CheckPuzzleComplete();
        }
        else
        {
            yield return C.player_invis.Say("That doesn't belong here.");
        }

        yield return E.Break;
    }

    // Clicking the empty Feather hotspot with no item
    IEnumerator OnInteractHotspotFeather(IHotspot hotspot)
    {
        yield return C.player_invis.Say("I should place something here.");
        yield return E.Break;
    }

    ////////////////////////////////////////////////////////////////////////////////////
    // TeddyBear Slot

    IEnumerator OnUseInvHotspotTeddyBear(IHotspot hotspot, IInventory item)
    {
        if (item.ScriptName == "TeddyBear")
        {
            item.Remove();
            ItemsPlaced.TeddyPlaced = true;
            Prop("Teddybear").Show();
            hotspot.Disable(); // hide hotspot prompt now slot is filled
            yield return C.player_invis.Say("Past.");
            yield return CheckPuzzleComplete();
        }
        else
        {
            yield return C.player_invis.Say("That doesn't belong here.");
        }

        yield return E.Break;
    }

    // Clicking the empty TeddyBear hotspot with no item
    IEnumerator OnInteractHotspotTeddyBear(IHotspot hotspot)
    {
        yield return C.player_invis.Say("I should place something here.");
        yield return E.Break;
    }

    ////////////////////////////////////////////////////////////////////////////////////
    // Pin Slot

    IEnumerator OnUseInvHotspotPin(IHotspot hotspot, IInventory item)
    {
        if (item.ScriptName == "MumsPin")
        {
            item.Remove();
            ItemsPlaced.PinPlaced = true;
            Prop("MumsPin").Show();
            hotspot.Disable(); // hide hotspot prompt now slot is filled
            yield return C.player_invis.Say("Present.");
            yield return CheckPuzzleComplete();
        }
        else
        {
            yield return C.player_invis.Say("That doesn't belong here.");
        }

        yield return E.Break;
    }

    // Clicking the empty Pin hotspot with no item
    IEnumerator OnInteractHotspotPin(IHotspot hotspot)
    {
        yield return C.player_invis.Say("I should place something here.");
        yield return E.Break;
    }

    ////////////////////////////////////////////////////////////////////////////////////
    // Prop Interactions — clicking a placed prop returns it to inventory
    // Only works if puzzle is not yet solved
    // Note: props need colliders enabled in the Unity editor to be clickable

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
        yield return C.player_invis.Say("I picked the feather back up.");

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
        Hotspot("Feather").Enable(); // re-enable the shared feather slot
        I.SecretDoll.AddAsActive();
        yield return C.player_invis.Say("I picked the doll back up.");

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
        yield return C.player_invis.Say("I picked the teddy back up.");

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
        yield return C.player_invis.Say("I picked the pin back up.");

        yield return E.Break;
    }

    ////////////////////////////////////////////////////////////////////////////////////
    // Puzzle Completion — called after every successful placement

    IEnumerator CheckPuzzleComplete()
    {
        if (ItemsPlaced.SecretSolution)
        {
            yield return C.player_invis.Say("Something opened... somewhere else.");
            Audio.Play("DoorOpen");
        }
        else if (ItemsPlaced.AllItemsPlaced)
        {
            yield return C.player_invis.Say("Something opened!");
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