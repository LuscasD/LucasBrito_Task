using UnityEngine;

[System.Serializable]
public class SlotScipt : MonoBehaviour
{
    public InventoryItem item;
    public int quantity;

    public bool IsEmpty => item == null;

    public void AddItem(InventoryItem newItem, int amount)
    {
        if (item == newItem && item.isStackable)
        {
            quantity += amount;
        }
        else
        {
            item = newItem;
            quantity = amount;
        }
    }

    public void ClearSlot()
    {
        item = null;
        quantity = 0;
    }
}

