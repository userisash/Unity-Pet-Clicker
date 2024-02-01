using UnityEngine;

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
                uiManager.UpdateSkillUI(course.associatedSkill);
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