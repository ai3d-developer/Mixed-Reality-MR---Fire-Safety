using UnityEngine;
using UnityEngine.Events;

public class PinRemovable : MonoBehaviour
{
    public UnityEvent onRemoved;
    public float removeDistance = 0.15f;  // how far to pull before it counts as removed

    private Vector3 startLocalPos;
    private bool removed = false;

    void Start()
    {
        startLocalPos = transform.localPosition;
    }

    void Update()
    {
        if (removed) return;
        float dist = Vector3.Distance(transform.localPosition, startLocalPos);
        if (dist > removeDistance) RemovePin();
    }

    public void RemovePin()
    {
        if (removed) return;
        removed = true;

        transform.SetParent(null);
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = false;

        onRemoved?.Invoke();
        Debug.Log("Pin pulled out!");
    }
}
