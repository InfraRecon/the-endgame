
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAiTutorial : MonoBehaviour
{
    public NavMeshAgent agent;

    private Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    //Patroling
    [Header("Patrolling")]
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Taunting
    [Header("Taunting")]
    public float tauntTime = 2f; // desired taunt time in seconds
    public float attackChance = 0.5f; // chance of attacking after taunt

    private bool isTaunting = false; // flag to keep track of whether we're currently taunting

    //Attacking
    [Header("Attacking")]
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    // public GameObject projectile;

    //States
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

    private Vector3 velocity = Vector3.zero;


    private bool enemyDead = false;

    private gameCounters counters;

    private toggleUI tUI;

    private flickerGameObjectMaterial[] flickerPlayer;

    ThirdPersonCameraMovement thirdPersonCameraMovement;

    private void Awake()
    {
        player = GameObject.Find("Ghost Player").transform;
        agent = GetComponent<NavMeshAgent>();

        playerController = GameObject.Find("Ghost Player").GetComponent<CharacterController>();
        thirdPersonCameraMovement = GameObject.Find("Ghost Player").GetComponent<ThirdPersonCameraMovement>();
        enemyAnimator = transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Animator>();

        counters = GameObject.Find("Game Manager").GetComponent<gameCounters>();
        tUI = GameObject.Find("Lives").GetComponent<toggleUI>();
    }

    private void Update()
    {
        if(!enemyDead)
        {
            //Check for sight and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInTauntAttackRange = Physics.CheckSphere(transform.position, tauntAttackRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && playerInTauntAttackRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInTauntAttackRange && !playerInAttackRange) ChasePlayer();
            if (playerInTauntAttackRange && playerInSightRange && !playerInAttackRange) TauntAttackPlayer();
            if (playerInAttackRange && playerInTauntAttackRange && playerInSightRange) AttackPlayer();

            // Move the object back for a desired period of time and then stop
            if (isMovingBack)
            {
                moveBackTimer += Time.deltaTime;
                if (moveBackTimer < moveBackTime)
                {
                    player.Translate(transform.forward * Time.deltaTime * moveSmoothness, Space.World);
                }
                else
                {
                    isMovingBack = false;
                    moveBackTimer = 0f;
                }
            }
        }
        else
        {
            Die();
        }
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);
            Walk();

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        Run();
    }

    private void TauntAttackPlayer()
    {
        // //Make sure enemy doesn't move
        // agent.SetDestination(transform.position);

        // transform.LookAt(player);

        // if (!alreadyAttacked)
        // {
        //     ///Attack code here
        //     // Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        //     // rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
        //     // rb.AddForce(transform.up * 8f, ForceMode.Impulse);


        //     ///End of attack code

        //     alreadyAttacked = true;
        //     Invoke(nameof(ResetAttack), timeBetweenAttacks);
        // }

        // if (Random.value < attackChance)
        // {
        //     // if we decide to attack, set a trigger parameter to transition to the attack animation
        //     agent.SetDestination(player.position);
        // }
        // else
        // {
            // if we decide to keep taunting, set a trigger parameter to loop the taunt animation
            agent.SetDestination(transform.position);
            transform.LookAt(new Vector3(player.position.x,transform.position.y,player.position.z));
            Taunt();
            isTaunting = false; // reset the flag to indicate that we're no longer taunting
        // }
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(new Vector3(player.position.x,transform.position.y,player.position.z));

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
                    // for(var i = 0; i < flickerPlayer.Length; i++)
                    // {
                    //     flickerPlayer[i].StartFlickerEffect();
                    // }

                    // Get the direction from the launch point to the object
                    Vector3 direction = player.position - transform.position;

                    // Calculate the launch force based on the distance from the launch point
                    float distance = direction.magnitude;
                    float launchForceAdjusted = launchForce / distance;

                    // Launch the object
                    Vector3 velocity = direction.normalized * launchForceAdjusted;
                    player.Translate(velocity * Time.deltaTime * moveSmoothness, Space.World);

                    // Make the object bounce off the launcher
                    if (distance <= bounceRadius)
                    {
                        Vector3 bounceDirection = direction.normalized + Vector3.up;
                        Vector3 bounceVelocity = bounceDirection * bounceForce;
                        player.Translate(bounceVelocity * Time.deltaTime * moveSmoothness, Space.World);
                    }

                    // Start moving the object back
                    if (!isMovingBack)
                    {
                        isMovingBack = true;
                        thirdPersonCameraMovement.Jump(true);
                    }

                    
                }
            }

            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
////

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
    public void enemyIsDead()
    {
        enemyDead = true;
        TakeDamage(1);
    }

    void Die()
    {
        // Do the attack animation or sound effect here
        // Apply damage to the player
        animationStater("IsDead");
    }

    void Searching()
    {
        // Do the attack animation or sound effect here
        // Apply damage to the player
        animationStater("IsIdling");
    }

    void animationStater(string animationState)
    {
        for(int i = 0; i < animationStates.Count;i++)
        {
            enemyAnimator.SetBool(animationStates[i], false);
        }

        enemyAnimator.SetBool(animationState, true);
    }

    ////
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) 
            enemyDead = true;
            Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject,5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, tauntAttackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
