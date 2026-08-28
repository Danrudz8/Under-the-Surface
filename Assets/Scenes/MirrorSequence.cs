using UnityEngine;
using TMPro;
using System.Collections;

public class MirrorSequence : MonoBehaviour
{
    public TMP_Text monologueText;
    public TMP_Text objectiveText;

    public MonoBehaviour playerMovement;
    public PlayerInteraction playerInteraction;

    public float textTime = 2f;

    public void StartMirrorSequence()
    {
        StartCoroutine(MirrorSequenceCoroutine());
    }

    IEnumerator MirrorSequenceCoroutine()
    {
        playerInteraction.interactionLocked = true;
        HudController.instance.DisableInteractionText();
        // Disable player movement
        playerMovement.enabled = false;

        // Show monologue
        monologueText.gameObject.SetActive(true);

        monologueText.text =
            "Oh god... my acne is really inflamed today...";

        yield return new WaitForSeconds(textTime);

        monologueText.text =
            "I should make smart food choices.";

        yield return new WaitForSeconds(textTime);
        BedroomProgress.mirrorChecked = true;

        // Change objective
        objectiveText.text = "Select your food wisely";

        // Hide monologue
        monologueText.gameObject.SetActive(false);

        // Enable player movement
        playerMovement.enabled = true;
        playerInteraction.interactionLocked = false;
    }
}
