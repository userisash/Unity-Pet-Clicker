using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class HappinessItem : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite icon;
    public float increaseAmount;
    public int quantity = 1;  // New addition
}
