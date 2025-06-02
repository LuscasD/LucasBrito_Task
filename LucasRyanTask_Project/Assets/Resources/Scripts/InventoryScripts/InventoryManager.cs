using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class InventoryManager : MonoBehaviour
{
    public int maxSlots = 20;
    public List<SlotScipt> slots = new List<SlotScipt>();
    public List<InventoryItem> allItems; // Database of possible items
    public Inventory_UI uiManager;

    private const string SaveKey = "InventorySave";

    void Awake()
    {
        for (int i = 0; i < maxSlots; i++)
            slots.Add(new SlotScipt());

        LoadInventory();
    }

    public void DebugInventory()
    {
        Debug.Log("Salvando usando PlayerPrefs com a chave: " + SaveKey);
    }

    public bool AddItem(InventoryItem item, int amount = 1)
    {
        SaveInventory();
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

        // Salvar o nome do item equipado, se houver
        if (EquipmentManager.Instance.equippedItem != null)
        {
            data.equippedWeaponName = EquipmentManager.Instance.equippedItem.name;
        }

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("InventorySave", json);
        PlayerPrefs.Save();
        Debug.Log("Inventário salvo nos PlayerPrefs.");
    }

    public void LoadInventory()
    {
        if (!PlayerPrefs.HasKey("InventorySave"))
        {
            Debug.Log("Nenhum save encontrado nos PlayerPrefs.");
            return;
        }

        string json = PlayerPrefs.GetString("InventorySave");
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
                    Debug.LogWarning($"Item '{sData.itemName}' não encontrado na database.");
            }
        }

        // Restaurar o item equipado (caso tenha sido salvo)
        if (!string.IsNullOrEmpty(data.equippedWeaponName))
        {
            InventoryItem equipped = allItems.Find(x => x.name == data.equippedWeaponName);
            if (equipped != null)
            {
                EquipmentSlotUI.Instance.Equip(equipped);
            }
        }

        Debug.Log("Inventário carregado dos PlayerPrefs.");
    }

    public void DeleteSave()
    {
        if (PlayerPrefs.HasKey(SaveKey))
        {
            PlayerPrefs.DeleteKey(SaveKey);
            Debug.Log("Save do inventário apagado.");
        }
    }
}

[System.Serializable]
public class InventorySaveData
{
    public List<SlotData> slots = new List<SlotData>();
    public string equippedWeaponName;
}

[System.Serializable]
public class SlotData
{
    public string itemName;
    public int quantity;
}