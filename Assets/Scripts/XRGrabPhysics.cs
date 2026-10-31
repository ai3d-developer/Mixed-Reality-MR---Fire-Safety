using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
public class XRGrabPhysics : MonoBehaviour
{
    private Rigidbody rb;
    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Start as kinematic (not affected by physics)
        rb.isKinematic = true;
        rb.useGravity = false;

        // Subscribe to grab/release events
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnDestroy()
    {
        // Unsubscribe when destroyed (avoid memory leaks)
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        rb.isKinematic = false;  // stays physical
        rb.useGravity = true;    // falls after release
    }
}
