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

    private bool lockLeft = false;
    private bool lockRight = false;
    private bool lockJump = false;
    private bool lockAction = false;
    private bool wasLeft = false;
    private bool wasRight = false;
    private bool wasJump = false;

    public GameObject limbPool;
    private Rigidbody rb;
    private FallingLimb fLScript;
    private float horizontaltimeElapsed;
    private float verticaltimeElapsed;

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

    void FixedUpdate()
    {
        if(GameManager.instance.godMode) {
          resetState();
        }
        Vector3 movement = ComputedVector();
        rb.MovePosition(transform.position + (movement * Time.fixedDeltaTime));
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

    Vector3 ComputedVector()
    {
      Vector3 result = new Vector3();

      float vertical = Input.GetAxis("Vertical");
      if(vertical > 0 && !lockJump)
      {
        if (!allowPlayerInput) goto result;
        wasJump = true;
        verticaltimeElapsed += Time.deltaTime;
        float acceleration = verticalCurve.Evaluate(verticaltimeElapsed);
        result.y = jumpSpeed * acceleration;
        // god mode
        if(GameManager.instance.godMode)
        {
          fLScript.LoseFeet();
        }
      }

      if(vertical <= 0 && wasJump)
      {
        lockJump = true;
        verticaltimeElapsed = 0.0f;
        fLScript.LoseFeet();
      }

      if(GameManager.instance.godMode && vertical == 0) {
        verticaltimeElapsed = 0.0f;
      }

      if (Input.GetButtonUp("Action") && !lockAction)
      {
        // TODO
            if (!allowPlayerInput) goto result;

      }

      if(Input.GetButtonDown("Action"))
      {
            if (!allowPlayerInput) goto result;
            lockAction = true;
            fLScript.LoseHead();
      }


      float horizontal = Input.GetAxis("Horizontal");
      // if we pressed left and right at the same time
      if(Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow)) {
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
        if(GameManager.instance.godMode)
        {
          fLScript.LoseLeftArm();
        }
      } else if (horizontal > 0.0f)
      {
        wasRight = true;

        if(wasLeft)
        {
          lockLeft = true;
          fLScript.LoseRightArm();
        }
        //god mode
        if(GameManager.instance.godMode)
        {
          fLScript.LoseRightArm();
        }
      } else { //getAxisHorizontal = 0
        if(wasLeft)
        {
          lockLeft = true;
          fLScript.LoseRightArm();
        }

        if(wasRight)
        {
          lockRight = true;
          fLScript.LoseLeftArm();
        }
      }

      if(horizontal < 0.0f && !lockLeft) {
        horizontaltimeElapsed += Time.deltaTime;
        float acceleration = horizontalCurve.Evaluate(horizontaltimeElapsed);
        result.x = - speed * acceleration;
      } else if (horizontal > 0.0f && !lockRight) {
        horizontaltimeElapsed += Time.deltaTime;
        float acceleration = horizontalCurve.Evaluate(horizontaltimeElapsed);
        result.x = speed * acceleration;
      } else {
        horizontaltimeElapsed = 0.0f;
      }

        result: 
      // return the computed vector3
      return result;
    }
}
