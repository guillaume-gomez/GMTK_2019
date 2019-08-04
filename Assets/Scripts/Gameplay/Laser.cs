using RoboRyanTron.Unite2017.Events;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LineRenderer line;
    [SerializeField] private GameObject laserEnd;

    [Header("Game Events")]
    [SerializeField] private GameEvent onLaserCollision;

    [Header("Parameters")]
    [SerializeField] private bool enableLaser;

    private Ray m_ray;
    private RaycastHit m_hit;
    private float m_lineLength;
    private Vector3 m_colliderSize;
    private Vector3 m_midPoint;

    private void Start()
    {
        line.enabled = enableLaser;
    }

    private void FixedUpdate()
    {
        line.enabled = enableLaser;
        if (!line.enabled) return;

        m_ray = new Ray(transform.position, transform.right);

        line.SetPosition(0, m_ray.origin);

        if (Physics.Raycast(m_ray, out m_hit, 100))
        {
            line.SetPosition(1, m_hit.point);

            Debug.Log(m_hit.collider.name);
            if (m_hit.collider.name == laserEnd.name) return;
            if (!onLaserCollision)
            {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
                Debug.Log("onLaserCollision game event not set, can not be thrown");
#endif
                return;
            }

            onLaserCollision.Raise();
        }
        else
            line.SetPosition(1, m_ray.GetPoint(100));
    }
}