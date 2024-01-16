using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotificationManager : MonoBehaviour
{
    [SerializeField] private GameObject notificationPrefab;
    [SerializeField] private Transform logContent;
    [SerializeField] private GameObject notificationLogPanel;

    private Queue<NotificationData> notificationsQueue = new Queue<NotificationData>();
    private bool isDisplayingNotification = false;

    private void Start()
    {
        notificationLogPanel.SetActive(false);
    }

    private void Update()
    {
        if (!isDisplayingNotification && notificationsQueue.Count > 0)
        {
            var data = notificationsQueue.Dequeue();
            DisplayNotification(data);
        }
    }

    private void DisplayNotification(NotificationData data)
    {
        if (notificationPrefab == null)
        {
            Debug.LogError("Notification Prefab is not assigned in the NotificationManager script.");
            return;
        }

        isDisplayingNotification = true;
        GameObject newNotification = Instantiate(notificationPrefab, logContent);

        // Set position above the canvas
        RectTransform rt = newNotification.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(920f, 100f); // Above the canvas (adjust as needed)

        // Update text component
        TextMeshProUGUI textComponent = newNotification.GetComponentInChildren<TextMeshProUGUI>();
        if (textComponent == null)
        {
            Debug.LogError("Text component not found in the Notification Prefab.");
            Destroy(newNotification);
            return;
        }
        textComponent.text = data.Message;

        // Update image component
        Image imageComponent = newNotification.GetComponentInChildren<Image>();
        if (imageComponent == null)
        {
            Debug.LogError("Image component not found in the Notification Prefab.");
            Destroy(newNotification);
            return;
        }
        imageComponent.sprite = data.Image;

        // Use LeanTween for animation
        float moveDuration = 1f;
        float holdDuration = 2f;

        LeanTween.moveY(rt, rt.anchoredPosition.y - 200f, moveDuration) // Move down within the canvas in 1 second
            .setEaseOutQuad()
            .setOnComplete(() =>
            {
                // Hold for 2 seconds
                LeanTween.delayedCall(holdDuration, () =>
                {
                    // Move back up
                    LeanTween.moveY(rt, rt.anchoredPosition.y + 200f, moveDuration)
                        .setEaseOutQuad()
                        .setOnComplete(() =>
                        {
                            StartCoroutine(DisableAfterDelay(newNotification, 2f)); // Hold for 5 seconds
                        });
                });
            });
    }



    IEnumerator DisableAfterDelay(GameObject notification, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(notification);
        isDisplayingNotification = false;
    }

    public void AddNotification(string message, Sprite image = null, string milestoneName = "")
    {
        notificationsQueue.Enqueue(new NotificationData { Message = message, Image = image, MilestoneName = milestoneName });
    }


    public void CheckMilestones(int cash, int viewers, int followers, Milestone milestone)
    {
        if (cash >= milestone.requiredCash &&
            viewers >= milestone.requiredViewers &&
            followers >= milestone.requiredFollowers)
        {
            AddNotification($"{milestone.milestoneName} achieved!", milestone.milestoneImage, milestone.milestoneName);
        }
    }


    public void ToggleNotificationLogPanel()
    {
        notificationLogPanel.SetActive(!notificationLogPanel.activeSelf);
    }

    private class NotificationData
    {
        public string Message;
        public Sprite Image;
        public string MilestoneName; // Add this line
    }
}