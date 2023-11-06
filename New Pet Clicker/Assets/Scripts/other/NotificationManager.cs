using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class NotificationManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI notificationText; // Assign in inspector
    [SerializeField] private GameObject notificationLogPanel;  // Assign in inspector
    [SerializeField] private TextMeshProUGUI notificationLogText; // Assign in inspector
    public GameObject notificationPrefab; // Assign this in the inspector
    public Transform logContent; // Assign the Content transform of the Scroll View in the inspector


    [SerializeField] private float displayTime = 3f; // Time to display each notification


    private string lastNotification = "";
    private Queue<string> notificationsQueue = new Queue<string>();
    private List<string> notificationsHistory = new List<string>();
    private bool isDisplayingNotification = false;

    private void Start()
    {
        notificationLogPanel.SetActive(false); // Assuming the log panel is initially set to inactive
    }

    private void Update()
    {
        if (!isDisplayingNotification && notificationsQueue.Count > 0)
        {
            StartCoroutine(DisplayNotification());
        }
    }

    private IEnumerator DisplayNotification()
    {
        isDisplayingNotification = true;

        while (notificationsQueue.Count > 0)
        {
            string message = notificationsQueue.Dequeue();
            notificationText.text = message;
            notificationText.gameObject.SetActive(true); // Assuming the text is initially set to inactive

            AddToNotificationHistory(message);

            yield return new WaitForSeconds(displayTime);

            notificationText.gameObject.SetActive(false);
        }

        isDisplayingNotification = false;
    }

    public void AddNotification(string message)
    {
        if (message != lastNotification)
        {
            notificationsQueue.Clear(); // Clear the queue to prioritize the new message
            notificationsQueue.Enqueue(message);

            if (isDisplayingNotification)
            {
                StopAllCoroutines(); // Stop the current notification
                isDisplayingNotification = false;
                notificationText.gameObject.SetActive(false); // Immediately hide the current notification
            }

            lastNotification = message;
        }
    }

    private void AddToNotificationHistory(string message)
    {
        // Only add the message to the history if it doesn't already exist
        if (!notificationsHistory.Contains(message))
        {
            notificationsHistory.Add(message);
            AddToLog(message); // This will create a new log entry
        }
    }


    public void AddToLog(string message)
    {
        // Check if there is already a log entry with the same message
        foreach (Transform child in logContent)
        {
            if (child.GetComponent<TextMeshProUGUI>().text == message)
            {
                return; // If found, do not add a duplicate
            }
        }

        GameObject newNotification = Instantiate(notificationPrefab, logContent);
        newNotification.GetComponent<TextMeshProUGUI>().text = message;
        newNotification.transform.localScale = Vector3.one; // Ensure the scale is reset if it's different when instantiated
        newNotification.transform.localPosition = Vector3.zero; // Reset position to align properly in the scroll view
    }

    private void UpdateNotificationLogText()
    {
        notificationLogText.text = string.Join("\n", notificationsHistory.ToArray());
    }

    public void ToggleNotificationLogPanel()
    {
        notificationLogPanel.SetActive(!notificationLogPanel.activeSelf);
    }
}
