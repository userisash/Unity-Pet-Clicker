using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Transform skillUIParent;  // Assign the parent for instantiated skill UIs
    public SkillSO[] skills;

    // Expose the progress bar image and level text for each skill
    public Image[] progressBarImages;
    public TMP_Text[] levelTexts;

    void Start()
    {
        InitializeSkillUI();
    }

    void InitializeSkillUI()
    {
        progressBarImages = new Image[skills.Length];
        levelTexts = new TMP_Text[skills.Length];

        for (int i = 0; i < skills.Length; i++)
        {
            var skill = skills[i];

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
            progressBarImages[i] = progressBarImage; // Store the progress bar image
            levelTexts[i] = levelText; // Store the level text
            UpdateSkillUI(skill, progressBarImage, levelText);
        }
    }

    public void UpdateSkillUI(SkillSO skill, Image progressBarImage, TMP_Text levelText)
    {
        // Update the fill amount of the XPBar directly
        progressBarImage.fillAmount = skill.GetRelativeXP();
        // Update the level text
        levelText.text = "Lvl " + skill.level.ToString();
    }

    // Method to get the progress bar image associated with a skill
    public Image GetProgressBarImage(SkillSO skill)
    {
        for (int i = 0; i < skills.Length; i++)
        {
            if (skills[i] == skill)
            {
                return progressBarImages[i];
            }
        }
        return null;
    }

    // Method to get the level text associated with a skill
    public TMP_Text GetLevelText(SkillSO skill)
    {
        for (int i = 0; i < skills.Length; i++)
        {
            if (skills[i] == skill)
            {
                return levelTexts[i];
            }
        }
        return null;
    }
}
