using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExampleClass : MonoBehaviour
{
    public float thrust = 15f;
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.AddForce(transform.right * thrust);
    }
}