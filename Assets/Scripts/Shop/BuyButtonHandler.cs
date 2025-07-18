using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro;


public class BuyButtonHandler : MonoBehaviour
{
    public ShopItem item;
    public ShopCartManager cartManager;
    public GameObject buttonObject;

    public Image iconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI priceText;

    void Start()
    {
        buttonObject.GetComponent<Button>().onClick.AddListener(OnBuy);
        UpdateDisplay();
    }

    void OnBuy()
    {
        cartManager.AddToCart(item);
    }
    void UpdateDisplay()
    {
        if (iconImage != null) iconImage.sprite = item.icon;
        if (nameText != null) nameText.text = item.itemName;
        if (priceText != null) priceText.text = item.price + "";
    }
}
