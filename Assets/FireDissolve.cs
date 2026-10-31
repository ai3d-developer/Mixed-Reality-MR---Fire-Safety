using UnityEngine;

public class FireDissolve : MonoBehaviour
{
    private ParticleSystem fireParticles;
    private ParticleSystem.MainModule mainModule;
    private ParticleSystem.EmissionModule emissionModule;

    [Header("Extinguish Settings")]
    public string smokeTag = "ExtinguisherSmoke"; // Tag of smoke particle system
    public float extinguishSpeed = 0.3f;          // Speed of extinguishing
    public float startSize = 2f;                  // Initial fire size
    public float endSize = 0f;                    // When fully extinguished
    public float startRate = 20f;                 // Initial emission rate
    public float endRate = 0f;                    // Final emission rate

    private float extinguishProgress = 0f;        // Goes from 0 → 1
    private bool isExtinguishing = false;

    void Start()
    {
        fireParticles = GetComponent<ParticleSystem>();
        mainModule = fireParticles.main;
        emissionModule = fireParticles.emission;

        mainModule.startSize = startSize;
        emissionModule.rateOverTime = startRate;
    }

    void Update()
    {
        if (isExtinguishing && extinguishProgress < 1f)
        {
            extinguishProgress += Time.deltaTime * extinguishSpeed;

            // Lerp size and emission
            mainModule.startSize = Mathf.Lerp(startSize, endSize, extinguishProgress);
            emissionModule.rateOverTime = Mathf.Lerp(startRate, endRate, extinguishProgress);

            // Stop system fully when finished
            if (extinguishProgress >= 1f)
            {
                fireParticles.Stop();
            }
        }
    }

    // Called when smoke particles collide with fire
    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag(smokeTag))
        {
            isExtinguishing = true;
        }
    }
}
