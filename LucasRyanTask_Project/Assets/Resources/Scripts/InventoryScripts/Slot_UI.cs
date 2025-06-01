using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot_UI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Image icon;
    public Text quantityText;
    public Button useButton;

    private int index;

    private Transform originalParent;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private Inventory_UI inventoryUI => GetComponentInParent<Inventory_UI>();

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var slot = inventoryUI.inventory.slots[index];
        if (slot.IsEmpty) return;

        originalParent = transform.parent;
        transform.SetParent(canvas.transform);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent);
        transform.localPosition = Vector3.zero;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        var dragged = eventData.pointerDrag?.GetComponent<Slot_UI>();
        if (dragged == null || dragged == this) return;

        SwapSlots(dragged.index, this.index);
        inventoryUI.UpdateUI();
    }

    private void SwapSlots(int from, int to)
    {
        var inventory = inventoryUI.inventory;
        var tempItem = inventory.slots[from].item;
        var tempQty = inventory.slots[from].quantity;

        inventory.slots[from].item = inventory.slots[to].item;
        inventory.slots[from].quantity = inventory.slots[to].quantity;

        inventory.slots[to].item = tempItem;
        inventory.slots[to].quantity = tempQty;
    }

    public void SetIndex(int i)
    {
        index = i;
        useButton.onClick.AddListener(UseItem);
    }

    public void UpdateSlot(SlotScipt slot)
    {
        if (slot.item != null)
        {
            icon.sprite = slot.item.icon;
            icon.enabled = true;
            quantityText.text = slot.item.isStackable ? slot.quantity.ToString() : "";
        }
        else
        {
            icon.enabled = false;
            quantityText.text = "";
        }
    }

    public void UseItem()
    {
        var slot = inventoryUI.inventory.slots[index];
        if (!slot.IsEmpty)
        {
            slot.item.Use();
            if (!slot.item.isStackable || --slot.quantity <= 0)
                slot.ClearSlot();

            inventoryUI.UpdateUI();
        }
    }
}
