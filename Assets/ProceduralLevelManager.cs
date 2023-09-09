using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralLevelManager : MonoBehaviour
{
    public GameObject[] ProceduralLevels;

    public void SpawnLevel(int levelNumber)
    {

        foreach (Transform child in transform) 
        {
            Destroy(child.gameObject);
        }

        GameObject level = Instantiate(ProceduralLevels[levelNumber], transform.position, Quaternion.identity, transform);
    }
}
