using UnityEngine;
using UnityEngine.UI;

public class HappinessBar : MonoBehaviour
{
    public Slider happinessSlider;
    private Image fillImage; // Reference to the fill area's Image component
    private float happiness = 1.0f; // Full happiness is 1.0
    private float decreaseRate = 0.01f; // The rate at which happiness decreases

    // Color thresholds
    private Color fullHappinessColor = Color.yellow; // Color when happiness is more than 50% (#94C9FF)
    private Color mediumHappinessColor = new Color(1f, 0.65f, 0f); // Orange color
    private Color lowHappinessColor = Color.red; // Color when happiness is 20% or less

    void Start()
    {
        fillImage = happinessSlider.fillRect.GetComponent<Image>();
        happinessSlider.value = happiness;
        UpdateHappinessColor();
    }

    public void IncreaseHappiness(float increaseAmount)
    {
        happiness += increaseAmount;
        happiness = Mathf.Clamp(happiness, 0f, 1f); // Ensure happiness stays within bounds
        happinessSlider.value = happiness;
        UpdateHappinessColor();
    }


    public void ChangeHappiness(float amount)
    {
        happiness += amount;
        happiness = Mathf.Clamp(happiness, 0f, 1f);
        happinessSlider.value = happiness;
        UpdateHappinessColor();

        // Optional: Check happiness level for other effects
        CheckHappinessLevel();
    }

    private void UpdateHappinessColor()
    {
        if (happiness <= 0.2f)
        {
            fillImage.color = lowHappinessColor;
        }
        else if (happiness <= 0.5f)
        {
            fillImage.color = mediumHappinessColor;
        }
        else
        {
            fillImage.color = fullHappinessColor;
        }
    }

    private void CheckHappinessLevel()
    {
        if (happiness <= 0)
        {
            // Trigger any events or effects when happiness is depleted
            Debug.Log("Happiness is depleted!");
        }
    }

    public void DecreaseHappinessOnButtonClick()
    {
        ChangeHappiness(-decreaseRate);
    }

    public void IncreaseHappinessOverTime(float increaseRate)
    {
        ChangeHappiness(increaseRate * Time.deltaTime);
    }
}
