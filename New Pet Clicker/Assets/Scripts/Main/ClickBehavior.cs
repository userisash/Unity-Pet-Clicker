using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class ClickBehavior : MonoBehaviour
{
    public NumbersManager numbersManager;
    public int views = 0;
    public int monthlyViews = 0;
    public int allTimeViews = 0;
    public int followers = 0;
    public int cash = 0;
    public int coins = 0;
    private int lastCashAwardAtFollowers = 0;


    public DonationsFeatureController donationsFeatureController;
    public NotificationManager notificationManager;
    public SponsorshipManager sponsorshipManager;
    public HappinessBar happinessBar;


    public GameObject flyingNumberPrefab; // Drag your created prefab here
    public Transform uiCanvasTransform;   // Drag your canvas or a panel inside the canvas here

    public TextMeshProUGUI viewsText;
    public TextMeshProUGUI monthlyViewsText;
    public TextMeshProUGUI allTimeViewsText;
    public TextMeshProUGUI followersText;
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI cashTooltip;

    private int viewsPerClick = 1;
    private int followersPerClick = 1; // for future upgrades


    private int lastAwardedFollowersAtViews = 0;

    void Start()
    {
        if (sponsorshipManager == null)
        {
            sponsorshipManager = FindObjectOfType<SponsorshipManager>();
        }

        // Initialize with data from NumbersManager
        if (NumbersManager.Instance != null)
        {
            views = NumbersManager.Instance.GetViews();
            followers = NumbersManager.Instance.GetFollowers();
            monthlyViews = NumbersManager.Instance.GetMonthlyViews();
            allTimeViews = NumbersManager.Instance.GetAllTimeViews();
            cash = NumbersManager.Instance.GetCash();
            coins = NumbersManager.Instance.GetCash();
        }
    }
    public void OnButtonClick()
    {
        int randomChance = Random.Range(1, 1000); // generates a random number between 1 and 150 inclusive.

        if (randomChance == 90) // you can choose any number between 1 and 150, I chose 75 as an example.
        {
            views += 1000;
            CheckCounters();
        }
        else
        {

            IncrementViews();
            ShowFlyingNumberEffect(viewsPerClick);

        }

        UpdateAllText();
        happinessBar.DecreaseHappinessOnButtonClick();
    }


    public void IncrementViews()
    {
        views += viewsPerClick;
        NumbersManager.Instance.UpdateViews(views);


        // Check if followers should be incremented
        CheckCounters();
    }

    public void IncrementFollowers()
    {
        int previousFollowers = followers;
        followers += followersPerClick;
        NumbersManager.Instance.UpdateFollowers(followers);

        // If followers increased by at least 10, attempt to generate a donation
        if ((followers / 10) > (previousFollowers / 10))
        {
            donationsFeatureController.TryGenerateDonation();
        }

        CheckAndAwardCashForFollowers(); // Check for cash award based on follower count
        UpdateAllText();
        CheckCounters();
    }



    public void IncrementCash()
    {
        NumbersManager.Instance.UpdateCash(cash);
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        NumbersManager.Instance.UpdateCoins(coins);
        UpdateAllText();
    }

    public void CheckAndAwardCashForFollowers()
    {
        if (followers >= 500)
        {
            // Determine the cash award based on every 1000 followers
            int cashAward = (followers / 1000) * Random.Range(2, 5); // For every 1000 followers, award between 3 to 5 cash

            // Optionally, you can limit the cash award to only happen once per certain threshold or every time they pass another 1000 followers
            // This is an example to give cash once per 1000 followers increment
            if (lastCashAwardAtFollowers / 1000 < followers / 1000)
            {
                AddCash(cashAward);
                lastCashAwardAtFollowers = followers;
                // Optionally, show a notification for receiving cash
                notificationManager?.AddNotification($"Earned {cashAward} cash from followers!");
            }
        }
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



    }

    public void ShowFlyingNumberEffect(int incrementValue)
    {
        // Instantiate the number
        Vector3 mousePos = Input.mousePosition;
        GameObject numberInstance = Instantiate(flyingNumberPrefab, mousePos, Quaternion.identity, uiCanvasTransform);
        TextMeshProUGUI numberText = numberInstance.GetComponent<TextMeshProUGUI>();
        numberText.text = $"+{FormatNumber(incrementValue)}";



        // Start the move animation
        StartCoroutine(MoveNumberToTarget(numberInstance.transform, viewsText.transform.position, 2f));
    }

    IEnumerator MoveNumberToTarget(Transform numberTransform, Vector3 targetPosition, float duration)
    {
        float elapsed = 0f;
        Vector3 startingPosition = numberTransform.position;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            numberTransform.position = Vector3.Lerp(startingPosition, targetPosition, elapsed / duration);
            yield return null;
        }

        Destroy(numberTransform.gameObject);
    }



    public void UpdateAllText()
    {
        viewsText.text = FormatNumber(views);
        followersText.text = FormatNumber(followers);
        cashText.text = FormatNumber(cash);
        monthlyViewsText.text = FormatNumber(monthlyViews);
        allTimeViewsText.text = FormatNumber(allTimeViews);
    }

    public void AddToClickValues(int viewsIncrement, int followersIncrement, int cashIncrement)
    {
        viewsPerClick += viewsIncrement;
        followersPerClick += followersIncrement;
    }

    public int GetCash()
    {
        return cash;
    }

    public void AddCash(int amount)
    {
        Debug.Log("Adding Cash");
        cash += amount;
        UpdateAllText();
    }

    public void ResetAndUpdateViews()
    {
        NumbersManager.Instance.UpdateAllTimeViews(allTimeViews);
        allTimeViews += views; // Add current views to allTimeViews
        monthlyViews = views; // Set monthlyViews to the current views before reset
        views = 0; // Reset views
        UpdateAllText(); // Update UI

        lastAwardedFollowersAtViews = 0;
    }

    public void UpdateSponsorshipCashTooltip(int totalSponsorshipCash)
    {
        // Assuming you have a TextMeshProUGUI element for the tooltip
        cashTooltip.text = $"Total Sponsorship Cash: {FormatNumber(totalSponsorshipCash)}";
    }


    public string FormatNumber(double number)
    {
        if (number < 1000)
        {
            return number.ToString("0");
        }
        else if (number < 1000000)
        {
            return (number / 1000).ToString("0.#") + "K";
        }
        else if (number < 1000000000)
        {
            return (number / 1000000).ToString("0.#") + "M";
        }
        else if (number < 1000000000000)
        {
            return (number / 1000000000).ToString("0.#") + "B";
        }
        else
        {
            return (number / 1000000000000).ToString("0.#") + "T";
        }
    }
}