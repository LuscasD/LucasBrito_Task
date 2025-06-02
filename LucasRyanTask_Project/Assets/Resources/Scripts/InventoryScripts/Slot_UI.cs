using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot_UI : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler,
    IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    public Text quantityText;
    public Button useButton;

    public int Index { get; private set; }

    private Transform originalParent;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    public Description_Manager descriptionManager;

    public enum SlotType { Inventory, WeaponSlot }
    public SlotType slotType = SlotType.Inventory;

    private Inventory_UI inventoryUI => GetComponentInParent<Inventory_UI>();

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
        descriptionManager = FindObjectOfType<Description_Manager>(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var slot = inventoryUI.inventory.slots[Index];
        if (!slot.IsEmpty)
        {
            descriptionManager.SetDescription(slot.item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionManager.SetDescription(null);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var slot = inventoryUI.inventory.slots[Index];
        if (slot.IsEmpty || slot.item == null) return;

        DragItemIconScript.Instance.Show(slot.item.icon);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
       /* var slot = inventoryUI.inventory.slots[Index];
        if (slot.IsEmpty || slot.item == null) return;

        transform.position = eventData.position;*/
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragItemIconScript.Instance.Hide();
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        var dragged = eventData.pointerDrag?.GetComponent<Slot_UI>();
        if (dragged == null || dragged == this) return;

        var draggedSlot = inventoryUI.inventory.slots[dragged.Index];
        if (draggedSlot.IsEmpty || draggedSlot.item == null) return;

        if (slotType == SlotType.WeaponSlot && draggedSlot.item.isWeapon)
        {
            EquipmentManager.Instance.EquipWeapon(draggedSlot.item);
            draggedSlot.ClearSlot();
        }
        else if (slotType == SlotType.Inventory && dragged.slotType == SlotType.Inventory)
        {
            SwapSlots(dragged.Index, this.Index);
        }

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
        Index = i;
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
        var slot = inventoryUI.inventory.slots[Index];
        if (!slot.IsEmpty)
        {
            slot.item.Use();
            if (!slot.item.isStackable || --slot.quantity <= 0)
                slot.ClearSlot();

            inventoryUI.UpdateUI();
        }
    }
}
