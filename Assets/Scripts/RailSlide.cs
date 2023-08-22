using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailSlide : MonoBehaviour
{
    public GameObject playerObject; // The game object we want to move on the mesh
    public Transform desiredPoint; // The point on the mesh we want to move the player object towards
    public float movementSpeed = 5f; // The speed at which the player object moves on the mesh

    private Collider meshCollider; // The collider for the mesh we're moving on

    private void Start()
    {
        meshCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        // Check if the player object is on the mesh
        if (IsOnMesh(playerObject.transform.position))
        {
            // Calculate the direction towards the desired point on the mesh
            Vector3 direction = desiredPoint.position - playerObject.transform.position;

            // Normalize the direction and move the player object in that direction
            playerObject.transform.position += direction.normalized * movementSpeed * Time.deltaTime;

            // Ensure that the player object is always on the mesh while moving towards the desired point
            Vector3 closestPointOnMesh = meshCollider.ClosestPoint(playerObject.transform.position);
            playerObject.transform.position = new Vector3(closestPointOnMesh.x, playerObject.transform.position.y, closestPointOnMesh.z);
        }
    }

    private bool IsOnMesh(Vector3 position)
    {
        // Check if the given position is on the mesh by performing a raycast from above and checking if it hits the mesh collider
        Ray ray = new Ray(position + Vector3.up * 5f, Vector3.down);
        RaycastHit hit;
        if (meshCollider.Raycast(ray, out hit, 10f))
        {
            return true;
        }
        return false;
    }
}
