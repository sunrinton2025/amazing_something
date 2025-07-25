using System.Collections;
using minyee2913.Utils;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    StatController stat;
    Rigidbody2D rigid;
    CapsuleCollider2D col;
    public Vector2 moveDelta;
    public float rollPower;
    public float rollTime;
    public float stopMove;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        stat = GetComponent<StatController>();
    }

    void Update()
    {
        if (stopMove > 0)
            stopMove -= Time.deltaTime;
    }

    public void Move(Vector2 axis)
    {
        if (stopMove > 0)
            return;
        moveDelta = axis;

        moveDelta.y += axis.x * 0.08f;
        moveDelta.x -= axis.y * 0.1f;

        transform.Translate(moveDelta.normalized * stat.GetResultValue("moveSpeed") * Time.deltaTime);
    }

    public void Roll()
    {
        StartCoroutine(roll());
    }

    IEnumerator roll()
    {
        rigid.linearVelocity = moveDelta.normalized * rollPower;

        yield return new WaitForSeconds(rollTime);

        rigid.linearVelocity = Vector2.zero;
    }
}
