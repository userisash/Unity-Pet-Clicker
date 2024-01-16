using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OwnedItems", menuName = "Inventory/OwnedItems")]
public class OwnedItems : ScriptableObject, IEnumerable<HappinessItem>
{
    public List<HappinessItem> items = new List<HappinessItem>();

    public IEnumerator<HappinessItem> GetEnumerator()
    {
        return items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
