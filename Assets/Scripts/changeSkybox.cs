using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeSkybox : MonoBehaviour
{
  
    public Material otherSkybox; // assign via inspector
    public Cubemap[] textures;

    void Start()
    {
        otherSkybox.SetTexture("_Tex",textures[0]);
    }

    public void changeSkyBox(int boxNum)
    {
        otherSkybox.SetTexture("_Tex",textures[boxNum]);
    } 
}
