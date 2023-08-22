using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleCollisionSystem : MonoBehaviour
{
    private ParticleSystem part;
    private ThirdPersonCameraMovement thirdPersonCameraMovement;
    private gameCounters counters;
    private toggleUI tUI;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        counters = GameObject.Find("Game Manager").GetComponent<gameCounters>();
        tUI = GameObject.Find("Lives").GetComponent<toggleUI>();
    }

    void OnParticleCollision(GameObject other)
    {
        if(other.layer == 3)
        {
            thirdPersonCameraMovement = other.GetComponent<ThirdPersonCameraMovement>();
            counters.updateRevives(-1);
            tUI.triggerUI();
            // Destroy(other);
        }
    }

}

