using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDistance : MonoBehaviour
{
    private float dist = 60f;

    private GameObject player;

    private void Start()
    {   
        player = GameObject.Find("Ghost Player");
    }

    private void FixedUpdate()
    {

        dist = Vector3.Distance(player.transform.position, transform.position);

        if (dist > 300f)
        {
            Destroy(gameObject);
        }
    }
}
