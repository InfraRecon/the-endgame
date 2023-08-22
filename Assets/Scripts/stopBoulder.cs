using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopBoulder : MonoBehaviour
{
   private Rigidbody rb;
   private rigidBodyObjectMove rbom;
   public Transform dustLocation;
   public GameObject dustParticle;

    private Rotater rotater;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            rb = other.gameObject.GetComponent<Rigidbody>();
            rbom = other.gameObject.GetComponent<rigidBodyObjectMove>();
            rotater = other.gameObject.GetComponent<Rotater>();
            Destroy(rotater);
            Destroy(rbom);
            Destroy(rb);
            
            Instantiate(dustParticle, dustLocation.position, Quaternion.identity);
        }
    }
}
