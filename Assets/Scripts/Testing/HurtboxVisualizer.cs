using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class HurtboxVisualizer : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [SerializeField]
    public Entity player;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.useWorldSpace = true;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.loop = false;
        lineRenderer.positionCount = 0;
    }

    private void Update()
    {
        if (player.activeAbility == null || player.state == Entity.ActionState.Windup)
        {
            Clear();
            return;
        }
        DrawSector(
            player.transform.position,
            player.facingDirection == Vector2.zero ? Vector2.up : player.facingDirection,
            player.activeAbility.radius,
            player.activeAbility.angle
        );
    }

    public void Clear()
    {
        lineRenderer.positionCount = 0;
    }

    public void DrawSector(
        Vector2 origin,
        Vector2 forward,
        float radius,
        float angle
    )
    {
        int segments = Mathf.Max(16, Mathf.RoundToInt(angle / 2f));
        float halfAngle = angle * 0.5f;

        int totalPoints = segments + 3;
        Vector3[] points = new Vector3[totalPoints];

        points[0] = origin;

        Vector2 leftDir = Quaternion.Euler(0f, 0f, -halfAngle) * forward;
        points[1] = origin + leftDir.normalized * radius;

        for (int i = 0; i <= segments; i++)
        {
            float t = i / (float)segments;
            float currentAngle = -halfAngle + angle * t;

            Vector2 dir = Quaternion.Euler(0f, 0f, currentAngle) * forward;
            points[i + 2] = origin + dir.normalized * radius;
        }

        points[totalPoints - 1] = origin;

        lineRenderer.positionCount = totalPoints;
        lineRenderer.SetPositions(points);
    }
}
