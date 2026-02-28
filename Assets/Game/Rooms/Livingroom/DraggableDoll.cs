using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableDoll : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string dollID;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 startAnchoredPos;
    private Transform originalParent;

    private bool locked = false;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        startAnchoredPos = rectTransform.anchoredPosition;
        originalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (locked) return;

        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (locked) return;

        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (locked) return;

        canvasGroup.blocksRaycasts = true;

        if (transform.parent == originalParent)
        {
            rectTransform.anchoredPosition = startAnchoredPos;
        }
    }

    public void SnapToSlot(Transform slot)
    {
        transform.SetParent(slot);
        rectTransform.anchoredPosition = Vector2.zero;

        locked = true;

        DollhouseManager.Instance.RegisterPlacement(this, slot.name);
    }
}