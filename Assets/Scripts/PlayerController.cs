using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float jumpSpeed = 1.0f;
    public float speed = 1.0f;
    public float accelerationTime = 0.05f;
    public float decelerationTime = 0.05f;

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
        Vector3 movement = ComputedVector();
        rb.AddForce(movement);
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
        wasJump = true;
        result.y = jumpSpeed;
      }

      if(vertical <= 0 && wasJump)
      {
        lockJump = true;
        fLScript.LoseFeet();
      }

      if(Input.GetButtonUp("Action") && !lockAction)
      {
        // TODO

      }

      if(Input.GetButtonDown("Action"))
      {
        lockAction = true;
        fLScript.LoseHead();
      }

      float horizontal = Input.GetAxis("Horizontal");
      if(horizontal < 0)
      {
        wasLeft = true;

        if (wasRight)
        {
          lockRight = true;
          fLScript.LoseLeftArm();
        }
      } else if (horizontal > 0)
      {
        wasRight = true;

        if(wasLeft)
        {
          lockLeft = true;
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

      if(horizontal < 0 && !lockLeft) {
        result.x = - speed;
      } else if (horizontal > 0 && !lockRight) {
        result.x = speed;
      }
      // return the computed vector3
      return result;
    }
}
