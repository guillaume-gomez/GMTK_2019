using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private enum MoveType
    {
        TRANSLATION,
        ROTATION
    }

    [Header("References")]
    [SerializeField] private Transform pointerReference;

    [Header("Parameters")]
    [SerializeField] private MoveType moveType = MoveType.TRANSLATION;
    [SerializeField] [Range(0,0.5f)] private float speed = 0.1f;

    private Vector3 m_startPosition = Vector3.zero;
    private bool m_hasReachedPointer = false;
    private bool m_hasReachedStartPos = false;

    void Start()
    {
        m_startPosition = transform.position;
    }

    private void Update()
    {
        if (!pointerReference)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.LogError("pointerReference is not set, can not move moving platform");
#endif
            return;
        }

        if (moveType == MoveType.TRANSLATION)
        {
            if (!m_hasReachedPointer)
                this.transform.position = Vector3.MoveTowards(this.transform.position, pointerReference.position, speed);
            else
                this.transform.position = Vector3.MoveTowards(this.transform.position, m_startPosition, speed);

            if (Vector3.Distance(transform.position, pointerReference.position) < 0.001f)
            {
                m_hasReachedStartPos = false;
                m_hasReachedPointer = true;

                return;
            }

            if (Vector3.Distance(transform.position, m_startPosition) < 0.001f)
            {
                m_hasReachedStartPos = true;
                m_hasReachedPointer = false;

                return;
            }
        }
        else if (moveType == MoveType.ROTATION)
        {

        }
    }
}