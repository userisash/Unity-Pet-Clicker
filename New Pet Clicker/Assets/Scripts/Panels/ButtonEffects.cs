using UnityEngine;
using UnityEngine.EventSystems; // Required when using Event data.
using UnityEngine.UI; // Required when Using UI elements.

public class ButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1.1f); // Scale of button when hovered over
    public Sprite normalSprite; // The default sprite
    public Sprite clickSprite; // The sprite to change to when clicked
    public GameObject panelToToggle; // Reference to the panel to toggle

    private Image buttonImage;
    private Vector3 originalScale;

    // Start is called before the first frame update
    void Start()
    {
        buttonImage = GetComponent<Image>();
        if (buttonImage == null)
        {
            Debug.LogError("ButtonEffects requires an Image component on the same GameObject.");
            return;
        }

        originalScale = transform.localScale; // Remember the original scale
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = hoverScale; // Scale up
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale; // Return to original scale
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonImage.sprite = clickSprite; // Change the sprite
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonImage.sprite = normalSprite; // Change the sprite back to normal
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Toggle the panel's visibility
        if (panelToToggle != null)
        {
            panelToToggle.SetActive(!panelToToggle.activeSelf);
        }
        else
        {
            Debug.LogError("No panel GameObject assigned to the ButtonEffects script.");
        }
    }
}
