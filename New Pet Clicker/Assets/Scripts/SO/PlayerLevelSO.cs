using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerLevel", menuName = "Player Level")]
public class PlayerLevelSO : ScriptableObject
{
    public int level = 1;
    public int xp = 0;
    public int xpRequiredForNextLevel = 100;

    // Method to add XP
    public void AddXP(int amount)
    {
        xp += amount;
        if (xp >= xpRequiredForNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        xp -= xpRequiredForNextLevel;
        level++;
        xpRequiredForNextLevel = CalculateXPRequirementForLevel(level);

        // Optionally, invoke an event to notify the game of the player level-up
    }

    private int CalculateXPRequirementForLevel(int newLevel)
    {
        // Adjust this formula as needed for your game's balance
        return Mathf.FloorToInt(xpRequiredForNextLevel * 1.2f);
    }
}
