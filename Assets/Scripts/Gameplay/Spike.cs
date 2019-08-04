using RoboRyanTron.Unite2017.Events;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameEvent onSpikeCollision;
    [SerializeField] private GameEvent onPlayerDied;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision == null) return;
        if (collision.collider == null) return;
        if (collision.collider.attachedRigidbody == null) return;

        onPlayerDied?.Raise(); 

        if (!onSpikeCollision) return;

        onSpikeCollision.Raise();
    }
}
