using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float playerReach = 3f;
    public bool interactionLocked = false;

    private IInteractable currentInteractable;

    void Update()
    {
        if (interactionLocked)
        {
            if (HudController.instance != null)
            {
                HudController.instance.DisableInteractionText();
            }

            return;
        }

        CheckInteraction();

        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            if (HudController.instance != null)
            {
                HudController.instance.DisableInteractionText();
            }

            currentInteractable.Interact();
        }
    }

    void CheckInteraction()
    {
        RaycastHit hit;

        Ray ray = new Ray(
            Camera.main.transform.position,
            Camera.main.transform.forward
        );

        if (Physics.Raycast(ray, out hit, playerReach))
        {
            FoodInteractable food =
                hit.collider.GetComponentInParent<FoodInteractable>();

            MirrorInteractable mirror =
                hit.collider.GetComponentInParent<MirrorInteractable>();

            if (food != null)
            {
                currentInteractable = food;

                if (HudController.instance != null)
                {
                    HudController.instance.EnableInteractionText("Eat");
                }
            }
            else if (mirror != null)
            {
                currentInteractable = mirror;

                if (HudController.instance != null)
                {
                    HudController.instance.EnableInteractionText("Look in the mirror");
                }
            }
            else
            {
                currentInteractable = null;

                if (HudController.instance != null)
                {
                    HudController.instance.DisableInteractionText();
                }
            }
        }
        else
        {
            currentInteractable = null;

            if (HudController.instance != null)
            {
                HudController.instance.DisableInteractionText();
            }
        }
    }
}