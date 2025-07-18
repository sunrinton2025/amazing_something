using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Animator animator;

    public void Attack()
    {
        //animator.SetTrigger("Attack");
        Debug.Log("attack");
        // 공격 로직은 여기 추가 (ex. 데미지 계산, 이펙트 등)
    }
}
