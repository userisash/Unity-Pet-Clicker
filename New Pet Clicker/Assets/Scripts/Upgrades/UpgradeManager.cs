using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class UpgradeManager : MonoBehaviour
{
    public ClickBehavior ClickBehavior;  // Reference to your ClickBehavior script.
    public GameObject popupMenu;

    public Button tierOneButton;
    public Button tierTwoButton;
    public Button tierThreeButton;
    public Button tierFourButton;
    public Button tierFiveButton;
    public Button tierSixButton;

    private int tierOneCost = 10;
    private int tierTwoCost = 20;
    private int tierThreeCost = 40;
    private int tierFourCost = 80;
    private int tierFiveCost = 160;
    private int tierSixCost = 320;

    private void Update()
    {
        // Check interactability every frame (you can optimize this by checking only when cash changes if needed)
        UpdateButtonInteractability();
    }

    public void PurchaseTierOneUpgrade()
    {
        if (ClickBehavior.GetCash() >= tierOneCost)
        {
            ClickBehavior.AddCash(-tierOneCost);
            ClickBehavior.SetClickValues(2, 0, 0);
            tierOneCost *= 2; // Double the cost for next purchase
        }
    }

    public void PurchaseTierTwoUpgrade()
    {
        if (ClickBehavior.GetCash() >= tierTwoCost)
        {
            ClickBehavior.AddCash(-tierTwoCost);
            ClickBehavior.SetClickValues(2, 1, 0);
            tierTwoCost *= 2;
        }
    }

    public void PurchaseTierThreeUpgrade()
    {
        if (ClickBehavior.GetCash() >= tierThreeCost)
        {
            ClickBehavior.AddCash(-tierThreeCost);
            ClickBehavior.SetClickValues(4, 2, 0);
            tierThreeCost *= 2;
        }
    }

    public void PurchaseTierFourUpgrade()
    {
        if (ClickBehavior.GetCash() >= tierFourCost)
        {
            ClickBehavior.AddCash(-tierFourCost);
            ClickBehavior.SetClickValues(8, 4, 1);
            tierFourCost *= 2;
        }
    }

    public void PurchaseTierFiveUpgrade()
    {
        if (ClickBehavior.GetCash() >= tierFiveCost)
        {
            ClickBehavior.AddCash(-tierFiveCost);
            ClickBehavior.SetClickValues(32, 8, 4);
            tierFiveCost *= 2;
        }
    }

    public void PurchaseTierSixUpgrade()
    {
        if (ClickBehavior.GetCash() >= tierSixCost)
        {
            ClickBehavior.AddCash(-tierSixCost);
            ClickBehavior.SetClickValues(64, 16, 8);
            tierSixCost *= 2;
        }
    }

    // ... Similarly for other tiers ...

    private void UpdateButtonInteractability()
    {
        tierOneButton.interactable = ClickBehavior.GetCash() >= tierOneCost;
        tierTwoButton.interactable = ClickBehavior.GetCash() >= tierTwoCost;
        tierThreeButton.interactable = ClickBehavior.GetCash() >= tierThreeCost;
        tierFourButton.interactable = ClickBehavior.GetCash() >= tierFourCost;
        tierFiveButton.interactable = ClickBehavior.GetCash() >= tierFiveCost;
        tierSixButton.interactable = ClickBehavior.GetCash() >= tierSixCost;
        // ... Similarly for other tiers ...
    }
    public void ToggleMenu()
    {
        Debug.Log("ToggleMenu method called.");
        popupMenu.SetActive(!popupMenu.activeSelf);
    }
}



