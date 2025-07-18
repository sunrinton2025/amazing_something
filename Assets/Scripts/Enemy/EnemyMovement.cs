using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float evadeSpeed = 5f;
    public float roamRange = 5f;

    public SpriteRenderer spriteRenderer;

    private bool canMove = false;
    private Vector2 moveDelta;

    public void SetMovementEnabled(bool enabled)
    {
        canMove = enabled;
    }

    public void Move(Vector2 axis)
    {
        if (!canMove) return;

        moveDelta = axis;
        moveDelta.y += axis.x * 0.08f;
        moveDelta.x -= axis.y * 0.1f;


        Vector2 moveDirection = moveDelta.normalized * moveSpeed * Time.deltaTime;
        transform.Translate(moveDirection);


    }

    public void Evade(Vector3 direction)
    {
        StartCoroutine(EvadeRoutine(direction));
    }

    private IEnumerator EvadeRoutine(Vector3 direction)
    {
        float evadeTime = 1f;
        float elapsed = 0f;

        Vector2 evadeDir = direction.normalized;

        while (elapsed < evadeTime)
        {
            transform.position += (Vector3)(evadeDir * evadeSpeed * Time.deltaTime);

            if (spriteRenderer != null && evadeDir.x != 0)
            {
                spriteRenderer.flipX = evadeDir.x < 0;
            }

            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    public Vector3 GetRandomPosition()
    {
        Vector2 randPos = MapManager.Instance.GetPosInMap(Random.Range(-2, 2));
        return randPos;
    }
}
