using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noiseOnPlayerMove : MonoBehaviour
{
    public float speed = 1.0f;
    public float magnitude = 1.0f;
    public Vector3 direction = Vector3.up;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow) || 
        Input.GetKey(KeyCode.RightArrow) ||
        Input.GetKey(KeyCode.LeftArrow) ||
        Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 noise = new Vector3(Mathf.PerlinNoise(Time.time * speed, 0), Mathf.PerlinNoise(Time.time * speed, 1), Mathf.PerlinNoise(Time.time * speed, 2));
            Vector3 movement = direction + (noise * 2.0f - Vector3.one) * magnitude;

            transform.position = startPosition + movement;
        }
    }
}
