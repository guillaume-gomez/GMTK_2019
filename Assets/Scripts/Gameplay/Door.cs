﻿using RoboRyanTron.Unite2017.Events;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameEvent onDoorCollision;

    private void OnCollisionEnter(Collision collision)
    {
        if (!onDoorCollision)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.LogError("onDoorCollision variable not set, can not throw event");
#endif
            return;
        }

        onDoorCollision.Raise();
    }
}