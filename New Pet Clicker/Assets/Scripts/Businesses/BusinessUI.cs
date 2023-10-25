using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BusinessUI : MonoBehaviour
{
    //public Image businessImage;
    //public TextMeshProUGUI businessNameText;
    //public Slider progressBar;
    //public Button actionButton;

    //private Business business;
    //private bool isFilling = false;

    //public void Setup(Business newBusiness, Transform parent)
    //{
    //    // Create UI elements dynamically.
    //    GameObject imageGO = new GameObject("Business Image");
    //    businessImage = imageGO.AddComponent<Image>();
    //    imageGO.transform.SetParent(parent, false);

    //    GameObject textGO = new GameObject("Business Name");
    //    businessNameText = textGO.AddComponent<TextMeshProUGUI>();
    //    textGO.transform.SetParent(parent, false);

    //    GameObject sliderGO = new GameObject("Progress Bar");
    //    progressBar = sliderGO.AddComponent<Slider>();
    //    sliderGO.transform.SetParent(parent, false);

    //    GameObject buttonGO = new GameObject("Action Button");
    //    actionButton = buttonGO.AddComponent<Button>();
    //    buttonGO.transform.SetParent(parent, false);

    //    business = newBusiness;
    //    businessImage.sprite = business.businessImage;
    //    businessNameText.text = business.businessName;
    //    progressBar.maxValue = business.fillTime;
    //    actionButton.onClick.AddListener(StartFilling);
    //}

    //private void Update()
    //{
    //    if (isFilling)
    //    {
    //        business.currentFillProgress += Time.deltaTime;
    //        progressBar.value = business.currentFillProgress;

    //        if (business.currentFillProgress >= business.fillTime)
    //        {
    //            isFilling = false;
    //            business.currentFillProgress = 0;
    //            // Grant reward to the player.
    //            FindObjectOfType<ClickBehavior>().AddCash(business.rewardAmount);
    //        }
    //    }
    //}

    //private void StartFilling()
    //{
    //    if (!isFilling)
    //    {
    //        isFilling = true;
    //    }
    //}
}
