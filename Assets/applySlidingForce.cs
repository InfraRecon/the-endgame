using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class applySlidingForce : MonoBehaviour
{
    public float forceMagnitude = 10f;  // The magnitude of the continuous force
    private CharacterController controller;  // Reference to the CharacterController component
    public Transform forwardObject;

    private void Start()
    {
        // Get the CharacterController component attached to the game object
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Apply a continuous force to the character controller in the forward direction
        Vector3 force = forwardObject.forward * forceMagnitude;
        controller.Move(force * Time.deltaTime);
    }
}
