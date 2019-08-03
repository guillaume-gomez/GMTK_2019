using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BrokenMirror : MonoBehaviour
{
    public bool animate = false;
    public float duration = 0.6f;

    private float m_startScale;

    private void Start()
    {
        m_startScale = transform.GetChild(0).localScale.x;
        ResetAnim();
    }

    // Start is called before the first frame update
    void Update()
    {
        if (animate)
        {
            Animate();
            animate = false;
        }
    }


    [SerializeField]
    bool m_animating = false;

    // Update is called once per frame
    public void Animate()
    {
        if (m_animating) return;
        m_animating = true;

        var sequence = DOTween.Sequence();

        var cam = Camera.main;

        sequence.Append(cam.DOShakeRotation(duration * 0.25f, 3f, 4, 10f));

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);

            //sequence.Insert(R(0f, 0.05f), transform.GetChild(i).DOLocalRotate(new Vector3(R(0f, 5f), R(0f, 5f), R(-10f, 10f)), 0.2f).SetEase(Ease.InCirc));
            var targetScale = transform.GetChild(i).localScale;
            targetScale.x *= R(0.88f, 1.1f);
            targetScale.y *= R(0.88f, 1.1f);
            sequence.Insert(R(0.1f, 0.12f), transform.GetChild(i).DOScale(targetScale, 0.1f).SetEase(Ease.InExpo));

            sequence.Insert(R(0.1f, 0.75f), transform.GetChild(i).DOLocalRotate(new Vector3(R(0f, 5f), R(0f, 5f), R(-10f, 10f)), duration)
                 .SetLoops(2, LoopType.Incremental));
            var pos = transform.GetChild(i).localPosition;
            pos.y += 20f;
            if (i % 3 == 0) pos.z += 50f;
            pos.x += R(-6f, 6f);
            sequence.Insert(R(0.33f, 0.7f), transform.GetChild(i).DOLocalMove(pos, duration).SetEase(Ease.InBack, 0.1f));
        }

        sequence.OnComplete(ResetAnim);
    }

    private void ResetAnim()
    {
        m_animating = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
            transform.GetChild(i).localPosition = Vector3.zero;
            transform.GetChild(i).localRotation = Quaternion.identity;
            transform.GetChild(i).localScale = Vector3.one * m_startScale;
        }
    }

    float R(float min, float max)
    {
        return Random.Range(min, max);
    }

}
