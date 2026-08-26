using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FoodSequence : MonoBehaviour
{
    public MonoBehaviour playerMovement;
    public CanvasGroup fadeScreen;

    public void StartFoodSequence()
    {
        StartCoroutine(FoodSequenceCoroutine());
    }

    IEnumerator FoodSequenceCoroutine()
    {
        // Disable player movement
        playerMovement.enabled = false;

        Debug.Log("Player is eating...");

        // Temporary pause so we can test the sequence
        yield return new WaitForSeconds(2f);

        // Fade to black
        float duration = 1f;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            fadeScreen.alpha = time / duration;
            yield return null;
        }

        // Load the Body scene
        SceneManager.LoadScene("Body");
    }
}