using System.Collections.Generic;
using minyee2913.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerCamera))]
[RequireComponent(typeof(PlayerBattle))]
[RequireComponent(typeof(StatController))]
[RequireComponent(typeof(HealthObject))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Local { get; private set; }
    public PlayerInput input;
    public PlayerAnimator animator;
    public PlayerMovement movement;
    public PlayerCamera cam;
    public PlayerBattle battle;

    public Gem holding;
    [SerializeField]
    Vector2 holdoffset;


    void Awake()
    {
        Local = this;

        input = GetComponent<PlayerInput>();
        movement = GetComponent<PlayerMovement>();
        animator = GetComponent<PlayerAnimator>();
        cam = GetComponent<PlayerCamera>();
        battle = GetComponent<PlayerBattle>();

    }

    void Update()
    {
        input.GetAxisRaw();

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            movement.Roll();
            animator.Trigger("roll");
        }

        if (Input.GetMouseButtonDown(0) && Time.timeScale != 0)
        {
            battle.Attack();
        }

        if (Input.GetMouseButtonDown(1) && Time.timeScale != 0)
        {
            battle.Pick();
        }

        (float minX, float maxX) = MapManager.Instance.GetXBoundsOfMaps();

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX + 6, maxX - 6), transform.position.y, transform.position.z);

        if (holding != null)
        {
            holding.transform.position = Vector2.Lerp(holding.transform.position, (Vector2)transform.position + holdoffset, 5 * Time.deltaTime);
        }

        animator.animator.SetBool("isHolding", holding != null);
    }

    void FixedUpdate()
    {
        movement.Move(input.axisRaw);

        animator.SetMoving(input.HasAxisRaw());
        animator.SetDirection((int)input.axisRaw.x);

        cam.Follow(transform.position);
    }
}
