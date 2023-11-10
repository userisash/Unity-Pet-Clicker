using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class Tier
{
    public ClickBehavior ClickBehavior;

    public string Name; // Name of the tier for easy identification
    public TextMeshProUGUI CostText;
    public TextMeshProUGUI IncrementText;
    public Button PurchaseButton;

    public int Cost;
    public int ViewsIncrement;
    public int FollowersIncrement;
    public int CashIncrement;
 

    // Method to purchase this tier's upgrade
    public bool PurchaseUpgrade(ClickBehavior clickBehavior)
    {
        if (clickBehavior.GetCash() >= Cost)
        {
            clickBehavior.AddCash(-Cost);
            clickBehavior.AddToClickValues(ViewsIncrement, FollowersIncrement, CashIncrement);
            Cost *= 2;
            UpdateTexts();
            return true;
        }
        return false;
    }

    // Update the UI elements for this tier
    public void UpdateTexts()
    {
        CostText.text = "$: " + ClickBehavior.FormatNumber(Cost);
        IncrementText.text = GetIncrementDescription();
    }

    private string GetIncrementDescription()
    {
        string desc = "";
        if (ViewsIncrement > 0) desc += $"Views +{ViewsIncrement}, ";
        if (FollowersIncrement > 0) desc += $"Followers +{FollowersIncrement}, ";
        if (CashIncrement > 0) desc += $"Cash +{CashIncrement}, ";
        return desc.TrimEnd(',', ' ');
    }
}

public class UpgradeManager : MonoBehaviour
{
    public ClickBehavior ClickBehavior;
    public GameObject popupMenu;
    public List<Tier> tiers;

    private void Start()
    {
        // Assign button callbacks
        foreach (var tier in tiers)
        {
            Button btn = tier.PurchaseButton;
            int tierIndex = tiers.IndexOf(tier); // Get the index of the current tier
            btn.onClick.AddListener(() => tiers[tierIndex].PurchaseUpgrade(ClickBehavior));
        }
    }


    private void Update()
    {
        UpdateButtonInteractability();
        UpdateAllTexts();
    }

    private void UpdateButtonInteractability()
    {
        foreach (var tier in tiers)
        {
            tier.PurchaseButton.interactable = ClickBehavior.GetCash() >= tier.Cost;
        }
    }

    private void UpdateAllTexts()
    {
        foreach (var tier in tiers)
        {
            tier.UpdateTexts();
        }
    }

    public void ToggleMenu()
    {
        popupMenu.SetActive(!popupMenu.activeSelf);
    }
}
