using System.Collections;
using DG.Tweening;
using minyee2913.Utils;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBattle : MonoBehaviour
{
    public HealthObject health;
    public RangeController range;
    PlayerAnimator animator;
    PlayerMovement movement;
    Rigidbody2D rigid;

    Cooldown atkCool = new(0.5f);
    Cooldown pickCool = new(0.6f);
    [SerializeField]
    Image spaceship;
    Vector2 spacePos;
    [SerializeField]
    GameObject laser_;
    [SerializeField]
    Slider hp;

    void Awake()
    {
        spacePos = spaceship.transform.localPosition;
        health = GetComponent<HealthObject>();
        range = GetComponent<RangeController>();
        animator = GetComponent<PlayerAnimator>();
        rigid = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        hp.value = health.Rate;
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
        movement.stopMove = 0.4f;
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

        Vector2 axis = new Vector2(80 * animator.direction, 0);

        Vector2 moveDelta = axis;

        moveDelta.y += axis.x * 0.08f;
        moveDelta.x -= axis.y * 0.1f;

        rigid.linearVelocity = moveDelta.normalized;

        yield return new WaitForSeconds(0.2f);

        CamEffector.current.Shake(1, 0.3f);

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

    public void Pick()
    {
        if (pickCool.IsIn())
            return;
        pickCool.Start();

        StartCoroutine(pick());
    }

    IEnumerator pick()
    {
        movement.stopMove = 0.4f;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x > transform.position.x)
        {
            animator.SetDirection(1);
        }
        else if (mousePos.x < transform.position.x)
        {
            animator.SetDirection(-1);

        }

        animator.Trigger("pick");

        yield return new WaitForSeconds(0.2f);

        CamEffector.current.Shake(3);

        foreach (Transform target in range.GetHitInRange2D(range.GetRange("attack"), LayerMask.GetMask("ore")))
        {
            HealthObject hp = target.GetComponent<HealthObject>();

            if (hp != null)
            {
                hp.GetDamage(7, health, HealthObject.Cause.Melee);
            }
        }

        foreach (Transform target in range.GetHitInRange2D(range.GetRange("attack"), LayerMask.GetMask("damageable")))
        {
            HealthObject hp = target.GetComponent<HealthObject>();

            if (hp != null)
            {
                hp.GetDamage(2, health, HealthObject.Cause.Melee);
            }
        }
    }

    public void CastSkill(string name)
    {
        if (name == "레이저 빔")
        {
            StartCoroutine(laser());
        }
    }

    IEnumerator laser()
    {
        Vector2 targetPos = transform.position;
        foreach (Transform target in range.GetHitInRange2D(range.GetRange("area"), LayerMask.GetMask("damageable")))
        {
            HealthObject hp = target.GetComponent<HealthObject>();

            if (hp != null)
            {
                targetPos = target.position;
            }
        }
        spaceship.transform.DOLocalMove(spacePos + new Vector2(0, 300), 0.3f);

        IndicatorManager.Instance.GenerateText("목표 지정", targetPos + new Vector2(0, 1), Color.red);

        yield return new WaitForSeconds(0.5f);

        CamEffector.current.Shake(6);

        Destroy(Instantiate(laser_, targetPos, Quaternion.identity), 0.3f);

        foreach (Transform target in range.GetHitInRangeFreely2D(targetPos, range.GetRange("skill"), LayerMask.GetMask("damageable")))
        {
            HealthObject hp = target.GetComponent<HealthObject>();

            if (hp != null)
            {
                hp.GetDamage(21, health, HealthObject.Cause.Skill);
            }
        }

        yield return new WaitForSeconds(0.5f);

        spaceship.transform.DOLocalMove(spacePos, 0.3f);
    }
}
