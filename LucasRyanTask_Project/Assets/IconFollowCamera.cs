using UnityEngine;

public class IconFollowCamera : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (mainCamera == null) return;

        // Faz o objeto olhar para a câmera (ajuste conforme necessário)
        transform.forward = mainCamera.transform.forward;
    }
}
