using UnityEngine;
using UnityEngine.UI;

public class MonthlyExpenses : MonoBehaviour
{
    public ClickBehavior ClickBehavior; // Reference to the ClickBehavior script
    public Managers ManagerScript; // Reference to the Managers script to get tiers

    public int baseTaxCost = 100; // Base cost for taxes
    public int baseManagerCost = 50; // Base cost for a manager
    public float managerTierMultiplier = 1.5f; // Multiplier to increase manager cost with each tier

    [SerializeField] // This attribute makes the private variable editable from the editor.
    private float timeToDeduct = 300f; // Time in seconds (5 minutes = 300 seconds by default). Changeable in the editor.

    public Slider timeSlider;

    private float currentTime;

    private void Start()
    {
        currentTime = timeToDeduct; // Initialize the current time.
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

        int totalManagerCost = 0;

        // Iterate over each manager to compute the cost based on their tiers.
        foreach (Manager manager in ManagerScript.managers)
        {
            totalManagerCost += (int)(baseManagerCost * Mathf.Pow(managerTierMultiplier, manager.currentTier));
        }

        int totalTaxCost = baseTaxCost; // This can be more complex if needed.

        int totalMonthlyExpenses = totalManagerCost + totalTaxCost;

        ClickBehavior.AddCash(-totalMonthlyExpenses); // Deducting the expenses from cash
    }

    private void UpdateSliderValue()
    {
        timeSlider.value = currentTime / timeToDeduct; // This will normalize the value between 0 and 1.
    }
}
