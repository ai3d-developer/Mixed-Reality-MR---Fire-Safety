using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GlowOnHover : MonoBehaviour
{
    public XRGrabInteractable grabInteractable;
    public Color glowColor = Color.green;
    private Material mat;
    private Color originalEmission;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        originalEmission = mat.GetColor("_EmissionColor");

        grabInteractable.hoverEntered.AddListener(OnHoverEnter);
        grabInteractable.hoverExited.AddListener(OnHoverExit);
    }

    void OnHoverEnter(HoverEnterEventArgs args)
    {
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", glowColor);
    }

    void OnHoverExit(HoverExitEventArgs args)
    {
        mat.SetColor("_EmissionColor", originalEmission);
    }

    private void OnDestroy()
    {
        grabInteractable.hoverEntered.RemoveListener(OnHoverEnter);
        grabInteractable.hoverExited.RemoveListener(OnHoverExit);
    }
}
