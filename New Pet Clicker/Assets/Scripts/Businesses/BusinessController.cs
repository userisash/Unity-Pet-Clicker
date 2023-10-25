using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class BusinessController : MonoBehaviour
{
    private Business business;
    private ClickBehavior clickBehavior;
    private bool isRunning;

    public void Initialize(Business newBusiness, ClickBehavior clickBehaviorReference)
    {
        business = newBusiness;
        clickBehavior = clickBehaviorReference;

        // Set the UI Elements
        business.nameText.text = business.businessName;
        business.businessImg.sprite = business.businessImage;

        business.startBusinessButton.onClick.AddListener(StartBusiness);
    }

    public void StartBusiness()
    {
        if (!isRunning)
        {
            StartCoroutine(RunBusiness());
        }
    }

    private IEnumerator RunBusiness()
    {
        isRunning = true;

        float timePassed = 0;
        while (timePassed < business.fillDuration)
        {
            business.progressBar.value = timePassed / business.fillDuration;
            timePassed += Time.deltaTime;
            yield return null;
        }

        clickBehavior.AddCash(business.cashReward);

        business.progressBar.value = 0;
        isRunning = false;
    }
}

