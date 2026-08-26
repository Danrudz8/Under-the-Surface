using UnityEngine;

public class FoodInteractable : MonoBehaviour, IInteractable
{
    public FoodSequence foodSequence;

    public void Interact()
    {
        Debug.Log("You selected the food.");

        foodSequence.StartFoodSequence();
    }
}