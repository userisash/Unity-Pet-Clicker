using UnityEngine;
using UnityEngine.UI;

public class BusinessController : MonoBehaviour
{
    public Business business;
    public ClickBehavior clickBehavior;

    private Button startBusinessButton; // Reference to the button component

    private void Start()
    {
        // Assuming the button is a child of the business GameObject
        startBusinessButton = GetComponentInChildren<Button>();
    }

    public void Initialize(Business newBusiness, ClickBehavior clickBehaviorReference)
    {
        business = newBusiness;
        clickBehavior = clickBehaviorReference;

        // Set the UI Elements
        business.nameText.text = business.businessName;
        business.businessImg.sprite = business.businessImage;

        // Add listener to the button
        business.startBusinessButton.onClick.AddListener(StartBusiness);

        // Register with BusinessManager
        WorkManager.Instance.RegisterBusiness(this);
    }

    private void StartBusiness()
    {
        // Check if there is enough energy to start the business
        if (EnergyController.Instance.energy.ConsumeEnergy(business.energyCost))
        {
            // Energy is consumed, proceed with business logic
            business.isRunning = true;
            business.timePassed = 0;

            // Disable the button while the business is running
            startBusinessButton.interactable = false;
        }
        else
        {
            // Not enough energy, display a message or take appropriate action
            Debug.LogWarning("Not enough energy to start the business!");
        }
    }

    public void UpdateBusiness()
    {
        if (business.isRunning)
        {
            business.timePassed += Time.deltaTime;

            business.progressBar.value = business.timePassed / business.fillDuration;

            if (business.timePassed >= business.fillDuration)
            {
                clickBehavior.AddCash(business.cashReward);
                business.progressBar.value = 0;
                business.isRunning = false;

                // Enable the button when the business is done
                startBusinessButton.interactable = true;
            }
        }
    }
}
