using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Robot : MonoBehaviour
{
    SpriteRenderer render;
    [SerializeField]
    Transform follow;
    [SerializeField]
    Vector2 offset;

    [SerializeField]
    Text interaction;
    [SerializeField]
    Transform shopPanel, point, point2, shopCam;
    Vector2 panelPos;

    public bool opened;

    bool following;
    void Start()
    {
        render = GetComponent<SpriteRenderer>();

        panelPos = shopPanel.position;
    }

    void Update()
    {
        SkewedBoundary2D.Instance.Apply(transform);

        if (following)
        {
            if (Vector2.Distance(follow.transform.position, transform.position) < 6)
            {
                following = false;
            }
        }

        if (Vector2.Distance(follow.transform.position, transform.position) > 7 || following)
        {
            Vector2 delta = (Vector2)follow.position + offset;
            transform.position = Vector2.Lerp(transform.position, (Vector2)follow.position + offset, 2 * Time.deltaTime);

            if (delta.x < 0)
                render.flipX = false;
            else if (delta.x > 0)
                render.flipX = true;
        }

        if (Vector2.Distance(follow.transform.position, transform.position) <= 4)
        {
            interaction.text = "[F] 상호작용";

            if (Input.GetKeyDown(KeyCode.F))
            {
                OpenShop();
            }
        }
        else
        {
            interaction.text = "";
        }

        if (opened)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseShop();
            }

            interaction.text = "";
        }
    }

    public void OpenShop()
    {
        StartCoroutine(open());
    }

    IEnumerator open()
    {
        Time.timeScale = 0;

        shopCam.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(0.5f);

        shopPanel.gameObject.SetActive(true);
        shopPanel.transform.position = point.position;
        shopPanel.transform.DOMove(point2.position, 0.4f).SetEase(Ease.OutExpo).SetUpdate(true);

        opened = true;
    }

    public void CloseShop()
    {
        Time.timeScale = 1;

        shopPanel.gameObject.SetActive(false);
        shopCam.gameObject.SetActive(false);

        opened = false;
    }
}
