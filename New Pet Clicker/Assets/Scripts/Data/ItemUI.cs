using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI priceText;
    //public TextMeshProUGUI descriptionText;
    public Button buyButton;

    private Item item;
    private ClickBehavior currencySystem;

    public void Initialize(Item _item, System.Action<Item> buyAction, ClickBehavior _currencySystem)
    {
        item = _item;
        currencySystem = _currencySystem;

        icon.sprite = item.icon;
        itemNameText.text = item.itemName;
        priceText.text = "$: " + item.price.ToString();
        //descriptionText.text = item.description;

        buyButton.onClick.AddListener(() => buyAction(item));
    }

    private void Update()
    {
        buyButton.interactable = currencySystem.GetCash() >= item.price;
    }
}
