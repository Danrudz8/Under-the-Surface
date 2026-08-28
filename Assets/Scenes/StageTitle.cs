using UnityEngine;
using TMPro;
using System.Collections;

public class StageTitle : MonoBehaviour
{
    public TMP_Text stageTitleText;

    public Movement playerMovement;
    public PlayerInteraction playerInteraction;

    public CameraBob cameraBob;
    public CharacterBob characterBob;

    public string stageName;

    public float fadeInDuration = 0.6f;
    public float displayTime = 1.5f;
    public float fadeOutDuration = 0.6f;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered)
            return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartCoroutine(ShowTitle());
        }
    }

    IEnumerator ShowTitle()
    {
        // Lock player controls
        playerMovement.enabled = false;
        playerInteraction.interactionLocked = true;

        // Disable camera and character bob
        if (cameraBob != null)
        {
            cameraBob.enabled = false;
        }

        if (characterBob != null)
        {
            characterBob.enabled = false;
        }

        // Set the stage title
        stageTitleText.text = stageName;
        stageTitleText.gameObject.SetActive(true);

        // Start completely transparent
        Color color = stageTitleText.color;
        color.a = 0f;
        stageTitleText.color = color;

        // --------------------
        // FADE IN
        // --------------------

        float time = 0f;

        while (time < fadeInDuration)
        {
            time += Time.deltaTime;

            color.a = Mathf.Lerp(
                0f,
                1f,
                time / fadeInDuration
            );

            stageTitleText.color = color;

            yield return null;
        }

        color.a = 1f;
        stageTitleText.color = color;

        // --------------------
        // HOLD
        // --------------------

        yield return new WaitForSeconds(displayTime);

        // --------------------
        // FADE OUT
        // --------------------

        time = 0f;

        while (time < fadeOutDuration)
        {
            time += Time.deltaTime;

            color.a = Mathf.Lerp(
                1f,
                0f,
                time / fadeOutDuration
            );

            stageTitleText.color = color;

            yield return null;
        }

        color.a = 0f;
        stageTitleText.color = color;

        stageTitleText.gameObject.SetActive(false);

        // Re-enable camera and character bob
        if (cameraBob != null)
        {
            cameraBob.enabled = true;
        }

        if (characterBob != null)
        {
            characterBob.enabled = true;
        }

        // Give player control back
        playerMovement.enabled = true;
        playerInteraction.interactionLocked = false;
    }
}