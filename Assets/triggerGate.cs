using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerGate : MonoBehaviour
{
    public GameObject targetObject;
    public Vector3 targetPosition;
    public float movementSpeed = 5f;

    private bool isMoving = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isMoving = true;
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            // Calculate the direction and distance to the target position
            Vector3 direction = targetPosition - targetObject.transform.localPosition ;
            float distance = direction.magnitude;

            if (distance > 0.1f)
            {
                // Normalize the direction vector
                direction.Normalize();

                // Calculate the amount to move this frame
                float movementAmount = movementSpeed * Time.deltaTime;

                // Move the game object towards the target position
                targetObject.transform.localPosition += direction * movementAmount;
            }
            else
            {
                // Stop moving when the target position is reached
                isMoving = false;
                Destroy(this);
            }
        }
    }
}
