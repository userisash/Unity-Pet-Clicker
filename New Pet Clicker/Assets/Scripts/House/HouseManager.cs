using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class HouseManager : MonoBehaviour
{
    public Image houseImage;
    public Button updateButton;
    public Button upgradeButton;
    public TMP_Text levelText;
    public TMP_Text updateNotificationText;
    public Sprite[] houseLevels; // Sprites for levels within a house type
    public Sprite[] upgradedHouseImages; // Sprites for each new house type after upgrade
    public Image[] updateLevelIndicators; // Assign in the inspector, 10 images for level indicators

    private ClickBehavior clickBehavior;
    private int houseLevel = 1;
    private int cashRequiredForNextUpdate = 10;
    private int currentHouseTypeIndex = 0; // Index to track the current type of house

    private void Start()
    {
        clickBehavior = FindObjectOfType<ClickBehavior>();
        if (clickBehavior == null)
        {
            Debug.LogError("ClickBehavior script not found.");
            return;
        }
        updateNotificationText.gameObject.SetActive(false);
        upgradeButton.interactable = false;
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

            if (houseLevel >= 10)
            {
                upgradeButton.interactable = true;
            }

            UpdateLevelIndicator();
            StartCoroutine(ShowUpdateNotification());
            UpdateUI();
        }
        else
        {
            Debug.Log("Not enough cash to update the house.");
        }
    }

    public void UpgradeHouse()
    {
        if (houseLevel == 10)
        {
            // Advance to the next house type if available
            currentHouseTypeIndex++;
            if (currentHouseTypeIndex >= upgradedHouseImages.Length)
            {
                currentHouseTypeIndex = 0; // Optionally loop or handle the end of upgrades
            }

            houseLevel = 1; // Reset level for the new house type
            UpdateHouseImage(); // Update to the new house image
            upgradeButton.interactable = false;
            ResetUpdateLevelIndicators();
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        levelText.text = $"House Level: {houseLevel}";
    }

    private IEnumerator ShowUpdateNotification()
    {
        updateNotificationText.text = "House Updated!";
        updateNotificationText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        updateNotificationText.gameObject.SetActive(false);
    }

    private void UpdateLevelIndicator()
    {
        for (int i = 0; i < updateLevelIndicators.Length; i++)
        {
            if (i < houseLevel)
            {
                updateLevelIndicators[i].color = Color.green; // Update to green up to the current level
            }
        }
    }

    private void ResetUpdateLevelIndicators()
    {
        foreach (var indicator in updateLevelIndicators)
        {
            indicator.color = Color.gray; // Reset all indicators to gray
        }
    }

    // This method might be simplified or adjusted based on new requirements
    private void UpdateHouseImage()
    {
        houseImage.sprite = upgradedHouseImages[currentHouseTypeIndex];
    }
}
