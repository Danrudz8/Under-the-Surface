using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class FinalMirrorEnding : MonoBehaviour, IInteractable
{
    public Movement playerMovement;
    public PlayerInteraction playerInteraction;

    public Transform playerCamera;
    public Transform cutsceneCameraPoint;

    public TMP_Text endingText;
    public CanvasGroup fadeScreen;

    public float cameraMoveDuration = 1f;

    public CameraBob cameraBob;

    // AUDIO
    public AudioSource mindAmbience;
    public AudioSource sfxSource;
    public AudioClip transitionSound;

    private bool hasTriggered = false;

    public void Interact()
    {
        if (hasTriggered)
            return;

        hasTriggered = true;
        StartCoroutine(EndingSequence());
    }

    IEnumerator EndingSequence()
    {
        // STOP MIND AMBIENCE
        if (mindAmbience != null)
        {
            mindAmbience.Stop();
            mindAmbience.gameObject.SetActive(false);
        }

        // LOCK PLAYER
        playerMovement.enabled = false;
        playerInteraction.interactionLocked = true;

        if (cameraBob != null)
        {
            cameraBob.enabled = false;
        }

        // HIDE INTERACTION PROMPT
        if (HudController.instance != null)
        {
            HudController.instance.DisableInteractionText();
        }

        // MOVE CAMERA
        Vector3 startPosition = playerCamera.position;
        Quaternion startRotation = playerCamera.rotation;

        float time = 0f;

        while (time < cameraMoveDuration)
        {
            time += Time.deltaTime;
            float t = time / cameraMoveDuration;

            playerCamera.position =
                Vector3.Lerp(
                    startPosition,
                    cutsceneCameraPoint.position,
                    t
                );

            playerCamera.rotation =
                Quaternion.Slerp(
                    startRotation,
                    cutsceneCameraPoint.rotation,
                    t
                );

            yield return null;
        }

        // SHOW ENDING TEXT
        endingText.gameObject.SetActive(true);

        endingText.text =
            "A single food choice does not define you. \nYou are more than what you consume.";

        yield return new WaitForSeconds(5f);

        // PLAY TRANSITION SOUND
        if (sfxSource != null && transitionSound != null)
        {
            sfxSource.PlayOneShot(transitionSound);
        }

        // FADE TO BLACK WHILE SOUND PLAYS
        yield return StartCoroutine(Fade(1f));

        // ALLOW SOUND TO CONTINUE BEFORE CHANGING SCENE
        yield return new WaitForSeconds(2f);

        // LOAD BEDROOM
        GameEndingState.returningFromBody = true;
        SceneManager.LoadScene("Bedroom");
    }

    IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeScreen.alpha;
        float duration = 1f;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;

            fadeScreen.alpha =
                Mathf.Lerp(
                    startAlpha,
                    targetAlpha,
                    time / duration
                );

            yield return null;
        }

        fadeScreen.alpha = targetAlpha;
    }
}