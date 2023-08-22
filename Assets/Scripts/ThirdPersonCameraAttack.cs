using System.Collections;
using System.Collections.Generic;
// using UnityEngine.InputSystem;
using UnityEngine;

public class ThirdPersonCameraAttack : MonoBehaviour
{
    public Animator ghostAnimator;
    // PlayerControls controls;

    ThirdPersonCameraMovement thirdPersonCameraMovement;

    public static List<string> animationStates = new List<string>(new string[] { "IsIdling", "IsMoving", "IsFalling","IsDashing", "IsAttacking", "IsBlocking" });
    public GameObject swordEffect;
    public Transform swordEffectLocation;
    public rotatePlayerObject rotater;
    public float waitTime = 0.1f; // Number of seconds to wait

    public GameObject attackExplosion;
    
    // void Awake()
    // {
    //     controls = new PlayerControls();
    // }

    // Start is called before the first frame update
    void Start()
    {
        thirdPersonCameraMovement = GetComponent<ThirdPersonCameraMovement>();
        // rotater = GetComponent<Rotater>();
    }

    // void OnEnable()
    // {
    //     controls.Player.Enable();
    // }

    // void OnDisable()
    // {
    //     controls.Player.Disable();
    // }

    // Update is called once per frame
    void Update()
    {
        // if (ghostAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && 
        // ghostAnimator.GetCurrentAnimatorStateInfo(0).IsName("Ghost Attack"))
        // {
        //     ghostAnimator.SetBool("IsAttacking", false);
        //     //noOfClicks = 0;
        // }
        if(Input.GetKeyDown(KeyCode.C) && !ghostAnimator.GetCurrentAnimatorStateInfo(0).IsName("Ghost Attack"))
        {
            SpinAttack();
            if(!thirdPersonCameraMovement.isGrounded)
            {
                StartCoroutine(WaitAndPrint());
            }
            GameObject attackPart = Instantiate(attackExplosion, transform.position,attackExplosion.transform.rotation);
            attackPart.transform.parent = transform;
        }
        // else
        // {
        //     AttackIsNull();
        // }

        //so it looks at how many clicks have been made and if one animation has finished playing starts another one.

        //cooldown time
        // controls.Player.LightAttack.performed += ctx => LightAttack();
        // controls.Player.LightAttack.canceled += ctx => AttackIsNull();
    }

    IEnumerator WaitAndPrint()
    {
        Debug.Log("Coroutine started");
        Vector3 attackPosition = transform.position;
        thirdPersonCameraMovement.enabled = false;
        transform.position = attackPosition;
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Coroutine finished after " + waitTime + " seconds");
        thirdPersonCameraMovement.enabled = true;
    }

    void SpinAttack()
    {
        if(thirdPersonCameraMovement.isGrounded)
        {
            ghostAnimator.SetFloat("AttackBlend", 0);
            //rotater.RotateAndReset();
        }
        else
        {
            ghostAnimator.SetFloat("AttackBlend", 1);
            thirdPersonCameraMovement.speed = 0;
        }

        ghostAnimator.SetBool("IsAttacking", true);
        rotater.RotateAndReset();
    }

    
    void AttackIsNull()
    {
        //so it looks at how many clicks have been made and if one animation has finished playing starts another one.
        animationStater("IsIdling");
    }

    void animationStater(string animationState)
    {
        for(int i = 0; i < animationStates.Count;i++)
        {
            ghostAnimator.SetBool(animationStates[i], false);
        }

        ghostAnimator.SetBool(animationState, true);
    }
}
