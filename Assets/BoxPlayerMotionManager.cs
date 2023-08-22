using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPlayerMotionManager : MonoBehaviour
{
    public KeyCode keyToPress;
    public Transform[] childObjects;

    private GameObject player;

    ThirdPersonCameraMovement thirdPersonCameraMovement;
    ThirdPersonCameraAttack thirdPersonCameraAttack;

    Animator ghostAnimator;
    public static List<string> animationStates = new List<string>(new string[] { "IsIdling", "IsMoving", "IsFalling", "IsDashing", "IsAttacking", "IsPushing" });

    public float moveSpeed = 5f;
    public float forceMultiplier = 10f;

    private Rigidbody rb;
    private Transform targetChild;
    private toggleUI tUI;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Ghost Player");
        thirdPersonCameraMovement = player.GetComponent<ThirdPersonCameraMovement>();
        thirdPersonCameraAttack = player.GetComponent<ThirdPersonCameraAttack>();
        ghostAnimator = player.transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>();
        tUI = GameObject.Find("Button Prompts").GetComponent<toggleUI>();
    }

    private void Update()
    {
        Transform closestChild = GetClosestChild();
        if (closestChild != null)
        {
            tUI.triggerUI();
            if (Input.GetKey(keyToPress))
            {
                if (player != null && childObjects.Length > 0)
                {
                    thirdPersonCameraMovement.speed = 0;
                    thirdPersonCameraMovement.enabled = false;
                    player.transform.LookAt(transform);
                    targetChild = closestChild;
                    animationStater("IsPushing");
                }

                float moveHorizontal = Input.GetAxis("Horizontal");
                float moveVertical = Input.GetAxis("Vertical");

                Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
                Vector3 force = movement * moveSpeed * forceMultiplier;

                rb.AddForce(force);
            }
            else
            {
                thirdPersonCameraMovement.enabled = true;
                targetChild = null;
                animationStater("IsIdling");
            }
        }

        if (targetChild != null)
        {
            MoveToChild(targetChild);
        }
    }

    private Transform GetClosestChild()
    {
        Transform closestChild = null;
        float closestDistance = 5f;

        foreach (Transform child in childObjects)
        {
            float distance = Vector3.Distance(player.transform.position, child.position);
            if (distance < closestDistance)
            {
                thirdPersonCameraAttack.enabled = false;
                closestChild = child;
                closestDistance = distance;
            }
            else
            {
                thirdPersonCameraAttack.enabled = true;
            }
        }

        return closestChild;
    }

    private void MoveToChild(Transform child)
    {
        Vector3 targetPosition = child.position;
        Vector3 newPosition = Vector3.Lerp(player.transform.position, targetPosition, Time.deltaTime * moveSpeed/2);
        player.transform.position = newPosition;
    }

    void animationStater(string animationState)
    {
        for (int i = 0; i < animationStates.Count; i++)
        {
            ghostAnimator.SetBool(animationStates[i], false);
        }

        ghostAnimator.SetBool(animationState, true);
    }
}
