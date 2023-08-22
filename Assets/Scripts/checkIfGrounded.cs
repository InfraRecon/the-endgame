using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkIfGrounded : MonoBehaviour
{    public bool isGrounded;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float gravity = -9.81f;

    Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        isGrounded = Physics.SphereCast(transform.position, groundDistance,Vector3.down, out hit, groundDistance, groundMask);

        //gravity
        if(!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
            transform.position = velocity * Time.deltaTime;
        }
    }
}
