using UnityEngine;
using UnityEngine.UI;

public class Slot_UI : MonoBehaviour
{
    public Image icon;
    public Text quantityText;
    public Button useButton;

    private int index;
    private Inventory_UI inventoryUI => GetComponentInParent<Inventory_UI>();

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
