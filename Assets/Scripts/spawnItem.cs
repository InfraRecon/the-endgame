using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnItem : MonoBehaviour
{
    public GameObject Boulder;
    public Transform spawnLocation;

    private bool spawnedItem = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !spawnedItem)
        {
            Instantiate(Boulder, spawnLocation.position,spawnLocation.rotation);
            spawnedItem = true;
        }
    }
}
