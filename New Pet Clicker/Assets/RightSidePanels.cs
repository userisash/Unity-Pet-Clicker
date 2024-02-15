using UnityEngine;
using UnityEngine.UI;

public class RightSidePanels: MonoBehaviour
{
    public RectTransform panelTransform;
    public float duration = 1f;
    public float moveDistance = 200f;

    private bool isPanelOpen = false;

    // Function to toggle the panel animation
    public void TogglePanelAnimation()
    {
        if (isPanelOpen)
        {
            // If the panel is open, move it back to its original position
            LeanTween.moveX(panelTransform, panelTransform.anchoredPosition.x - moveDistance, duration)
                .setEase(LeanTweenType.easeInOutQuad);
        }
        else
        {
            // If the panel is closed, move it to the right
            LeanTween.moveX(panelTransform, panelTransform.anchoredPosition.x + moveDistance, duration)
                .setEase(LeanTweenType.easeInOutQuad);
        }

        // Toggle the panel state
        isPanelOpen = !isPanelOpen;
    }
}
