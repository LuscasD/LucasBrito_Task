using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    public InventoryManager inventory;
    public GameObject slotPrefab;
    public Transform slotParent;

    private Slot_UI[] slotUIs;

    public static Inventory_UI Instance;

    void Awake()
    {
        Instance = this;

        slotUIs = new Slot_UI[inventory.maxSlots];

        for (int i = 0; i < inventory.maxSlots; i++)
        {
            GameObject slotGO = Instantiate(slotPrefab, slotParent);
            slotUIs[i] = slotGO.GetComponent<Slot_UI>();
            slotUIs[i].SetIndex(i);
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            slotUIs[i].UpdateSlot(inventory.slots[i]);
        }
    }
}
