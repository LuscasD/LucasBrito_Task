using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class EquipmentSlotUI : MonoBehaviour
{
    public Image icon;
    private InventoryItem equippedItem;

    public void Equip(InventoryItem item)
    {
        equippedItem = item;
        icon.sprite = item.icon;
        icon.enabled = true;

        EquipmentManager.Instance.EquipWeapon(item);
    }

    public void Unequip()
    {
        equippedItem = null;
        icon.enabled = false;
        EquipmentManager.Instance.UnequipWeapon();
    }
}
