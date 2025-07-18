using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;

    public SpriteRenderer spriteRenderer;

    private bool canMove = false;

    public void SetMovementEnabled(bool enabled)
    {
        canMove = enabled;
    }

    public void Move(Vector2 axis)
    {
        if (!canMove) return;

        Vector2 moveDirection = axis.normalized * moveSpeed * Time.deltaTime;
        transform.Translate(moveDirection);


    }
}
