/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    
    public ClickBehavior currencySystem; // Assuming your ClickBehavior script has currency related methods
    public List<Item> storeItems = new List<Item>(); // Items the store sells
    public GameObject itemPrefab; // This is a prefab of the item UI (e.g., a panel with icon, name, price...)
    public Transform storeItemsParent; // This is where the item UI elements will be instantiated (e.g., a vertical layout group)

    private void Start()
    {
        DisplayStoreItems();
    }

    public void DisplayStoreItems()
    {
        foreach (Item item in storeItems)
        {
            GameObject itemUI = Instantiate(itemPrefab, storeItemsParent);
            itemUI.GetComponent<ItemUI>().Initialize(item, BuyItem, currencySystem);
            // This assumes you have an "ItemUI" script on your prefab to handle the display and buy action.
        }
    }

    public void BuyItem(Item item)
    {
        if (currencySystem.GetCash() >= item.price)
        {
            currencySystem.AddCash(-item.price);
            
        }
        else
        {
            // Notify the player they don't have enough currency
        }
    }
}
*/