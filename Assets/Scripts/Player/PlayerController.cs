using minyee2913.Utils;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerCamera))]
[RequireComponent(typeof(StatController))]
[RequireComponent(typeof(HealthObject))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Local { get; private set; }
    public PlayerInput input;
    public PlayerAnimator animator;
    public PlayerMovement movement;
    public PlayerCamera cam;


    void Awake()
    {
        Local = this;

        input = GetComponent<PlayerInput>();
        movement = GetComponent<PlayerMovement>();
        animator = GetComponent<PlayerAnimator>();
        cam = GetComponent<PlayerCamera>();

    }

    void Update()
    {
        input.GetAxisRaw();

        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     //(float minX, float maxX) = MapManager.Instance.GetXBoundsOfMaps();
        //     (float minX, float maxX) = MapManager.Instance.GetXBoundsInCamera();
        //     transform.position = MapManager.Instance.GetPosInMap(Random.Range(minX, maxX));
        // }
    }

    void FixedUpdate()
    {
        movement.Move(input.axisRaw);

        animator.SetMoving(input.HasAxisRaw());
        animator.SetDirection((int)input.axisRaw.x);

        cam.Follow(transform.position);
    }
}
