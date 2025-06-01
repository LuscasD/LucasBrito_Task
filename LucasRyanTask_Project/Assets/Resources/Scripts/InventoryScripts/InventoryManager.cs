using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public int maxSlots = 20;
    public List<SlotScipt> slots = new List<SlotScipt>();

    void Awake()
    {
        for (int i = 0; i < maxSlots; i++)
        {
            slots.Add(new SlotScipt());
        }
    }

    public bool AddItem(InventoryItem item, int amount = 1)
    {
        // Try stack
        if (item.isStackable)
        {
            foreach (var slot in slots)
            {
                if (slot.item == item && slot.quantity < item.maxStack)
                {
                    slot.AddItem(item, amount);
                    return true;
                }
            }
        }

        // Find empty slot
        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                slot.AddItem(item, amount);
                return true;
            }
        }

        Debug.Log("Inventory full!");
        return false;
    }

    public void RemoveItem(int index)
    {
        if (index >= 0 && index < slots.Count)
            slots[index].ClearSlot();
    }
}
