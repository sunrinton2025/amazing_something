using UnityEngine;

[ExecuteAlways]
public class IndexOrder : MonoBehaviour
{
    public float offset;
    SpriteRenderer render;

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        render.sortingOrder = -(int)((transform.position.y + offset) * 10);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(transform.position + new Vector3(0, offset), 0.3f);
    }
}
