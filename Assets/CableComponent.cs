using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CableComponent : MonoBehaviour
{
    [Header("Cable Settings")]
    public Transform endPoint;         // End of cable (e.g., nozzle)
    public Material cableMaterial;     // Material for LineRenderer
    public float cableLength = 2f;     // Total rope length
    public int totalSegments = 20;     // Rope subdivisions
    public float cableWidth = 0.05f;   // Rope thickness
    public int solverIterations = 8;   // More = more stable
    public float damping = 0.98f;      // Velocity damping (0.95–0.99)

    private LineRenderer lineRenderer;
    private List<CableSegment> segments = new List<CableSegment>();
    private float segmentLength;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = totalSegments;
        lineRenderer.material = cableMaterial;
        lineRenderer.startWidth = cableWidth;
        lineRenderer.endWidth = cableWidth;

        // Calculate rope segment length
        segmentLength = cableLength / (totalSegments - 1);

        // Create rope segments
        Vector3 ropeStart = transform.position;
        for (int i = 0; i < totalSegments; i++)
        {
            segments.Add(new CableSegment(ropeStart));
            ropeStart.y -= segmentLength;
        }
    }

    void FixedUpdate()
    {
        Simulate();
        ApplyConstraints();
    }

    void LateUpdate()
    {
        DrawCable();
    }

    void Simulate()
    {
        foreach (var seg in segments)
        {
            Vector3 velocity = (seg.posNow - seg.posOld) * damping;
            seg.posOld = seg.posNow;
            seg.posNow += velocity;
            seg.posNow += Physics.gravity * Time.fixedDeltaTime * Time.fixedDeltaTime;
        }
    }

    void ApplyConstraints()
    {
        // Lock first and last points
        segments[0].posNow = transform.position;
        segments[segments.Count - 1].posNow = endPoint.position;

        // Iteratively enforce rope constraints
        for (int k = 0; k < solverIterations; k++)
        {
            for (int i = 0; i < segments.Count - 1; i++)
            {
                CableSegment segA = segments[i];
                CableSegment segB = segments[i + 1];

                float dist = (segA.posNow - segB.posNow).magnitude;
                float error = dist - segmentLength;
                Vector3 changeDir = (segA.posNow - segB.posNow).normalized;
                Vector3 change = changeDir * (error * 0.5f);

                if (i != 0)
                    segA.posNow -= change;
                if (i != segments.Count - 1)
                    segB.posNow += change;
            }
        }
    }

    void DrawCable()
    {
        for (int i = 0; i < segments.Count; i++)
        {
            lineRenderer.SetPosition(i, segments[i].posNow);
        }
    }

    public class CableSegment
    {
        public Vector3 posNow;
        public Vector3 posOld;

        public CableSegment(Vector3 pos)
        {
            posNow = pos;
            posOld = pos;
        }
    }
}
