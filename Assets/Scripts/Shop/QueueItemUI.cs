using UnityEngine;
using TMPro;

public class QueueItemUI : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI cooldownText;

    public void SetItem(ShopItem item)
    {
        itemNameText.text = item.itemName;
        cooldownText.gameObject.SetActive(false);
    }

    public void ShowCooldown(int timeLeft)
    {
        cooldownText.text = timeLeft.ToString();
        cooldownText.gameObject.SetActive(true);
    }

    public void HideCooldown()
    {
        cooldownText.gameObject.SetActive(false);
    }
}
