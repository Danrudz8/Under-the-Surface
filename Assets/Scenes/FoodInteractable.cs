using UnityEngine;

public class FoodInteractable : MonoBehaviour, IInteractable
{
    public FoodSequence foodSequence;
    public string foodName;

    public void Interact()
    {
        // Don't allow food to be eaten until the mirror has been checked
        if (!BedroomProgress.mirrorChecked)
        {
            Debug.Log("Look in the mirror first.");
            return;
        }

        // Remember which food was chosen
        FoodChoiceState.chosenFood = foodName;

        Debug.Log("Chosen food: " + FoodChoiceState.chosenFood);

        // Start the sequence and tell it which food was eaten
        foodSequence.StartFoodSequence(gameObject);
    }
}