using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class renderObjects : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        // foreach(GameObject go in allObjects)
        // {
        //     if (go.activeInHierarchy)
        //     {
        //         if(go.TryGetComponent<MeshCollider>(out var meshCollider))
        //         {
        //             if(go.TryGetComponent<MeshRenderer>(out var meshRenderer))
        //             {
        //                 if(go.TryGetComponent<MeshFilter>(out var meshFilter))
        //                 {
        //                     meshRenderer.enabled = false;
        //                 }
        //             }
        //         }
        //     }
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider belongs to the player or some specific tag
        if (other.gameObject.layer == 22)
        {
            GameObject targetObject = other.gameObject;
            MeshRenderer meshRenderer;
            meshRenderer = targetObject.GetComponent<MeshRenderer>();
            // Activate the MeshRenderer
            meshRenderer.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the entering collider belongs to the player or some specific tag
        if (other.gameObject.layer == 22)
        {
            GameObject targetObject = other.gameObject;
            MeshRenderer meshRenderer;
            meshRenderer = targetObject.GetComponent<MeshRenderer>();
            // DE Activate the MeshRenderer
            meshRenderer.enabled = false;
        }
    }
}
