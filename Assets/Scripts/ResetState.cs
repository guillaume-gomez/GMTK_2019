using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetState : MonoBehaviour
{

    public bool testReset = false;

    public interface ResetableComponents
    {
        void ResetComponent();
    }

    private ResetableComponents[] resetableComps;
    private MeshRenderer[] renderers;
    private Vector3 m_startPosition;
    private Vector3 m_startScale;
    private Vector3 m_startEulers;

    // Start is called before the first frame update
    void Start()
    {
        renderers = GetComponentsInChildren<MeshRenderer>();
        resetableComps = GetComponentsInChildren<ResetableComponents>();
        m_startPosition = transform.position;
        m_startScale = transform.localScale;
        m_startEulers = transform.eulerAngles;

    }

    private void Update()
    {
        if (testReset)
        {
            ResetValueAndAnimate();
            testReset = false;
        }
    }

    public void ResetValueAndAnimate()
    {
        if (transform.position == m_startPosition &&
            m_startScale == transform.localScale &&
            m_startEulers == transform.eulerAngles)
        {
            return;
        }

        Sequence sequence = DOTween.Sequence();
        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i] == null) continue;
            for (int j = 0; j < renderers[i].materials.Length; j++)
            {
                sequence.Insert(0f, renderers[i].materials[j].DOFade(0f, 0.4f).SetEase(Ease.InSine));
                sequence.Insert(0.5f, renderers[i].materials[j].DOFade(1f, 0.4f).SetEase(Ease.OutSine));
            }
        }

        sequence.InsertCallback(0.4f, SetValuesAsStartingValues);

    }

    void SetValuesAsStartingValues()
    {
        transform.position = m_startPosition;
        transform.localScale = m_startScale;
        transform.eulerAngles = m_startEulers;
        for (int i = 0; i < resetableComps.Length; i++)
        {
            resetableComps[i].ResetComponent();
        }
    }


}
