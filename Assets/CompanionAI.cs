using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionAI : MonoBehaviour
{
public Transform player; // Reference to the player object
    public float moveSpeed = 5f; // Speed at which the object moves
    public float rotationSpeed = 5f; // Speed at which the object rotates
    public float minDistance = 2f; // Minimum distance from the player
    public float maxDistance = 5f; // Maximum distance from the player
    public float originalIntervalMin = 2f; // Original minimum interval between movements
    public float originalIntervalMax = 5f; // Original maximum interval between movements
    public float intervalMin = 0f; // Original minimum interval between movements
    public float intervalMax = 0f; // Original maximum interval between movements
    public float farIntervalMin = 0.1f; // Interval between movements when the object is far from the player
    public float farIntervalMax = 0.1f; // Interval between movements when the object is far from the player

    private float nextMovementTime; // Time for the next movement
    private Vector3 targetPosition; // Position to move towards

    ///

    public float speed = 2f;           // Speed of the movement
    public float frequency = 1f;       // Frequency of the noise
    public float amplitude = 1f;       // Amplitude of the noise

    private void Start()
    {
        nextMovementTime = Time.time + Random.Range(originalIntervalMin, originalIntervalMax);
    }

    private void Update()
    {
        if (Time.time >= nextMovementTime)
        {
            CalculateNewTargetPosition();
            nextMovementTime = Time.time + Random.Range(originalIntervalMin, originalIntervalMax);
        }

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > maxDistance)
        {
            intervalMin = farIntervalMin;
            intervalMax = farIntervalMax;
        }
        else
        {
            intervalMin = originalIntervalMin;
            intervalMax = originalIntervalMax;
        }

        MoveTowardsTarget();
        RotateTowardsTarget();
        AddNoiseTowardsTarget();
    }

    private void CalculateNewTargetPosition()
    {
        float distance = Random.Range(minDistance, maxDistance);
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector3 playerPosition = player.position;

        if (player.parent != null)
        {
            // If the player is a child object, calculate the target position around its local position
            playerPosition += player.localPosition;
        }

        Vector3 newPosition = playerPosition + new Vector3(randomDirection.x, 0f, randomDirection.y) * distance;
        targetPosition = newPosition;
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    private void RotateTowardsTarget()
    {
        Vector3 direction = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void AddNoiseTowardsTarget()
    {
        // Calculate the noise values for X and Y axes
        float noiseX = Mathf.PerlinNoise(Time.time * frequency, 0f);
        float noiseY = Mathf.PerlinNoise(0f, Time.time * frequency);

        // Calculate the new position using the noise values
        float offsetX = (noiseX - 0.5f) * 2f * amplitude;
        float offsetY = (noiseY - 0.5f) * 2f * amplitude;
        Vector3 newPosition = transform.position + new Vector3(offsetX, offsetY, 0f);

        // Move the GameObject towards the new position smoothly using Lerp
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * speed);
    }
}
