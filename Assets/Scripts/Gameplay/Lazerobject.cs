using RoboRyanTron.Unite2017.Events;
using UnityEngine;

public class Lazerobject : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameEvent onLaserCollision;
    [SerializeField] private GameEvent onPlayerDied;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision == null) return;
        if (collision.GetComponent<Collider>() == null) return;
        if (collision.GetComponent<Collider>().attachedRigidbody == null) return;

        onPlayerDied?.Raise(); 

        if (!onLaserCollision) return;

        onLaserCollision.Raise();
    }
}
