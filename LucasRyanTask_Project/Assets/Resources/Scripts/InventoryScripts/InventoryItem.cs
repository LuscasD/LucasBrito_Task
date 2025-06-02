using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public bool isStackable;
    public int maxStack = 1;
    public string description;
    public UnityEvent onPickup;

    public GameObject modelPrefab;
    public bool isWeapon;


    public virtual void Use()
    {
        Debug.Log("Used: " + itemName);

        if (isWeapon)
        {
            EquipmentSlotUI.Instance.Equip(this);
        }
        // Extend this in child classes for specific behavior
    }
}
