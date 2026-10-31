using UnityEngine;
using System.Collections.Generic;

public class FireQuench : MonoBehaviour
{
    public ParticleSystem fireParticles;
    private ParticleSystem.MainModule mainModule;
    private ParticleSystem.EmissionModule emissionModule;

    private float currentSize;
    private float currentEmission;

    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    void Start()
    {
        mainModule = fireParticles.main;
        emissionModule = fireParticles.emission;

        currentSize = mainModule.startSize.constant;
        currentEmission = emissionModule.rateOverTime.constant;
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("ExtinguisherSmoke"))
        {
            ParticleSystem ps = other.GetComponent<ParticleSystem>();
            int numCollisionEvents = ps.GetCollisionEvents(gameObject, collisionEvents);

            for (int i = 0; i < numCollisionEvents; i++)
            {
                Vector3 hitPos = collisionEvents[i].intersection;

                // Only quench if the hit is inside the fire zone (near fire position)
                if (hitPos.y <= transform.position.y + 1f) // adjust fire height
                {
                    currentSize = Mathf.Max(0, currentSize - 2f * Time.deltaTime);
                    currentEmission = Mathf.Max(0, currentEmission - 4f * Time.deltaTime);

                    mainModule.startSize = currentSize;
                    emissionModule.rateOverTime = currentEmission;

                    if (currentSize <= 0.05f && currentEmission <= 0)
                    {
                        fireParticles.Stop();
                        gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
