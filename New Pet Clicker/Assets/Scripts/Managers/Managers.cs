using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class Managers : MonoBehaviour
{
    public ClickBehavior ClickBehavior;

    public int viewsManagerCost = 10;
    public float viewsIncrementValue = 1;

    public int followersManagerCost = 20;
    public float followersIncrementValue = 1;

    public int cashManagerCost = 40;
    public float cashIncrementValue = 1;

    private bool viewsManagerActive = false;
    private bool followersManagerActive = false;
    private bool cashManagerActive = false;


    public TextMeshProUGUI viewsManagerCostText;
    public TextMeshProUGUI viewsIncrementValueText;
    public Button viewsManagerButton;

    public TextMeshProUGUI followersManagerCostText;
    public TextMeshProUGUI followersIncrementValueText;
    public Button followersManagerButton;

    public TextMeshProUGUI cashManagerCostText;
    public TextMeshProUGUI cashIncrementValueText;
    public Button cashManagerButton;

    public GameObject managersMenu;


     void Update()
    {
        CheckButtonInteractiblity();
    }
    public void PurchaseViewsManager()
    {
        if (ClickBehavior.GetCash() >= viewsManagerCost)
        {
            ClickBehavior.AddCash(-viewsManagerCost);

            if (!viewsManagerActive)
            {
                StartCoroutine(AutoIncrementViews());
                viewsManagerActive = true;
            }

            viewsManagerCost *= 2;
            viewsIncrementValue *= 2;
            UpdateManagerTexts();
        }
    }

    public void PurchaseFollowersManager()
    {
        if (ClickBehavior.GetCash() >= followersManagerCost)
        {
            ClickBehavior.AddCash(-followersManagerCost);

            if (!followersManagerActive)
            {
                StartCoroutine(AutoIncrementFollowers());
                followersManagerActive = true;
            }

            followersManagerCost *= 2;
            followersIncrementValue *= 2;
            UpdateManagerTexts();
        }
    }

    public void PurchaseCashManager()
    {
        if (ClickBehavior.GetCash() >= cashManagerCost)
        {
            ClickBehavior.AddCash(-cashManagerCost);

            if (!cashManagerActive)
            {
                StartCoroutine(AutoIncrementCash());
                cashManagerActive = true;
            }

            cashManagerCost *= 2;
            cashIncrementValue *= 2;
            UpdateManagerTexts();
        }
    }


    private IEnumerator AutoIncrementViews()
    {
        while (true)
        {
            for (int i = 0; i < viewsIncrementValue; i++)
            {
                ClickBehavior.IncrementViews();
            }
            yield return new WaitForSeconds(1);
            ClickBehavior.UpdateAllText();
        }
    }

    private IEnumerator AutoIncrementFollowers()
    {
        while (true)
        {
            for (int i = 0; i < followersIncrementValue; i++)
            {
                ClickBehavior.IncrementFollowers();
            }
            yield return new WaitForSeconds(1);
            ClickBehavior.UpdateAllText();
        }
    }

    private IEnumerator AutoIncrementCash()
    {
        while (true)
        {
            for (int i = 0; i < cashIncrementValue; i++)
            {
                ClickBehavior.IncrementCash();
            }
            yield return new WaitForSeconds(1);
            ClickBehavior.UpdateAllText();
        }
    }

    private void UpdateManagerTexts()
    {
        viewsManagerCostText.text = "$:" + viewsManagerCost.ToString();
        viewsIncrementValueText.text = viewsIncrementValue.ToString() + " P/S";

        followersManagerCostText.text = "$:" + followersManagerCost.ToString();
        followersIncrementValueText.text = followersIncrementValue.ToString() + " P/S";

        cashManagerCostText.text = "$:" + cashManagerCost.ToString();
        cashIncrementValueText.text = cashIncrementValue.ToString() + " P/S";
    }

    private void CheckButtonInteractiblity()
    {
        viewsManagerButton.interactable = ClickBehavior.GetCash() >= viewsManagerCost;
        followersManagerButton.interactable = ClickBehavior.GetCash() >= followersManagerCost;
        cashManagerButton.interactable = ClickBehavior.GetCash() >= cashManagerCost;
    }

    public void PopupMenu()
    {
        managersMenu.SetActive(!managersMenu.activeSelf);
    }
}
