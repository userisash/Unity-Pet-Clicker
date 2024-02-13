using UnityEngine;

[CreateAssetMenu(fileName = "New Course", menuName = "Course")]
public class CourseSO : ScriptableObject
{
    public string courseTitle;
    public int cost;
    public SkillSO associatedSkill;
    public int xpReward;
    public string rewardDescription; // Short description of the reward
}
