using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class SponsorshipManager : MonoBehaviour
{
    public ClickBehavior clickBehavior; // Reference to ClickBehavior script
    public GameObject sponsorshipPanel; // Reference to the sponsorship panel UI
    public TextMeshProUGUI descriptionText; // Text component to show deal description
    public Button acceptButton; // Accept button
    public Button rejectButton; // Reject button
    public TextMeshProUGUI timerText; // Text component for the countdown timer
    public TextMeshProUGUI cashReceivedTooltip; // Tooltip for cash received from sponsors

    private bool isDealActive = false;
    private bool isFirstDeal = true;
    private float dealDuration = 300f; // 5 minutes
    private float timeRemaining;
    private int cashPerDeal;
    private Coroutine dealCoroutine;

    void Start()
    {
        sponsorshipPanel.SetActive(false);
        acceptButton.onClick.AddListener(AcceptDeal);
        rejectButton.onClick.AddListener(RejectDeal);
    }

    void Update()
    {
        if (isDealActive)
        {
            UpdateTimer();
        }

        // Check for first deal when player hits 10000 followers
        if (isFirstDeal && clickBehavior.followers >= 10000)
        {
            PresentFirstDeal();
        }
    }

    private void PresentFirstDeal()
    {
        isFirstDeal = false;
        PrepareAndShowDeal();
    }

    private void PrepareAndShowDeal()
    {
        cashPerDeal = clickBehavior.followers / 100;
        descriptionText.text = $"Receive {cashPerDeal} cash over 5 minutes.";
        sponsorshipPanel.SetActive(true);
    }

    private IEnumerator WaitForNextDeal()
    {
        float waitTime = Random.Range(120f, 600f); // Random time between 2 to 10 minutes
        yield return new WaitForSeconds(waitTime);
        PrepareAndShowDeal();
    }

    private void AcceptDeal()
    {
        isDealActive = true;
        timeRemaining = dealDuration;
        dealCoroutine = StartCoroutine(DealDuration());
        timerText.gameObject.SetActive(true);
    }

    private void RejectDeal()
    {
        sponsorshipPanel.SetActive(false);
        StartCoroutine(WaitForNextDeal());
    }

    private IEnumerator DealDuration()
    {
        while (timeRemaining > 0)
        {
            yield return new WaitForSeconds(60); // Distribute cash every minute
            DistributeCash();
            timeRemaining -= 60;
        }
        CompleteDeal();
    }

    private void DistributeCash()
    {
        int cashToDistribute = Mathf.Min(cashPerDeal / 5, cashPerDeal);

        Debug.Log("Adding 1MIN cash");
        clickBehavior.AddCash(cashToDistribute);
        cashPerDeal -= cashToDistribute;
    }

    private void CompleteDeal()
    {
        Debug.Log("Adding last min cash");
        clickBehavior.AddCash(cashPerDeal); // Add any remaining cash
        if (dealCoroutine != null)
        {
            StopCoroutine(dealCoroutine);
        }
        isDealActive = false;
        sponsorshipPanel.SetActive(false);
        //UpdateSponsorshipCashTooltip();
        timerText.gameObject.SetActive(false); // Disable the timer text
        StartCoroutine(WaitForNextDeal()); // Start waiting for the next deal
    }

    private void UpdateTimer()
    {
        if (timeRemaining > 0)
        {
            timerText.text = $"{timeRemaining:0} seconds remaining";
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            CompleteDeal();
        }
    }

    private string FormatTime(float time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        return $"{minutes:00}:{seconds:00}";
    }

    //public void UpdateSponsorshipCashTooltip()
    //{
    //    Debug.Log("calculating cash");
    //    // Update the tooltip with the total cash received from sponsors
    //    cashReceivedTooltip.text = $"Total Cash from Sponsors: {clickBehavior.GetCash()}";
    //}
}
