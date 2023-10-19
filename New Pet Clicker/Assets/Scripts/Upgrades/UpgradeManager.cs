using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public ClickBehavior ClickBehavior;
    public GameObject popupMenu;

    public Button tierOneButton;
    public Button tierTwoButton;
    //... other tier buttons...

    public int tierOneCost = 10;
    public int tierTwoCost = 20;
    //... other tier costs...

    private void Update()
    {
        UpdateButtonInteractability();
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
        }
    }


    public void PurchaseTierTwoUpgrade()
    {
        if (ClickBehavior.GetCash() >= tierTwoCost)
        {
            ClickBehavior.AddCash(-tierTwoCost);
            ClickBehavior.AddToClickValues(2, 2, 0);
            tierTwoCost *= 2;
        }
    }


    //... repeat for other tiers...

    public void ToggleMenu()
    {
        Debug.Log("ToggleMenu method called.");
        popupMenu.SetActive(!popupMenu.activeSelf);
    }
}
