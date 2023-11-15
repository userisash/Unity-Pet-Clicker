using UnityEngine;
using TMPro;

public class TooltipManager : MonoBehaviour
{
    public TMP_Text tooltipText;
    private int totalSponsorshipCash = 0;

    public void UpdateTotalSponsorshipCash(int cashReceived)
    {
        totalSponsorshipCash += cashReceived;
        tooltipText.text = $"Total Sponsorship Cash: {totalSponsorshipCash}";
    }

    public void ShowTooltip(bool show)
    {
        tooltipText.gameObject.SetActive(show);
    }
}
