using UnityEngine;

public class PlayerLevelManager : MonoBehaviour
{
    public PlayerLevelSO playerLevel;
    public SkillSO[] skills;

    private void Start()
    {
        foreach (var skill in skills)
        {
            skill.OnLevelUp += HandleSkillLevelUp;
        }
    }

    private void OnDestroy()
    {
        foreach (var skill in skills)
        {
            skill.OnLevelUp -= HandleSkillLevelUp;
        }
    }

    private void HandleSkillLevelUp()
    {
        // Add XP to the player level every time a skill levels up
        playerLevel.AddXP(10); // Example value, adjust based on your game's balance
    }
}
