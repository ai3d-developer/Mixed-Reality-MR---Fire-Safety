using UnityEngine;
using UnityEngine.InputSystem;

public class FireHandle : MonoBehaviour
{
    public ParticleSystem sprayParticles;
    public InputActionProperty sprayAction;

    void Update()
    {
        float value = sprayAction.action.ReadValue<float>();
        Debug.Log("Trigger Value: " + value);

        if (value > 0.5f)
        {
            if (!sprayParticles.isPlaying)
            {
                Debug.Log("Spray Start!");
                sprayParticles.Play();
            }
        }
        else
        {
            if (sprayParticles.isPlaying)
            {
                Debug.Log("Spray Stop!");
                sprayParticles.Stop();
            }
        }
    }
}
