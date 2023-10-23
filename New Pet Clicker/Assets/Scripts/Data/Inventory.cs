using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public void AddItem(Item item)
    {
        items.Add(item);
        // Update your UI or trigger any events if needed
    }

    public bool HasItem(Item item)
    {
        return items.Contains(item);
    }

    // Other inventory-related methods (remove, check space, etc.)...
}
