using RoboRyanTron.Unite2017.Events;
using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LineRenderer line;
    [SerializeField] private Transform laserSolid;
    [SerializeField] private GameObject laserEnd;
    
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
        laserSolid.gameObject.SetActive(enableLaser);

        if (m_checkLineStateCR != null) StopCoroutine(m_checkLineStateCR);
        if (interval == 0) return;

        m_checkLineStateCR = StartCoroutine(CheckLineStateAsync());
    }

    private void FixedUpdate()
    {
        if (!laserSolid.gameObject.activeSelf) return;
    }

    private void OnValidate()
    {
        if (!Application.isPlaying) return;

        laserSolid.gameObject.SetActive(enableLaser);

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
            laserSolid.gameObject.SetActive(!laserSolid.gameObject.activeSelf);
        }
    }
}