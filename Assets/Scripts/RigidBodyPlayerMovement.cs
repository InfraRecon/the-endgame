using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class RigidBodyPlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float dashSpeed;
    public float dashSpeedChangeFactor;

    public float groundDrag;

    public float jumpForce;
    public float gravity;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public Transform groundCheck;

    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    RigidbodyConstraints originalConstraints;
    private UnityEngine.AI.NavMeshAgent agent;

    ////////////
    // public float speed;
    // public float rotationSpeed;

    // public float jumpSpeed;
    // private CharacterController characterController;
    // private float ySpeed;

    private GameObject CM;

    public MovementState state;

    public enum MovementState
    {
        dashing,
        air
    }

    public bool dashing;

    // private PlayerInput playerInput;

    // private void Awake()
    // {
    //     playerInput = new PlayerInput();
    // }

    // private void OnEnable()
    // {
    //     playerInput.Enable();
    // }

    // private void OnDisable()
    // {
    //     playerInput.Disable();
    // }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        // characterController = GetComponent<CharacterController>();

        readyToJump = true;

        originalConstraints = GetComponent<Rigidbody>().constraints;
        rb.constraints = originalConstraints;
    }

    private void Update()
    {
        try 
        {
            CM = GameObject.Find("Menu");

            if (CM.activeSelf)
            {

            }
        }
        catch (Exception e) 
        {
            // ground check
            grounded = Physics.Raycast(groundCheck.position, Vector3.down, playerHeight * 1f + 0.5f, whatIsGround);
            // RaycastHit hit;
            //grounded = Physics.SphereCast(groundCheck.position,  playerHeight * 1f + 0.5f,transform.down,out hit ,whatIsGround);
            // ySpeed += Physics.gravity.y * Time.deltaTime;
            MyInput();
            SpeedControl();

            // handle drag
            if (grounded)
            {
                rb.drag = groundDrag;
                rb.constraints = originalConstraints;
            }
            else
            {
                rb.drag = 0;
            }
        }  

    }

    private void FixedUpdate()
    {
        try 
        {
            CM = GameObject.Find("Character Menu");

            if (CM.activeSelf)
            {

            }
        }
        catch (Exception e) 
        {
            MovePlayer();
            rb.AddForce(Vector3.down * gravity, ForceMode.Impulse);
        } 
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        Debug.Log("BUTTON");
        Debug.Log(Input.GetKey(jumpKey));
        Debug.Log("READY");
        Debug.Log(readyToJump);
        Debug.Log("RAY");
        Debug.Log(grounded);

        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Vector3 movementDirection = new Vector3(horizontalInput, 0 , verticalInput);
            float magnitude = Mathf.Clamp01(movementDirection.magnitude) * moveSpeed;
            movementDirection.Normalize();

            Jump();

            // Vector3 velocity = moveDirection * magnitude;
            // velocity.y = ySpeed;
            // characterController.Move(velocity * Time.deltaTime);

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;
    private MovementState lastState;
    private bool keepMomentum;

    private void StateHandler()
    {
        if(dashing)
        {
            state = MovementState.dashing;
            desiredMoveSpeed = dashSpeed;
            speedChangeFactor = dashSpeedChangeFactor;
        }
        else
        {
            state = MovementState.air;
            if(desiredMoveSpeed < moveSpeed)
            {
                desiredMoveSpeed = moveSpeed;
            }
            else
            {
                desiredMoveSpeed = moveSpeed;
            }
        }

        bool desiredMoveSpeedHasChanged = desiredMoveSpeed != lastDesiredMoveSpeed;

        if (lastState == MovementState.dashing) 
        {
            keepMomentum = true;
        }

        if(keepMomentum)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothlyLerpMoveSpeed());
        }
        else
        {
            StopAllCoroutines();
            moveSpeed = desiredMoveSpeed;
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;
        lastState = state;
    }

    private float speedChangeFactor;

    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        //smoothly lerp movementSpeed to desired value
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        float boostFactor = speedChangeFactor;

        while(time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);

            time += Time.deltaTime * boostFactor;

            yield return null;
        }

        moveSpeed = desiredMoveSpeed;
        speedChangeFactor = 1f;
        keepMomentum = false;
    }

    private void MovePlayer()
    {
        // if(state == MovementState.dashing)
        // {
        //     return;
        // }

        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if(grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }

        // in air
        else if(!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        if (agent.enabled)
        {
            // set the agents target to where you are before the jump
            // this stops her before she jumps. Alternatively, you could
            // cache this value, and set it again once the jump is complete
            // to continue the original move
            agent.SetDestination(transform.position);
            // disable the agent
            agent.updatePosition = false;
            agent.updateRotation = false;
            agent.isStopped = true;
        }

        // reset y velocity
        // rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        // rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        Debug.Log("I jumped");

        rb.velocity = Vector3.up * jumpForce;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        //ySpeed = jumpSpeed;
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    public void addToMoveSpeed(int addSpeed)
    {
        moveSpeed += addSpeed;
    }

    public void addToDashSpeed(int addSpeed)
    {
        dashSpeed += addSpeed;
    }

    public void addToJumpForce(int addForce)
    {
        jumpForce += addForce;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider != null && collision.collider.tag == "Level")
        {
            if (!grounded)
            {
                if (agent.enabled)
                {
                    agent.updatePosition = true;
                    agent.updateRotation = true;
                    agent.isStopped = false;
                }
                GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<Rigidbody>().useGravity = false;
            }
        }
    }
}