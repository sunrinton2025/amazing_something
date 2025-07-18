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

}
