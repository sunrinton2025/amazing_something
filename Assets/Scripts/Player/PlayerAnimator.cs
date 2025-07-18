using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;
    SpriteRenderer render;
    public int direction;

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
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        else if (val > 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        direction = val;
    }

    public void Trigger(string val)
    {
        animator.SetTrigger(val);
    }
}