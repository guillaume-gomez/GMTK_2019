using RoboRyanTron.Unite2017.Events;
using RoboRyanTron.Unite2017.Variables;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Teleporter otherTeleporter;

    public bool HasEnterTeleporter { get; set; } = false;
    public bool HasExitTeleporter { get; set; } = true;

    private Transform m_currentCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other == null) return;
        if (other.attachedRigidbody == null) return;

        if (!HasExitTeleporter) return;
        if (otherTeleporter.HasEnterTeleporter) return;
        if (m_currentCollider != null) return;

        m_currentCollider = other.attachedRigidbody.transform;

        HasExitTeleporter = false;
        HasEnterTeleporter = true;

        m_currentCollider.transform.position = otherTeleporter.transform.position;
    }

    private void OnTriggerExit(Collider other)
    {
        HasExitTeleporter = true;
        if (otherTeleporter.HasEnterTeleporter)
        {
            HasEnterTeleporter = false;
            otherTeleporter.HasEnterTeleporter = false;
        }

        if (m_currentCollider == null) return;
        m_currentCollider = null;
    }
}