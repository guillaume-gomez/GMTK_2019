using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using RoboRyanTron.Unite2017.Events;
using RoboRyanTron.Unite2017.Variables;

public class BrokenMirror : MonoBehaviour
{
    public float startDelay = 0.5f;
    public float duration = 0.6f;

    public BoolVariable allowPlayerInput;

    [SerializeField]
    bool m_animating = false;
    private float m_startScale;

    public bool isMainMenu = false;

    private void Start()
    {
        m_startScale = transform.GetChild(0).localScale.x;
        if (!isMainMenu && !m_animating) ResetAnim();
    }

    // Update is called once per frame
    public void Animate()
    {
        if (m_animating) return;
        m_animating = true;
        allowPlayerInput?.SetValue(false);
        var sequence = DOTween.Sequence();

        var cam = Camera.main;

        sequence.AppendInterval(startDelay);
        if (isMainMenu)
        {
            sequence.Append(cam.DOShakeRotation(duration * 0.33f, 3f, 4, 10f));
            sequence.Insert(0.1f, cam.DOOrthoSize(cam.orthographicSize * 0.33f, duration * 1.1f).SetEase(Ease.InCirc));
        }
        else
            sequence.Append(cam.DOShakeRotation(duration * 0.33f, 15f, 6, 20f));
        GameManager.instance.PlaySound("game_over_glass_break");

        for (int i = 0; i < transform.childCount; i++)
        {
            int index = i;
            sequence.InsertCallback(startDelay, () => transform.GetChild(index).gameObject.SetActive(true));

            //sequence.Insert(R(0f, 0.05f), transform.GetChild(i).DOLocalRotate(new Vector3(R(0f, 5f), R(0f, 5f), R(-10f, 10f)), 0.2f).SetEase(Ease.InCirc));
            var targetScale = transform.GetChild(i).localScale;
            targetScale.x *= R(0.77f, 1f);
            targetScale.y *= R(0.77f, 1f);
            sequence.Insert(startDelay + R(0.1f, 0.12f), transform.GetChild(i).DOScale(targetScale, R(0.08f, 0.12f)).SetEase(Ease.InOutSine));

            sequence.Insert(startDelay + R(0.1f, 0.75f), transform.GetChild(i).DOLocalRotate(new Vector3(R(0f, 5f), R(0f, 5f), R(-10f, 10f)), duration)
                 .SetLoops(2, LoopType.Incremental));
            var pos = transform.GetChild(i).localPosition;
            pos.y += 20f;
            if (i % 3 == 0) pos.z += 30f;
            pos.x += R(-6f, 6f);
            sequence.Insert(startDelay + R(0.33f, 0.7f), transform.GetChild(i).DOLocalMove(pos, duration).SetEase(Ease.InBack, R(0.25f, 0.65f)));
        }

        sequence.InsertCallback(startDelay + duration / 2f, () => allowPlayerInput?.SetValue(true));

        sequence.OnComplete(ResetAnim);
    }

    private void ResetAnim()
    {
        m_animating = false;
        allowPlayerInput?.SetValue(true);

        Vector3 localPos = Vector3.zero;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
            transform.GetChild(i).localPosition = localPos;
            transform.GetChild(i).localRotation = Quaternion.identity;
            transform.GetChild(i).localScale = Vector3.one * m_startScale;
            localPos.z += 0.5f;
        }
    }

    float R(float min, float max)
    {
        return Random.Range(min, max);
    }

}
