using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleLandingCalculator : MonoBehaviour
{
 public ParticleSystem particleSystem;
    public GameObject landingObjectPrefab; // Prefab to instantiate at landing position
    public Vector3 gravity = new Vector3(0, -9.81f, 0); // Adjust gravity as needed

    private Transform particleSystemTransform; // Transform of the particle system

    private Queue<GameObject> landingObjectPool = new Queue<GameObject>(); // Object pool

    private void Start()
    {
        if (particleSystem == null)
        {
            Debug.LogError("Particle System not assigned!");
            return;
        }

        particleSystemTransform = particleSystem.transform;

        // Create initial pool of objects
        for (int i = 0; i < particleSystem.main.maxParticles; i++)
        {
            GameObject landingObject = Instantiate(landingObjectPrefab, particleSystemTransform);
            landingObject.SetActive(false);
            landingObjectPool.Enqueue(landingObject);
        }
    }

    private void Update()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
        int particleCount = particleSystem.GetParticles(particles);

        for (int i = 0; i < particleCount; i++)
        {
            if (landingObjectPool.Count > 0)
            {
                GameObject landingObject = landingObjectPool.Dequeue();
                landingObject.SetActive(true);

                Vector3 initialPosition = particleSystemTransform.TransformPoint(particles[i].position);
                Vector3 initialVelocity = particleSystemTransform.TransformDirection(particles[i].velocity);

                float timeToGround = CalculateTimeToGround(initialVelocity.y, initialPosition.y);
                Vector3 landingPosition = CalculateLandingPosition(initialPosition, initialVelocity, timeToGround);

                landingObject.transform.localPosition = landingPosition;
            }
        }

        // Deactivate unused objects in the pool
        foreach (var pooledObject in landingObjectPool)
        {
            pooledObject.SetActive(false);
        }
    }

    private float CalculateTimeToGround(float initialVerticalVelocity, float initialHeight)
    {
        // Calculate time to reach the ground using kinematic equations
        float timeToGround = Mathf.Sqrt((2 * initialHeight) / Mathf.Abs(gravity.y));
        return timeToGround;
    }

    private Vector3 CalculateLandingPosition(Vector3 initialPosition, Vector3 initialVelocity, float timeToGround)
    {
        if (float.IsNaN(initialPosition.x) || float.IsNaN(initialPosition.y) || float.IsNaN(initialPosition.z))
        {
            Debug.LogWarning("Initial position contains NaN values.");
            return initialPosition;
        }

        if (float.IsNaN(initialVelocity.x) || float.IsNaN(initialVelocity.y) || float.IsNaN(initialVelocity.z))
        {
            Debug.LogWarning("Initial velocity contains NaN values.");
            return initialPosition;
        }

        if (float.IsNaN(timeToGround))
        {
            Debug.LogWarning("Time to ground contains NaN value.");
            return initialPosition;
        }

        // Calculate landing position using kinematic equations
        Vector3 landingPosition = initialPosition + initialVelocity * timeToGround +
                                0.5f * gravity * timeToGround * timeToGround;

        if (float.IsNaN(landingPosition.x) || float.IsNaN(landingPosition.y) || float.IsNaN(landingPosition.z))
        {
            Debug.LogWarning("Calculated landing position contains NaN values.");
            return initialPosition;
        }

        return landingPosition;
    }
}

public class ParticleObjectController : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public int particleIndex;

    private void Update()
    {
        if (particleSystem != null)
        {
            ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
            int particleCount = particleSystem.GetParticles(particles);

            if (particleIndex < particleCount)
            {
                // Update object position to match particle position
                transform.position = particles[particleIndex].position;
            }
            else
            {
                // Particle no longer exists, destroy the object
                Destroy(gameObject);
            }
        }
    }
}



