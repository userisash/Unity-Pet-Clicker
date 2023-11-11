using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class SponsorshipManager : MonoBehaviour
{
    public GameObject sponsorshipDealPanelType1;
    public GameObject sponsorshipDealPanelType2;
    public TextMeshProUGUI sponsorshipDealTextType1;
    public TextMeshProUGUI sponsorshipDealTextType2;
    public TextMeshProUGUI totalCashFromSponsorsText; // Tooltip text
    public TextMeshProUGUI dealTimerText;
    public TextMeshProUGUI activeDealsCounterText;

    public NotificationManager notificationManager;
    public ClickBehavior clickBehavior;

    private List<SponsorshipDeal> activeDeals = new List<SponsorshipDeal>();
    private SponsorshipType currentDealType;

    private float sponsorshipTimer = 0f;
    private const float SPONSORSHIP_INTERVAL = 30f; // 5 minutes
    private const float MIN_SPONSORSHIP_INTERVAL = 180f; // e.g., 3 minutes
    private const float MAX_SPONSORSHIP_INTERVAL = 600f; // e.g., 10 minutes


    private int totalCashFromSponsors = 0;
    private float cashAccumulator = 0f;
    private bool exclusiveDealActive = false;
    private bool sponsorshipUnlocked = false;

    private void Update()
    {
        // Increment the timer
        sponsorshipTimer += Time.deltaTime;

        // Check if it's time to present a new sponsorship offer
        if (sponsorshipTimer >= SPONSORSHIP_INTERVAL)
        {
            PresentSponsorshipOffer();
            sponsorshipTimer = 0f; // Reset the timer
        }

        // Update timers and handle deal expiration
        exclusiveDealActive = false;
        for (int i = activeDeals.Count - 1; i >= 0; i--)
        {
            var deal = activeDeals[i];
            deal.Timer -= Time.deltaTime;
            if (deal.Timer <= 0)
            {
                activeDeals.RemoveAt(i); // Deal expired
            }
            else if (deal.Type == SponsorshipType.Exclusive)
            {
                exclusiveDealActive = true;
            }

            DistributeCashFromActiveDeals();
            // Update UI for deal timers
            UpdateDealTimerUI(deal);
        }

        // Update the active deals counter
        activeDealsCounterText.text = activeDeals.Count.ToString();

        // Distribute cash from active deals
        DistributeCashFromActiveDeals();

        // Update total cash from sponsors tooltip
        totalCashFromSponsorsText.text = $"Total Cash from Sponsors: ${totalCashFromSponsors}";

    }

    private void PresentSponsorshipOffer()
    {
        // Logic to choose and present a sponsorship offer
        // Example: Randomly choose between exclusive and non-exclusive
        SponsorshipType type = (Random.value > 0.5f) ? SponsorshipType.Exclusive : SponsorshipType.NonExclusive;
        ShowSponsorshipDeal(type);
        sponsorshipTimer = Random.Range(MIN_SPONSORSHIP_INTERVAL, MAX_SPONSORSHIP_INTERVAL);
    }
    private void DistributeCashFromActiveDeals()
    {
        foreach (var deal in activeDeals)
        {
            cashAccumulator += CalculateCashDistribution(deal.CashAmount, deal.Duration);
            if (cashAccumulator >= 1f) // Only distribute when at least 1 unit of cash is accumulated
            {
                int cashToDistribute = Mathf.FloorToInt(cashAccumulator);
                Debug.Log($"Cash Accumulator: {cashAccumulator}");

                clickBehavior.AddCash(cashToDistribute);
                totalCashFromSponsors += cashToDistribute;
                cashAccumulator -= cashToDistribute;
            }
        }
    }


    private float CalculateCashDistribution(int totalCash, float duration)
    {
        // Calculate cash per second
        float cashPerSecond = (float)totalCash / duration;

        // Distribute cash for the current frame
        return cashPerSecond * Time.deltaTime;
    }

    public void ShowSponsorshipDeal(SponsorshipType type)
    {
        var newDeal = GenerateNewDeal(type);
        var panel = type == SponsorshipType.Exclusive ? sponsorshipDealPanelType1 : sponsorshipDealPanelType2;
        var text = type == SponsorshipType.Exclusive ? sponsorshipDealTextType1 : sponsorshipDealTextType2;

        text.text = $"Earn ${newDeal.CashAmount} over {newDeal.Duration} seconds!";
        panel.SetActive(true);
    }

    private SponsorshipDeal GenerateNewDeal(SponsorshipType type)
    {
        return new SponsorshipDeal
        {
            Type = type,
            Duration = 30f, // 5 minutes
            CashAmount = clickBehavior.followers / 100, // Example calculation
            Timer = 30f
        };
    }

    public void SetCurrentDealType(SponsorshipType type)
    {
        currentDealType = type;
    }

    public void AcceptSponsorshipDeal()
    {
        if (currentDealType == SponsorshipType.Exclusive && exclusiveDealActive)
        {
            // Prevent accepting another exclusive deal
            return;
        }

        var deal = GenerateNewDeal(currentDealType);
        activeDeals.Add(deal);
        // Hide the deal panel
        var panel = currentDealType == SponsorshipType.Exclusive ? sponsorshipDealPanelType1 : sponsorshipDealPanelType2;
        panel.SetActive(false);
    }


    private void UpdateDealTimerUI(SponsorshipDeal deal)
    {
        dealTimerText.text = $"Time Remaining: {deal.Timer:F2} s";
    }

    public void CheckAndUnlockSponsorship()
    {
        if (!sponsorshipUnlocked && clickBehavior.followers >= 10000)
        {
            sponsorshipUnlocked = true;
            notificationManager.AddNotification("Sponsorship deals unlocked!");
            // You can also add logic here to show the first sponsorship deal
            sponsorshipTimer = Random.Range(MIN_SPONSORSHIP_INTERVAL, MAX_SPONSORSHIP_INTERVAL);
        }
    }
    // Add other necessary methods...
}
