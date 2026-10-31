using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireExtinguisherPin : MonoBehaviour
{
    public FireExtinguisherController fireExtinguisher;
    private XRGrabInteractable grabInteractable;
    private bool pinRemoved = false;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (!pinRemoved)
        {
            transform.parent = null;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;

            pinRemoved = true;

            if (fireExtinguisher != null)
                fireExtinguisher.PinRemoved();
        }
    }

    private void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
    }
}
