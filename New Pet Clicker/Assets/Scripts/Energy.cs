using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Energy
{
    public Sprite energyImage;
    public float maxEnergy;
    public float rechargeDuration;

    // UI Elements
    public TMPro.TextMeshProUGUI energyText;
    public Image energyImg;
    public Slider progressBar;

    // Current energy level
    private float currentEnergy;

    public void Initialize(Slider energySlider)
    {
        // Set initial values
        currentEnergy = maxEnergy;

        // Set the slider reference
        progressBar = energySlider;

        UpdateUI();
    }


    public void UpdateEnergy()
    {
        if (currentEnergy < maxEnergy)
        {
            // Recharge energy over time
            float rechargeRate = maxEnergy / rechargeDuration;
            currentEnergy += rechargeRate * Time.deltaTime;

            // Ensure energy does not exceed the maximum
            currentEnergy = Mathf.Clamp(currentEnergy, 0f, maxEnergy);

            // Update UI
            UpdateUI();
        }
    }

    public bool ConsumeEnergy(float amount)
    {
        if (currentEnergy >= amount)
        {
            currentEnergy -= amount;
            UpdateUI();
            return true; // Successfully consumed energy
        }
        else
        {
            return false; // Not enough energy to consume
        }
    }

    private void UpdateUI()
    {
        // Update UI elements based on the current energy level
        energyText.text = "Energy: " + Mathf.RoundToInt(currentEnergy);
        progressBar.value = currentEnergy / maxEnergy;
    }
}