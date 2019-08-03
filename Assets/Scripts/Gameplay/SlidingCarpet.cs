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
    [SerializeField] [Range(0, 1)] private float speed = 0.1f;

    private List<Collider> m_collidersSet = new List<Collider>();
    private Collider m_currentCollider;
    private bool m_isTriggered = false;
    private Vector3 m_slidingVector = Vector3.zero;

    private void OnTriggerEnter(Collider other)
    {
        if (other == null) return;
        if (other.attachedRigidbody == null) return;

        m_isTriggered = true;
        m_collidersSet.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        m_collidersSet.Remove(other);
        if (m_collidersSet.Count == 0) m_isTriggered = false;
    }

    private void Update()
    {
        if (!m_isTriggered) return;

        int count = m_collidersSet.Count;
        if (m_collidersSet.Count == 0) return;

        m_slidingVector.x = direction == Direction.LEFT ? speed * -1 : speed;

        for (int i = 0; i < count; i++)
        {
            m_currentCollider = m_collidersSet[i];

            if (m_currentCollider.transform.parent == null)
                m_currentCollider.transform.position += m_slidingVector;
            else
                m_currentCollider.transform.parent.position += m_slidingVector;
        }
    }
}
