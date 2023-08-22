using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableScriptAfterDelay : MonoBehaviour
{
    public flickerGameObjectMaterial[] scriptToEnable;
    public float delay = 5f;

    private void Start()
    {
        Invoke("EnableScript", delay);
    }

    private void EnableScript()
    {
        for (var i = 0; i < scriptToEnable.Length; i++)
        {
            scriptToEnable[i].enabled = true;
        }
    }
}
