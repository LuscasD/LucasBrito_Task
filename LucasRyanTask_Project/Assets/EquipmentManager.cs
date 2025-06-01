using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance;

    public Transform weaponHandTransform;
    private GameObject currentWeaponModel;

    private InventoryItem equippedWeapon;

    void Awake()
    {
        Instance = this;
    }

    public void EquipWeapon(InventoryItem weapon)
    {
        if (currentWeaponModel != null)
            Destroy(currentWeaponModel);

        equippedWeapon = weapon;

        if (weapon.modelPrefab != null)
        {
            currentWeaponModel = Instantiate(weapon.modelPrefab, weaponHandTransform);
            currentWeaponModel.transform.localPosition = Vector3.zero;
            currentWeaponModel.transform.localRotation = Quaternion.identity;
        }
    }

    public void UnequipWeapon()
    {
        if (currentWeaponModel != null)
            Destroy(currentWeaponModel);

        equippedWeapon = null;
    }
}
