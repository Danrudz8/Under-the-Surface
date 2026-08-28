using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FoodSequence : MonoBehaviour
{
    public MonoBehaviour playerMovement;
    public CanvasGroup fadeScreen;

    public GameObject objectiveUI;

    public AudioSource bedroomAmbience;
    public AudioSource audioSource;
    public AudioClip eatingSound;

    public void StartFoodSequence(GameObject chosenFoodObject)
    {
        StartCoroutine(FoodSequenceCoroutine(chosenFoodObject));
    }

    IEnumerator FoodSequenceCoroutine(GameObject chosenFoodObject)
    {
        // Disable player movement
        playerMovement.enabled = false;

        // Stop bedroom ambience
        if (bedroomAmbience != null)
        {
            bedroomAmbience.Stop();
        }

        // Hide objective UI
        if (objectiveUI != null)
        {
            objectiveUI.SetActive(false);
        }

        Debug.Log("Player is eating...");

        // Make only the chosen food disappear
        if (chosenFoodObject != null)
        {
            chosenFoodObject.SetActive(false);
        }

        // Play eating sound
        if (audioSource != null && eatingSound != null)
        {
            audioSource.PlayOneShot(eatingSound);
        }

        // Give the eating sound a moment before fading
        yield return new WaitForSeconds(1.2f);

        // Fade to black
        float duration = 1f;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;

            fadeScreen.alpha = Mathf.Lerp(
                0f,
                1f,
                time / duration
            );

            yield return null;
        }

        fadeScreen.alpha = 1f;

        // Load the Body scene
        SceneManager.LoadScene("Body");
    }
}