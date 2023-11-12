using UnityEngine;

public class NumbersManager : MonoBehaviour
{
    public static NumbersManager Instance { get; private set; }

    // Store the data
    public int Views { get; private set; }
    public int Followers { get; private set; }
    public int MonthlyViews { get; private set; }
    public int AllTimeViews { get; private set; }
    public int Cash { get; private set; }
    public int Coins { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update methods
    public void UpdateViews(int newViews) => Views = newViews;
    public void UpdateFollowers(int newFollowers) => Followers = newFollowers;
    public void UpdateMonthlyViews(int newMonthlyViews) => MonthlyViews = newMonthlyViews;
    public void UpdateAllTimeViews(int newAllTimeViews) => AllTimeViews = newAllTimeViews;
    public void UpdateCash(int newCash) => Cash = newCash;
    public void UpdateCoins(int newCoins) => Coins = newCoins;

    // Retrieve methods
    public int GetViews() => Views;
    public int GetFollowers() => Followers;
    public int GetMonthlyViews() => MonthlyViews;
    public int GetAllTimeViews() => AllTimeViews;
    public int GetCash() => Cash;
    public int GetCoins() => Coins;
}


