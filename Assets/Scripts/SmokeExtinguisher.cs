using UnityEngine;

public class SmokeExtinguisher : MonoBehaviour
{
    void OnParticleCollision(GameObject other)
    {
        FireController fire = other.GetComponent<FireController>();
        if (fire != null)
        {
            fire.QuenchFire();
        }
    }
}
