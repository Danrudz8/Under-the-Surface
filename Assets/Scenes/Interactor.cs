using UnityEngine;

public interface IInteractable
{
    void Interact();
}

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange = 3f;

    public InteractionUI interactionUI;

    void Update()
    {
        Ray r = new Ray(InteractorSource.position, InteractorSource.forward);

        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                interactionUI.ShowInteraction("[E] Look in Mirror");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactObj.Interact();
                }

                return;
            }
        }

        interactionUI.HideInteraction();
    }
}
