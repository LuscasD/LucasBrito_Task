using UnityEngine;

public class StartGameManager : MonoBehaviour
{

   public InventoryManager _inventoryManager;
   public Inventory_UI inventoryUIManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _inventoryManager.gameObject.SetActive(false);
        _inventoryManager.LoadInventory();
        inventoryUIManager.UpdateUI();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
