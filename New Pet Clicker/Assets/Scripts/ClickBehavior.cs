using UnityEngine;
using TMPro; // Required for TextMeshPro

public class ClickBehavior : MonoBehaviour
{
    public TextMeshProUGUI viewsText; // Reference to TextMeshProUGUI component to display views
    public TextMeshProUGUI followersText; // Reference for followers
    public TextMeshProUGUI cashText; // Reference for cash

    public int views = 0;
    public int followers = 0;
    public int cash = 10;

    public void OnButtonClick()
    {
        IncrementViews();
    }

    private void IncrementViews()
    {
        views++;
        UpdateViewsText();

        if (views % 10 == 0)
        {
            IncrementFollowers();
        }
    }

    private void IncrementFollowers()
    {
        followers++;
        UpdateFollowersText();

        if (followers % 10 == 0)
        {
            IncrementCash();
        }
    }

    private void IncrementCash()
    {
        cash++;
        UpdateCashText();
    }




    public void UpdateViewsText()
    {
        viewsText.text = ": " + views.ToString();
    }

    public void UpdateFollowersText()
    {
        followersText.text = ": " + followers.ToString();
    }

    public void UpdateCashText()
    {
        cashText.text = ": " + cash.ToString();
    }

    private void CheckAndIncreaseCash()
    {
        if (followers % 10 == 0)
        {
            cash++;
            UpdateCashText();
        }
    }

    public void AddCash(int amount)
    {
        cash += amount;
        UpdateCashText();
    }

    public int GetCash()
    {
        return cash;
    }

    public void AddFollowers(int amount)
    {
        followers += amount;
        UpdateFollowersText();
        CheckAndIncreaseCash();
    }
}
