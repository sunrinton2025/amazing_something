using UnityEngine;

public class SecEnemyBehav : MonoBehaviour
{
    public float detectDist = 5f;     // 플레이어 탐지 거리
    public float arriveThreshold = 0.2f; // 목적지 도달 기준
    public Animator animator;
    public float distanceToPlayer;

    private Vector2 targetPosition;
    private bool isChasing = false;

    void Start()
    {
        PickNewRandomPosition();
    }

    void Update()
    {
        distanceToPlayer = Vector2.Distance(PlayerController.Local.transform.position, transform.position);

        if (distanceToPlayer <= detectDist)
        {
            isChasing = true;
            targetPosition = PlayerController.Local.transform.position;
        }
        else
        {
            isChasing = false;
        }

        MoveTowardsTarget();

        SkewedBoundary2D.Instance.Apply(transform);
    }

    void MoveTowardsTarget()
    {
        Vector2 currentPos = transform.position;
        Vector2 direction = (targetPosition - currentPos).normalized;
        float distance = Vector2.Distance(currentPos, targetPosition);

        if (distance > arriveThreshold)
        {
            Move(direction);
            animator.SetBool("isWalk", true);
            animator.SetBool("isAttack", false);
        }
        else
        {
            animator.SetBool("isWalk", false);
            if (!isChasing)
            {
                PickNewRandomPosition();
            }
        }

        // 좌우 반전 처리
        if (direction.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(direction.x) * -Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    void PickNewRandomPosition()
    {
        (float minX, float maxX) = MapManager.Instance.GetXBoundsOfMaps();
        float randomX = Random.Range(minX, maxX);
        Vector2 randPos = MapManager.Instance.GetPosInMap(randomX);
        targetPosition = randPos;
    }
    public void Move(Vector2 axis)
    {
        Vector2 moveDirection = axis.normalized * 3 * Time.deltaTime;
        transform.Translate(moveDirection);
    }
}
