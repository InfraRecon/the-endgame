using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyBoxRotator : MonoBehaviour
{
    public Material otherSkybox; // assign via inspector
    public float skyRotationSpeed = 0.1f;

    void Update()
    {
        otherSkybox.SetFloat("_Rotation", Time.time * skyRotationSpeed);
    }
}
