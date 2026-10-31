using UnityEngine;

public class FireController : MonoBehaviour
{
    public ParticleSystem fireParticles;
    private bool isBurning = true;

    void Start()
    {
        if (fireParticles == null)
            fireParticles = GetComponent<ParticleSystem>();

        fireParticles.Play();
    }

    public void QuenchFire()
    {
        if (isBurning)
        {
            isBurning = false;
            fireParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            Debug.Log("🔥 Fire quenched!");
        }
    }
}
