using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pulseGameObject : MonoBehaviour
{
    public float pulseDuration = 1f; // The duration of the pulse effect in seconds
    public float maxScaleMultiplier = 1.5f; // The maximum scale multiplier that the GameObject should reach during the pulse effect

    private Vector3 originalScale; // The original scale of the GameObject
    private bool isPulsing = false; // A boolean that keeps track of whether the pulse effect is currently active
    private float pulseTimer = 0f; // A timer that keeps track of the progress of the pulse effect
    public bool autoPulse = false;

    void Start()
    {
        originalScale = transform.localScale; // Store the original scale of the GameObject
    }

    void Update()
    {
        if(autoPulse)
        {
            if(isPulsing == false)
            {
                isPulsing = true;
            }
        }

        if (isPulsing)
        {
            pulseTimer += Time.deltaTime; // Increment the pulse timer

            // Calculate the new scale based on the pulse timer
            float t = Mathf.Clamp01(pulseTimer / pulseDuration);
            float scaleMultiplier = Mathf.Lerp(1f, maxScaleMultiplier, Mathf.Sin(t * Mathf.PI));

            // Update the scale of the GameObject
            transform.localScale = originalScale * scaleMultiplier;

            if (pulseTimer >= pulseDuration)
            {
                // End the pulse effect and reset the scale of the GameObject
                isPulsing = false;
                pulseTimer = 0f;
                transform.localScale = originalScale;
            }
        }
    }

    public void Pulse()
    {
        // Start the pulse effect
        isPulsing = true;
        pulseTimer = 0f;
    }
}
