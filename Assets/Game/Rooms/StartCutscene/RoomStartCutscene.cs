using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class RoomStartCutscene : RoomScript<RoomStartCutscene>
{

    VideoPlayer video;
    
    IEnumerator OnEnterRoomAfterFade()
	{
        Cursor.Visible = true;
        video = GameObject.Find("Intro").GetComponent<VideoPlayer>();
        video.Play();
       
        yield return E.Break;

    }
    
    IEnumerator OnInteractHotspotContinue( IHotspot hotspot )
	{
        yield return E.ChangeRoom(R.Bedroom);
        yield return E.Break;
	}

	
}