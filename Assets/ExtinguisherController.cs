using UnityEngine;
using UnityEngine.XR; // for VR input

public class ExtinguisherController : MonoBehaviour
{
    public SphereCollider smokeCollider;  // assign in Inspector
    public ParticleSystem smokeParticles; // assign in Inspector
    public XRNode controllerNode = XRNode.RightHand; // change to LeftHand if needed

    private InputDevice device;

    void Start()
    {
        device = InputDevices.GetDeviceAtXRNode(controllerNode);

        if (smokeCollider != null)
            smokeCollider.enabled = false; // start off

        if (smokeParticles != null)
            smokeParticles.Stop();
    }

    void Update()
    {
        if (!device.isValid)
            device = InputDevices.GetDeviceAtXRNode(controllerNode);

        if (device.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed))
        {
            if (triggerPressed)
            {
                if (smokeCollider != null) smokeCollider.enabled = true;
                if (smokeParticles != null && !smokeParticles.isPlaying) smokeParticles.Play();
            }
            else
            {
                if (smokeCollider != null) smokeCollider.enabled = false;
                if (smokeParticles != null && smokeParticles.isPlaying) smokeParticles.Stop();
            }
        }
    }
}
