using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonthlyExpenses : MonoBehaviour
{
    public ClickBehavior ClickBehavior; // Reference to the ClickBehavior script
    public Managers ManagerScript; // Reference to the Managers script to get tiers

    public int baseTaxCost = 100; // Base cost for taxes
    public int baseManagerCost = 50; // Base cost for a manager
    public float managerTierMultiplier = 1.5f; // Multiplier to increase manager cost with each tier

    [SerializeField] // This attribute makes the private variable editable from the editor.
    private float timeToDeduct = 30f; // Time in seconds (5 minutes = 300 seconds by default). Changeable in the editor.

    public Slider timeSlider;
    public GameObject expensesPopup; // The entire popup
    public TextMeshProUGUI upcomingManagerExpensesText;
    public TextMeshProUGUI upcomingTaxExpensesText;
    public TextMeshProUGUI upcomingTotalExpensesText;

    public TextMeshProUGUI currentCashText;


    private float currentTime;

    private void Start()
    {
        currentTime = timeToDeduct; // Initialize the current time.
        CalculateUpcomingExpenses();
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            DeductExpenses();
            currentTime = timeToDeduct; // Reset the timer
        }

        UpdateSliderValue();
    }

    private void DeductExpenses()
    {
        if (ClickBehavior.GetCash() <= 0)
        {
            return; // Exit the method early if cash is 0 or below.
        }

        int totalExpenses = CalculateUpcomingExpenses();

        ClickBehavior.AddCash(-totalExpenses);  // Deducting the expenses from cash
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

        int totalMonthlyExpenses = totalManagerCost + totalTaxCost;

        upcomingManagerExpensesText.text = "Upcoming Manager Expenses: $" + totalManagerCost.ToString();
        upcomingTaxExpensesText.text = "Upcoming Tax: $" + totalTaxCost.ToString();
        upcomingTotalExpensesText.text = "Upcoming Total: $" + totalMonthlyExpenses.ToString();

        return totalMonthlyExpenses;
    }

    private void UpdateSliderValue()
    {
        timeSlider.value = currentTime / timeToDeduct; // This will normalize the value between 0 and 1.
    }
}
