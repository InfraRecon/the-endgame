using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flickerGameObjectMaterial : MonoBehaviour
{
    public Material flickerMaterial; // The material to be flickered
    public Material otherflickerMaterial; // The material to be flickered
    public float flickerDuration = 1f; // The duration of the flicker effect in seconds
    public float flickerInterval = 0.1f; // The interval between flickers in seconds

    private float flickerTimer = 0f; // A timer that keeps track of the progress of the flicker effect
    private bool isFlickering = false; // A boolean that keeps track of whether the flicker effect is currently active
    private Renderer rendererComponent; // The Renderer component of the GameObject
    public bool automaticFlicker = false;
    void Start()
    {
        rendererComponent = GetComponent<Renderer>(); // Get the Renderer component of the GameObject
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || automaticFlicker)
        {
            // Start the flicker effect
            isFlickering = true;
            flickerTimer = 0f;
            rendererComponent.material = null;
        }

        if (isFlickering || automaticFlicker)
        {
            flickerTimer += Time.deltaTime; // Increment the flicker timer

            if (flickerTimer >= flickerDuration)
            {
                // End the flicker effect and reset the material of the Renderer component
                isFlickering = false;
                flickerTimer = 0f;
                rendererComponent.material = flickerMaterial;
            }
            else if (flickerTimer % flickerInterval < flickerInterval / 2f)
            {
                // Flicker the material by assigning it to the null material
                rendererComponent.material = otherflickerMaterial;
            }
            else
            {
                // Restore the original material of the Renderer component
                rendererComponent.material = flickerMaterial;
            }
        }
    }

    public void StartFlickerEffect()
    {
        // Start the flicker effect
        isFlickering = true;
        flickerTimer = 0f;
        rendererComponent.material = null;
    }
}
