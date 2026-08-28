using UnityEngine;
using TMPro;
using System.Collections;

public class BedroomIntro : MonoBehaviour
{
    public Movement playerMovement;
    public PlayerInteraction playerInteraction;

    public CanvasGroup fadeScreen;

    public TMP_Text monologueText;
    public TMP_Text objectiveText;

    // Parent containing both the OBJECTIVE label
    // and the changing objective text
    public GameObject objectiveUI;

    public float fadeDuration = 1.5f;
    public float monologueDuration = 2.5f;

    void Start()
    {
        // If we're returning from the Body for the ending,
        // don't play the intro again
        if (GameEndingState.returningFromBody)
            return;

        // Reset bedroom progression for a new playthrough
        BedroomProgress.mirrorChecked = false;

        StartCoroutine(IntroSequence());
    }

    IEnumerator IntroSequence()
    {
        // Lock player during intro
        playerMovement.enabled = false;
        playerInteraction.interactionLocked = true;

        // Start on black
        fadeScreen.alpha = 1f;

        // Hide intro text and the ENTIRE objective UI
        monologueText.gameObject.SetActive(false);
        objectiveUI.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        // Fade into bedroom
        yield return StartCoroutine(Fade(0f));

        yield return new WaitForSeconds(0.5f);

        // First thought
        monologueText.gameObject.SetActive(true);
        monologueText.text =
            "God... I feel so disgusting. I feel like I broke out again.";

        yield return new WaitForSeconds(monologueDuration);

        // Second thought
        monologueText.text =
            "Let me check in the mirror.";

        yield return new WaitForSeconds(monologueDuration);

        // Hide monologue
        monologueText.gameObject.SetActive(false);

        // Show objective UI
        objectiveUI.SetActive(true);
        objectiveText.text =
            "Look at yourself in the mirror";

        // Give control back to player
        playerMovement.enabled = true;
        playerInteraction.interactionLocked = false;
    }

    IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeScreen.alpha;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            fadeScreen.alpha = Mathf.Lerp(
                startAlpha,
                targetAlpha,
                time / fadeDuration
            );

            yield return null;
        }

        fadeScreen.alpha = targetAlpha;
    }
}