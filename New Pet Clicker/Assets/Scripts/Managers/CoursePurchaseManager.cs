using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoursePurchaseManager : MonoBehaviour
{
    private UIManager uiManager;
    public ClickBehavior playerStats;

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        // Other initialization code...
    }

    public void PurchaseCourse(CourseSO course)
    {
        if (course.associatedSkill == null || playerStats == null)
        {
            Debug.LogError("Setup Error");
            return;
        }

        if (playerStats.GetCash() >= course.cost)
        {
            // Deduct cost and update skill
            playerStats.AddCash(-course.cost);
            SkillSO skill = course.associatedSkill;
            skill.AddXP(course.xpReward);

            // Update UI
            if (uiManager != null)
            {
                // Find the Image component of the progress bar
                Image progressBarImage = uiManager.GetProgressBarImage(skill);

                // Find the TMP_Text component for the level text
                TMP_Text levelText = uiManager.GetLevelText(skill);

                // Update the skill UI
                uiManager.UpdateSkillUI(skill, progressBarImage, levelText);
            }
        }
        else
        {
            Debug.Log("Not enough cash to purchase this course.");
            // Optionally, provide feedback to the player
        }
    }

    private float CalculateNewXPRequirement(int level)
    {
        return Mathf.Floor(100 * Mathf.Pow(1.2f, level));
    }
}
