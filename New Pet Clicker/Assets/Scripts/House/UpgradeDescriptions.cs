using UnityEngine;
using System.Collections.Generic;

public class UpgradeDescriptions : MonoBehaviour
{
    // List of all possible upgrade descriptions
    private List<string> allDescriptions = new List<string>
    {
        // Add your 100 descriptions here
        "New paint", "Better windows", "Modern kitchen", "Home brewery",
        "Revolving bookcase door", "Zipline into the pool", "Secret passages",
        "In-home waterfall", "Backyard observatory", "Floating bed", "Jellyfish tank",
        "Slide staircase", "Rain shower head", "Geothermal heating system", "Rock climbing wall",
        "Fresh paint", "Solar panels", "Landscaped garden", "Hardwood floors", "Modern kitchen"
    };

    // Method to get 10 random descriptions
    public List<string> GetRandomDescriptions(int count = 10)
    {
        List<string> selectedDescriptions = new List<string>();
        List<string> tempDescriptions = new List<string>(allDescriptions); // Copy to a temporary list

        for (int i = 0; i < count && tempDescriptions.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, tempDescriptions.Count);
            selectedDescriptions.Add(tempDescriptions[randomIndex]);
            tempDescriptions.RemoveAt(randomIndex); // Remove to avoid duplicates
        }

        return selectedDescriptions;
    }
}
