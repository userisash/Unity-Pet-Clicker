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
        if (sponsorshipUnlocked)
        {
            // Increment the timer
            sponsorshipTimer += Time.deltaTime;

            // Check if it's time to present a new sponsorship offer
            if (sponsorshipTimer >= SPONSORSHIP_INTERVAL)
            {
                PresentSponsorshipOffer();
                // Reset timer to a random value within the range
                sponsorshipTimer = Random.Range(MIN_SPONSORSHIP_INTERVAL, MAX_SPONSORSHIP_INTERVAL);
            }
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
        for (int i = activeDeals.Count - 1; i >= 0; i--)
        {
            var deal = activeDeals[i];

            // Check if a minute has passed since the last distribution and the deal is still active
            if (deal.Duration - deal.Timer >= deal.LastDistributionTime + 60f && deal.Timer > 0f)
            {
                DistributeCashForDeal(deal);
            }

            // Check if the deal has expired
            if (deal.Timer <= 0f && deal.CashDistributed < deal.CashAmount)
            {
                // Distribute remaining cash if any
                int remainingCash = deal.CashAmount - deal.CashDistributed;
                if (remainingCash > 0)
                {
                    clickBehavior.AddCash(remainingCash);
                    deal.CashDistributed += remainingCash;
                }
                activeDeals.RemoveAt(i); // Remove the expired deal
            }

            deal.Timer -= Time.deltaTime; // Update deal timer
        }
    }

    private void DistributeCashForDeal(SponsorshipDeal deal)
    {
        int cashThisMinute = CalculateCashPerMinute(deal.CashAmount, deal.Duration);
        int cashToDistribute = Mathf.Min(cashThisMinute, deal.CashAmount - deal.CashDistributed);

        clickBehavior.AddCash(cashToDistribute);
        deal.CashDistributed += cashToDistribute;
        deal.LastDistributionTime = deal.Duration - deal.Timer;
    }

    private int CalculateCashPerMinute(int totalCash, float duration)
    {
        return Mathf.CeilToInt((float)totalCash / (duration / 60f));
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
            Duration = 300f, // 5 minutes
            CashAmount = clickBehavior.followers / 100, // Example calculation
            Timer = 300f,
            CashDistributed = 0,
            LastDistributionTime = 0
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
        CloseCurrentDealPanel();
    }


    public void RejectSponsorshipDeal()
    {
        // Hide the deal panel
        CloseCurrentDealPanel();
    }


    private void CloseCurrentDealPanel()
    {
        if (currentDealType == SponsorshipType.Exclusive)
        {
            sponsorshipDealPanelType1.SetActive(false);
        }
        else
        {
            sponsorshipDealPanelType2.SetActive(false);
        }
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
