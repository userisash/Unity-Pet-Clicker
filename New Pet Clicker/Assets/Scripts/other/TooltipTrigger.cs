using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltipPanel; // Assign this in the inspector

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Show the tooltip
        tooltipPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Hide the tooltip
        tooltipPanel.SetActive(false);
    }
}

