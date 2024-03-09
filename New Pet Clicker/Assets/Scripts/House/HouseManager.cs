using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class HouseManager : MonoBehaviour
{
    public Image houseImage;
    public Button updateButton;
    public Button upgradeButton;
    public TMP_Text levelText;
    public Sprite[] houseLevels; // Optional if you're using sprites to represent house upgrade levels
    public Sprite[] upgradedHouseImages; // Sprites for each new house type after upgrade
    public Image[] updateLevelIndicators; // UI Indicators
    public TMP_Text[] updateLevelIndicatorTexts; // Texts for each upgrade indicator
    public TMP_Text updateCostText; // Assign in the inspector
    public TMP_Text upgradeCostText; // Assign in the inspector
    public Sprite updateIndicatorSprite; // Sprite to show on update, assigned in the Inspector

    private ClickBehavior clickBehavior;
    private UpgradeDescriptions upgradeDescriptions;
    private int houseLevel = 1;
    private int cashRequiredForNextUpdate = 10;
    private int upgradeCost = 10000; // Example upgrade cost, adjust as needed
    private int currentHouseTypeIndex = 0; // Index to track the current type of house

    private void Start()
    {
        clickBehavior = FindObjectOfType<ClickBehavior>();
        upgradeDescriptions = FindObjectOfType<UpgradeDescriptions>();

        if (clickBehavior == null || upgradeDescriptions == null)
        {
            Debug.LogError("Required component not found.");
            return;
        }
     
        upgradeButton.interactable = false;
        // Initialize with random descriptions for the first set
        ResetUpdateLevelIndicators(upgradeDescriptions.GetRandomDescriptions());
        UpdateUI();
        UpdateHouseImage();
    }

    public void UpdateHouse()
    {
        if (clickBehavior.GetCash() >= cashRequiredForNextUpdate)
        {
            clickBehavior.AddCash(-cashRequiredForNextUpdate);
            houseLevel++;
            cashRequiredForNextUpdate *= 2;

            if (houseLevel > updateLevelIndicators.Length)
            {
                upgradeButton.interactable = true;
            }

            UpdateLevelIndicator();
            
            UpdateUI();
            CheckUpgradePossibility();
        }
        else
        {
            Debug.Log("Not enough cash to update the house.");
        }
    }

    public void UpgradeHouse()
    {
        if (houseLevel == 10 && clickBehavior.GetCash() >= upgradeCost)
        {
            clickBehavior.AddCash(-upgradeCost); // Deduct the upgrade cost
            houseLevel = 1; // Reset house level for the new upgrade
            currentHouseTypeIndex++; // Move to next house type
            if (currentHouseTypeIndex >= upgradedHouseImages.Length)
            {
                currentHouseTypeIndex = 0; // Reset or handle as per your design
            }
            UpdateHouseImage(); // Update the house image to reflect the upgrade
            ResetUpdateLevelIndicators(upgradeDescriptions.GetRandomDescriptions()); // Reset the indicators
            UpdateUI(); // Update the UI, which includes updating indicator visuals and texts
        }
        else
        {
            Debug.Log("Not enough cash to upgrade the house.");
        }
    }


    private void UpdateUI()
    {
        levelText.text = $"House Level: {houseLevel}";
        updateCostText.text = $"Update Cost: {cashRequiredForNextUpdate}";
        upgradeCostText.text = $"Upgrade Cost: {upgradeCost}";
        CheckUpgradePossibility();
    }

 

    private void UpdateLevelIndicator()
    {
        // Activates the sprite for indicators up to the current level
        for (int i = 0; i < houseLevel && i < updateLevelIndicators.Length; i++)
        {
            updateLevelIndicators[i].sprite = updateIndicatorSprite;
            // Keep the text unchanged
        }
    }

    private void ResetUpdateLevelIndicators(List<string> descriptions)
    {
        for (int i = 0; i < updateLevelIndicators.Length; i++)
        {
            updateLevelIndicators[i].sprite = null; // Reset the sprite to default or null as per your design
            updateLevelIndicators[i].color = Color.gray; // Reset color if you're using color to indicate an inactive state
            if (i < descriptions.Count)
            {
                updateLevelIndicatorTexts[i].text = descriptions[i];
            }
            else
            {
                // Ensure that any indicators without a new description are cleared or set to a default state
                updateLevelIndicatorTexts[i].text = "";
            }
        }
    }


    private void CheckUpgradePossibility()
    {
        // Check both the level and cash conditions for upgrading
        upgradeButton.interactable = houseLevel >= 10 && clickBehavior.GetCash() >= upgradeCost;
    }

    private void UpdateHouseImage()
    {
        // Updates the main house image to the current type
        houseImage.sprite = upgradedHouseImages[currentHouseTypeIndex];
    }
}
