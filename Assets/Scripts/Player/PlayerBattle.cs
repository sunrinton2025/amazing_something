using System.Collections;
using minyee2913.Utils;
using UnityEngine;

public class PlayerBattle : MonoBehaviour
{
    public HealthObject health;
    public RangeController range;
    PlayerAnimator animator;
    Rigidbody2D rigid;

    Cooldown atkCool = new(0.5f);

    void Awake()
    {
        health = GetComponent<HealthObject>();
        range = GetComponent<RangeController>();
        animator = GetComponent<PlayerAnimator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Attack()
    {
        if (atkCool.IsIn())
            return;
        atkCool.Start();

        StartCoroutine(attack());
    }

    IEnumerator attack()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x > transform.position.x)
        {
            animator.SetDirection(1);
        }
        else if (mousePos.x < transform.position.x)
        {
            animator.SetDirection(-1);

        }

        animator.Trigger("attack");

        Vector2 axis = new Vector2(40 * -animator.direction, 0);

        Vector2 moveDelta = axis;

        moveDelta.y += axis.x * 0.08f;
        moveDelta.x -= axis.y * 0.1f;

        rigid.linearVelocity = moveDelta.normalized;

        yield return new WaitForSeconds(0.2f);

        CamEffector.current.Shake(4);

        rigid.linearVelocity = Vector2.zero;

        foreach (Transform target in range.GetHitInRange2D(range.GetRange("attack"), LayerMask.GetMask("damageable")))
        {
            HealthObject hp = target.GetComponent<HealthObject>();

            if (hp != null)
            {
                hp.GetDamage(10, health, HealthObject.Cause.Melee);
            }
        }
    }
}
