using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Manager
{
    public string managerName;
    public float incrementValue;
    public int cost;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI incrementValueText;
    public Button purchaseButton;
    [HideInInspector]
    public bool isActive = false;
    [HideInInspector]
    public int currentTier = 0; // Add a tier to each manager.
    public delegate void IncrementAction();
    [HideInInspector]
    public IncrementAction OnIncrement;
}

public class Managers : MonoBehaviour
{
    public ClickBehavior ClickBehavior;
    public List<Manager> managers;

    private void Start()
    {
        // Assuming you have 3 managers in the order: Views, Followers, Cash
        managers[0].OnIncrement = ClickBehavior.IncrementViews;
        managers[1].OnIncrement = ClickBehavior.IncrementFollowers;
        managers[2].OnIncrement = ClickBehavior.IncrementCash;

        foreach (var manager in managers)
        {
            UpdateManagerTexts(manager);
        }
    }

    private void Update()
    {
        CheckButtonInteractiblity();
    }

    public void PurchaseManager(int managerIndex)
    {
        Manager manager = managers[managerIndex];
        if (ClickBehavior.GetCash() >= manager.cost)
        {
            ClickBehavior.AddCash(-manager.cost);

            if (!manager.isActive)
            {
                StartCoroutine(AutoIncrement(manager));
                manager.isActive = true;
            }

            manager.cost *= 2;
            manager.incrementValue *= 2;

            manager.currentTier++; // Increment the manager's tier whenever it's purchased.

            UpdateManagerTexts(manager);
        }
    }

    private IEnumerator AutoIncrement(Manager manager)
    {
        while (true)
        {
            for (int i = 0; i < manager.incrementValue; i++)
            {
                manager.OnIncrement?.Invoke();
            }
            yield return new WaitForSeconds(1);
            ClickBehavior.UpdateAllText();
        }
    }

    private void UpdateManagerTexts(Manager manager)
    {
        manager.costText.text = "$:" + ClickBehavior.FormatNumber(manager.cost);
        manager.incrementValueText.text = ClickBehavior.FormatNumber(manager.incrementValue) + " P/S";
    }

    private void CheckButtonInteractiblity()
    {
        foreach (Manager manager in managers)
        {
            manager.purchaseButton.interactable = ClickBehavior.GetCash() >= manager.cost;
        }
    }
}
