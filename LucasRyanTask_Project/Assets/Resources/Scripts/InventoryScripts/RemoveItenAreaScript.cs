using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class RemoveItenAreaScript : MonoBehaviour, IDropHandler

{
    public Inventory_UI inventoryUI;
    public CanvasGroup areaCanvaGroup;

    void Start()
    {
        areaCanvaGroup = GetComponentInParent<CanvasGroup>();
        areaCanvaGroup.DOFade(0, 0);
    }

        public void OnDrop(PointerEventData eventData)
    {
        var dragged = eventData.pointerDrag?.GetComponent<Slot_UI>();
        if (dragged == null) return;

        var slot = inventoryUI.inventory.slots[dragged.Index];

        if (!slot.IsEmpty)
        {
            slot.ClearSlot();
            inventoryUI.UpdateUI();
            Debug.Log("Item removido do inventário.");
        }
    }
}
