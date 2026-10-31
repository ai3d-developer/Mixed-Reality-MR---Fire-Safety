using UnityEngine;
using System.Collections.Generic;

public class SmoothFireQuencher : MonoBehaviour
{
    private ParticleSystem fireParticles;
    private ParticleSystem.MainModule mainModule;
    private ParticleSystem.EmissionModule emissionModule;

    [Header("Extinguish Settings")]
    public string smokeTag = "ExtinguisherSmoke";
    public float extinguishSpeed = 0.3f;   // how fast progress goes up (0–1)
    public float sizeStart = 2f;
    public float sizeEnd = 0.1f;
    public float emissionStart = 50f;
    public float emissionEnd = 0f;
    public float fireHeight = 1.5f;        // height of the flame area

    private float extinguishProgress = 0f;
    private bool isExtinguishing = false;

    // For collision events
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    void Start()
    {
        fireParticles = GetComponent<ParticleSystem>();
        mainModule = fireParticles.main;
        emissionModule = fireParticles.emission;

        mainModule.startSize = sizeStart;
        emissionModule.rateOverTime = emissionStart;
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag(smokeTag))
        {
            ParticleSystem smoke = other.GetComponent<ParticleSystem>();
            int count = smoke.GetCollisionEvents(gameObject, collisionEvents);

            for (int i = 0; i < count; i++)
            {
                Vector3 hitPos = collisionEvents[i].intersection;

                // ✅ Check if hit is inside the fire vertical zone
                if (hitPos.y >= transform.position.y && hitPos.y <= transform.position.y + fireHeight)
                {
                    isExtinguishing = true; // only activate if smoke really touched fire
                }
            }
        }
    }

    void Update()
    {
        if (isExtinguishing && extinguishProgress < 1f)
        {
            extinguishProgress += extinguishSpeed * Time.deltaTime;
            extinguishProgress = Mathf.Clamp01(extinguishProgress);

            float newSize = Mathf.Lerp(sizeStart, sizeEnd, extinguishProgress);
            mainModule.startSize = newSize;

            float newEmission = Mathf.Lerp(emissionStart, emissionEnd, extinguishProgress);
            emissionModule.rateOverTime = newEmission;

            if (extinguishProgress >= 1f)
            {
                fireParticles.Stop();
                gameObject.SetActive(false);
            }
        }
    }
}
