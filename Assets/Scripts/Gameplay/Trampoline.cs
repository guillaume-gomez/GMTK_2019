using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float heightForce = 0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == null) return;
        if (collision.collider.attachedRigidbody == null) return;
        Debug.Log("on collision enter");
        //var force = Vector3.up * heightForce - collision.collider.attachedRigidbody.velocity;
        //collision.collider.attachedRigidbody.AddForce(force, ForceMode.Impulse);   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == null) return;
        if (other.attachedRigidbody == null) return;
        Debug.Log("on trigger enter");

        var force = Vector3.up * heightForce - other.attachedRigidbody.velocity;
        other.attachedRigidbody.AddForce(force, ForceMode.Impulse);
    }
}
