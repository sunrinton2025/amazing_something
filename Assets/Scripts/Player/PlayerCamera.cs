using Unity.Cinemachine;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    CinemachineCamera cam;

    Vector2 locked, follow;
    [SerializeField]
    float differ, yOffset;
    [SerializeField] float viewRatioY = 0.4f; // 선이 화면에서 차지할 비율 (0~1), 예: 0.4는 아래쪽 40%

    bool following;
    public void Follow(Vector2 target)
    {
        float halfHeight = cam.Lens.OrthographicSize;

        // 화면에서 선이 보이길 원하는 상대 위치 (뷰포트 비율)
        float screenY = -halfHeight + 2 * halfHeight * viewRatioY;

        // 선의 실제 y 위치
        float lineY = target.x * differ + yOffset;

        // 카메라 y 위치 = 선 위치 - 화면 내 위치
        float camY = lineY - screenY;

        follow = new Vector3(target.x, camY, cam.transform.position.z);

        if (following)
        {
            if (Vector2.Distance(locked, follow) > 1f)
                following = false;
        }
        else
        {
            if (Vector2.Distance(locked, follow) > 10)
            {
                locked = follow;
                following = true;
            }
        }

        cam.transform.position = Vector3.Lerp(
            cam.transform.position,
            new Vector3(locked.x, locked.y, cam.transform.position.z),
            4 * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        // 이 선은 카메라 뷰 기준에서 항상 일정하게 보여야 함
        Gizmos.color = Color.cyan;

        float startX = transform.position.x - 100;
        float endX = transform.position.x + 100;

        Vector3 p1 = new Vector3(startX, startX * differ + yOffset, 0);
        Vector3 p2 = new Vector3(endX, endX * differ + yOffset, 0);

        Gizmos.DrawLine(p1, p2);
    }
}
