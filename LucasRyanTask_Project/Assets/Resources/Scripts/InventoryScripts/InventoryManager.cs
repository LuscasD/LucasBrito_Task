using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class InventoryManager : MonoBehaviour
{
    public int maxSlots = 20;
    public List<SlotScipt> slots = new List<SlotScipt>();
    public List<InventoryItem> allItems; // Database of possible items
    public Inventory_UI uiManager;

    private string SavePath => Path.Combine(Application.persistentDataPath, "inventory_save.json");

    void Awake()
    {
        for (int i = 0; i < maxSlots; i++)
            slots.Add(new SlotScipt());

        LoadInventory();
    }

   public void DebugInventory()
    {
        Debug.Log("Save file path: " + SavePath);
    }

    public bool AddItem(InventoryItem item, int amount = 1)
    {
        if (item.isStackable)
        {
            foreach (var slot in slots)
            {
                if (slot.item == item && slot.quantity < item.maxStack)
                {
                    slot.AddItem(item, amount);
                    uiManager.UpdateUI();
                    return true;
                }
            }
        }

        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                slot.AddItem(item, amount);
                uiManager.UpdateUI();
                return true;
            }
        }

        Debug.Log("Inventory is full.");
        return false;
    }

    public void RemoveItem(int index)
    {
        if (index >= 0 && index < slots.Count)
            slots[index].ClearSlot();
    }

    public void SaveInventory()
    {
        InventorySaveData data = new InventorySaveData();

        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                data.slots.Add(new SlotData { itemName = "", quantity = 0 });
            }
            else
            {
                data.slots.Add(new SlotData
                {
                    itemName = slot.item.name,
                    quantity = slot.quantity
                });
            }
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
        Debug.Log($"Inventory saved to: {SavePath}");
    }

    public void LoadInventory()
    {
        if (!File.Exists(SavePath))
        {
            Debug.Log("No inventory save file found.");
            return;
        }

        string json = File.ReadAllText(SavePath);
        InventorySaveData data = JsonUtility.FromJson<InventorySaveData>(json);

        for (int i = 0; i < slots.Count && i < data.slots.Count; i++)
        {
            var sData = data.slots[i];

            if (string.IsNullOrEmpty(sData.itemName))
            {
                slots[i].ClearSlot();
            }
            else
            {
                InventoryItem item = allItems.Find(x => x.name == sData.itemName);
                if (item != null)
                    slots[i].AddItem(item, sData.quantity);
                else
                    Debug.LogWarning($"Item '{sData.itemName}' not found in item database.");
            }
        }

        Debug.Log("Inventory loaded from file.");
    }

    public void DeleteSave()
    {
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
            Debug.Log("Inventory save deleted.");
        }
    }
}

public class InventorySaveData
{
    public List<SlotData> slots = new List<SlotData>();
}

public class SlotData
{
    public string itemName;
    public int quantity;
}
