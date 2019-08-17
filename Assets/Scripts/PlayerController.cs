using RoboRyanTron.Unite2017.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    //public float speed = 1.0f;
    //public AnimationCurve horizontalCurve;
    //public AnimationCurve verticalCurve;

    [Header("References")]
    public BoolVariable allowPlayerInput;
    public ParticleSystem propellerLeft;
    public ParticleSystem propellerRight;
    public ParticleSystem propellerFeet;

    public Transform yRotator;
    public Transform xRotator;


    //private bool wasLeft = false;
    //private bool wasRight = false;
    //private bool wasJump = false;

    public GameObject limbPool;
    private Rigidbody rb;
    private FallingLimb fLScript;
    //private float horizontaltimeElapsed;
    //private float verticaltimeElapsed;

    [Header("Horizontal")]
    public float horizontalMaxSpeed;
    public AnimationCurve horizontalAccelerationCurve;
    public AnimationCurve horizontalDeccelerationCurve;
    public float horizontalAccelerationTime;
    public float horiztonalDeccelerationTime;


    [Header("Vertical")]
    public float verticalMaxSpeed = 10f;
    public AnimationCurve verticalAccelerationCurve;
    public AnimationCurve verticalDeccelerationCurve;
    public float verticalAccelerationTime;
    public float verticalDeccelerationTime;


    private bool m_lastUpPressed = false;
    private bool m_lastRightPressed = false;
    private bool m_lastLeftPressed = false;

    private float m_rightTimer;
    private float m_leftTimer;
    private float m_upTimer;

    //Sounds management
    private bool snd_isPropelled = false;


    [Header("Read Only")]
    public float right;
    public float left;
    public float up = 0f;
    Vector3 m_movement = new Vector3();
    [SerializeField]
    private bool lockLeft = false;
    [SerializeField]
    private bool lockRight = false;
    [SerializeField]
    private bool lockJump = false;
    [SerializeField]
    private bool lockAction = false;

    Vector3 m_currentXRotatorEulers = new Vector3();
    Vector3 m_currentYRotatorEulers = new Vector3();
    private AudioSource audioData;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fLScript = limbPool.GetComponent<FallingLimb>();
        audioData = GetComponent<AudioSource>();
    }

    // I read that input management should be here. I will do everything in FixedUpdate for the moment
    // Don't hesitate to correct the code if a miss something :)
    void Update()
    {

    }

    public float jumpForce;
    public ForceMode jumpForceMode;

    void FixedUpdate()
    {
        if (Application.isEditor && GameManager.instance.godMode)
        {
            resetState();
        }
        //Vector3 movement = ComputedVector();

        if (allowPlayerInput == false) return;

        m_movement.x = ComputeHorizontal();
        m_movement.y = 0f;
        m_movement.z = 0f;

        jumpForce = computeVertical();
        rb.MovePosition(transform.position + (m_movement * Time.fixedDeltaTime));
        rb.AddForce(Vector3.up * jumpForce * Time.fixedDeltaTime, jumpForceMode);


        m_currentXRotatorEulers.x = GetCurrentHorizontal() * 17f;
        m_currentYRotatorEulers.y = GetCurrentHorizontal() * -40f;

        xRotator.localEulerAngles = m_currentXRotatorEulers;
        yRotator.localEulerAngles = m_currentYRotatorEulers;
        
    }

    public FallingLimb GetFallingLimb()
    {
        return fLScript;
    }

    public float GetCurrentHorizontal()
    {
        return left + right;
    }

    void resetState()
    {
        lockLeft = false;
        lockRight = false;
        lockJump = false;
        lockAction = false;
        //wasLeft = false;
        //wasRight = false;
        //wasJump = false;
    }

    float ComputeHorizontal()
    {
        if (!allowPlayerInput) return 0f;

        bool rightPressed = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        bool leftPressed = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);

        bool upPressed = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);

        if (snd_isPropelled == true && rightPressed == false && leftPressed == false && upPressed == false) {
            snd_isPropelled = false;
            StopJetPackSound();
            Debug.LogWarning("Stop propeller sound");
        }


        if (!lockRight && rightPressed)
        {
            if (!m_lastRightPressed)
            {
                PlayJetPackSound();
                propellerLeft.Play();
                m_rightTimer = 0f;
            }
            m_rightTimer += Time.deltaTime * 1f / horizontalAccelerationTime;
            right = Mathf.Min(horizontalAccelerationCurve.Evaluate(m_rightTimer), 1f);
        }
        else
        {
            if (!lockRight && m_lastRightPressed)
            {
                fLScript.LoseLeftArm(true);
                m_rightTimer = 1f;
                lockRight = true;
                //StopJetPackSound();
                if(!leftPressed && !upPressed)
                {
                    propellerLeft.Stop();
                }
                PlayDestroyedComponentParticles(fLScript.headCenter.transform.position);
            }
            m_rightTimer -= Time.deltaTime * 1f / horiztonalDeccelerationTime;
            right = Mathf.Min(horizontalDeccelerationCurve.Evaluate(m_rightTimer), 1f);
        }

        if (!lockLeft && leftPressed)
        {
            if (!m_lastLeftPressed)
            {
                PlayJetPackSound();
                propellerRight.Play();
                m_leftTimer = 0f;
            }
            m_leftTimer += Time.deltaTime * 1f / horizontalAccelerationTime;
            left = Mathf.Min(horizontalAccelerationCurve.Evaluate(m_leftTimer), 1f);
        }
        else
        {
            if (!lockLeft && m_lastLeftPressed)
            {
                fLScript.LoseRightArm(true);
                m_leftTimer = 1f;
                lockLeft = true;
                //StopJetPackSound();
                if(!rightPressed && !upPressed)
                {
                    propellerRight.Stop();
                }
                PlayDestroyedComponentParticles(fLScript.headCenter.transform.position);

            }
            m_leftTimer -= Time.deltaTime * 1f / horiztonalDeccelerationTime;
            left = Mathf.Max(horizontalDeccelerationCurve.Evaluate(m_leftTimer), 0f);
        }

        left *= -1;

        m_lastLeftPressed = leftPressed;
        m_lastRightPressed = rightPressed;

        return (left + right) * horizontalMaxSpeed;
    }



    float computeVertical()
    {
        if (!allowPlayerInput) return 0f;

        bool upPressed = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);

        bool rightPressed = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        bool leftPressed = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);

        if (snd_isPropelled == true && rightPressed == false && leftPressed == false && upPressed == false)
        {
            snd_isPropelled = false;
            StopJetPackSound();
            Debug.Log("Stop propeller sound");
        }


        if (!lockJump && upPressed)
        {
            if (!m_lastUpPressed)
            {
                //rb.useGravity = false;
                PlayJetPackSound();
                propellerFeet.Play();
                m_upTimer = 0f;
            }
            m_upTimer += Time.deltaTime * 1f / verticalAccelerationTime;
            up = Mathf.Min(verticalAccelerationCurve.Evaluate(m_upTimer), 1f);
        }
        else
        {
            //rb.useGravity = true;
            if (!lockJump && m_lastUpPressed)
            {
                fLScript.LoseFeet(true);
                m_upTimer = 1f;
                lockJump = true;
                //StopJetPackSound();
                if(!leftPressed && !rightPressed)
                {
                    propellerFeet.Stop();
                }
                PlayDestroyedComponentParticles(fLScript.headCenter.transform.position);
            }
            m_upTimer -= Time.deltaTime * 1f / horiztonalDeccelerationTime;
            up = Mathf.Max(verticalDeccelerationCurve.Evaluate(m_upTimer), 0f);
        }

        m_lastUpPressed = upPressed;

        return up * verticalMaxSpeed;
    }

    void PlayDestroyedComponentParticles(Vector3 pos)
    {
        pos.z = -2f;
        ParticleSystem particles = Instantiate(Resources.Load<ParticleSystem>("Particles/particles_destruction"));
        if (particles)
        {
            particles.transform.position = pos;
            Destroy(particles.gameObject, 2f);
        }
    }

    void PlayJetPackSound() {
        snd_isPropelled = true;
        if (!GameManager.instance.muteFx)
        {
            audioData.Play();
        }
    }

    void StopJetPackSound() {
        if(!GameManager.instance.muteFx)
        {
            audioData.Stop();
        }
    }

}
