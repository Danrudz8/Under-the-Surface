using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class BedroomEnding : MonoBehaviour
{
    public Movement playerMovement;
    public PlayerInteraction playerInteraction;

    public Transform player;
    public Transform endingPoint;

    public TMP_Text endingText;

    public GameObject objectiveUI;

    public CanvasGroup fadeScreen;

    // AUDIO
    public AudioSource bedroomAmbience;
    public AudioSource sfxSource;
    public AudioClip endThud;

    void Start()
    {
        if (GameEndingState.returningFromBody)
        {
            StartCoroutine(Ending());
        }
    }

    IEnumerator Ending()
    {
        // STOP BEDROOM AMBIENCE
        if (bedroomAmbience != null)
        {
            bedroomAmbience.Stop();
        }

        // Hide any old monologue text immediately
        endingText.gameObject.SetActive(false);

        // Lock player controls
        playerMovement.enabled = false;
        playerInteraction.interactionLocked = true;

        // Hide the entire objective UI
        objectiveUI.SetActive(false);

        CharacterController controller =
            player.GetComponent<CharacterController>();

        if (controller != null)
        {
            controller.enabled = false;
        }

        player.position = endingPoint.position;
        player.rotation = endingPoint.rotation;

        if (controller != null)
        {
            controller.enabled = true;
        }

        fadeScreen.alpha = 1f;

        yield return new WaitForSeconds(1f);

        // Fade back into bedroom
        yield return StartCoroutine(Fade(0f));

        // Silence here
        endingText.text = "Woah... that was weird... Maybe I should stop obsessing over my skin...";
        endingText.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        // Fade to black
        yield return StartCoroutine(Fade(1f));

        // THE END
        endingText.text = "THE END";

        // THUD
        if (sfxSource != null && endThud != null)
        {
            sfxSource.PlayOneShot(endThud);
        }

        yield return new WaitForSeconds(3f);

        GameEndingState.returningFromBody = false;

        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeScreen.alpha;
        float duration = 1f;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;

            fadeScreen.alpha = Mathf.Lerp(
                startAlpha,
                targetAlpha,
                time / duration
            );

            yield return null;
        }

        fadeScreen.alpha = targetAlpha;
    }
}