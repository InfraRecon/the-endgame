using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class centerGameObjectToMesh : MonoBehaviour
{
    public Transform raycastOriginsLeft;
    public Transform raycastOriginsRight;
    public LayerMask raycastLayers;
    public float raycastDistance = 1f;
    public Color raycastColor = Color.white;
    public float smoothSpeed = 2.0f; // Adjust this value to control the smoothness of the movement.

    public Transform playerLocation;

    void Update()
    {
        // Loop through all the raycast origins and cast a ray
        Vector3 originLeft = raycastOriginsLeft.position;
        Vector3 originRight = raycastOriginsRight.position;
        Vector3 direction = -transform.up;
        RaycastHit hitInfoLeft;

        // Cast the ray and show debug information
        if (Physics.Raycast(originLeft, direction, out hitInfoLeft, raycastDistance, raycastLayers))
        {
            Debug.DrawRay(originLeft, direction * hitInfoLeft.distance, raycastColor);
            Debug.LogFormat("Raycast hit {0} at distance {1} from {2}", hitInfoLeft.collider.name, hitInfoLeft.distance, originLeft);
        }
        else
        {
            Debug.DrawRay(originLeft, direction * raycastDistance, raycastColor);

            //transform.position = new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z);
            MoveSmoothly(-1f);
        }
        
        RaycastHit hitInfoRight;
        if (Physics.Raycast(originRight, direction, out hitInfoRight, raycastDistance, raycastLayers))
        {
            Debug.DrawRay(originRight, direction * hitInfoRight.distance, raycastColor);
            Debug.LogFormat("Raycast hit {0} at distance {1} from {2}", hitInfoRight.collider.name, hitInfoRight.distance, originRight);
        }
        else
        {
            Debug.DrawRay(originRight, direction * raycastDistance, raycastColor);

            //transform.position = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
            MoveSmoothly(1f);
        }

        if(playerLocation.position.x > originLeft.x - 7f)
        {
            //transform.position = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
            MoveSmoothly(1f);
        }
        else if(playerLocation.position.x < originRight.x + 7f)
        {
            //transform.position = new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z);
            MoveSmoothly(-1f);
        }
    }

    private void MoveSmoothly(float sideNum)
    {
        Vector3 targetPosition = targetPosition = new Vector3(transform.position.x + sideNum, transform.position.y, transform.position.z);
        
        // Move the object towards the target position over time
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
