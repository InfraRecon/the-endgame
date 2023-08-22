using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovementMechanics : MonoBehaviour
{
  public float wallSlideSpeed = 1.0f;
    public float wallJumpForce = 10.0f;
    public float wallJumpAngle = 45.0f;
    public LayerMask wallLayer;

    private bool isWallSliding = false;
    private CharacterController controller;
    private Vector3 velocity;
    public bool isTouchingWall = false;
    ThirdPersonCameraMovement thirdPersonCameraMovement;

    void Start()
    {
        thirdPersonCameraMovement = GetComponent<ThirdPersonCameraMovement>();
    }

    void FixedUpdate()
    {
        // Check if the object is touching a wall
        isTouchingWall = Physics.Raycast(transform.position, transform.forward, 5f, wallLayer);

        if (isTouchingWall )//&& Mathf.Abs(velocity.x) < 0.1f && Input.GetAxisRaw("Horizontal") == 0)
        {
            // Start wall sliding
            velocity.y = -wallSlideSpeed;
            isWallSliding = true;
        }
        else
        {
            // Stop wall sliding
            isWallSliding = false;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isWallSliding)
            {
                // Perform wall jump
                Vector3 jumpDirection = new Vector3(-Mathf.Sign(transform.localScale.x), 1.0f, 0.0f);
                Vector3 jumpVelocity = jumpDirection.normalized * wallJumpForce;
                velocity = jumpVelocity;

                // Apply wall jump angle
                float angle = wallJumpAngle * Mathf.Deg2Rad;
                Vector3 jumpDirectionWithAngle = new Vector3(jumpVelocity.x * Mathf.Cos(angle) - jumpVelocity.y * Mathf.Sin(angle), jumpVelocity.x * Mathf.Sin(angle) + jumpVelocity.y * Mathf.Cos(angle), 0.0f);
                velocity = jumpDirectionWithAngle;

                // Flip the object horizontally
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }

        // Apply gravity and move the character
        velocity.y += Physics.gravity.y * Time.deltaTime;
        thirdPersonCameraMovement.gravity = velocity.y;
        controller.Move(-velocity * Time.deltaTime);
    }

    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        Gizmos.DrawRay(transform.position, direction);
    }
}
