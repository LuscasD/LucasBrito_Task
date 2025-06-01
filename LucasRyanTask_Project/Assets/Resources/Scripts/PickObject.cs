using UnityEngine;
using UnityEngine.Events;

public class PickObject : MonoBehaviour
{
    [Header("Interaction Settings")]
    public UnityEvent onPickup;
    public GameObject inRangeFeedBack;

    private bool isPlayerInRange = false;

    public InventoryItem item;
    public InventoryManager inventory;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            inventory.AddItem(item);
            Debug.Log("Item added: " + item.itemName);
            onPickup.Invoke();
            Destroy(gameObject);

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (inRangeFeedBack != null)
                inRangeFeedBack.SetActive(true);
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (inRangeFeedBack != null)
                inRangeFeedBack.SetActive(false);
            isPlayerInRange = false;
        }
    }
}
