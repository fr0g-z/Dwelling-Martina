using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class RoomEND : RoomScript<RoomEND>
{
    public void OnEnterRoom()
    {
        Audio.Stop("Gamesoundtrack");
        G.InventoryBar.Hide();
        


    }

}