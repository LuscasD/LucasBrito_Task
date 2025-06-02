using UnityEngine;
using UnityEngine.UI;

public class DragItemIconScript : MonoBehaviour
{
    public static DragItemIconScript Instance;

    public Image iconImage;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        Instance = this;
        canvasGroup = GetComponent<CanvasGroup>();
        Hide();
    }

    public void Show(Sprite sprite)
    {
        iconImage.sprite = sprite;
        iconImage.enabled = true;
        canvasGroup.alpha = 1f;
    }

    public void Hide()
    {
        iconImage.sprite = null;
        iconImage.enabled = false;
        canvasGroup.alpha = 0f;
    }

    void Update()
    {
        if (canvasGroup.alpha > 0f)
            transform.position = Input.mousePosition;
    }
}
