using UnityEngine;

public class FireQuencher : MonoBehaviour
{
    private ParticleSystem fireParticles;
    private ParticleSystem.MainModule mainModule;

    [Header("Settings")]
    public string extinguisherTag = "Extinguisher";
    public float quenchDuration = 5f; // Time to fully quench fire (seconds)

    private bool isQuenching = false;
    private float quenchTimer = 0f;
    private float initialSize;

    void Start()
    {
        fireParticles = GetComponent<ParticleSystem>();
        mainModule = fireParticles.main;
        initialSize = mainModule.startSizeMultiplier; // remember the starting size
    }

    void Update()
    {
        if (isQuenching && fireParticles != null)
        {
            quenchTimer += Time.deltaTime;
            float progress = Mathf.Clamp01(quenchTimer / quenchDuration);

            // Shrink fire over time
            mainModule.startSizeMultiplier = Mathf.Lerp(initialSize, 0f, progress);

            if (progress >= 1f)
            {
                fireParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                isQuenching = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(extinguisherTag))
        {
            isQuenching = true;
            quenchTimer = 0f; // reset timer when extinguisher enters
        }
    }
}
