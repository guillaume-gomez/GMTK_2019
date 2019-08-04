using RoboRyanTron.Unite2017.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimator : MonoBehaviour
{

    public BoolVariable allowPlayerInput;
    public bool testDeath = false;
    public bool testWin = false;
    private Rigidbody rb;
    public Collider playerCollider;

    private Vector3 m_startPos;
    private PlayerController m_controller;

    public float force = 10f;

    private bool m_playingDeath = false;

    void Start()
    {
        m_controller = transform.GetComponent<PlayerController>();
        m_startPos = transform.position;
        playerCollider = GetComponent<Collider>();
        rb = transform.GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (testDeath)
        {
            PlayDeathAnimation();
            testDeath = false;
        }
        else if (testWin)
        {
            PlayWinAnimation();
            testWin = false;
        }
    }

    public void PlayDeathAnimation()
    {

        if (m_playingDeath) return;
        m_playingDeath = true;

        allowPlayerInput?.SetValue(false);
        //playerCollider.enabled = false;
        transform.tag = "Untagged";
        rb.AddForce(R(-force, force), R(-force, force), 0f, ForceMode.Impulse);

        var limb = m_controller.GetFallingLimb();

        rb.DORotate(new Vector3(R(-45, 45f), R(-54f, 45f), R(60f, 120f)), 3f);

        Sequence sequence = DOTween.Sequence();
        sequence.InsertCallback(R(0f, 0.12f), limb.LoseFeet);
        sequence.InsertCallback(R(0.15f, 0.28f), limb.LoseHead);
        sequence.InsertCallback(R(0.1f, 0.2f), limb.LoseLeftArm);
        sequence.InsertCallback(R(0.1f, 0.2f), limb.LoseRightArm);
        sequence.Insert(1.25f, transform.DOScale(0f, 0.5f).SetDelay(1.25f).SetEase(Ease.InBack, 0.8f)).OnComplete(OnAnimationDeathComplete);


    }

    public void PlayWinAnimation()
    {
        allowPlayerInput.SetValue(false);
        rb.isKinematic = true;
        var collider = GetComponent<Collider>();
        if (collider) collider.isTrigger = true;

        Sequence sequence = DOTween.Sequence();

        GameObject door = GameObject.FindGameObjectWithTag("Door");
        if (door)
        {
            sequence.Insert(0f, transform.DOMove(door.transform.position, 0.5f).SetEase(Ease.InOutSine));
            sequence.Insert(0f, transform.DORotate(Vector3.forward * 360f, 0.5f, RotateMode.LocalAxisAdd).SetEase(Ease.InOutSine));
            sequence.Insert(0f, transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack, 1.9f));
        }
        else
        {
            var pos = transform.position;
            sequence.Append(transform.DOJump(pos, 1f, 2, 0.5f));
        }

        var limb = m_controller.GetFallingLimb();
        sequence.InsertCallback(R(0f, 0.12f), limb.LoseFeet);
        sequence.InsertCallback(R(0.15f, 0.28f), limb.LoseHead);
        sequence.InsertCallback(R(0.1f, 0.2f), limb.LoseLeftArm);
        sequence.InsertCallback(R(0.1f, 0.2f), limb.LoseRightArm);

    }

    void OnAnimationDeathComplete()
    {
        GameObject newPlayer = Instantiate(Resources.Load<GameObject>("CapsuleRobot"));
        newPlayer.transform.position = m_startPos;
        newPlayer.transform.SetParent(this.transform.parent);
        Destroy(this.gameObject);
    }

    private float R(float min, float max)
    {
        return Random.Range(min, max);
    }

}
