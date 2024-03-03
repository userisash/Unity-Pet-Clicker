using UnityEngine;
using UnityEngine.UI;


public class PanelAnimator : MonoBehaviour
{
    public RectTransform panel; // Reference to your panel's RectTransform
    public float animationDuration = 0.5f; // Duration of the animation
    public float offset = 100; 

    public void AnimatePanelUp()
    {
        LeanTween.moveY(panel, 0f, animationDuration).setEase(LeanTweenType.easeOutQuad);
    }

    public void AnimatePanelDown()
    {
        LeanTween.moveY(panel, -panel.rect.height + offset, animationDuration).setEase(LeanTweenType.easeOutQuad);
    }
}
