using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour
{
    public float detectDist = 5f;
    public float extendedDetectDist = 10f;
    public float attackRange = 2f;
    public float randomMoveTime = 3f;
    public float arriveThreshold = 0.5f;

    float stopMove = 0;

    public Animator animator;
    public EnemyMovement enemyMovement;
    public EnemyAttack enemyAttack;

    private bool isActive = false;
    public float currentDetectDist;
    private Vector3 lastKnownPlayerPosition;
    private bool isChasingLastPosition = false;

    void Start()
    {
        currentDetectDist = detectDist;
    }

    void Update()
    {
        if (stopMove > 0f)
        {
            stopMove -= Time.deltaTime;
        }
        float distanceToPlayer = Vector3.Distance(PlayerController.Local.transform.position, transform.position);

        if (distanceToPlayer <= currentDetectDist)
        {
            if (!isActive)
                ActivateEnemy();
            else
            {
                if (PlayerController.Local.transform.position.x > transform.localPosition.x)
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
                else if (PlayerController.Local.transform.position.x <= transform.localPosition.x)
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
            lastKnownPlayerPosition = PlayerController.Local.transform.position;
            isChasingLastPosition = false;

            if (distanceToPlayer <= attackRange)
            {
                enemyAttack.Attack();
                animator.SetBool("isWalk", false);
            }
            else
            {
                if (stopMove <= 0)
                    enemyMovement.Move(Vector2.MoveTowards(transform.position, PlayerController.Local.transform.position, Time.deltaTime));
                animator.SetBool("isWalk", true);
            }
        }
        else if (isActive && !isChasingLastPosition)
        {
            StartCoroutine(MoveToLastKnownPositionThenRoam());
        }
        else
        {
            animator.SetBool("isWalk", false);
        }
    }

    void ActivateEnemy()
    {
        isActive = true;
        currentDetectDist = extendedDetectDist;
        animator.SetBool("isActive", true);
        stopMove = 0.5f;
        enemyMovement.SetMovementEnabled(true);
    }

    IEnumerator MoveToLastKnownPositionThenRoam()
    {
        isChasingLastPosition = true;

        while (Vector3.Distance(transform.position, lastKnownPlayerPosition) > arriveThreshold)
        {
            enemyMovement.Move(lastKnownPlayerPosition - transform.position);
            animator.SetBool("isWalk", true);
            yield return null;
        }

        float roamDuration = Random.Range(1f, randomMoveTime);
        float elapsed = 0f;
        Vector3 randomTarget = enemyMovement.GetRandomPosition();

        while (elapsed < roamDuration)
        {
            enemyMovement.Move(randomTarget - transform.position);
            animator.SetBool("isWalk", true);
            elapsed += Time.deltaTime;
            if (Vector3.Distance(transform.position, randomTarget) > arriveThreshold)
            {
                randomTarget = enemyMovement.GetRandomPosition();
            }
            yield return null;
        }

        isActive = false;
        isChasingLastPosition = false;
        currentDetectDist = detectDist;
        animator.SetBool("isActive", false);
        animator.SetBool("isWalk", false);
        enemyMovement.SetMovementEnabled(false);
    }

    public void OnDamaged(Vector3 playerDirection)
    {
        if (Random.value < 0.5f)
        {
            Vector3 evadeDir = (transform.position - playerDirection).normalized;
            enemyMovement.Evade(evadeDir);
            animator.SetBool("isWalk", true);
        }
        else
        {
            enemyAttack.Attack();
            animator.SetBool("isWalk", false);
        }
    }
}
