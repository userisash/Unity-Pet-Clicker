using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    public ClickBehavior ClickBehavior;
    public GameObject popupMenu;

    public TextMeshProUGUI tierOneCostText;
    public TextMeshProUGUI tierTwoCostText;


    public TextMeshProUGUI tierOneIncrementText;
    public TextMeshProUGUI tierTwoIncrementText;

    public Button tierOneButton;
    public Button tierTwoButton;
    //... other tier buttons...

    public int tierOneCost = 10;
    public int tierTwoCost = 20;
    //... other tier costs...

    private void Update()
    {
        UpdateButtonInteractability();
        UpdateAllTexts();
    }

    private void UpdateButtonInteractability()
    {
        tierOneButton.interactable = ClickBehavior.GetCash() >= tierOneCost;
        tierTwoButton.interactable = ClickBehavior.GetCash() >= tierTwoCost;
        //... Similarly for other tiers...
    }



    public void PurchaseTierOneUpgrade()
    {
        if (ClickBehavior.GetCash() >= tierOneCost)
        {
            ClickBehavior.AddCash(-tierOneCost);
            ClickBehavior.AddToClickValues(2, 0, 0);
            tierOneCost *= 2;
            UpdateText(tierOneCostText, tierOneCost);
        }
    }



    public void PurchaseTierTwoUpgrade()
    {
        if (ClickBehavior.GetCash() >= tierTwoCost)
        {
            ClickBehavior.AddCash(-tierTwoCost);
            ClickBehavior.AddToClickValues(2, 2, 0);
            tierTwoCost *= 2;
            UpdateText(tierTwoCostText, tierTwoCost);
        }
    }


    private void UpdateAllTexts()
    {
        UpdateText(tierOneCostText, tierOneCost);
        UpdateText(tierTwoCostText, tierTwoCost);
       
        tierOneIncrementText.text = "Views +2";
        tierTwoIncrementText.text = "Views +2, Followers +1";
       
    }

    private void UpdateText(TextMeshProUGUI targetText, int value)
    {
        targetText.text = "$: " + value.ToString();
    }

    //... repeat for other tiers...

    public void ToggleMenu()
    {
        Debug.Log("ToggleMenu method called.");
        popupMenu.SetActive(!popupMenu.activeSelf);
    }
}
