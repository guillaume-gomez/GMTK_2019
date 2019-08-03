using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float heightForce = 0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == null) return;
        if (collision.collider.attachedRigidbody == null) return;

        var force = Vector3.up * heightForce - collision.collider.attachedRigidbody.velocity;
        collision.collider.attachedRigidbody.AddForce(force, ForceMode.Impulse);   
    }
}
