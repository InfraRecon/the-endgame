using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class renderObjects : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach(GameObject go in allObjects)
        {
            if (go.activeInHierarchy)
            {
                if(go.TryGetComponent<MeshCollider>(out var meshCollider))
                {
                    if(go.TryGetComponent<MeshRenderer>(out var meshRenderer))
                    {
                        if(go.TryGetComponent<MeshFilter>(out var meshFilter))
                        {
                            meshRenderer.enabled = false;
                        }
                    }
                }
            }
        }
    }
}
