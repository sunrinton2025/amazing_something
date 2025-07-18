using System.Collections;
using DG.Tweening;
using minyee2913.Utils;
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
            if (PlayerController.Local.holding != null)
            {
                interaction.text = "[F] 광물 담기";
            }
            else
            {
                interaction.text = "[F] 선택창 열기";
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (PlayerController.Local.holding != null)
                {
                    PlayerController.Local.holding.transform.DOMove(transform.position, 0.3f);
                    Destroy(PlayerController.Local.holding.gameObject, 0.4f);

                    GameManager.Instance.point += 50;
                    IndicatorManager.Instance.GenerateText("+50pt", transform.position + new Vector3(0, 2.5f), Color.cyan);

                    PlayerController.Local.holding = null;
                }
                else
                {
                    OpenShop();
                }
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
