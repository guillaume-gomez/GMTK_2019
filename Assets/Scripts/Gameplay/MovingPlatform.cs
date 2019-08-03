﻿using System.Collections;
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

    [Header("Rotation parameters")]
    [SerializeField] [Range(0,0.5f)] private float translationSpeed = 0.1f;

    [Header("Translation parameters")]
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float rotationRadius = 2.5f;

    private Vector3 m_startPosition = Vector3.zero;
    private bool m_hasReachedPointer = false;
    private bool m_hasReachedStartPos = false;

    private float m_posX;
    private float m_posY;
    private float m_angle = 0f;

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
                this.transform.position = Vector3.MoveTowards(this.transform.position, pointerReference.position, translationSpeed);
            else
                this.transform.position = Vector3.MoveTowards(this.transform.position, m_startPosition, translationSpeed);

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
            m_posX = pointerReference.position.x + Mathf.Cos(m_angle) * rotationRadius;
            m_posY = pointerReference.position.y + Mathf.Sin(m_angle) * rotationRadius;
            transform.position = new Vector3(m_posX, m_posY, transform.position.z);
            m_angle = m_angle + Time.deltaTime * rotationSpeed;

            if (m_angle >= 360f)
                m_angle = 0f;
        }
    }
}