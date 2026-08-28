using UnityEngine;
using TMPro;
using System.Collections;

public class BodyMonologue : MonoBehaviour
{
    public TMP_Text monologueText;

    private Coroutine currentMonologue;

    public void ShowMonologue(string stage)
    {
        string textToShow = GetMonologue(stage);

        if (currentMonologue != null)
        {
            StopCoroutine(currentMonologue);
        }

        currentMonologue = StartCoroutine(
            ShowMonologueCoroutine(textToShow)
        );
    }

    string GetMonologue(string stage)
    {
        string food = FoodChoiceState.chosenFood;

        if (food == "Burger")
        {
            if (stage == "Bloodstream")
                return "I probably shouldn't have eaten that...";

            if (stage == "Skin")
                return "What if that makes my skin worse?";

            if (stage == "Mind")
                return "Why do I always blame myself over one choice?";
        }

        if (food == "Donut")
        {
            if (stage == "Bloodstream")
                return "Maybe I should've had more self-control...";

            if (stage == "Skin")
                return "What if this makes me break out more?";

            if (stage == "Mind")
                return "Why do I feel guilty just for eating something I wanted?";
        }

        if (food == "Avocado")
        {
            if (stage == "Bloodstream")
                return "Good... at least I made the right choice.";

            if (stage == "Skin")
                return "But what if I'm still not doing enough?";

            if (stage == "Mind")
                return "Why do I feel like every choice has to be perfect?";
        }

        return "Why am I thinking so much about one food choice?";
    }

    IEnumerator ShowMonologueCoroutine(string text)
    {
        // THIS is the important part
        monologueText.gameObject.SetActive(true);

        monologueText.text = text;

        yield return new WaitForSeconds(4f);

        monologueText.gameObject.SetActive(false);

        currentMonologue = null;
    }
}