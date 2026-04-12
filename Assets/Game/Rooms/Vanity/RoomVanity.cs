using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomVanity : RoomScript<RoomVanity>
{
    IEnumerator OnEnterRoomAfterFade()
    {
       
            yield return C.player_invis.Say("looks like i need three items to put here...but what?");
       

        yield return E.Break;
    }

    // Feather hotspot
    IEnumerator OnUseInvHotspotFeather(IHotspot hotspot, IInventory item)
    {       
        if (item.ScriptName == "SecretDoll" && !ItemsPlaced.SecretDollPlaced)
        {
            item.Remove();
            ItemsPlaced.SecretDollPlaced = true;

            Prop("SecretDoll").Show();
            C.player_invis.Say("Future");
           
            if (ItemsPlaced.SecretSolution)
            {
                C.player_invis.Say("Something opened... somewhere else.");
                Audio.Play("DoorOpen");
                Hotspot("Feather").Disable();

               
            }

            yield return E.Break;
            yield break;
        }

        if (item.ScriptName == "Feather" && !ItemsPlaced.FeatherPlaced)
        {
            item.Remove();
            ItemsPlaced.FeatherPlaced = true;
            Prop("Feather").Show();
            C.player_invis.Say("Future");
            Hotspot("Feather").Disable();
        }
        else
        {
            C.player_invis.Say("Wrong item!");
        }

        if (ItemsPlaced.AllItemsPlaced)
        {
            C.player_invis.Say("Something Opened!");
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
            C.player_invis.Say("Past");
        }
        else
        {
            C.player_invis.Say("Wrong item!");
        }

        if (ItemsPlaced.AllItemsPlaced)
        {
            C.player_invis.Say("Something Opened!");
            Audio.Play("DoorOpen");
        }

        if (ItemsPlaced.SecretSolution)
        {
            C.player_invis.Say("Something opened... somewhere else.");
            Audio.Play("DoorOpen");
            Hotspot("Feather").Disable();


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
            C.player_invis.Say("Present");
        }
        else
        {
            C.player_invis.Say("Wrong item!");
        }

        if (ItemsPlaced.AllItemsPlaced)
        {
            C.player_invis.Say("Something Opened!");
            Audio.Play("DoorOpen");
        }

        if (ItemsPlaced.SecretSolution)
        {
            C.player_invis.Say("Something opened... somewhere else.");
            Audio.Play("DoorOpen");
            Hotspot("Feather").Disable();


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

	IEnumerator OnInteractHotspotNote( IHotspot hotspot )
	{
        G.VanityNote.Visible = true;
		yield return E.Break;
	}

	IEnumerator OnInteractHotspotFeather( IHotspot hotspot )
	{

		yield return E.Break;
	}
}