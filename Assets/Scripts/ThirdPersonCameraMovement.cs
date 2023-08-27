using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonCameraMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator ghostAnimator;
    public static List<string> animationStates = new List<string>(new string[] { "IsIdling", "IsMoving", "IsFalling","IsDashing", "IsAttacking", "IsPushing", "IsDeading"});
    public Transform cam;
    public float speed;
    public float RunningSpeed;
    public float WalkingSpeed;
    public float gravity = -9.81f;
    public float jumpHeight = 3;
    Vector3 velocity;
    public bool isGrounded;
    public bool isTrapped;
    public bool isSliding;
    public Transform groundCheck;
    public float groundDistance = 1f;
    public LayerMask groundMask;
    public LayerMask trapMask;
    public LayerMask slideMask;
    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;
    ///
    private bool canDoubleJump = false; // Whether the character can perform a double jump

    [Header("ATTACK MECHANIC")]
    public Transform starterTargetObject;
    public Transform targetObject;
    public int randomAttack;
    public int randomPreviousAttack;
    public bool cameraLookAtEnemy = false;
    public float cooldownTime = 2f;
    public bool equiping = false;
    public GameObject swordEffect;
    public Transform swordEffectLocation;
    public List<Transform> enemyList = new List<Transform>();

    [Header("DASH MECHANIC")]
    public float dashMeterCap;
    public float currentDashMeter;
    public float dashConsumption;
    public GameObject dashEffect;

    public bool canSlam = false;
    public GameObject slamEffect;

    [Header("Cooldown")]
    public float dashCd;
    public float dashCdTimer;

    [Header("Movement")]
    public float dashSpeed;
    public float dashSpeedChangeFactor;
    public bool sliding = false;
    public float rotationSpeed = 10000000f; // The speed of rotation
    public float rotationLimit = 10f; // The maximum angle the gameobject can rotate
    public GameObject slidingEffect;

