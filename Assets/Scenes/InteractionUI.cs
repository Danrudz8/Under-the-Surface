using UnityEngine;
using TMPro;

public class InteractionUI : MonoBehaviour
{
    public GameObject interactionText;

    public void ShowInteraction(string message)
    {
        interactionText.SetActive(true);

        interactionText.GetComponent<TMP_Text>().text = message;
    }

    public void HideInteraction()
    {
        interactionText.SetActive(false);
    }
}
