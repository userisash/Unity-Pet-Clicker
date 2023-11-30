using UnityEngine;

public class FollowerMilestoneRewards : MonoBehaviour
{
    public ClickBehavior clickBehavior;
    public NotificationManager notificationManager;
    private int lastMilestoneReached = 0;

    void Update()
    {
        CheckForMilestone();
    }

    private void CheckForMilestone()
    {
        int currentFollowers = clickBehavior.followers;
        int nextMilestone = CalculateNextMilestone(lastMilestoneReached);

        if (currentFollowers >= nextMilestone)
        {
            RewardForMilestone(nextMilestone);
            lastMilestoneReached = nextMilestone;
        }
    }

    private int CalculateNextMilestone(int lastMilestone)
    {
        if (lastMilestone == 0) return 10; // First milestone

        return lastMilestone * 10; // Next milestone is 10 times the last one
    }

    private void RewardForMilestone(int milestone)
    {
        int rewardAmount = milestone; // Reward amount is equal to the milestone reached
        clickBehavior.AddCash(rewardAmount);
        notificationManager.AddNotification($"Congratulations! You've reached {milestone} followers and received a gift of {rewardAmount} cash!");
    }
}
