// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System;

// public class detectObjectWithRaycast : MonoBehaviour
// {
//     public GameObject reticle;
//     public float maxRayDistance = 100f; // The maximum distance the raycast can travel.
//     public LayerMask groundLayer; // The layer(s) that represent the ground.

//     private float raycastDistance = 0f; // The distance the raycast traveled before hitting the ground.

//     public GameObject explosionBoxOnDetection;
//     public GameObject explosionSkullBoxOnDetection;
//     public AudioSource boxAudioSource;
//     public GameObject soul;
//     public gameCounters counters;

//     public ThirdPersonCameraMovement thirdPersonCameraMovement;

//     //States
//     public float attackRange;
//     public bool objectInAttackRange;
//     public LayerMask whatIsEnemy;

//     public toggleUI tUI;
//     // public GameObject AttackBubble;

//     public Animator ghostAnimator;

//     private Collider[] hitColliders;

//     private void Start()
//     {
//         reticle = Instantiate(reticle);
//     }

//     private void Update()
//     {
//         //Attacking Stuff
//         bool isAttackInput = Input.GetKeyDown(KeyCode.C);
//         bool isGhostAttack = ghostAnimator.GetCurrentAnimatorStateInfo(0).IsName("Ghost Attack") &&
//             ghostAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.9f;

//         if (isAttackInput || isGhostAttack)
//         {
//             hitColliders = Physics.OverlapSphere(transform.position, attackRange, whatIsEnemy);
//             foreach (var hitCollider in hitColliders)
//             {
//                 if (hitCollider.gameObject.layer == 8)
//                 {
//                     InstantiateObjectsOnAttack(hitCollider.gameObject);
//                     Destroy(hitCollider.gameObject);
//                 }
//                 else if (hitCollider.gameObject.layer == 20)
//                 {
//                     thirdPersonCameraMovement.Jump(true);
//                     InstantiateObjectsOnAttack(hitCollider.gameObject);
//                     Destroy(hitCollider.gameObject);
//                 }
//                 else if (hitCollider.gameObject.layer == 14)
//                 {
//                     transform.parent.LookAt(hitCollider.gameObject.transform);
//                     InstantiateObjectsOnAttack(hitCollider.gameObject);
//                     EnemyAiTutorial enemy = hitCollider.gameObject.GetComponent<EnemyAiTutorial>();
//                     try
//                     {
//                         enemy.enemyIsDead();
//                     }
//                     catch (Exception e)
//                     {
//                         //Debug.Log("Enemy was still Detected");
//                     }
//                 }
//             }
//         }

//         RaycastHit hit;
//         //Reticle on Stuff
//         if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.5f, groundLayer))
//         {
//             raycastDistance = hit.distance;
//             reticle.transform.position = new Vector3(transform.position.x, transform.position.y + raycastDistance + 0.1f, transform.position.z);
//             //Debug.Log("Raycast hit ground at distance: " + raycastDistance);
//         }
//         else
//         {
//             reticle.transform.position = new Vector3(transform.position.x, reticle.transform.position.y, transform.position.z);
//             raycastDistance = maxRayDistance;
//             //Debug.Log("Raycast did not hit ground.");
//         }

//         //Jumping on Stuff
//         Collider[] jumpHitColliders = Physics.OverlapSphere(transform.position, maxRayDistance, groundLayer);
//         foreach (var jumpHitCollider in jumpHitColliders)
//         {
//             if (jumpHitCollider.gameObject.layer == 8)
//             {
//                 thirdPersonCameraMovement.Jump(true);
//                 InstantiateObjectsOnJump(jumpHitCollider.gameObject);
//                 Destroy(jumpHitCollider.gameObject);
//             }
//             else if (jumpHitCollider.gameObject.layer == 20)
//             {
//                 thirdPersonCameraMovement.Jump(true);
//                 InstantiateObjectsOnJump(jumpHitCollider.gameObject);
//                 Destroy(jumpHitCollider.gameObject);
//             }
//             else if (jumpHitCollider.gameObject.layer == 12)
//             {
//                 counters.updateSoulEssence(+1);
//             }
//             else if (jumpHitCollider.gameObject.layer == 14)
//             {
//                 Instantiate(soul, jumpHitCollider.gameObject.transform.position, soul.transform.rotation);
//                 Instantiate(explosionBoxOnDetection, jumpHitCollider.gameObject.transform.position, explosionBoxOnDetection.transform.rotation);
//                 EnemyAiTutorial enemy = jumpHitCollider.gameObject.GetComponent<EnemyAiTutorial>();
//                 enemy.enemyIsDead();
//             }
//         }
//     }

