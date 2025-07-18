using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    public ShopCartManager cartManager;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnBuy);
    }
    public void OnBuy()
    {
        cartManager.ConfirmPurchase();
        Robot.Instance.CloseShop();

        cartManager.cart.Clear();
        foreach (var a in cartManager.objs)
        {
            Destroy(a);
        }
        cartManager.objs.Clear();
    }
}
