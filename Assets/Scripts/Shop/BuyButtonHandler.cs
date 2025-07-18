using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;


public class BuyButtonHandler : MonoBehaviour
{
    public ShopItem item;
    public ShopCartManager cartManager;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnBuy);
    }

    void OnBuy()
    {
        cartManager.AddToCart(item);
    }
}
