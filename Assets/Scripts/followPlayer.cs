using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public Transform objectToFollow;
    public ThirdPersonCameraMovement thirdPersonCameraMovement;
    public Vector3 offset;
    public bool followX = false;
    public bool followY = false;
    public bool followZ = false;


    // public Transform target;
    // public float smoothTime = 0.3F;
    // private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        //         // Define a target position above and behind the target transform
        // Vector3 targetPosition = target.TransformPoint(new Vector3(0, 5, -10));

        // // Smoothly move the camera towards that target position
        // transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        if(followX)
        {
            transform.position = new Vector3(objectToFollow.position.x,transform.position.y, transform.position.z) + offset;
        }

        if(!Input.GetKey(KeyCode.X))
        {
            if(followY && thirdPersonCameraMovement.isGrounded)
            {
                transform.position = new Vector3(transform.position.x,objectToFollow.position.y, transform.position.z) + offset;
            }
        }


        if(followZ)
        {
            transform.position = new Vector3(transform.position.x,transform.position.y, objectToFollow.position.z) + offset;
        }
    
    }
}
