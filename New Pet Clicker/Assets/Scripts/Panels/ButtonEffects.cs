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
    private Animator animator; // Reference to the Animator component

    // Start is called before the first frame update
    void Start()
    {
        buttonImage = GetComponent<Image>();
        animator = GetComponent<Animator>(); // Get the Animator component

        if (buttonImage == null)
        {
            Debug.LogError("ButtonEffects requires an Image component on the same GameObject.");
            return;
        }

        originalScale = transform.localScale; // Remember the original scale

        // Initialize Animator parameters
        if (animator != null)
        {
            animator.SetBool("isUnclicked", true);
            animator.SetBool("click", false);
        }
        else
        {
            Debug.LogError("ButtonEffects requires an Animator component on the same GameObject.");
        }
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
        if (animator != null) animator.SetBool("click", true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonImage.sprite = normalSprite; // Change the sprite back to normal
        if (animator != null) animator.SetBool("click", false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Toggle the panel's visibility
        if (panelToToggle != null)
        {
            panelToToggle.SetActive(!panelToToggle.activeSelf);
            if (animator != null) animator.SetBool("isUnclicked", !panelToToggle.activeSelf);
        }
        else
        {
            Debug.LogError("No panel GameObject assigned to the ButtonEffects script.");
        }
    }
}
