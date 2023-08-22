using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkDistanceFromPlayer : MonoBehaviour
{
    private Transform player;
    private void Start()
    {
        player = GameObject.Find("Ghost Player").transform;
    }

    private void FixedUpdate()
    {
    
        float dist = Vector3.Distance(player.position, transform.position);

        if (dist > 120f)
        {
            Destroy(gameObject);
        }
    }
}
