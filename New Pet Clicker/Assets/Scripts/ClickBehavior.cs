using UnityEngine;
using TMPro;

public class ClickBehavior : MonoBehaviour
{
    public TextMeshProUGUI viewsText;
    public TextMeshProUGUI followersText;
    public TextMeshProUGUI cashText;

    public int views = 0;
    public int followers = 0;
    public int cash = 10;
  
    private int viewsPerClick = 1;
    private int followersPerClick = 1;
    private int cashPerClick = 1;

    public void SetClickValues(int views, int followers, int cash)
    {
        viewsPerClick = views;
        followersPerClick = followers;
        cashPerClick = cash;
    }

    public void OnButtonClick()
    {
        IncrementViews();
    }

    private void IncrementViews()
    {
        views += viewsPerClick;
        UpdateViewsText();

        if (views % 10 == 0)
        {
            IncrementFollowers();
        }
    }

    private void IncrementFollowers()
    {
        followers += followersPerClick;
        UpdateFollowersText();

        if (followers % 10 == 0)
        {
            IncrementCash();
        }
    }

    private void IncrementCash()
    {
        cash += cashPerClick;
        UpdateCashText();
    }

    public void UpdateViewsText()
    {
        viewsText.text = " " + views.ToString();
    }

    public void UpdateFollowersText()
    {
        followersText.text = " " + followers.ToString();
    }

    public void UpdateCashText()
    {
        cashText.text = " " + cash.ToString();
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
    }
}
