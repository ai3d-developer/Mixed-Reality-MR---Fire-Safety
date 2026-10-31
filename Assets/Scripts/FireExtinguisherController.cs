using UnityEngine;

public class FireExtinguisherController : MonoBehaviour
{
    public ParticleSystem sprayParticles;
    private bool pinRemoved = true; // set false if using pin

    public void Spray()
    {
        if (pinRemoved && sprayParticles != null && !sprayParticles.isPlaying)
            sprayParticles.Play();
    }

    public void StopSpray()
    {
        if (sprayParticles != null && sprayParticles.isPlaying)
            sprayParticles.Stop();
    }

    public void PinRemoved()
    {
        pinRemoved = true;
    }
}
