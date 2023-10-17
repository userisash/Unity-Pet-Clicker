using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic; // Required for List<T>



[System.Serializable]
public class Upgrade
{
    public string name;
    public Button associatedButton;
    public int baseCost; // Starting cost of the upgrade
    public int baseValue; // Starting value of the upgrade
    public bool isPurchased = false;

    // Calculated cost based on base cost and times it's been purchased
    public int Cost
    {
        get
        {
            return baseCost * (isPurchased ? 2 : 1);
        }
    }

    // Calculated value based on base value and times it's been purchased
    public int Value
    {
        get
        {
            return baseValue * (isPurchased ? 2 : 1);
        }
    }
}

public class ClickBehavior : MonoBehaviour
{
    public TextMeshProUGUI viewsText;
    public TextMeshProUGUI followersText;
    public TextMeshProUGUI cashText;
    public List<Upgrade> upgrades; // List of available upgrades
    public GameObject popupMenu;
    private int clickValue = 1; // This will be the default value. Will change based on upgrades.


    public int views = 0;
    public int followers = 0;
    public int cash = 10;
    private int cashPerClick = 1; // Default cash increment per click

    public void OnButtonClick()
    {
        IncrementViews();
    }

    public void UpdateUpgradeButtonsInteractivity()
    {
        foreach (Upgrade upgrade in upgrades)
        {
            if (upgrade.associatedButton != null) // check if button reference exists
            {
                upgrade.associatedButton.interactable = cash >= upgrade.Cost && !upgrade.isPurchased;
            }
        }
    }


    private void IncrementViews()
    {
        views += clickValue; // Use clickValue here
        UpdateViewsText();

        while (views >= 10) // Use >= instead of % for clarity
        {
            IncrementFollowers();
            views -= 10; // Reset the count for the next tier
        }
    }


    private void IncrementFollowers()
    {
        followers++;
        UpdateFollowersText();

        if (followers % 10 == 0)
        {
            IncrementCash();
        }
    }

    private void IncrementCash()
    {
        cash += cashPerClick;
        UpdateCashText();
        UpdateUpgradeButtonsInteractivity();
    }

    public void UpdateViewsText()
    {
        viewsText.text = ": " + views.ToString();
    }

    public void UpdateFollowersText()
    {
        followersText.text = ": " + followers.ToString();
    }

    public void UpdateCashText()
    {
        cashText.text = ": " + cash.ToString();
    }

    private void CheckAndIncreaseCash()
    {
        if (followers % 10 == 0)
        {
            cash++;
            UpdateCashText();
        }
    }

    public void AddCash(int amount)
    {
        cash += amount;
        UpdateCashText();
        UpdateUpgradeButtonsInteractivity();
    }

    public int GetCash()
    {
        return cash;
    }

    public void AddFollowers(int amount)
    {
        followers += amount;
        UpdateFollowersText();
        CheckAndIncreaseCash();
    }

    // Function to purchase an upgrade
    public void PurchaseUpgrade(int index)
    {
        if (index < 0 || index >= upgrades.Count)
            return;

        Upgrade upgrade = upgrades[index];

        if (cash >= upgrade.Cost && !upgrade.isPurchased)
        {
            cash -= upgrade.Cost;
            upgrade.isPurchased = true;

            // Update click value with the purchased upgrade's value
            clickValue = upgrade.Value;

            // Update the UI
            UpdateCashText();
            UpdateUpgradeButtonsInteractivity();  // Make sure you still have this method to update button interactivity
        }
    }


    private int GetTotalUpgradeValue()
    {
        int totalValue = 1; // start with the base value of 1
        foreach (Upgrade upgrade in upgrades)
        {
            if (upgrade.isPurchased)
            {
                totalValue += upgrade.Value; // add the upgrade value if it's purchased
            }
        }
        return totalValue;
    }


    public void ToggleMenu()
    {
        Debug.Log("ToggleMenu method called.");
        popupMenu.SetActive(!popupMenu.activeSelf);
    }
}
