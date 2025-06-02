using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class EquipmentSlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    private InventoryItem equippedItem;
    private Transform originalParent;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    public static EquipmentSlotUI Instance;

    private Description_Manager descriptionManager;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        descriptionManager = FindObjectOfType<Description_Manager>(true);
    }

    public void Equip(InventoryItem item)
    {
        Unequip();
        equippedItem = item;
        icon.sprite = item.icon;
        icon.enabled = true;
        EquipmentManager.Instance.EquipWeapon(item);
    }

    public void Unequip()
    {
        if (equippedItem == null) return;

        bool added = Inventory_UI.Instance.inventory.AddItem(equippedItem);
        if (added)
        {
            EquipmentManager.Instance.UnequipWeapon();
            equippedItem = null;
            icon.enabled = false;
        }
        else
        {
            Debug.Log("Inventário cheio. Não foi possível desequipar.");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (equippedItem != null)
        {
            descriptionManager.SetDescription(equippedItem);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionManager.SetDescription(null);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && equippedItem != null)
        {
            Unequip();
        }
    }
}
