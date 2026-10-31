using UnityEngine;
using System.Collections.Generic;

public class LocalFireQuencher : MonoBehaviour
{
    private ParticleSystem fireParticles;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    [Header("Extinguish Settings")]
    public string smokeTag = "ExtinguisherSmoke";
    public float killRadius = 0.3f;   // how much area around hit to extinguish

    void Start()
    {
        fireParticles = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {
        if (!other.CompareTag(smokeTag)) return;

        ParticleSystem smoke = other.GetComponent<ParticleSystem>();
        int count = smoke.GetCollisionEvents(gameObject, collisionEvents);

        // Get current fire particles
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[fireParticles.particleCount];
        int alive = fireParticles.GetParticles(particles);

        for (int i = 0; i < count; i++)
        {
            Vector3 hitPos = collisionEvents[i].intersection;

            // Loop through fire particles
            for (int j = 0; j < alive; j++)
            {
                float dist = Vector3.Distance(
                    fireParticles.transform.TransformPoint(particles[j].position),
                    hitPos
                );

                if (dist <= killRadius)
                {
                    particles[j].remainingLifetime = 0f; // kill this particle
                }
            }
        }

        // Apply changes
        fireParticles.SetParticles(particles, alive);
    }
}
