using RoboRyanTron.Unite2017.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float jumpSpeed = 1.0f;
    public float speed = 1.0f;
    public AnimationCurve horizontalCurve;
    public AnimationCurve verticalCurve;
    public BoolVariable allowPlayerInput;

    public ParticleSystem propellerLeft;
    public ParticleSystem propellerRight;
    public ParticleSystem propellerFeet;


    private bool wasLeft = false;
    private bool wasRight = false;
    private bool wasJump = false;

    public GameObject limbPool;
    private Rigidbody rb;
    private FallingLimb fLScript;
    private float horizontaltimeElapsed;
    private float verticaltimeElapsed;

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


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fLScript = limbPool.GetComponent<FallingLimb>();
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
        if (GameManager.instance.godMode)
        {
            resetState();
        }
        //Vector3 movement = ComputedVector();

        m_movement.x = ComputeHorizontal();
        m_movement.y = 0f;
        m_movement.z = 0f;

        jumpForce = computeVertical();
        rb.MovePosition(transform.position + (m_movement * Time.fixedDeltaTime));
        rb.AddForce(Vector3.up * jumpForce * Time.fixedDeltaTime, jumpForceMode);
    }

    public FallingLimb GetFallingLimb()
    {
        return fLScript;
    }

    void resetState()
    {
        lockLeft = false;
        lockRight = false;
        lockJump = false;
        lockAction = false;
        wasLeft = false;
        wasRight = false;
        wasJump = false;
    }

    float ComputeHorizontal()
    {
        if (!allowPlayerInput) return 0f;

        bool rightPressed = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.A);
        bool leftPressed = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.D);


        if (!lockRight && rightPressed)
        {
            if (!m_lastRightPressed)
            {
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
                fLScript.LoseLeftArm();
                m_rightTimer = 1f;
                lockRight = true;
                propellerLeft.Stop();
            }
            m_rightTimer -= Time.deltaTime * 1f / horiztonalDeccelerationTime;
            right = Mathf.Min(horizontalDeccelerationCurve.Evaluate(m_rightTimer), 1f);
        }

        if (!lockLeft && leftPressed)
        {
            if (!m_lastLeftPressed)
            {
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
                fLScript.LoseRightArm();
                m_leftTimer = 1f;
                lockLeft = true;
                propellerRight.Stop();
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

        if (!lockJump && upPressed)
        {
            if (!m_lastUpPressed)
            {
                //rb.useGravity = false;
                propellerFeet.Play();
                GameManager.instance.PlaySound("jetpack");
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
                fLScript.LoseFeet();
                m_upTimer = 1f;
                lockJump = true;
                propellerFeet.Stop();
            }
            m_upTimer -= Time.deltaTime * 1f / horiztonalDeccelerationTime;
            up = Mathf.Max(verticalDeccelerationCurve.Evaluate(m_upTimer), 0f);
        }

        m_lastUpPressed = upPressed;

        return up * verticalMaxSpeed;

        //float result = 0;
        //float vertical = Input.GetAxis("Vertical");
        //if (vertical > 0 && !lockJump)
        //{
        //    GameManager.instance.PlaySound("jetpack");
        //    propellerFeet.Emit(1);
        //    wasJump = true;
        //    verticaltimeElapsed += Time.deltaTime;
        //    float acceleration = verticalCurve.Evaluate(verticaltimeElapsed);
        //    result = jumpSpeed * acceleration;
        //    // god mode
        //    if (GameManager.instance.godMode)
        //    {
        //        fLScript.LoseFeet();
        //    }
        //}

        //if (vertical <= 0 && wasJump)
        //{
        //    lockJump = true;
        //    verticaltimeElapsed = 0.0f;
        //    fLScript.LoseFeet();
        //}

        //if (GameManager.instance.godMode && vertical == 0)
        //{
        //    verticaltimeElapsed = 0.0f;
        //}

        //return result;
    }

    Vector3 ComputedVector()
    {
        Vector3 result = new Vector3();

        float vertical = Input.GetAxis("Vertical");
        if (vertical > 0 && !lockJump)
        {
            GameManager.instance.PlaySound("jetpack");
            propellerFeet.Emit(1);
            if (!allowPlayerInput) goto result;
            wasJump = true;
            verticaltimeElapsed += Time.deltaTime;
            float acceleration = verticalCurve.Evaluate(verticaltimeElapsed);
            result.y = jumpSpeed * acceleration;
            // god mode
            if (GameManager.instance.godMode)
            {
                fLScript.LoseFeet();
            }
        }

        if (vertical <= 0 && wasJump)
        {
            lockJump = true;
            verticaltimeElapsed = 0.0f;
            fLScript.LoseFeet();
        }

        if (GameManager.instance.godMode && vertical == 0)
        {
            verticaltimeElapsed = 0.0f;
        }

        // for post mortem
        /*if (Input.GetButtonUp("Action") && !lockAction)
        {
          if (!allowPlayerInput) goto result;
        }

        if(Input.GetButtonDown("Action"))
        {
              if (!allowPlayerInput) goto result;
              lockAction = true;
              fLScript.LoseHead();
        }*/


        float horizontal = Input.GetAxis("Horizontal");
        // if we pressed left and right at the same time
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            horizontal = 0.0f;
            if (!allowPlayerInput) goto result;

        }

        if (horizontal < 0.0f)
        {
            wasLeft = true;

            if (wasRight)
            {
                lockRight = true;
                fLScript.LoseLeftArm();
            }
            // god mode
            if (GameManager.instance.godMode)
            {
                fLScript.LoseLeftArm();
            }
        }
        else if (horizontal > 0.0f)
        {
            wasRight = true;

            if (wasLeft)
            {
                lockLeft = true;
                fLScript.LoseRightArm();
            }
            //god mode
            if (GameManager.instance.godMode)
            {
                fLScript.LoseRightArm();
            }
        }
        else
        { //getAxisHorizontal = 0
            if (wasLeft)
            {
                lockLeft = true;
                fLScript.LoseRightArm();
            }

            if (wasRight)
            {
                lockRight = true;
                fLScript.LoseLeftArm();
                //propeller.Stop();
            }
        }

        if (horizontal < 0.0f && !lockLeft)
        {
            propellerRight.Emit(1);
            horizontaltimeElapsed += Time.deltaTime;
            float acceleration = horizontalCurve.Evaluate(horizontaltimeElapsed);
            result.x = -speed * acceleration;
        }
        else if (horizontal > 0.0f && !lockRight)
        {
            propellerLeft.Emit(1);
            horizontaltimeElapsed += Time.deltaTime;
            float acceleration = horizontalCurve.Evaluate(horizontaltimeElapsed);
            result.x = speed * acceleration;
        }
        else
        {
            horizontaltimeElapsed = 0.0f;
        }

        result:
        // return the computed vector3
        return result;
    }
}
