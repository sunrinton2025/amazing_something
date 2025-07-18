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

    bool following;
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
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
                render.flipX = true;
            else if (delta.x > 0)
                render.flipX = false;
        }

        if (Vector2.Distance(follow.transform.position, transform.position) <= 3)
        {
            interaction.text = "[F] 상호작용";
        }
        else
        {
            interaction.text = "";
        }
    }
}
