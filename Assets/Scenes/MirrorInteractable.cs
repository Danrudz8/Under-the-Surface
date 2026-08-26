using UnityEngine;

public class MirrorInteractable : MonoBehaviour, IInteractable
{
    public MirrorSequence mirrorSequence;

    public void Interact()
    {
        Debug.Log("You looked in the mirror.");

        mirrorSequence.StartMirrorSequence();
    }
}
