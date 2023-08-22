using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getCenterPoint : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform[] followPoints;
    [SerializeField] private float followDistance = 1f;
    [SerializeField] private float followSpeed = 5f;

    private Transform currentPoint;

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player not assigned!");
            return;
        }

        if (followPoints == null || followPoints.Length == 0)
        {
            Debug.LogError("Follow points not assigned!");
            return;
        }

        currentPoint = followPoints[0];
    }

    private void FixedUpdate()
    {
        if (currentPoint == null)
        {
            return;
        }

        float distance = Vector3.Distance(player.position, currentPoint.position);

        if (distance <= followDistance)
        {
            int nextIndex = (System.Array.IndexOf(followPoints, currentPoint) + 1) % followPoints.Length;
            currentPoint = followPoints[nextIndex];
        }

        float step = followSpeed * Time.deltaTime;
        Vector3 newPosition = Vector3.MoveTowards(transform.position, currentPoint.position, step);
        transform.position = new Vector3(newPosition.x, transform.position.y, transform.position.z);
    }
}
