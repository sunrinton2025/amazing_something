using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class QueueManager : MonoBehaviour
{
    public Transform queueContainer;
    public GameObject queueItemPrefab;
    //public float cooldownDuration = 5f;

    private Queue<ShopItem> queue = new Queue<ShopItem>();
    private List<GameObject> queueObjects = new List<GameObject>();
    private float cooldownRemaining = 0f;

    void Update()
    {
        if (queue.Count == 0) return;

        cooldownRemaining -= Time.deltaTime;
        if (cooldownRemaining <= 0f)
        {
            DequeueItem();
        }

        UpdateQueueUI();
    }
    public void EnqueueItem(ShopItem item)
    {
        queue.Enqueue(item);
        GameObject obj = Instantiate(queueItemPrefab, queueContainer);
        var queueItemUI = obj.GetComponent<QueueItemUI>();
        queueItemUI.SetItem(item);
        queueObjects.Add(obj);

        if (cooldownRemaining <= 0f)
        {
            cooldownRemaining = item.cooltime;
        }
    }

    private void DequeueItem()
    {
        if (queue.Count > 0)
        {
            queue.Dequeue();
            Destroy(queueObjects[0]);
            queueObjects.RemoveAt(0);
            if(queue.Count > 0)cooldownRemaining = queue.Peek().cooltime;
        }
    }

    private void UpdateQueueUI()
    {
        for (int i = 0; i < queueObjects.Count; i++)
        {
            var ui = queueObjects[i].GetComponent<QueueItemUI>();
            if (i == 0)
            {
                ui.ShowCooldown(Mathf.CeilToInt(cooldownRemaining));
            }
            else
            {
                ui.HideCooldown();
            }
        }
    }
}