using UnityEngine;

public class DollDrag : MonoBehaviour
{
    public int dollID;
    public float snapDistance = 3f;

    private bool dragging = false;
    private Vector3 offset;
    private Camera cam;

    private GameObject lastSlot = null;
    private Vector3 lastPosition;

    void Start()
    {
        cam = Camera.main;
        lastPosition = transform.position;
    }

    // Called by manager to restore position on room load
    public void SetLastSlot(GameObject slot)
    {
        lastSlot = slot;
        lastPosition = slot.transform.position;
    }

    void OnMouseDown()
    {
        if (DollPuzzleManager.Instance != null && DollPuzzleManager.Instance.IsPuzzleComplete())
            return;

        dragging = true;

        Vector3 mouse = cam.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;
        offset = transform.position - mouse;

        DollPuzzleManager.Instance.UpdateDollSlot(dollID, null);
    }

    void Update()
    {
        if (!dragging) return;

        if (DollPuzzleManager.Instance.IsPuzzleComplete())
        {
            dragging = false;
            return;
        }

        Vector3 mouse = cam.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;
        transform.position = mouse + offset;

        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
            SnapToSlot();
        }
    }

    void SnapToSlot()
    {
        GameObject[] slots = GameObject.FindGameObjectsWithTag("Slot");

        GameObject closest = null;
        float minDist = Mathf.Infinity;

        foreach (GameObject slot in slots)
        {
            float dist = Vector3.Distance(transform.position, slot.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = slot;
            }
        }

        if (closest != null && minDist <= snapDistance)
        {
            // Prevent stacking
            DollDrag[] dolls = FindObjectsOfType<DollDrag>();
            foreach (DollDrag doll in dolls)
            {
                if (doll != this && doll.lastSlot == closest)
                {
                    // Slot occupied → return to previous slot
                    transform.position = lastPosition;
                    DollPuzzleManager.Instance.UpdateDollSlot(dollID, lastSlot);
                    return;
                }
            }

            // Snap to free slot
            transform.position = closest.transform.position;
            lastSlot = closest;
            lastPosition = transform.position;
            DollPuzzleManager.Instance.UpdateDollSlot(dollID, closest);
        }
        else
        {
            // Return to previous slot
            transform.position = lastPosition;
            DollPuzzleManager.Instance.UpdateDollSlot(dollID, lastSlot);
        }
    }
}