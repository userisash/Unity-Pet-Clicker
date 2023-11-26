using UnityEngine;
using UnityEngine.UI;

public class ButtonSpawner : MonoBehaviour
{
    public GameObject buttonPrefab; // Assign the DynamicButton prefab in the Inspector
    public Transform buttonParent; // Assign a parent Transform for new buttons in the Inspector
    public int buttonCount = 5; // Number of buttons to spawn

    public void SpawnButtons()
    {
        Debug.Log("Spawning buttons. Prefab: " + buttonPrefab + ", Parent: " + buttonParent);

        for (int i = 0; i < buttonCount; i++)
        {
            GameObject newButton = Instantiate(buttonPrefab, buttonParent);
            Debug.Log("Button instantiated: " + newButton);

            if (newButton.GetComponentInChildren<Text>() != null)
            {
                newButton.GetComponentInChildren<Text>().text = "Button " + (i + 1);
            }
            else
            {
                Debug.LogError("Text component not found in button prefab");
            }
        }
    }

}
