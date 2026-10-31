using UnityEngine;
using System.Collections.Generic;

public class LocalBoxFireQuencher : MonoBehaviour
{
    private ParticleSystem fireParticles;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    [Header("Extinguish Settings")]
    public string smokeTag = "ExtinguisherSmoke";
    public float killRadius = 0.3f;
    public BoxCollider fireBox;  // assign your box collider in Inspector

    void Start()
    {
        fireParticles = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {
        if (!other.CompareTag(smokeTag)) return;

        ParticleSystem smoke = other.GetComponent<ParticleSystem>();
        int count = smoke.GetCollisionEvents(gameObject, collisionEvents);

        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[fireParticles.particleCount];
        int alive = fireParticles.GetParticles(particles);

        for (int i = 0; i < count; i++)
        {
            Vector3 hitPos = collisionEvents[i].intersection;

            // ✅ Check if hit point is inside fireBox
            if (!fireBox.bounds.Contains(hitPos)) continue;

            for (int j = 0; j < alive; j++)
            {
                Vector3 worldPos = fireParticles.transform.TransformPoint(particles[j].position);

                float dist = Vector3.Distance(worldPos, hitPos);

                if (dist <= killRadius)
                {
                    particles[j].remainingLifetime = 0f; // kill only particles in box
                }
            }
        }

        fireParticles.SetParticles(particles, alive);
    }
}
