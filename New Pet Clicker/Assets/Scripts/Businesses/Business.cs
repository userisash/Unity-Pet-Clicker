using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Business
{
    public string businessName;
    public Sprite businessImage;
    public int rewardAmount;
    public float fillTime;
    [HideInInspector]
    public float currentFillAmount = 0;

    // UI elements
    public GameObject businessUI; // The GameObject containing all the business's UI elements.
    public Image imageComponent;
    public TextMeshProUGUI titleComponent;
    public Slider progressBarComponent;
    public Button businessButton;
}
