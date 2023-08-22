using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateRandomChild : MonoBehaviour
{
    public GameObject parentObject;
    public float activationInterval = 2f; // Time interval in seconds between activations

    private float nextActivationTime;

    private void Start()
    {
        if (parentObject == null)
        {
            Debug.LogError("Parent GameObject not assigned!");
            enabled = false; // Disable the script if the parent object is not assigned
            return;
        }

        // Set the initial activation time
        nextActivationTime = Time.time + activationInterval;
    }

    private void Update()
    {
        // Check if it's time for the next activation
        if (Time.time >= nextActivationTime)
        {
            ActivateRandomChild();
            nextActivationTime = Time.time + activationInterval;
        }
    }

    private void ActivateRandomChild()
    {
        int childCount = parentObject.transform.childCount;

        if (childCount > 0)
        {
            int randomChildIndex = Random.Range(0, childCount);
            Transform randomChild = parentObject.transform.GetChild(randomChildIndex);
            randomChild.gameObject.SetActive(true);
        }
    }
}
