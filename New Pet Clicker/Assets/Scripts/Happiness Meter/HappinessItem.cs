using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "HappinessItem", order = 1)]
public class HappinessItem : ScriptableObject
{
    public string itemName;
    public float happinessEffect; // Percentage increase in happiness
    public int price;
    public Sprite itemSprite;
    public bool isOwned = true; // Assuming all items are owned by default
}
