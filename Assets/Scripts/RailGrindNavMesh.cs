using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailGrindNavMesh : MonoBehaviour
{
    private GameObject playerObject; // The game object we want to move on the mesh
    private CharacterController playerController;
    private ThirdPersonCameraMovement thirdPersonCameraMovement;
    private PlayerNavMeshHandler playerNavMeshHandler;
    public float movementSpeed = 5f; // The speed at which the player object moves on the mesh

    public GameObject startTarget; // The object to follow
    public GameObject endTarget; // The object to follow

    public bool moveToStart = false;
    public bool moveToEnd = true;


    ////


    public float stopDistance = 0.1f;

    private bool isMoving = false;


    private UnityEngine.AI.NavMeshAgent agent; // Reference to the NavMeshAgent component

    private Collider meshCollider; // The collider for the mesh we're moving on

    private void Start()
    {
        meshCollider = GetComponent<Collider>();
        playerObject = GameObject.Find("PlayerNavMeshAgent");
        playerController = GameObject.Find("Ghost Player").GetComponent<CharacterController>();
        thirdPersonCameraMovement = GameObject.Find("Ghost Player").GetComponent<ThirdPersonCameraMovement>();
        //agent = playerObject.GetComponent<UnityEngine.AI.NavMeshAgent>(); // Get the NavMeshAgent component
        playerNavMeshHandler = playerObject.GetComponent<PlayerNavMeshHandler>();
    }

    private void Update()
    {
        // Check if the player object is on the mesh
        if (IsOnMesh(playerObject.transform.position))
        {
            moveInDirection();
            thirdPersonCameraMovement.Sliding(true);
            if(Input.GetKey(KeyCode.DownArrow))
            {
                switchDirection(true, false);
            }
            else if(Input.GetKey(KeyCode.UpArrow))
            {
                switchDirection(false, true);
            }

            playerNavMeshHandler.EnableAgent();
            playerNavMeshHandler.PlayerRailGrinding();
            agent = playerObject.GetComponent<UnityEngine.AI.NavMeshAgent>(); // Get the NavMeshAgent component
            agent.speed = movementSpeed;
        }

        if(moveToStart || moveToEnd)
        {
            Transform TargetFollow = null;
            if(moveToStart)
            {
                if(IsOnMesh(playerObject.transform.position))
                {
                    agent.SetDestination(new Vector3(startTarget.transform.position.x,playerObject.transform.position.y,startTarget.transform.position.z)); // Set the target position for the NavMeshAgent to follow
                }
                float distToStart = Vector3.Distance(playerObject.transform.position, startTarget.transform.position);
                if(distToStart <= 5f)
                {
                    playerNavMeshHandler.DisableAgent();
                    playerNavMeshHandler.PlayerInControl();
                    thirdPersonCameraMovement.Sliding(false);
    
                    isMoving = true;
                    TargetFollow = startTarget.transform.GetChild(0).transform;
                    
                    //switchDirection(false, true);
                }
            }
            else if(moveToEnd)
            {
                if(IsOnMesh(playerObject.transform.position))
                {
                    agent.SetDestination(new Vector3(endTarget.transform.position.x,playerObject.transform.position.y,endTarget.transform.position.z)); // Set the target position for the NavMeshAgent to follow
                }
                float distToEnd = Vector3.Distance(playerObject.transform.position, endTarget.transform.position);
                if(distToEnd <= 5f)
                {
                    playerNavMeshHandler.DisableAgent();
                    playerNavMeshHandler.PlayerInControl();
                    thirdPersonCameraMovement.Sliding(false);

                    isMoving = true;
                    TargetFollow = endTarget.transform.GetChild(0).transform;
        
                    //switchDirection(true, false);
                }
                else
                {
                    isMoving = true;
                }
            }

            if(isMoving)
            {
                launchCharacter(TargetFollow);
            }
        }

    }

    private bool IsOnMesh(Vector3 position)
    {
        // Check if the given position is on the mesh by performing a raycast from above and checking if it hits the mesh collider

            Ray ray = new Ray(position + Vector3.up * 5f, Vector3.down);
            RaycastHit hit;
            if (meshCollider.Raycast(ray, out hit, 10f))
            {
                return true;
            }
        return false;
    }

    public void switchDirection(bool goToStart, bool goToEnd)
    {
        moveToStart = goToStart;
        moveToEnd = goToEnd;

        if(moveToStart && moveToEnd)
        {
            moveToStart = false;
            moveToEnd = true;
        }
    }

    public void moveInDirection()
    {
        Quaternion targetRotation = Quaternion.identity;
    
        if(Input.GetKey(KeyCode.RightArrow))
        {
            targetRotation = Quaternion.Euler(0, 0, -25);
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            targetRotation = Quaternion.Euler(0, 0, 25);
        }
        
        playerController.gameObject.transform.rotation = Quaternion.Lerp(playerController.gameObject.transform.rotation, targetRotation, Time.deltaTime * 5f);
    }

    public void launchCharacter(Transform direction)
    {
        if (isMoving)
        {
            if(direction != null)
            {
                float distance = Vector3.Distance( new Vector3(playerController.gameObject.transform.position.x,0,playerController.gameObject.transform.position.z), new Vector3(direction.position.x,0, direction.position.z));
                if (distance > stopDistance)
                {
                    playerController.gameObject.transform.position = Vector3.Lerp(playerController.gameObject.transform.position, direction.position, Time.deltaTime * 1f);
                }
                else
                {
                    isMoving = false;
                }
            }
            else
            {
                return;
            }
        }
    }
}
