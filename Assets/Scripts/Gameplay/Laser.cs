using RoboRyanTron.Unite2017.Events;
using System.Collections;
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
    [SerializeField] [Range(0, 10)] private float interval;

    private Ray m_ray;
    private RaycastHit m_hit;
    private float m_lineLength;
    private Vector3 m_colliderSize;
    private Vector3 m_midPoint;

    private Coroutine m_checkLineStateCR;

    private void Start()
    {
        line.enabled = enableLaser;

        if (m_checkLineStateCR != null) StopCoroutine(m_checkLineStateCR);
        if (interval == 0) return;

        m_checkLineStateCR = StartCoroutine(CheckLineStateAsync());
    }

    private void FixedUpdate()
    {
        if (!line.enabled) return;

        m_ray = new Ray(transform.position, transform.right);

        line.SetPosition(0, m_ray.origin);

        if (Physics.Raycast(m_ray, out m_hit, 100))
        {
            line.SetPosition(1, m_hit.point);

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

    private void OnValidate()
    {
        if (!Application.isPlaying) return;

        line.enabled = enableLaser;

        if (m_checkLineStateCR != null) StopCoroutine(m_checkLineStateCR);
        if (interval == 0) return;

        m_checkLineStateCR = StartCoroutine(CheckLineStateAsync());
    }

    private IEnumerator CheckLineStateAsync()
    {
        WaitForSeconds wfs = new WaitForSeconds(interval);
        while (true)
        {
            yield return wfs;
            line.enabled = !line.enabled;
        }
    }
}