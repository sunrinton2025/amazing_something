using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 axis, axisRaw;

    public Vector2 GetAxis()
    {
        axis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        return axis;
    }

    public Vector2 GetAxisRaw()
    {
        axisRaw = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        return axisRaw;
    }
}
