using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rigidBodyObjectMove : MonoBehaviour
{
    public float speed = 5f; // The desired speed of the Rigidbody.
    public Vector3 direction = Vector3.forward; // The desired direction to move the Rigidbody in.

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + (direction.normalized * speed * Time.fixedDeltaTime));
    }
}
