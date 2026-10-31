using UnityEngine;

public class BurnFade : MonoBehaviour
{
    public Renderer cubeRenderer;
    public ParticleSystem fireParticle;

    private bool startBurning = false;
    private float burnProgress = 0f;

    void Update()
    {
        if (!startBurning && fireParticle != null && !fireParticle.isPlaying)
        {
            startBurning = true;
        }

        if (startBurning && burnProgress < 1f)
        {
            burnProgress += Time.deltaTime * 0.5f; // speed
            Color current = Color.Lerp(Color.white, Color.black, burnProgress);
            cubeRenderer.material.color = current;
        }
    }
}
