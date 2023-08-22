using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFogFollowBuffer : MonoBehaviour
{
    public Transform playerTransform;
    public float maxFollowDistance = 10.0f;
    public float followOffset = 2.0f;

    void Update()
    {
        float distanceToPlayer = Mathf.Abs(transform.position.z - playerTransform.position.z);
        if (distanceToPlayer > maxFollowDistance)
        {
            // Follow the player with an offset
            Vector3 targetPosition = playerTransform.position + new Vector3(0.0f, 0.0f, followOffset);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
        }
    }
}
