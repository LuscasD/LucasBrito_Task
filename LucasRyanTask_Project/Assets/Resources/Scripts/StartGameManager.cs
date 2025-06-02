using UnityEngine;

public class StartGameManager : MonoBehaviour
{

   public InventoryManager inventoryManager;
   public Inventory_UI inventoryUIManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryManager.gameObject.SetActive(false);
        inventoryUIManager.UpdateUI();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
