using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappinessInventoryManager : MonoBehaviour
{
    public List<HappinessItem> items; // Assign this in the Inspector
    public HappinessBar happinessBar;

    // Method to use an item
    public void UseItem(HappinessItem item)
    {
        if (item.isOwned)
        {
            // Increase happiness
            happinessBar.IncreaseHappiness(item.happinessEffect);
        }
        else
        {
            Debug.Log("Item not owned.");
        }
    }
}
