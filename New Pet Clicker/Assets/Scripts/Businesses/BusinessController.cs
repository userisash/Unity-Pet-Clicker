using UnityEngine;
using UnityEngine.UI;

public class BusinessController : MonoBehaviour
{
    public Business business;
    public ClickBehavior clickBehavior;

    private void Start()
    {
        InitializeBusiness();
    }

    private void Update()
    {
        UpdateBusiness();
    }

    public void Initialize(Business newBusiness, ClickBehavior clickBehaviorReference)
    {
        business = newBusiness;
        clickBehavior = clickBehaviorReference;

        // Set the UI Elements
        business.nameText.text = business.businessName;
        business.businessImg.sprite = business.businessImage;

        business.startBusinessButton.onClick.AddListener(StartBusiness);

        // Register with BusinessManager
        WorkManager.Instance.RegisterBusiness(this);
    }
    private void InitializeBusiness()
    {
        business.startBusinessButton.onClick.AddListener(StartBusiness);
    }

    private void StartBusiness()
    {
        // Check if there is enough energy to start the business
        if (EnergyController.Instance.energy.ConsumeEnergy(business.energyCost))
        {
            // Energy is consumed, proceed with business logic
            business.isRunning = true;
            business.timePassed = 0;
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
            }
        }
    }
}
