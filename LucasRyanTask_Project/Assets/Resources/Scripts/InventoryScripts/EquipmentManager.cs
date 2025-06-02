using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance;

    public Transform weaponHolder; // remover o static
    private GameObject currentWeapon;

    void Awake()
    {
        Instance = this;
    }

    public void EquipWeapon(InventoryItem weaponItem)
    {
        if (currentWeapon != null)
            Destroy(currentWeapon);

        if (weaponItem.modelPrefab != null && weaponHolder != null)
        {
            currentWeapon = Instantiate(weaponItem.modelPrefab, weaponHolder);
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localRotation = Quaternion.identity;
        }
    }

    public void UnequipWeapon()
    {
        if (currentWeapon != null)
            Destroy(currentWeapon);
    }
}
