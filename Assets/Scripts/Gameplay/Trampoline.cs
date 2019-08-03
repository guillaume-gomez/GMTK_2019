using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float heightForce = 0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == null) return;
        if (collision.collider.attachedRigidbody == null) return;

        Debug.Log("on collision");
        var force = Vector3.up * heightForce - collision.collider.attachedRigidbody.velocity;
        Debug.Log(force);
        collision.collider.attachedRigidbody.AddForce(force, ForceMode.Impulse);   
    }
}
