using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]
public class SkillSO : ScriptableObject
{
    public string skillName;
    public int level;
    public int xp; // Current absolute XP points
    public int xpRequiredForNextLevel;
    public Sprite skillIcon;
    public GameObject skillUIPrefab; // Reference to the UI prefab

    // Define an event for XP change
    public event System.Action OnXPChanged;
    public event System.Action OnLevelUp;

    // Calculates the relative XP value for the UI (0 to 1)
    public float GetRelativeXP()
    {
        return (float)xp / xpRequiredForNextLevel;
    }

    // Call this method to add XP and handle level up
    public void AddXP(int amount)
    {
        xp += amount;
        while (xp >= xpRequiredForNextLevel)
        {
            LevelUp();
        }

        // Trigger the XP changed event
        OnXPChanged?.Invoke();
    }

    private void LevelUp()
    {
        xp -= xpRequiredForNextLevel;
        level++;
        xpRequiredForNextLevel = CalculateXPRequirementForLevel(level);

        OnLevelUp?.Invoke(); // Invoke level up event
    }

    private int CalculateXPRequirementForLevel(int level)
    {
        // Example: Each level requires 20% more XP than the last
        return Mathf.FloorToInt(100 * Mathf.Pow(1.2f, level));
    }
}
