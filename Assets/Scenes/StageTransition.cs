using UnityEngine;
using System.Collections;

public class StageTransition : MonoBehaviour
{
    public GameObject currentStage;
    public GameObject nextStage;

    public Transform nextSpawnPoint;
    public CanvasGroup fadeScreen;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered)
            return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartCoroutine(Transition(other.transform));
        }
    }

    IEnumerator Transition(Transform player)
    {
        // Fade to black
        float time = 0f;
        float duration = 1f;

        while (time < duration)
        {
            time += Time.deltaTime;
            fadeScreen.alpha = time / duration;
            yield return null;
        }

        // Switch stages
        currentStage.SetActive(false);
        nextStage.SetActive(true);

        // Teleport player
        CharacterController controller =
            player.GetComponent<CharacterController>();

        if (controller != null)
        {
            controller.enabled = false;
        }

        player.position = nextSpawnPoint.position;
        player.rotation = nextSpawnPoint.rotation;

        if (controller != null)
        {
            controller.enabled = true;
        }

        // Fade back in
        time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            fadeScreen.alpha = 1f - (time / duration);
            yield return null;
        }

        fadeScreen.alpha = 0f;
    }
}