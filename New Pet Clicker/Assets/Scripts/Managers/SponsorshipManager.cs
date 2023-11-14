using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class SponsorshipManager : MonoBehaviour
{
    public GameObject sponsorshipDealPanel; // Only one panel for deals
    public TextMeshProUGUI sponsorshipDealText;
    public TextMeshProUGUI totalCashFromSponsorsText; // Tooltip text
    public TextMeshProUGUI dealTimerText;

    public NotificationManager notificationManager;
    public ClickBehavior clickBehavior;

    private SponsorshipDeal activeDeal; // Only one active deal at a time
    private float nextDealTimer;
    private const float MIN_DEAL_INTERVAL = 120f; // Minimum 2 minutes
    private const float MAX_DEAL_INTERVAL = 600f; // Maximum 10 minutes

    private int totalCashFromSponsors = 0;
    private bool sponsorshipUnlocked = false;

    private void Update()
    {
        if (sponsorshipUnlocked)
        {
            // Handle active deal
            if (activeDeal != null)
            {
                HandleActiveDeal();
            }
            else
            {
                // Wait for next deal
                nextDealTimer -= Time.deltaTime;
                if (nextDealTimer <= 0)
                {
                    PresentSponsorshipOffer();
                }
            }
        }
    }

    private void PresentSponsorshipOffer()
    {
        // Set up a new deal but don't start it yet
        activeDeal = GenerateNewDeal();
        sponsorshipDealText.text = $"Earn ${activeDeal.CashAmount} over {activeDeal.Duration} seconds!";
        sponsorshipDealPanel.SetActive(true);
        // Don't start the timer or show it yet
    }


    private void HandleActiveDeal()
    {
        if (activeDeal != null && dealTimerText.gameObject.activeInHierarchy)
        {
            activeDeal.Timer -= Time.deltaTime;
            dealTimerText.text = $"Time Remaining: {activeDeal.Timer:F2} s";

            if (activeDeal.Duration - activeDeal.Timer >= activeDeal.LastDistributionTime + 60f && !activeDeal.CashPerMinuteDistributed)
            {
                DistributeCashForDeal(activeDeal);
                activeDeal.LastDistributionTime = activeDeal.Duration - activeDeal.Timer;
                activeDeal.CashPerMinuteDistributed = true;
            }

            if (activeDeal.Timer <= 0f)
            {
                EndDeal();
            }
        }
    }


    public void AcceptSponsorshipDeal()
    {
        // Start the deal when the player accepts
        dealTimerText.gameObject.SetActive(true);
        activeDeal.CashPerMinuteDistributed = false; // Reset flag for new deal
        activeDeal.LastDistributionTime = 0f; // Reset the last distribution time
        sponsorshipDealPanel.SetActive(false); // Close the panel
    }


    private SponsorshipDeal GenerateNewDeal()
    {
        return new SponsorshipDeal
        {
            Duration = 300f, // 5 minutes
            CashAmount = clickBehavior.followers / 100,
            Timer = 300f,
            CashDistributed = 0
        };
    }

    private void DistributeCashForDeal(SponsorshipDeal deal)
    {
        int cashThisMinute = CalculateCashPerMinute(deal.CashAmount, deal.Duration);
        int cashToDistribute = Mathf.Min(cashThisMinute, deal.CashAmount - deal.CashDistributed);

        clickBehavior.AddCash(cashToDistribute);
        deal.CashDistributed += cashToDistribute;
    }


    private int CalculateCashPerMinute(int totalCash, float duration)
    {
        return Mathf.CeilToInt((float)totalCash / (duration / 60f));
    }
    private void EndDeal()
    {
        int remainingCash = activeDeal.CashAmount - activeDeal.CashDistributed;
        if (remainingCash > 0)
        {
            clickBehavior.AddCash(remainingCash);
            activeDeal.CashDistributed += remainingCash;
        }

        activeDeal = null;
        dealTimerText.gameObject.SetActive(false);
        nextDealTimer = Random.Range(MIN_DEAL_INTERVAL, MAX_DEAL_INTERVAL); // Set timer for next deal
    }

    public void CheckAndUnlockSponsorship()
    {
        if (!sponsorshipUnlocked && clickBehavior.followers >= 10000)
        {
            sponsorshipUnlocked = true;
            notificationManager.AddNotification("Sponsorship deals unlocked!");
            nextDealTimer = 0; // Immediately present first deal
        }
    }

    // Additional methods...
}
