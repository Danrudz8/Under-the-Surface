using UnityEngine;

public class MirrorInteractable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("You looked in the mirror.");
    }
}
