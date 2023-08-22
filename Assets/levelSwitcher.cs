using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelSwitcher : MonoBehaviour
{
    //public int levelEnvironmentNumber = 1;
    private EnvironmentSpawnManager environmentSpawnManager;
    // Start is called before the first frame update
    void Start()
    {
        environmentSpawnManager = GameObject.Find("Environment Home Area").GetComponent<EnvironmentSpawnManager>();
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void switchToLevel(int levelEnvironmentNumber)
    {
            environmentSpawnManager.changeBlock(levelEnvironmentNumber);
            environmentSpawnManager.spawnONCall();
    }
}
