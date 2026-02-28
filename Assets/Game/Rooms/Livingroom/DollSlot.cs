using UnityEngine;
using UnityEngine.EventSystems;

public class DollSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        DraggableDoll doll = eventData.pointerDrag.GetComponent<DraggableDoll>();

        if (doll != null)
        {
            doll.SnapToSlot(transform);
        }
    }
}