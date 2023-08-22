using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pillarMover : MonoBehaviour
{
    public float speed = 2.0f; // speed of movement
    public float distance = 3.0f; // distance to move on x-axis
    public Vector3 startPosition; // starting position

    private float direction = 1.0f; // direction of movement

    private int decideToMove;

    void Start()
    {
        startPosition = transform.position; // set starting position
        speed = Random.Range(-speed,speed);
        decideToMove = Random.Range(0,2);
    }

    void Update()
    {
        if(decideToMove == 1)
        {
            // calculate new position
            float newPosition = Mathf.PingPong(Time.time * speed, distance);
            transform.position = new Vector3(startPosition.x, transform.position.y, startPosition.z) + Vector3.right * newPosition * direction;

            // change direction if needed
            if (newPosition == 0.0f || newPosition == distance)
            {
                direction = -direction;
            }
        }
    }
}
