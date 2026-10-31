using UnityEngine;

public class FireExtinguish : MonoBehaviour
{
    private ParticleSystem fireParticles;

    void Start()
    {
        fireParticles = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("ExtinguisherSmoke"))
        {
            // Reduce fire emission gradually
            var emission = fireParticles.emission;
            emission.rateOverTime = Mathf.Max(0, emission.rateOverTime.constant - 5f);

            // Optional: disable completely when small
            if (emission.rateOverTime.constant <= 0)
            {
                fireParticles.Stop();
                gameObject.SetActive(false); // hides fire
            }
        }
    }
}
