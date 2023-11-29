using UnityEngine;
using System.Collections.Generic;

public class PanelManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> panels = new List<GameObject>();

    public float moveAmount = 500f; // Amount to move the panel
    public float speed = 1f; // Speed of the movement animation
    public float fadeSpeed = 0.5f; // Speed of the fade animation, faster than movement

    private GameObject activePanel = null;

    public void TogglePanel(GameObject panelToToggle)
    {
        if (activePanel != null && activePanel != panelToToggle)
        {
            StartHidingPanel(activePanel);
            activePanel = null;
        }

        if (panelToToggle != null && !panelToToggle.activeSelf)
        {
            ShowPanel(panelToToggle);
            activePanel = panelToToggle;
        }
        else if (panelToToggle != null && panelToToggle.activeSelf)
        {
            StartHidingPanel(panelToToggle);
            if (activePanel == panelToToggle)
            {
                activePanel = null;
            }
        }
    }

    private void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);
        CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = panel.AddComponent<CanvasGroup>();

        canvasGroup.alpha = 0;
        LeanTween.alphaCanvas(canvasGroup, 1, fadeSpeed); // Use fadeSpeed for fade-in
        panel.transform.localPosition = new Vector3(-moveAmount, panel.transform.localPosition.y, panel.transform.localPosition.z);
        LeanTween.moveLocalX(panel, 0, speed).setEaseOutQuad();
    }

    private void StartHidingPanel(GameObject panel)
    {
        CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = panel.AddComponent<CanvasGroup>();

        LeanTween.alphaCanvas(canvasGroup, 0, fadeSpeed) // Use fadeSpeed for fade-out
            .setOnComplete(() => {
                panel.SetActive(false);
            });

        LeanTween.moveLocalX(panel, -moveAmount, speed).setEaseOutQuad();
    }

    void Start()
    {
        InitializePanels();
    }

    private void InitializePanels()
    {
        foreach (var panel in panels)
        {
            panel.SetActive(false);
            if (panel.GetComponent<CanvasGroup>() == null)
            {
                panel.AddComponent<CanvasGroup>();
            }
            panel.transform.localPosition = new Vector3(-moveAmount, panel.transform.localPosition.y, panel.transform.localPosition.z);
        }
    }
}
