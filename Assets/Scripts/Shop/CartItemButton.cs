using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CartItemButton : MonoBehaviour
{
    private ShopItem item;
    private ShopCartManager cartManager;

    public void Init(ShopItem newItem, ShopCartManager manager)
    {
        item = newItem;
        cartManager = manager;
        GetComponentInChildren<TextMeshProUGUI>().text = item.itemName + " (" + item.price + ")";
        GetComponent<Button>().onClick.AddListener(OnRemove);
    }

    void OnRemove()
    {
        cartManager.RemoveFromCart(item, gameObject);
    }
}