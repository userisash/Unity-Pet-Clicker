using UnityEngine;
using TMPro;

public class ClickBehavior : MonoBehaviour
{
    public TextMeshProUGUI viewsText;
    public TextMeshProUGUI followersText;
    public TextMeshProUGUI cashText;

    private int views = 0;
    private int followers = 0;
    private int cash = 20;

    private int viewsPerClick = 1;
    private int followersPerClick = 1; // for future upgrades
    private int cashPerClick = 1;      // for future upgrades

    private int lastAwardedFollowersAtViews = 0;
    private int lastAwardedCashAtFollowers = 0;


    public void OnButtonClick()
    {
        IncrementViews();
        UpdateAllText();
    }

    private void IncrementViews()
    {
        views += viewsPerClick;

        // Check if followers should be incremented
        CheckCounters();
    }

    private void IncrementFollowers()
    {
        followers += followersPerClick;

        // Check if cash should be incremented
        CheckCounters();
    }

    private void IncrementCash()
    {
        cash += cashPerClick;
    }

    public void CheckCounters()
    {
        int maxIterations = 10; // for safety

        int iterations = 0;
        while ((views - lastAwardedFollowersAtViews) >= 10 && iterations < maxIterations)
        {
            lastAwardedFollowersAtViews += 10;
            IncrementFollowers();
            iterations++;
        }

        iterations = 0;
        while ((followers - lastAwardedCashAtFollowers) >= 10 && iterations < maxIterations)
        {
            lastAwardedCashAtFollowers += 10;
            IncrementCash();
            iterations++;
        }
    }


    private void UpdateAllText()
    {
        viewsText.text = views.ToString();
        followersText.text = followers.ToString();
        cashText.text = cash.ToString();
    }

    public void AddToClickValues(int viewsIncrement, int followersIncrement, int cashIncrement)
    {
        viewsPerClick += viewsIncrement;
        followersPerClick += followersIncrement;
        cashPerClick += cashIncrement;
    }


    public int GetCash()
    {
        return cash;
    }

    public void AddCash(int amount)
    {
        cash += amount;
        UpdateAllText();
    }
}
