using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUI;
    private bool isOpen = false;
    [SerializeField] private GameObject inventoryIcon;
    [SerializeField] private CanvasGroup inventoryCanvaGroup;

    private void Start()
    {
        inventoryCanvaGroup.DOFade(0, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            StartCoroutine(animationOpenInventory());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMenu();
        }

        IEnumerator animationOpenInventory()
        {
            isOpen = !isOpen;

            if (isOpen)
            {
                inventoryUI.SetActive(isOpen);
                inventoryIcon.SetActive(!isOpen);
                Cursor.lockState = CursorLockMode.None;
                inventoryCanvaGroup.DOFade(1, 0.7f);


            }
            else if (!isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                inventoryCanvaGroup.DOFade(0, 0.7f);
                yield return new WaitForSeconds(0.7f);
                inventoryIcon.SetActive(!isOpen);
                inventoryUI.SetActive(isOpen);
            }
        }

    }

    public void BackToMenu()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
        SceneManager.LoadScene(nextSceneIndex);
    }

}
