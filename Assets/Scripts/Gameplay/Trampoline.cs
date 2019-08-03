using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float heightForce = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other == null) return;
        if (other.attachedRigidbody == null) return;

        var force = Vector3.up * heightForce - other.attachedRigidbody.velocity;
        other.attachedRigidbody.AddForce(force, ForceMode.Impulse);
    }
}