using UnityEngine;

public class DollDrag : MonoBehaviour
{
    public int dollID;
    private Vector3 offset;
    private Vector3 startPos;
    public float snapDistance = 3f;
    private bool dragging = false;

    void Start()
    {
        startPos = transform.position;
    }

    void OnMouseDown()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;
        offset = transform.position - mouse;
        dragging = true;
    }

    void OnMouseDrag()
    {
        if (!dragging) return;
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;
        transform.position = mouse + offset;
    }

    void OnMouseUp()
    {
        dragging = false;
        SnapToSlot();
    }

    void SnapToSlot()
    {
        GameObject[] slots = GameObject.FindGameObjectsWithTag("Slot");
        GameObject closest = null;
        float minDist = float.MaxValue;
        Vector3 dollPos = transform.position;

        foreach (GameObject slot in slots)
        {
            float dist = Vector3.Distance(dollPos, slot.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = slot;
            }
        }

        if (closest != null && minDist <= snapDistance)
        {
            transform.position = closest.transform.position;
            // Pass the actual slot GameObject to the manager
            DollPuzzleManager.Instance.UpdateDollSlot(dollID, closest);
        }
        else
        {
            transform.position = startPos;
            DollPuzzleManager.Instance.UpdateDollSlot(dollID, null);
        }
    }
}