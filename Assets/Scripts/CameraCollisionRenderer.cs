using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CameraCollisionRenderer : MonoBehaviour
{
    private Renderer rend;
    void OnTriggerEnter(Collider obj) 
    {
        try 
        {
           obj.TryGetComponent<Renderer>(out Renderer meshrend);
           rend = meshrend;
        }
        catch (Exception e) 
        {
            Debug.Log("Can't Access Render");
        }   

        if(rend != null)
        {
            rend.enabled = true;
        }
    }

    void OnTriggerExit(Collider obj) 
    {
        try 
        {
            obj.TryGetComponent<Renderer>(out Renderer meshrend);
            rend = meshrend;
        }
        catch (Exception e) 
        {
            Debug.Log("Can't Access Render");
        }  


        if(rend != null)
        {
            rend.enabled = false;
        }
    }
}
