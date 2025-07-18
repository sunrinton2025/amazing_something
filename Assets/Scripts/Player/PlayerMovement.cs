using minyee2913.Utils;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    StatController stat;
    Rigidbody2D rigid;
    CapsuleCollider2D col;
    public Vector2 moveDelta;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        stat = GetComponent<StatController>();
    }

    public void Move(Vector2 axis)
    {
        moveDelta = axis.normalized * stat.GetResultValue("moveSpeed");

        transform.Translate(moveDelta * Time.deltaTime);
    }
}
