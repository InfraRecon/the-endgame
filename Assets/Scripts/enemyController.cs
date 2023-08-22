using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour
{
    private Transform player;
    public float followRange = 10f;
    public float attackRange = 5f;
    public static List<string> animationStates = new List<string>(new string[] { "IsIdling", "IsWalking", "IsRunning", "IsAttacking" , "IsDead" });

    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private bool isFollowingPlayer = false;
    private bool isAttackingPlayer = false;
    private bool attackedPlayer = false;
    public Animator enemyAnimator;

    public gameCounters counters;

    public LayerMask whatIsEnemy;

    public toggleUI tUI;

    public flickerGameObjectMaterial[] flickerPlayer;

    private bool enemyDead = false;

    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.Find("Ghost Player").transform;
        counters = GameObject.Find("Game Manager").GetComponent<gameCounters>();
        tUI = GameObject.Find("Lives").GetComponent<toggleUI>();
    }

    void Update()
    {
        if(!enemyDead)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer < followRange && distanceToPlayer > attackRange)
            {
                isFollowingPlayer = true;
            }
            else if(distanceToPlayer < attackRange)
            {
                isFollowingPlayer = false;
                isAttackingPlayer = true;
            }
            else
            {
                isFollowingPlayer = false;
                navMeshAgent.speed = 0;
                animationStater("IsIdling");
                // enemyAnimator.speed = 1;
            }

            if (isFollowingPlayer && !enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Enemy Attack"))
            {
                navMeshAgent.SetDestination(player.position);
                navMeshAgent.speed = 6;
                animationStater("IsRunning");
                // enemyAnimator.speed = navMeshAgent.speed;
            }
            
            else if (isAttackingPlayer)
            {
                navMeshAgent.speed = 0;
                animationStater("IsAttacking");

                if(enemyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5)
                {
                    attackedPlayer = false;
                }

                Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange,whatIsEnemy);
            
                foreach (var hitCollider in hitColliders)
                {
                    if(hitCollider.gameObject.layer == 3 && enemyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5 && !attackedPlayer)
                    {
                        counters.updateRevives(-1);
                        tUI.triggerUI();
                        for(var i = 0; i < flickerPlayer.Length; i++)
                        {
                            flickerPlayer[i].StartFlickerEffect();
                        }
                        attackedPlayer = true;
                    }
                }
                // enemyAnimator.speed = navMeshAgent.speed;
            }
        }
        else
        {
            animationStater("IsDead");
        }
    }

    public void enemyIsDead()
    {
        enemyDead = true;
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
        Gizmos.DrawSphere(transform.position, followRange);

        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, attackRange);
    }
}
