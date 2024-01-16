using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MonthlyExpenses : MonoBehaviour
{
    public ClickBehavior ClickBehavior; // Reference to the ClickBehavior script
    public Managers ManagerScript; // Reference to the Managers script to get tiers

    public int baseTaxCost = 100; // Base cost for taxes
    public int baseManagerCost = 50; // Base cost for a manager
    public float managerTierMultiplier = 1.5f; // Multiplier to increase manager cost with each tier

    public float timeToDeduct = 30f; // Time in seconds (5 minutes = 300 seconds by default).

    public Slider timeSlider;
    public GameObject expensesPopup; // The entire popup
    public TextMeshProUGUI upcomingTotalExpensesText;
    public TextMeshProUGUI currentCashText;

    private float currentTime;

    private void Start()
    {
        currentTime = timeToDeduct; // Initialize the current time.
        UpdateUpcomingExpensesUI();
        UpdateCurrentCashUI();
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = timeToDeduct; // Reset the timer
            DeductExpenses();
            UpdateUpcomingExpensesUI(); // Update the UI after deducting expenses
        }

        UpdateSliderValue();
    }

    private void DeductExpenses()
    {
        int totalExpenses = CalculateUpcomingExpenses();
        ClickBehavior.AddCash(-totalExpenses); // Deducting the expenses from cash
        UpdateCurrentCashUI(); // Update current cash UI after deduction

        ResetViewsCounter(); // Reset views and update related values
    }

    private void ResetViewsCounter()
    {
        // Reset views and update allTimeViews and monthlyViews in ClickBehavior
        ClickBehavior.ResetAndUpdateViews();

        // Any additional logic related to views reset can be added here
    }
    private int CalculateUpcomingExpenses()
    {
        int totalManagerCost = 0;

        // Assuming each manager has a tier in the Managers script.
        foreach (var manager in ManagerScript.managers)
        {
            totalManagerCost += (int)(baseManagerCost * Mathf.Pow(managerTierMultiplier, manager.currentTier + 1));
        }

        int totalTaxCost = baseTaxCost;
        return totalManagerCost + totalTaxCost;
    }

    private void UpdateUpcomingExpensesUI()
    {
        int totalMonthlyExpenses = CalculateUpcomingExpenses();
        upcomingTotalExpensesText.text = "Total: $" + totalMonthlyExpenses.ToString();
    }

    private void UpdateCurrentCashUI()
    {
        Debug.Log("Updating cash display: " + ClickBehavior.GetCash()); // Debug line
        currentCashText.text = "$" + ClickBehavior.GetCash().ToString();
    }


    private void UpdateSliderValue()
    {
        timeSlider.value = currentTime / timeToDeduct; // This will normalize the value between 0 and 1.
    }
}
