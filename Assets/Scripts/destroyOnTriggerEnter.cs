using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOnTriggerEnter : MonoBehaviour
{
    public bool DestroyTriggerer;
    public bool DestroyTriggered;

    public float destroyDelay = 1f;

    public int layerNumber = 12;
    
    private void OnTriggerEnter(Collider other)
    {
        if(DestroyTriggerer)
        {
            if(other.gameObject.layer == layerNumber)
            {
                Destroy(other.gameObject, destroyDelay);
                //Instantiate(explosionBoxOnDetection, other.gameObject.transform.position,explosionBoxOnDetection.transform.rotation);
            }
        }

        if(DestroyTriggered)
        {
            if(other.gameObject.layer == layerNumber)
            {
                Destroy(gameObject, destroyDelay);
                //Instantiate(explosionBoxOnDetection, other.gameObject.transform.position,explosionBoxOnDetection.transform.rotation);
            }
        }
    }
}
