using Unity.Cinemachine;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    CinemachineCamera cam;

    Vector2 locked, follow;

    bool following;

    public void Follow(Vector2 target)
    {
        follow = target;
        follow.y += target.x * 0.08f;

        if (following)
        {
            if (Vector2.Distance(locked, follow) > 1f)
            {
                following = false;
            }
        }
        else
        {

            if (Vector2.Distance(locked, follow) > 10)
            {
                locked = follow;
                following = true;
            }
        }

        cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(locked.x, cam.transform.position.y, cam.transform.position.z), Time.deltaTime);
    }
}
