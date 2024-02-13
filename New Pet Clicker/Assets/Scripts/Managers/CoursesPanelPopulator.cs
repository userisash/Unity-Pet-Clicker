using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoursesPanelPopulator : MonoBehaviour
{
    public GameObject coursePrefab;
    public Transform beginnerTabContent;
    public Transform intermediateTabContent;
    public Transform advancedTabContent;

    public CourseSO[] beginnerCourses; // Assign in the Inspector
    public CourseSO[] intermediateCourses; // Assign in the Inspector
    public CourseSO[] advancedCourses; // Assign in the Inspector

    void Start()
    {
        PopulateCourses(beginnerCourses, beginnerTabContent);
        PopulateCourses(intermediateCourses, intermediateTabContent);
        PopulateCourses(advancedCourses, advancedTabContent);
    }

    void PopulateCourses(CourseSO[] courses, Transform tabContent)
    {
        foreach (var course in courses)
        {
            GameObject courseObj = Instantiate(coursePrefab, tabContent);

            // Set the course title
            TMP_Text titleText = courseObj.transform.Find("TitleText").GetComponent<TMP_Text>();
            titleText.text = course.courseTitle;

            // Set the cost
            TMP_Text costText = courseObj.transform.Find("Price").GetComponent<TMP_Text>();
            costText.text = course.cost.ToString();


            // Set up the buy button
            Button buyButton = courseObj.transform.Find("BuyButton").GetComponent<Button>();
            buyButton.onClick.AddListener(() => PurchaseCourse(course));

            // set up the associated skill image
            Image skillImage = courseObj.transform.Find("SkillImage").GetComponent<Image>();
            skillImage.sprite = course.associatedSkill.skillIcon;

        }
    }

    void PurchaseCourse(CourseSO course)
    {
        CoursePurchaseManager purchaseManager = FindObjectOfType<CoursePurchaseManager>();
        if (purchaseManager != null)
        {
            purchaseManager.PurchaseCourse(course);
        }
    }
}
