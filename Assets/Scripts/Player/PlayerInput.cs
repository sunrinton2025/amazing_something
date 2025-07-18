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

    public bool HasAxisRaw()
    {
        return Mathf.Abs(axisRaw.x) + Mathf.Abs(axisRaw.y) != 0;
    }

    public bool HasAxis()
    {
        return Mathf.Abs(axis.x) + Mathf.Abs(axis.y) != 0;
    }
}
