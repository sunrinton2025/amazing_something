using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ShopCartManager : MonoBehaviour
{
    public Transform cartList;
    public GameObject cartItemPrefab;
    public CartEnergyDisplay energyDisplay;
    public QueueManager queueManager;

    public List<ShopItem> cart = new List<ShopItem>();
    public List<GameObject> objs = new List<GameObject>();

    public void AddToCart(ShopItem item)
    {
        cart.Add(item);
        var cartObj = Instantiate(cartItemPrefab, cartList);
        cartObj.GetComponent<CartItemButton>().Init(item, this);
        objs.Add(cartObj);
        energyDisplay.SetCart(cart);
    }

    public void RemoveFromCart(ShopItem item, GameObject obj)
    {
        cart.Remove(item);
        objs.Remove(obj);
        Destroy(obj);
        energyDisplay.SetCart(cart);
    }

    public void ConfirmPurchase()
    {
        int point = 0;
        foreach (var item in cart)
        {
            point += item.price;
        }
        if (GameManager.Instance.point >= point)
        {
            GameManager.Instance.point -= point;
            foreach (var item in cart)
            {
                queueManager.EnqueueItem(item);
            }
        }
        else
        {
            return;
        }

        foreach (Transform child in cartList)
        {
            Destroy(child.gameObject);
        }

        cart.Clear();
        energyDisplay.SetCart(cart);
    }
}