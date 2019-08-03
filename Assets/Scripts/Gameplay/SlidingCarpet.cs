using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingCarpet : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private Vector3 slidingVector = Vector3.right;

    private Collider m_currentCollider;
    private bool m_isTriggered = false;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("on collision");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == null) return;
        if (other.attachedRigidbody == null) return;

        Debug.Log("on trigger");
        m_isTriggered = true;
        m_currentCollider = other;

    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("on trigger exit");
        m_isTriggered = false;
        m_currentCollider = null;
    }

    private void Update()
    {
        if (!m_isTriggered) return;
        m_currentCollider.transform.position += slidingVector;
    }
}
