using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private Transform playerTransform; // Reference to the player's transform
    public Transform rangeLocation;
    public float lookRange = 10f; // Range within which the object looks at the player
    public float smoothSpeed = 5f; // Speed of smooth rotation

    private Quaternion targetRotation; // Target rotation of the object
    private Quaternion initialRotation; // Initial rotation of the object

    public Camera mainCamera;

    public bool lookAtCamera = false;

    private void Start()
    {
        playerTransform = GameObject.Find("Player Center").transform;
        initialRotation = transform.rotation; // Store the initial rotation
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if(lookAtCamera)
        {
            transform.LookAt(mainCamera.transform);
        }
        else
        {
            // Check if the player transform is valid and within range
            if (playerTransform != null && Vector3.Distance(rangeLocation.position, playerTransform.position) <= lookRange)
            {
                // Calculate the direction from this object to the player
                Vector3 direction = playerTransform.position - transform.position;

                // Rotate this object to look at the player
                targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            }
            else
            {
                // Set the initial rotation as the target rotation to reset the rotation
                targetRotation = initialRotation;
            }

              // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the look range sphere in the Unity editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(rangeLocation.position, lookRange);
    }
}
