using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OwnedItems", menuName = "Inventory/OwnedItems")]
public class OwnedItems : ScriptableObject, IEnumerable<Item>
{
    public List<Item> items = new List<Item>();

    public IEnumerator<Item> GetEnumerator()
    {
        return items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}