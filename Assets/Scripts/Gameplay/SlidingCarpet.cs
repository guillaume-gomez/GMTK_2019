using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingCarpet : MonoBehaviour
{
    private enum Direction
    {
        RIGHT,
        LEFT
    }

    [Header("Parameters")]
    [SerializeField] private Direction direction = Direction.RIGHT;
    [SerializeField] [Range(0,1)] private float speed = 0.1f;

    private Collider m_currentCollider;
    private bool m_isTriggered = false;
    private Vector3 m_slidingVector = Vector3.zero;

    private void OnTriggerEnter(Collider other)
    {
        if (other == null) return;
        if (other.attachedRigidbody == null) return;

        m_isTriggered = true;
        m_currentCollider = other;
    }

    private void OnTriggerExit(Collider other)
    {
        m_isTriggered = false;
        m_currentCollider = null;
    }

    private void Update()
    {
        if (!m_isTriggered) return;

        m_slidingVector.x = direction == Direction.LEFT ? speed * -1 : speed;
        m_currentCollider.transform.position += m_slidingVector;
    }
}
