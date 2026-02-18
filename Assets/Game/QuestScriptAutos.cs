using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PowerTools.Quest;

namespace PowerScript
{	
	// Shortcut access to SystemAudio.Get
	public class Audio : SystemAudio
	{
	}

	public static partial class C
	{
		// Access to specific characters (Auto-generated)
		public static ICharacter player_invis   { get { return PowerQuest.Get.GetCharacter("player_invis"); } }
		// #CHARS# - Do not edit this line, it's used by the system to insert characters
	}

	public static partial class I
	{		
		// Access to specific Inventory (Auto-generated)
		public static IInventory Bucket         { get { return PowerQuest.Get.GetInventory("Bucket"); } }
		public static IInventory Feather        { get { return PowerQuest.Get.GetInventory("Feather"); } }
		public static IInventory MumsPin        { get { return PowerQuest.Get.GetInventory("MumsPin"); } }
		public static IInventory TeddyBear      { get { return PowerQuest.Get.GetInventory("TeddyBear"); } }
		public static IInventory Hook           { get { return PowerQuest.Get.GetInventory("Hook"); } }
		// #INVENTORY# - Do not edit this line, it's used by the system to insert rooms for easy access
	}

	public static partial class G
	{
		// Access to specific gui (Auto-generated)
		public static IGui DialogTree     { get { return PowerQuest.Get.GetGui("DialogTree"); } }
		public static IGui SpeechBox      { get { return PowerQuest.Get.GetGui("SpeechBox"); } }
		public static IGui HoverText  { get { return PowerQuest.Get.GetGui("HoverText"); } }
		public static IGui DisplayBox    { get { return PowerQuest.Get.GetGui("DisplayBox"); } }
		public static IGui Prompt         { get { return PowerQuest.Get.GetGui("Prompt"); } }
		public static IGui Toolbar          { get { return PowerQuest.Get.GetGui("Toolbar"); } }
		public static IGui InventoryBar   { get { return PowerQuest.Get.GetGui("InventoryBar"); } }
		public static IGui Options        { get { return PowerQuest.Get.GetGui("Options"); } }
		public static IGui Save           { get { return PowerQuest.Get.GetGui("Save"); } }
		public static IGui CombLock       { get { return PowerQuest.Get.GetGui("CombLock"); } }
		public static IGui Deathdoc       { get { return PowerQuest.Get.GetGui("Deathdoc"); } }
		public static IGui Pills          { get { return PowerQuest.Get.GetGui("Pills"); } }
		// #GUI# - Do not edit this line, it's used by the system to insert rooms for easy access
	}

	public static partial class R
	{
		// Access to specific room (Auto-generated)
		public static IRoom Title          { get { return PowerQuest.Get.GetRoom("Title"); } }
		public static IRoom Bedroom        { get { return PowerQuest.Get.GetRoom("Bedroom"); } }
		public static IRoom Hallway        { get { return PowerQuest.Get.GetRoom("Hallway"); } }
		public static IRoom Vanity         { get { return PowerQuest.Get.GetRoom("Vanity"); } }
		public static IRoom Hallway_2      { get { return PowerQuest.Get.GetRoom("Hallway_2"); } }
		public static IRoom Mom_room       { get { return PowerQuest.Get.GetRoom("Mom_room"); } }
		public static IRoom Bathroom       { get { return PowerQuest.Get.GetRoom("Bathroom"); } }
		public static IRoom Kitchen        { get { return PowerQuest.Get.GetRoom("Kitchen"); } }
		public static IRoom Closet         { get { return PowerQuest.Get.GetRoom("Closet"); } }
		public static IRoom Livingroom     { get { return PowerQuest.Get.GetRoom("Livingroom"); } }
		public static IRoom Insidelockbox  { get { return PowerQuest.Get.GetRoom("Insidelockbox"); } }
		public static IRoom Clock          { get { return PowerQuest.Get.GetRoom("Clock"); } }
		public static IRoom END            { get { return PowerQuest.Get.GetRoom("END"); } }
		public static IRoom TextDump       { get { return PowerQuest.Get.GetRoom("TextDump"); } }
		public static IRoom Insidedollhouse { get { return PowerQuest.Get.GetRoom("Insidedollhouse"); } }
		public static IRoom Sink           { get { return PowerQuest.Get.GetRoom("Sink"); } }
		public static IRoom UnderTable     { get { return PowerQuest.Get.GetRoom("UnderTable"); } }
		public static IRoom Basement       { get { return PowerQuest.Get.GetRoom("Basement"); } }
		// #ROOM# - Do not edit this line, it's used by the system to insert rooms for easy access
	}

	// Dialog
	public static partial class D
	{
		// Access to specific dialog trees (Auto-generated)
		// #DIALOG# - Do not edit this line, it's used by the system to insert rooms for easy access	    	    
	}


}
