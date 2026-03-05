using UnityEngine;
using System.Collections.Generic;

public class DollhouseManager : MonoBehaviour
{
    public static DollhouseManager Instance;

    public GameObject latch;
    public AudioSource placeSound;
    public bool puzzleCompleted = false;

    private Dictionary<string, string> placements = new Dictionary<string, string>();

    void Awake()
    {
        Instance = this;
    }

    public void RegisterPlacement(DraggableDoll doll, string slotName)
    {
        placements[doll.dollID] = slotName;

        if (placeSound != null)
            placeSound.Play();

        CheckPuzzle();
    }

    void CheckPuzzle()
    {
        if (puzzleCompleted) return;

        bool correct1 =
            placements.ContainsKey("DollA") &&
            placements["DollA"] == "Slot2";

        bool correct2 =
            placements.ContainsKey("DollC") &&
            placements["DollC"] == "Slot6";

        if (correct1 && correct2)
        {
            puzzleCompleted = true;
            latch.SetActive(true);

            Debug.Log("Puzzle Complete");
        }
    }

}