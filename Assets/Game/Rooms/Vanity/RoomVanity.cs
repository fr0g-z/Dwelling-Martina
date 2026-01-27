using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomVanity : RoomScript<RoomVanity>
{

    // Feather hotspot
    IEnumerator OnUseInvHotspotFeather(IHotspot hotspot, IInventory item)
    {
        if (item.ScriptName == "Feather" && !ItemsPlaced.FeatherPlaced)
        {       
            item.Remove(); 
            ItemsPlaced.FeatherPlaced = true;
            Prop("Feather").Show();
            C.player_invis.Say("Feather placed!");
        }
        else
        {
            C.player_invis.Say("Wrong item for Feather hotspot!");
        }
        if (ItemsPlaced.AllItemsPlaced)
        {
            C.player_invis.Say("Something Clicked!");
            Audio.Play("DoorOpen");

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
            Prop("Teddybear").Show();
            C.player_invis.Say("TeddyBear placed!");
        }
        else
        {
            C.player_invis.Say("Wrong item for TeddyBear hotspot!");
        }

        if (ItemsPlaced.AllItemsPlaced)
        {
            C.player_invis.Say("Something Clicked");
            Audio.Play("DoorOpen");
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
            Prop("MumsPin").Show();
            C.player_invis.Say("Pin placed!");
        }
        else
        {
            C.player_invis.Say("Wrong item for Pin hotspot!");
        }

        if (ItemsPlaced.AllItemsPlaced)
        {
            C.player_invis.Say("Something Clicked!");
            Audio.Play("DoorOpen");
        }

        yield return E.Break;
    }

	IEnumerator OnInteractHotspotHallway( IHotspot hotspot )
	{
        yield return C.Plr.ChangeRoom(R.Hallway);
        yield return E.Break;
	}

    // The below is just to declare the props so no errors occur

    IEnumerator OnInteractPropTeddybear( IProp prop )
	{

		yield return E.Break;
	}

	IEnumerator OnInteractPropMumsPin( IProp prop )
	{

		yield return E.Break;
	}

	IEnumerator OnInteractPropFeather( IProp prop )
	{

		yield return E.Break;
	}
}