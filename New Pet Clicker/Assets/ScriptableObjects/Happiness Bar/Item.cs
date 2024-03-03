using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite icon;
    public float increaseAmount;
    public int quantity = 1;  // New addition
    public bool isEnergyItem = false; // New addition
    public int EnergyAmount = 0; // New addition
}
