using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class SkewedBoundary2D : MonoBehaviour
{
    [Header("꼭짓점(시계방향, 마지막은 자동으로 처음과 연결됨)")]
    public Vector2[] boundaryPoints = new Vector2[]
    {
        new Vector2(-5, -2),
        new Vector2(5, -1),
        new Vector2(4, 3),
        new Vector2(-4, 2)
    };

    [Header("테스트용 제한할 오브젝트")]
    public Transform target;

    private EdgeCollider2D edgeCollider;

    void Start()
    {
        edgeCollider = GetComponent<EdgeCollider2D>();
        SetEdgeCollider();
    }

    void Update()
    {
        if (target != null)
        {
            Vector2 projected = ProjectPointInside(target.position);
            target.position = new Vector3(projected.x, projected.y, target.position.z);
        }
    }

    void SetEdgeCollider()
    {
        Vector2[] closedPoints = new Vector2[boundaryPoints.Length + 1];
        for (int i = 0; i < boundaryPoints.Length; i++)
        {
            closedPoints[i] = boundaryPoints[i];
        }
        closedPoints[boundaryPoints.Length] = boundaryPoints[0]; // loop

        edgeCollider.points = closedPoints;
    }

    Vector2 ProjectPointInside(Vector2 point)
    {
        if (IsInsidePolygon(point, boundaryPoints))
            return point;

        Vector2 closest = point;
        float minDist = float.MaxValue;

        for (int i = 0; i < boundaryPoints.Length; i++)
        {
            Vector2 a = boundaryPoints[i];
            Vector2 b = boundaryPoints[(i + 1) % boundaryPoints.Length];
            Vector2 projected = ClosestPointOnSegment(point, a, b);
            float dist = Vector2.SqrMagnitude(projected - point);
            if (dist < minDist)
            {
                minDist = dist;
                closest = projected;
            }
        }

        return closest;
    }

    bool IsInsidePolygon(Vector2 point, Vector2[] polygon)
    {
        int crossingNumber = 0;
        for (int i = 0; i < polygon.Length; i++)
        {
            Vector2 a = polygon[i];
            Vector2 b = polygon[(i + 1) % polygon.Length];
            if (((a.y > point.y) != (b.y > point.y)) &&
                (point.x < (b.x - a.x) * (point.y - a.y) / (b.y - a.y) + a.x))
            {
                crossingNumber++;
            }
        }
        return crossingNumber % 2 == 1;
    }

    Vector2 ClosestPointOnSegment(Vector2 p, Vector2 a, Vector2 b)
    {
        Vector2 ab = b - a;
        float t = Vector2.Dot(p - a, ab) / ab.sqrMagnitude;
        t = Mathf.Clamp01(t);
        return a + t * ab;
    }

    void OnDrawGizmos()
    {
        if (boundaryPoints == null || boundaryPoints.Length < 2)
            return;

        Gizmos.color = Color.cyan;
        for (int i = 0; i < boundaryPoints.Length; i++)
        {
            Vector2 current = boundaryPoints[i];
            Vector2 next = boundaryPoints[(i + 1) % boundaryPoints.Length];
            Gizmos.DrawLine(transform.TransformPoint(current), transform.TransformPoint(next));
        }
    }
}
