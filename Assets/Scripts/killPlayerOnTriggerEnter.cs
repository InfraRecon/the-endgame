using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killPlayerOnTriggerEnter : MonoBehaviour
{
    private ThirdPersonCameraMovement thirdPersonCameraMovement;
    private GameObject PlayerObject;

    private void Start()
    {
        PlayerObject = GameObject.Find("Ghost Player");
        thirdPersonCameraMovement = GameObject.Find("Ghost Player").GetComponent<ThirdPersonCameraMovement>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            // Destroy(other.gameObject);
            thirdPersonCameraMovement.playerDead(true);
            thirdPersonCameraMovement.enabled = false;
            Destroy(gameObject,5f);
            //Instantiate(explosionBoxOnDetection, other.gameObject.transform.position,explosionBoxOnDetection.transform.rotation);
        }
    }

     private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            thirdPersonCameraMovement.enabled = true;
            thirdPersonCameraMovement.playerDead(false);
            //Instantiate(explosionBoxOnDetection, other.gameObject.transform.position,explosionBoxOnDetection.transform.rotation);
        }
    }
}
