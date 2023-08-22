using System.Collections;
using UnityEngine;

public class enemyMoveAI : MonoBehaviour
{
    public float minWaitTime = 1f;
    public float maxWaitTime = 3f;
    // Store the current speed and target speed
    private float currentMoveSpeed;
    private float targetMoveSpeed;

    // The duration over which to interpolate the speed change
    public float speedChangeDuration = 1.0f;

    // Variable to track the elapsed time during speed change
    private float speedChangeTimer = 0.0f;
    private float initialMoveSpeed;
    public float moveSpeed = 5f;
    public GameObject lightSpeedPrefab;
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public Transform fireProjectileLocation;
    public float fireRange = 10f; // The range within which the enemy can fire at the player

    private Vector3 targetPosition;
    private bool isMoving = false;
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Rock Player");
        if(player != null)
        {
            // Start the movement coroutine
            StartCoroutine(MoveObject());
        }
        initialMoveSpeed = moveSpeed;
    }

    private IEnumerator MoveObject()
    {
        while (true)
        {
            // Wait for a random period of time
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));

            if(player != null)
            {
                // Set a new random target position on the x and z axis
                targetPosition = player.transform.position + new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
            }

            // Start moving towards the target position
            isMoving = true;

            // Wait until the object reaches the target position
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                // Move towards the target position
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            // Stop moving
            isMoving = false;

            // Calculate the distance between the enemy and the player
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            // If the player is within the fire range, fire projectile
            if (distanceToPlayer <= fireRange)
            {
                FireProjectile();
            }
        }
    }

    private void FireProjectile()
    {
        // Calculate the direction towards the player
        Vector3 direction = (player.transform.position - transform.position).normalized;

        // Instantiate the projectile prefab at the current position
        GameObject projectile = Instantiate(projectilePrefab, fireProjectileLocation.position, Quaternion.identity);

        // Set the velocity of the projectile
        Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.velocity = direction * projectileSpeed;
    }

    private void Update()
    {
        if(player != null)
        {
            if (isMoving)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

                // Check if the distance to the player is greater than or equal to the fire range
                if (distanceToPlayer >= fireRange)
                {
                    // Set the target speed to double the move speed
                    targetMoveSpeed = initialMoveSpeed * 2f;

                    // Instantiate the light speed particle
                    GameObject lightSpeedParticle = Instantiate(lightSpeedPrefab, fireProjectileLocation.position, fireProjectileLocation.rotation);

                    // Reset the speed change timer
                    speedChangeTimer = 0.0f;
                }
                else
                {
                    // Set the target speed to the initial move speed
                    targetMoveSpeed = initialMoveSpeed;
                }

                // Gradually interpolate the current speed towards the target speed
                if (currentMoveSpeed != targetMoveSpeed)
                {
                    speedChangeTimer += Time.deltaTime;
                    float t = Mathf.Clamp01(speedChangeTimer / speedChangeDuration);
                    currentMoveSpeed = Mathf.Lerp(currentMoveSpeed, targetMoveSpeed, t);
                }

                // Update the movement speed
                moveSpeed = currentMoveSpeed;

                transform.LookAt(player.transform);
                Debug.DrawLine(transform.position, targetPosition, Color.red);
            }
        }
    }
}