///Sliding
    public float forceMagnitude = 2f;  // The magnitude of the continuous force
    public bool keepSpeed = false;
    public GameObject slidingCam;
    // public Transform forwardObject;
    // public MovementState state;

    // public enum MovementState
    // {
    //     dashing,
    //     air
    // }
    // public bool dashing;

    [Header("AUDIO")]
    public playerAudioManager AudioManager;
    // PlayerControls controls;
    public Vector2 move;
    float smoothTime = 0.5f;
    float negSmoothTime = 0.1f;
    float yVelocity = 0.2f;
    float negyVelocity = -0.2f;
    public bool lockOn = false;
    //FadeINandOutScreen
    public loadingScreenFader fade;
    private float timeRemaining = 1;
    public float startTimeRemaining = 1;
    public bool IsAttackingInAir = false;
    
    void Start()
    {
        currentDashMeter = dashMeterCap;
        timeRemaining = startTimeRemaining;
        ghostAnimator.SetFloat("IdleBlend",0);
    }

    void Update()
    {
        //jump
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        isTrapped = Physics.CheckSphere(groundCheck.position, groundDistance, trapMask);

        isSliding = Physics.CheckSphere(groundCheck.position, groundDistance, slideMask);
        
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, groundDistance,Vector3.down, out hit, groundDistance, trapMask))
        {
            if(isTrapped)
            {
                Debug.Log("IN DA TRAP" + hit);
                Debug.Log("THE NAME: " + hit.collider.gameObject.transform.GetChild(0).gameObject.name);
                Debug.Log("THE POSITION: " + hit.collider.gameObject.transform.GetChild(0).gameObject.transform.position);

                if(timeRemaining == startTimeRemaining)
                {
                    fade.StartFade();
                    controller.enabled = false;
                }

                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                }
                else
                {
                    transform.position = hit.collider.gameObject.transform.GetChild(0).gameObject.transform.position;
                    controller.enabled = true;
                    timeRemaining = startTimeRemaining;
                }
            }
        }
        
        if(Physics.SphereCast(transform.position, groundDistance,Vector3.down, out hit, groundDistance, slideMask))
        {
            Transform forwardObject = hit.collider.gameObject.transform.GetChild(0).gameObject.transform;
            // Apply a continuous force to the character controller in the forward direction
            Vector3 force = forwardObject.forward * forceMagnitude;
            controller.Move(force/2 * Time.deltaTime);
            
            Quaternion targetRotation; // The target rotation

            // Get the direction to the target object
            Vector3 lookDirection = forwardObject.forward;
            lookDirection.y = 0f; // Optional: Ignore vertical difference

            if (lookDirection != Vector3.zero)
            {
                // Calculate the target rotation
                targetRotation = Quaternion.LookRotation(lookDirection);

                // Smoothly rotate towards the target rotation
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
            Sliding(true);
        }

        if (isGrounded && velocity.y < 0 && !isSliding)
        {
            velocity.y = -2f;
            ghostAnimator.SetBool("IsIdling", true);
            ghostAnimator.SetBool("IsFalling", false);
            keepSpeed = false;
            Sliding(false);

            // Choose a random animation to play
            float clipToPlay = Mathf.PerlinNoise(Time.time/5,Random.Range(0,1));

            // Play the animation
            ghostAnimator.SetFloat("IdleBlend", 0.6f);

        }
        else if(!isGrounded && !isSliding)
        {
            if(Input.GetKeyDown(KeyCode.C))
            {
                animationStater("IsAttacking");
                IsAttackingInAir = true;
            }
            
            if(!IsAttackingInAir)
            {
                animationStater("IsFalling");
            }
        }

        if(isGrounded)
        {
            IsAttackingInAir = false;

            if(canSlam)
            {
                Instantiate(slamEffect, new Vector3(groundCheck.transform.position.x, groundCheck.transform.position.y - groundDistance + 1f, groundCheck.transform.position.z), slamEffect.transform.rotation);
                canSlam = false;
            }
        }

        //Optimise Movement / Animation Calls
        if(speed > 0.25f)
        {
            if(isGrounded && !isSliding)
            {
                ghostAnimator.SetFloat("MovementSpeed", speed);
                ghostAnimator.SetBool("IsIdling", false);
                ghostAnimator.SetBool("IsMoving", true);
                Sliding(false);
                keepSpeed = false;
            }

            if(speed > 15f)
            {
                ghostAnimator.SetFloat("BlockBlend",15f);
            }
            else
            {
                ghostAnimator.SetFloat("BlockBlend",speed);
            }
        }
        else
        {
            speed = 0;
            ghostAnimator.SetFloat("BlockBlend",5f);
            ghostAnimator.SetBool("IsMoving", false);
        }

        ////Controls

        if(Input.GetKey(KeyCode.UpArrow) || 
           Input.GetKey(KeyCode.DownArrow) || 
           Input.GetKey(KeyCode.LeftArrow) || 
           Input.GetKey(KeyCode.RightArrow))
        {
            if(Input.GetKey(KeyCode.UpArrow))
            {
                move.y = 1;
            }
            else if(move.y > 0)
            {
                move.y = 0;
            }

            if(Input.GetKey(KeyCode.DownArrow))
            {
                move.y = -1;
            }
            else if(move.y < 0)
            {
                move.y = 0;
            }

            if(Input.GetKey(KeyCode.LeftArrow))
            {
                move.x  = -1;
            }
            else if(move.x < 0)
            {
                move.x = 0;
            }

            if(Input.GetKey(KeyCode.RightArrow))
            {
                move.x = 1;
            }
            else if(move.x > 0)
            {
                move.x = 0;
            }
        }
        else
        {
            move.x = 0;
            move.y = 0;
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            Jump(false);
        }

        if(keepSpeed)
        {
            Vector3 force = transform.forward * forceMagnitude;
            controller.Move(force/2 * Time.deltaTime);
            slidingCam.SetActive(true);
        }
        else
        {
            slidingCam.SetActive(false);
        }

        // if (currentDashMeter < dashMeterCap && dashing == false)
        // {
        //     if(currentDashMeter < 0)
        //     {
        //         currentDashMeter = 0;
        //     }
        //     currentDashMeter += 0.05f;
        // }

        // if(dashCdTimer > 0)
        // {
        //     dashCdTimer -= Time.deltaTime;
        // }

        //gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //walk
        Vector2 m = new Vector2(move.x, move.y) * Time.deltaTime;

        float horizontal = m.x;
        float vertical = m.y;
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.25f)
        {
            speed = Mathf.SmoothDamp(speed,direction.magnitude * RunningSpeed, ref yVelocity, smoothTime);

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            // Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * transform.forward;

            controller.Move(transform.forward * speed * Time.deltaTime);
        }
        else
        {
            speed = Mathf.SmoothDamp(speed,direction.magnitude * RunningSpeed, ref negyVelocity, negSmoothTime);
        }

        // if(enemyList.Count <= 0)
        // {
        //     cameraLookAtEnemy = false;
        //     targetObject.localPosition = starterTargetObject.localPosition;
        //     transform.GetChild(0).gameObject.transform.rotation = transform.rotation;
        // }
    }

    void Block()
    {
        ghostAnimator.SetTrigger("IsBlocking");

        if(speed > 0)
        {
            transform.GetChild(0).gameObject.transform.localPosition = new Vector3(transform.GetChild(0).gameObject.transform.localPosition.x,
            -0.3f,
            transform.GetChild(0).gameObject.transform.localPosition.z);
        }
        else
        {
            transform.GetChild(0).gameObject.transform.localPosition = new Vector3(transform.GetChild(0).gameObject.transform.localPosition.x,
            0.6f,
            transform.GetChild(0).gameObject.transform.localPosition.z);
        }
    }

    void BlockIsNull()
    {
        ghostAnimator.SetTrigger("IsBlocking");
        transform.GetChild(0).gameObject.transform.localPosition = new Vector3(transform.GetChild(0).gameObject.transform.localPosition.x,
        -0.3f,
        transform.GetChild(0).gameObject.transform.localPosition.z);
    }

    public void Jump(bool ignoreGround)
    {            
        if(isGrounded || ignoreGround)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            ghostAnimator.SetFloat("JumpBraceBlend", 0.25f);
            animationStater("IsFalling");

            if(isGrounded)
            {
                canDoubleJump = true;
            }
        }
        
        if(!isGrounded && canDoubleJump)
        {
            velocity.y = Mathf.Sqrt(jumpHeight*1.5f * -2 * gravity);
            ghostAnimator.SetFloat("JumpBraceBlend", 2);
            animationStater("IsFalling");
            AudioManager.JumpAudio();
            canDoubleJump = false;    
        }   

        if(!isGrounded)
        {
            canSlam = true; 
        }
    }

    public void Sliding(bool IsSliding)
    {
        if(IsSliding == true)
        {
            ghostAnimator.SetFloat("DashBlend",0.8f);
            ghostAnimator.SetBool("IsDashing",true);
            sliding = IsSliding;
            keepSpeed = true;
            slidingEffect.SetActive(true);
        }
        else if(IsSliding == false)
        {
            ghostAnimator.SetBool("IsDashing",false);
            sliding = false;
            slidingEffect.SetActive(false);
        }
    }

    public void playerDead(bool dead)
    {
        animationStater("IsDeading");
        speed = 0;
    }

    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;
    // private MovementState lastState;
    private bool keepMomentum;

    // private void StateHandler()
    // {
    //     if(dashing)
    //     {
    //         state = MovementState.dashing;
    //         desiredMoveSpeed = dashSpeed;
    //         speedChangeFactor = dashSpeedChangeFactor;
    //     }
    //     else
    //     {
    //         state = MovementState.air;
    //         if(desiredMoveSpeed < speed)
    //         {
    //             desiredMoveSpeed = speed;
    //         }
    //         else
    //         {
    //             desiredMoveSpeed = speed;
    //         }
    //     }

    //     if(keepMomentum)
    //     {
    //         StopAllCoroutines();
    //         StartCoroutine(SmoothlyLerpMoveSpeed());
    //     }
    //     else
    //     {
    //         StopAllCoroutines();
    //         speed = desiredMoveSpeed;
    //     }

    //     lastDesiredMoveSpeed = desiredMoveSpeed;
    //     speed = lastDesiredMoveSpeed;
    //     lastState = state;
    //     dashing = false;
    // }

    private float speedChangeFactor;

    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        //smoothly lerp movementSpeed to desired value
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - speed);
        float startValue = speed;

        float boostFactor = speedChangeFactor;

        while(time < difference)
        {
            speed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);

            time += Time.deltaTime * boostFactor;

            yield return null;
        }

        speed = desiredMoveSpeed;
        speedChangeFactor = 1f;
        keepMomentum = false;
    }

    void animationStater(string animationState)
    {
        for(int i = 0; i < animationStates.Count;i++)
        {
            ghostAnimator.SetBool(animationStates[i], false);
        }

        ghostAnimator.SetBool(animationState, true);
    }

    private IEnumerator WaitForAnimation(float animSeconds)
    {
        yield return new WaitForSeconds(animSeconds);
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheck.position, 1);
    }
}
