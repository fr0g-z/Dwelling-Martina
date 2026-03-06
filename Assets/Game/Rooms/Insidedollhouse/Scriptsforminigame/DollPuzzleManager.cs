using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using PowerTools.Quest;
using PowerScript;
using static GlobalScript;

public class DollPuzzleManager : MonoBehaviour
{
    public static DollPuzzleManager Instance;

    public int totalCorrectNeeded = 2;

    public GameObject slot1; // the slot where doll 1 needs to go
    public GameObject slot5; // the slot where doll 3 needs to go

    // NEW: reference to the flap sprite in Unity
    public GameObject flapSprite; // assign DollFlapSprite in the Inspector

    // Track which dolls are in which slots
    private Dictionary<int, GameObject> currentDollSlots = new Dictionary<int, GameObject>();

    // Target slots → target doll IDs
    public Dictionary<GameObject, int> targetSlots = new Dictionary<GameObject, int>();

    private bool puzzleComplete = false;

    void Awake()
    {
        Instance = this;
        // Map slots to the target doll IDs
        targetSlots = new Dictionary<GameObject, int>()
        {
            {slot1, 1}, // Doll 1 must be in slot2
            {slot5, 3}  // Doll 3 must be in slot6
        };
    }

    public void UpdateDollSlot(int dollID, GameObject slot)
    {
        if (slot == null)
        {
            if (currentDollSlots.ContainsKey(dollID))
                currentDollSlots.Remove(dollID);
        }
        else
        {
            currentDollSlots[dollID] = slot;
        }

        CheckPuzzleComplete();
    }

    private void CheckPuzzleComplete()
    {
        if (puzzleComplete) return;

        int correctCount = 0;
        foreach (var kv in targetSlots)
        {
            GameObject targetSlot = kv.Key;
            int targetDollID = kv.Value;

            if (currentDollSlots.ContainsKey(targetDollID) &&
                currentDollSlots[targetDollID] == targetSlot)
            {
                correctCount++;
            }
        }

        if (correctCount >= totalCorrectNeeded)
        {
            puzzleComplete = true;
            PuzzleComplete();
        }
    }

    private void PuzzleComplete()
    {
        // NEW: Hide the Unity flap sprite
        if (flapSprite != null)
            flapSprite.SetActive(false);

        C.player_invis.Say("The Latch Opened!!");
        Debug.Log("Puzzle Complete!");
    }
}