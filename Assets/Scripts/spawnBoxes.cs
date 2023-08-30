using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBoxes : MonoBehaviour
{
    public GameObject[] prefabToInstantiate; // The prefab to be instantiated
    private GameObject boxObject;
    public int numberOfPrefabsToInstantiate; // The number of prefabs to be instantiated
    public float minSpawnPositionX; // The minimum position at which prefabs can spawn
    public float maxSpawnPositionX; // The maximum position at which prefabs can spawn
    public float minSpawnPositionY; // The minimum position at which prefabs can spawn
    public float maxSpawnPositionY; // The maximum position at which prefabs can spawn
    public float minSpawnPositionZ; // The minimum position at which prefabs can spawn
    public float maxSpawnPositionZ; // The maximum position at which prefabs can spawn

    void Start()
    {
        numberOfPrefabsToInstantiate = Random.Range(numberOfPrefabsToInstantiate/2,numberOfPrefabsToInstantiate);
        // Instantiate the prefabs randomly within the designated space
        for (int i = 0; i < numberOfPrefabsToInstantiate; i++)
        {
            // Generate a random position within the designated space
            float x = Random.Range(minSpawnPositionX, maxSpawnPositionX);
            float y = Random.Range(minSpawnPositionY, maxSpawnPositionY);
            float z = Random.Range(minSpawnPositionZ, maxSpawnPositionZ);
            Vector3 randomSpawnPosition = new Vector3(x, y, z);

            int prefabNum = Random.Range(0, prefabToInstantiate.Length-1);

            // Instantiate the prefab at the random position
            boxObject = Instantiate(prefabToInstantiate[prefabNum], 
            new Vector3(transform.position.x + randomSpawnPosition.x,
            transform.position.y + randomSpawnPosition.y, 
            transform.position.z + randomSpawnPosition.z), transform.rotation);

            boxObject.transform.parent = transform;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a gizmo to visualize the box cast in the Unity editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(maxSpawnPositionX*2, maxSpawnPositionY*2, maxSpawnPositionZ*2));
    }
}
