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
    }
}
