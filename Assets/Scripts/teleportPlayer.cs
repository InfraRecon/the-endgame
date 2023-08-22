using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding game object is the player
        if (other.gameObject.layer == 10)
        {
            // Teleport the player to the desired location
            transform.position = other.gameObject.transform.GetChild(0).position;
        }
    }
}