//     private void InstantiateObjectsOnAttack(GameObject hitObject)
//     {
//         Instantiate(soul, hitObject.transform.position, soul.transform.rotation);
//         Instantiate(explosionBoxOnDetection, hitObject.transform.position, explosionBoxOnDetection.transform.rotation);
//         counters.updateBoxesDestroyed(+1);
//         tUI.triggerUI();
//         boxAudioSource.pitch = UnityEngine.Random.Range(0.5f, 1.1f);
//         boxAudioSource.Play();
//     }

//     private void InstantiateObjectsOnJump(GameObject hitObject)
//     {
//         Instantiate(explosionBoxOnDetection, hitObject.transform.position, explosionBoxOnDetection.transform.rotation);
//         Instantiate(soul, hitObject.transform.position, soul.transform.rotation);
//         counters.updateBoxesDestroyed(+1);
//         tUI.triggerUI();
//         boxAudioSource.pitch = UnityEngine.Random.Range(0.5f, 1.1f);
//         boxAudioSource.Play();
//     }

//     private void OnDrawGizmosSelected()
//     {
//         Gizmos.color = Color.red;
//         Gizmos.DrawWireSphere(transform.position, attackRange);
//     }
// }
/////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class detectObjectWithRaycast : MonoBehaviour
{
    // public GameObject reticle;
    public float maxRayDistance = 100f; // The maximum distance the raycast can travel.
    public LayerMask groundLayer; // The layer(s) that represent the ground.

    private float raycastDistance = 0f; // The distance the raycast traveled before hitting the ground.

    public GameObject explosionBoxOnDetection;
    public GameObject explosionskullBoxOnDetection;
    public GameObject explosionEnemyOnDetection;
    public AudioSource boxAudioSource;
    public GameObject soul;
    public gameCounters counters;

    public ThirdPersonCameraMovement thirdPersonCameraMovement;

    //States
    public float attackRange;
    public bool objectInAttackRange;
    public LayerMask whatIsEnemy;

    public toggleUI tUI;
    // public GameObject AttackBubble;

    public Animator ghostAnimator;

    public bool triggerAttack = false;

    private void Start()
    {
        // reticle = Instantiate(reticle);
    }

    private void Update()
    {
        //Attacking Stuff
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange,whatIsEnemy);
        foreach (var hitCollider in hitColliders)
        {
            if(Input.GetKeyDown(KeyCode.C) && hitCollider.gameObject.layer == 8 || ghostAnimator.GetCurrentAnimatorStateInfo(0).IsName("Ghost Attack") && ghostAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.9f || triggerAttack)
            {
                Instantiate(soul, hitCollider.GetComponent<Collider>().gameObject.transform.position,soul.transform.rotation);
                Instantiate(explosionBoxOnDetection, hitCollider.gameObject.transform.position,explosionBoxOnDetection.transform.rotation);
                counters.updateBoxesDestroyed(+1);
                tUI.triggerUI();
                boxAudioSource.pitch = UnityEngine.Random.Range(0.5f,1.1f);
                boxAudioSource.Play();
                Destroy(hitCollider.gameObject);
            }

            if(Input.GetKeyDown(KeyCode.C) && hitCollider.gameObject.layer == 20 || ghostAnimator.GetCurrentAnimatorStateInfo(0).IsName("Ghost Attack") && ghostAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.9f || triggerAttack)
            {
                //Invoke("explosionType", 5); 
                Instantiate(explosionskullBoxOnDetection, hitCollider.GetComponent<Collider>().gameObject.transform.position,explosionBoxOnDetection.transform.rotation);
                //Instantiate(soul, hitCollider.GetComponent<Collider>().gameObject.transform.position,soul.transform.rotation);
                counters.updateBoxesDestroyed(+1);
                tUI.triggerUI();
                boxAudioSource.pitch = UnityEngine.Random.Range(0.5f,1.1f);
                boxAudioSource.Play();
                Destroy(hitCollider.GetComponent<Collider>().gameObject);
            }

            if(Input.GetKeyDown(KeyCode.C) && hitCollider.gameObject.layer == 14 || ghostAnimator.GetCurrentAnimatorStateInfo(0).IsName("Ghost Attack") && ghostAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.9f || triggerAttack)
            {
                transform.parent.LookAt(hitCollider.gameObject.transform);
                Instantiate(soul, hitCollider.GetComponent<Collider>().gameObject.transform.position,soul.transform.rotation);
                Instantiate(explosionEnemyOnDetection, hitCollider.gameObject.transform.position,explosionBoxOnDetection.transform.rotation);
                EnemyAiTutorial enemy = hitCollider.gameObject.GetComponent<EnemyAiTutorial>();
                try
                {
                    enemy.enemyIsDead();
                    hitCollider.gameObject.layer = 6;
                }
                catch(Exception e)
                {
                    //Debug.Log("Enemy was still Detected");
                }
            }
        }

        RaycastHit hit;
        //Reticle on Stuff
        // if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.5f, groundLayer))
        // {
        //     raycastDistance = hit.distance;
        //     reticle.transform.position = new Vector3(transform.position.x, transform.position.y + raycastDistance + 0.1f, transform.position.z); 
        //     Debug.Log("Raycast hit ground at distance: " + raycastDistance);
        // }
        // else
        // {
        //     reticle.transform.position = new Vector3(transform.position.x, reticle.transform.position.y, transform.position.z); 
        //     raycastDistance = maxRayDistance;
        //     Debug.Log("Raycast did not hit ground.");
        // }

        //Jumping on Stuff
        Collider[] jumpHitColliders = Physics.OverlapSphere(transform.position, maxRayDistance,groundLayer);
        foreach (var jumpHitCollider in jumpHitColliders)
        {
            if(jumpHitCollider.GetComponent<Collider>().gameObject.layer == 8)
            {
                thirdPersonCameraMovement.Jump(true,1f);
                Instantiate(explosionBoxOnDetection, jumpHitCollider.GetComponent<Collider>().gameObject.transform.position,explosionBoxOnDetection.transform.rotation);
                Instantiate(soul, jumpHitCollider.GetComponent<Collider>().gameObject.transform.position,soul.transform.rotation);
                counters.updateBoxesDestroyed(+1);
                tUI.triggerUI();
                boxAudioSource.pitch = UnityEngine.Random.Range(0.5f,1.1f);
                boxAudioSource.Play();
                Destroy(jumpHitCollider.GetComponent<Collider>().gameObject);
            }

            if(jumpHitCollider.GetComponent<Collider>().gameObject.layer == 20)
            {
                thirdPersonCameraMovement.Jump(true,1f);
                //Invoke("explosionType", 5); 
                Instantiate(explosionskullBoxOnDetection, jumpHitCollider.GetComponent<Collider>().gameObject.transform.position,explosionBoxOnDetection.transform.rotation);
                //Instantiate(soul, jumpHitCollider.GetComponent<Collider>().gameObject.transform.position,soul.transform.rotation);
                counters.updateBoxesDestroyed(+1);
                tUI.triggerUI();
                boxAudioSource.pitch = UnityEngine.Random.Range(0.5f,1.1f);
                boxAudioSource.Play();
                Destroy(jumpHitCollider.GetComponent<Collider>().gameObject);
            }

            if(jumpHitCollider.GetComponent<Collider>().gameObject.layer == 26)
            {
                thirdPersonCameraMovement.Jump(true,3f);
            }

            if(jumpHitCollider.GetComponent<Collider>().gameObject.layer == 12)
            {
                counters.updateSoulEssence(+1);
            }

            if(jumpHitCollider.GetComponent<Collider>().gameObject.layer == 14)
            {
                Instantiate(soul, jumpHitCollider.GetComponent<Collider>().gameObject.transform.position,soul.transform.rotation);
                Instantiate(explosionBoxOnDetection, jumpHitCollider.gameObject.transform.position,explosionBoxOnDetection.transform.rotation);
                EnemyAiTutorial enemy = jumpHitCollider.gameObject.GetComponent<EnemyAiTutorial>();
                thirdPersonCameraMovement.Jump(true,1f);
                Instantiate(soul, jumpHitCollider.GetComponent<Collider>().gameObject.transform.position,soul.transform.rotation);
                Instantiate(explosionEnemyOnDetection, jumpHitCollider.gameObject.transform.position,explosionBoxOnDetection.transform.rotation);
                try
                {
                    enemy.enemyIsDead();
                    jumpHitCollider.gameObject.layer = 6;
                }
                catch(Exception e)
                {
                    //Debug.Log("Enemy was still Detected");
                }
                Destroy(jumpHitCollider.GetComponent<Collider>().gameObject);
            }

            // else if(hit.collider.gameObject.layer == 10)
            // {
            //     transform.parent.position = hit.collider.gameObject.transform.GetChild(0).transform.position;
            // }
        }
    }

    public void triggerAttackRange(bool isAttacking)
    {
        if(isAttacking)
        {
            attackRange = 7.5f;
        }
        else
        {
            attackRange = 5f;
        }
        triggerAttack = isAttacking;
    }
    // void explosionType()
    // {
    //      Instantiate(explosionskullBoxOnDetection, jumpHitCollider.GetComponent<Collider>().gameObject.transform.position,explosionBoxOnDetection.transform.rotation);
    // }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
