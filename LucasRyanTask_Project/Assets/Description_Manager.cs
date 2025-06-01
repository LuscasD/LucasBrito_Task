using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Description_Manager : MonoBehaviour
{

    InventoryItem currentItem;
    public Image _icon;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    public void SetDescription(InventoryItem item)
    {
        if (item == null)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);
        currentItem = item;
        _icon.enabled = true;
        _icon.sprite = item.icon;
        nameText.text = item.itemName;
        descriptionText.text = item.description;
    }

}
