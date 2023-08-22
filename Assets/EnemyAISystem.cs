using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAISystem : MonoBehaviour
{
    public float minWaitTime = 1f;
    public float maxWaitTime = 3f;
    // Store the current speed and target speed
    private float currentMoveSpeed;
    private float targetMoveSpeed;

    // The duration over which to interpolate the speed change
    public float speedChangeDuration = 1.0f;

    // Variable to track the elapsed time during speed change
    private float speedChangeTimer = 0.0f;
    private float initialMoveSpeed;
    public float moveSpeed = 1f;
    public GameObject lightSpeedPrefab;
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    //public Transform fireProjectileLocation;
    public float fireRange = 10f; // The range within which the enemy can fire at the player

    private Vector3 targetPosition;
    public bool isMoving = false;
    private GameObject player;
    public float gravity = 9.81f;       // Gravity force applied to the object

    private CharacterController controller;
    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundMask, whatIsPlayer;
    public float groundDistance = 1f;
    Vector3 velocity;

    public float health;
    public GameObject BlobExplosion;

    ////
    //Attacking
    [Header("Attacking")]
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    [Header("States")]
    public float sightRange, tauntAttackRange, attackRange;
    public bool playerInSightRange, playerInTauntAttackRange, playerInAttackRange;

     public static List<string> animationStates = new List<string>(new string[] { "IsIdling", "IsWalking", "IsRunning", "IsTaunting", "IsAttacking" , "IsDead" });

    public Animator enemyAnimator;

    public float launchForce = 10f; // The force with which the object is launched
    public float bounceForce = 5f; // The force with which the object bounces off the launcher
    public float bounceRadius = 1f; // The radius within which the object will bounce off the launcher
    public float moveSmoothness = 5f; // The smoothness of the movement
    public float moveBackTime = 1f; // The amount of time the object will move back

    private bool isMovingBack = false; // Flag indicating if the object is moving back
    private float moveBackTimer = 0f; // The elapsed time since the launch


    private CharacterController playerController; // The player's CharacterController component
    private Vector3 playerVelocity; // The player's velocity

    private Vector3 attackVelocity = Vector3.zero;


    private bool enemyDead = false;

    private gameCounters counters;

    private toggleUI tUI;

    ThirdPersonCameraMovement thirdPersonCameraMovement;

    ///

    public float maxMoveSpeed = 5f; // The maximum speed you want to achieve
    public float accelerationDuration = 1f; // The time it takes to reach the maximum speed

    private float currentSpeed = 0f; // The current speed, initialized to 0

    public float maxTimeout = 10f; // The maximum time allowed to reach the target

    private void Start()
    {
        player = GameObject.Find("Ghost Player");
        controller = GetComponent<CharacterController>();

        playerController = GameObject.Find("Ghost Player").GetComponent<CharacterController>();
        thirdPersonCameraMovement = GameObject.Find("Ghost Player").GetComponent<ThirdPersonCameraMovement>();
        enemyAnimator = transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Animator>();
        counters = GameObject.Find("Game Manager").GetComponent<gameCounters>();
        tUI = GameObject.Find("Lives").GetComponent<toggleUI>();
        
        if(player != null)
        {
            // Start the movement coroutine
            StartCoroutine(MoveObject());
        }
        initialMoveSpeed = moveSpeed;
    }

    private IEnumerator MoveObject()
    {
        while (true)
        {
            // Wait for a random period of time
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            Searching();
            if(player != null)
            {
                if(playerInSightRange && !playerInAttackRange)
                {
                    // Set a new random target position on the x and z axis
                    targetPosition = player.transform.position + new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
                }
                
                if(!playerInSightRange && !playerInAttackRange)
                {
                    targetPosition = transform.position + new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
                }
            }

            // Start moving towards the target position
            isMoving = true;

            // Wait until the object reaches the target position
            // while (Vector3.Distance(transform.position, targetPosition) > 1f)
            // {
            //     // Move towards the target position
            //     transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            //     Walk();
            //     yield return null;
            // }

            float elapsedTime = 0f;
            float dist = Vector3.Distance(transform.position, targetPosition);
            maxTimeout = dist/moveSpeed;

            while (Vector3.Distance(transform.position, targetPosition) > 1f && elapsedTime < maxTimeout)
            {
                // Calculate the direction towards the target position
                Vector3 direction = (targetPosition - transform.position).normalized;

                // Calculate the desired speed based on the maximum speed and the elapsed time
                float desiredSpeed = Mathf.Lerp(0f, maxMoveSpeed, elapsedTime / accelerationDuration);

                // Calculate the velocity based on the direction and the desired speed
                Vector3 velocity = direction * desiredSpeed;

                // Move towards the target position
                controller.Move(new Vector3(velocity.x, 0, velocity.z) * Time.deltaTime);

                // Increase the elapsed time
                elapsedTime += Time.deltaTime;

                yield return null;
            }

            // Stop moving
            isMoving = false;

            // Calculate the distance between the enemy and the player
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            // If the player is within the fire range, fire projectile
            if (distanceToPlayer <= fireRange)
            {
                FireProjectile();
            }
        }
    }

    private void FireProjectile()
    {
        // Calculate the direction towards the player
        Vector3 direction = (player.transform.position - transform.position).normalized;

        // Instantiate the projectile prefab at the current position
        //GameObject projectile = Instantiate(projectilePrefab, fireProjectileLocation.position, Quaternion.identity);

        // Set the velocity of the projectile
        //Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
        //projectileRigidbody.velocity = direction * projectileSpeed;
    }

    private void Update()
    {
        if(player != null)
        {
            if(!enemyDead)
            {
                //Check for sight and attack range
                playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
                playerInTauntAttackRange = Physics.CheckSphere(transform.position, tauntAttackRange, whatIsPlayer);
                playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
                isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
                
                if (!isGrounded)
                {
                    velocity.y -= gravity * Time.deltaTime;
                    controller.Move(velocity * Time.deltaTime);
                }
                
                if(isMoving)
                {

                    // Smoothly rotate towards the look-at position
                    Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position, Vector3.up);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
                    Run();
                }
                else
                {
                    Searching();
                }

                if (!playerInSightRange && !playerInAttackRange) Move();
                if (playerInSightRange && !playerInAttackRange) Move();
                if (playerInAttackRange && playerInSightRange) AttackPlayer();
            }
            
            Debug.DrawLine(transform.position, targetPosition, Color.red);
        }
    }

    private void Move()
    {
        if (isMoving)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            // Check if the distance to the player is greater than or equal to the fire range
            if (distanceToPlayer >= fireRange)
            {
                // Set the target speed to double the move speed
                targetMoveSpeed = initialMoveSpeed * 2f;

                // Instantiate the light speed particle
                //GameObject lightSpeedParticle = Instantiate(lightSpeedPrefab, fireProjectileLocation.position, fireProjectileLocation.rotation);

                // Reset the speed change timer
                speedChangeTimer = 0.0f;
            }
            else
            {
                // Set the target speed to the initial move speed
                targetMoveSpeed = initialMoveSpeed;
            }

            // Gradually interpolate the current speed towards the target speed
            if (currentMoveSpeed != targetMoveSpeed)
            {
                speedChangeTimer += Time.deltaTime;
                float t = Mathf.Clamp01(speedChangeTimer / speedChangeDuration);
                currentMoveSpeed = Mathf.Lerp(currentMoveSpeed, targetMoveSpeed, t);
            }

            // Update the movement speed
            moveSpeed = currentMoveSpeed;
        }
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move

        transform.LookAt(new Vector3(player.transform.position.x,transform.position.y,player.transform.position.z));

        if (!alreadyAttacked)
        {
            ///Attack code here
            // Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            // rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            // rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            Attack();

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange,whatIsPlayer);
        
            foreach (var hitCollider in hitColliders)
            {
                if(hitCollider.gameObject.layer == 3 &&
                enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Enemy Attack"))
                {
                    counters.updateRevives(-1);
                    tUI.triggerUI();

                    // // Get the direction from the launch point to the object
                    // Vector3 direction = player.transform.position - transform.position;

                    // // Calculate the launch force based on the distance from the launch point
                    // float distance = direction.magnitude;
                    // float launchForceAdjusted = launchForce / distance;

                    // // Launch the object
                    // Vector3 attackVelocity = direction.normalized * launchForceAdjusted;
                    // player.transform.Translate(attackVelocity * Time.deltaTime * moveSmoothness, Space.World);

                    // // Make the object bounce off the launcher
                    // if (distance <= bounceRadius)
                    // {
                    //     Vector3 bounceDirection = direction.normalized + Vector3.up;
                    //     Vector3 bounceVelocity = bounceDirection * bounceForce;
                    //     player.transform.Translate(bounceVelocity * Time.deltaTime * moveSmoothness, Space.World);
                    // }

                    // // Start moving the object back
                    // if (!isMovingBack)
                    // {
                    //     isMovingBack = true;
                    // }
                }
            }

            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
////
    void Searching()
    {
        // Do the attack animation or sound effect here
        // Apply damage to the player
        animationStater("IsIdling");
    }

    void Run()
    {
        // Do the attack animation or sound effect here
        // Apply damage to the player
        animationStater("IsRunning");
    }

    void Walk()
    {
        // Do the attack animation or sound effect here
        // Apply damage to the player
        animationStater("IsWalking");
    }

    void Taunt()
    {
        // Do the taunt animation or sound effect here
        animationStater("IsTaunting");
    }

    void Attack()
    {
        // Do the attack animation or sound effect here
        // Apply damage to the player
        animationStater("IsAttacking");
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void enemyIsDead()
    {
        enemyDead = true;
        TakeDamage(1);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) 
        {
            enemyDead = true;
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }


    void Die()
    {
        // Do the attack animation or sound effect here
        // Apply damage to the player
        animationStater("IsDead");
    }

    void animationStater(string animationState)
    {
        for(int i = 0; i < animationStates.Count;i++)
        {
            enemyAnimator.SetBool(animationStates[i], false);
        }

        enemyAnimator.SetBool(animationState, true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
