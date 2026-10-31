using UnityEngine;

public class BurnMark : MonoBehaviour
{
    public Renderer cubeRenderer;
    public Material burnedMaterial;
    public ParticleSystem fireParticle;

    private bool burned = false;

    void Update()
    {
        if (!burned && fireParticle != null && !fireParticle.isPlaying)
        {
            // Fire finished, apply black material
            cubeRenderer.material = burnedMaterial;
            burned = true;
        }
    }
}
