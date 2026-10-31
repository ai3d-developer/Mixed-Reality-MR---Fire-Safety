using UnityEngine;
using System.Collections.Generic;

public class FireDissolveByHit : MonoBehaviour
{
    private ParticleSystem fireParticles;
    private ParticleSystem.MainModule mainModule;
    private ParticleSystem.EmissionModule emissionModule;

    [Header("Extinguish Settings")]
    public string smokeTag = "ExtinguisherSmoke";
    public float extinguishSpeed = 0.5f; // dissolve speed
    public float radius = 0.5f;          // how far dissolve spreads from hit point

    private List<Vector3> hitPoints = new List<Vector3>(); // store smoke hit positions
    private float dissolveProgress = 0f;

    void Start()
    {
        fireParticles = GetComponent<ParticleSystem>();
        mainModule = fireParticles.main;
        emissionModule = fireParticles.emission;
    }

    void Update()
    {
        if (hitPoints.Count > 0 && dissolveProgress < 1f)
        {
            dissolveProgress += Time.deltaTime * extinguishSpeed;

            // Gradually reduce fire size and emission
            mainModule.startSize = Mathf.Lerp(mainModule.startSize.constant, 0f, dissolveProgress);
            emissionModule.rateOverTime = Mathf.Lerp(emissionModule.rateOverTime.constant, 0f, dissolveProgress);

            if (dissolveProgress >= 1f)
                fireParticles.Stop();
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (!other.CompareTag(smokeTag)) return;

        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        int eventCount = fireParticles.GetCollisionEvents(other, collisionEvents);

        for (int i = 0; i < eventCount; i++)
        {
            Vector3 hitPos = collisionEvents[i].intersection;
            hitPoints.Add(hitPos);

            // Debug sphere to visualize hit point
            GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            marker.transform.position = hitPos;
            marker.transform.localScale = Vector3.one * 0.1f;
            Destroy(marker, 1f);

            Debug.Log("Smoke hit fire at " + hitPos);
        }
    }
}
