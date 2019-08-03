using RoboRyanTron.Unite2017.Events;
using RoboRyanTron.Unite2017.Variables;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [Header("Game Events")]
    [SerializeField] private GameEvent onTeleporterCollision;

    [Header("Modular Variables")]
    [SerializeField] private VectorVariable teleporterPosition;
    [SerializeField] private VectorVariable otherTeleporterPosition;

    private Collider m_currentCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other == null) return;
        if (other.attachedRigidbody == null) return;

        if (!onTeleporterCollision)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.LogError("onTeleporterCollision game event not set, can be thrown");
#endif
            return;
        }

        m_currentCollider = other;
        onTeleporterCollision.Raise();
    }

    public void OnOtherTeleporterCollision()
    {
        if (!teleporterPosition)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.LogError("teleporterPosition modular variable not set, can not set teleporter position");
#endif
            return;
        }

        teleporterPosition.Value = transform.position;
    }

    public void OnOtherTeleporterPosReceived()
    {
        if (m_currentCollider == null) return;
        if (!otherTeleporterPosition)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.LogError("otherTeleporterPosition modular variable not set, can not teleport");
#endif
            return;
        }

        m_currentCollider.transform.position = otherTeleporterPosition.Value;
    }
}
