using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addNoiseToGameObject : MonoBehaviour
{
    public float magnitude = 0.1f;  // the magnitude of the noise
    public float speed = 1f;        // the speed of the noise
    public float offset = 0f;       // the starting offset of the noise

    private Vector3 initialPosition; // the initial position of the object
    private float noiseOffset;       // the current offset of the noise

    // void Start()
    // {
    //     initialPosition = transform.position;
    //     noiseOffset = offset;
    // }

    void Update()
    {
        initialPosition = transform.position;
        noiseOffset = offset;

        // add noise to the object's position
        Vector3 noise = new Vector3(
            Mathf.PerlinNoise(noiseOffset, 0f) * 2f - 1f,
            Mathf.PerlinNoise(0f, noiseOffset) * 2f - 1f,
            Mathf.PerlinNoise(noiseOffset, noiseOffset) * 2f - 1f
        ) * magnitude;
        transform.position = initialPosition + noise;

        // update the noise offset based on time
        noiseOffset += speed * Time.deltaTime;
    }
}
