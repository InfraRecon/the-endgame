using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearAllChildren : MonoBehaviour
{
    public void ClearChildren()
    {
        foreach (Transform child in transform) 
        {
            Destroy(child.gameObject);
        }
    }
}
