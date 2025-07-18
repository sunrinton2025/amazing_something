using System.Collections.Generic;
using minyee2913.Utils;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    [SerializeField]
    List<BoxCollider2D> maps = new();
    protected override bool UseDontDestroyOnLoad => false;

    public Vector2 GetPosInMap(float posX)
    {
        foreach (var map in maps)
        {
            if (map == null) continue;

            Vector2 localPoint = map.transform.InverseTransformPoint(new Vector2(posX, 0));

            Vector2 size = map.size;
            Vector2 offset = map.offset;
            float left = offset.x - size.x / 2f;
            float right = offset.x + size.x / 2f;

            if (localPoint.x >= left && localPoint.x <= right)
            {
                float bottom = offset.y - size.y / 2f;
                float top = offset.y + size.y / 2f;

                // 로컬 기준으로 랜덤 생성
                float localY = Random.Range(bottom, top);

                Vector2 finalWorldPos = map.transform.TransformPoint(new Vector2(localPoint.x, localY));
                return finalWorldPos;
            }
        }

        Debug.LogWarning($"x = {posX} 를 포함하는 맵이 없습니다.");
        return Vector2.zero;
    }

    public (float minX, float maxX) GetXBoundsOfMaps()
    {
        float minX = float.PositiveInfinity;
        float maxX = float.NegativeInfinity;

        foreach (var map in maps)
        {
            if (map == null) continue;

            // BoxCollider2D의 4개 꼭짓점을 월드 좌표로 계산
            Vector2 size = map.size;
            Vector2 offset = map.offset;

            // 로컬 기준 꼭짓점
            Vector2 half = size / 2f;

            Vector2[] localCorners = new Vector2[4]
            {
                offset + new Vector2(-half.x, -half.y),
                offset + new Vector2(-half.x,  half.y),
                offset + new Vector2( half.x,  half.y),
                offset + new Vector2( half.x, -half.y)
            };

            foreach (var local in localCorners)
            {
                Vector2 world = map.transform.TransformPoint(local);
                minX = Mathf.Min(minX, world.x);
                maxX = Mathf.Max(maxX, world.x);
            }
        }

        // 예외 처리
        if (minX == float.PositiveInfinity || maxX == float.NegativeInfinity)
        {
            Debug.LogWarning("맵 콜라이더가 비어있거나 유효하지 않음");
            return (0f, 0f);
        }

        return (minX, maxX);
    }

    public (float minX, float maxX) GetXBoundsInCamera()
    {
        Camera cam = Camera.main;
        if (cam == null)
        {
            Debug.LogWarning("Main Camera not found.");
            return (0, 0);
        }

        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
        Rect cameraRect = new Rect(
            Mathf.Min(bottomLeft.x, topRight.x),
            Mathf.Min(bottomLeft.y, topRight.y),
            Mathf.Abs(topRight.x - bottomLeft.x),
            Mathf.Abs(topRight.y - bottomLeft.y)
        );

        float minX = float.PositiveInfinity;
        float maxX = float.NegativeInfinity;

        foreach (var map in maps)
        {
            if (map == null) continue;

            Vector2 size = map.size;
            Vector2 offset = map.offset;
            Vector2 half = size / 2f;

            Vector2[] localCorners = new Vector2[4]
            {
                offset + new Vector2(-half.x, -half.y),
                offset + new Vector2(-half.x,  half.y),
                offset + new Vector2( half.x,  half.y),
                offset + new Vector2( half.x, -half.y)
            };

            // 꼭짓점 → 월드 좌표
            Vector2[] worldCorners = new Vector2[4];
            for (int i = 0; i < 4; i++)
                worldCorners[i] = map.transform.TransformPoint(localCorners[i]);

            // 카메라 뷰 영역과 겹치는지 판단
            if (!PolygonIntersectsRect(worldCorners, cameraRect))
                continue;

            foreach (var pt in worldCorners)
            {
                minX = Mathf.Min(minX, pt.x);
                maxX = Mathf.Max(maxX, pt.x);
            }
        }

        if (minX == float.PositiveInfinity || maxX == float.NegativeInfinity)
        {
            Debug.LogWarning("카메라 안에 포함된 맵 콜라이더가 없습니다.");
            return (0f, 0f);
        }

        return (minX, maxX);
    }


    private bool PolygonIntersectsRect(Vector2[] polygon, Rect rect)
    {
        foreach (var point in polygon)
        {
            if (rect.Contains(point))
                return true;
        }

        Vector2[] rectCorners = new Vector2[4]
        {
            new Vector2(rect.xMin, rect.yMin),
            new Vector2(rect.xMax, rect.yMin),
            new Vector2(rect.xMax, rect.yMax),
            new Vector2(rect.xMin, rect.yMax)
        };

        for (int i = 0; i < 4; i++)
        {
            Vector2 a1 = rectCorners[i];
            Vector2 a2 = rectCorners[(i + 1) % 4];

            for (int j = 0; j < polygon.Length; j++)
            {
                Vector2 b1 = polygon[j];
                Vector2 b2 = polygon[(j + 1) % polygon.Length];

                if (LinesIntersect(a1, a2, b1, b2))
                    return true;
            }
        }

        return false;
    }

    // 선분 교차 여부
    private bool LinesIntersect(Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2)
    {
        float d = (a2.x - a1.x) * (b2.y - b1.y) - (a2.y - a1.y) * (b2.x - b1.x);
        if (Mathf.Approximately(d, 0)) return false;

        float u = ((b1.x - a1.x) * (b2.y - b1.y) - (b1.y - a1.y) * (b2.x - b1.x)) / d;
        float v = ((b1.x - a1.x) * (a2.y - a1.y) - (b1.y - a1.y) * (a2.x - a1.x)) / d;

        return u >= 0 && u <= 1 && v >= 0 && v <= 1;
    }

}
