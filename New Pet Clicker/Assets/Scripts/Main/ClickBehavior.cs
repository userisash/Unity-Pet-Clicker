using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI; 

public class ClickBehavior : MonoBehaviour
{
    public int views = 0;
    public int followers = 0;
    public int cash = 0;
    public int coins = 0;

    public DonationsFeatureController donationsFeatureController;
    public NotificationManager notificationManager;

    public GameObject flyingNumberPrefab; // Drag your created prefab here
    public Transform uiCanvasTransform;   // Drag your canvas or a panel inside the canvas here
    public TextMeshProUGUI viewsText;
    public TextMeshProUGUI followersText;
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI coinsText;

    private int viewsPerClick = 1;
    private int followersPerClick = 1; // for future upgrades
    private int cashPerClick = 1;      // for future upgrades
    private bool hasReachedGoal = false;

    private int lastAwardedFollowersAtViews = 0;


    public void OnButtonClick()
    {
        int randomChance = Random.Range(1, 1000); // generates a random number between 1 and 150 inclusive.

        if (randomChance == 90) // you can choose any number between 1 and 150, I chose 75 as an example.
        {
            //views += 1000;
            //CheckCounters();
        }
        else
        {
            IncrementViews();
        }

        UpdateAllText();
    }
    

    public void IncrementViews()
    {
        views += viewsPerClick;
        ShowFlyingNumberEffect(viewsPerClick);

        // Check if followers should be incremented
        CheckCounters();
    }

    public void IncrementFollowers()
    {
        int previousFollowers = followers;
        followers += followersPerClick;

        // If followers increased by at least 10, attempt to generate donation
        if ((followers / 10) > (previousFollowers / 10))
        {
            donationsFeatureController.TryGenerateDonation();
        }

        CheckCounters();
    }


    public void IncrementCash()
    {
        //if (hasReachedGoal)
        //{
        //    cash += cashPerClick;
        //}
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateAllText();
    }

    public void CheckCounters()
    {
        if(followers >= 1000)
        {
            hasReachedGoal = true;
            notificationManager.AddNotification("Goal reached! Followers: 1000");
        }
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
        numberText.text = $"+{incrementValue}";

       

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
