using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ShopCartManager : MonoBehaviour
{
    public Transform cartList;
    public GameObject cartItemPrefab;
    public CartEnergyDisplay energyDisplay;
    public QueueManager queueManager;

    private List<ShopItem> cart = new List<ShopItem>();

    public void AddToCart(ShopItem item)
    {
        cart.Add(item);
        var cartObj = Instantiate(cartItemPrefab, cartList);
        cartObj.GetComponent<CartItemButton>().Init(item, this);
        energyDisplay.SetCart(cart);
    }

    public void RemoveFromCart(ShopItem item, GameObject obj)
    {
        cart.Remove(item);
        Destroy(obj);
        energyDisplay.SetCart(cart);
    }

    public void ConfirmPurchase()
    {
        foreach (var item in cart)
        {
            queueManager.EnqueueItem(item);
        }

        foreach (Transform child in cartList)
        {
            Destroy(child.gameObject);
        }

        cart.Clear();
        energyDisplay.SetCart(cart);
    }
}