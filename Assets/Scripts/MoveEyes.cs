using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEyes : MonoBehaviour
{
    public float moveIntervalMin = 1f;
    public float moveIntervalMax = 5f;
    public float moveDistance = 1f;

    private float moveTimer = 0f;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float moveSpeed;

    //

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.localPosition;
        moveTimer = Random.Range(moveIntervalMin, moveIntervalMax);
        targetPosition = GetRandomPosition();
        moveSpeed = Random.Range(1f, 5f); // Random speed between 1 to 5 units per second
    }

    // Update is called once per frame
    void Update()
    {
        moveTimer -= Time.deltaTime;

        if (moveTimer <= 0f)
        {
            moveTimer = Random.Range(moveIntervalMin, moveIntervalMax);
            targetPosition = GetRandomPosition();
            moveSpeed = Random.Range(1f, 5f); // Random speed between 1 to 5 units per second
        }

        // Move towards the target position with Lerp
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(targetPosition.x,targetPosition.y,0), Time.deltaTime * moveSpeed);
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitCircle.normalized;
        Vector3 newPosition = startPosition + transform.TransformDirection(randomDirection) * moveDistance;

        return newPosition;
    }
}