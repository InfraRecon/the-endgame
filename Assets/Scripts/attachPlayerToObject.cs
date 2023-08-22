using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attachPlayerToObject : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.gameObject.transform.SetParent(transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.gameObject.transform.SetParent(null);
        }
    }
}
