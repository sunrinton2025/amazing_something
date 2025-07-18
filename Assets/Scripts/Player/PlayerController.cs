using minyee2913.Utils;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(StatController))]
[RequireComponent(typeof(HealthObject))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Local { get; private set; }
    public PlayerInput input;
    public PlayerMovement movement;


    void Awake()
    {
        Local = this;
        input = GetComponent<PlayerInput>();
        movement = GetComponent<PlayerMovement>();

    }

    void Update()
    {
        input.GetAxisRaw();
    }

    void FixedUpdate()
    {
        movement.Move(input.axisRaw);
    }
}
