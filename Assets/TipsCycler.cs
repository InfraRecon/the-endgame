using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsCycler : MonoBehaviour
{
    public GameObject[] objectsToCycle; // Array of GameObjects to cycle through
    public float cycleInterval = 3.0f; // Time interval between cycles
    private int currentIndex = 0; // Current index in the array

    private float timer = 0.0f;

    private void Start()
    {
        if (objectsToCycle.Length == 0)
        {
            Debug.LogWarning("No objects to cycle through.");
            enabled = false; // Disable the script if there are no objects
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= cycleInterval)
        {
            CycleObjects();
            timer = 0.0f;
        }
    }

    private void CycleObjects()
    {
        // Deactivate the current object
        if (currentIndex < objectsToCycle.Length)
            objectsToCycle[currentIndex].SetActive(false);

        // Move to the next object
        currentIndex = (currentIndex + 1) % objectsToCycle.Length;

        // Activate the next object
        objectsToCycle[currentIndex].SetActive(true);
    }
}

