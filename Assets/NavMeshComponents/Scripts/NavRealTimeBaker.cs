using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class NavRealTimeBaker : MonoBehaviour
{
    // Start is called before the 
    [SerializeField]
    NavMeshSurface[] navMeshBlocks;

    void Start()
    {
        Invoke("BakeLevel",5f);
    }

    public void BakeLevel()
    {
        for(int i = 0; i < navMeshBlocks.Length; i++)
        {
            try
            {
                navMeshBlocks[i].BuildNavMesh();
            }
            catch(Exception e)
            {
                Debug.Log("That Cannot Bake Right Now");
            }
        }
    }
}
