using UnityEngine;
using System.Collections.Generic;

public class PanelManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> panels = new List<GameObject>();

    public float moveAmount = 500f; // Amount to move the panel
    public float speed = 1f; // Speed of the animation

    private GameObject activePanel = null;

    public void TogglePanel(GameObject panelToToggle)
    {
        // If there's an active panel and it's not the one we're trying to toggle, hide it
        if (activePanel != null && activePanel != panelToToggle)
        {
            GameObject panelToDeactivate = activePanel; // Store the current active panel for deactivation
            LeanTween.moveLocalX(activePanel, -moveAmount, speed)
                    .setEaseOutQuad()
                    .setOnComplete(() => {
                        panelToDeactivate.SetActive(false); // Deactivate the previously active panel
                });
            activePanel = null; // Reset the activePanel as it's now being hidden
        }

        // If the panel to toggle is not already active, show it
        if (panelToToggle != null && !panelToToggle.activeSelf)
        {
            panelToToggle.SetActive(true);
            panelToToggle.transform.localPosition = new Vector3(-moveAmount, panelToToggle.transform.localPosition.y, panelToToggle.transform.localPosition.z);
            LeanTween.moveLocalX(panelToToggle, 0, speed).setEaseOutQuad();
            activePanel = panelToToggle; // Set the new panel as active
        }
        // If the panel to toggle is already active, hide it
        else if (panelToToggle != null && panelToToggle.activeSelf)
        {
            LeanTween.moveLocalX(panelToToggle, -moveAmount, speed)
                    .setEaseOutQuad()
                    .setOnComplete(() => {
                        panelToToggle.SetActive(false); // Deactivate the panel being hidden
                    if (activePanel == panelToToggle)
                        {
                            activePanel = null; // Reset the activePanel as it's now hidden
                    }
                    });
        }
    }





    public void InitializePanels()
    {
        foreach (var panel in panels)
        {
            panel.SetActive(false);
            panel.transform.localPosition = new Vector3(-moveAmount, panel.transform.localPosition.y, panel.transform.localPosition.z);
        }
    }

    void Start()
    {
        InitializePanels();
    }
}