using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class BusinessController : MonoBehaviour
{
    public Business business;
    private ClickBehavior clickBehavior;
    private bool isRunning;
    private float timePassed = 0;

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

    public void StartBusiness()
    {
        if (!isRunning)
        {
            isRunning = true;
            timePassed = 0;
        }
    }

    public void UpdateBusiness()
    {
        if (isRunning)
        {
            timePassed += Time.deltaTime;

            business.progressBar.value = timePassed / business.fillDuration;

            if (timePassed >= business.fillDuration)
            {
                clickBehavior.AddCash(business.cashReward);
                business.progressBar.value = 0;
                isRunning = false;
            }
        }
    }
}

