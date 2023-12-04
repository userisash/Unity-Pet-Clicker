using UnityEngine;

[CreateAssetMenu(fileName = "New Milestone", menuName = "Milestone")]
public class Milestone : ScriptableObject
{
    [Header("Milestone Details")]
    public string milestoneName = "New Milestone";
    public int requiredCash = 0;
    public int requiredViewers = 0;
    public int requiredFollowers = 0;

    [Header("Visuals")]
    public Sprite milestoneImage;
}
