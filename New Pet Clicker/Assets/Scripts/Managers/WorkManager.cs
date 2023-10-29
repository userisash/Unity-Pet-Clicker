using UnityEngine;
using System.Collections.Generic;
public class WorkManager : MonoBehaviour
{
    // Singleton instance
    public static WorkManager Instance { get; private set; }

    private List<BusinessController> businessControllers = new List<BusinessController>();

    private void Awake()
    {
        // Singleton pattern initialization
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void RegisterBusiness(BusinessController businessController)
    {
        businessControllers.Add(businessController);
    }

    private void Update()
    {
        foreach (var business in businessControllers)
        {
            business.UpdateBusiness();
        }
    }
}

