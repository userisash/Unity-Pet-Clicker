using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

[System.Serializable]
public class BusinessUpgrade
{
    public string upgradeName;
    public int upgradeCost;
    public int rewardIncrease;
    public int currentLevel = 0;
    public TextMeshProUGUI upgradeCostText;
    public TextMeshProUGUI upgradeNameText;
    public Button upgradeButton;
}

public class BusinessUpgradeManager : MonoBehaviour
{
    public List<BusinessUpgrade> upgrades;
    public ClickBehavior clickBehavior;

    private void Start()
    {
        UpdateUpgradeUI();
    }

    void Update()
    {
        foreach (var upgrade in upgrades)
        {
            CheckUpgradeButtonInteractivity(upgrade);
        }
    }


    private void UpdateUpgradeUI()
    {
        for (int i = 0; i < upgrades.Count; i++)
        {
            int index = i; // Capture loop variable for use in lambda expression
            upgrades[i].upgradeCostText.text = $"Cost: ${upgrades[i].upgradeCost}";
            upgrades[i].upgradeButton.onClick.RemoveAllListeners();
            upgrades[i].upgradeButton.onClick.AddListener(() => PurchaseUpgrade(index));
            CheckUpgradeButtonInteractivity(upgrades[i]);
        }
    }

    public void PurchaseUpgrade(int index)
    {
        BusinessUpgrade upgrade = upgrades[index];
        if (clickBehavior.GetCash() >= upgrade.upgradeCost)
        {
            Debug.Log($"Purchased upgrade: {upgrade.upgradeName}");
            clickBehavior.AddCash(-upgrade.upgradeCost);
            upgrade.currentLevel++;
            // Increase cash reward of the associated business
            // This requires a reference to the associated BusinessManager or BusinessController
            // Example: upgrade.associatedBusiness.reward += upgrade.rewardIncrease;

            // Optionally increase the cost of the next upgrade
            upgrade.upgradeCost += CalculateNextUpgradeCost(upgrade);

            UpdateUpgradeUI();
        }
    }

    private int CalculateNextUpgradeCost(BusinessUpgrade upgrade)
    {
        // Implement logic to calculate the cost of the next upgrade
        // For example, increase by a fixed amount or percentage
        return upgrade.upgradeCost; // Placeholder, replace with actual calculation
    }

    private void CheckUpgradeButtonInteractivity(BusinessUpgrade upgrade)
    {
        bool canAfford = clickBehavior.GetCash() >= upgrade.upgradeCost;
        Debug.Log($"Checking button interactability for {upgrade.upgradeName}: Can afford = {canAfford}");
        upgrade.upgradeButton.interactable = canAfford;
    }

}
