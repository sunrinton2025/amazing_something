using minyee2913.Utils;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public HealthObject health;
    public Animator animator;

    void Awake()
    {
        health = GetComponent<HealthObject>();

        health.OnDamageFinal(onHurtFinal);
        health.OnDeath(onDeath);
    }

    void onHurtFinal(HealthObject.OnDamageFinalEv ev) {
        IndicatorManager.Instance.GenerateText(ev.Damage.ToString(), transform.position + new Vector3(Random.Range(-2, 2), Random.Range(1, 2)), Color.white);
    }

    void onDeath(HealthObject.OnDamageEv ev)
    {
        Debug.Log("death");
        animator.SetBool("IsDeath", true);

        Destroy(gameObject, 2);
    }

    public void Attack()
    {
        //animator.SetTrigger("Attack");
        //Debug.Log("attack");
        // 공격 로직은 여기 추가 (ex. 데미지 계산, 이펙트 등)
    }
}
