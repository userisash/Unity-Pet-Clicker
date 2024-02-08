using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Transform skillUIParent;  // Assign the parent for instantiated skill UIs
    public SkillSO[] skills;

    void Start()
    {
        foreach (var skill in skills)
        {
            // Instantiate the skill UI prefab
            GameObject skillUIObj = Instantiate(skill.skillUIPrefab, skillUIParent);

            // Assign the UI elements to the instantiated prefab
            Image skillImage = skillUIObj.transform.Find("SkillImage").GetComponent<Image>();
            TMP_Text titleText = skillUIObj.transform.Find("TitleText").GetComponent<TMP_Text>();
            TMP_Text levelText = skillUIObj.transform.Find("LevelText").GetComponent<TMP_Text>();

            // Find the XPBar Image component
            Image progressBarImage = skillUIObj.transform.Find("XPBar").GetComponent<Image>();

            // Initialize the UI elements with skill data
            skillImage.sprite = skill.skillIcon;
            titleText.text = skill.skillName;
            progressBarImage.fillAmount = skill.GetRelativeXP(); // Set the fill amount directly
            levelText.text = "Lvl " + skill.level.ToString();
        }
    }

    public void UpdateSkillUI(SkillSO skill)
    {
        foreach (var skillUIObj in skillUIParent.GetComponentsInChildren<Transform>())
        {
            // Find the skill UI object for the corresponding skill
            if (skillUIObj != null)
            {
                // Find the XPBar Image component
                Image progressBarImage = skillUIObj.transform.Find("XPBar").GetComponent<Image>();

                // Update the fill amount of the XPBar directly
                progressBarImage.fillAmount = skill.GetRelativeXP();
            }
        }
    }
}
