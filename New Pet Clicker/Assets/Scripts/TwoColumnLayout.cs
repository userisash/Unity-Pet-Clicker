using UnityEngine;
using UnityEngine.UI;

public class TwoColumnLayout : MonoBehaviour
{
    //public GameObject upgradeButtonPrefab; // Drag your button prefab here
    public int numberOfUpgrades = 10; // The total number of upgrade buttons you want

    private void Start()
    {
        PopulateUpgrades();
    }

    private void PopulateUpgrades()
    {
        for (int i = 0; i < numberOfUpgrades; i++)
        {
            //GameObject newButton = Instantiate(upgradeButtonPrefab, transform);
            // You can set button properties here if needed
        }
    }
}
