using UnityEngine;

public class FollowerMilestoneRewards : MonoBehaviour
{
    public ClickBehavior clickBehavior;
    public NotificationManager notificationManager;
    public Milestone[] followerMilestones; // Assign your milestone scriptable objects here

    private int lastMilestoneIndex = 0;

    void Update()
    {
        CheckForMilestone();
    }

    private void CheckForMilestone()
    {
        int currentFollowers = clickBehavior.followers;

        // Iterate through milestones
        for (int i = lastMilestoneIndex; i < followerMilestones.Length; i++)
        {
            Milestone milestone = followerMilestones[i];

            if (currentFollowers >= milestone.requiredFollowers)
            {
                RewardForMilestone(milestone);
                lastMilestoneIndex = i + 1; // Move to the next milestone
            }
            else
            {
                break; // Stop checking for milestones if the current followers are not enough for the next milestone
            }
        }
    }

    private void RewardForMilestone(Milestone milestone)
    {
        int rewardAmount = milestone.requiredFollowers; // Reward amount is equal to the milestone reached
        clickBehavior.AddCash(rewardAmount);
        notificationManager.AddNotification($"Congratulations! You've reached {milestone.requiredFollowers} followers and received a gift of {rewardAmount} cash!", milestone.milestoneImage);
    }
}

