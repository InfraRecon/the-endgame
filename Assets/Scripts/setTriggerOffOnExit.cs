using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setTriggerOffOnExit : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            gameObject.transform.GetChild(0).gameObject.GetComponent<Collider>().isTrigger = false;
        }
    }
}
