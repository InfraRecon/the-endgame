using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cullAssets : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
