using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnPlayerOnTrigger : MonoBehaviour
{
    private Transform playerLoc;
    public Transform respawnLocation;

    void Start()
    {
        playerLoc = GameObject.Find("Ghost Player").transform;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerLoc.position = respawnLocation.position;
        }
    }
}
