using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeSkybox : MonoBehaviour
{
  
    public Material otherSkybox; // assign via inspector
    public Cubemap texture;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            otherSkybox.SetTexture("_Tex",texture);
        }
    }
}
