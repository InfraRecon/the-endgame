using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleUI : MonoBehaviour
{

    public Transform targetPosition; // The position the object will move towards
    public float moveSpeed = 5f; // The speed at which the object will move
    public float waitTime = 2f; // The amount of time the object will wait before returning to its original position

    private Vector3 startPosition; // The object's original position
    private bool isMoving = false; // Flag to indicate whether the object is currently moving
    private bool triggeredUI = false;

    public GameObject menu;

    private void Start()
    {
        startPosition = transform.position; // Store the object's original position
    }

    private void Update()
    {
        // Check if the user has triggered the movement and the object is not already moving
        if(menu != null)
        {    
            if(menu.activeSelf && menu.transform.parent.transform.gameObject.activeSelf)
            {
                triggerUI();
            }
        } 

        if (triggeredUI && !isMoving)
        {
            // Start the movement coroutine
            StartCoroutine(MoveToTarget());
        }
    }

    public void triggerUI()
    {
        triggeredUI = true;
    }

    private IEnumerator MoveToTarget()
    {
        isMoving = true;

        // Move the object towards the target position
        while (Vector3.Distance(transform.position, targetPosition.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        triggeredUI = false;

        // Wait for the specified amount of time
        yield return new WaitForSeconds(waitTime);

        // If the user has not triggered the movement again, move the object back to its original position
        if (!triggeredUI)
        {
            while (Vector3.Distance(transform.position, startPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }

        isMoving = false;
    }
}
