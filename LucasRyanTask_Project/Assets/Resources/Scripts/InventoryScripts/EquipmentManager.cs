using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance;

    public Transform weaponHolder; // remover o static
    private GameObject currentWeapon;
    public InventoryItem equippedItem;
    public InventoryManager _inventoryManager;

    void Awake()
    {
        Instance = this;
    }

    public void EquipWeapon(InventoryItem weaponItem)
    {
         equippedItem = weaponItem;
        if (currentWeapon != null)
            Destroy(currentWeapon);

        if (weaponItem.modelPrefab != null && weaponHolder != null)
        {
            currentWeapon = Instantiate(weaponItem.modelPrefab, weaponHolder);
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localRotation = Quaternion.identity;
        }
        _inventoryManager.SaveInventory();
    }

    public void UnequipWeapon()
    {
        equippedItem = null;
        if (currentWeapon != null)
            Destroy(currentWeapon);

        _inventoryManager.SaveInventory();
    }
}
