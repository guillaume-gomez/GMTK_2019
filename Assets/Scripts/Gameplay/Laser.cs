using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LineRenderer line;
    [SerializeField] private CapsuleCollider lineCollider;

    [Header("Parameters")]
    [SerializeField] private bool enableLaser;

    private Ray m_ray;
    private RaycastHit m_hit;

    private void Start()
    {
        line.enabled = enableLaser;

        //lineCollider.radius = line.startWidth / 2;
        //lineCollider.center = Vector3.zero;
        //lineCollider.direction = 2;
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
            //if (!m_hit.rigidbody) return;

            //m_hit.rigidbody.AddForceAtPosition(transform.right * 5, m_hit.point);
        }
        else
            line.SetPosition(1, m_ray.GetPoint(100));
    }

    private void Update()
    {
        if (m_hit.rigidbody == null) return;

        //lineCollider.transform.position = this.transform.position + (m_hit.rigidbody.position - this.transform.position) / 2;
        //lineCollider.transform.LookAt(this.transform.position);
        //lineCollider.height = (m_hit.rigidbody.position - this.transform.position).magnitude;
    }
}
