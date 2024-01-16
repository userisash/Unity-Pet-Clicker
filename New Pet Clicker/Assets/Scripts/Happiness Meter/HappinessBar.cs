// HappinessBar.cs
using UnityEngine;
using UnityEngine.UI;

public class HappinessBar : MonoBehaviour
{
    public Slider happinessSlider;
    private float happiness = 1.0f; // Full happiness is 1.0
    private float decreaseRate = 0.01f; // The rate at which happiness decreases

    void Start()
    {
        // Initialize happiness
        happinessSlider.value = happiness;
        InvokeRepeating(nameof(DecreaseHappinessOverTime), 60f, 60f); // Decrease happiness over time every 60 seconds
    }

    public void DecreaseHappinessOnButtonClick()
    {
        ChangeHappiness(-decreaseRate);
    }

    private void DecreaseHappinessOverTime()
    {
        ChangeHappiness(-decreaseRate);
    }

    public void IncreaseHappiness(float increaseAmount)
    {
        happiness += increaseAmount;
        happiness = Mathf.Clamp(happiness, 0f, 1f); // Ensure happiness stays within bounds
        happinessSlider.value = happiness;
    }

    private void ChangeHappiness(float amount)
    {
        happiness += amount;
        happiness = Mathf.Clamp(happiness, 0f, 1f);
        happinessSlider.value = happiness;

        // Optional: Check happiness level for other effects
        CheckHappinessLevel();
    }

    private void CheckHappinessLevel()
    {
        if (happiness <= 0)
        {
            // Trigger any events or effects when happiness is depleted
            Debug.Log("Happiness is depleted!");
        }
    }
}
