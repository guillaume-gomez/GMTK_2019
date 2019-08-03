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
    [SerializeField] private BoolVariable hasExitTeleporter;
    [SerializeField] private BoolVariable hasExitOtherTeleporter;

    private Collider m_currentCollider;

    private void Awake()
    {
        if (!hasExitTeleporter)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.LogError("hasExitOtherTeleporter not set, can not set value");
#endif
            return;
        }

        hasExitTeleporter.Value = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(name + " on trigger enter");

        if (!hasExitOtherTeleporter)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.LogError("hasExitOtherTeleporter not set, can not check teleporter");
#endif
            return;
        }

        if (hasExitOtherTeleporter.Value) return;
        //if (!hasExitTeleporter.Value) return;

        if (other == null) return;
        if (other.attachedRigidbody == null) return;

        if (!hasExitTeleporter)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.LogError("hasExitOtherTeleporter not set, can not check teleporter");
#endif
            return;
        }

        if (!onTeleporterCollision)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.LogError("onTeleporterCollision game event not set, can be thrown");
#endif
            return;
        }

        if (!otherTeleporterPosition)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.LogError("otherTeleporterPosition modular variable not set, can not teleport");
#endif
            return;
        }

        m_currentCollider = other;
        onTeleporterCollision.Raise();

        hasExitTeleporter.Value = false;
        hasExitOtherTeleporter.Value = true;

        m_currentCollider.transform.position = otherTeleporterPosition.Value;
    }

    private void OnTriggerExit(Collider other)
    {

        Debug.Log(name + " on trigger exit");
        if (!hasExitTeleporter)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.LogError("hasExitOtherTeleporter not set, can not check teleporter");
#endif
            return;
        }

        hasExitTeleporter.Value = true;
        hasExitOtherTeleporter.Value = false;
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
}
