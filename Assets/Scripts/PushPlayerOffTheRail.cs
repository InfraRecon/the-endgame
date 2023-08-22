using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPlayerOffTheRail : MonoBehaviour
{
    private CharacterController controller; // Reference to the CharacterController component
    public float speed = 5f; // Movement speed

    private GameObject playerObject; // The game object we want to move on the mesh
    private PlayerNavMeshHandler playerNavMeshHandler;

    private bool canMove = false; // Flag to determine if the character can move

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") // Check if the trigger has the "Trigger" tag
        {
            canMove = true; // Set the canMove flag to true
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") // Check if the trigger has the "Trigger" tag
        {
            canMove = false; // Set the canMove flag to false
        }
    }

    void Start()
    {
        playerObject = GameObject.Find("Ghost Player");
        playerNavMeshHandler = playerObject.GetComponent<PlayerNavMeshHandler>();
        controller = playerObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        if (canMove) // Check if the character can move
        {
            playerNavMeshHandler.DisableAgent();
            Vector3 moveDirection = transform.forward * speed; // Calculate the movement direction
            controller.Move(moveDirection * Time.deltaTime); // Move the character
        }
    }
}
