using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUI;
    private bool isOpen = false;
    [SerializeField] private GameObject inventoryIcon;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {

            isOpen = !isOpen;
            inventoryUI.SetActive(isOpen);
            inventoryIcon.SetActive(!isOpen);

            if (isOpen) 
            {
                Cursor.lockState = CursorLockMode.None;
                
            }
            else if (!isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
      


        }
    }
}
