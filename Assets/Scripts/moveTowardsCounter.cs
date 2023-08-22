using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTowardsCounter : MonoBehaviour
{
    private Transform targetSoulTransform; // The target location that the GameObject should move towards
    private Transform targetBoxTransform; // The target location that the GameObject should move towards
    private Transform targetLivesTransform; // The target location that the GameObject should move towards
    private Transform targetGemTransform; // The target location that the GameObject should move towards
    public bool goToTargetSoul = false;
    public bool goToTargetBox = false;
    public bool goToTargetLives = false;
    public bool goToTargetGem = false;
    
    public float moveSpeed = 1f; // The speed at which the GameObject should move
    public float sinSpeed = 1f; // The speed at which the sin motion should oscillate
    public float sinMagnitude = 0.5f; // The maximum distance that the sin motion should move the GameObject

    private Vector3 startPosition; // The starting position of the GameObject
    private float sinOffset; // The sin offset that should be added to the movement

    private bool isMoving = false; // A boolean that keeps track of whether the GameObject is currently moving

    private Rigidbody rigBod;

    private AudioSource audioSource;

    void Start()
    {
        startPosition = transform.position; // Store the starting position of the GameObject
        targetSoulTransform = GameObject.Find("3D Soul Counter").transform;
        targetBoxTransform = GameObject.Find("3D Box Counter").transform;
        targetLivesTransform = GameObject.Find("3D Lives Counter").transform;
        targetGemTransform = GameObject.Find("3D Gem Counter").transform;
        rigBod = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (!isMoving) // Only trigger the movement if the GameObject is not already moving
            {
                StartCoroutine(MoveCoroutine()); // Start the movement coroutine
                rigBod.isKinematic = true;
                rigBod.useGravity = false;
                audioSource.pitch = Random.Range(1.1f,1.5f);
                audioSource.Play();
            }
        }
    }

    IEnumerator MoveCoroutine()
    {
        isMoving = true; // Set the isMoving boolean to true
        sinOffset = Random.Range(0f, 2f * Mathf.PI); // Generate a random sin offset

        float t = 0f; // A variable that keeps track of the movement progress
        while (t < 1f) // Move the GameObject towards the target location
        {
            t += Time.deltaTime * moveSpeed;
            float sin = Mathf.Sin((t + sinOffset) * sinSpeed) * sinMagnitude; // Calculate the sin motion
            if(goToTargetSoul)
            {
                transform.position = Vector3.Lerp(startPosition, targetSoulTransform.position, t) + sin * Vector3.up; // Move the GameObject with the sin motion
            }
            else if(goToTargetBox)
            {
                transform.position = Vector3.Lerp(startPosition, targetBoxTransform.position, t) + sin * Vector3.up; // Move the GameObject with the sin motion
            }
            else if(goToTargetLives)
            {
                transform.position = Vector3.Lerp(startPosition, targetLivesTransform.position, t) + sin * Vector3.up; // Move the GameObject with the sin motion
            }
            else if(goToTargetGem)
            {
                transform.position = Vector3.Lerp(startPosition, targetGemTransform.position, t) + sin * Vector3.up; // Move the GameObject with the sin motion
            }
            yield return null;
        }

        isMoving = false; // Set the isMoving boolean back to false
    }
}
