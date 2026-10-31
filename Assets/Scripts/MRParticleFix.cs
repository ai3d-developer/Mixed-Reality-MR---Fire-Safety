using UnityEngine;

[RequireComponent(typeof(ParticleSystemRenderer))]
public class MRParticleFix : MonoBehaviour
{
    void Start()
    {
        var rend = GetComponent<ParticleSystemRenderer>();
        var mat = rend.material;

        if (mat == null) return;

        mat.renderQueue = 4000;   // draw on top of passthrough

        if (mat.HasProperty("_ZWrite"))
            mat.SetInt("_ZWrite", 0);

        if (mat.HasProperty("_ZTest"))
            mat.SetInt("_ZTest", (int)UnityEngine.Rendering.CompareFunction.Always);
    }
}
