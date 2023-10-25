using UnityEngine;
using System.Collections.Generic;

public class BusinessPopup : MonoBehaviour
{
    public List<Business> allBusinesses; // This list contains data for each business
    public ClickBehavior clickBehavior;

    private void Start()
    {
        foreach (var business in allBusinesses)
        {
            // Assuming BusinessController is attached to the same GameObject as the business UI elements
            BusinessController controller = business.startBusinessButton.GetComponentInParent<BusinessController>();
            controller.Initialize(business, clickBehavior);
        }
    }
}
