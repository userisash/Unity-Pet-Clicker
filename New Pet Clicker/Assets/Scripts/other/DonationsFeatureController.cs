using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DonationsFeatureController : MonoBehaviour
{
    public ClickBehavior clickBehavior;
    public NotificationManager notificationManager;

    //public TextMeshProUGUI donationsFeatureText;

    private bool isDonationsFeatureUnlocked = false;

    // Update is called once per frame
    void Update()
    {
        if (!isDonationsFeatureUnlocked && clickBehavior.followers >= 10000)
        {
            UnlockDonationsFeature();
        }
    }

    private void UnlockDonationsFeature()
    {
        isDonationsFeatureUnlocked = true;
        notificationManager.AddNotification("Donations Feature is now unlocked!");
    }

    public void TryGenerateDonation()
    {
        if (isDonationsFeatureUnlocked && clickBehavior.followers % 10 == 0)
        {
            // 20% chance to generate a donation
            if (Random.Range(0, 5) == 0) // There's 1 in 5 chance for this to be true, which corresponds to 20%
            {
                int donationAmount = Random.Range(5, 11); // Generates a random donation between 5 and 10
                clickBehavior.AddCash(donationAmount);
            }
        }
    }
}
