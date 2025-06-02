using TMPro.Examples;
using UnityEngine;

public class NpcDialog : MonoBehaviour
{

    public GameObject inRangeFeedBack;
    private bool isPlayerInRange = false;

    public GameObject dialogBox;
    private bool isOpen = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
            dialogBox.SetActive(isOpen);
            inRangeFeedBack.SetActive(!isOpen);
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
            isOpen = false;
            dialogBox.SetActive(false);
            if (inRangeFeedBack != null)
                inRangeFeedBack.SetActive(false);
            isPlayerInRange = false;
        }
    }


}
