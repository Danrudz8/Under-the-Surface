using UnityEngine;
using TMPro;

public class HudController : MonoBehaviour
{
    public static HudController instance;

    [SerializeField] TMP_Text interactionText;

    private void Awake()
    {
        instance = this;
        DisableInteractionText();
    }

    public void EnableInteractionText(string text)
    {
        interactionText.text = "[E] " + text;
        interactionText.gameObject.SetActive(true);
    }

    public void DisableInteractionText()
    {
        interactionText.gameObject.SetActive(false);
    }
}
