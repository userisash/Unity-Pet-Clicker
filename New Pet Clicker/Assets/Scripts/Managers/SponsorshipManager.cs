using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SponsorshipTier
{
    public int followersRequired;
    public int cashReward;
    public bool isUnlocked = false;
    public Button unlockButton; // Reference to the unlock button
}

public class SponsorshipManager : MonoBehaviour
{
    public ClickBehavior clickBehavior;
    public GameObject tierPrefab; // Prefab for each tier
    public Transform contentTransform; // Content Transform of the ScrollView
    public List<SponsorshipTier> sponsorshipTiers;
    public GameObject panel; // Main panel that contains all tiers
    public TextMeshProUGUI timerText;

    private float rewardInterval = 300f; // 5 minutes
    private float timeRemaining;
    private bool timerStarted = false;

    void Start()
    {
        panel.SetActive(false); // Hide panel initially
        timerText.gameObject.SetActive(false);

        foreach (var tier in sponsorshipTiers)
        {
            CreateTierUI(tier);
        }
    }

    void Update()
    {
        if (timerStarted)
        {
            UpdateTimer();
        }

        UpdateTierInteractability();
    }

    private void CreateTierUI(SponsorshipTier tier)
    {
        GameObject tierInstance = Instantiate(tierPrefab, contentTransform);
        TextMeshProUGUI followersText = tierInstance.transform.Find("FollowersText").GetComponent<TextMeshProUGUI>();
        Button unlockButton = tierInstance.transform.Find("UnlockButton").GetComponent<Button>();
        TextMeshProUGUI rewardText = tierInstance.transform.Find("RewardText").GetComponent<TextMeshProUGUI>();

        followersText.text = $"{tier.followersRequired} Followers Required";
        rewardText.text = $"Reward: {tier.cashReward} cash per 5 minutes";

        unlockButton.onClick.AddListener(() => UnlockTier(tier));
        tier.unlockButton = unlockButton; // Store a reference to the unlock button
    }

    private void UpdateTierInteractability()
    {
        foreach (var tier in sponsorshipTiers)
        {
            tier.unlockButton.interactable = clickBehavior.followers >= tier.followersRequired && !tier.isUnlocked;
        }
    }

    private void UnlockTier(SponsorshipTier tier)
    {
        if (clickBehavior.followers >= tier.followersRequired && !tier.isUnlocked)
        {
            tier.isUnlocked = true;
            tier.unlockButton.interactable = false;
            if (!timerStarted)
            {
                StartTimer();
            }
        }
    }

    private void StartTimer()
    {
        timerStarted = true;
        timeRemaining = rewardInterval;
        timerText.gameObject.SetActive(true);
        StartCoroutine(RewardDistributionCoroutine());
    }

    private IEnumerator RewardDistributionCoroutine()
    {
        while (timerStarted)
        {
            yield return new WaitForSeconds(rewardInterval);
            DistributeRewards();
            timeRemaining = rewardInterval;
        }
    }

    private void DistributeRewards()
    {
        foreach (var tier in sponsorshipTiers)
        {
            if (tier.isUnlocked)
            {
                clickBehavior.AddCash(tier.cashReward);
            }
        }
    }

    private void UpdateTimer()
    {
        timeRemaining -= Time.deltaTime;
        timerText.text = $"{timeRemaining:0} seconds remaining";

        if (timeRemaining <= 0)
        {
            timeRemaining = rewardInterval;
        }
    }

    public void TogglePanelVisibility()
    {
        panel.SetActive(!panel.activeSelf);
    }
}
