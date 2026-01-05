using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class InventoryFeather : InventoryScript<InventoryFeather>
{


	IEnumerator OnLookAtInventory( IInventory thisItem )
	{
		yield return C.Display("It's a feather");
		yield return E.Break;
	}

	IEnumerator OnUseInvInventory( IInventory thisItem, IInventory item )
	{
		
		yield return E.Break;
	}
}