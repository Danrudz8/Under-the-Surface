using UnityEngine;

public class MonologueTrigger : MonoBehaviour
{
    public BodyMonologue bodyMonologue;

    public string stageName;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered)
            return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true;

            bodyMonologue.ShowMonologue(stageName);
        }
    }
}