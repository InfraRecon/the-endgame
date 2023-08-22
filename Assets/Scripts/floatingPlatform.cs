using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingPlatform : MonoBehaviour
{
// The amount to move the object down on the y-axis when the button is pressed
    public float moveDistance = 0.5f;

    // The speed at which to move the object
    public float moveSpeed = 1f;

    // The position of the object on the y-axis before it moves
    private float startY;

    // Whether the button is currently being pressed
    private bool isPressed = false;

    private void Start()
    {
        // Store the starting position of the object on the y-axis
        startY = transform.position.y;
    }

    private void Update()
    {
        // Check if the button is currently being pressed
        if (isPressed)
        {
            // Calculate the new position of the object based on the desired move distance
            float newY = Mathf.MoveTowards(transform.position.y, startY - moveDistance, moveSpeed * Time.deltaTime);

            // Move the object to its new position
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            // If the object has reached its desired position, stop moving
            if (transform.position.y <= startY - moveDistance)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }
        }
        else if(!isPressed)
        {
            // Calculate the new position of the object based on the original y-axis position
            float newY = Mathf.MoveTowards(transform.position.y, startY, moveSpeed * Time.deltaTime);

            // Move the object to its new position
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            // If the object has returned to its original position, stop moving
            if (transform.position.y >= startY)
            {
                transform.position = new Vector3(transform.position.x, startY, transform.position.z);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Start moving the object down on the y-axis
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Start moving the object down on the y-axis
            isPressed = false;
        }
    }
}
