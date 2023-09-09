using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleCollisionSystem : MonoBehaviour
{
    private ParticleSystem part;
    private ThirdPersonCameraMovement thirdPersonCameraMovement;
    private gameCounters counters;
    private toggleUI tUI;

    public GameObject explosionBoxOnDetection;
    public GameObject souls;

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

        else if(other.layer == 14 || other.layer == 8 || other.layer == 20)
        {
            if(other.layer == 8)
            {
                Instantiate(souls, other.transform.position,Quaternion.identity); 
            }
            else if(other.layer == 20)
            {
                Instantiate(explosionBoxOnDetection, other.transform.position,Quaternion.identity); 
            }
            Destroy(other);
        }
    }

}

