
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
            courseObj.GetComponentInChildren<TMP_Text>().text = course.courseTitle;
            // Set up other course details like cost...

            Button purchaseButton = courseObj.GetComponentInChildren<Button>();
            CoursePurchaseManager purchaseManager = FindObjectOfType<CoursePurchaseManager>();
            if (purchaseButton != null && purchaseManager != null)
            {
                purchaseButton.onClick.AddListener(() => purchaseManager.PurchaseCourse(course));
            }
        }
    }
}