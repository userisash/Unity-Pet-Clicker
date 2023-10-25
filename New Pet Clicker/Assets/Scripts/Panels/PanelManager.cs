using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject[] panels; // Assign all panels here in the Inspector
    private GameObject currentlyOpenPanel = null;

    public void OpenPanel(GameObject panelToOpen)
    {
        // If there's already an open panel, close it
        if (currentlyOpenPanel != null && currentlyOpenPanel != panelToOpen)
        {
            currentlyOpenPanel.SetActive(false);
        }

        // Open the requested panel
        panelToOpen.SetActive(true);

        // Update the currently open panel
        currentlyOpenPanel = panelToOpen;
    }

    // Optional: If you want a separate method to close panels
    public void ClosePanel(GameObject panelToClose)
    {
        if (panelToClose == currentlyOpenPanel)
        {
            panelToClose.SetActive(false);
            currentlyOpenPanel = null;
        }
    }
}
