using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class Characterplayer_invis : CharacterScript<Characterplayer_invis>
{


	IEnumerator OnLookAt()
	{

		yield return E.Break;
	}

	IEnumerator OnUseInv( IInventory item )
	{

		yield return E.Break;
	}
}