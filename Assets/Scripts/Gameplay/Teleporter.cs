using RoboRyanTron.Unite2017.Events;
using RoboRyanTron.Unite2017.Variables;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Teleporter otherTeleporter;

    public bool HasExitTeleporter { get; set; }

    private Collider m_currentCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (otherTeleporter.HasExitTeleporter) return;

        if (other == null) return;
        if (other.attachedRigidbody == null) return;

        m_currentCollider = other;

        HasExitTeleporter = false;
        otherTeleporter.HasExitTeleporter = true;

        m_currentCollider.transform.position = otherTeleporter.transform.position;
    }

    private void OnTriggerExit(Collider other)
    {
        HasExitTeleporter = true;
        otherTeleporter.HasExitTeleporter = false;
    }
}