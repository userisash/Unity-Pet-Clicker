using UnityEngine;

public class PanelManager : MonoBehaviour
{
    private GameObject currentlyOpenPanel = null;

    public void TogglePanel(GameObject panelToToggle)
    {
        if (currentlyOpenPanel != null)
        {
            currentlyOpenPanel.SetActive(false); // Close the currently open panel
        }

        if (currentlyOpenPanel != panelToToggle)
        {
            panelToToggle.SetActive(true); // Open the new panel
            currentlyOpenPanel = panelToToggle; // Update the currently open panel
        }
        else
        {
            currentlyOpenPanel = null; // No panel is open
        }
    }
}
