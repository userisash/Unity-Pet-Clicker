using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BusinessManager : MonoBehaviour
{
    public ClickBehavior clickBehavior; // Reference to the ClickBehavior to add cash
    public Business[] businesses; // An array of all businesses.
    public GameObject popupMenu; // The popup menu where businesses will be displayed.

    private void Start()
    {
        InitializeBusinesses();
        CloseMenu(); // Close the menu on game start.
    }

    void InitializeBusinesses()
    {
        foreach (Business business in businesses)
        {
            // Assign the UI components
            business.imageComponent.sprite = business.businessImage;
            business.titleComponent.text = business.businessName;

            // Add a click listener to the button.
            business.businessButton.onClick.AddListener(() => StartFill(business, business.progressBarComponent));
        }
    }

    void StartFill(Business business, Slider progressBar)
    {
        StartCoroutine(FillProgressBar(business, progressBar));
    }

    IEnumerator FillProgressBar(Business business, Slider progressBar)
    {
        float startTime = Time.time;

        while (Time.time - startTime <= business.fillTime)
        {
            business.currentFillAmount = (Time.time - startTime) / business.fillTime;
            progressBar.value = business.currentFillAmount;
            yield return null;
        }

        business.currentFillAmount = 0; // Reset the fill amount
        progressBar.value = 0; // Reset the progress bar

        // Reward the player with cash.
        clickBehavior.AddCash(business.rewardAmount);
    }

    public void ToggleMenu()
    {
        popupMenu.SetActive(!popupMenu.activeSelf);
    }

    void CloseMenu()
    {
        popupMenu.SetActive(false);
    }
}
