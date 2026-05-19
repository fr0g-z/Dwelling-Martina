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

    public GameObject slot1;
    public GameObject slot5;

    public GameObject flapSprite;
    public GameObject dollsprite;

    private Dictionary<int, GameObject> currentDollSlots = new Dictionary<int, GameObject>();
    public Dictionary<GameObject, int> targetSlots = new Dictionary<GameObject, int>();

    // --- PERSISTENCE ---
    public bool puzzleCompletedPersist = false;
    public Dictionary<int, GameObject> persistedDollSlots = new Dictionary<int, GameObject>();

    private bool puzzleComplete = false;

    void Awake()
    {
        Instance = this;

        targetSlots = new Dictionary<GameObject, int>()
        {
            { slot1, 1 },
            { slot5, 3 }
        };

        // Restore puzzle completion state on room re-entry
        if (puzzleCompletedPersist)
        {
            puzzleComplete = true;

            if (flapSprite != null)
                flapSprite.SetActive(false);

            if (dollsprite != null)
                dollsprite.SetActive(false);
        }

        // Restore doll positions
        foreach (var kv in persistedDollSlots)
        {
            int dollID = kv.Key;
            GameObject slot = kv.Value;

            if (slot != null)
            {
                DollDrag doll = FindDollByID(dollID);
                if (doll != null)
                {
                    doll.transform.position = slot.transform.position;
                    currentDollSlots[dollID] = slot;
                    doll.SetLastSlot(slot);
                }
            }
        }
    }

    public bool IsPuzzleComplete() => puzzleComplete;

    public void UpdateDollSlot(int dollID, GameObject slot)
    {
        if (slot == null)
        {
            if (currentDollSlots.ContainsKey(dollID))
                currentDollSlots.Remove(dollID);

            persistedDollSlots[dollID] = null;
        }
        else
        {
            currentDollSlots[dollID] = slot;
            persistedDollSlots[dollID] = slot;
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
            puzzleCompletedPersist = true;
            PuzzleComplete();
        }
    }

    private void PuzzleComplete()
    {
        // Hide the flap so the reward is visible
        if (flapSprite != null)
            flapSprite.SetActive(false);

        if (dollsprite != null)
            StartCoroutine(HideDollAfterDelay(4f));

        // Set the flag first so the coroutine below sees it as true
        DollHouseDone.DollhouseDone = true;

        Debug.Log("Puzzle Complete — running dialogue and item grant.");

        // Run dialogue and item grant as a coroutine on this MonoBehaviour
        StartCoroutine(PuzzleCompleteDialogue());
    }

    private IEnumerator HideDollAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (dollsprite != null)
            dollsprite.SetActive(false);
    }

    private IEnumerator PuzzleCompleteDialogue()
    {
        // Small pause so the flap hide animation settles
        yield return new WaitForSeconds(0.5f);

        yield return C.player_invis.Say("That's how we were.");
        yield return C.player_invis.Say("Before everything changed.");

        yield return new WaitForSeconds(1.5f);

        yield return C.player_invis.Say("There's something hidden inside.");

        yield return new WaitForSeconds(1.5f);

        yield return C.player_invis.Say("A doll.");
        yield return C.player_invis.Say("I made this. I carved her face from wood and painted it to look like Mum.");

        yield return new WaitForSeconds(1.5f);

        yield return C.player_invis.Say("I made it so she'd always have someone.");
        yield return C.player_invis.Say("So she'd never be alone, even when I wasn't there.");

        yield return new WaitForSeconds(1.5f);

        yield return C.player_invis.Say("I didn't know then what 'not being there' would really mean.");

        I.SecretDoll.AddAsActive();
    }

    private DollDrag FindDollByID(int dollID)
    {
        foreach (DollDrag doll in FindObjectsOfType<DollDrag>())
        {
            if (doll.dollID == dollID)
                return doll;
        }
        return null;
    }
}