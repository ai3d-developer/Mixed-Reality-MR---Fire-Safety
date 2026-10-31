using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NozzleFollower : MonoBehaviour
{
    public Transform attachPoint; // Reference to HoseAttachPoint inside extinguisher
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    void LateUpdate()
    {
        // If not being held, follow extinguisher attach point
        if (!grabInteractable.isSelected && attachPoint != null)
        {
            transform.position = attachPoint.position;
            transform.rotation = attachPoint.rotation;
        }
    }
}
