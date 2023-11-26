using UnityEngine;
using System.Collections.Generic;

public class HappinessStoreManager : MonoBehaviour
{
    public List<HappinessItem> availableItems; // Assign this in the Inspector
    public HappinessBar happinessBar;

    // Method to purchase an item
    public void PurchaseItem(HappinessItem item)
    {
        // Check if the player has enough currency
        // Assuming you have a method GetPlayerCurrency() to get the player's current currency
        if (GetPlayerCurrency() >= item.price)
        {
            // Decrease player's currency
            DecreasePlayerCurrency(item.price);

            // Increase happiness
            happinessBar.IncreaseHappiness(item.happinessEffect);
        }
        else
        {
            Debug.Log("Not enough currency to purchase this item.");
        }
    }

    private int GetPlayerCurrency()
    {
        // Return the player's current currency
        return 0; // Replace with actual currency logic
    }

    private void DecreasePlayerCurrency(int amount)
    {
        // Logic to decrease player's currency
    }
}
