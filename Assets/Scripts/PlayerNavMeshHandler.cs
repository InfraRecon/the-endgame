using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNavMeshHandler : MonoBehaviour
{
    private GameObject playerObject;
    private bool playerInControl = true;
    private UnityEngine.AI.NavMeshAgent agent; // Reference to the NavMeshAgent component

    void Start()
    {
        playerObject = GameObject.Find("Ghost Player");
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); // Get the NavMeshAgent component
    }

    void Update()
    {
        if(playerInControl)
        {
            transform.position = playerObject.transform.position;    
        }
        else
        {
            playerObject.transform.position = new Vector3(transform.position.x, playerObject.transform.position.y,transform.position.z);
            playerObject.transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y,playerObject.transform.eulerAngles.z);
        }
    }

    public void EnableAgent()
    {
        agent.enabled = true; // Disable the NavMeshAgent component
    }

    public void DisableAgent()
    {
        agent.enabled = false; // Disable the NavMeshAgent component
    }

    public void PlayerInControl()
    {
        playerInControl = true;
    }

    public void PlayerRailGrinding()
    {
        playerInControl = false;
    }
}
