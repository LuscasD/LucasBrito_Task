using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public bool isStackable;
    public int maxStack = 1;

    public virtual void Use()
    {
        Debug.Log("Used: " + itemName);
        // Extend this in child classes for specific behavior
    }
}
