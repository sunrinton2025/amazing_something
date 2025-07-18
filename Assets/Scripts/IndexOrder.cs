using UnityEngine;

[ExecuteAlways]
public class IndexOrder : MonoBehaviour
{
    SpriteRenderer render;

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        render.sortingOrder = -(int)(transform.position.y * 10);
    }
}
