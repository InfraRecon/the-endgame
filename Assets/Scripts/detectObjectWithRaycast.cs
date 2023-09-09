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
    public GameObject explosionskullBoxOnDetectionImeddiate;
    public GameObject explosionskull;
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
        foreach (var newHitCollider in hitColliders)
        {
           //&& ghostAnimator.GetCurrentAnimatorStateInfo(0).IsName("Ghost Attack") && ghostAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.9f ;
            GameObject HitCollider = newHitCollider.GetComponent<Collider>().gameObject;

            if(Input.GetKeyDown(KeyCode.C) && HitCollider.layer == 8 ||  
               Input.GetKeyDown(KeyCode.JoystickButton0) && HitCollider.layer == 8 || 
               triggerAttack && HitCollider.layer == 8)
            {
                Instantiate(soul, HitCollider.transform.position,Quaternion.identity);
                //Instantiate(explosionBoxOnDetection, hitCollider.gameObject.transform.position,explosionBoxOnDetection.transform.rotation);
                counters.updateBoxesDestroyed(+1);
                tUI.triggerUI();
                boxAudioSource.pitch = UnityEngine.Random.Range(0.5f,1.1f);
                boxAudioSource.Play();
                Destroy(HitCollider);
            }
            
            else if(Input.GetKeyDown(KeyCode.C) && HitCollider.layer == 20 ||  
               Input.GetKeyDown(KeyCode.JoystickButton0) && HitCollider.layer == 20 || 
               triggerAttack && HitCollider.layer == 20)
            {
                Instantiate(explosionskullBoxOnDetectionImeddiate, HitCollider.transform.position,explosionBoxOnDetection.transform.rotation);
                counters.updateBoxesDestroyed(+1);
                tUI.triggerUI();
                boxAudioSource.pitch = UnityEngine.Random.Range(0.5f,1.1f);
                boxAudioSource.Play();
                Destroy(HitCollider);
            }

            else if(Input.GetKeyDown(KeyCode.C) && HitCollider.layer == 14 || 
               Input.GetKeyDown(KeyCode.JoystickButton0) && HitCollider.layer == 14 || 
               triggerAttack && HitCollider.layer == 14)
            {
                transform.parent.LookAt(HitCollider.transform);
                Instantiate(soul, HitCollider.transform.position,Quaternion.identity);
                Instantiate(explosionEnemyOnDetection, HitCollider.transform.position,explosionBoxOnDetection.transform.rotation);
                EnemyAiTutorial enemy = HitCollider.GetComponent<EnemyAiTutorial>();
                try
                {
                    enemy.enemyIsDead();
                    HitCollider.layer = 6;
                }
                catch(Exception e)
                {
                    //Debug.Log("Enemy was still Detected");
                }
                Destroy(HitCollider);
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
        foreach (var newJumpHitCollider in jumpHitColliders)
        {
            GameObject jumpHitCollider = newJumpHitCollider.GetComponent<Collider>().gameObject;
            if(jumpHitCollider.layer == 8)
            {
                thirdPersonCameraMovement.Jump(true,1f);
                // Instantiate(explosionBoxOnDetection, jumpHitCollider.transform.position,explosionBoxOnDetection.transform.rotation);
                Instantiate(soul, jumpHitCollider.transform.position,Quaternion.identity);
                counters.updateBoxesDestroyed(+1);
                tUI.triggerUI();
                boxAudioSource.pitch = UnityEngine.Random.Range(0.5f,1.1f);
                boxAudioSource.Play();
                Destroy(jumpHitCollider);
            }

            else if(jumpHitCollider.layer == 20)
            {
                jumpHitCollider.layer = 6;
                thirdPersonCameraMovement.Jump(true,1f);
                // Add a delay before instantiating the explosion (e.g., 2 seconds)
                Instantiate(explosionskullBoxOnDetection, jumpHitCollider.transform.position,Quaternion.identity);
                Instantiate(explosionskull,jumpHitCollider.transform.position,Quaternion.identity);
                counters.updateBoxesDestroyed(+1);
                tUI.triggerUI();
                boxAudioSource.pitch = UnityEngine.Random.Range(0.5f, 1.1f);
                boxAudioSource.Play();
                Destroy(jumpHitCollider);
                // float delay = 5.0f;
                // StartCoroutine(DelayedExplosion(delay, jumpHitCollider, explosionBoxOnDetection));
            }

            else if(jumpHitCollider.layer == 26)
            {
                thirdPersonCameraMovement.Jump(true,3f);
            }

            else if(jumpHitCollider.layer == 12)
            {
                counters.updateSoulEssence(+1);
            }

            else if(jumpHitCollider.layer == 14)
            {
                Instantiate(soul, jumpHitCollider.transform.position,soul.transform.rotation);
                //Instantiate(explosionBoxOnDetection, jumpHitCollider.transform.position,explosionBoxOnDetection.transform.rotation);
                EnemyAiTutorial enemy = jumpHitCollider.GetComponent<EnemyAiTutorial>();
                thirdPersonCameraMovement.Jump(true,1f);
                //Instantiate(soul, jumpHitCollider.transform.position,soul.transform.rotation);
                //Instantiate(explosionEnemyOnDetection, jumpHitCollider.transform.position,explosionBoxOnDetection.transform.rotation);
                try
                {
                    enemy.enemyIsDead();
                    jumpHitCollider.layer = 6;
                }
                catch(Exception e)
                {
                    //Debug.Log("Enemy was still Detected");
                }
                Destroy(jumpHitCollider);
            }

            // else if(hit.collider.gameObject.layer == 10)
            // {
            //     transform.parent.position = hit.collider.gameObject.transform.GetChild(0).transform.position;
            // }
        }
    }

    public void triggerAttackRange(bool isAttacking)
    {

        attackRange = 5f;
        triggerAttack = isAttacking;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
