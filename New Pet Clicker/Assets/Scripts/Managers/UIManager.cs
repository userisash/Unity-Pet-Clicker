
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public SkillUIComponent[] skillUIComponents;

    public void UpdateSkillUI(SkillSO skill)
    {
        foreach (var skillUI in skillUIComponents)
        {
            if (skillUI.skill == skill)
            {
                skillUI.xpBar.value = skill.GetRelativeXP();
                skillUI.levelText.text = "Level: " + skill.level.ToString();
            }
        }
    }

    [System.Serializable]
    public class SkillUIComponent
    {
        public SkillSO skill;
        public Slider xpBar;
        public TMP_Text levelText;
    }
}