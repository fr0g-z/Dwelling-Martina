using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomBADending : RoomScript<RoomBADending>
{
    void OnEnterRoom()
    {
        Audio.Stop("mombreathing");
    }
    IEnumerator OnEnterRoomAfterFade()
    {
        // Hide inventory bar before animation
        E.GetGui("InventoryBar").Hide();

        yield return E.Break;
    }
}