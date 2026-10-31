using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SimpleHose : MonoBehaviour
{
    [Header("Hose Settings")]
    public Transform startPoint;   // Fire extinguisher connector
    public Transform endPoint;     // Nozzle
    public int segments = 10;      // Smoothness of curve
    public float sagAmount = 0.2f; // How much the hose sags
    public float sagOffset = 0.3f; // Shifts sag upward (0 = center, 0.5 = above middle)
    public float hoseWidth = 0.016982f;  // Hose thickness

    private LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = segments;
        line.startWidth = hoseWidth;
        line.endWidth = hoseWidth;
    }

    void Update()
    {
        DrawHose();
    }

    void DrawHose()
    {
        Vector3 p0 = startPoint.position;
        Vector3 p1 = endPoint.position;

        // Left direction relative to extinguisher
        Vector3 leftDir = -transform.right;

        for (int i = 0; i < segments; i++)
        {
            float t = i / (float)(segments - 1);

            // Straight line
            Vector3 pos = Vector3.Lerp(p0, p1, t);

            // Sag curve, shifted upward with sagOffset
            float sagCurve = Mathf.Sin((t + sagOffset) * Mathf.PI);

            // Apply sag
            pos += leftDir * sagCurve * sagAmount;

            line.SetPosition(i, pos);
        }
    }
}
