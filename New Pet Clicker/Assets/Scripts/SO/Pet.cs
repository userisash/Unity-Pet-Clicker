using UnityEngine;

[CreateAssetMenu(fileName = "New Pet", menuName = "Pet")]
public class Pet : ScriptableObject
{
    public string petName;
    public Sprite petSprite;
    public GameObject petPrefab; // New variable for the prefab with animations
}

