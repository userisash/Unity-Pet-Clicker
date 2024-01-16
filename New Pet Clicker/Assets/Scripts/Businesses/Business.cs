using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Business
{
    public string businessName;
    public Sprite businessImage;
    public int cashReward;
    public float fillDuration;
    public float energyCost; // New addition

    // UI Elements
    public TMPro.TextMeshProUGUI nameText;
    public Image businessImg;
    public Button startBusinessButton;
    public Slider progressBar;

    // Other business-related fields
    public bool isRunning;
    public float timePassed;
}




