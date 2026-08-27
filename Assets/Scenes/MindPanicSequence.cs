using UnityEngine;
using System.Collections;

public class MindPanicSequence : MonoBehaviour
{
    public PlayerInteraction playerInteraction;
    public Transform playerCamera;

    public Transform[] cameraPoints;

    public GameObject mirrors;
    public GameObject finalMirror;

    public CanvasGroup fadeScreen;
    public MonoBehaviour playerMovement;
    public MonoBehaviour playerLook;

    public float lookDuration = 0.8f;
    public float fadeDuration = 1f;

    private bool triggered = false;

    public GameObject finalText;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(PanicSequence());
        }
    }

    IEnumerator PanicSequence()
    {
        // Lock interaction/movement
        playerInteraction.interactionLocked = true;

        // Look around the mirrors
        foreach (Transform point in cameraPoints)
        {
            yield return StartCoroutine(MoveCameraToPoint(point));
        }

        // Fade to black
        yield return StartCoroutine(Fade(1f));

        // Remove all mirrors
        mirrors.SetActive(false);

        // Show final mirror
        finalMirror.SetActive(true);

        finalText.SetActive(true);

        // Fade back in
        yield return StartCoroutine(Fade(0f));

        // Wait a little
        yield return new WaitForSeconds(5f);

        yield return StartCoroutine(Fade(1f));
        
    }

    IEnumerator MoveCameraToPoint(Transform target)
    {
        Vector3 startPosition = playerCamera.position;
        Quaternion startRotation = playerCamera.rotation;

        float time = 0f;

        while (time < lookDuration)
        {
            time += Time.deltaTime;

            float t = time / lookDuration;

            playerCamera.position =
                Vector3.Lerp(startPosition, target.position, t);

            playerCamera.rotation =
                Quaternion.Slerp(startRotation, target.rotation, t);

            yield return null;
        }
    }

    IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeScreen.alpha;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            fadeScreen.alpha =
                Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);

            yield return null;
        }

        fadeScreen.alpha = targetAlpha;
    }
}