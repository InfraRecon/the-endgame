using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController1 : MonoBehaviour
{
    private Transform player; // The player to follow
     public float followDistance = 10f; // The distance at which the AI will follow the player
    public float attackDistance = 1.5f; // The distance to attack the player
    public float tauntTime = 2f; // The time to taunt the player before attacking
    public float moveAroundDistance = 2f; // The distance to move around the player
    public float moveAroundTime = 2f; // The time to move around the player before making another decision
    public float keepMovingAroundProbability = 0.5f; // The probability of keep moving around the player

    private NavMeshAgent agent; // The NavMeshAgent component
    private float timeSinceLastDecision; // The time since the last decision was made

    public static List<string> animationStates = new List<string>(new string[] { "IsIdling", "IsWalking", "IsRunning", "IsTaunting", "IsAttacking" , "IsDead" });

    public Animator enemyAnimator;


    /////////


    public float knockbackForce = 10f; // The force of the knockback
    public float knockbackDuration = 0.5f; // The duration of the knockback
    public float stunDuration = 1f; // The duration of the stun after the knockback

    private bool isKnockback = false; // Flag to check if the player is currently being knocked back
    private bool isStun = false; // Flag to check if the player is currently stunned
    private Vector3 knockbackDirection; // The direction of the knockback
    private float knockbackStartTime; // The time the knockback started
    private float stunStartTime; // The time the stun started
    private CharacterController playerController; // The player's CharacterController component
    private Vector3 playerVelocity; // The player's velocity


    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;


    private bool enemyDead = false;

    private gameCounters counters;

    public LayerMask whatIsEnemy;

    private toggleUI tUI;

    private flickerGameObjectMaterial[] flickerPlayer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component on this game object
        timeSinceLastDecision = moveAroundTime; // Set the time since the last decision to the move around time to start
        player = GameObject.Find("Ghost Player").transform;
        playerController = GameObject.Find("Ghost Player").GetComponent<CharacterController>();
        enemyAnimator = transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Animator>();

        counters = GameObject.Find("Game Manager").GetComponent<gameCounters>();
        tUI = GameObject.Find("Lives").GetComponent<toggleUI>();
    }

    void Update()
    {
        if (player != null) // If there is a player to follow
        {            
            if(enemyDead)
            {
                Die();
            }
            else
            {
                float distanceToPlayer = Vector3.Distance(transform.position, player.position); // Get the distance to the player
                if (distanceToPlayer > followDistance) // If the player is out of range, wait until the player is back in range
                {
                    Searching();
                }
                else if (distanceToPlayer < followDistance && distanceToPlayer > attackDistance) // If the player is too far away to attack
                {
                    Run();
                }
                else if(distanceToPlayer < followDistance && distanceToPlayer < attackDistance)
                {
                    if (timeSinceLastDecision >= moveAroundTime) // If enough time has passed since the last decision was made
                    {
                        timeSinceLastDecision = 0; // Reset the time since the last decision

                        Vector3 randomDirection = Random.insideUnitSphere * moveAroundDistance; // Get a random direction to move in around the player
                        randomDirection += player.position; // Add the player position to the random direction
                        NavMeshHit hit;
                        NavMesh.SamplePosition(randomDirection, out hit, moveAroundDistance, 1); // Sample a point on the NavMesh to move to

                        if (Random.Range(0f, 1f) < keepMovingAroundProbability) // Probability of keep moving around
                        {
                            agent.SetDestination(hit.position); // Set the NavMeshAgent destination to the sampled point
                            Walk();
                        }
                        else if (Random.Range(0f, 1f) < 0.5f) // 50% chance to stop and taunt
                        {
                            Taunt(); // Start the taunt coroutine
                        }
                        else // 50% chance to attack the player
                        {
                            Attack(); // Attack the player
                        }
                    }
                    else
                    {
                        timeSinceLastDecision += Time.deltaTime; // Increase the time since the last decision was made
                        // agent.SetDestination(player.position); // Stop moving
                        // Run();
                    }
                }
                // else // If the player is close enough to attack
                // {
                //     //agent.SetDestination(transform.position); // Stop moving
                //     Attack(); // Attack the player
                // }
            }
        }
    }

    void Taunt()
    {
        // Do the taunt animation or sound effect here
        animationStater("IsTaunting");
        transform.LookAt(player);
        agent.SetDestination(transform.position); // Stop moving
        agent.speed = 0; // Stop moving
    }

    void Attack()
    {
        // Do the attack animation or sound effect here
        // Apply damage to the player
        animationStater("IsAttacking");
        transform.LookAt(player);
        agent.SetDestination(transform.position); // Stop moving
        agent.speed = 0; // Stop moving

        Vector3 knockbackDirection = (player.position - transform.position).normalized; // Get the direction of the knockback
        playerVelocity = knockbackDirection * knockbackForce; // Set the player's velocity to the knockback force

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackDistance,whatIsEnemy);
    
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.gameObject.layer == 3 && enemyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5)
            {
                counters.updateRevives(-1);
                tUI.triggerUI();
                // for(var i = 0; i < flickerPlayer.Length; i++)
                // {
                //     flickerPlayer[i].StartFlickerEffect();
                // }

                isKnockback = true; // Set the knockback flag to true
                knockbackStartTime = Time.time; // Set the knockback start time
            }
        }

        if (isKnockback) // If the player is currently being knocked back
        {
            playerController.Move( Vector3.Lerp(new Vector3(0,0,0),new Vector3(playerVelocity.x, 0.1f, playerVelocity.z),1) * Time.deltaTime); // Move the player with the knockback velocity
        }

        if (isKnockback && Time.time - knockbackStartTime > knockbackDuration) // If the player is currently being knocked back and the knockback duration has passed
        {
            isKnockback = false; // Set the knockback flag to false
            isStun = true; // Set the stun flag to true
            playerVelocity = Vector3.zero; // Set the player's velocity to zero
            stunStartTime = Time.time; // Set the stun start time
        }

        if (isStun && Time.time - stunStartTime > stunDuration) // If the player is currently stunned and the stun duration has passed
        {
            isStun = false; // Set the stun flag to false
        }
////
    }

    void Run()
    {
        // Do the attack animation or sound effect here
        // Apply damage to the player
        animationStater("IsRunning");
        transform.LookAt(player);
        agent.SetDestination(player.position); // Stop moving
        agent.speed = 3; // Stop moving

    }

    void Walk()
    {
        // Do the attack animation or sound effect here
        // Apply damage to the player
        animationStater("IsWalking");
        transform.LookAt(player);
        agent.speed = 1; // Stop moving
    }

    public void enemyIsDead()
    {
        enemyDead = true;
    }

    void Die()
    {
        // Do the attack animation or sound effect here
        // Apply damage to the player
        animationStater("IsDead");
        Destroy(gameObject,5);
    }

    void Searching()
    {
        // Do the attack animation or sound effect here
        // Apply damage to the player
        animationStater("IsIdling");
        agent.SetDestination(transform.position); // Stop moving
        agent.speed = 0; // Stop moving
    }

    void animationStater(string animationState)
    {
        for(int i = 0; i < animationStates.Count;i++)
        {
            enemyAnimator.SetBool(animationStates[i], false);
        }

        enemyAnimator.SetBool(animationState, true);
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, followDistance);

        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, attackDistance);

        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, moveAroundDistance);
    }
}
