using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class BusinessManager
{
    public string managerName;
    public int reward; // Reward player gets when the bar fills up
    public int cost;

    public BusinessController associatedBusinessController;

    [HideInInspector]
    public Business assignedBusiness;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI rewardText;
    public TextMeshProUGUI titleText;
    public Button purchaseButton;
    public Slider businessProgress;
    [HideInInspector]
    public bool isActive = false;

    public delegate void BusinessCompleteAction();
    [HideInInspector]
    public BusinessCompleteAction OnBusinessComplete;

    public float fillRateMultiplier = 1f;
}

public class BusinessManagers : MonoBehaviour
{
    public ClickBehavior ClickBehavior;
    public List<BusinessManager> businessManagers;

    private void Start()
    {
        foreach (var manager in businessManagers)
        {
            Debug.Log($"Manager: {manager.managerName}, Associated Controller: {manager.associatedBusinessController}, Business: {manager.associatedBusinessController?.business}");
            int reward = manager.associatedBusinessController.business.cashReward;
            manager.OnBusinessComplete = () => ClickBehavior.AddCash(reward);
        }
    }


    private void Update()
    {
        CheckButtonInteractibility();
    }

    public void PurchaseManager(int managerIndex)
    {
        BusinessManager manager = businessManagers[managerIndex];
        if (ClickBehavior.GetCash() >= manager.cost)
        {
            Debug.Log($"Purchased manager: {manager.managerName}. Starting business...");
            ClickBehavior.AddCash(-manager.cost);

            if (manager.isActive)
            {
                // If already active, stop the coroutine and restart it
                StopCoroutine(RunBusiness(manager));
            }

            StartCoroutine(RunBusiness(manager));
            manager.isActive = true;

            manager.cost *= 2; // Increase the cost for next time

            UpdateManagerTexts(manager);
        }
    }


    private IEnumerator RunBusiness(BusinessManager manager)
    {
        while (true)
        {
            float fillDuration = manager.associatedBusinessController.business.fillDuration / manager.fillRateMultiplier; // use multiplier to adjust duration
            Debug.Log($"Running business with fillDuration: {fillDuration}"); // Debug

            float timePassed = 0;
            while (timePassed < fillDuration)
            {
                manager.businessProgress.value = timePassed / fillDuration;
                timePassed += Time.deltaTime;
                yield return null;
            }

            Debug.Log($"Business completed for: {manager.managerName}"); // Debug

            manager.OnBusinessComplete?.Invoke();
            manager.businessProgress.value = 0;

            yield return new WaitForSeconds(fillDuration); // wait for the fill duration before starting again
        }
    }


    private void UpdateManagerTexts(BusinessManager manager)
    {
        manager.costText.text = "$:" + manager.cost.ToString();
        manager.rewardText.text = "+$:" + manager.reward.ToString();
        manager.titleText.text = manager.managerName;
    }

    private void CheckButtonInteractibility()
    {
        foreach (BusinessManager manager in businessManagers)
        {
            manager.purchaseButton.interactable = ClickBehavior.GetCash() >= manager.cost;
        }
    }
}