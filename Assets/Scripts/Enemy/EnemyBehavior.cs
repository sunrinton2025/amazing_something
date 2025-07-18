using System.Collections;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float detectDist = 5f;           // 기본 탐지 거리
    public float extendedDetectDist = 10f;  // 플레이어가 탐지 거리 내에 들어오면 확장되는 탐지 거리
    public float attackRange = 2f;          // 공격 범위
    public float arriveThreshold = 0.5f;    // 도달 기준 거리

    private float stopMove = 0;
    private bool isActive = false;
    private Vector3 lastKnownPlayerPosition;
    private float currentDetectDist;

    public Animator animator;
    public EnemyMovement enemyMovement;
    public EnemyAttack enemyAttack;

    void Start()
    {
        currentDetectDist = detectDist;
    }

    void Update()
    {
        HandleStopMove();
        HandleDetectionAndChase();
        UpdateOrientation();
    }

    // Stop move 시간 처리
    void HandleStopMove()
    {
        if (stopMove > 0f)
        {
            stopMove -= Time.deltaTime;
        }
    }

    void UpdateOrientation()
    {
        if (PlayerController.Local.transform.position.x > transform.localPosition.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else if (PlayerController.Local.transform.position.x <= transform.localPosition.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
    }

    // 탐지 범위 내에 플레이어가 있을 때 추적 처리
    void HandleDetectionAndChase()
    {
        float distanceToPlayer = Vector3.Distance(PlayerController.Local.transform.position, transform.position);

        if (distanceToPlayer <= currentDetectDist)
        {
            if (!isActive)
                ActivateEnemy();

            lastKnownPlayerPosition = PlayerController.Local.transform.position;

            if (distanceToPlayer <= attackRange)
            {
                AttackPlayer();
            }
            else
            {
                ChasePlayer();
            }
        }
        else if (isActive)
        {
            StartCoroutine(MoveToLastKnownPositionThenDeactivate());
        }
        else
        {
            StopMoving();
        }
    }

    // 공격 애니메이션 실행
    void AttackPlayer()
    {
        enemyAttack.Attack();
        animator.SetBool("isWalk", false);
        animator.SetBool("isAttack", true);
    }

    // 추적 애니메이션 실행
    void ChasePlayer()
    {
        if (stopMove <= 0)
            enemyMovement.Move(Vector2.MoveTowards(transform.position, PlayerController.Local.transform.position, Time.deltaTime));

        animator.SetBool("isWalk", true);
        animator.SetBool("isAttack", false);
    }

    // 적 활성화
    void ActivateEnemy()
    {
        isActive = true;
        currentDetectDist = extendedDetectDist;
        animator.SetBool("isActive", true);
        stopMove = 0.5f;
        enemyMovement.SetMovementEnabled(true);
    }

    // 마지막 위치로 이동 후 비활성화
    IEnumerator MoveToLastKnownPositionThenDeactivate()
    {
        // 마지막 위치로 이동
        while (Vector3.Distance(transform.position, lastKnownPlayerPosition) > arriveThreshold)
        {
            enemyMovement.Move(Vector2.MoveTowards(transform.position, lastKnownPlayerPosition, Time.deltaTime));
            animator.SetBool("isWalk", true);
            yield return null;
        }

        DeactivateEnemy();
    }

    // 적 비활성화
    void DeactivateEnemy()
    {
        isActive = false;
        currentDetectDist = detectDist;
        animator.SetBool("isActive", false);
        animator.SetBool("isWalk", false);
        enemyMovement.SetMovementEnabled(false);
    }

    // 이동 멈추기
    void StopMoving()
    {
        animator.SetBool("isWalk", false);
    }

    // 피해 처리
    public void OnDamaged(Vector3 playerDirection)
    {
        AttackPlayer();
    }
}
