using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CartEnergyDisplay : MonoBehaviour
{
    public TextMeshProUGUI energyText;
    public int totalEnergy = 100;
    private List<ShopItem> cart = new List<ShopItem>();

    public void SetCart(List<ShopItem> currentCart)
    {
        cart = currentCart;
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        int totalUsed = 0;
        foreach (var item in cart)
        {
            totalUsed += item.price;
        }
        energyText.text = totalUsed + "/" + totalEnergy;
    }
}