using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalVectorTest : MonoBehaviour
{

    public float targetSpeed = 2f;

    public AnimationCurve accelerationCurve;
    public AnimationCurve deccelerationCurve;
    public float accelerationTime;
    public float deccelerationTime;

    public float resultDelta = 0;

    private float m_lastAxisHoriztonal;

    private bool m_lastRightPressed = false;
    private bool m_lastLeftPressed = false;

    private float m_rightTimer;
    private float m_leftTimer;

    public float right;
    public float left;

    public float tempVerticalStep = 0.5f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        var vector = new Vector3(1f, 0f, 0f);
        vector.x *= resultDelta * targetSpeed * Time.fixedDeltaTime;

        rb.MovePosition(transform.position + vector);

        //Debug.Log("Up: " + Input.GetAxis("Vertical"));
        if (Input.GetAxis("Vertical") > 0f)
        {
            rb.AddForce(Vector3.up * tempVerticalStep, ForceMode.Impulse);
        }
    }

    void Update()
    {

        //un peu faite a la rapidos 
        bool rightPressed = Input.GetKey(KeyCode.RightArrow);
        bool leftPressed = Input.GetKey(KeyCode.LeftArrow);


        if (rightPressed)
        {
            if (!m_lastRightPressed) m_rightTimer = 0f;
            m_rightTimer += Time.deltaTime * 1f / accelerationTime;
            right = Mathf.Min(accelerationCurve.Evaluate(m_rightTimer), 1f);
        }
        else
        {
            if (m_lastRightPressed) m_rightTimer = 1f;
            m_rightTimer -= Time.deltaTime * 1f / deccelerationTime;
            right = Mathf.Min(deccelerationCurve.Evaluate(m_rightTimer), 1f);
        }

        if (leftPressed)
        {
            if (!m_lastLeftPressed) m_leftTimer = 0f;
            m_leftTimer += Time.deltaTime * 1f / accelerationTime;
            left = Mathf.Min(accelerationCurve.Evaluate(m_leftTimer), 1f);
        }
        else
        {
            if (m_lastLeftPressed) m_leftTimer = 1f;
            m_leftTimer -= Time.deltaTime * 1f / deccelerationTime;
            left = Mathf.Max(deccelerationCurve.Evaluate(m_leftTimer), 0f);
        }

        left *= -1;

        m_lastLeftPressed = leftPressed;
        m_lastRightPressed = rightPressed;

        resultDelta = left + right;
    }
}
