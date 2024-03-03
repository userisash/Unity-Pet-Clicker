using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HappyInventory : MonoBehaviour
{
    public OwnedItems ownedItems;
    public Transform itemsParent;
    public GameObject inventorySlotPrefab; // Prefab for the UI slot
    public HappinessBar happinessBar; // Reference to the HappinessBar script
    public EnergyController energyController; // Reference to the EnergyController script

    void Start()
    {
        if (inventorySlotPrefab == null || itemsParent == null || happinessBar == null || energyController == null)
        {
            Debug.LogError("Inventory slot prefab, items parent, HappinessBar, or EnergyController not assigned!");
            return;
        }

        PopulateInventory();
    }

    void PopulateInventory()
    {
        foreach (Item item in ownedItems)
        {
            if (item == null)
            {
                Debug.LogWarning("Item is null!");
                continue;
            }

            GameObject slot = Instantiate(inventorySlotPrefab, itemsParent);

            // Access UI elements in the slot prefab
            Transform itemNameTransform = slot.transform.Find("ItemName");
            Transform quantityTextTransform = slot.transform.Find("QuantityText");
            Image itemImage = slot.transform.Find("ItemImage")?.GetComponent<Image>();
            Button consumeButton = slot.transform.Find("ConsumeButton")?.GetComponent<Button>();

            if (itemNameTransform == null || quantityTextTransform == null || itemImage == null || consumeButton == null)
            {
                Debug.LogError("One or more UI elements not found in the prefab!");
                continue;
            }

            // Access TMP components
            TextMeshProUGUI itemNameText = itemNameTransform.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI quantityText = quantityTextTransform.GetComponent<TextMeshProUGUI>();

            if (itemNameText == null || quantityText == null)
            {
                Debug.LogError("One or more TMP components not found in the prefab!");
                continue;
            }

            // Update UI elements with item information
            itemNameText.text = item.name;
            quantityText.text = item.quantity.ToString();  // Set quantity directly
            itemImage.sprite = item.icon;

            // Assign a function to the consume button
            consumeButton.onClick.AddListener(() => ConsumeItem(item, quantityText));
        }
    }

    // Function to handle item consumption
    void ConsumeItem(Item item, TextMeshProUGUI quantityText)
    {
        if (item.quantity > 0)
    {
        if (item.isEnergyItem)
        {
            // Add the EnergyAmount to the energy bar
            EnergyController.Instance.energy.IncreaseEnergy(item.EnergyAmount);
        }
        else
        {
            // Add the increaseAmount to the happiness bar
            happinessBar.IncreaseHappiness(item.increaseAmount);
        }

        // Subtract the quantity
        item.quantity--;

        // Update the quantity text directly
        quantityText.text = item.quantity.ToString();
    }
    else
    {
        Debug.LogWarning("Not enough items to consume!");
    }
    }
}
