using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRHandlePress : MonoBehaviour
{
    public FireExtinguisherController extinguisher;
    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // Start spraying while handle is held
        if (extinguisher != null)
            extinguisher.Spray();
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // Stop spraying when handle is released
        if (extinguisher != null)
            extinguisher.StopSpray();
    }

    private void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }
}
