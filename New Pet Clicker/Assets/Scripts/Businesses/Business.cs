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

    // UI Elements
    public TMPro.TextMeshProUGUI nameText;
    public Image businessImg;
    public Button startBusinessButton;
    public Slider progressBar;
}



