using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomVanity : RoomScript<RoomVanity>
{


    IEnumerator OnUseInvHotspotFeather(IHotspot hotspot, IInventory item)
    {
        if (item.ScriptName == "Feather" && !ItemsPlaced.FeatherPlaced)
        {
            // Remove item from inventory
            item.Remove();

            // Set global flag
            ItemsPlaced.FeatherPlaced = true;

            C.player_invis.Say("Feather placed!");
        }
        else
        {
            C.player_invis.Say("Wrong item for Feather hotspot!");
        }

        // Check if all items are placed
        if (ItemsPlaced.AllItemsPlaced)
        {
            C.player_invis.Say("All items placed! Trigger global event!");
            // TODO: trigger your global event here (door, cutscene, etc.)
        }

        yield return E.Break;
    }

    // TeddyBear hotspot
    IEnumerator OnUseInvHotspotTeddyBear(IHotspot hotspot, IInventory item)
    {
        if (item.ScriptName == "TeddyBear" && !ItemsPlaced.TeddyPlaced)
        {
            item.Remove();
            ItemsPlaced.TeddyPlaced = true;
            C.player_invis.Say("TeddyBear placed!");
        }
        else
        {
            C.player_invis.Say("Wrong item for TeddyBear hotspot!");
        }

        if (ItemsPlaced.AllItemsPlaced)
        {
            C.player_invis.Say("All items placed! Trigger global event!");
        }

        yield return E.Break;
    }

    // Pin hotspot
    IEnumerator OnUseInvHotspotPin(IHotspot hotspot, IInventory item)
    {
        if (item.ScriptName == "MumsPin" && !ItemsPlaced.PinPlaced)
        {
            item.Remove();
            ItemsPlaced.PinPlaced = true;
            C.player_invis.Say("Pin placed!");
        }
        else
        {
            C.player_invis.Say("Wrong item for Pin hotspot!");
        }

        if (ItemsPlaced.AllItemsPlaced)
        {
            C.player_invis.Say("All items placed! Trigger global event!");
        }

        yield return E.Break;
    }
}