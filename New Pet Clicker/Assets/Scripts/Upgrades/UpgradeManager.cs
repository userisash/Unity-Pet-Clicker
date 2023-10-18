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

    public int tierOneCost = 10;
    public int tierTwoCost = 20;
    public int tierThreeCost = 40;
    public int tierFourCost = 80;
    public int tierFiveCost = 160;
    public int tierSixCost = 320;

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

            ClickBehavior.DirectlyAddViews(2);
            // Tier one doesn't add followers or cash directly.

            tierOneCost *= 2;
        }
    }

    public void PurchaseTierTwoUpgrade()
    {
        if (ClickBehavior.GetCash() >= tierTwoCost)
        {
            ClickBehavior.AddCash(-tierTwoCost);
            ClickBehavior.SetClickValues(2, 1, 0);
            CheckCountersTens();
            tierTwoCost *= 2;
        }
    }

    public void PurchaseTierThreeUpgrade()
    {
        if (ClickBehavior.GetCash() >= tierThreeCost)
        {
            ClickBehavior.AddCash(-tierThreeCost);
            ClickBehavior.SetClickValues(4, 2, 0);
            CheckCountersTens();
            tierThreeCost *= 2;
        }
    }

    public void PurchaseTierFourUpgrade()
    {
        if (ClickBehavior.GetCash() >= tierFourCost)
        {
            ClickBehavior.AddCash(-tierFourCost);
            ClickBehavior.SetClickValues(8, 4, 1);
            CheckCountersTens();
            tierFourCost *= 2;
        }
    }

    public void PurchaseTierFiveUpgrade()
    {
        if (ClickBehavior.GetCash() >= tierFiveCost)
        {
            ClickBehavior.AddCash(-tierFiveCost);
            ClickBehavior.SetClickValues(32, 8, 4);
            CheckCountersTens();
            tierFiveCost *= 2;
        }
    }

    public void PurchaseTierSixUpgrade()
    {
        if (ClickBehavior.GetCash() >= tierSixCost)
        {
            ClickBehavior.AddCash(-tierSixCost);
            ClickBehavior.SetClickValues(64, 16, 8);
            CheckCountersTens();
            tierSixCost *= 2;
        }
    }

    // ... Similarly for other tiers ...

    private void CheckCountersTens()
    {
        if (ClickBehavior.views % 10 == 0)
        {
            ClickBehavior.followers++;
        }

        if (ClickBehavior.followers % 10 == 0)
        {
            ClickBehavior.cash++;
        }
    }

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



