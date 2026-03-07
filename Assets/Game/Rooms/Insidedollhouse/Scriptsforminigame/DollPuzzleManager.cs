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
    public bool puzzleCompletedPersist = false;                     // remembers if puzzle done
    public Dictionary<int, GameObject> persistedDollSlots = new Dictionary<int, GameObject>();   // remembers doll positions

    private bool puzzleComplete = false;

    void Awake()
    {
        Instance = this;

        targetSlots = new Dictionary<GameObject, int>()
        {
            {slot1, 1},
            {slot5, 3}
        };

        // Restore puzzle completion
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
                    doll.SetLastSlot(slot); // Update lastSlot in drag script
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
        if (flapSprite != null)
            flapSprite.SetActive(false);

        if (dollsprite != null)
            StartCoroutine(hidedollafterdelay(1f));

        //Example calls from your original manager
        I.SecretDoll.AddAsActive();
        C.player_invis.Say("The Roof Opened!!");
        Debug.Log("Puzzle Complete!");
    }

    private IEnumerator hidedollafterdelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        dollsprite.SetActive(false);
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