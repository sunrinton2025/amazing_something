using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator animator;
    SpriteRenderer render;
    int direction;

    void Awake()
    {
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
    }

    public void SetMoving(bool val)
    {
        animator.SetBool("isMoving", val);
    }

    public void SetDirection(int val)
    {
        if (val < 0)
            render.flipX = true;
        else if (val > 0)
            render.flipX = false;
        direction = val;
    }
}